using System;
using System.Collections.Generic;
using System.Text;

namespace FXFinder.Core.Models
{
    public class CurrencyChange
    {
        public string NewMainCurrencyTitle { get; set; }
        public string NewMainCurrencySymbol { get; set; }
        public string FormerMainCurrencyTitle { get; set; }
        public string FormerMainCurrencySymbol { get; set; }
        public string NewAmountInWalletWithChangedCurr { get; set; }
    }
}
