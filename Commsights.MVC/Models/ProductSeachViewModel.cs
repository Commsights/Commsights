using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commsights.MVC.Models
{
    public class ProductSeachViewModel
    {
        public DateTime DatePublishBegin { get; set; }
        public DateTime DatePublishEnd { get; set; }
        public string Search { get; set; }
        public bool IsTitle { get; set; }
        public bool IsDescription { get; set; }
    }
}
