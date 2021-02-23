using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FXFinder.Core.Models
{
    public class SignUp
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; } 
        [Required]
        public string LastName { get; set; } 
        [Required]
        public string PhoneNumber { get; set; } 

    }
}
