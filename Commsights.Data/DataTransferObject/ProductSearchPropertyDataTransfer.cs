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
        public string DatePublishString
        {
            get
            {
                string result = "";
                if (DatePublish != null)
                {
                    result = DatePublish.Value.ToString("dd/MM/yyyy");
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
                    result = DatePublish.Value.ToString("MM/dd/yyyy");
                }
                return result;
            }
        }
        public string TitleEnglish { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string URLCode { get; set; }
        public string Media { get; set; }
        public string MediaURLFull { get; set; }
        public string MediaType { get; set; }
        public string MediaTypeVietnamese { get; set; }
        public string ChannelName { get; set; }
        public string ArticleTypeName { get; set; }
        public string ArticleTypeNameVietnamese { get; set; }        
        public string CompanyName { get; set; }
        public string AssessName { get; set; }
        public string AssessNameVietnamese { get; set; }
        public string ProductName { get; set; }
        public string IndustryName { get; set; }
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
        public string Page { get; set; }
        public string DescriptionEnglish { get; set; }
        public ModelTemplate ArticleType { get; set; }
        public ModelTemplate Company { get; set; }
        public ModelTemplate AssessType { get; set; }
    }
}
