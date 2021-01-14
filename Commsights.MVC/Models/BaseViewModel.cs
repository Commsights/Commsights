using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commsights.MVC.Models
{
    public class BaseViewModel
    {       
        public int ID { get; set; }
        public int IndustryID { get; set; }
        public string IndustryName { get; set; }
        public int IndustryIDUploadScan { get; set; }
        public int IndustryIDUploadGoogleSearch { get; set; }
        public int IndustryIDUploadGoogleSearchAndAutoFilter { get; set; }
        public int IndustryIDUploadGoogleSearch001 { get; set; }
        public int IndustryIDUploadAndiSource { get; set; }
        public int IndustryIDUploadYounet { get; set; }
        public DateTime DatePublish { get; set; }
        public bool IsIndustryIDUploadScan { get; set; }
        public bool IsIndustryIDUploadGoogleSearch { get; set; }
        public bool IsIndustryIDUploadGoogleSearch001 { get; set; }
        public bool IsIndustryIDUploadAndiSource { get; set; }
        public bool IsIndustryIDUploadYounet { get; set; }
        public bool IsUploadScanOverride { get; set; }
        public bool IsUploadGoogleSearchOverride { get; set; }
        public bool IsUploadGoogleSearch001Override { get; set; }
        public bool IsUploadAndiSourceOverride { get; set; }
        public bool IsUploadYounetOverride { get; set; }
        public bool AllData { get; set; }
        public bool AllSummary { get; set; }
        public DateTime DatePublishBegin { get; set; }
        public DateTime DatePublishEnd { get; set; }
        public string DatePublishBeginString { get; set; }
        public string DatePublishEndString { get; set; }
        public string Action { get; set; }
        public string ActionView { get; set; }
        public string Content { get; set; }
        public int YearFinance { get; set; }
        public int MonthFinance { get; set; }
    }
}
