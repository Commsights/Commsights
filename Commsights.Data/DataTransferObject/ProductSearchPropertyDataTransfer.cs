using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class ProductSearchPropertyDataTransfer : ProductSearchProperty
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? DatePublish { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string URLCode { get; set; }
        public string WebsiteName { get; set; }
        public string ChannelName { get; set; }
        public string ArticleTypeName { get; set; }
        public string CompanyName { get; set; }
        public string AssessName { get; set; }
        public ModelTemplate ArticleType { get; set; }
        public ModelTemplate Company { get; set; }
        public ModelTemplate AssessType { get; set; }
    }
}
