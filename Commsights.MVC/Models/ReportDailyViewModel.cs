using Commsights.Data.DataTransferObject;
using Commsights.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commsights.MVC.Models
{
    public class ReportDailyViewModel
    {
        public ProductSearchDataTransfer ProductSearchDataTransfer { get; set; }
        public List<ProductSearchPropertyDataTransfer> ListProductSearchPropertyDataTransfer { get; set; }       

    }
}
