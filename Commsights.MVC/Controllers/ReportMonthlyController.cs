using System;
using System.Text;
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
using Commsights.Data.DataTransferObject;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using OfficeOpenXml;
using Commsights.MVC.Models;
using Commsights.Service.Mail;
using System.Globalization;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Commsights.MVC.Controllers
{
    public class ReportMonthlyController : BaseController
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfigRepository _configRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductPropertyRepository _productPropertyRepository;
        private readonly IReportMonthlyRepository _reportMonthlyRepository;
        private readonly IReportMonthlyPropertyRepository _reportMonthlyPropertyRepository;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMembershipPermissionRepository _membershipPermissionRepository;
        public ReportMonthlyController(IConfigRepository configRepository, IMembershipRepository membershipRepository, IMembershipPermissionRepository membershipPermissionRepository, IReportMonthlyPropertyRepository reportMonthlyPropertyRepository, IReportMonthlyRepository reportMonthlyRepository, IProductPropertyRepository productPropertyRepository, IProductRepository productRepository, IWebHostEnvironment hostingEnvironment, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _configRepository = configRepository;
            _membershipRepository = membershipRepository;
            _membershipPermissionRepository = membershipPermissionRepository;
            _productRepository = productRepository;
            _productPropertyRepository = productPropertyRepository;
            _reportMonthlyRepository = reportMonthlyRepository;
            _reportMonthlyPropertyRepository = reportMonthlyPropertyRepository;
        }
        public IActionResult Monthly()
        {
            BaseViewModel model = new BaseViewModel();
            model.YearFinance = DateTime.Now.Year;
            model.MonthFinance = DateTime.Now.Month;
            return View(model);
        }
        public IActionResult MonthlyData(int ID)
        {
            ReportMonthly model = new ReportMonthly();
            if (ID > 0)
            {
                model = _reportMonthlyRepository.GetByID(ID);
            }
            return View(model);
        }
        public IActionResult MonthlyIndustry(int ID)
        {
            ReportMonthlyViewModel model = new ReportMonthlyViewModel();
            if (ID > 0)
            {
                ReportMonthly reportMonthly = _reportMonthlyRepository.GetByID(ID);
                if (reportMonthly != null)
                {
                    model.ID = reportMonthly.ID;
                    model.Title = reportMonthly.Title;
                    model.ListReportMonthlyIndustryDataTransfer = _reportMonthlyRepository.GetIndustryByIDWithoutSUMToList(model.ID);
                }
            }
            return View(model);
        }
        public IActionResult MonthlyIndustryCount(int ID)
        {
            ReportMonthlyViewModel model = new ReportMonthlyViewModel();
            if (ID > 0)
            {
                ReportMonthly reportMonthly = _reportMonthlyRepository.GetByID(ID);
                if (reportMonthly != null)
                {
                    model.ID = reportMonthly.ID;
                    model.Title = reportMonthly.Title;
                    model.ListReportMonthlyIndustryDataTransfer = _reportMonthlyRepository.GetIndustryByID001WithoutSUMToList(model.ID);
                }
            }
            return View(model);
        }
        public IActionResult MonthlyCompanyCount(int ID)
        {
            ReportMonthlyViewModel model = new ReportMonthlyViewModel();
            if (ID > 0)
            {
                ReportMonthly reportMonthly = _reportMonthlyRepository.GetByID(ID);
                if (reportMonthly != null)
                {
                    model.ID = reportMonthly.ID;
                    model.Title = reportMonthly.Title;
                    model.ListReportMonthlyIndustryDataTransfer = _reportMonthlyRepository.GetIndustryByID001WithoutSUMToList(model.ID);
                }
            }
            return View(model);
        }
        public IActionResult MonthlyFeatureIndustry(int ID)
        {
            ReportMonthlyViewModel model = new ReportMonthlyViewModel();
            if (ID > 0)
            {
                ReportMonthly reportMonthly = _reportMonthlyRepository.GetByID(ID);
                if (reportMonthly != null)
                {
                    model.ID = reportMonthly.ID;
                    model.Title = reportMonthly.Title;
                    model.ListReportMonthlyIndustryDataTransfer = _reportMonthlyRepository.GetFeatureIndustryWithoutSUMByIDToList(model.ID);
                }
            }
            return View(model);
        }
        public IActionResult MonthlySentimentIndustry(int ID)
        {
            ReportMonthlyViewModel model = new ReportMonthlyViewModel();
            if (ID > 0)
            {
                ReportMonthly reportMonthly = _reportMonthlyRepository.GetByID(ID);
                if (reportMonthly != null)
                {
                    model.ID = reportMonthly.ID;
                    model.Title = reportMonthly.Title;
                    model.ListReportMonthlySentimentDataTransfer = _reportMonthlyRepository.GetSentimentByIDWithoutSUMToList(model.ID);
                    model.ListReportMonthlySentimentAndMediaTypeDataTransfer = _reportMonthlyRepository.GetSentimentAndMediaTypeWithoutSUMByIDToList(model.ID);
                    model.ListReportMonthlySentimentAndFeatureDataTransfer = _reportMonthlyRepository.GetSentimentAndFeatureWithoutSUMByIDToList(model.ID);
                }
            }
            return View(model);
        }
        public IActionResult MonthlyChannel(int ID)
        {
            ReportMonthlyViewModel model = new ReportMonthlyViewModel();
            if (ID > 0)
            {
                ReportMonthly reportMonthly = _reportMonthlyRepository.GetByID(ID);
                if (reportMonthly != null)
                {
                    model.ID = reportMonthly.ID;
                    model.Title = reportMonthly.Title;
                    model.ListReportMonthlyChannelDataTransfer = _reportMonthlyRepository.GetChannelByIDWithoutSUMToList(model.ID);
                    model.ListReportMonthlyChannelAndFeatureDataTransfer = _reportMonthlyRepository.GetChannelAndFeatureWithoutSUMByIDToList(model.ID);
                    model.ListReportMonthlyChannelAndMentionDataTransfer = _reportMonthlyRepository.GetChannelAndMentionWithoutSUMByIDToList(model.ID);
                }
            }
            return View(model);
        }
        public IActionResult MonthlyTierCommsights(int ID)
        {
            ReportMonthlyViewModel model = new ReportMonthlyViewModel();
            if (ID > 0)
            {
                ReportMonthly reportMonthly = _reportMonthlyRepository.GetByID(ID);
                if (reportMonthly != null)
                {
                    model.ID = reportMonthly.ID;
                    model.Title = reportMonthly.Title;
                    model.ListReportMonthlyTierCommsightsDataTransfer = _reportMonthlyRepository.GetTierCommsightsWithoutSUMByIDToList(ID);
                }
            }
            return View(model);
        }
        public IActionResult MonthlyTrendLine(int ID)
        {
            ReportMonthlyViewModel model = new ReportMonthlyViewModel();
            if (ID > 0)
            {
                ReportMonthly reportMonthly = _reportMonthlyRepository.GetByID(ID);
                if (reportMonthly != null)
                {
                    model.ID = reportMonthly.ID;
                    model.Title = reportMonthly.Title;
                }
            }
            return View(model);
        }
        public IActionResult MonthlyTierCommsightsAndCompanyName(int ID)
        {
            ReportMonthlyViewModel model = new ReportMonthlyViewModel();
            if (ID > 0)
            {
                ReportMonthly reportMonthly = _reportMonthlyRepository.GetByID(ID);
                if (reportMonthly != null)
                {
                    model.ID = reportMonthly.ID;
                    model.Title = reportMonthly.Title;
                }
            }
            return View(model);
        }
        public IActionResult MonthlyCompanyAndYear(int ID)
        {
            ReportMonthlyViewModel model = new ReportMonthlyViewModel();
            if (ID > 0)
            {
                ReportMonthly reportMonthly = _reportMonthlyRepository.GetByID(ID);
                if (reportMonthly != null)
                {
                    model.ID = reportMonthly.ID;
                    model.Title = reportMonthly.Title;
                    model.ListReportMonthlyCompanyAndYearDataTransfer = _reportMonthlyRepository.GetCompanyAndYearWithoutSUMByIDToList(ID);
                    model.ListMonth = new List<MonthData>();
                    for (int i = 1; i < 13; i++)
                    {
                        MonthData monthData = new MonthData();
                        monthData.Month = i.ToString();
                        model.ListMonth.Add(monthData);
                    }
                    model.ListSeries = new List<SeriesData>();
                    foreach (ReportMonthlyCompanyAndYearDataTransfer item in model.ListReportMonthlyCompanyAndYearDataTransfer)
                    {
                        SeriesData seriesData = new SeriesData();
                        seriesData.Name = item.CompanyName;
                        seriesData.Data = new List<int?>();
                        seriesData.Data.Add(item.Month01Count);
                        seriesData.Data.Add(item.Month02Count);
                        seriesData.Data.Add(item.Month03Count);
                        seriesData.Data.Add(item.Month04Count);
                        seriesData.Data.Add(item.Month05Count);
                        seriesData.Data.Add(item.Month06Count);
                        seriesData.Data.Add(item.Month07Count);
                        seriesData.Data.Add(item.Month08Count);
                        seriesData.Data.Add(item.Month09Count);
                        seriesData.Data.Add(item.Month10Count);
                        seriesData.Data.Add(item.Month11Count);
                        seriesData.Data.Add(item.Month12Count);
                        model.ListSeries.Add(seriesData);
                    }
                }
            }
            return View(model);
        }
        public IActionResult MonthlySegmentProduct(int ID)
        {
            ReportMonthlyViewModel model = new ReportMonthlyViewModel();
            if (ID > 0)
            {
                ReportMonthly reportMonthly = _reportMonthlyRepository.GetByID(ID);
                if (reportMonthly != null)
                {
                    model.ID = reportMonthly.ID;
                    model.Title = reportMonthly.Title;
                }
            }
            return View(model);
        }
        public IActionResult Upload(int ID)
        {
            ReportMonthly model = new ReportMonthly();
            model.ID = ID;
            if (model.ID > 0)
            {
                model = _reportMonthlyRepository.GetByID(model.ID);
            }
            if ((model == null) || (model.ID < 1))
            {
                model = new ReportMonthly();
                model.ID = ID;
                model.Year = DateTime.Now.Year;
                model.Month = DateTime.Now.Month;
            }
            return View(model);
        }
        public IActionResult Delete(int ID)
        {
            string note = AppGlobal.InitString;
            int result = 0;
            if (_reportMonthlyRepository.DeleteByID(ID) == "-1")
            {
                result = 1;
            }
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
        public IActionResult DeleteReportMonthlyProperty(int ID)
        {
            string note = AppGlobal.InitString;
            int result = _reportMonthlyPropertyRepository.Delete(ID);
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
        public ActionResult GetByYearAndMonthToList([DataSourceRequest] DataSourceRequest request, int year, int month)
        {
            var data = _reportMonthlyRepository.GetByYearAndMonthToList(year, month);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetReportMonthlyPropertyDataTransferByParentIDToList([DataSourceRequest] DataSourceRequest request, int parentID)
        {
            var data = _reportMonthlyPropertyRepository.GetReportMonthlyPropertyDataTransferByParentIDToList(parentID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetIndustryByIDToListToJSON(int ID)
        {
            return Json(_reportMonthlyRepository.GetIndustryByIDToList(ID));
        }
        public ActionResult GetIndustryByID001ToListToJSON(int ID)
        {
            return Json(_reportMonthlyRepository.GetIndustryByID001ToList(ID));
        }
        public ActionResult GetCompanyByIDToListToJSON(int ID)
        {
            return Json(_reportMonthlyRepository.GetCompanyByIDToList(ID));
        }
        public ActionResult GetFeatureIndustryByIDToListToJSON(int ID)
        {
            return Json(_reportMonthlyRepository.GetFeatureIndustryByIDToList(ID));
        }
        public ActionResult GetSentimentByIDWithoutSUMToListToJSON(int ID)
        {
            return Json(_reportMonthlyRepository.GetSentimentByIDWithoutSUMToList(ID));
        }
        public ActionResult GetSentimentAndFeatureWithoutSUMByIDToListToJSON(int ID)
        {
            return Json(_reportMonthlyRepository.GetSentimentAndFeatureWithoutSUMByIDToList(ID));
        }
        public ActionResult GetSentimentAndMediaTypeWithoutSUMByIDToListToJSON(int ID)
        {
            return Json(_reportMonthlyRepository.GetSentimentAndMediaTypeWithoutSUMByIDToList(ID));
        }
        public ActionResult GetSentimentByIDToListToJSON(int ID)
        {
            return Json(_reportMonthlyRepository.GetSentimentByIDToList(ID));
        }
        public ActionResult GetChannelByIDToListToJSON(int ID)
        {
            return Json(_reportMonthlyRepository.GetChannelByIDToList(ID));
        }
        public ActionResult GetChannelAndFeatureByIDToListToJSON(int ID)
        {
            return Json(_reportMonthlyRepository.GetChannelAndFeatureByIDToList(ID));
        }
        public ActionResult GetChannelAndMentionByIDToListToJSON(int ID)
        {
            return Json(_reportMonthlyRepository.GetChannelAndMentionByIDToList(ID));
        }
        public ActionResult GetTierCommsightsByIDToListToJSON(int ID)
        {
            return Json(_reportMonthlyRepository.GetTierCommsightsByIDToList(ID));
        }
        public ActionResult GetCompanyAndYearByIDToListToJSON(int ID)
        {
            return Json(_reportMonthlyRepository.GetCompanyAndYearByIDToList(ID));
        }
        public ActionResult GetSegmentProductWithoutSUMByIDToListToJSON(int ID)
        {
            return Json(_reportMonthlyRepository.GetSegmentProductWithoutSUMByIDToList(ID));
        }
        public ActionResult GetTrendLineWithoutSUMByIDToListToJSON(int ID)
        {
            string html = "";
            DataTable tbl = new DataTable();
            ReportMonthly model = _reportMonthlyRepository.GetByID(ID);
            if (model != null)
            {
                int monthLast = model.Month.Value - 1;
                int yearLast = model.Year.Value;
                if (monthLast < 1)
                {
                    monthLast = 1;
                    yearLast = yearLast - 1;
                }
                List<ReportMonthlyTrendLineDataTransfer> list = _reportMonthlyRepository.GetTrendLineWithoutSUMByIDToList(ID);
                List<ReportMonthlyTrendLineDataTransfer> listCompanyName = _reportMonthlyRepository.GetTrendLineDistinctCompanyNameByIDToList(ID);
                tbl.Columns.Add(new DataColumn("Year"));
                tbl.Columns.Add(new DataColumn("Month"));
                tbl.Columns.Add(new DataColumn("Day"));
                foreach (ReportMonthlyTrendLineDataTransfer item in listCompanyName)
                {
                    tbl.Columns.Add(new DataColumn(item.CompanyName));
                }
                for (int i = 1; i < 32; i++)
                {
                    DataRow row = tbl.NewRow();
                    row["Year"] = yearLast;
                    row["Month"] = monthLast;
                    row["Day"] = i;
                    tbl.Rows.Add(row);
                }
                for (int i = 1; i < 32; i++)
                {
                    DataRow row = tbl.NewRow();
                    row["Year"] = model.Year;
                    row["Month"] = model.Month;
                    row["Day"] = i;
                    tbl.Rows.Add(row);
                }
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    foreach (ReportMonthlyTrendLineDataTransfer item in list)
                    {
                        for (int j = 0; j < tbl.Columns.Count; j++)
                        {
                            string columnName = tbl.Columns[j].ColumnName;
                            if (item.CompanyName == columnName)
                            {
                                try
                                {
                                    int year = int.Parse(tbl.Rows[i]["Year"].ToString());
                                    int month = int.Parse(tbl.Rows[i]["Month"].ToString());
                                    int day = int.Parse(tbl.Rows[i]["Day"].ToString());
                                    if ((year == item.Year) && (month == item.Month) && (day == item.Day))
                                    {
                                        tbl.Rows[i][columnName] = item.TrendLineCount;
                                        j = tbl.Columns.Count;
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                }

                StringBuilder txt = new StringBuilder();
                txt.AppendLine("<table class='border01' id='Data01' cellspacing='4' style='background-color:#ffffff; width:100%;'>");
                txt.AppendLine("<thead>");
                txt.AppendLine("<tr>");
                for (int i = 0; i < tbl.Columns.Count; i++)
                {
                    txt.AppendLine("<th class='text-center'><a style='cursor:pointer;'>" + tbl.Columns[i].ColumnName + "</a></th>");
                }
                txt.AppendLine("</tr>");
                txt.AppendLine("</thead>");
                txt.AppendLine("<tbody>");
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        txt.AppendLine("<tr style='background-color:#ffffff;'>");
                    }
                    else
                    {
                        txt.AppendLine("<tr style='background-color:#f1f1f1;'>");
                    }
                    txt.AppendLine("<td class='text-center'>" + tbl.Rows[i]["Year"].ToString() + "</td>");
                    txt.AppendLine("<td class='text-center'>" + tbl.Rows[i]["Month"].ToString() + "</td>");
                    txt.AppendLine("<td class='text-center'>" + tbl.Rows[i]["Day"].ToString() + "</td>");
                    for (int j = 3; j < tbl.Columns.Count; j++)
                    {
                        txt.AppendLine("<td class='text-right'>" + tbl.Rows[i][tbl.Columns[j].ColumnName].ToString() + "</td>");
                    }
                    txt.AppendLine("</tr>");
                }
                txt.AppendLine("</tbody>");
                txt.AppendLine("</table>");
                html = txt.ToString();
            }
            return Json(html);
        }
        public ActionResult GetMonthlyTierCommsightsAndCompanyNameToJSON(int ID)
        {
            ReportMonthlyViewModel model = new ReportMonthlyViewModel();
            if (ID > 0)
            {
                ReportMonthly reportMonthly = _reportMonthlyRepository.GetByID(ID);
                if (reportMonthly != null)
                {
                    model.ID = reportMonthly.ID;
                    model.Title = reportMonthly.Title;
                    model.ListCompanyName = _reportMonthlyRepository.GetTierCommsightsAndCompanyNameDistinctByIDToList(model.ID);
                    model.ListTierCommsightsAndCompanyNameAndPortal = _reportMonthlyRepository.GetTierCommsightsAndCompanyNameAndPortalByIDToList(model.ID);
                    model.ListTierCommsightsAndCompanyNameAndOther = _reportMonthlyRepository.GetTierCommsightsAndCompanyNameAndOtherByIDToList(model.ID);
                    model.ListTierCommsightsAndCompanyNameAndMass = _reportMonthlyRepository.GetTierCommsightsAndCompanyNameAndMassByIDToList(model.ID);
                    model.ListTierCommsightsAndCompanyNameAndIndustry = _reportMonthlyRepository.GetTierCommsightsAndCompanyNameAndIndustryByIDToList(model.ID);
                }
            }
            return Json(model);
        }
        [HttpPost]
        public IActionResult Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);
            return File(fileContents, contentType, fileName);
        }
        public ActionResult UploadDataReportMonthly(Commsights.Data.Models.ReportMonthly model)
        {
            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files[0];
                    if (file == null || file.Length == 0)
                    {
                    }
                    if (file != null)
                    {
                        string fileExtension = Path.GetExtension(file.FileName);
                        string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                        fileName = "ReportMonthly_" + model.CompanyID + "_" + model.Year + "_" + model.Month;
                        fileName = fileName + "-" + AppGlobal.DateTimeCode + fileExtension;
                        var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, AppGlobal.FTPUploadExcel, fileName);
                        using (var stream = new FileStream(physicalPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                            FileInfo fileLocation = new FileInfo(physicalPath);
                            if (fileLocation.Length > 0)
                            {
                                if ((fileExtension == ".xlsx") || (fileExtension == ".xls"))
                                {
                                    using (ExcelPackage package = new ExcelPackage(stream))
                                    {
                                        if (package.Workbook.Worksheets.Count > 0)
                                        {
                                            ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                                            if (workSheet != null)
                                            {
                                                model.Note = fileName;
                                                model.Initialization(InitType.Insert, RequestUserID);
                                                model.IsMonthly = true;
                                                model.Title = "ReportMonthly_" + model.CompanyID + "_" + model.Year + "_" + model.Month + "_" + AppGlobal.DateTimeCode;
                                                Membership customer = _membershipRepository.GetByID(model.CompanyID.Value);
                                                if (customer != null)
                                                {
                                                    model.Title = "ReportMonthly_" + model.CompanyID + "_" + customer.Account + "_" + model.Year + "_" + model.Month + "_" + AppGlobal.DateTimeCode;
                                                }
                                                _reportMonthlyRepository.Create(model);
                                                int totalRows = workSheet.Dimension.Rows;
                                                for (int i = 2; i <= totalRows; i++)
                                                {
                                                    try
                                                    {
                                                        Product product = new Product();
                                                        product.GUICode = AppGlobal.InitGuiCode;
                                                        product.Source = AppGlobal.SourceAuto;
                                                        product.Initialization(InitType.Insert, RequestUserID);
                                                        string datePublish = "";
                                                        if (workSheet.Cells[i, 2].Value != null)
                                                        {
                                                            datePublish = workSheet.Cells[i, 2].Value.ToString().Trim();
                                                            try
                                                            {
                                                                product.DatePublish = DateTime.Parse(datePublish);
                                                            }
                                                            catch
                                                            {
                                                                try
                                                                {
                                                                    int year = int.Parse(datePublish.Split('/')[2]);
                                                                    int month = int.Parse(datePublish.Split('/')[0]);
                                                                    int day = int.Parse(datePublish.Split('/')[1]);
                                                                    product.DatePublish = new DateTime(year, month, day, 0, 0, 0);
                                                                }
                                                                catch
                                                                {
                                                                    try
                                                                    {
                                                                        int year = int.Parse(datePublish.Split('/')[2]);
                                                                        int month = int.Parse(datePublish.Split('/')[1]);
                                                                        int day = int.Parse(datePublish.Split('/')[0]);
                                                                        product.DatePublish = new DateTime(year, month, day, 0, 0, 0);
                                                                    }
                                                                    catch
                                                                    {
                                                                        try
                                                                        {
                                                                            DateTime DateTimeStandard = new DateTime(1899, 12, 30);
                                                                            product.DatePublish = DateTimeStandard.AddDays(int.Parse(datePublish));
                                                                        }
                                                                        catch
                                                                        {
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        if (workSheet.Cells[i, 15].Value != null)
                                                        {
                                                            product.Title = workSheet.Cells[i, 15].Value.ToString().Trim();
                                                            if (workSheet.Cells[i, 15].Hyperlink != null)
                                                            {
                                                                product.URLCode = workSheet.Cells[i, 15].Hyperlink.AbsoluteUri.Trim();
                                                            }
                                                        }
                                                        if (workSheet.Cells[i, 16].Value != null)
                                                        {
                                                            product.TitleEnglish = workSheet.Cells[i, 16].Value.ToString().Trim();
                                                            if (workSheet.Cells[i, 16].Hyperlink != null)
                                                            {
                                                                product.URLCode = workSheet.Cells[i, 16].Hyperlink.AbsoluteUri.Trim();
                                                            }
                                                        }
                                                        if (workSheet.Cells[i, 17].Value != null)
                                                        {
                                                            product.URLCode = workSheet.Cells[i, 17].Value.ToString().Trim();
                                                        }
                                                        if (workSheet.Cells[i, 18].Value != null)
                                                        {
                                                            product.Page = workSheet.Cells[i, 18].Value.ToString().Trim();
                                                        }
                                                        if (workSheet.Cells[i, 19].Value != null)
                                                        {
                                                            product.Author = workSheet.Cells[i, 19].Value.ToString().Trim();
                                                        }
                                                        if (!string.IsNullOrEmpty(product.URLCode))
                                                        {
                                                            Product product001 = _productRepository.GetByURLCode(product.URLCode);
                                                            if (product001 != null)
                                                            {
                                                                product001.DatePublish = product.DatePublish;
                                                                product001.TitleEnglish = product.TitleEnglish;
                                                                product001.Title = product.Title;
                                                                _productRepository.AsyncUpdateSingleItem(product001);
                                                            }
                                                            else
                                                            {
                                                                product001 = product;
                                                                Uri myUri = new Uri(product.URLCode);
                                                                string domain = myUri.Host;
                                                                Config website = _configRepository.GetByGroupNameAndCodeAndTitle(AppGlobal.CRM, AppGlobal.Website, domain);
                                                                if (website == null)
                                                                {
                                                                    website = new Config();
                                                                    website.Color = AppGlobal.AdValue;
                                                                    website.URLFull = domain;
                                                                    website.Title = domain;
                                                                    website.ParentID = AppGlobal.ParentID;
                                                                    website.GroupName = AppGlobal.CRM;
                                                                    website.Code = AppGlobal.Website;
                                                                    website.Active = true;
                                                                    website.Initialization(InitType.Insert, RequestUserID);
                                                                    _configRepository.Create(website);
                                                                }
                                                                if (website.ID > 0)
                                                                {
                                                                    product001.ParentID = website.ID;
                                                                    Config tier = new Config();
                                                                    tier.GroupName = AppGlobal.CRM;
                                                                    tier.Code = AppGlobal.Tier;
                                                                    tier.ParentID = website.ID;
                                                                    tier.TierID = AppGlobal.TierOtherID;
                                                                    tier.IndustryID = model.IndustryID;
                                                                    tier.Initialization(InitType.Insert, RequestUserID);
                                                                    if (_configRepository.GetByGroupNameAndCodeAndParentIDAndTierID(tier.GroupName, tier.Code, tier.ParentID.Value, tier.TierID.Value) == null)
                                                                    {
                                                                        _configRepository.Create(tier);
                                                                    }
                                                                }
                                                                _productRepository.AsyncInsertSingleItem(product001);
                                                            }
                                                            if ((product001.ID > 0) && (model.ID > 0))
                                                            {
                                                                ProductProperty productProperty = new ProductProperty();
                                                                productProperty.Initialization(InitType.Insert, RequestUserID);
                                                                productProperty.IsMonthly = true;
                                                                productProperty.GUICode = product001.GUICode;
                                                                productProperty.ParentID = product001.ID;
                                                                if (workSheet.Cells[i, 1].Value != null)
                                                                {
                                                                    productProperty.Source = workSheet.Cells[i, 1].Value.ToString().Trim();
                                                                }
                                                                productProperty.Year = product001.DatePublish.Year.ToString();
                                                                productProperty.Month = product001.DatePublish.Month.ToString();
                                                                productProperty.Day = product001.DatePublish.Day.ToString();
                                                                int quarter = (product001.DatePublish.Month + 2) / 3;
                                                                productProperty.Quarter = quarter.ToString();
                                                                productProperty.Week = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday).ToString();
                                                                if (workSheet.Cells[i, 3].Value != null)
                                                                {
                                                                    productProperty.CategoryMain = workSheet.Cells[i, 3].Value.ToString().Trim();
                                                                    //Config categoryMain = _configRepository.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.CategoryMain, productProperty.CategoryMain);
                                                                    //if (categoryMain == null)
                                                                    //{
                                                                    //    categoryMain = new Config();
                                                                    //    categoryMain.CodeName = productProperty.CategoryMain;
                                                                    //    categoryMain.GroupName = AppGlobal.CRM;
                                                                    //    categoryMain.Code = AppGlobal.CategoryMain;
                                                                    //    categoryMain.ParentID = 0;
                                                                    //    categoryMain.Initialization(InitType.Insert, RequestUserID);
                                                                    //    _configRepository.Create(categoryMain);
                                                                    //}
                                                                    //if (categoryMain.ID > 0)
                                                                    //{
                                                                    //    productProperty.CategoryMainID = categoryMain.ID;
                                                                    //}
                                                                }
                                                                if (workSheet.Cells[i, 4].Value != null)
                                                                {
                                                                    productProperty.CategorySub = workSheet.Cells[i, 4].Value.ToString().Trim();
                                                                    //Config categorySub = _configRepository.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.CategoryMain, productProperty.CategorySub);
                                                                    //if (categorySub == null)
                                                                    //{
                                                                    //    categorySub = new Config();
                                                                    //    categorySub.CodeName = productProperty.CategorySub;
                                                                    //    categorySub.GroupName = AppGlobal.CRM;
                                                                    //    categorySub.Code = AppGlobal.CategoryMain;
                                                                    //    categorySub.ParentID = 0;
                                                                    //    categorySub.Initialization(InitType.Insert, RequestUserID);
                                                                    //    _configRepository.Create(categorySub);
                                                                    //}
                                                                    //if (categorySub.ID > 0)
                                                                    //{
                                                                    //    productProperty.CategorySubID = categorySub.ID;
                                                                    //}
                                                                }

                                                                if (workSheet.Cells[i, 5].Value != null)
                                                                {
                                                                    productProperty.CompanyName = workSheet.Cells[i, 5].Value.ToString().Trim();
                                                                    //Membership company = _membershipRepository.GetByAccount(productProperty.CompanyName);
                                                                    //if (company == null)
                                                                    //{
                                                                    //    company = new Membership();
                                                                    //    company.ParentID = AppGlobal.ParentIDCompetitor;
                                                                    //    company.Active = true;
                                                                    //    company.Account = productProperty.CompanyName;
                                                                    //    company.Initialization(InitType.Insert, RequestUserID);
                                                                    //    _membershipRepository.Create(company);
                                                                    //}
                                                                    //if (company.ID > 0)
                                                                    //{
                                                                    //    productProperty.CompanyID = company.ID;
                                                                    //}
                                                                }
                                                                if (workSheet.Cells[i, 6].Value != null)
                                                                {
                                                                    productProperty.CorpCopy = workSheet.Cells[i, 6].Value.ToString().Trim();
                                                                    //Config corpCopy = _configRepository.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.CorpCopy, productProperty.CorpCopy);
                                                                    //if (corpCopy == null)
                                                                    //{
                                                                    //    corpCopy = new Config();
                                                                    //    corpCopy.CodeName = productProperty.CorpCopy;
                                                                    //    corpCopy.GroupName = AppGlobal.CRM;
                                                                    //    corpCopy.Code = AppGlobal.CorpCopy;
                                                                    //    corpCopy.ParentID = 0;
                                                                    //    corpCopy.Initialization(InitType.Insert, RequestUserID);
                                                                    //    _configRepository.Create(corpCopy);
                                                                    //}
                                                                    //if (corpCopy.ID > 0)
                                                                    //{
                                                                    //    productProperty.CorpCopyID = corpCopy.ID;
                                                                    //}
                                                                }
                                                                if (workSheet.Cells[i, 7].Value != null)
                                                                {
                                                                    string sOECompany = workSheet.Cells[i, 7].Value.ToString().Trim();
                                                                    sOECompany = sOECompany.Replace(@"%", "");
                                                                    sOECompany = sOECompany.Trim();
                                                                    try
                                                                    {
                                                                        productProperty.SOECompany = decimal.Parse(sOECompany);
                                                                        if (productProperty.SOECompany <= 1)
                                                                        {
                                                                            productProperty.SOECompany = productProperty.SOECompany * 100;
                                                                        }
                                                                        if (productProperty.SOECompany < 60)
                                                                        {
                                                                            productProperty.FeatureCorpID = AppGlobal.MentionID;
                                                                        }
                                                                        else
                                                                        {
                                                                            productProperty.FeatureCorpID = AppGlobal.FeatureID;
                                                                        }
                                                                        //Config feature = _configRepository.GetByID(productProperty.FeatureCorpID.Value);
                                                                        //if (feature != null)
                                                                        //{
                                                                        //    productProperty.FeatureCorp = feature.CodeName;
                                                                        //}
                                                                    }
                                                                    catch
                                                                    {
                                                                    }
                                                                }
                                                                if (workSheet.Cells[i, 8].Value != null)
                                                                {
                                                                    productProperty.FeatureCorp = workSheet.Cells[i, 8].Value.ToString().Trim();
                                                                    //Config featureCorp = _configRepository.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.Feature, productProperty.FeatureCorp);
                                                                    //if (featureCorp == null)
                                                                    //{
                                                                    //    featureCorp = new Config();
                                                                    //    featureCorp.CodeName = productProperty.FeatureCorp;
                                                                    //    featureCorp.GroupName = AppGlobal.CRM;
                                                                    //    featureCorp.Code = AppGlobal.Feature;
                                                                    //    featureCorp.ParentID = 0;
                                                                    //    featureCorp.Initialization(InitType.Insert, RequestUserID);
                                                                    //    _configRepository.Create(featureCorp);
                                                                    //}
                                                                    //if (featureCorp.ID > 0)
                                                                    //{
                                                                    //    productProperty.FeatureCorpID = featureCorp.ID;
                                                                    //}
                                                                }
                                                                if (workSheet.Cells[i, 9].Value != null)
                                                                {
                                                                    productProperty.SentimentCorp = workSheet.Cells[i, 9].Value.ToString().Trim();
                                                                    //Config sentimentCorp = _configRepository.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.Sentiment, productProperty.SentimentCorp);
                                                                    //if (sentimentCorp == null)
                                                                    //{
                                                                    //    sentimentCorp = new Config();
                                                                    //    sentimentCorp.CodeName = productProperty.SentimentCorp;
                                                                    //    sentimentCorp.GroupName = AppGlobal.CRM;
                                                                    //    sentimentCorp.Code = AppGlobal.Sentiment;
                                                                    //    sentimentCorp.ParentID = 0;
                                                                    //    sentimentCorp.Initialization(InitType.Insert, RequestUserID);
                                                                    //    _configRepository.Create(sentimentCorp);
                                                                    //}
                                                                    //if (sentimentCorp.ID > 0)
                                                                    //{
                                                                    //    productProperty.SentimentCorpID = sentimentCorp.ID;

                                                                    //}
                                                                    if (string.IsNullOrEmpty(productProperty.SentimentProduct))
                                                                    {
                                                                        productProperty.SentimentProduct = productProperty.SentimentCorp;
                                                                    }
                                                                    if (productProperty.SentimentProductID == null)
                                                                    {
                                                                        productProperty.SentimentProductID = productProperty.SentimentCorpID;
                                                                    }
                                                                }
                                                                if (workSheet.Cells[i, 10].Value != null)
                                                                {
                                                                    productProperty.SegmentProduct = workSheet.Cells[i, 10].Value.ToString().Trim();
                                                                    //Config segmentProduct = _configRepository.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.Segment, productProperty.SegmentProduct);
                                                                    //if (segmentProduct == null)
                                                                    //{
                                                                    //    segmentProduct = new Config();
                                                                    //    segmentProduct.ParentID = model.IndustryID;
                                                                    //    segmentProduct.CodeName = productProperty.SegmentProduct;
                                                                    //    segmentProduct.GroupName = AppGlobal.CRM;
                                                                    //    segmentProduct.Code = AppGlobal.Segment;
                                                                    //    segmentProduct.ParentID = 0;
                                                                    //    segmentProduct.Initialization(InitType.Insert, RequestUserID);
                                                                    //    _configRepository.Create(segmentProduct);
                                                                    //}
                                                                    //if (segmentProduct.ID > 0)
                                                                    //{
                                                                    //    productProperty.SegmentProductID = segmentProduct.ID;
                                                                    //    productProperty.SegmentID = segmentProduct.ID;
                                                                    //}
                                                                }
                                                                if (workSheet.Cells[i, 11].Value != null)
                                                                {
                                                                    productProperty.ProductName_ProjectName = workSheet.Cells[i, 11].Value.ToString().Trim();
                                                                    //MembershipPermission membershipPermissionProduct = _membershipPermissionRepository.GetByCodeAndProductName(AppGlobal.Product, productProperty.ProductName_ProjectName);
                                                                    //if (membershipPermissionProduct == null)
                                                                    //{
                                                                    //    membershipPermissionProduct = new MembershipPermission();
                                                                    //    membershipPermissionProduct.Code = AppGlobal.Product;
                                                                    //    membershipPermissionProduct.MembershipID = productProperty.CompanyID;
                                                                    //    membershipPermissionProduct.IndustryID = productProperty.IndustryID;
                                                                    //    membershipPermissionProduct.SegmentID = productProperty.SegmentID;
                                                                    //    membershipPermissionProduct.Initialization(InitType.Insert, RequestUserID);
                                                                    //    _membershipPermissionRepository.Create(membershipPermissionProduct);
                                                                    //}
                                                                    //if (membershipPermissionProduct.ID > 0)
                                                                    //{
                                                                    //    productProperty.ProductID = membershipPermissionProduct.ID;
                                                                    //}
                                                                }
                                                                if (workSheet.Cells[i, 12].Value != null)
                                                                {
                                                                    string sOEProduct = workSheet.Cells[i, 12].Value.ToString().Trim();
                                                                    sOEProduct = sOEProduct.Replace(@"%", "");
                                                                    sOEProduct = sOEProduct.Trim();
                                                                    try
                                                                    {
                                                                        productProperty.SOEProduct = decimal.Parse(sOEProduct);
                                                                        if (productProperty.SOEProduct <= 1)
                                                                        {
                                                                            productProperty.SOEProduct = productProperty.SOEProduct * 100;
                                                                        }
                                                                        if (productProperty.SOEProduct < 60)
                                                                        {
                                                                            productProperty.FeatureProductID = AppGlobal.MentionID;
                                                                        }
                                                                        else
                                                                        {
                                                                            productProperty.FeatureProductID = AppGlobal.FeatureID;
                                                                        }
                                                                        //Config feature = _configRepository.GetByID(productProperty.FeatureProductID.Value);
                                                                        //if (feature != null)
                                                                        //{
                                                                        //    productProperty.FeatureProduct = feature.CodeName;
                                                                        //}
                                                                    }
                                                                    catch
                                                                    {
                                                                    }
                                                                }
                                                                if (workSheet.Cells[i, 13].Value != null)
                                                                {
                                                                    productProperty.FeatureProduct = workSheet.Cells[i, 13].Value.ToString().Trim();
                                                                    //Config featureProduct = _configRepository.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.Feature, productProperty.FeatureProduct);
                                                                    //if (featureProduct == null)
                                                                    //{
                                                                    //    featureProduct = new Config();
                                                                    //    featureProduct.CodeName = productProperty.FeatureProduct;
                                                                    //    featureProduct.GroupName = AppGlobal.CRM;
                                                                    //    featureProduct.Code = AppGlobal.Feature;
                                                                    //    featureProduct.ParentID = 0;
                                                                    //    featureProduct.Initialization(InitType.Insert, RequestUserID);
                                                                    //    _configRepository.Create(featureProduct);
                                                                    //}
                                                                    //if (featureProduct.ID > 0)
                                                                    //{
                                                                    //    productProperty.FeatureProductID = featureProduct.ID;
                                                                    //}
                                                                }
                                                                if (workSheet.Cells[i, 14].Value != null)
                                                                {
                                                                    productProperty.SentimentProduct = workSheet.Cells[i, 14].Value.ToString().Trim();
                                                                    //Config sentimentProduct = _configRepository.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.Sentiment, productProperty.SentimentProduct);
                                                                    //if (sentimentProduct == null)
                                                                    //{
                                                                    //    sentimentProduct = new Config();
                                                                    //    sentimentProduct.CodeName = productProperty.SentimentProduct;
                                                                    //    sentimentProduct.GroupName = AppGlobal.CRM;
                                                                    //    sentimentProduct.Code = AppGlobal.Sentiment;
                                                                    //    sentimentProduct.ParentID = 0;
                                                                    //    sentimentProduct.Initialization(InitType.Insert, RequestUserID);
                                                                    //    _configRepository.Create(sentimentProduct);
                                                                    //}
                                                                    //if (sentimentProduct.ID > 0)
                                                                    //{
                                                                    //    productProperty.SentimentProductID = sentimentProduct.ID;
                                                                    //}
                                                                    if (string.IsNullOrEmpty(productProperty.SentimentCorp))
                                                                    {
                                                                        productProperty.SentimentCorp = productProperty.SentimentProduct;
                                                                    }
                                                                    if (productProperty.SentimentCorpID == null)
                                                                    {
                                                                        productProperty.SentimentCorpID = productProperty.SentimentProductID;
                                                                    }
                                                                }
                                                                if (workSheet.Cells[i, 20].Value != null)
                                                                {
                                                                    productProperty.TierCommsights = workSheet.Cells[i, 20].Value.ToString().Trim();
                                                                    productProperty.TierCommsightsID = AppGlobal.TierOtherID;
                                                                    if (productProperty.TierCommsights.Contains(@"Mass") == true)
                                                                    {
                                                                        productProperty.TierCommsightsID = AppGlobal.TierMassMediaID;
                                                                    }
                                                                    if (productProperty.TierCommsights.Contains(@"Local ") == true)
                                                                    {
                                                                        productProperty.TierCommsightsID = AppGlobal.TierLocalMediaID;
                                                                    }
                                                                    if (productProperty.TierCommsights.Contains(@"Other") == true)
                                                                    {
                                                                        productProperty.TierCommsightsID = AppGlobal.TierOtherID;
                                                                    }
                                                                    if (productProperty.TierCommsights.Contains(@"Portal") == true)
                                                                    {
                                                                        productProperty.TierCommsightsID = AppGlobal.TierPortalID;
                                                                    }
                                                                    if (productProperty.TierCommsights.Contains(@"Industry") == true)
                                                                    {
                                                                        productProperty.TierCommsightsID = AppGlobal.TierIndustryID;
                                                                    }
                                                                    //Uri myUri = new Uri(product.URLCode);
                                                                    //string domain = myUri.Host;
                                                                    //Config website = _configRepository.GetByGroupNameAndCodeAndTitle(AppGlobal.CRM, AppGlobal.Website, domain);
                                                                    //if (website != null)
                                                                    //{
                                                                    //    if (website.ID > 0)
                                                                    //    {
                                                                    //        Config tier = _configRepository.GetByGroupNameAndCodeAndParentIDAndTierID(AppGlobal.CRM, AppGlobal.Tier, website.ID, productProperty.TierCommsightsID.Value);
                                                                    //        if (tier == null)
                                                                    //        {
                                                                    //            tier = new Config();
                                                                    //            tier.GroupName = AppGlobal.CRM;
                                                                    //            tier.Code = AppGlobal.Tier;
                                                                    //            tier.ParentID = website.ID;
                                                                    //            tier.TierID = productProperty.TierCommsightsID;
                                                                    //            tier.IndustryID = model.IndustryID;
                                                                    //            tier.Initialization(InitType.Insert, RequestUserID);
                                                                    //            _configRepository.Create(tier);
                                                                    //        }
                                                                    //    }
                                                                    //}
                                                                }
                                                                if (workSheet.Cells[i, 21].Value != null)
                                                                {
                                                                    productProperty.TierCustomer = workSheet.Cells[i, 21].Value.ToString().Trim();
                                                                }
                                                                if (workSheet.Cells[i, 22].Value != null)
                                                                {
                                                                    productProperty.SpokePersonName = workSheet.Cells[i, 22].Value.ToString().Trim();
                                                                }
                                                                if (workSheet.Cells[i, 23].Value != null)
                                                                {
                                                                    productProperty.SpokePersonTitle = workSheet.Cells[i, 23].Value.ToString().Trim();
                                                                }
                                                                if (workSheet.Cells[i, 24].Value != null)
                                                                {
                                                                    try
                                                                    {
                                                                        productProperty.ToneValue = decimal.Parse(workSheet.Cells[i, 24].Value.ToString().Trim());
                                                                    }
                                                                    catch
                                                                    {
                                                                    }
                                                                }
                                                                if (workSheet.Cells[i, 25].Value != null)
                                                                {
                                                                    try
                                                                    {
                                                                        productProperty.HeadlineValue = decimal.Parse(workSheet.Cells[i, 25].Value.ToString().Trim());
                                                                    }
                                                                    catch
                                                                    {
                                                                    }
                                                                }
                                                                if (workSheet.Cells[i, 26].Value != null)
                                                                {
                                                                    try
                                                                    {
                                                                        productProperty.SpokePersonValue = decimal.Parse(workSheet.Cells[i, 26].Value.ToString().Trim());
                                                                    }
                                                                    catch
                                                                    {
                                                                    }
                                                                }
                                                                if (workSheet.Cells[i, 27].Value != null)
                                                                {
                                                                    try
                                                                    {
                                                                        productProperty.FeatureValue = decimal.Parse(workSheet.Cells[i, 27].Value.ToString().Trim());
                                                                    }
                                                                    catch
                                                                    {
                                                                    }
                                                                }
                                                                if (workSheet.Cells[i, 28].Value != null)
                                                                {
                                                                    try
                                                                    {
                                                                        productProperty.TierValue = decimal.Parse(workSheet.Cells[i, 28].Value.ToString().Trim());
                                                                    }
                                                                    catch
                                                                    {
                                                                    }
                                                                }
                                                                if (workSheet.Cells[i, 29].Value != null)
                                                                {
                                                                    try
                                                                    {
                                                                        productProperty.PictureValue = decimal.Parse(workSheet.Cells[i, 29].Value.ToString().Trim());
                                                                    }
                                                                    catch
                                                                    {
                                                                    }
                                                                }
                                                                if (workSheet.Cells[i, 30].Value != null)
                                                                {
                                                                    try
                                                                    {
                                                                        productProperty.MPS = decimal.Parse(workSheet.Cells[i, 30].Value.ToString().Trim());
                                                                    }
                                                                    catch
                                                                    {
                                                                    }
                                                                }
                                                                if (workSheet.Cells[i, 31].Value != null)
                                                                {
                                                                    try
                                                                    {
                                                                        productProperty.ROME_Corp_VND = decimal.Parse(workSheet.Cells[i, 31].Value.ToString().Trim());
                                                                    }
                                                                    catch
                                                                    {
                                                                    }
                                                                }
                                                                if (workSheet.Cells[i, 32].Value != null)
                                                                {
                                                                    try
                                                                    {
                                                                        productProperty.ROME_Product_VND = decimal.Parse(workSheet.Cells[i, 32].Value.ToString().Trim());
                                                                    }
                                                                    catch
                                                                    {
                                                                    }
                                                                }
                                                                productProperty.AssessID = productProperty.SentimentCorpID;
                                                                _productPropertyRepository.Create(productProperty);
                                                                if (productProperty.ID > 0)
                                                                {
                                                                    ReportMonthlyProperty reportMonthlyProperty = new ReportMonthlyProperty();
                                                                    reportMonthlyProperty.Initialization(InitType.Insert, RequestUserID);
                                                                    reportMonthlyProperty.ParentID = model.ID;
                                                                    reportMonthlyProperty.ProductID = product001.ID;
                                                                    reportMonthlyProperty.ProductPropertyID = productProperty.ID;
                                                                    _reportMonthlyPropertyRepository.Create(reportMonthlyProperty);
                                                                }
                                                            }
                                                        }
                                                    }
                                                    catch (Exception e1)
                                                    {
                                                        string mes1 = e1.Message;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string mes = e.Message;
            }
            string action = "Upload";
            string controller = "ReportMonthly";
            return RedirectToAction(action, controller, new { ID = model.ID });
        }
        public ActionResult UploadDataReportMonthly001(Commsights.Data.Models.ReportMonthly model)
        {
            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files[0];
                    if (file == null || file.Length == 0)
                    {
                    }
                    if (file != null)
                    {
                        string fileExtension = Path.GetExtension(file.FileName);
                        string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                        fileName = "ReportMonthly_" + model.CompanyID + "_" + model.Year + "_" + model.Month;
                        fileName = fileName + "-" + AppGlobal.DateTimeCode + fileExtension;
                        var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, AppGlobal.FTPUploadExcel, fileName);
                        using (var stream = new FileStream(physicalPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                            FileInfo fileLocation = new FileInfo(physicalPath);
                            if (fileLocation.Length > 0)
                            {
                                if ((fileExtension == ".xlsx") || (fileExtension == ".xls"))
                                {
                                    using (ExcelPackage package = new ExcelPackage(stream))
                                    {
                                        if (package.Workbook.Worksheets.Count > 0)
                                        {
                                            ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                                            if (workSheet != null)
                                            {
                                                model.Note = fileName;
                                                model.Initialization(InitType.Insert, RequestUserID);
                                                model.IsMonthly = true;
                                                model.Title = "ReportMonthly_" + model.CompanyID + "_" + model.Year + "_" + model.Month + "_" + AppGlobal.DateTimeCode;
                                                Membership customer = _membershipRepository.GetByID(model.CompanyID.Value);
                                                if (customer != null)
                                                {
                                                    model.Title = "ReportMonthly_" + model.CompanyID + "_" + customer.Account + "_" + model.Year + "_" + model.Month + "_" + AppGlobal.DateTimeCode;
                                                }
                                                _reportMonthlyRepository.Create(model);
                                                DataTable tbl = new DataTable();
                                                bool hasHeader = true;
                                                foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
                                                {
                                                    tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                                                }
                                                var startRow = hasHeader ? 2 : 1;
                                                string feature_Corp = "";
                                                string feature_Product = "";
                                                string url = "";
                                                string domain = "";
                                                int index = 0;
                                                List<string> listURL = new List<string>();
                                                List<string> listMedia = new List<string>();
                                                for (int rowNum = startRow; rowNum <= workSheet.Dimension.End.Row; rowNum++)
                                                {
                                                    var wsRow = workSheet.Cells[rowNum, 1, rowNum, workSheet.Dimension.End.Column];
                                                    DataRow row = tbl.Rows.Add();
                                                    foreach (var cell in wsRow)
                                                    {
                                                        string address = cell.Address.Substring(0, 1);
                                                        switch (address)
                                                        {
                                                            case "G":
                                                                if (cell.Value != null)
                                                                {
                                                                    string value = cell.Value.ToString();
                                                                    value = value.Replace(@"%", @"");
                                                                    row[cell.Start.Column - 1] = cell.Value;
                                                                    try
                                                                    {
                                                                        decimal sOE_Corp = decimal.Parse(value);
                                                                        sOE_Corp = sOE_Corp * 100;
                                                                        row[cell.Start.Column - 1] = (int)sOE_Corp;
                                                                        if (sOE_Corp < 60)
                                                                        {
                                                                            feature_Corp = "Mention";
                                                                        }
                                                                        else
                                                                        {
                                                                            feature_Corp = "Feature";
                                                                        }
                                                                    }
                                                                    catch
                                                                    {
                                                                    }
                                                                }
                                                                break;
                                                            case "H":
                                                                if (cell.Value == null)
                                                                {
                                                                    if (!string.IsNullOrEmpty(feature_Corp))
                                                                    {
                                                                        row[cell.Start.Column - 1] = feature_Corp;
                                                                        feature_Corp = "";
                                                                    }
                                                                }
                                                                break;
                                                            case "L":
                                                                if (cell.Value != null)
                                                                {
                                                                    string value = cell.Value.ToString();
                                                                    value = value.Replace(@"%", @"");
                                                                    row[cell.Start.Column - 1] = cell.Value;
                                                                    try
                                                                    {
                                                                        decimal sOE_Product = decimal.Parse(value);
                                                                        sOE_Product = sOE_Product * 100;
                                                                        row[cell.Start.Column - 1] = (int)sOE_Product;
                                                                        if (sOE_Product < 60)
                                                                        {
                                                                            feature_Product = "Mention";
                                                                        }
                                                                        else
                                                                        {
                                                                            feature_Product = "Feature";
                                                                        }
                                                                    }
                                                                    catch
                                                                    {
                                                                    }
                                                                }
                                                                break;
                                                            case "M":
                                                                if (cell.Value == null)
                                                                {
                                                                    if (!string.IsNullOrEmpty(feature_Product))
                                                                    {
                                                                        row[cell.Start.Column - 1] = feature_Product;
                                                                        feature_Product = "";
                                                                    }
                                                                }
                                                                break;
                                                            case "O":
                                                                row[cell.Start.Column - 1] = cell.Value;
                                                                if (cell.Value != null)
                                                                {
                                                                    if (cell.Hyperlink != null)
                                                                    {
                                                                        url = cell.Hyperlink.AbsoluteUri.Trim();
                                                                        listURL.Add(url);
                                                                        Uri myUri = new Uri(url);
                                                                        domain = myUri.Host;
                                                                        domain = domain.Replace(@"www.", @"");
                                                                        listMedia.Add(domain);
                                                                    }
                                                                }
                                                                break;
                                                            case "Q":
                                                                row[cell.Start.Column - 1] = cell.Value;
                                                                if (cell.Value == null)
                                                                {
                                                                    row[cell.Start.Column - 1] = url;
                                                                }
                                                                else
                                                                {
                                                                    url = cell.Value.ToString();
                                                                }
                                                                break;
                                                            default:
                                                                row[cell.Start.Column - 1] = cell.Value;
                                                                break;
                                                        }
                                                    }
                                                    string url001 = tbl.Rows[index]["URL"].ToString();
                                                    if (string.IsNullOrEmpty(url001))
                                                    {
                                                        tbl.Rows[index]["URL"] = url.Trim();
                                                        try
                                                        {

                                                            tbl.Rows[index]["Media"] = domain.Trim();
                                                        }
                                                        catch
                                                        {
                                                        }
                                                    }
                                                    string domain001 = tbl.Rows[index]["Media"].ToString();
                                                    if (string.IsNullOrEmpty(domain001))
                                                    {
                                                        try
                                                        {
                                                            tbl.Rows[index]["Media"] = domain;
                                                        }
                                                        catch
                                                        {
                                                        }
                                                    }
                                                    index = index + 1;
                                                }
                                                if (tbl.Columns.Count < 33)
                                                {
                                                    tbl.Columns.Add(new DataColumn("Media"));
                                                }
                                                //for (int i = 0; i < tbl.Rows.Count; i++)
                                                //{
                                                //    try
                                                //    {
                                                //        tbl.Rows[i]["URL"] = listURL[i].Trim();
                                                //    }
                                                //    catch
                                                //    {
                                                //    }
                                                //    try
                                                //    {
                                                //        tbl.Rows[i]["Media"] = listMedia[i];
                                                //    }
                                                //    catch
                                                //    {
                                                //    }
                                                //}
                                                _reportMonthlyRepository.InsertItemsByDataTableAndReportMonthlyIDAndRequestUserID(tbl, model.ID, RequestUserID);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string mes = e.Message;
            }
            string action = "Upload";
            string controller = "ReportMonthly";
            return RedirectToAction(action, controller, new { ID = model.ID });
        }
    }
}
