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
