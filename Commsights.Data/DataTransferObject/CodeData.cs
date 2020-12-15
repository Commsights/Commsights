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
        public int? Source { get; set; }
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
        public decimal? SpokePersonValue { get; set; }
        public decimal? ToneValue { get; set; }
        public decimal? HeadlineValue { get; set; }
        public decimal? FeatureValue { get; set; }
        public decimal? TierValue { get; set; }
        public decimal? PictureValue { get; set; }
        public decimal? SentimentValue { get; set; }
        public decimal? KOLValue { get; set; }
        public decimal? OtherValue { get; set; }
        public decimal? TasteValue { get; set; }
        public decimal? PriceValue { get; set; }
        public decimal? NutritionFactValue { get; set; }
        public decimal? VitaminValue { get; set; }
        public decimal? GoodForHealthValue { get; set; }
        public decimal? Bottle_CanDesignValue { get; set; }
        public decimal? CompetitiveNewsValue { get; set; }
        public decimal? MPS { get; set; }
        public decimal? ROME_Corp_VND { get; set; }
        public decimal? ROME_Product_VND { get; set; }
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
    }
}
