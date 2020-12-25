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
        private readonly IConfigRepository _configResposistory;
        private readonly IProductRepository _productRepository;
        private readonly IProductPropertyRepository _productPropertyRepository;
        public CodeDataController(IWebHostEnvironment hostingEnvironment, ICodeDataRepository codeDataRepository, IConfigRepository configResposistory, IProductRepository productRepository, IProductPropertyRepository productPropertyRepository, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _codeDataRepository = codeDataRepository;
            _configResposistory = configResposistory;
            _productRepository = productRepository;
            _productPropertyRepository = productPropertyRepository;
        }
        public IActionResult Data()
        {
            CodeDataViewModel model = new CodeDataViewModel();
            model.DatePublishBegin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            model.DatePublishEnd = DateTime.Now;
            model.IndustryID = AppGlobal.IndustryID;
            return View(model);
        }
        public IActionResult DataByEmployeeID()
        {
            CodeDataViewModel model = new CodeDataViewModel();
            model.DatePublishBegin = DateTime.Now;
            model.DatePublishEnd = DateTime.Now;
            model.IndustryID = AppGlobal.IndustryID;
            return View(model);
        }
        public IActionResult Export()
        {
            CodeDataViewModel model = new CodeDataViewModel();
            model.DatePublishBegin = DateTime.Now;
            model.Hour = DateTime.Now.Hour;
            model.IndustryID = AppGlobal.IndustryID;
            return View(model);
        }
        public IActionResult Industry()
        {
            CodeDataViewModel model = new CodeDataViewModel();
            model.DatePublishBegin = DateTime.Now;
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
        public IActionResult EmployeeProductPermission(int rowIndex)
        {
            CodeDataViewModel model = new CodeDataViewModel();
            model.DatePublishBegin = DateTime.Now;
            model.DatePublishEnd = DateTime.Now;
            model.IndustryID = AppGlobal.IndustryID;
            return View(model);
        }
        public IActionResult ExportExcelByCookiesOfDateUpdatedAndHourAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysis(CancellationToken cancellationToken)
        {
            List<CodeData> list = new List<CodeData>();
            string excelName = @"Code_" + AppGlobal.DateTimeCode + ".xlsx";
            string sheetName = AppGlobal.DateTimeCode;
            try
            {
                string industryName = "";
                DateTime dateUpdated = DateTime.Parse(Request.Cookies["CodeDataDateUpdated"]);
                int hour = int.Parse(Request.Cookies["CodeDataHour"]);
                int industryID = int.Parse(Request.Cookies["CodeDataIndustryID"]);
                string companyName = Request.Cookies["CodeDataCompanyName"];
                bool isCoding = bool.Parse(Request.Cookies["CodeDataIsCoding"]);
                bool isAnalysis = bool.Parse(Request.Cookies["CodeDataIsAnalysis"]);
                list = _codeDataRepository.GetReportByDateUpdatedAndHourAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysisToList(dateUpdated, hour, industryID, companyName, isCoding, isAnalysis);

                Config industry = _configResposistory.GetByID(industryID);
                if (industry != null)
                {
                    industryName = industry.CodeName;
                }
                sheetName = industryName;
                industryName = AppGlobal.SetName(industryName);
                companyName = AppGlobal.SetName(companyName);
                excelName = @"Code_" + industryName + "_" + companyName + "_" + dateUpdated.ToString("yyyyMMdd") + "_" + hour + "_" + isCoding.ToString() + "_" + isAnalysis.ToString() + "_" + AppGlobal.DateTimeCode + ".xlsx";
            }
            catch
            {
            }
            var stream = new MemoryStream();
            Color color = Color.FromArgb(int.Parse("#c00000".Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier));
            Color colorTitle = Color.FromArgb(int.Parse("#ed7d31".Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier));
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add(sheetName);
                if (list.Count > 0)
                {
                    int rowExcel = 1;
                    workSheet.Cells[rowExcel, 1].Value = "Source";
                    workSheet.Cells[rowExcel, 2].Value = "File name";
                    workSheet.Cells[rowExcel, 3].Value = "Date";
                    workSheet.Cells[rowExcel, 4].Value = "Main Cat";
                    workSheet.Cells[rowExcel, 5].Value = "Sub Cat";
                    workSheet.Cells[rowExcel, 6].Value = "Company Name";
                    workSheet.Cells[rowExcel, 7].Value = "Corp Copy";
                    workSheet.Cells[rowExcel, 8].Value = "SOE (%)";
                    workSheet.Cells[rowExcel, 9].Value = "Feature Corp";
                    workSheet.Cells[rowExcel, 10].Value = "Product Segment";
                    workSheet.Cells[rowExcel, 11].Value = "Product Name/Project Name";
                    workSheet.Cells[rowExcel, 12].Value = "SOE (%)";
                    workSheet.Cells[rowExcel, 13].Value = "Feature Product";
                    workSheet.Cells[rowExcel, 14].Value = "Sentiment";
                    workSheet.Cells[rowExcel, 15].Value = "Headline";
                    workSheet.Cells[rowExcel, 16].Value = "Headline (Eng)";
                    workSheet.Cells[rowExcel, 17].Value = "Summary";
                    workSheet.Cells[rowExcel, 18].Value = "Media Title";
                    workSheet.Cells[rowExcel, 19].Value = "Media tier";
                    workSheet.Cells[rowExcel, 20].Value = "Media Type";
                    workSheet.Cells[rowExcel, 21].Value = "Journalist";
                    workSheet.Cells[rowExcel, 22].Value = "Ad Value";
                    workSheet.Cells[rowExcel, 23].Value = "Media Value Corp";
                    workSheet.Cells[rowExcel, 24].Value = "Media Value Pro";
                    workSheet.Cells[rowExcel, 25].Value = "Key message";
                    workSheet.Cells[rowExcel, 26].Value = "Campaign name";
                    workSheet.Cells[rowExcel, 27].Value = "Campaign's key messages";
                    for (int i = 1; i < 28; i++)
                    {
                        workSheet.Cells[rowExcel, i].Style.Font.Bold = true;
                        workSheet.Cells[rowExcel, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        workSheet.Cells[rowExcel, i].Style.Font.Color.SetColor(System.Drawing.Color.White);
                        workSheet.Cells[rowExcel, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        workSheet.Cells[rowExcel, i].Style.Fill.BackgroundColor.SetColor(color);
                        workSheet.Cells[rowExcel, i].Style.Font.Name = "Times New Roman";
                        workSheet.Cells[rowExcel, i].Style.Font.Size = 11;
                        workSheet.Cells[rowExcel, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowExcel, i].Style.Border.Top.Color.SetColor(Color.Black);
                        workSheet.Cells[rowExcel, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowExcel, i].Style.Border.Left.Color.SetColor(Color.Black);
                        workSheet.Cells[rowExcel, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowExcel, i].Style.Border.Right.Color.SetColor(Color.Black);
                        workSheet.Cells[rowExcel, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowExcel, i].Style.Border.Bottom.Color.SetColor(Color.Black);
                    }
                    rowExcel = rowExcel + 1;
                    foreach (CodeData item in list)
                    {
                        workSheet.Cells[rowExcel, 1].Value = item.Source;
                        workSheet.Cells[rowExcel, 2].Value = item.FileName;
                        workSheet.Cells[rowExcel, 3].Value = item.DatePublish.ToString("MM/dd/yyyy");
                        workSheet.Cells[rowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        workSheet.Cells[rowExcel, 4].Value = item.CategoryMain;
                        workSheet.Cells[rowExcel, 5].Value = item.CategorySub;
                        workSheet.Cells[rowExcel, 6].Value = item.CompanyName;
                        workSheet.Cells[rowExcel, 7].Value = item.CorpCopy;
                        workSheet.Cells[rowExcel, 8].Value = item.SOECompany;
                        workSheet.Cells[rowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        workSheet.Cells[rowExcel, 9].Value = item.FeatureCorp;
                        workSheet.Cells[rowExcel, 10].Value = item.Segment;
                        workSheet.Cells[rowExcel, 11].Value = item.ProductName_ProjectName;
                        workSheet.Cells[rowExcel, 12].Value = item.SOEProduct;
                        workSheet.Cells[rowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        workSheet.Cells[rowExcel, 13].Value = item.FeatureProduct;
                        workSheet.Cells[rowExcel, 14].Value = item.SentimentCorp;
                        if (item.SentimentCorp.Equals("Negative"))
                        {
                            workSheet.Cells[rowExcel, 14].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                        }
                        workSheet.Cells[rowExcel, 15].Value = item.Title;
                        if ((!string.IsNullOrEmpty(item.Title)) && (!string.IsNullOrEmpty(item.URLCode)))
                        {
                            try
                            {
                                workSheet.Cells[rowExcel, 15].Hyperlink = new Uri(item.URLCode);
                            }
                            catch
                            {
                            }
                            workSheet.Cells[rowExcel, 15].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                        }
                        workSheet.Cells[rowExcel, 16].Value = item.TitleEnglish;
                        if ((!string.IsNullOrEmpty(item.TitleEnglish)) && (!string.IsNullOrEmpty(item.URLCode)))
                        {
                            try
                            {
                                workSheet.Cells[rowExcel, 16].Hyperlink = new Uri(item.URLCode);
                            }
                            catch
                            {
                            }
                            workSheet.Cells[rowExcel, 16].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                        }
                        workSheet.Cells[rowExcel, 17].Value = item.Description;
                        workSheet.Cells[rowExcel, 18].Value = item.MediaTitle;
                        workSheet.Cells[rowExcel, 19].Value = item.TierCommsights;
                        workSheet.Cells[rowExcel, 20].Value = item.MediaType;
                        workSheet.Cells[rowExcel, 21].Value = item.Journalist;
                        workSheet.Cells[rowExcel, 22].Value = item.Advalue.Value.ToString("N0");
                        workSheet.Cells[rowExcel, 22].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        workSheet.Cells[rowExcel, 23].Value = item.ROME_Corp_VND;
                        workSheet.Cells[rowExcel, 23].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        workSheet.Cells[rowExcel, 24].Value = item.ROME_Product_VND;
                        workSheet.Cells[rowExcel, 24].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        workSheet.Cells[rowExcel, 25].Value = item.KeyMessage;
                        workSheet.Cells[rowExcel, 26].Value = item.CampaignName;
                        workSheet.Cells[rowExcel, 27].Value = item.CampaignKeyMessage;

                        for (int i = 1; i < 28; i++)
                        {
                            workSheet.Cells[rowExcel, i].Style.Font.Name = "Times New Roman";
                            workSheet.Cells[rowExcel, i].Style.Font.Size = 11;
                            workSheet.Cells[rowExcel, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[rowExcel, i].Style.Border.Top.Color.SetColor(Color.Black);
                            workSheet.Cells[rowExcel, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[rowExcel, i].Style.Border.Left.Color.SetColor(Color.Black);
                            workSheet.Cells[rowExcel, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[rowExcel, i].Style.Border.Right.Color.SetColor(Color.Black);
                            workSheet.Cells[rowExcel, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[rowExcel, i].Style.Border.Bottom.Color.SetColor(Color.Black);
                        }
                        rowExcel = rowExcel + 1;
                    }
                    for (int i = 1; i < 28; i++)
                    {
                        workSheet.Column(i).AutoFit();
                    }
                }
                package.Save();
            }
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        public ActionResult GetReportByDateUpdatedAndHourAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysisToList([DataSourceRequest] DataSourceRequest request, DateTime dateUpdated, int hour, int industryID, string companyName, bool isCoding, bool isAnalysis)
        {
            string isCodingString = isCoding.ToString();
            if (string.IsNullOrEmpty(companyName))
            {
                companyName = "";
            }
            var cookieExpires = new CookieOptions();
            cookieExpires.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("CodeDataDateUpdated", dateUpdated.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataHour", hour.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataIndustryID", industryID.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataCompanyName", companyName, cookieExpires);
            Response.Cookies.Append("CodeDataIsCoding", isCoding.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataIsAnalysis", isAnalysis.ToString(), cookieExpires);
            List<CodeData> list = _codeDataRepository.GetReportByDateUpdatedAndHourAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysisToList(dateUpdated, hour, industryID, companyName, isCoding, isAnalysis);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetReportSelectByDatePublishBeginAndDatePublishEnd001ToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd)
        {
            List<Membership> list = _codeDataRepository.GetReportSelectByDatePublishBeginAndDatePublishEnd001ToList(datePublishBegin, datePublishEnd);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetReportByDatePublishBeginAndDatePublishEndToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd)
        {
            List<CodeDataReport> list = _codeDataRepository.GetReportByDatePublishBeginAndDatePublishEndToList(datePublishBegin, datePublishEnd);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetByDatePublishBeginAndDatePublishEndAndIndustryIDToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, int industryID)
        {
            var cookieExpires = new CookieOptions();
            cookieExpires.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("CodeDataIndustryID", industryID.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataDatePublishBegin", datePublishBegin.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataDatePublishEnd", datePublishEnd.ToString("MM/dd/yyyy"), cookieExpires);
            List<CodeData> list = _codeDataRepository.GetByDatePublishBeginAndDatePublishEndAndIndustryIDToList(datePublishBegin, datePublishEnd, industryID);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, int industryID)
        {
            var cookieExpires = new CookieOptions();
            cookieExpires.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("CodeDataIndustryID", industryID.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataDatePublishBegin", datePublishBegin.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataDatePublishEnd", datePublishEnd.ToString("MM/dd/yyyy"), cookieExpires);
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
        public string CheckCodeData(CodeData model)
        {
            bool check = true;
            string actionMessage = "";
            if (model.SOECompany > 0)
            {
                if (model.SOEProduct > model.SOECompany)
                {
                    check = false;
                    actionMessage = AppGlobal.Error + " - SOE Product > SOE Company";
                }
            }
            if (!string.IsNullOrEmpty(model.ProductName_ProjectName))
            {
                if (model.SOEProduct == 0)
                {
                    check = false;
                    actionMessage = AppGlobal.Error + " - ProductName exist but SOE is null or 0";
                }
            }
            if (model.SOEProduct > 0)
            {
                if (string.IsNullOrEmpty(model.ProductName_ProjectName))
                {
                    check = false;
                    actionMessage = AppGlobal.Error + " - SOE > 0 but ProductName not exist";
                }
            }
            if (check == true)
            {
                model.IsCoding = true;
                model.UserUpdated = RequestUserID;
                _productRepository.UpdateSingleItemByCodeData(model);
                _productPropertyRepository.UpdateItemsByCodeDataCopyVersion(model);
            }
            return actionMessage;
        }
        public int Copy(int rowIndex)
        {
            CodeData model = GetCodeData(rowIndex);
            _productPropertyRepository.InsertSingleItemByCopyCodeData(model.ProductPropertyID.Value, RequestUserID);
            rowIndex = rowIndex + 1;
            return rowIndex;
        }
        public IActionResult SaveCoding(CodeData model)
        {
            string actionMessage = CheckCodeData(model);
            return RedirectToAction("Detail", "CodeData", new { RowIndex = model.RowIndex, ActionMessage = actionMessage });
        }
        public IActionResult SaveCodingDetailBasic(CodeData model)
        {
            string actionMessage = CheckCodeData(model);
            return RedirectToAction("DetailBasic", "CodeData", new { RowIndex = model.RowIndex, ActionMessage = actionMessage });
        }
        public int CopyURLSame(int rowIndex)
        {
            rowIndex = Copy(rowIndex);
            return rowIndex;
        }
        public int CopyURLAnother(int rowIndex)
        {
            rowIndex = Copy(rowIndex);
            return rowIndex;
        }
        public int BasicCopyURLSame(int rowIndex)
        {
            CodeData model = GetCodeData(rowIndex);
            _productPropertyRepository.InsertSingleItemByCopyCodeData(model.ProductPropertyID.Value, RequestUserID);
            rowIndex = rowIndex + 1;
            return rowIndex;
        }
        public int BasicCopyURLAnother(int rowIndex)
        {
            CodeData model = GetCodeData(rowIndex);
            rowIndex = _productPropertyRepository.InsertItemsByCopyCodeData(model.ProductPropertyID.Value, RequestUserID, rowIndex);
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
        public IActionResult DeleteProductProperty(int rowIndex)
        {
            CodeData model = GetCodeData(rowIndex);
            string note = AppGlobal.InitString;
            _productPropertyRepository.DeleteItemsByIDCodeData(model.ProductPropertyID.Value);
            int result = 1;            
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.DeleteSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.DeleteFail;
            }
            return Json(note);
        }

    }
}
