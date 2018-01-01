using CryptoTax.Cryptocurrency;
using CryptoTax.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CryptoTax.TransactionImport;
using System.IO;

namespace CryptoTax
{
    public partial class ImportTransactionsDialog : Form
    {
        public IReadOnlyCollection<Transaction> Transactions {get; private set; }

        public ImportTransactionsDialog()
        {
            InitializeComponent();

            this.ImportButton.Click += this.ImportButton_Click;
            this.FileBrowseButton.Click += this.FileBrowseButton_Click;
        }

        private void FileBrowseButton_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.FilenameInput.Text = openFileDialog.FileName;
            }
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            ITransactionImporter transactionImporter;
            if(this.CoinbaseCsvRadioButton.Checked)
            {
                transactionImporter = new CoinbaseCsvImporter();
            }
            else if(this.GdaxFillCsvRadioButton.Checked)
            {
                transactionImporter = new GdaxFillCsvImporter();
            }
            else if (this.BitrexOrderCsvRadioButton.Checked)
            {
                transactionImporter = new BitrixOrderCsvImporter();
            }
            else
            {
                MessageBox.Show("Please select a source file type.", "");
                return;
            }

            if (!File.Exists(this.FilenameInput.Text))
            {
                MessageBox.Show("The selected file does not exist.", "");
                return;
            }

            var transactionImportResult = transactionImporter.ImportFile(new TransactonImporterSettings { Filename = this.FilenameInput.Text });
            if (transactionImportResult.IsSuccess && this.ConfirmImportedTransactions(transactionImportResult))
            {
                this.DialogResult = DialogResult.OK;
                this.Transactions = transactionImportResult.Transactions;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
            this.Close();
        }

        private bool ConfirmImportedTransactions(TransactionImportResult result)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{ result.Transactions.Count} transaction(s) were found in the imported file.");

            if(result.Message != null && result.Message.Length > 0)
            {
                stringBuilder.AppendLine(result.Message);
                stringBuilder.AppendLine();
            }

            stringBuilder.AppendLine("Are you sure you want to import these transactions?");
            stringBuilder.AppendLine();

            var confirmResult = MessageBox.Show(stringBuilder.ToString(),
                "Confirm transaction import",  MessageBoxButtons.YesNo);
            return confirmResult == DialogResult.Yes;
        }
    }
}
