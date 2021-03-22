using System;
using System.Collections.Generic;
using System.Text;
using FXFinder.Core.DBModels;

namespace FXFinder.Core.Models
{
    public class WalletView
    {
        public string CurrencyTitle { get; set; }
        public string CurrencySymbol { get; set; }
        public int UserId { get; set; }
        public FXUser UserCreated { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string CreatedBy { get; set; }
        public string ActionTaken { get; set; }
        public string WalletAcct { get; set; }
        public decimal Amount { get; set; }
    }
}