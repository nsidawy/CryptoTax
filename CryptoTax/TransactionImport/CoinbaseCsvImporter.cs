﻿using System;
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
    public class CoinbaseCsvImporter : ITransactionImporter
    {
        private IReadOnlyDictionary<string, Crypto.CryptoType> _cryptoMapping = new Dictionary<string, Crypto.CryptoType>
        {
            {"BTC", Crypto.CryptoType.Bitcoin },
            {"LTC", Crypto.CryptoType.Litecoin },
            {"ETH", Crypto.CryptoType.Ethereum },
            {"BCH", Crypto.CryptoType.BitcoinCash },
        };

        public event RowProcessedEventHandler RowProcessed;

        public Task<TransactionImportResult> ImportFile(TransactonImporterSettings settings)
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
            var rowCount = 0;
            while (csvReader.Read())
            {
                var record = csvReader.GetRecord<CoinbaseCsvRecord>();
                if (record.TransferTotal == null
                    || !this._cryptoMapping.ContainsKey(record.Currency))
                {
                    nonBuyOrSellTransactionCount++;
                    continue;
                }
                transactions.Add(new Transaction
                {
                    Crypto = this._cryptoMapping[record.Currency],
                    // Sell transactions appear as negative crypto amounts so that needs to be corrected.
                    Quantity = record.Amount.Value * (record.TransactionType == TransactionType.Buy ? 1 : -1),
                    TransactionDate = record.Timestamp,
                    TransactionType = record.TransactionType,
                    UsDollarAmount = record.TransferTotal.Value - record.TransferFee.Value
                });

                this.RowProcessed?.Invoke(this, new RowProcessedEventArgs { RowsProcessed = ++rowCount });
            }

            return Task.Factory.StartNew(() => new TransactionImportResult
            {
                IsSuccess = true,
                Transactions = transactions,
                Message = $"{nonBuyOrSellTransactionCount} item(s) in the CSV were ignored because they are not a buy or sell transction."
            });
        }

        private class CoinbaseCsvRecord
        {
            public decimal? Amount { get; set; }
            public string Currency { get; set; }
            public DateTime Timestamp { get; set; }
            public TransactionType TransactionType { get; set; }
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
                Map(m => m.TransactionType).Name("Notes").TypeConverter<NotesTypeConverter>();
                Map(m => m.TransferTotal).Name("Transfer Total");
                Map(m => m.TransferFee).Name("Transfer Fee");
                Map(m => m.TransferCurrency).Name("Transfer Total Currency");
            }

            private class NotesTypeConverter : ITypeConverter
            {
                public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
                {
                    return text.ToLower().StartsWith("bought") ? TransactionType.Buy : TransactionType.Sell; 
                }

                public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
