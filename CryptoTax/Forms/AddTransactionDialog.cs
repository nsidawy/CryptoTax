using CryptoTax.Crypto;
using CryptoTax.Transactions;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CryptoTax.Forms
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
            this.CryptoInput.DataSource = 
                Enum.GetValues(typeof(CryptoType))
                    .Cast<CryptoType>().OrderBy(x => Enum.GetName(typeof(CryptoType), x))
                    .ToList();
        }
        
        public void SetToEditMode(Transaction transaction)
        {
            // initialize the data inputs with the input transaction
            this.Transaction = transaction;
            this.TransactionDateInput.Value = this.Transaction.TransactionDate;
            this.TransactionTypeInput.SelectedItem = this.Transaction.TransactionType;
            this.CryptoInput.SelectedItem = this.Transaction.Crypto;
            this.CryptoAmountInput.Value = this.Transaction.Quantity;
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
            this.Transaction.Crypto = (Crypto.CryptoType)this.CryptoInput.SelectedValue;
            this.Transaction.Quantity = this.CryptoAmountInput.Value;
            this.Transaction.UsDollarAmount = this.UsdAmountInput.Value;
            this.Transaction.ExcludeFromPortfolio = this.ExcludeFromPortfolioCheckBox.Checked;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
