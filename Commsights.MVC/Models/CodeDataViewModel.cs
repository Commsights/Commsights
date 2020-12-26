using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commsights.MVC.Models
{
    public class CodeDataViewModel
    {
        public int ID { get; set; }
        public DateTime DatePublishBegin { get; set; }
        public DateTime DatePublishEnd { get; set; }
        public int IndustryID { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string Industry { get; set; }
        public int HourBegin { get; set; }
        public int HourEnd { get; set; }
    }
}
