using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Commsights.Data.Repositories;
using Commsights.Data.Models;
using Commsights.Data.Helpers;
using Commsights.Data.Enum;
using System.Xml;
using System.Text;
using Commsights.MVC.Models;
using System.Net;
using System.IO;
using Commsights.Data.DataTransferObject;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System.Diagnostics.Eventing.Reader;
using Commsights.Service.Mail;
using System.Drawing;
using Microsoft.AspNetCore.Http;

namespace Commsights.MVC.Controllers
{
    public class CodeDataController : BaseController
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ICodeDataRepository _codeDataRepository;
        public CodeDataController(IWebHostEnvironment hostingEnvironment, ICodeDataRepository codeDataRepository, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _codeDataRepository = codeDataRepository;
        }
        public IActionResult Data()
        {
            BaseViewModel model = new BaseViewModel();
            model.DatePublishBegin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            model.DatePublishEnd = DateTime.Now;
            model.IndustryID = AppGlobal.IndustryID;
            return View(model);
        }
        public IActionResult Company()
        {
            return View();
        }
        public IActionResult Detail(int rowIndex)
        {
            CodeData model = new CodeData();
            DateTime datePublishBegin = DateTime.Now;
            DateTime datePublishEnd = DateTime.Now;
            int industryID = 0;
            try
            {
                industryID = int.Parse(Request.Cookies["CodeDataIndustryID"]);
                datePublishBegin = DateTime.Parse(Request.Cookies["CodeDataDatePublishBegin"]);
                datePublishEnd = DateTime.Parse(Request.Cookies["CodeDataDatePublishEnd"]);
            }
            catch
            {

            }
            List<CodeData> list = _codeDataRepository.GetByDatePublishBeginAndDatePublishEndAndIndustryIDToList(datePublishBegin, datePublishEnd, industryID);
            if ((list.Count > 0) && (rowIndex < list.Count))
            {
                model = list[rowIndex];
                model.RowCount = list.Count;
                model.RowLast = model.RowCount - 1;
                model.RowBack = rowIndex - 1;
                model.RowCurrent = rowIndex + 1;
            }
            return View(model);
        }
        public ActionResult GetByDatePublishBeginAndDatePublishEndAndIndustryIDToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, int industryID)
        {
            var CookieExpires = new CookieOptions();
            CookieExpires.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("CodeDataIndustryID", industryID.ToString(), CookieExpires);
            Response.Cookies.Append("CodeDataDatePublishBegin", datePublishBegin.ToString("MM/dd/yyyy"), CookieExpires);
            Response.Cookies.Append("CodeDataDatePublishEnd", datePublishEnd.ToString("MM/dd/yyyy"), CookieExpires);
            var data = _codeDataRepository.GetByDatePublishBeginAndDatePublishEndAndIndustryIDToList(datePublishBegin, datePublishEnd, industryID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetCategorySubByCategoryMainToList([DataSourceRequest] DataSourceRequest request, string categoryMain)
        {
            var data = _codeDataRepository.GetCategorySubByCategoryMainToList(categoryMain);
            return Json(data.ToDataSourceResult(request));
        }
    }
}
