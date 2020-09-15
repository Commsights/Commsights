using Commsights.Data.DataTransferObject;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commsights.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        public int AddRange(List<Product> list);
        public bool IsValid(string url);
        public bool IsValidByFileNameAndDatePublish(string fileName, DateTime datePublish);
        public List<Product> GetByCategoryIDAndDatePublishToList(int CategoryID, DateTime datePublish);
        public List<Product> GetByParentIDAndDatePublishToList(int parentID, DateTime datePublish);
        public List<Product> GetByDatePublishToList(DateTime datePublish);
        public List<Product> GetByDateUpdatedToList(DateTime dateUpdated);
        public List<Product> GetBySearchToList(string search);
        public List<ProductDataTransfer> GetDataTransferByProductSearchIDToList(int productSearchID);
        public List<ProductDataTransfer> GetDataTransferByDatePublishToList(DateTime datePublish);
        public List<ProductDataTransfer> GetDataTransferByDatePublishAndArticleTypeIDAndIndustryIDAndActionToList(DateTime datePublish, int articleTypeID, int industryID, int action);
        public List<ProductDataTransfer> GetDataTransferByDatePublishAndArticleTypeIDAndProductIDAndActionToList(DateTime datePublish, int articleTypeID, int productID, int action);
        public List<ProductDataTransfer> GetDataTransferByDatePublishAndArticleTypeIDAndCompanyIDAndActionToList(DateTime datePublish, int articleTypeID, int companyID, int action);
        public List<Product> GetBySearchAndDatePublishBeginAndDatePublishEndToList(string search, DateTime datePublishBegin, DateTime datePublishEnd);
        public List<ProductDataTransfer> ReportDailyByDatePublishAndCompanyIDToList(DateTime datePublish, int companyID);
    }
}
