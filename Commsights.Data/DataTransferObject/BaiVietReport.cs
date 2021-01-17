using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class BaiVietReport
    {
        public int? EmployeeID { get; set; }       
        public string Employee { get; set; }      
        public int? BaiVietSum { get; set; }
        public int? BaiVietDuplicate { get; set; }
        public int? BaiVietExtant { get; set; }

    }
}
