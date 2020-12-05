using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class ReportMonthlyTierCommsightsDataTransfer
    {        
        public string CompanyName { get; set; }
        public int? MassLastMonthCount { get; set; }
        public int? IndustryLastMonthCount { get; set; }
        public int? PortalLastMonthCount { get; set; }
        public int? OrthersLastMonthCount { get; set; }
        public int? MassCount { get; set; }
        public int? IndustryCount { get; set; }
        public int? PortalCount { get; set; }
        public int? OrthersCount { get; set; }    
        public int? MassGrowthPercent { get; set; }
        public int? IndustryGrowthPercent { get; set; }
        public int? PortalGrowthPercent { get; set; }
        public int? OrthersGrowthPercent { get; set; }      

    }
}
