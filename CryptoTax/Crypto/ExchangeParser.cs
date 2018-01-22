using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTax.Crypto
{
    public class ExchangeParser
    {
        private IReadOnlyDictionary<string, TransactionCurrencyType> _transactionCurrencySymbolMapping = new Dictionary<string, TransactionCurrencyType>
        {
            {"BTC", TransactionCurrencyType.Bitcoin },
            {"USD", TransactionCurrencyType.Usd },
            {"ETH", TransactionCurrencyType.Ethereum },
        };

        private IReadOnlyDictionary<string, CryptoType> _cryptoSymbolMapping = new Dictionary<string, CryptoType>
        {
            {"BTC", CryptoType.Bitcoin },
            {"ETH", CryptoType.Ethereum },
            {"LTC", CryptoType.Litecoin },
            {"BCH", CryptoType.BitcoinCash },
            {"DOGE", CryptoType.Dogecoin },
            {"XRP", CryptoType.Ripple },
            {"MANA", CryptoType.Decentraland },
            {"EMC2", CryptoType.Einsteinium },
            {"QTUM", CryptoType.Quantum },
            {"DASH", CryptoType.Dash },
            {"REP", CryptoType.Augur },
            {"ADA", CryptoType.Ada },
            {"XVG", CryptoType.Verge },
            {"NXT", CryptoType.Nxt },
            {"GNT", CryptoType.Golem },
            {"STRAT", CryptoType.Stratis },
            {"ZEC", CryptoType.ZCash },
            {"HMQ", CryptoType.Humaniq },
            {"XMR", CryptoType.Monero },
            {"XLM", CryptoType.Stellar },
            {"NEO", CryptoType.Neo },
            {"ZRX", CryptoType.ZeroX },
            {"RDD", CryptoType.Reddcoin },
            {"COSS", CryptoType.Coss },
        };

        public ExchangeResult ParseExchange(string exchange)
        {
            var symbols = exchange.Split('-');
            if(symbols.Length != 2)
            {
                throw new InvalidOperationException("Invalid exchange string. Must be of format [transaction currency]-[crypto]. Input: " + exchange);
            }

            if (!this._transactionCurrencySymbolMapping.TryGetValue(symbols[0], out TransactionCurrencyType transactionCurrencyType))
            {
                throw new InvalidOperationException("Unrecognized transaction currency: " + symbols[0]);
            }
            if (!this._cryptoSymbolMapping.TryGetValue(symbols[1], out CryptoType cryptoType))
            {
                throw new InvalidOperationException("Unrecognized transaction currency: " + symbols[0]);
            }

            return new ExchangeResult
            {
                TransactionCurrency = transactionCurrencyType,
                AssetCurrency = cryptoType,
            };
        }

        public class ExchangeResult
        {
            public TransactionCurrencyType TransactionCurrency { get; set; }
            public CryptoType AssetCurrency { get; set; }
        }
    }
}
