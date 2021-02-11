using System;
using System.Collections.Generic;
using System.Text;

namespace FXFinder.Core.DBModels
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Username { get; set; }

        // collecting currency symbol is ma
        public string CurrencySymbol { get; set; }
        public string CurrencyTitle { get; set; }
        public string AuthToken { get; set; }
        public string RefreshToken { get; set; }
        public string Role { get; set; }
        public bool IsEmailConfirm { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public virtual List<Wallet> Wallets { get; set; }
    }
}
