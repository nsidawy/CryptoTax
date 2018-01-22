using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Timers;

namespace CryptoTax.Crypto
{
    public class CoinMarketCapDataProvider
    {
        private readonly MemoryCache _cache;
        private readonly Func<CacheItemPolicy> _defaultCacheItemPolicyFactory = () => new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(30)
        };

        private static IReadOnlyDictionary<CryptoType, string> _coinMarketcapCrypoLookupNames =
            new Dictionary<CryptoType, string>
            {
                { CryptoType.Bitcoin, "bitcoin" },
                { CryptoType.Ethereum, "ethereum" },
                { CryptoType.Litecoin, "litecoin" },
                { CryptoType.Augur, "augur" },
                { CryptoType.Ada, "cardano" },
                { CryptoType.Dogecoin, "dogecoin" },
                { CryptoType.Golem, "golem" },
                { CryptoType.Ripple, "ripple" },
                { CryptoType.Stratis, "stratis" },
                { CryptoType.BitcoinCash, "bitcoin-cash" },
                { CryptoType.Decentraland, "decentraland" },
                { CryptoType.Dash, "dash" },
                { CryptoType.Einsteinium, "einsteinium" },
                { CryptoType.Monero, "monero" },
                { CryptoType.ZCash, "zcash" },
                { CryptoType.ZeroX, "0x" },
                { CryptoType.Humaniq, "humaniq" },
                { CryptoType.Quantum, "qtum" },
                { CryptoType.Verge, "verge" },
                { CryptoType.Nxt, "nxt" },
                { CryptoType.Reddcoin, "reddcoin" },
                { CryptoType.Neo, "neo" },
                { CryptoType.Stellar, "stellar" },
                { CryptoType.Coss, "coss" },
            };

        public CoinMarketCapDataProvider()
        {
            this._cache = new MemoryCache("CoinMarketCapDataProviderCache", null);
        }

        public async Task<CoinMarketCapData> GetCoinMarketCapData(CryptoType cryptoType)
        {
            if(!_coinMarketcapCrypoLookupNames.ContainsKey(cryptoType))
            {
                return null;
            }

            var coinMarketCapData = this._cache.Get(cryptoType.ToString()) as CoinMarketCapData;
            if(coinMarketCapData == null)
            {

                var client = new HttpClient
                {
                    BaseAddress = new Uri($"https://api.coinmarketcap.com/v1/ticker/{_coinMarketcapCrypoLookupNames[cryptoType]}/")
                };

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync("");
                    var content = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var data = Newtonsoft.Json.Linq.JArray.Parse(content).First();
                        coinMarketCapData = new CoinMarketCapData
                        {
                            Crypto = cryptoType,
                            PriceInUsd = data.Value<decimal>("price_usd"),
                            OneHourChangePercent = data.Value<decimal>("percent_change_1h") / (decimal)100.0,
                            TwentyFourHourChangePercent = data.Value<decimal>("percent_change_24h") / (decimal)100.0,
                            MarketCap = data.Value<decimal>("market_cap_usd"),
                        };

                        this._cache.Set(new CacheItem(cryptoType.ToString(), coinMarketCapData), this._defaultCacheItemPolicyFactory());
                    }
                }
                catch
                {
                    /* swallow exception */
                }
            }

            return coinMarketCapData;
        }

        public class CoinMarketCapData
        {
            public CryptoType Crypto { get; set; }
            public decimal PriceInUsd { get; set; }
            public decimal OneHourChangePercent { get; set; }
            public decimal TwentyFourHourChangePercent { get; set; }
            public decimal MarketCap { get; set; }
            public string Link { get => $"https://coinmarketcap.com/currencies/{_coinMarketcapCrypoLookupNames[this.Crypto]}/"; }
        }
    }
}
