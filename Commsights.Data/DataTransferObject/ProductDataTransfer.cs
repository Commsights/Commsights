 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class ProductDataTransfer : Product
    {
        public string DatePublishString
        {
            get
            {
                string result = "";
                if (DatePublish != null)
                {
                    result = DatePublish.ToString("dd/MM/yyyy");
                }
                return result;
            }
        }
        public string DatePublishStringEnglish
        {
            get
            {
                string result = "";
                if (DatePublish != null)
                {
                    result = DatePublish.ToString("MM/dd/yyyy");
                }
                return result;
            }
        }
        public int? AdvertisementValue { get; set; }
        public string AdvertisementValueString
        {
            get
            {
                string result = "";
                if (AdvertisementValue != null)
                {
                    result = ((decimal)AdvertisementValue.Value).ToString("N0");
                }
                return result;
            }
        }
        public string Media { get; set; }
        public string MediaType { get; set; }
        public string MediaTypeVietnamese { get; set; }
        public string Summary { get; set; }
        public int Point { get; set; }
        public string ArticleTypeName { get; set; }
        public string ArticleTypeNameVietnamese { get; set; }
        public string AssessName { get; set; }
        public string AssessNameVietnamese { get; set; }
        public string CompanyName { get; set; }
        public string SegmentName { get; set; }
        public string IndustryName { get; set; }
        public string ProductName { get; set; }
        public string ParentName { get; set; }
        public string Frequency { get; set; }        
        public int? MembershipTypeID { get; set; }
        public int? MembershipPermissionProductID { get; set; }
        public bool? IsSource { get; set; }
        public bool? IsCopy { get; set; }
        public ModelTemplate ArticleType { get; set; }
        public ModelTemplate Company { get; set; }
        public ModelTemplate AssessType { get; set; }
        public ModelTemplate Segment { get; set; }
        public ModelTemplate Product { get; set; }
    }
}
