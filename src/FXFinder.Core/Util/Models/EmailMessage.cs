using System;
using System.Collections.Generic;
using System.Text;

namespace FXFinder.Core.Util.Models
{
    public class EmailMessage
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
       // public List<IFormFile> Attachments { get; set; }
    }
}
