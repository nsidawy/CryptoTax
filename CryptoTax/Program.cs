using Autofac;
using CryptoTax.Cryptocurrency;
using CryptoTax.TransactionImport;
using CryptoTax.Transactions;
using CryptoTax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoTax
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var containerBuilder = SetupContainer();
            using (var container = containerBuilder.Build())
            {
                Application.Run(container.Resolve<MainWindow>());
            }
        }

        private static ContainerBuilder SetupContainer()
        {
            var container = new ContainerBuilder();

            container.RegisterType<PriceInUsdProvider>()
                .InstancePerLifetimeScope();

            container.RegisterType<BittrexOrderCsvImporter>()
                .Keyed<ITransactionImporter>(TransactionImporterType.BitrixOrderCsvImporter);
            container.RegisterType<GdaxFillCsvImporter>()
                .Keyed<ITransactionImporter>(TransactionImporterType.GdaxFillCsvImporter);
            container.RegisterType<CoinbaseCsvImporter>()
                .Keyed<ITransactionImporter>(TransactionImporterType.CoinbaseCsvImporter);
            container.RegisterType<ZoDogeCsvImporter>()
                .Keyed<ITransactionImporter>(TransactionImporterType.ZoDogeCsvImporter);
            container.RegisterType<CustomCsvImporter>()
                .Keyed<ITransactionImporter>(TransactionImporterType.CustomCsvImporter);

            container.RegisterType<TaxCalculator>();
            container.RegisterType<PortfolioSummaryProvider>();
            container.RegisterType<SaveFileReaderWriter>();

            container.RegisterType<FormFactory>();
            container.RegisterType<ImportTransactionsDialog>()
                .InstancePerDependency();
            container.RegisterType<AddTransactionDialog>()
                .InstancePerDependency();
            container.RegisterType<CustomCsvImporterDialog>()
                .InstancePerDependency();
            container.RegisterType<MainWindow>()
                .InstancePerDependency();

            return container;
        }
    }
}
