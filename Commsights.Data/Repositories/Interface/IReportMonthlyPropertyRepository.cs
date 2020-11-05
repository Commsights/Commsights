using Commsights.Data.DataTransferObject;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commsights.Data.Repositories
{
    public interface IReportMonthlyPropertyRepository : IRepository<ReportMonthlyProperty>
    {
        public List<ReportMonthlyProperty> GetByParentID001ToList(int parentID);
        public List<ReportMonthlyPropertyDataTransfer> GetReportMonthlyPropertyDataTransferByParentIDToList(int parentID);
    }
}
