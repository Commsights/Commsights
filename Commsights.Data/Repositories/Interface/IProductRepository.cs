using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commsights.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        public bool IsValid(string url);
        public List<Product> GetByCategoryIDAndDatePublishToList(int categoryID, DateTime datePublish);
        public List<Product> GetByParentIDAndDatePublishToList(int parentID, DateTime datePublish);
        public List<Product> GetBySearchToList(string search);
    }
}
