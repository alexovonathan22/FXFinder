using System;
using System.Collections.Generic;
using System.Text;

namespace FXFinder.Core.Util
{
    public class APIResponse
    {
        /// <summary>
        /// StatusCodes 00, 01
        /// 00 when its successful
        /// 01 when its failed
        /// </summary>
        public string StatusCode { get; set; } = "01";
        public string ApiMessage { get; set; }
        public object? Result { get; set; }
        //public object? Error { get; set; }

    }
}
