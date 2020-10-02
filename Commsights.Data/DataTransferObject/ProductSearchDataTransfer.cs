using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Commsights.Data.Helpers;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class ProductSearchDataTransfer : ProductSearch
    {
        public string FileName
        {
            get
            {
                return CompanyName + "-" + Title;
            }
        }
        public string Domain
        {
            get
            {
                return AppGlobal.Domain;
            }
        }
        public string URL
        {
            get
            {
                return AppGlobal.Domain + "Report/DailyPrintPreviewFormHTML/" + ID;
            }
        }
        public string URLSendMail
        {
            get
            {
                return AppGlobal.Domain + "Report/SendMail/" + ID;
            }
        }
        public bool IsCompanyAll { get; set; }
        public bool IsProductAll { get; set; }
        public bool IsIndustryAll { get; set; }
        public bool IsCompetitorAll { get; set; }
        public bool IsAll { get; set; }
        public string CompanyName { get; set; }
        public string PhysicalPath { get; set; }
        public ModelTemplate Company { get; set; }
    }
}
