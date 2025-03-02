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
using Microsoft.AspNetCore.WebUtilities;
using System.Windows.Forms;

namespace CryptoTax.Crypto
{
    public class CryptoDataProvider
    {
        private readonly Func<CacheItemPolicy> _defaultCacheItemPolicyFactory = () => new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(60)
        };

        private static IReadOnlyDictionary<CryptoType, string> _cryptoLookupNames =
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
                { CryptoType.SmoothLovePotion, "smooth-love-potion" },
                { CryptoType.Stellar, "stellar" },
                { CryptoType.Solana, "solana" },
                { CryptoType.Coss, "coss" },
                { CryptoType.Holo, "holotoken" },
                { CryptoType.Avax, "avalanche-2" },
                { CryptoType.AxieInfinity, "axie-infinity" },
                { CryptoType.Ygg, "yield-guild-games" },
                { CryptoType.Shiba, "shiba-inu" },
                { CryptoType.Ronin, "ronin" },
                { CryptoType.Meld, "meld" },
                { CryptoType.Milk, "muesliswap-milk" },
                { CryptoType.Rainbow, "rainbow-token-2" },
                { CryptoType.Ergo, "ergo" },
                { CryptoType.Min, "minswap" },
                { CryptoType.Liqwid, "liqwid-finance" },
                { CryptoType.Axo, "axo" },
            };

        public async Task<List<CoinMarketCapData>> GetCryptoData(IReadOnlyCollection<CryptoType> cryptoTypes)
        {
            var supportedCryptos = cryptoTypes.Where(_cryptoLookupNames.ContainsKey)
                .Select(c => _cryptoLookupNames[c]);

            var queryString = new Dictionary<string, string>();
            queryString["ids"] = String.Join(",", supportedCryptos);
            queryString["vs_currencies"] = "USD";
            queryString["include_market_cap"] = "true";
            queryString["include_24hr_change"] = "true";
            var client = new HttpClient
            {
                BaseAddress = new Uri(QueryHelpers.AddQueryString("https://api.coingecko.com/api/v3/simple/price", queryString))
            };
            var newCoinMarketCapData = new List<CoinMarketCapData>();
            try
            {
                HttpResponseMessage response = await client.GetAsync("");
                var content = await response.Content.ReadAsStringAsync();
                if(!response.IsSuccessStatusCode)
                {
                    // No data pulled
                    return null;
                }
                var data = Newtonsoft.Json.Linq.JObject.Parse(content);
                foreach(var d in data)
                {
                    newCoinMarketCapData.Add(new CoinMarketCapData
                    {
                        Crypto = _cryptoLookupNames.Single(kv => kv.Value == d.Key).Key,
                        PriceInUsd = d.Value.Value<decimal>("usd"),
                        TwentyFourHourChangePercent = d.Value.Value<decimal>("usd_24h_change") / (decimal)100.0,
                        MarketCap = d.Value.Value<decimal>("usd_market_cap"),
                    });
                }
            }
            catch (Exception ex) {
               MessageBox.Show(ex.Message);
            }

            return newCoinMarketCapData;
        }

        public class CoinMarketCapData
        {
            public CryptoType Crypto { get; set; }
            public decimal PriceInUsd { get; set; }
            public decimal TwentyFourHourChangePercent { get; set; }
            public decimal MarketCap { get; set; }
            public string Link { get => $"https://coinmarketcap.com/currencies/{_cryptoLookupNames[this.Crypto]}/"; }
        }
    }
}
