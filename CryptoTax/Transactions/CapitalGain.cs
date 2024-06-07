using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTax.Transactions
{
    public class CapitalGain
    {
        public TimeSpan AssetLifetime { get; set; }
        public decimal UsdAmount { get; set; }
        public int YearIncurred { get; set; }
        //TODO: should this be a fiscal year and take into account leap years?
        public bool IsLongTerm { get => this.AssetLifetime > new TimeSpan(365, 0, 0, 0); }

        public override bool Equals(object obj)
        {
            if(!(obj is CapitalGain))
            {
                return false;
            }
            var capitalGainsObj = obj as CapitalGain;

            return capitalGainsObj.AssetLifetime == this.AssetLifetime
                && capitalGainsObj.UsdAmount == this.UsdAmount
                && capitalGainsObj.YearIncurred == this.YearIncurred;
        }
    }
}
