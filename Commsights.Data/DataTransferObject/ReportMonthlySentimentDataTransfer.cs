using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class ReportMonthlySentimentDataTransfer
    {
        public string SegmentProduct { get; set; }
        public int? PositiveLastMonthCount { get; set; }
        public int? NeutralLastMonthCount { get; set; }
        public int? NegativeLastMonthCount { get; set; }
        public int? PositiveCount { get; set; }
        public int? NeutralCount { get; set; }
        public int? NegativeCount { get; set; }
        public int? PositiveGrowthPercent { get; set; }
        public int? NeutralGrowthPercent { get; set; }
        public int? NegativeGrowthPercent { get; set; }
        public decimal? PositivePercent { get; set; }
        public decimal? NeutralPercent { get; set; }
        public decimal? NegativePercent { get; set; }

    }
}
