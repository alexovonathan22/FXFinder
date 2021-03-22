using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FXFinder.Core.DBModels
{
    public class Wallet:BaseEntity
    {
        // This grand amount is for inner workings
        [Column(TypeName = "decimal(18,4)")]
        public decimal GrandAmount { get; set; }

        // This new amount is what user sees(if there is frontend),
        // is only updated with value in Grand When Approved
        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }
        public bool IsCurrencyConverted { get; set; }
        public bool IsMainCurrency { get; set; }

        public string AcctDigits { get; set; }
        public string CurrnencyTitle { get; set; }
        public string CurrencySymbol { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual FXUser User { get; set; }
    }
}
