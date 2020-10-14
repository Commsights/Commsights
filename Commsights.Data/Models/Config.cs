using System;
using System.Collections.Generic;

namespace Commsights.Data.Models
{
    public partial class Config : BaseModel
    {
        public string GroupName { get; set; }
        public string Code { get; set; }
        public string CodeName { get; set; }
        public string CodeNameSub { get; set; }
        public int? SortOrder { get; set; }
        public string Icon { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string URLFull { get; set; }
        public string URLSub { get; set; }
        public string Title { get; set; }
        public bool? IsMenuLeft { get; set; }
        public int? BlackWhite { get; set; }
        public int? Color { get; set; }
        public int? CountryID { get; set; }
        public int? LanguageID { get; set; }
        public int? FrequencyID { get; set; }
        public int? ColorTypeID { get; set; }
        public int? IndustryID { get; set; }
        public int? TierID { get; set; }
        public void Initialization()
        {
            if (!string.IsNullOrEmpty(this.Title))
            {
                this.Title = this.Title[0].ToString().ToUpper() + this.Title.Substring(1).ToLower();
            }
        }
    }
}
