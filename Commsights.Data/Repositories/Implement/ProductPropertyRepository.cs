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
        public bool IsExist(ProductProperty model)
        {
            return _context.ProductProperty.FirstOrDefault(item => item.ParentID == model.ParentID && item.Code.Equals(model.Code) && item.CompanyID == model.CompanyID && item.IndustryID == model.IndustryID) == null ? true : false;
        }
        public bool IsExistByProductIDAndCodeAndCompanyID(int productID, string code, int companyID)
        {
            return _context.ProductProperty.FirstOrDefault(item => item.ProductID == productID && item.Code.Equals(code) && item.CompanyID == companyID) == null ? true : false;
        }
        public bool IsExistByGUICodeAndCodeAndCompanyID(string gUICode, string code, int companyID)
        {
            var model = _context.ProductProperty.FirstOrDefault(item => item.GUICode.Equals(gUICode) && item.Code.Equals(code) && item.CompanyID == companyID);
            if (model != null)
            {
                return true;
            }
            return false;
        }
        public bool IsExistByGUICodeAndCodeAndIndustryID(string gUICode, string code, int industryID)
        {
            var model = _context.ProductProperty.FirstOrDefault(item => item.GUICode.Equals(gUICode) && item.Code.Equals(code) && item.IndustryID == industryID);
            if (model != null)
            {
                return true;
            }
            return false;
        }
        public bool IsExistByGUICodeAndCodeAndIndustryIDAndSegmentID(string gUICode, string code, int industryID, int segmentID)
        {
            var model = _context.ProductProperty.FirstOrDefault(item => item.GUICode.Equals(gUICode) && item.Code.Equals(code) && item.IndustryID == industryID && item.SegmentID == segmentID);
            if (model != null)
            {
                return true;
            }
            return false;
        }
        public bool IsExistByGUICodeAndCodeAndProductID(string gUICode, string code, int productID)
        {
            var model = _context.ProductProperty.FirstOrDefault(item => item.GUICode.Equals(gUICode) && item.Code.Equals(code) && item.ProductID == productID);
            if (model != null)
            {
                return true;
            }
            return false;
        }
        public ProductProperty GetByProductIDAndCodeAndCompanyID(int productID, string code, int companyID)
        {
            return _context.ProductProperty.FirstOrDefault(item => item.ProductID == productID && item.Code.Equals(code) && item.CompanyID == companyID);
        }
        public List<ProductProperty> GetByParentIDAndCodeToList(int parentID, string code)
        {
            return _context.ProductProperty.Where(item => item.ParentID == parentID && item.Code.Equals(code)).OrderBy(item => item.DateUpdated).ToList();
        }
        public List<ProductProperty> GetByParentIDAndCompanyIDAndArticleTypeIDToList(int parentID, int companyID, int articleTypeID)
        {
            return _context.ProductProperty.Where(item => item.ParentID == parentID && item.CompanyID == companyID && item.ArticleTypeID == articleTypeID).OrderBy(item => item.DateUpdated).ToList();
        }
        public ProductProperty GetByID001(int ID)
        {
            ProductProperty model = new ProductProperty();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@ID",ID),
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductPropertySelectByID", parameters);
                model = SQLHelper.ToList<ProductProperty>(dt).FirstOrDefault();
            }

            return model;
        }
        public List<ProductPropertyDataTransfer> GetDataTransferCompanyByParentIDToList(int parentID)
        {
            List<ProductPropertyDataTransfer> list = new List<ProductPropertyDataTransfer>();
            if (parentID > 0)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@ParentID",parentID),
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductPropertySelectDataTransferCompanyByParentID", parameters);
                list = SQLHelper.ToList<ProductPropertyDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].AssessType = new ModelTemplate();
                    list[i].AssessType.ID = list[i].AssessID;
                    list[i].AssessType.TextName = list[i].AssessName;
                }
            }

            return list;
        }
        public List<ProductPropertyDataTransfer> GetDataTransferIndustryByParentIDToList(int parentID)
        {
            List<ProductPropertyDataTransfer> list = new List<ProductPropertyDataTransfer>();
            if (parentID > 0)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@ParentID",parentID),
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductPropertySelectDataTransferIndustryByParentID", parameters);
                list = SQLHelper.ToList<ProductPropertyDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].AssessType = new ModelTemplate();
                    list[i].AssessType.ID = list[i].AssessID;
                    list[i].AssessType.TextName = list[i].AssessName;
                }
            }

            return list;
        }
        public List<ProductPropertyDataTransfer> GetDataTransferProductByParentIDToList(int parentID)
        {
            List<ProductPropertyDataTransfer> list = new List<ProductPropertyDataTransfer>();
            if (parentID > 0)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@ParentID",parentID),
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductPropertySelectDataTransferProductByParentID", parameters);
                list = SQLHelper.ToList<ProductPropertyDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].AssessType = new ModelTemplate();
                    list[i].AssessType.ID = list[i].AssessID;
                    list[i].AssessType.TextName = list[i].AssessName;
                }
            }

            return list;
        }
        public string Initialization()
        {
            return SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ProductPropertyInitialization");
        }
        public string InsertItemsByID(int ID)
        {
            SqlParameter[] parameters =
                      {
                new SqlParameter("@ID",ID),
            };
            return SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ProductPropertyInsertItemsByID", parameters);
        }
        public string InsertItemByID(int ID)
        {
            SqlParameter[] parameters =
                      {
                new SqlParameter("@ID",ID),
            };
            string result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ProductPropertyInsertItemByID", parameters);
            return result;
        }
    }
}
