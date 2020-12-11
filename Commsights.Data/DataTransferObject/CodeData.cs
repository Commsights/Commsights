using System;
using System.Collections.Generic;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class CodeData
    {
        public int? RowLast { get; set; }
        public int? RowBack { get; set; }
        public int? RowCurrent { get; set; }
        public int? RowCount { get; set; }
        public int? RowIndex { get; set; }
        public int? IndustryID { get; set; }
        public string Industry { get; set; }
        public int? ProductPropertyID { get; set; }
        public string Source { get; set; }
        public string FileName { get; set; }
        public string CategoryMain { get; set; }
        public string CategorySub { get; set; }
        public string CompanyName { get; set; }
        public string CorpCopy { get; set; }
        public decimal? SOECompany { get; set; }
        public string FeatureCorp { get; set; }
        public string Segment { get; set; }
        public string ProductName_ProjectName { get; set; }
        public decimal? SOEProduct { get; set; }
        public string FeatureProduct { get; set; }
        public string SentimentCorp { get; set; }
        public decimal? Advalue { get; set; }
        public string TierCommsights { get; set; }
        public int? ProductID { get; set; }
        public string Title { get; set; }
        public string TitleEnglish { get; set; }
        public string Description { get; set; }
        public string DescriptionEnglish { get; set; }
        public string Journalist { get; set; }
        public string Author { get; set; }
        public string URLCode { get; set; }
        public string ProductSource { get; set; }
        public string MediaTitle { get; set; }
        public string MediaType { get; set; }
        public bool? IsSummary { get; set; }
        public DateTime DatePublish { get; set; }
        public DateTime DateUpload { get; set; }
    }
}
