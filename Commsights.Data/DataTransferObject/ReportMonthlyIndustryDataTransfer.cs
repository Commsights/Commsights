using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class ReportMonthlyIndustryDataTransfer
    {
        public string SegmentProduct { get; set; }
        public string CompanyName { get; set; }
        public int? ProductCount { get; set; }
        public int? ProductPercent { get; set; }
        public int SortOrder { get; set; }
        public int? ProductNewsLastMonthCount { get; set; }
        public int? ProductMediaLastMonthCount { get; set; }
        public int? ProductNewsCount { get; set; }
        public int? ProductMediaCount { get; set; }
        public int? NewsGrowthPercent { get; set; }
        public int? MediaGrowthPercent { get; set; }
        public int? ProductNewsLastMonthCountPercent { get; set; }
        public int? ProductMediaLastMonthCountPercent { get; set; }
        public int? ProductNewsCountPercent { get; set; }
        public int? ProductMediaCountPercent { get; set; }
        public decimal? ProductMediaLastMonthValue { get; set; }
        public decimal? ProductMediaThisMonthValue { get; set; }
        public int? FeatureLastMonthCount { get; set; }
        public int? MentionLastMonthCount { get; set; }
        public int? FeatureCount { get; set; }
        public int? MentionCount { get; set; }
        public int? FeatureGrowthPercent { get; set; }
        public int? MentionGrowthPercent { get; set; }
        public int? FeaturePercent { get; set; }
        public int? MentionPercent { get; set; }

    }
}
