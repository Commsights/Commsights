using System;
using System.Collections.Generic;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class MembershipPermissionDataTransfer : MembershipPermission
    {
        public string IndustryName { get; set; }
        public string SegmentName { get; set; }
        public string MembershipName { get; set; }
        public string CompanyName { get; set; }
        public string IndustryCompareName { get; set; }
        public string ProductSourceName { get; set; }
        public string ProductCompareName { get; set; }
        public string DailyReportSectionName { get; set; }
        public string DailyReportColumnName { get; set; }
        public string DailyReportColumnNote { get; set; }
        public string ReportTypeName { get; set; }
        public string ChannelName { get; set; }
        public string LanguageName { get; set; }
        public ModelTemplate Industry { get; set; }
        public ModelTemplate Segment { get; set; }
        public ModelTemplate Membership { get; set; }
        public ModelTemplate Company { get; set; }
        public ModelTemplate IndustryCompare { get; set; }
        public ModelTemplate Product { get; set; }
        public ModelTemplate ProductCompare { get; set; }
        public ModelTemplate ReportType { get; set; }
        public ModelTemplate Language { get; set; }
        public ModelTemplateIndustry Industry001 { get; set; }
        public ModelTemplateIndustry IndustryCompare001 { get; set; }
    }
}
