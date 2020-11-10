using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class ReportMonthlyTierCommsightsAndCompanyNameDataTransfer
    {
        public string CompanyName { get; set; }
        public string Media { get; set; }
        public int? TierCount { get; set; }
    }
}
