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

namespace CryptoTax.Crypto
{
    public class PriceInUsdProvider
    {
       public async Task<decimal> GetBitcoinPrice(DateTime transactionTime)
        {
            return await this.GetGdaxPrice(transactionTime, "BTC-USD");
        }

        public async Task<decimal> GetEthereumPrice(DateTime transactionTime)
        {
            return await this.GetGdaxPrice(transactionTime, "ETH-USD");
        }

        // see https://docs.gdax.com/#get-historic-rates for API method documentation
        private async Task<decimal> GetGdaxPrice(DateTime transactionTime, string exchange)
        {
            var startTime = new DateTime(transactionTime.Year, transactionTime.Month, transactionTime.Day, transactionTime.Hour, transactionTime.Minute, 0);
            var endTime = startTime.AddMinutes(1);
            // request the prices at the minute of the transaction
            var startTimeString = startTime.ToString("yyyy-MM-ddTHH\\:mm\\:ss");
            var endTimeTimeString = endTime.ToString("yyyy-MM-ddTHH\\:mm\\:ss");

            var address = $"https://api.gdax.com/products/{exchange}/candles?start={startTimeString}&end={endTimeTimeString}&granularity=60";
            var client = new HttpClient
            {
                BaseAddress = new Uri(address)
            };
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // User-Agent header required by Gdax. 
            client.DefaultRequestHeaders.Add("User-Agent", "C# App");
            HttpResponseMessage response = null;
            try
            {
                response = await client.GetAsync("");
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    // get the first (and only item)
                    var values = Newtonsoft.Json.Linq.JArray.Parse(responseContent)[0];
                    // array items: [ time, low, high, open, close, volume ]
                    // get the close value
                    return (decimal)values[4];
                }
                else
                {
                    throw new InvalidOperationException("Request to Gdax for historic bitcoin price failed."
                        + Environment.NewLine + Environment.NewLine + responseContent);
                }
            }
            catch (Exception e)
            {
                // if we are hitting gdax rate limits, wait one second then try again
                // https://docs.gdax.com/#rate-limits
                if (response != null && response.StatusCode == (System.Net.HttpStatusCode)429)
                {
                    Thread.Sleep(1000);
                    return await this.GetGdaxPrice(transactionTime, exchange);
                }
                throw new InvalidOperationException("Unable to get bitcoin price from Gdax rest API.", e);
            }
        }

        // see https://www.cryptocompare.com/api/#-api-data-pricehistorical for API method documentation
        public async Task<decimal?> GetDogePrice(DateTime transactionTime)
        {
            // request the prices at the minute of the transaction
            var unixTimestamp = (int)(transactionTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);

            var address = $"https://min-api.cryptocompare.com/data/pricehistorical?fsym=DOGE&tsyms=USD&ts={unixTimestamp}";
            var client = new HttpClient
            {
                BaseAddress = new Uri(address)
            };
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // User-Agent header required by Gdax. 
            client.DefaultRequestHeaders.Add("User-Agent", "C# App");
            HttpResponseMessage response = null;
            try
            {
                response = await client.GetAsync("");
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var value = Newtonsoft.Json.Linq.JObject.Parse(responseContent)["DOGE"].Value<decimal>("USD");
                    return value == 0 ? (decimal?)null : value;
                }
                else
                {
                    throw new InvalidOperationException("Request to CryptoCompare for historic dogecoin price failed."
                        + Environment.NewLine + Environment.NewLine + responseContent);
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to get dogecoin price from CryptoCompare rest API.", e);
            }
        }
    }
}
