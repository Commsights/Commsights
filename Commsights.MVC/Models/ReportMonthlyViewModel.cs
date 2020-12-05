using Commsights.Data.DataTransferObject;
using Commsights.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commsights.MVC.Models
{
    public class ReportMonthlyViewModel
    {        
        public int ID { get; set; }
        public string Title { get; set; }
        public List<ReportMonthlyIndustryDataTransfer> ListReportMonthlyIndustryDataTransfer { get; set; }
        public List<ReportMonthlySentimentDataTransfer> ListReportMonthlySentimentDataTransfer { get; set; }
        public List<ReportMonthlySentimentDataTransfer> ListReportMonthlySentimentAndMediaTypeDataTransfer { get; set; }
        public List<ReportMonthlySentimentDataTransfer> ListReportMonthlySentimentAndFeatureDataTransfer { get; set; }
        public List<ReportMonthlyChannelDataTransfer> ListReportMonthlyChannelDataTransfer { get; set; }
        public List<ReportMonthlyChannelDataTransfer> ListReportMonthlyChannelAndFeatureDataTransfer { get; set; }
        public List<ReportMonthlyChannelDataTransfer> ListReportMonthlyChannelAndMentionDataTransfer { get; set; }
        public List<ReportMonthlyTierCommsightsDataTransfer> ListReportMonthlyTierCommsightsDataTransfer { get; set; }
        public List<ReportMonthlyTierCommsightsAndCompanyNameDataTransfer> ListCompanyName { get; set; }
        public List<ReportMonthlyTierCommsightsAndCompanyNameDataTransfer> ListTierCommsightsAndCompanyNameAndIndustry { get; set; }
        public List<ReportMonthlyTierCommsightsAndCompanyNameDataTransfer> ListTierCommsightsAndCompanyNameAndMass { get; set; }
        public List<ReportMonthlyTierCommsightsAndCompanyNameDataTransfer> ListTierCommsightsAndCompanyNameAndOther { get; set; }
        public List<ReportMonthlyTierCommsightsAndCompanyNameDataTransfer> ListTierCommsightsAndCompanyNameAndPortal { get; set; }
        public List<ReportMonthlyCompanyAndYearDataTransfer> ListReportMonthlyCompanyAndYearDataTransfer { get; set; }
        public List<ReportMonthlySegmentProductDataTransfer> ListReportMonthlySegmentProductDataTransfer { get; set; }
        public List<MonthData> ListMonth { get; set; }
        public List<SeriesData> ListSeries { get; set; }
    }
}
