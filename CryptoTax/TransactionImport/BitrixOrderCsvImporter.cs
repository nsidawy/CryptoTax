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
using System.Threading;

namespace CryptoTax.TransactionImport
{
    public class BitrixOrderCsvImporter : ITransactionImporter
    {
        private IReadOnlyDictionary<string, CryptocurrencyType> _exchangeMapping = new Dictionary<string, CryptocurrencyType>
        {
            {"BTC-XRP", CryptocurrencyType.Ripple },
            {"BTC-MANA", CryptocurrencyType.Decentraland },
            {"BTC-EMC2", CryptocurrencyType.Einsteinium },
            {"BTC-QTUM", CryptocurrencyType.Quantum },
            {"BTC-DASH", CryptocurrencyType.Dash },
            {"BTC-REP", CryptocurrencyType.Augur },
            {"BTC-ADA", CryptocurrencyType.Ada },
            {"BTC-XVG", CryptocurrencyType.Ripple },
            {"BTC-NXT", CryptocurrencyType.Nxt },
            {"BTC-GNT", CryptocurrencyType.Golem },
            {"BTC-STRAT", CryptocurrencyType.Stratis },
            {"BTC-ZEC", CryptocurrencyType.ZCash },
            {"BTC-HMQ", CryptocurrencyType.Humaniq },
            {"BTC-XMR", CryptocurrencyType.Monero },
            {"BTC-XLM", CryptocurrencyType.Stellar },
            {"BTC-NEO", CryptocurrencyType.Neo },
            {"BTC-RDD", CryptocurrencyType.Reddcoin },
        };

        public TransactionImportResult ImportFile(TransactonImporterSettings settings)
        {
            var priceInUsdProvider = new PriceInUsdProvider();
            var textReader = new StreamReader(settings.Filename);
            var csvReader = new CsvReader(textReader);
            csvReader.Configuration.RegisterClassMap<BitrixOrderCsvRecordClassMap>();

            var transactions = new List<Transaction>();
            var unknownTransactionTypeCount = 0;
            var unknownExchangeCount = 0;
            var unknownExchangeSet = new HashSet<string>();
            var unknownTransactionTypeSet = new HashSet<string>();
            while (csvReader.Read())
            {
                var record = csvReader.GetRecord<BitrixOrderCsvRecord>();
                if((!record.TransactionType.Equals("limit_buy", StringComparison.OrdinalIgnoreCase) && !record.TransactionType.Equals("limit_sell", StringComparison.OrdinalIgnoreCase)))
                {
                    unknownTransactionTypeCount++;
                    unknownTransactionTypeSet.Add(record.TransactionType);
                    continue;
                }
                if (!this._exchangeMapping.ContainsKey(record.Exchange))
                {
                    unknownExchangeCount++;
                    unknownExchangeSet.Add(record.Exchange);
                    continue;
                }

                var bitcoinPriceAtTransactionTime = priceInUsdProvider.GetBitcoinPrice(record.ClosedTimestamp).Result;
                var bitcoinAmount = record.AssetAmount * record.PriceInBitcoin;
                var usdEquivalentAmounnt = bitcoinAmount * bitcoinPriceAtTransactionTime;

                transactions.Add(new Transaction
                {
                    Cryptocurrency = CryptocurrencyType.Bitcoin,
                    TransactionDate = record.ClosedTimestamp,
                    TransactionType = record.TransactionType.Equals("limit_buy", StringComparison.OrdinalIgnoreCase) ? TransactionType.Sell : TransactionType.Buy,
                    CryptocurrencyAmount = bitcoinAmount + record.CommissionInBitcoin,
                    UsDollarAmount = usdEquivalentAmounnt
                });

                transactions.Add(new Transaction
                {
                    Cryptocurrency = this._exchangeMapping[record.Exchange],
                    TransactionDate = record.ClosedTimestamp,
                    TransactionType = record.TransactionType.Equals("limit_buy", StringComparison.OrdinalIgnoreCase) ? TransactionType.Buy : TransactionType.Sell,
                    CryptocurrencyAmount = record.AssetAmount,
                    UsDollarAmount = usdEquivalentAmounnt
                });
            }

            var messageStringBuilder = new StringBuilder();
            if(unknownExchangeCount > 0)
            {
                messageStringBuilder.AppendLine($"{unknownExchangeCount} transaction(s) were ignored because they used an unsupported exchange. The following unsupported exchange(s) were found: {string.Join(", ", unknownExchangeSet)}.");
            }

            if(unknownTransactionTypeCount > 0)
            {
                messageStringBuilder.AppendLine($"{unknownTransactionTypeCount} transaction(s) were ignored because they used an unsupoorted transaction type. The following unsupported transaction type(s) were found: {string.Join(", ", unknownTransactionTypeSet)}.");
            }

            return new TransactionImportResult
            {
                IsSuccess = true,
                Transactions = transactions,
                Message = messageStringBuilder.ToString()
            };
        }

        private class BitrixOrderCsvRecord
        {
            public DateTime ClosedTimestamp { get; set; }
            public string Exchange { get; set; }
            public decimal AssetAmount { get; set; }
            public decimal PriceInBitcoin { get; set; }
            public decimal CommissionInBitcoin { get; set; }
            public string TransactionType { get; set; }
        }

        private sealed class BitrixOrderCsvRecordClassMap : ClassMap<BitrixOrderCsvRecord>
        {
            public BitrixOrderCsvRecordClassMap()
            {
                Map(m => m.ClosedTimestamp).Name("Closed");
                Map(m => m.AssetAmount).Name("Quantity");
                Map(m => m.PriceInBitcoin).Name("Limit");
                Map(m => m.TransactionType).Name("Type");
                Map(m => m.CommissionInBitcoin).Name("CommissionPaid");
                Map(m => m.Exchange);
            }
        }
    }
}
