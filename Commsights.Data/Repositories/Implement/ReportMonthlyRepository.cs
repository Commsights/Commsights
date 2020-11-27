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
    public class ReportMonthlyRepository : Repository<ReportMonthly>, IReportMonthlyRepository
    {
        private readonly CommsightsContext _context;

        public ReportMonthlyRepository(CommsightsContext context) : base(context)
        {
            _context = context;
        }
        public List<ReportMonthly> GetByYearAndMonthToList(int year, int month)
        {
            return _context.ReportMonthly.Where(item => item.Year == year && item.Month == month).OrderByDescending(item => item.DateUpdated).ToList();
        }
        public string DeleteByID(int ID)
        {
            string result = "";
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ReportMonthlyDeleteByID", parameters);

            }
            return result;
        }
        public string InsertItemsByReportMonthlyID(DataTable table, int reportMonthlyID)
        {
            string result = "";
            if (reportMonthlyID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@table",table),
                    new SqlParameter("@ReportMonthlyID",reportMonthlyID),
                };
                result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ProductPropertyInsertItemsByReportMonthlyID", parameters);
            }
            return result;
        }
        public string InsertItemsByDataTableAndReportMonthlyIDAndRequestUserID(DataTable table, int reportMonthlyID, int RequestUserID)
        {
            string result = "";
            if (reportMonthlyID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@table",table),
                    new SqlParameter("@ReportMonthlyID",reportMonthlyID),
                    new SqlParameter("@RequestUserID",RequestUserID),
                };
                result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ProductPropertyInsertItemsByDataTableAndReportMonthlyIDAndRequestUserID", parameters);
            }
            return result;
        }
        public string InsertItemsByProductProperty005AndReportMonthlyIDAndRequestUserID(DataTable table, int reportMonthlyID, int RequestUserID)
        {
            string result = "";
            if (reportMonthlyID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@table",table),
                    new SqlParameter("@ReportMonthlyID",reportMonthlyID),
                    new SqlParameter("@RequestUserID",RequestUserID),
                };
                result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ProductPropertyInsertItemsByProductProperty005AndReportMonthlyIDAndRequestUserID", parameters);
            }
            return result;
        }
        public List<ReportMonthlyIndustryDataTransfer> GetIndustryByIDToList(int ID)
        {
            List<ReportMonthlyIndustryDataTransfer> list = new List<ReportMonthlyIndustryDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectIndustryByID", parameters);
                list = SQLHelper.ToList<ReportMonthlyIndustryDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlyIndustryDataTransfer> GetIndustryByIDWithoutSUMToList(int ID)
        {
            List<ReportMonthlyIndustryDataTransfer> list = new List<ReportMonthlyIndustryDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectIndustryWithoutSUMByID", parameters);
                list = SQLHelper.ToList<ReportMonthlyIndustryDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlyIndustryDataTransfer> GetIndustryByID001ToList(int ID)
        {
            List<ReportMonthlyIndustryDataTransfer> list = new List<ReportMonthlyIndustryDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectIndustry001ByID", parameters);
                list = SQLHelper.ToList<ReportMonthlyIndustryDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlyIndustryDataTransfer> GetIndustryByID001WithoutSUMToList(int ID)
        {
            List<ReportMonthlyIndustryDataTransfer> list = new List<ReportMonthlyIndustryDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectIndustry001WithoutSUMByID", parameters);
                list = SQLHelper.ToList<ReportMonthlyIndustryDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlyIndustryDataTransfer> GetCompanyByIDToList(int ID)
        {
            List<ReportMonthlyIndustryDataTransfer> list = new List<ReportMonthlyIndustryDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectCompanyByID", parameters);
                list = SQLHelper.ToList<ReportMonthlyIndustryDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlyIndustryDataTransfer> GetFeatureIndustryByIDToList(int ID)
        {
            List<ReportMonthlyIndustryDataTransfer> list = new List<ReportMonthlyIndustryDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectFeatureIndustryByID", parameters);
                list = SQLHelper.ToList<ReportMonthlyIndustryDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlyIndustryDataTransfer> GetFeatureIndustryWithoutSUMByIDToList(int ID)
        {
            List<ReportMonthlyIndustryDataTransfer> list = new List<ReportMonthlyIndustryDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectFeatureIndustryWithoutSUMByID", parameters);
                list = SQLHelper.ToList<ReportMonthlyIndustryDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlySentimentDataTransfer> GetSentimentByIDToList(int ID)
        {
            List<ReportMonthlySentimentDataTransfer> list = new List<ReportMonthlySentimentDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectSentimentByID", parameters);
                list = SQLHelper.ToList<ReportMonthlySentimentDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlySentimentDataTransfer> GetSentimentByIDWithoutSUMToList(int ID)
        {
            List<ReportMonthlySentimentDataTransfer> list = new List<ReportMonthlySentimentDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectSentiment001ByID", parameters);
                list = SQLHelper.ToList<ReportMonthlySentimentDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlySentimentDataTransfer> GetSentimentAndFeatureWithoutSUMByIDToList(int ID)
        {
            List<ReportMonthlySentimentDataTransfer> list = new List<ReportMonthlySentimentDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectSentimentAndFeatureWithoutSUMByID", parameters);
                list = SQLHelper.ToList<ReportMonthlySentimentDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlySentimentDataTransfer> GetSentimentAndMediaTypeWithoutSUMByIDToList(int ID)
        {
            List<ReportMonthlySentimentDataTransfer> list = new List<ReportMonthlySentimentDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectSentimentAndMediaTypeWithoutSUMByID", parameters);
                list = SQLHelper.ToList<ReportMonthlySentimentDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlyChannelDataTransfer> GetChannelByIDToList(int ID)
        {
            List<ReportMonthlyChannelDataTransfer> list = new List<ReportMonthlyChannelDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectChannelByID", parameters);
                list = SQLHelper.ToList<ReportMonthlyChannelDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlyChannelDataTransfer> GetChannelByIDWithoutSUMToList(int ID)
        {
            List<ReportMonthlyChannelDataTransfer> list = new List<ReportMonthlyChannelDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectChannelWithoutSUMByID", parameters);
                list = SQLHelper.ToList<ReportMonthlyChannelDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlyChannelDataTransfer> GetChannelAndFeatureByIDToList(int ID)
        {
            List<ReportMonthlyChannelDataTransfer> list = new List<ReportMonthlyChannelDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectChannelAndFeatureByID", parameters);
                list = SQLHelper.ToList<ReportMonthlyChannelDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlyChannelDataTransfer> GetChannelAndFeatureWithoutSUMByIDToList(int ID)
        {
            List<ReportMonthlyChannelDataTransfer> list = new List<ReportMonthlyChannelDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectChannelAndFeatureWithoutSUMByID", parameters);
                list = SQLHelper.ToList<ReportMonthlyChannelDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlyChannelDataTransfer> GetChannelAndMentionByIDToList(int ID)
        {
            List<ReportMonthlyChannelDataTransfer> list = new List<ReportMonthlyChannelDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectChannelAndMentionByID", parameters);
                list = SQLHelper.ToList<ReportMonthlyChannelDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlyChannelDataTransfer> GetChannelAndMentionWithoutSUMByIDToList(int ID)
        {
            List<ReportMonthlyChannelDataTransfer> list = new List<ReportMonthlyChannelDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectChannelAndMentionWithoutSUMByID", parameters);
                list = SQLHelper.ToList<ReportMonthlyChannelDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlyTierCommsightsDataTransfer> GetTierCommsightsByIDToList(int ID)
        {
            List<ReportMonthlyTierCommsightsDataTransfer> list = new List<ReportMonthlyTierCommsightsDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectTierCommsightsByID", parameters);
                list = SQLHelper.ToList<ReportMonthlyTierCommsightsDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlyTierCommsightsDataTransfer> GetTierCommsightsWithoutSUMByIDToList(int ID)
        {
            List<ReportMonthlyTierCommsightsDataTransfer> list = new List<ReportMonthlyTierCommsightsDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectTierCommsightsWithoutSUMByID", parameters);
                list = SQLHelper.ToList<ReportMonthlyTierCommsightsDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlyTierCommsightsAndCompanyNameDataTransfer> GetTierCommsightsAndCompanyNameDistinctByIDToList(int ID)
        {
            List<ReportMonthlyTierCommsightsAndCompanyNameDataTransfer> list = new List<ReportMonthlyTierCommsightsAndCompanyNameDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectTierCommsightsAndCompanyNameDistinctByID", parameters);
                list = SQLHelper.ToList<ReportMonthlyTierCommsightsAndCompanyNameDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlyTierCommsightsAndCompanyNameDataTransfer> GetTierCommsightsAndCompanyNameAndPortalByIDToList(int ID)
        {
            List<ReportMonthlyTierCommsightsAndCompanyNameDataTransfer> list = new List<ReportMonthlyTierCommsightsAndCompanyNameDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectTierCommsightsAndCompanyNameAndPortalByID", parameters);
                list = SQLHelper.ToList<ReportMonthlyTierCommsightsAndCompanyNameDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlyTierCommsightsAndCompanyNameDataTransfer> GetTierCommsightsAndCompanyNameAndOtherByIDToList(int ID)
        {
            List<ReportMonthlyTierCommsightsAndCompanyNameDataTransfer> list = new List<ReportMonthlyTierCommsightsAndCompanyNameDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectTierCommsightsAndCompanyNameAndOtherByID", parameters);
                list = SQLHelper.ToList<ReportMonthlyTierCommsightsAndCompanyNameDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlyTierCommsightsAndCompanyNameDataTransfer> GetTierCommsightsAndCompanyNameAndMassByIDToList(int ID)
        {
            List<ReportMonthlyTierCommsightsAndCompanyNameDataTransfer> list = new List<ReportMonthlyTierCommsightsAndCompanyNameDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectTierCommsightsAndCompanyNameAndMassByID", parameters);
                list = SQLHelper.ToList<ReportMonthlyTierCommsightsAndCompanyNameDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlyTierCommsightsAndCompanyNameDataTransfer> GetTierCommsightsAndCompanyNameAndIndustryByIDToList(int ID)
        {
            List<ReportMonthlyTierCommsightsAndCompanyNameDataTransfer> list = new List<ReportMonthlyTierCommsightsAndCompanyNameDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectTierCommsightsAndCompanyNameAndIndustryByID", parameters);
                list = SQLHelper.ToList<ReportMonthlyTierCommsightsAndCompanyNameDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlyCompanyAndYearDataTransfer> GetCompanyAndYearByIDToList(int ID)
        {
            List<ReportMonthlyCompanyAndYearDataTransfer> list = new List<ReportMonthlyCompanyAndYearDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectCompanyAndYearByID", parameters);
                list = SQLHelper.ToList<ReportMonthlyCompanyAndYearDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlyCompanyAndYearDataTransfer> GetCompanyAndYearWithoutSUMByIDToList(int ID)
        {
            List<ReportMonthlyCompanyAndYearDataTransfer> list = new List<ReportMonthlyCompanyAndYearDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectCompanyAndYearWithoutSUMByID", parameters);
                list = SQLHelper.ToList<ReportMonthlyCompanyAndYearDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlySegmentProductDataTransfer> GetSegmentProductByIDToList(int ID)
        {
            List<ReportMonthlySegmentProductDataTransfer> list = new List<ReportMonthlySegmentProductDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectSegmentProductByID", parameters);
                list = SQLHelper.ToList<ReportMonthlySegmentProductDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlySegmentProductDataTransfer> GetSegmentProductWithoutSUMByIDToList(int ID)
        {
            List<ReportMonthlySegmentProductDataTransfer> list = new List<ReportMonthlySegmentProductDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectSegmentProductWithoutSUMByID", parameters);
                list = SQLHelper.ToList<ReportMonthlySegmentProductDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlyTrendLineDataTransfer> GetTrendLineWithoutSUMByIDToList(int ID)
        {
            List<ReportMonthlyTrendLineDataTransfer> list = new List<ReportMonthlyTrendLineDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectTrendLineByID", parameters);
                list = SQLHelper.ToList<ReportMonthlyTrendLineDataTransfer>(dt);
            }
            return list;
        }
        public List<ReportMonthlyTrendLineDataTransfer> GetTrendLineDistinctCompanyNameByIDToList(int ID)
        {
            List<ReportMonthlyTrendLineDataTransfer> list = new List<ReportMonthlyTrendLineDataTransfer>();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlySelectTrendLineDistinctCompanyNameByID", parameters);
                list = SQLHelper.ToList<ReportMonthlyTrendLineDataTransfer>(dt);
            }
            return list;
        }
    }
}
