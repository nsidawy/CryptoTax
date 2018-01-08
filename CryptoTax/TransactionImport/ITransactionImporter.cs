using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoTax.Transactions;

namespace CryptoTax.TransactionImport
{
    public enum TransactionImporterType
    {
        CoinbaseCsvImporter,
        GdaxFillCsvImporter,
        BitrixOrderCsvImporter,
        ZoDogeCsvImporter,
        CustomCsvImporter,
    }

    public interface ITransactionImporter
    {
        event RowProcessedEventHandler RowProcessed;
        Task<TransactionImportResult> ImportFile(TransactonImporterSettings settings);
    }

    public delegate void RowProcessedEventHandler(object sender, RowProcessedEventArgs e);

    public class RowProcessedEventArgs : EventArgs
    {
        public int RowsProcessed { get; set; }
    }

    public class TransactonImporterSettings
    {
        public string Filename { get; set; }
    }

    public class TransactionImportResult
    {
        public IReadOnlyList<Transaction> Transactions { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
