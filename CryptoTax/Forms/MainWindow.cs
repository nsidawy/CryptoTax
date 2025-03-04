﻿using CryptoTax.Crypto;
using CryptoTax.Transactions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using CryptoTax.Utilities;
using System.ComponentModel;
using System.Diagnostics;
using static CryptoTax.Crypto.CryptoDataProvider;

namespace CryptoTax.Forms
{
    public partial class MainWindow : Form
    {
        private PriceInUsdProvider _priceInUsdProvider;
        private CryptoDataProvider _coinMarketCapDataProvider;
        private TaxCalculator _taxCalculator;
        private PortfolioSummaryProvider _portfolioSummaryProvider;
        private FormFactory _formFactory;
        private SaveFileReaderWriter _saveFileReaderWriter;

        private string _filename = null;

        private BindingList<Transaction> Transactions = new BindingList<Transaction>();
        private SortableBindingList<Transaction> TransactionDataGridBindingSource = new SortableBindingList<Transaction>();
        private SortableBindingList<PortfolioSummaryProvider.CryptoPortfolioSummaryInfo> SummaryDataGridBindingSource = new SortableBindingList<PortfolioSummaryProvider.CryptoPortfolioSummaryInfo>();
        private SortableBindingList<PortfolioSummaryProvider.CryptoYearSummaryInfo> YearSummaryDataGridBindingSource = new SortableBindingList<PortfolioSummaryProvider.CryptoYearSummaryInfo>();

        private List<CoinMarketCapData> CoinMarketCapData = new();

        public MainWindow(
            PriceInUsdProvider priceInUsdProvider,
            CryptoDataProvider coinMarketCapDataProvider,
            TaxCalculator taxCalculator,
            PortfolioSummaryProvider portfolioSummaryProvider,
            FormFactory formFactory,
            SaveFileReaderWriter saveFileReaderWriter)
        {
            InitializeComponent();

            this._priceInUsdProvider = priceInUsdProvider;
            this._coinMarketCapDataProvider = coinMarketCapDataProvider;
            this._taxCalculator = taxCalculator;
            this._portfolioSummaryProvider = portfolioSummaryProvider;
            this._formFactory = formFactory;
            this._saveFileReaderWriter = saveFileReaderWriter;
            
            this.SummaryDataRefreshTimer.Tick += async (object o, EventArgs e) => await this.UpdateSummaryData();
            this.SummaryDataRefreshTimer.Start();
               
            this.SetupDataGrids();
            this.SetupEventHandlers();

            this.KeyPreview = true;
            this.KeyDown += this.MainWindow_KeyDown;
        }

        private void SetupDataGrids()
        {
            // data sources
            this.SummaryDataGrid.DataSource = this.SummaryDataGridBindingSource;
            this.TransactionDataGrid.DataSource = this.TransactionDataGridBindingSource;
            this.YearSummaryDataGrid.DataSource = this.YearSummaryDataGridBindingSource;

            this.TransactionDataGrid.UserDeletingRow += (object o, DataGridViewRowCancelEventArgs e) =>
            {
                // don't let the normal delete row happen. The data grid will be updated when the item is removed from it binding list
                e.Cancel = true;
                this.Transactions.Remove((Transaction)e.Row.DataBoundItem);
            };
            this.TransactionDataGrid.MouseDown += this.TransactionDataGrid_Click;

            // setup formatting
            this.TransactionDataGrid.CellFormatting += this.TransactionDataGrid_CellFormatting;
            this.SummaryDataGrid.CellFormatting += this.SummaryDataGrid_CellFormatting;
            this.YearSummaryDataGrid.CellFormatting += this.YearSummaryDataGrid_CellFormatting;

            this.TransactionDataGrid.Sort(this.TransactionDataGrid.Columns[0], ListSortDirection.Descending);
            this.SummaryDataGrid.Sort(this.SummaryDataGrid.Columns[3], ListSortDirection.Descending);

            this.Transactions.ListChanged += this.TransactionsUpdated;
            
            var cryptocurrenyFilterInput = ((ToolStripComboBox)this.toolStrip2.Items["CryptoFilterInput"]);
            var noneOption = new NoneOption();
            cryptocurrenyFilterInput.Items.Add(noneOption);
            cryptocurrenyFilterInput.SelectedIndex = 0;
            cryptocurrenyFilterInput.SelectedIndexChanged += this.OnCryptoFilterInputChange;
            cryptocurrenyFilterInput.TextChanged += this.OnCryptoFilterInputChange;
        }

        private void SetupEventHandlers()
        {
            this.toolStrip1.Items["AddTransactionButton"].Click += this.AddTransactionButton_Click;
            this.toolStrip1.Items["EditTransactionButton"].Click += this.EditTransactionButton_Click;
            this.toolStrip1.Items["ImportTransactionsButton"].Click += this.ImportTransactionButtons_Click;
            this.toolStrip1.Items["SaveButton"].Click += this.SaveButton_Click;
            this.toolStrip1.Items["SaveAsButton"].Click += this.SaveAsButton_Click;
            this.toolStrip1.Items["OpenFileButton"].Click += this.OpenFileButton_Click;
        }
        
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.S)
            {
                this.SaveButton_Click(sender, e);
            }
            else if (e.Modifiers == (Keys.Control | Keys.Shift) && e.KeyCode == Keys.S)
            {
                this.SaveAsButton_Click(sender, e);
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.O)
            {
                this.OpenFileButton_Click(sender, e);
            }
        }

        private async void TransactionsUpdated(object sender, ListChangedEventArgs e)
        {
            await this.UpdateSummaryData();
            this.UpdateFiscalYearSummaryData();

            // update transactions grid source data
            this.SyncTransactionDataGrid();

            var cryptocurrenyFilterInput = ((ToolStripComboBox)this.toolStrip2.Items["CryptoFilterInput"]);
            var selectedItem = cryptocurrenyFilterInput.SelectedItem;
            cryptocurrenyFilterInput.SelectedIndexChanged -= this.OnCryptoFilterInputChange;
            cryptocurrenyFilterInput.Items.Clear();
            var noneOption = new NoneOption();
            cryptocurrenyFilterInput.Items.Add(noneOption);
            cryptocurrenyFilterInput.Items
                .AddRange(this.Transactions.Select(t => t.Crypto).Distinct().Cast<object>().ToArray());
            cryptocurrenyFilterInput.SelectedItem = noneOption;
            cryptocurrenyFilterInput.SelectedIndexChanged += this.OnCryptoFilterInputChange;
        }

        private void SyncTransactionDataGrid()
        {
            IEnumerable<Transaction> transactions = this.Transactions.ToList();
            var cryptocurrenyFilterInput = ((ToolStripComboBox)this.toolStrip2.Items["CryptoFilterInput"]);
            if (!(cryptocurrenyFilterInput.SelectedItem is NoneOption))
            {
                var cryptoFilter = (CryptoType)cryptocurrenyFilterInput.SelectedItem;
                transactions = transactions.Where(x => x.Crypto == cryptoFilter);
            }

            this.ReplaceBindingListItems(this.TransactionDataGridBindingSource, transactions.ToList());
            this.TransactionDataGridBindingSource.Resort();
        }

        private void UpdateFiscalYearSummaryData()
        {
            var transactions = this.Transactions
                .Where(x => !x.ExcludeFromPortfolio)
                .ToList();
            var yearSummaryInfos = this._portfolioSummaryProvider.GetCryptoYearSummaryInfo(transactions)
                .OrderBy(x => x.Crypto)
                .ThenBy(x => x.Year)
                .ToList();

            var firstVisibleRowIndex = this.YearSummaryDataGrid.FirstDisplayedScrollingRowIndex;
            this.ReplaceBindingListItems(this.YearSummaryDataGridBindingSource, yearSummaryInfos);
            this.YearSummaryDataGridBindingSource.Resort();
            // do this to maintain data grid scroll location after data rebinding
            if (firstVisibleRowIndex >= 0)
            {
                this.YearSummaryDataGrid.FirstDisplayedScrollingRowIndex = Math.Min(firstVisibleRowIndex, this.SummaryDataGrid.RowCount - 1);
            }
        }

        private async Task UpdateSummaryData()
        {
            var transactions = this.Transactions
                .Where(x => !x.ExcludeFromPortfolio)
                .ToList();

            var cryptos = transactions.Select(x => x.Crypto).Distinct().ToList();
            var newCoinMarketCapData = await this._coinMarketCapDataProvider.GetCryptoData(cryptos);
            if(newCoinMarketCapData != null)
            {
                this.CoinMarketCapData = newCoinMarketCapData;
            }
            var summaryInfos = (this._portfolioSummaryProvider.GetCryptoPortfolioSummaryInfo(transactions, this.CoinMarketCapData))
                .OrderByDescending(x => x.TotalUsd)
                .ToList();

            // update portfolio summary label
            var networth = summaryInfos.Aggregate((decimal)0, (v, s) => v + (s.TotalUsd ?? 0));
            this.SummaryLabel.Text = "Crypto Net Worth - $" + networth.ToString("N2");


            var selectedCells = new List<Tuple<int, int>>();
            for(var i = 0; i < this.SummaryDataGrid.SelectedCells.Count; i++)
            {
                selectedCells.Add(new Tuple<int, int>(this.SummaryDataGrid.SelectedCells[i].ColumnIndex, this.SummaryDataGrid.SelectedCells[i].RowIndex));
            }
            var firstVisiblRowIndex = this.SummaryDataGrid.FirstDisplayedScrollingRowIndex;
            if(firstVisiblRowIndex < 0)
            {
                firstVisiblRowIndex = 0;
            }
            this.ReplaceBindingListItems(this.SummaryDataGridBindingSource, summaryInfos);
            this.SummaryDataGridBindingSource.Resort();
            // do this to maintain data grid scroll location after data rebinding
            if (firstVisiblRowIndex >= 0 && this.SummaryDataGrid.RowCount > 0)
            {
                this.SummaryDataGrid.FirstDisplayedScrollingRowIndex = Math.Max(Math.Min(firstVisiblRowIndex, this.SummaryDataGrid.RowCount - 1), 0);
            }
            this.SummaryDataGrid.ClearSelection();
            foreach (var selectedCell in selectedCells)
            {
                this.SummaryDataGrid[selectedCell.Item1, selectedCell.Item2].Selected = true;
            }
        } 

        private void OnCryptoFilterInputChange(object sender, EventArgs e)
        {
            this.SyncTransactionDataGrid();
        }

        private class NoneOption
        {
            public override string ToString()
            {
                return "(None)";
            }
        }

        #region Toolstip button handlers

        private void AddTransactionButton_Click(object sender, EventArgs e)
        {
            using (var popup = this._formFactory.CreateForm<AddTransactionDialog>())
            {
                var result = popup.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.Transactions.Add(popup.Transaction);
                }
            }
        }

        private void EditTransactionButton_Click(object sender, EventArgs e)
        {
            if(this.TransactionDataGrid.SelectedRows.Count != 1)
            {
                MessageBox.Show("Select one row to edit.", string.Empty, MessageBoxButtons.OK);
                return;
            }
            var transactionToEdit = (Transaction)this.TransactionDataGrid.SelectedRows[0].DataBoundItem;
            using (var popup = this._formFactory.CreateForm<AddTransactionDialog>())
            {
                popup.SetToEditMode(transactionToEdit);
                var result = popup.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.Transactions.ResetBindings();
                }
            }
        }
        private void ImportTransactionButtons_Click(object sender, EventArgs e)
        {
            using (var popup = this._formFactory.CreateForm<ImportTransactionsDialog>())
            {
                var result = popup.ShowDialog();
                if (result == DialogResult.OK)
                {
                    foreach(var transaction in popup.Transactions)
                    {
                        this.Transactions.Add(transaction);
                    }
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (this._filename == null)
            {
                this.SaveAsButton_Click(sender, e);
                return;
            }

            using (var filestream = File.Open(this._filename, FileMode.Truncate))
            {
                this.SaveFile(filestream);
            }
        }

        private void SaveAsButton_Click(object sender, EventArgs e)
        {
            using(var saveFileDialog = new SaveFileDialog())
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Stream filestream;
                    if ((filestream = saveFileDialog.OpenFile()) != null)
                    {
                        this._filename = saveFileDialog.FileName;
                        this.SaveFile(filestream);
                    }
                }
            }
        }

        private void SaveFile(Stream filestream)
        {
            var streamWriter = new StreamWriter(filestream);
            this._saveFileReaderWriter.SaveTransactionsToStream(streamWriter, this.Transactions.ToList());
            streamWriter.Flush();
            filestream.Close();
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.Transactions.Clear();
                    Stream filestream;
                    if ((filestream = openFileDialog.OpenFile()) != null)
                    {
                        this._filename = openFileDialog.FileName;
                        using (var streamReader = new StreamReader(filestream))
                        {
                            var transactions = this._saveFileReaderWriter.ReadTransactionsFromStream(streamReader)
                                .ToList();
                            this.ReplaceBindingListItems(this.Transactions, transactions);
                        }
                    }
                }
            }
        }

        #endregion

        #region Data grid handlers

        private void TransactionDataGrid_Click(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }
            int currentMouseOverRow = this.TransactionDataGrid.HitTest(e.X, e.Y).RowIndex;

            if (currentMouseOverRow >= 0)
            {
                this.TransactionDataGrid.ClearSelection();
                this.TransactionDataGrid.Rows[currentMouseOverRow].Selected = true;
                ContextMenuStrip cm = new ContextMenuStrip();
                cm.Items.Add("Edit transaction");
                cm.Items[0].Click += this.EditTransactionButton_Click;
                cm.Items.Add("Delete transaction");
                cm.Items[1].Click += (o1, e2) => this.Transactions.Remove((Transaction)this.TransactionDataGrid.Rows[currentMouseOverRow].DataBoundItem);
                cm.Show(this.TransactionDataGrid, new Point(e.X, e.Y));
            }
        }

        private void YearSummaryDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.YearSummaryDataGrid.Columns[nameof(PortfolioSummaryProvider.CryptoYearSummaryInfo.FifoLongTermCapitalGains)].Index == e.ColumnIndex
                || this.YearSummaryDataGrid.Columns[nameof(PortfolioSummaryProvider.CryptoYearSummaryInfo.FifoShortTermCapitalGains)].Index == e.ColumnIndex
                || this.YearSummaryDataGrid.Columns[nameof(PortfolioSummaryProvider.CryptoYearSummaryInfo.LifoLongTermCapitalGains)].Index == e.ColumnIndex
                || this.YearSummaryDataGrid.Columns[nameof(PortfolioSummaryProvider.CryptoYearSummaryInfo.LifoShortTermCapitalGains)].Index == e.ColumnIndex
                || this.YearSummaryDataGrid.Columns[nameof(PortfolioSummaryProvider.CryptoYearSummaryInfo.UsdInvested)].Index == e.ColumnIndex
                || this.YearSummaryDataGrid.Columns[nameof(PortfolioSummaryProvider.CryptoYearSummaryInfo.UsdReturns)].Index == e.ColumnIndex)
            {
                e.CellStyle.Format = "C2";
            }
        }

        private void TransactionDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.TransactionDataGrid.Columns[nameof(Transaction.UsDollarAmount)].Index == e.ColumnIndex)
            {
                e.CellStyle.Format = "C2";
            }
            else if (this.TransactionDataGrid.Columns[nameof(Transaction.PriceInUsd)].Index == e.ColumnIndex)
            {
                e.CellStyle.Format = "$#,##0.00##";
            }
            else if (this.TransactionDataGrid.Columns[nameof(Transaction.TransactionType)].Index == e.ColumnIndex)
            {
                switch ((TransactionType)e.Value)
                {
                    case TransactionType.Buy:
                        e.CellStyle.ForeColor = Color.Green;
                        break;
                    case TransactionType.Sell:
                        e.CellStyle.ForeColor = Color.Red;
                        break;
                    default:
                        throw new InvalidOperationException($"Unknown value of Transaction Type: {e.Value}");
                }
            }
        }

        private void SummaryDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.SummaryDataGrid.Columns[nameof(PortfolioSummaryProvider.CryptoPortfolioSummaryInfo.TotalUsd)].Index == e.ColumnIndex
                || this.SummaryDataGrid.Columns[nameof(PortfolioSummaryProvider.CryptoPortfolioSummaryInfo.PrincipalUsd)].Index == e.ColumnIndex
                || this.SummaryDataGrid.Columns[nameof(PortfolioSummaryProvider.CryptoPortfolioSummaryInfo.ReturnsUsd)].Index == e.ColumnIndex)
            {
                e.CellStyle.Format = "C2";
            }
            else if (this.SummaryDataGrid.Columns[nameof(PortfolioSummaryProvider.CryptoPortfolioSummaryInfo.PriceInUsd)].Index == e.ColumnIndex
                || this.SummaryDataGrid.Columns[nameof(PortfolioSummaryProvider.CryptoPortfolioSummaryInfo.AveragePriceBought)].Index == e.ColumnIndex)
            {
                e.CellStyle.Format = "$#,##0.00##";
            }
            else if (this.SummaryDataGrid.Columns[nameof(PortfolioSummaryProvider.CryptoPortfolioSummaryInfo.MarketCap)].Index == e.ColumnIndex)
            {
                if((decimal?)e.Value > 1000000000 /*One billion*/)
                {
                    e.CellStyle.Format = "$#,##0,,,.# B";
                }
                else
                {
                    e.CellStyle.Format = "$#,##0,,.# M";
                }
            }
            else if (this.SummaryDataGrid.Columns[nameof(PortfolioSummaryProvider.CryptoPortfolioSummaryInfo.TwentyFourHourChange)].Index == e.ColumnIndex
                || this.SummaryDataGrid.Columns[nameof(PortfolioSummaryProvider.CryptoPortfolioSummaryInfo.Return)].Index == e.ColumnIndex)
            {
                e.CellStyle.Format = @"P1";
                e.CellStyle.ForeColor = (decimal?)e.Value >= 0 ? Color.Green : Color.Red;
            }
        }

        #endregion
        
        #region generated methods

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        #endregion

        private void ReplaceBindingListItems<T>(BindingList<T> bindingList, IReadOnlyCollection<T> newItems)
        {
            bindingList.RaiseListChangedEvents = false;
            bindingList.Clear();
            foreach (var item in newItems)
            {
                bindingList.Add(item);
            }
            bindingList.RaiseListChangedEvents = true;
            bindingList.ResetBindings();
        }
    }
}
