using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FXFinder.Core.Models
{
    public class FundWalletModel
    {
        [Required]
        public string Symbol { get; set; }
        [Required]
        public decimal Amount { get; set; }

        // These two are required for Admin to fund user wallets
        [Required]
        public int UserId { get; set; }
        [Required]
        public string AcctDigits { get; set; }
    }
}
