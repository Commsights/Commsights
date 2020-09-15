using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Commsights.Data.Models
{
    public partial class ProductSearch : BaseModel
    {
        public int? CompanyID { get; set; }
        public int? CompanyCount { get; set; }
        public int? ProductCount { get; set; }
        public int? IndustryCount { get; set; }
        public int? SegmentCount { get; set; }
        public int? CompetitorCount { get; set; }
        public string Title { get; set; }
        public string SearchString { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime DateSearch { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime DatePublishBegin { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime DatePublishEnd { get; set; }
    }
}
