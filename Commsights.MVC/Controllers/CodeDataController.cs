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
        public IActionResult DataByEmployeeID()
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
        public ActionResult GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, int industryID)
        {
            var CookieExpires = new CookieOptions();
            CookieExpires.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("CodeDataIndustryID", industryID.ToString(), CookieExpires);
            Response.Cookies.Append("CodeDataDatePublishBegin", datePublishBegin.ToString("MM/dd/yyyy"), CookieExpires);
            Response.Cookies.Append("CodeDataDatePublishEnd", datePublishEnd.ToString("MM/dd/yyyy"), CookieExpires);
            List<CodeData> list = _codeDataRepository.GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDToList(datePublishBegin, datePublishEnd, industryID, RequestUserID);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetCategorySubByCategoryMainToList([DataSourceRequest] DataSourceRequest request, string categoryMain)
        {
            var data = _codeDataRepository.GetCategorySubByCategoryMainToList(categoryMain);
            return Json(data.ToDataSourceResult(request));
        }
        public string GetCompanyNameByTitle(string title)
        {
            return _codeDataRepository.GetCompanyNameByTitle(title);
        }
        public string GetCompanyNameByURLCode(string uRLCode)
        {
            return _codeDataRepository.GetCompanyNameByURLCode(uRLCode);
        }
        public string GetProductNameByTitle(string title)
        {
            return _codeDataRepository.GetProductNameByTitle(title);
        }
        public string GetProductNameByURLCode(string uRLCode)
        {
            return _codeDataRepository.GetProductNameByURLCode(uRLCode);
        }
        public IActionResult SaveCoding(CodeData model)
        {
            model.IsCoding = true;
            model.UserUpdated = RequestUserID;
            _productRepository.UpdateSingleItemByCodeData(model);
            _productPropertyRepository.UpdateSingleItemByCodeData(model);
            return RedirectToAction("Detail", "CodeData", new { RowIndex = model.RowIndex });
        }
        public IActionResult SaveCodingDetailBasic(CodeData model)
        {
            model.IsCoding = true;
            model.UserUpdated = RequestUserID;
            _productRepository.UpdateSingleItemByCodeData(model);
            _productPropertyRepository.UpdateSingleItemByCodeData(model);
            return RedirectToAction("DetailBasic", "CodeData", new { RowIndex = model.RowIndex });
        }
        public int Copy(int rowIndex)
        {
            CodeData model = GetCodeData(rowIndex);
            ProductProperty productProperty = _productPropertyRepository.GetByID001(model.ProductPropertyID.Value);
            if (productProperty != null)
            {
                productProperty.ID = 0;
                productProperty.Initialization(InitType.Insert, RequestUserID);
                _productPropertyRepository.Create(productProperty);
                rowIndex = rowIndex + 1;
            }
            return rowIndex;
        }
        public int CopyDetailBasic(int rowIndex)
        {
            CodeData model = GetCodeData(rowIndex);
            ProductProperty productProperty = _productPropertyRepository.GetByID001(model.ProductPropertyID.Value);
            if (productProperty != null)
            {
                productProperty.ID = 0;
                productProperty.IsCopy = true;
                productProperty.Initialization(InitType.Insert, RequestUserID);
                _productPropertyRepository.Create(productProperty);
                rowIndex = rowIndex + 1;
            }
            return rowIndex;
        }
        public IActionResult ExportExcelEnglish()
        {
            return Json("");
        }
        private CodeData GetCodeData(int rowIndex)
        {
            CodeData model = new CodeData();
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
                    List<CodeData> list = _codeDataRepository.GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDToList(datePublishBegin, datePublishEnd, industryID, RequestUserID);
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (rowIndex == list[i].RowIndex)
                        {
                            model = list[i];
                            model.CompanyNameHiden = _codeDataRepository.GetCompanyNameByTitle(model.Title);
                            model.ProductNameHiden = _codeDataRepository.GetProductNameByTitle(model.Title);
                            model.RowBack = rowIndex - 1;
                            model.RowCurrent = rowIndex;
                            model.RowNext = rowIndex + 1;
                            if (model.RowBack < list[0].RowIndex)
                            {
                                model.RowBack = list[0].RowIndex;
                            }
                            if (model.RowNext > list[list.Count - 1].RowIndex)
                            {
                                model.RowNext = list[list.Count - 1].RowIndex;
                            }
                            i = list.Count;
                        }
                    }
                }
                catch
                {
                }
            }
            return model;
        }
    }
}
