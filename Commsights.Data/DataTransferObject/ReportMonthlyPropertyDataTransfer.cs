using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class ReportMonthlyPropertyDataTransfer : ProductProperty
    {
        public int? ReportMonthlyPropertyID { get; set; }
        public string Title { get; set; }
        public string TitleEnglish { get; set; }
        public string Page { get; set; }
        public string Author { get; set; }
        public string URLCode { get; set; }
        public DateTime? DatePublish { get; set; }
        public string Media { get; set; }
        public string MediaType { get; set; }
        public int? AdValue { get; set; }
        public string TierSource { get; set; }
    }
}
