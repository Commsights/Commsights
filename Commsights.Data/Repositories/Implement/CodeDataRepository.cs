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
    }
}
