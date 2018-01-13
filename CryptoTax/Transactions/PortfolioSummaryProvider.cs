﻿using CryptoTax.Cryptocurrency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTax.Transactions
{
    public class PortfolioSummaryProvider
    {
        private readonly TaxCalculator _taxCalculator;

        public PortfolioSummaryProvider(TaxCalculator taxCalculator)
        {
            this._taxCalculator = taxCalculator;
        }

        public IReadOnlyList<CryptoPortfolioSummaryInfo> GetCryptocurrencyPortfolioSummaryInfo(
            IReadOnlyList<Transaction> transactions,
            IReadOnlyDictionary<CryptocurrencyType, CoinMarketCapDataProvider.CoinMarketCapData> coinMarketCapData)
        {
            var groupedTransactions = transactions.GroupBy(x => x.Crypto);
            var summaryInfos = new List<CryptoPortfolioSummaryInfo>();
            foreach (var groupedTransaction in groupedTransactions)
            {
                coinMarketCapData.TryGetValue(groupedTransaction.Key, out CoinMarketCapDataProvider.CoinMarketCapData data);

                var heldAssets = this.GetHeldAssets(groupedTransaction.ToList());
                decimal? averagePriceBought = null;
                decimal? percentReturn = null;
                var totalAssetAmount = heldAssets.Sum(x => x.Amount);
                if (totalAssetAmount != 0)
                {
                    averagePriceBought = heldAssets.Aggregate((decimal)0, (t, a) => t + a.Amount * a.ExchangeRate) / totalAssetAmount;
                }

                summaryInfos.Add(new CryptoPortfolioSummaryInfo
                {
                    Crypto = groupedTransaction.Key,
                    PriceInUsd = data?.PriceInUsd,
                    OneHourChange = data?.OneHourChangePercent,
                    TwentyFourHourChange = data?.TwentyFourHourChangePercent,
                    MarketCap = data?.MarketCap,
                    AveragePriceBought = averagePriceBought,
                    Link = data?.Link,
                    Quantity = totalAssetAmount
                });
            }

            return summaryInfos.Where(x => x.Quantity != 0).ToList();
        }

        public IReadOnlyList<CryptoYearSummaryInfo> GetCryptocurrencyYearSummaryInfo(
            IReadOnlyList<Transaction> transactions)
        {
            var yearSummaryInfos = new List<CryptoYearSummaryInfo>();
            foreach (var cryptocurrency in transactions.Select(x => x.Crypto).Distinct())
            {
                var thisCryptoTransactions = transactions.Where(t => t.Crypto == cryptocurrency);
                var transactionYears = thisCryptoTransactions.Select(x => x.TransactionDate.Year).Distinct().ToList();
                var yearToSummaryInfoLookUp = transactionYears.ToDictionary(x => x, x => new CryptoYearSummaryInfo
                {
                    Year = x,
                    Crypto = cryptocurrency
                });

                // calculate USD invested and returns
                thisCryptoTransactions
                    .GroupBy(t => t.TransactionDate.Year)
                    .ToList()
                    .ForEach(x =>
                    {
                        var yearToSummaryInfo = yearToSummaryInfoLookUp[x.Key];
                        yearToSummaryInfo.UsdInvested = x
                            .Where(t => t.TransactionType == TransactionType.Buy)
                            .Sum(t => t.UsDollarAmount);
                        yearToSummaryInfo.UsdReturns = x
                            .Where(t => t.TransactionType == TransactionType.Sell)
                            .Sum(t => t.UsDollarAmount);
                        yearToSummaryInfo.QuantityBought = x
                            .Where(t => t.TransactionType == TransactionType.Buy)
                            .Sum(t => t.Quantity);
                        yearToSummaryInfo.QuantitySold = x
                            .Where(t => t.TransactionType == TransactionType.Sell)
                            .Sum(t => t.Quantity);
                    });

                // calculate capital gains with both accounting methods
                var capitalGainsLifo = this._taxCalculator.CalculateCapialGains(transactions, AccountingMethodType.Lifo, cryptocurrency);
                var capitalGainsFifo = this._taxCalculator.CalculateCapialGains(transactions, AccountingMethodType.Fifo, cryptocurrency);
                
                if (capitalGainsFifo != null)
                {
                    transactionYears.ForEach(x =>
                    {
                        yearToSummaryInfoLookUp[x].FifoLongTermCapitalGains = capitalGainsFifo.Where(y => y.IsLongTerm && y.YearIncurred == x).Sum(y => y.UsdAmount);
                        yearToSummaryInfoLookUp[x].FifoShortTermCapitalGains = capitalGainsFifo.Where(y => !y.IsLongTerm && y.YearIncurred == x).Sum(y => y.UsdAmount);

                    });
                }
                if (capitalGainsLifo != null)
                {
                    transactionYears.ForEach(x =>
                    {
                        yearToSummaryInfoLookUp[x].LifoLongTermCapitalGains = capitalGainsLifo.Where(y => y.IsLongTerm && y.YearIncurred == x).Sum(y => y.UsdAmount);
                        yearToSummaryInfoLookUp[x].LifoShortTermCapitalGains = capitalGainsLifo.Where(y => !y.IsLongTerm && y.YearIncurred == x).Sum(y => y.UsdAmount);

                    });
                }

                yearSummaryInfos.AddRange(yearToSummaryInfoLookUp.Values.OrderBy(x => x.Crypto).ThenBy(x => x.Year));
            }

            return yearSummaryInfos;
        }

        private List<Asset> GetHeldAssets(IReadOnlyCollection<Transaction> transactions)
        {
            var assetCollection = new AssetCollection(AccountingMethodType.Lifo);

            var sortedtransactions = transactions
                .OrderBy(x => x.TransactionDate);

            foreach (var transaction in sortedtransactions)
            {
                switch (transaction.TransactionType)
                {
                    case TransactionType.Buy:
                        assetCollection.Add(new Asset
                        {
                            TransactionDate = transaction.TransactionDate,
                            Amount = transaction.Quantity,
                            ExchangeRate = transaction.PriceInUsd
                        });
                        break;
                    case TransactionType.Sell:
                        var cryptocurrencySellAmount = transaction.Quantity;
                        while (cryptocurrencySellAmount > 0)
                        {
                            if (assetCollection.Count == 0)
                            {
                                // return an empty list if the assets can't be correctly calculated
                                return new List<Asset>();
                            }
                            Asset soldAsset;
                            decimal sellAmount;
                            if (assetCollection.Peek().Amount <= cryptocurrencySellAmount)
                            {
                                soldAsset = assetCollection.Pop();
                                sellAmount = soldAsset.Amount;
                            }
                            else
                            {
                                soldAsset = assetCollection.Peek();
                                sellAmount = cryptocurrencySellAmount;
                                soldAsset.Amount -= sellAmount;
                            }
                            cryptocurrencySellAmount -= sellAmount;
                        }
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }

            return assetCollection.ToList();
        }

        private TValue GetValueOrDefault<TKey, TValue>(IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue defaultValue)
        {
            return dictionary.TryGetValue(key, out TValue value) ? value : defaultValue;
        }


        public class CryptoPortfolioSummaryInfo
        {
            public CryptocurrencyType Crypto { get; set; }
            public decimal? OneHourChange { get; set; }
            public decimal? TwentyFourHourChange { get; set; }
            public decimal? Return => (this.PriceInUsd / this.AveragePriceBought) - 1;
            public decimal Quantity { get; set; }
            public decimal? TotalUsd => this.PriceInUsd * this.Quantity;
            public decimal? PriceInUsd { get; set; }
            public decimal? AveragePriceBought { get; set; }
            public decimal? MarketCap { get; set; }
            public string Link { get; set; }
        }
        
        public class CryptoYearSummaryInfo
        {
            public CryptocurrencyType Crypto { get; set; }
            public int Year { get; set; }
            public decimal QuantityBought { get; set; }
            public decimal QuantitySold { get; set; }
            public decimal UsdInvested { get; set; }
            public decimal UsdReturns { get; set; }
            public decimal? LifoLongTermCapitalGains { get; set; }
            public decimal? LifoShortTermCapitalGains { get; set; }
            public decimal? FifoLongTermCapitalGains { get; set; }
            public decimal? FifoShortTermCapitalGains { get; set; }
        }
    }
}
