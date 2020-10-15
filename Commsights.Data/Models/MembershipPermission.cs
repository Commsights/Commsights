using System;
using System.Collections.Generic;

namespace Commsights.Data.Models
{
    public partial class MembershipPermission : BaseModel
    {
        public int? MembershipID { get; set; }
        public int? MenuID { get; set; }
        public bool? IsView { get; set; }
        public int? CategoryID { get; set; }
        public int? IndustryID { get; set; }
        public int? SegmentID { get; set; }
        public int? ProductID { get; set; }
        public int? CompanyID { get; set; }
        public int? IndustryCompareID { get; set; }
        public int? ProductCompareID { get; set; }
        public int? SortOrder { get; set; }
        public decimal? Hour { get; set; }
        public decimal? Minute { get; set; }
        public decimal? Second { get; set; }
        public string ProductName { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? LanguageID { get; set; }
        public bool? IsDaily { get; set; }
        public bool? IsWeekly { get; set; }
        public bool? IsMonthly { get; set; }
        public bool? IsQuarterly { get; set; }
        public bool? IsYearly { get; set; }
    }
}
