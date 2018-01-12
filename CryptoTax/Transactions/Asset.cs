using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTax.Transactions
{
    public class Asset
    {
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal ExchangeRate { get; set; }
    }

    public class AssetCollection
    {
        public AssetCollection(AccountingMethodType accountingMethod)
        {
            this.AccountingMethod = accountingMethod;
            switch (this.AccountingMethod)
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

        public int Count
        {
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

        public List<Asset> ToList()
        {
            switch (this.AccountingMethod)
            {
                case AccountingMethodType.Fifo:
                    return this.AssetQueue.ToList();
                case AccountingMethodType.Lifo:
                    return this.AssetStack.ToList();
                default:
                    throw new ArgumentException($"{this.AccountingMethod.ToString()} is not a valid input");
            }
        }
    }
}
