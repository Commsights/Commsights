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
    public class ProductSearchPropertyRepository : Repository<ProductSearchProperty>, IProductSearchPropertyRepository
    {
        private readonly CommsightsContext _context;
        public ProductSearchPropertyRepository(CommsightsContext context) : base(context)
        {
            _context = context;
        }
        public List<ProductSearchPropertyDataTransfer> GetDataTransferProductSearchByProductSearchIDToList(int productSearchID)
        {
            List<ProductSearchPropertyDataTransfer> list = new List<ProductSearchPropertyDataTransfer>();
            SqlParameter[] parameters =
                       {
                new SqlParameter("@ProductSearchID",productSearchID),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductSearchPropertySelectProductSearchByProductSearchID", parameters);
            list = SQLHelper.ToList<ProductSearchPropertyDataTransfer>(dt);           
            for (int i = 0; i < list.Count; i++)
            {
                list[i].ArticleType = new ModelTemplate();
                list[i].ArticleType.ID = list[i].ArticleTypeID;
                list[i].ArticleType.TextName = list[i].ArticleTypeName;
            }
            return list;
        }
        public List<ProductSearchPropertyDataTransfer> GetDataTransferByParentIDToList(int parentID)
        {
            List<ProductSearchPropertyDataTransfer> list = new List<ProductSearchPropertyDataTransfer>();
            SqlParameter[] parameters =
                       {
                new SqlParameter("@ParentID",parentID),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductSearchPropertySelectDataTransferByParentID", parameters);
            list = SQLHelper.ToList<ProductSearchPropertyDataTransfer>(dt);           
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Company = new ModelTemplate();
                list[i].Company.ID = list[i].CompanyID;
                list[i].Company.TextName = list[i].CompanyName;
                list[i].AssessType = new ModelTemplate();
                list[i].AssessType.ID = list[i].AssessID;
                list[i].AssessType.TextName = list[i].AssessName;
            }
            return list;
        }
    }
}
