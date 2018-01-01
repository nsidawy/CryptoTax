﻿using Newtonsoft.Json;
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
    public class PriceInUsdProvider
    {
        private IReadOnlyDictionary<CryptocurrencyType, string> _coinMarketcapCrypocurrencyLookupNames =
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
            };

        public async Task<decimal?> GetPriceInUsd(CryptocurrencyType cryptocurrencyType)
        {
            if(!this._coinMarketcapCrypocurrencyLookupNames.ContainsKey(cryptocurrencyType))
            {
                return null;
            }

            var client = new HttpClient
            {
                BaseAddress = new Uri($"https://api.coinmarketcap.com/v1/ticker/{this._coinMarketcapCrypocurrencyLookupNames[cryptocurrencyType]}/")
            };

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = await client.GetAsync("").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var values = Newtonsoft.Json.Linq.JArray.Parse(content);
                    return values.First().Value<decimal>("price_usd");
                }
            }
            catch (Exception e) {
                /* swallow exceptions for now*/
            }

            return null;
        }


        // see https://docs.gdax.com/#get-historic-rates for API method documentation
        public async Task<decimal> GetBitcoinPrice(DateTime transactionTime)
        {
            var startTime = new DateTime(transactionTime.Year, transactionTime.Month, transactionTime.Day, transactionTime.Hour, transactionTime.Minute, 0);
            var endTime = startTime.AddMinutes(1);
            // request the prices at the minute of the transaction
            var startTimeString = startTime.ToString("yyyy-MM-ddTHH\\:mm\\:ss");
            var endTimeTimeString = endTime.ToString("yyyy-MM-ddTHH\\:mm\\:ss");

            var address = $"https://api.gdax.com/products/BTC-USD/candles?start={startTimeString}&end={endTimeTimeString}&granularity=60";
            var client = new HttpClient
            {
                BaseAddress = new Uri($"https://api.gdax.com/products/BTC-USD/candles?start={startTimeString}&end={endTimeTimeString}&granularity=60")
            };
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // User-Agent header required by Gdax. 
            client.DefaultRequestHeaders.Add("User-Agent", "C# App");
            HttpResponseMessage response = null;
            try
            {
                response = await client.GetAsync("").ConfigureAwait(false);
                var respondeContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    // get the first (and only item)
                    var values = Newtonsoft.Json.Linq.JArray.Parse(respondeContent)[0];
                    // array items: [ time, low, high, open, close, volume ]
                    // get the close value
                    return (decimal)values[4];
                }
                else
                {
                    throw new InvalidOperationException("Request to Gdax for historic bitcoin price failed."
                        + Environment.NewLine + Environment.NewLine + respondeContent);
                }
            }
            catch (Exception e)
            {
                // if we are hitting gdax rate limits, wait one second then try again
                // https://docs.gdax.com/#rate-limits
                if (response != null && response.StatusCode == (System.Net.HttpStatusCode)429)
                {
                    Thread.Sleep(1000);
                    return await this.GetBitcoinPrice(transactionTime);
                }
                throw new InvalidOperationException("Unable to get bitcoin price from Gdax rest API.", e);
            }
        }
    }
}