using CryptoTax.Cryptocurrency;
using CryptoTax.Transactions;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CryptoTax.Forms
{
    public partial class CustomCsvImporterDialog : Form
    {
        public CustomCsvHeaderSettings Settings { get; set; }

        public CustomCsvImporterDialog()
        {
            InitializeComponent();

            this.ImportButton.Click += this.ImportButton_Click;
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            if(this.TransactionDateInput.Text == string.Empty
                || this.TransctionTypeInput.Text == string.Empty
                || this.ExchangeInput.Text == string.Empty
                || this.CryptocurrencyPriceInput.Text == string.Empty
                || this.ExcludeFromPortfolioInput.Text == string.Empty
                || this.CryptocurrencyAmountInput.Text == string.Empty)
            {
                MessageBox.Show("Fill in all fields.");
                return;
            }

            this.Settings = new CustomCsvHeaderSettings
            {
                DateHeaderName = this.TransactionDateInput.Text,
                TransactionTypeHeaderName = this.TransctionTypeInput.Text,
                ExchangeHeaderName = this.ExchangeInput.Text,
                CryptocurrencyPriceHeaderName = this.CryptocurrencyPriceInput.Text,
                CryptocurrencyAmountHeaderName = this.CryptocurrencyAmountInput.Text,
                ExcludeFromPortfolioHeaderName = this.ExcludeFromPortfolioInput.Text,
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public class CustomCsvHeaderSettings
        {
            public string DateHeaderName { get; set; }
            public string TransactionTypeHeaderName { get; set; }
            public string ExchangeHeaderName { get; set; }
            public string CryptocurrencyPriceHeaderName { get; set; }
            public string CryptocurrencyAmountHeaderName { get; set; }
            public string ExcludeFromPortfolioHeaderName { get; set; }
        }
    }
}
