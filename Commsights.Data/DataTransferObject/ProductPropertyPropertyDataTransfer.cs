using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class ProductPropertyPropertyDataTransfer : ProductProperty
    {

        public string ArticleTypeName { get; set; }
        public string AssessName { get; set; }
        public string CompanyName { get; set; }
        public string IndustryName { get; set; }
        public string SegmentName { get; set; }
        public string ProductName { get; set; }

    }
}
