using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyIssue.Web.Model
{
    public class PageRequest
    {
        public string? Link { get; set; }
        public int? Page { get; set; }
        public int? Size { get; set; }
    }
}
