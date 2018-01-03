using CryptoTax.Cryptocurrency;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTax.Transactions
{
    public static class TransactionParsingUtilities
    {
        public static IEnumerable<Transaction> ReadTransactionsFromStream(StreamReader filestream)
        {
            var csvReader = new CsvReader(filestream);
            csvReader.Configuration.RegisterClassMap<TransactionRecordMapper>();
            // no headers on this file
            csvReader.Configuration.HasHeaderRecord = false;
            csvReader.Configuration.MissingFieldFound = null;

            var transactions = new List<Transaction>();
            var unknownDogecoinPriceIds = new HashSet<string>();
            while (csvReader.Read())
            {
                yield return csvReader.GetRecord<Transaction>();
            }
        }

        public static void SaveTransactionsToStream(StreamWriter filestream, IReadOnlyList<Transaction> transactions)
        {
            var csvWriter = new CsvWriter(filestream);
            csvWriter.Configuration.RegisterClassMap<TransactionRecordMapper>();
            csvWriter.Configuration.HasHeaderRecord = false;
            csvWriter.Configuration.Delimiter = ",";
            csvWriter.WriteRecords(transactions);
            csvWriter.Flush();
        }

        private class TransactionRecordMapper : ClassMap<Transaction>
        {
            public TransactionRecordMapper()
            {
                Map(m => m.TransactionDate).Index(0);
                Map(m => m.TransactionType).Index(1);
                Map(m => m.Cryptocurrency).Index(2);
                Map(m => m.CryptocurrencyAmount).Index(3);
                Map(m => m.UsDollarAmount).Index(4);
                Map(m => m.ExcludeFromPortfolio).Index(5).Default(false);

                Map(m => m.PriceInUsd).Ignore();
            }
        }
    }
}
