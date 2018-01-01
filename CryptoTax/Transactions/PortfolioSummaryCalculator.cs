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
        public IReadOnlyList<CryptocurrencySummaryInfo> GetCryptocurrencySummaryInfo(
            IReadOnlyList<Transaction> transactions,
            IReadOnlyDictionary<CryptocurrencyType, decimal> pricesInUsd)
        {
            var groupedTransactions = transactions.GroupBy(x => x.Cryptocurrency);
            var summaryInfos = new List<CryptocurrencySummaryInfo>();
            foreach (var groupedTransaction in groupedTransactions)
            {
                var foundPriceInUsd = pricesInUsd.TryGetValue(groupedTransaction.Key, out decimal priceInUsd);
                summaryInfos.Add(new CryptocurrencySummaryInfo
                {
                    Cryptocurrency = groupedTransaction.Key,
                    PriceInUsd = foundPriceInUsd ? priceInUsd : (decimal?)null,
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

        public class CryptocurrencySummaryInfo
        {
            public CryptocurrencyType Cryptocurrency { get; set; }
            public decimal CryptocurrencyAmount { get; set; }
            public decimal? PriceInUsd { get; set; }
            public decimal? UsdAmount => this.PriceInUsd * this.CryptocurrencyAmount;
        }
    }
}
