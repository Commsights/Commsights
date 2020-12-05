using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class ReportMonthlySentimentDataTransfer
    {
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string FeatureCorp { get; set; }
        public string Sentiment { get; set; }
        public int? PositiveLastMonthCount { get; set; }
        public int? NeutralLastMonthCount { get; set; }
        public int? NegativeLastMonthCount { get; set; }
        public int? PositiveCount { get; set; }
        public int? NeutralCount { get; set; }
        public int? NegativeCount { get; set; }
        public int? PositiveGrowthPercent { get; set; }
        public int? NeutralGrowthPercent { get; set; }
        public int? NegativeGrowthPercent { get; set; }
        public int? PositivePercent { get; set; }
        public int? NeutralPercent { get; set; }
        public int? NegativePercent { get; set; }
        public int? OnlineCount { get; set; }
        public int? NewspaperCount { get; set; }
        public int? TVCount { get; set; }
        public int? MagazineCount { get; set; }

    }
}
