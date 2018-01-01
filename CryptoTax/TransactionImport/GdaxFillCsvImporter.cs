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

namespace CryptoTax.TransactionImport
{
    public class GdaxFillCsvImporter : ITransactionImporter
    {
        private enum ProductTransationCurrency
        {
            Usd,
            Bitcoin
        }

        private IReadOnlyDictionary<string, GdaxProduct> _productMapping = new Dictionary<string, GdaxProduct>
        {
            {"BTC-USD", new GdaxProduct{ ProductAsset = CryptocurrencyType.Bitcoin, TransactionCurrency = ProductTransationCurrency.Usd } },
            {"LTC-USD", new GdaxProduct{ ProductAsset = CryptocurrencyType.Litecoin, TransactionCurrency = ProductTransationCurrency.Usd } },
            {"ETH-USD", new GdaxProduct{ ProductAsset = CryptocurrencyType.Ethereum, TransactionCurrency = ProductTransationCurrency.Usd } },
            {"BCH-USD", new GdaxProduct{ ProductAsset = CryptocurrencyType.BitcoinCash, TransactionCurrency = ProductTransationCurrency.Usd } },
            {"ETH-BTC", new GdaxProduct{ ProductAsset = CryptocurrencyType.Ethereum, TransactionCurrency = ProductTransationCurrency.Bitcoin } },
            {"LTC-BTC", new GdaxProduct{ ProductAsset = CryptocurrencyType.Litecoin, TransactionCurrency = ProductTransationCurrency.Bitcoin } },
        };

        private readonly PriceInUsdProvider _priceInUsdProvider;

        public GdaxFillCsvImporter(PriceInUsdProvider priceInUsdProvider)
        {
            this._priceInUsdProvider = priceInUsdProvider;
        }

        public TransactionImportResult ImportFile(TransactonImporterSettings settings)
        {
            var textReader = new StreamReader(settings.Filename);
            var csvReader = new CsvReader(textReader);
            csvReader.Configuration.RegisterClassMap<GdaxFillCsvRecordClassMap>();

            var transactions = new List<Transaction>();
            var unknownProductTransactionCount = 0;
            var unknownProductTransactionSet = new HashSet<string>();
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
                    case ProductTransationCurrency.Usd:
                        transactions.Add(new Transaction
                        {
                            Cryptocurrency = product.ProductAsset,
                            TransactionDate = record.CreatedAt,
                            TransactionType = record.Side.Equals("buy", StringComparison.OrdinalIgnoreCase) ? TransactionType.Buy : TransactionType.Sell,
                            CryptocurrencyAmount = record.AssetAmount,
                            // if the transacton is a buy then the USD amount will be a negative value on the record
                            UsDollarAmount = record.AssetPrice * record.AssetAmount
                        });
                        break;
                    case ProductTransationCurrency.Bitcoin:
                        var bitcoinPriceAtTransactionTime = this._priceInUsdProvider.GetBitcoinPrice(record.CreatedAt).Result;
                        var bitcoinAmount = record.AssetPrice * record.AssetAmount;
                        var usdEquivalentAmounnt = bitcoinAmount * bitcoinPriceAtTransactionTime;

                        transactions.Add(new Transaction
                        {
                            Cryptocurrency = CryptocurrencyType.Bitcoin,
                            TransactionDate = record.CreatedAt,
                            TransactionType = record.Side.Equals("buy", StringComparison.OrdinalIgnoreCase) ? TransactionType.Sell : TransactionType.Buy,
                            CryptocurrencyAmount = bitcoinAmount,
                            UsDollarAmount = usdEquivalentAmounnt
                        });

                        transactions.Add(new Transaction
                        {
                            Cryptocurrency = product.ProductAsset,
                            TransactionDate = record.CreatedAt,
                            TransactionType = record.Side.Equals("buy", StringComparison.OrdinalIgnoreCase) ? TransactionType.Buy : TransactionType.Sell,
                            CryptocurrencyAmount = record.AssetAmount,
                            UsDollarAmount = usdEquivalentAmounnt
                        });
                        break;
                    default:
                        throw new InvalidOperationException($"Unknown transaction currency: {product.TransactionCurrency}");
                }
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
            public string Side { get; set; }
            public decimal AssetPrice { get; set; }
            public decimal AssetAmount { get; set; }
        }

        private sealed class GdaxFillCsvRecordClassMap : ClassMap<GdaxFillCsvRecord>
        {
            public GdaxFillCsvRecordClassMap()
            {
                Map(m => m.CreatedAt).Name("created at");
                Map(m => m.Side).Name("side");
                Map(m => m.AssetAmount).Name("size");
                Map(m => m.Product).Name("product");
                Map(m => m.AssetPrice).Name("price");
            }
        }

        private class GdaxProduct
        {
            public ProductTransationCurrency TransactionCurrency { get; set; }
            public CryptocurrencyType ProductAsset { get; set; }
        }
    }
}
