using System;
using System.Collections.Generic;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class CodeData
    {
        public int? RowLast { get; set; }
        public int? RowBack { get; set; }
        public int? RowCurrent { get; set; }
        public int? RowCount { get; set; }
        public int? RowIndex { get; set; }
        public int? IndustryID { get; set; }
        public string Industry { get; set; }
        public int? ProductPropertyID { get; set; }
        public string Source { get; set; }
        public string FileName { get; set; }
        public string CategoryMain { get; set; }
        public string CategorySub { get; set; }
        public string CompanyName { get; set; }
        public string CorpCopy { get; set; }
        public decimal? SOECompany { get; set; }
        public string FeatureCorp { get; set; }
        public string Segment { get; set; }
        public string ProductName_ProjectName { get; set; }
        public decimal? SOEProduct { get; set; }
        public string FeatureProduct { get; set; }
        public string SentimentCorp { get; set; }
        public decimal? Advalue { get; set; }
        public string TierCommsights { get; set; }
        public string CampaignName { get; set; }
        public string KeyMessage { get; set; }
        public string CompetitiveWhat { get; set; }
        public string SpokePersonName { get; set; }
        public string SpokePersonTitle { get; set; }
        public int? SpokePersonValue { get; set; }
        public int? ToneValue { get; set; }
        public int? HeadlineValue { get; set; }
        public int? FeatureValue { get; set; }
        public int? TierValue { get; set; }
        public int? PictureValue { get; set; }
        public int? SentimentValue { get; set; }
        public int? KOLValue { get; set; }
        public int? OtherValue { get; set; }
        public int? TasteValue { get; set; }
        public int? PriceValue { get; set; }
        public int? NutritionFactValue { get; set; }
        public int? VitaminValue { get; set; }
        public int? GoodForHealthValue { get; set; }
        public int? Bottle_CanDesignValue { get; set; }
        public int? CompetitiveNewsValue { get; set; }
        public int? MPS { get; set; }
        public int? ROME_Corp_VND { get; set; }
        public int? ROME_Product_VND { get; set; }
        public int? ProductID { get; set; }
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
        public DateTime DatePublish { get; set; }
        public DateTime DateUpload { get; set; }
    }
}
