using System;
using System.Collections.Generic;
using System.Text;
using FXFinder.Core.DBModels;

namespace FXFinder.Core.Models
{
    public class PromotionDetail
    {
        public string Message { get; set; }
        public string UpdatedRole { get; set; }
        public bool IsRoleChanged { get; set; }
    }
}
