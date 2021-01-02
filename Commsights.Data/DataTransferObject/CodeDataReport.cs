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
        public int? ProductGoogleCount { get; set; }
        public int? ProductAndiCount { get; set; }
        public int? ProductTVCount { get; set; }
        public int? ProductNewspageCount { get; set; }
    }
}
