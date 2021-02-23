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
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public WalletView WalletAccounts { get; set; }
        public string Message { get; set; }
        public bool IsVerified { get; set; }
        public string  VerifyUrl { get; set; }
    }
}
