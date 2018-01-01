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

namespace CryptoTax
{
    public partial class MainWindow : Form
    {
        private PriceInUsdProvider _exchangeRateProvider;
        private ConcurrentDictionary<CryptocurrencyType, decimal> _pricesInUsd;

        public BindingSource TransactionDataGridBindingSource = new BindingSource();
        public BindingSource SummaryDataGridBindingSource = new BindingSource();
        public BindingSource FiscalYearSummaryDataGridBindingSource = new BindingSource();

        public MainWindow()
        {
            InitializeComponent();

            this._exchangeRateProvider = new PriceInUsdProvider();
            this._pricesInUsd = new ConcurrentDictionary<CryptocurrencyType, decimal>();
            this.SummaryDataRefreshTimer.Tick += this.RefreshSummaryData;
            this.SummaryDataRefreshTimer.Start();
            this.RefreshSummaryData(null, null);
               
            this.SetupDataGrids();
            this.SetupEventHandlers();
        }

        private void SetupDataGrids()
        {
            // data sources
            this.SummaryDataGrid.DataSource = this.SummaryDataGridBindingSource;
            this.TransactionDataGrid.DataSource = this.TransactionDataGridBindingSource;
            this.FiscalYearSummaryDataGrid.DataSource = this.FiscalYearSummaryDataGridBindingSource;
            // setup formatting
            this.TransactionDataGrid.CellFormatting += this.DataGrid_BuySellFormatting;
            this.TransactionDataGrid.CellFormatting += this.DataGrid_MoneyFormatting;
            this.SummaryDataGrid.CellFormatting += this.SummaryDataGrid_MoneyFormatting;

            this.TransactionDataGridBindingSource.ListChanged += this.UpdateSummaryData;
            this.TransactionDataGridBindingSource.ListChanged += this.UpdateFiscalYearSummaryData;
;
        }

        private void SetupEventHandlers()
        {
            this.toolStrip1.Items["AddTransactionButton"].Click += this.AddTransactionButton_Click;
            this.toolStrip1.Items["EditTransactionButton"].Click += this.EditTransactionButton_Click;
            this.toolStrip1.Items["ImportTransactionsButton"].Click += this.ImportTransactionButtons_Click;
            this.toolStrip1.Items["SaveButton"].Click += this.SaveButton_Click;
            this.toolStrip1.Items["OpenFileButton"].Click += this.OpenFileButton_Click;
        }

        private void AddTransactionButton_Click(object sender, EventArgs e)
        {
            using (var popup = new AddTransactionDialog())
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
            using (var popup = new AddTransactionDialog(transactionToEdit))
            {
                var result = popup.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.TransactionDataGridBindingSource.ResetBindings(false);
                }
            }
        }
        private void ImportTransactionButtons_Click(object sender, EventArgs e)
        {
            using (var popup = new ImportTransactionsDialog())
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
            if (this.TransactionDataGrid.Columns[e.ColumnIndex].Name == nameof(Transaction.PriceInUsd)
                || this.TransactionDataGrid.Columns[e.ColumnIndex].Name == nameof(Transaction.UsDollarAmount))
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void SummaryDataGrid_MoneyFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.SummaryDataGrid.Columns[e.ColumnIndex].Name == nameof(PortfolioSummaryProvider.CryptocurrencySummaryInfo.PriceInUsd)
                || this.SummaryDataGrid.Columns[e.ColumnIndex].Name == nameof(PortfolioSummaryProvider.CryptocurrencySummaryInfo.UsdAmount))
            {
                e.CellStyle.Format = "N2";
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream filestream;
                if ((filestream = saveFileDialog.OpenFile()) != null)
                {
                    var streamWriter = new StreamWriter(filestream);
                    foreach(var transaction in this.TransactionDataGridBindingSource.List.Cast<Transaction>())
                    {
                        streamWriter.WriteLine(transaction.TransactionToFileRow());
                    }
                    streamWriter.Flush();
                    filestream.Close();
                }
            }
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new OpenFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.TransactionDataGridBindingSource.Clear();
                Stream filestream;
                if ((filestream = saveFileDialog.OpenFile()) != null)
                {
                    var streamReader = new StreamReader(filestream);
                    while(streamReader.EndOfStream == false)
                    {
                        this.TransactionDataGridBindingSource.Add(TransactionParsingUtilities.ParseFileRow(streamReader.ReadLine()));
                    }
                }
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
            foreach(var cryptoType in cryptoTypes)
            {
                var queryTask = new Task(async () =>
                {
                    var priceInUsd = await this._exchangeRateProvider.GetPriceInUsd(cryptoType);
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
            var taxCalculator = new TaxCalculator();
            var transactions = this.TransactionDataGridBindingSource.List.Cast<Transaction>().ToList();
            var fiscalYearSummaryInfos = new List<FiscalYearSummaryInfo>();
            foreach(var cryptocurrency in transactions.Select(x => x.Cryptocurrency).Distinct())
            {
                var thisCryptoTransactions = transactions.Where(t => t.Cryptocurrency == cryptocurrency);
                var transactionYears = thisCryptoTransactions.Select(x => x.TransactionDate.Year).Distinct();

                var capitalGainsLifo = taxCalculator.CalculateCapialGains(transactions, AccountingMethodType.Lifo, cryptocurrency);
                var capitalGainsFifo = taxCalculator.CalculateCapialGains(transactions, AccountingMethodType.Fifo, cryptocurrency);
                var usdInvested = thisCryptoTransactions
                        .GroupBy(t => t.TransactionDate.Year)
                        .ToDictionary(x => x.Key, x => x.Aggregate((decimal)0, (v, t) => v + (t.UsDollarAmount * (t.TransactionType == TransactionType.Sell ? -1 : 1))));

                if (capitalGainsFifo == null)
                {
                    fiscalYearSummaryInfos.AddRange(transactionYears
                        .Select(x => new FiscalYearSummaryInfo
                        {
                            Cryptocurrency = cryptocurrency,
                            Year = x,
                            UsdInvested = GetValueOrDefault(usdInvested, x, 0),
                        }));
                    continue;
                }
                
                fiscalYearSummaryInfos.AddRange(transactionYears.Select(x => new FiscalYearSummaryInfo
                {
                    Cryptocurrency = cryptocurrency,
                    Year = x,
                    UsdInvested = GetValueOrDefault(usdInvested, x, 0),
                    FifoShortTermCapitalGains = capitalGainsFifo.Where(y => !y.IsLongTerm && y.YearIncurred == x).Sum(y => y.UsdAmount),
                    FifoLongTermCapitalGains = capitalGainsFifo.Where(y => y.IsLongTerm && y.YearIncurred == x).Sum(y => y.UsdAmount),
                    LifoShortTermCapitalGains = capitalGainsLifo.Where(y => !y.IsLongTerm && y.YearIncurred == x).Sum(y => y.UsdAmount),
                    LifoLongTermCapitalGains = capitalGainsLifo.Where(y => y.IsLongTerm && y.YearIncurred == x).Sum(y => y.UsdAmount),
                }));
            }
            this.FiscalYearSummaryDataGridBindingSource.DataSource = fiscalYearSummaryInfos;
            this.FiscalYearSummaryDataGridBindingSource.ResetBindings(true);
        }

        public static TValue GetValueOrDefault<TKey, TValue>(IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue defaultValue)
        {
            return dictionary.TryGetValue(key, out TValue value) ? value : defaultValue;
        }

        private async void UpdateSummaryData(object sender, EventArgs e)
        {
            var transactions = this.TransactionDataGridBindingSource.List.Cast<Transaction>().ToList();
            var summaryInfos = (new PortfolioSummaryProvider()).GetCryptocurrencySummaryInfo(transactions, this._pricesInUsd);

            // update portfolio summary label
            if(summaryInfos.Any(x => x.UsdAmount.HasValue))
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

        private class FiscalYearSummaryInfo
        {
            public CryptocurrencyType Cryptocurrency { get; set; }
            public int Year { get; set; }
            public decimal UsdInvested { get; set; }
            public decimal? LifoLongTermCapitalGains { get; set; }
            public decimal? LifoShortTermCapitalGains { get; set; }
            public decimal? FifoLongTermCapitalGains { get; set; }
            public decimal? FifoShortTermCapitalGains { get; set; }
        }

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
