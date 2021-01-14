using Commsights.Data.DataTransferObject;
using Commsights.Data.Enum;
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
            List<ProductProperty> list = new List<ProductProperty>();
            if (parentID > 0)
            {
                list = _context.ProductProperty.Where(item => item.ParentID == parentID && item.Code.Equals(code)).OrderBy(item => item.DateUpdated).ToList();
            }
            return list;
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
        public List<ProductProperty> GetRequestUserIDAndParentIDAndCodeAndDateUpdatedToList(int requestUserID, int parentID, string code, DateTime dateUpdated)
        {
            List<ProductProperty> list = new List<ProductProperty>();
            if (requestUserID > 0)
            {
                SqlParameter[] parameters =
                {
                new SqlParameter("@RequestUserID",requestUserID),
                new SqlParameter("@ParentID",parentID),
                new SqlParameter("@Code",code),
                new SqlParameter("@DateUpdated",dateUpdated),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductPropertySelectByRequestUserIDAndParentIDAndCodeAndDateUpdated", parameters);
                list = SQLHelper.ToList<ProductProperty>(dt);
            }
            return list;
        }
        public List<ProductProperty> GetRequestUserIDAndParentIDAndCodeAndDateUpdatedAndActiveToList(int requestUserID, int parentID, string code, DateTime dateUpdated, bool active)
        {
            List<ProductProperty> list = new List<ProductProperty>();
            if (requestUserID > 0)
            {
                SqlParameter[] parameters =
                {
                new SqlParameter("@RequestUserID",requestUserID),
                new SqlParameter("@ParentID",parentID),
                new SqlParameter("@Code",code),
                new SqlParameter("@DateUpdated",dateUpdated),
                new SqlParameter("@Active",active),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductPropertySelectByRequestUserIDAndParentIDAndCodeAndDateUpdatedAndActive", parameters);
                list = SQLHelper.ToList<ProductProperty>(dt);
            }
            return list;
        }
        public List<ProductProperty> GetRequestUserIDAndParentIDAndCodeAndActiveToList(int requestUserID, int parentID, string code, bool active)
        {
            List<ProductProperty> list = new List<ProductProperty>();
            if (requestUserID > 0)
            {
                SqlParameter[] parameters =
                {
                new SqlParameter("@RequestUserID",requestUserID),
                new SqlParameter("@ParentID",parentID),
                new SqlParameter("@Code",code),
                new SqlParameter("@Active",active),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductPropertySelectByRequestUserIDAndParentIDAndCodeAndActive", parameters);
                list = SQLHelper.ToList<ProductProperty>(dt);
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
        public ProductProperty GetTitleAndCopyVersionAndIsCoding(string title, int copyVersion, bool isCoding)
        {
            ProductProperty model = new ProductProperty();

            SqlParameter[] parameters =
                   {
                new SqlParameter("@Title",title),
                new SqlParameter("@CopyVersion",copyVersion),
                new SqlParameter("@IsCoding",isCoding),
             };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductPropertySelectByTitleAndCopyVersionAndIsCoding", parameters);
            model = SQLHelper.ToList<ProductProperty>(dt).FirstOrDefault();

            return model;
        }
        public List<ProductProperty> GetTitleAndSourceToList(string title, int source)
        {
            List<ProductProperty> list = new List<ProductProperty>();
            SqlParameter[] parameters =
                   {
                new SqlParameter("@Title",title),
                new SqlParameter("@Source",source),
             };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductPropertySelectByTitleAndSource", parameters);
            list = SQLHelper.ToList<ProductProperty>(dt);
            return list;
        }
        public List<ProductProperty> GetByIDAndCodeToList(int ID, string code)
        {
            List<ProductProperty> list = new List<ProductProperty>();
            SqlParameter[] parameters =
                   {
                new SqlParameter("@ID",ID),
                new SqlParameter("@Code",code),
             };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductPropertySelectByIDAndCode", parameters);
            list = SQLHelper.ToList<ProductProperty>(dt);
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
        public string InsertItemsByIDAndRequestUserIDAndCode(int ID, int RequestUserID, string code)
        {
            SqlParameter[] parameters =
                      {
                new SqlParameter("@ID",ID),
                new SqlParameter("@RequestUserID",RequestUserID),
                new SqlParameter("@Code",code),
            };
            string result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ProductPropertyInsertItemsByIDAndRequestUserIDAndCode", parameters);
            return result;
        }
        public string UpdateItemsByIDAndRequestUserIDAndProductFeatureListAndCode(int ID, int RequestUserID, string productFeatureList, string code)
        {
            if (string.IsNullOrEmpty(productFeatureList))
            {
                productFeatureList = "";
            }
            SqlParameter[] parameters =
                      {
                new SqlParameter("@ID",ID),
                new SqlParameter("@RequestUserID",RequestUserID),
                new SqlParameter("@ProductFeatureList",productFeatureList),
                new SqlParameter("@Code",code),
            };
            string result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ProductPropertyUpdateItemsByIDAndRequestUserIDAndProductFeatureListAndCode", parameters);
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
new SqlParameter("@Advalue",model.Advalue),
new SqlParameter("@IsCoding",model.IsCoding),
new SqlParameter("@UserUpdated",model.UserUpdated),
new SqlParameter("@DatePublish",model.DatePublish),
new SqlParameter("@MediaType",model.MediaType),
new SqlParameter("@MediaTitle",model.MediaTitle),
new SqlParameter("@Page",model.Page),
new SqlParameter("@RowIndex",model.RowIndex),
new SqlParameter("@Title",model.Title),
};
            string result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ProductPropertyUpdateSingleItemByCodeData", parameters);
            return result;
        }
        public string UpdateItemsByCodeDataCopyVersion(CodeData model)
        {
            if (!string.IsNullOrEmpty(model.CategorySub))
            {
                if (model.CategorySub.Split(':').Length > 1)
                {
                    model.CategoryMain = model.CategorySub.Split(':')[0];
                    model.CategorySub = model.CategorySub.Split(':')[1];
                    model.CategoryMain = model.CategoryMain.Trim();
                    model.CategorySub = model.CategorySub.Trim();
                }
            }
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
new SqlParameter("@Advalue",model.Advalue),
new SqlParameter("@IsCoding",model.IsCoding),
new SqlParameter("@UserUpdated",model.UserUpdated),
new SqlParameter("@DatePublish",model.DatePublish),
new SqlParameter("@MediaType",model.MediaType),
new SqlParameter("@MediaTitle",model.MediaTitle),
new SqlParameter("@Page",model.Page),
new SqlParameter("@RowIndex",model.RowIndex),
new SqlParameter("@Title",model.Title),
};
            string result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ProductPropertyUpdateItemsByCodeDataCopyVersion", parameters);
            return result;
        }
        public string UpdateItemsByDailyDownload(DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int hourBegin, int hourEnd, int industryID, string companyName, bool isCoding, bool isAnalysis, int RequestUserID)
        {
            string result = "";
            if (industryID > 0)
            {
                dateUpdatedBegin = new DateTime(dateUpdatedBegin.Year, dateUpdatedBegin.Month, dateUpdatedBegin.Day, hourBegin, 0, 0);
                dateUpdatedEnd = new DateTime(dateUpdatedEnd.Year, dateUpdatedEnd.Month, dateUpdatedEnd.Day, hourEnd, 59, 59);
                SqlParameter[] parameters =
                {
                new SqlParameter("@DateUpdatedBegin",dateUpdatedBegin),
                new SqlParameter("@DateUpdatedEnd",dateUpdatedEnd),
                new SqlParameter("@IndustryID",industryID),
                new SqlParameter("@CompanyName",companyName),
                new SqlParameter("@IsCoding",isCoding),
                new SqlParameter("@IsAnalysis",isAnalysis),
                new SqlParameter("@@RequestUserID",RequestUserID),
                };
                result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ProductPropertyUpdateItemsByDailyDownload", parameters);
            }
            return result;
        }
        public int InsertItemsByCopyCodeData(int ID, int RequestUserID)
        {
            List<ProductProperty> listSame = GetProductPropertySelectItemsSameParentIDByIDToList(ID);
            List<ProductProperty> listParentID = GetProductPropertySelectItemsDistinctParentIDSameTitleAndDifferentURLCodeByIDToList(ID);
            for (int j = 0; j < listParentID.Count; j++)
            {
                int parentID = listParentID[j].ParentID.Value;
                List<ProductProperty> listDifferent = GetSQLByParentIDToList(parentID);
                if (listDifferent.Count > 0)
                {
                    if (listSame.Count > listDifferent.Count)
                    {
                        int rowBegin = listDifferent.Count;
                        int rowEnd = listSame.Count;
                        for (int i = rowBegin; i < rowEnd; i++)
                        {
                            ProductProperty itemCopy = listSame[i];
                            itemCopy.IsCoding = true;
                            itemCopy.ParentID = parentID;
                            itemCopy.Source = listDifferent[0].Source;
                            itemCopy.CopyVersion = listSame[i].CopyVersion;
                            itemCopy.GUICode = listDifferent[0].GUICode;
                            itemCopy.ID = 0;
                            itemCopy.Advalue = 0;
                            itemCopy.FileName = "";
                            itemCopy.MediaTitle = "";
                            itemCopy.MediaType = "";
                            itemCopy.Initialization(InitType.Insert, RequestUserID);
                            _context.Set<ProductProperty>().Add(itemCopy);
                            _context.SaveChanges();
                            InitializationCodeDataByID(itemCopy.ID);
                        }
                    }
                }
            }
            return 0;
        }
        public string InitializationCodeDataByID(int ID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ID",ID),
            };
            string result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ProductPropertyInitializationCodeDataByID", parameters);
            return result;
        }
        public int InsertSingleItemByCopyCodeData(int ID, int RequestUserID)
        {
            int IDNew = 0;
            using (SqlConnection cn = new SqlConnection(AppGlobal.ConectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ProductPropertyInsertSingleItemByCopyCodeData", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                cmd.Parameters.Add("@RequestUserID", SqlDbType.Int).Value = RequestUserID;
                cmd.Parameters.Add("@returnValue", SqlDbType.Int);
                cmd.Parameters["@returnValue"].Direction = ParameterDirection.Output;
                cn.Open();
                int ret = cmd.ExecuteNonQuery();
                if (ret > 0)
                {
                    IDNew = (int)cmd.Parameters["@returnValue"].Value;
                }
            }
            return IDNew;
        }
        public string DeleteItemsByIDCodeData(int ID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ID",ID),
            };
            string result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ProductPropertyDeleteItemsByIDCodeData", parameters);
            return result;
        }
        public string DeleteItemsByID(int ID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ID",ID),
            };
            string result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ProductPropertyDeleteItemsByID", parameters);
            return result;
        }
        public List<ProductProperty> GetSQLByParentIDToList(int parentID)
        {
            List<ProductProperty> list = new List<ProductProperty>();
            SqlParameter[] parameters =
            {
                new SqlParameter("@ParentID",parentID),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductPropertySelectByParentID", parameters);
            list = SQLHelper.ToList<ProductProperty>(dt);
            return list;
        }
        public List<ProductProperty> GetProductPropertySelectItemsDistinctParentIDSameTitleAndDifferentURLCodeByIDToList(int ID)
        {
            List<ProductProperty> list = new List<ProductProperty>();
            SqlParameter[] parameters =
                       {
                new SqlParameter("@ID",ID),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductPropertySelectItemsDistinctParentIDSameTitleAndDifferentURLCodeByID", parameters);
            list = SQLHelper.ToList<ProductProperty>(dt);
            return list;
        }
        public List<ProductProperty> GetProductPropertySelectItemsSameTitleAndDifferentURLCodeByIDToList(int ID)
        {
            List<ProductProperty> list = new List<ProductProperty>();
            SqlParameter[] parameters =
                       {
                new SqlParameter("@ID",ID),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductPropertySelectItemsSameTitleAndDifferentURLCodeByID", parameters);
            list = SQLHelper.ToList<ProductProperty>(dt);
            return list;
        }
        public List<ProductProperty> GetProductPropertySelectItemsSameTitleAndURLCodeByIDToList(int ID)
        {
            List<ProductProperty> list = new List<ProductProperty>();
            SqlParameter[] parameters =
                       {
                new SqlParameter("@ID",ID),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductPropertySelectItemsSameTitleAndURLCodeByID", parameters);
            list = SQLHelper.ToList<ProductProperty>(dt);
            return list;
        }
        public List<ProductProperty> GetProductPropertySelectItemsSameParentIDByIDToList(int ID)
        {
            List<ProductProperty> list = new List<ProductProperty>();
            SqlParameter[] parameters =
                       {
                new SqlParameter("@ID",ID),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductPropertySelectItemsSameParentIDByID", parameters);
            list = SQLHelper.ToList<ProductProperty>(dt);
            return list;
        }
        public string DeleteItemsByIDList(string IDList)
        {
            string result = "";
            if (!string.IsNullOrEmpty(IDList))
            {
                SqlParameter[] parameters =
                {
                new SqlParameter("@IDList",IDList),
                };
                result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ProductPropertyDeleteItemsByIDList", parameters);
            }
            return result;
        }
    }
}
