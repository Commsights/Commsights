using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Commsights.Data.Models
{
    public partial class ProductProperty : BaseModel
    {
        public int? ArticleTypeID { get; set; }
        public int? CompanyID { get; set; }
        public int? AssessID { get; set; }
        public int? IndustryID { get; set; }
        public int? SegmentID { get; set; }
        public int? ProductID { get; set; }
        public decimal? Point { get; set; }
        public string GUICode { get; set; }
        public string Code { get; set; }
        public bool? IsSummary { get; set; }
        public bool? IsData { get; set; }
    }
}
