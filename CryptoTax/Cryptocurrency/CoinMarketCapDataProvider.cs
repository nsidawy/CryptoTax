using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace CryptoTax.Cryptocurrency
{
    public class CoinMarketCapDataProvider
    {
        private static IReadOnlyDictionary<CryptocurrencyType, string> _coinMarketcapCrypocurrencyLookupNames =
            new Dictionary<CryptocurrencyType, string>
            {
                { CryptocurrencyType.Bitcoin, "bitcoin" },
                { CryptocurrencyType.Ethereum, "ethereum" },
                { CryptocurrencyType.Litecoin, "litecoin" },
                { CryptocurrencyType.Augur, "augur" },
                { CryptocurrencyType.Ada, "cardano" },
                { CryptocurrencyType.Dogecoin, "dogecoin" },
                { CryptocurrencyType.Golem, "golem" },
                { CryptocurrencyType.Ripple, "ripple" },
                { CryptocurrencyType.Stratis, "stratis" },
                { CryptocurrencyType.BitcoinCash, "bitcoin-cash" },
                { CryptocurrencyType.Decentraland, "decentraland" },
                { CryptocurrencyType.Dash, "dash" },
                { CryptocurrencyType.Einsteinium, "einsteinium" },
                { CryptocurrencyType.Monero, "monero" },
                { CryptocurrencyType.ZCash, "zcash" },
                { CryptocurrencyType.ZeroX, "0x" },
                { CryptocurrencyType.Humaniq, "humaniq" },
                { CryptocurrencyType.Quantum, "qtum" },
                { CryptocurrencyType.Verge, "verge" },
                { CryptocurrencyType.Nxt, "nxt" },
                { CryptocurrencyType.Reddcoin, "reddcoin" },
                { CryptocurrencyType.Neo, "neo" },
                { CryptocurrencyType.Stellar, "stellar" },
                { CryptocurrencyType.Coss, "coss" },
            };

        public async Task<CoinMarketCapData> GetCoinMarketCapData(CryptocurrencyType cryptocurrencyType)
        {
            if(!_coinMarketcapCrypocurrencyLookupNames.ContainsKey(cryptocurrencyType))
            {
                return null;
            }

            var client = new HttpClient
            {
                BaseAddress = new Uri($"https://api.coinmarketcap.com/v1/ticker/{_coinMarketcapCrypocurrencyLookupNames[cryptocurrencyType]}/")
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
                    return new CoinMarketCapData
                    {
                        CryptocurrencyType = cryptocurrencyType,
                        PriceInUsd = data.Value<decimal>("price_usd"),
                        OneHourChangePercent = data.Value<decimal>("percent_change_1h") / (decimal)100.0,
                        TwentyFourHourChangePercent = data.Value<decimal>("percent_change_24h") / (decimal)100.0,
                        MarketCap = data.Value<decimal>("market_cap_usd"),
                    };
                }
            }
            catch (Exception e) {
                /* swallow exceptions for now*/
            }

            return null;
        }

        public class CoinMarketCapData
        {
            public CryptocurrencyType CryptocurrencyType { get; set; }
            public decimal PriceInUsd { get; set; }
            public decimal OneHourChangePercent { get; set; }
            public decimal TwentyFourHourChangePercent { get; set; }
            public decimal MarketCap { get; set; }
            public string Link { get => $"https://coinmarketcap.com/currencies/{_coinMarketcapCrypocurrencyLookupNames[this.CryptocurrencyType]}/"; }
        }
    }
}
