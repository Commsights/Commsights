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
    public class CodeDataRepository : ICodeDataRepository
    {
        public CodeDataRepository()
        {
        }
        public List<CodeData> GetByDatePublishBeginAndDatePublishEndAndIndustryIDToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID)
        {
            List<CodeData> list = new List<CodeData>();
            if (industryID > 0)
            {
                try
                {
                    datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
                    datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
                    SqlParameter[] parameters =
                    {
                new SqlParameter("@DatePublishBegin",datePublishBegin),
                new SqlParameter("@DatePublishEnd",datePublishEnd),
                new SqlParameter("@IndustryID",industryID),
                };
                    DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectByDatePublishBeginAndDatePublishEndAndIndustryID", parameters);
                    list = SQLHelper.ToList<CodeData>(dt);
                }
                catch (Exception e)
                {
                    string mes = e.Message;
                }
            }
            return list;
        }
        public List<CodeData> GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndIsPublishToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int employeeID, bool isPublish)
        {
            List<CodeData> list = new List<CodeData>();
            if (isPublish == true)
            {
                list = GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDToList(datePublishBegin, datePublishEnd, industryID, employeeID);
            }
            else
            {
                list = GetByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndEmployeeIDToList(datePublishBegin, datePublishEnd, industryID, employeeID);
            }
            return list;
        }
        public List<CodeData> GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndIsUploadToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int employeeID, bool isUpload)
        {
            List<CodeData> list = new List<CodeData>();
            if (isUpload == false)
            {
                list = GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDToList(datePublishBegin, datePublishEnd, industryID, employeeID);
            }
            else
            {
                list = GetByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndEmployeeIDToList(datePublishBegin, datePublishEnd, industryID, employeeID);
            }
            return list;
        }
        public List<CodeData> GetByDateUpdatedBeginAndDateUpdatedEndAndHourBeginAndHourEndAndIndustryIDToList(DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int hourBegin, int hourEnd, int industryID)
        {
            List<CodeData> list = new List<CodeData>();
            if (industryID > 0)
            {
                try
                {
                    dateUpdatedBegin = new DateTime(dateUpdatedBegin.Year, dateUpdatedBegin.Month, dateUpdatedBegin.Day, hourBegin, 0, 0);
                    dateUpdatedEnd = new DateTime(dateUpdatedEnd.Year, dateUpdatedEnd.Month, dateUpdatedEnd.Day, hourEnd, 59, 59);
                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@DateUpdatedBegin",dateUpdatedBegin),
                        new SqlParameter("@DateUpdatedEnd",dateUpdatedEnd),
                        new SqlParameter("@IndustryID",industryID),
                    };
                    DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectByDateUpdatedBeginAndDateUpdatedEndAndIndustryID", parameters);
                    list = SQLHelper.ToList<CodeData>(dt);
                }
                catch (Exception e)
                {
                    string mes = e.Message;
                }
            }
            return list;
        }
        public List<CodeData> GetDailyByDateUpdatedBeginAndDateUpdatedEndAndHourBeginAndHourEndAndIndustryIDAndIsCodingToList(DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int hourBegin, int hourEnd, int industryID, bool isCoding)
        {
            List<CodeData> list = new List<CodeData>();
            if (industryID > 0)
            {
                try
                {
                    dateUpdatedBegin = new DateTime(dateUpdatedBegin.Year, dateUpdatedBegin.Month, dateUpdatedBegin.Day, hourBegin, 0, 0);
                    dateUpdatedEnd = new DateTime(dateUpdatedEnd.Year, dateUpdatedEnd.Month, dateUpdatedEnd.Day, hourEnd, 59, 59);
                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@DateUpdatedBegin",dateUpdatedBegin),
                        new SqlParameter("@DateUpdatedEnd",dateUpdatedEnd),
                        new SqlParameter("@IndustryID",industryID),
                        new SqlParameter("@IsCoding",isCoding),
                    };
                    DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataDailySelectByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndIsCoding", parameters);
                    list = SQLHelper.ToList<CodeData>(dt);
                }
                catch (Exception e)
                {
                    string mes = e.Message;
                }
            }
            return list;
        }
        public List<CodeData> GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int employeeID)
        {
            List<CodeData> list = new List<CodeData>();
            if (industryID > 0)
            {
                try
                {
                    datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
                    datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
                    SqlParameter[] parameters =
                    {
                new SqlParameter("@DatePublishBegin",datePublishBegin),
                new SqlParameter("@DatePublishEnd",datePublishEnd),
                new SqlParameter("@IndustryID",industryID),
                new SqlParameter("@EmployeeID",employeeID),
                };
                    DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeID", parameters);
                    list = SQLHelper.ToList<CodeData>(dt);
                }
                catch (Exception e)
                {
                    string mes = e.Message;
                }
            }
            return list;
        }
        public List<CodeData> GetByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndEmployeeIDToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int employeeID)
        {
            List<CodeData> list = new List<CodeData>();
            if (industryID > 0)
            {
                try
                {
                    datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
                    datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
                    SqlParameter[] parameters =
                    {
                new SqlParameter("@DatePublishBegin",datePublishBegin),
                new SqlParameter("@DatePublishEnd",datePublishEnd),
                new SqlParameter("@IndustryID",industryID),
                new SqlParameter("@EmployeeID",employeeID),
                };
                    DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndEmployeeID", parameters);
                    list = SQLHelper.ToList<CodeData>(dt);
                }
                catch (Exception e)
                {
                    string mes = e.Message;
                }
            }
            return list;
        }
        public List<CodeData> GetByDateUpdatedBeginAndDateUpdatedEndAndHourBeginAndHourEndAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysisToList(DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int hourBegin, int hourEnd, int industryID, string companyName, bool isCoding, bool isAnalysis)
        {
            List<CodeData> list = new List<CodeData>();
            if (industryID > 0)
            {
                try
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
                    };
                    DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysis", parameters);
                    list = SQLHelper.ToList<CodeData>(dt);
                }
                catch (Exception e)
                {
                    string mes = e.Message;
                }
            }
            return list;
        }
        public List<CodeDataReport> GetReportByDatePublishBeginAndDatePublishEndAndIsUploadToList(DateTime datePublishBegin, DateTime datePublishEnd, bool isUpload)
        {
            List<CodeDataReport> list = new List<CodeDataReport>();
            if (isUpload == true)
            {
                list = GetReportByDateUpdatedBeginAndDateUpdatedEndToList(datePublishBegin, datePublishEnd);
            }
            else
            {
                list = GetReportByDatePublishBeginAndDatePublishEndToList(datePublishBegin, datePublishEnd);
            }
            return list;
        }
        public List<CodeDataReport> GetReportByDatePublishBeginAndDatePublishEndToList(DateTime datePublishBegin, DateTime datePublishEnd)
        {
            List<CodeDataReport> list = new List<CodeDataReport>();
            try
            {
                datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
                datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
                SqlParameter[] parameters =
                {
                new SqlParameter("@DatePublishBegin",datePublishBegin),
                new SqlParameter("@DatePublishEnd",datePublishEnd),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataReportSelectByDatePublishBeginAndDatePublishEnd", parameters);
                list = SQLHelper.ToList<CodeDataReport>(dt);
            }
            catch (Exception e)
            {
                string mes = e.Message;
            }
            return list;
        }
        public List<CodeDataReport> GetReportByDateUpdatedBeginAndDateUpdatedEndToList(DateTime datePublishBegin, DateTime datePublishEnd)
        {
            List<CodeDataReport> list = new List<CodeDataReport>();
            try
            {
                datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
                datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
                SqlParameter[] parameters =
                {
                new SqlParameter("@DatePublishBegin",datePublishBegin),
                new SqlParameter("@DatePublishEnd",datePublishEnd),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataReportSelectByDateUpdatedBeginAndDateUpdatedEnd", parameters);
                list = SQLHelper.ToList<CodeDataReport>(dt);
            }
            catch (Exception e)
            {
                string mes = e.Message;
            }
            return list;
        }
        public List<CodeData> GetReportByDateUpdatedAndHourAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysisToList(DateTime dateUpdated, int hour, int industryID, string companyName, bool isCoding, bool isAnalysis)
        {
            List<CodeData> list = new List<CodeData>();
            if (dateUpdated != null)
            {
                if (dateUpdated.Year > 2019)
                {
                    if (string.IsNullOrEmpty(companyName))
                    {
                        companyName = "";
                    }
                    SqlParameter[] parameters =
                    {
                    new SqlParameter("@DateUpdated",dateUpdated),
                    new SqlParameter("@Hour",hour),
                    new SqlParameter("@IndustryID",industryID),
                    new SqlParameter("@CompanyName",companyName),
                    new SqlParameter("@IsCoding",isCoding),
                    new SqlParameter("@IsAnalysis",isAnalysis),
                    };
                    DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectByDateUpdatedAndHourAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysis", parameters);
                    list = SQLHelper.ToList<CodeData>(dt);
                }
            }
            return list;
        }
        public List<CodeData> GetReportByDateUpdatedAndHourBeginAndHourEndAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysisToList(DateTime dateUpdated, int hourBegin, int hourEnd, int industryID, string companyName, bool isCoding, bool isAnalysis)
        {
            List<CodeData> list = new List<CodeData>();
            if (dateUpdated != null)
            {
                if (dateUpdated.Year > 2019)
                {
                    if (string.IsNullOrEmpty(companyName))
                    {
                        companyName = "";
                    }
                    SqlParameter[] parameters =
                    {
                    new SqlParameter("@DateUpdated",dateUpdated),
                    new SqlParameter("@HourBegin",hourBegin),
                    new SqlParameter("@HourEnd",hourEnd),
                    new SqlParameter("@IndustryID",industryID),
                    new SqlParameter("@CompanyName",companyName),
                    new SqlParameter("@IsCoding",isCoding),
                    new SqlParameter("@IsAnalysis",isAnalysis),
                    };
                    DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectByDateUpdatedAndHourBeginAndHourEndAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysis", parameters);
                    list = SQLHelper.ToList<CodeData>(dt);
                }
            }
            return list;
        }
        public List<CodeData> GetReportByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndIsCodingToList(DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int industryID, bool isCoding)
        {
            List<CodeData> list = new List<CodeData>();
            if (industryID > 0)
            {
                if ((dateUpdatedBegin.Year > 2019) && (dateUpdatedEnd.Year > 2019))
                {
                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@DateUpdatedBegin",dateUpdatedBegin),
                        new SqlParameter("@DateUpdatedEnd",dateUpdatedEnd),
                        new SqlParameter("@IndustryID",industryID),
                        new SqlParameter("@IsCoding",isCoding),
                    };
                    DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndIsCoding", parameters);
                    list = SQLHelper.ToList<CodeData>(dt);
                }
            }
            return list;
        }
        public List<Membership> GetReportSelectByDatePublishBeginAndDatePublishEnd001ToList(DateTime datePublishBegin, DateTime datePublishEnd)
        {
            List<Membership> list = new List<Membership>();
            try
            {
                datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
                datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
                SqlParameter[] parameters =
                {
                new SqlParameter("@DatePublishBegin",datePublishBegin),
                new SqlParameter("@DatePublishEnd",datePublishEnd),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataReportSelectByDatePublishBeginAndDatePublishEnd001", parameters);
                list = SQLHelper.ToList<Membership>(dt);
            }
            catch (Exception e)
            {
                string mes = e.Message;
            }
            return list;
        }
        public List<Config> GetCategorySubByCategoryMainToList(string categoryMain)
        {
            List<Config> list = new List<Config>();
            SqlParameter[] parameters =
                       {
                new SqlParameter("@categoryMain",categoryMain),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectCategorySubByCategoryMain", parameters);
            list = SQLHelper.ToList<Config>(dt);
            return list;
        }
        public string GetCompanyNameByTitle(string title)
        {
            SqlParameter[] parameters =
                       {
                new SqlParameter("@Title",title),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectCompanyNameByTitle", parameters);
            string result = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    result = result + dt.Rows[i][0].ToString();
                }
                else
                {
                    result = result + " , " + dt.Rows[i][0].ToString();
                }
            }
            return result;
        }
        public string GetCompanyNameByURLCode(string uRLCode)
        {
            SqlParameter[] parameters =
                       {
                new SqlParameter("@URLCode",uRLCode),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectCompanyNameByURLCode", parameters);
            string result = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    result = result + dt.Rows[i][0].ToString();
                }
                else
                {
                    result = result + " , " + dt.Rows[i][0].ToString();
                }
            }
            return result;
        }
        public string GetProductNameByTitle(string title)
        {
            SqlParameter[] parameters =
                       {
                new SqlParameter("@Title",title),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectProductNameByTitle", parameters);
            string result = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    result = result + dt.Rows[i][0].ToString();
                }
                else
                {
                    result = result + " , " + dt.Rows[i][0].ToString();
                }
            }
            return result;
        }
        public string GetProductNameByURLCode(string uRLCode)
        {
            SqlParameter[] parameters =
                       {
                new SqlParameter("@URLCode",uRLCode),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectProductNameByURLCode", parameters);
            string result = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    result = result + dt.Rows[i][0].ToString();
                }
                else
                {
                    result = result + " , " + dt.Rows[i][0].ToString();
                }
            }
            return result;
        }
    }
}
