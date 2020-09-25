using Commsights.Data.DataTransferObject;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commsights.Data.Repositories
{
    public interface IProductSearchRepository : IRepository<ProductSearch>
    {
        public List<ProductSearchDataTransfer> GetByDatePublishBeginAndDatePublishEndAndIndustryIDToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID);
        public ProductSearchDataTransfer GetDataTransferByID(int ID);
        public List<ProductSearchDataTransfer> InitializationByDatePublishToList(DateTime datePublish);
        public List<ProductSearch> GetByDateSearchBeginAndDateSearchEndToList(DateTime dateSearchBegin, DateTime dateSearchEnd);
        public string UpdateByID(int ID, int userUpdated, bool isSend);
        public ProductSearch SaveProductSearch(string search, DateTime datePublishBegin, DateTime datePublishEnd, int requestUserID, bool isAll);
    }
}
