using Commsights.Data.DataTransferObject;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commsights.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        public Product GetByImageThumbnail(string imageThumbnail);
        public Product GetByByDatePublishBeginAndDatePublishEndAndIndustryIDAndSourceID(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int sourceID);
        public Product GetByID001(int ID);
        public Product GetByURLCode(string uRLCode);
        public Product GetByURLCode001(string uRLCode);
        public Product GetByFileName(string fileName);
        public Product GetByFileNameAndDatePublish(string fileName, DateTime datePublish);
        public void FilterProduct(Product product, List<ProductProperty> listProductProperty, int RequestUserID);
        public int AddRange(List<Product> list);
        public bool IsValid(string url);
        public bool IsValidBySQL(string url);
        public bool IsValidByFileNameAndDatePublish(string fileName, DateTime datePublish);
        public List<Product> GetByCategoryIDAndDatePublishToList(int CategoryID, DateTime datePublish);
        public List<Product> GetByParentIDAndDatePublishToList(int parentID, DateTime datePublish);
        public List<Product> GetByDatePublishToList(DateTime datePublish);
        public List<Product> GetByDateUpdatedToList(DateTime dateUpdated);
        public List<Product> GetBySearchToList(string search);
        public List<Product> GetByTitleToList(string title);
        public List<ProductDataTransfer> GetDataTransferByProductSearchIDToList(int productSearchID);
        public List<ProductDataTransfer> GetDataTransferByDatePublishToList(DateTime datePublish);
        public List<ProductDataTransfer> GetDataTransferByDatePublishAndArticleTypeIDAndIndustryIDAndActionToList(DateTime datePublish, int articleTypeID, int industryID, int action);
        public List<ProductDataTransfer> GetDataTransferByDatePublishAndArticleTypeIDAndProductIDAndActionToList(DateTime datePublish, int articleTypeID, int productID, int action);
        public List<ProductDataTransfer> GetDataTransferByDatePublishAndArticleTypeIDAndCompanyIDAndActionToList(DateTime datePublish, int articleTypeID, int companyID, int action);
        public List<Product> GetBySearchAndDatePublishBeginAndDatePublishEndToList(string search, DateTime datePublishBegin, DateTime datePublishEnd);
        public List<ProductDataTransfer> GetDataTransferByDatePublishBeginAndDatePublishEndAndIndustryIDToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID);
        public List<ProductDataTransfer> ReportDailyByDatePublishAndCompanyIDToList(DateTime datePublish, int companyID);
        public List<ProductDataTransfer> ReportDailyProductByDatePublishAndCompanyIDToList(DateTime datePublish, int companyID);
        public List<ProductDataTransfer> ReportDailyIndustryByDatePublishAndCompanyIDToList(DateTime datePublish, int companyID);
        public List<ProductDataTransfer> ReportDailyCompetitorByDatePublishAndCompanyIDToList(DateTime datePublish, int companyID);
        public List<ProductDataTransfer> GetByIDListToList(string iDList);
        public List<Product> GetByDatePublishBeginAndDatePublishEndAndSearchAndSourceToList(DateTime datePublishBegin, DateTime datePublishEnd, string search, string source);
        public Task<List<Product>> AsyncGetByDatePublishBeginAndDatePublishEndAndSearchAndSourceToList(DateTime datePublishBegin, DateTime datePublishEnd, string search, string source);
        public Task<List<ProductCompact>> AsyncGetProductCompactByDatePublishBeginAndDatePublishEndAndSearchAndSourceToList(DateTime datePublishBegin, DateTime datePublishEnd, string search, string source);
        public Task<string> AsyncInsertSingleItem(Product product);
        public Task<string> AsyncInsertSingleItemAuto(Product product);
        public string InsertSingleItem(Product product);
        public Task<string> AsyncUpdateSingleItem(Product product);
        public List<Product> GetByAndiToList();
        public Product GetByProductID(int productID);
        public Product GetByPriceUnitID(int priceUnitID);
        public Product GetByTitle(string title);
        public Product GetByTitleAndSource(string title, string source);
        public Task<List<ProductCompact>> AsyncGetProductCompactByDatePublishBeginAndDatePublishEndAndSearchAndIsTitleAndIsDescriptionAndSourceToList(DateTime datePublishBegin, DateTime datePublishEnd, string search, string source, bool isTitle, bool isDescription);
        public List<ProductCompact001> GetAllProductCompact001ToList();
        public List<ProductCompact001> GetProductCompactBySourceToList(string source);
        public string UpdateProductCompactSingleItem(ProductCompact product);
        public Task<string> AsyncUpdateProductCompact001SingleItem(ProductCompact001 product);
        public Task<string> AsyncUpdateProductCompact001SingleItemWithIDAndDescription(ProductCompact001 product);
        public List<ProductCompact001> GetProductCompact001BySourceWithIDAndTitleToList(string source);
        public List<ProductCompact001> GetProductCompact001BySourceAndRowBeginAndRowEndWithIDAndDescriptionToList(string source, int rowBegin, int rowEnd);
        public string UpdateSingleItemByCodeData(CodeData model);
        public string Initialization();
        public Task<List<ProductCompact>> AsyncGetProductCompactByDatePublishBeginAndDatePublishEndAndSearchAndIsTitleAndIsDescriptionAndSourceAndIsPublishToList(DateTime datePublishBegin, DateTime datePublishEnd, string search, string source, bool isTitle, bool isDescription, bool isPublish);
        public Task<List<ProductCompact>> AsyncGetProductCompactByDatePublishBeginAndDatePublishEndAndSearchAndIsTitleAndIsDescriptionAndSourceAndIsUploadToList(DateTime datePublishBegin, DateTime datePublishEnd, string search, string source, bool isTitle, bool isDescription, bool isUpload);
        public string InsertSingleItemAuto(Product product);
        public Task<string> AsyncInsertSingleItemAutoNoFilter(Product product);
    }
}
