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
        private readonly IProductRepository _productRepository;
        private readonly IProductPropertyRepository _productPropertyRepository;
        public CodeDataController(IWebHostEnvironment hostingEnvironment, ICodeDataRepository codeDataRepository, IProductRepository productRepository, IProductPropertyRepository productPropertyRepository, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _codeDataRepository = codeDataRepository;
            _productRepository = productRepository;
            _productPropertyRepository = productPropertyRepository;
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
            CodeData model = GetCodeData(rowIndex);
            return View(model);
        }
        public IActionResult DetailBasic(int rowIndex)
        {
            CodeData model = GetCodeData(rowIndex);
            return View(model);
        }
        public ActionResult GetByDatePublishBeginAndDatePublishEndAndIndustryIDToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, int industryID)
        {
            var CookieExpires = new CookieOptions();
            CookieExpires.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("CodeDataIndustryID", industryID.ToString(), CookieExpires);
            Response.Cookies.Append("CodeDataDatePublishBegin", datePublishBegin.ToString("MM/dd/yyyy"), CookieExpires);
            Response.Cookies.Append("CodeDataDatePublishEnd", datePublishEnd.ToString("MM/dd/yyyy"), CookieExpires);
            List<CodeData> list = _codeDataRepository.GetByDatePublishBeginAndDatePublishEndAndIndustryIDToList(datePublishBegin, datePublishEnd, industryID);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetCategorySubByCategoryMainToList([DataSourceRequest] DataSourceRequest request, string categoryMain)
        {
            var data = _codeDataRepository.GetCategorySubByCategoryMainToList(categoryMain);
            return Json(data.ToDataSourceResult(request));
        }
        public IActionResult SaveCoding(CodeData model)
        {
            _productRepository.UpdateSingleItemByCodeData(model);
            _productPropertyRepository.UpdateSingleItemByCodeData(model);
            return RedirectToAction("Detail", "CodeData", new { RowIndex = model.RowIndex });
        }
        public IActionResult SaveCodingDetailBasic(CodeData model)
        {
            _productRepository.UpdateSingleItemByCodeData(model);
            _productPropertyRepository.UpdateSingleItemByCodeData(model);
            return RedirectToAction("DetailBasic", "CodeData", new { RowIndex = model.RowIndex });
        }
        public IActionResult Copy(int rowIndex)
        {
            CodeData model = GetCodeData(rowIndex);
            ProductProperty productProperty = _productPropertyRepository.GetByID001(model.ProductPropertyID.Value);
            if (productProperty != null)
            {
                productProperty.ID = 0;
                productProperty.Initialization(InitType.Insert, RequestUserID);
                _productPropertyRepository.Create(productProperty);
                rowIndex = rowIndex + 2;
            }
            return RedirectToAction("Detail", "CodeData", new { RowIndex = rowIndex });
        }
        public IActionResult CopyDetailBasic(int rowIndex)
        {
            CodeData model = GetCodeData(rowIndex);
            ProductProperty productProperty = _productPropertyRepository.GetByID001(model.ProductPropertyID.Value);
            if (productProperty != null)
            {
                productProperty.ID = 0;
                productProperty.Initialization(InitType.Insert, RequestUserID);
                _productPropertyRepository.Create(productProperty);
                rowIndex = rowIndex + 2;
            }
            return RedirectToAction("DetailBasic", "CodeData", new { RowIndex = rowIndex });
        }
        public IActionResult ExportExcelEnglish()
        {
            return Json("");
        }
        private CodeData GetCodeData(int rowIndex)
        {
            CodeData model = new CodeData();
            rowIndex = rowIndex - 1;
            if (rowIndex > -1)
            {
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
                if ((list.Count > 0) && (rowIndex < list.Count) && (rowIndex > -1))
                {
                    model = list[rowIndex];
                    model.RowCount = list.Count;
                    model.RowLast = model.RowCount - 1;
                    model.RowBack = rowIndex - 1;
                    model.RowCurrent = rowIndex + 1;
                }
            }
            return model;
        }
    }
}
