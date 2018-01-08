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
using Autofac.Features.Indexed;

namespace CryptoTax.Forms
{
    public partial class ImportTransactionsDialog : Form
    {
        private readonly IIndex<TransactionImporterType, ITransactionImporter> _transacionImporterIndex;

        public IReadOnlyCollection<Transaction> Transactions {get; private set; }

        public ImportTransactionsDialog(
            IIndex<TransactionImporterType, ITransactionImporter> transacionImporterIndex)
        {
            InitializeComponent();

            this._transacionImporterIndex = transacionImporterIndex;

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

        private async void ImportButton_Click(object sender, EventArgs e)
        {
            ITransactionImporter transactionImporter;
            if(this.CoinbaseCsvRadioButton.Checked)
            {
                transactionImporter = this._transacionImporterIndex[TransactionImporterType.CoinbaseCsvImporter];
            }
            else if(this.GdaxFillCsvRadioButton.Checked)
            {
                transactionImporter = this._transacionImporterIndex[TransactionImporterType.GdaxFillCsvImporter];
            }
            else if (this.BitrexOrderCsvRadioButton.Checked)
            {
                transactionImporter = this._transacionImporterIndex[TransactionImporterType.BitrixOrderCsvImporter];
            }
            else if (this.ZoDogeCsvRadioButton.Checked)
            {
                transactionImporter = this._transacionImporterIndex[TransactionImporterType.ZoDogeCsvImporter];
            }
            else if (this.CustomCsvRadioButton.Checked)
            {
                transactionImporter = this._transacionImporterIndex[TransactionImporterType.CustomCsvImporter];
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

            this.Enabled = false;
            var transactionImportResult = await transactionImporter.ImportFile(new TransactonImporterSettings { Filename = this.FilenameInput.Text });
            if (transactionImportResult.IsSuccess && this.ConfirmImportedTransactions(transactionImportResult))
            {
                this.DialogResult = DialogResult.OK;
                this.Transactions = transactionImportResult.Transactions;
            }
            else
            {
                MessageBox.Show(transactionImportResult.Message);
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
                stringBuilder.AppendLine();
                stringBuilder.AppendLine(result.Message);
            }

            stringBuilder.AppendLine();
            stringBuilder.AppendLine("Are you sure you want to import these transactions?");

            var confirmResult = MessageBox.Show(stringBuilder.ToString(),
                "Confirm transaction import",  MessageBoxButtons.YesNo);
            return confirmResult == DialogResult.Yes;
        }
    }
}
