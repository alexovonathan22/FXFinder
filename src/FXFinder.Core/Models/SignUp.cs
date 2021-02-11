using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FXFinder.Core.Models
{
    public class SignUp
    {
        [Required]
        public string CurrencySymbol { get; set; }
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; } // Role might change depending on project requirements.

    }
}
