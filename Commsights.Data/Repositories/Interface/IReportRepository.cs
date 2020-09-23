using Commsights.Data.DataTransferObject;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commsights.Data.Repositories
{
    public interface IReportRepository
    {
        public string InitializationByProductSearchIDAndRequestUserID(int productSearchID, int requestUserID);
        public List<ProductSearchDataTransfer> InitializationByDatePublishToList(DateTime datePublish);
        public List<ProductDataTransfer> ReportDailyByDatePublishAndCompanyIDToList(DateTime datePublish, int companyID);
        public List<ProductDataTransfer> ReportDailyProductByDatePublishAndCompanyIDToList(DateTime datePublish, int companyID);
        public List<ProductDataTransfer> ReportDailyIndustryByDatePublishAndCompanyIDToList(DateTime datePublish, int companyID);
        public List<ProductDataTransfer> ReportDailyCompetitorByDatePublishAndCompanyIDToList(DateTime datePublish, int companyID);
        public List<ProductSearchPropertyDataTransfer> ReportDaily02ByProductSearchIDToList(int productSearchID);
        public List<ProductSearchPropertyDataTransfer> ReportDaily02ByProductSearchIDAndActiveToList(int productSearchID, bool active);
        public List<ProductDataTransfer> GetDataTransferByDatePublishBeginAndDatePublishEndAndIndustryIDToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID);
        public List<ProductSearchDataTransfer> InitializationByDatePublishBeginAndDatePublishEndAndIndustryIDToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID);
        public List<ProductSearchDataTransfer> InitializationByDatePublishBeginAndDatePublishEndAndIndustryIDAndAllDataAndAllSummaryToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, bool allData, bool allSummary, int requestUserID);
        public string UpdateByDatePublishBeginAndDatePublishEndAndIndustryIDAndAllData(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, bool allData, int requestUserID);
    }
}
