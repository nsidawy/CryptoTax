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
using CsvHelper.TypeConversion;
using CryptoTax.Utilities;
using System.Windows.Forms;
using CryptoTax.Forms;

namespace CryptoTax.TransactionImport
{
    public class CustomCsvImporter : ITransactionImporter
    { 
        private readonly FormFactory _formFactory;
        private readonly PriceInUsdProvider _priceInUsdProvider;
        private readonly ExchangeParser _exchangeParser;

        public CustomCsvImporter(
            PriceInUsdProvider priceInUsdProvider,
            FormFactory formFactory,
            ExchangeParser exchangeParser)
        {
            this._priceInUsdProvider = priceInUsdProvider;
            this._formFactory = formFactory;
            this._exchangeParser = exchangeParser;
        }

        public event RowProcessedEventHandler RowProcessed;

        public async Task<TransactionImportResult> ImportFile(TransactonImporterSettings settings)
        {
            var settingsDiaolog = this._formFactory.CreateForm<CustomCsvImporterDialog>();
            var result = settingsDiaolog.ShowDialog();

            if(result != DialogResult.OK)
            {
                return new TransactionImportResult
                {
                    IsSuccess = false,
                    Message = "Custom CSV import canceled."
                };
            }

            var customCsvHeaderSettings = settingsDiaolog.Settings;
            var textReader = new StreamReader(settings.Filename);
            var csvReader = new CsvReader(textReader);
            csvReader.Configuration.RegisterClassMap(new CustomCsvImporterRecordClassMap(customCsvHeaderSettings));

            var transactions = new List<Transaction>();
            var unknownDogecoinPriceIds = new HashSet<string>();
            var rowCount = 0;
            while (csvReader.Read())
            {
                var record = csvReader.GetRecord<CustomCsvImporterRecord>();
                var exchangeResult = this._exchangeParser.ParseExchange(record.Exchange);

                if (exchangeResult.TransactionCurrency == TransactionCurrencyType.Usd)
                {
                    transactions.Add(new Transaction
                    {
                        Cryptocurrency = exchangeResult.AssetCurrency,
                        TransactionDate = record.Date,
                        TransactionType = record.TransactionType,
                        CryptocurrencyAmount = record.CryptocurrencyAmount,
                        UsDollarAmount = record.TransactionCurrencyAmount,
                        ExcludeFromPortfolio = record.ExcludeFromPortfolio
                    });
                }
                else
                {
                    decimal transactionCurrencyePriceAtTransactionTime;
                    switch (exchangeResult.TransactionCurrency)
                    {
                        case TransactionCurrencyType.Bitcoin:
                            transactionCurrencyePriceAtTransactionTime = await this._priceInUsdProvider.GetBitcoinPrice(record.Date);
                            break;
                        case TransactionCurrencyType.Ethereum:
                            transactionCurrencyePriceAtTransactionTime = await this._priceInUsdProvider.GetEthereumPrice(record.Date);
                            break;
                        default:
                            throw new InvalidOperationException("Should never get here.");
                    }
                    
                    var usdEquivalentAmount = record.TransactionCurrencyAmount * transactionCurrencyePriceAtTransactionTime;
                    transactions.Add(new Transaction
                    {
                        Cryptocurrency = exchangeResult.TransactionCurrency.ToCryptocurrencyType(),
                        TransactionDate = record.Date,
                        TransactionType = record.TransactionType == TransactionType.Buy ? TransactionType.Sell : TransactionType.Buy,
                        CryptocurrencyAmount = record.TransactionCurrencyAmount,
                        UsDollarAmount = usdEquivalentAmount,
                        ExcludeFromPortfolio = record.ExcludeFromPortfolio
                    });
                    transactions.Add(new Transaction
                    {
                        Cryptocurrency = exchangeResult.AssetCurrency,
                        TransactionDate = record.Date,
                        TransactionType = record.TransactionType == TransactionType.Buy ? TransactionType.Buy : TransactionType.Sell,
                        CryptocurrencyAmount = record.CryptocurrencyAmount,
                        UsDollarAmount = usdEquivalentAmount,
                        ExcludeFromPortfolio = record.ExcludeFromPortfolio
                    });

                    this.RowProcessed?.Invoke(this, new RowProcessedEventArgs { RowsProcessed = ++rowCount });
                }
            }

            return new TransactionImportResult
            {
                IsSuccess = true,
                Transactions = transactions,
            };
        }

        private class CustomCsvImporterRecord
        {
            public CustomCsvImporterRecord()
            {
                // default value
                this.ExcludeFromPortfolio = false;
            }

            public DateTime Date { get; set; }
            public TransactionType TransactionType { get; set; }
            public string Exchange { get; set; }
            public decimal CryptocurrencyAmount { get; set; }
            public decimal CryptocurrencyPrice { get; set; }
            public bool ExcludeFromPortfolio { get; set; }
            public decimal TransactionCurrencyAmount { get => this.CryptocurrencyAmount * CryptocurrencyPrice; }
        }

        private sealed class CustomCsvImporterRecordClassMap : ClassMap<CustomCsvImporterRecord>
        {
            public CustomCsvImporterRecordClassMap(CustomCsvImporterDialog.CustomCsvHeaderSettings settings)
            {
                Map(m => m.Date).Name(settings.DateHeaderName);
                Map(m => m.TransactionType).Name(settings.TransactionTypeHeaderName).TypeConverter<TransactionTypeConverter>();
                Map(m => m.Exchange).Name(settings.ExchangeHeaderName);
                Map(m => m.CryptocurrencyAmount).Name(settings.CryptocurrencyAmountHeaderName);
                Map(m => m.CryptocurrencyPrice).Name(settings.CryptocurrencyPriceHeaderName);
                if(settings.HasExcludeFromPortfolioValues)
                {
                    Map(m => m.ExcludeFromPortfolio).Name(settings.ExcludeFromPortfolioHeaderName).TypeConverter<ExcludeFromPortfolioConverter>();
                }
                else
                {
                    Map(m => m.ExcludeFromPortfolio).Ignore();
                }

                Map(m => m.TransactionCurrencyAmount).Ignore();
            }

            private class TransactionTypeConverter : ITypeConverter
            {
                public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
                {
                    return text.ToLower().Contains("buy") ? TransactionType.Buy : TransactionType.Sell;
                }

                public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
                {
                    throw new NotImplementedException();
                }
            }

            private class ExcludeFromPortfolioConverter : ITypeConverter
            {
                public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
                {
                    return text.Equals("true", StringComparison.OrdinalIgnoreCase) || text.Equals("1");
                }

                public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
