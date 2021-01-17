using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class CodeData
    {
        public int? UserUpdated { get; set; }
        public int? RowIndexCount { get; set; }
        public decimal? RowBegin { get; set; }
        public decimal? RowEnd { get; set; }
        public int? RowLast { get; set; }
        public int? RowBack { get; set; }
        public int? RowNext { get; set; }
        public int? RowCurrent { get; set; }
        public int? RowCount { get; set; }
        public int? RowIndex { get; set; }
        public int? IndustryID { get; set; }
        public string Industry { get; set; }
        public int? ProductPropertyID { get; set; }
        public int? Source { get; set; }
        public string FileName { get; set; }
        public string CategoryMain { get; set; }
        public string CategorySub { get; set; }
        public string CategoryMainVietnamese { get; set; }
        public string CategorySubVietnamese { get; set; }
        public string CompanyName { get; set; }
        public string CorpCopy { get; set; }
        public string SOECompanyString { get; set; }
        public string SOEProductString { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? SOECompany { get; set; }
        public string FeatureCorp { get; set; }
        public string Segment { get; set; }
        public string ProductName_ProjectName { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? SOEProduct { get; set; }
        public string FeatureProduct { get; set; }
        public string SentimentCorp { get; set; }
        public string SentimentCorpVietnamese { get; set; }
        public decimal? Advalue { get; set; }
        public decimal? Color { get; set; }
        public string TierCommsights { get; set; }
        public string CampaignName { get; set; }
        public string CampaignKeyMessage { get; set; }
        public string KeyMessage { get; set; }
        public string CompetitiveWhat { get; set; }
        public string SpokePersonName { get; set; }
        public string SpokePersonTitle { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? SpokePersonValue { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? ToneValue { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? HeadlineValue { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? FeatureValue { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? TierValue { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? PictureValue { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? SentimentValue { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? KOLValue { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? OtherValue { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? TasteValue { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? PriceValue { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? NutritionFactValue { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? VitaminValue { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? GoodForHealthValue { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? Bottle_CanDesignValue { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? CompetitiveNewsValue { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? MPS { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? ROME_Corp_VND { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? ROME_Product_VND { get; set; }
        public int? ProductID { get; set; }
        public int? ProductParentID { get; set; }
        public string Title { get; set; }
        public string TitleEnglish { get; set; }
        public string Description { get; set; }
        public string DescriptionEnglish { get; set; }
        public string Journalist { get; set; }
        public string Author { get; set; }
        public string URLCode { get; set; }
        public string ProductSource { get; set; }
        public string MediaTitle { get; set; }
        public string MediaType { get; set; }
        public string Page { get; set; }
        public string Duration { get; set; }
        public bool? IsSummary { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DatePublish { get; set; }
        public DateTime DateUpload { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateCoding { get; set; }
        public string AdvalueString
        {
            get
            {
                string resut = "";
                if (Advalue != null)
                {
                    resut = Advalue.Value.ToString("N0");
                }
                return resut;
            }
        }
        public bool? IsCoding { get; set; }
        public bool? IsAnalysis { get; set; }
        public bool? IsDownload { get; set; }
        public bool? IsSend { get; set; }
        public bool? IsCopy { get; set; }
        public string CompanyNameHiden { get; set; }
        public string ProductNameHiden { get; set; }
        public string CategorySubHiden { get; set; }
        public string URLCoding { get; set; }
        public string Frequency { get; set; }
        public int? CopyVersion { get; set; }
        public string TitleProperty { get; set; }
        public int? SourceProperty { get; set; }
        public string TimeLine { get; set; }
        public string TotalSize { get; set; }
        public int? EmployeeID { get; set; }
        public string FullName { get; set; }
        public bool? IsDailyDownload { get; set; }
        public DateTime? DateDailyDownload { get; set; }
        public string Note { get; set; }

        public bool? IsVideo { get; set; }

        public string ProductFeatureList { get; set; }
    }
}
