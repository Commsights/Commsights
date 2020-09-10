﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class ProductDataTransfer : Product
    {
        public string ArticleTypeName { get; set; }
        public string AssessName { get; set; }
        public string CompanyName { get; set; }
        public string SegmentName { get; set; }
        public string IndustryName { get; set; }
        public string ProductName { get; set; }
        public string ParentName { get; set; }
        public ModelTemplate ArticleType { get; set; }
        public ModelTemplate Company { get; set; }
        public ModelTemplate AssessType { get; set; }
    }
}
