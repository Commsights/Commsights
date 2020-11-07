using Commsights.Data.DataTransferObject;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Commsights.Data.Repositories
{
    public interface IReportMonthlyRepository : IRepository<ReportMonthly>
    {
        public string DeleteByID(int ID);
        public string InsertItemsByReportMonthlyID(DataTable table, int reportMonthlyID);
        public List<ReportMonthly> GetByYearAndMonthToList(int year, int month);
        public List<ReportMonthlyIndustryDataTransfer> GetIndustryByIDToList(int ID);
        public List<ReportMonthlyIndustryDataTransfer> GetIndustryByIDWithoutSUMToList(int ID);
        public List<ReportMonthlyIndustryDataTransfer> GetIndustryByID001ToList(int ID);
        public List<ReportMonthlyIndustryDataTransfer> GetIndustryByID001WithoutSUMToList(int ID);
        public List<ReportMonthlyIndustryDataTransfer> GetCompanyByIDToList(int ID);
        public List<ReportMonthlyIndustryDataTransfer> GetFeatureIndustryByIDToList(int ID);
        public List<ReportMonthlyIndustryDataTransfer> GetFeatureIndustryWithoutSUMByIDToList(int ID);
    }
}
