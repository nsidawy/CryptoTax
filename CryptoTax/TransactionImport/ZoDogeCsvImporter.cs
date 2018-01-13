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

namespace CryptoTax.TransactionImport
{
    public class ZoDogeCsvImporter : ITransactionImporter
    {

        private readonly PriceInUsdProvider _priceInUsdProvider;

        public ZoDogeCsvImporter(PriceInUsdProvider priceInUsdProvider)
        {
            this._priceInUsdProvider = priceInUsdProvider;
        }

        public event RowProcessedEventHandler RowProcessed;

        public async Task<TransactionImportResult> ImportFile(TransactonImporterSettings settings)
        {
            var textReader = new StreamReader(settings.Filename);
            var csvReader = new CsvReader(textReader);
            csvReader.Configuration.RegisterClassMap<ZoDogeCsvImporterRecordClassMap>();

            var transactions = new List<Transaction>();
            var unknownDogecoinPriceIds = new HashSet<string>();
            var rowCount = 0;
            while (csvReader.Read())
            {
                var record = csvReader.GetRecord<ZoDogeCsvImporterRecord>();
                if(!record.IsConfirmed)
                {
                    continue;
                }

                var dogecoinPriceInUsdAtTransactionTime = await this._priceInUsdProvider.GetDogePrice(record.TimeStamp);

                if(!dogecoinPriceInUsdAtTransactionTime.HasValue)
                {
                    unknownDogecoinPriceIds.Add(record.Id);
                    continue;
                }

                transactions.Add(new Transaction
                {
                    Crypto = CryptocurrencyType.Dogecoin,
                    TransactionDate = record.TimeStamp,
                    TransactionType = record.IsReceived ? TransactionType.Buy : TransactionType.Sell,
                    Quantity = record.Amount,
                    UsDollarAmount = dogecoinPriceInUsdAtTransactionTime.Value * record.Amount
                });

                this.RowProcessed?.Invoke(this, new RowProcessedEventArgs { RowsProcessed = ++rowCount });
            }

            string message = null;
            if (unknownDogecoinPriceIds.Any())
            {
                message = "The following transaction(s) could not be imported because the historic price is unknown:" + Environment.NewLine
                    + string.Join(Environment.NewLine, unknownDogecoinPriceIds);
            }
            return new TransactionImportResult
            {
                IsSuccess = true,
                Transactions = transactions,
                Message = message
            };
        }

        private class ZoDogeCsvImporterRecord
        {
            public DateTime TimeStamp { get; set; }
            public decimal Amount { get; set; }
            public bool IsConfirmed { get; set; }
            public bool IsReceived { get; set; }
            public string Id { get; set; }
        }

        private sealed class ZoDogeCsvImporterRecordClassMap : ClassMap<ZoDogeCsvImporterRecord>
        {
            public ZoDogeCsvImporterRecordClassMap()
            {
                Map(m => m.TimeStamp).Name("Date");
                Map(m => m.Amount).Name("Amount (DOGE)").TypeConverter<AmountTypeConverter>();
                Map(m => m.IsConfirmed).Name("Confirmed");
                Map(m => m.Id).Name("ID");
                Map(m => m.IsReceived).Name("Type").TypeConverter<IsReceivedTypeConverter>();
            }

            private class IsReceivedTypeConverter : ITypeConverter
            {
                public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
                {
                    return text.Equals("received with", StringComparison.InvariantCultureIgnoreCase);
                }

                public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
                {
                    throw new NotImplementedException();
                }
            }

            private class AmountTypeConverter : ITypeConverter
            {
                public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
                {
                    return decimal.Parse(text, System.Globalization.NumberStyles.Any);
                }

                public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
