using Commsights.Data.DataTransferObject;
using Commsights.Data.Helpers;
using Commsights.Data.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commsights.Data.Repositories
{
    public class ReportRepository : IReportRepository
    {
        public ReportRepository()
        {
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
        public List<ProductSearchDataTransfer> InitializationByDatePublishToList(DateTime datePublish)
        {
            List<ProductSearchDataTransfer> list = new List<ProductSearchDataTransfer>();
            if (datePublish.Year > 2019)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@DatePublish",datePublish),
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportDailyInitializationByDatePublish", parameters);
                list = SQLHelper.ToList<ProductSearchDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Company = new ModelTemplate();
                    list[i].Company.ID = list[i].CompanyID;
                    list[i].Company.TextName = list[i].CompanyName;
                }
            }

            return list;
        }
        public List<ProductDataTransfer> ReportDailyByDatePublishAndCompanyIDToList(DateTime datePublish, int companyID)
        {
            List<ProductDataTransfer> list = new List<ProductDataTransfer>();
            if ((datePublish != null) && (companyID > 0))
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@DatePublish",datePublish),
                new SqlParameter("@CompanyID",companyID)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportDailyByDatePublishAndCompanyID", parameters);
                list = SQLHelper.ToList<ProductDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].AssessType = new ModelTemplate();
                    list[i].AssessType.ID = list[i].AssessID;
                    list[i].AssessType.TextName = list[i].AssessName;
                }
            }
            return list;
        }
        public List<ProductDataTransfer> ReportDailyProductByDatePublishAndCompanyIDToList(DateTime datePublish, int companyID)
        {
            List<ProductDataTransfer> list = new List<ProductDataTransfer>();
            if ((datePublish != null) && (companyID > 0))
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@DatePublish",datePublish),
                new SqlParameter("@CompanyID",companyID)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportDailyProductByDatePublishAndCompanyID", parameters);
                list = SQLHelper.ToList<ProductDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].AssessType = new ModelTemplate();
                    list[i].AssessType.ID = list[i].AssessID;
                    list[i].AssessType.TextName = list[i].AssessName;
                }
            }
            return list;
        }
        public List<ProductDataTransfer> ReportDailyIndustryByDatePublishAndCompanyIDToList(DateTime datePublish, int companyID)
        {
            List<ProductDataTransfer> list = new List<ProductDataTransfer>();
            if ((datePublish != null) && (companyID > 0))
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@DatePublish",datePublish),
                new SqlParameter("@CompanyID",companyID)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportDailyIndustryByDatePublishAndCompanyID", parameters);
                list = SQLHelper.ToList<ProductDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].AssessType = new ModelTemplate();
                    list[i].AssessType.ID = list[i].AssessID;
                    list[i].AssessType.TextName = list[i].AssessName;
                }
            }
            return list;
        }
        public List<ProductDataTransfer> ReportDailyCompetitorByDatePublishAndCompanyIDToList(DateTime datePublish, int companyID)
        {
            List<ProductDataTransfer> list = new List<ProductDataTransfer>();
            if ((datePublish != null) && (companyID > 0))
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@DatePublish",datePublish),
                new SqlParameter("@CompanyID",companyID)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportDailyCompetitorByDatePublishAndCompanyID", parameters);
                list = SQLHelper.ToList<ProductDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].AssessType = new ModelTemplate();
                    list[i].AssessType.ID = list[i].AssessID;
                    list[i].AssessType.TextName = list[i].AssessName;
                }
            }
            return list;
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
        public List<ProductSearchPropertyDataTransfer> ReportDailyByProductSearchIDAndActiveToListToHTML(int productSearchID, bool active)
        {
            List<ProductSearchPropertyDataTransfer> list = new List<ProductSearchPropertyDataTransfer>();
            if (productSearchID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ProductSearchID",productSearchID),
                    new SqlParameter("@Active",active)
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportDailyByProductSearchIDAndActiveToHTML", parameters);
                list = SQLHelper.ToList<ProductSearchPropertyDataTransfer>(dt);
            }
            return list;
        }
        public List<ProductSearchPropertyDataTransfer> ReportDailyByProductSearchIDToListToHTML(int productSearchID)
        {
            List<ProductSearchPropertyDataTransfer> list = new List<ProductSearchPropertyDataTransfer>();
            if (productSearchID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ProductSearchID",productSearchID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportDailyByProductSearchIDToHTML", parameters);
                list = SQLHelper.ToList<ProductSearchPropertyDataTransfer>(dt);
            }
            return list;
        }
        public List<ProductDataTransfer> GetDataTransferByDatePublishBeginAndDatePublishEndAndIndustryIDToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID)
        {
            List<ProductDataTransfer> list = new List<ProductDataTransfer>();
            if (industryID > 0)
            {
                datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
                datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
                SqlParameter[] parameters =
                       {
                    new SqlParameter("@DatePublishBegin",datePublishBegin),
                    new SqlParameter("@DatePublishEnd",datePublishEnd),
                    new SqlParameter("@IndustryID",industryID)
                    };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportSelectDataTransferByDatePublishBeginAndDatePublishEndAndIndustryID", parameters);
                list = SQLHelper.ToList<ProductDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].ArticleType = new ModelTemplate();
                    list[i].ArticleType.ID = list[i].ArticleTypeID;
                    list[i].ArticleType.TextName = list[i].ArticleTypeName;
                    list[i].Company = new ModelTemplate();
                    list[i].Company.ID = list[i].CompanyID;
                    list[i].Company.TextName = list[i].CompanyName;
                    list[i].AssessType = new ModelTemplate();
                    list[i].AssessType.ID = list[i].AssessID;
                    list[i].AssessType.TextName = list[i].AssessName;
                }
            }
            return list;
        }
        public List<ProductSearchDataTransfer> InitializationByDatePublishBeginAndDatePublishEndAndIndustryIDToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID)
        {
            List<ProductSearchDataTransfer> list = new List<ProductSearchDataTransfer>();
            if (industryID > 0)
            {
                SqlParameter[] parameters =
                {
                new SqlParameter("@DatePublishBegin",datePublishBegin),
                new SqlParameter("@DatePublishEnd",datePublishEnd),
                new SqlParameter("@IndustryID",industryID)
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportDailyInitializationByDatePublishBeginAndDatePublishEndAndIndustryID", parameters);
                list = SQLHelper.ToList<ProductSearchDataTransfer>(dt);
            }
            return list;
        }
        public List<ProductSearchDataTransfer> InitializationByDatePublishBeginAndDatePublishEndAndIndustryIDAndHourSearchToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int hourSearch)
        {
            List<ProductSearchDataTransfer> list = new List<ProductSearchDataTransfer>();
            if (industryID > 0)
            {
                SqlParameter[] parameters =
                {
                new SqlParameter("@DatePublishBegin",datePublishBegin),
                new SqlParameter("@DatePublishEnd",datePublishEnd),
                new SqlParameter("@IndustryID",industryID),
                new SqlParameter("@HourSearch",hourSearch),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportDailyInitializationByDatePublishBeginAndDatePublishEndAndIndustryIDAndHourSearch", parameters);
                list = SQLHelper.ToList<ProductSearchDataTransfer>(dt);
            }
            return list;
        }
        public List<ProductSearchDataTransfer> InitializationByDatePublishBeginAndDatePublishEndAndIndustryIDAndAllDataAndAllSummaryToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, bool allData, bool allSummary, int requestUserID)
        {
            List<ProductSearchDataTransfer> list = new List<ProductSearchDataTransfer>();
            if (industryID > 0)
            {
                SqlParameter[] parameters =
                {
                new SqlParameter("@DatePublishBegin",datePublishBegin),
                new SqlParameter("@DatePublishEnd",datePublishEnd),
                new SqlParameter("@IndustryID",industryID),
                 new SqlParameter("@AllData",allData),
                  new SqlParameter("@AllSummary",allSummary),
                  new SqlParameter("@RequestUserID",requestUserID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportDailyInitializationByDatePublishBeginAndDatePublishEndAndIndustryIDAndAllDataAndAllSummary", parameters);
                list = SQLHelper.ToList<ProductSearchDataTransfer>(dt);
            }
            return list;
        }
        public string UpdateByCompanyIDAndTitleAndProductPropertyIDAndRequestUserID(int companyID, string title, int productPropertyID, int requestUserID)
        {
            string result = "";
            if (companyID > 0)
            {
                SqlParameter[] parameters =
                {
                new SqlParameter("@CompanyID",companyID),
                new SqlParameter("@Title",title),
                new SqlParameter("@ProductPropertyID",productPropertyID),
                  new SqlParameter("@RequestUserID",requestUserID),
                };
                result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ReportDailyUpdateByCompanyIDAndTitleAndProductPropertyIDAndRequestUserID", parameters);

            }
            return result;
        }
        public string UpdateByDatePublishBeginAndDatePublishEndAndIndustryIDAndAllData(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, bool allData, int requestUserID)
        {
            string result = "";
            if (industryID > 0)
            {
                SqlParameter[] parameters =
                {
                new SqlParameter("@DatePublishBegin",datePublishBegin),
                new SqlParameter("@DatePublishEnd",datePublishEnd),
                new SqlParameter("@IndustryID",industryID),
                 new SqlParameter("@AllData",allData),
                  new SqlParameter("@RequestUserID",requestUserID),
                };
                result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ReportDailyUpdateByDatePublishBeginAndDatePublishEndAndIndustryIDAndAllData", parameters);

            }
            return result;
        }
        public string UpdateByDatePublishBeginAndDatePublishEndAndIndustryIDAndAllData001(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, bool allData, int requestUserID)
        {
            string result = "";
            if (industryID > 0)
            {
                SqlParameter[] parameters =
                {
                new SqlParameter("@DatePublishBegin",datePublishBegin),
                new SqlParameter("@DatePublishEnd",datePublishEnd),
                new SqlParameter("@IndustryID",industryID),
                 new SqlParameter("@AllData",allData),
                  new SqlParameter("@RequestUserID",requestUserID),
                };
                result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ReportDailyUpdateByDatePublishBeginAndDatePublishEndAndIndustryIDAndAllData001", parameters);

            }
            return result;
        }
        public List<ProductDataTransfer> GetProductDataTransferByDatePublishBeginAndDatePublishEndAndIndustryIDToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID)
        {
            List<ProductDataTransfer> list = new List<ProductDataTransfer>();
            if (industryID > 0)
            {
                datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
                datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
                SqlParameter[] parameters =
                       {
                    new SqlParameter("@DatePublishBegin",datePublishBegin),
                    new SqlParameter("@DatePublishEnd",datePublishEnd),
                    new SqlParameter("@IndustryID",industryID)
                    };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportSelectProductDataTransferByDatePublishBeginAndDatePublishEndAndIndustryID", parameters);
                list = SQLHelper.ToList<ProductDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].ArticleType = new ModelTemplate();
                    list[i].ArticleType.ID = list[i].ArticleTypeID;
                    list[i].ArticleType.TextName = list[i].ArticleTypeName;
                    list[i].Company = new ModelTemplate();
                    list[i].Company.ID = list[i].CompanyID;
                    list[i].Company.TextName = list[i].CompanyName;
                    list[i].AssessType = new ModelTemplate();
                    list[i].AssessType.ID = list[i].AssessID;
                    list[i].AssessType.TextName = list[i].AssessName;
                    list[i].Segment = new ModelTemplate();
                    list[i].Segment.ID = list[i].SegmentID;
                    list[i].Segment.TextName = list[i].SegmentName;
                    list[i].Product = new ModelTemplate();
                    list[i].Product.ID = list[i].MembershipPermissionProductID;
                    list[i].Product.TextName = list[i].ProductName;
                }
            }
            return list;
        }
        public List<ProductDataTransfer> GetProductDataTransferByDatePublishBeginAndDatePublishEndAndIndustryIDAndIsDailyToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, bool isDaily)
        {
            List<ProductDataTransfer> list = new List<ProductDataTransfer>();
            if (industryID > 0)
            {
                datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
                datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
                SqlParameter[] parameters =
                       {
                    new SqlParameter("@DatePublishBegin",datePublishBegin),
                    new SqlParameter("@DatePublishEnd",datePublishEnd),
                    new SqlParameter("@IndustryID",industryID),
                    new SqlParameter("@IsDaily",isDaily),
                    };
                DataTable dt = dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportSelectProductDataTransferByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndIsDaily", parameters);
                list = SQLHelper.ToList<ProductDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].ArticleType = new ModelTemplate();
                    list[i].ArticleType.ID = list[i].ArticleTypeID;
                    list[i].ArticleType.TextName = list[i].ArticleTypeName;
                    list[i].Company = new ModelTemplate();
                    list[i].Company.ID = list[i].CompanyID;
                    list[i].Company.TextName = list[i].CompanyName;
                    list[i].AssessType = new ModelTemplate();
                    list[i].AssessType.ID = list[i].AssessID;
                    list[i].AssessType.TextName = list[i].AssessName;
                    list[i].Segment = new ModelTemplate();
                    list[i].Segment.ID = list[i].SegmentID;
                    list[i].Segment.TextName = list[i].SegmentName;
                    list[i].Product = new ModelTemplate();
                    list[i].Product.ID = list[i].MembershipPermissionProductID;
                    list[i].Product.TextName = list[i].ProductName;
                }
            }
            return list;
        }
        public List<ProductDataTransfer> GetProductDataTransferByDatePublishBeginAndDatePublishEndAndIndustryIDAndIsDailyAndIsUploadToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, bool isDaily, bool isUpload)
        {            
            List<ProductDataTransfer> list = new List<ProductDataTransfer>();
            if (industryID > 0)
            {
                datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
                datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
                SqlParameter[] parameters =
                       {
                    new SqlParameter("@DatePublishBegin",datePublishBegin),
                    new SqlParameter("@DatePublishEnd",datePublishEnd),
                    new SqlParameter("@IndustryID",industryID),
                    new SqlParameter("@IsDaily",isDaily),
                    };
                DataTable dt = new DataTable();
                if (isUpload == true)
                {
                    dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportSelectProductDataTransferByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndIsDaily", parameters);
                }
                else
                {
                    dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportSelectProductDataTransferByDatePublishBeginAndDatePublishEndAndIndustryIDAndIsDaily", parameters);
                }
                list = SQLHelper.ToList<ProductDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].ArticleType = new ModelTemplate();
                    list[i].ArticleType.ID = list[i].ArticleTypeID;
                    list[i].ArticleType.TextName = list[i].ArticleTypeName;
                    list[i].Company = new ModelTemplate();
                    list[i].Company.ID = list[i].CompanyID;
                    list[i].Company.TextName = list[i].CompanyName;
                    list[i].AssessType = new ModelTemplate();
                    list[i].AssessType.ID = list[i].AssessID;
                    list[i].AssessType.TextName = list[i].AssessName;
                    list[i].Segment = new ModelTemplate();
                    list[i].Segment.ID = list[i].SegmentID;
                    list[i].Segment.TextName = list[i].SegmentName;
                    list[i].Product = new ModelTemplate();
                    list[i].Product.ID = list[i].MembershipPermissionProductID;
                    list[i].Product.TextName = list[i].ProductName;
                }
            }
            return list;
        }
        public ProductDataTransfer GetProductDataTransferByProductPropertyID(int productPropertyID)
        {
            ProductDataTransfer model = new ProductDataTransfer();
            if (productPropertyID > 0)
            {
                SqlParameter[] parameters =
                    {
                    new SqlParameter("@ProductPropertyID",productPropertyID)
                    };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportSelectProductDataTransferByProductPropertyID", parameters);
                model = SQLHelper.ToList<ProductDataTransfer>(dt).FirstOrDefault();
            }
            return model;
        }
        public string UpdateProductPropertyByDatePublishBeginAndDatePublishEndAndIndustryID(DateTime datePublishBegin, DateTime datePublishEnd, int industryID)
        {
            string result = "";
            if (industryID > 0)
            {
                datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
                datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
                SqlParameter[] parameters =
                       {
                    new SqlParameter("@DatePublishBegin",datePublishBegin),
                    new SqlParameter("@DatePublishEnd",datePublishEnd),
                    new SqlParameter("@IndustryID",industryID)
                    };
                result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ReportDailyUpdateProductPropertyByDatePublishBeginAndDatePublishEndAndIndustryID", parameters);
            }
            return result;
        }
        public string UpdateProductByDatePublishBeginAndDatePublishEndAndIndustryID(DateTime datePublishBegin, DateTime datePublishEnd, int industryID)
        {
            string result = "";
            if (industryID > 0)
            {
                datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
                datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
                SqlParameter[] parameters =
                       {
                    new SqlParameter("@DatePublishBegin",datePublishBegin),
                    new SqlParameter("@DatePublishEnd",datePublishEnd),
                    new SqlParameter("@IndustryID",industryID)
                    };
                result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ReportDailyUpdateProductByDatePublishBeginAndDatePublishEndAndIndustryID", parameters);
            }
            return result;
        }
        public ProductSearchDataTransfer GetByProductSearchID(int productSearchID)
        {
            ProductSearchDataTransfer model = new ProductSearchDataTransfer();
            if (productSearchID > 0)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@ProductSearchID",productSearchID),
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportDailyByProductSearchID", parameters);
                model = SQLHelper.ToList<ProductSearchDataTransfer>(dt).FirstOrDefault();
            }
            return model;
        }
        public string DeleteProductAndProductPropertyByDatePublishBeginAndDatePublishEndAndIndustryID(DateTime datePublishBegin, DateTime datePublishEnd, int industryID)
        {
            string result = "";
            if (industryID > 0)
            {
                datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
                datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
                SqlParameter[] parameters =
                       {
                    new SqlParameter("@DatePublishBegin",datePublishBegin),
                    new SqlParameter("@DatePublishEnd",datePublishEnd),
                    new SqlParameter("@IndustryID",industryID)
                    };
                result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ReportDailyDeleteProductAndProductPropertyByDatePublishBeginAndDatePublishEndAndIndustryID", parameters);
            }
            return result;
        }
        public string DeleteProductAndProductPropertyByDatePublishBeginAndDatePublishEndAndIndustryIDAndIsUpload(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, bool isUpload)
        {
            string result = "";
            if (industryID > 0)
            {
                datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
                datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
                SqlParameter[] parameters =
                       {
                    new SqlParameter("@DatePublishBegin",datePublishBegin),
                    new SqlParameter("@DatePublishEnd",datePublishEnd),
                    new SqlParameter("@IndustryID",industryID)
                    };
                if (isUpload == true)
                {
                    result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ReportDailyDeleteProductAndProductPropertyByDateUpdatedBeginAndDateUpdatedEndAndIndustryID", parameters);
                }
                else
                {
                    result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ReportDailyDeleteProductAndProductPropertyByDatePublishBeginAndDatePublishEndAndIndustryID", parameters);
                }
            }
            return result;
        }
        public string DeleteProductSearchAndProductSearchPropertyByDatePublishBeginAndDatePublishEndAndIndustryIDAndHourSearch(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int hourSearch)
        {
            string result = "";
            if (industryID > 0)
            {
                datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
                datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
                SqlParameter[] parameters =
                       {
                    new SqlParameter("@DatePublishBegin",datePublishBegin),
                    new SqlParameter("@DatePublishEnd",datePublishEnd),
                    new SqlParameter("@IndustryID",industryID),
                    new SqlParameter("@HourSearch",hourSearch),
                    };
                result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ReportDailyDeleteProductSearchAndProductSearchPropertyByDatePublishBeginAndDatePublishEndAndIndustryIDAndHourSearch", parameters);
            }
            return result;
        }
        public List<ProductDataTransfer> GetByIDListToList(string iDList)
        {
            List<ProductDataTransfer> list = new List<ProductDataTransfer>();
            if (!string.IsNullOrEmpty(iDList))
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@IDList",iDList),
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductSelectByIDList", parameters);
                list = SQLHelper.ToList<ProductDataTransfer>(dt);
            }
            return list;
        }
        public async Task<List<ProductDataTransfer>> AsyncGetByIDListToList(string iDList)
        {
            List<ProductDataTransfer> list = new List<ProductDataTransfer>();
            if (!string.IsNullOrEmpty(iDList))
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@IDList",iDList),
                };
                DataTable dt = await SQLHelper.FillAsync(AppGlobal.ConectionString, "sp_ProductSelectByIDList", parameters);
                foreach (DataRow row in dt.Rows)
                {
                    ProductDataTransfer item = new ProductDataTransfer();
                    item.AdvertisementValue = row["AdvertisementValue"] == DBNull.Value ? 0 : (int)row["AdvertisementValue"];
                    item.Media = row["Media"] == DBNull.Value ? "" : (string)row["Media"];
                    item.DatePublish = row["DatePublish"] == DBNull.Value ? DateTime.Now : (DateTime)row["DatePublish"];
                    item.Title = row["Title"] == DBNull.Value ? "" : (string)row["Title"];
                    item.TitleEnglish = row["TitleEnglish"] == DBNull.Value ? "" : (string)row["TitleEnglish"];
                    item.Description = row["Description"] == DBNull.Value ? "" : (string)row["Description"];
                    item.URLCode = row["URLCode"] == DBNull.Value ? "" : (string)row["URLCode"];
                    item.Summary = row["Summary"] == DBNull.Value ? "" : (string)row["Summary"];
                    list.Add(item);
                }
            }
            return list;
        }
    }
}
