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
using System.Web;

namespace CryptoTax.Crypto
{
    public class CoinMarketCapDataProvider
    {
        private readonly string ApiKey = "8feee1aa-8179-4355-8b91-fc05096c391d";
        private readonly MemoryCache _cache;
        private readonly Func<CacheItemPolicy> _defaultCacheItemPolicyFactory = () => new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(60)
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
                { CryptoType.Solana, "solana" },
                { CryptoType.Coss, "coss" },
                { CryptoType.Holo, "holotoken" },
                { CryptoType.Avax, "avalanche-2" },
                { CryptoType.AxieInfinity, "axie-infinity" },
                { CryptoType.Ygg, "yield-guild-games" },
                { CryptoType.Shiba, "shiba-inu" },
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
                var url = new UriBuilder("https://api.coingecko.com/api/v3/simple/price");

                var queryString = HttpUtility.ParseQueryString(string.Empty);
                queryString["ids"] = _coinMarketcapCrypoLookupNames[cryptoType];
                queryString["vs_currencies"] = "USD";
                queryString["include_market_cap"] = "true";
                queryString["include_24hr_change"] = "true";

                url.Query = queryString.ToString();

                var client = new HttpClient
                {
                    BaseAddress = url.Uri
                };
                try
                {
                    HttpResponseMessage response = await client.GetAsync("");
                    var content = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var data = Newtonsoft.Json.Linq.JObject.Parse(content).First?.First;
                        if (data != null)
                        {
                            coinMarketCapData = new CoinMarketCapData
                            {
                                Crypto = cryptoType,
                                PriceInUsd = data.Value<decimal>("usd"),
                                TwentyFourHourChangePercent = data.Value<decimal>("usd_24h_change") / (decimal)100.0,
                                MarketCap = data.Value<decimal>("usd_market_cap"),
                            };
                        }

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
            public decimal TwentyFourHourChangePercent { get; set; }
            public decimal MarketCap { get; set; }
            public string Link { get => $"https://coinmarketcap.com/currencies/{_coinMarketcapCrypoLookupNames[this.Crypto]}/"; }
        }
    }
}
