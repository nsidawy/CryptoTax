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
using System.Net.Http;
using System.Net.Http.Headers;
using CsvHelper.TypeConversion;

namespace CryptoTax.TransactionImport
{
    public class GdaxFillCsvImporter : ITransactionImporter
    {
        private IReadOnlyDictionary<string, GdaxProduct> _productMapping = new Dictionary<string, GdaxProduct>
        {
            {"BTC-USD", new GdaxProduct{ ProductAsset = CryptocurrencyType.Bitcoin, TransactionCurrency = TransactionCurrencyType.Usd } },
            {"LTC-USD", new GdaxProduct{ ProductAsset = CryptocurrencyType.Litecoin, TransactionCurrency = TransactionCurrencyType.Usd } },
            {"ETH-USD", new GdaxProduct{ ProductAsset = CryptocurrencyType.Ethereum, TransactionCurrency = TransactionCurrencyType.Usd } },
            {"BCH-USD", new GdaxProduct{ ProductAsset = CryptocurrencyType.BitcoinCash, TransactionCurrency = TransactionCurrencyType.Usd } },
            {"ETH-BTC", new GdaxProduct{ ProductAsset = CryptocurrencyType.Ethereum, TransactionCurrency = TransactionCurrencyType.Bitcoin } },
            {"LTC-BTC", new GdaxProduct{ ProductAsset = CryptocurrencyType.Litecoin, TransactionCurrency = TransactionCurrencyType.Bitcoin } },
        };

        private readonly PriceInUsdProvider _priceInUsdProvider;

        public event RowProcessedEventHandler RowProcessed;

        public GdaxFillCsvImporter(PriceInUsdProvider priceInUsdProvider)
        {
            this._priceInUsdProvider = priceInUsdProvider;
        }

        public async Task<TransactionImportResult> ImportFile(TransactonImporterSettings settings)
        {
            var textReader = new StreamReader(settings.Filename);
            var csvReader = new CsvReader(textReader);
            csvReader.Configuration.RegisterClassMap<GdaxFillCsvRecordClassMap>();

            var transactions = new List<Transaction>();
            var unknownProductTransactionCount = 0;
            var unknownProductTransactionSet = new HashSet<string>();
            var rowCount = 0;
            while (csvReader.Read())
            {
                var record = csvReader.GetRecord<GdaxFillCsvRecord>();
                if (!this._productMapping.ContainsKey(record.Product))
                {
                    unknownProductTransactionCount++;
                    unknownProductTransactionSet.Add(record.Product);
                    continue;
                }

                var product = this._productMapping[record.Product];
                switch (product.TransactionCurrency)
                {
                    case TransactionCurrencyType.Usd:
                        transactions.Add(new Transaction
                        {
                            Cryptocurrency = product.ProductAsset,
                            TransactionDate = record.CreatedAt,
                            TransactionType = record.TransactionType,
                            CryptocurrencyAmount = record.AssetAmount,
                            UsDollarAmount = record.AssetPrice * record.AssetAmount
                        });
                        break;
                    case TransactionCurrencyType.Bitcoin:
                        var bitcoinPriceAtTransactionTime = await this._priceInUsdProvider.GetBitcoinPrice(record.CreatedAt);
                        var bitcoinAmount = record.AssetPrice * record.AssetAmount;
                        var usdEquivalentAmounnt = bitcoinAmount * bitcoinPriceAtTransactionTime;

                        transactions.Add(new Transaction
                        {
                            Cryptocurrency = CryptocurrencyType.Bitcoin,
                            TransactionDate = record.CreatedAt,
                            TransactionType = record.TransactionType == TransactionType.Buy ? TransactionType.Sell : TransactionType.Buy,
                            CryptocurrencyAmount = bitcoinAmount,
                            UsDollarAmount = usdEquivalentAmounnt
                        });

                        transactions.Add(new Transaction
                        {
                            Cryptocurrency = product.ProductAsset,
                            TransactionDate = record.CreatedAt,
                            TransactionType = record.TransactionType,
                            CryptocurrencyAmount = record.AssetAmount,
                            UsDollarAmount = usdEquivalentAmounnt
                        });
                        break;
                    default:
                        throw new InvalidOperationException($"Unknown transaction currency: {product.TransactionCurrency}");
                }

                this.RowProcessed?.Invoke(this, new RowProcessedEventArgs { RowsProcessed = ++rowCount });
            }

            var result = new TransactionImportResult
            {
                IsSuccess = true,
                Transactions = transactions,
            };
            if(unknownProductTransactionCount > 0)
            {
                result.Message = $@"{unknownProductTransactionCount} transaction(s) found were ignored since they used an unsupported product. The following unsupported product(s) were found: {string.Join(", ", unknownProductTransactionSet)}.";

            }
            return result;
        }

        private class GdaxFillCsvRecord
        {
            public DateTime CreatedAt { get; set; }
            public string Product { get; set; }
            public TransactionType TransactionType { get; set; }
            public decimal AssetPrice { get; set; }
            public decimal AssetAmount { get; set; }
        }

        private sealed class GdaxFillCsvRecordClassMap : ClassMap<GdaxFillCsvRecord>
        {
            public GdaxFillCsvRecordClassMap()
            {
                Map(m => m.CreatedAt).Name("created at");
                Map(m => m.TransactionType).Name("side").TypeConverter<TransactionTypeConverter>();
                Map(m => m.AssetAmount).Name("size");
                Map(m => m.Product).Name("product");
                Map(m => m.AssetPrice).Name("price");
            }

            private class TransactionTypeConverter : ITypeConverter
            {
                public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
                {
                    return text.ToLower().Equals("buy", StringComparison.OrdinalIgnoreCase) ? TransactionType.Buy : TransactionType.Sell;
                }

                public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
                {
                    throw new NotImplementedException();
                }
            }
        }

        private class GdaxProduct
        {
            public TransactionCurrencyType TransactionCurrency { get; set; }
            public CryptocurrencyType ProductAsset { get; set; }
        }
    }
}
