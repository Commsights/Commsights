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
            model.HourBegin = 6;
            model.HourEnd = DateTime.Now.Hour;
            model.IndustryID = AppGlobal.IndustryID;
            return View(model);
        }
        public IActionResult Export001()
        {
            DateTime now = DateTime.Now;
            CodeDataViewModel model = new CodeDataViewModel();
            model.HourBegin = 1;
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
        public IActionResult Company()
        {
            return View();
        }

        public IActionResult Detail(int rowIndex)
        {
            CodeData model = GetCodeData(rowIndex);
            return View(model);
        }
        public IActionResult DetailBasic(int productPropertyID)
        {
            CodeData model = GetCodeData(productPropertyID);
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
        public IActionResult Export001ExportExcel(CancellationToken cancellationToken)
        {
            List<CodeData> list = new List<CodeData>();
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
                list = _codeDataRepository.GetByDateUpdatedBeginAndDateUpdatedEndAndHourBeginAndHourEndAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysisToList(dateUpdatedBegin, dateUpdatedEnd, hourBegin, hourEnd, industryID, companyName, isCoding, isAnalysis);
                Config industry = _configResposistory.GetByID(industryID);
                if (industry != null)
                {
                    industryName = industry.CodeName;
                }
                sheetName = industryName;
                industryName = AppGlobal.SetName(industryName);
                excelName = @"Code_" + industryName + "_" + dateUpdatedBegin.ToString("yyyyMMdd") + "_" + dateUpdatedEnd.ToString("yyyyMMdd") + "_" + AppGlobal.DateTimeCode + ".xlsx";
                _productPropertyRepository.UpdateItemsByDailyDownload(dateUpdatedBegin, dateUpdatedEnd, hourBegin, hourEnd, industryID, companyName, isCoding, isAnalysis, RequestUserID);
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
        public ActionResult GetReportByDatePublishBeginAndDatePublishEndAndIsUploadToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, bool isUpload)
        {
            List<CodeDataReport> list = _codeDataRepository.GetReportByDatePublishBeginAndDatePublishEndAndIsUploadToList(datePublishBegin, datePublishEnd, isUpload);
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
        public ActionResult GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndIsPublishToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, int industryID, bool isPublish)
        {
            var cookieExpires = new CookieOptions();
            cookieExpires.Expires = DateTime.Now.AddDays(1);
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
            cookieExpires.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("CodeDataIndustryID", industryID.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataDatePublishBegin", datePublishBegin.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataDatePublishEnd", datePublishEnd.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataIsUpload", isUpload.ToString(), cookieExpires);
            List<CodeData> list = _codeDataRepository.GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndIsUploadToList(datePublishBegin, datePublishEnd, industryID, RequestUserID, isUpload);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndIsUploadAndSourceIsNewspageAndTVToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, int industryID, bool isUpload)
        {
            var cookieExpires = new CookieOptions();
            cookieExpires.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("CodeDataIndustryID", industryID.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataDatePublishBegin", datePublishBegin.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataDatePublishEnd", datePublishEnd.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataIsUpload", isUpload.ToString(), cookieExpires);            
            List<CodeData> list = _codeDataRepository.GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndIsUploadAndSourceIsNewspageAndTVToList(datePublishBegin, datePublishEnd, industryID, RequestUserID, isUpload, AppGlobal.Newspage, AppGlobal.TV);
            return Json(list.ToDataSourceResult(request));
        }
        public ActionResult GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndIsUploadAndSourceIsNotNewspageAndTVToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, int industryID, bool isUpload)
        {
            var cookieExpires = new CookieOptions();
            cookieExpires.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("CodeDataIndustryID", industryID.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataDatePublishBegin", datePublishBegin.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataDatePublishEnd", datePublishEnd.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataIsUpload", isUpload.ToString(), cookieExpires);
            List<CodeData> list = _codeDataRepository.GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndIsUploadAndSourceIsNotNewspageAndTVToList(datePublishBegin, datePublishEnd, industryID, RequestUserID, isUpload, AppGlobal.Newspage, AppGlobal.TV);
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
            string actionMessage = "";

            if ((!string.IsNullOrEmpty(model.TitleProperty)) && (model.SourceProperty > 0))
            {
                List<ProductProperty> list = _productPropertyRepository.GetTitleAndSourceToList(model.TitleProperty, model.SourceProperty.Value);
                foreach (ProductProperty productProperty in list)
                {
                    productProperty.ID = 0;
                    productProperty.FileName = "";
                    productProperty.MediaTitle = "";
                    productProperty.MediaType = "";
                    productProperty.ParentID = model.ProductID;
                    productProperty.Source = model.Source;
                    productProperty.IsCoding = true;
                    productProperty.DateCoding = DateTime.Now;
                    productProperty.Initialization(InitType.Insert, RequestUserID);
                    _productPropertyRepository.Create(productProperty);
                }
                _productPropertyRepository.Delete(model.ProductPropertyID.Value);
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
            return RedirectToAction("Detail", "CodeData", new { RowIndex = model.RowIndex, ActionMessage = actionMessage });
        }
        public IActionResult SaveCodingDetailBasic(CodeData model)
        {
            string actionMessage = CheckCodeData(model);
            return RedirectToAction("DetailBasic", "CodeData", new { RowIndex = model.RowIndex, ActionMessage = actionMessage });
        }
        public int CopyURLSame(int productPropertyID)
        {
            productPropertyID = Copy(productPropertyID);
            return productPropertyID;
        }
        public int CopyURLAnother(int productPropertyID)
        {
            productPropertyID = Copy(productPropertyID);
            return productPropertyID;
        }
        public int BasicCopyURLSame(int productPropertyID)
        {
            CodeData model = GetCodeData(productPropertyID);
            _productPropertyRepository.InsertSingleItemByCopyCodeData(model.ProductPropertyID.Value, RequestUserID);
            return productPropertyID;
        }
        public int BasicCopyURLAnother(int productPropertyID)
        {
            CodeData model = GetCodeData(productPropertyID);
            _productPropertyRepository.InsertItemsByCopyCodeData(model.ProductPropertyID.Value, RequestUserID);
            model = GetCodeData(productPropertyID);
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
                bool isUpload = false;
                try
                {
                    industryID = int.Parse(Request.Cookies["CodeDataIndustryID"]);
                    datePublishBegin = DateTime.Parse(Request.Cookies["CodeDataDatePublishBegin"]);
                    datePublishEnd = DateTime.Parse(Request.Cookies["CodeDataDatePublishEnd"]);
                    isUpload = bool.Parse(Request.Cookies["CodeDataIsUpload"]);
                    List<CodeData> list = _codeDataRepository.GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDAndIsUploadToList(datePublishBegin, datePublishEnd, industryID, RequestUserID, isUpload);
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (productPropertyID == list[i].ProductPropertyID)
                        {
                            model = list[i];
                            model.CompanyNameHiden = _codeDataRepository.GetCompanyNameByURLCode(model.URLCode);
                            model.ProductNameHiden = _codeDataRepository.GetProductNameByURLCode(model.URLCode);
                            i = list.Count;
                        }
                    }

                    model.RowNext = 0;
                    List<CodeData> listIsCoding = list.Where(item => item.IsCoding == false || item.IsCoding == null).ToList();
                    if (listIsCoding.Count > 0)
                    {
                        model.RowNext = listIsCoding[0].ProductPropertyID;
                    }
                    model.RowIndexCount = listIsCoding.Count;
                    model.RowCount = list.Count;

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
            Response.Cookies.Append("CodeDataDailyDatePublishBegin", dateUpdatedEnd.ToString("MM/dd/yyyy"), cookieExpires);
            Response.Cookies.Append("CodeDataDailyHourBegin", hourBegin.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataDailyHourEnd", hourEnd.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataDailyIndustryID", industryID.ToString(), cookieExpires);
            Response.Cookies.Append("CodeDataDailyIsCoding", true.ToString(), cookieExpires);
            List<CodeData> list = _codeDataRepository.GetDailyByDateUpdatedBeginAndDateUpdatedEndAndHourBeginAndHourEndAndIndustryIDAndIsCodingToList(dateUpdatedBegin, dateUpdatedEnd, hourBegin, hourEnd, industryID, true);
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
                DateTime dateUpdatedEnd = DateTime.Parse(Request.Cookies["CodeDataDailyDatePublishBegin"]);
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
        public IActionResult Export001ExportExcelForDailyVietnamese(CancellationToken cancellationToken)
        {
            List<CodeData> list = new List<CodeData>();
            string excelName = @"Daily_" + AppGlobal.DateTimeCode + ".xlsx";
            string sheetName = AppGlobal.DateTimeCode;
            try
            {
                string industryName = "";
                DateTime dateUpdatedBegin = DateTime.Parse(Request.Cookies["CodeDataDailyDatePublishBegin"]);
                DateTime dateUpdatedEnd = DateTime.Parse(Request.Cookies["CodeDataDailyDatePublishBegin"]);
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
    }
}
