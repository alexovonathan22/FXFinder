using System;
using System.Collections.Generic;
using System.Text;

namespace FXFinder.Core.Models
{
    public class FundedWalletRespnse
    {
        public bool IsApproved { get; set; }
        public decimal NewAmountAfterApproval { get; set; }
        public decimal Amount { get; set; }
        public string AcctDigits { get; set; }
        public int UserIdToFund { get; set; }

    }
}
