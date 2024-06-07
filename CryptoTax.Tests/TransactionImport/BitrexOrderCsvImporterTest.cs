using CryptoTax.Crypto;
using CryptoTax.TransactionImport;
using NUnit.Framework;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace CryptoTax.Tests
{
    [TestFixture]
    public class BitrexOrderCsvImporterTest
    {
        [Test]
        public void BasicTest()
        {
            var importer = new BittrexOrderCsvImporter(new PriceInUsdProvider());
            var result = importer.ImportFile(new TransactonImporterSettings
            {
                Filename = "C:\\Users\\Nick Sidawy\\Downloads\\fullOrders (1) (1).csv"
            });
        }
    }
}
