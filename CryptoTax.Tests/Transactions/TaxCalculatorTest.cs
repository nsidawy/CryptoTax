using CryptoTax.Crypto;
using CryptoTax.Transactions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptoTax.Tests
{
    [TestFixture]
    public class TaxCalculatorTest
    {
        private Dictionary<int, List<Transaction>> _inputTransactionSet = new Dictionary<int, List<Transaction>>
        {
            {1, new List<Transaction>() },
            {2, new List<Transaction>
            {
                new Transaction {Crypto = Crypto.CryptoType.Augur, TransactionType = TransactionType.Buy,
                Quantity = 1000, UsDollarAmount = 100, TransactionDate = new DateTime(2017, 1, 1) }
            } },
            {3, new List<Transaction>
            {
                new Transaction {Crypto = Crypto.CryptoType.Augur, TransactionType = TransactionType.Buy,
                Quantity = 1000, UsDollarAmount = 100, TransactionDate = new DateTime(2017, 1, 1) },
                new Transaction {Crypto = Crypto.CryptoType.Augur, TransactionType = TransactionType.Buy,
                Quantity = 1000, UsDollarAmount = 200, TransactionDate = new DateTime(2017, 1, 2) },
                new Transaction {Crypto = Crypto.CryptoType.Augur, TransactionType = TransactionType.Sell,
                Quantity = 1000, UsDollarAmount = 300, TransactionDate = new DateTime(2017, 1, 3) },
            } },
            {4, new List<Transaction>
            {
                new Transaction {Crypto = Crypto.CryptoType.Augur, TransactionType = TransactionType.Buy,
                Quantity = 1000, UsDollarAmount = 100, TransactionDate = new DateTime(2016, 1, 1) },
                new Transaction {Crypto = Crypto.CryptoType.Augur, TransactionType = TransactionType.Buy,
                Quantity = 1000, UsDollarAmount = 200, TransactionDate = new DateTime(2016, 1, 2) },
                new Transaction {Crypto = Crypto.CryptoType.Augur, TransactionType = TransactionType.Sell,
                Quantity = 1500, UsDollarAmount = 750, TransactionDate = new DateTime(2017, 1, 3) },
            } },
            {5, new List<Transaction>
            {
                new Transaction {Crypto = Crypto.CryptoType.Augur, TransactionType = TransactionType.Buy,
                Quantity = 1000, UsDollarAmount = 100, TransactionDate = new DateTime(2016, 1, 1) },
                new Transaction {Crypto = Crypto.CryptoType.Augur, TransactionType = TransactionType.Buy,
                Quantity = 1000, UsDollarAmount = 200, TransactionDate = new DateTime(2016, 1, 2) },
                new Transaction {Crypto = Crypto.CryptoType.Augur, TransactionType = TransactionType.Sell,
                Quantity = 1500, UsDollarAmount = 750, TransactionDate = new DateTime(2017, 1, 3) },
                new Transaction {Crypto = Crypto.CryptoType.Augur, TransactionType = TransactionType.Sell,
                Quantity = 500, UsDollarAmount = 1000, TransactionDate = new DateTime(2018, 1, 3) },
            } },
            {6, new List<Transaction>
            {
                new Transaction {Crypto = Crypto.CryptoType.Augur, TransactionType = TransactionType.Buy,
                Quantity = 1000, UsDollarAmount = 100, TransactionDate = new DateTime(2016, 1, 1) },
                new Transaction {Crypto = Crypto.CryptoType.Bitcoin, TransactionType = TransactionType.Buy,
                Quantity = 1000, UsDollarAmount = 200, TransactionDate = new DateTime(2016, 1, 2) },
                new Transaction {Crypto = Crypto.CryptoType.Bitcoin, TransactionType = TransactionType.Sell,
                Quantity = 500, UsDollarAmount = 1000, TransactionDate = new DateTime(2018, 1, 3) },
            } },
        };

        private Dictionary<int, List<CapitalGain>> _baselines = new Dictionary<int, List<CapitalGain>>
        {
            {1, new List<CapitalGain>() },
            {2, new List<CapitalGain> {new CapitalGain { AssetLifetime = new TimeSpan(2, 0, 0, 0), UsdAmount = 200, YearIncurred = 2017}}},
            {3, new List<CapitalGain> {new CapitalGain { AssetLifetime = new TimeSpan(1, 0, 0, 0), UsdAmount = 100, YearIncurred = 2017}}},
            {4, new List<CapitalGain> {
                new CapitalGain { AssetLifetime = new TimeSpan(368, 0, 0, 0), UsdAmount = 400, YearIncurred = 2017},
                new CapitalGain { AssetLifetime = new TimeSpan(367, 0, 0, 0), UsdAmount = 150, YearIncurred = 2017},
            } },
            {5, new List<CapitalGain> {
                new CapitalGain { AssetLifetime = new TimeSpan(367, 0, 0, 0), UsdAmount = 300, YearIncurred = 2017},
                new CapitalGain { AssetLifetime = new TimeSpan(368, 0, 0, 0), UsdAmount = 200, YearIncurred = 2017},
            } },
            {6, new List<CapitalGain> {
                new CapitalGain { AssetLifetime = new TimeSpan(368, 0, 0, 0), UsdAmount = 400, YearIncurred = 2017},
                new CapitalGain { AssetLifetime = new TimeSpan(367, 0, 0, 0), UsdAmount = 150, YearIncurred = 2017},
                new CapitalGain { AssetLifetime = new TimeSpan(732, 0, 0, 0), UsdAmount = 900, YearIncurred = 2018},
            } },
            {7, new List<CapitalGain> {
                new CapitalGain { AssetLifetime = new TimeSpan(367, 0, 0, 0), UsdAmount = 300, YearIncurred = 2017},
                new CapitalGain { AssetLifetime = new TimeSpan(368, 0, 0, 0), UsdAmount = 200, YearIncurred = 2017},
                new CapitalGain { AssetLifetime = new TimeSpan(733, 0, 0, 0), UsdAmount = 950, YearIncurred = 2018},
            } },
        };

        [TestCase(1, 1, AccountingMethodType.Fifo, Description = "Fifo - no transactions")]
        [TestCase(2, 1, AccountingMethodType.Fifo, Description = "Lifo - no transactions")]
        [TestCase(1, 1, AccountingMethodType.Lifo, Description = "Fifo - only buy")]
        [TestCase(2, 1, AccountingMethodType.Lifo, Description = "Lifo - only buy")]
        [TestCase(3, 2, AccountingMethodType.Fifo, Description = "Fifo - single asset sell")]
        [TestCase(3, 3, AccountingMethodType.Lifo, Description = "Lifo - single asset sell")]
        [TestCase(4, 4, AccountingMethodType.Fifo, Description = "Fifo - two asset sell")]
        [TestCase(4, 5, AccountingMethodType.Lifo, Description = "Lifo - two asset sell")]
        [TestCase(5, 6, AccountingMethodType.Fifo, Description = "Fifo - multi year sell")]
        [TestCase(5, 7, AccountingMethodType.Lifo, Description = "Lifo - multi year sell")]
        [TestCase(6, 1, AccountingMethodType.Lifo, Description = "Lifo - multi year sell")]
        public void Test(int transactionSetId, int baselineId, AccountingMethodType accountingMethodType)
        {
            var currencyType = Crypto.CryptoType.Augur;
            var actualCapitalGains = (new TaxCalculator()).CalculateCapialGains(this._inputTransactionSet[transactionSetId], accountingMethodType, currencyType);

            Assert.IsTrue(Enumerable.SequenceEqual(actualCapitalGains, this._baselines[baselineId]));
        }
    }
}
