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
using System.Web;
using System.Text.RegularExpressions;

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
        public IActionResult SearchByEmployeeID()
        {
            CodeDataViewModel model = new CodeDataViewModel();
            model.DatePublishBegin = DateTime.Now;
            model.DatePublishEnd = DateTime.Now;
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
        public IActionResult DataByEmployeeIDAndSourceIsNewspageAndTV()
        {
            CodeDataViewModel model = new CodeDataViewModel();
            model.DatePublishBegin = DateTime.Now;
            model.DatePublishEnd = DateTime.Now;
            model.IndustryID = AppGlobal.IndustryID;
            return View(model);
        }
        public IActionResult DataByEmployeeIDAndSourceIsNotNewspageAndTV()
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
            model.HourBegin = 0;
            model.HourEnd = DateTime.Now.Hour;
            model.IndustryID = AppGlobal.IndustryID;
            return View(model);
        }
        public IActionResult Export001()
        {
            DateTime now = DateTime.Now;
            CodeDataViewModel model = new CodeDataViewModel();
            model.HourBegin = 0;
            model.HourEnd = now.Hour;
            model.DatePublishBegin = now;
            model.DatePublishEnd = now;
            model.IndustryID = AppGlobal.IndustryID;
            model.CompanyName = "";
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
        public IActionResult SearchStatistical()
        {
            CodeDataViewModel model = new CodeDataViewModel();
            model.DatePublishBegin = DateTime.Now;
            model.DatePublishEnd = DateTime.Now;
            model.IndustryID = AppGlobal.IndustryID;
            return View(model);
        }
        public IActionResult Employee()
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

        public IActionResult Detail(int productPropertyID)
        {
            CodeData model = GetCodeData(productPropertyID);
            if (string.IsNullOrEmpty(model.Title))
            {
                return RedirectToAction("Detail", "CodeData", new { ProductPropertyID = model.ProductPropertyID });
            }
            return View(model);
        }
        public IActionResult DetailBasic(int productPropertyID)
        {
            CodeData model = GetCodeData(productPropertyID);
            if (string.IsNullOrEmpty(model.Title))
            {
                return RedirectToAction("DetailBasic", "CodeData", new { ProductPropertyID = model.ProductPropertyID });
            }
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
        public IActionResult ExportExcelByCookiesOfDateUpdatedAndHourBeginAndHourEndAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysis(CancellationToken cancellationToken)
        {
            List<CodeData> list = new List<CodeData>();
            string excelName = @"Code_" + AppGlobal.DateTimeCode + ".xlsx";
            string sheetName = AppGlobal.DateTimeCode;
            try
            {
                string industryName = "";
                DateTime dateUpdated = DateTime.Parse(Request.Cookies["CodeDataDateUpdated"]);
                int hourBegin = int.Parse(Request.Cookies["CodeDataHourBegin"]);
                int hourEnd = int.Parse(Request.Cookies["CodeDataHourEnd"]);
                int industryID = int.Parse(Request.Cookies["CodeDataIndustryID"]);
                string companyName = Request.Cookies["CodeDataCompanyName"];
                bool isCoding = bool.Parse(Request.Cookies["CodeDataIsCoding"]);
                bool isAnalysis = bool.Parse(Request.Cookies["CodeDataIsAnalysis"]);
                list = _codeDataRepository.GetReportByDateUpdatedAndHourBeginAndHourEndAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysisToList(dateUpdated, hourBegin, hourEnd, industryID, companyName, isCoding, isAnalysis);

                Config industry = _configResposistory.GetByID(industryID);
                if (industry != null)
                {
                    industryName = industry.CodeName;
                }
                sheetName = industryName;
                industryName = AppGlobal.SetName(industryName);
                companyName = AppGlobal.SetName(companyName);
                excelName = @"Code_" + industryName + "_" + companyName + "_" + dateUpdated.ToString("yyyyMMdd") + "_" + hourBegin + "_" + hourEnd + "_" + isCoding.ToString() + "_" + isAnalysis.ToString() + "_" + AppGlobal.DateTimeCode + ".xlsx";
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
                    workSheet.Cells[rowExcel, 28].Value = "Note";
                    for (int i = 1; i < 29; i++)
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
                        workSheet.Cells[rowExcel, 28].Value = item.Note;
                        for (int i = 1; i < 29; i++)
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
        public IActionResult ExportExcelByCookiesOfDateUpdatedAndHourBeginAndHourEndAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysisForDaily(CancellationToken cancellationToken)
        {
            List<CodeData> list = new List<CodeData>();
            string excelName = @"Daily_" + AppGlobal.DateTimeCode + ".xlsx";
            string sheetName = AppGlobal.DateTimeCode;
            try
            {
                string industryName = "";
                DateTime dateUpdated = DateTime.Parse(Request.Cookies["CodeDataDateUpdated"]);
                int hourBegin = int.Parse(Request.Cookies["CodeDataHourBegin"]);
                int hourEnd = int.Parse(Request.Cookies["CodeDataHourEnd"]);
                int industryID = int.Parse(Request.Cookies["CodeDataIndustryID"]);
                string companyName = Request.Cookies["CodeDataCompanyName"];
                bool isCoding = bool.Parse(Request.Cookies["CodeDataIsCoding"]);
                bool isAnalysis = bool.Parse(Request.Cookies["CodeDataIsAnalysis"]);
                list = _codeDataRepository.GetReportByDateUpdatedAndHourBeginAndHourEndAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysisToList(dateUpdated, hourBegin, hourEnd, industryID, companyName, isCoding, isAnalysis);

                Config industry = _configResposistory.GetByID(industryID);
                if (industry != null)
                {
                    industryName = industry.CodeName;
                }
                sheetName = industryName;
                industryName = AppGlobal.SetName(industryName);
                companyName = AppGlobal.SetName(companyName);
                excelName = @"Daily_" + industryName + "_" + companyName + "_" + dateUpdated.ToString("yyyyMMdd") + "_" + hourBegin + "_" + hourEnd + "_" + isCoding.ToString() + "_" + isAnalysis.ToString() + "_" + AppGlobal.DateTimeCode + ".xlsx";
            }
            catch
            {
            }
            var stream = new MemoryStream();
            Color color = Color.FromArgb(int.Parse("#c00000".Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier));
            Color colorTitle = Color.FromArgb(int.Parse("#ed7d31".Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier));
            List<Config> listDailyReportColumn = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.DailyReportColumn);
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add(sheetName);
                if (list.Count > 0)
                {
                    int column = 1;
                    int rowExcel = 1;
                    foreach (Config item in listDailyReportColumn)
                    {
                        workSheet.Cells[rowExcel, column].Value = item.CodeName;
                        workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                        workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                        workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                        workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                        workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                        workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                        workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                        workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                        workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                        column = column + 1;
                    }
                    workSheet.Cells[rowExcel, column].Value = "Headline (Eng)";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    column = column + 1;
                    workSheet.Cells[rowExcel, column].Value = "Summary (Eng)";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    column = column + 1;
                    workSheet.Cells[rowExcel, column].Value = "Note";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    column = column + 1;
                    workSheet.Cells[rowExcel, column].Value = "Upload";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    column = column + 1;
                    workSheet.Cells[rowExcel, column].Value = "URL";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    int index = 0;
                    rowExcel = rowExcel + 1;
                    for (int row = rowExcel; row <= list.Count + rowExcel - 1; row++)
                    {
                        for (int i = 1; i <= column; i++)
                        {
                            if (i == 1)
                            {
                                workSheet.Cells[row, i].Value = list[index].DatePublish;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                workSheet.Cells[row, i].Style.Numberformat.Format = "mm/dd/yyyy";
                            }
                            if (i == 2)
                            {
                                workSheet.Cells[row, i].Value = list[index].CategoryMain;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 3)
                            {
                                workSheet.Cells[row, i].Value = list[index].Segment;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 4)
                            {
                                workSheet.Cells[row, i].Value = "";
                            }
                            if (i == 5)
                            {
                                if (!string.IsNullOrEmpty(list[index].CompanyName))
                                {
                                    workSheet.Cells[row, i].Value = list[index].CompanyName;
                                }
                                else
                                {
                                    workSheet.Cells[row, i].Value = list[index].CategoryMain;
                                }
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 6)
                            {
                                workSheet.Cells[row, i].Value = list[index].ProductName_ProjectName;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 7)
                            {
                                workSheet.Cells[row, i].Value = list[index].SentimentCorp;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 8)
                            {
                                workSheet.Cells[row, i].Value = list[index].Title;
                                if ((!string.IsNullOrEmpty(list[index].Title)) && (!string.IsNullOrEmpty(list[index].URLCode)))
                                {
                                    try
                                    {
                                        workSheet.Cells[row, i].Hyperlink = new Uri(list[index].URLCode);
                                    }
                                    catch
                                    {

                                    }
                                    workSheet.Cells[row, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                }
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 9)
                            {
                                workSheet.Cells[row, i].Value = list[index].TitleEnglish;
                                if ((!string.IsNullOrEmpty(list[index].TitleEnglish)) && (!string.IsNullOrEmpty(list[index].URLCode)))
                                {
                                    try
                                    {
                                        workSheet.Cells[row, i].Hyperlink = new Uri(list[index].URLCode);
                                    }
                                    catch
                                    {

                                    }
                                    workSheet.Cells[row, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                }
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 10)
                            {
                                workSheet.Cells[row, i].Value = list[index].MediaTitle;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 11)
                            {
                                workSheet.Cells[row, i].Value = list[index].MediaType;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 12)
                            {
                                workSheet.Cells[row, i].Value = list[index].Page;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            }
                            if (i == 13)
                            {
                                workSheet.Cells[row, i].Value = list[index].Advalue.Value.ToString("N0");
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            }
                            if (i == 14)
                            {
                                workSheet.Cells[row, i].Value = list[index].DescriptionEnglish;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 15)
                            {
                                workSheet.Cells[row, i].Value = list[index].Duration;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 16)
                            {
                                workSheet.Cells[row, i].Value = list[index].Frequency;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 17)
                            {
                                workSheet.Cells[row, i].Value = list[index].TitleEnglish;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 18)
                            {
                                workSheet.Cells[row, i].Value = list[index].DescriptionEnglish;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 19)
                            {
                                workSheet.Cells[row, i].Value = list[index].Note;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 20)
                            {
                                workSheet.Cells[row, i].Value = list[index].DateUpdated;
                                workSheet.Cells[row, i].Style.Numberformat.Format = "mm/dd/yyyy HH:mm:ss";
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            }
                            if (i == 21)
                            {
                                if (!string.IsNullOrEmpty(list[index].URLCode))
                                {
                                    try
                                    {
                                        workSheet.Cells[row, column].Value = list[index].URLCode;
                                        workSheet.Cells[row, column].Hyperlink = new Uri(list[index].URLCode);
                                    }
                                    catch
                                    {

                                    }
                                }
                                workSheet.Cells[row, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            workSheet.Cells[row, i].Style.Font.Name = "Times New Roman";
                            workSheet.Cells[row, i].Style.Font.Size = 11;
                            workSheet.Cells[row, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[row, i].Style.Border.Top.Color.SetColor(Color.Black);
                            workSheet.Cells[row, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[row, i].Style.Border.Left.Color.SetColor(Color.Black);
                            workSheet.Cells[row, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[row, i].Style.Border.Right.Color.SetColor(Color.Black);
                            workSheet.Cells[row, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[row, i].Style.Border.Bottom.Color.SetColor(Color.Black);
                        }

                        index = index + 1;
                    }
                    for (int i = 1; i <= column; i++)
                    {
                        workSheet.Column(i).AutoFit();
                    }
                }
                package.Save();
            }
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        public IActionResult ExportExcelByCookiesOfByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndIsCoding(CancellationToken cancellationToken)
        {
            List<CodeData> list = new List<CodeData>();
            string excelName = @"Code_" + AppGlobal.DateTimeCode + ".xlsx";
            string sheetName = AppGlobal.DateTimeCode;
            try
            {
                string industryName = "";
                DateTime dateUpdatedBegin = DateTime.Parse(Request.Cookies["CodeDataDateUpdatedBegin"]);
                DateTime dateUpdatedEnd = DateTime.Parse(Request.Cookies["CodeDataDateUpdatedEnd"]);
                int industryID = int.Parse(Request.Cookies["CodeDataIndustryID"]);
                bool isCoding = bool.Parse(Request.Cookies["CodeDataIsCoding"]);
                list = _codeDataRepository.GetReportByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndIsCodingToList(dateUpdatedBegin, dateUpdatedEnd, industryID, isCoding);
                Config industry = _configResposistory.GetByID(industryID);
                if (industry != null)
                {
                    industryName = industry.CodeName;
                }
                sheetName = industryName;
                industryName = AppGlobal.SetName(industryName);
                excelName = @"Code_" + industryName + "_" + dateUpdatedBegin.ToString("yyyyMMdd") + "_" + dateUpdatedEnd.ToString("yyyyMMdd") + "_" + isCoding.ToString() + "_" + AppGlobal.DateTimeCode + ".xlsx";
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
        public IActionResult ExportExcelByCookiesOfDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndIsCodingForDaily(CancellationToken cancellationToken)
        {
            List<CodeData> list = new List<CodeData>();
            string excelName = @"Daily_" + AppGlobal.DateTimeCode + ".xlsx";
            string sheetName = AppGlobal.DateTimeCode;
            try
            {
                string industryName = "";
                DateTime dateUpdatedBegin = DateTime.Parse(Request.Cookies["CodeDataDateUpdatedBegin"]);
                DateTime dateUpdatedEnd = DateTime.Parse(Request.Cookies["CodeDataDateUpdatedEnd"]);
                int industryID = int.Parse(Request.Cookies["CodeDataIndustryID"]);
                bool isCoding = bool.Parse(Request.Cookies["CodeDataIsCoding"]);
                list = _codeDataRepository.GetReportByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndIsCodingToList(dateUpdatedBegin, dateUpdatedEnd, industryID, isCoding);

                Config industry = _configResposistory.GetByID(industryID);
                if (industry != null)
                {
                    industryName = industry.CodeName;
                }
                sheetName = industryName;
                industryName = AppGlobal.SetName(industryName);
                excelName = @"Daily_" + industryName + "_" + dateUpdatedBegin.ToString("yyyyMMdd") + "_" + dateUpdatedEnd.ToString("yyyyMMdd") + "_" + isCoding.ToString() + "_" + AppGlobal.DateTimeCode + ".xlsx";
            }
            catch
            {
            }
            var stream = new MemoryStream();
            Color color = Color.FromArgb(int.Parse("#c00000".Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier));
            Color colorTitle = Color.FromArgb(int.Parse("#ed7d31".Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier));
            List<Config> listDailyReportColumn = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.DailyReportColumn);
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add(sheetName);
                if (list.Count > 0)
                {
                    int column = 1;
                    int rowExcel = 1;
                    foreach (Config item in listDailyReportColumn)
                    {
                        workSheet.Cells[rowExcel, column].Value = item.CodeName;
                        workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                        workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                        workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                        workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                        workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                        workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                        workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                        workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                        workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                        column = column + 1;
                    }
                    workSheet.Cells[rowExcel, column].Value = "Upload";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    column = column + 1;
                    workSheet.Cells[rowExcel, column].Value = "URL";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    int index = 0;
                    rowExcel = rowExcel + 1;
                    for (int row = rowExcel; row <= list.Count + rowExcel - 1; row++)
                    {
                        for (int i = 1; i <= column; i++)
                        {
                            if (i == 1)
                            {
                                workSheet.Cells[row, i].Value = list[index].DatePublish;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                workSheet.Cells[row, i].Style.Numberformat.Format = "mm/dd/yyyy";
                            }
                            if (i == 2)
                            {
                                workSheet.Cells[row, i].Value = list[index].CategoryMain;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 3)
                            {
                                workSheet.Cells[row, i].Value = list[index].Segment;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 4)
                            {
                                workSheet.Cells[row, i].Value = list[index].CategorySub;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 5)
                            {
                                if (!string.IsNullOrEmpty(list[index].CompanyName))
                                {
                                    workSheet.Cells[row, i].Value = list[index].CompanyName;
                                }
                                else
                                {
                                    workSheet.Cells[row, i].Value = list[index].CategoryMain;
                                }
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 6)
                            {
                                workSheet.Cells[row, i].Value = list[index].ProductName_ProjectName;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 7)
                            {
                                workSheet.Cells[row, i].Value = list[index].SentimentCorp;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 8)
                            {
                                workSheet.Cells[row, i].Value = list[index].Title;
                                if ((!string.IsNullOrEmpty(list[index].Title)) && (!string.IsNullOrEmpty(list[index].URLCode)))
                                {
                                    try
                                    {
                                        workSheet.Cells[row, i].Hyperlink = new Uri(list[index].URLCode);
                                    }
                                    catch
                                    {

                                    }
                                    workSheet.Cells[row, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                }
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 9)
                            {
                                workSheet.Cells[row, i].Value = list[index].TitleEnglish;
                                if ((!string.IsNullOrEmpty(list[index].TitleEnglish)) && (!string.IsNullOrEmpty(list[index].URLCode)))
                                {
                                    try
                                    {
                                        workSheet.Cells[row, i].Hyperlink = new Uri(list[index].URLCode);
                                    }
                                    catch
                                    {

                                    }
                                    workSheet.Cells[row, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                }
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 10)
                            {
                                workSheet.Cells[row, i].Value = list[index].MediaTitle;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 11)
                            {
                                workSheet.Cells[row, i].Value = list[index].MediaType;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 12)
                            {
                                workSheet.Cells[row, i].Value = list[index].Page;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            }
                            if (i == 13)
                            {
                                workSheet.Cells[row, i].Value = list[index].Advalue.Value.ToString("N0");
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            }
                            if (i == 14)
                            {
                                workSheet.Cells[row, i].Value = list[index].DescriptionEnglish;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 15)
                            {
                                workSheet.Cells[row, i].Value = list[index].Duration;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 16)
                            {
                                workSheet.Cells[row, i].Value = list[index].Frequency;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 17)
                            {
                                workSheet.Cells[row, i].Value = list[index].DateUpdated;
                                workSheet.Cells[row, i].Style.Numberformat.Format = "mm/dd/yyyy HH:mm:ss";
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            }
                            if (i == 18)
                            {
                                if (!string.IsNullOrEmpty(list[index].URLCode))
                                {
                                    try
                                    {
                                        workSheet.Cells[row, column].Value = list[index].URLCode;
                                        workSheet.Cells[row, column].Hyperlink = new Uri(list[index].URLCode);
                                    }
                                    catch
                                    {

                                    }
                                }
                                workSheet.Cells[row, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            workSheet.Cells[row, i].Style.Font.Name = "Times New Roman";
                            workSheet.Cells[row, i].Style.Font.Size = 11;
                            workSheet.Cells[row, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[row, i].Style.Border.Top.Color.SetColor(Color.Black);
                            workSheet.Cells[row, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[row, i].Style.Border.Left.Color.SetColor(Color.Black);
                            workSheet.Cells[row, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[row, i].Style.Border.Right.Color.SetColor(Color.Black);
                            workSheet.Cells[row, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[row, i].Style.Border.Bottom.Color.SetColor(Color.Black);
                        }

                        index = index + 1;
                    }
                    for (int i = 1; i <= column; i++)
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
        public ActionResult GetReportByDateUpdatedAndHourBeginAndHourEndAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysisToList([DataSourceRequest] DataSourceRequest request, DateTime dateUpdated, int hourBegin, int hourEnd, int industryID, string companyName, bool isCoding, bool isAnalysis)
        {
            string isCodingString = isCoding.ToString();
            if (string.IsNullOrEmpty(companyName))
            {
                companyName = "";
            }
            var cookieExpires = new CookieOptions();
            cookieExpires.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("CodeDataDateUpdated", dateUpdated.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataHourBegin", hourBegin.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataHourEnd", hourEnd.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataIndustryID", industryID.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataCompanyName", companyName, cookieExpires);
            Response.Cookies.Append("CodeDataIsCoding", isCoding.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataIsAnalysis", isAnalysis.ToString(), cookieExpires);
            List<CodeData> list = _codeDataRepository.GetReportByDateUpdatedAndHourBeginAndHourEndAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysisToList(dateUpdated, hourBegin, hourEnd, industryID, companyName, isCoding, isAnalysis);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetReportByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndIsCodingToList([DataSourceRequest] DataSourceRequest request, DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int industryID, bool isCoding)
        {
            var cookieExpires = new CookieOptions();
            cookieExpires.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("CodeDataDateUpdatedBegin", dateUpdatedBegin.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataDateUpdatedEnd", dateUpdatedEnd.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataIndustryID", industryID.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataIsCoding", isCoding.ToString(), cookieExpires);
            List<CodeData> list = _codeDataRepository.GetReportByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDAndIsCodingToList(dateUpdatedBegin, dateUpdatedEnd, industryID, isCoding);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetByDateUpdatedBeginAndDateUpdatedEndAndHourBeginAndHourEndAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysisToList([DataSourceRequest] DataSourceRequest request, DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int hourBegin, int hourEnd, int industryID, string companyName, bool isCoding, bool isAnalysis)
        {
            if (string.IsNullOrEmpty(companyName))
            {
                companyName = "";
            }
            var cookieExpires = new CookieOptions();
            cookieExpires.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("CodeDataDateUpdatedBegin", dateUpdatedBegin.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataDateUpdatedEnd", dateUpdatedEnd.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataHourBegin", hourBegin.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataHourEnd", hourEnd.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataIndustryID", industryID.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataCompanyName", companyName.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataIsCoding", isCoding.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataIsAnalysis", isAnalysis.ToString(), cookieExpires);
            List<CodeData> list = _codeDataRepository.GetByDateUpdatedBeginAndDateUpdatedEndAndHourBeginAndHourEndAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysisToList(dateUpdatedBegin, dateUpdatedEnd, hourBegin, hourEnd, industryID, companyName, isCoding, isAnalysis);
            return Json(list.ToDataSourceResult(request));
        }

        public ActionResult GetByDateUpdatedBeginAndDateUpdatedEndAndHourBeginAndHourEndAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysisAndIsUploadToList([DataSourceRequest] DataSourceRequest request, DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int hourBegin, int hourEnd, int industryID, string companyName, bool isCoding, bool isAnalysis, bool isUpload)
        {
            if (string.IsNullOrEmpty(companyName))
            {
                companyName = "";
            }
            var cookieExpires = new CookieOptions();
            cookieExpires.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("CodeDataDateUpdatedBegin", dateUpdatedBegin.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataDateUpdatedEnd", dateUpdatedEnd.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataHourBegin", hourBegin.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataHourEnd", hourEnd.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataIndustryID", industryID.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataCompanyName", companyName.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataIsCoding", isCoding.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataIsAnalysis", isAnalysis.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataIsUpload", isUpload.ToString(), cookieExpires);
            List<CodeData> list = _codeDataRepository.GetByDateUpdatedBeginAndDateUpdatedEndAndHourBeginAndHourEndAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysisAndIsUploadToList(dateUpdatedBegin, dateUpdatedEnd, hourBegin, hourEnd, industryID, companyName, isCoding, isAnalysis, isUpload);
            return Json(list.ToDataSourceResult(request));
        }
        public string Export001ExportExcel(CancellationToken cancellationToken)
        {
            List<CodeData> list = new List<CodeData>();
            List<Config> listProductFeature = new List<Config>();
            Config industry = new Config();
            string excelName = @"Code_" + AppGlobal.DateTimeCode + ".xlsx";
            string sheetName = AppGlobal.DateTimeCode;
            try
            {
                string industryName = "";
                DateTime dateUpdatedBegin = DateTime.Parse(Request.Cookies["CodeDataDateUpdatedBegin"]);
                DateTime dateUpdatedEnd = DateTime.Parse(Request.Cookies["CodeDataDateUpdatedEnd"]);
                int hourBegin = int.Parse(Request.Cookies["CodeDataHourBegin"]);
                int hourEnd = int.Parse(Request.Cookies["CodeDataHourEnd"]);
                int industryID = int.Parse(Request.Cookies["CodeDataIndustryID"]);
                string companyName = Request.Cookies["CodeDataCompanyName"];
                bool isCoding = bool.Parse(Request.Cookies["CodeDataIsCoding"]);
                bool isAnalysis = bool.Parse(Request.Cookies["CodeDataIsAnalysis"]);
                bool isUpload = bool.Parse(Request.Cookies["CodeDataIsUpload"]);
                list = _codeDataRepository.GetByDateUpdatedBeginAndDateUpdatedEndAndHourBeginAndHourEndAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysisAndIsUploadToList(dateUpdatedBegin, dateUpdatedEnd, hourBegin, hourEnd, industryID, companyName, isCoding, isAnalysis, isUpload);
                industry = _configResposistory.GetByID(industryID);
                if (industry != null)
                {
                    industryName = industry.CodeName;
                }
                sheetName = industryName;
                industryName = AppGlobal.SetName(industryName);
                excelName = @"Code_" + industryName + "_" + dateUpdatedBegin.ToString("yyyyMMdd") + "_" + dateUpdatedEnd.ToString("yyyyMMdd") + "_" + AppGlobal.DateTimeCode + ".xlsx";
                _productPropertyRepository.UpdateItemsByDailyDownload(dateUpdatedBegin, dateUpdatedEnd, hourBegin, hourEnd, industryID, companyName, isCoding, isAnalysis, RequestUserID);
                if (industry != null)
                {
                    if (industry.IsMenuLeft == true)
                    {
                        listProductFeature = _configResposistory.GetByGroupNameAndCodeAndIndustryIDToList(AppGlobal.CRM, AppGlobal.ProductFeature, industryID);
                    }
                }
            }
            catch
            {
            }
            var streamExport = new MemoryStream();
            Color color = Color.FromArgb(int.Parse("#c00000".Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier));
            Color colorTitle = Color.FromArgb(int.Parse("#ed7d31".Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier));
            using (var package = new ExcelPackage(streamExport))
            {
                var workSheet = package.Workbook.Worksheets.Add(sheetName);
                if (list.Count > 0)
                {
                    int rowExcel = 1;
                    int columnExcel = 1;
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
                    workSheet.Cells[rowExcel, 17].Value = "Summary (Eng)";
                    workSheet.Cells[rowExcel, 18].Value = "Summary";
                    workSheet.Cells[rowExcel, 19].Value = "URL";
                    workSheet.Cells[rowExcel, 20].Value = "Page";
                    workSheet.Cells[rowExcel, 21].Value = "Media Title";
                    workSheet.Cells[rowExcel, 22].Value = "Media tier";
                    workSheet.Cells[rowExcel, 23].Value = "Media Type";
                    workSheet.Cells[rowExcel, 24].Value = "Journalist";
                    workSheet.Cells[rowExcel, 25].Value = "Ad Value";
                    workSheet.Cells[rowExcel, 26].Value = "Media Value Corp";
                    workSheet.Cells[rowExcel, 27].Value = "Media Value Pro";
                    workSheet.Cells[rowExcel, 28].Value = "Key message";
                    workSheet.Cells[rowExcel, 29].Value = "Campaign name";
                    workSheet.Cells[rowExcel, 30].Value = "Campaign's key messages";
                    workSheet.Cells[rowExcel, 31].Value = "Note";
                    workSheet.Cells[rowExcel, 32].Value = "Product feature";
                    columnExcel = 33;
                    if (listProductFeature.Count > 0)
                    {
                        foreach (Config item in listProductFeature)
                        {
                            workSheet.Cells[rowExcel, columnExcel].Value = item.CodeName;
                            columnExcel = columnExcel + 1;
                        }
                    }
                    for (int i = 1; i < columnExcel; i++)
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
                        workSheet.Cells[rowExcel, 3].Value = item.DatePublish;
                        workSheet.Cells[rowExcel, 3].Style.Numberformat.Format = "mm/dd/yyyy";
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
                        if (!string.IsNullOrEmpty(item.SentimentCorp))
                        {
                            if (item.SentimentCorp.Equals("Negative"))
                            {
                                workSheet.Cells[rowExcel, 14].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                            }
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
                        workSheet.Cells[rowExcel, 18].Value = item.DescriptionEnglish;
                        workSheet.Cells[rowExcel, 19].Value = item.URLCode;
                        if (!string.IsNullOrEmpty(item.URLCode))
                        {
                            try
                            {
                                workSheet.Cells[rowExcel, 19].Hyperlink = new Uri(item.URLCode);
                            }
                            catch
                            {
                            }
                            workSheet.Cells[rowExcel, 19].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                        }
                        workSheet.Cells[rowExcel, 20].Value = item.Page;
                        workSheet.Cells[rowExcel, 21].Value = item.MediaTitle;
                        workSheet.Cells[rowExcel, 22].Value = item.TierCommsights;
                        workSheet.Cells[rowExcel, 23].Value = item.MediaType;
                        workSheet.Cells[rowExcel, 24].Value = item.Journalist;
                        workSheet.Cells[rowExcel, 25].Value = item.Advalue.Value.ToString("N0");
                        workSheet.Cells[rowExcel, 25].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        workSheet.Cells[rowExcel, 26].Value = item.ROME_Corp_VND;
                        workSheet.Cells[rowExcel, 26].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        workSheet.Cells[rowExcel, 27].Value = item.ROME_Product_VND;
                        workSheet.Cells[rowExcel, 27].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        workSheet.Cells[rowExcel, 28].Value = item.KeyMessage;
                        workSheet.Cells[rowExcel, 29].Value = item.CampaignName;
                        workSheet.Cells[rowExcel, 30].Value = item.CampaignKeyMessage;
                        workSheet.Cells[rowExcel, 31].Value = item.Note;
                        string productFeature = "";
                        try
                        {
                            if (listProductFeature.Count > 0)
                            {
                                List<ProductProperty> listProductProperty = _productPropertyRepository.GetByIDAndCodeToList(item.ProductPropertyID.Value, AppGlobal.ProductFeature);
                                int listProductPropertyIndex = 0;
                                for (int i = 30; i < columnExcel; i++)
                                {
                                    if (listProductProperty[listProductPropertyIndex].Active == true)
                                    {
                                        workSheet.Cells[rowExcel, i].Value = "x";
                                        productFeature = productFeature + ", " + listProductProperty[listProductPropertyIndex].ProductName_ProjectName;
                                    }
                                    listProductPropertyIndex = listProductPropertyIndex + 1;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            string mes = e.Message;
                        }
                        if (!string.IsNullOrEmpty(productFeature))
                        {
                            if (productFeature[0] == ',')
                            {
                                productFeature = productFeature.Substring(1);
                            }
                            productFeature = productFeature.Trim();
                        }
                        workSheet.Cells[rowExcel, 32].Value = productFeature;
                        for (int i = 1; i < columnExcel; i++)
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
                    for (int i = 1; i < columnExcel; i++)
                    {
                        workSheet.Column(i).Width = 30;
                    }
                }
                package.Save();
            }
            streamExport.Position = 0;
            var physicalPathCreate = Path.Combine(_hostingEnvironment.WebRootPath, AppGlobal.FTPDownloadReprotMonth, excelName);
            using (var stream = new FileStream(physicalPathCreate, FileMode.Create))
            {
                streamExport.CopyTo(stream);
            }
            string result = AppGlobal.DomainSub + AppGlobal.URLDownloadReprotMonth + excelName;
            return result;
        }
        public ActionResult GetReportSelectByDatePublishBeginAndDatePublishEnd001ToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd)
        {
            List<Membership> list = _codeDataRepository.GetReportSelectByDatePublishBeginAndDatePublishEnd001ToList(datePublishBegin, datePublishEnd);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetReportByDatePublishBeginAndDatePublishEndAndIndustryIDToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, int industryID)
        {
            List<CodeDataReport> list = _codeDataRepository.GetReportByDatePublishBeginAndDatePublishEndAndIndustryIDToList(datePublishBegin, datePublishEnd, industryID);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetReportByDatePublishBeginAndDatePublishEndToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd)
        {
            List<CodeDataReport> list = _codeDataRepository.GetReportByDatePublishBeginAndDatePublishEndToList(datePublishBegin, datePublishEnd);
            return Json(list.ToDataSourceResult(request));
        }

        public ActionResult GetReportByDatePublishBeginAndDatePublishEndAndIsUploadToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, bool isUpload)
        {
            List<CodeDataReport> list = _codeDataRepository.GetReportByDatePublishBeginAndDatePublishEndAndIsUploadToList(datePublishBegin, datePublishEnd, isUpload);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetReportEmployeeByDateUpdatedBeginAndDateUpdatedEndToList([DataSourceRequest] DataSourceRequest request, DateTime dateUpdatedBegin, DateTime dateUpdatedEnd)
        {
            List<CodeDataReport> list = _codeDataRepository.GetReportEmployeeByDateUpdatedBeginAndDateUpdatedEndToList(dateUpdatedBegin, dateUpdatedEnd);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetReportIndustryByDateUpdatedBeginAndDateUpdatedEndAndEmployeeIDToList([DataSourceRequest] DataSourceRequest request, DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int employeeID)
        {
            List<CodeDataReport> list = _codeDataRepository.GetReportIndustryByDateUpdatedBeginAndDateUpdatedEndAndEmployeeIDToList(dateUpdatedBegin, dateUpdatedEnd, employeeID);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetReportCompanyNameByDateUpdatedBeginAndDateUpdatedEndAndEmployeeIDToList([DataSourceRequest] DataSourceRequest request, DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int employeeID)
        {
            List<CodeDataReport> list = _codeDataRepository.GetReportCompanyNameByDateUpdatedBeginAndDateUpdatedEndAndEmployeeIDToList(dateUpdatedBegin, dateUpdatedEnd, employeeID);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetReportCompanyNameByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDToList([DataSourceRequest] DataSourceRequest request, DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int industryID)
        {
            List<CodeDataReport> list = _codeDataRepository.GetReportCompanyNameByDateUpdatedBeginAndDateUpdatedEndAndIndustryIDToList(dateUpdatedBegin, dateUpdatedEnd, industryID);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetReportCompanyNameByDateUpdatedBeginAndDateUpdatedEndAndEmployeeIDAndIndustryIDToList([DataSourceRequest] DataSourceRequest request, DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int employeeID, int industryID)
        {
            List<CodeDataReport> list = _codeDataRepository.GetReportCompanyNameByDateUpdatedBeginAndDateUpdatedEndAndEmployeeIDAndIndustryIDToList(dateUpdatedBegin, dateUpdatedEnd, employeeID, industryID);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetReportByDatePublishBeginAndDatePublishEndAndIndustryIDAndIsUploadToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, int industryID, bool isUpload)
        {
            List<CodeDataReport> list = _codeDataRepository.GetReportByDatePublishBeginAndDatePublishEndAndIndustryIDAndIsUploadToList(datePublishBegin, datePublishEnd, industryID, isUpload);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetByDatePublishBeginAndDatePublishEndAndIndustryIDToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, int industryID)
        {
            var cookieExpires = new CookieOptions();
            cookieExpires.Expires = DateTime.Now.AddMonths(1);
            Response.Cookies.Append("CodeDataIndustryID", industryID.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataDatePublishBegin", datePublishBegin.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataDatePublishEnd", datePublishEnd.ToString("MM/dd/yyyy"), cookieExpires);
            List<CodeData> list = _codeDataRepository.GetByDatePublishBeginAndDatePublishEndAndIndustryIDToList(datePublishBegin, datePublishEnd, industryID);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, int industryID)
        {
            var cookieExpires = new CookieOptions();
            cookieExpires.Expires = DateTime.Now.AddMonths(1);
            Response.Cookies.Append("CodeDataIndustryID", industryID.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataDatePublishBegin", datePublishBegin.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataDatePublishEnd", datePublishEnd.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataAction", "1", cookieExpires);
            List<CodeData> list = _codeDataRepository.GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDToList(datePublishBegin, datePublishEnd, industryID, RequestUserID);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndIsPublishToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, int industryID, bool isPublish)
        {
            var cookieExpires = new CookieOptions();
            cookieExpires.Expires = DateTime.Now.AddMonths(1);
            Response.Cookies.Append("CodeDataIndustryID", industryID.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataDatePublishBegin", datePublishBegin.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataDatePublishEnd", datePublishEnd.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataIsPublish", isPublish.ToString(), cookieExpires);
            List<CodeData> list = _codeDataRepository.GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndIsPublishToList(datePublishBegin, datePublishEnd, industryID, RequestUserID, isPublish);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndIsUploadToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, int industryID, bool isUpload)
        {
            var cookieExpires = new CookieOptions();
            cookieExpires.Expires = DateTime.Now.AddMonths(1);
            Response.Cookies.Append("CodeDataIndustryID", industryID.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataDatePublishBegin", datePublishBegin.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataDatePublishEnd", datePublishEnd.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataIsUpload", isUpload.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataAction", "1", cookieExpires);
            List<CodeData> list = _codeDataRepository.GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndIsUploadToList(datePublishBegin, datePublishEnd, industryID, RequestUserID, isUpload).OrderBy(item => item.IsCoding).ToList();
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetByDateUpdatedBeginAndDateUpdatedEndAndEmployeeIDAndIsFilterToList([DataSourceRequest] DataSourceRequest request, DateTime dateUpdatedBegin, DateTime dateUpdatedEnd)
        {
            List<CodeData> list = _codeDataRepository.GetByDateUpdatedBeginAndDateUpdatedEndAndEmployeeIDAndIsFilterToList(dateUpdatedBegin, dateUpdatedEnd, RequestUserID, true);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndIsUploadAndSourceIsNewspageAndTVToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, int industryID, bool isUpload)
        {
            var cookieExpires = new CookieOptions();
            cookieExpires.Expires = DateTime.Now.AddMonths(1);
            Response.Cookies.Append("CodeDataIndustryID", industryID.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataDatePublishBegin", datePublishBegin.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataDatePublishEnd", datePublishEnd.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataIsUpload", isUpload.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataAction", "2", cookieExpires);
            List<CodeData> list = _codeDataRepository.GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndIsUploadAndSourceIsNewspageAndTVToList(datePublishBegin, datePublishEnd, industryID, RequestUserID, isUpload, AppGlobal.Newspage, AppGlobal.TV);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndSourceIsNewspageAndTVToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, int industryID, bool isUpload)
        {
            var cookieExpires = new CookieOptions();
            cookieExpires.Expires = DateTime.Now.AddMonths(1);
            Response.Cookies.Append("CodeDataIndustryID", industryID.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataDatePublishBegin", datePublishBegin.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataDatePublishEnd", datePublishEnd.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataAction", "2", cookieExpires);
            List<CodeData> list = _codeDataRepository.GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndSourceIsNewspageAndTVToList(datePublishBegin, datePublishEnd, industryID, RequestUserID, AppGlobal.Newspage, AppGlobal.TV);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndIsUploadAndSourceIsNotNewspageAndTVToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, int industryID, bool isUpload)
        {
            var cookieExpires = new CookieOptions();
            cookieExpires.Expires = DateTime.Now.AddMonths(1);
            Response.Cookies.Append("CodeDataIndustryID", industryID.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataDatePublishBegin", datePublishBegin.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataDatePublishEnd", datePublishEnd.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataIsUpload", isUpload.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataAction", "3", cookieExpires);
            List<CodeData> list = _codeDataRepository.GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndIsUploadAndSourceIsNotNewspageAndTVToList(datePublishBegin, datePublishEnd, industryID, RequestUserID, isUpload, AppGlobal.Newspage, AppGlobal.TV);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndSourceIsNotNewspageAndTVToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, int industryID, bool isUpload)
        {
            var cookieExpires = new CookieOptions();
            cookieExpires.Expires = DateTime.Now.AddMonths(1);
            Response.Cookies.Append("CodeDataIndustryID", industryID.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataDatePublishBegin", datePublishBegin.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataDatePublishEnd", datePublishEnd.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataAction", "3", cookieExpires);
            List<CodeData> list = _codeDataRepository.GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndSourceIsNotNewspageAndTVToList(datePublishBegin, datePublishEnd, industryID, RequestUserID, AppGlobal.Newspage, AppGlobal.TV);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetByDateUpdatedBeginAndDateUpdatedEndAndSourceIsNewspageAndTVToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd)
        {
            var cookieExpires = new CookieOptions();
            cookieExpires.Expires = DateTime.Now.AddMonths(1);
            Response.Cookies.Append("CodeDataDatePublishBegin", datePublishBegin.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataDatePublishEnd", datePublishEnd.ToString("MM/dd/yyyy"), cookieExpires);
            List<CodeData> list = _codeDataRepository.GetByDateUpdatedBeginAndDateUpdatedEndAndSourceIsNewspageAndTVToList(datePublishBegin, datePublishEnd, AppGlobal.Newspage, AppGlobal.TV);
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
            _productRepository.UpdateSingleItemByCodeData(model);
            model.UserUpdated = RequestUserID;
            _productPropertyRepository.UpdateItemsByCodeDataCopyVersion(model);
            string actionMessage = "";
            if ((!string.IsNullOrEmpty(model.TitleProperty)) && (model.SourceProperty > 0))
            {
                _productPropertyRepository.Delete(model.ProductPropertyID.Value);
                List<ProductProperty> list = _productPropertyRepository.GetTitleAndSourceToList(model.TitleProperty, model.SourceProperty.Value);
                foreach (ProductProperty productProperty in list)
                {
                    productProperty.ID = 0;
                    productProperty.TitleProperty = model.TitleProperty;
                    productProperty.SourceProperty = model.SourceProperty;
                    productProperty.FileName = "";
                    productProperty.MediaTitle = "";
                    productProperty.MediaType = "";
                    productProperty.ParentID = model.ProductID;
                    productProperty.Source = model.Source;
                    productProperty.IsCoding = true;
                    productProperty.DateCoding = DateTime.Now;
                    productProperty.Initialization(InitType.Insert, RequestUserID);
                    _productPropertyRepository.Create(productProperty);
                    model.ProductPropertyID = productProperty.ID;
                }
            }
            else
            {
                bool check = true;
                Config industry = _configResposistory.GetByID(model.IndustryID.Value);
                if (industry != null)
                {
                    if (industry.Active == false)
                    {
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
                        if (!string.IsNullOrEmpty(model.ProductName_ProjectName))
                        {
                            if (model.CategorySub.Contains("industry") || model.CategorySub.Contains("corporate") || model.CategorySub.Contains("company") || model.CategorySub.Contains("competitor"))
                            {
                                check = false;
                                actionMessage = AppGlobal.Error + " - ProductName exist but Category Sub not relate to Product";
                            }
                        }
                    }
                }
                if (check == true)
                {
                    model.IsCoding = true;
                    model.UserUpdated = RequestUserID;
                    _productPropertyRepository.UpdateItemsByCodeDataCopyVersion(model);
                    _productPropertyRepository.UpdateItemsByIDAndRequestUserIDAndProductFeatureListAndCode(model.ProductPropertyID.Value, RequestUserID, model.ProductFeatureList, AppGlobal.ProductFeature);
                }
            }
            return actionMessage;
        }
        public int Copy(int productPropertyID)
        {
            CodeData model = GetCodeData(productPropertyID);
            _productPropertyRepository.InsertSingleItemByCopyCodeData(model.ProductPropertyID.Value, RequestUserID);
            return productPropertyID;
        }
        public IActionResult SaveCoding(CodeData model)
        {
            string actionMessage = CheckCodeData(model);
            return RedirectToAction("Detail", "CodeData", new { ProductPropertyID = model.ProductPropertyID, ActionMessage = actionMessage });
        }
        public IActionResult SaveCodingDetailBasic(CodeData model)
        {
            string actionMessage = CheckCodeData(model);
            return RedirectToAction("DetailBasic", "CodeData", new { ProductPropertyID = model.ProductPropertyID, ActionMessage = actionMessage });
        }
        public int CopyURLSame(int productPropertyID)
        {
            productPropertyID = _productPropertyRepository.InsertSingleItemByCopyCodeData(productPropertyID, RequestUserID);
            return productPropertyID;
        }
        public int CopyURLAnother(int productPropertyID)
        {
            _productPropertyRepository.InsertItemsByCopyCodeData(productPropertyID, RequestUserID);
            CodeData model = GetCodeData(productPropertyID);
            return model.RowNext.Value;
        }
        public int BasicCopyURLSame(int productPropertyID)
        {
            productPropertyID = _productPropertyRepository.InsertSingleItemByCopyCodeData(productPropertyID, RequestUserID);
            return productPropertyID;
        }
        public int BasicCopyURLAnother(int productPropertyID)
        {
            _productPropertyRepository.InsertItemsByCopyCodeData(productPropertyID, RequestUserID);
            CodeData model = GetCodeData(productPropertyID);
            return model.RowNext.Value;
        }
        public IActionResult ExportExcelEnglish()
        {
            return Json("");
        }
        private CodeData GetCodeData(int productPropertyID)
        {
            CodeData model = new CodeData();
            if (productPropertyID > 0)
            {
                DateTime datePublishBegin = DateTime.Now;
                DateTime datePublishEnd = DateTime.Now;
                int industryID = 0;
                try
                {
                    industryID = int.Parse(Request.Cookies["CodeDataIndustryID"]);
                    datePublishBegin = DateTime.Parse(Request.Cookies["CodeDataDatePublishBegin"]);
                    datePublishEnd = DateTime.Parse(Request.Cookies["CodeDataDatePublishEnd"]);
                    string codeDataAction = Request.Cookies["CodeDataAction"];
                    List<CodeData> list = new List<CodeData>();
                    switch (codeDataAction)
                    {
                        case "1":
                            list = _codeDataRepository.GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDToList(datePublishBegin, datePublishEnd, industryID, RequestUserID);
                            break;
                        case "2":
                            list = _codeDataRepository.GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndSourceIsNewspageAndTVToList(datePublishBegin, datePublishEnd, industryID, RequestUserID, AppGlobal.Newspage, AppGlobal.TV);
                            break;
                        case "3":
                            list = _codeDataRepository.GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndSourceIsNotNewspageAndTVToList(datePublishBegin, datePublishEnd, industryID, RequestUserID, AppGlobal.Newspage, AppGlobal.TV);
                            break;
                    }
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (productPropertyID == list[i].ProductPropertyID)
                        {
                            model = list[i];
                            i = list.Count;
                        }
                    }
                    if ((model == null) || (string.IsNullOrEmpty(model.Title)))
                    {
                        model = _codeDataRepository.GetByProductPropertyID(productPropertyID);
                    }
                    if ((model == null) || (string.IsNullOrEmpty(model.Title)))
                    {
                        model = new CodeData();
                    }
                    if (model != null)
                    {
                        if (!string.IsNullOrEmpty(model.Title))
                        {
                            model.CompanyNameHiden = _codeDataRepository.GetCompanyNameByURLCode(model.URLCode);
                            model.ProductNameHiden = _codeDataRepository.GetProductNameByURLCode(model.URLCode);
                            model.CategorySubHiden = _codeDataRepository.GetCategorySubByURLCode(model.URLCode);
                        }
                    }
                    model.RowNext = 0;
                    List<CodeData> listIsCoding = list.Where(item => item.IsCoding == false || item.IsCoding == null).ToList();
                    if (listIsCoding.Count > 0)
                    {
                        model.RowNext = listIsCoding[0].ProductPropertyID;
                        if (string.IsNullOrEmpty(model.Title))
                        {
                            model.ProductPropertyID = model.RowNext;
                        }
                    }
                    model.RowIndexCount = listIsCoding.Count;
                    model.RowCount = list.Count;
                    _productPropertyRepository.InsertItemsByIDAndRequestUserIDAndCode(productPropertyID, RequestUserID, AppGlobal.ProductFeature);
                }
                catch
                {
                }
            }
            return model;
        }
        public IActionResult DeleteProductProperty(int productPropertyID)
        {
            string note = AppGlobal.InitString;
            int result = 0;
            ProductProperty model = _productPropertyRepository.GetByID(productPropertyID);
            if (model.ID > 0)
            {
                if ((model.IsCoding == false) || (model.IsCoding == null))
                {
                    result = _productPropertyRepository.Delete(productPropertyID);
                }
            }
            result = 1;
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
        public IActionResult DeleteProductPropertyCode(int productPropertyID)
        {
            string note = AppGlobal.InitString;
            int result = 0;
            result = _productPropertyRepository.Delete(productPropertyID);
            result = 1;
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
        public ActionResult GetByDateUpdatedBeginAndDateUpdatedEndAndHourBeginAndHourEndAndIndustryIDToList([DataSourceRequest] DataSourceRequest request, DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int hourBegin, int hourEnd, int industryID)
        {
            List<CodeData> list = _codeDataRepository.GetByDateUpdatedBeginAndDateUpdatedEndAndHourBeginAndHourEndAndIndustryIDToList(dateUpdatedBegin, dateUpdatedEnd, hourBegin, hourEnd, industryID);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetDailyByDateUpdatedBeginAndDateUpdatedEndAndHourBeginAndHourEndAndIndustryIDAndIsCodingToList([DataSourceRequest] DataSourceRequest request, DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int hourBegin, int hourEnd, int industryID)
        {
            var cookieExpires = new CookieOptions();
            cookieExpires.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("CodeDataDailyDatePublishBegin", dateUpdatedBegin.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataDailyDatePublishEnd", dateUpdatedEnd.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataDailyHourBegin", hourBegin.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataDailyHourEnd", hourEnd.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataDailyIndustryID", industryID.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataDailyIsCoding", true.ToString(), cookieExpires);
            List<CodeData> list = _codeDataRepository.GetDailyByDateUpdatedBeginAndDateUpdatedEndAndHourBeginAndHourEndAndIndustryIDAndIsCodingToList(dateUpdatedBegin, dateUpdatedEnd, hourBegin, hourEnd, industryID, true);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetDailyByDateUpdatedBeginAndDateUpdatedEndAndHourBeginAndHourEndAndIndustryIDToList([DataSourceRequest] DataSourceRequest request, DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int hourBegin, int hourEnd, int industryID)
        {
            var cookieExpires = new CookieOptions();
            cookieExpires.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("CodeDataDailyDatePublishBegin", dateUpdatedBegin.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataDailyDatePublishEnd", dateUpdatedEnd.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataDailyHourBegin", hourBegin.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataDailyHourEnd", hourEnd.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataDailyIndustryID", industryID.ToString(), cookieExpires);
            List<CodeData> list = _codeDataRepository.GetDailyByDateUpdatedBeginAndDateUpdatedEndAndHourBeginAndHourEndAndIndustryIDToList(dateUpdatedBegin, dateUpdatedEnd, hourBegin, hourEnd, industryID);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetDailyByDatePublishBeginAndDatePublishEndAndHourBeginAndHourEndAndIndustryIDToList([DataSourceRequest] DataSourceRequest request, DateTime dateUpdatedBegin, DateTime dateUpdatedEnd, int hourBegin, int hourEnd, int industryID)
        {
            var cookieExpires = new CookieOptions();
            cookieExpires.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("CodeDataDailyDatePublishBegin", dateUpdatedBegin.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataDailyDatePublishEnd", dateUpdatedEnd.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataDailyHourBegin", hourBegin.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataDailyHourEnd", hourEnd.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataDailyIndustryID", industryID.ToString(), cookieExpires);
            List<CodeData> list = _codeDataRepository.GetDailyByDatePublishBeginAndDatePublishEndAndHourBeginAndHourEndAndIndustryIDToList(dateUpdatedBegin, dateUpdatedEnd, hourBegin, hourEnd, industryID);
            return Json(list.ToDataSourceResult(request));
        }
        public IActionResult DeleteProductPropertyByID(int ProductPropertyID)
        {
            string note = AppGlobal.InitString;
            _productPropertyRepository.DeleteItemsByIDCodeData(ProductPropertyID);
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
        public IActionResult UpdateProduct(CodeData model)
        {
            int result = 0;
            string note = AppGlobal.InitString;
            Product product = _productRepository.GetByID(model.ProductID.Value);
            if (product != null)
            {
                if (product.ID > 0)
                {
                    product.IsSummary = model.IsSummary;
                    product.Title = model.Title;
                    product.Description = model.Description;
                    product.TitleEnglish = model.TitleEnglish;
                    product.DescriptionEnglish = model.DescriptionEnglish;
                    product.Note = model.Note;
                    result = _productRepository.Update(product.ID, product);
                }
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.EditFail;
            }
            return Json(note);
        }
        public IActionResult Export001ExportExcelForDaily(CancellationToken cancellationToken)
        {
            List<CodeData> list = new List<CodeData>();
            string excelName = @"Daily_" + AppGlobal.DateTimeCode + ".xlsx";
            string sheetName = AppGlobal.DateTimeCode;
            try
            {
                string industryName = "";
                DateTime dateUpdatedBegin = DateTime.Parse(Request.Cookies["CodeDataDailyDatePublishBegin"]);
                DateTime dateUpdatedEnd = DateTime.Parse(Request.Cookies["CodeDataDailyDatePublishEnd"]);
                int hourBegin = int.Parse(Request.Cookies["CodeDataDailyHourBegin"]);
                int hourEnd = int.Parse(Request.Cookies["CodeDataDailyHourEnd"]);
                int industryID = int.Parse(Request.Cookies["CodeDataDailyIndustryID"]);
                bool isCoding = bool.Parse(Request.Cookies["CodeDataDailyIsCoding"]);
                list = _codeDataRepository.GetDailyByDateUpdatedBeginAndDateUpdatedEndAndHourBeginAndHourEndAndIndustryIDAndIsCodingToList(dateUpdatedBegin, dateUpdatedEnd, hourBegin, hourEnd, industryID, isCoding);
                Config industry = _configResposistory.GetByID(industryID);
                if (industry != null)
                {
                    industryName = industry.CodeName;
                }
                sheetName = industryName;
                industryName = AppGlobal.SetName(industryName);
                excelName = @"Daily_" + industryName + "_" + dateUpdatedBegin.ToString("yyyyMMdd") + "_" + dateUpdatedEnd.ToString("yyyyMMdd") + "_" + AppGlobal.DateTimeCode + ".xlsx";
            }
            catch
            {
            }
            var stream = new MemoryStream();
            Color color = Color.FromArgb(int.Parse("#c00000".Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier));
            Color colorTitle = Color.FromArgb(int.Parse("#ed7d31".Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier));
            List<Config> listDailyReportColumn = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.DailyReportColumn);
            List<CodeData> listISummary = list.Where(item => item.IsSummary == true).ToList();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add(sheetName);
                if (list.Count > 0)
                {
                    int column = 1;
                    int rowExcel = 1;
                    workSheet.Cells[rowExcel, 5].Value = "DAILY REPORT (" + DateTime.Now.ToString("dd/MM/yyyy") + ")";
                    workSheet.Cells[rowExcel, 5].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, 5].Style.Font.Size = 12;
                    workSheet.Cells[rowExcel, 5].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, 5].Style.Font.Color.SetColor(color);
                    rowExcel = rowExcel + 1;
                    if (listISummary.Count > 0)
                    {
                        workSheet.Cells[rowExcel, 1].Value = "I - HIGHLIGHT NEWS OF THE DAY";
                        workSheet.Cells[rowExcel, 1].Style.Font.Bold = true;
                        workSheet.Cells[rowExcel, 1].Style.Font.Size = 12;
                        workSheet.Cells[rowExcel, 1].Style.Font.Name = "Times New Roman";
                        workSheet.Cells[rowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        workSheet.Cells[rowExcel, 1].Style.Font.Color.SetColor(colorTitle);
                        workSheet.Cells[rowExcel, 1, rowExcel, 3].Merge = true;
                        rowExcel = 3;
                        foreach (CodeData data in listISummary)
                        {
                            if (data.IsSummary == true)
                            {
                                workSheet.Cells[rowExcel, 1].Value = "" + data.CompanyName + ": ";
                                workSheet.Cells[rowExcel, 1].Style.Font.Bold = true;
                                workSheet.Cells[rowExcel, 1].Style.Font.Size = 11;
                                workSheet.Cells[rowExcel, 1].Style.Font.Name = "Times New Roman";
                                workSheet.Cells[rowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                workSheet.Cells[rowExcel, 2].Value = "" + data.TitleEnglish;
                                if (!string.IsNullOrEmpty(data.Title))
                                {
                                    workSheet.Cells[rowExcel, 2].Hyperlink = new Uri(data.URLCode);
                                    workSheet.Cells[rowExcel, 2].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                }
                                workSheet.Cells[rowExcel, 2].Style.Font.Bold = true;
                                workSheet.Cells[rowExcel, 2].Style.Font.Size = 11;
                                workSheet.Cells[rowExcel, 2].Style.Font.Name = "Times New Roman";
                                workSheet.Cells[rowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                workSheet.Cells[rowExcel, 3].Value = "(" + data.MediaTitle + " - " + data.DatePublish.ToString("MM/dd/yyyy") + ")";
                                workSheet.Cells[rowExcel, 3].Style.Font.Bold = true;
                                workSheet.Cells[rowExcel, 3].Style.Font.Size = 11;
                                workSheet.Cells[rowExcel, 3].Style.Font.Name = "Times New Roman";
                                workSheet.Cells[rowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                if (!string.IsNullOrEmpty(data.Description))
                                {
                                    rowExcel = rowExcel + 1;
                                    workSheet.Cells[rowExcel, 1].Value = "" + data.DescriptionEnglish;
                                    workSheet.Cells[rowExcel, 1].Style.Font.Bold = true;
                                    workSheet.Cells[rowExcel, 1].Style.Font.Size = 11;
                                    workSheet.Cells[rowExcel, 1].Style.Font.Name = "Times New Roman";
                                    workSheet.Cells[rowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    workSheet.Cells[rowExcel, 1, rowExcel, 3].Merge = true;
                                    rowExcel = rowExcel + 1;
                                }
                                rowExcel = rowExcel + 1;
                            }
                        }
                        workSheet.Cells[rowExcel, 1].Value = "II - INFORMATION";
                        workSheet.Cells[rowExcel, 1].Style.Font.Bold = true;
                        workSheet.Cells[rowExcel, 1].Style.Font.Size = 12;
                        workSheet.Cells[rowExcel, 1].Style.Font.Name = "Times New Roman";
                        workSheet.Cells[rowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        workSheet.Cells[rowExcel, 1].Style.Font.Color.SetColor(colorTitle);
                        workSheet.Cells[rowExcel, 1, rowExcel, 3].Merge = true;
                        rowExcel = rowExcel + 1;
                    }
                    foreach (Config item in listDailyReportColumn)
                    {
                        workSheet.Cells[rowExcel, column].Value = item.CodeName;
                        workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                        workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                        workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                        workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                        workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                        workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                        workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                        workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                        workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                        column = column + 1;
                    }

                    workSheet.Cells[rowExcel, column].Value = "Note";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    column = column + 1;
                    workSheet.Cells[rowExcel, column].Value = "Upload";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    column = column + 1;
                    workSheet.Cells[rowExcel, column].Value = "URL";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    int index = 0;
                    rowExcel = rowExcel + 1;
                    for (int row = rowExcel; row <= list.Count + rowExcel - 1; row++)
                    {
                        for (int i = 1; i <= column; i++)
                        {
                            if (i == 1)
                            {
                                workSheet.Cells[row, i].Value = list[index].DatePublish;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                workSheet.Cells[row, i].Style.Numberformat.Format = "mm/dd/yyyy";
                            }
                            if (i == 2)
                            {
                                workSheet.Cells[row, i].Value = list[index].CategoryMain;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 3)
                            {
                                workSheet.Cells[row, i].Value = list[index].Segment;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 4)
                            {
                                workSheet.Cells[row, i].Value = list[index].CategorySub;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 5)
                            {
                                if (!string.IsNullOrEmpty(list[index].CompanyName))
                                {
                                    workSheet.Cells[row, i].Value = list[index].CompanyName;
                                }
                                else
                                {
                                    workSheet.Cells[row, i].Value = list[index].CategoryMain;
                                }
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 6)
                            {
                                workSheet.Cells[row, i].Value = list[index].ProductName_ProjectName;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 7)
                            {
                                workSheet.Cells[row, i].Value = list[index].SentimentCorp;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 8)
                            {
                                workSheet.Cells[row, i].Value = list[index].Title;
                                if ((!string.IsNullOrEmpty(list[index].Title)) && (!string.IsNullOrEmpty(list[index].URLCode)))
                                {
                                    try
                                    {
                                        workSheet.Cells[row, i].Hyperlink = new Uri(list[index].URLCode);
                                    }
                                    catch
                                    {

                                    }
                                    workSheet.Cells[row, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                }
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 9)
                            {
                                workSheet.Cells[row, i].Value = list[index].TitleEnglish;
                                if ((!string.IsNullOrEmpty(list[index].TitleEnglish)) && (!string.IsNullOrEmpty(list[index].URLCode)))
                                {
                                    try
                                    {
                                        workSheet.Cells[row, i].Hyperlink = new Uri(list[index].URLCode);
                                    }
                                    catch
                                    {

                                    }
                                    workSheet.Cells[row, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                }
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 10)
                            {
                                workSheet.Cells[row, i].Value = list[index].MediaTitle;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 11)
                            {
                                workSheet.Cells[row, i].Value = list[index].MediaType;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 12)
                            {
                                workSheet.Cells[row, i].Value = list[index].Page;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            }
                            if (i == 13)
                            {
                                workSheet.Cells[row, i].Value = list[index].Advalue.Value.ToString("N0");
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            }
                            if (i == 14)
                            {
                                workSheet.Cells[row, i].Value = list[index].DescriptionEnglish;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 15)
                            {
                                workSheet.Cells[row, i].Value = list[index].Duration;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 16)
                            {
                                workSheet.Cells[row, i].Value = list[index].Frequency;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 17)
                            {
                                workSheet.Cells[row, i].Value = list[index].Note;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 18)
                            {
                                workSheet.Cells[row, i].Value = list[index].DateUpdated;
                                workSheet.Cells[row, i].Style.Numberformat.Format = "mm/dd/yyyy HH:mm:ss";
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            }
                            if (i == 19)
                            {
                                if (!string.IsNullOrEmpty(list[index].URLCode))
                                {
                                    try
                                    {
                                        workSheet.Cells[row, column].Value = list[index].URLCode;
                                        workSheet.Cells[row, column].Hyperlink = new Uri(list[index].URLCode);
                                    }
                                    catch
                                    {

                                    }
                                }
                                workSheet.Cells[row, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            workSheet.Cells[row, i].Style.Font.Name = "Times New Roman";
                            workSheet.Cells[row, i].Style.Font.Size = 11;
                            workSheet.Cells[row, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[row, i].Style.Border.Top.Color.SetColor(Color.Black);
                            workSheet.Cells[row, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[row, i].Style.Border.Left.Color.SetColor(Color.Black);
                            workSheet.Cells[row, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[row, i].Style.Border.Right.Color.SetColor(Color.Black);
                            workSheet.Cells[row, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[row, i].Style.Border.Bottom.Color.SetColor(Color.Black);
                        }

                        index = index + 1;
                    }
                    for (int i = 1; i <= column; i++)
                    {
                        workSheet.Column(i).AutoFit();
                    }
                }
                package.Save();
            }
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        public IActionResult Export001ExportExcelForDailyVietnamese(CancellationToken cancellationToken)
        {
            List<CodeData> list = new List<CodeData>();
            string excelName = @"Daily_" + AppGlobal.DateTimeCode + ".xlsx";
            string sheetName = AppGlobal.DateTimeCode;
            try
            {
                string industryName = "";
                DateTime dateUpdatedBegin = DateTime.Parse(Request.Cookies["CodeDataDailyDatePublishBegin"]);
                DateTime dateUpdatedEnd = DateTime.Parse(Request.Cookies["CodeDataDailyDatePublishEnd"]);
                int hourBegin = int.Parse(Request.Cookies["CodeDataDailyHourBegin"]);
                int hourEnd = int.Parse(Request.Cookies["CodeDataDailyHourEnd"]);
                int industryID = int.Parse(Request.Cookies["CodeDataDailyIndustryID"]);
                bool isCoding = bool.Parse(Request.Cookies["CodeDataDailyIsCoding"]);
                list = _codeDataRepository.GetDailyByDateUpdatedBeginAndDateUpdatedEndAndHourBeginAndHourEndAndIndustryIDAndIsCodingToList(dateUpdatedBegin, dateUpdatedEnd, hourBegin, hourEnd, industryID, isCoding);
                Config industry = _configResposistory.GetByID(industryID);
                if (industry != null)
                {
                    industryName = industry.CodeName;
                }
                sheetName = industryName;
                industryName = AppGlobal.SetName(industryName);
                excelName = @"Daily_" + industryName + "_" + dateUpdatedBegin.ToString("yyyyMMdd") + "_" + dateUpdatedEnd.ToString("yyyyMMdd") + "_" + AppGlobal.DateTimeCode + ".xlsx";
            }
            catch
            {
            }
            var stream = new MemoryStream();
            Color color = Color.FromArgb(int.Parse("#c00000".Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier));
            Color colorTitle = Color.FromArgb(int.Parse("#ed7d31".Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier));
            List<Config> listDailyReportColumn = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.DailyReportColumn);
            List<CodeData> listISummary = list.Where(item => item.IsSummary == true).ToList();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add(sheetName);
                if (list.Count > 0)
                {
                    int column = 1;
                    int rowExcel = 1;
                    workSheet.Cells[rowExcel, 5].Value = "DAILY REPORT (" + DateTime.Now.ToString("dd/MM/yyyy") + ")";
                    workSheet.Cells[rowExcel, 5].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, 5].Style.Font.Size = 12;
                    workSheet.Cells[rowExcel, 5].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, 5].Style.Font.Color.SetColor(color);
                    rowExcel = rowExcel + 1;
                    if (listISummary.Count > 0)
                    {
                        workSheet.Cells[rowExcel, 1].Value = "I - HIGHLIGHT NEWS OF THE DAY";
                        workSheet.Cells[rowExcel, 1].Style.Font.Bold = true;
                        workSheet.Cells[rowExcel, 1].Style.Font.Size = 12;
                        workSheet.Cells[rowExcel, 1].Style.Font.Name = "Times New Roman";
                        workSheet.Cells[rowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        workSheet.Cells[rowExcel, 1].Style.Font.Color.SetColor(colorTitle);
                        workSheet.Cells[rowExcel, 1, rowExcel, 3].Merge = true;
                        rowExcel = 3;
                        foreach (CodeData data in listISummary)
                        {
                            if (data.IsSummary == true)
                            {
                                workSheet.Cells[rowExcel, 1].Value = "" + data.CompanyName + ": ";
                                workSheet.Cells[rowExcel, 1].Style.Font.Bold = true;
                                workSheet.Cells[rowExcel, 1].Style.Font.Size = 11;
                                workSheet.Cells[rowExcel, 1].Style.Font.Name = "Times New Roman";
                                workSheet.Cells[rowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                workSheet.Cells[rowExcel, 2].Value = "" + data.Title;
                                if (!string.IsNullOrEmpty(data.Title))
                                {
                                    workSheet.Cells[rowExcel, 2].Hyperlink = new Uri(data.URLCode);
                                    workSheet.Cells[rowExcel, 2].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                }
                                workSheet.Cells[rowExcel, 2].Style.Font.Bold = true;
                                workSheet.Cells[rowExcel, 2].Style.Font.Size = 11;
                                workSheet.Cells[rowExcel, 2].Style.Font.Name = "Times New Roman";
                                workSheet.Cells[rowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                workSheet.Cells[rowExcel, 3].Value = "(" + data.MediaTitle + " - " + data.DatePublish.ToString("MM/dd/yyyy") + ")";
                                workSheet.Cells[rowExcel, 3].Style.Font.Bold = true;
                                workSheet.Cells[rowExcel, 3].Style.Font.Size = 11;
                                workSheet.Cells[rowExcel, 3].Style.Font.Name = "Times New Roman";
                                workSheet.Cells[rowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                if (!string.IsNullOrEmpty(data.Description))
                                {
                                    rowExcel = rowExcel + 1;
                                    workSheet.Cells[rowExcel, 1].Value = "" + data.Description;
                                    workSheet.Cells[rowExcel, 1].Style.Font.Bold = true;
                                    workSheet.Cells[rowExcel, 1].Style.Font.Size = 11;
                                    workSheet.Cells[rowExcel, 1].Style.Font.Name = "Times New Roman";
                                    workSheet.Cells[rowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    workSheet.Cells[rowExcel, 1, rowExcel, 3].Merge = true;
                                    rowExcel = rowExcel + 1;
                                }
                                rowExcel = rowExcel + 1;
                            }
                        }
                        workSheet.Cells[rowExcel, 1].Value = "II - INFORMATION";
                        workSheet.Cells[rowExcel, 1].Style.Font.Bold = true;
                        workSheet.Cells[rowExcel, 1].Style.Font.Size = 12;
                        workSheet.Cells[rowExcel, 1].Style.Font.Name = "Times New Roman";
                        workSheet.Cells[rowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        workSheet.Cells[rowExcel, 1].Style.Font.Color.SetColor(colorTitle);
                        workSheet.Cells[rowExcel, 1, rowExcel, 3].Merge = true;
                        rowExcel = rowExcel + 1;
                    }
                    foreach (Config item in listDailyReportColumn)
                    {
                        workSheet.Cells[rowExcel, column].Value = item.Note;
                        workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                        workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                        workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                        workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                        workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                        workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                        workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                        workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                        workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                        column = column + 1;
                    }

                    workSheet.Cells[rowExcel, column].Value = "Note";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    column = column + 1;
                    workSheet.Cells[rowExcel, column].Value = "Upload";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    column = column + 1;
                    workSheet.Cells[rowExcel, column].Value = "URL";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    int index = 0;
                    rowExcel = rowExcel + 1;
                    for (int row = rowExcel; row <= list.Count + rowExcel - 1; row++)
                    {
                        for (int i = 1; i <= column; i++)
                        {
                            if (i == 1)
                            {
                                workSheet.Cells[row, i].Value = list[index].DatePublish;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                workSheet.Cells[row, i].Style.Numberformat.Format = "mm/dd/yyyy";
                            }
                            if (i == 2)
                            {
                                workSheet.Cells[row, i].Value = list[index].CategoryMainVietnamese;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 3)
                            {
                                workSheet.Cells[row, i].Value = list[index].Segment;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 4)
                            {
                                workSheet.Cells[row, i].Value = list[index].CategorySubVietnamese;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 5)
                            {
                                if (!string.IsNullOrEmpty(list[index].CompanyName))
                                {
                                    workSheet.Cells[row, i].Value = list[index].CompanyName;
                                }
                                else
                                {
                                    workSheet.Cells[row, i].Value = list[index].CategoryMain;
                                }
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 6)
                            {
                                workSheet.Cells[row, i].Value = list[index].ProductName_ProjectName;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 7)
                            {
                                workSheet.Cells[row, i].Value = list[index].SentimentCorpVietnamese;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 8)
                            {
                                workSheet.Cells[row, i].Value = list[index].Title;
                                if ((!string.IsNullOrEmpty(list[index].Title)) && (!string.IsNullOrEmpty(list[index].URLCode)))
                                {
                                    try
                                    {
                                        workSheet.Cells[row, i].Hyperlink = new Uri(list[index].URLCode);
                                    }
                                    catch
                                    {

                                    }
                                    workSheet.Cells[row, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                }
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 9)
                            {
                                workSheet.Cells[row, i].Value = list[index].TitleEnglish;
                                if ((!string.IsNullOrEmpty(list[index].TitleEnglish)) && (!string.IsNullOrEmpty(list[index].URLCode)))
                                {
                                    try
                                    {
                                        workSheet.Cells[row, i].Hyperlink = new Uri(list[index].URLCode);
                                    }
                                    catch
                                    {

                                    }
                                    workSheet.Cells[row, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                }
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 10)
                            {
                                workSheet.Cells[row, i].Value = list[index].MediaTitle;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 11)
                            {
                                workSheet.Cells[row, i].Value = list[index].MediaType;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 12)
                            {
                                workSheet.Cells[row, i].Value = list[index].Page;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            }
                            if (i == 13)
                            {
                                workSheet.Cells[row, i].Value = list[index].Advalue.Value.ToString("N0");
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            }
                            if (i == 14)
                            {
                                workSheet.Cells[row, i].Value = list[index].DescriptionEnglish;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 15)
                            {
                                workSheet.Cells[row, i].Value = list[index].Duration;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 16)
                            {
                                workSheet.Cells[row, i].Value = list[index].Frequency;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 17)
                            {
                                workSheet.Cells[row, i].Value = list[index].Note;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 18)
                            {
                                workSheet.Cells[row, i].Value = list[index].DateUpdated;
                                workSheet.Cells[row, i].Style.Numberformat.Format = "mm/dd/yyyy HH:mm:ss";
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            }
                            if (i == 19)
                            {
                                if (!string.IsNullOrEmpty(list[index].URLCode))
                                {
                                    try
                                    {
                                        workSheet.Cells[row, column].Value = list[index].URLCode;
                                        workSheet.Cells[row, column].Hyperlink = new Uri(list[index].URLCode);
                                    }
                                    catch
                                    {

                                    }
                                }
                                workSheet.Cells[row, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            workSheet.Cells[row, i].Style.Font.Name = "Times New Roman";
                            workSheet.Cells[row, i].Style.Font.Size = 11;
                            workSheet.Cells[row, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[row, i].Style.Border.Top.Color.SetColor(Color.Black);
                            workSheet.Cells[row, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[row, i].Style.Border.Left.Color.SetColor(Color.Black);
                            workSheet.Cells[row, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[row, i].Style.Border.Right.Color.SetColor(Color.Black);
                            workSheet.Cells[row, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[row, i].Style.Border.Bottom.Color.SetColor(Color.Black);
                        }

                        index = index + 1;
                    }
                    for (int i = 1; i <= column; i++)
                    {
                        workSheet.Column(i).AutoFit();
                    }
                }
                package.Save();
            }
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        public IActionResult Export001ExportExcelForDailyAll(CancellationToken cancellationToken)
        {
            List<CodeData> list = new List<CodeData>();
            string excelName = @"Daily_" + AppGlobal.DateTimeCode + ".xlsx";
            string sheetName = AppGlobal.DateTimeCode;
            try
            {
                string industryName = "";
                DateTime dateUpdatedBegin = DateTime.Parse(Request.Cookies["CodeDataDailyDatePublishBegin"]);
                DateTime dateUpdatedEnd = DateTime.Parse(Request.Cookies["CodeDataDailyDatePublishEnd"]);
                int hourBegin = int.Parse(Request.Cookies["CodeDataDailyHourBegin"]);
                int hourEnd = int.Parse(Request.Cookies["CodeDataDailyHourEnd"]);
                int industryID = int.Parse(Request.Cookies["CodeDataDailyIndustryID"]);
                list = _codeDataRepository.GetDailyByDateUpdatedBeginAndDateUpdatedEndAndHourBeginAndHourEndAndIndustryIDToList(dateUpdatedBegin, dateUpdatedEnd, hourBegin, hourEnd, industryID);
                Config industry = _configResposistory.GetByID(industryID);
                if (industry != null)
                {
                    industryName = industry.CodeName;
                }
                sheetName = industryName;
                industryName = AppGlobal.SetName(industryName);
                excelName = @"Daily_" + industryName + "_" + dateUpdatedBegin.ToString("yyyyMMdd") + "_" + dateUpdatedEnd.ToString("yyyyMMdd") + "_" + AppGlobal.DateTimeCode + ".xlsx";
            }
            catch
            {
            }
            var stream = new MemoryStream();
            Color color = Color.FromArgb(int.Parse("#c00000".Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier));
            Color colorTitle = Color.FromArgb(int.Parse("#ed7d31".Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier));
            List<Config> listDailyReportColumn = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.DailyReportColumn);
            List<CodeData> listISummary = list.Where(item => item.IsSummary == true).ToList();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add(sheetName);
                if (list.Count > 0)
                {
                    int column = 1;
                    int rowExcel = 1;
                    workSheet.Cells[rowExcel, 5].Value = "DAILY REPORT (" + DateTime.Now.ToString("dd/MM/yyyy") + ")";
                    workSheet.Cells[rowExcel, 5].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, 5].Style.Font.Size = 12;
                    workSheet.Cells[rowExcel, 5].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, 5].Style.Font.Color.SetColor(color);
                    rowExcel = rowExcel + 1;
                    if (listISummary.Count > 0)
                    {
                        workSheet.Cells[rowExcel, 1].Value = "I - HIGHLIGHT NEWS OF THE DAY";
                        workSheet.Cells[rowExcel, 1].Style.Font.Bold = true;
                        workSheet.Cells[rowExcel, 1].Style.Font.Size = 12;
                        workSheet.Cells[rowExcel, 1].Style.Font.Name = "Times New Roman";
                        workSheet.Cells[rowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        workSheet.Cells[rowExcel, 1].Style.Font.Color.SetColor(colorTitle);
                        workSheet.Cells[rowExcel, 1, rowExcel, 3].Merge = true;
                        rowExcel = 3;
                        foreach (CodeData data in listISummary)
                        {
                            if (data.IsSummary == true)
                            {
                                workSheet.Cells[rowExcel, 1].Value = "" + data.CompanyName + ": ";
                                workSheet.Cells[rowExcel, 1].Style.Font.Bold = true;
                                workSheet.Cells[rowExcel, 1].Style.Font.Size = 11;
                                workSheet.Cells[rowExcel, 1].Style.Font.Name = "Times New Roman";
                                workSheet.Cells[rowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                workSheet.Cells[rowExcel, 2].Value = "" + data.TitleEnglish;
                                if (!string.IsNullOrEmpty(data.Title))
                                {
                                    workSheet.Cells[rowExcel, 2].Hyperlink = new Uri(data.URLCode);
                                    workSheet.Cells[rowExcel, 2].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                }
                                workSheet.Cells[rowExcel, 2].Style.Font.Bold = true;
                                workSheet.Cells[rowExcel, 2].Style.Font.Size = 11;
                                workSheet.Cells[rowExcel, 2].Style.Font.Name = "Times New Roman";
                                workSheet.Cells[rowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                workSheet.Cells[rowExcel, 3].Value = "(" + data.MediaTitle + " - " + data.DatePublish.ToString("MM/dd/yyyy") + ")";
                                workSheet.Cells[rowExcel, 3].Style.Font.Bold = true;
                                workSheet.Cells[rowExcel, 3].Style.Font.Size = 11;
                                workSheet.Cells[rowExcel, 3].Style.Font.Name = "Times New Roman";
                                workSheet.Cells[rowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                if (!string.IsNullOrEmpty(data.Description))
                                {
                                    rowExcel = rowExcel + 1;
                                    workSheet.Cells[rowExcel, 1].Value = "" + data.DescriptionEnglish;
                                    workSheet.Cells[rowExcel, 1].Style.Font.Bold = true;
                                    workSheet.Cells[rowExcel, 1].Style.Font.Size = 11;
                                    workSheet.Cells[rowExcel, 1].Style.Font.Name = "Times New Roman";
                                    workSheet.Cells[rowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    workSheet.Cells[rowExcel, 1, rowExcel, 3].Merge = true;
                                    rowExcel = rowExcel + 1;
                                }
                                rowExcel = rowExcel + 1;
                            }
                        }
                        workSheet.Cells[rowExcel, 1].Value = "II - INFORMATION";
                        workSheet.Cells[rowExcel, 1].Style.Font.Bold = true;
                        workSheet.Cells[rowExcel, 1].Style.Font.Size = 12;
                        workSheet.Cells[rowExcel, 1].Style.Font.Name = "Times New Roman";
                        workSheet.Cells[rowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        workSheet.Cells[rowExcel, 1].Style.Font.Color.SetColor(colorTitle);
                        workSheet.Cells[rowExcel, 1, rowExcel, 3].Merge = true;
                        rowExcel = rowExcel + 1;
                    }
                    foreach (Config item in listDailyReportColumn)
                    {
                        workSheet.Cells[rowExcel, column].Value = item.CodeName;
                        workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                        workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                        workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                        workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                        workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                        workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                        workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                        workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                        workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                        column = column + 1;
                    }

                    workSheet.Cells[rowExcel, column].Value = "Note";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    column = column + 1;
                    workSheet.Cells[rowExcel, column].Value = "Upload";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    column = column + 1;
                    workSheet.Cells[rowExcel, column].Value = "URL";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    int index = 0;
                    rowExcel = rowExcel + 1;
                    for (int row = rowExcel; row <= list.Count + rowExcel - 1; row++)
                    {
                        try
                        {
                            for (int i = 1; i <= column; i++)
                            {
                                if (i == 1)
                                {
                                    workSheet.Cells[row, i].Value = list[index].DatePublish;
                                    workSheet.Cells[row, i].Style.Numberformat.Format = "mm/dd/yyyy";
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                }
                                if (i == 2)
                                {
                                    workSheet.Cells[row, i].Value = list[index].CategoryMain;
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                if (i == 3)
                                {
                                    workSheet.Cells[row, i].Value = list[index].Segment;
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                if (i == 4)
                                {
                                    workSheet.Cells[row, i].Value = list[index].CategorySub;
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                if (i == 5)
                                {
                                    if (!string.IsNullOrEmpty(list[index].CompanyName))
                                    {
                                        workSheet.Cells[row, i].Value = list[index].CompanyName;
                                    }
                                    else
                                    {
                                        workSheet.Cells[row, i].Value = list[index].CategoryMain;
                                    }
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                if (i == 6)
                                {
                                    workSheet.Cells[row, i].Value = list[index].ProductName_ProjectName;
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                if (i == 7)
                                {
                                    workSheet.Cells[row, i].Value = list[index].SentimentCorp;
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                if (i == 8)
                                {
                                    workSheet.Cells[row, i].Value = list[index].Title;
                                    if ((!string.IsNullOrEmpty(list[index].Title)) && (!string.IsNullOrEmpty(list[index].URLCode)))
                                    {
                                        try
                                        {
                                            workSheet.Cells[row, i].Hyperlink = new Uri(list[index].URLCode);
                                        }
                                        catch
                                        {

                                        }
                                        workSheet.Cells[row, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                    }
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                if (i == 9)
                                {
                                    workSheet.Cells[row, i].Value = list[index].TitleEnglish;
                                    if ((!string.IsNullOrEmpty(list[index].TitleEnglish)) && (!string.IsNullOrEmpty(list[index].URLCode)))
                                    {
                                        try
                                        {
                                            workSheet.Cells[row, i].Hyperlink = new Uri(list[index].URLCode);
                                        }
                                        catch
                                        {

                                        }
                                        workSheet.Cells[row, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                    }
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                if (i == 10)
                                {
                                    workSheet.Cells[row, i].Value = list[index].MediaTitle;
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                if (i == 11)
                                {
                                    workSheet.Cells[row, i].Value = list[index].MediaType;
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                if (i == 12)
                                {
                                    workSheet.Cells[row, i].Value = list[index].Page;
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                }
                                if (i == 13)
                                {
                                    if (list[index].Advalue != null)
                                    {
                                        workSheet.Cells[row, i].Value = list[index].Advalue.Value.ToString("N0");
                                    }
                                    else
                                    {
                                        workSheet.Cells[row, i].Value = "0";
                                    }
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                }
                                if (i == 14)
                                {
                                    workSheet.Cells[row, i].Value = list[index].DescriptionEnglish;
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                if (i == 15)
                                {
                                    workSheet.Cells[row, i].Value = list[index].Duration;
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                if (i == 16)
                                {
                                    workSheet.Cells[row, i].Value = list[index].Frequency;
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                if (i == 17)
                                {
                                    workSheet.Cells[row, i].Value = list[index].Note;
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                if (i == 18)
                                {
                                    workSheet.Cells[row, i].Value = list[index].DateUpdated;
                                    workSheet.Cells[row, i].Style.Numberformat.Format = "mm/dd/yyyy HH:mm:ss";
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                }
                                if (i == 19)
                                {
                                    if (!string.IsNullOrEmpty(list[index].URLCode))
                                    {
                                        try
                                        {
                                            workSheet.Cells[row, column].Value = list[index].URLCode;
                                            workSheet.Cells[row, column].Hyperlink = new Uri(list[index].URLCode);
                                        }
                                        catch
                                        {

                                        }
                                    }
                                    workSheet.Cells[row, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                workSheet.Cells[row, i].Style.Font.Name = "Times New Roman";
                                workSheet.Cells[row, i].Style.Font.Size = 11;
                                workSheet.Cells[row, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                workSheet.Cells[row, i].Style.Border.Top.Color.SetColor(Color.Black);
                                workSheet.Cells[row, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                workSheet.Cells[row, i].Style.Border.Left.Color.SetColor(Color.Black);
                                workSheet.Cells[row, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                workSheet.Cells[row, i].Style.Border.Right.Color.SetColor(Color.Black);
                                workSheet.Cells[row, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                workSheet.Cells[row, i].Style.Border.Bottom.Color.SetColor(Color.Black);
                            }
                            index = index + 1;
                        }
                        catch (Exception e)
                        {
                            string mes = e.Message;
                        }
                    }
                    for (int i = 1; i <= column; i++)
                    {
                        workSheet.Column(i).AutoFit();
                    }
                }
                package.Save();
            }
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        public IActionResult Export001ExportExcelForDailyAllVietnamese(CancellationToken cancellationToken)
        {
            List<CodeData> list = new List<CodeData>();
            string excelName = @"Daily_" + AppGlobal.DateTimeCode + ".xlsx";
            string sheetName = AppGlobal.DateTimeCode;
            try
            {
                string industryName = "";
                DateTime dateUpdatedBegin = DateTime.Parse(Request.Cookies["CodeDataDailyDatePublishBegin"]);
                DateTime dateUpdatedEnd = DateTime.Parse(Request.Cookies["CodeDataDailyDatePublishEnd"]);
                int hourBegin = int.Parse(Request.Cookies["CodeDataDailyHourBegin"]);
                int hourEnd = int.Parse(Request.Cookies["CodeDataDailyHourEnd"]);
                int industryID = int.Parse(Request.Cookies["CodeDataDailyIndustryID"]);
                list = _codeDataRepository.GetDailyByDateUpdatedBeginAndDateUpdatedEndAndHourBeginAndHourEndAndIndustryIDToList(dateUpdatedBegin, dateUpdatedEnd, hourBegin, hourEnd, industryID);
                Config industry = _configResposistory.GetByID(industryID);
                if (industry != null)
                {
                    industryName = industry.CodeName;
                }
                sheetName = industryName;
                industryName = AppGlobal.SetName(industryName);
                excelName = @"Daily_" + industryName + "_" + dateUpdatedBegin.ToString("yyyyMMdd") + "_" + dateUpdatedEnd.ToString("yyyyMMdd") + "_" + AppGlobal.DateTimeCode + ".xlsx";
            }
            catch
            {
            }
            var stream = new MemoryStream();
            Color color = Color.FromArgb(int.Parse("#c00000".Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier));
            Color colorTitle = Color.FromArgb(int.Parse("#ed7d31".Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier));
            List<Config> listDailyReportColumn = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.DailyReportColumn);
            List<CodeData> listISummary = list.Where(item => item.IsSummary == true).ToList();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add(sheetName);
                if (list.Count > 0)
                {
                    int column = 1;
                    int rowExcel = 1;
                    workSheet.Cells[rowExcel, 5].Value = "DAILY REPORT (" + DateTime.Now.ToString("dd/MM/yyyy") + ")";
                    workSheet.Cells[rowExcel, 5].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, 5].Style.Font.Size = 12;
                    workSheet.Cells[rowExcel, 5].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, 5].Style.Font.Color.SetColor(color);
                    rowExcel = rowExcel + 1;
                    if (listISummary.Count > 0)
                    {
                        workSheet.Cells[rowExcel, 1].Value = "I - HIGHLIGHT NEWS OF THE DAY";
                        workSheet.Cells[rowExcel, 1].Style.Font.Bold = true;
                        workSheet.Cells[rowExcel, 1].Style.Font.Size = 12;
                        workSheet.Cells[rowExcel, 1].Style.Font.Name = "Times New Roman";
                        workSheet.Cells[rowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        workSheet.Cells[rowExcel, 1].Style.Font.Color.SetColor(colorTitle);
                        workSheet.Cells[rowExcel, 1, rowExcel, 3].Merge = true;
                        rowExcel = 3;
                        foreach (CodeData data in listISummary)
                        {
                            if (data.IsSummary == true)
                            {
                                workSheet.Cells[rowExcel, 1].Value = "" + data.CompanyName + ": ";
                                workSheet.Cells[rowExcel, 1].Style.Font.Bold = true;
                                workSheet.Cells[rowExcel, 1].Style.Font.Size = 11;
                                workSheet.Cells[rowExcel, 1].Style.Font.Name = "Times New Roman";
                                workSheet.Cells[rowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                workSheet.Cells[rowExcel, 2].Value = "" + data.Title;
                                if (!string.IsNullOrEmpty(data.Title))
                                {
                                    workSheet.Cells[rowExcel, 2].Hyperlink = new Uri(data.URLCode);
                                    workSheet.Cells[rowExcel, 2].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                }
                                workSheet.Cells[rowExcel, 2].Style.Font.Bold = true;
                                workSheet.Cells[rowExcel, 2].Style.Font.Size = 11;
                                workSheet.Cells[rowExcel, 2].Style.Font.Name = "Times New Roman";
                                workSheet.Cells[rowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                workSheet.Cells[rowExcel, 3].Value = "(" + data.MediaTitle + " - " + data.DatePublish.ToString("MM/dd/yyyy") + ")";
                                workSheet.Cells[rowExcel, 3].Style.Font.Bold = true;
                                workSheet.Cells[rowExcel, 3].Style.Font.Size = 11;
                                workSheet.Cells[rowExcel, 3].Style.Font.Name = "Times New Roman";
                                workSheet.Cells[rowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                if (!string.IsNullOrEmpty(data.Description))
                                {
                                    rowExcel = rowExcel + 1;
                                    workSheet.Cells[rowExcel, 1].Value = "" + data.Description;
                                    workSheet.Cells[rowExcel, 1].Style.Font.Bold = true;
                                    workSheet.Cells[rowExcel, 1].Style.Font.Size = 11;
                                    workSheet.Cells[rowExcel, 1].Style.Font.Name = "Times New Roman";
                                    workSheet.Cells[rowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    workSheet.Cells[rowExcel, 1, rowExcel, 3].Merge = true;
                                    rowExcel = rowExcel + 1;
                                }
                                rowExcel = rowExcel + 1;
                            }
                        }
                        workSheet.Cells[rowExcel, 1].Value = "II - INFORMATION";
                        workSheet.Cells[rowExcel, 1].Style.Font.Bold = true;
                        workSheet.Cells[rowExcel, 1].Style.Font.Size = 12;
                        workSheet.Cells[rowExcel, 1].Style.Font.Name = "Times New Roman";
                        workSheet.Cells[rowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        workSheet.Cells[rowExcel, 1].Style.Font.Color.SetColor(colorTitle);
                        workSheet.Cells[rowExcel, 1, rowExcel, 3].Merge = true;
                        rowExcel = rowExcel + 1;
                    }
                    foreach (Config item in listDailyReportColumn)
                    {
                        workSheet.Cells[rowExcel, column].Value = item.Note;
                        workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                        workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                        workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                        workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                        workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                        workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                        workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                        workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                        workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                        column = column + 1;
                    }

                    workSheet.Cells[rowExcel, column].Value = "Note";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    column = column + 1;
                    workSheet.Cells[rowExcel, column].Value = "Upload";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    column = column + 1;
                    workSheet.Cells[rowExcel, column].Value = "URL";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    int index = 0;
                    rowExcel = rowExcel + 1;
                    for (int row = rowExcel; row <= list.Count + rowExcel - 1; row++)
                    {
                        try
                        {
                            for (int i = 1; i <= column; i++)
                            {
                                if (i == 1)
                                {
                                    workSheet.Cells[row, i].Value = list[index].DatePublish;
                                    workSheet.Cells[row, i].Style.Numberformat.Format = "mm/dd/yyyy";
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                }
                                if (i == 2)
                                {
                                    workSheet.Cells[row, i].Value = list[index].CategoryMainVietnamese;
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                if (i == 3)
                                {
                                    workSheet.Cells[row, i].Value = list[index].Segment;
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                if (i == 4)
                                {
                                    workSheet.Cells[row, i].Value = list[index].CategorySubVietnamese;
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                if (i == 5)
                                {
                                    if (!string.IsNullOrEmpty(list[index].CompanyName))
                                    {
                                        workSheet.Cells[row, i].Value = list[index].CompanyName;
                                    }
                                    else
                                    {
                                        workSheet.Cells[row, i].Value = list[index].CategoryMain;
                                    }
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                if (i == 6)
                                {
                                    workSheet.Cells[row, i].Value = list[index].ProductName_ProjectName;
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                if (i == 7)
                                {
                                    workSheet.Cells[row, i].Value = list[index].SentimentCorpVietnamese;
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                if (i == 8)
                                {
                                    workSheet.Cells[row, i].Value = list[index].Title;
                                    if ((!string.IsNullOrEmpty(list[index].Title)) && (!string.IsNullOrEmpty(list[index].URLCode)))
                                    {
                                        try
                                        {
                                            workSheet.Cells[row, i].Hyperlink = new Uri(list[index].URLCode);
                                        }
                                        catch
                                        {

                                        }
                                        workSheet.Cells[row, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                    }
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                if (i == 9)
                                {
                                    workSheet.Cells[row, i].Value = list[index].TitleEnglish;
                                    if ((!string.IsNullOrEmpty(list[index].TitleEnglish)) && (!string.IsNullOrEmpty(list[index].URLCode)))
                                    {
                                        try
                                        {
                                            workSheet.Cells[row, i].Hyperlink = new Uri(list[index].URLCode);
                                        }
                                        catch
                                        {

                                        }
                                        workSheet.Cells[row, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                    }
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                if (i == 10)
                                {
                                    workSheet.Cells[row, i].Value = list[index].MediaTitle;
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                if (i == 11)
                                {
                                    workSheet.Cells[row, i].Value = list[index].MediaType;
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                if (i == 12)
                                {
                                    workSheet.Cells[row, i].Value = list[index].Page;
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                }
                                if (i == 13)
                                {
                                    if (list[index].Advalue != null)
                                    {
                                        workSheet.Cells[row, i].Value = list[index].Advalue.Value.ToString("N0");
                                    }
                                    else
                                    {
                                        workSheet.Cells[row, i].Value = "0";
                                    }
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                }
                                if (i == 14)
                                {
                                    workSheet.Cells[row, i].Value = list[index].DescriptionEnglish;
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                if (i == 15)
                                {
                                    workSheet.Cells[row, i].Value = list[index].Duration;
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                if (i == 16)
                                {
                                    workSheet.Cells[row, i].Value = list[index].Frequency;
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                if (i == 17)
                                {
                                    workSheet.Cells[row, i].Value = list[index].Note;
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                if (i == 18)
                                {
                                    workSheet.Cells[row, i].Value = list[index].DateUpdated;
                                    workSheet.Cells[row, i].Style.Numberformat.Format = "mm/dd/yyyy HH:mm:ss";
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                }
                                if (i == 19)
                                {
                                    if (!string.IsNullOrEmpty(list[index].URLCode))
                                    {
                                        try
                                        {
                                            workSheet.Cells[row, column].Value = list[index].URLCode;
                                            workSheet.Cells[row, column].Hyperlink = new Uri(list[index].URLCode);
                                        }
                                        catch
                                        {

                                        }
                                    }
                                    workSheet.Cells[row, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                    workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                }
                                workSheet.Cells[row, i].Style.Font.Name = "Times New Roman";
                                workSheet.Cells[row, i].Style.Font.Size = 11;
                                workSheet.Cells[row, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                workSheet.Cells[row, i].Style.Border.Top.Color.SetColor(Color.Black);
                                workSheet.Cells[row, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                workSheet.Cells[row, i].Style.Border.Left.Color.SetColor(Color.Black);
                                workSheet.Cells[row, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                workSheet.Cells[row, i].Style.Border.Right.Color.SetColor(Color.Black);
                                workSheet.Cells[row, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                workSheet.Cells[row, i].Style.Border.Bottom.Color.SetColor(Color.Black);
                            }
                            index = index + 1;
                        }
                        catch (Exception e)
                        {
                            string mes = e.Message;
                        }
                    }
                    for (int i = 1; i <= column; i++)
                    {
                        workSheet.Column(i).AutoFit();
                    }
                }
                package.Save();
            }
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        public IActionResult ExportExcelScanFiles(CancellationToken cancellationToken)
        {
            List<CodeData> list = new List<CodeData>();
            string excelName = @"ScanFiles_" + AppGlobal.DateTimeCode + ".xlsx";
            string sheetName = AppGlobal.DateTimeCode;
            try
            {
                DateTime dateUpdatedBegin = DateTime.Parse(Request.Cookies["CodeDataDatePublishBegin"]);
                DateTime dateUpdatedEnd = DateTime.Parse(Request.Cookies["CodeDataDatePublishEnd"]);
                list = _codeDataRepository.GetByDateUpdatedBeginAndDateUpdatedEndAndSourceIsNewspageAndTVToList(dateUpdatedBegin, dateUpdatedEnd, AppGlobal.Newspage, AppGlobal.TV);
                sheetName = "ScanFiles";
                excelName = @"ScanFiles_" + dateUpdatedBegin.ToString("yyyyMMdd") + "_" + dateUpdatedEnd.ToString("yyyyMMdd") + "_" + AppGlobal.DateTimeCode + ".xlsx";
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
                    int column = 1;
                    int rowExcel = 1;
                    workSheet.Cells[rowExcel, column].Value = "Industry";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    column = column + 1;
                    workSheet.Cells[rowExcel, column].Value = "Media title";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    column = column + 1;
                    workSheet.Cells[rowExcel, column].Value = "Media type";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    column = column + 1;
                    workSheet.Cells[rowExcel, column].Value = "Date publish";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    column = column + 1;
                    workSheet.Cells[rowExcel, column].Value = "Headline (Vie)";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    column = column + 1;
                    workSheet.Cells[rowExcel, column].Value = "Page/TimeLine";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    column = column + 1;
                    workSheet.Cells[rowExcel, column].Value = "Duration/Total size";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    column = column + 1;
                    workSheet.Cells[rowExcel, column].Value = "Advalue";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    column = column + 1;
                    workSheet.Cells[rowExcel, column].Value = "URL";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    column = column + 1;
                    workSheet.Cells[rowExcel, column].Value = "Media Advalue";
                    workSheet.Cells[rowExcel, column].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[rowExcel, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    workSheet.Cells[rowExcel, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[rowExcel, column].Style.Fill.BackgroundColor.SetColor(color);
                    workSheet.Cells[rowExcel, column].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, column].Style.Font.Size = 11;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[rowExcel, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                    int index = 0;
                    rowExcel = rowExcel + 1;
                    for (int row = rowExcel; row <= list.Count + rowExcel - 1; row++)
                    {
                        for (int i = 1; i <= column; i++)
                        {
                            if (i == 1)
                            {
                                workSheet.Cells[row, i].Value = list[index].Industry;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 2)
                            {
                                workSheet.Cells[row, i].Value = list[index].MediaType;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 3)
                            {
                                workSheet.Cells[row, i].Value = list[index].MediaTitle;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 4)
                            {
                                workSheet.Cells[row, i].Value = list[index].DatePublish.ToString("dd/MM/yyyy");
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                //workSheet.Cells[row, i].Style.Numberformat.Format = "dd/mm/yyyy";
                            }
                            if (i == 5)
                            {
                                workSheet.Cells[row, i].Value = list[index].Title;
                                if ((!string.IsNullOrEmpty(list[index].Title)) && (!string.IsNullOrEmpty(list[index].URLCode)))
                                {
                                    try
                                    {
                                        workSheet.Cells[row, i].Hyperlink = new Uri(list[index].URLCode);
                                    }
                                    catch
                                    {

                                    }
                                    workSheet.Cells[row, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                }
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 6)
                            {
                                workSheet.Cells[row, i].Value = list[index].Page;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            }
                            if (i == 7)
                            {
                                workSheet.Cells[row, i].Value = list[index].Duration;
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            }
                            if (i == 8)
                            {
                                workSheet.Cells[row, i].Value = list[index].Advalue.Value.ToString("N0");
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            }
                            if (i == 9)
                            {
                                workSheet.Cells[row, i].Value = list[index].URLCode;
                                if (!string.IsNullOrEmpty(list[index].URLCode))
                                {
                                    try
                                    {
                                        workSheet.Cells[row, i].Hyperlink = new Uri(list[index].URLCode);
                                    }
                                    catch
                                    {

                                    }
                                    workSheet.Cells[row, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                }
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                            if (i == 10)
                            {
                                workSheet.Cells[row, i].Value = list[index].Color.Value.ToString("N0");
                                workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            }
                            workSheet.Cells[row, i].Style.Font.Name = "Times New Roman";
                            workSheet.Cells[row, i].Style.Font.Size = 11;
                            workSheet.Cells[row, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[row, i].Style.Border.Top.Color.SetColor(Color.Black);
                            workSheet.Cells[row, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[row, i].Style.Border.Left.Color.SetColor(Color.Black);
                            workSheet.Cells[row, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[row, i].Style.Border.Right.Color.SetColor(Color.Black);
                            workSheet.Cells[row, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[row, i].Style.Border.Bottom.Color.SetColor(Color.Black);
                        }
                        index = index + 1;
                    }
                    for (int i = 1; i <= column; i++)
                    {
                        workSheet.Column(i).AutoFit();
                    }
                }
                package.Save();
            }
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        public IActionResult Create(CodeData model)
        {
            string note = AppGlobal.InitString;
            int result = 0;
            foreach (string item in model.URLCode.Split(' '))
            {
                string url = item;
                url = url.Trim();
                if (!string.IsNullOrEmpty(url))
                {
                    Uri website = new Uri(url);
                    Config config = _configResposistory.GetByGroupNameAndCodeAndTitle(AppGlobal.CRM, AppGlobal.Website, website.Authority);
                    if ((config == null) || (config.ID == 0))
                    {
                        config = new Config();
                        config.GroupName = AppGlobal.CRM;
                        config.Code = AppGlobal.Website;
                        config.Title = website.Authority;
                        config.URLFull = website.Scheme + "/" + website.Authority;
                        config.Initialization(InitType.Insert, RequestUserID);
                        _configResposistory.Create(config);
                    }
                    if ((config != null) && (config.ID > 0))
                    {
                        Product product = _productRepository.GetByURLCode(url);
                        if ((product == null) || (product.ID == 0))
                        {
                            product = new Product();

                            product.Title = model.Title;
                            product.Description = model.Description;
                            product.DatePublish = model.DatePublish;
                            product.IsFilter = true;
                            product.ParentID = config.ID;
                            product.CategoryID = config.ID;
                            product.Source = AppGlobal.SourceAuto;
                            product.URLCode = url;
                            if (product.DatePublish.Year == 2020)
                            {
                                product.Active = false;
                            }
                        }
                        else
                        {
                            product.Active = true;
                        }
                        if (string.IsNullOrEmpty(product.Title))
                        {
                            product.Title = AppGlobal.FinderTitle001(product.URLCode);
                        }
                        if (string.IsNullOrEmpty(product.Description))
                        {
                            string html = AppGlobal.FinderHTMLContent(product.URLCode);
                            AppGlobal.FinderContentAndDatePublish002(html, product);
                        }
                        //if ((product.DatePublish.Year > 2020) && (product.Active == true))
                        //{
                        if (!string.IsNullOrEmpty(product.Title))
                        {
                            product.Title = HttpUtility.HtmlDecode(product.Title);
                            product.MetaTitle = AppGlobal.SetName(product.Title);
                        }
                        if (!string.IsNullOrEmpty(product.Description))
                        {
                            product.Description = HttpUtility.HtmlDecode(product.Description);
                        }
                        if (!string.IsNullOrEmpty(product.ContentMain))
                        {
                            product.ContentMain = HttpUtility.HtmlDecode(product.ContentMain);
                        }
                        product.Initialization(InitType.Insert, RequestUserID);
                        string resultString = _productRepository.InsertSingleItemAuto(product);
                        if (resultString == "-1")
                        {
                            result = 1;
                        }
                        //}
                    }
                }
            }

            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult Update(CodeData model)
        {
            string note = AppGlobal.InitString;
            int result = 0;

            Product product = _productRepository.GetByID(model.ProductID.Value);
            if (product != null)
            {
                if (product.ID > 0)
                {
                    product.Title = model.Title;
                    product.Description = model.Description;
                    product.DatePublish = model.DatePublish;
                    product.Initialization(InitType.Update, RequestUserID);
                    _productRepository.Update(product.ID, product);
                }
            }
            ProductProperty productProperty = _productPropertyRepository.GetByID(model.ProductID.Value);
            if (productProperty != null)
            {
                if (productProperty.ID > 0)
                {
                    productProperty.SentimentCorp = model.SentimentCorp;
                    productProperty.SOECompany = model.SOECompany;
                    productProperty.SOEProduct = model.SOEProduct;
                    productProperty.Initialization(InitType.Update, RequestUserID);
                    _productPropertyRepository.Update(productProperty.ID, productProperty);
                }
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
    }
}
