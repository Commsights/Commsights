using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class ReportMonthlySegmentProductDataTransfer
    {        
        public string SegmentProduct { get; set; }
        public string FullName { get; set; }
        public string ProductName_ProjectName { get; set; }
        public int? ProductPropertyCount { get; set; }
    }
}
