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
            return _context.ReportMonthly.Where(item => item.Year == year && item.Month == month).OrderBy(item => item.Title).ToList();
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
    }
}
