using CryptoTax.Crypto;
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
            // enable or disable the exclude porfolio input based on the checkbox status
            this.ExcludeFromPortfolioCheckbox.Click += (o, e) => this.ExcludeFromPortfolioInput.Enabled = this.ExcludeFromPortfolioCheckbox.Checked;
        }
        
        private void ImportButton_Click(object sender, EventArgs e)
        {
            if(this.TransactionDateInput.Text == string.Empty
                || this.TransctionTypeInput.Text == string.Empty
                || this.ExchangeInput.Text == string.Empty
                || this.CryptoPriceInput.Text == string.Empty
                || this.CryptoAmountInput.Text == string.Empty
                || (this.ExcludeFromPortfolioCheckbox.Checked && this.ExcludeFromPortfolioInput.Text == string.Empty))
            {
                MessageBox.Show("Fill in all fields.");
                return;
            }

            this.Settings = new CustomCsvHeaderSettings
            {
                DateHeaderName = this.TransactionDateInput.Text,
                TransactionTypeHeaderName = this.TransctionTypeInput.Text,
                ExchangeHeaderName = this.ExchangeInput.Text,
                CryptoPriceHeaderName = this.CryptoPriceInput.Text,
                CryptoAmountHeaderName = this.CryptoAmountInput.Text,
                ExcludeFromPortfolioHeaderName = this.ExcludeFromPortfolioCheckbox.Checked ? this.ExcludeFromPortfolioInput.Text : null,
                HasExcludeFromPortfolioValues = this.ExcludeFromPortfolioCheckbox.Checked
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public class CustomCsvHeaderSettings
        {
            public string DateHeaderName { get; set; }
            public string TransactionTypeHeaderName { get; set; }
            public string ExchangeHeaderName { get; set; }
            public string CryptoPriceHeaderName { get; set; }
            public string CryptoAmountHeaderName { get; set; }
            public string ExcludeFromPortfolioHeaderName { get; set; }
            public bool HasExcludeFromPortfolioValues { get; set; }
        }
    }
}
