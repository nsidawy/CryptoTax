using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoTax.Cryptocurrency;
using CryptoTax.Transactions;
using CsvHelper.Configuration;
using CsvHelper;

namespace CryptoTax.TransactionImport
{
    public class CoinbaseCsvImporter : ITransactionImporter
    {
        private IReadOnlyDictionary<string, CryptocurrencyType> _cryptocurrencyMapping = new Dictionary<string, CryptocurrencyType>
        {
            {"BTC", CryptocurrencyType.Bitcoin },
            {"LTC", CryptocurrencyType.Litecoin },
            {"ETH", CryptocurrencyType.Ethereum },
            {"BCH", CryptocurrencyType.BitcoinCash },
        };

        public TransactionImportResult ImportFile(TransactonImporterSettings settings)
        {
            var textReader = new StreamReader(settings.Filename);
            // skip rows prior to the transaction section
            for (var i = 1; i < 5; i++)
            {
                textReader.ReadLine();
            }
            var csvReader = new CsvReader(textReader);
            csvReader.Configuration.RegisterClassMap<CoinbaseCsvRecordClassMap>();

            var transactions = new List<Transaction>();
            var nonBuyOrSellTransactionCount = 0;
            while (csvReader.Read())
            {
                var record = csvReader.GetRecord<CoinbaseCsvRecord>();
                if ((!record.Notes.StartsWith("Bought") && !record.Notes.StartsWith("Sold"))
                    || !this._cryptocurrencyMapping.ContainsKey(record.Currency))
                {
                    nonBuyOrSellTransactionCount++;
                    continue;
                }
                transactions.Add(new Transaction
                {
                    Cryptocurrency = this._cryptocurrencyMapping[record.Currency],
                    CryptocurrencyAmount = record.Amount.Value,
                    TransactionDate = record.Timestamp,
                    TransactionType = record.Notes.StartsWith("Bought") ? TransactionType.Buy : TransactionType.Sell,
                    UsDollarAmount = record.TransferTotal.Value - record.TransferFee.Value
                });
            }

            return new TransactionImportResult
            {
                IsSuccess = true,
                Transactions = transactions,
                Message = $"{nonBuyOrSellTransactionCount} item(s) in the CSV were ignored because they are not a buy or sell transction."
            };
        }

        private class CoinbaseCsvRecord
        {
            public decimal? Amount { get; set; }
            public string Currency { get; set; }
            public DateTime Timestamp { get; set; }
            public string Notes { get; set; }
            public decimal? TransferTotal { get; set; }
            public decimal? TransferFee { get; set; }
            public string TransferCurrency { get; set; }
        }

        private sealed class CoinbaseCsvRecordClassMap : ClassMap<CoinbaseCsvRecord>
        {
            public CoinbaseCsvRecordClassMap()
            {
                Map(m => m.Amount);
                Map(m => m.Currency);
                Map(m => m.Timestamp);
                Map(m => m.Notes);
                Map(m => m.TransferTotal).Name("Transfer Total");
                Map(m => m.TransferFee).Name("Transfer Fee");
                Map(m => m.TransferCurrency).Name("Transfer Total Currency");
            }
        }
    }
}
