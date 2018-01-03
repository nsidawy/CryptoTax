using CryptoTax.Cryptocurrency;
using CryptoTax.Transactions;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CryptoTax
{
    public partial class AddTransactionDialog : Form
    {
        public Transaction Transaction {get; private set; }

        public AddTransactionDialog()
        {
            InitializeComponent();
            this.AddButton.Click += this.AddButton_Click;
            this.TransactionDateInput.KeyDown += this.TransactionDateInput_KeyDown;

            this.TransactionTypeInput.DataSource = Enum.GetValues(typeof(TransactionType));
            this.CryptocurrencyInput.DataSource = 
                Enum.GetValues(typeof(CryptocurrencyType))
                    .Cast<CryptocurrencyType>().OrderBy(x => Enum.GetName(typeof(CryptocurrencyType), x))
                    .ToList();
        }
        
        public void SetToEditMode(Transaction transaction)
        {
            // initialize the data inputs with the input transaction
            this.Transaction = transaction;
            this.TransactionDateInput.Value = this.Transaction.TransactionDate;
            this.TransactionTypeInput.SelectedItem = this.Transaction.TransactionType;
            this.CryptocurrencyInput.SelectedItem = this.Transaction.Cryptocurrency;
            this.CryptoAmountInput.Value = this.Transaction.CryptocurrencyAmount;
            this.UsdAmountInput.Value = this.Transaction.UsDollarAmount;
            this.ExcludeFromPortfolioCheckBox.Checked = this.Transaction.ExcludeFromPortfolio;

            this.Text = "Edit Transaction";
            this.AddButton.Text = "Edit";

        }

        //handle pasting into the date time picker
        private void TransactionDateInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V)
            {
                DateTime result;
                if(DateTime.TryParse(Clipboard.GetText(), out result))
                {
                    this.TransactionDateInput.Value = result;
                }
                e.Handled = true;
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if(this.Transaction == null)
            {
                this.Transaction = new Transaction();
            }
            this.Transaction.TransactionDate = this.TransactionDateInput.Value;
            this.Transaction.TransactionType = (TransactionType)this.TransactionTypeInput.SelectedValue;
            this.Transaction.Cryptocurrency = (CryptocurrencyType)this.CryptocurrencyInput.SelectedValue;
            this.Transaction.CryptocurrencyAmount = this.CryptoAmountInput.Value;
            this.Transaction.UsDollarAmount = this.UsdAmountInput.Value;
            this.Transaction.ExcludeFromPortfolio = this.ExcludeFromPortfolioCheckBox.Checked;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
