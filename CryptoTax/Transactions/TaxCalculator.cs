using CryptoTax.Cryptocurrency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTax.Transactions
{
    public class TaxCalculator
    {
        public decimal CalculateTaxForYear(IReadOnlyList<Transaction> transactions, int taxYear)
        {
            return 0;
        }

        public IReadOnlyList<CapitalGain> CalculateCapialGains(IReadOnlyList<Transaction> transactions, AccountingMethodType accountingMethod, CryptocurrencyType cryptocurrency)
        {
            var capitalGains = new List<CapitalGain>();
            var sortedTransactions = transactions
                .Where(x => x.Cryptocurrency == cryptocurrency)
                .OrderBy(x => x.TransactionDate);
            var assetCollection = new AssetCollection(accountingMethod);           
            foreach(var transaction in sortedTransactions)
            {
                switch (transaction.TransactionType)
                {
                    case TransactionType.Buy:
                        assetCollection.Add(new Asset
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
                            if (assetCollection.Count == 0)
                            {
                                return null;
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
                            capitalGains.Add(new CapitalGain
                            {
                                YearIncurred = transaction.TransactionDate.Year,
                                AssetLifetime = transaction.TransactionDate - soldAsset.TransactionDate,
                                UsdAmount = sellAmount * (transaction.PriceInUsd - soldAsset.ExchangeRate)
                            });
                        }
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
            return capitalGains;
        }
    }
}
