using System;
using System.Collections.Generic;
using System.Text;
using FXFinder.Core.DBModels;

namespace FXFinder.Core.Models
{
    public class UserView
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string MainCurrency { get; set; }
        public string MainSymbol { get; set; }
        public WalletView WalletAccounts { get; set; }
        public string Message { get; set; }
    }
}
