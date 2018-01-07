using CryptoTax.Cryptocurrency;
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

namespace CryptoTax.Forms
{
    public partial class MainWindow : Form
    {
        private PriceInUsdProvider _priceInUsdProvider;
        private TaxCalculator _taxCalculator;
        private PortfolioSummaryProvider _portfolioSummaryProvider;
        private FormFactory _formFactory;
        private SaveFileReaderWriter _saveFileReaderWriter;

        private string _filename = null;
        private ConcurrentDictionary<CryptocurrencyType, decimal> _pricesInUsd;

        private BindingList<Transaction> Transactions = new BindingList<Transaction>();
        private SortableBindingList<Transaction> TransactionDataGridBindingSource = new SortableBindingList<Transaction>();
        private SortableBindingList<PortfolioSummaryProvider.CryptocurrencyPortfolioSummaryInfo> SummaryDataGridBindingSource = new SortableBindingList<PortfolioSummaryProvider.CryptocurrencyPortfolioSummaryInfo>();
        private SortableBindingList<PortfolioSummaryProvider.CryptocurrencyYearSummaryInfo> YearSummaryDataGridBindingSource = new SortableBindingList<PortfolioSummaryProvider.CryptocurrencyYearSummaryInfo>();

        public MainWindow(
            PriceInUsdProvider priceInUsdProvider,
            TaxCalculator taxCalculator,
            PortfolioSummaryProvider portfolioSummaryProvider,
            FormFactory formFactory,
            SaveFileReaderWriter saveFileReaderWriter)
        {
            InitializeComponent();

            this._priceInUsdProvider = priceInUsdProvider;
            this._taxCalculator = taxCalculator;
            this._portfolioSummaryProvider = portfolioSummaryProvider;
            this._formFactory = formFactory;
            this._saveFileReaderWriter = saveFileReaderWriter;

            this._pricesInUsd = new ConcurrentDictionary<CryptocurrencyType, decimal>();
            this.SummaryDataRefreshTimer.Tick += this.RefreshLivePriceData;
            this.SummaryDataRefreshTimer.Start();
            this.RefreshLivePriceData(null, null);
               
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
                //this.ReplaceBindingListItems(this.Transactions, this.Transactions.Where(x => x.GetHashCode() != e.Row.DataBoundItem.GetHashCode()).ToList());
                this.Transactions.Remove((Transaction)e.Row.DataBoundItem);
            };
            this.TransactionDataGrid.MouseDown += (object o, MouseEventArgs e) =>
            {
                if(e.Button != MouseButtons.Right)
                {
                    return;
                }
                int currentMouseOverRow = this.TransactionDataGrid.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0)
                {
                    this.TransactionDataGrid.ClearSelection();
                    this.TransactionDataGrid.Rows[currentMouseOverRow].Selected = true;
                    ContextMenu cm = new ContextMenu();
                    cm.MenuItems.Add("Edit transaction");
                    cm.MenuItems[0].Click += this.EditTransactionButton_Click;
                    cm.MenuItems.Add("Delete transaction");
                    cm.MenuItems[1].Click += (o1, e2) => this.Transactions.Remove((Transaction)this.TransactionDataGrid.Rows[currentMouseOverRow].DataBoundItem);
                    cm.Show(this.TransactionDataGrid, new Point(e.X, e.Y));
                }
            };


            // setup formatting
            this.TransactionDataGrid.CellFormatting += this.DataGrid_BuySellFormatting;
            this.TransactionDataGrid.CellFormatting += this.DataGrid_MoneyFormatting;
            this.SummaryDataGrid.CellFormatting += this.SummaryDataGrid_MoneyFormatting;
            
            this.Transactions.ListChanged += this.TransactionsUpdated;

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

        private async void RefreshLivePriceData(object sender, EventArgs e)
        {
            this.UpdatePricesInUsd();
            this.UpdateSummaryData();
        }

        private async void TransactionsUpdated(object sender, ListChangedEventArgs e)
        {
            this.UpdateSummaryData();
            this.UpdateFiscalYearSummaryData();

            // update transactions grid source data
            this.SyncTransactionDataGrid();
        }

        private void SyncTransactionDataGrid()
        {
            IEnumerable<Transaction> transactions = this.Transactions.ToList();
            var cryptocurrenyFilterInput = ((ToolStripComboBox)this.toolStrip2.Items["CryptocurrencyFilterInput"]);
            if (!(cryptocurrenyFilterInput.SelectedItem is NoneOption))
            {
                var cryptocurrencyFilter = (CryptocurrencyType)cryptocurrenyFilterInput.SelectedItem;
                transactions = transactions.Where(x => x.Cryptocurrency == cryptocurrencyFilter);
            }

            this.ReplaceBindingListItems(this.TransactionDataGridBindingSource, transactions.ToList());
            this.TransactionDataGridBindingSource.Resort();
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

        private void UpdateFiscalYearSummaryData()
        {
            var transactions = this.Transactions
                .Where(x => !x.ExcludeFromPortfolio)
                .ToList();
            var yearSummaryInfos = this._portfolioSummaryProvider.GetCryptocurrencyYearSummaryInfo(transactions)
                .OrderBy(x => x.Cryptocurrency)
                .ThenBy(x => x.Year)
                .ToList();
            
            this.ReplaceBindingListItems(this.YearSummaryDataGridBindingSource, yearSummaryInfos);
        }

        private async void UpdateSummaryData()
        {
            var transactions = this.Transactions
                .Where(x => !x.ExcludeFromPortfolio)
                .ToList();
            var summaryInfos = this._portfolioSummaryProvider.GetCryptocurrencyPortfolioSummaryInfo(transactions, this._pricesInUsd)
                .OrderByDescending(x => x.UsdAmount)
                .ToList();

            // update portfolio summary label
            if (summaryInfos.Any(x => x.UsdAmount.HasValue))
            {
                this.SummaryLabel.Text = "Portfolio Summary - $" + summaryInfos.Aggregate((decimal)0, (v, s) => v + s.UsdAmount ?? 0).ToString("N2");
            }
            else
            {
                this.SummaryLabel.Text = "Portfolio Summary";
            }
            
            this.ReplaceBindingListItems(this.SummaryDataGridBindingSource, summaryInfos);
        }

        private void OnCryptocurrencyFilterInputChange(object sender, EventArgs e)
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
