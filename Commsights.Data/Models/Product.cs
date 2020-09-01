using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Commsights.Data.Models
{
    public partial class Product : BaseModel
    {
        public int? CategoryId { get; set; }
        public string Title { get; set; }
        public string Urlcode { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
        public string Tags { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public string ImageThumbnail { get; set; }
        public string Description { get; set; }
        public string ContentMain { get; set; }
        public decimal? Price { get; set; }
        public int? PriceUnitId { get; set; }
        public string Page { get; set; }
        public string TitleEnglish { get; set; }
        public string FileName { get; set; }
        public int? Liked { get; set; }
        public int? Comment { get; set; }
        public int? Share { get; set; }
        public int? Reach { get; set; }
        public string Duration { get; set; }
        public bool? IsVideo { get; set; }
        public int? ArticleTypeID { get; set; }
        public int? CompanyID { get; set; }
        public int? AssessID { get; set; }
        public int? IndustryID { get; set; }
        public int? SegmentID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime DatePublish { get; set; }
    }
}
