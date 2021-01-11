using Commsights.Data.DataTransferObject;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commsights.Data.Repositories
{
    public interface ICodeDataRepository
    {
        public List<CodeData> GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int employeeID);
        public List<CodeData> GetByDatePublishBeginAndDatePublishEndAndIndustryIDToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID);
        public List<Config> GetCategorySubByCategoryMainToList(string categoryMain);
        public string GetCompanyNameByTitle(string title);
        public string GetCompanyNameByURLCode(string uRLCode);
        public string GetProductNameByTitle(string title);
        public string GetProductNameByURLCode(string uRLCode);
        public string GetCategorySubByURLCode(string uRLCode);
        public List<CodeDataReport> GetReportByDatePublishBeginAndDatePublishEndToList(DateTime datePublishBegin, DateTime datePublishEnd);
        public List<CodeDataReport> GetReportByDateUpdatedBeginAndDateUpdatedEndToList(DateTime datePublishBegin, DateTime datePublishEnd);
        public List<CodeDataReport> GetReportByDatePublishBeginAndDatePublishEndAndIsUploadToList(DateTime datePublishBegin, DateTime datePublishEnd, bool isUpload);
        public List<Membership> GetReportSelectByDatePublishBeginAndDatePublishEnd001ToList(DateTime datePublishBegin, DateTime datePublishEnd);
        public List<CodeData> GetReportByDateUpdatedAndHourAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysisToList(DateTime dateUpdated, int hour, int industryID, string companyName, bool isCoding, bool isAnalysis);        
        public List<CodeData> GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndIsPublishToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int employeeID, bool isPublish);
        public List<CodeData> GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndIsUploadToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int employeeID, bool isUpload);
        public List<CodeData> GetReportByDateUpdatedAndHourBeginAndHourEndAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysisToList(DateTime dateUpdated, int hourBegin, int hourEnd, int industryID, string companyName, bool isCoding, bool isAnalysis);
        public List<CodeData> GetReportByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndIsCodingToList(DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int industryID, bool isCoding);
        public List<CodeData> GetByDateUpdatedBeginAndDateUpdatedEndAndHourBeginAndHourEndAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysisToList(DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int hourBegin, int hourEnd, int industryID, string companyName, bool isCoding, bool isAnalysis);
        public List<CodeData> GetByDateUpdatedBeginAndDateUpdatedEndAndHourBeginAndHourEndAndIndustryIDToList(DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int hourBegin, int hourEnd, int industryID);
        public List<CodeData> GetDailyByDateUpdatedBeginAndDateUpdatedEndAndHourBeginAndHourEndAndIndustryIDAndIsCodingToList(DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int hourBegin, int hourEnd, int industryID, bool isCoding);
        public List<CodeData> GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndIsUploadAndSourceIsNewspageAndTVToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int employeeID, bool isUpload, string sourceNewspage, string sourceTV);
        public List<CodeData> GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndIsUploadAndSourceIsNotNewspageAndTVToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int employeeID, bool isUpload, string sourceNewspage, string sourceTV);
        public List<CodeData> GetByDateUpdatedBeginAndDateUpdatedEndAndSourceIsNewspageAndTVToList(DateTime datePublishBegin, DateTime datePublishEnd, string sourceNewspage, string sourceTV);
        public List<CodeData> GetDailyByDateUpdatedBeginAndDateUpdatedEndAndHourBeginAndHourEndAndIndustryIDToList(DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int hourBegin, int hourEnd, int industryID);
        public CodeData GetByProductPropertyID(int productPropertyID);
        public List<CodeData> GetByDateUpdatedBeginAndDateUpdatedEndAndEmployeeIDAndIsFilterToList(DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int employeeID, bool isFilter);
    }
}
