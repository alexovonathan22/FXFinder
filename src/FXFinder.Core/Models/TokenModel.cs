using System;
using System.Collections.Generic;
using System.Text;

namespace FXFinder.Core.Models
{
    public class TokenModel
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
