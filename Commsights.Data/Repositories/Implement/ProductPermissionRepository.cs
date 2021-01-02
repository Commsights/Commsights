using Commsights.Data.DataTransferObject;
using Commsights.Data.Helpers;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commsights.Data.Repositories
{
    public class ProductPermissionRepository : Repository<ProductPermission>, IProductPermissionRepository
    {
        private readonly CommsightsContext _context;
        public ProductPermissionRepository(CommsightsContext context) : base(context)
        {
            _context = context;
        }
        public List<ProductPermissionDataTransfer> GetProductPermissionDataTransferByIndustryIDToList(int industryID)
        {
            List<ProductPermissionDataTransfer> list = new List<ProductPermissionDataTransfer>();
            if (industryID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@IndustryID",industryID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductPermissionSelectByIndustryID", parameters);
                list = SQLHelper.ToList<ProductPermissionDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Employee = new ModelTemplate();
                    list[i].Employee.ID = list[i].EmployeeID;
                    list[i].Employee.TextName = list[i].EmployeeName;
                }
            }
            return list;
        }        
    }
}
