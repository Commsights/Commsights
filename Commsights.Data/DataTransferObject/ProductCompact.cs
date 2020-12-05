 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class ProductCompact 
    {
        public int ID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DatePublish { get; set; }
        public string Title { get; set; }
        public string TitleEnglish { get; set; }
        public string Description { get; set; }
        public string DescriptionEnglish { get; set; }
        public string URLCode { get; set; }
        public string Search { get; set; }
        public string SearchAndID { get; set; }
        public bool? IsError { get; set; }
    }
}
