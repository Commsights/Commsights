using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class CodeDataReport
    {
        public int? IndustryID { get; set; }
        public string Industry { get; set; }
        public int? ProductPropertyCount { get; set; }
        public int? CodingCount { get; set; }
        public int? AnalysisCount { get; set; }
        public int? ProductCount { get; set; }
    }
}
