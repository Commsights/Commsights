using Commsights.Data.DataTransferObject;
using Commsights.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commsights.MVC.Models
{
    public class ReportMonthlyViewModel
    {        
        public int ID { get; set; }
        public string Title { get; set; }
        public List<ReportMonthlyIndustryDataTransfer> ListReportMonthlyIndustryDataTransfer { get; set; }       

    }
}
