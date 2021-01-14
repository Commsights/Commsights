using Commsights.Data.DataTransferObject;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commsights.Data.Repositories
{
    public interface IProductPropertyRepository : IRepository<ProductProperty>
    {
        public bool IsExist(ProductProperty model);
        public string InsertItemsByID(int ID);
        public string InsertItemByID(int ID);
        public string UpdateItemsWithParentIDIsZero();
        public string UpdateSingleItemByIDAndFileName(int ID, string fileName);
        public Task<string> AsyncUpdateSingleItemByIDAndFileName(int ID, string fileName);
        public bool IsExistByProductIDAndCodeAndCompanyID(int productID, string code, int companyID);
        public bool IsExistByGUICodeAndCodeAndCompanyID(string gUICode, string code, int companyID);
        public bool IsExistByGUICodeAndCodeAndIndustryID(string gUICode, string code, int industryID);
        public bool IsExistByGUICodeAndCodeAndIndustryIDAndSegmentID(string gUICode, string code, int industryID, int segmentID);
        public bool IsExistByGUICodeAndCodeAndProductID(string gUICode, string code, int productID);
        public List<ProductPropertyDataTransfer> GetDataTransferCompanyByParentIDToList(int parentID);
        public List<ProductPropertyDataTransfer> GetDataTransferIndustryByParentIDToList(int parentID);
        public List<ProductPropertyDataTransfer> GetDataTransferProductByParentIDToList(int parentID);
        public List<ProductProperty> GetByParentIDAndCodeToList(int parentID, string code);
        public ProductProperty GetByProductIDAndCodeAndCompanyID(int productID, string code, int companyID);
        public string Initialization();
        public ProductProperty GetByID001(int ID);
        public List<ProductProperty> GetByParentIDAndCompanyIDAndArticleTypeIDToList(int parentID, int companyID, int articleTypeID);
        public List<ProductProperty> GetByReportMonthlyIDToList(int reportMonthlyID);
        public string UpdateSingleItemByCodeData(CodeData model);
        public string UpdateItemsByCodeDataCopyVersion(CodeData model);
        public int InsertItemsByCopyCodeData(int ID, int RequestUserID);
        public int InsertSingleItemByCopyCodeData(int ID, int RequestUserID);
        public string DeleteItemsByIDCodeData(int ID);
        public string DeleteItemsByID(int ID);
        public ProductProperty GetTitleAndCopyVersionAndIsCoding(string title, int copyVersion, bool isCoding);
        public List<ProductProperty> GetTitleAndSourceToList(string title, int source);
        public List<ProductProperty> GetRequestUserIDAndParentIDAndCodeAndDateUpdatedToList(int requestUserID, int parentID, string code, DateTime dateUpdated);
        public List<ProductProperty> GetRequestUserIDAndParentIDAndCodeAndDateUpdatedAndActiveToList(int requestUserID, int parentID, string code, DateTime dateUpdated, bool active);
        public List<ProductProperty> GetRequestUserIDAndParentIDAndCodeAndActiveToList(int requestUserID, int parentID, string code, bool active);
        public string UpdateItemsByDailyDownload(DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int hourBegin, int hourEnd, int industryID, string companyName, bool isCoding, bool isAnalysis, int RequestUserID);
        public string InsertItemsByIDAndRequestUserIDAndCode(int ID, int RequestUserID, string code);
        public List<ProductProperty> GetByIDAndCodeToList(int ID, string code);
        public string UpdateItemsByIDAndRequestUserIDAndProductFeatureListAndCode(int ID, int RequestUserID, string productFeatureList, string code);
        public string DeleteItemsByIDList(string IDList);
    }
}
