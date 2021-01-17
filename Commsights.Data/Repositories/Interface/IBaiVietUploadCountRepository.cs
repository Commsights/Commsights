using Commsights.Data.DataTransferObject;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commsights.Data.Repositories
{
    public interface IBaiVietUploadCountRepository : IRepository<BaiVietUploadCount>
    {
        public List<BaiVietReport> GetReportByDateBeginAndDateEndToList(DateTime dateBegin, DateTime dateEnd);
        public List<BaiVietReport> GetReportByDateBeginAndDateEndAndIndustryIDToList(DateTime dateBegin, DateTime dateEnd, int industryID);
    }
}
