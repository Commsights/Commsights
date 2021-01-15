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
    public class BaiVietUploadCountRepository : Repository<BaiVietUploadCount>, IBaiVietUploadCountRepository
    {
        private readonly CommsightsContext _context;

        public BaiVietUploadCountRepository(CommsightsContext context) : base(context)
        {
            _context = context;
        }
        public List<BaiVietReport> GetReportByDateBeginAndDateEndToList(DateTime dateBegin, DateTime dateEnd)
        {
            List<BaiVietReport> list = new List<BaiVietReport>();
            try
            {
                dateBegin = new DateTime(dateBegin.Year, dateBegin.Month, dateBegin.Day, 0, 0, 0);
                dateEnd = new DateTime(dateEnd.Year, dateEnd.Month, dateEnd.Day, 23, 59, 59);
                SqlParameter[] parameters =
                {
                    new SqlParameter("@DateBegin",dateBegin),
                    new SqlParameter("@DateEnd",dateEnd),
                    };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_BaiVietUploadReportByDateBeginAndDateEnd", parameters);
                list = SQLHelper.ToList<BaiVietReport>(dt);
            }
            catch (Exception e)
            {
                string mes = e.Message;
            }
            return list;
        }
        public List<BaiVietReport> GetReportByDateBeginAndDateEndAndIndustryIDToList(DateTime dateBegin, DateTime dateEnd, int industryID)
        {
            List<BaiVietReport> list = new List<BaiVietReport>();
            if (industryID > 0)
            {
                try
                {
                    dateBegin = new DateTime(dateBegin.Year, dateBegin.Month, dateBegin.Day, 0, 0, 0);
                    dateEnd = new DateTime(dateEnd.Year, dateEnd.Month, dateEnd.Day, 23, 59, 59);
                    SqlParameter[] parameters =
                    {
                    new SqlParameter("@DateBegin",dateBegin),
                    new SqlParameter("@DateEnd",dateEnd),
                    new SqlParameter("@IndustryID",industryID),
                    };
                    DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_BaiVietUploadReportByDateBeginAndDateEndAndIndustryID", parameters);
                    list = SQLHelper.ToList<BaiVietReport>(dt);
                }
                catch (Exception e)
                {
                    string mes = e.Message;
                }
            }
            return list;
        }
    }
}
