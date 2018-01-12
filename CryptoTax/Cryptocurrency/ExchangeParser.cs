using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTax.Cryptocurrency
{
    public class ExchangeParser
    {
        private IReadOnlyDictionary<string, TransactionCurrencyType> _transactionCurrencySymbolMapping = new Dictionary<string, TransactionCurrencyType>
        {
            {"BTC", TransactionCurrencyType.Bitcoin },
            {"USD", TransactionCurrencyType.Usd },
            {"ETH", TransactionCurrencyType.Ethereum },
        };

        private IReadOnlyDictionary<string, CryptocurrencyType> _cryptocurrencySymbolMapping = new Dictionary<string, CryptocurrencyType>
        {
            {"BTC", CryptocurrencyType.Bitcoin },
            {"ETH", CryptocurrencyType.Ethereum },
            {"LTC", CryptocurrencyType.Litecoin },
            {"BCH", CryptocurrencyType.BitcoinCash },
            {"DOGE", CryptocurrencyType.Dogecoin },
            {"XRP", CryptocurrencyType.Ripple },
            {"MANA", CryptocurrencyType.Decentraland },
            {"EMC2", CryptocurrencyType.Einsteinium },
            {"QTUM", CryptocurrencyType.Quantum },
            {"DASH", CryptocurrencyType.Dash },
            {"REP", CryptocurrencyType.Augur },
            {"ADA", CryptocurrencyType.Ada },
            {"XVG", CryptocurrencyType.Verge },
            {"NXT", CryptocurrencyType.Nxt },
            {"GNT", CryptocurrencyType.Golem },
            {"STRAT", CryptocurrencyType.Stratis },
            {"ZEC", CryptocurrencyType.ZCash },
            {"HMQ", CryptocurrencyType.Humaniq },
            {"XMR", CryptocurrencyType.Monero },
            {"XLM", CryptocurrencyType.Stellar },
            {"NEO", CryptocurrencyType.Neo },
            {"ZRX", CryptocurrencyType.ZeroX },
            {"RDD", CryptocurrencyType.Reddcoin },
            {"COSS", CryptocurrencyType.Coss },
        };

        public ExchangeResult ParseExchange(string exchange)
        {
            var symbols = exchange.Split('-');
            if(symbols.Length != 2)
            {
                throw new InvalidOperationException("Invalid exchange string. Must be of format [transaction currency]-[cryptocurrency]. Input: " + exchange);
            }

            if (!this._transactionCurrencySymbolMapping.TryGetValue(symbols[0], out TransactionCurrencyType transactionCurrencyType))
            {
                throw new InvalidOperationException("Unrecognized transaction currency: " + symbols[0]);
            }
            if (!this._cryptocurrencySymbolMapping.TryGetValue(symbols[1], out CryptocurrencyType cryptocurrencyType))
            {
                throw new InvalidOperationException("Unrecognized transaction currency: " + symbols[0]);
            }

            return new ExchangeResult
            {
                TransactionCurrency = transactionCurrencyType,
                AssetCurrency = cryptocurrencyType,
            };
        }

        public class ExchangeResult
        {
            public TransactionCurrencyType TransactionCurrency { get; set; }
            public CryptocurrencyType AssetCurrency { get; set; }
        }
    }
}
