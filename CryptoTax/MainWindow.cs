﻿using CryptoTax.Cryptocurrency;
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

namespace CryptoTax
{
    public partial class MainWindow : Form
    {
        private PriceInUsdProvider _priceInUsdProvider;
        private TaxCalculator _taxCalculator;
        private PortfolioSummaryProvider _portfolioSummaryProvider;
        private FormFactory _formFactory;

        private string _filename = null;
        private ConcurrentDictionary<CryptocurrencyType, decimal> _pricesInUsd;
        
        private BindingSource TransactionDataGridBindingSource = new BindingSource();
        private BindingSource SummaryDataGridBindingSource = new BindingSource();
        private BindingSource YearSummaryDataGridBindingSource = new BindingSource();

        public MainWindow(
            PriceInUsdProvider priceInUsdProvider,
            TaxCalculator taxCalculator,
            PortfolioSummaryProvider portfolioSummaryProvider,
            FormFactory formFactory)
        {
            InitializeComponent();

            this._priceInUsdProvider = priceInUsdProvider;
            this._taxCalculator = taxCalculator;
            this._portfolioSummaryProvider = portfolioSummaryProvider;
            this._formFactory = formFactory;

            this._pricesInUsd = new ConcurrentDictionary<CryptocurrencyType, decimal>();
            this.SummaryDataRefreshTimer.Tick += this.RefreshSummaryData;
            this.SummaryDataRefreshTimer.Start();
            this.RefreshSummaryData(null, null);
               
            this.SetupDataGrids();
            this.SetupEventHandlers();

            this.KeyPreview = true;
            this.KeyDown += this.MainWindow_KeyDown;
        }

        private void SetupDataGrids()
        {
            this.SummaryDataGridBindingSource.DataSource = new List<PortfolioSummaryProvider.CryptocurrencyPortfolioSummaryInfo>();
            this.YearSummaryDataGridBindingSource.DataSource = new List<PortfolioSummaryProvider.CryptocurrencyYearSummaryInfo>();
            this.TransactionDataGridBindingSource.DataSource = new List<Transaction>();

            // data sources
            this.SummaryDataGrid.DataSource = this.SummaryDataGridBindingSource;
            this.TransactionDataGrid.DataSource = this.TransactionDataGridBindingSource;
            this.YearSummaryDataGrid.DataSource = this.YearSummaryDataGridBindingSource;

            this.TransactionDataGrid.Columns[nameof(Transaction.ExcludeFromPortfolio)].ReadOnly = false;
            
            // setup formatting
            this.TransactionDataGrid.CellFormatting += this.DataGrid_BuySellFormatting;
            this.TransactionDataGrid.CellFormatting += this.DataGrid_MoneyFormatting;
            this.SummaryDataGrid.CellFormatting += this.SummaryDataGrid_MoneyFormatting;

            this.TransactionDataGridBindingSource.ListChanged += this.UpdateSummaryData;
            this.TransactionDataGridBindingSource.ListChanged += this.UpdateFiscalYearSummaryData;
            this.TransactionDataGridBindingSource.ListChanged += this.OnCryptocurrencyFilterInputChange;

            var cryptocurrenyFilterInput = ((ToolStripComboBox)this.toolStrip2.Items["CryptocurrencyFilterInput"]);
            var noneOption = new NoneOption();
            cryptocurrenyFilterInput.Items.Add(noneOption);
            cryptocurrenyFilterInput.SelectedIndex = 0;
            cryptocurrenyFilterInput.Items
                .AddRange(Enum.GetValues(typeof(CryptocurrencyType)).Cast<object>().ToArray());
            cryptocurrenyFilterInput.SelectedIndexChanged += this.OnCryptocurrencyFilterInputChange;
            cryptocurrenyFilterInput.TextChanged += this.OnCryptocurrencyFilterInputChange;
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


        private async void RefreshSummaryData(object sender, EventArgs e)
        {
            this.UpdatePricesInUsd();
            this.UpdateSummaryData(sender, e);
        }

        private void UpdatePricesInUsd()
        {
            var cryptoTypes = Enum.GetValues(typeof(CryptocurrencyType)).Cast<CryptocurrencyType>();
            var queryTasks = new List<Task>();
            foreach (var cryptoType in cryptoTypes)
            {
                var queryTask = new Task(async () =>
                {
                    var priceInUsd = await this._priceInUsdProvider.GetPriceInUsd(cryptoType);
                    if (priceInUsd.HasValue)
                    {
                        this._pricesInUsd[cryptoType] = priceInUsd.Value;
                    }
                });
                queryTask.Start();
                queryTasks.Add(queryTask);
            }
            Task.WaitAll(queryTasks.ToArray());
        }

        private void UpdateFiscalYearSummaryData(object sender, EventArgs e)
        {
            var transactions = this.TransactionDataGridBindingSource.List.Cast<Transaction>()
                .Where(x => !x.ExcludeFromPortfolio)
                .ToList();
            var yearSummaryInfos = this._portfolioSummaryProvider.GetCryptocurrencyYearSummaryInfo(transactions);

            this.YearSummaryDataGridBindingSource.DataSource = yearSummaryInfos;
            this.YearSummaryDataGridBindingSource.ResetBindings(true);
        }

        private async void UpdateSummaryData(object sender, EventArgs e)
        {
            var transactions = this.TransactionDataGridBindingSource.List
                .Cast<Transaction>()
                .Where(x => !x.ExcludeFromPortfolio)
                .ToList();
            var summaryInfos = this._portfolioSummaryProvider.GetCryptocurrencyPortfolioSummaryInfo(transactions, this._pricesInUsd)
                .OrderByDescending(x => x.UsdAmount);

            // update portfolio summary label
            if (summaryInfos.Any(x => x.UsdAmount.HasValue))
            {
                this.SummaryLabel.Text = "Portfolio Summary - $" + summaryInfos.Aggregate((decimal)0, (v, s) => v + s.UsdAmount ?? 0).ToString("N2");
            }
            else
            {
                this.SummaryLabel.Text = "Portfolio Summary";
            }

            this.SummaryDataGridBindingSource.DataSource = summaryInfos;
            this.SummaryDataGridBindingSource.ResetBindings(true);
        }

        private void OnCryptocurrencyFilterInputChange(object sender, EventArgs e)
        {
            var cryptocurrenyFilterInput = ((ToolStripComboBox)this.toolStrip2.Items["CryptocurrencyFilterInput"]);
            if(!cryptocurrenyFilterInput.Selected || (cryptocurrenyFilterInput.SelectedItem is NoneOption))
            {
                for (var i = 0; i < this.TransactionDataGrid.Rows.Count; i++)
                {
                    this.TransactionDataGrid.Rows[i].Visible = true;
                }
            }
            else
            {
                var cryptocurrencyFilter = (CryptocurrencyType)cryptocurrenyFilterInput.SelectedItem;

                this.TransactionDataGrid.CurrentCell = null;
                for (var i = 0; i < this.TransactionDataGrid.Rows.Count; i++)
                {
                    this.TransactionDataGrid.Rows[i].Visible =
                        (CryptocurrencyType)this.TransactionDataGrid.Rows[i].Cells[nameof(Transaction.Cryptocurrency)].Value == cryptocurrencyFilter;
                }
            }
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
                    this.TransactionDataGridBindingSource.Add(popup.Transaction);
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
                    this.TransactionDataGridBindingSource.ResetBindings(false);
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
                        this.TransactionDataGridBindingSource.Add(transaction);
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

            using (var filestream = File.OpenWrite(this._filename))
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
            TransactionParsingUtilities.SaveTransactionsToStream(streamWriter, this.TransactionDataGridBindingSource.List.Cast<Transaction>().ToList());
            streamWriter.Flush();
            filestream.Close();
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.TransactionDataGridBindingSource.Clear();
                    Stream filestream;
                    if ((filestream = openFileDialog.OpenFile()) != null)
                    {
                        this._filename = openFileDialog.FileName;
                        using (var streamReader = new StreamReader(filestream))
                        {
                            var transactions = TransactionParsingUtilities.ReadTransactionsFromStream(streamReader);
                            this.TransactionDataGridBindingSource.List.Clear();
                            foreach(var transaction in transactions)
                            {
                                this.TransactionDataGridBindingSource.Add(transaction);
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Data grid formatting handlers

        private void DataGrid_BuySellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.TransactionDataGrid.Columns[e.ColumnIndex].Name == nameof(Transaction.TransactionType))
            {
                switch((TransactionType)e.Value)
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

        private void DataGrid_MoneyFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.TransactionDataGrid.Columns[e.ColumnIndex].Name == nameof(Transaction.UsDollarAmount))
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void SummaryDataGrid_MoneyFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.SummaryDataGrid.Columns[e.ColumnIndex].Name == nameof(PortfolioSummaryProvider.CryptocurrencyPortfolioSummaryInfo.UsdAmount))
            {
                e.CellStyle.Format = "N2";
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
    }
}
