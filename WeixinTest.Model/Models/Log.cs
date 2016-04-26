using System;
using System.Collections.Generic;

namespace WeixinTest.Model.Models
{
    public partial class Log
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Info { get; set; }
        public bool IsSuccess { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
