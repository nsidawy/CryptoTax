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

        public CustomCsvImporter(
            PriceInUsdProvider priceInUsdProvider,
            FormFactory formFactory)
        {
            this._priceInUsdProvider = priceInUsdProvider;
            this._formFactory = formFactory;
        }

        public TransactionImportResult ImportFile(TransactonImporterSettings settings)
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
            while (csvReader.Read())
            {
                var record = csvReader.GetRecord<CustomCsvImporterRecord>();
                transactions.Add(new Transaction {
                    TransactionDate = record.Date,
                    TransactionType = record.TransactionType,
                    ExcludeFromPortfolio = record.ExcludeFromPortfolio,
                    Cryptocurrency = CryptocurrencyType.Bitcoin,
                    CryptocurrencyAmount = record.CryptocurrencyAmount,
                    UsDollarAmount = record.CryptocurrencyAmount * record.CryptocurrencyPrice
                });
            }

            return new TransactionImportResult
            {
                IsSuccess = true,
                Transactions = transactions,
            };
        }

        private class CustomCsvImporterRecord
        {
            public DateTime Date { get; set; }
            public TransactionType TransactionType { get; set; }
            public string Exchange { get; set; }
            public decimal CryptocurrencyAmount { get; set; }
            public decimal CryptocurrencyPrice { get; set; }
            public bool ExcludeFromPortfolio { get; set; }
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
                Map(m => m.ExcludeFromPortfolio).Name(settings.ExcludeFromPortfolioHeaderName).TypeConverter<ExcludeFromPortfolioConverter>();
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
