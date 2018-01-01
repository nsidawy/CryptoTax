using CryptoTax.Cryptocurrency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTax.Transactions
{
    public static class TransactionParsingUtilities
    {
        public static Transaction ParseFileRow(string row)
        {
            var values = row.Split(',');
            return new Transaction
            {
                TransactionDate = DateTime.Parse(values[0]),
                TransactionType = (TransactionType)Enum.Parse(typeof(TransactionType), values[1]),
                Cryptocurrency = (CryptocurrencyType)Enum.Parse(typeof(CryptocurrencyType), values[2]),
                CryptocurrencyAmount = Decimal.Parse(values[3]),
                UsDollarAmount = Decimal.Parse(values[4])
            };
        }

        public static string TransactionToFileRow(this Transaction @this)
        {
            return $"{@this.TransactionDate},{@this.TransactionType},{@this.Cryptocurrency},{@this.CryptocurrencyAmount},{@this.UsDollarAmount}";
        }
    }
}
