using Commsights.Data.DataTransferObject;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commsights.Data.Repositories
{
    public interface IProductSearchPropertyRepository : IRepository<ProductSearchProperty>
    {
       
        public List<ProductSearchProperty> GetByProductIDToList(int productID);
        public List<ProductSearchPropertyDataTransfer> GetDataTransferProductSearchByProductSearchIDToList(int productSearchID);
        public List<ProductSearchPropertyDataTransfer> GetDataTransferByParentIDToList(int parentID);
    }
}
