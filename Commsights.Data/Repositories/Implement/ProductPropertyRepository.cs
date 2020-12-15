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
        public string UpdateSingleItemByIDAndFileName(int ID, string fileName)
        {
            SqlParameter[] parameters =
                       {
                new SqlParameter("@ID",ID),
                new SqlParameter("@FileName",fileName),
            };
            string result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ProductPropertyUpdateSingleItemByIDAndFileName", parameters);
            return result;
        }
        public async Task<string> AsyncUpdateSingleItemByIDAndFileName(int ID, string fileName)
        {
            SqlParameter[] parameters =
                       {
                new SqlParameter("@ID",ID),
                new SqlParameter("@FileName",fileName),
            };
            string result = await SQLHelper.ExecuteNonQueryAsync(AppGlobal.ConectionString, "sp_ProductPropertyUpdateSingleItemByIDAndFileName", parameters);
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
        public List<ProductProperty> GetByReportMonthlyIDToList(int reportMonthlyID)
        {
            return _context.ProductProperty.Where(item => item.ReportMonthlyID == reportMonthlyID).OrderBy(item => item.ID).ToList();
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
        public string UpdateSingleItemByCodeData(CodeData model)
        {
            SqlParameter[] parameters =
            {
new SqlParameter("@ID",model.ProductPropertyID),
new SqlParameter("@CategoryMain",model.CategoryMain),
new SqlParameter("@CategorySub",model.CategorySub),
new SqlParameter("@CompanyName",model.CompanyName),
new SqlParameter("@CorpCopy",model.CorpCopy),
new SqlParameter("@SOECompany",model.SOECompany),
new SqlParameter("@ProductName_ProjectName",model.ProductName_ProjectName),
new SqlParameter("@SOEProduct",model.SOEProduct),
new SqlParameter("@SentimentCorp",model.SentimentCorp),
new SqlParameter("@TierCommsights",model.TierCommsights),
new SqlParameter("@CampaignName",model.CampaignName),
new SqlParameter("@CampaignKeyMessage",model.CampaignKeyMessage),
new SqlParameter("@KeyMessage",model.KeyMessage),
new SqlParameter("@CompetitiveWhat",model.CompetitiveWhat),
new SqlParameter("@CompetitiveNewsValue",model.CompetitiveNewsValue),
new SqlParameter("@TasteValue",model.TasteValue),
new SqlParameter("@PriceValue",model.PriceValue),
new SqlParameter("@NutritionFactValue",model.NutritionFactValue),
new SqlParameter("@VitaminValue",model.VitaminValue),
new SqlParameter("@GoodForHealthValue",model.GoodForHealthValue),
new SqlParameter("@Bottle_CanDesignValue",model.Bottle_CanDesignValue),
new SqlParameter("@TierValue",model.TierValue),
new SqlParameter("@HeadlineValue",model.HeadlineValue),
new SqlParameter("@PictureValue",model.PictureValue),
new SqlParameter("@KOLValue",model.KOLValue),
new SqlParameter("@OtherValue",model.OtherValue),
new SqlParameter("@SpokePersonName",model.SpokePersonName),
new SqlParameter("@SpokePersonTitle",model.SpokePersonTitle),
new SqlParameter("@Segment",model.Segment),
new SqlParameter("@FeatureValue",model.FeatureValue),
new SqlParameter("@SpokePersonValue",model.SpokePersonValue),
new SqlParameter("@ToneValue",model.ToneValue),
new SqlParameter("@MPS",model.MPS),
new SqlParameter("@ROME_Corp_VND",model.ROME_Corp_VND),
new SqlParameter("@ROME_Product_VND",model.ROME_Product_VND),
new SqlParameter("@FeatureCorp",model.FeatureCorp),
new SqlParameter("@FeatureProduct",model.FeatureProduct),
};
            string result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ProductPropertyUpdateSingleItemByCodeData", parameters);
            return result;
        }
    }
}
