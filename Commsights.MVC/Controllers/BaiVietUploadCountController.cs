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
    public class BaiVietUploadCountController : Controller, IActionFilter
    {
        private readonly IBaiVietUploadCountRepository _baiVietUploadCountRepository;
        public BaiVietUploadCountController(IBaiVietUploadCountRepository baiVietUploadCountRepository)
        {
            _baiVietUploadCountRepository = baiVietUploadCountRepository;
        }

        public ActionResult GetReportByDateBeginAndDateEndToList([DataSourceRequest] DataSourceRequest request, DateTime dateBegin, DateTime dateEnd)
        {
            var data = _baiVietUploadCountRepository.GetReportByDateBeginAndDateEndToList(dateBegin, dateEnd);
            return Json(data.ToDataSourceResult(request));
        }
    }
}
