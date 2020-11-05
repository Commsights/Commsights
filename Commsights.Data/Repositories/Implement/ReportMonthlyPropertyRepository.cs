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
    public class ReportMonthlyPropertyRepository : Repository<ReportMonthlyProperty>, IReportMonthlyPropertyRepository
    {
        private readonly CommsightsContext _context;

        public ReportMonthlyPropertyRepository(CommsightsContext context) : base(context)
        {
            _context = context;
        }
        public List<ReportMonthlyProperty> GetByParentID001ToList(int parentID)
        {
            return _context.ReportMonthlyProperty.Where(item => item.ParentID == parentID).OrderBy(item => item.ProductPropertyID).ToList();
        }
        public List<ReportMonthlyPropertyDataTransfer> GetReportMonthlyPropertyDataTransferByParentIDToList(int parentID)
        {
            List<ReportMonthlyPropertyDataTransfer> list = new List<ReportMonthlyPropertyDataTransfer>();
            if (parentID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ParentID",parentID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportMonthlyPropertySelectReportMonthlyPropertyDataTransferByParentID", parameters);
                list = SQLHelper.ToList<ReportMonthlyPropertyDataTransfer>(dt);
            }
            return list;
        }
    }
}
