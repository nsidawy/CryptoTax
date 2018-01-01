using NUnit.Framework;
using System;
using System.IO;
using System.Text.RegularExpressions;
using CryptoTax.Cryptocurrency;

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
    }
}
