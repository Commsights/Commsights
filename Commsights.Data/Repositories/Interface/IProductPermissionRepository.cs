using Commsights.Data.DataTransferObject;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commsights.Data.Repositories
{
    public interface IProductPermissionRepository : IRepository<ProductPermission>
    {
        public List<ProductPermissionDataTransfer> GetProductPermissionDataTransferByIndustryIDToList(int industryID);
    }
}
