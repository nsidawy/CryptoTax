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
        ZoDogeCsvImporter
    }

    public interface ITransactionImporter
    {
        TransactionImportResult ImportFile(TransactonImporterSettings settings);
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
