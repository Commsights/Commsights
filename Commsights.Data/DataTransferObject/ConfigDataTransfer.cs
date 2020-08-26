using System;
using System.Collections.Generic;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class ConfigDataTransfer : Config
    {
        public int CountChildren { get; set; }
        public string DisplayName
        {
            get
            {
                return Title + " (" + CountChildren + ")";
            }
        }
        public string ParentName { get; set; }
        public string CountryName { get; set; }       
        public string LanguageName { get; set; }
        public string FrequencyName { get; set; }
        public string ColorTypeName { get; set; }
        public ModelTemplate Parent { get; set; }
        public ModelTemplate Country { get; set; }        
        public ModelTemplate Language { get; set; }
        public ModelTemplate Frequency { get; set; }
        public ModelTemplate ColorType { get; set; }
    }
}
