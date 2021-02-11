using System;
using System.Collections.Generic;
using System.Text;

namespace FXFinder.Core.DBModels
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string ActionTaken { get; set; }
    }
}
