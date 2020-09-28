using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Commsights.Data.Models
{
    public partial class ProductSearchProperty : BaseModel
    {
        public string Summary { get; set; }
        public int? ProductSearchID { get; set; }
        public int? ProductID { get; set; }
        public int? ProductPropertyID { get; set; }
        public int? ArticleTypeID { get; set; }
        public int? CompanyID { get; set; }
        public int? AssessID { get; set; }
        public int? IndustryID { get; set; }
        public int? SegmentID { get; set; }
        public int? ProductID001 { get; set; }
        public decimal? Point { get; set; }
        public int? CategoryID { get; set; }
        public bool? IsSummary { get; set; }
        public int? SortOrder { get; set; }
    }
}
