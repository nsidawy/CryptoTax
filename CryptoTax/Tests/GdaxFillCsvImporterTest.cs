using CryptoTax.Cryptocurrency;
using CryptoTax.TransactionImport;
using NUnit.Framework;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace CryptoTax.Tests
{
    [TestFixture]
    public class GdaxFillCsvImporterTest
    {
        [Test]
        public void BasicTest()
        {
            var importer = new TransactionImport.GdaxFillCsvImporter(new PriceInUsdProvider());
            var result = importer.ImportFile(new TransactonImporterSettings
            {
                Filename = "C:\\Users\\Nick Sidawy\\Downloads\\fills (1).csv"
            });
        }
    }
}
