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
        public ActionResult GetHourFinanceToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = HourFinance.GetAllToList();
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetSEOToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = SOE.GetAllToList();
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetCorpCopyToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = CorpCopy.GetAllToList();
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetCodeDataValueToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = CodeDataValue.GetAllToList();
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetCodeDataValueItem0ToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = CodeDataValue.GetItem0ToList();
            return Json(data.ToDataSourceResult(request));
        }
    }
}
