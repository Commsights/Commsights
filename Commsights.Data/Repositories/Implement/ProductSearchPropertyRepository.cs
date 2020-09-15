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
        public List<ProductSearchProperty> GetByProductIDToList(int productID)
        {
            return _context.ProductSearchProperty.Where(item => item.ProductID == (productID)).OrderByDescending(item => item.ID).ToList();
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
        public string InitializationByProductSearchIDAndRequestUserID(int productSearchID, int requestUserID)
        {
            string result = "0";
            if (productSearchID > 0)
            {
                SqlParameter[] parameters =
                           {
                new SqlParameter("@ProductSearchID",productSearchID),
                new SqlParameter("@RequestUserID",requestUserID)
            };
                result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ProductSearchPropertyInitializationByProductSearchIDAndRequestUserID", parameters);
            }
            return result;
        }
        public string UpdateByProductSearchIDAndRequestUserID(int productSearchID, int requestUserID)
        {
            string result = "0";
            if (productSearchID > 0)
            {
                SqlParameter[] parameters =
                           {
                new SqlParameter("@ProductSearchID",productSearchID),
                new SqlParameter("@RequestUserID",requestUserID)
            };
                result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ProductSearchPropertyUpdateByProductSearchIDAndRequestUserID", parameters);
            }
            return result;
        }
        public List<ProductSearchPropertyDataTransfer> ReportDaily02ByProductSearchIDToList(int productSearchID)
        {
            List<ProductSearchPropertyDataTransfer> list = new List<ProductSearchPropertyDataTransfer>();
            if (productSearchID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ProductSearchID",productSearchID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportDaily02ByProductSearchID", parameters);
                list = SQLHelper.ToList<ProductSearchPropertyDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].AssessType = new ModelTemplate();
                    list[i].AssessType.ID = list[i].AssessID;
                    list[i].AssessType.TextName = list[i].AssessName;
                }
            }
            return list;
        }
        public List<ProductSearchPropertyDataTransfer> ReportDaily02ByProductSearchIDAndActiveToList(int productSearchID, bool active)
        {
            List<ProductSearchPropertyDataTransfer> list = new List<ProductSearchPropertyDataTransfer>();
            if (productSearchID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ProductSearchID",productSearchID),
                    new SqlParameter("@Active",active)
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportDaily02ByProductSearchIDAndActive", parameters);
                list = SQLHelper.ToList<ProductSearchPropertyDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].AssessType = new ModelTemplate();
                    list[i].AssessType.ID = list[i].AssessID;
                    list[i].AssessType.TextName = list[i].AssessName;
                }
            }
            return list;
        }        
    }
}
