using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoTax.Crypto;
using CryptoTax.Transactions;
using CsvHelper.Configuration;
using CsvHelper;
using CsvHelper.TypeConversion;

namespace CryptoTax.TransactionImport
{
    public class BittrexOrderCsvImporter : ITransactionImporter
    {
        private IReadOnlyDictionary<string, Crypto.CryptoType> _exchangeMapping = new Dictionary<string, Crypto.CryptoType>
        {
            {"BTC-XRP", Crypto.CryptoType.Ripple },
            {"BTC-MANA", Crypto.CryptoType.Decentraland },
            {"BTC-EMC2", Crypto.CryptoType.Einsteinium },
            {"BTC-QTUM", Crypto.CryptoType.Quantum },
            {"BTC-DASH", Crypto.CryptoType.Dash },
            {"BTC-REP", Crypto.CryptoType.Augur },
            {"BTC-ADA", Crypto.CryptoType.Ada },
            {"BTC-XVG", Crypto.CryptoType.Verge },
            {"BTC-NXT", Crypto.CryptoType.Nxt },
            {"BTC-GNT", Crypto.CryptoType.Golem },
            {"BTC-STRAT", Crypto.CryptoType.Stratis },
            {"BTC-ZEC", Crypto.CryptoType.ZCash },
            {"BTC-HMQ", Crypto.CryptoType.Humaniq },
            {"BTC-XMR", Crypto.CryptoType.Monero },
            {"BTC-XLM", Crypto.CryptoType.Stellar },
            {"BTC-NEO", Crypto.CryptoType.Neo },
            {"BTC-RDD", Crypto.CryptoType.Reddcoin },
        };

        private readonly PriceInUsdProvider _priceInUsdProvider;

        public event RowProcessedEventHandler RowProcessed;

        public BittrexOrderCsvImporter(PriceInUsdProvider priceInUsdProvider)
        {
            this._priceInUsdProvider = priceInUsdProvider;
        }

        public async Task<TransactionImportResult> ImportFile(TransactonImporterSettings settings)
        {
            var textReader = new StreamReader(settings.Filename);
            var csvReader = new CsvReader(textReader);
            csvReader.Configuration.RegisterClassMap<BitrixOrderCsvRecordClassMap>();

            var transactions = new List<Transaction>();
            var unknownTransactionTypeCount = 0;
            var unknownExchangeCount = 0;
            var unknownExchangeSet = new HashSet<string>();
            var unknownTransactionTypeSet = new HashSet<string>();
            var rowCount = 0;
            while (csvReader.Read())
            {
                var record = csvReader.GetRecord<BitrixOrderCsvRecord>();
                if(!record.TransactionType.HasValue)
                {
                    unknownTransactionTypeCount++;
                    unknownTransactionTypeSet.Add(record.RawTransactionTypeText);
                    continue;
                }
                if (!this._exchangeMapping.ContainsKey(record.Exchange))
                {
                    unknownExchangeCount++;
                    unknownExchangeSet.Add(record.Exchange);
                    continue;
                }

                var bitcoinPriceAtTransactionTime = await this._priceInUsdProvider.GetBitcoinPrice(record.ClosedTimestamp);
                var bitcoinAmount = record.AssetAmount * record.PriceInBitcoin;
                var usdEquivalentAmount = bitcoinAmount * bitcoinPriceAtTransactionTime;

                transactions.Add(new Transaction
                {
                    Crypto = Crypto.CryptoType.Bitcoin,
                    TransactionDate = record.ClosedTimestamp,
                    TransactionType = record.TransactionType.Value == TransactionType.Buy ? TransactionType.Sell : TransactionType.Buy,
                    Quantity = bitcoinAmount + record.CommissionInBitcoin,
                    UsDollarAmount = usdEquivalentAmount
                });

                transactions.Add(new Transaction
                {
                    Crypto = this._exchangeMapping[record.Exchange],
                    TransactionDate = record.ClosedTimestamp,
                    TransactionType = record.TransactionType.Value,
                    Quantity = record.AssetAmount,
                    UsDollarAmount = usdEquivalentAmount
                });

                this.RowProcessed?.Invoke(this, new RowProcessedEventArgs { RowsProcessed = ++rowCount });
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
            public TransactionType? TransactionType { get; set; }
            public string RawTransactionTypeText { get; set; }
        }

        private sealed class BitrixOrderCsvRecordClassMap : ClassMap<BitrixOrderCsvRecord>
        {
            public BitrixOrderCsvRecordClassMap()
            {
                Map(m => m.ClosedTimestamp).Name("Closed");
                Map(m => m.AssetAmount).Name("Quantity");
                Map(m => m.PriceInBitcoin).Name("Limit");
                Map(m => m.TransactionType).Name("Type").TypeConverter<TransactionTypeTypeConverter>();
                Map(m => m.RawTransactionTypeText).Name("Type");
                Map(m => m.CommissionInBitcoin).Name("CommissionPaid");
                Map(m => m.Exchange);
            }

            private class TransactionTypeTypeConverter : ITypeConverter
            {
                public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
                {
                    if (text.Equals("limit_buy", StringComparison.OrdinalIgnoreCase)) {
                        return TransactionType.Buy;
                    }
                    if (text.Equals("limit_sell", StringComparison.OrdinalIgnoreCase))
                    {
                        return TransactionType.Sell;
                    }

                    return null;
                }

                public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
