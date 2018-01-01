using CryptoTax.TransactionImport;
using NUnit.Framework;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace CryptoTax.Tests
{
    [TestFixture]
    public class CoinbaseCsvImporterTest
    {
        [Test]
        public void BasicTest()
        {
            var importer = new TransactionImport.CoinbaseCsvImporter();
            var result = importer.ImportFile(new TransactonImporterSettings
            {
                Filename = "C:\\Users\\Nick Sidawy\\Downloads\\lol.csv"
            });
        }
    }
}
