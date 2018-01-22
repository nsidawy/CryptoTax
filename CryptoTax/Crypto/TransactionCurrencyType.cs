using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTax.Crypto
{
    public enum TransactionCurrencyType
    {
        Usd,
        Bitcoin,
        Ethereum
    }

    public static class TransactionCurrencyTypeUtilities
    {
        private static readonly Dictionary<TransactionCurrencyType, CryptoType> _transactionToCryptoMapping = new Dictionary<TransactionCurrencyType, CryptoType>
        {
            {TransactionCurrencyType.Bitcoin, CryptoType.Bitcoin },
            {TransactionCurrencyType.Ethereum, CryptoType.Ethereum },
        };

        public static CryptoType ToCryptoType(this TransactionCurrencyType transactionCurrency)
        {
            if(_transactionToCryptoMapping.TryGetValue(transactionCurrency, out CryptoType crypto))
            {
                return crypto;
            }
            throw new InvalidOperationException($"To crypto mapping for {transactionCurrency}");
        }
    }
}
