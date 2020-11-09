﻿using Commsights.Data.DataTransferObject;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Commsights.Data.Repositories
{
    public interface IReportMonthlyRepository : IRepository<ReportMonthly>
    {
        public string DeleteByID(int ID);
        public string InsertItemsByReportMonthlyID(DataTable table, int reportMonthlyID);
        public List<ReportMonthly> GetByYearAndMonthToList(int year, int month);
        public List<ReportMonthlyIndustryDataTransfer> GetIndustryByIDToList(int ID);
        public List<ReportMonthlyIndustryDataTransfer> GetIndustryByIDWithoutSUMToList(int ID);
        public List<ReportMonthlyIndustryDataTransfer> GetIndustryByID001ToList(int ID);
        public List<ReportMonthlyIndustryDataTransfer> GetIndustryByID001WithoutSUMToList(int ID);
        public List<ReportMonthlyIndustryDataTransfer> GetCompanyByIDToList(int ID);
        public List<ReportMonthlyIndustryDataTransfer> GetFeatureIndustryByIDToList(int ID);
        public List<ReportMonthlyIndustryDataTransfer> GetFeatureIndustryWithoutSUMByIDToList(int ID);
        public List<ReportMonthlySentimentDataTransfer> GetSentimentByIDToList(int ID);
        public List<ReportMonthlySentimentDataTransfer> GetSentimentByIDWithoutSUMToList(int ID);
        public List<ReportMonthlySentimentDataTransfer> GetSentimentAndFeatureWithoutSUMByIDToList(int ID);
        public List<ReportMonthlySentimentDataTransfer> GetSentimentAndMediaTypeWithoutSUMByIDToList(int ID);
        public List<ReportMonthlyChannelDataTransfer> GetChannelByIDToList(int ID);
        public List<ReportMonthlyChannelDataTransfer> GetChannelByIDWithoutSUMToList(int ID);
        public List<ReportMonthlyChannelDataTransfer> GetChannelAndFeatureByIDToList(int ID);
        public List<ReportMonthlyChannelDataTransfer> GetChannelAndFeatureWithoutSUMByIDToList(int ID);
        public List<ReportMonthlyChannelDataTransfer> GetChannelAndMentionByIDToList(int ID);
        public List<ReportMonthlyChannelDataTransfer> GetChannelAndMentionWithoutSUMByIDToList(int ID);
        public List<ReportMonthlyTierCommsightsDataTransfer> GetTierCommsightsByIDToList(int ID);
        public List<ReportMonthlyTierCommsightsDataTransfer> GetTierCommsightsWithoutSUMByIDToList(int ID);
    }
}
