using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commsights.Data.Repositories
{
    public interface IProductSearchRepository : IRepository<ProductSearch>
    {
        public ProductSearch SaveProductSearch(string search, DateTime datePublishBegin, DateTime datePublishEnd, int requestUserID);
    }
}
