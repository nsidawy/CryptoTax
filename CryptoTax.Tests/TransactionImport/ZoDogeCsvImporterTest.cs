using CryptoTax.Crypto;
using CryptoTax.TransactionImport;
using NUnit.Framework;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace CryptoTax.Tests
{
    [TestFixture]
    public class ZoDogeCsvImporterTest
    {
        [Test]
        public void BasicTest()
        {
            var importer = new ZoDogeCsvImporter(new PriceInUsdProvider());
            var result = importer.ImportFile(new TransactonImporterSettings
            {
                Filename = "C:\\Users\\Nick Sidawy\\Downloads\\testing doge.csv"
            });
        }
    }
}
