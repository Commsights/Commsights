using Commsights.Data.DataTransferObject;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commsights.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        public bool IsValid(string url);
        public bool IsValidByFileNameAndDatePublish(string fileName, DateTime datePublish);
        public List<Product> GetByCategoryIDAndDatePublishToList(int categoryID, DateTime datePublish);
        public List<Product> GetByParentIDAndDatePublishToList(int parentID, DateTime datePublish);
        public List<Product> GetByDatePublishToList(DateTime datePublish);
        public List<Product> GetByDateUpdatedToList(DateTime dateUpdated);
        public List<Product> GetBySearchToList(string search);
        public List<ProductDataTransfer> GetDataTransferByProductSearchIDToList(int productSearchID);
        public List<Product> GetBySearchAndDatePublishBeginAndDatePublishEndToList(string search, DateTime datePublishBegin, DateTime datePublishEnd);
    }
}
