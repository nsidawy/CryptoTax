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
            var assetList = new AssetList(accountingMethod);           
            foreach(var transaction in sortedTransactions)
            {
                switch (transaction.TransactionType)
                {
                    case TransactionType.Buy:
                        assetList.Add(new Asset
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
                            if (assetList.Count == 0)
                            {
                                return null;
                            }
                            Asset soldAsset;
                            decimal sellAmount;
                            if (assetList.Peek().Amount <= cryptocurrencySellAmount)
                            {
                                soldAsset = assetList.Pop();
                                sellAmount = soldAsset.Amount;
                            }
                            else
                            {
                                soldAsset = assetList.Peek();
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

        public class Asset
        {
            public decimal Amount { get; set; }
            public DateTime TransactionDate { get; set; }
            public decimal ExchangeRate { get; set; }
        }

        private class AssetList
        {
            public AssetList(AccountingMethodType accountingMethod)
            {
                this.AccountingMethod = accountingMethod;
                switch(this.AccountingMethod)
                {
                    case AccountingMethodType.Fifo:
                        this.AssetQueue = new Queue<Asset>();
                        break;
                    case AccountingMethodType.Lifo:
                        this.AssetStack = new Stack<Asset>();
                        break;
                    default:
                        throw new ArgumentException($"{this.AccountingMethod.ToString()} is not a valid input");
                }
            }

            public AccountingMethodType AccountingMethod { get; private set; }

            private Queue<Asset> AssetQueue { get; set; }
            private Stack<Asset> AssetStack { get; set; }

            public int Count {
                get
                {
                    switch (this.AccountingMethod)
                    {
                        case AccountingMethodType.Fifo:
                            return this.AssetQueue.Count;
                        case AccountingMethodType.Lifo:
                            return this.AssetStack.Count;
                        default:
                            throw new ArgumentException($"{this.AccountingMethod.ToString()} is not a valid input");
                    }
                }
            }
            
            public void Add(Asset asset)
            {
                switch (this.AccountingMethod)
                {
                    case AccountingMethodType.Fifo:
                        this.AssetQueue.Enqueue(asset);
                        break;
                    case AccountingMethodType.Lifo:
                        this.AssetStack.Push(asset);
                        break;
                    default:
                        throw new ArgumentException($"{this.AccountingMethod.ToString()} is not a valid input");
                }
            }

            public Asset Peek()
            {
                switch (this.AccountingMethod)
                {
                    case AccountingMethodType.Fifo:
                        return this.AssetQueue.Peek();
                    case AccountingMethodType.Lifo:
                        return this.AssetStack.Peek();
                    default:
                        throw new ArgumentException($"{this.AccountingMethod.ToString()} is not a valid input");
                }
            }

            public Asset Pop()
            {
                switch (this.AccountingMethod)
                {
                    case AccountingMethodType.Fifo:
                        return this.AssetQueue.Dequeue();
                    case AccountingMethodType.Lifo:
                        return this.AssetStack.Pop();
                    default:
                        throw new ArgumentException($"{this.AccountingMethod.ToString()} is not a valid input");
                }
            }
        }
    }
}
