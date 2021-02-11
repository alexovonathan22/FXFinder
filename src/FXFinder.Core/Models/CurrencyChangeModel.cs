using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FXFinder.Core.Models
{
    public class CurrencyChangeModel
    {
        [Required]
        public string Symbol { get; set; }

        [Required]
        public int UserId { get; set; }

    }
}
