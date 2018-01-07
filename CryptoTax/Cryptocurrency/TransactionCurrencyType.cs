using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTax.Cryptocurrency
{
    public enum TransactionCurrencyType
    {
        Usd,
        Bitcoin,
        Ethereum
    }

    public static class TransactionCurrencyTypeUtilities
    {
        private static readonly Dictionary<TransactionCurrencyType, CryptocurrencyType> _transactionToCryptoMapping = new Dictionary<TransactionCurrencyType, CryptocurrencyType>
        {
            {TransactionCurrencyType.Bitcoin, CryptocurrencyType.Bitcoin },
            {TransactionCurrencyType.Ethereum, CryptocurrencyType.Ethereum },
        };

        public static CryptocurrencyType ToCryptocurrencyType(this TransactionCurrencyType transactionCurrency)
        {
            if(_transactionToCryptoMapping.TryGetValue(transactionCurrency, out CryptocurrencyType cryptocurrency))
            {
                return cryptocurrency;
            }
            throw new InvalidOperationException($"To cryptocurrency mapping for {transactionCurrency}");
        }
    }
}
