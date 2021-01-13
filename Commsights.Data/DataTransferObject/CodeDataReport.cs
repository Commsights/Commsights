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
        public int? EmployeeID { get; set; }
        public string Employee { get; set; }
        public string CompanyName { get; set; }
        public int? ProductPropertyCount { get; set; }
        public int? CodingCount { get; set; }
        public int? AnalysisCount { get; set; }
        public int? ProductCount { get; set; }
        public int? ProductGoogleCount { get; set; }
        public int? ProductAndiCount { get; set; }
        public int? ProductTVCount { get; set; }
        public int? ProductNewspageCount { get; set; }
        public int? ProductPropertyCoding { get; set; }
        public int? ProductPropertyCodingNot { get; set; }
        public int? ProductPropertyEmployeeCount { get; set; }
        public int? ProductPropertyEmployeeNotCount { get; set; }
        public int? ProductPropertyEmployeeCodingCount { get; set; }
        public int? ProductPropertyEmployeeCodingNotCount { get; set; }

        public int? ProductCodingNot { get; set; }
        public int? ProductEmployeeCount { get; set; }
        public int? ProductEmployeeCodingCount { get; set; }
        public int? ProductEmployeeCodingNotCount { get; set; }
        
    }
}
