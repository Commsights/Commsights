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
        public List<CodeData> GetByDateUpdatedBeginAndDateUpdatedEndAndEmployeeIDAndIsFilterToList(DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int employeeID, bool isFilter)
        {
            List<CodeData> list = new List<CodeData>();
            if (employeeID > 0)
            {
                try
                {
                    dateUpdatedBegin = new DateTime(dateUpdatedBegin.Year, dateUpdatedBegin.Month, dateUpdatedBegin.Day, 0, 0, 0);
                    dateUpdatedEnd = new DateTime(dateUpdatedEnd.Year, dateUpdatedEnd.Month, dateUpdatedEnd.Day, 23, 59, 59);
                    SqlParameter[] parameters =
                    {
                new SqlParameter("@DateUpdatedBegin",dateUpdatedBegin),
                new SqlParameter("@DateUpdatedEnd",dateUpdatedEnd),
                new SqlParameter("@EmployeeID",employeeID),
                new SqlParameter("@IsFilter",isFilter),
                };
                    DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectByDateUpdatedBeginAndDateUpdatedEndAndEmployeeIDAndIsFilter", parameters);
                    list = SQLHelper.ToList<CodeData>(dt);
                }
                catch (Exception e)
                {
                    string mes = e.Message;
                }
            }
            return list;
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
                list.AddRange(GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDToList(datePublishBegin, datePublishEnd, industryID, employeeID));
            }
            else
            {
                list.AddRange(GetByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndEmployeeIDToList(datePublishBegin, datePublishEnd, industryID, employeeID));
            }
            return list;
        }
        public List<CodeData> GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndIsUploadAndSourceIsNewspageAndTVToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int employeeID, bool isUpload, string sourceNewspage, string sourceTV)
        {
            List<CodeData> list = new List<CodeData>();
            if (isUpload == false)
            {
                list.AddRange(GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndSourceIsNewspageAndTVToList(datePublishBegin, datePublishEnd, industryID, employeeID, sourceNewspage, sourceTV));
            }
            else
            {
                list.AddRange(GetByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndEmployeeIDAndSourceIsNewspageAndTVToList(datePublishBegin, datePublishEnd, industryID, employeeID, sourceNewspage, sourceTV));
            }
            return list;
        }
        public List<CodeData> GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndIsUploadAndSourceIsNotNewspageAndTVToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int employeeID, bool isUpload, string sourceNewspage, string sourceTV)
        {
            List<CodeData> list = new List<CodeData>();
            if (isUpload == false)
            {
                list.AddRange(GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndSourceIsNotNewspageAndTVToList(datePublishBegin, datePublishEnd, industryID, employeeID, sourceNewspage, sourceTV));
            }
            else
            {
                list.AddRange(GetByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndEmployeeIDAndSourceIsNotNewspageAndTVToList(datePublishBegin, datePublishEnd, industryID, employeeID, sourceNewspage, sourceTV));
            }
            return list;
        }
        public List<CodeData> GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndSourceIsNewspageAndTVToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int employeeID, string sourceNewspage, string sourceTV)
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
                    new SqlParameter("@SourceNewspage",sourceNewspage),
                    new SqlParameter("@SourceTV",sourceTV),
                    };
                    DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndSourceIsNewspageAndTV", parameters);
                    list = SQLHelper.ToList<CodeData>(dt);
                }
                catch (Exception e)
                {
                    string mes = e.Message;
                }
            }
            return list;
        }
        public List<CodeData> GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndSourceIsNewspageAndTVAndIsCodingToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int employeeID, string sourceNewspage, string sourceTV, bool isCoding)
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
                    new SqlParameter("@SourceNewspage",sourceNewspage),
                    new SqlParameter("@SourceTV",sourceTV),
                    new SqlParameter("@IsCoding",isCoding),
                    };
                    DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndSourceIsNewspageAndTVAndIsCoding", parameters);
                    list = SQLHelper.ToList<CodeData>(dt);
                }
                catch (Exception e)
                {
                    string mes = e.Message;
                }
            }
            return list;
        }
        public List<CodeData> GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndSourceIsNotNewspageAndTVToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int employeeID, string sourceNewspage, string sourceTV)
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
                    new SqlParameter("@SourceNewspage",sourceNewspage),
                    new SqlParameter("@SourceTV",sourceTV),
                    };
                    DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndSourceIsNotNewspageAndTV", parameters);
                    list = SQLHelper.ToList<CodeData>(dt);
                }
                catch (Exception e)
                {
                    string mes = e.Message;
                }
            }
            return list;
        }
        public List<CodeData> GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndSourceIsNotNewspageAndTVAndIsCodingToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int employeeID, string sourceNewspage, string sourceTV, bool isCoding)
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
                    new SqlParameter("@SourceNewspage",sourceNewspage),
                    new SqlParameter("@SourceTV",sourceTV),
                    new SqlParameter("@IsCoding",isCoding),
                    };
                    DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndSourceIsNotNewspageAndTVAndIsCoding", parameters);
                    list = SQLHelper.ToList<CodeData>(dt);
                }
                catch (Exception e)
                {
                    string mes = e.Message;
                }
            }
            return list;
        }
        public List<CodeData> GetByDateUpdatedBeginAndDateUpdatedEndAndSourceIsNewspageAndTVToList(DateTime datePublishBegin, DateTime datePublishEnd, string sourceNewspage, string sourceTV)
        {
            List<CodeData> list = new List<CodeData>();
            try
            {
                datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
                datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
                SqlParameter[] parameters =
                {
                    new SqlParameter("@DatePublishBegin",datePublishBegin),
                    new SqlParameter("@DatePublishEnd",datePublishEnd),
                    new SqlParameter("@SourceNewspage",sourceNewspage),
                    new SqlParameter("@SourceTV",sourceTV),
                    };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectByDateUpdatedBeginAndDateUpdatedEndAndSourceIsNewspageAndTV", parameters);
                list = SQLHelper.ToList<CodeData>(dt);
            }
            catch (Exception e)
            {
                string mes = e.Message;
            }
            return list;
        }
        public List<CodeData> GetByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndEmployeeIDAndSourceIsNewspageAndTVToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int employeeID, string sourceNewspage, string sourceTV)
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
                    new SqlParameter("@SourceNewspage",sourceNewspage),
                    new SqlParameter("@SourceTV",sourceTV),
                    };
                    DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndEmployeeIDAndSourceIsNewspageAndTV", parameters);
                    list = SQLHelper.ToList<CodeData>(dt);
                }
                catch (Exception e)
                {
                    string mes = e.Message;
                }
            }
            return list;
        }
        public List<CodeData> GetByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndEmployeeIDAndSourceIsNewspageAndTVAndIsCodingToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int employeeID, string sourceNewspage, string sourceTV, bool isCoding)
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
                    new SqlParameter("@SourceNewspage",sourceNewspage),
                    new SqlParameter("@SourceTV",sourceTV),
                    new SqlParameter("@IsCoding",isCoding),
                    };
                    DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndEmployeeIDAndSourceIsNewspageAndTVAndIsCoding", parameters);
                    list = SQLHelper.ToList<CodeData>(dt);
                }
                catch (Exception e)
                {
                    string mes = e.Message;
                }
            }
            return list;
        }
        public List<CodeData> GetByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndEmployeeIDAndSourceIsNotNewspageAndTVToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int employeeID, string sourceNewspage, string sourceTV)
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
                    new SqlParameter("@SourceNewspage",sourceNewspage),
                    new SqlParameter("@SourceTV",sourceTV),
                    };
                    DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndEmployeeIDAndSourceIsNotNewspageAndTV", parameters);
                    list = SQLHelper.ToList<CodeData>(dt);
                }
                catch (Exception e)
                {
                    string mes = e.Message;
                }
            }
            return list;
        }
        public List<CodeData> GetByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndEmployeeIDAndSourceIsNotNewspageAndTVAndIsCodingToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int employeeID, string sourceNewspage, string sourceTV, bool isCoding)
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
                    new SqlParameter("@SourceNewspage",sourceNewspage),
                    new SqlParameter("@SourceTV",sourceTV),
                    new SqlParameter("@IsCoding",isCoding),
                    };
                    DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndEmployeeIDAndSourceIsNotNewspageAndTVAndIsCoding", parameters);
                    list = SQLHelper.ToList<CodeData>(dt);
                }
                catch (Exception e)
                {
                    string mes = e.Message;
                }
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
        public CodeData GetByProductPropertyID(int productPropertyID)
        {
            CodeData model = new CodeData();
            if (productPropertyID > 0)
            {
                try
                {

                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@ProductPropertyID",productPropertyID),
                    };
                    DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectByProductPropertyID", parameters);
                    model = SQLHelper.ToList<CodeData>(dt).FirstOrDefault();
                }
                catch (Exception e)
                {
                    string mes = e.Message;
                }
            }
            return model;
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
        public List<CodeData> GetDailyByDateUpdatedBeginAndDateUpdatedEndAndHourBeginAndHourEndAndIndustryIDToList(DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int hourBegin, int hourEnd, int industryID)
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
                    DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataDailySelectByDateUpdatedBeginAndDateUpdatedEndAndIndustryID", parameters);
                    list = SQLHelper.ToList<CodeData>(dt);
                }
                catch (Exception e)
                {
                    string mes = e.Message;
                }
            }
            return list;
        }
        public List<CodeData> GetDailyByDatePublishBeginAndDatePublishEndAndHourBeginAndHourEndAndIndustryIDToList(DateTime dateBegin, DateTime dateEnd, int hourBegin, int hourEnd, int industryID)
        {
            List<CodeData> list = new List<CodeData>();
            if (industryID > 0)
            {
                try
                {
                    dateBegin = new DateTime(dateBegin.Year, dateBegin.Month, dateBegin.Day, hourBegin, 0, 0);
                    dateEnd = new DateTime(dateEnd.Year, dateEnd.Month, dateEnd.Day, hourEnd, 59, 59);
                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@DatePublishBegin",dateBegin),
                        new SqlParameter("@DatePublishEnd",dateEnd),
                        new SqlParameter("@IndustryID",industryID),
                    };
                    DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataDailySelectByDatePublishBeginAndDatePublishEndAndIndustryID", parameters);
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
        public List<CodeData> GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndIsCodingToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int employeeID, bool isCoding)
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
                        new SqlParameter("@IsCoding",isCoding),
                    };
                    DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndIsCoding", parameters);
                    list = SQLHelper.ToList<CodeData>(dt);
                }
                catch (Exception e)
                {
                    string mes = e.Message;
                }
            }
            return list;
        }
        public List<CodeData> GetByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndEmployeeIDAndIsCodingToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int employeeID, bool isCoding)
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
                        new SqlParameter("@IsCoding",isCoding),
                    };
                    DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndEmployeeIDAndIsCoding", parameters);
                    list = SQLHelper.ToList<CodeData>(dt);
                }
                catch (Exception e)
                {
                    string mes = e.Message;
                }
            }
            return list;
        }
        public List<CodeData> GetByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndEmployeeIDAndIsCoding001ToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int employeeID, bool isCoding)
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
                        new SqlParameter("@IsCoding",isCoding),
                    };
                    DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndEmployeeIDAndIsCoding001", parameters);
                    list = SQLHelper.ToList<CodeData>(dt);
                }
                catch (Exception e)
                {
                    string mes = e.Message;
                }
            }
            return list;
        }
        public List<CodeData> GetByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndEmployeeID001ToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int employeeID)
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
                    DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndEmployeeID001", parameters);
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
        public List<CodeData> GetByDateUpdatedBeginAndDateUpdatedEndAndHourBeginAndHourEndAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysisAndIsUploadToList(DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int hourBegin, int hourEnd, int industryID, string companyName, bool isCoding, bool isAnalysis, bool isUpload)
        {
            List<CodeData> list = new List<CodeData>();
            if (isUpload == true)
            {
                list = GetByDateUpdatedBeginAndDateUpdatedEndAndHourBeginAndHourEndAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysisToList(dateUpdatedBegin, dateUpdatedEnd, hourBegin, hourEnd, industryID, companyName, isCoding, isAnalysis);
            }
            else
            {
                list = GetByDatePublishBeginAndDatePublishEndAndHourBeginAndHourEndAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysisToList(dateUpdatedBegin, dateUpdatedEnd, hourBegin, hourEnd, industryID, companyName, isCoding, isAnalysis);
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
        public List<CodeData> GetByDatePublishBeginAndDatePublishEndAndHourBeginAndHourEndAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysisToList(DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int hourBegin, int hourEnd, int industryID, string companyName, bool isCoding, bool isAnalysis)
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
                        new SqlParameter("@DatePublishBegin",dateUpdatedBegin),
                        new SqlParameter("@DatePublishEnd",dateUpdatedEnd),
                        new SqlParameter("@IndustryID",industryID),
                        new SqlParameter("@CompanyName",companyName),
                        new SqlParameter("@IsCoding",isCoding),
                        new SqlParameter("@IsAnalysis",isAnalysis),
                    };
                    DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectByDatePublishBeginAndDatePublishEndAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysis", parameters);
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
        public List<CodeDataReport> GetReportByDatePublishBeginAndDatePublishEndAndIndustryIDAndIsUploadToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, bool isUpload)
        {
            List<CodeDataReport> list = new List<CodeDataReport>();
            if (isUpload == true)
            {
                list = GetReportByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDToList(datePublishBegin, datePublishEnd, industryID);
            }
            else
            {
                list = GetReportByDatePublishBeginAndDatePublishEndAndIndustryIDToList(datePublishBegin, datePublishEnd, industryID);
            }
            return list;
        }
        public List<CodeDataReport> GetReportByDatePublishBeginAndDatePublishEndAndIndustryIDToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID)
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
                new SqlParameter("@IndustryID",industryID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataReportSelectByDatePublishBeginAndDatePublishEndAndIndustryID", parameters);
                list = SQLHelper.ToList<CodeDataReport>(dt);
            }
            catch (Exception e)
            {
                string mes = e.Message;
            }
            return list;
        }
        public List<CodeDataReport> GetReportByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID)
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
                new SqlParameter("@IndustryID",industryID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataReportSelectByDateUpdatedBeginAndDateUpdatedEndAndIndustryID", parameters);
                list = SQLHelper.ToList<CodeDataReport>(dt);
            }
            catch (Exception e)
            {
                string mes = e.Message;
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
        public string GetCategorySubByURLCode(string uRLCode)
        {
            SqlParameter[] parameters =
                       {
                new SqlParameter("@URLCode",uRLCode),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataSelectCategorySubByURLCode", parameters);
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

        public List<CodeDataReport> GetReportEmployeeByDateUpdatedBeginAndDateUpdatedEndToList(DateTime dateUpdatedBegin, DateTime dateUpdatedEnd)
        {
            List<CodeDataReport> list = new List<CodeDataReport>();
            try
            {
                dateUpdatedBegin = new DateTime(dateUpdatedBegin.Year, dateUpdatedBegin.Month, dateUpdatedBegin.Day, 0, 0, 0);
                dateUpdatedEnd = new DateTime(dateUpdatedEnd.Year, dateUpdatedEnd.Month, dateUpdatedEnd.Day, 23, 59, 59);
                SqlParameter[] parameters =
                {
                new SqlParameter("@DateUpdatedBegin",dateUpdatedBegin),
                new SqlParameter("@DateUpdatedEnd",dateUpdatedEnd),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataReportSelectEmployeeByDateUpdatedBeginAndDateUpdatedEnd", parameters);
                list = SQLHelper.ToList<CodeDataReport>(dt);
            }
            catch (Exception e)
            {
                string mes = e.Message;
            }
            return list;
        }
        public List<CodeDataReport> GetReportIndustryByDateUpdatedBeginAndDateUpdatedEndAndEmployeeIDToList(DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int employeeID)
        {
            List<CodeDataReport> list = new List<CodeDataReport>();
            try
            {
                dateUpdatedBegin = new DateTime(dateUpdatedBegin.Year, dateUpdatedBegin.Month, dateUpdatedBegin.Day, 0, 0, 0);
                dateUpdatedEnd = new DateTime(dateUpdatedEnd.Year, dateUpdatedEnd.Month, dateUpdatedEnd.Day, 23, 59, 59);
                SqlParameter[] parameters =
                {
                new SqlParameter("@DateUpdatedBegin",dateUpdatedBegin),
                new SqlParameter("@DateUpdatedEnd",dateUpdatedEnd),
                new SqlParameter("@EmployeeID",employeeID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataReportSelectIndustryByDateUpdatedBeginAndDateUpdatedEndAndEmployeeID", parameters);
                list = SQLHelper.ToList<CodeDataReport>(dt);
            }
            catch (Exception e)
            {
                string mes = e.Message;
            }
            return list;
        }
        public List<CodeDataReport> GetReportCompanyNameByDateUpdatedBeginAndDateUpdatedEndAndEmployeeIDToList(DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int employeeID)
        {
            List<CodeDataReport> list = new List<CodeDataReport>();
            try
            {
                dateUpdatedBegin = new DateTime(dateUpdatedBegin.Year, dateUpdatedBegin.Month, dateUpdatedBegin.Day, 0, 0, 0);
                dateUpdatedEnd = new DateTime(dateUpdatedEnd.Year, dateUpdatedEnd.Month, dateUpdatedEnd.Day, 23, 59, 59);
                SqlParameter[] parameters =
                {
                new SqlParameter("@DateUpdatedBegin",dateUpdatedBegin),
                new SqlParameter("@DateUpdatedEnd",dateUpdatedEnd),
                new SqlParameter("@EmployeeID",employeeID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataReportSelectCompanyNameByDateUpdatedBeginAndDateUpdatedEndAndEmployeeID", parameters);
                list = SQLHelper.ToList<CodeDataReport>(dt);
            }
            catch (Exception e)
            {
                string mes = e.Message;
            }
            return list;
        }
        public List<CodeDataReport> GetReportCompanyNameByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDToList(DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int industryID)
        {
            List<CodeDataReport> list = new List<CodeDataReport>();
            try
            {
                dateUpdatedBegin = new DateTime(dateUpdatedBegin.Year, dateUpdatedBegin.Month, dateUpdatedBegin.Day, 0, 0, 0);
                dateUpdatedEnd = new DateTime(dateUpdatedEnd.Year, dateUpdatedEnd.Month, dateUpdatedEnd.Day, 23, 59, 59);
                SqlParameter[] parameters =
                {
                new SqlParameter("@DateUpdatedBegin",dateUpdatedBegin),
                new SqlParameter("@DateUpdatedEnd",dateUpdatedEnd),
                new SqlParameter("@IndustryID",industryID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataReportSelectCompanyNameByDateUpdatedBeginAndDateUpdatedEndAndIndustryID", parameters);
                list = SQLHelper.ToList<CodeDataReport>(dt);
            }
            catch (Exception e)
            {
                string mes = e.Message;
            }
            return list;
        }
        public List<CodeDataReport> GetReportCompanyNameByDateUpdatedBeginAndDateUpdatedEndAndEmployeeIDAndIndustryIDToList(DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int employeeID, int industryID)
        {
            List<CodeDataReport> list = new List<CodeDataReport>();
            try
            {
                dateUpdatedBegin = new DateTime(dateUpdatedBegin.Year, dateUpdatedBegin.Month, dateUpdatedBegin.Day, 0, 0, 0);
                dateUpdatedEnd = new DateTime(dateUpdatedEnd.Year, dateUpdatedEnd.Month, dateUpdatedEnd.Day, 23, 59, 59);
                SqlParameter[] parameters =
                {
                new SqlParameter("@DateUpdatedBegin",dateUpdatedBegin),
                new SqlParameter("@DateUpdatedEnd",dateUpdatedEnd),
                new SqlParameter("@EmployeeID",employeeID),
                new SqlParameter("@IndustryID",industryID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_CodeDataReportSelectCompanyNameByDateUpdatedBeginAndDateUpdatedEndAndEmployeeIDAndIndustryID", parameters);
                list = SQLHelper.ToList<CodeDataReport>(dt);
            }
            catch (Exception e)
            {
                string mes = e.Message;
            }
            return list;
        }
    }
}
