using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class ReportMonthlyMediaTypeDataTransfer
    {
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public int? OnlineLastMonthCount { get; set; }
        public int? NewspaperLastMonthCount { get; set; }
        public int? TVLastMonthCount { get; set; }
        public int? MagazineLastMonthCount { get; set; }
        public int? OnlineCount { get; set; }
        public int? NewspaperCount { get; set; }
        public int? TVCount { get; set; }
        public int? MagazineCount { get; set; }
        public int? OnlineGrowth { get; set; }
        public int? NewspaperGrowth { get; set; }
        public int? TVGrowth { get; set; }
        public int? MagazineGrowth { get; set; }
        public int? OnlineGrowthPercent { get; set; }
        public int? NewspaperGrowthPercent { get; set; }
        public int? TVGrowthPercent { get; set; }
        public int? MagazineGrowthPercent { get; set; }

    }
}
