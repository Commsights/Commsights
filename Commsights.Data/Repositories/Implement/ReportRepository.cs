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
    }
}
