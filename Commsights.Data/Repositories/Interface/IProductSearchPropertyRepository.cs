using Commsights.Data.DataTransferObject;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commsights.Data.Repositories
{
    public interface IProductSearchPropertyRepository : IRepository<ProductSearchProperty>
    {
        public string UpdateByProductSearchIDAndRequestUserID(int productSearchID, int requestUserID);
        public string InitializationByProductSearchIDAndRequestUserID(int productSearchID, int requestUserID);
        public List<ProductSearchProperty> GetByProductIDToList(int productID);
        public List<ProductSearchPropertyDataTransfer> GetDataTransferProductSearchByProductSearchIDToList(int productSearchID);
        public List<ProductSearchPropertyDataTransfer> GetDataTransferByParentIDToList(int parentID);
        public List<ProductSearchPropertyDataTransfer> ReportDaily02ByProductSearchIDToList(int productSearchID);
        public List<ProductSearchPropertyDataTransfer> ReportDaily02ByProductSearchIDAndActiveToList(int productSearchID, bool active);        
    }
}
