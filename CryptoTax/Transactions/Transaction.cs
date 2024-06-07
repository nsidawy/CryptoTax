using CryptoTax.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTax.Transactions
{
    public enum TransactionType
    {
        Buy,
        Sell
    }

    public class Transaction
    {
        public DateTime TransactionDate { get; set; }
        public CryptoType Crypto { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Quantity { get; set; }
        public decimal UsDollarAmount { get; set; }
        public decimal PriceInUsd { get => this.UsDollarAmount / this.Quantity; }
        public bool ExcludeFromPortfolio { get; set; }
    }
}
