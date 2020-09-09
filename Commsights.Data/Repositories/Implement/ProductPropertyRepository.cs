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
    public class ProductPropertyRepository : Repository<ProductProperty>, IProductPropertyRepository
    {
        private readonly CommsightsContext _context;
        public ProductPropertyRepository(CommsightsContext context) : base(context)
        {
            _context = context;
        }
        public string UpdateItemsWithParentIDIsZero()
        {
            string result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ProductPropertyUpdateItemsWithParentIDIsZero");
            return result;
        }
    }
}
