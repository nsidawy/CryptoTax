using CryptoTax.Cryptocurrency;
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

        public IReadOnlyList<CryptocurrencyPortfolioSummaryInfo> GetCryptocurrencyPortfolioSummaryInfo(
            IReadOnlyList<Transaction> transactions,
            IReadOnlyDictionary<CryptocurrencyType, CoinMarketCapDataProvider.CoinMarketCapData> coinMarketCapData)
        {
            var groupedTransactions = transactions.GroupBy(x => x.Cryptocurrency);
            var summaryInfos = new List<CryptocurrencyPortfolioSummaryInfo>();
            foreach (var groupedTransaction in groupedTransactions)
            {
                coinMarketCapData.TryGetValue(groupedTransaction.Key, out CoinMarketCapDataProvider.CoinMarketCapData data);
                summaryInfos.Add(new CryptocurrencyPortfolioSummaryInfo
                {
                    Cryptocurrency = groupedTransaction.Key,
                    PriceInUsd = data?.PriceInUsd,
                    OneHourChange = data?.OneHourChangePercent,
                    TwentyFourHourChange = data?.TwentyFourHourChangePercent,
                    MarketCap = data?.MarketCap,
                    AveragePriceBought = this.GetAverageAssetPrice(groupedTransaction.ToList()),
                    Link = data?.Link,
                    CryptocurrencyAmount = groupedTransaction.Aggregate((decimal)0, (val, t) =>
                    {
                        switch (t.TransactionType)
                        {
                            case TransactionType.Buy:
                                return val + t.CryptocurrencyAmount;
                            case TransactionType.Sell:
                                return val - t.CryptocurrencyAmount;
                            default:
                                throw new InvalidOperationException($"Unknown value of Transaction Type: {t.TransactionType}");
                        }
                    })
                });
            }

            return summaryInfos;
        }

        public IReadOnlyList<CryptocurrencyYearSummaryInfo> GetCryptocurrencyYearSummaryInfo(
            IReadOnlyList<Transaction> transactions)
        {
            var yearSummaryInfos = new List<CryptocurrencyYearSummaryInfo>();
            foreach (var cryptocurrency in transactions.Select(x => x.Cryptocurrency).Distinct())
            {
                var thisCryptoTransactions = transactions.Where(t => t.Cryptocurrency == cryptocurrency);
                var transactionYears = thisCryptoTransactions.Select(x => x.TransactionDate.Year).Distinct().ToList();
                var yearToSummaryInfoLookUp = transactionYears.ToDictionary(x => x, x => new CryptocurrencyYearSummaryInfo
                {
                    Year = x,
                    Cryptocurrency = cryptocurrency
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
                        yearToSummaryInfo.CryptocurrencyBought = x
                            .Where(t => t.TransactionType == TransactionType.Buy)
                            .Sum(t => t.CryptocurrencyAmount);
                        yearToSummaryInfo.CryptocurrencySold = x
                            .Where(t => t.TransactionType == TransactionType.Sell)
                            .Sum(t => t.CryptocurrencyAmount);
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

                yearSummaryInfos.AddRange(yearToSummaryInfoLookUp.Values.OrderBy(x => x.Cryptocurrency).ThenBy(x => x.Year));
            }

            return yearSummaryInfos;
        }

        private decimal? GetAverageAssetPrice(IReadOnlyCollection<Transaction> transactions)
        {
            var AssetCollection = new AssetCollection(AccountingMethodType.Lifo);

            var sortedtransactions = transactions
                .OrderBy(x => x.TransactionDate);

            foreach (var transaction in sortedtransactions)
            {
                switch (transaction.TransactionType)
                {
                    case TransactionType.Buy:
                        AssetCollection.Add(new Asset
                        {
                            TransactionDate = transaction.TransactionDate,
                            Amount = transaction.CryptocurrencyAmount,
                            ExchangeRate = transaction.PriceInUsd
                        });
                        break;
                    case TransactionType.Sell:
                        var cryptocurrencySellAmount = transaction.CryptocurrencyAmount;
                        while (cryptocurrencySellAmount > 0)
                        {
                            if (AssetCollection.Count == 0)
                            {
                                return null;
                            }
                            Asset soldAsset;
                            decimal sellAmount;
                            if (AssetCollection.Peek().Amount <= cryptocurrencySellAmount)
                            {
                                soldAsset = AssetCollection.Pop();
                                sellAmount = soldAsset.Amount;
                            }
                            else
                            {
                                soldAsset = AssetCollection.Peek();
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

            var assets = AssetCollection.ToList();
            var totalWeight = assets.Sum(x => x.Amount);
            if(totalWeight == 0)
            {
                return null;
            }

            return assets.Aggregate((decimal)0, (t, a) => t + a.Amount * a.ExchangeRate) / totalWeight;
        }

        private TValue GetValueOrDefault<TKey, TValue>(IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue defaultValue)
        {
            return dictionary.TryGetValue(key, out TValue value) ? value : defaultValue;
        }


        public class CryptocurrencyPortfolioSummaryInfo
        {
            public CryptocurrencyType Cryptocurrency { get; set; }
            public decimal? OneHourChange { get; set; }
            public decimal? TwentyFourHourChange { get; set; }
            public decimal CryptocurrencyAmount { get; set; }
            public decimal? PriceInUsd { get; set; }
            public decimal? UsdAmount => this.PriceInUsd * this.CryptocurrencyAmount;
            public decimal? AveragePriceBought { get; set; }
            public decimal? MarketCap { get; set; }
            public string Link { get; set; }
        }
        
        public class CryptocurrencyYearSummaryInfo
        {
            public CryptocurrencyType Cryptocurrency { get; set; }
            public int Year { get; set; }
            public decimal CryptocurrencyBought { get; set; }
            public decimal CryptocurrencySold { get; set; }
            public decimal UsdInvested { get; set; }
            public decimal UsdReturns { get; set; }
            public decimal? LifoLongTermCapitalGains { get; set; }
            public decimal? LifoShortTermCapitalGains { get; set; }
            public decimal? FifoLongTermCapitalGains { get; set; }
            public decimal? FifoShortTermCapitalGains { get; set; }
        }
    }
}
