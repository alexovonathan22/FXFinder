using System;
using System.Collections.Generic;
using System.Text;

namespace FXFinder.Core.DBModels
{
    public class FXUser : BaseEntity
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string AuthToken { get; set; }
        public string RefreshToken { get; set; }
        public string Role { get; set; }
        public bool IsEmailConfirm { get; set; }
        public bool IsPhoneNumConfirm { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

    }
}
