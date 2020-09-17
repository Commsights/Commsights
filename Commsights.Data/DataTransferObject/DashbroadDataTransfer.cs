using System;
using System.Collections.Generic;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class DashbroadDataTransfer
    {       
        public DateTime DatePublish { get; set; }
        public int? CustomerCount { get; set; }
        public int? ArticleCount { get; set; }
        public int? ArticleCompanyCount { get; set; }
        public int? ArticleProductCount { get; set; }
        public int? ArticleIndustryCount { get; set; }
        public int? ArticleCompetitorCount { get; set; }
        public string CompanyName { get; set; }
        public string IndustryName { get; set; }
        public string ProductName { get; set; }
    }
}
