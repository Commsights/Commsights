using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commsights.MVC.Models
{
    public class BaseViewModel
    {
        public int ID { get; set; }
        public string Search { get; set; }
        public DateTime DatePublish { get; set; }
        public DateTime DatePublishBegin { get; set; }
        public DateTime DatePublishEnd { get; set; }
    }
}
