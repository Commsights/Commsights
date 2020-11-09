using Commsights.Data.DataTransferObject;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        public string UpdateByDatePublishBeginAndDatePublishEndAndIndustryIDAndAllData001(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, bool allData, int requestUserID);
        public List<ProductSearchPropertyDataTransfer> ReportDailyByProductSearchIDAndActiveToListToHTML(int productSearchID, bool active);
        public List<ProductSearchPropertyDataTransfer> ReportDailyByProductSearchIDToListToHTML(int productSearchID);
        public List<ProductDataTransfer> GetProductDataTransferByDatePublishBeginAndDatePublishEndAndIndustryIDToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID);
        public List<ProductSearchDataTransfer> InitializationByDatePublishBeginAndDatePublishEndAndIndustryIDAndHourSearchToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int hourSearch);
        public string UpdateProductPropertyByDatePublishBeginAndDatePublishEndAndIndustryID(DateTime datePublishBegin, DateTime datePublishEnd, int industryID);
        public string UpdateProductByDatePublishBeginAndDatePublishEndAndIndustryID(DateTime datePublishBegin, DateTime datePublishEnd, int industryID);
        public ProductDataTransfer GetProductDataTransferByProductPropertyID(int productPropertyID);
        public string UpdateByCompanyIDAndTitleAndProductPropertyIDAndRequestUserID(int companyID, string title, int productPropertyID, int requestUserID);
        public ProductSearchDataTransfer GetByProductSearchID(int productSearchID);
        public string DeleteProductAndProductPropertyByDatePublishBeginAndDatePublishEndAndIndustryID(DateTime datePublishBegin, DateTime datePublishEnd, int industryID);
        public string DeleteProductSearchAndProductSearchPropertyByDatePublishBeginAndDatePublishEndAndIndustryIDAndHourSearch(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int hourSearch);
        public List<ProductDataTransfer> GetByIDListToList(string iDList);
        public Task<List<ProductDataTransfer>> AsyncGetByIDListToList(string iDList);
        public List<ProductDataTransfer> GetProductDataTransferByDatePublishBeginAndDatePublishEndAndIndustryIDAndIsDailyToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, bool isDaily);
        public List<ProductDataTransfer> GetProductDataTransferByDatePublishBeginAndDatePublishEndAndIndustryIDAndIsDailyAndIsUploadToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, bool isDaily, bool isUpload);
        public string DeleteProductAndProductPropertyByDatePublishBeginAndDatePublishEndAndIndustryIDAndIsUpload(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, bool isUpload);
    }
}
