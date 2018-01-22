using NUnit.Framework;
using System;
using System.IO;
using System.Text.RegularExpressions;
using CryptoTax.Crypto;

namespace CryptoTax.Tests
{
    [TestFixture]
    public class PriceInUsdProviderTest
    {
        [Test]
        public void BtcPriceTest()
        {
            var priceInUsdProvider = new PriceInUsdProvider();
            var value = priceInUsdProvider.GetBitcoinPrice(new DateTime(2017, 12, 25)).Result;
            Assert.IsTrue(value == 14250);
        }

        [Test]
        public void DogericeTest()
        {
            var priceInUsdProvider = new PriceInUsdProvider();
            var value = priceInUsdProvider.GetDogePrice(new DateTime(2017, 12, 25)).Result;
            Assert.IsTrue(value == (decimal)0.008643);
        }
    }
}
