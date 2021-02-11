using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FXFinder.Core.Models
{
    public class WalletModel
    {
        
        [Required]
        public string CurrencySymbolToCreateWallet { get; set; }
        //public decimal InitialAmt { get; set; }
    }
}
