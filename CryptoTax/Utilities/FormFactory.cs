using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoTax.Utilities
{
    public class FormFactory
    {
        private readonly ILifetimeScope _container;
        
        public FormFactory(ILifetimeScope container)
        {
            this._container = container;
            
        }

        public TForm CreateForm<TForm>() where TForm : Form
        {
            return this._container.Resolve<TForm>();
        }
    }
}
