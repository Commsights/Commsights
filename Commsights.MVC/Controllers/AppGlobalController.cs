using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commsights.Data.Enum;
using Commsights.Data.Helpers;
using Commsights.Data.Models;
using Commsights.Data.Repositories;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Commsights.MVC.Controllers
{
    public class AppGlobalController : Controller, IActionFilter
    {
        private readonly IMembershipAccessHistoryRepository _membershipAccessHistoryRepository;
        public AppGlobalController(IMembershipAccessHistoryRepository membershipAccessHistoryRepository)
        {
            _membershipAccessHistoryRepository = membershipAccessHistoryRepository;
        }
        public ActionResult GetYearFinanceToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = YearFinance.GetAllToList();
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetMonthFinanceToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = MonthFinance.GetAllToList();
            return Json(data.ToDataSourceResult(request));
        }
    }
}
