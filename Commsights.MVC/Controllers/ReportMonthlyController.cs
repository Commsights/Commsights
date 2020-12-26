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
using System.Threading;
using System.Drawing;
using OfficeOpenXml.Style;

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
        public IActionResult MonthlyReport(int ID)
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
        public ActionResult GetProductPropertyByReportMonthlyIDToList([DataSourceRequest] DataSourceRequest request, int reportMonthlyID)
        {
            var data = _productPropertyRepository.GetByReportMonthlyIDToList(reportMonthlyID);
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
        public ActionResult GetChannelByIDWithoutSUMToList(int ID)
        {
            return Json(_reportMonthlyRepository.GetChannelByIDWithoutSUMToList(ID));
        }
        public ActionResult GetChannelAndFeatureByIDToListToJSON(int ID)
        {
            return Json(_reportMonthlyRepository.GetChannelAndFeatureByIDToList(ID));
        }
        public ActionResult GetChannelAndFeatureWithoutSUMByIDToList(int ID)
        {
            return Json(_reportMonthlyRepository.GetChannelAndFeatureWithoutSUMByIDToList(ID));
        }
        public ActionResult GetChannelAndMentionByIDToListToJSON(int ID)
        {
            return Json(_reportMonthlyRepository.GetChannelAndMentionByIDToList(ID));
        }
        public ActionResult GetChannelAndMentionWithoutSUMByIDToList(int ID)
        {
            return Json(_reportMonthlyRepository.GetChannelAndMentionWithoutSUMByIDToList(ID));
        }
        public ActionResult GetTierCommsightsByIDToListToJSON(int ID)
        {
            return Json(_reportMonthlyRepository.GetTierCommsightsByIDToList(ID));
        }
        public ActionResult GetTierCommsightsWithoutSUMByIDToList(int ID)
        {
            return Json(_reportMonthlyRepository.GetTierCommsightsWithoutSUMByIDToList(ID));
        }
        public ActionResult GetCompanyAndYearByIDToListToJSON(int ID)
        {
            return Json(_reportMonthlyRepository.GetCompanyAndYearByIDToList(ID));
        }
        public ActionResult GetCompanyAndYearWithoutSUMByIDToList(int ID)
        {
            return Json(_reportMonthlyRepository.GetCompanyAndYearWithoutSUMByIDToList(ID));
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
                string monthString = "";
                string monthLastString = "";
                switch (model.Month)
                {
                    case 1:
                        monthString = "Jan";
                        break;
                    case 2:
                        monthString = "Feb";
                        break;
                    case 3:
                        monthString = "Mar";
                        break;
                    case 4:
                        monthString = "Apr";
                        break;
                    case 5:
                        monthString = "May";
                        break;
                    case 6:
                        monthString = "Jun";
                        break;
                    case 7:
                        monthString = "Jul";
                        break;
                    case 8:
                        monthString = "Aug";
                        break;
                    case 9:
                        monthString = "Sep";
                        break;
                    case 10:
                        monthString = "Oct";
                        break;
                    case 11:
                        monthString = "Nov";
                        break;
                    case 12:
                        monthString = "Dec";
                        break;
                }
                switch (monthLast)
                {
                    case 1:
                        monthLastString = "Jan";
                        break;
                    case 2:
                        monthLastString = "Feb";
                        break;
                    case 3:
                        monthLastString = "Mar";
                        break;
                    case 4:
                        monthLastString = "Apr";
                        break;
                    case 5:
                        monthLastString = "May";
                        break;
                    case 6:
                        monthLastString = "Jun";
                        break;
                    case 7:
                        monthLastString = "Jul";
                        break;
                    case 8:
                        monthLastString = "Aug";
                        break;
                    case 9:
                        monthLastString = "Sep";
                        break;
                    case 10:
                        monthLastString = "Oct";
                        break;
                    case 11:
                        monthLastString = "Nov";
                        break;
                    case 12:
                        monthLastString = "Dec";
                        break;
                }
                List<ReportMonthlyTrendLineDataTransfer> list = _reportMonthlyRepository.GetTrendLineWithoutSUMByIDToList(ID);
                List<ReportMonthlyTrendLineDataTransfer> listCompanyName = _reportMonthlyRepository.GetTrendLineDistinctCompanyNameByIDToList(ID);
                tbl.Columns.Add(new DataColumn("Year"));
                tbl.Columns.Add(new DataColumn("Month"));
                tbl.Columns.Add(new DataColumn("Day"));
                tbl.Columns.Add(new DataColumn("MonthString"));
                tbl.Columns.Add(new DataColumn("Date"));
                foreach (ReportMonthlyTrendLineDataTransfer item in listCompanyName)
                {
                    tbl.Columns.Add(new DataColumn(item.CompanyName));
                }
                for (int i = 1; i < 32; i++)
                {
                    string monthString001 = model.Month.ToString();
                    if (model.Month < 10)
                    {
                        monthString001 = "0" + monthString001;
                    }
                    string dayString001 = i.ToString();
                    if (i < 10)
                    {
                        dayString001 = "0" + dayString001;
                    }
                    DataRow row = tbl.NewRow();
                    row["Year"] = model.Year;
                    row["Month"] = model.Month;
                    row["Day"] = i;
                    row["MonthString"] = monthString + "-" + model.Year;
                    row["Date"] = monthString001 + "/" + dayString001 + "/" + model.Year;
                    tbl.Rows.Add(row);
                }
                for (int i = 1; i < 32; i++)
                {
                    string monthString001 = monthLast.ToString();
                    if (monthLast < 10)
                    {
                        monthString001 = "0" + monthString001;
                    }
                    string dayString001 = i.ToString();
                    if (i < 10)
                    {
                        dayString001 = "0" + dayString001;
                    }
                    DataRow row = tbl.NewRow();
                    row["Year"] = yearLast;
                    row["Month"] = monthLast;
                    row["Day"] = i;
                    row["MonthString"] = monthLastString + "-" + yearLast;
                    row["Date"] = monthString001 + "/" + dayString001 + "/" + yearLast;
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
                    string columnName = tbl.Columns[i].ColumnName.Trim();
                    if ((columnName != "Year") && (columnName != "Month") && (columnName != "Day"))
                    {
                        switch (columnName)
                        {
                            case "MonthString":
                                columnName = "Month";
                                break;
                            case "Date":
                                columnName = "Day";
                                break;
                        }
                        txt.AppendLine("<th class='text-center'><a style='cursor:pointer;'>" + columnName + "</a></th>");
                    }
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
                    txt.AppendLine("<td class='text-center'>" + tbl.Rows[i]["MonthString"].ToString() + "</td>");
                    txt.AppendLine("<td class='text-center'>" + tbl.Rows[i]["Date"].ToString() + "</td>");
                    for (int j = 5; j < tbl.Columns.Count; j++)
                    {
                        string columnName = tbl.Columns[j].ColumnName.Trim();
                        txt.AppendLine("<td class='text-right'>" + tbl.Rows[i][columnName].ToString() + "</td>");
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
                                                                    try
                                                                    {
                                                                        productProperty.Source = int.Parse(workSheet.Cells[i, 1].Value.ToString().Trim());
                                                                    }
                                                                    catch
                                                                    {

                                                                    }
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
                                                int count = workSheet.Dimension.End.Row;
                                                if (count > 1)
                                                {
                                                    model.Note = fileName;
                                                    model.Initialization(InitType.Insert, RequestUserID);
                                                    model.IsMonthly = true;
                                                    model.Title = "ReportMonthly_" + model.CompanyID + "_" + model.Year + "_" + model.Month;
                                                    Membership customer = _membershipRepository.GetByID(model.CompanyID.Value);
                                                    if (customer != null)
                                                    {
                                                        model.Title = "ReportMonthly_" + model.CompanyID + "_" + customer.Account + "_" + model.Year + "_" + model.Month;
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
                                                                            if (sOE_Corp < 1)
                                                                            {
                                                                                sOE_Corp = sOE_Corp * 100;
                                                                            }
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
                                                                        catch (Exception e)
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
                                                                    else
                                                                    {
                                                                        row[cell.Start.Column - 1] = cell.Value;
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
                                                                            if (sOE_Product < 1)
                                                                            {
                                                                                sOE_Product = sOE_Product * 100;
                                                                            }
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
                                                                        catch (Exception e)
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
                                                                    else
                                                                    {
                                                                        row[cell.Start.Column - 1] = cell.Value;
                                                                    }
                                                                    break;
                                                                case "O":
                                                                    row[cell.Start.Column - 1] = cell.Value;
                                                                    if (cell.Value != null)
                                                                    {
                                                                        if (cell.Hyperlink != null)
                                                                        {
                                                                            try
                                                                            {
                                                                                url = cell.Hyperlink.AbsoluteUri.Trim();
                                                                                Uri myUri = new Uri(url);
                                                                                domain = myUri.Host;
                                                                                domain = domain.Replace(@"www.", @"");
                                                                            }
                                                                            catch (Exception e)
                                                                            {
                                                                            }
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
                                                                tbl.Rows[index]["MediaTitle"] = domain.Trim();
                                                            }
                                                            catch (Exception e)
                                                            {
                                                            }
                                                        }
                                                        string domain001 = tbl.Rows[index]["MediaTitle"].ToString();
                                                        if (string.IsNullOrEmpty(domain001))
                                                        {
                                                            try
                                                            {
                                                                tbl.Rows[index]["MediaTitle"] = domain;
                                                            }
                                                            catch (Exception e)
                                                            {
                                                            }
                                                        }
                                                        index = index + 1;
                                                    }
                                                    int columnsCount = tbl.Columns.Count;
                                                    for (int i = 36; i < columnsCount; i++)
                                                    {
                                                        tbl.Columns.RemoveAt(i);
                                                    }
                                                    int columnsCount001 = tbl.Columns.Count;
                                                    _reportMonthlyRepository.InsertItemsByProductProperty005AndReportMonthlyIDAndRequestUserID(tbl, model.ID, RequestUserID);
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
        public string ExportExcelByID(CancellationToken cancellationToken, int ID)
        {
            string result = "";
            ReportMonthly model = _reportMonthlyRepository.GetByID(ID);
            if (model != null)
            {
                string excelName = model.Title + ".xlsx";
                var streamExport = new MemoryStream();
                using (var package = new ExcelPackage(streamExport))
                {
                    Color color = Color.FromArgb(int.Parse("#c00000".Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier));
                    InitializationIndustry(package, color, ID);
                    InitializationCompanyCount(package, color, ID);
                    InitializationFeature(package, color, ID);
                    InitializationSentiment(package, color, ID);
                    InitializationChannel(package, color, ID);
                    InitializationTierCommsights(package, color, ID);
                    InitializationTrendline(package, color, ID);
                    InitializationTopTitles(package, color, ID);
                    InitializationData(package, color, ID);
                    package.Save();
                }
                streamExport.Position = 0;
                var physicalPathCreate = Path.Combine(_hostingEnvironment.WebRootPath, AppGlobal.FTPDownloadReprotMonth, excelName);
                using (var stream = new FileStream(physicalPathCreate, FileMode.Create))
                {
                    streamExport.CopyTo(stream);
                }
                result = AppGlobal.DomainSub + AppGlobal.URLDownloadReprotMonth + excelName;
            }
            return result;
        }
        private void InitializationIndustry(ExcelPackage package, Color color, int ID)
        {
            var segment = package.Workbook.Worksheets.Add("Segment");
            segment.Cells[1, 1].Value = "Segment";
            segment.Cells[1, 1].Style.Font.Bold = true;
            segment.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            segment.Cells[1, 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
            segment.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            segment.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(color);
            segment.Cells[1, 1].Style.Font.Name = "Times New Roman";
            segment.Cells[1, 1].Style.Font.Size = 12;
            segment.Cells[1, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            segment.Cells[1, 1].Style.Border.Top.Color.SetColor(Color.Black);
            segment.Cells[1, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            segment.Cells[1, 1].Style.Border.Left.Color.SetColor(Color.Black);
            segment.Cells[1, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            segment.Cells[1, 1].Style.Border.Right.Color.SetColor(Color.Black);
            segment.Cells[1, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            segment.Cells[1, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

            segment.Cells[1, 2].Value = "Count";
            segment.Cells[1, 2].Style.Font.Bold = true;
            segment.Cells[1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            segment.Cells[1, 2].Style.Font.Color.SetColor(System.Drawing.Color.White);
            segment.Cells[1, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
            segment.Cells[1, 2].Style.Fill.BackgroundColor.SetColor(color);
            segment.Cells[1, 2].Style.Font.Name = "Times New Roman";
            segment.Cells[1, 2].Style.Font.Size = 12;
            segment.Cells[1, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            segment.Cells[1, 2].Style.Border.Top.Color.SetColor(Color.Black);
            segment.Cells[1, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            segment.Cells[1, 2].Style.Border.Left.Color.SetColor(Color.Black);
            segment.Cells[1, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            segment.Cells[1, 2].Style.Border.Right.Color.SetColor(Color.Black);
            segment.Cells[1, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            segment.Cells[1, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

            segment.Cells[1, 3].Value = "Percent (%)";
            segment.Cells[1, 3].Style.Font.Bold = true;
            segment.Cells[1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            segment.Cells[1, 3].Style.Font.Color.SetColor(System.Drawing.Color.White);
            segment.Cells[1, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
            segment.Cells[1, 3].Style.Fill.BackgroundColor.SetColor(color);
            segment.Cells[1, 3].Style.Font.Name = "Times New Roman";
            segment.Cells[1, 3].Style.Font.Size = 12;
            segment.Cells[1, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            segment.Cells[1, 3].Style.Border.Top.Color.SetColor(Color.Black);
            segment.Cells[1, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            segment.Cells[1, 3].Style.Border.Left.Color.SetColor(Color.Black);
            segment.Cells[1, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            segment.Cells[1, 3].Style.Border.Right.Color.SetColor(Color.Black);
            segment.Cells[1, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            segment.Cells[1, 3].Style.Border.Bottom.Color.SetColor(Color.Black);
            List<ReportMonthlyIndustryDataTransfer> listReportMonthlyIndustryDataTransfer = _reportMonthlyRepository.GetIndustryByIDToList(ID);
            int row = 2;
            foreach (ReportMonthlyIndustryDataTransfer item in listReportMonthlyIndustryDataTransfer)
            {
                segment.Cells[row, 1].Value = item.SegmentProduct;
                segment.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                segment.Cells[row, 1].Style.Font.Name = "Times New Roman";
                segment.Cells[row, 1].Style.Font.Size = 12;
                segment.Cells[row, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                segment.Cells[row, 1].Style.Border.Top.Color.SetColor(Color.Black);
                segment.Cells[row, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                segment.Cells[row, 1].Style.Border.Left.Color.SetColor(Color.Black);
                segment.Cells[row, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                segment.Cells[row, 1].Style.Border.Right.Color.SetColor(Color.Black);
                segment.Cells[row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                segment.Cells[row, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

                segment.Cells[row, 2].Value = item.ProductCount;
                segment.Cells[row, 2].Style.Numberformat.Format = "#,##0";
                segment.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                segment.Cells[row, 2].Style.Font.Name = "Times New Roman";
                segment.Cells[row, 2].Style.Font.Size = 12;
                segment.Cells[row, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                segment.Cells[row, 2].Style.Border.Top.Color.SetColor(Color.Black);
                segment.Cells[row, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                segment.Cells[row, 2].Style.Border.Left.Color.SetColor(Color.Black);
                segment.Cells[row, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                segment.Cells[row, 2].Style.Border.Right.Color.SetColor(Color.Black);
                segment.Cells[row, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                segment.Cells[row, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

                segment.Cells[row, 3].Value = item.ProductPercent;
                segment.Cells[row, 3].Style.Numberformat.Format = "#,##0";
                segment.Cells[row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                segment.Cells[row, 3].Style.Font.Name = "Times New Roman";
                segment.Cells[row, 3].Style.Font.Size = 12;
                segment.Cells[row, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                segment.Cells[row, 3].Style.Border.Top.Color.SetColor(Color.Black);
                segment.Cells[row, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                segment.Cells[row, 3].Style.Border.Left.Color.SetColor(Color.Black);
                segment.Cells[row, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                segment.Cells[row, 3].Style.Border.Right.Color.SetColor(Color.Black);
                segment.Cells[row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                segment.Cells[row, 3].Style.Border.Bottom.Color.SetColor(Color.Black);
                row = row + 1;
            }
            segment.Column(1).AutoFit();
            segment.Column(2).AutoFit();
            segment.Column(3).AutoFit();
        }
        private void InitializationCompanyCount(ExcelPackage package, Color color, int ID)
        {
            var companyCount = package.Workbook.Worksheets.Add("Company Count");
            companyCount.Cells[1, 1].Value = "Company";
            companyCount.Cells[1, 1].Style.Font.Bold = true;
            companyCount.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            companyCount.Cells[1, 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
            companyCount.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            companyCount.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(color);
            companyCount.Cells[1, 1].Style.Font.Name = "Times New Roman";
            companyCount.Cells[1, 1].Style.Font.Size = 12;
            companyCount.Cells[1, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[1, 1].Style.Border.Top.Color.SetColor(Color.Black);
            companyCount.Cells[1, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[1, 1].Style.Border.Left.Color.SetColor(Color.Black);
            companyCount.Cells[1, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[1, 1].Style.Border.Right.Color.SetColor(Color.Black);
            companyCount.Cells[1, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[1, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

            companyCount.Cells[1, 2].Value = "News (Last month)";
            companyCount.Cells[1, 2].Style.Font.Bold = true;
            companyCount.Cells[1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            companyCount.Cells[1, 2].Style.Font.Color.SetColor(System.Drawing.Color.White);
            companyCount.Cells[1, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
            companyCount.Cells[1, 2].Style.Fill.BackgroundColor.SetColor(color);
            companyCount.Cells[1, 2].Style.Font.Name = "Times New Roman";
            companyCount.Cells[1, 2].Style.Font.Size = 12;
            companyCount.Cells[1, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[1, 2].Style.Border.Top.Color.SetColor(Color.Black);
            companyCount.Cells[1, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[1, 2].Style.Border.Left.Color.SetColor(Color.Black);
            companyCount.Cells[1, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[1, 2].Style.Border.Right.Color.SetColor(Color.Black);
            companyCount.Cells[1, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[1, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

            companyCount.Cells[1, 3].Value = "Media value (Last month)";
            companyCount.Cells[1, 3].Style.Font.Bold = true;
            companyCount.Cells[1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            companyCount.Cells[1, 3].Style.Font.Color.SetColor(System.Drawing.Color.White);
            companyCount.Cells[1, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
            companyCount.Cells[1, 3].Style.Fill.BackgroundColor.SetColor(color);
            companyCount.Cells[1, 3].Style.Font.Name = "Times New Roman";
            companyCount.Cells[1, 3].Style.Font.Size = 12;
            companyCount.Cells[1, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[1, 3].Style.Border.Top.Color.SetColor(Color.Black);
            companyCount.Cells[1, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[1, 3].Style.Border.Left.Color.SetColor(Color.Black);
            companyCount.Cells[1, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[1, 3].Style.Border.Right.Color.SetColor(Color.Black);
            companyCount.Cells[1, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[1, 3].Style.Border.Bottom.Color.SetColor(Color.Black);

            companyCount.Cells[1, 4].Value = "News (This month)";
            companyCount.Cells[1, 4].Style.Font.Bold = true;
            companyCount.Cells[1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            companyCount.Cells[1, 4].Style.Font.Color.SetColor(System.Drawing.Color.White);
            companyCount.Cells[1, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
            companyCount.Cells[1, 4].Style.Fill.BackgroundColor.SetColor(color);
            companyCount.Cells[1, 4].Style.Font.Name = "Times New Roman";
            companyCount.Cells[1, 4].Style.Font.Size = 12;
            companyCount.Cells[1, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[1, 4].Style.Border.Top.Color.SetColor(Color.Black);
            companyCount.Cells[1, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[1, 4].Style.Border.Left.Color.SetColor(Color.Black);
            companyCount.Cells[1, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[1, 4].Style.Border.Right.Color.SetColor(Color.Black);
            companyCount.Cells[1, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[1, 4].Style.Border.Bottom.Color.SetColor(Color.Black);

            companyCount.Cells[1, 5].Value = "Media value (This month)";
            companyCount.Cells[1, 5].Style.Font.Bold = true;
            companyCount.Cells[1, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            companyCount.Cells[1, 5].Style.Font.Color.SetColor(System.Drawing.Color.White);
            companyCount.Cells[1, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
            companyCount.Cells[1, 5].Style.Fill.BackgroundColor.SetColor(color);
            companyCount.Cells[1, 5].Style.Font.Name = "Times New Roman";
            companyCount.Cells[1, 5].Style.Font.Size = 12;
            companyCount.Cells[1, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[1, 5].Style.Border.Top.Color.SetColor(Color.Black);
            companyCount.Cells[1, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[1, 5].Style.Border.Left.Color.SetColor(Color.Black);
            companyCount.Cells[1, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[1, 5].Style.Border.Right.Color.SetColor(Color.Black);
            companyCount.Cells[1, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[1, 5].Style.Border.Bottom.Color.SetColor(Color.Black);

            List<ReportMonthlyIndustryDataTransfer> listReportMonthlyCompanyCount = _reportMonthlyRepository.GetCompanyByIDToList(ID);
            int row = 2;
            foreach (ReportMonthlyIndustryDataTransfer item in listReportMonthlyCompanyCount)
            {
                companyCount.Cells[row, 1].Value = item.CompanyName;
                companyCount.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                companyCount.Cells[row, 1].Style.Font.Name = "Times New Roman";
                companyCount.Cells[row, 1].Style.Font.Size = 12;
                companyCount.Cells[row, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 1].Style.Border.Top.Color.SetColor(Color.Black);
                companyCount.Cells[row, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 1].Style.Border.Left.Color.SetColor(Color.Black);
                companyCount.Cells[row, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 1].Style.Border.Right.Color.SetColor(Color.Black);
                companyCount.Cells[row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

                companyCount.Cells[row, 2].Value = item.ProductNewsLastMonthCount;
                companyCount.Cells[row, 2].Style.Numberformat.Format = "#,##0";
                companyCount.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                companyCount.Cells[row, 2].Style.Font.Name = "Times New Roman";
                companyCount.Cells[row, 2].Style.Font.Size = 12;
                companyCount.Cells[row, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 2].Style.Border.Top.Color.SetColor(Color.Black);
                companyCount.Cells[row, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 2].Style.Border.Left.Color.SetColor(Color.Black);
                companyCount.Cells[row, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 2].Style.Border.Right.Color.SetColor(Color.Black);
                companyCount.Cells[row, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

                companyCount.Cells[row, 3].Value = item.ProductMediaLastMonthValue;
                companyCount.Cells[row, 3].Style.Numberformat.Format = "#,##0";
                companyCount.Cells[row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                companyCount.Cells[row, 3].Style.Font.Name = "Times New Roman";
                companyCount.Cells[row, 3].Style.Font.Size = 12;
                companyCount.Cells[row, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 3].Style.Border.Top.Color.SetColor(Color.Black);
                companyCount.Cells[row, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 3].Style.Border.Left.Color.SetColor(Color.Black);
                companyCount.Cells[row, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 3].Style.Border.Right.Color.SetColor(Color.Black);
                companyCount.Cells[row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 3].Style.Border.Bottom.Color.SetColor(Color.Black);

                companyCount.Cells[row, 4].Value = item.ProductNewsCount;
                companyCount.Cells[row, 4].Style.Numberformat.Format = "#,##0";
                companyCount.Cells[row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                companyCount.Cells[row, 4].Style.Font.Name = "Times New Roman";
                companyCount.Cells[row, 4].Style.Font.Size = 12;
                companyCount.Cells[row, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 4].Style.Border.Top.Color.SetColor(Color.Black);
                companyCount.Cells[row, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 4].Style.Border.Left.Color.SetColor(Color.Black);
                companyCount.Cells[row, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 4].Style.Border.Right.Color.SetColor(Color.Black);
                companyCount.Cells[row, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 4].Style.Border.Bottom.Color.SetColor(Color.Black);

                companyCount.Cells[row, 5].Value = item.ProductMediaThisMonthValue;
                companyCount.Cells[row, 5].Style.Numberformat.Format = "#,##0";
                companyCount.Cells[row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                companyCount.Cells[row, 5].Style.Font.Name = "Times New Roman";
                companyCount.Cells[row, 5].Style.Font.Size = 12;
                companyCount.Cells[row, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 5].Style.Border.Top.Color.SetColor(Color.Black);
                companyCount.Cells[row, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 5].Style.Border.Left.Color.SetColor(Color.Black);
                companyCount.Cells[row, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 5].Style.Border.Right.Color.SetColor(Color.Black);
                companyCount.Cells[row, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 5].Style.Border.Bottom.Color.SetColor(Color.Black);
                row = row + 1;
            }
            row = row + 1;
            companyCount.Cells[row, 1].Value = "Company";
            companyCount.Cells[row, 1].Style.Font.Bold = true;
            companyCount.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            companyCount.Cells[row, 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
            companyCount.Cells[row, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            companyCount.Cells[row, 1].Style.Fill.BackgroundColor.SetColor(color);
            companyCount.Cells[row, 1].Style.Font.Name = "Times New Roman";
            companyCount.Cells[row, 1].Style.Font.Size = 12;
            companyCount.Cells[row, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[row, 1].Style.Border.Top.Color.SetColor(Color.Black);
            companyCount.Cells[row, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[row, 1].Style.Border.Left.Color.SetColor(Color.Black);
            companyCount.Cells[row, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[row, 1].Style.Border.Right.Color.SetColor(Color.Black);
            companyCount.Cells[row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[row, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

            companyCount.Cells[row, 2].Value = "News (Last month)";
            companyCount.Cells[row, 2].Style.Font.Bold = true;
            companyCount.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            companyCount.Cells[row, 2].Style.Font.Color.SetColor(System.Drawing.Color.White);
            companyCount.Cells[row, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
            companyCount.Cells[row, 2].Style.Fill.BackgroundColor.SetColor(color);
            companyCount.Cells[row, 2].Style.Font.Name = "Times New Roman";
            companyCount.Cells[row, 2].Style.Font.Size = 12;
            companyCount.Cells[row, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[row, 2].Style.Border.Top.Color.SetColor(Color.Black);
            companyCount.Cells[row, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[row, 2].Style.Border.Left.Color.SetColor(Color.Black);
            companyCount.Cells[row, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[row, 2].Style.Border.Right.Color.SetColor(Color.Black);
            companyCount.Cells[row, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[row, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

            companyCount.Cells[row, 3].Value = "Media (Last month)";
            companyCount.Cells[row, 3].Style.Font.Bold = true;
            companyCount.Cells[row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            companyCount.Cells[row, 3].Style.Font.Color.SetColor(System.Drawing.Color.White);
            companyCount.Cells[row, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
            companyCount.Cells[row, 3].Style.Fill.BackgroundColor.SetColor(color);
            companyCount.Cells[row, 3].Style.Font.Name = "Times New Roman";
            companyCount.Cells[row, 3].Style.Font.Size = 12;
            companyCount.Cells[row, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[row, 3].Style.Border.Top.Color.SetColor(Color.Black);
            companyCount.Cells[row, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[row, 3].Style.Border.Left.Color.SetColor(Color.Black);
            companyCount.Cells[row, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[row, 3].Style.Border.Right.Color.SetColor(Color.Black);
            companyCount.Cells[row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[row, 3].Style.Border.Bottom.Color.SetColor(Color.Black);

            companyCount.Cells[row, 4].Value = "News (This month)";
            companyCount.Cells[row, 4].Style.Font.Bold = true;
            companyCount.Cells[row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            companyCount.Cells[row, 4].Style.Font.Color.SetColor(System.Drawing.Color.White);
            companyCount.Cells[row, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
            companyCount.Cells[row, 4].Style.Fill.BackgroundColor.SetColor(color);
            companyCount.Cells[row, 4].Style.Font.Name = "Times New Roman";
            companyCount.Cells[row, 4].Style.Font.Size = 12;
            companyCount.Cells[row, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[row, 4].Style.Border.Top.Color.SetColor(Color.Black);
            companyCount.Cells[row, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[row, 4].Style.Border.Left.Color.SetColor(Color.Black);
            companyCount.Cells[row, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[row, 4].Style.Border.Right.Color.SetColor(Color.Black);
            companyCount.Cells[row, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[row, 4].Style.Border.Bottom.Color.SetColor(Color.Black);

            companyCount.Cells[row, 5].Value = "Media (This month)";
            companyCount.Cells[row, 5].Style.Font.Bold = true;
            companyCount.Cells[row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            companyCount.Cells[row, 5].Style.Font.Color.SetColor(System.Drawing.Color.White);
            companyCount.Cells[row, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
            companyCount.Cells[row, 5].Style.Fill.BackgroundColor.SetColor(color);
            companyCount.Cells[row, 5].Style.Font.Name = "Times New Roman";
            companyCount.Cells[row, 5].Style.Font.Size = 12;
            companyCount.Cells[row, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[row, 5].Style.Border.Top.Color.SetColor(Color.Black);
            companyCount.Cells[row, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[row, 5].Style.Border.Left.Color.SetColor(Color.Black);
            companyCount.Cells[row, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[row, 5].Style.Border.Right.Color.SetColor(Color.Black);
            companyCount.Cells[row, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[row, 5].Style.Border.Bottom.Color.SetColor(Color.Black);

            companyCount.Cells[row, 6].Value = "Growth News (%)";
            companyCount.Cells[row, 6].Style.Font.Bold = true;
            companyCount.Cells[row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            companyCount.Cells[row, 6].Style.Font.Color.SetColor(System.Drawing.Color.White);
            companyCount.Cells[row, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
            companyCount.Cells[row, 6].Style.Fill.BackgroundColor.SetColor(color);
            companyCount.Cells[row, 6].Style.Font.Name = "Times New Roman";
            companyCount.Cells[row, 6].Style.Font.Size = 12;
            companyCount.Cells[row, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[row, 6].Style.Border.Top.Color.SetColor(Color.Black);
            companyCount.Cells[row, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[row, 6].Style.Border.Left.Color.SetColor(Color.Black);
            companyCount.Cells[row, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[row, 6].Style.Border.Right.Color.SetColor(Color.Black);
            companyCount.Cells[row, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[row, 6].Style.Border.Bottom.Color.SetColor(Color.Black);

            companyCount.Cells[row, 7].Value = "Growth Media (%)";
            companyCount.Cells[row, 7].Style.Font.Bold = true;
            companyCount.Cells[row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            companyCount.Cells[row, 7].Style.Font.Color.SetColor(System.Drawing.Color.White);
            companyCount.Cells[row, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
            companyCount.Cells[row, 7].Style.Fill.BackgroundColor.SetColor(color);
            companyCount.Cells[row, 7].Style.Font.Name = "Times New Roman";
            companyCount.Cells[row, 7].Style.Font.Size = 12;
            companyCount.Cells[row, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[row, 7].Style.Border.Top.Color.SetColor(Color.Black);
            companyCount.Cells[row, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[row, 7].Style.Border.Left.Color.SetColor(Color.Black);
            companyCount.Cells[row, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[row, 7].Style.Border.Right.Color.SetColor(Color.Black);
            companyCount.Cells[row, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            companyCount.Cells[row, 7].Style.Border.Bottom.Color.SetColor(Color.Black);

            List<ReportMonthlyIndustryDataTransfer> listReportMonthlyCompanyCount001 = _reportMonthlyRepository.GetIndustryByID001ToList(ID);
            row = row + 1;
            foreach (ReportMonthlyIndustryDataTransfer item in listReportMonthlyCompanyCount001)
            {
                companyCount.Cells[row, 1].Value = item.CompanyName;
                companyCount.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                companyCount.Cells[row, 1].Style.Font.Name = "Times New Roman";
                companyCount.Cells[row, 1].Style.Font.Size = 12;
                companyCount.Cells[row, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 1].Style.Border.Top.Color.SetColor(Color.Black);
                companyCount.Cells[row, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 1].Style.Border.Left.Color.SetColor(Color.Black);
                companyCount.Cells[row, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 1].Style.Border.Right.Color.SetColor(Color.Black);
                companyCount.Cells[row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

                companyCount.Cells[row, 2].Value = item.ProductNewsLastMonthCount;
                companyCount.Cells[row, 2].Style.Numberformat.Format = "#,##0";
                companyCount.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                companyCount.Cells[row, 2].Style.Font.Name = "Times New Roman";
                companyCount.Cells[row, 2].Style.Font.Size = 12;
                companyCount.Cells[row, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 2].Style.Border.Top.Color.SetColor(Color.Black);
                companyCount.Cells[row, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 2].Style.Border.Left.Color.SetColor(Color.Black);
                companyCount.Cells[row, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 2].Style.Border.Right.Color.SetColor(Color.Black);
                companyCount.Cells[row, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

                companyCount.Cells[row, 3].Value = item.ProductMediaLastMonthCount;
                companyCount.Cells[row, 3].Style.Numberformat.Format = "#,##0";
                companyCount.Cells[row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                companyCount.Cells[row, 3].Style.Font.Name = "Times New Roman";
                companyCount.Cells[row, 3].Style.Font.Size = 12;
                companyCount.Cells[row, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 3].Style.Border.Top.Color.SetColor(Color.Black);
                companyCount.Cells[row, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 3].Style.Border.Left.Color.SetColor(Color.Black);
                companyCount.Cells[row, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 3].Style.Border.Right.Color.SetColor(Color.Black);
                companyCount.Cells[row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 3].Style.Border.Bottom.Color.SetColor(Color.Black);

                companyCount.Cells[row, 4].Value = item.ProductNewsCount;
                companyCount.Cells[row, 4].Style.Numberformat.Format = "#,##0";
                companyCount.Cells[row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                companyCount.Cells[row, 4].Style.Font.Name = "Times New Roman";
                companyCount.Cells[row, 4].Style.Font.Size = 12;
                companyCount.Cells[row, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 4].Style.Border.Top.Color.SetColor(Color.Black);
                companyCount.Cells[row, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 4].Style.Border.Left.Color.SetColor(Color.Black);
                companyCount.Cells[row, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 4].Style.Border.Right.Color.SetColor(Color.Black);
                companyCount.Cells[row, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 4].Style.Border.Bottom.Color.SetColor(Color.Black);

                companyCount.Cells[row, 5].Value = item.ProductMediaCount;
                companyCount.Cells[row, 5].Style.Numberformat.Format = "#,##0";
                companyCount.Cells[row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                companyCount.Cells[row, 5].Style.Font.Name = "Times New Roman";
                companyCount.Cells[row, 5].Style.Font.Size = 12;
                companyCount.Cells[row, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 5].Style.Border.Top.Color.SetColor(Color.Black);
                companyCount.Cells[row, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 5].Style.Border.Left.Color.SetColor(Color.Black);
                companyCount.Cells[row, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 5].Style.Border.Right.Color.SetColor(Color.Black);
                companyCount.Cells[row, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 5].Style.Border.Bottom.Color.SetColor(Color.Black);

                companyCount.Cells[row, 6].Value = item.NewsGrowthPercent;
                companyCount.Cells[row, 6].Style.Numberformat.Format = "#,##0";
                companyCount.Cells[row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                companyCount.Cells[row, 6].Style.Font.Name = "Times New Roman";
                companyCount.Cells[row, 6].Style.Font.Size = 12;
                companyCount.Cells[row, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 6].Style.Border.Top.Color.SetColor(Color.Black);
                companyCount.Cells[row, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 6].Style.Border.Left.Color.SetColor(Color.Black);
                companyCount.Cells[row, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 6].Style.Border.Right.Color.SetColor(Color.Black);
                companyCount.Cells[row, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 6].Style.Border.Bottom.Color.SetColor(Color.Black);

                companyCount.Cells[row, 7].Value = item.MediaGrowthPercent;
                companyCount.Cells[row, 7].Style.Numberformat.Format = "#,##0";
                companyCount.Cells[row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                companyCount.Cells[row, 7].Style.Font.Name = "Times New Roman";
                companyCount.Cells[row, 7].Style.Font.Size = 12;
                companyCount.Cells[row, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 7].Style.Border.Top.Color.SetColor(Color.Black);
                companyCount.Cells[row, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 7].Style.Border.Left.Color.SetColor(Color.Black);
                companyCount.Cells[row, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 7].Style.Border.Right.Color.SetColor(Color.Black);
                companyCount.Cells[row, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                companyCount.Cells[row, 7].Style.Border.Bottom.Color.SetColor(Color.Black);
                row = row + 1;
            }
            companyCount.Column(1).AutoFit();
            companyCount.Column(2).AutoFit();
            companyCount.Column(3).AutoFit();
            companyCount.Column(4).AutoFit();
            companyCount.Column(5).AutoFit();
            companyCount.Column(6).AutoFit();
            companyCount.Column(7).AutoFit();
        }
        private void InitializationFeature(ExcelPackage package, Color color, int ID)
        {
            var feature = package.Workbook.Worksheets.Add("Feature");
            feature.Cells[1, 1].Value = "Company";
            feature.Cells[1, 1].Style.Font.Bold = true;
            feature.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            feature.Cells[1, 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
            feature.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            feature.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(color);
            feature.Cells[1, 1].Style.Font.Name = "Times New Roman";
            feature.Cells[1, 1].Style.Font.Size = 12;
            feature.Cells[1, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            feature.Cells[1, 1].Style.Border.Top.Color.SetColor(Color.Black);
            feature.Cells[1, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            feature.Cells[1, 1].Style.Border.Left.Color.SetColor(Color.Black);
            feature.Cells[1, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            feature.Cells[1, 1].Style.Border.Right.Color.SetColor(Color.Black);
            feature.Cells[1, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            feature.Cells[1, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

            feature.Cells[1, 2].Value = "Feature (Last month)";
            feature.Cells[1, 2].Style.Font.Bold = true;
            feature.Cells[1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            feature.Cells[1, 2].Style.Font.Color.SetColor(System.Drawing.Color.White);
            feature.Cells[1, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
            feature.Cells[1, 2].Style.Fill.BackgroundColor.SetColor(color);
            feature.Cells[1, 2].Style.Font.Name = "Times New Roman";
            feature.Cells[1, 2].Style.Font.Size = 12;
            feature.Cells[1, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            feature.Cells[1, 2].Style.Border.Top.Color.SetColor(Color.Black);
            feature.Cells[1, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            feature.Cells[1, 2].Style.Border.Left.Color.SetColor(Color.Black);
            feature.Cells[1, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            feature.Cells[1, 2].Style.Border.Right.Color.SetColor(Color.Black);
            feature.Cells[1, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            feature.Cells[1, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

            feature.Cells[1, 3].Value = "Mention (Last month)";
            feature.Cells[1, 3].Style.Font.Bold = true;
            feature.Cells[1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            feature.Cells[1, 3].Style.Font.Color.SetColor(System.Drawing.Color.White);
            feature.Cells[1, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
            feature.Cells[1, 3].Style.Fill.BackgroundColor.SetColor(color);
            feature.Cells[1, 3].Style.Font.Name = "Times New Roman";
            feature.Cells[1, 3].Style.Font.Size = 12;
            feature.Cells[1, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            feature.Cells[1, 3].Style.Border.Top.Color.SetColor(Color.Black);
            feature.Cells[1, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            feature.Cells[1, 3].Style.Border.Left.Color.SetColor(Color.Black);
            feature.Cells[1, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            feature.Cells[1, 3].Style.Border.Right.Color.SetColor(Color.Black);
            feature.Cells[1, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            feature.Cells[1, 3].Style.Border.Bottom.Color.SetColor(Color.Black);

            feature.Cells[1, 4].Value = "Feature (This month)";
            feature.Cells[1, 4].Style.Font.Bold = true;
            feature.Cells[1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            feature.Cells[1, 4].Style.Font.Color.SetColor(System.Drawing.Color.White);
            feature.Cells[1, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
            feature.Cells[1, 4].Style.Fill.BackgroundColor.SetColor(color);
            feature.Cells[1, 4].Style.Font.Name = "Times New Roman";
            feature.Cells[1, 4].Style.Font.Size = 12;
            feature.Cells[1, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            feature.Cells[1, 4].Style.Border.Top.Color.SetColor(Color.Black);
            feature.Cells[1, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            feature.Cells[1, 4].Style.Border.Left.Color.SetColor(Color.Black);
            feature.Cells[1, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            feature.Cells[1, 4].Style.Border.Right.Color.SetColor(Color.Black);
            feature.Cells[1, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            feature.Cells[1, 4].Style.Border.Bottom.Color.SetColor(Color.Black);

            feature.Cells[1, 5].Value = "Mention (This month)";
            feature.Cells[1, 5].Style.Font.Bold = true;
            feature.Cells[1, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            feature.Cells[1, 5].Style.Font.Color.SetColor(System.Drawing.Color.White);
            feature.Cells[1, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
            feature.Cells[1, 5].Style.Fill.BackgroundColor.SetColor(color);
            feature.Cells[1, 5].Style.Font.Name = "Times New Roman";
            feature.Cells[1, 5].Style.Font.Size = 12;
            feature.Cells[1, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            feature.Cells[1, 5].Style.Border.Top.Color.SetColor(Color.Black);
            feature.Cells[1, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            feature.Cells[1, 5].Style.Border.Left.Color.SetColor(Color.Black);
            feature.Cells[1, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            feature.Cells[1, 5].Style.Border.Right.Color.SetColor(Color.Black);
            feature.Cells[1, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            feature.Cells[1, 5].Style.Border.Bottom.Color.SetColor(Color.Black);

            feature.Cells[1, 6].Value = "Growth Feature (%)";
            feature.Cells[1, 6].Style.Font.Bold = true;
            feature.Cells[1, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            feature.Cells[1, 6].Style.Font.Color.SetColor(System.Drawing.Color.White);
            feature.Cells[1, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
            feature.Cells[1, 6].Style.Fill.BackgroundColor.SetColor(color);
            feature.Cells[1, 6].Style.Font.Name = "Times New Roman";
            feature.Cells[1, 6].Style.Font.Size = 12;
            feature.Cells[1, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            feature.Cells[1, 6].Style.Border.Top.Color.SetColor(Color.Black);
            feature.Cells[1, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            feature.Cells[1, 6].Style.Border.Left.Color.SetColor(Color.Black);
            feature.Cells[1, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            feature.Cells[1, 6].Style.Border.Right.Color.SetColor(Color.Black);
            feature.Cells[1, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            feature.Cells[1, 6].Style.Border.Bottom.Color.SetColor(Color.Black);

            feature.Cells[1, 7].Value = "Growth Mention (%)";
            feature.Cells[1, 7].Style.Font.Bold = true;
            feature.Cells[1, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            feature.Cells[1, 7].Style.Font.Color.SetColor(System.Drawing.Color.White);
            feature.Cells[1, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
            feature.Cells[1, 7].Style.Fill.BackgroundColor.SetColor(color);
            feature.Cells[1, 7].Style.Font.Name = "Times New Roman";
            feature.Cells[1, 7].Style.Font.Size = 12;
            feature.Cells[1, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            feature.Cells[1, 7].Style.Border.Top.Color.SetColor(Color.Black);
            feature.Cells[1, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            feature.Cells[1, 7].Style.Border.Left.Color.SetColor(Color.Black);
            feature.Cells[1, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            feature.Cells[1, 7].Style.Border.Right.Color.SetColor(Color.Black);
            feature.Cells[1, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            feature.Cells[1, 7].Style.Border.Bottom.Color.SetColor(Color.Black);

            List<ReportMonthlyIndustryDataTransfer> listFeature = _reportMonthlyRepository.GetFeatureIndustryByIDToList(ID);
            int row = 2;
            foreach (ReportMonthlyIndustryDataTransfer item in listFeature)
            {
                feature.Cells[row, 1].Value = item.CompanyName;
                feature.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                feature.Cells[row, 1].Style.Font.Name = "Times New Roman";
                feature.Cells[row, 1].Style.Font.Size = 12;
                feature.Cells[row, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                feature.Cells[row, 1].Style.Border.Top.Color.SetColor(Color.Black);
                feature.Cells[row, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                feature.Cells[row, 1].Style.Border.Left.Color.SetColor(Color.Black);
                feature.Cells[row, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                feature.Cells[row, 1].Style.Border.Right.Color.SetColor(Color.Black);
                feature.Cells[row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                feature.Cells[row, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

                feature.Cells[row, 2].Value = item.FeatureLastMonthCount;
                feature.Cells[row, 2].Style.Numberformat.Format = "#,##0";
                feature.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                feature.Cells[row, 2].Style.Font.Name = "Times New Roman";
                feature.Cells[row, 2].Style.Font.Size = 12;
                feature.Cells[row, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                feature.Cells[row, 2].Style.Border.Top.Color.SetColor(Color.Black);
                feature.Cells[row, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                feature.Cells[row, 2].Style.Border.Left.Color.SetColor(Color.Black);
                feature.Cells[row, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                feature.Cells[row, 2].Style.Border.Right.Color.SetColor(Color.Black);
                feature.Cells[row, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                feature.Cells[row, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

                feature.Cells[row, 3].Value = item.MentionLastMonthCount;
                feature.Cells[row, 3].Style.Numberformat.Format = "#,##0";
                feature.Cells[row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                feature.Cells[row, 3].Style.Font.Name = "Times New Roman";
                feature.Cells[row, 3].Style.Font.Size = 12;
                feature.Cells[row, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                feature.Cells[row, 3].Style.Border.Top.Color.SetColor(Color.Black);
                feature.Cells[row, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                feature.Cells[row, 3].Style.Border.Left.Color.SetColor(Color.Black);
                feature.Cells[row, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                feature.Cells[row, 3].Style.Border.Right.Color.SetColor(Color.Black);
                feature.Cells[row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                feature.Cells[row, 3].Style.Border.Bottom.Color.SetColor(Color.Black);

                feature.Cells[row, 4].Value = item.FeatureCount;
                feature.Cells[row, 4].Style.Numberformat.Format = "#,##0";
                feature.Cells[row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                feature.Cells[row, 4].Style.Font.Name = "Times New Roman";
                feature.Cells[row, 4].Style.Font.Size = 12;
                feature.Cells[row, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                feature.Cells[row, 4].Style.Border.Top.Color.SetColor(Color.Black);
                feature.Cells[row, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                feature.Cells[row, 4].Style.Border.Left.Color.SetColor(Color.Black);
                feature.Cells[row, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                feature.Cells[row, 4].Style.Border.Right.Color.SetColor(Color.Black);
                feature.Cells[row, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                feature.Cells[row, 4].Style.Border.Bottom.Color.SetColor(Color.Black);

                feature.Cells[row, 5].Value = item.MentionCount;
                feature.Cells[row, 5].Style.Numberformat.Format = "#,##0";
                feature.Cells[row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                feature.Cells[row, 5].Style.Font.Name = "Times New Roman";
                feature.Cells[row, 5].Style.Font.Size = 12;
                feature.Cells[row, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                feature.Cells[row, 5].Style.Border.Top.Color.SetColor(Color.Black);
                feature.Cells[row, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                feature.Cells[row, 5].Style.Border.Left.Color.SetColor(Color.Black);
                feature.Cells[row, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                feature.Cells[row, 5].Style.Border.Right.Color.SetColor(Color.Black);
                feature.Cells[row, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                feature.Cells[row, 5].Style.Border.Bottom.Color.SetColor(Color.Black);

                feature.Cells[row, 6].Value = item.FeatureGrowthPercent;
                feature.Cells[row, 6].Style.Numberformat.Format = "#,##0";
                feature.Cells[row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                feature.Cells[row, 6].Style.Font.Name = "Times New Roman";
                feature.Cells[row, 6].Style.Font.Size = 12;
                feature.Cells[row, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                feature.Cells[row, 6].Style.Border.Top.Color.SetColor(Color.Black);
                feature.Cells[row, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                feature.Cells[row, 6].Style.Border.Left.Color.SetColor(Color.Black);
                feature.Cells[row, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                feature.Cells[row, 6].Style.Border.Right.Color.SetColor(Color.Black);
                feature.Cells[row, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                feature.Cells[row, 6].Style.Border.Bottom.Color.SetColor(Color.Black);

                feature.Cells[row, 7].Value = item.MentionGrowthPercent;
                feature.Cells[row, 7].Style.Numberformat.Format = "#,##0";
                feature.Cells[row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                feature.Cells[row, 7].Style.Font.Name = "Times New Roman";
                feature.Cells[row, 7].Style.Font.Size = 12;
                feature.Cells[row, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                feature.Cells[row, 7].Style.Border.Top.Color.SetColor(Color.Black);
                feature.Cells[row, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                feature.Cells[row, 7].Style.Border.Left.Color.SetColor(Color.Black);
                feature.Cells[row, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                feature.Cells[row, 7].Style.Border.Right.Color.SetColor(Color.Black);
                feature.Cells[row, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                feature.Cells[row, 7].Style.Border.Bottom.Color.SetColor(Color.Black);
                row = row + 1;
            }
            feature.Column(1).AutoFit();
            feature.Column(2).AutoFit();
            feature.Column(3).AutoFit();
            feature.Column(4).AutoFit();
            feature.Column(5).AutoFit();
            feature.Column(6).AutoFit();
            feature.Column(7).AutoFit();
        }
        private void InitializationSentiment(ExcelPackage package, Color color, int ID)
        {
            var sentiment = package.Workbook.Worksheets.Add("Sentiment");

            sentiment.Cells[1, 1].Value = "";
            sentiment.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sentiment.Cells[1, 1].Style.Font.Name = "Times New Roman";
            sentiment.Cells[1, 1].Style.Font.Size = 12;
            sentiment.Cells[1, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[1, 1].Style.Border.Top.Color.SetColor(Color.Black);
            sentiment.Cells[1, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[1, 1].Style.Border.Left.Color.SetColor(Color.Black);
            sentiment.Cells[1, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[1, 1].Style.Border.Right.Color.SetColor(Color.Black);
            sentiment.Cells[1, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[1, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

            sentiment.Cells[1, 2].Value = "Last month";
            sentiment.Cells[1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sentiment.Cells[1, 2].Style.Font.Name = "Times New Roman";
            sentiment.Cells[1, 2].Style.Font.Size = 12;
            sentiment.Cells[1, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[1, 2].Style.Border.Top.Color.SetColor(Color.Black);
            sentiment.Cells[1, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[1, 2].Style.Border.Left.Color.SetColor(Color.Black);
            sentiment.Cells[1, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[1, 2].Style.Border.Right.Color.SetColor(Color.Black);
            sentiment.Cells[1, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[1, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

            sentiment.Cells[1, 5].Value = "This month";
            sentiment.Cells[1, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sentiment.Cells[1, 5].Style.Font.Name = "Times New Roman";
            sentiment.Cells[1, 5].Style.Font.Size = 12;
            sentiment.Cells[1, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[1, 5].Style.Border.Top.Color.SetColor(Color.Black);
            sentiment.Cells[1, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[1, 5].Style.Border.Left.Color.SetColor(Color.Black);
            sentiment.Cells[1, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[1, 5].Style.Border.Right.Color.SetColor(Color.Black);
            sentiment.Cells[1, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[1, 5].Style.Border.Bottom.Color.SetColor(Color.Black);

            sentiment.Cells[1, 8].Value = "Growth (%)";
            sentiment.Cells[1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sentiment.Cells[1, 8].Style.Font.Name = "Times New Roman";
            sentiment.Cells[1, 8].Style.Font.Size = 12;
            sentiment.Cells[1, 8].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[1, 8].Style.Border.Top.Color.SetColor(Color.Black);
            sentiment.Cells[1, 8].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[1, 8].Style.Border.Left.Color.SetColor(Color.Black);
            sentiment.Cells[1, 8].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[1, 8].Style.Border.Right.Color.SetColor(Color.Black);
            sentiment.Cells[1, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[1, 8].Style.Border.Bottom.Color.SetColor(Color.Black);

            sentiment.Cells[1, 2, 1, 4].Merge = true;
            sentiment.Cells[1, 5, 1, 7].Merge = true;
            sentiment.Cells[1, 8, 1, 10].Merge = true;

            sentiment.Cells[2, 1].Value = "Company";
            sentiment.Cells[2, 1].Style.Font.Bold = true;
            sentiment.Cells[2, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sentiment.Cells[2, 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
            sentiment.Cells[2, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sentiment.Cells[2, 1].Style.Fill.BackgroundColor.SetColor(color);
            sentiment.Cells[2, 1].Style.Font.Name = "Times New Roman";
            sentiment.Cells[2, 1].Style.Font.Size = 12;
            sentiment.Cells[2, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 1].Style.Border.Top.Color.SetColor(Color.Black);
            sentiment.Cells[2, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 1].Style.Border.Left.Color.SetColor(Color.Black);
            sentiment.Cells[2, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 1].Style.Border.Right.Color.SetColor(Color.Black);
            sentiment.Cells[2, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

            sentiment.Cells[2, 2].Value = "Positive";
            sentiment.Cells[2, 2].Style.Font.Bold = true;
            sentiment.Cells[2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sentiment.Cells[2, 2].Style.Font.Color.SetColor(System.Drawing.Color.White);
            sentiment.Cells[2, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sentiment.Cells[2, 2].Style.Fill.BackgroundColor.SetColor(color);
            sentiment.Cells[2, 2].Style.Font.Name = "Times New Roman";
            sentiment.Cells[2, 2].Style.Font.Size = 12;
            sentiment.Cells[2, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 2].Style.Border.Top.Color.SetColor(Color.Black);
            sentiment.Cells[2, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 2].Style.Border.Left.Color.SetColor(Color.Black);
            sentiment.Cells[2, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 2].Style.Border.Right.Color.SetColor(Color.Black);
            sentiment.Cells[2, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

            sentiment.Cells[2, 3].Value = "Neutral";
            sentiment.Cells[2, 3].Style.Font.Bold = true;
            sentiment.Cells[2, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sentiment.Cells[2, 3].Style.Font.Color.SetColor(System.Drawing.Color.White);
            sentiment.Cells[2, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sentiment.Cells[2, 3].Style.Fill.BackgroundColor.SetColor(color);
            sentiment.Cells[2, 3].Style.Font.Name = "Times New Roman";
            sentiment.Cells[2, 3].Style.Font.Size = 12;
            sentiment.Cells[2, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 3].Style.Border.Top.Color.SetColor(Color.Black);
            sentiment.Cells[2, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 3].Style.Border.Left.Color.SetColor(Color.Black);
            sentiment.Cells[2, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 3].Style.Border.Right.Color.SetColor(Color.Black);
            sentiment.Cells[2, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 3].Style.Border.Bottom.Color.SetColor(Color.Black);

            sentiment.Cells[2, 4].Value = "Negative";
            sentiment.Cells[2, 4].Style.Font.Bold = true;
            sentiment.Cells[2, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sentiment.Cells[2, 4].Style.Font.Color.SetColor(System.Drawing.Color.White);
            sentiment.Cells[2, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sentiment.Cells[2, 4].Style.Fill.BackgroundColor.SetColor(color);
            sentiment.Cells[2, 4].Style.Font.Name = "Times New Roman";
            sentiment.Cells[2, 4].Style.Font.Size = 12;
            sentiment.Cells[2, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 4].Style.Border.Top.Color.SetColor(Color.Black);
            sentiment.Cells[2, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 4].Style.Border.Left.Color.SetColor(Color.Black);
            sentiment.Cells[2, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 4].Style.Border.Right.Color.SetColor(Color.Black);
            sentiment.Cells[2, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 4].Style.Border.Bottom.Color.SetColor(Color.Black);

            sentiment.Cells[2, 5].Value = "Positive";
            sentiment.Cells[2, 5].Style.Font.Bold = true;
            sentiment.Cells[2, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sentiment.Cells[2, 5].Style.Font.Color.SetColor(System.Drawing.Color.White);
            sentiment.Cells[2, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sentiment.Cells[2, 5].Style.Fill.BackgroundColor.SetColor(color);
            sentiment.Cells[2, 5].Style.Font.Name = "Times New Roman";
            sentiment.Cells[2, 5].Style.Font.Size = 12;
            sentiment.Cells[2, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 5].Style.Border.Top.Color.SetColor(Color.Black);
            sentiment.Cells[2, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 5].Style.Border.Left.Color.SetColor(Color.Black);
            sentiment.Cells[2, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 5].Style.Border.Right.Color.SetColor(Color.Black);
            sentiment.Cells[2, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 5].Style.Border.Bottom.Color.SetColor(Color.Black);

            sentiment.Cells[2, 6].Value = "Neutral";
            sentiment.Cells[2, 6].Style.Font.Bold = true;
            sentiment.Cells[2, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sentiment.Cells[2, 6].Style.Font.Color.SetColor(System.Drawing.Color.White);
            sentiment.Cells[2, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sentiment.Cells[2, 6].Style.Fill.BackgroundColor.SetColor(color);
            sentiment.Cells[2, 6].Style.Font.Name = "Times New Roman";
            sentiment.Cells[2, 6].Style.Font.Size = 12;
            sentiment.Cells[2, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 6].Style.Border.Top.Color.SetColor(Color.Black);
            sentiment.Cells[2, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 6].Style.Border.Left.Color.SetColor(Color.Black);
            sentiment.Cells[2, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 6].Style.Border.Right.Color.SetColor(Color.Black);
            sentiment.Cells[2, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 6].Style.Border.Bottom.Color.SetColor(Color.Black);

            sentiment.Cells[2, 7].Value = "Negative";
            sentiment.Cells[2, 7].Style.Font.Bold = true;
            sentiment.Cells[2, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sentiment.Cells[2, 7].Style.Font.Color.SetColor(System.Drawing.Color.White);
            sentiment.Cells[2, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sentiment.Cells[2, 7].Style.Fill.BackgroundColor.SetColor(color);
            sentiment.Cells[2, 7].Style.Font.Name = "Times New Roman";
            sentiment.Cells[2, 7].Style.Font.Size = 12;
            sentiment.Cells[2, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 7].Style.Border.Top.Color.SetColor(Color.Black);
            sentiment.Cells[2, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 7].Style.Border.Left.Color.SetColor(Color.Black);
            sentiment.Cells[2, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 7].Style.Border.Right.Color.SetColor(Color.Black);
            sentiment.Cells[2, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 7].Style.Border.Bottom.Color.SetColor(Color.Black);

            sentiment.Cells[2, 8].Value = "Positive";
            sentiment.Cells[2, 8].Style.Font.Bold = true;
            sentiment.Cells[2, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sentiment.Cells[2, 8].Style.Font.Color.SetColor(System.Drawing.Color.White);
            sentiment.Cells[2, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sentiment.Cells[2, 8].Style.Fill.BackgroundColor.SetColor(color);
            sentiment.Cells[2, 8].Style.Font.Name = "Times New Roman";
            sentiment.Cells[2, 8].Style.Font.Size = 12;
            sentiment.Cells[2, 8].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 8].Style.Border.Top.Color.SetColor(Color.Black);
            sentiment.Cells[2, 8].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 8].Style.Border.Left.Color.SetColor(Color.Black);
            sentiment.Cells[2, 8].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 8].Style.Border.Right.Color.SetColor(Color.Black);
            sentiment.Cells[2, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 8].Style.Border.Bottom.Color.SetColor(Color.Black);

            sentiment.Cells[2, 9].Value = "Neutral";
            sentiment.Cells[2, 9].Style.Font.Bold = true;
            sentiment.Cells[2, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sentiment.Cells[2, 9].Style.Font.Color.SetColor(System.Drawing.Color.White);
            sentiment.Cells[2, 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sentiment.Cells[2, 9].Style.Fill.BackgroundColor.SetColor(color);
            sentiment.Cells[2, 9].Style.Font.Name = "Times New Roman";
            sentiment.Cells[2, 9].Style.Font.Size = 12;
            sentiment.Cells[2, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 9].Style.Border.Top.Color.SetColor(Color.Black);
            sentiment.Cells[2, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 9].Style.Border.Left.Color.SetColor(Color.Black);
            sentiment.Cells[2, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 9].Style.Border.Right.Color.SetColor(Color.Black);
            sentiment.Cells[2, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 9].Style.Border.Bottom.Color.SetColor(Color.Black);

            sentiment.Cells[2, 10].Value = "Negative";
            sentiment.Cells[2, 10].Style.Font.Bold = true;
            sentiment.Cells[2, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sentiment.Cells[2, 10].Style.Font.Color.SetColor(System.Drawing.Color.White);
            sentiment.Cells[2, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sentiment.Cells[2, 10].Style.Fill.BackgroundColor.SetColor(color);
            sentiment.Cells[2, 10].Style.Font.Name = "Times New Roman";
            sentiment.Cells[2, 10].Style.Font.Size = 12;
            sentiment.Cells[2, 10].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 10].Style.Border.Top.Color.SetColor(Color.Black);
            sentiment.Cells[2, 10].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 10].Style.Border.Left.Color.SetColor(Color.Black);
            sentiment.Cells[2, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 10].Style.Border.Right.Color.SetColor(Color.Black);
            sentiment.Cells[2, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[2, 10].Style.Border.Bottom.Color.SetColor(Color.Black);
            List<ReportMonthlySentimentDataTransfer> listReportMonthlySentimentDataTransfer = _reportMonthlyRepository.GetSentimentByIDToList(ID);
            int row = 3;
            foreach (ReportMonthlySentimentDataTransfer item in listReportMonthlySentimentDataTransfer)
            {
                sentiment.Cells[row, 1].Value = item.CompanyName;
                sentiment.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                sentiment.Cells[row, 1].Style.Font.Name = "Times New Roman";
                sentiment.Cells[row, 1].Style.Font.Size = 12;
                sentiment.Cells[row, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 1].Style.Border.Top.Color.SetColor(Color.Black);
                sentiment.Cells[row, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 1].Style.Border.Left.Color.SetColor(Color.Black);
                sentiment.Cells[row, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 1].Style.Border.Right.Color.SetColor(Color.Black);
                sentiment.Cells[row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

                sentiment.Cells[row, 2].Value = item.PositiveLastMonthCount;
                sentiment.Cells[row, 2].Style.Numberformat.Format = "#,##0";
                sentiment.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                sentiment.Cells[row, 2].Style.Font.Name = "Times New Roman";
                sentiment.Cells[row, 2].Style.Font.Size = 12;
                sentiment.Cells[row, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 2].Style.Border.Top.Color.SetColor(Color.Black);
                sentiment.Cells[row, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 2].Style.Border.Left.Color.SetColor(Color.Black);
                sentiment.Cells[row, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 2].Style.Border.Right.Color.SetColor(Color.Black);
                sentiment.Cells[row, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

                sentiment.Cells[row, 3].Value = item.NeutralLastMonthCount;
                sentiment.Cells[row, 3].Style.Numberformat.Format = "#,##0";
                sentiment.Cells[row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                sentiment.Cells[row, 3].Style.Font.Name = "Times New Roman";
                sentiment.Cells[row, 3].Style.Font.Size = 12;
                sentiment.Cells[row, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 3].Style.Border.Top.Color.SetColor(Color.Black);
                sentiment.Cells[row, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 3].Style.Border.Left.Color.SetColor(Color.Black);
                sentiment.Cells[row, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 3].Style.Border.Right.Color.SetColor(Color.Black);
                sentiment.Cells[row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 3].Style.Border.Bottom.Color.SetColor(Color.Black);

                sentiment.Cells[row, 4].Value = item.NegativeLastMonthCount;
                sentiment.Cells[row, 4].Style.Numberformat.Format = "#,##0";
                sentiment.Cells[row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                sentiment.Cells[row, 4].Style.Font.Name = "Times New Roman";
                sentiment.Cells[row, 4].Style.Font.Size = 12;
                sentiment.Cells[row, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 4].Style.Border.Top.Color.SetColor(Color.Black);
                sentiment.Cells[row, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 4].Style.Border.Left.Color.SetColor(Color.Black);
                sentiment.Cells[row, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 4].Style.Border.Right.Color.SetColor(Color.Black);
                sentiment.Cells[row, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 4].Style.Border.Bottom.Color.SetColor(Color.Black);

                sentiment.Cells[row, 5].Value = item.PositiveCount;
                sentiment.Cells[row, 5].Style.Numberformat.Format = "#,##0";
                sentiment.Cells[row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                sentiment.Cells[row, 5].Style.Font.Name = "Times New Roman";
                sentiment.Cells[row, 5].Style.Font.Size = 12;
                sentiment.Cells[row, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 5].Style.Border.Top.Color.SetColor(Color.Black);
                sentiment.Cells[row, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 5].Style.Border.Left.Color.SetColor(Color.Black);
                sentiment.Cells[row, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 5].Style.Border.Right.Color.SetColor(Color.Black);
                sentiment.Cells[row, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 5].Style.Border.Bottom.Color.SetColor(Color.Black);

                sentiment.Cells[row, 6].Value = item.NeutralCount;
                sentiment.Cells[row, 6].Style.Numberformat.Format = "#,##0";
                sentiment.Cells[row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                sentiment.Cells[row, 6].Style.Font.Name = "Times New Roman";
                sentiment.Cells[row, 6].Style.Font.Size = 12;
                sentiment.Cells[row, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 6].Style.Border.Top.Color.SetColor(Color.Black);
                sentiment.Cells[row, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 6].Style.Border.Left.Color.SetColor(Color.Black);
                sentiment.Cells[row, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 6].Style.Border.Right.Color.SetColor(Color.Black);
                sentiment.Cells[row, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 6].Style.Border.Bottom.Color.SetColor(Color.Black);

                sentiment.Cells[row, 7].Value = item.NegativeCount;
                sentiment.Cells[row, 7].Style.Numberformat.Format = "#,##0";
                sentiment.Cells[row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                sentiment.Cells[row, 7].Style.Font.Name = "Times New Roman";
                sentiment.Cells[row, 7].Style.Font.Size = 12;
                sentiment.Cells[row, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 7].Style.Border.Top.Color.SetColor(Color.Black);
                sentiment.Cells[row, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 7].Style.Border.Left.Color.SetColor(Color.Black);
                sentiment.Cells[row, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 7].Style.Border.Right.Color.SetColor(Color.Black);
                sentiment.Cells[row, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 7].Style.Border.Bottom.Color.SetColor(Color.Black);

                sentiment.Cells[row, 8].Value = item.PositiveGrowthPercent;
                sentiment.Cells[row, 8].Style.Numberformat.Format = "#,##0";
                sentiment.Cells[row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                sentiment.Cells[row, 8].Style.Font.Name = "Times New Roman";
                sentiment.Cells[row, 8].Style.Font.Size = 12;
                sentiment.Cells[row, 8].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 8].Style.Border.Top.Color.SetColor(Color.Black);
                sentiment.Cells[row, 8].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 8].Style.Border.Left.Color.SetColor(Color.Black);
                sentiment.Cells[row, 8].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 8].Style.Border.Right.Color.SetColor(Color.Black);
                sentiment.Cells[row, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 8].Style.Border.Bottom.Color.SetColor(Color.Black);

                sentiment.Cells[row, 9].Value = item.NeutralGrowthPercent;
                sentiment.Cells[row, 9].Style.Numberformat.Format = "#,##0";
                sentiment.Cells[row, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                sentiment.Cells[row, 9].Style.Font.Name = "Times New Roman";
                sentiment.Cells[row, 9].Style.Font.Size = 12;
                sentiment.Cells[row, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 9].Style.Border.Top.Color.SetColor(Color.Black);
                sentiment.Cells[row, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 9].Style.Border.Left.Color.SetColor(Color.Black);
                sentiment.Cells[row, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 9].Style.Border.Right.Color.SetColor(Color.Black);
                sentiment.Cells[row, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 9].Style.Border.Bottom.Color.SetColor(Color.Black);

                sentiment.Cells[row, 10].Value = item.NegativeGrowthPercent;
                sentiment.Cells[row, 10].Style.Numberformat.Format = "#,##0";
                sentiment.Cells[row, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                sentiment.Cells[row, 10].Style.Font.Name = "Times New Roman";
                sentiment.Cells[row, 10].Style.Font.Size = 12;
                sentiment.Cells[row, 10].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 10].Style.Border.Top.Color.SetColor(Color.Black);
                sentiment.Cells[row, 10].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 10].Style.Border.Left.Color.SetColor(Color.Black);
                sentiment.Cells[row, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 10].Style.Border.Right.Color.SetColor(Color.Black);
                sentiment.Cells[row, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 10].Style.Border.Bottom.Color.SetColor(Color.Black);
                row = row + 1;
            }
            row = row + 1;

            sentiment.Cells[row, 1].Value = "Company";
            sentiment.Cells[row, 1].Style.Font.Bold = true;
            sentiment.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sentiment.Cells[row, 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
            sentiment.Cells[row, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sentiment.Cells[row, 1].Style.Fill.BackgroundColor.SetColor(color);
            sentiment.Cells[row, 1].Style.Font.Name = "Times New Roman";
            sentiment.Cells[row, 1].Style.Font.Size = 12;
            sentiment.Cells[row, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 1].Style.Border.Top.Color.SetColor(Color.Black);
            sentiment.Cells[row, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 1].Style.Border.Left.Color.SetColor(Color.Black);
            sentiment.Cells[row, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 1].Style.Border.Right.Color.SetColor(Color.Black);
            sentiment.Cells[row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

            sentiment.Cells[row, 2].Value = "Sentiment";
            sentiment.Cells[row, 2].Style.Font.Bold = true;
            sentiment.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sentiment.Cells[row, 2].Style.Font.Color.SetColor(System.Drawing.Color.White);
            sentiment.Cells[row, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sentiment.Cells[row, 2].Style.Fill.BackgroundColor.SetColor(color);
            sentiment.Cells[row, 2].Style.Font.Name = "Times New Roman";
            sentiment.Cells[row, 2].Style.Font.Size = 12;
            sentiment.Cells[row, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 2].Style.Border.Top.Color.SetColor(Color.Black);
            sentiment.Cells[row, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 2].Style.Border.Left.Color.SetColor(Color.Black);
            sentiment.Cells[row, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 2].Style.Border.Right.Color.SetColor(Color.Black);
            sentiment.Cells[row, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

            sentiment.Cells[row, 3].Value = "Online";
            sentiment.Cells[row, 3].Style.Font.Bold = true;
            sentiment.Cells[row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sentiment.Cells[row, 3].Style.Font.Color.SetColor(System.Drawing.Color.White);
            sentiment.Cells[row, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sentiment.Cells[row, 3].Style.Fill.BackgroundColor.SetColor(color);
            sentiment.Cells[row, 3].Style.Font.Name = "Times New Roman";
            sentiment.Cells[row, 3].Style.Font.Size = 12;
            sentiment.Cells[row, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 3].Style.Border.Top.Color.SetColor(Color.Black);
            sentiment.Cells[row, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 3].Style.Border.Left.Color.SetColor(Color.Black);
            sentiment.Cells[row, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 3].Style.Border.Right.Color.SetColor(Color.Black);
            sentiment.Cells[row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 3].Style.Border.Bottom.Color.SetColor(Color.Black);

            sentiment.Cells[row, 4].Value = "Newspaper";
            sentiment.Cells[row, 4].Style.Font.Bold = true;
            sentiment.Cells[row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sentiment.Cells[row, 4].Style.Font.Color.SetColor(System.Drawing.Color.White);
            sentiment.Cells[row, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sentiment.Cells[row, 4].Style.Fill.BackgroundColor.SetColor(color);
            sentiment.Cells[row, 4].Style.Font.Name = "Times New Roman";
            sentiment.Cells[row, 4].Style.Font.Size = 12;
            sentiment.Cells[row, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 4].Style.Border.Top.Color.SetColor(Color.Black);
            sentiment.Cells[row, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 4].Style.Border.Left.Color.SetColor(Color.Black);
            sentiment.Cells[row, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 4].Style.Border.Right.Color.SetColor(Color.Black);
            sentiment.Cells[row, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 4].Style.Border.Bottom.Color.SetColor(Color.Black);

            sentiment.Cells[row, 5].Value = "TV";
            sentiment.Cells[row, 5].Style.Font.Bold = true;
            sentiment.Cells[row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sentiment.Cells[row, 5].Style.Font.Color.SetColor(System.Drawing.Color.White);
            sentiment.Cells[row, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sentiment.Cells[row, 5].Style.Fill.BackgroundColor.SetColor(color);
            sentiment.Cells[row, 5].Style.Font.Name = "Times New Roman";
            sentiment.Cells[row, 5].Style.Font.Size = 12;
            sentiment.Cells[row, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 5].Style.Border.Top.Color.SetColor(Color.Black);
            sentiment.Cells[row, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 5].Style.Border.Left.Color.SetColor(Color.Black);
            sentiment.Cells[row, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 5].Style.Border.Right.Color.SetColor(Color.Black);
            sentiment.Cells[row, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 5].Style.Border.Bottom.Color.SetColor(Color.Black);

            sentiment.Cells[row, 6].Value = "Magazine";
            sentiment.Cells[row, 6].Style.Font.Bold = true;
            sentiment.Cells[row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sentiment.Cells[row, 6].Style.Font.Color.SetColor(System.Drawing.Color.White);
            sentiment.Cells[row, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sentiment.Cells[row, 6].Style.Fill.BackgroundColor.SetColor(color);
            sentiment.Cells[row, 6].Style.Font.Name = "Times New Roman";
            sentiment.Cells[row, 6].Style.Font.Size = 12;
            sentiment.Cells[row, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 6].Style.Border.Top.Color.SetColor(Color.Black);
            sentiment.Cells[row, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 6].Style.Border.Left.Color.SetColor(Color.Black);
            sentiment.Cells[row, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 6].Style.Border.Right.Color.SetColor(Color.Black);
            sentiment.Cells[row, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 6].Style.Border.Bottom.Color.SetColor(Color.Black);
            List<ReportMonthlySentimentDataTransfer> listReportMonthlySentimentDataTransfer001 = _reportMonthlyRepository.GetSentimentAndMediaTypeWithoutSUMByIDToList(ID);
            row = row + 1;
            foreach (ReportMonthlySentimentDataTransfer item in listReportMonthlySentimentDataTransfer001)
            {
                sentiment.Cells[row, 1].Value = item.CompanyName;
                sentiment.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                sentiment.Cells[row, 1].Style.Font.Name = "Times New Roman";
                sentiment.Cells[row, 1].Style.Font.Size = 12;
                sentiment.Cells[row, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 1].Style.Border.Top.Color.SetColor(Color.Black);
                sentiment.Cells[row, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 1].Style.Border.Left.Color.SetColor(Color.Black);
                sentiment.Cells[row, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 1].Style.Border.Right.Color.SetColor(Color.Black);
                sentiment.Cells[row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

                sentiment.Cells[row, 2].Value = item.Sentiment;
                sentiment.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                sentiment.Cells[row, 2].Style.Font.Name = "Times New Roman";
                sentiment.Cells[row, 2].Style.Font.Size = 12;
                sentiment.Cells[row, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 2].Style.Border.Top.Color.SetColor(Color.Black);
                sentiment.Cells[row, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 2].Style.Border.Left.Color.SetColor(Color.Black);
                sentiment.Cells[row, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 2].Style.Border.Right.Color.SetColor(Color.Black);
                sentiment.Cells[row, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

                sentiment.Cells[row, 3].Value = item.OnlineCount;
                sentiment.Cells[row, 3].Style.Numberformat.Format = "#,##0";
                sentiment.Cells[row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                sentiment.Cells[row, 3].Style.Font.Name = "Times New Roman";
                sentiment.Cells[row, 3].Style.Font.Size = 12;
                sentiment.Cells[row, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 3].Style.Border.Top.Color.SetColor(Color.Black);
                sentiment.Cells[row, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 3].Style.Border.Left.Color.SetColor(Color.Black);
                sentiment.Cells[row, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 3].Style.Border.Right.Color.SetColor(Color.Black);
                sentiment.Cells[row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 3].Style.Border.Bottom.Color.SetColor(Color.Black);

                sentiment.Cells[row, 4].Value = item.NewspaperCount;
                sentiment.Cells[row, 4].Style.Numberformat.Format = "#,##0";
                sentiment.Cells[row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                sentiment.Cells[row, 4].Style.Font.Name = "Times New Roman";
                sentiment.Cells[row, 4].Style.Font.Size = 12;
                sentiment.Cells[row, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 4].Style.Border.Top.Color.SetColor(Color.Black);
                sentiment.Cells[row, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 4].Style.Border.Left.Color.SetColor(Color.Black);
                sentiment.Cells[row, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 4].Style.Border.Right.Color.SetColor(Color.Black);
                sentiment.Cells[row, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 4].Style.Border.Bottom.Color.SetColor(Color.Black);

                sentiment.Cells[row, 5].Value = item.TVCount;
                sentiment.Cells[row, 5].Style.Numberformat.Format = "#,##0";
                sentiment.Cells[row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                sentiment.Cells[row, 5].Style.Font.Name = "Times New Roman";
                sentiment.Cells[row, 5].Style.Font.Size = 12;
                sentiment.Cells[row, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 5].Style.Border.Top.Color.SetColor(Color.Black);
                sentiment.Cells[row, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 5].Style.Border.Left.Color.SetColor(Color.Black);
                sentiment.Cells[row, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 5].Style.Border.Right.Color.SetColor(Color.Black);
                sentiment.Cells[row, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 5].Style.Border.Bottom.Color.SetColor(Color.Black);

                sentiment.Cells[row, 6].Value = item.MagazineCount;
                sentiment.Cells[row, 6].Style.Numberformat.Format = "#,##0";
                sentiment.Cells[row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                sentiment.Cells[row, 6].Style.Font.Name = "Times New Roman";
                sentiment.Cells[row, 6].Style.Font.Size = 12;
                sentiment.Cells[row, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 6].Style.Border.Top.Color.SetColor(Color.Black);
                sentiment.Cells[row, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 6].Style.Border.Left.Color.SetColor(Color.Black);
                sentiment.Cells[row, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 6].Style.Border.Right.Color.SetColor(Color.Black);
                sentiment.Cells[row, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 6].Style.Border.Bottom.Color.SetColor(Color.Black);

                row = row + 1;
            }
            row = row + 1;
            sentiment.Cells[row, 1].Value = "Company";
            sentiment.Cells[row, 1].Style.Font.Bold = true;
            sentiment.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sentiment.Cells[row, 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
            sentiment.Cells[row, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sentiment.Cells[row, 1].Style.Fill.BackgroundColor.SetColor(color);
            sentiment.Cells[row, 1].Style.Font.Name = "Times New Roman";
            sentiment.Cells[row, 1].Style.Font.Size = 12;
            sentiment.Cells[row, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 1].Style.Border.Top.Color.SetColor(Color.Black);
            sentiment.Cells[row, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 1].Style.Border.Left.Color.SetColor(Color.Black);
            sentiment.Cells[row, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 1].Style.Border.Right.Color.SetColor(Color.Black);
            sentiment.Cells[row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

            sentiment.Cells[row, 2].Value = "Feature/Mention";
            sentiment.Cells[row, 2].Style.Font.Bold = true;
            sentiment.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sentiment.Cells[row, 2].Style.Font.Color.SetColor(System.Drawing.Color.White);
            sentiment.Cells[row, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sentiment.Cells[row, 2].Style.Fill.BackgroundColor.SetColor(color);
            sentiment.Cells[row, 2].Style.Font.Name = "Times New Roman";
            sentiment.Cells[row, 2].Style.Font.Size = 12;
            sentiment.Cells[row, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 2].Style.Border.Top.Color.SetColor(Color.Black);
            sentiment.Cells[row, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 2].Style.Border.Left.Color.SetColor(Color.Black);
            sentiment.Cells[row, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 2].Style.Border.Right.Color.SetColor(Color.Black);
            sentiment.Cells[row, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

            sentiment.Cells[row, 3].Value = "Positive";
            sentiment.Cells[row, 3].Style.Font.Bold = true;
            sentiment.Cells[row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sentiment.Cells[row, 3].Style.Font.Color.SetColor(System.Drawing.Color.White);
            sentiment.Cells[row, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sentiment.Cells[row, 3].Style.Fill.BackgroundColor.SetColor(color);
            sentiment.Cells[row, 3].Style.Font.Name = "Times New Roman";
            sentiment.Cells[row, 3].Style.Font.Size = 12;
            sentiment.Cells[row, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 3].Style.Border.Top.Color.SetColor(Color.Black);
            sentiment.Cells[row, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 3].Style.Border.Left.Color.SetColor(Color.Black);
            sentiment.Cells[row, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 3].Style.Border.Right.Color.SetColor(Color.Black);
            sentiment.Cells[row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 3].Style.Border.Bottom.Color.SetColor(Color.Black);

            sentiment.Cells[row, 4].Value = "Neutral";
            sentiment.Cells[row, 4].Style.Font.Bold = true;
            sentiment.Cells[row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sentiment.Cells[row, 4].Style.Font.Color.SetColor(System.Drawing.Color.White);
            sentiment.Cells[row, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sentiment.Cells[row, 4].Style.Fill.BackgroundColor.SetColor(color);
            sentiment.Cells[row, 4].Style.Font.Name = "Times New Roman";
            sentiment.Cells[row, 4].Style.Font.Size = 12;
            sentiment.Cells[row, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 4].Style.Border.Top.Color.SetColor(Color.Black);
            sentiment.Cells[row, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 4].Style.Border.Left.Color.SetColor(Color.Black);
            sentiment.Cells[row, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 4].Style.Border.Right.Color.SetColor(Color.Black);
            sentiment.Cells[row, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 4].Style.Border.Bottom.Color.SetColor(Color.Black);

            sentiment.Cells[row, 5].Value = "Negative";
            sentiment.Cells[row, 5].Style.Font.Bold = true;
            sentiment.Cells[row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            sentiment.Cells[row, 5].Style.Font.Color.SetColor(System.Drawing.Color.White);
            sentiment.Cells[row, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sentiment.Cells[row, 5].Style.Fill.BackgroundColor.SetColor(color);
            sentiment.Cells[row, 5].Style.Font.Name = "Times New Roman";
            sentiment.Cells[row, 5].Style.Font.Size = 12;
            sentiment.Cells[row, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 5].Style.Border.Top.Color.SetColor(Color.Black);
            sentiment.Cells[row, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 5].Style.Border.Left.Color.SetColor(Color.Black);
            sentiment.Cells[row, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 5].Style.Border.Right.Color.SetColor(Color.Black);
            sentiment.Cells[row, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            sentiment.Cells[row, 5].Style.Border.Bottom.Color.SetColor(Color.Black);

            List<ReportMonthlySentimentDataTransfer> listReportMonthlySentimentDataTransfer002 = _reportMonthlyRepository.GetSentimentAndFeatureWithoutSUMByIDToList(ID);
            row = row + 1;
            foreach (ReportMonthlySentimentDataTransfer item in listReportMonthlySentimentDataTransfer002)
            {
                sentiment.Cells[row, 1].Value = item.CompanyName;
                sentiment.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                sentiment.Cells[row, 1].Style.Font.Name = "Times New Roman";
                sentiment.Cells[row, 1].Style.Font.Size = 12;
                sentiment.Cells[row, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 1].Style.Border.Top.Color.SetColor(Color.Black);
                sentiment.Cells[row, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 1].Style.Border.Left.Color.SetColor(Color.Black);
                sentiment.Cells[row, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 1].Style.Border.Right.Color.SetColor(Color.Black);
                sentiment.Cells[row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

                sentiment.Cells[row, 2].Value = item.FeatureCorp;
                sentiment.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                sentiment.Cells[row, 2].Style.Font.Name = "Times New Roman";
                sentiment.Cells[row, 2].Style.Font.Size = 12;
                sentiment.Cells[row, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 2].Style.Border.Top.Color.SetColor(Color.Black);
                sentiment.Cells[row, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 2].Style.Border.Left.Color.SetColor(Color.Black);
                sentiment.Cells[row, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 2].Style.Border.Right.Color.SetColor(Color.Black);
                sentiment.Cells[row, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

                sentiment.Cells[row, 3].Value = item.PositiveCount;
                sentiment.Cells[row, 3].Style.Numberformat.Format = "#,##0";
                sentiment.Cells[row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                sentiment.Cells[row, 3].Style.Font.Name = "Times New Roman";
                sentiment.Cells[row, 3].Style.Font.Size = 12;
                sentiment.Cells[row, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 3].Style.Border.Top.Color.SetColor(Color.Black);
                sentiment.Cells[row, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 3].Style.Border.Left.Color.SetColor(Color.Black);
                sentiment.Cells[row, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 3].Style.Border.Right.Color.SetColor(Color.Black);
                sentiment.Cells[row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 3].Style.Border.Bottom.Color.SetColor(Color.Black);

                sentiment.Cells[row, 4].Value = item.NeutralCount;
                sentiment.Cells[row, 4].Style.Numberformat.Format = "#,##0";
                sentiment.Cells[row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                sentiment.Cells[row, 4].Style.Font.Name = "Times New Roman";
                sentiment.Cells[row, 4].Style.Font.Size = 12;
                sentiment.Cells[row, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 4].Style.Border.Top.Color.SetColor(Color.Black);
                sentiment.Cells[row, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 4].Style.Border.Left.Color.SetColor(Color.Black);
                sentiment.Cells[row, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 4].Style.Border.Right.Color.SetColor(Color.Black);
                sentiment.Cells[row, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 4].Style.Border.Bottom.Color.SetColor(Color.Black);

                sentiment.Cells[row, 5].Value = item.NegativeCount;
                sentiment.Cells[row, 5].Style.Numberformat.Format = "#,##0";
                sentiment.Cells[row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                sentiment.Cells[row, 5].Style.Font.Name = "Times New Roman";
                sentiment.Cells[row, 5].Style.Font.Size = 12;
                sentiment.Cells[row, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 5].Style.Border.Top.Color.SetColor(Color.Black);
                sentiment.Cells[row, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 5].Style.Border.Left.Color.SetColor(Color.Black);
                sentiment.Cells[row, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 5].Style.Border.Right.Color.SetColor(Color.Black);
                sentiment.Cells[row, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                sentiment.Cells[row, 5].Style.Border.Bottom.Color.SetColor(Color.Black);

                row = row + 1;
            }

            sentiment.Column(1).AutoFit();
            sentiment.Column(2).AutoFit();
            sentiment.Column(3).AutoFit();
            sentiment.Column(4).AutoFit();
            sentiment.Column(5).AutoFit();
            sentiment.Column(6).AutoFit();
            sentiment.Column(7).AutoFit();
            sentiment.Column(8).AutoFit();
            sentiment.Column(9).AutoFit();
            sentiment.Column(10).AutoFit();
        }
        private void InitializationChannel(ExcelPackage package, Color color, int ID)
        {
            var channel = package.Workbook.Worksheets.Add("Channel");
            int row = 1;
            channel.Cells[row, 1].Value = "";
            channel.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 1].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 1].Style.Font.Size = 12;
            channel.Cells[row, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 1].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 1].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 1].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 2].Value = "Last month";
            channel.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 2].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 2].Style.Font.Size = 12;
            channel.Cells[row, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 2].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 2].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 2].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 6].Value = "This month";
            channel.Cells[row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 6].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 6].Style.Font.Size = 12;
            channel.Cells[row, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 6].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 6].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 6].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 6].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 10].Value = "Growth";
            channel.Cells[row, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 10].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 10].Style.Font.Size = 12;
            channel.Cells[row, 10].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 10].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 10].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 10].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 10].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 10].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 14].Value = "Growth (%)";
            channel.Cells[row, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 14].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 14].Style.Font.Size = 12;
            channel.Cells[row, 14].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 14].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 14].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 14].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 14].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 14].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 14].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 14].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 2, row, 5].Merge = true;
            channel.Cells[row, 6, row, 9].Merge = true;
            channel.Cells[row, 10, row, 13].Merge = true;
            channel.Cells[row, 14, row, 17].Merge = true;

            row = row + 1;
            channel.Cells[row, 1].Value = "Company";
            channel.Cells[row, 1].Style.Font.Bold = true;
            channel.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 1].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 1].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 1].Style.Font.Size = 12;
            channel.Cells[row, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 1].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 1].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 1].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 2].Value = "Online";
            channel.Cells[row, 2].Style.Font.Bold = true;
            channel.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 2].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 2].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 2].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 2].Style.Font.Size = 12;
            channel.Cells[row, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 2].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 2].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 2].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 3].Value = "Newspaper";
            channel.Cells[row, 3].Style.Font.Bold = true;
            channel.Cells[row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 3].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 3].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 3].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 3].Style.Font.Size = 12;
            channel.Cells[row, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 3].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 3].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 3].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 3].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 4].Value = "Magazine";
            channel.Cells[row, 4].Style.Font.Bold = true;
            channel.Cells[row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 4].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 4].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 4].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 4].Style.Font.Size = 12;
            channel.Cells[row, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 4].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 4].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 4].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 4].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 5].Value = "TV";
            channel.Cells[row, 5].Style.Font.Bold = true;
            channel.Cells[row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 5].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 5].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 5].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 5].Style.Font.Size = 12;
            channel.Cells[row, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 5].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 5].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 5].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 5].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 6].Value = "Online";
            channel.Cells[row, 6].Style.Font.Bold = true;
            channel.Cells[row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 6].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 6].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 6].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 6].Style.Font.Size = 12;
            channel.Cells[row, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 6].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 6].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 6].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 6].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 7].Value = "Newspaper";
            channel.Cells[row, 7].Style.Font.Bold = true;
            channel.Cells[row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 7].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 7].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 7].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 7].Style.Font.Size = 12;
            channel.Cells[row, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 7].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 7].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 7].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 7].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 8].Value = "Magazine";
            channel.Cells[row, 8].Style.Font.Bold = true;
            channel.Cells[row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 8].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 8].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 8].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 8].Style.Font.Size = 12;
            channel.Cells[row, 8].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 8].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 8].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 8].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 8].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 8].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 8].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 9].Value = "TV";
            channel.Cells[row, 9].Style.Font.Bold = true;
            channel.Cells[row, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 9].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 9].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 9].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 9].Style.Font.Size = 12;
            channel.Cells[row, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 9].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 9].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 9].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 9].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 10].Value = "Online";
            channel.Cells[row, 10].Style.Font.Bold = true;
            channel.Cells[row, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 10].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 10].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 10].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 10].Style.Font.Size = 12;
            channel.Cells[row, 10].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 10].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 10].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 10].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 10].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 10].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 11].Value = "Newspaper";
            channel.Cells[row, 11].Style.Font.Bold = true;
            channel.Cells[row, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 11].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 11].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 11].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 11].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 11].Style.Font.Size = 12;
            channel.Cells[row, 11].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 11].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 11].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 11].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 11].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 11].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 11].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 12].Value = "Magazine";
            channel.Cells[row, 12].Style.Font.Bold = true;
            channel.Cells[row, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 12].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 12].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 12].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 12].Style.Font.Size = 12;
            channel.Cells[row, 12].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 12].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 12].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 12].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 12].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 12].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 12].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 13].Value = "TV";
            channel.Cells[row, 13].Style.Font.Bold = true;
            channel.Cells[row, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 13].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 13].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 13].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 13].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 13].Style.Font.Size = 12;
            channel.Cells[row, 13].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 13].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 13].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 13].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 13].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 13].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 13].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 13].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 14].Value = "Online";
            channel.Cells[row, 14].Style.Font.Bold = true;
            channel.Cells[row, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 14].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 14].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 14].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 14].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 14].Style.Font.Size = 12;
            channel.Cells[row, 14].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 14].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 14].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 14].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 14].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 14].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 14].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 14].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 15].Value = "Newspaper";
            channel.Cells[row, 15].Style.Font.Bold = true;
            channel.Cells[row, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 15].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 15].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 15].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 15].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 15].Style.Font.Size = 12;
            channel.Cells[row, 15].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 15].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 15].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 15].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 15].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 15].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 15].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 15].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 16].Value = "Magazine";
            channel.Cells[row, 16].Style.Font.Bold = true;
            channel.Cells[row, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 16].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 16].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 16].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 16].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 16].Style.Font.Size = 12;
            channel.Cells[row, 16].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 16].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 16].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 16].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 16].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 16].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 16].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 16].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 17].Value = "TV";
            channel.Cells[row, 17].Style.Font.Bold = true;
            channel.Cells[row, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 17].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 17].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 17].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 17].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 17].Style.Font.Size = 12;
            channel.Cells[row, 17].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 17].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 17].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 17].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 17].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 17].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 17].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 17].Style.Border.Bottom.Color.SetColor(Color.Black);

            List<ReportMonthlyChannelDataTransfer> listReportMonthlyChannelDataTransfer = _reportMonthlyRepository.GetChannelByIDToList(ID);
            row = row + 1;
            foreach (ReportMonthlyChannelDataTransfer item in listReportMonthlyChannelDataTransfer)
            {
                channel.Cells[row, 1].Value = item.CompanyName;
                channel.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                channel.Cells[row, 1].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 1].Style.Font.Size = 12;
                channel.Cells[row, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 1].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 1].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 1].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 2].Value = item.OnlineLastMonthCount;
                channel.Cells[row, 2].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 2].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 2].Style.Font.Size = 12;
                channel.Cells[row, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 2].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 2].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 2].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 3].Value = item.NewspaperLastMonthCount;
                channel.Cells[row, 3].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 3].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 3].Style.Font.Size = 12;
                channel.Cells[row, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 3].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 3].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 3].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 3].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 4].Value = item.MagazineLastMonthCount;
                channel.Cells[row, 4].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 4].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 4].Style.Font.Size = 12;
                channel.Cells[row, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 4].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 4].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 4].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 4].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 5].Value = item.TVLastMonthCount;
                channel.Cells[row, 5].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 5].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 5].Style.Font.Size = 12;
                channel.Cells[row, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 5].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 5].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 5].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 5].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 6].Value = item.OnlineCount;
                channel.Cells[row, 6].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 6].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 6].Style.Font.Size = 12;
                channel.Cells[row, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 6].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 6].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 6].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 6].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 7].Value = item.NewspaperCount;
                channel.Cells[row, 7].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 7].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 7].Style.Font.Size = 12;
                channel.Cells[row, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 7].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 7].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 7].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 7].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 8].Value = item.MagazineCount;
                channel.Cells[row, 8].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 8].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 8].Style.Font.Size = 12;
                channel.Cells[row, 8].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 8].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 8].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 8].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 8].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 8].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 8].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 9].Value = item.TVCount;
                channel.Cells[row, 9].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 9].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 9].Style.Font.Size = 12;
                channel.Cells[row, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 9].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 9].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 9].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 9].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 10].Value = item.OnlineGrowth;
                channel.Cells[row, 10].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 10].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 10].Style.Font.Size = 12;
                channel.Cells[row, 10].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 10].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 10].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 10].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 10].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 10].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 11].Value = item.NewspaperGrowth;
                channel.Cells[row, 11].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 11].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 11].Style.Font.Size = 12;
                channel.Cells[row, 11].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 11].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 11].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 11].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 11].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 11].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 11].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 12].Value = item.MagazineGrowth;
                channel.Cells[row, 12].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 12].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 12].Style.Font.Size = 12;
                channel.Cells[row, 12].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 12].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 12].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 12].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 12].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 12].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 12].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 13].Value = item.TVGrowth;
                channel.Cells[row, 13].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 13].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 13].Style.Font.Size = 12;
                channel.Cells[row, 13].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 13].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 13].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 13].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 13].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 13].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 13].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 13].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 14].Value = item.OnlineGrowthPercent;
                channel.Cells[row, 14].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 14].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 14].Style.Font.Size = 12;
                channel.Cells[row, 14].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 14].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 14].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 14].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 14].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 14].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 14].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 14].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 15].Value = item.NewspaperGrowthPercent;
                channel.Cells[row, 15].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 15].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 15].Style.Font.Size = 12;
                channel.Cells[row, 15].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 15].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 15].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 15].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 15].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 15].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 15].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 15].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 16].Value = item.MagazineGrowthPercent;
                channel.Cells[row, 16].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 16].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 16].Style.Font.Size = 12;
                channel.Cells[row, 16].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 16].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 16].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 16].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 16].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 16].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 16].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 16].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 17].Value = item.TVGrowthPercent;
                channel.Cells[row, 17].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 17].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 17].Style.Font.Size = 12;
                channel.Cells[row, 17].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 17].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 17].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 17].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 17].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 17].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 17].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 17].Style.Border.Bottom.Color.SetColor(Color.Black);

                row = row + 1;
            }
            row = row + 1;
            channel.Cells[row, 1].Value = "";
            channel.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 1].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 1].Style.Font.Size = 12;
            channel.Cells[row, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 1].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 1].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 1].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 2].Value = "Last month";
            channel.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 2].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 2].Style.Font.Size = 12;
            channel.Cells[row, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 2].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 2].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 2].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 6].Value = "This month";
            channel.Cells[row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 6].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 6].Style.Font.Size = 12;
            channel.Cells[row, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 6].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 6].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 6].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 6].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 10].Value = "Growth";
            channel.Cells[row, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 10].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 10].Style.Font.Size = 12;
            channel.Cells[row, 10].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 10].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 10].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 10].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 10].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 10].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 14].Value = "Growth (%)";
            channel.Cells[row, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 14].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 14].Style.Font.Size = 12;
            channel.Cells[row, 14].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 14].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 14].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 14].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 14].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 14].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 14].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 14].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 2, row, 5].Merge = true;
            channel.Cells[row, 6, row, 9].Merge = true;
            channel.Cells[row, 10, row, 13].Merge = true;
            channel.Cells[row, 14, row, 17].Merge = true;

            row = row + 1;
            channel.Cells[row, 1].Value = "Company";
            channel.Cells[row, 1].Style.Font.Bold = true;
            channel.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 1].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 1].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 1].Style.Font.Size = 12;
            channel.Cells[row, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 1].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 1].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 1].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 2].Value = "Online";
            channel.Cells[row, 2].Style.Font.Bold = true;
            channel.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 2].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 2].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 2].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 2].Style.Font.Size = 12;
            channel.Cells[row, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 2].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 2].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 2].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 3].Value = "Newspaper";
            channel.Cells[row, 3].Style.Font.Bold = true;
            channel.Cells[row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 3].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 3].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 3].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 3].Style.Font.Size = 12;
            channel.Cells[row, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 3].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 3].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 3].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 3].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 4].Value = "Magazine";
            channel.Cells[row, 4].Style.Font.Bold = true;
            channel.Cells[row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 4].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 4].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 4].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 4].Style.Font.Size = 12;
            channel.Cells[row, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 4].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 4].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 4].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 4].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 5].Value = "TV";
            channel.Cells[row, 5].Style.Font.Bold = true;
            channel.Cells[row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 5].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 5].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 5].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 5].Style.Font.Size = 12;
            channel.Cells[row, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 5].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 5].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 5].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 5].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 6].Value = "Online";
            channel.Cells[row, 6].Style.Font.Bold = true;
            channel.Cells[row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 6].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 6].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 6].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 6].Style.Font.Size = 12;
            channel.Cells[row, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 6].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 6].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 6].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 6].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 7].Value = "Newspaper";
            channel.Cells[row, 7].Style.Font.Bold = true;
            channel.Cells[row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 7].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 7].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 7].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 7].Style.Font.Size = 12;
            channel.Cells[row, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 7].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 7].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 7].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 7].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 8].Value = "Magazine";
            channel.Cells[row, 8].Style.Font.Bold = true;
            channel.Cells[row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 8].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 8].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 8].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 8].Style.Font.Size = 12;
            channel.Cells[row, 8].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 8].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 8].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 8].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 8].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 8].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 8].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 9].Value = "TV";
            channel.Cells[row, 9].Style.Font.Bold = true;
            channel.Cells[row, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 9].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 9].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 9].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 9].Style.Font.Size = 12;
            channel.Cells[row, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 9].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 9].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 9].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 9].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 10].Value = "Online";
            channel.Cells[row, 10].Style.Font.Bold = true;
            channel.Cells[row, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 10].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 10].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 10].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 10].Style.Font.Size = 12;
            channel.Cells[row, 10].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 10].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 10].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 10].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 10].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 10].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 11].Value = "Newspaper";
            channel.Cells[row, 11].Style.Font.Bold = true;
            channel.Cells[row, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 11].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 11].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 11].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 11].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 11].Style.Font.Size = 12;
            channel.Cells[row, 11].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 11].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 11].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 11].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 11].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 11].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 11].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 12].Value = "Magazine";
            channel.Cells[row, 12].Style.Font.Bold = true;
            channel.Cells[row, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 12].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 12].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 12].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 12].Style.Font.Size = 12;
            channel.Cells[row, 12].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 12].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 12].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 12].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 12].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 12].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 12].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 13].Value = "TV";
            channel.Cells[row, 13].Style.Font.Bold = true;
            channel.Cells[row, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 13].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 13].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 13].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 13].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 13].Style.Font.Size = 12;
            channel.Cells[row, 13].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 13].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 13].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 13].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 13].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 13].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 13].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 13].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 14].Value = "Online";
            channel.Cells[row, 14].Style.Font.Bold = true;
            channel.Cells[row, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 14].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 14].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 14].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 14].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 14].Style.Font.Size = 12;
            channel.Cells[row, 14].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 14].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 14].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 14].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 14].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 14].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 14].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 14].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 15].Value = "Newspaper";
            channel.Cells[row, 15].Style.Font.Bold = true;
            channel.Cells[row, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 15].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 15].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 15].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 15].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 15].Style.Font.Size = 12;
            channel.Cells[row, 15].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 15].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 15].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 15].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 15].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 15].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 15].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 15].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 16].Value = "Magazine";
            channel.Cells[row, 16].Style.Font.Bold = true;
            channel.Cells[row, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 16].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 16].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 16].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 16].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 16].Style.Font.Size = 12;
            channel.Cells[row, 16].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 16].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 16].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 16].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 16].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 16].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 16].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 16].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 17].Value = "TV";
            channel.Cells[row, 17].Style.Font.Bold = true;
            channel.Cells[row, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 17].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 17].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 17].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 17].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 17].Style.Font.Size = 12;
            channel.Cells[row, 17].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 17].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 17].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 17].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 17].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 17].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 17].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 17].Style.Border.Bottom.Color.SetColor(Color.Black);

            List<ReportMonthlyChannelDataTransfer> listReportMonthlyChannelDataTransfer001 = _reportMonthlyRepository.GetChannelAndFeatureByIDToList(ID);
            row = row + 1;
            foreach (ReportMonthlyChannelDataTransfer item in listReportMonthlyChannelDataTransfer001)
            {
                channel.Cells[row, 1].Value = item.CompanyName;
                channel.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                channel.Cells[row, 1].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 1].Style.Font.Size = 12;
                channel.Cells[row, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 1].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 1].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 1].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 2].Value = item.OnlineLastMonthCount;
                channel.Cells[row, 2].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 2].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 2].Style.Font.Size = 12;
                channel.Cells[row, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 2].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 2].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 2].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 3].Value = item.NewspaperLastMonthCount;
                channel.Cells[row, 3].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 3].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 3].Style.Font.Size = 12;
                channel.Cells[row, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 3].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 3].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 3].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 3].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 4].Value = item.MagazineLastMonthCount;
                channel.Cells[row, 4].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 4].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 4].Style.Font.Size = 12;
                channel.Cells[row, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 4].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 4].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 4].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 4].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 5].Value = item.TVLastMonthCount;
                channel.Cells[row, 5].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 5].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 5].Style.Font.Size = 12;
                channel.Cells[row, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 5].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 5].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 5].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 5].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 6].Value = item.OnlineCount;
                channel.Cells[row, 6].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 6].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 6].Style.Font.Size = 12;
                channel.Cells[row, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 6].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 6].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 6].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 6].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 7].Value = item.NewspaperCount;
                channel.Cells[row, 7].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 7].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 7].Style.Font.Size = 12;
                channel.Cells[row, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 7].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 7].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 7].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 7].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 8].Value = item.MagazineCount;
                channel.Cells[row, 8].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 8].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 8].Style.Font.Size = 12;
                channel.Cells[row, 8].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 8].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 8].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 8].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 8].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 8].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 8].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 9].Value = item.TVCount;
                channel.Cells[row, 9].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 9].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 9].Style.Font.Size = 12;
                channel.Cells[row, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 9].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 9].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 9].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 9].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 10].Value = item.OnlineGrowth;
                channel.Cells[row, 10].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 10].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 10].Style.Font.Size = 12;
                channel.Cells[row, 10].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 10].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 10].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 10].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 10].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 10].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 11].Value = item.NewspaperGrowth;
                channel.Cells[row, 11].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 11].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 11].Style.Font.Size = 12;
                channel.Cells[row, 11].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 11].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 11].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 11].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 11].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 11].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 11].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 12].Value = item.MagazineGrowth;
                channel.Cells[row, 12].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 12].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 12].Style.Font.Size = 12;
                channel.Cells[row, 12].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 12].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 12].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 12].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 12].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 12].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 12].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 13].Value = item.TVGrowth;
                channel.Cells[row, 13].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 13].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 13].Style.Font.Size = 12;
                channel.Cells[row, 13].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 13].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 13].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 13].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 13].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 13].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 13].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 13].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 14].Value = item.OnlineGrowthPercent;
                channel.Cells[row, 14].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 14].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 14].Style.Font.Size = 12;
                channel.Cells[row, 14].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 14].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 14].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 14].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 14].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 14].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 14].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 14].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 15].Value = item.NewspaperGrowthPercent;
                channel.Cells[row, 15].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 15].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 15].Style.Font.Size = 12;
                channel.Cells[row, 15].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 15].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 15].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 15].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 15].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 15].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 15].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 15].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 16].Value = item.MagazineGrowthPercent;
                channel.Cells[row, 16].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 16].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 16].Style.Font.Size = 12;
                channel.Cells[row, 16].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 16].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 16].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 16].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 16].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 16].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 16].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 16].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 17].Value = item.TVGrowthPercent;
                channel.Cells[row, 17].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 17].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 17].Style.Font.Size = 12;
                channel.Cells[row, 17].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 17].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 17].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 17].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 17].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 17].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 17].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 17].Style.Border.Bottom.Color.SetColor(Color.Black);

                row = row + 1;
            }

            row = row + 1;
            channel.Cells[row, 1].Value = "";
            channel.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 1].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 1].Style.Font.Size = 12;
            channel.Cells[row, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 1].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 1].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 1].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 2].Value = "Last month";
            channel.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 2].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 2].Style.Font.Size = 12;
            channel.Cells[row, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 2].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 2].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 2].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 6].Value = "This month";
            channel.Cells[row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 6].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 6].Style.Font.Size = 12;
            channel.Cells[row, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 6].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 6].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 6].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 6].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 10].Value = "Growth";
            channel.Cells[row, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 10].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 10].Style.Font.Size = 12;
            channel.Cells[row, 10].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 10].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 10].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 10].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 10].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 10].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 14].Value = "Growth (%)";
            channel.Cells[row, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 14].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 14].Style.Font.Size = 12;
            channel.Cells[row, 14].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 14].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 14].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 14].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 14].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 14].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 14].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 14].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 2, row, 5].Merge = true;
            channel.Cells[row, 6, row, 9].Merge = true;
            channel.Cells[row, 10, row, 13].Merge = true;
            channel.Cells[row, 14, row, 17].Merge = true;

            row = row + 1;
            channel.Cells[row, 1].Value = "Company";
            channel.Cells[row, 1].Style.Font.Bold = true;
            channel.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 1].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 1].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 1].Style.Font.Size = 12;
            channel.Cells[row, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 1].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 1].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 1].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 2].Value = "Online";
            channel.Cells[row, 2].Style.Font.Bold = true;
            channel.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 2].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 2].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 2].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 2].Style.Font.Size = 12;
            channel.Cells[row, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 2].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 2].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 2].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 3].Value = "Newspaper";
            channel.Cells[row, 3].Style.Font.Bold = true;
            channel.Cells[row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 3].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 3].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 3].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 3].Style.Font.Size = 12;
            channel.Cells[row, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 3].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 3].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 3].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 3].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 4].Value = "Magazine";
            channel.Cells[row, 4].Style.Font.Bold = true;
            channel.Cells[row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 4].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 4].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 4].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 4].Style.Font.Size = 12;
            channel.Cells[row, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 4].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 4].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 4].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 4].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 5].Value = "TV";
            channel.Cells[row, 5].Style.Font.Bold = true;
            channel.Cells[row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 5].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 5].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 5].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 5].Style.Font.Size = 12;
            channel.Cells[row, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 5].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 5].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 5].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 5].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 6].Value = "Online";
            channel.Cells[row, 6].Style.Font.Bold = true;
            channel.Cells[row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 6].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 6].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 6].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 6].Style.Font.Size = 12;
            channel.Cells[row, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 6].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 6].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 6].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 6].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 7].Value = "Newspaper";
            channel.Cells[row, 7].Style.Font.Bold = true;
            channel.Cells[row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 7].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 7].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 7].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 7].Style.Font.Size = 12;
            channel.Cells[row, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 7].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 7].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 7].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 7].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 8].Value = "Magazine";
            channel.Cells[row, 8].Style.Font.Bold = true;
            channel.Cells[row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 8].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 8].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 8].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 8].Style.Font.Size = 12;
            channel.Cells[row, 8].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 8].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 8].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 8].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 8].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 8].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 8].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 9].Value = "TV";
            channel.Cells[row, 9].Style.Font.Bold = true;
            channel.Cells[row, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 9].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 9].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 9].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 9].Style.Font.Size = 12;
            channel.Cells[row, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 9].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 9].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 9].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 9].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 10].Value = "Online";
            channel.Cells[row, 10].Style.Font.Bold = true;
            channel.Cells[row, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 10].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 10].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 10].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 10].Style.Font.Size = 12;
            channel.Cells[row, 10].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 10].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 10].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 10].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 10].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 10].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 11].Value = "Newspaper";
            channel.Cells[row, 11].Style.Font.Bold = true;
            channel.Cells[row, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 11].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 11].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 11].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 11].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 11].Style.Font.Size = 12;
            channel.Cells[row, 11].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 11].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 11].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 11].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 11].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 11].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 11].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 12].Value = "Magazine";
            channel.Cells[row, 12].Style.Font.Bold = true;
            channel.Cells[row, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 12].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 12].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 12].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 12].Style.Font.Size = 12;
            channel.Cells[row, 12].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 12].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 12].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 12].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 12].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 12].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 12].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 13].Value = "TV";
            channel.Cells[row, 13].Style.Font.Bold = true;
            channel.Cells[row, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 13].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 13].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 13].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 13].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 13].Style.Font.Size = 12;
            channel.Cells[row, 13].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 13].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 13].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 13].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 13].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 13].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 13].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 13].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 14].Value = "Online";
            channel.Cells[row, 14].Style.Font.Bold = true;
            channel.Cells[row, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 14].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 14].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 14].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 14].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 14].Style.Font.Size = 12;
            channel.Cells[row, 14].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 14].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 14].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 14].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 14].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 14].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 14].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 14].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 15].Value = "Newspaper";
            channel.Cells[row, 15].Style.Font.Bold = true;
            channel.Cells[row, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 15].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 15].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 15].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 15].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 15].Style.Font.Size = 12;
            channel.Cells[row, 15].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 15].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 15].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 15].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 15].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 15].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 15].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 15].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 16].Value = "Magazine";
            channel.Cells[row, 16].Style.Font.Bold = true;
            channel.Cells[row, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 16].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 16].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 16].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 16].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 16].Style.Font.Size = 12;
            channel.Cells[row, 16].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 16].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 16].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 16].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 16].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 16].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 16].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 16].Style.Border.Bottom.Color.SetColor(Color.Black);

            channel.Cells[row, 17].Value = "TV";
            channel.Cells[row, 17].Style.Font.Bold = true;
            channel.Cells[row, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            channel.Cells[row, 17].Style.Font.Color.SetColor(System.Drawing.Color.White);
            channel.Cells[row, 17].Style.Fill.PatternType = ExcelFillStyle.Solid;
            channel.Cells[row, 17].Style.Fill.BackgroundColor.SetColor(color);
            channel.Cells[row, 17].Style.Font.Name = "Times New Roman";
            channel.Cells[row, 17].Style.Font.Size = 12;
            channel.Cells[row, 17].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 17].Style.Border.Top.Color.SetColor(Color.Black);
            channel.Cells[row, 17].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 17].Style.Border.Left.Color.SetColor(Color.Black);
            channel.Cells[row, 17].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 17].Style.Border.Right.Color.SetColor(Color.Black);
            channel.Cells[row, 17].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            channel.Cells[row, 17].Style.Border.Bottom.Color.SetColor(Color.Black);

            List<ReportMonthlyChannelDataTransfer> listReportMonthlyChannelDataTransfer002 = _reportMonthlyRepository.GetChannelAndMentionByIDToList(ID);
            row = row + 1;
            foreach (ReportMonthlyChannelDataTransfer item in listReportMonthlyChannelDataTransfer002)
            {
                channel.Cells[row, 1].Value = item.CompanyName;
                channel.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                channel.Cells[row, 1].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 1].Style.Font.Size = 12;
                channel.Cells[row, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 1].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 1].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 1].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 2].Value = item.OnlineLastMonthCount;
                channel.Cells[row, 2].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 2].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 2].Style.Font.Size = 12;
                channel.Cells[row, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 2].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 2].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 2].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 3].Value = item.NewspaperLastMonthCount;
                channel.Cells[row, 3].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 3].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 3].Style.Font.Size = 12;
                channel.Cells[row, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 3].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 3].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 3].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 3].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 4].Value = item.MagazineLastMonthCount;
                channel.Cells[row, 4].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 4].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 4].Style.Font.Size = 12;
                channel.Cells[row, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 4].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 4].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 4].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 4].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 5].Value = item.TVLastMonthCount;
                channel.Cells[row, 5].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 5].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 5].Style.Font.Size = 12;
                channel.Cells[row, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 5].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 5].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 5].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 5].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 6].Value = item.OnlineCount;
                channel.Cells[row, 6].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 6].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 6].Style.Font.Size = 12;
                channel.Cells[row, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 6].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 6].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 6].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 6].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 7].Value = item.NewspaperCount;
                channel.Cells[row, 7].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 7].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 7].Style.Font.Size = 12;
                channel.Cells[row, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 7].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 7].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 7].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 7].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 8].Value = item.MagazineCount;
                channel.Cells[row, 8].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 8].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 8].Style.Font.Size = 12;
                channel.Cells[row, 8].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 8].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 8].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 8].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 8].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 8].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 8].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 9].Value = item.TVCount;
                channel.Cells[row, 9].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 9].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 9].Style.Font.Size = 12;
                channel.Cells[row, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 9].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 9].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 9].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 9].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 10].Value = item.OnlineGrowth;
                channel.Cells[row, 10].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 10].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 10].Style.Font.Size = 12;
                channel.Cells[row, 10].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 10].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 10].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 10].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 10].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 10].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 11].Value = item.NewspaperGrowth;
                channel.Cells[row, 11].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 11].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 11].Style.Font.Size = 12;
                channel.Cells[row, 11].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 11].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 11].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 11].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 11].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 11].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 11].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 12].Value = item.MagazineGrowth;
                channel.Cells[row, 12].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 12].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 12].Style.Font.Size = 12;
                channel.Cells[row, 12].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 12].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 12].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 12].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 12].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 12].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 12].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 13].Value = item.TVGrowth;
                channel.Cells[row, 13].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 13].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 13].Style.Font.Size = 12;
                channel.Cells[row, 13].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 13].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 13].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 13].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 13].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 13].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 13].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 13].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 14].Value = item.OnlineGrowthPercent;
                channel.Cells[row, 14].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 14].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 14].Style.Font.Size = 12;
                channel.Cells[row, 14].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 14].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 14].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 14].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 14].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 14].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 14].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 14].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 15].Value = item.NewspaperGrowthPercent;
                channel.Cells[row, 15].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 15].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 15].Style.Font.Size = 12;
                channel.Cells[row, 15].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 15].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 15].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 15].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 15].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 15].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 15].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 15].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 16].Value = item.MagazineGrowthPercent;
                channel.Cells[row, 16].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 16].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 16].Style.Font.Size = 12;
                channel.Cells[row, 16].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 16].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 16].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 16].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 16].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 16].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 16].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 16].Style.Border.Bottom.Color.SetColor(Color.Black);

                channel.Cells[row, 17].Value = item.TVGrowthPercent;
                channel.Cells[row, 17].Style.Numberformat.Format = "#,##0";
                channel.Cells[row, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                channel.Cells[row, 17].Style.Font.Name = "Times New Roman";
                channel.Cells[row, 17].Style.Font.Size = 12;
                channel.Cells[row, 17].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 17].Style.Border.Top.Color.SetColor(Color.Black);
                channel.Cells[row, 17].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 17].Style.Border.Left.Color.SetColor(Color.Black);
                channel.Cells[row, 17].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 17].Style.Border.Right.Color.SetColor(Color.Black);
                channel.Cells[row, 17].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                channel.Cells[row, 17].Style.Border.Bottom.Color.SetColor(Color.Black);

                row = row + 1;
            }
            channel.Column(1).AutoFit();
            channel.Column(2).AutoFit();
            channel.Column(3).AutoFit();
            channel.Column(4).AutoFit();
            channel.Column(5).AutoFit();
            channel.Column(6).AutoFit();
            channel.Column(7).AutoFit();
            channel.Column(8).AutoFit();
            channel.Column(9).AutoFit();
            channel.Column(10).AutoFit();
            channel.Column(11).AutoFit();
            channel.Column(12).AutoFit();
            channel.Column(13).AutoFit();
            channel.Column(14).AutoFit();
            channel.Column(15).AutoFit();
            channel.Column(16).AutoFit();
            channel.Column(17).AutoFit();
        }
        private void InitializationTierCommsights(ExcelPackage package, Color color, int ID)
        {
            var tierCommsights = package.Workbook.Worksheets.Add("MediaTiers");
            int row = 1;
            tierCommsights.Cells[row, 1].Value = "";
            tierCommsights.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            tierCommsights.Cells[row, 1].Style.Font.Name = "Times New Roman";
            tierCommsights.Cells[row, 1].Style.Font.Size = 12;
            tierCommsights.Cells[row, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 1].Style.Border.Top.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 1].Style.Border.Left.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 1].Style.Border.Right.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

            tierCommsights.Cells[row, 2].Value = "Last month";
            tierCommsights.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            tierCommsights.Cells[row, 2].Style.Font.Name = "Times New Roman";
            tierCommsights.Cells[row, 2].Style.Font.Size = 12;
            tierCommsights.Cells[row, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 2].Style.Border.Top.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 2].Style.Border.Left.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 2].Style.Border.Right.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

            tierCommsights.Cells[row, 6].Value = "This month";
            tierCommsights.Cells[row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            tierCommsights.Cells[row, 6].Style.Font.Name = "Times New Roman";
            tierCommsights.Cells[row, 6].Style.Font.Size = 12;
            tierCommsights.Cells[row, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 6].Style.Border.Top.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 6].Style.Border.Left.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 6].Style.Border.Right.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 6].Style.Border.Bottom.Color.SetColor(Color.Black);

            tierCommsights.Cells[row, 10].Value = "Growth (%)";
            tierCommsights.Cells[row, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            tierCommsights.Cells[row, 10].Style.Font.Name = "Times New Roman";
            tierCommsights.Cells[row, 10].Style.Font.Size = 12;
            tierCommsights.Cells[row, 10].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 10].Style.Border.Top.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 10].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 10].Style.Border.Left.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 10].Style.Border.Right.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 10].Style.Border.Bottom.Color.SetColor(Color.Black);

            tierCommsights.Cells[row, 2, row, 5].Merge = true;
            tierCommsights.Cells[row, 6, row, 9].Merge = true;
            tierCommsights.Cells[row, 10, row, 13].Merge = true;

            row = row + 1;
            tierCommsights.Cells[row, 1].Value = "Company";
            tierCommsights.Cells[row, 1].Style.Font.Bold = true;
            tierCommsights.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            tierCommsights.Cells[row, 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
            tierCommsights.Cells[row, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            tierCommsights.Cells[row, 1].Style.Fill.BackgroundColor.SetColor(color);
            tierCommsights.Cells[row, 1].Style.Font.Name = "Times New Roman";
            tierCommsights.Cells[row, 1].Style.Font.Size = 12;
            tierCommsights.Cells[row, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 1].Style.Border.Top.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 1].Style.Border.Left.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 1].Style.Border.Right.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

            tierCommsights.Cells[row, 2].Value = "Mass media";
            tierCommsights.Cells[row, 2].Style.Font.Bold = true;
            tierCommsights.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            tierCommsights.Cells[row, 2].Style.Font.Color.SetColor(System.Drawing.Color.White);
            tierCommsights.Cells[row, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
            tierCommsights.Cells[row, 2].Style.Fill.BackgroundColor.SetColor(color);
            tierCommsights.Cells[row, 2].Style.Font.Name = "Times New Roman";
            tierCommsights.Cells[row, 2].Style.Font.Size = 12;
            tierCommsights.Cells[row, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 2].Style.Border.Top.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 2].Style.Border.Left.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 2].Style.Border.Right.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

            tierCommsights.Cells[row, 3].Value = "Industry media";
            tierCommsights.Cells[row, 3].Style.Font.Bold = true;
            tierCommsights.Cells[row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            tierCommsights.Cells[row, 3].Style.Font.Color.SetColor(System.Drawing.Color.White);
            tierCommsights.Cells[row, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
            tierCommsights.Cells[row, 3].Style.Fill.BackgroundColor.SetColor(color);
            tierCommsights.Cells[row, 3].Style.Font.Name = "Times New Roman";
            tierCommsights.Cells[row, 3].Style.Font.Size = 12;
            tierCommsights.Cells[row, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 3].Style.Border.Top.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 3].Style.Border.Left.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 3].Style.Border.Right.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 3].Style.Border.Bottom.Color.SetColor(Color.Black);

            tierCommsights.Cells[row, 4].Value = "Portal";
            tierCommsights.Cells[row, 4].Style.Font.Bold = true;
            tierCommsights.Cells[row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            tierCommsights.Cells[row, 4].Style.Font.Color.SetColor(System.Drawing.Color.White);
            tierCommsights.Cells[row, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
            tierCommsights.Cells[row, 4].Style.Fill.BackgroundColor.SetColor(color);
            tierCommsights.Cells[row, 4].Style.Font.Name = "Times New Roman";
            tierCommsights.Cells[row, 4].Style.Font.Size = 12;
            tierCommsights.Cells[row, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 4].Style.Border.Top.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 4].Style.Border.Left.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 4].Style.Border.Right.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 4].Style.Border.Bottom.Color.SetColor(Color.Black);

            tierCommsights.Cells[row, 5].Value = "Others";
            tierCommsights.Cells[row, 5].Style.Font.Bold = true;
            tierCommsights.Cells[row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            tierCommsights.Cells[row, 5].Style.Font.Color.SetColor(System.Drawing.Color.White);
            tierCommsights.Cells[row, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
            tierCommsights.Cells[row, 5].Style.Fill.BackgroundColor.SetColor(color);
            tierCommsights.Cells[row, 5].Style.Font.Name = "Times New Roman";
            tierCommsights.Cells[row, 5].Style.Font.Size = 12;
            tierCommsights.Cells[row, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 5].Style.Border.Top.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 5].Style.Border.Left.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 5].Style.Border.Right.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 5].Style.Border.Bottom.Color.SetColor(Color.Black);

            tierCommsights.Cells[row, 6].Value = "Mass media";
            tierCommsights.Cells[row, 6].Style.Font.Bold = true;
            tierCommsights.Cells[row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            tierCommsights.Cells[row, 6].Style.Font.Color.SetColor(System.Drawing.Color.White);
            tierCommsights.Cells[row, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
            tierCommsights.Cells[row, 6].Style.Fill.BackgroundColor.SetColor(color);
            tierCommsights.Cells[row, 6].Style.Font.Name = "Times New Roman";
            tierCommsights.Cells[row, 6].Style.Font.Size = 12;
            tierCommsights.Cells[row, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 6].Style.Border.Top.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 6].Style.Border.Left.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 6].Style.Border.Right.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 6].Style.Border.Bottom.Color.SetColor(Color.Black);

            tierCommsights.Cells[row, 7].Value = "Industry media";
            tierCommsights.Cells[row, 7].Style.Font.Bold = true;
            tierCommsights.Cells[row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            tierCommsights.Cells[row, 7].Style.Font.Color.SetColor(System.Drawing.Color.White);
            tierCommsights.Cells[row, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
            tierCommsights.Cells[row, 7].Style.Fill.BackgroundColor.SetColor(color);
            tierCommsights.Cells[row, 7].Style.Font.Name = "Times New Roman";
            tierCommsights.Cells[row, 7].Style.Font.Size = 12;
            tierCommsights.Cells[row, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 7].Style.Border.Top.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 7].Style.Border.Left.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 7].Style.Border.Right.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 7].Style.Border.Bottom.Color.SetColor(Color.Black);

            tierCommsights.Cells[row, 8].Value = "Portal";
            tierCommsights.Cells[row, 8].Style.Font.Bold = true;
            tierCommsights.Cells[row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            tierCommsights.Cells[row, 8].Style.Font.Color.SetColor(System.Drawing.Color.White);
            tierCommsights.Cells[row, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
            tierCommsights.Cells[row, 8].Style.Fill.BackgroundColor.SetColor(color);
            tierCommsights.Cells[row, 8].Style.Font.Name = "Times New Roman";
            tierCommsights.Cells[row, 8].Style.Font.Size = 12;
            tierCommsights.Cells[row, 8].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 8].Style.Border.Top.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 8].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 8].Style.Border.Left.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 8].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 8].Style.Border.Right.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 8].Style.Border.Bottom.Color.SetColor(Color.Black);

            tierCommsights.Cells[row, 9].Value = "Others";
            tierCommsights.Cells[row, 9].Style.Font.Bold = true;
            tierCommsights.Cells[row, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            tierCommsights.Cells[row, 9].Style.Font.Color.SetColor(System.Drawing.Color.White);
            tierCommsights.Cells[row, 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
            tierCommsights.Cells[row, 9].Style.Fill.BackgroundColor.SetColor(color);
            tierCommsights.Cells[row, 9].Style.Font.Name = "Times New Roman";
            tierCommsights.Cells[row, 9].Style.Font.Size = 12;
            tierCommsights.Cells[row, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 9].Style.Border.Top.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 9].Style.Border.Left.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 9].Style.Border.Right.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 9].Style.Border.Bottom.Color.SetColor(Color.Black);

            tierCommsights.Cells[row, 10].Value = "Mass media";
            tierCommsights.Cells[row, 10].Style.Font.Bold = true;
            tierCommsights.Cells[row, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            tierCommsights.Cells[row, 10].Style.Font.Color.SetColor(System.Drawing.Color.White);
            tierCommsights.Cells[row, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
            tierCommsights.Cells[row, 10].Style.Fill.BackgroundColor.SetColor(color);
            tierCommsights.Cells[row, 10].Style.Font.Name = "Times New Roman";
            tierCommsights.Cells[row, 10].Style.Font.Size = 12;
            tierCommsights.Cells[row, 10].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 10].Style.Border.Top.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 10].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 10].Style.Border.Left.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 10].Style.Border.Right.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 10].Style.Border.Bottom.Color.SetColor(Color.Black);

            tierCommsights.Cells[row, 11].Value = "Industry media";
            tierCommsights.Cells[row, 11].Style.Font.Bold = true;
            tierCommsights.Cells[row, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            tierCommsights.Cells[row, 11].Style.Font.Color.SetColor(System.Drawing.Color.White);
            tierCommsights.Cells[row, 11].Style.Fill.PatternType = ExcelFillStyle.Solid;
            tierCommsights.Cells[row, 11].Style.Fill.BackgroundColor.SetColor(color);
            tierCommsights.Cells[row, 11].Style.Font.Name = "Times New Roman";
            tierCommsights.Cells[row, 11].Style.Font.Size = 12;
            tierCommsights.Cells[row, 11].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 11].Style.Border.Top.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 11].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 11].Style.Border.Left.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 11].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 11].Style.Border.Right.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 11].Style.Border.Bottom.Color.SetColor(Color.Black);

            tierCommsights.Cells[row, 12].Value = "Portal";
            tierCommsights.Cells[row, 12].Style.Font.Bold = true;
            tierCommsights.Cells[row, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            tierCommsights.Cells[row, 12].Style.Font.Color.SetColor(System.Drawing.Color.White);
            tierCommsights.Cells[row, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
            tierCommsights.Cells[row, 12].Style.Fill.BackgroundColor.SetColor(color);
            tierCommsights.Cells[row, 12].Style.Font.Name = "Times New Roman";
            tierCommsights.Cells[row, 12].Style.Font.Size = 12;
            tierCommsights.Cells[row, 12].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 12].Style.Border.Top.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 12].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 12].Style.Border.Left.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 12].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 12].Style.Border.Right.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 12].Style.Border.Bottom.Color.SetColor(Color.Black);

            tierCommsights.Cells[row, 13].Value = "Others";
            tierCommsights.Cells[row, 13].Style.Font.Bold = true;
            tierCommsights.Cells[row, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            tierCommsights.Cells[row, 13].Style.Font.Color.SetColor(System.Drawing.Color.White);
            tierCommsights.Cells[row, 13].Style.Fill.PatternType = ExcelFillStyle.Solid;
            tierCommsights.Cells[row, 13].Style.Fill.BackgroundColor.SetColor(color);
            tierCommsights.Cells[row, 13].Style.Font.Name = "Times New Roman";
            tierCommsights.Cells[row, 13].Style.Font.Size = 12;
            tierCommsights.Cells[row, 13].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 13].Style.Border.Top.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 13].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 13].Style.Border.Left.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 13].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 13].Style.Border.Right.Color.SetColor(Color.Black);
            tierCommsights.Cells[row, 13].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            tierCommsights.Cells[row, 13].Style.Border.Bottom.Color.SetColor(Color.Black);

            List<ReportMonthlyTierCommsightsDataTransfer> listReportMonthlyTierCommsightsDataTransfer = _reportMonthlyRepository.GetTierCommsightsByIDToList(ID);
            row = row + 1;
            foreach (ReportMonthlyTierCommsightsDataTransfer item in listReportMonthlyTierCommsightsDataTransfer)
            {
                tierCommsights.Cells[row, 1].Value = item.CompanyName;
                tierCommsights.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                tierCommsights.Cells[row, 1].Style.Font.Name = "Times New Roman";
                tierCommsights.Cells[row, 1].Style.Font.Size = 12;
                tierCommsights.Cells[row, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 1].Style.Border.Top.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 1].Style.Border.Left.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 1].Style.Border.Right.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

                tierCommsights.Cells[row, 2].Value = item.MassLastMonthCount;
                tierCommsights.Cells[row, 2].Style.Numberformat.Format = "#,##0";
                tierCommsights.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                tierCommsights.Cells[row, 2].Style.Font.Name = "Times New Roman";
                tierCommsights.Cells[row, 2].Style.Font.Size = 12;
                tierCommsights.Cells[row, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 2].Style.Border.Top.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 2].Style.Border.Left.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 2].Style.Border.Right.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

                tierCommsights.Cells[row, 3].Value = item.IndustryLastMonthCount;
                tierCommsights.Cells[row, 3].Style.Numberformat.Format = "#,##0";
                tierCommsights.Cells[row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                tierCommsights.Cells[row, 3].Style.Font.Name = "Times New Roman";
                tierCommsights.Cells[row, 3].Style.Font.Size = 12;
                tierCommsights.Cells[row, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 3].Style.Border.Top.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 3].Style.Border.Left.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 3].Style.Border.Right.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 3].Style.Border.Bottom.Color.SetColor(Color.Black);

                tierCommsights.Cells[row, 4].Value = item.PortalLastMonthCount;
                tierCommsights.Cells[row, 4].Style.Numberformat.Format = "#,##0";
                tierCommsights.Cells[row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                tierCommsights.Cells[row, 4].Style.Font.Name = "Times New Roman";
                tierCommsights.Cells[row, 4].Style.Font.Size = 12;
                tierCommsights.Cells[row, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 4].Style.Border.Top.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 4].Style.Border.Left.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 4].Style.Border.Right.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 4].Style.Border.Bottom.Color.SetColor(Color.Black);

                tierCommsights.Cells[row, 5].Value = item.OrthersLastMonthCount;
                tierCommsights.Cells[row, 5].Style.Numberformat.Format = "#,##0";
                tierCommsights.Cells[row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                tierCommsights.Cells[row, 5].Style.Font.Name = "Times New Roman";
                tierCommsights.Cells[row, 5].Style.Font.Size = 12;
                tierCommsights.Cells[row, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 5].Style.Border.Top.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 5].Style.Border.Left.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 5].Style.Border.Right.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 5].Style.Border.Bottom.Color.SetColor(Color.Black);

                tierCommsights.Cells[row, 6].Value = item.MassCount;
                tierCommsights.Cells[row, 6].Style.Numberformat.Format = "#,##0";
                tierCommsights.Cells[row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                tierCommsights.Cells[row, 6].Style.Font.Name = "Times New Roman";
                tierCommsights.Cells[row, 6].Style.Font.Size = 12;
                tierCommsights.Cells[row, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 6].Style.Border.Top.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 6].Style.Border.Left.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 6].Style.Border.Right.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 6].Style.Border.Bottom.Color.SetColor(Color.Black);

                tierCommsights.Cells[row, 7].Value = item.IndustryCount;
                tierCommsights.Cells[row, 7].Style.Numberformat.Format = "#,##0";
                tierCommsights.Cells[row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                tierCommsights.Cells[row, 7].Style.Font.Name = "Times New Roman";
                tierCommsights.Cells[row, 7].Style.Font.Size = 12;
                tierCommsights.Cells[row, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 7].Style.Border.Top.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 7].Style.Border.Left.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 7].Style.Border.Right.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 7].Style.Border.Bottom.Color.SetColor(Color.Black);

                tierCommsights.Cells[row, 8].Value = item.PortalCount;
                tierCommsights.Cells[row, 8].Style.Numberformat.Format = "#,##0";
                tierCommsights.Cells[row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                tierCommsights.Cells[row, 8].Style.Font.Name = "Times New Roman";
                tierCommsights.Cells[row, 8].Style.Font.Size = 12;
                tierCommsights.Cells[row, 8].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 8].Style.Border.Top.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 8].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 8].Style.Border.Left.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 8].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 8].Style.Border.Right.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 8].Style.Border.Bottom.Color.SetColor(Color.Black);

                tierCommsights.Cells[row, 9].Value = item.OrthersCount;
                tierCommsights.Cells[row, 9].Style.Numberformat.Format = "#,##0";
                tierCommsights.Cells[row, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                tierCommsights.Cells[row, 9].Style.Font.Name = "Times New Roman";
                tierCommsights.Cells[row, 9].Style.Font.Size = 12;
                tierCommsights.Cells[row, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 9].Style.Border.Top.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 9].Style.Border.Left.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 9].Style.Border.Right.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 9].Style.Border.Bottom.Color.SetColor(Color.Black);

                tierCommsights.Cells[row, 10].Value = item.MassGrowthPercent;
                tierCommsights.Cells[row, 10].Style.Numberformat.Format = "#,##0";
                tierCommsights.Cells[row, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                tierCommsights.Cells[row, 10].Style.Font.Name = "Times New Roman";
                tierCommsights.Cells[row, 10].Style.Font.Size = 12;
                tierCommsights.Cells[row, 10].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 10].Style.Border.Top.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 10].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 10].Style.Border.Left.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 10].Style.Border.Right.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 10].Style.Border.Bottom.Color.SetColor(Color.Black);

                tierCommsights.Cells[row, 11].Value = item.IndustryGrowthPercent;
                tierCommsights.Cells[row, 11].Style.Numberformat.Format = "#,##0";
                tierCommsights.Cells[row, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                tierCommsights.Cells[row, 11].Style.Font.Name = "Times New Roman";
                tierCommsights.Cells[row, 11].Style.Font.Size = 12;
                tierCommsights.Cells[row, 11].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 11].Style.Border.Top.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 11].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 11].Style.Border.Left.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 11].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 11].Style.Border.Right.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 11].Style.Border.Bottom.Color.SetColor(Color.Black);

                tierCommsights.Cells[row, 12].Value = item.PortalGrowthPercent;
                tierCommsights.Cells[row, 12].Style.Numberformat.Format = "#,##0";
                tierCommsights.Cells[row, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                tierCommsights.Cells[row, 12].Style.Font.Name = "Times New Roman";
                tierCommsights.Cells[row, 12].Style.Font.Size = 12;
                tierCommsights.Cells[row, 12].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 12].Style.Border.Top.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 12].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 12].Style.Border.Left.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 12].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 12].Style.Border.Right.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 12].Style.Border.Bottom.Color.SetColor(Color.Black);

                tierCommsights.Cells[row, 13].Value = item.OrthersGrowthPercent;
                tierCommsights.Cells[row, 13].Style.Numberformat.Format = "#,##0";
                tierCommsights.Cells[row, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                tierCommsights.Cells[row, 13].Style.Font.Name = "Times New Roman";
                tierCommsights.Cells[row, 13].Style.Font.Size = 12;
                tierCommsights.Cells[row, 13].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 13].Style.Border.Top.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 13].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 13].Style.Border.Left.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 13].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 13].Style.Border.Right.Color.SetColor(Color.Black);
                tierCommsights.Cells[row, 13].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                tierCommsights.Cells[row, 13].Style.Border.Bottom.Color.SetColor(Color.Black);

                row = row + 1;
            }

            tierCommsights.Column(1).AutoFit();
            tierCommsights.Column(2).AutoFit();
            tierCommsights.Column(3).AutoFit();
            tierCommsights.Column(4).AutoFit();
            tierCommsights.Column(5).AutoFit();
            tierCommsights.Column(6).AutoFit();
            tierCommsights.Column(7).AutoFit();
            tierCommsights.Column(8).AutoFit();
            tierCommsights.Column(9).AutoFit();
            tierCommsights.Column(10).AutoFit();
            tierCommsights.Column(11).AutoFit();
            tierCommsights.Column(12).AutoFit();
            tierCommsights.Column(13).AutoFit();
        }
        private void InitializationTrendline(ExcelPackage package, Color color, int ID)
        {
            var trendline = package.Workbook.Worksheets.Add("Trendline");
            int row = 1;
            List<ReportMonthlyTrendLineDataTransfer> list = _reportMonthlyRepository.GetTrendLineWithoutSUMByIDToList(ID);
            List<ReportMonthlyTrendLineDataTransfer> listCompanyName = _reportMonthlyRepository.GetTrendLineDistinctCompanyNameByIDToList(ID);
            trendline.Cells[row, 1].Value = "Month";
            trendline.Cells[row, 1].Style.Font.Bold = true;
            trendline.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            trendline.Cells[row, 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
            trendline.Cells[row, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            trendline.Cells[row, 1].Style.Fill.BackgroundColor.SetColor(color);
            trendline.Cells[row, 1].Style.Font.Name = "Times New Roman";
            trendline.Cells[row, 1].Style.Font.Size = 12;
            trendline.Cells[row, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            trendline.Cells[row, 1].Style.Border.Top.Color.SetColor(Color.Black);
            trendline.Cells[row, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            trendline.Cells[row, 1].Style.Border.Left.Color.SetColor(Color.Black);
            trendline.Cells[row, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            trendline.Cells[row, 1].Style.Border.Right.Color.SetColor(Color.Black);
            trendline.Cells[row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            trendline.Cells[row, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

            trendline.Cells[row, 2].Value = "Day";
            trendline.Cells[row, 2].Style.Font.Bold = true;
            trendline.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            trendline.Cells[row, 2].Style.Font.Color.SetColor(System.Drawing.Color.White);
            trendline.Cells[row, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
            trendline.Cells[row, 2].Style.Fill.BackgroundColor.SetColor(color);
            trendline.Cells[row, 2].Style.Font.Name = "Times New Roman";
            trendline.Cells[row, 2].Style.Font.Size = 12;
            trendline.Cells[row, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            trendline.Cells[row, 2].Style.Border.Top.Color.SetColor(Color.Black);
            trendline.Cells[row, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            trendline.Cells[row, 2].Style.Border.Left.Color.SetColor(Color.Black);
            trendline.Cells[row, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            trendline.Cells[row, 2].Style.Border.Right.Color.SetColor(Color.Black);
            trendline.Cells[row, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            trendline.Cells[row, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

            int columnIndex = 3;
            foreach (ReportMonthlyTrendLineDataTransfer item in listCompanyName)
            {
                trendline.Cells[row, columnIndex].Value = item.CompanyName;
                trendline.Cells[row, columnIndex].Style.Font.Bold = true;
                trendline.Cells[row, columnIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                trendline.Cells[row, columnIndex].Style.Font.Color.SetColor(System.Drawing.Color.White);
                trendline.Cells[row, columnIndex].Style.Fill.PatternType = ExcelFillStyle.Solid;
                trendline.Cells[row, columnIndex].Style.Fill.BackgroundColor.SetColor(color);
                trendline.Cells[row, columnIndex].Style.Font.Name = "Times New Roman";
                trendline.Cells[row, columnIndex].Style.Font.Size = 12;
                trendline.Cells[row, columnIndex].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                trendline.Cells[row, columnIndex].Style.Border.Top.Color.SetColor(Color.Black);
                trendline.Cells[row, columnIndex].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                trendline.Cells[row, columnIndex].Style.Border.Left.Color.SetColor(Color.Black);
                trendline.Cells[row, columnIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                trendline.Cells[row, columnIndex].Style.Border.Right.Color.SetColor(Color.Black);
                trendline.Cells[row, columnIndex].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                trendline.Cells[row, columnIndex].Style.Border.Bottom.Color.SetColor(Color.Black);
                columnIndex = columnIndex + 1;
            }
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
                string monthString = "";
                string monthLastString = "";
                switch (model.Month)
                {
                    case 1:
                        monthString = "Jan";
                        break;
                    case 2:
                        monthString = "Feb";
                        break;
                    case 3:
                        monthString = "Mar";
                        break;
                    case 4:
                        monthString = "Apr";
                        break;
                    case 5:
                        monthString = "May";
                        break;
                    case 6:
                        monthString = "Jun";
                        break;
                    case 7:
                        monthString = "Jul";
                        break;
                    case 8:
                        monthString = "Aug";
                        break;
                    case 9:
                        monthString = "Sep";
                        break;
                    case 10:
                        monthString = "Oct";
                        break;
                    case 11:
                        monthString = "Nov";
                        break;
                    case 12:
                        monthString = "Dec";
                        break;
                }
                switch (monthLast)
                {
                    case 1:
                        monthLastString = "Jan";
                        break;
                    case 2:
                        monthLastString = "Feb";
                        break;
                    case 3:
                        monthLastString = "Mar";
                        break;
                    case 4:
                        monthLastString = "Apr";
                        break;
                    case 5:
                        monthLastString = "May";
                        break;
                    case 6:
                        monthLastString = "Jun";
                        break;
                    case 7:
                        monthLastString = "Jul";
                        break;
                    case 8:
                        monthLastString = "Aug";
                        break;
                    case 9:
                        monthLastString = "Sep";
                        break;
                    case 10:
                        monthLastString = "Oct";
                        break;
                    case 11:
                        monthLastString = "Nov";
                        break;
                    case 12:
                        monthLastString = "Dec";
                        break;
                }
                tbl.Columns.Add(new DataColumn("Year"));
                tbl.Columns.Add(new DataColumn("Month"));
                tbl.Columns.Add(new DataColumn("Day"));
                tbl.Columns.Add(new DataColumn("MonthString"));
                tbl.Columns.Add(new DataColumn("Date"));
                foreach (ReportMonthlyTrendLineDataTransfer item in listCompanyName)
                {
                    tbl.Columns.Add(new DataColumn(item.CompanyName));
                }
                for (int i = 1; i < 32; i++)
                {
                    string monthString001 = model.Month.ToString();
                    if (model.Month < 10)
                    {
                        monthString001 = "0" + monthString001;
                    }
                    string dayString001 = i.ToString();
                    if (i < 10)
                    {
                        dayString001 = "0" + dayString001;
                    }
                    DataRow dr = tbl.NewRow();
                    dr["Year"] = model.Year;
                    dr["Month"] = model.Month;
                    dr["Day"] = i;
                    dr["MonthString"] = monthString + "-" + model.Year;
                    dr["Date"] = monthString001 + "/" + dayString001 + "/" + model.Year;
                    tbl.Rows.Add(dr);
                }
                for (int i = 1; i < 32; i++)
                {
                    string monthString001 = monthLast.ToString();
                    if (monthLast < 10)
                    {
                        monthString001 = "0" + monthString001;
                    }
                    string dayString001 = i.ToString();
                    if (i < 10)
                    {
                        dayString001 = "0" + dayString001;
                    }
                    DataRow dr = tbl.NewRow();
                    dr["Year"] = yearLast;
                    dr["Month"] = monthLast;
                    dr["Day"] = i;
                    dr["MonthString"] = monthLastString + "-" + yearLast;
                    dr["Date"] = monthString001 + "/" + dayString001 + "/" + yearLast;
                    tbl.Rows.Add(dr);
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
                row = row + 1;
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    trendline.Cells[row, 1].Value = tbl.Rows[i]["MonthString"].ToString();
                    trendline.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    trendline.Cells[row, 1].Style.Font.Name = "Times New Roman";
                    trendline.Cells[row, 1].Style.Font.Size = 12;
                    trendline.Cells[row, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    trendline.Cells[row, 1].Style.Border.Top.Color.SetColor(Color.Black);
                    trendline.Cells[row, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    trendline.Cells[row, 1].Style.Border.Left.Color.SetColor(Color.Black);
                    trendline.Cells[row, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    trendline.Cells[row, 1].Style.Border.Right.Color.SetColor(Color.Black);
                    trendline.Cells[row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    trendline.Cells[row, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

                    trendline.Cells[row, 2].Value = tbl.Rows[i]["Date"].ToString();
                    trendline.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    trendline.Cells[row, 2].Style.Font.Name = "Times New Roman";
                    trendline.Cells[row, 2].Style.Font.Size = 12;
                    trendline.Cells[row, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    trendline.Cells[row, 2].Style.Border.Top.Color.SetColor(Color.Black);
                    trendline.Cells[row, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    trendline.Cells[row, 2].Style.Border.Left.Color.SetColor(Color.Black);
                    trendline.Cells[row, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    trendline.Cells[row, 2].Style.Border.Right.Color.SetColor(Color.Black);
                    trendline.Cells[row, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    trendline.Cells[row, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

                    for (int j = 5; j < tbl.Columns.Count; j++)
                    {
                        int columnsIndex = j - 2;
                        trendline.Cells[row, columnsIndex].Value = tbl.Rows[i][tbl.Columns[j].ColumnName].ToString();
                        trendline.Cells[row, columnsIndex].Style.Numberformat.Format = "#,##0";
                        trendline.Cells[row, columnsIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        trendline.Cells[row, columnsIndex].Style.Font.Name = "Times New Roman";
                        trendline.Cells[row, columnsIndex].Style.Font.Size = 12;
                        trendline.Cells[row, columnsIndex].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        trendline.Cells[row, columnsIndex].Style.Border.Top.Color.SetColor(Color.Black);
                        trendline.Cells[row, columnsIndex].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        trendline.Cells[row, columnsIndex].Style.Border.Left.Color.SetColor(Color.Black);
                        trendline.Cells[row, columnsIndex].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        trendline.Cells[row, columnsIndex].Style.Border.Right.Color.SetColor(Color.Black);
                        trendline.Cells[row, columnsIndex].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        trendline.Cells[row, columnsIndex].Style.Border.Bottom.Color.SetColor(Color.Black);
                    }
                    row = row + 1;
                }
            }
            for (int i = 1; i < listCompanyName.Count + 4; i++)
            {
                trendline.Column(i).AutoFit();
            }
        }
        private void InitializationTopTitles(ExcelPackage package, Color color, int ID)
        {
            var topTitles = package.Workbook.Worksheets.Add("TopTitles");
            int row = 1;
            topTitles.Cells[row, 1].Value = "Company";
            topTitles.Cells[row, 1].Style.Font.Bold = true;
            topTitles.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            topTitles.Cells[row, 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
            topTitles.Cells[row, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            topTitles.Cells[row, 1].Style.Fill.BackgroundColor.SetColor(color);
            topTitles.Cells[row, 1].Style.Font.Name = "Times New Roman";
            topTitles.Cells[row, 1].Style.Font.Size = 12;
            topTitles.Cells[row, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            topTitles.Cells[row, 1].Style.Border.Top.Color.SetColor(Color.Black);
            topTitles.Cells[row, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            topTitles.Cells[row, 1].Style.Border.Left.Color.SetColor(Color.Black);
            topTitles.Cells[row, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            topTitles.Cells[row, 1].Style.Border.Right.Color.SetColor(Color.Black);
            topTitles.Cells[row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            topTitles.Cells[row, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

            topTitles.Cells[row, 2].Value = "Mass";
            topTitles.Cells[row, 2].Style.Font.Bold = true;
            topTitles.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            topTitles.Cells[row, 2].Style.Font.Color.SetColor(System.Drawing.Color.White);
            topTitles.Cells[row, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
            topTitles.Cells[row, 2].Style.Fill.BackgroundColor.SetColor(color);
            topTitles.Cells[row, 2].Style.Font.Name = "Times New Roman";
            topTitles.Cells[row, 2].Style.Font.Size = 12;
            topTitles.Cells[row, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            topTitles.Cells[row, 2].Style.Border.Top.Color.SetColor(Color.Black);
            topTitles.Cells[row, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            topTitles.Cells[row, 2].Style.Border.Left.Color.SetColor(Color.Black);
            topTitles.Cells[row, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            topTitles.Cells[row, 2].Style.Border.Right.Color.SetColor(Color.Black);
            topTitles.Cells[row, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            topTitles.Cells[row, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

            topTitles.Cells[row, 3].Value = "Industry";
            topTitles.Cells[row, 3].Style.Font.Bold = true;
            topTitles.Cells[row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            topTitles.Cells[row, 3].Style.Font.Color.SetColor(System.Drawing.Color.White);
            topTitles.Cells[row, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
            topTitles.Cells[row, 3].Style.Fill.BackgroundColor.SetColor(color);
            topTitles.Cells[row, 3].Style.Font.Name = "Times New Roman";
            topTitles.Cells[row, 3].Style.Font.Size = 12;
            topTitles.Cells[row, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            topTitles.Cells[row, 3].Style.Border.Top.Color.SetColor(Color.Black);
            topTitles.Cells[row, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            topTitles.Cells[row, 3].Style.Border.Left.Color.SetColor(Color.Black);
            topTitles.Cells[row, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            topTitles.Cells[row, 3].Style.Border.Right.Color.SetColor(Color.Black);
            topTitles.Cells[row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            topTitles.Cells[row, 3].Style.Border.Bottom.Color.SetColor(Color.Black);

            topTitles.Cells[row, 4].Value = "Portal";
            topTitles.Cells[row, 4].Style.Font.Bold = true;
            topTitles.Cells[row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            topTitles.Cells[row, 4].Style.Font.Color.SetColor(System.Drawing.Color.White);
            topTitles.Cells[row, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
            topTitles.Cells[row, 4].Style.Fill.BackgroundColor.SetColor(color);
            topTitles.Cells[row, 4].Style.Font.Name = "Times New Roman";
            topTitles.Cells[row, 4].Style.Font.Size = 12;
            topTitles.Cells[row, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            topTitles.Cells[row, 4].Style.Border.Top.Color.SetColor(Color.Black);
            topTitles.Cells[row, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            topTitles.Cells[row, 4].Style.Border.Left.Color.SetColor(Color.Black);
            topTitles.Cells[row, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            topTitles.Cells[row, 4].Style.Border.Right.Color.SetColor(Color.Black);
            topTitles.Cells[row, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            topTitles.Cells[row, 4].Style.Border.Bottom.Color.SetColor(Color.Black);

            topTitles.Cells[row, 5].Value = "Other";
            topTitles.Cells[row, 5].Style.Font.Bold = true;
            topTitles.Cells[row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            topTitles.Cells[row, 5].Style.Font.Color.SetColor(System.Drawing.Color.White);
            topTitles.Cells[row, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
            topTitles.Cells[row, 5].Style.Fill.BackgroundColor.SetColor(color);
            topTitles.Cells[row, 5].Style.Font.Name = "Times New Roman";
            topTitles.Cells[row, 5].Style.Font.Size = 12;
            topTitles.Cells[row, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            topTitles.Cells[row, 5].Style.Border.Top.Color.SetColor(Color.Black);
            topTitles.Cells[row, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            topTitles.Cells[row, 5].Style.Border.Left.Color.SetColor(Color.Black);
            topTitles.Cells[row, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            topTitles.Cells[row, 5].Style.Border.Right.Color.SetColor(Color.Black);
            topTitles.Cells[row, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            topTitles.Cells[row, 5].Style.Border.Bottom.Color.SetColor(Color.Black);

            ReportMonthlyViewModel model = new ReportMonthlyViewModel();
            if (ID > 0)
            {
                row = row + 1;
                model.ID = ID;
                model.ListCompanyName = _reportMonthlyRepository.GetTierCommsightsAndCompanyNameDistinctByIDToList(model.ID);
                model.ListTierCommsightsAndCompanyNameAndPortal = _reportMonthlyRepository.GetTierCommsightsAndCompanyNameAndPortalByIDToList(model.ID);
                model.ListTierCommsightsAndCompanyNameAndOther = _reportMonthlyRepository.GetTierCommsightsAndCompanyNameAndOtherByIDToList(model.ID);
                model.ListTierCommsightsAndCompanyNameAndMass = _reportMonthlyRepository.GetTierCommsightsAndCompanyNameAndMassByIDToList(model.ID);
                model.ListTierCommsightsAndCompanyNameAndIndustry = _reportMonthlyRepository.GetTierCommsightsAndCompanyNameAndIndustryByIDToList(model.ID);

                foreach (ReportMonthlyTierCommsightsAndCompanyNameDataTransfer item in model.ListCompanyName)
                {
                    topTitles.Cells[row, 1].Value = item.CompanyName;
                    topTitles.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    topTitles.Cells[row, 1].Style.Font.Name = "Times New Roman";
                    topTitles.Cells[row, 1].Style.Font.Size = 12;
                    topTitles.Cells[row, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    topTitles.Cells[row, 1].Style.Border.Top.Color.SetColor(Color.Black);
                    topTitles.Cells[row, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    topTitles.Cells[row, 1].Style.Border.Left.Color.SetColor(Color.Black);
                    topTitles.Cells[row, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    topTitles.Cells[row, 1].Style.Border.Right.Color.SetColor(Color.Black);
                    topTitles.Cells[row, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    topTitles.Cells[row, 1].Style.Border.Bottom.Color.SetColor(Color.Black);
                    topTitles.Cells[row, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    StringBuilder txtMass = new StringBuilder();
                    foreach (ReportMonthlyTierCommsightsAndCompanyNameDataTransfer itemMass in model.ListTierCommsightsAndCompanyNameAndMass)
                    {
                        if (itemMass.CompanyName == item.CompanyName)
                        {
                            txtMass.AppendLine(itemMass.Media + " (" + itemMass.TierCount + ")");
                        }
                    }
                    StringBuilder txtIndustry = new StringBuilder();
                    foreach (ReportMonthlyTierCommsightsAndCompanyNameDataTransfer itemIndustry in model.ListTierCommsightsAndCompanyNameAndIndustry)
                    {
                        if (itemIndustry.CompanyName == item.CompanyName)
                        {
                            txtIndustry.AppendLine(itemIndustry.Media + " (" + itemIndustry.TierCount + ")");
                        }
                    }
                    StringBuilder txtPortal = new StringBuilder();
                    foreach (ReportMonthlyTierCommsightsAndCompanyNameDataTransfer itemPortal in model.ListTierCommsightsAndCompanyNameAndPortal)
                    {
                        if (itemPortal.CompanyName == item.CompanyName)
                        {
                            txtPortal.AppendLine(itemPortal.Media + " (" + itemPortal.TierCount + ")");
                        }
                    }
                    StringBuilder txtOther = new StringBuilder();
                    foreach (ReportMonthlyTierCommsightsAndCompanyNameDataTransfer itemOther in model.ListTierCommsightsAndCompanyNameAndOther)
                    {
                        if (itemOther.CompanyName == item.CompanyName)
                        {
                            txtOther.AppendLine(itemOther.Media + " (" + itemOther.TierCount + ")");
                        }
                    }

                    topTitles.Cells[row, 2].Value = txtMass.ToString();
                    topTitles.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    topTitles.Cells[row, 2].Style.Font.Name = "Times New Roman";
                    topTitles.Cells[row, 2].Style.Font.Size = 12;
                    topTitles.Cells[row, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    topTitles.Cells[row, 2].Style.Border.Top.Color.SetColor(Color.Black);
                    topTitles.Cells[row, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    topTitles.Cells[row, 2].Style.Border.Left.Color.SetColor(Color.Black);
                    topTitles.Cells[row, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    topTitles.Cells[row, 2].Style.Border.Right.Color.SetColor(Color.Black);
                    topTitles.Cells[row, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    topTitles.Cells[row, 2].Style.Border.Bottom.Color.SetColor(Color.Black);
                    topTitles.Cells[row, 2].Style.WrapText = true;
                    topTitles.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    topTitles.Cells[row, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                    topTitles.Cells[row, 3].Value = txtIndustry.ToString();
                    topTitles.Cells[row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    topTitles.Cells[row, 3].Style.Font.Name = "Times New Roman";
                    topTitles.Cells[row, 3].Style.Font.Size = 12;
                    topTitles.Cells[row, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    topTitles.Cells[row, 3].Style.Border.Top.Color.SetColor(Color.Black);
                    topTitles.Cells[row, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    topTitles.Cells[row, 3].Style.Border.Left.Color.SetColor(Color.Black);
                    topTitles.Cells[row, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    topTitles.Cells[row, 3].Style.Border.Right.Color.SetColor(Color.Black);
                    topTitles.Cells[row, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    topTitles.Cells[row, 3].Style.Border.Bottom.Color.SetColor(Color.Black);
                    topTitles.Cells[row, 3].Style.WrapText = true;
                    topTitles.Cells[row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    topTitles.Cells[row, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                    topTitles.Cells[row, 4].Value = txtPortal.ToString();
                    topTitles.Cells[row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    topTitles.Cells[row, 4].Style.Font.Name = "Times New Roman";
                    topTitles.Cells[row, 4].Style.Font.Size = 12;
                    topTitles.Cells[row, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    topTitles.Cells[row, 4].Style.Border.Top.Color.SetColor(Color.Black);
                    topTitles.Cells[row, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    topTitles.Cells[row, 4].Style.Border.Left.Color.SetColor(Color.Black);
                    topTitles.Cells[row, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    topTitles.Cells[row, 4].Style.Border.Right.Color.SetColor(Color.Black);
                    topTitles.Cells[row, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    topTitles.Cells[row, 4].Style.Border.Bottom.Color.SetColor(Color.Black);
                    topTitles.Cells[row, 4].Style.WrapText = true;
                    topTitles.Cells[row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    topTitles.Cells[row, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                    topTitles.Cells[row, 5].Value = txtOther.ToString();
                    topTitles.Cells[row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    topTitles.Cells[row, 5].Style.Font.Name = "Times New Roman";
                    topTitles.Cells[row, 5].Style.Font.Size = 12;
                    topTitles.Cells[row, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    topTitles.Cells[row, 5].Style.Border.Top.Color.SetColor(Color.Black);
                    topTitles.Cells[row, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    topTitles.Cells[row, 5].Style.Border.Left.Color.SetColor(Color.Black);
                    topTitles.Cells[row, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    topTitles.Cells[row, 5].Style.Border.Right.Color.SetColor(Color.Black);
                    topTitles.Cells[row, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    topTitles.Cells[row, 5].Style.Border.Bottom.Color.SetColor(Color.Black);
                    topTitles.Cells[row, 5].Style.WrapText = true;
                    topTitles.Cells[row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    topTitles.Cells[row, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                    row = row + 1;
                }
            }
            topTitles.Column(1).Width = 20;
            topTitles.Column(2).Width = 40;
            topTitles.Column(3).Width = 40;
            topTitles.Column(4).Width = 40;
            topTitles.Column(5).Width = 40;
        }
        private void InitializationData(ExcelPackage package, Color color, int ID)
        {
            var data = package.Workbook.Worksheets.Add("Data");
            int row = 1;

            data.Cells[row, 1].Value = "Source";
            data.Cells[row, 2].Value = "Date";
            data.Cells[row, 3].Value = "Main_Category";
            data.Cells[row, 4].Value = "Sub_Category";
            data.Cells[row, 5].Value = "Company";
            data.Cells[row, 6].Value = "Corp_Copy";
            data.Cells[row, 7].Value = "SOE_Corp";
            data.Cells[row, 8].Value = "Feature_Corp";
            data.Cells[row, 9].Value = "Sentiment_Corp";
            data.Cells[row, 10].Value = "Segment_Product";
            data.Cells[row, 11].Value = "Product_Name";
            data.Cells[row, 12].Value = "SOE_Product";
            data.Cells[row, 13].Value = "Feature_Product";
            data.Cells[row, 14].Value = "Sentiment_Product";
            data.Cells[row, 15].Value = "Headline";
            data.Cells[row, 16].Value = "Headline_English";
            data.Cells[row, 17].Value = "URL";
            data.Cells[row, 18].Value = "Page";
            data.Cells[row, 19].Value = "Duration";
            data.Cells[row, 20].Value = "Journalist";
            data.Cells[row, 21].Value = "Tier_CommSights";
            data.Cells[row, 22].Value = "Tier_Customer";
            data.Cells[row, 23].Value = "Spoke_Person_Name";
            data.Cells[row, 24].Value = "Spoke_Person_Title";
            data.Cells[row, 25].Value = "Tone_Value";
            data.Cells[row, 26].Value = "Headline_Value";
            data.Cells[row, 27].Value = "Spoke_Person_Value";
            data.Cells[row, 28].Value = "Feature_Value";
            data.Cells[row, 29].Value = "Tier_Value";
            data.Cells[row, 30].Value = "Picture_Value";
            data.Cells[row, 31].Value = "MPS";
            data.Cells[row, 32].Value = "ROME_Corp";
            data.Cells[row, 33].Value = "ROME_Product";
            data.Cells[row, 34].Value = "MediaTitle";
            data.Cells[row, 35].Value = "MediaType";
            data.Cells[row, 36].Value = "Advalue";

            for (int i = 1; i < 37; i++)
            {
                data.Cells[row, i].Style.Font.Bold = true;
                data.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                data.Cells[row, i].Style.Font.Color.SetColor(System.Drawing.Color.White);
                data.Cells[row, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                data.Cells[row, i].Style.Fill.BackgroundColor.SetColor(color);
                data.Cells[row, i].Style.Font.Name = "Times New Roman";
                data.Cells[row, i].Style.Font.Size = 12;
                data.Cells[row, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                data.Cells[row, i].Style.Border.Top.Color.SetColor(Color.Black);
                data.Cells[row, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                data.Cells[row, i].Style.Border.Left.Color.SetColor(Color.Black);
                data.Cells[row, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                data.Cells[row, i].Style.Border.Right.Color.SetColor(Color.Black);
                data.Cells[row, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                data.Cells[row, i].Style.Border.Bottom.Color.SetColor(Color.Black);
            }
            List<ProductProperty> list = _productPropertyRepository.GetByReportMonthlyIDToList(ID);
            row = row + 1;
            foreach (ProductProperty item in list)
            {
                data.Cells[row, 1].Value = item.Source;
                data.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                data.Cells[row, 2].Value = item.DatePublish;
                data.Cells[row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                data.Cells[row, 2].Style.Numberformat.Format = "MM/dd/yyyy";

                data.Cells[row, 3].Value = item.CategoryMain;
                data.Cells[row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                data.Cells[row, 4].Value = item.CategorySub;
                data.Cells[row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                data.Cells[row, 5].Value = item.CompanyName;
                data.Cells[row, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                data.Cells[row, 6].Value = item.CorpCopy;
                data.Cells[row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                if (item.SOECompany != null)
                {
                    data.Cells[row, 7].Value = item.SOECompany.Value.ToString("N0") + "%";
                    data.Cells[row, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    data.Cells[row, 7].Style.Numberformat.Format = "#,##0";
                }

                data.Cells[row, 8].Value = item.FeatureCorp;
                data.Cells[row, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                data.Cells[row, 9].Value = item.SentimentCorp;
                data.Cells[row, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                data.Cells[row, 10].Value = item.Segment;
                data.Cells[row, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                data.Cells[row, 11].Value = item.ProductName_ProjectName;
                data.Cells[row, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                if (item.SOEProduct != null)
                {
                    data.Cells[row, 12].Value = item.SOEProduct.Value.ToString("N0") + "%";
                    data.Cells[row, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    data.Cells[row, 12].Style.Numberformat.Format = "#,##0";
                }
                data.Cells[row, 13].Value = item.FeatureProduct;
                data.Cells[row, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                data.Cells[row, 14].Value = item.SentimentProduct;
                data.Cells[row, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                data.Cells[row, 15].Value = item.Headline;
                data.Cells[row, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                if ((!string.IsNullOrEmpty(item.URL)) && (!string.IsNullOrEmpty(item.Headline)))
                {
                    try
                    {
                        data.Cells[row, 15].Hyperlink = new Uri(item.URL);
                    }
                    catch
                    {
                    }
                    data.Cells[row, 15].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                }

                data.Cells[row, 16].Value = item.HeadlineEngLish;
                data.Cells[row, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                if ((!string.IsNullOrEmpty(item.URL)) && (!string.IsNullOrEmpty(item.HeadlineEngLish)))
                {
                    try
                    {
                        data.Cells[row, 16].Hyperlink = new Uri(item.URL);
                    }
                    catch
                    {
                    }
                    data.Cells[row, 16].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                }

                data.Cells[row, 17].Value = item.URL;
                data.Cells[row, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                if (!string.IsNullOrEmpty(item.URL))
                {
                    try
                    {
                        data.Cells[row, 17].Hyperlink = new Uri(item.URL);
                    }
                    catch
                    {
                    }
                    data.Cells[row, 17].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                }

                data.Cells[row, 18].Value = item.Page;
                data.Cells[row, 18].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                data.Cells[row, 19].Value = item.Duration;
                data.Cells[row, 19].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                data.Cells[row, 20].Value = item.Journalist;
                data.Cells[row, 20].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                data.Cells[row, 21].Value = item.TierCommsights;
                data.Cells[row, 21].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                data.Cells[row, 22].Value = item.TierCustomer;
                data.Cells[row, 22].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                data.Cells[row, 23].Value = item.SpokePersonName;
                data.Cells[row, 23].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                data.Cells[row, 24].Value = item.SpokePersonTitle;
                data.Cells[row, 24].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                data.Cells[row, 25].Value = item.ToneValue;
                data.Cells[row, 25].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                data.Cells[row, 25].Style.Numberformat.Format = "#,##0";

                data.Cells[row, 26].Value = item.HeadlineValue;
                data.Cells[row, 26].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                data.Cells[row, 26].Style.Numberformat.Format = "#,##0";

                data.Cells[row, 27].Value = item.SpokePersonValue;
                data.Cells[row, 27].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                data.Cells[row, 27].Style.Numberformat.Format = "#,##0";

                data.Cells[row, 28].Value = item.FeatureValue;
                data.Cells[row, 28].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                data.Cells[row, 28].Style.Numberformat.Format = "#,##0";

                data.Cells[row, 29].Value = item.TierValue;
                data.Cells[row, 29].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                data.Cells[row, 29].Style.Numberformat.Format = "#,##0";

                data.Cells[row, 30].Value = item.PictureValue;
                data.Cells[row, 30].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                data.Cells[row, 30].Style.Numberformat.Format = "#,##0";

                data.Cells[row, 31].Value = item.MPS;
                data.Cells[row, 31].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                data.Cells[row, 31].Style.Numberformat.Format = "#,##0";

                data.Cells[row, 32].Value = item.ROME_Corp_VND;
                data.Cells[row, 32].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                data.Cells[row, 32].Style.Numberformat.Format = "#,##0";

                data.Cells[row, 33].Value = item.ROME_Product_VND;
                data.Cells[row, 33].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                data.Cells[row, 33].Style.Numberformat.Format = "#,##0";

                data.Cells[row, 34].Value = item.MediaTitle;
                data.Cells[row, 34].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                data.Cells[row, 35].Value = item.MediaType;
                data.Cells[row, 35].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                data.Cells[row, 36].Value = item.Advalue.Value.ToString("N0");
                data.Cells[row, 36].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                data.Cells[row, 36].Style.Numberformat.Format = "#,##0";
                row = row + 1;
            }
            row = 2;
            foreach (ProductProperty item in list)
            {
                for (int i = 1; i < 37; i++)
                {
                    data.Cells[row, i].Style.Font.Name = "Times New Roman";
                    data.Cells[row, i].Style.Font.Size = 12;
                    data.Cells[row, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    data.Cells[row, i].Style.Border.Top.Color.SetColor(Color.Black);
                    data.Cells[row, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    data.Cells[row, i].Style.Border.Left.Color.SetColor(Color.Black);
                    data.Cells[row, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    data.Cells[row, i].Style.Border.Right.Color.SetColor(Color.Black);
                    data.Cells[row, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    data.Cells[row, i].Style.Border.Bottom.Color.SetColor(Color.Black);
                }
                row = row + 1;
            }

            for (int i = 1; i < 37; i++)
            {
                data.Column(i).AutoFit();
            }
        }
    }
}
