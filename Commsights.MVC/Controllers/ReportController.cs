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
using System.Web;

namespace Commsights.MVC.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IReportRepository _reportRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductPropertyRepository _productPropertyRepository;
        private readonly IProductSearchRepository _productSearchRepository;
        private readonly IProductSearchPropertyRepository _productSearchPropertyRepository;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMembershipPermissionRepository _membershipPermissionRepository;
        private readonly IConfigRepository _configResposistory;
        private readonly IBaiVietUploadCountRepository _baiVietUploadCountRepository;
        private readonly IBaiVietUploadRepository _baiVietUploadRepository;
        private readonly IMailService _mailService;
        public ReportController(IWebHostEnvironment hostingEnvironment, IMailService mailService, IBaiVietUploadCountRepository baiVietUploadCountRepository, IBaiVietUploadRepository baiVietUploadRepository, IConfigRepository configResposistory, IMembershipRepository membershipRepository, IMembershipPermissionRepository membershipPermissionRepository, IProductRepository productRepository, IProductPropertyRepository productPropertyRepository, IReportRepository reportRepository, IProductSearchRepository productSearchRepository, IProductSearchPropertyRepository productSearchPropertyRepository, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _reportRepository = reportRepository;
            _productRepository = productRepository;
            _productPropertyRepository = productPropertyRepository;
            _productSearchRepository = productSearchRepository;
            _productSearchPropertyRepository = productSearchPropertyRepository;
            _membershipRepository = membershipRepository;
            _membershipPermissionRepository = membershipPermissionRepository;
            _configResposistory = configResposistory;
            _baiVietUploadRepository = baiVietUploadRepository;
            _baiVietUploadCountRepository = baiVietUploadCountRepository;
            _mailService = mailService;
        }
        private void Initialization(ProductSearchDataTransfer model)
        {
            if (!string.IsNullOrEmpty(model.Title))
            {
                model.Title = model.Title.Trim();
            }
            if (!string.IsNullOrEmpty(model.Summary))
            {
                model.Summary = model.Summary.Trim();
            }
        }
        private void Initialization(ProductDataTransfer model)
        {
            if (!string.IsNullOrEmpty(model.TitleEnglish))
            {
                model.TitleEnglish = model.TitleEnglish.Trim();
            }
            if (!string.IsNullOrEmpty(model.Description))
            {
                model.Description = model.Description.Trim();
            }
            if (!string.IsNullOrEmpty(model.DescriptionEnglish))
            {
                model.DescriptionEnglish = model.DescriptionEnglish.Trim();
            }
        }
        public IActionResult SearchByKeywordAndDateBeginAndDateEnd()
        {
            ProductSearch model = new ProductSearch();
            DateTime datePublishEnd = DateTime.Now;
            DateTime datePublishBegin = DateTime.Now;
            model.DatePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day);
            model.DatePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day);
            return View(model);
        }
        public IActionResult Index(int industryID, string datePublishBeginString, string datePublishEndString)
        {
            BaseViewModel model = new BaseViewModel();
            model.DatePublishBegin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            model.DatePublishEnd = DateTime.Now;
            model.IndustryID = AppGlobal.IndustryID;
            int day = 0;
            int month = 0;
            int year = 0;
            if (industryID > 0)
            {
                model.IndustryID = industryID;
            }
            if (!string.IsNullOrEmpty(datePublishBeginString))
            {
                try
                {
                    day = int.Parse(datePublishBeginString.Split('-')[2]);
                    month = int.Parse(datePublishBeginString.Split('-')[1]);
                    year = int.Parse(datePublishBeginString.Split('-')[0]);
                    model.DatePublishBegin = new DateTime(year, month, day);
                }
                catch
                {
                }
            }
            if (!string.IsNullOrEmpty(datePublishEndString))
            {
                try
                {
                    day = int.Parse(datePublishEndString.Split('-')[2]);
                    month = int.Parse(datePublishEndString.Split('-')[1]);
                    year = int.Parse(datePublishEndString.Split('-')[0]);
                    model.DatePublishEnd = new DateTime(year, month, day);
                }
                catch
                {
                }
            }
            _reportRepository.UpdateProductByDatePublishBeginAndDatePublishEndAndIndustryID(model.DatePublishBegin, model.DatePublishEnd, model.IndustryID);
            return View(model);
        }
        public IActionResult DailyData()
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
        public IActionResult DailyDataAll()
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
        public IActionResult DailyData2020(int industryID, string datePublishBeginString, string datePublishEndString)
        {
            BaseViewModel model = new BaseViewModel();
            model.DatePublishBegin = DateTime.Now;
            model.DatePublishEnd = DateTime.Now;
            model.IndustryID = AppGlobal.IndustryID;
            int day = 0;
            int month = 0;
            int year = 0;
            if (industryID > 0)
            {
                model.IndustryID = industryID;
            }
            if (!string.IsNullOrEmpty(datePublishBeginString))
            {
                try
                {
                    day = int.Parse(datePublishBeginString.Split('-')[2]);
                    month = int.Parse(datePublishBeginString.Split('-')[1]);
                    year = int.Parse(datePublishBeginString.Split('-')[0]);
                    model.DatePublishBegin = new DateTime(year, month, day);
                }
                catch
                {
                }
            }
            if (!string.IsNullOrEmpty(datePublishEndString))
            {
                try
                {
                    day = int.Parse(datePublishEndString.Split('-')[2]);
                    month = int.Parse(datePublishEndString.Split('-')[1]);
                    year = int.Parse(datePublishEndString.Split('-')[0]);
                    model.DatePublishEnd = new DateTime(year, month, day);
                }
                catch
                {
                }
            }
            _reportRepository.UpdateProductByDatePublishBeginAndDatePublishEndAndIndustryID(model.DatePublishBegin, model.DatePublishEnd, model.IndustryID);
            return View(model);
        }
        public IActionResult DailyPreview(int industryID, string datePublishBeginString, string datePublishEndString)
        {
            int day = 0;
            int month = 0;
            int year = 0;
            BaseViewModel model = new BaseViewModel();
            model.DatePublishBegin = DateTime.Now.AddDays(-1);
            model.DatePublishEnd = DateTime.Now;
            if (industryID > 0)
            {
                model.IndustryID = industryID;
                model.IndustryName = _configResposistory.GetByID(model.IndustryID).CodeName;
            }
            if (!string.IsNullOrEmpty(datePublishBeginString))
            {
                try
                {
                    day = int.Parse(datePublishBeginString.Split('-')[2]);
                    month = int.Parse(datePublishBeginString.Split('-')[1]);
                    year = int.Parse(datePublishBeginString.Split('-')[0]);
                    model.DatePublishBegin = new DateTime(year, month, day);
                }
                catch
                {
                }
            }
            if (!string.IsNullOrEmpty(datePublishEndString))
            {
                try
                {
                    day = int.Parse(datePublishEndString.Split('-')[2]);
                    month = int.Parse(datePublishEndString.Split('-')[1]);
                    year = int.Parse(datePublishEndString.Split('-')[0]);
                    model.DatePublishEnd = new DateTime(year, month, day);
                }
                catch
                {
                }
            }
            model.DatePublishBeginString = model.DatePublishBegin.ToString("yyyy-MM-dd");
            model.DatePublishEndString = model.DatePublishEnd.ToString("yyyy-MM-dd");
            return View(model);
        }
        public IActionResult Upload()
        {
            BaseViewModel model = new BaseViewModel();
            model.Action = "Upload";
            return View(model);
        }
        public IActionResult UploadAndiView()
        {
            BaseViewModel model = new BaseViewModel();
            model.Action = "UploadAndiView";
            return View(model);
        }
        public IActionResult UploadGoogleView()
        {
            BaseViewModel model = new BaseViewModel();
            model.Action = "UploadGoogleView";
            return View(model);
        }
        public IActionResult UploadScanView()
        {
            BaseViewModel model = new BaseViewModel();
            model.Action = "UploadScanView";
            return View(model);
        }
        public IActionResult UploadYounetView()
        {
            BaseViewModel model = new BaseViewModel();
            model.Action = "UploadYounetView";
            return View(model);
        }
        public IActionResult ViewContent(int ID)
        {
            ProductDataTransfer model = _reportRepository.GetProductDataTransferByProductPropertyID(ID);
            return View(model);
        }
        public IActionResult DataHTML(int ID)
        {
            ProductSearchDataTransfer model = new ProductSearchDataTransfer();
            if (ID > 0)
            {
                model = _productSearchRepository.GetDataTransferByID(ID);
            }
            return View(model);
        }
        public IActionResult DailyPrintPreview(int ID)
        {
            ProductSearchDataTransfer model = new ProductSearchDataTransfer();
            if (ID > 0)
            {
                model = _productSearchRepository.GetDataTransferByID(ID);
            }
            return View(model);
        }
        public IActionResult DailyPrintPreviewFormHTML(int ID)
        {
            ProductSearchDataTransfer model = new ProductSearchDataTransfer();
            if (ID > 0)
            {
                model = _productSearchRepository.GetDataTransferByID(ID);
                model = InitializationReportDailyHTML(model, "ReportDailySub.html");
            }
            return View(model);
        }
        public IActionResult DailyPrintPreviewByIndustryIDAndDatePublishBeginAndDatePublishEnd(int industryID, string datePublishBeginString, string datePublishEndString, bool isUpload)
        {
            DateTime datePublishBegin = DateTime.Now;
            DateTime datePublishEnd = DateTime.Now;
            int day = 0;
            int month = 0;
            int year = 0;
            if (!string.IsNullOrEmpty(datePublishBeginString))
            {
                try
                {
                    day = int.Parse(datePublishBeginString.Split('-')[2]);
                    month = int.Parse(datePublishBeginString.Split('-')[1]);
                    year = int.Parse(datePublishBeginString.Split('-')[0]);
                    datePublishBegin = new DateTime(year, month, day);
                }
                catch
                {
                }
            }
            if (!string.IsNullOrEmpty(datePublishEndString))
            {
                try
                {
                    day = int.Parse(datePublishEndString.Split('-')[2]);
                    month = int.Parse(datePublishEndString.Split('-')[1]);
                    year = int.Parse(datePublishEndString.Split('-')[0]);
                    datePublishEnd = new DateTime(year, month, day);
                }
                catch
                {
                }
            }
            BaseViewModel model = new BaseViewModel();
            List<Config> listDailyReportColumn = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.DailyReportColumn);
            List<ProductDataTransfer> listData = _reportRepository.GetProductDataTransferByDatePublishBeginAndDatePublishEndAndIndustryIDAndIsDailyAndIsUploadToList(datePublishBegin, datePublishEnd, industryID, true, isUpload);
            List<ProductDataTransfer> listDataISummary = listData.Where(item => item.IsSummary == true).ToList();
            StringBuilder txt = new StringBuilder();
            txt.AppendLine(@"<div style='text-align: center;'><b>DAILY REPORT (" + DateTime.Now.ToString("dd/MM/yyyy") + ")</b></div>");
            if (listDataISummary.Count > 0)
            {
                txt.AppendLine(@"<b style='color: #ed7d31; font-size:14px;'>I - HIGHLIGHT NEWS OF THE DAY</b>");
                txt.AppendLine(@"<br />");
                txt.AppendLine(@"<br />");
                txt.AppendLine(@"<div style='font-size:14px;'>");
                foreach (ProductDataTransfer data in listDataISummary)
                {
                    string title = "<a target='_blank' style='color: blue; cursor:pointer; text-decoration: none;' href='" + data.URLCode + "' title='" + data.URLCode + "'>" + data.Title + "</a></td>";
                    string titleEnglish = "<a target='_blank' style='color: blue; cursor:pointer; text-decoration: none;' href='" + data.URLCode + "' title='" + data.URLCode + "'>" + data.TitleEnglish + "</a></td>";
                    string mediaURLFull = "<a target='_blank' style='color: blue; cursor:pointer; text-decoration: none;' href='" + data.Media + "' title='" + data.Media + "'>" + data.Media + "</a></td>";
                    if (data.IsSummary == true)
                    {
                        txt.AppendLine(@"<b>" + data.CompanyName + ": " + titleEnglish + " (" + mediaURLFull + " - " + data.DatePublishString + ")</b>");
                        txt.AppendLine(@"<br />");
                        txt.AppendLine(@"<b>(" + data.CompanyName + ": " + title + " (" + mediaURLFull + " - " + data.DatePublishString + "))</b>");
                        txt.AppendLine(@"<br />");
                        txt.AppendLine(@"" + data.DescriptionEnglish);
                        txt.AppendLine(@"<br />");
                        txt.AppendLine(@"(" + data.Description + ")");
                        txt.AppendLine(@"<br />");
                        txt.AppendLine(@"<br />");
                    }
                }
                txt.AppendLine(@"</div>");
                txt.AppendLine(@"<b style='color: #ed7d31; font-size:14px;'>II - INFORMATION</b>");
                txt.AppendLine(@"<br />");
                txt.AppendLine(@"<br />");
            }
            txt.AppendLine(@"<table class='border' width='100%' style='font-size:11; font-family: Times New Roman;'>");
            txt.AppendLine(@"<thead>");
            int column = 1;
            txt.AppendLine(@"<th style='color: #ffffff; background-color: #c00000;'>No</th>");
            foreach (Config item in listDailyReportColumn)
            {
                txt.AppendLine(@"<th style='color: #ffffff; background-color: #c00000;'>" + item.CodeName + "</th>");
                column = column + 1;
            }
            txt.AppendLine(@"<thead>");
            txt.AppendLine(@"<tbody>");
            int index = 0;
            for (int row = 4; row <= listData.Count + 3; row++)
            {
                int no = index + 1;
                txt.AppendLine(@"<tr>");
                txt.AppendLine(@"<td style='text-align: center;'>" + no + "</td>");
                for (int i = 1; i < column; i++)
                {
                    if (i == 1)
                    {
                        txt.AppendLine(@"<td style='text-align: right;'>" + listData[index].DatePublishString + "</td>");
                    }
                    if (i == 2)
                    {
                        txt.AppendLine(@"<td style='text-align: left;'>");
                        txt.AppendLine(@"" + listData[index].ArticleTypeName + "");
                        txt.AppendLine(@"<br/>");
                        txt.AppendLine(@"(" + listData[index].ArticleTypeNameVietnamese + ")");
                        txt.AppendLine(@"</td>");
                    }
                    if (i == 3)
                    {
                        txt.AppendLine(@"<td style='text-align: left;'>");
                        txt.AppendLine(@"" + listData[index].SegmentName + "");
                        txt.AppendLine(@"</td>");
                    }
                    if (i == 4)
                    {
                        txt.AppendLine(@"<td style='text-align: left;'></td>");

                    }
                    if (i == 5)
                    {
                        txt.AppendLine(@"<td style='text-align: left;'>");
                        if (!string.IsNullOrEmpty(listData[index].CompanyName))
                        {
                            txt.AppendLine(@"" + listData[index].CompanyName);
                        }
                        else
                        {
                            txt.AppendLine(@"" + listData[index].ArticleTypeName + "");
                            txt.AppendLine(@"<br/>");
                            txt.AppendLine(@"(" + listData[index].ArticleTypeNameVietnamese + ")");
                        }
                        txt.AppendLine(@"</td>");
                    }
                    if (i == 6)
                    {
                        txt.AppendLine(@"<td style='text-align: left;'>" + listData[index].ProductName + "</td>");
                    }
                    if (i == 7)
                    {
                        txt.AppendLine(@"<td style='text-align: left;'>");
                        txt.AppendLine(@"" + listData[index].AssessName + "");
                        txt.AppendLine(@"<br/>");
                        txt.AppendLine(@"(" + listData[index].AssessNameVietnamese + ")");
                        txt.AppendLine(@"</td>");
                    }
                    if (i == 8)
                    {
                        string url = "<a style='color: blue; cursor: pointer; text-decoration: none;' target='_blank' href='" + listData[index].URLCode + "' title='" + listData[index].Title + "'>" + listData[index].Title + "</a>";
                        txt.AppendLine(@"<td style='text-align: left; '><div style='width:170px; word-break: break-all;'>" + url + "</div></td>");
                    }
                    if (i == 9)
                    {
                        string url = "<a style='color: blue; cursor: pointer; text-decoration: none;' target='_blank' href='" + listData[index].URLCode + "' title='" + listData[index].TitleEnglish + "'>" + listData[index].TitleEnglish + "</a>";
                        txt.AppendLine(@"<td style='text-align: left;'><div style='width:170px; word-break: break-all;'>" + url + "</div></td>");
                    }
                    if (i == 10)
                    {
                        txt.AppendLine(@"<td style='text-align: left;'>" + listData[index].Media + "</td>");
                    }
                    if (i == 11)
                    {
                        txt.AppendLine(@"<td style='text-align: left;'>");
                        txt.AppendLine(@"" + listData[index].MediaType + "");
                        txt.AppendLine(@"<br/>");
                        txt.AppendLine(@"(" + listData[index].MediaTypeVietnamese + ")");
                        txt.AppendLine(@"</td>");
                    }
                    if (i == 12)
                    {
                        txt.AppendLine(@"<td style='text-align: right;'>" + listData[index].Page + "</td>");
                    }
                    if (i == 13)
                    {
                        txt.AppendLine(@"<td style='text-align: right;'>" + listData[index].AdvertisementValueString + "</td>");
                    }
                    if (i == 14)
                    {
                        string description = "<a style='color: #000000; cursor: pointer; text-decoration: none;' href='#' title='" + listData[index].Description + "'>" + listData[index].Description + "</a>";
                        string descriptionEnglish = "<a style='color: #000000; cursor: pointer; text-decoration: none;' href='#' title='" + listData[index].DescriptionEnglish + "'>" + listData[index].DescriptionEnglish + "</a>";
                        txt.AppendLine(@"<td style='text-align: left;'><div style='width:100px; word-break: break-all;'>");
                        txt.AppendLine(@"" + description);
                        txt.AppendLine(@"<br/>");
                        txt.AppendLine(@"(" + descriptionEnglish + ")");
                        txt.AppendLine(@"</div></td>");
                    }
                    if (i == 15)
                    {
                        txt.AppendLine(@"<td style='text-align: right;'>" + listData[index].Duration + "</td>");
                    }
                    if (i == 16)
                    {
                        txt.AppendLine(@"<td style='text-align: right;'>" + listData[index].Frequency + "</td>");
                    }
                }
                txt.AppendLine(@"</tr>");
                index = index + 1;
            }
            txt.AppendLine(@"<tbody>");
            txt.AppendLine(@"</table>");
            model.Content = txt.ToString();
            return View(model);
        }
        public IActionResult SendMailReportDailyByProductSearchID(int productSearchID)
        {
            ProductSearchDataTransfer item = _reportRepository.GetByProductSearchID(productSearchID);
            if (item != null)
            {
                ProductSearchDataTransfer model = InitializationReportDailyHTMLSendMail(item, "ReportDaily.html");
                Commsights.Service.Mail.Mail mail = new Service.Mail.Mail();
                mail.Initialization();
                mail.AttachmentFiles = model.PhysicalPath;
                mail.Content = model.Note;
                mail.Subject = "Daily Report (" + item.CompanyName + " - CommSights) " + item.DateSearch.ToString("dd.MM.yyyy");
                mail.ToMail = AppGlobal.EmailSupport;
                try
                {
                    _mailService.Send(mail);
                    _productSearchRepository.UpdateByID(item.ID, RequestUserID, true);
                }
                catch (Exception e)
                {
                    string message = e.Message;
                }
            }
            string note = AppGlobal.Success + " - " + AppGlobal.SendMailSuccess;
            return Json(note);
        }
        public IActionResult SendMailReportDaily(int industryID, DateTime datePublishBegin, DateTime datePublishEnd)
        {
            List<ProductSearchDataTransfer> list = _productSearchRepository.GetByDatePublishBeginAndDatePublishEndAndIndustryIDToList(datePublishBegin, datePublishEnd, industryID);
            foreach (ProductSearchDataTransfer item in list)
            {
                if (item.IsSend != true)
                {
                    ProductSearchDataTransfer model = InitializationReportDailyHTMLSendMail(item, "ReportDaily.html");
                    Commsights.Service.Mail.Mail mail = new Service.Mail.Mail();
                    mail.Initialization();
                    mail.AttachmentFiles = model.PhysicalPath;
                    mail.Content = model.Note;
                    mail.Subject = "Daily Report (" + item.CompanyName + " - CommSights) " + item.DateSearch.ToString("dd.MM.yyyy");
                    mail.ToMail = AppGlobal.EmailSupport;
                    try
                    {
                        _mailService.Send(mail);
                        _productSearchRepository.UpdateByID(item.ID, RequestUserID, true);
                    }
                    catch (Exception e)
                    {
                        string message = e.Message;
                    }
                }
            }
            string note = AppGlobal.Success + " - " + AppGlobal.SendMailSuccess;
            return Json(note);
        }
        public async Task<IActionResult> ExportExcelReportDailyByProductSearchIDAndActive(CancellationToken cancellationToken, int ID)
        {
            await Task.Yield();
            ProductSearchDataTransfer model = _productSearchRepository.GetDataTransferByID(ID);
            List<ProductSearchPropertyDataTransfer> listData = _reportRepository.ReportDailyByProductSearchIDToListToHTML(model.ID);
            List<ProductSearchPropertyDataTransfer> listDataISummary = listData.Where(item => item.IsSummary == true).ToList();
            List<MembershipPermission> listDailyReportColumn = _membershipPermissionRepository.GetDailyReportColumnByMembershipIDAndIndustryIDAndCodeAndIsDailyToList(model.CompanyID.Value, model.IndustryID.Value, AppGlobal.DailyReportColumn, true);
            List<MembershipPermission> listDailyReportSection = _membershipPermissionRepository.GetByMembershipIDAndIndustryIDAndCodeToList(model.CompanyID.Value, model.IndustryID.Value, AppGlobal.DailyReportSection);
            int DailyReportColumnSegmentID = 0;
            int DailyReportColumnSubCatID = 0;
            int DailyReportColumnDatePublishID = 0;
            int DailyReportColumnCategoryID = 0;
            int DailyReportColumnCompanyID = 0;
            int DailyReportColumnSentimentID = 0;
            int DailyReportColumnHeadlineVietnameseID = 0;
            int DailyReportColumnHeadlineEnglishID = 0;
            int DailyReportColumnMediaTitleID = 0;
            int DailyReportColumnMediaTypeID = 0;
            int DailyReportColumnPageID = 0;
            int DailyReportColumnAdvertisementID = 0;
            int DailyReportColumnSummaryID = 0;
            int DailyReportColumnSegmentIDSortOrder = 0;
            int DailyReportColumnSubCatIDSortOrder = 0;
            int DailyReportColumnDatePublishIDSortOrder = 0;
            int DailyReportColumnCategoryIDSortOrder = 0;
            int DailyReportColumnCompanyIDSortOrder = 0;
            int DailyReportColumnSentimentIDSortOrder = 0;
            int DailyReportColumnHeadlineVietnameseIDSortOrder = 0;
            int DailyReportColumnHeadlineEnglishIDSortOrder = 0;
            int DailyReportColumnMediaTitleIDSortOrder = 0;
            int DailyReportColumnMediaTypeIDSortOrder = 0;
            int DailyReportColumnPageIDSortOrder = 0;
            int DailyReportColumnAdvertisementIDSortOrder = 0;
            int DailyReportColumnSummaryIDSortOrder = 0;

            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                foreach (MembershipPermission dailyReportSection in listDailyReportSection)
                {
                    if ((dailyReportSection.CategoryID == AppGlobal.DailyReportSectionDataID) && (dailyReportSection.Active == true))
                    {
                        if (listData.Count > 0)
                        {
                            Color color = Color.FromArgb(int.Parse("#c00000".Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier));
                            Color colorTitle = Color.FromArgb(int.Parse("#ed7d31".Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier));
                            workSheet.Cells[1, 5].Value = "DAILY REPORT (" + model.DateSearch.ToString("dd/MM/yyyy") + ")";
                            workSheet.Cells[1, 5].Style.Font.Bold = true;
                            workSheet.Cells[1, 5].Style.Font.Size = 12;
                            workSheet.Cells[1, 5].Style.Font.Name = "Times New Roman";
                            workSheet.Cells[1, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            workSheet.Cells[1, 5].Style.Font.Color.SetColor(color);
                            int rowExcel = 2;
                            if (listDataISummary.Count > 0)
                            {
                                workSheet.Cells[rowExcel, 1].Value = "I - HIGHLIGHT NEWS OF THE DAY";
                                workSheet.Cells[rowExcel, 1].Style.Font.Bold = true;
                                workSheet.Cells[rowExcel, 1].Style.Font.Size = 12;
                                workSheet.Cells[rowExcel, 1].Style.Font.Name = "Times New Roman";
                                workSheet.Cells[rowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                workSheet.Cells[rowExcel, 1].Style.Font.Color.SetColor(colorTitle);
                                workSheet.Cells[rowExcel, 1, rowExcel, 3].Merge = true;
                                rowExcel = 3;
                                foreach (ProductSearchPropertyDataTransfer data in listDataISummary)
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

                                        workSheet.Cells[rowExcel, 3].Value = "(" + data.Media + " - " + data.DatePublishString + ")";
                                        workSheet.Cells[rowExcel, 3].Style.Font.Bold = true;
                                        workSheet.Cells[rowExcel, 3].Style.Font.Size = 11;
                                        workSheet.Cells[rowExcel, 3].Style.Font.Name = "Times New Roman";
                                        workSheet.Cells[rowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        if ((!string.IsNullOrEmpty(data.Description)) || (!string.IsNullOrEmpty(data.DescriptionEnglish)))
                                        {
                                            rowExcel = rowExcel + 1;
                                            workSheet.Cells[rowExcel, 1].Value = "" + data.Description + "|" + data.DescriptionEnglish;
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
                            int column = 1;
                            foreach (MembershipPermission dailyReportColumn in listDailyReportColumn)
                            {
                                if (dailyReportColumn.IsDaily == true)
                                {
                                    if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                    {
                                        workSheet.Cells[rowExcel, column].Value = dailyReportColumn.Phone;
                                    }
                                    else
                                    {
                                        workSheet.Cells[rowExcel, column].Value = dailyReportColumn.Email;
                                    }
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
                                if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnSegmentID)
                                {
                                    DailyReportColumnSegmentID = dailyReportColumn.ID;
                                    DailyReportColumnSegmentIDSortOrder = dailyReportColumn.SortOrder.Value;
                                }
                                if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnSubCatID)
                                {
                                    DailyReportColumnSubCatID = dailyReportColumn.ID;
                                    DailyReportColumnSubCatIDSortOrder = dailyReportColumn.SortOrder.Value;
                                }
                                if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnDatePublishID)
                                {
                                    DailyReportColumnDatePublishID = dailyReportColumn.ID;
                                    DailyReportColumnDatePublishIDSortOrder = dailyReportColumn.SortOrder.Value;
                                }
                                if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnCategoryID)
                                {
                                    DailyReportColumnCategoryID = dailyReportColumn.ID;
                                    DailyReportColumnCategoryIDSortOrder = dailyReportColumn.SortOrder.Value;
                                }
                                if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnCompanyID)
                                {
                                    DailyReportColumnCompanyID = dailyReportColumn.ID;
                                    DailyReportColumnCompanyIDSortOrder = dailyReportColumn.SortOrder.Value;
                                }
                                if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnSentimentID)
                                {
                                    DailyReportColumnSentimentID = dailyReportColumn.ID;
                                    DailyReportColumnSentimentIDSortOrder = dailyReportColumn.SortOrder.Value;
                                }
                                if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnHeadlineVietnameseID)
                                {
                                    DailyReportColumnHeadlineVietnameseID = dailyReportColumn.ID;
                                    DailyReportColumnHeadlineVietnameseIDSortOrder = dailyReportColumn.SortOrder.Value;
                                }
                                if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnHeadlineEnglishID)
                                {
                                    DailyReportColumnHeadlineEnglishID = dailyReportColumn.ID;
                                    DailyReportColumnHeadlineEnglishIDSortOrder = dailyReportColumn.SortOrder.Value;
                                }
                                if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnMediaTitleID)
                                {
                                    DailyReportColumnMediaTitleID = dailyReportColumn.ID;
                                    DailyReportColumnMediaTitleIDSortOrder = dailyReportColumn.SortOrder.Value;
                                }
                                if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnMediaTypeID)
                                {
                                    DailyReportColumnMediaTypeID = dailyReportColumn.ID;
                                    DailyReportColumnMediaTypeIDSortOrder = dailyReportColumn.SortOrder.Value;
                                }
                                if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnPageID)
                                {
                                    DailyReportColumnPageID = dailyReportColumn.ID;
                                    DailyReportColumnPageIDSortOrder = dailyReportColumn.SortOrder.Value;
                                }
                                if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnAdvertisementID)
                                {
                                    DailyReportColumnAdvertisementID = dailyReportColumn.ID;
                                    DailyReportColumnAdvertisementIDSortOrder = dailyReportColumn.SortOrder.Value;
                                }
                                if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnSummaryID)
                                {
                                    DailyReportColumnSummaryID = dailyReportColumn.ID;
                                    DailyReportColumnSummaryIDSortOrder = dailyReportColumn.SortOrder.Value;
                                }
                            }
                            rowExcel = rowExcel + 1;
                            int index = 0;
                            for (int row = rowExcel; row <= listData.Count + rowExcel - 1; row++)
                            {
                                for (int i = 1; i < 13; i++)
                                {
                                    if ((DailyReportColumnSegmentID > 0) && (DailyReportColumnSegmentIDSortOrder == i))
                                    {
                                        workSheet.Cells[row, i].Value = "";
                                        workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
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
                                    if ((DailyReportColumnSubCatID > 0) && (DailyReportColumnSubCatIDSortOrder == i))
                                    {
                                        workSheet.Cells[row, i].Value = "";
                                        workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
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
                                    if ((DailyReportColumnDatePublishID > 0) && (DailyReportColumnDatePublishIDSortOrder == i))
                                    {
                                        workSheet.Cells[row, i].Value = listData[index].DatePublish;
                                        workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        workSheet.Cells[row, i].Style.Numberformat.Format = "mm/dd/yyyy";
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
                                    if ((DailyReportColumnCategoryID > 0) && (DailyReportColumnCategoryIDSortOrder == i))
                                    {
                                        if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                        {
                                            workSheet.Cells[row, i].Value = listData[index].ArticleTypeNameVietnamese;
                                        }
                                        else
                                        {
                                            workSheet.Cells[row, i].Value = listData[index].ArticleTypeName;
                                        }
                                        workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
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
                                    if ((DailyReportColumnCompanyID > 0) && (DailyReportColumnCompanyIDSortOrder == i))
                                    {

                                        if (!string.IsNullOrEmpty(listData[index].CompanyName))
                                        {
                                            workSheet.Cells[row, i].Value = listData[index].CompanyName;
                                        }
                                        else
                                        {
                                            if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                            {
                                                workSheet.Cells[row, i].Value = listData[index].ArticleTypeNameVietnamese;
                                            }
                                            else
                                            {
                                                workSheet.Cells[row, i].Value = listData[index].ArticleTypeName;
                                            }
                                        }
                                        workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
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
                                    if ((DailyReportColumnSentimentID > 0) && (DailyReportColumnSentimentIDSortOrder == i))
                                    {
                                        if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                        {
                                            workSheet.Cells[row, i].Value = listData[index].AssessNameVietnamese;
                                        }
                                        else
                                        {
                                            workSheet.Cells[row, i].Value = listData[index].AssessName;
                                        }
                                        if (listData[index].AssessID == AppGlobal.NegativeID)
                                        {
                                            workSheet.Cells[row, i].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                                        }
                                        workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
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
                                    if ((DailyReportColumnHeadlineVietnameseID > 0) && (DailyReportColumnHeadlineVietnameseIDSortOrder == i))
                                    {
                                        workSheet.Cells[row, i].Value = listData[index].Title;
                                        if ((!string.IsNullOrEmpty(listData[index].Title)) && (!string.IsNullOrEmpty(listData[index].URLCode)))
                                        {
                                            try
                                            {
                                                workSheet.Cells[row, i].Hyperlink = new Uri(listData[index].URLCode);
                                            }
                                            catch
                                            {
                                            }
                                            workSheet.Cells[row, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                        }
                                        workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
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
                                    if ((DailyReportColumnHeadlineEnglishID > 0) && (DailyReportColumnHeadlineEnglishIDSortOrder == i))
                                    {
                                        workSheet.Cells[row, i].Value = listData[index].TitleEnglish;
                                        if ((!string.IsNullOrEmpty(listData[index].TitleEnglish)) && (!string.IsNullOrEmpty(listData[index].URLCode)))
                                        {
                                            workSheet.Cells[row, i].Hyperlink = new Uri(listData[index].URLCode);
                                            workSheet.Cells[row, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                        }
                                        workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
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
                                    if ((DailyReportColumnMediaTitleID > 0) && (DailyReportColumnMediaTitleIDSortOrder == i))
                                    {
                                        workSheet.Cells[row, i].Value = listData[index].Media;
                                        workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
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
                                    if ((DailyReportColumnMediaTypeID > 0) && (DailyReportColumnMediaTypeIDSortOrder == i))
                                    {
                                        if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                        {
                                            workSheet.Cells[row, i].Value = listData[index].MediaTypeVietnamese;
                                        }
                                        else
                                        {
                                            workSheet.Cells[row, i].Value = listData[index].MediaType;
                                        }
                                        workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
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
                                    if ((DailyReportColumnPageID > 0) && (DailyReportColumnPageIDSortOrder == i))
                                    {
                                        workSheet.Cells[row, i].Value = listData[index].Page;
                                        workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
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
                                    if ((DailyReportColumnAdvertisementID > 0) && (DailyReportColumnAdvertisementIDSortOrder == i))
                                    {
                                        workSheet.Cells[row, i].Value = listData[index].AdvertisementValueString;
                                        workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
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
                                    if ((DailyReportColumnSummaryID > 0) && (DailyReportColumnSummaryIDSortOrder == i))
                                    {
                                        if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                        {
                                            workSheet.Cells[row, i].Value = listData[index].Description;
                                        }
                                        else
                                        {
                                            workSheet.Cells[row, i].Value = listData[index].DescriptionEnglish;
                                        }
                                        workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
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
                                }
                                index = index + 1;
                            }
                            workSheet.Column(1).AutoFit();
                            workSheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            workSheet.Column(2).AutoFit();
                            workSheet.Column(3).AutoFit();
                            workSheet.Column(4).AutoFit();
                            workSheet.Column(5).AutoFit();
                            workSheet.Column(6).AutoFit();
                            workSheet.Column(7).AutoFit();
                            workSheet.Column(8).AutoFit();
                            workSheet.Column(9).AutoFit();
                            workSheet.Column(9).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            workSheet.Column(10).AutoFit();
                        }
                    }
                }
                package.Save();
            }
            stream.Position = 0;
            string excelName = @"ReportDaily_" + AppGlobal.DateTimeCode + ".xlsx";
            model = _productSearchRepository.GetDataTransferByID(ID);
            if (model != null)
            {
                excelName = model.CompanyName + "_" + model.Title + "_" + AppGlobal.DateTimeCode + ".xlsx";
            }
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        public ProductSearchDataTransfer InitializationReportDailyHTMLSendMail(ProductSearchDataTransfer model, string fileName)
        {
            List<ProductSearchPropertyDataTransfer> listData = _reportRepository.ReportDailyByProductSearchIDToListToHTML(model.ID);
            List<ProductSearchPropertyDataTransfer> listDataISummary = listData.Where(item => item.IsSummary == true).ToList();
            List<MembershipPermission> listDailyReportColumn = _membershipPermissionRepository.GetDailyReportColumnByMembershipIDAndIndustryIDAndCodeAndIsDailyToList(model.CompanyID.Value, model.IndustryID.Value, AppGlobal.DailyReportColumn, true);
            List<MembershipPermission> listDailyReportSection = _membershipPermissionRepository.GetByMembershipIDAndIndustryIDAndCodeToList(model.CompanyID.Value, model.IndustryID.Value, AppGlobal.DailyReportSection);
            int DailyReportColumnSegmentID = 0;
            int DailyReportColumnSubCatID = 0;
            int DailyReportColumnDatePublishID = 0;
            int DailyReportColumnCategoryID = 0;
            int DailyReportColumnCompanyID = 0;
            int DailyReportColumnSentimentID = 0;
            int DailyReportColumnHeadlineVietnameseID = 0;
            int DailyReportColumnHeadlineEnglishID = 0;
            int DailyReportColumnMediaTitleID = 0;
            int DailyReportColumnMediaTypeID = 0;
            int DailyReportColumnPageID = 0;
            int DailyReportColumnAdvertisementID = 0;
            int DailyReportColumnSummaryID = 0;
            int DailyReportColumnSegmentIDSortOrder = 0;
            int DailyReportColumnSubCatIDSortOrder = 0;
            int DailyReportColumnDatePublishIDSortOrder = 0;
            int DailyReportColumnCategoryIDSortOrder = 0;
            int DailyReportColumnCompanyIDSortOrder = 0;
            int DailyReportColumnSentimentIDSortOrder = 0;
            int DailyReportColumnHeadlineVietnameseIDSortOrder = 0;
            int DailyReportColumnHeadlineEnglishIDSortOrder = 0;
            int DailyReportColumnMediaTitleIDSortOrder = 0;
            int DailyReportColumnMediaTypeIDSortOrder = 0;
            int DailyReportColumnPageIDSortOrder = 0;
            int DailyReportColumnAdvertisementIDSortOrder = 0;
            int DailyReportColumnSummaryIDSortOrder = 0;

            //HTML
            string html = "";
            var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, AppGlobal.HTML, fileName);
            using (var stream = new FileStream(physicalPath, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }
            }
            if (!string.IsNullOrEmpty(html))
            {
                html = html.Replace(@"[Logo01URLFull]", @"" + AppGlobal.Logo01URLFull);
                html = html.Replace(@"[CompanyTitleEnglish]", @"" + AppGlobal.CompanyTitleEnglish);
                html = html.Replace(@"[WebsiteHTML]", @"" + AppGlobal.WebsiteHTML);
                html = html.Replace(@"[PhoneReportHTML]", @"" + AppGlobal.PhoneReportHTML);
                html = html.Replace(@"[EmailReportHTML]", @"" + AppGlobal.EmailReportHTML);
                html = html.Replace(@"[FacebookHTML]", @"" + AppGlobal.FacebookHTML);
                html = html.Replace(@"[GoogleMapHTML]", @"" + AppGlobal.GoogleMapHTML);
                html = html.Replace(@"[PreviewDate]", @"" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                html = html.Replace(@"[CompanyName]", @"" + model.CompanyName);
                html = html.Replace(@"[Title]", @"DAILY REPORT");
                html = html.Replace(@"[DateSearchString]", @"" + model.DateSearch.ToString("dd/MM/yyyy"));
                StringBuilder reportData = new StringBuilder();
                StringBuilder reportSummary = new StringBuilder();
                foreach (MembershipPermission dailyReportSection in listDailyReportSection)
                {
                    if ((dailyReportSection.CategoryID == AppGlobal.DailyReportSectionSummaryID) && (dailyReportSection.Active == true))
                    {
                        if (listDataISummary.Count > 0)
                        {
                            reportSummary.AppendLine(@"<b style='color: #ed7d31; font-size:14px;'>I - HIGHLIGHT NEWS OF THE DAY</b>");
                            reportSummary.AppendLine(@"<br />");
                            reportSummary.AppendLine(@"<br />");
                            reportSummary.AppendLine(@"<div style='font-size:12px;'>");
                            foreach (ProductSearchPropertyDataTransfer data in listDataISummary)
                            {
                                string title = "<a target='_blank' style='color: blue; cursor:pointer;' href='" + data.URLCode + "' title='" + data.URLCode + "'>" + data.Title + "</a></td>";
                                string titleEnglish = "<a target='_blank' style='color: blue; cursor:pointer;' href='" + data.URLCode + "' title='" + data.URLCode + "'>" + data.TitleEnglish + "</a></td>";
                                string mediaURLFull = "" + data.Media + "";
                                if (data.IsSummary == true)
                                {
                                    if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                    {
                                        reportSummary.AppendLine(@"<b>" + data.CompanyName + ": " + title + " (" + mediaURLFull + " - " + data.DatePublishString + ")</b>");
                                        reportSummary.AppendLine(@"<br />");
                                        reportSummary.AppendLine(@"" + data.Description);
                                    }
                                    else
                                    {
                                        reportSummary.AppendLine(@"<b>" + data.CompanyName + ": " + titleEnglish + " (" + mediaURLFull + " - " + data.DatePublishString + ")</b>");
                                        reportSummary.AppendLine(@"<br />");
                                        reportSummary.AppendLine(@"" + data.DescriptionEnglish);
                                    }
                                    reportSummary.AppendLine(@"<br />");
                                    reportSummary.AppendLine(@"<br />");
                                }
                            }
                            reportSummary.AppendLine(@"</div>");
                        }
                    }
                    if ((dailyReportSection.CategoryID == AppGlobal.DailyReportSectionDataID) && (dailyReportSection.Active == true))
                    {
                        if (listData.Count > 0)
                        {
                            if (listDataISummary.Count > 0)
                            {
                                reportData.AppendLine(@"<b style='color: #ed7d31; font-size:14px;'>II - INFORMATION</b>");
                                reportData.AppendLine(@"<br />");
                                reportData.AppendLine(@"<br />");
                            }
                            reportData.AppendLine(@"<table style='width:1200px; font-size:12px; border-color: #000000; border-style: solid;border-width: 1px; padding: 4px; border-collapse: collapse;'>");
                            reportData.AppendLine(@"<thead>");
                            reportData.AppendLine(@"<tr>");
                            foreach (MembershipPermission dailyReportColumn in listDailyReportColumn)
                            {
                                if (dailyReportColumn.IsDaily == true)
                                {
                                    if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnSegmentID)
                                    {
                                        DailyReportColumnSegmentID = dailyReportColumn.ID;
                                        DailyReportColumnSegmentIDSortOrder = dailyReportColumn.SortOrder.Value;
                                    }
                                    if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnSubCatID)
                                    {
                                        DailyReportColumnSubCatID = dailyReportColumn.ID;
                                        DailyReportColumnSubCatIDSortOrder = dailyReportColumn.SortOrder.Value;
                                    }
                                    if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnDatePublishID)
                                    {
                                        DailyReportColumnDatePublishID = dailyReportColumn.ID;
                                        DailyReportColumnDatePublishIDSortOrder = dailyReportColumn.SortOrder.Value;
                                    }
                                    if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnCategoryID)
                                    {
                                        DailyReportColumnCategoryID = dailyReportColumn.ID;
                                        DailyReportColumnCategoryIDSortOrder = dailyReportColumn.SortOrder.Value;
                                    }
                                    if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnCompanyID)
                                    {
                                        DailyReportColumnCompanyID = dailyReportColumn.ID;
                                        DailyReportColumnCompanyIDSortOrder = dailyReportColumn.SortOrder.Value;
                                    }
                                    if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnSentimentID)
                                    {
                                        DailyReportColumnSentimentID = dailyReportColumn.ID;
                                        DailyReportColumnSentimentIDSortOrder = dailyReportColumn.SortOrder.Value;
                                    }
                                    if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnHeadlineVietnameseID)
                                    {
                                        DailyReportColumnHeadlineVietnameseID = dailyReportColumn.ID;
                                        DailyReportColumnHeadlineVietnameseIDSortOrder = dailyReportColumn.SortOrder.Value;
                                    }
                                    if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnHeadlineEnglishID)
                                    {
                                        DailyReportColumnHeadlineEnglishID = dailyReportColumn.ID;
                                        DailyReportColumnHeadlineEnglishIDSortOrder = dailyReportColumn.SortOrder.Value;
                                    }
                                    if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnMediaTitleID)
                                    {
                                        DailyReportColumnMediaTitleID = dailyReportColumn.ID;
                                        DailyReportColumnMediaTitleIDSortOrder = dailyReportColumn.SortOrder.Value;
                                    }
                                    if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnMediaTypeID)
                                    {
                                        DailyReportColumnMediaTypeID = dailyReportColumn.ID;
                                        DailyReportColumnMediaTypeIDSortOrder = dailyReportColumn.SortOrder.Value;
                                    }
                                    if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnPageID)
                                    {
                                        DailyReportColumnPageID = dailyReportColumn.ID;
                                        DailyReportColumnPageIDSortOrder = dailyReportColumn.SortOrder.Value;
                                    }
                                    if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnAdvertisementID)
                                    {
                                        DailyReportColumnAdvertisementID = dailyReportColumn.ID;
                                        DailyReportColumnAdvertisementIDSortOrder = dailyReportColumn.SortOrder.Value;
                                    }
                                    if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnSummaryID)
                                    {
                                        DailyReportColumnSummaryID = dailyReportColumn.ID;
                                        DailyReportColumnSummaryIDSortOrder = dailyReportColumn.SortOrder.Value;
                                    }
                                    reportData.Append(@"<th style='text-align:center; background-color:#c00000; padding: 4px; border-color: #000000; border-style: solid;border-width: 1px;'>");
                                    if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                    {
                                        reportData.Append(@"" + dailyReportColumn.Phone);
                                    }
                                    else
                                    {
                                        reportData.Append(@"" + dailyReportColumn.Email);
                                    }
                                    reportData.Append(@"</th>");
                                }
                            }
                            reportData.AppendLine(@"</tr>");
                            reportData.AppendLine(@"</thead>");
                            reportData.AppendLine(@"<tbody>");
                            int rowIndex = -1;
                            int rowspan = 1;
                            foreach (ProductSearchPropertyDataTransfer data in listData)
                            {
                                rowIndex = rowIndex + 1;
                                string title = "<a target='_blank' style='color: blue; cursor:pointer;' href='" + data.URLCode + "' title='" + data.URLCode + "'>" + data.Title + "</a></td>";
                                string titleEnglish = "<a target='_blank' style='color: blue; cursor:pointer;' href='" + data.URLCode + "' title='" + data.URLCode + "'>" + data.TitleEnglish + "</a></td>";
                                reportData.AppendLine(@"<tr>");
                                for (int i = 1; i < 12; i++)
                                {
                                    if ((DailyReportColumnSegmentID > 0) && (DailyReportColumnSegmentIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='width: 100px; height:20px; text-align: right; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;'></td>");
                                    }
                                    if ((DailyReportColumnSubCatID > 0) && (DailyReportColumnSubCatIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='width: 100px; height:20px; text-align: right; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;'></td>");
                                    }
                                    if ((DailyReportColumnDatePublishID > 0) && (DailyReportColumnDatePublishIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='width: 100px; height:20px; text-align: right; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;'>" + data.DatePublishStringEnglish + "</td>");
                                    }
                                    if ((DailyReportColumnCategoryID > 0) && (DailyReportColumnCategoryIDSortOrder == i))
                                    {
                                        if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                        {
                                            reportData.AppendLine(@"<td style='width: 100px; height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;'>" + data.ArticleTypeNameVietnamese + "</td>");
                                        }
                                        else
                                        {
                                            reportData.AppendLine(@"<td style='width: 100px; height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;'>" + data.ArticleTypeName + "</td>");
                                        }
                                    }
                                    if ((DailyReportColumnCompanyID > 0) && (DailyReportColumnCompanyIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='width: 100px; height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;'>");
                                        if (!string.IsNullOrEmpty(data.CompanyName))
                                        {
                                            reportData.AppendLine(@"" + data.CompanyName);
                                        }
                                        else
                                        {
                                            if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                            {
                                                reportData.AppendLine(@"" + data.ArticleTypeNameVietnamese);
                                            }
                                            else
                                            {
                                                reportData.AppendLine(@"" + data.ArticleTypeName);
                                            }
                                        }
                                        reportData.AppendLine(@"</td>");
                                        if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                        {
                                            reportData.AppendLine(@"<td style='width: 100px; height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;'>" + data.ArticleTypeNameVietnamese + "</td>");
                                        }
                                        else
                                        {
                                            reportData.AppendLine(@"<td style='width: 100px; height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;'>" + data.ArticleTypeName + "</td>");
                                        }

                                    }
                                    if ((DailyReportColumnSentimentID > 0) && (DailyReportColumnSentimentIDSortOrder == i))
                                    {
                                        reportData.Append(@"<td style='width: 100px; height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;'>");
                                        if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                        {
                                            if (data.AssessID == AppGlobal.NegativeID)
                                            {
                                                reportData.Append(@"<span style='color:red;'>" + data.AssessNameVietnamese + "</span>");
                                            }
                                            else
                                            {
                                                reportData.Append(@"" + data.AssessNameVietnamese);
                                            }
                                        }
                                        else
                                        {
                                            if (data.AssessID == AppGlobal.NegativeID)
                                            {
                                                reportData.Append(@"<span style='color:red;'>" + data.AssessName + "</span>");
                                            }
                                            else
                                            {
                                                reportData.Append(@"" + data.AssessName);
                                            }
                                        }
                                        reportData.Append(@"</td>");
                                    }
                                    if ((DailyReportColumnHeadlineVietnameseID > 0) && (DailyReportColumnHeadlineVietnameseIDSortOrder == i))
                                    {
                                        if (DailyReportColumnSummaryID > 0)
                                        {
                                            reportData.AppendLine(@"<td style='width:300px; height:20px; border-color: #000000; border-style: solid; border-width: 1px; padding: 2px;'>");
                                        }
                                        else
                                        {
                                            reportData.AppendLine(@"<td style='width:300px; height:20px; border-color: #000000; border-style: solid; border-width: 1px; padding: 2px;'>");
                                        }
                                        reportData.AppendLine(@"<p style='word-break: break-word; text-align: left;'>" + title + "</p>");
                                        reportData.AppendLine(@"</td>");
                                    }
                                    if ((DailyReportColumnHeadlineEnglishID > 0) && (DailyReportColumnHeadlineEnglishIDSortOrder == i))
                                    {
                                        if (DailyReportColumnSummaryID > 0)
                                        {
                                            reportData.AppendLine(@"<td style='width:300px; height:20px; border-color: #000000; border-style: solid; border-width: 1px; padding: 2px;'>");
                                        }
                                        else
                                        {
                                            reportData.AppendLine(@"<td style='width:300px; height:20px; border-color: #000000; border-style: solid; border-width: 1px; padding: 2px;'>");
                                        }
                                        reportData.AppendLine(@"<p style='word-break: break-word; text-align: left;'>" + titleEnglish + "</p>");
                                        reportData.AppendLine(@"</td>");
                                    }
                                    if ((DailyReportColumnMediaTitleID > 0) && (DailyReportColumnMediaTitleIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='width: 100px; height:20px;text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;white-space: nowrap;'>" + data.Media + "</td>");
                                    }
                                    if ((DailyReportColumnMediaTypeID > 0) && (DailyReportColumnMediaTypeIDSortOrder == i))
                                    {
                                        if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                        {
                                            reportData.AppendLine(@"<td style='width: 100px; height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;white-space: nowrap;'>" + data.MediaTypeVietnamese + "</td>");
                                        }
                                        else
                                        {
                                            reportData.AppendLine(@"<td style='width: 100px; height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;white-space: nowrap;'>" + data.MediaType + "</td>");
                                        }

                                    }
                                    if ((DailyReportColumnPageID > 0) && (DailyReportColumnPageIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='width: 100px; height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;white-space: nowrap;'>" + data.Page + "</td>");
                                    }
                                    if ((DailyReportColumnAdvertisementID > 0) && (DailyReportColumnAdvertisementIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='width: 100px; height:20px; text-align: right; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;white-space: nowrap;'>" + data.AdvertisementValueString + "</td>");
                                    }
                                    if ((DailyReportColumnSummaryID > 0) && (DailyReportColumnSummaryIDSortOrder == i))
                                    {
                                        string description = "";
                                        if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                        {
                                            description = data.Description;
                                        }
                                        else
                                        {
                                            description = data.DescriptionEnglish;
                                        }
                                        if (rowspan == 1)
                                        {
                                            if (!string.IsNullOrEmpty(description))
                                            {
                                                for (int rowIndex001 = rowIndex + 1; rowIndex001 < listData.Count; rowIndex001++)
                                                {
                                                    if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                                    {
                                                        if (listData[rowIndex001].Description.Equals(description))
                                                        {
                                                            rowspan = rowspan + 1;
                                                        }
                                                        else
                                                        {
                                                            rowIndex001 = listData.Count;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (listData[rowIndex001].DescriptionEnglish.Equals(description))
                                                        {
                                                            rowspan = rowspan + 1;
                                                        }
                                                        else
                                                        {
                                                            rowIndex001 = listData.Count;
                                                        }
                                                    }
                                                }
                                            }
                                            reportData.Append(@"<td rowspan='" + rowspan + "' style='width: 300px; height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;'>");
                                            reportData.Append(@"<p style='text-align: left;'>" + description + "</p>");
                                            reportData.Append(@"</td>");
                                        }
                                        else
                                        {
                                            rowspan = rowspan - 1;
                                        }
                                    }
                                }
                                reportData.AppendLine(@"</tr>");
                            }
                            reportData.AppendLine(@"</tbody>");
                            reportData.AppendLine(@"</table>");
                        }
                    }
                }
                html = html.Replace(@"[ReportSummary]", @"" + reportSummary.ToString());
                html = html.Replace(@"[ReportData]", @"" + reportData.ToString());
            }
            model.Note = html;


            ////Export excel file
            foreach (MembershipPermission dailyReportSection in listDailyReportSection)
            {
                if ((dailyReportSection.CategoryID == AppGlobal.DailyReportSectionDataID) && (dailyReportSection.Active == true))
                {
                    if (listData.Count > 0)
                    {
                        var streamExport = new MemoryStream();
                        using (var package = new ExcelPackage(streamExport))
                        {
                            Color color = Color.FromArgb(int.Parse("#c00000".Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier));
                            var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                            workSheet.Cells[1, 5].Value = "DAILY REPORT (" + model.DateSearch.ToString("dd/MM/yyyy") + ")";
                            workSheet.Cells[1, 5].Style.Font.Bold = true;
                            workSheet.Cells[1, 5].Style.Font.Size = 12;
                            workSheet.Cells[1, 5].Style.Font.Name = "Times New Roman";
                            workSheet.Cells[1, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            int column = 1;
                            foreach (MembershipPermission dailyReportColumn in listDailyReportColumn)
                            {
                                if (dailyReportColumn.Active == true)
                                {
                                    if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                    {
                                        workSheet.Cells[3, column].Value = dailyReportColumn.Phone;
                                    }
                                    else
                                    {
                                        workSheet.Cells[3, column].Value = dailyReportColumn.Email;
                                    }
                                    workSheet.Cells[3, column].Style.Font.Bold = true;
                                    workSheet.Cells[3, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    workSheet.Cells[3, column].Style.Font.Color.SetColor(System.Drawing.Color.White);
                                    workSheet.Cells[3, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    workSheet.Cells[3, column].Style.Fill.BackgroundColor.SetColor(color);
                                    workSheet.Cells[3, column].Style.Font.Name = "Times New Roman";
                                    workSheet.Cells[3, column].Style.Font.Size = 11;
                                    workSheet.Cells[3, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    workSheet.Cells[3, column].Style.Border.Top.Color.SetColor(Color.Black);
                                    workSheet.Cells[3, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    workSheet.Cells[3, column].Style.Border.Left.Color.SetColor(Color.Black);
                                    workSheet.Cells[3, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    workSheet.Cells[3, column].Style.Border.Right.Color.SetColor(Color.Black);
                                    workSheet.Cells[3, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    workSheet.Cells[3, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                                    column = column + 1;
                                }
                            }
                            int index = 0;
                            for (int row = 4; row <= listData.Count + 1; row++)
                            {
                                for (int i = 1; i < 12; i++)
                                {
                                    if ((DailyReportColumnDatePublishID > 0) && (DailyReportColumnDatePublishIDSortOrder == i))
                                    {
                                        workSheet.Cells[row, i].Value = listData[index].DatePublish;
                                        workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        workSheet.Cells[row, i].Style.Numberformat.Format = "mm/dd/yyyy";
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
                                    if ((DailyReportColumnCategoryID > 0) && (DailyReportColumnCategoryIDSortOrder == i))
                                    {
                                        if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                        {
                                            workSheet.Cells[row, i].Value = listData[index].ArticleTypeNameVietnamese;
                                        }
                                        else
                                        {
                                            workSheet.Cells[row, i].Value = listData[index].ArticleTypeName;
                                        }
                                        workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
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
                                    if ((DailyReportColumnCompanyID > 0) && (DailyReportColumnCompanyIDSortOrder == i))
                                    {

                                        if (!string.IsNullOrEmpty(listData[index].CompanyName))
                                        {
                                            workSheet.Cells[row, i].Value = listData[index].CompanyName;
                                        }
                                        else
                                        {
                                            if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                            {
                                                workSheet.Cells[row, i].Value = listData[index].ArticleTypeNameVietnamese;
                                            }
                                            else
                                            {
                                                workSheet.Cells[row, i].Value = listData[index].ArticleTypeName;
                                            }
                                        }
                                        workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
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
                                    if ((DailyReportColumnSentimentID > 0) && (DailyReportColumnSentimentIDSortOrder == i))
                                    {
                                        if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                        {
                                            workSheet.Cells[row, i].Value = listData[index].AssessNameVietnamese;
                                        }
                                        else
                                        {
                                            workSheet.Cells[row, i].Value = listData[index].AssessName;
                                        }
                                        if (listData[index].AssessID == AppGlobal.NegativeID)
                                        {
                                            workSheet.Cells[row, i].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                                        }
                                        workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
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
                                    if ((DailyReportColumnHeadlineVietnameseID > 0) && (DailyReportColumnHeadlineVietnameseIDSortOrder == i))
                                    {
                                        workSheet.Cells[row, i].Value = listData[index].Title;
                                        if ((!string.IsNullOrEmpty(listData[index].Title)) && (!string.IsNullOrEmpty(listData[index].Title)))
                                        {
                                            try
                                            {
                                                workSheet.Cells[row, i].Hyperlink = new Uri(listData[index].URLCode);
                                            }
                                            catch
                                            {
                                            }
                                            workSheet.Cells[row, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                        }
                                        workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
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
                                    if ((DailyReportColumnHeadlineEnglishID > 0) && (DailyReportColumnHeadlineEnglishIDSortOrder == i))
                                    {
                                        workSheet.Cells[row, i].Value = listData[index].TitleEnglish;
                                        if ((!string.IsNullOrEmpty(listData[index].TitleEnglish)) && (!string.IsNullOrEmpty(listData[index].Title)))
                                        {
                                            try
                                            {
                                                workSheet.Cells[row, i].Hyperlink = new Uri(listData[index].URLCode);
                                            }
                                            catch
                                            {
                                            }
                                            workSheet.Cells[row, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                        }
                                        workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
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
                                    if ((DailyReportColumnMediaTitleID > 0) && (DailyReportColumnMediaTitleIDSortOrder == i))
                                    {
                                        workSheet.Cells[row, i].Value = listData[index].Media;
                                        workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
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
                                    if ((DailyReportColumnMediaTypeID > 0) && (DailyReportColumnMediaTypeIDSortOrder == i))
                                    {
                                        if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                        {
                                            workSheet.Cells[row, i].Value = listData[index].MediaTypeVietnamese;
                                        }
                                        else
                                        {
                                            workSheet.Cells[row, i].Value = listData[index].MediaType;
                                        }
                                        workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
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
                                    if ((DailyReportColumnPageID > 0) && (DailyReportColumnPageIDSortOrder == i))
                                    {
                                        workSheet.Cells[row, i].Value = listData[index].Page;
                                        workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
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
                                    if ((DailyReportColumnAdvertisementID > 0) && (DailyReportColumnAdvertisementIDSortOrder == i))
                                    {
                                        workSheet.Cells[row, i].Value = listData[index].AdvertisementValueString;
                                        workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
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
                                    if ((DailyReportColumnSummaryID > 0) && (DailyReportColumnSummaryIDSortOrder == i))
                                    {
                                        if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                        {
                                            workSheet.Cells[row, 10].Value = listData[index].Description;
                                        }
                                        else
                                        {
                                            workSheet.Cells[row, 10].Value = listData[index].DescriptionEnglish;
                                        }
                                        workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
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
                                }
                                index = index + 1;
                            }
                            workSheet.Column(1).AutoFit();
                            workSheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            workSheet.Column(2).AutoFit();
                            workSheet.Column(3).AutoFit();
                            workSheet.Column(4).AutoFit();
                            workSheet.Column(5).AutoFit();
                            workSheet.Column(6).AutoFit();
                            workSheet.Column(7).AutoFit();
                            workSheet.Column(8).AutoFit();
                            workSheet.Column(9).AutoFit();
                            workSheet.Column(9).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            workSheet.Column(10).AutoFit();
                            package.Save();
                        }
                        streamExport.Position = 0;
                        string excelName = @"ReportDaily_" + AppGlobal.DateTimeCode + ".xlsx";
                        if (model != null)
                        {
                            excelName = "Daily Report (" + model.CompanyName + " - CommSights) " + model.DateSearch.ToString("dd.MM.yyyy") + ".xlsx";
                        }
                        var physicalPathCreate = Path.Combine(_hostingEnvironment.WebRootPath, AppGlobal.FTPDownloadReprotDaily, excelName);
                        using (var stream = new FileStream(physicalPathCreate, FileMode.Create))
                        {
                            streamExport.CopyTo(stream);
                        }
                        model.PhysicalPath = physicalPathCreate;
                    }
                }
            }
            return model;
        }
        public ProductSearchDataTransfer InitializationReportDailyHTML(ProductSearchDataTransfer model, string fileName)
        {
            string html = "";
            var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, AppGlobal.HTML, fileName);
            using (var stream = new FileStream(physicalPath, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }
            }
            if (!string.IsNullOrEmpty(html))
            {
                html = html.Replace(@"[Logo01URLFull]", @"" + AppGlobal.Logo01URLFull);
                html = html.Replace(@"[CompanyTitleEnglish]", @"" + AppGlobal.CompanyTitleEnglish);
                html = html.Replace(@"[WebsiteHTML]", @"" + AppGlobal.WebsiteHTML);
                html = html.Replace(@"[PhoneReportHTML]", @"" + AppGlobal.PhoneReportHTML);
                html = html.Replace(@"[EmailReportHTML]", @"" + AppGlobal.EmailReportHTML);
                html = html.Replace(@"[FacebookHTML]", @"" + AppGlobal.FacebookHTML);
                html = html.Replace(@"[GoogleMapHTML]", @"" + AppGlobal.GoogleMapHTML);
                html = html.Replace(@"[PreviewDate]", @"" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                html = html.Replace(@"[CompanyName]", @"" + model.CompanyName);
                html = html.Replace(@"[Title]", @"DAILY REPORT");
                html = html.Replace(@"[DateSearchString]", @"" + model.DateSearch.ToString("dd/MM/yyyy"));
                StringBuilder reportData = new StringBuilder();
                StringBuilder reportSummary = new StringBuilder();
                List<MembershipPermission> listDailyReportSection = _membershipPermissionRepository.GetByMembershipIDAndIndustryIDAndCodeToList(model.CompanyID.Value, model.IndustryID.Value, AppGlobal.DailyReportSection);
                List<ProductSearchPropertyDataTransfer> listData = _reportRepository.ReportDailyByProductSearchIDToListToHTML(model.ID);
                List<ProductSearchPropertyDataTransfer> listDataISummary = listData.Where(item => item.IsSummary == true).ToList();
                foreach (MembershipPermission dailyReportSection in listDailyReportSection)
                {
                    if ((dailyReportSection.CategoryID == AppGlobal.DailyReportSectionSummaryID) && (dailyReportSection.Active == true))
                    {
                        if (listDataISummary.Count > 0)
                        {
                            reportSummary.AppendLine(@"<b style='color: #ed7d31; font-size:14px;'>I - HIGHLIGHT NEWS OF THE DAY</b>");
                            reportSummary.AppendLine(@"<br />");
                            reportSummary.AppendLine(@"<br />");
                            reportSummary.AppendLine(@"<div style='font-size:14px;'>");
                            foreach (ProductSearchPropertyDataTransfer data in listDataISummary)
                            {
                                string title = "<a target='_blank' style='color: blue; cursor:pointer;' href='" + data.URLCode + "' title='" + data.URLCode + "'>" + data.Title + "</a></td>";
                                string titleEnglish = "<a target='_blank' style='color: blue; cursor:pointer;' href='" + data.URLCode + "' title='" + data.URLCode + "'>" + data.TitleEnglish + "</a></td>";
                                string mediaURLFull = "<a target='_blank' style='color: blue; cursor:pointer;' href='" + data.MediaURLFull + "' title='" + data.MediaURLFull + "'>" + data.Media + "</a></td>";
                                if (data.IsSummary == true)
                                {
                                    if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                    {
                                        reportSummary.AppendLine(@"<b>" + data.CompanyName + ": " + title + " (" + mediaURLFull + " - " + data.DatePublishString + ")</b>");
                                        reportSummary.AppendLine(@"<br />");
                                        reportSummary.AppendLine(@"" + data.Description);
                                    }
                                    else
                                    {
                                        reportSummary.AppendLine(@"<b>" + data.CompanyName + ": " + titleEnglish + " (" + mediaURLFull + " - " + data.DatePublishString + ")</b>");
                                        reportSummary.AppendLine(@"<br />");
                                        reportSummary.AppendLine(@"" + data.DescriptionEnglish);
                                    }
                                    reportSummary.AppendLine(@"<br />");
                                    reportSummary.AppendLine(@"<br />");
                                }
                            }
                            reportSummary.AppendLine(@"</div>");
                        }
                    }
                    if ((dailyReportSection.CategoryID == AppGlobal.DailyReportSectionDataID) && (dailyReportSection.Active == true))
                    {
                        if (listData.Count > 0)
                        {
                            int no = 0;
                            int DailyReportColumnSegmentID = 0;
                            int DailyReportColumnSubCatID = 0;
                            int DailyReportColumnDatePublishID = 0;
                            int DailyReportColumnCategoryID = 0;
                            int DailyReportColumnCompanyID = 0;
                            int DailyReportColumnSentimentID = 0;
                            int DailyReportColumnHeadlineVietnameseID = 0;
                            int DailyReportColumnHeadlineEnglishID = 0;
                            int DailyReportColumnMediaTitleID = 0;
                            int DailyReportColumnMediaTypeID = 0;
                            int DailyReportColumnPageID = 0;
                            int DailyReportColumnAdvertisementID = 0;
                            int DailyReportColumnSummaryID = 0;
                            int DailyReportColumnSegmentIDSortOrder = 0;
                            int DailyReportColumnSubCatIDSortOrder = 0;
                            int DailyReportColumnDatePublishIDSortOrder = 0;
                            int DailyReportColumnCategoryIDSortOrder = 0;
                            int DailyReportColumnCompanyIDSortOrder = 0;
                            int DailyReportColumnSentimentIDSortOrder = 0;
                            int DailyReportColumnHeadlineVietnameseIDSortOrder = 0;
                            int DailyReportColumnHeadlineEnglishIDSortOrder = 0;
                            int DailyReportColumnMediaTitleIDSortOrder = 0;
                            int DailyReportColumnMediaTypeIDSortOrder = 0;
                            int DailyReportColumnPageIDSortOrder = 0;
                            int DailyReportColumnAdvertisementIDSortOrder = 0;
                            int DailyReportColumnSummaryIDSortOrder = 0;
                            List<MembershipPermission> listDailyReportColumn = _membershipPermissionRepository.GetDailyReportColumnByMembershipIDAndIndustryIDAndCodeAndIsDailyToList(model.CompanyID.Value, model.IndustryID.Value, AppGlobal.DailyReportColumn, true).OrderBy(item => item.SortOrder).ToList();
                            if (listDataISummary.Count > 0)
                            {
                                reportData.AppendLine(@"<b style='color: #ed7d31; font-size:14px;'>II - INFORMATION</b>");
                                reportData.AppendLine(@"<br />");
                                reportData.AppendLine(@"<br />");
                            }
                            reportData.AppendLine(@"<table style='width:100%; font-size:14px; border-color: #000000; border-style: solid;border-width: 1px; border-collapse: collapse;'>");
                            reportData.AppendLine(@"<thead>");
                            reportData.AppendLine(@"<tr>");
                            reportData.Append(@"<th style='text-align:center; background-color:#c00000; padding: 4px; border-color: #000000; border-style: solid;border-width: 1px; color:#ffffff;'></th>");
                            foreach (MembershipPermission dailyReportColumn in listDailyReportColumn)
                            {
                                if (dailyReportColumn.IsDaily == true)
                                {
                                    if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnSegmentID)
                                    {
                                        DailyReportColumnSegmentID = dailyReportColumn.ID;
                                        DailyReportColumnSegmentIDSortOrder = dailyReportColumn.SortOrder.Value;
                                    }
                                    if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnSubCatID)
                                    {
                                        DailyReportColumnSubCatID = dailyReportColumn.ID;
                                        DailyReportColumnSubCatIDSortOrder = dailyReportColumn.SortOrder.Value;
                                    }
                                    if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnDatePublishID)
                                    {
                                        DailyReportColumnDatePublishID = dailyReportColumn.ID;
                                        DailyReportColumnDatePublishIDSortOrder = dailyReportColumn.SortOrder.Value;
                                    }
                                    if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnCategoryID)
                                    {
                                        DailyReportColumnCategoryID = dailyReportColumn.ID;
                                        DailyReportColumnCategoryIDSortOrder = dailyReportColumn.SortOrder.Value;
                                    }
                                    if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnCompanyID)
                                    {
                                        DailyReportColumnCompanyID = dailyReportColumn.ID;
                                        DailyReportColumnCompanyIDSortOrder = dailyReportColumn.SortOrder.Value;
                                    }
                                    if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnSentimentID)
                                    {
                                        DailyReportColumnSentimentID = dailyReportColumn.ID;
                                        DailyReportColumnSentimentIDSortOrder = dailyReportColumn.SortOrder.Value;
                                    }
                                    if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnHeadlineVietnameseID)
                                    {
                                        DailyReportColumnHeadlineVietnameseID = dailyReportColumn.ID;
                                        DailyReportColumnHeadlineVietnameseIDSortOrder = dailyReportColumn.SortOrder.Value;
                                    }
                                    if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnHeadlineEnglishID)
                                    {
                                        DailyReportColumnHeadlineEnglishID = dailyReportColumn.ID;
                                        DailyReportColumnHeadlineEnglishIDSortOrder = dailyReportColumn.SortOrder.Value;
                                    }
                                    if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnMediaTitleID)
                                    {
                                        DailyReportColumnMediaTitleID = dailyReportColumn.ID;
                                        DailyReportColumnMediaTitleIDSortOrder = dailyReportColumn.SortOrder.Value;
                                    }
                                    if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnMediaTypeID)
                                    {
                                        DailyReportColumnMediaTypeID = dailyReportColumn.ID;
                                        DailyReportColumnMediaTypeIDSortOrder = dailyReportColumn.SortOrder.Value;
                                    }
                                    if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnPageID)
                                    {
                                        DailyReportColumnPageID = dailyReportColumn.ID;
                                        DailyReportColumnPageIDSortOrder = dailyReportColumn.SortOrder.Value;
                                    }
                                    if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnAdvertisementID)
                                    {
                                        DailyReportColumnAdvertisementID = dailyReportColumn.ID;
                                        DailyReportColumnAdvertisementIDSortOrder = dailyReportColumn.SortOrder.Value;
                                    }
                                    if (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnSummaryID)
                                    {
                                        DailyReportColumnSummaryID = dailyReportColumn.ID;
                                        DailyReportColumnSummaryIDSortOrder = dailyReportColumn.SortOrder.Value;
                                    }
                                    if ((DailyReportColumnHeadlineVietnameseID > 0) || (DailyReportColumnHeadlineEnglishID > 0) || (DailyReportColumnSummaryID > 0))
                                    {
                                        reportData.Append(@"<th style='text-align:center; background-color:#c00000; padding: 4px; border-color: #000000; border-style: solid;border-width: 1px; color:#ffffff;'>");
                                    }
                                    else
                                    {
                                        reportData.Append(@"<th style='text-align:center; background-color:#c00000; padding: 4px; border-color: #000000; border-style: solid;border-width: 1px; color:#ffffff;'>");
                                    }
                                    if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                    {
                                        reportData.Append(@"" + dailyReportColumn.Phone);
                                    }
                                    else
                                    {
                                        reportData.Append(@"" + dailyReportColumn.Email);
                                    }
                                    reportData.Append(@"</th>");
                                }
                            }
                            reportData.AppendLine(@"</tr>");
                            reportData.AppendLine(@"</thead>");
                            reportData.AppendLine(@"<tbody>");
                            int rowIndex = -1;
                            int rowspan = 1;
                            foreach (ProductSearchPropertyDataTransfer data in listData)
                            {
                                rowIndex = rowIndex + 1;
                                string title = "<a target='_blank' style='color: blue; cursor:pointer;' href='" + data.URLCode + "' title='" + data.Title + "'>" + data.Title + "</a>";
                                string titleEnglish = "<a target='_blank' style='color: blue; cursor:pointer;' href='" + data.URLCode + "' title='" + data.TitleEnglish + "'>" + data.TitleEnglish + "</a>";
                                no = no + 1;
                                reportData.AppendLine(@"<tr style='background-color:#ffffff;'>");
                                reportData.AppendLine(@"<td style='width: 30px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;'><a onclick='javascript:OpenWindowByURL(""/Report/ViewContent/" + data.ID + @""");' title='Edit' style='color:blue; cursor: pointer;'>Edit</td>");
                                for (int i = 1; i < 13; i++)
                                {
                                    if ((DailyReportColumnSegmentID > 0) && (DailyReportColumnSegmentIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='width: 80px; height:20px; text-align: right; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;'></td>");
                                    }
                                    if ((DailyReportColumnSubCatID > 0) && (DailyReportColumnSubCatIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='width: 80px; height:20px; text-align: right; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;'></td>");
                                    }
                                    if ((DailyReportColumnDatePublishID > 0) && (DailyReportColumnDatePublishIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='width: 80px; height:20px; text-align: right; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;'>" + data.DatePublishStringEnglish + "</td>");
                                    }
                                    if ((DailyReportColumnCategoryID > 0) && (DailyReportColumnCategoryIDSortOrder == i))
                                    {
                                        if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                        {
                                            reportData.AppendLine(@"<td style='width: 80px; height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;'>" + data.ArticleTypeNameVietnamese + "</td>");
                                        }
                                        else
                                        {
                                            reportData.AppendLine(@"<td style='width: 80px; height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;'>" + data.ArticleTypeName + "</td>");
                                        }
                                    }
                                    if ((DailyReportColumnCompanyID > 0) && (DailyReportColumnCompanyIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='width: 80px; height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;'>");
                                        if (!string.IsNullOrEmpty(data.CompanyName))
                                        {
                                            reportData.AppendLine(@"" + data.CompanyName);
                                        }
                                        else
                                        {
                                            if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                            {
                                                reportData.AppendLine(@"" + data.ArticleTypeNameVietnamese);
                                            }
                                            else
                                            {
                                                reportData.AppendLine(@"" + data.ArticleTypeName);
                                            }
                                        }
                                        reportData.AppendLine(@"</td>");
                                    }
                                    if ((DailyReportColumnSentimentID > 0) && (DailyReportColumnSentimentIDSortOrder == i))
                                    {
                                        reportData.Append(@"<td style='width: 80px; height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;'>");
                                        if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                        {
                                            if (data.AssessID == AppGlobal.NegativeID)
                                            {
                                                reportData.Append(@"<span style='color:red;'>" + data.AssessNameVietnamese + "</span>");
                                            }
                                            else
                                            {
                                                reportData.Append(@"" + data.AssessNameVietnamese);
                                            }
                                        }
                                        else
                                        {
                                            if (data.AssessID == AppGlobal.NegativeID)
                                            {
                                                reportData.Append(@"<span style='color:red;'>" + data.AssessName + "</span>");
                                            }
                                            else
                                            {
                                                reportData.Append(@"" + data.AssessName);
                                            }
                                        }

                                        reportData.Append(@"</td>");
                                    }
                                    if ((DailyReportColumnHeadlineVietnameseID > 0) && (DailyReportColumnHeadlineVietnameseIDSortOrder == i))
                                    {
                                        if (DailyReportColumnSummaryID > 0)
                                        {
                                            reportData.AppendLine(@"<td style='width:300px; height:20px; border-color: #000000; border-style: solid; border-width: 1px; padding: 2px;'>");
                                        }
                                        else
                                        {
                                            reportData.AppendLine(@"<td style='width:300px; height:20px; border-color: #000000; border-style: solid; border-width: 1px; padding: 2px;'>");
                                        }
                                        reportData.AppendLine(@"<p style='text-align: left;'>" + title + "</p>");
                                        reportData.AppendLine(@"</td>");
                                    }
                                    if ((DailyReportColumnHeadlineEnglishID > 0) && (DailyReportColumnHeadlineEnglishIDSortOrder == i))
                                    {
                                        if (DailyReportColumnSummaryID > 0)
                                        {
                                            reportData.AppendLine(@"<td style='width:300px; height:20px; border-color: #000000; border-style: solid; border-width: 1px; padding: 2px;'>");
                                        }
                                        else
                                        {
                                            reportData.AppendLine(@"<td style='width:300px; height:20px; border-color: #000000; border-style: solid; border-width: 1px; padding: 2px;'>");
                                        }
                                        reportData.AppendLine(@"<p style='text-align: left;'>" + titleEnglish + "</p>");
                                        reportData.AppendLine(@"</td>");
                                    }
                                    if ((DailyReportColumnMediaTitleID > 0) && (DailyReportColumnMediaTitleIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='width: 80px; height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;'>" + data.Media + "</td>");
                                    }
                                    if ((DailyReportColumnMediaTypeID > 0) && (DailyReportColumnMediaTypeIDSortOrder == i))
                                    {
                                        if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                        {
                                            reportData.AppendLine(@"<td style='width: 80px; height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;'>" + data.MediaTypeVietnamese + "</td>");
                                        }
                                        else
                                        {
                                            reportData.AppendLine(@"<td style='width: 80px; height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;'>" + data.MediaType + "</td>");
                                        }
                                    }
                                    if ((DailyReportColumnPageID > 0) && (DailyReportColumnPageIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='width: 80px; height:20px; text-align: right; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;'>" + data.Page + "</td>");
                                    }
                                    if ((DailyReportColumnAdvertisementID > 0) && (DailyReportColumnAdvertisementIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='width: 80px; height:20px; text-align: right; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;'>" + data.AdvertisementValueString + "</td>");
                                    }
                                    if ((DailyReportColumnSummaryID > 0) && (DailyReportColumnSummaryIDSortOrder == i))
                                    {
                                        string description = "";
                                        if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                        {
                                            description = data.Description;
                                        }
                                        else
                                        {
                                            description = data.DescriptionEnglish;
                                        }
                                        if (rowspan == 1)
                                        {
                                            if (!string.IsNullOrEmpty(description))
                                            {
                                                for (int rowIndex001 = rowIndex + 1; rowIndex001 < listData.Count; rowIndex001++)
                                                {
                                                    if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                                    {
                                                        if (listData[rowIndex001].Description.Equals(description))
                                                        {
                                                            rowspan = rowspan + 1;
                                                        }
                                                        else
                                                        {
                                                            rowIndex001 = listData.Count;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (listData[rowIndex001].DescriptionEnglish.Equals(description))
                                                        {
                                                            rowspan = rowspan + 1;
                                                        }
                                                        else
                                                        {
                                                            rowIndex001 = listData.Count;
                                                        }
                                                    }
                                                }
                                            }
                                            reportData.Append(@"<td rowspan='" + rowspan + "' style='width: 30px; height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;'>");
                                            reportData.Append(@"<p style='text-align: left;'>" + description + "</p>");
                                            reportData.Append(@"</td>");
                                        }
                                        else
                                        {
                                            rowspan = rowspan - 1;
                                        }
                                    }
                                }
                                reportData.AppendLine(@"</tr>");
                            }
                            reportData.AppendLine(@"</tbody>");
                            reportData.AppendLine(@"</table>");
                        }
                    }
                }
                html = html.Replace(@"[ReportSummary]", @"" + reportSummary.ToString());
                html = html.Replace(@"[ReportData]", @"" + reportData.ToString());
            }
            model.Note = html;
            return model;
        }
        public IActionResult ExportExcelReportDailyByDatePublishBeginAndDatePublishEndAndIndustryID(CancellationToken cancellationToken, string datePublishBeginString, string datePublishEndString, int industryID, bool isUpload)
        {
            DateTime datePublishBegin = DateTime.Now;
            DateTime datePublishEnd = DateTime.Now;
            int day = 0;
            int month = 0;
            int year = 0;
            if (!string.IsNullOrEmpty(datePublishBeginString))
            {
                try
                {
                    day = int.Parse(datePublishBeginString.Split('-')[2]);
                    month = int.Parse(datePublishBeginString.Split('-')[1]);
                    year = int.Parse(datePublishBeginString.Split('-')[0]);
                    datePublishBegin = new DateTime(year, month, day);
                }
                catch
                {
                }
            }
            if (!string.IsNullOrEmpty(datePublishEndString))
            {
                try
                {
                    day = int.Parse(datePublishEndString.Split('-')[2]);
                    month = int.Parse(datePublishEndString.Split('-')[1]);
                    year = int.Parse(datePublishEndString.Split('-')[0]);
                    datePublishEnd = new DateTime(year, month, day);
                }
                catch
                {
                }
            }
            string excelName = @"ReportDaily_" + AppGlobal.DateTimeCode + ".xlsx";
            var stream = new MemoryStream();
            Color color = Color.FromArgb(int.Parse("#c00000".Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier));
            Color colorTitle = Color.FromArgb(int.Parse("#ed7d31".Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier));
            List<Config> listDailyReportColumn = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.DailyReportColumn);
            List<ProductDataTransfer> listData = _reportRepository.GetProductDataTransferByDatePublishBeginAndDatePublishEndAndIndustryIDAndIsDailyAndIsUploadToList(datePublishBegin, datePublishEnd, industryID, true, isUpload);
            List<ProductDataTransfer> listDataISummary = listData.Where(item => item.IsSummary == true).ToList();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells[1, 5].Value = "DAILY REPORT (" + DateTime.Now.ToString("dd/MM/yyyy") + ")";
                workSheet.Cells[1, 5].Style.Font.Bold = true;
                workSheet.Cells[1, 5].Style.Font.Size = 12;
                workSheet.Cells[1, 5].Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 5].Style.Font.Color.SetColor(color);
                int rowExcel = 2;
                if (listDataISummary.Count > 0)
                {
                    workSheet.Cells[rowExcel, 1].Value = "I - HIGHLIGHT NEWS OF THE DAY";
                    workSheet.Cells[rowExcel, 1].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, 1].Style.Font.Size = 12;
                    workSheet.Cells[rowExcel, 1].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    workSheet.Cells[rowExcel, 1].Style.Font.Color.SetColor(colorTitle);
                    workSheet.Cells[rowExcel, 1, rowExcel, 3].Merge = true;
                    rowExcel = 3;
                    foreach (ProductDataTransfer data in listDataISummary)
                    {
                        if (data.IsSummary == true)
                        {
                            workSheet.Cells[rowExcel, 1].Value = "" + data.CompanyName + ": ";
                            workSheet.Cells[rowExcel, 1].Style.Font.Bold = true;
                            workSheet.Cells[rowExcel, 1].Style.Font.Size = 11;
                            workSheet.Cells[rowExcel, 1].Style.Font.Name = "Times New Roman";
                            workSheet.Cells[rowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                            if (!string.IsNullOrEmpty(data.TitleEnglish))
                            {
                                workSheet.Cells[rowExcel, 2].Value = "" + data.TitleEnglish;
                                workSheet.Cells[rowExcel, 2].Hyperlink = new Uri(data.URLCode);
                                workSheet.Cells[rowExcel, 2].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                            }
                            workSheet.Cells[rowExcel, 2].Style.Font.Bold = true;
                            workSheet.Cells[rowExcel, 2].Style.Font.Size = 11;
                            workSheet.Cells[rowExcel, 2].Style.Font.Name = "Times New Roman";
                            workSheet.Cells[rowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                            workSheet.Cells[rowExcel, 3].Value = "(" + data.Media + " - " + data.DatePublishString + ")";
                            workSheet.Cells[rowExcel, 3].Style.Font.Bold = true;
                            workSheet.Cells[rowExcel, 3].Style.Font.Size = 11;
                            workSheet.Cells[rowExcel, 3].Style.Font.Name = "Times New Roman";
                            workSheet.Cells[rowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            if (!string.IsNullOrEmpty(data.DescriptionEnglish))
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
                int column = 1;
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
                for (int row = rowExcel; row <= listData.Count + rowExcel - 1; row++)
                {
                    for (int i = 1; i <= column; i++)
                    {
                        if (i == 1)
                        {
                            workSheet.Cells[row, i].Value = listData[index].DatePublish;
                            workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            workSheet.Cells[row, i].Style.Numberformat.Format = "mm/dd/yyyy";
                        }
                        if (i == 2)
                        {
                            workSheet.Cells[row, i].Value = listData[index].ArticleTypeName;
                            workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        }
                        if (i == 3)
                        {
                            workSheet.Cells[row, i].Value = listData[index].SegmentName;
                            workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        }
                        if (i == 4)
                        {
                            workSheet.Cells[row, i].Value = "";
                        }
                        if (i == 5)
                        {
                            if (!string.IsNullOrEmpty(listData[index].CompanyName))
                            {
                                workSheet.Cells[row, i].Value = listData[index].CompanyName;
                            }
                            else
                            {
                                workSheet.Cells[row, i].Value = listData[index].ArticleTypeName;
                            }
                            workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        }
                        if (i == 6)
                        {
                            workSheet.Cells[row, i].Value = listData[index].ProductName;
                            workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        }
                        if (i == 7)
                        {
                            workSheet.Cells[row, i].Value = listData[index].AssessName;
                            workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        }
                        if (i == 8)
                        {
                            workSheet.Cells[row, i].Value = listData[index].Title;
                            if ((!string.IsNullOrEmpty(listData[index].Title)) && (!string.IsNullOrEmpty(listData[index].URLCode)))
                            {
                                try
                                {
                                    workSheet.Cells[row, i].Hyperlink = new Uri(listData[index].URLCode);
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
                            workSheet.Cells[row, i].Value = listData[index].TitleEnglish;
                            if ((!string.IsNullOrEmpty(listData[index].TitleEnglish)) && (!string.IsNullOrEmpty(listData[index].URLCode)))
                            {
                                try
                                {
                                    workSheet.Cells[row, i].Hyperlink = new Uri(listData[index].URLCode);
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
                            workSheet.Cells[row, i].Value = listData[index].Media;
                            workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        }
                        if (i == 11)
                        {
                            workSheet.Cells[row, i].Value = listData[index].MediaType;
                            workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        }
                        if (i == 12)
                        {
                            workSheet.Cells[row, i].Value = listData[index].Page;
                            workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        }
                        if (i == 13)
                        {
                            workSheet.Cells[row, i].Value = listData[index].AdvertisementValueString;
                            workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        }
                        if (i == 14)
                        {
                            workSheet.Cells[row, i].Value = listData[index].DescriptionEnglish;
                            workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        }
                        if (i == 15)
                        {
                            workSheet.Cells[row, i].Value = listData[index].Duration;
                            workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        }
                        if (i == 16)
                        {
                            workSheet.Cells[row, i].Value = listData[index].Frequency;
                            workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        }
                        if (i == 17)
                        {
                            workSheet.Cells[row, i].Value = listData[index].DateUpdated;
                            workSheet.Cells[row, i].Style.Numberformat.Format = "mm/dd/yyyy HH:mm:ss";
                            workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        }
                        if (i == 18)
                        {
                            if (!string.IsNullOrEmpty(listData[index].URLCode))
                            {
                                try
                                {
                                    workSheet.Cells[row, column].Value = listData[index].URLCode;
                                    workSheet.Cells[row, column].Hyperlink = new Uri(listData[index].URLCode);
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
                package.Save();
            }
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        public IActionResult ExportExcelReportDailyByDatePublishBeginAndDatePublishEndAndIndustryIDWithVietnamese(CancellationToken cancellationToken, string datePublishBeginString, string datePublishEndString, int industryID, bool isUpload)
        {
            DateTime datePublishBegin = DateTime.Now;
            DateTime datePublishEnd = DateTime.Now;
            int day = 0;
            int month = 0;
            int year = 0;
            if (!string.IsNullOrEmpty(datePublishBeginString))
            {
                try
                {
                    day = int.Parse(datePublishBeginString.Split('-')[2]);
                    month = int.Parse(datePublishBeginString.Split('-')[1]);
                    year = int.Parse(datePublishBeginString.Split('-')[0]);
                    datePublishBegin = new DateTime(year, month, day);
                }
                catch
                {
                }
            }
            if (!string.IsNullOrEmpty(datePublishEndString))
            {
                try
                {
                    day = int.Parse(datePublishEndString.Split('-')[2]);
                    month = int.Parse(datePublishEndString.Split('-')[1]);
                    year = int.Parse(datePublishEndString.Split('-')[0]);
                    datePublishEnd = new DateTime(year, month, day);
                }
                catch
                {
                }
            }
            string excelName = @"ReportDaily_" + AppGlobal.DateTimeCode + ".xlsx";
            var stream = new MemoryStream();
            Color color = Color.FromArgb(int.Parse("#c00000".Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier));
            Color colorTitle = Color.FromArgb(int.Parse("#ed7d31".Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier));
            List<Config> listDailyReportColumn = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.DailyReportColumn);
            List<ProductDataTransfer> listData = _reportRepository.GetProductDataTransferByDatePublishBeginAndDatePublishEndAndIndustryIDAndIsDailyAndIsUploadToList(datePublishBegin, datePublishEnd, industryID, true, isUpload);
            List<ProductDataTransfer> listDataISummary = listData.Where(item => item.IsSummary == true).ToList();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells[1, 5].Value = "DAILY REPORT (" + DateTime.Now.ToString("dd/MM/yyyy") + ")";
                workSheet.Cells[1, 5].Style.Font.Bold = true;
                workSheet.Cells[1, 5].Style.Font.Size = 12;
                workSheet.Cells[1, 5].Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 5].Style.Font.Color.SetColor(color);
                int rowExcel = 2;
                if (listDataISummary.Count > 0)
                {
                    workSheet.Cells[rowExcel, 1].Value = "I - HIGHLIGHT NEWS OF THE DAY";
                    workSheet.Cells[rowExcel, 1].Style.Font.Bold = true;
                    workSheet.Cells[rowExcel, 1].Style.Font.Size = 12;
                    workSheet.Cells[rowExcel, 1].Style.Font.Name = "Times New Roman";
                    workSheet.Cells[rowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    workSheet.Cells[rowExcel, 1].Style.Font.Color.SetColor(colorTitle);
                    workSheet.Cells[rowExcel, 1, rowExcel, 3].Merge = true;
                    rowExcel = 3;
                    foreach (ProductDataTransfer data in listDataISummary)
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

                            workSheet.Cells[rowExcel, 3].Value = "(" + data.Media + " - " + data.DatePublishString + ")";
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
                int column = 1;
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
                for (int row = rowExcel; row <= listData.Count + rowExcel - 1; row++)
                {
                    for (int i = 1; i <= column; i++)
                    {
                        if (i == 1)
                        {
                            workSheet.Cells[row, i].Value = listData[index].DatePublish;
                            workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            workSheet.Cells[row, i].Style.Numberformat.Format = "mm/dd/yyyy";
                        }
                        if (i == 2)
                        {
                            workSheet.Cells[row, i].Value = listData[index].ArticleTypeNameVietnamese;
                            workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        }
                        if (i == 3)
                        {
                            workSheet.Cells[row, i].Value = listData[index].SegmentName;
                            workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        }
                        if (i == 4)
                        {
                            workSheet.Cells[row, i].Value = "";
                        }
                        if (i == 5)
                        {
                            if (!string.IsNullOrEmpty(listData[index].CompanyName))
                            {
                                workSheet.Cells[row, i].Value = listData[index].CompanyName;
                            }
                            else
                            {
                                workSheet.Cells[row, i].Value = listData[index].ArticleTypeNameVietnamese;
                            }
                            workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        }
                        if (i == 6)
                        {
                            workSheet.Cells[row, i].Value = listData[index].ProductName;
                            workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        }
                        if (i == 7)
                        {
                            if (listData[index].AssessID != null)
                            {
                                workSheet.Cells[row, i].Value = _configResposistory.GetByID(listData[index].AssessID.Value).CodeName;
                            }
                            else
                            {
                                workSheet.Cells[row, i].Value = listData[index].AssessName;
                            }
                            workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        }
                        if (i == 8)
                        {
                            workSheet.Cells[row, i].Value = listData[index].Title;
                            if ((!string.IsNullOrEmpty(listData[index].Title)) && (!string.IsNullOrEmpty(listData[index].URLCode)))
                            {
                                try
                                {
                                    workSheet.Cells[row, i].Hyperlink = new Uri(listData[index].URLCode);
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
                            workSheet.Cells[row, i].Value = listData[index].TitleEnglish;
                            if ((!string.IsNullOrEmpty(listData[index].TitleEnglish)) && (!string.IsNullOrEmpty(listData[index].URLCode)))
                            {
                                try
                                {
                                    workSheet.Cells[row, i].Hyperlink = new Uri(listData[index].URLCode);
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
                            workSheet.Cells[row, i].Value = listData[index].Media;
                            workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        }
                        if (i == 11)
                        {
                            workSheet.Cells[row, i].Value = listData[index].MediaTypeVietnamese;
                            workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        }
                        if (i == 12)
                        {
                            workSheet.Cells[row, i].Value = listData[index].Page;
                            workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        }
                        if (i == 13)
                        {
                            workSheet.Cells[row, i].Value = listData[index].AdvertisementValueString;
                            workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        }
                        if (i == 14)
                        {
                            workSheet.Cells[row, i].Value = listData[index].Description;
                            workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        }
                        if (i == 15)
                        {
                            workSheet.Cells[row, i].Value = listData[index].Duration;
                            workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        }
                        if (i == 16)
                        {
                            workSheet.Cells[row, i].Value = listData[index].Frequency;
                            workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        }
                        if (i == 17)
                        {
                            workSheet.Cells[row, i].Value = listData[index].DateUpdated;
                            workSheet.Cells[row, i].Style.Numberformat.Format = "mm/dd/yyyy HH:mm:ss";
                            workSheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        }
                        if (i == 18)
                        {
                            if (!string.IsNullOrEmpty(listData[index].URLCode))
                            {
                                try
                                {
                                    workSheet.Cells[row, column].Value = listData[index].URLCode;
                                    workSheet.Cells[row, column].Hyperlink = new Uri(listData[index].URLCode);
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
                package.Save();
            }
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        public IActionResult Daily()
        {
            BaseViewModel model = new BaseViewModel();
            model.DatePublish = DateTime.Now;
            return View(model);
        }
        public IActionResult Daily02(int ID)
        {
            ProductSearchDataTransfer model = new ProductSearchDataTransfer();
            if (ID > 0)
            {
                model = _productSearchRepository.GetDataTransferByID(ID);
                _reportRepository.InitializationByProductSearchIDAndRequestUserID(ID, RequestUserID);
            }
            model.IsCompanyAll = false;
            model.IsProductAll = false;
            model.IsIndustryAll = false;
            model.IsCompetitorAll = false;
            return View(model);
        }
        public IActionResult Daily03(int ID)
        {
            ProductSearchDataTransfer model = new ProductSearchDataTransfer();
            if (ID > 0)
            {
                model = _productSearchRepository.GetDataTransferByID(ID);
            }
            return View(model);
        }
        [AcceptVerbs("Post")]
        public IActionResult Save02(ProductSearchDataTransfer model)
        {
            if (model.ID > 0)
            {
                model.DateSearch = _productSearchRepository.GetDataTransferByID(model.ID).DateSearch;
                if (model.IsAll == true)
                {
                    _productSearchPropertyRepository.UpdateByProductSearchIDAndRequestUserID(model.ID, RequestUserID);
                }
            }
            return RedirectToAction("Daily03", new { ID = model.ID });
        }
        [AcceptVerbs("Post")]
        public IActionResult Save03(ProductSearchDataTransfer model)
        {
            Initialization(model);
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _productSearchRepository.Update(model.ID, model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.EditFail;
            }
            return RedirectToAction("Daily03", new { ID = model.ID });
        }
        public ActionResult GetProductDataTransferByDatePublishBeginAndDatePublishEndAndIndustryIDToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, int industryID)
        {
            var data = _reportRepository.GetProductDataTransferByDatePublishBeginAndDatePublishEndAndIndustryIDToList(datePublishBegin, datePublishEnd, industryID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetProductDataTransferByDatePublishBeginAndDatePublishEndAndIndustryIDAndIsDailyToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, int industryID)
        {
            var data = _reportRepository.GetProductDataTransferByDatePublishBeginAndDatePublishEndAndIndustryIDAndIsDailyToList(datePublishBegin, datePublishEnd, industryID, true);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetProductDataTransferByDatePublishBeginAndDatePublishEndAndIndustryIDAndIsDailyAndIsUploadToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, int industryID, bool isUpload)
        {
            _productRepository.Initialization();
            var data = _reportRepository.GetProductDataTransferByDatePublishBeginAndDatePublishEndAndIndustryIDAndIsDailyAndIsUploadToList(datePublishBegin, datePublishEnd, industryID, true, isUpload);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult InitializationByDatePublishBeginAndDatePublishEndAndIndustryIDToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, int industryID)
        {
            var data = _reportRepository.InitializationByDatePublishBeginAndDatePublishEndAndIndustryIDToList(datePublishBegin, datePublishEnd, industryID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult InitializationByDatePublishBeginAndDatePublishEndAndIndustryIDAndHourSearchToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, int industryID)
        {
            var data = _reportRepository.InitializationByDatePublishBeginAndDatePublishEndAndIndustryIDAndHourSearchToList(datePublishBegin, datePublishEnd, industryID, DateTime.Now.Hour);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult InitializationByDatePublishBeginAndDatePublishEndAndIndustryIDAndAllDataAndAllSummaryToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, int industryID, bool allData, bool allSummary)
        {
            var data = _reportRepository.InitializationByDatePublishBeginAndDatePublishEndAndIndustryIDAndAllDataAndAllSummaryToList(datePublishBegin, datePublishEnd, industryID, allData, allSummary, RequestUserID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult InitializationByDatePublishToList([DataSourceRequest] DataSourceRequest request, DateTime datePublish)
        {
            var data = _reportRepository.InitializationByDatePublishToList(datePublish);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult ReportDailyByDatePublishAndCompanyIDToList([DataSourceRequest] DataSourceRequest request, DateTime datePublish, int companyID)
        {
            var data = _reportRepository.ReportDailyByDatePublishAndCompanyIDToList(datePublish, companyID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult ReportDailyProductByDatePublishAndCompanyIDToList([DataSourceRequest] DataSourceRequest request, DateTime datePublish, int companyID)
        {
            var data = _reportRepository.ReportDailyProductByDatePublishAndCompanyIDToList(datePublish, companyID);
            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult ReportDailyIndustryByDatePublishAndCompanyIDToList([DataSourceRequest] DataSourceRequest request, DateTime datePublish, int companyID)
        {
            var data = _reportRepository.ReportDailyIndustryByDatePublishAndCompanyIDToList(datePublish, companyID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult ReportDailyCompetitorByDatePublishAndCompanyIDToList([DataSourceRequest] DataSourceRequest request, DateTime datePublish, int companyID)
        {
            var data = _reportRepository.ReportDailyCompetitorByDatePublishAndCompanyIDToList(datePublish, companyID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult ReportDaily02ByProductSearchIDToList([DataSourceRequest] DataSourceRequest request, int productSearchID)
        {
            var data = _reportRepository.ReportDaily02ByProductSearchIDToList(productSearchID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult ReportDaily02ByProductSearchIDAndActiveToList([DataSourceRequest] DataSourceRequest request, int productSearchID)
        {
            var data = _reportRepository.ReportDaily02ByProductSearchIDAndActiveToList(productSearchID, true);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult ReportDaily02ByProductSearchIDAndActiveToListJSON(int productSearchID)
        {
            List<ProductSearchPropertyDataTransfer> listProductSearchPropertyDataTransfer = _reportRepository.ReportDaily02ByProductSearchIDAndActiveToList(productSearchID, true);
            return Json(listProductSearchPropertyDataTransfer);
        }
        public ActionResult GetDataTransferByDatePublishBeginAndDatePublishEndAndIndustryIDToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, int industryID)
        {
            var data = _reportRepository.GetDataTransferByDatePublishBeginAndDatePublishEndAndIndustryIDToList(datePublishBegin, datePublishEnd, industryID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult ReportDailyByProductSearchIDAndActiveToListToHTML([DataSourceRequest] DataSourceRequest request, int productSearchID)
        {
            var data = _reportRepository.ReportDailyByProductSearchIDAndActiveToListToHTML(productSearchID, true);
            return Json(data.ToDataSourceResult(request));
        }
        public IActionResult UpdateItem(ProductDataTransfer model)
        {
            ProductProperty productProperty = _productPropertyRepository.GetByID001(model.ID);
            if (productProperty != null)
            {
                productProperty.IsSummary = model.IsSummary;
                productProperty.CompanyID = model.CompanyID;
                productProperty.SegmentID = model.SegmentID;
                productProperty.ArticleTypeID = model.ArticleTypeID;
                productProperty.AssessID = model.AssessID;
                productProperty.ProductID = model.ProductID;
                productProperty.Initialization(InitType.Update, RequestUserID);
                _productPropertyRepository.Update(productProperty.ID, productProperty);
            }
            Product product = _productRepository.GetByID001(model.ProductID.Value);
            if (product != null)
            {
                product.IsSummary = model.IsSummary;
                product.Title = model.Title;
                product.TitleEnglish = model.TitleEnglish;
                product.Description = model.Description;
                product.DescriptionEnglish = model.DescriptionEnglish;
                product.Initialization(InitType.Update, RequestUserID);
                _productRepository.Update(product.ID, product);
                foreach (Product item in _productRepository.GetByTitleToList(product.Title))
                {
                    item.TitleEnglish = product.TitleEnglish;
                    item.Description = product.Description;
                    item.DescriptionEnglish = product.DescriptionEnglish;
                    item.Initialization(InitType.Update, RequestUserID);
                    _productRepository.Update(item.ID, item);
                }
                Config media = _configResposistory.GetByID(product.ParentID.Value);
                if (media != null)
                {
                    if (media.Color != model.AdvertisementValue)
                    {
                        media.Color = model.AdvertisementValue;
                        media.Initialization(InitType.Update, RequestUserID);
                        _configResposistory.Update(media.ID, media);
                    }
                }
            }
            _reportRepository.UpdateByCompanyIDAndTitleAndProductPropertyIDAndRequestUserID(model.CompanyID.Value, model.Title, model.ID, RequestUserID);
            string note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            return Json(note);
        }
        public IActionResult UpdateDataTransfer(ProductDataTransfer model, DateTime datePublishBegin, DateTime datePublishEnd, int industryID)
        {
            Initialization(model);
            ProductProperty productProperty = _productPropertyRepository.GetByID001(model.ID);
            if (productProperty != null)
            {
                productProperty.IsSummary = model.IsSummary;
                productProperty.IsData = model.IsData;
                productProperty.CompanyID = model.Company.ID;
                productProperty.SegmentID = model.Segment.ID;
                productProperty.ArticleTypeID = model.ArticleType.ID;
                productProperty.AssessID = model.AssessType.ID;
                productProperty.ProductID = model.Product.ID;

                productProperty.SentimentCorpID = model.AssessID;
                if (model.Company.ID == 0)
                {
                    productProperty.CompanyID = null;
                    productProperty.ArticleTypeID = AppGlobal.TinNganhID;
                }
                if (productProperty.ProductID > 0)
                {
                    MembershipPermission membershipPermission = _membershipPermissionRepository.GetByID(productProperty.ProductID.Value);
                    productProperty.CompanyID = membershipPermission.MembershipID;
                }
                productProperty.Initialization(InitType.Update, RequestUserID);
                _productPropertyRepository.Update(productProperty.ID, productProperty);
            }
            Product product = _productRepository.GetByID001(model.ProductID.Value);
            if (product != null)
            {
                product.IsSummary = model.IsSummary;
                product.IsData = model.IsData;
                product.Title = model.Title;
                product.TitleEnglish = model.TitleEnglish;
                product.Description = model.Description;
                product.DescriptionEnglish = model.DescriptionEnglish;
                product.SourceID = model.SourceID;
                product.TargetID = model.TargetID;
                if (model.TargetID > 0)
                {
                    try
                    {
                        Product productSource = _productRepository.GetByByDatePublishBeginAndDatePublishEndAndIndustryIDAndSourceID(datePublishBegin, datePublishEnd, industryID, model.TargetID.Value);
                        if (productSource != null)
                        {
                            product.Description = productSource.Description;
                            product.DescriptionEnglish = productSource.DescriptionEnglish;
                        }
                    }
                    catch (Exception e)
                    {
                        string message = e.Message;
                    }
                }
                product.Initialization(InitType.Update, RequestUserID);
                _productRepository.Update(product.ID, product);
                List<Product> listProduct = _productRepository.GetByTitleToList(product.Title);
                foreach (Product item in listProduct)
                {
                    item.TitleEnglish = product.TitleEnglish;
                    item.Description = product.Description;
                    item.DescriptionEnglish = product.DescriptionEnglish;
                    item.Initialization(InitType.Update, RequestUserID);
                    _productRepository.Update(item.ID, item);
                }
                switch (industryID)
                {
                    case 1448:
                    case 3470:
                    case 3741:
                    case 3251:
                        foreach (Product item in listProduct)
                        {

                            if (_productPropertyRepository.GetByParentIDAndCompanyIDAndArticleTypeIDToList(item.ID, productProperty.CompanyID.Value, productProperty.ArticleTypeID.Value).Count > 0)
                            {

                            }
                            else
                            {
                                ProductProperty productProperty001 = productProperty;
                                productProperty001.ID = 0;
                                productProperty001.ParentID = item.ID;
                                productProperty001.Initialization(InitType.Insert, RequestUserID);
                                _productPropertyRepository.Create(productProperty001);
                            }
                        }
                        break;
                }
                Config media = _configResposistory.GetByID(product.ParentID.Value);
                if (media != null)
                {
                    if (media.Color != model.AdvertisementValue)
                    {
                        media.Color = model.AdvertisementValue;
                        media.Initialization(InitType.Update, RequestUserID);
                        _configResposistory.Update(media.ID, media);
                    }
                }
            }
            if (model.CompanyID != null)
            {
                _reportRepository.UpdateByCompanyIDAndTitleAndProductPropertyIDAndRequestUserID(model.CompanyID.Value, model.Title, model.ID, RequestUserID);
            }

            string note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            return Json(note);
        }
        public IActionResult CopyProductProperty(ProductDataTransfer model)
        {
            ProductProperty productProperty = _productPropertyRepository.GetByID001(model.ID);
            if (productProperty != null)
            {
                productProperty.ID = 0;
                productProperty.Initialization(InitType.Insert, RequestUserID);
                _productPropertyRepository.Create(productProperty);
            }
            string note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            return Json(note);
        }
        public IActionResult CopyProductPropertyByID(int ID)
        {
            _productPropertyRepository.InsertItemByID(ID);
            string note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            return Json(note);
        }
        public IActionResult DeleteProductAndProductProperty(ProductDataTransfer model)
        {
            int result = _productPropertyRepository.Delete(model.ID);
            //result = result + _productRepository.Delete(model.ProductID.Value);
            string note = AppGlobal.Success + " - " + AppGlobal.DeleteSuccess;
            return Json(note);
        }
        public IActionResult DeleteByDatePublishBeginAndDatePublishEndAndIndustryID(DateTime datePublishBegin, DateTime datePublishEnd, int industryID)
        {
            _reportRepository.DeleteProductAndProductPropertyByDatePublishBeginAndDatePublishEndAndIndustryID(datePublishBegin, datePublishEnd, industryID);
            string note = AppGlobal.Success + " - " + AppGlobal.DeleteSuccess;
            return Json(note);
        }
        public IActionResult DeleteByDatePublishBeginAndDatePublishEndAndIndustryIDAndIsUpload(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, bool isUpload)
        {
            _reportRepository.DeleteProductAndProductPropertyByDatePublishBeginAndDatePublishEndAndIndustryIDAndIsUpload(datePublishBegin, datePublishEnd, industryID, isUpload);
            string note = AppGlobal.Success + " - " + AppGlobal.DeleteSuccess;
            return Json(note);
        }
        public IActionResult UpdateByIndustryIDAndDatePublishBeginAndDatePublishEndAndAllData(int industryID, DateTime datePublishBegin, DateTime datePublishEnd, bool allData)
        {
            if (allData == true)
            {
                _reportRepository.UpdateByDatePublishBeginAndDatePublishEndAndIndustryIDAndAllData001(datePublishBegin, datePublishEnd, industryID, allData, RequestUserID);
            }
            string note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            return Json(note);
        }

        public async Task<IActionResult> ExportExcelReportDaily(CancellationToken cancellationToken, int ID)
        {
            await Task.Yield();
            var list = _reportRepository.ReportDaily02ByProductSearchIDToList(ID);
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells[1, 1].Value = "No";
                workSheet.Cells[1, 1].Style.Font.Bold = true;
                workSheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 2].Value = "Publish";
                workSheet.Cells[1, 2].Style.Font.Bold = true;
                workSheet.Cells[1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 3].Value = "Category";
                workSheet.Cells[1, 3].Style.Font.Bold = true;
                workSheet.Cells[1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 4].Value = "Industry";
                workSheet.Cells[1, 4].Style.Font.Bold = true;
                workSheet.Cells[1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 5].Value = "Company";
                workSheet.Cells[1, 5].Style.Font.Bold = true;
                workSheet.Cells[1, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 6].Value = "Product";
                workSheet.Cells[1, 6].Style.Font.Bold = true;
                workSheet.Cells[1, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 7].Value = "Sentiment";
                workSheet.Cells[1, 7].Style.Font.Bold = true;
                workSheet.Cells[1, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 8].Value = "Headline (Vie)";
                workSheet.Cells[1, 8].Style.Font.Bold = true;
                workSheet.Cells[1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 9].Value = "Headline (Eng)";
                workSheet.Cells[1, 9].Style.Font.Bold = true;
                workSheet.Cells[1, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 10].Value = "Media";
                workSheet.Cells[1, 10].Style.Font.Bold = true;
                workSheet.Cells[1, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 11].Value = "Media type";
                workSheet.Cells[1, 11].Style.Font.Bold = true;
                workSheet.Cells[1, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 12].Value = "Ad value";
                workSheet.Cells[1, 12].Style.Font.Bold = true;
                workSheet.Cells[1, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 13].Value = "Summary";
                workSheet.Cells[1, 13].Style.Font.Bold = true;
                workSheet.Cells[1, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 14].Value = "Point";
                workSheet.Cells[1, 14].Style.Font.Bold = true;
                workSheet.Cells[1, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                int i = 0;
                int no = 0;
                for (int row = 2; row <= list.Count + 1; row++)
                {
                    no = no + 1;
                    workSheet.Cells[row, 1].Value = no.ToString();
                    workSheet.Cells[row, 2].Value = list[i].DatePublishString;
                    workSheet.Cells[row, 3].Value = list[i].ArticleTypeName;

                    //string industryName = "";
                    //List<ProductPropertyDataTransfer> listProductPropertyDataTransfer = _productPropertyRepository.GetDataTransferIndustryByParentIDToList(list[i].ProductID.Value);
                    //foreach (ProductPropertyDataTransfer item in listProductPropertyDataTransfer)
                    //{
                    //    industryName = industryName + item.IndustryName + ", ";
                    //}
                    //workSheet.Cells[row, 4].Value = industryName;

                    //string companyName = "";
                    //listProductPropertyDataTransfer = _productPropertyRepository.GetDataTransferCompanyByParentIDToList(list[i].ProductID.Value);
                    //foreach (ProductPropertyDataTransfer item in listProductPropertyDataTransfer)
                    //{
                    //    companyName = companyName + item.CompanyName + ", ";
                    //}
                    //workSheet.Cells[row, 5].Value = companyName;

                    //string productName = "";
                    //listProductPropertyDataTransfer = _productPropertyRepository.GetDataTransferProductByParentIDToList(list[i].ProductID.Value);
                    //foreach (ProductPropertyDataTransfer item in listProductPropertyDataTransfer)
                    //{
                    //    productName = productName + item.ProductName + ", ";
                    //}
                    //workSheet.Cells[row, 6].Value = productName;

                    workSheet.Cells[row, 4].Value = list[i].IndustryName;
                    workSheet.Cells[row, 5].Value = list[i].CompanyName;
                    workSheet.Cells[row, 6].Value = list[i].ProductName;
                    workSheet.Cells[row, 7].Value = list[i].AssessName;
                    workSheet.Cells[row, 8].Value = list[i].Title;
                    if (!string.IsNullOrEmpty(list[i].Title))
                    {
                        workSheet.Cells[row, 8].Hyperlink = new Uri(list[i].URLCode);
                        workSheet.Cells[row, 8].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                    }
                    workSheet.Cells[row, 9].Value = list[i].TitleEnglish;
                    if (!string.IsNullOrEmpty(list[i].TitleEnglish))
                    {
                        workSheet.Cells[row, 9].Hyperlink = new Uri(list[i].URLCode);
                        workSheet.Cells[row, 9].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                    }
                    workSheet.Cells[row, 10].Value = list[i].Media;
                    workSheet.Cells[row, 11].Value = list[i].MediaType;
                    workSheet.Cells[row, 12].Value = list[i].AdvertisementValueString;
                    workSheet.Cells[row, 13].Value = list[i].Summary;
                    workSheet.Cells[row, 14].Value = list[i].Point;
                    i = i + 1;
                }
                workSheet.Column(1).AutoFit();
                workSheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Column(2).AutoFit();
                workSheet.Column(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Column(3).AutoFit();
                workSheet.Column(4).AutoFit();
                workSheet.Column(5).AutoFit();
                workSheet.Column(6).AutoFit();
                workSheet.Column(7).AutoFit();
                workSheet.Column(8).AutoFit();
                workSheet.Column(9).AutoFit();
                workSheet.Column(10).AutoFit();
                workSheet.Column(11).AutoFit();
                workSheet.Column(12).AutoFit();
                workSheet.Column(12).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Column(13).AutoFit();
                workSheet.Column(14).AutoFit();
                workSheet.Column(14).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                package.Save();
            }
            stream.Position = 0;
            string excelName = @"ReportDaily_" + AppGlobal.DateTimeCode + ".xlsx";
            ProductSearchDataTransfer model = new ProductSearchDataTransfer();
            if (ID > 0)
            {
                model = _productSearchRepository.GetDataTransferByID(ID);
                if (model != null)
                {
                    excelName = model.CompanyName + "_" + model.Title + "_" + AppGlobal.DateTimeCode + ".xlsx";
                }
            }
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        public async Task<IActionResult> ExportExcelReportDailyActive(CancellationToken cancellationToken, int ID)
        {
            await Task.Yield();
            var list = _reportRepository.ReportDaily02ByProductSearchIDAndActiveToList(ID, true);
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells[1, 1].Value = "No";
                workSheet.Cells[1, 1].Style.Font.Bold = true;
                workSheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 2].Value = "Publish";
                workSheet.Cells[1, 2].Style.Font.Bold = true;
                workSheet.Cells[1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 3].Value = "Category";
                workSheet.Cells[1, 3].Style.Font.Bold = true;
                workSheet.Cells[1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 4].Value = "Industry";
                workSheet.Cells[1, 4].Style.Font.Bold = true;
                workSheet.Cells[1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 5].Value = "Company";
                workSheet.Cells[1, 5].Style.Font.Bold = true;
                workSheet.Cells[1, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 6].Value = "Product";
                workSheet.Cells[1, 6].Style.Font.Bold = true;
                workSheet.Cells[1, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 7].Value = "Sentiment";
                workSheet.Cells[1, 7].Style.Font.Bold = true;
                workSheet.Cells[1, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 8].Value = "Headline (Vie)";
                workSheet.Cells[1, 8].Style.Font.Bold = true;
                workSheet.Cells[1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 9].Value = "Headline (Eng)";
                workSheet.Cells[1, 9].Style.Font.Bold = true;
                workSheet.Cells[1, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 10].Value = "Media";
                workSheet.Cells[1, 10].Style.Font.Bold = true;
                workSheet.Cells[1, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 11].Value = "Media type";
                workSheet.Cells[1, 11].Style.Font.Bold = true;
                workSheet.Cells[1, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 12].Value = "Ad value";
                workSheet.Cells[1, 12].Style.Font.Bold = true;
                workSheet.Cells[1, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 13].Value = "Summary";
                workSheet.Cells[1, 13].Style.Font.Bold = true;
                workSheet.Cells[1, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 14].Value = "Point";
                workSheet.Cells[1, 14].Style.Font.Bold = true;
                workSheet.Cells[1, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                int i = 0;
                int no = 0;
                for (int row = 2; row <= list.Count + 1; row++)
                {
                    no = no + 1;
                    workSheet.Cells[row, 1].Value = no.ToString();
                    workSheet.Cells[row, 2].Value = list[i].DatePublishString;
                    workSheet.Cells[row, 3].Value = list[i].ArticleTypeName;

                    //string industryName = "";
                    //List<ProductPropertyDataTransfer> listProductPropertyDataTransfer = _productPropertyRepository.GetDataTransferIndustryByParentIDToList(list[i].ProductID.Value);
                    //foreach (ProductPropertyDataTransfer item in listProductPropertyDataTransfer)
                    //{
                    //    industryName = item.IndustryName;
                    //}
                    //workSheet.Cells[row, 4].Value = industryName;

                    //string companyName = "";
                    //listProductPropertyDataTransfer = _productPropertyRepository.GetDataTransferCompanyByParentIDToList(list[i].ProductID.Value);
                    //foreach (ProductPropertyDataTransfer item in listProductPropertyDataTransfer)
                    //{
                    //    companyName = item.CompanyName;
                    //}
                    //workSheet.Cells[row, 5].Value = companyName;

                    //string productName = "";
                    //listProductPropertyDataTransfer = _productPropertyRepository.GetDataTransferProductByParentIDToList(list[i].ProductID.Value);
                    //foreach (ProductPropertyDataTransfer item in listProductPropertyDataTransfer)
                    //{
                    //    productName = item.ProductName;
                    //}
                    //workSheet.Cells[row, 6].Value = productName;

                    workSheet.Cells[row, 4].Value = list[i].IndustryName;
                    workSheet.Cells[row, 5].Value = list[i].CompanyName;
                    workSheet.Cells[row, 6].Value = list[i].ProductName;
                    workSheet.Cells[row, 7].Value = list[i].AssessName;
                    workSheet.Cells[row, 8].Value = list[i].Title;
                    if (!string.IsNullOrEmpty(list[i].Title))
                    {
                        workSheet.Cells[row, 8].Hyperlink = new Uri(list[i].URLCode);
                        workSheet.Cells[row, 8].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                    }
                    workSheet.Cells[row, 9].Value = list[i].TitleEnglish;
                    if (!string.IsNullOrEmpty(list[i].TitleEnglish))
                    {
                        workSheet.Cells[row, 9].Hyperlink = new Uri(list[i].URLCode);
                        workSheet.Cells[row, 9].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                    }
                    workSheet.Cells[row, 10].Value = list[i].Media;
                    workSheet.Cells[row, 11].Value = list[i].MediaType;
                    workSheet.Cells[row, 12].Value = list[i].AdvertisementValueString;
                    workSheet.Cells[row, 13].Value = list[i].Summary;
                    workSheet.Cells[row, 14].Value = list[i].Point;
                    i = i + 1;
                }
                workSheet.Column(1).AutoFit();
                workSheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Column(2).AutoFit();
                workSheet.Column(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Column(3).AutoFit();
                workSheet.Column(4).AutoFit();
                workSheet.Column(5).AutoFit();
                workSheet.Column(6).AutoFit();
                workSheet.Column(7).AutoFit();
                workSheet.Column(8).AutoFit();
                workSheet.Column(9).AutoFit();
                workSheet.Column(10).AutoFit();
                workSheet.Column(11).AutoFit();
                workSheet.Column(12).AutoFit();
                workSheet.Column(12).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Column(13).AutoFit();
                workSheet.Column(14).AutoFit();
                workSheet.Column(14).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                package.Save();
            }
            stream.Position = 0;
            string excelName = @"ReportDaily_" + AppGlobal.DateTimeCode + ".xlsx";
            ProductSearchDataTransfer model = new ProductSearchDataTransfer();
            if (ID > 0)
            {
                model = _productSearchRepository.GetDataTransferByID(ID);
                if (model != null)
                {
                    excelName = model.CompanyName + "_" + model.Title + "_" + AppGlobal.DateTimeCode + ".xlsx";
                }
            }
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        public async Task<IActionResult> ExportExcelArticleProduct(CancellationToken cancellationToken, int year, int month, int day)
        {
            DateTime datePublish = new DateTime(year, month, day);
            await Task.Yield();
            var list = _productRepository.GetDataTransferByDatePublishAndArticleTypeIDAndProductIDAndActionToList(datePublish, AppGlobal.TinSanPhamID, 0, 0);
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells[1, 1].Value = "No";
                workSheet.Cells[1, 1].Style.Font.Bold = true;
                workSheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 2].Value = "Publish";
                workSheet.Cells[1, 2].Style.Font.Bold = true;
                workSheet.Cells[1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 3].Value = "Category";
                workSheet.Cells[1, 3].Style.Font.Bold = true;
                workSheet.Cells[1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 4].Value = "Industry";
                workSheet.Cells[1, 4].Style.Font.Bold = true;
                workSheet.Cells[1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 5].Value = "Company";
                workSheet.Cells[1, 5].Style.Font.Bold = true;
                workSheet.Cells[1, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 6].Value = "Product";
                workSheet.Cells[1, 6].Style.Font.Bold = true;
                workSheet.Cells[1, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 7].Value = "Sentiment";
                workSheet.Cells[1, 7].Style.Font.Bold = true;
                workSheet.Cells[1, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 8].Value = "Headline (Vie)";
                workSheet.Cells[1, 8].Style.Font.Bold = true;
                workSheet.Cells[1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 9].Value = "Headline (Eng)";
                workSheet.Cells[1, 9].Style.Font.Bold = true;
                workSheet.Cells[1, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 10].Value = "Media";
                workSheet.Cells[1, 10].Style.Font.Bold = true;
                workSheet.Cells[1, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 11].Value = "Media type";
                workSheet.Cells[1, 11].Style.Font.Bold = true;
                workSheet.Cells[1, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 12].Value = "Ad value";
                workSheet.Cells[1, 12].Style.Font.Bold = true;
                workSheet.Cells[1, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 13].Value = "Summary";
                workSheet.Cells[1, 13].Style.Font.Bold = true;
                workSheet.Cells[1, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 14].Value = "Point";
                workSheet.Cells[1, 14].Style.Font.Bold = true;
                workSheet.Cells[1, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                int i = 0;
                int no = 0;
                for (int row = 2; row <= list.Count + 1; row++)
                {
                    no = no + 1;
                    workSheet.Cells[row, 1].Value = no.ToString();
                    workSheet.Cells[row, 2].Value = list[i].DatePublishString;
                    workSheet.Cells[row, 3].Value = list[i].ArticleTypeName;

                    //workSheet.Cells[row, 4].Value = list[i].IndustryName;
                    //workSheet.Cells[row, 5].Value = list[i].CompanyName;
                    //workSheet.Cells[row, 6].Value = list[i].ProductName;

                    string industryName = "";
                    List<ProductPropertyDataTransfer> listProductPropertyDataTransfer = _productPropertyRepository.GetDataTransferIndustryByParentIDToList(list[i].ID);
                    foreach (ProductPropertyDataTransfer item in listProductPropertyDataTransfer)
                    {
                        industryName = industryName + item.IndustryName + ", ";
                    }
                    workSheet.Cells[row, 4].Value = industryName;

                    string companyName = "";
                    listProductPropertyDataTransfer = _productPropertyRepository.GetDataTransferCompanyByParentIDToList(list[i].ID);
                    foreach (ProductPropertyDataTransfer item in listProductPropertyDataTransfer)
                    {
                        companyName = companyName + item.CompanyName + ", ";
                    }
                    workSheet.Cells[row, 5].Value = companyName;

                    string productName = "";
                    listProductPropertyDataTransfer = _productPropertyRepository.GetDataTransferProductByParentIDToList(list[i].ID);
                    foreach (ProductPropertyDataTransfer item in listProductPropertyDataTransfer)
                    {
                        productName = productName + item.ProductName + ", ";
                    }
                    workSheet.Cells[row, 6].Value = productName;


                    workSheet.Cells[row, 7].Value = list[i].AssessName;
                    workSheet.Cells[row, 8].Value = list[i].Title;
                    if (!string.IsNullOrEmpty(list[i].Title))
                    {
                        workSheet.Cells[row, 8].Hyperlink = new Uri(list[i].URLCode);
                        workSheet.Cells[row, 8].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                    }
                    workSheet.Cells[row, 9].Value = list[i].TitleEnglish;
                    if (!string.IsNullOrEmpty(list[i].TitleEnglish))
                    {
                        workSheet.Cells[row, 9].Hyperlink = new Uri(list[i].URLCode);
                        workSheet.Cells[row, 9].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                    }
                    workSheet.Cells[row, 10].Value = list[i].Media;
                    workSheet.Cells[row, 11].Value = list[i].MediaType;
                    workSheet.Cells[row, 12].Value = list[i].AdvertisementValueString;
                    workSheet.Cells[row, 13].Value = list[i].Summary;
                    workSheet.Cells[row, 14].Value = list[i].Point;
                    i = i + 1;
                }
                workSheet.Column(1).AutoFit();
                workSheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Column(2).AutoFit();
                workSheet.Column(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Column(3).AutoFit();
                workSheet.Column(4).AutoFit();
                workSheet.Column(5).AutoFit();
                workSheet.Column(6).AutoFit();
                workSheet.Column(7).AutoFit();
                workSheet.Column(8).AutoFit();
                workSheet.Column(9).AutoFit();
                workSheet.Column(10).AutoFit();
                workSheet.Column(11).AutoFit();
                workSheet.Column(12).AutoFit();
                workSheet.Column(12).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Column(13).AutoFit();
                workSheet.Column(14).AutoFit();
                workSheet.Column(14).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                package.Save();
            }
            stream.Position = 0;
            string excelName = @"ArticleProduct_" + datePublish.ToString("yyyyMMdd") + "_" + AppGlobal.DateTimeCode + ".xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        public async Task<IActionResult> ExportExcelArticleCompany(CancellationToken cancellationToken, int year, int month, int day)
        {
            DateTime datePublish = new DateTime(year, month, day);
            await Task.Yield();
            var list = _productRepository.GetDataTransferByDatePublishAndArticleTypeIDAndCompanyIDAndActionToList(datePublish, AppGlobal.TinDoanhNghiepID, 0, 0);
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells[1, 1].Value = "No";
                workSheet.Cells[1, 1].Style.Font.Bold = true;
                workSheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 2].Value = "Publish";
                workSheet.Cells[1, 2].Style.Font.Bold = true;
                workSheet.Cells[1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 3].Value = "Category";
                workSheet.Cells[1, 3].Style.Font.Bold = true;
                workSheet.Cells[1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 4].Value = "Industry";
                workSheet.Cells[1, 4].Style.Font.Bold = true;
                workSheet.Cells[1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 5].Value = "Company";
                workSheet.Cells[1, 5].Style.Font.Bold = true;
                workSheet.Cells[1, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 6].Value = "Product";
                workSheet.Cells[1, 6].Style.Font.Bold = true;
                workSheet.Cells[1, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 7].Value = "Sentiment";
                workSheet.Cells[1, 7].Style.Font.Bold = true;
                workSheet.Cells[1, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 8].Value = "Headline (Vie)";
                workSheet.Cells[1, 8].Style.Font.Bold = true;
                workSheet.Cells[1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 9].Value = "Headline (Eng)";
                workSheet.Cells[1, 9].Style.Font.Bold = true;
                workSheet.Cells[1, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 10].Value = "Media";
                workSheet.Cells[1, 10].Style.Font.Bold = true;
                workSheet.Cells[1, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 11].Value = "Media type";
                workSheet.Cells[1, 11].Style.Font.Bold = true;
                workSheet.Cells[1, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 12].Value = "Ad value";
                workSheet.Cells[1, 12].Style.Font.Bold = true;
                workSheet.Cells[1, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 13].Value = "Summary";
                workSheet.Cells[1, 13].Style.Font.Bold = true;
                workSheet.Cells[1, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 14].Value = "Point";
                workSheet.Cells[1, 14].Style.Font.Bold = true;
                workSheet.Cells[1, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                int i = 0;
                int no = 0;
                for (int row = 2; row <= list.Count + 1; row++)
                {
                    no = no + 1;
                    workSheet.Cells[row, 1].Value = no.ToString();
                    workSheet.Cells[row, 2].Value = list[i].DatePublishString;
                    workSheet.Cells[row, 3].Value = list[i].ArticleTypeName;
                    //workSheet.Cells[row, 4].Value = "";
                    //if (list[i].IndustryName != "General")
                    //{
                    //    workSheet.Cells[row, 4].Value = list[i].IndustryName;
                    //}
                    //workSheet.Cells[row, 5].Value = list[i].CompanyName;
                    //workSheet.Cells[row, 6].Value = list[i].ProductName;

                    string industryName = "";
                    List<ProductPropertyDataTransfer> listProductPropertyDataTransfer = _productPropertyRepository.GetDataTransferIndustryByParentIDToList(list[i].ID);
                    foreach (ProductPropertyDataTransfer item in listProductPropertyDataTransfer)
                    {
                        if (item.IndustryName != "General")
                        {
                            industryName = industryName + item.IndustryName + ", ";
                        }
                    }
                    workSheet.Cells[row, 4].Value = industryName;

                    string companyName = "";
                    listProductPropertyDataTransfer = _productPropertyRepository.GetDataTransferCompanyByParentIDToList(list[i].ID);
                    foreach (ProductPropertyDataTransfer item in listProductPropertyDataTransfer)
                    {
                        companyName = companyName + item.CompanyName + ", ";
                    }
                    workSheet.Cells[row, 5].Value = companyName;

                    string productName = "";
                    listProductPropertyDataTransfer = _productPropertyRepository.GetDataTransferProductByParentIDToList(list[i].ID);
                    foreach (ProductPropertyDataTransfer item in listProductPropertyDataTransfer)
                    {
                        productName = productName + item.ProductName + ", ";
                    }
                    workSheet.Cells[row, 6].Value = productName;


                    workSheet.Cells[row, 7].Value = list[i].AssessName;
                    workSheet.Cells[row, 8].Value = list[i].Title;
                    if (!string.IsNullOrEmpty(list[i].Title))
                    {
                        workSheet.Cells[row, 8].Hyperlink = new Uri(list[i].URLCode);
                        workSheet.Cells[row, 8].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                    }
                    workSheet.Cells[row, 9].Value = list[i].TitleEnglish;
                    if (!string.IsNullOrEmpty(list[i].TitleEnglish))
                    {
                        workSheet.Cells[row, 9].Hyperlink = new Uri(list[i].URLCode);
                        workSheet.Cells[row, 9].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                    }
                    workSheet.Cells[row, 10].Value = list[i].Media;
                    workSheet.Cells[row, 11].Value = list[i].MediaType;
                    workSheet.Cells[row, 12].Value = list[i].AdvertisementValueString;
                    workSheet.Cells[row, 13].Value = list[i].Summary;
                    workSheet.Cells[row, 14].Value = list[i].Point;
                    i = i + 1;
                }
                workSheet.Column(1).AutoFit();
                workSheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Column(2).AutoFit();
                workSheet.Column(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Column(3).AutoFit();
                workSheet.Column(4).AutoFit();
                workSheet.Column(5).AutoFit();
                workSheet.Column(6).AutoFit();
                workSheet.Column(7).AutoFit();
                workSheet.Column(8).AutoFit();
                workSheet.Column(9).AutoFit();
                workSheet.Column(10).AutoFit();
                workSheet.Column(11).AutoFit();
                workSheet.Column(12).AutoFit();
                workSheet.Column(12).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Column(13).AutoFit();
                workSheet.Column(14).AutoFit();
                workSheet.Column(14).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                package.Save();
            }
            stream.Position = 0;
            string excelName = @"ArticleCompany_" + datePublish.ToString("yyyyMMdd") + "_" + AppGlobal.DateTimeCode + ".xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        public async Task<IActionResult> ExportExcelArticleIndustry(CancellationToken cancellationToken, int year, int month, int day)
        {
            DateTime datePublish = new DateTime(year, month, day);
            await Task.Yield();
            var list = _productRepository.GetDataTransferByDatePublishAndArticleTypeIDAndIndustryIDAndActionToList(datePublish, AppGlobal.TinNganhID, 0, 0);
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells[1, 1].Value = "No";
                workSheet.Cells[1, 1].Style.Font.Bold = true;
                workSheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 2].Value = "Publish";
                workSheet.Cells[1, 2].Style.Font.Bold = true;
                workSheet.Cells[1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 3].Value = "Category";
                workSheet.Cells[1, 3].Style.Font.Bold = true;
                workSheet.Cells[1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 4].Value = "Industry";
                workSheet.Cells[1, 4].Style.Font.Bold = true;
                workSheet.Cells[1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 5].Value = "Company";
                workSheet.Cells[1, 5].Style.Font.Bold = true;
                workSheet.Cells[1, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 6].Value = "Product";
                workSheet.Cells[1, 6].Style.Font.Bold = true;
                workSheet.Cells[1, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 7].Value = "Sentiment";
                workSheet.Cells[1, 7].Style.Font.Bold = true;
                workSheet.Cells[1, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 8].Value = "Headline (Vie)";
                workSheet.Cells[1, 8].Style.Font.Bold = true;
                workSheet.Cells[1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 9].Value = "Headline (Eng)";
                workSheet.Cells[1, 9].Style.Font.Bold = true;
                workSheet.Cells[1, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 10].Value = "Media";
                workSheet.Cells[1, 10].Style.Font.Bold = true;
                workSheet.Cells[1, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 11].Value = "Media type";
                workSheet.Cells[1, 11].Style.Font.Bold = true;
                workSheet.Cells[1, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 12].Value = "Ad value";
                workSheet.Cells[1, 12].Style.Font.Bold = true;
                workSheet.Cells[1, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 13].Value = "Summary";
                workSheet.Cells[1, 13].Style.Font.Bold = true;
                workSheet.Cells[1, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 14].Value = "Point";
                workSheet.Cells[1, 14].Style.Font.Bold = true;
                workSheet.Cells[1, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                int i = 0;
                int no = 0;
                for (int row = 2; row <= list.Count + 1; row++)
                {
                    no = no + 1;
                    workSheet.Cells[row, 1].Value = no.ToString();
                    workSheet.Cells[row, 2].Value = list[i].DatePublishString;
                    workSheet.Cells[row, 3].Value = list[i].ArticleTypeName;
                    //workSheet.Cells[row, 4].Value = list[i].IndustryName;                                       
                    //workSheet.Cells[row, 5].Value = list[i].CompanyName;
                    //workSheet.Cells[row, 6].Value = list[i].ProductName;

                    string industryName = "";
                    List<ProductPropertyDataTransfer> listProductPropertyDataTransfer = _productPropertyRepository.GetDataTransferIndustryByParentIDToList(list[i].ID);
                    foreach (ProductPropertyDataTransfer item in listProductPropertyDataTransfer)
                    {
                        if (item.IndustryName != "General")
                        {
                            industryName = industryName + item.IndustryName + ", ";
                        }
                    }
                    workSheet.Cells[row, 4].Value = industryName;

                    string companyName = "";
                    listProductPropertyDataTransfer = _productPropertyRepository.GetDataTransferCompanyByParentIDToList(list[i].ID);
                    foreach (ProductPropertyDataTransfer item in listProductPropertyDataTransfer)
                    {
                        companyName = companyName + item.CompanyName + ", ";
                    }
                    workSheet.Cells[row, 5].Value = companyName;

                    string productName = "";
                    listProductPropertyDataTransfer = _productPropertyRepository.GetDataTransferProductByParentIDToList(list[i].ID);
                    foreach (ProductPropertyDataTransfer item in listProductPropertyDataTransfer)
                    {
                        productName = productName + item.ProductName + ", ";
                    }
                    workSheet.Cells[row, 6].Value = productName;

                    workSheet.Cells[row, 7].Value = list[i].AssessName;
                    workSheet.Cells[row, 8].Value = list[i].Title;
                    if (!string.IsNullOrEmpty(list[i].Title))
                    {
                        workSheet.Cells[row, 8].Hyperlink = new Uri(list[i].URLCode);
                        workSheet.Cells[row, 8].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                    }
                    workSheet.Cells[row, 9].Value = list[i].TitleEnglish;
                    if (!string.IsNullOrEmpty(list[i].TitleEnglish))
                    {
                        workSheet.Cells[row, 9].Hyperlink = new Uri(list[i].URLCode);
                        workSheet.Cells[row, 9].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                    }
                    workSheet.Cells[row, 10].Value = list[i].Media;
                    workSheet.Cells[row, 11].Value = list[i].MediaType;
                    workSheet.Cells[row, 12].Value = list[i].AdvertisementValueString;
                    workSheet.Cells[row, 13].Value = list[i].Summary;
                    workSheet.Cells[row, 14].Value = list[i].Point;
                    i = i + 1;
                }
                workSheet.Column(1).AutoFit();
                workSheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Column(2).AutoFit();
                workSheet.Column(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Column(3).AutoFit();
                workSheet.Column(4).AutoFit();
                workSheet.Column(5).AutoFit();
                workSheet.Column(6).AutoFit();
                workSheet.Column(7).AutoFit();
                workSheet.Column(8).AutoFit();
                workSheet.Column(9).AutoFit();
                workSheet.Column(10).AutoFit();
                workSheet.Column(11).AutoFit();
                workSheet.Column(12).AutoFit();
                workSheet.Column(12).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Column(13).AutoFit();
                workSheet.Column(14).AutoFit();
                workSheet.Column(14).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                package.Save();
            }
            stream.Position = 0;
            string excelName = @"ArticleIndustry_" + datePublish.ToString("yyyyMMdd") + "_" + AppGlobal.DateTimeCode + ".xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        public ActionResult UploadScan(Commsights.MVC.Models.BaseViewModel baseViewModel)
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
                        fileName = AppGlobal.SourceScan;
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
                                                int totalRows = workSheet.Dimension.Rows;
                                                for (int i = 2; i <= totalRows; i++)
                                                {
                                                    Product model = new Product();
                                                    model.Note = fileName;
                                                    model.Source = AppGlobal.SourceScan;
                                                    model.Initialization(InitType.Insert, RequestUserID);
                                                    model.GUICode = AppGlobal.InitGuiCode;
                                                    try
                                                    {
                                                        string mediaTitle = "";
                                                        if (workSheet.Cells[i, 1].Value != null)
                                                        {
                                                            string datePublish = workSheet.Cells[i, 1].Value.ToString().Trim();
                                                            try
                                                            {
                                                                model.DatePublish = DateTime.Parse(datePublish);
                                                            }
                                                            catch
                                                            {
                                                                try
                                                                {
                                                                    DateTime DateTimeStandard = new DateTime(1899, 12, 30);
                                                                    model.DatePublish = DateTimeStandard.AddDays(int.Parse(datePublish));
                                                                }
                                                                catch
                                                                {
                                                                }
                                                            }
                                                        }
                                                        if (workSheet.Cells[i, 9].Value != null)
                                                        {
                                                            model.Title = workSheet.Cells[i, 9].Value.ToString().Trim();
                                                            if (model.Title.Equals(model.Title.ToUpper()))
                                                            {
                                                                model.Title = AppGlobal.ToUpperFirstLetter(model.Title);
                                                            }
                                                            if (workSheet.Cells[i, 9].Hyperlink != null)
                                                            {
                                                                model.URLCode = workSheet.Cells[i, 9].Hyperlink.AbsoluteUri.Trim();
                                                            }
                                                        }
                                                        if (workSheet.Cells[i, 11].Value != null)
                                                        {
                                                            model.FileName = workSheet.Cells[i, 11].Value.ToString().Trim();
                                                        }
                                                        if (workSheet.Cells[i, 12].Value != null)
                                                        {
                                                            mediaTitle = workSheet.Cells[i, 12].Value.ToString().Trim();
                                                        }
                                                        if (workSheet.Cells[i, 23].Value != null)
                                                        {
                                                            model.Page = workSheet.Cells[i, 23].Value.ToString().Trim();
                                                        }
                                                        model.ParentID = AppGlobal.WebsiteID;
                                                        Config parent = _configResposistory.GetByGroupNameAndCodeAndTitle(AppGlobal.CRM, AppGlobal.PressList, mediaTitle);
                                                        if (parent == null)
                                                        {
                                                            parent = new Config();
                                                            parent.GroupName = AppGlobal.CRM;
                                                            parent.Code = AppGlobal.PressList;
                                                            parent.Title = mediaTitle;
                                                            parent.CodeName = mediaTitle;
                                                            parent.Color = AppGlobal.AdvertisementValue;
                                                            if (workSheet.Cells[i, 13].Value != null)
                                                            {
                                                                string type = workSheet.Cells[i, 13].Value.ToString().Trim();
                                                                Config mediaType = _configResposistory.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.WebsiteType, type);
                                                                if (mediaType == null)
                                                                {
                                                                    mediaType = new Config();
                                                                    mediaType.GroupName = AppGlobal.CRM;
                                                                    mediaType.Code = AppGlobal.WebsiteType;
                                                                    mediaType.CodeName = type;
                                                                    mediaType.Initialization(InitType.Insert, RequestUserID);
                                                                    _configResposistory.Create(mediaType);
                                                                }
                                                                parent.ParentID = mediaType.ID;
                                                            }
                                                            if (workSheet.Cells[i, 14].Value != null)
                                                            {
                                                                string type = workSheet.Cells[i, 14].Value.ToString().Trim();
                                                                Config country = _configResposistory.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.Country, type);
                                                                if (country == null)
                                                                {
                                                                    country = new Config();
                                                                    country.GroupName = AppGlobal.CRM;
                                                                    country.Code = AppGlobal.Country;
                                                                    country.CodeName = type;
                                                                    country.Initialization(InitType.Insert, RequestUserID);
                                                                    _configResposistory.Create(country);
                                                                }
                                                                parent.CountryID = country.ID;
                                                            }
                                                            if (workSheet.Cells[i, 17].Value != null)
                                                            {
                                                                string type = workSheet.Cells[i, 17].Value.ToString().Trim();
                                                                Config language = _configResposistory.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.Language, type);
                                                                if (language == null)
                                                                {
                                                                    language = new Config();
                                                                    language.GroupName = AppGlobal.CRM;
                                                                    language.Code = AppGlobal.Language;
                                                                    language.CodeName = type;
                                                                    language.Initialization(InitType.Insert, RequestUserID);
                                                                    _configResposistory.Create(language);
                                                                }
                                                                parent.LanguageID = language.ID;
                                                            }
                                                            if (workSheet.Cells[i, 18].Value != null)
                                                            {
                                                                string type = workSheet.Cells[i, 18].Value.ToString().Trim();
                                                                Config frequency = _configResposistory.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.Frequency, type);
                                                                if (frequency == null)
                                                                {
                                                                    frequency = new Config();
                                                                    frequency.GroupName = AppGlobal.CRM;
                                                                    frequency.Code = AppGlobal.Frequency;
                                                                    frequency.CodeName = type;
                                                                    frequency.Initialization(InitType.Insert, RequestUserID);
                                                                    _configResposistory.Create(frequency);
                                                                }
                                                                parent.FrequencyID = frequency.ID;
                                                            }
                                                            if (workSheet.Cells[i, 22].Value != null)
                                                            {
                                                                string type = workSheet.Cells[i, 22].Value.ToString().Trim();
                                                                Config colorType = _configResposistory.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.Color, type);
                                                                if (colorType == null)
                                                                {
                                                                    colorType = new Config();
                                                                    colorType.GroupName = AppGlobal.CRM;
                                                                    colorType.Code = AppGlobal.Color;
                                                                    colorType.CodeName = type;
                                                                    colorType.Initialization(InitType.Insert, RequestUserID);
                                                                    _configResposistory.Create(colorType);
                                                                }
                                                                parent.ColorTypeID = colorType.ID;
                                                            }
                                                            if (workSheet.Cells[i, 26].Value != null)
                                                            {
                                                                string type = workSheet.Cells[i, 26].Value.ToString().Trim();
                                                                try
                                                                {
                                                                    parent.BlackWhite = int.Parse(type);
                                                                    parent.Color = parent.BlackWhite;
                                                                }
                                                                catch
                                                                {
                                                                }
                                                            }
                                                            parent.Initialization(InitType.Insert, RequestUserID);
                                                            parent.Initialization();
                                                            _configResposistory.Create(parent);
                                                        }
                                                        if (parent != null)
                                                        {
                                                            model.ParentID = parent.ID;
                                                        }
                                                        Product product = _productRepository.GetByURLCode(model.URLCode);
                                                        if (product == null)
                                                        {
                                                            model.MetaTitle = AppGlobal.SetName(model.Title);
                                                            _productRepository.Create(model);
                                                            product = model;
                                                        }
                                                        if (product.ID > 0)
                                                        {
                                                            bool isCompany = true;
                                                            if (workSheet.Cells[i, 3].Value != null)
                                                            {
                                                                string industryName = workSheet.Cells[i, 3].Value.ToString().Trim();
                                                                Config industry = _configResposistory.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.Industry, industryName);
                                                                if (industry != null)
                                                                {
                                                                    if (industry.ID > 0)
                                                                    {
                                                                        ProductProperty productProperty = new ProductProperty();
                                                                        productProperty.Initialization(InitType.Insert, RequestUserID);
                                                                        productProperty.ParentID = product.ID;
                                                                        productProperty.GUICode = product.GUICode;
                                                                        productProperty.AssessID = AppGlobal.AssessID;
                                                                        productProperty.IndustryID = industry.IndustryID;
                                                                        productProperty.ArticleTypeID = AppGlobal.TinDoanhNghiepID;
                                                                        productProperty.Code = AppGlobal.Industry;
                                                                        productProperty.IsDaily = true;
                                                                        if (_productPropertyRepository.IsExist(productProperty) == true)
                                                                        {
                                                                            _productPropertyRepository.Create(productProperty);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            if (workSheet.Cells[i, 4].Value != null)
                                                            {
                                                                string companyName = workSheet.Cells[i, 4].Value.ToString().Trim();
                                                                if ((companyName.Contains("ngành")) || (companyName.Contains("industry")))
                                                                {
                                                                    isCompany = false;
                                                                }
                                                                else
                                                                {
                                                                    Membership company = _membershipRepository.GetByAccount(companyName);
                                                                    if (company == null)
                                                                    {
                                                                        company = _membershipRepository.GetByCodeAndFullName(AppGlobal.CompanyName, companyName);
                                                                    }
                                                                    if (company == null)
                                                                    {
                                                                        company = new Membership();
                                                                        company.Active = true;
                                                                        company.Account = companyName;
                                                                        company.FullName = companyName;
                                                                        company.ParentID = AppGlobal.ParentIDCompetitor;
                                                                        company.Initialization(InitType.Insert, RequestUserID);
                                                                        _membershipRepository.Create(company);
                                                                        MembershipPermission membershipPermission = new MembershipPermission();
                                                                        membershipPermission.MembershipID = company.ID;
                                                                        membershipPermission.IndustryID = baseViewModel.IndustryIDUploadScan;
                                                                        membershipPermission.Code = AppGlobal.Industry;
                                                                        membershipPermission.Initialization(InitType.Insert, RequestUserID);
                                                                        _membershipPermissionRepository.Create(membershipPermission);
                                                                    }
                                                                    if (baseViewModel.IsIndustryIDUploadScan == true)
                                                                    {
                                                                        foreach (MembershipPermission item in _membershipPermissionRepository.GetByMembershipIDAndCodeToList(company.ID, AppGlobal.Industry))
                                                                        {
                                                                            ProductProperty productProperty = new ProductProperty();
                                                                            productProperty.Initialization(InitType.Insert, RequestUserID);
                                                                            productProperty.ParentID = product.ID;
                                                                            productProperty.GUICode = product.GUICode;
                                                                            productProperty.AssessID = AppGlobal.AssessID;
                                                                            productProperty.IndustryID = item.IndustryID;
                                                                            productProperty.CompanyID = company.ID;
                                                                            productProperty.ArticleTypeID = AppGlobal.TinDoanhNghiepID;
                                                                            productProperty.Code = AppGlobal.Company;
                                                                            productProperty.IsDaily = true;
                                                                            if (_productPropertyRepository.IsExist(productProperty) == true)
                                                                            {
                                                                                _productPropertyRepository.Create(productProperty);
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        ProductProperty productProperty = new ProductProperty();
                                                                        productProperty.Initialization(InitType.Insert, RequestUserID);
                                                                        productProperty.ParentID = product.ID;
                                                                        productProperty.GUICode = product.GUICode;
                                                                        productProperty.AssessID = AppGlobal.AssessID;
                                                                        productProperty.IndustryID = baseViewModel.IndustryIDUploadScan;
                                                                        productProperty.CompanyID = company.ID;
                                                                        productProperty.ArticleTypeID = AppGlobal.TinDoanhNghiepID;
                                                                        productProperty.Code = AppGlobal.Company;
                                                                        productProperty.IsDaily = true;
                                                                        if (_productPropertyRepository.IsExist(productProperty) == true)
                                                                        {
                                                                            _productPropertyRepository.Create(productProperty);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                isCompany = false;
                                                            }
                                                            if (isCompany == false)
                                                            {
                                                                ProductProperty productProperty = new ProductProperty();
                                                                productProperty.Initialization(InitType.Insert, RequestUserID);
                                                                productProperty.ParentID = product.ID;
                                                                productProperty.GUICode = product.GUICode;
                                                                productProperty.ArticleTypeID = AppGlobal.TinNganhID;
                                                                productProperty.AssessID = AppGlobal.AssessID;
                                                                productProperty.Code = AppGlobal.Industry;
                                                                productProperty.IndustryID = baseViewModel.IndustryIDUploadScan;
                                                                productProperty.IsDaily = true;
                                                                if (_productPropertyRepository.IsExist(productProperty) == true)
                                                                {
                                                                    _productPropertyRepository.Create(productProperty);
                                                                }
                                                            }
                                                        }
                                                    }
                                                    catch
                                                    {
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
            catch
            {
            }
            if (string.IsNullOrEmpty(baseViewModel.ActionView))
            {
                baseViewModel.ActionView = "Upload";
            }
            return RedirectToAction(baseViewModel.ActionView);
        }
        public ActionResult UploadAndiSource(Commsights.MVC.Models.BaseViewModel baseViewModel)
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
                        fileName = AppGlobal.SourceAndi;
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
                                                int totalRows = workSheet.Dimension.Rows + 1;
                                                for (int i = 6; i <= totalRows; i++)
                                                {
                                                    string categoryMain = "";
                                                    List<ProductProperty> listProductPropertyURLCode = new List<ProductProperty>();
                                                    Product model = new Product();
                                                    model.Note = fileName;
                                                    model.Initialization(InitType.Insert, RequestUserID);
                                                    model.AssessID = AppGlobal.AssessID;
                                                    if (workSheet.Cells[i, 2].Value != null)
                                                    {
                                                        categoryMain = workSheet.Cells[i, 2].Value.ToString().Trim();
                                                    }
                                                    if (workSheet.Cells[i, 11].Value != null)
                                                    {
                                                        model.Page = workSheet.Cells[i, 11].Value.ToString().Trim();
                                                    }
                                                    try
                                                    {
                                                        if (workSheet.Cells[i, 1].Value != null)
                                                        {
                                                            string datePublish = workSheet.Cells[i, 1].Value.ToString().Trim();
                                                            try
                                                            {
                                                                int year = int.Parse(datePublish.Split('/')[2]);
                                                                int month = int.Parse(datePublish.Split('/')[1]);
                                                                int day = int.Parse(datePublish.Split('/')[0]);
                                                                int hour = int.Parse(model.Page.Split(':')[0]);
                                                                int minutes = int.Parse(model.Page.Split(':')[1]);
                                                                int second = int.Parse(model.Page.Split(':')[2]);
                                                                model.DatePublish = new DateTime(year, month, day, hour, minutes, second);
                                                            }
                                                            catch
                                                            {
                                                                try
                                                                {
                                                                    DateTime DateTimeStandard = new DateTime(1899, 12, 30);
                                                                    model.DatePublish = DateTimeStandard.AddDays(int.Parse(datePublish));
                                                                }
                                                                catch
                                                                {
                                                                }
                                                            }
                                                        }
                                                        if (workSheet.Cells[i, 5].Value != null)
                                                        {
                                                            model.Title = workSheet.Cells[i, 5].Value.ToString().Trim();
                                                            if (model.Title.Equals(model.Title.ToUpper()))
                                                            {
                                                                model.Title = AppGlobal.ToUpperFirstLetter(model.Title);
                                                            }
                                                            if (workSheet.Cells[i, 5].Hyperlink != null)
                                                            {
                                                                model.ImageThumbnail = workSheet.Cells[i, 5].Hyperlink.AbsoluteUri.Trim();
                                                                model.URLCode = model.ImageThumbnail;
                                                                AppGlobal.GetURLByURLAndi(model, listProductPropertyURLCode, RequestUserID);
                                                            }
                                                        }
                                                        if (workSheet.Cells[i, 6].Value != null)
                                                        {
                                                            model.TitleEnglish = workSheet.Cells[i, 6].Value.ToString().Trim();
                                                            if (model.TitleEnglish.Equals(model.TitleEnglish.ToUpper()))
                                                            {
                                                                model.TitleEnglish = AppGlobal.ToUpperFirstLetter(model.TitleEnglish);
                                                            }
                                                        }
                                                        if (workSheet.Cells[i, 7].Value != null)
                                                        {
                                                            model.FileName = workSheet.Cells[i, 7].Value.ToString().Trim();
                                                        }
                                                        if (workSheet.Cells[i, 8].Value != null)
                                                        {
                                                            string mediaTitle = workSheet.Cells[i, 8].Value.ToString().Trim();
                                                            mediaTitle = AppGlobal.ToUpperFirstLetter(mediaTitle);
                                                            string mediaType = "Online";
                                                            string code = AppGlobal.Website;
                                                            if (workSheet.Cells[i, 9].Value != null)
                                                            {
                                                                mediaType = workSheet.Cells[i, 9].Value.ToString().Trim();
                                                                if (mediaType.Contains("TV") == true)
                                                                {
                                                                    mediaType = "TV";
                                                                }
                                                                if (mediaType.Contains("Online") == true)
                                                                {
                                                                    mediaType = "Online";
                                                                }
                                                            }
                                                            if (mediaType.Contains("Online") == false)
                                                            {
                                                                code = AppGlobal.PressList;
                                                            }
                                                            Config parent = _configResposistory.GetByGroupNameAndCodeAndTitle(AppGlobal.CRM, code, mediaTitle);
                                                            if (parent == null)
                                                            {
                                                                parent = new Config();
                                                                parent.GroupName = AppGlobal.CRM;
                                                                parent.Code = code;
                                                                parent.Title = mediaTitle;
                                                                parent.CodeName = mediaTitle;
                                                                parent.Color = AppGlobal.AdvertisementValue;
                                                                Config parentOfParent = _configResposistory.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.WebsiteType, mediaType);
                                                                if (parentOfParent == null)
                                                                {
                                                                    parentOfParent = new Config();
                                                                    parentOfParent.GroupName = AppGlobal.CRM;
                                                                    parentOfParent.Code = AppGlobal.WebsiteType;
                                                                    parentOfParent.CodeName = mediaType;
                                                                    parentOfParent.Initialization(InitType.Insert, RequestUserID);
                                                                    _configResposistory.Create(parentOfParent);
                                                                }
                                                                if (workSheet.Cells[i, 10].Value != null)
                                                                {
                                                                    string frequencyName = workSheet.Cells[i, 10].Value.ToString().Trim();
                                                                    Config frequency = _configResposistory.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.Frequency, frequencyName);
                                                                    if (frequency == null)
                                                                    {
                                                                        frequency = new Config();
                                                                        frequency.GroupName = AppGlobal.CRM;
                                                                        frequency.Code = AppGlobal.Frequency;
                                                                        frequency.CodeName = frequencyName;
                                                                        frequency.Initialization(InitType.Insert, RequestUserID);
                                                                        _configResposistory.Create(frequency);
                                                                    }
                                                                    parent.FrequencyID = frequency.ID;
                                                                }
                                                                if (workSheet.Cells[i, 13].Value != null)
                                                                {
                                                                    try
                                                                    {
                                                                        string color = workSheet.Cells[i, 13].Value.ToString().Trim();
                                                                        parent.Color = int.Parse(color);
                                                                    }
                                                                    catch
                                                                    {
                                                                    }
                                                                }
                                                                parent.ParentID = parentOfParent.ID;
                                                                parent.Initialization(InitType.Insert, RequestUserID);
                                                                parent.Initialization();
                                                                _configResposistory.Create(parent);
                                                            }
                                                            model.ParentID = parent.ID;
                                                        }
                                                        if (workSheet.Cells[i, 12].Value != null)
                                                        {
                                                            model.Duration = workSheet.Cells[i, 12].Value.ToString().Trim();
                                                        }
                                                        if (workSheet.Cells[i, 13].Value != null)
                                                        {
                                                            try
                                                            {
                                                                string advalue = workSheet.Cells[i, 13].Value.ToString().Trim();
                                                                model.Advalue = int.Parse(advalue);
                                                            }
                                                            catch
                                                            {
                                                            }
                                                        }
                                                        if (workSheet.Cells[i, 14].Value != null)
                                                        {
                                                            model.Author = workSheet.Cells[i, 14].Value.ToString().Trim();
                                                        }
                                                        if (workSheet.Cells[i, 15].Value != null)
                                                        {
                                                            string assessString = workSheet.Cells[i, 15].Value.ToString().Trim();
                                                            switch (assessString)
                                                            {
                                                                case "-1":
                                                                    model.AssessID = AppGlobal.NegativeID;
                                                                    break;
                                                                case "0":
                                                                    model.AssessID = AppGlobal.NeutralID;
                                                                    break;
                                                                case "1":
                                                                    model.AssessID = AppGlobal.PositiveID;
                                                                    break;
                                                                default:
                                                                    Config assess = _configResposistory.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.AssessType, assessString);
                                                                    if (assess == null)
                                                                    {
                                                                        assess = new Config();
                                                                        assess.CodeName = assessString;
                                                                        assess.Initialization(InitType.Insert, RequestUserID);
                                                                        _configResposistory.Create(assess);
                                                                    }
                                                                    model.AssessID = assess.ID;
                                                                    break;
                                                            }
                                                        }
                                                        Product product = _productRepository.GetByImageThumbnail(model.ImageThumbnail);
                                                        bool urlCode = true;
                                                        if (product == null)
                                                        {
                                                            model.GUICode = AppGlobal.InitGuiCode;
                                                            model.Source = AppGlobal.SourceAndi;
                                                            model.MetaTitle = AppGlobal.SetName(model.Title);
                                                            _productRepository.Create(model);
                                                            product = model;
                                                        }
                                                        else
                                                        {
                                                            urlCode = false;
                                                        }
                                                        if (product.ID > 0)
                                                        {
                                                            if (urlCode == true)
                                                            {
                                                                if (listProductPropertyURLCode.Count > 0)
                                                                {
                                                                    product.URLCode = AppGlobal.DomainMain + "Product/ViewContent/" + product.ID;
                                                                    _productRepository.Update(product.ID, product);
                                                                    for (int j = 0; j < listProductPropertyURLCode.Count; j++)
                                                                    {
                                                                        listProductPropertyURLCode[j].ParentID = product.ID;
                                                                        listProductPropertyURLCode[j].IndustryID = product.IndustryID;
                                                                        listProductPropertyURLCode[j].ArticleTypeID = product.ArticleTypeID;
                                                                        listProductPropertyURLCode[j].AssessID = product.AssessID;
                                                                        listProductPropertyURLCode[j].IsDaily = true;
                                                                        listProductPropertyURLCode[j].CategoryMain = categoryMain;
                                                                    }
                                                                    _productPropertyRepository.Range(listProductPropertyURLCode);
                                                                }
                                                                if (product.IsVideo == true)
                                                                {
                                                                    product.URLCode = AppGlobal.DomainMain + "Product/ViewContent/" + product.ID;
                                                                }
                                                                else
                                                                {
                                                                    try
                                                                    {
                                                                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(product.URLCode);
                                                                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                                                                        product.URLCode = response.ResponseUri.AbsoluteUri;
                                                                    }
                                                                    catch (WebException e)
                                                                    {
                                                                        product.URLCode = e.Response.ResponseUri.AbsoluteUri;
                                                                    }
                                                                }
                                                                _productRepository.Update(product.ID, product);
                                                            }
                                                            bool isCompany = true;
                                                            if (workSheet.Cells[i, 3].Value != null)
                                                            {
                                                                string company = workSheet.Cells[i, 3].Value.ToString().Trim();
                                                                if ((company.Contains("ngành")) || (company.Contains("industry")))
                                                                {
                                                                    isCompany = false;
                                                                }
                                                                else
                                                                {
                                                                    Membership membership = _membershipRepository.GetByAccount(company);
                                                                    if (membership == null)
                                                                    {
                                                                        membership = _membershipRepository.GetByCodeAndFullName(AppGlobal.CompanyName, company);
                                                                    }
                                                                    if (membership == null)
                                                                    {
                                                                        membership = new Membership();
                                                                        membership.Active = true;
                                                                        membership.Account = company;
                                                                        membership.FullName = company;
                                                                        membership.ParentID = AppGlobal.ParentIDCompetitor;
                                                                        if (workSheet.Cells[i, 2].Value != null)
                                                                        {
                                                                            string mainCategory = workSheet.Cells[i, 2].Value.ToString().Trim();
                                                                            if (mainCategory.Contains("ompetitor"))
                                                                            {
                                                                                membership.ParentID = AppGlobal.ParentIDCompetitor;
                                                                            }
                                                                        }
                                                                        membership.Initialization(InitType.Insert, RequestUserID);
                                                                        _membershipRepository.Create(membership);
                                                                        MembershipPermission membershipPermission = new MembershipPermission();
                                                                        membershipPermission.MembershipID = membership.ID;
                                                                        membershipPermission.IndustryID = baseViewModel.IndustryIDUploadScan;
                                                                        membershipPermission.Code = AppGlobal.Industry;
                                                                        membershipPermission.Initialization(InitType.Insert, RequestUserID);
                                                                        _membershipPermissionRepository.Create(membershipPermission);
                                                                    }
                                                                    if (baseViewModel.IsIndustryIDUploadAndiSource == true)
                                                                    {
                                                                        foreach (MembershipPermission item in _membershipPermissionRepository.GetByMembershipIDAndCodeToList(membership.ID, AppGlobal.Industry))
                                                                        {
                                                                            ProductProperty productProperty = new ProductProperty();
                                                                            productProperty.Initialization(InitType.Insert, RequestUserID);
                                                                            productProperty.AssessID = product.AssessID;
                                                                            productProperty.ParentID = product.ID;
                                                                            productProperty.GUICode = product.GUICode;
                                                                            productProperty.CompanyID = membership.ID;
                                                                            productProperty.ArticleTypeID = AppGlobal.TinDoanhNghiepID;
                                                                            productProperty.Code = AppGlobal.Company;
                                                                            productProperty.IndustryID = item.IndustryID;
                                                                            productProperty.IsDaily = true;
                                                                            productProperty.CategoryMain = categoryMain;
                                                                            if (_productPropertyRepository.IsExist(productProperty) == true)
                                                                            {
                                                                                _productPropertyRepository.Create(productProperty);
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        ProductProperty productProperty = new ProductProperty();
                                                                        productProperty.Initialization(InitType.Insert, RequestUserID);
                                                                        productProperty.AssessID = product.AssessID;
                                                                        productProperty.ParentID = product.ID;
                                                                        productProperty.GUICode = product.GUICode;
                                                                        productProperty.IndustryID = baseViewModel.IndustryIDUploadAndiSource;
                                                                        productProperty.CompanyID = membership.ID;
                                                                        productProperty.ArticleTypeID = AppGlobal.TinDoanhNghiepID;
                                                                        productProperty.Code = AppGlobal.Company;
                                                                        productProperty.IsDaily = true;
                                                                        productProperty.CategoryMain = categoryMain;
                                                                        if (_productPropertyRepository.IsExist(productProperty) == true)
                                                                        {
                                                                            _productPropertyRepository.Create(productProperty);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                isCompany = false;
                                                            }
                                                            if (isCompany == false)
                                                            {
                                                                ProductProperty productProperty = new ProductProperty();
                                                                productProperty.Initialization(InitType.Insert, RequestUserID);
                                                                productProperty.Code = AppGlobal.Industry;
                                                                productProperty.ArticleTypeID = AppGlobal.TinNganhID;
                                                                productProperty.AssessID = product.AssessID;
                                                                productProperty.ParentID = product.ID;
                                                                productProperty.GUICode = product.GUICode;
                                                                productProperty.IndustryID = baseViewModel.IndustryIDUploadAndiSource;
                                                                productProperty.IsDaily = true;
                                                                productProperty.CategoryMain = categoryMain;
                                                                if (_productPropertyRepository.IsExist(productProperty) == true)
                                                                {
                                                                    _productPropertyRepository.Create(productProperty);
                                                                }
                                                            }
                                                            _productPropertyRepository.Initialization();
                                                        }
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        string message = e.Message;
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
            catch
            {
            }
            if (string.IsNullOrEmpty(baseViewModel.ActionView))
            {
                baseViewModel.ActionView = "Upload";
            }
            return RedirectToAction(baseViewModel.ActionView);
        }
        public ActionResult UploadYounet(Commsights.MVC.Models.BaseViewModel baseViewModel)
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
                        fileName = AppGlobal.SourceYounet;
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
                                                int totalRows = workSheet.Dimension.Rows;
                                                for (int i = 2; i <= totalRows; i++)
                                                {
                                                    Product model = new Product();
                                                    model.Note = fileName;
                                                    model.Initialization(InitType.Insert, RequestUserID);
                                                    model.GUICode = AppGlobal.InitGuiCode;
                                                    model.Source = AppGlobal.SourceYounet;
                                                    model.DatePublish = DateTime.Now;
                                                    model.ParentID = AppGlobal.WebsiteID;
                                                    model.CategoryID = AppGlobal.WebsiteID;
                                                    model.PriceUnitID = 0;
                                                    model.Liked = 0;
                                                    model.Comment = 0;
                                                    model.Share = 0;
                                                    model.Reach = 0;
                                                    try
                                                    {
                                                        string source = "";
                                                        string datePublish = "";
                                                        string timePublish = "";
                                                        string assessString = "";
                                                        if (workSheet.Cells[i, 1].Value != null)
                                                        {
                                                            model.Tags = workSheet.Cells[i, 1].Value.ToString().Trim();
                                                        }
                                                        if (workSheet.Cells[i, 3].Value != null)
                                                        {
                                                            source = workSheet.Cells[i, 3].Value.ToString().Trim();
                                                            Config parent = _configResposistory.GetByGroupNameAndCodeAndTitle(AppGlobal.CRM, AppGlobal.Website, source);
                                                            if (parent == null)
                                                            {
                                                                parent = _configResposistory.GetByGroupNameAndCodeAndTitle(AppGlobal.CRM, AppGlobal.PressList, source);
                                                            }
                                                            if (parent == null)
                                                            {
                                                                parent = new Config();
                                                                parent.GroupName = AppGlobal.CRM;
                                                                parent.Code = AppGlobal.Website;
                                                                parent.Initialization(InitType.Insert, RequestUserID);
                                                                parent.ParentID = AppGlobal.ParentID;
                                                                parent.CodeName = source;
                                                                parent.Title = source;
                                                                parent.URLFull = source;
                                                                parent.Color = AppGlobal.AdvertisementValue;
                                                                _configResposistory.Create(parent);
                                                            }
                                                            model.ParentID = parent.ID;
                                                            model.CategoryID = parent.ID;
                                                        }
                                                        if (workSheet.Cells[i, 5].Value != null)
                                                        {
                                                            model.URLCode = workSheet.Cells[i, 5].Value.ToString().Trim();
                                                        }
                                                        if (workSheet.Cells[i, 10].Value != null)
                                                        {
                                                            model.Author = workSheet.Cells[i, 10].Value.ToString().Trim();
                                                        }
                                                        if (workSheet.Cells[i, 14].Value != null)
                                                        {
                                                            model.Title = workSheet.Cells[i, 14].Value.ToString().Trim();
                                                            if (model.Title.Equals(model.Title.ToUpper()))
                                                            {
                                                                model.Title = AppGlobal.ToUpperFirstLetter(model.Title);
                                                            }
                                                        }
                                                        if (workSheet.Cells[i, 15].Value != null)
                                                        {
                                                            model.ContentMain = workSheet.Cells[i, 15].Value.ToString().Trim();
                                                        }
                                                        if (workSheet.Cells[i, 16].Value != null)
                                                        {
                                                            datePublish = workSheet.Cells[i, 16].Value.ToString().Trim();
                                                        }
                                                        if (workSheet.Cells[i, 17].Value != null)
                                                        {
                                                            timePublish = workSheet.Cells[i, 17].Value.ToString().Trim();
                                                        }
                                                        if (workSheet.Cells[i, 19].Value != null)
                                                        {
                                                            try
                                                            {
                                                                model.Liked = int.Parse(workSheet.Cells[i, 19].Value.ToString().Trim());
                                                            }
                                                            catch
                                                            {
                                                            }
                                                        }
                                                        if (workSheet.Cells[i, 20].Value != null)
                                                        {
                                                            try
                                                            {
                                                                model.Comment = int.Parse(workSheet.Cells[i, 20].Value.ToString().Trim());
                                                            }
                                                            catch
                                                            {
                                                            }
                                                        }
                                                        if (workSheet.Cells[i, 21].Value != null)
                                                        {
                                                            try
                                                            {
                                                                model.Share = int.Parse(workSheet.Cells[i, 21].Value.ToString().Trim());
                                                            }
                                                            catch
                                                            {
                                                            }
                                                        }
                                                        if (workSheet.Cells[i, 22].Value != null)
                                                        {
                                                            try
                                                            {
                                                                model.Reach = int.Parse(workSheet.Cells[i, 22].Value.ToString().Trim());
                                                            }
                                                            catch
                                                            {
                                                            }
                                                        }
                                                        try
                                                        {
                                                            int year = int.Parse(datePublish.Split('/')[2]);
                                                            int month = int.Parse(datePublish.Split('/')[1]);
                                                            int day = int.Parse(datePublish.Split('/')[0]);
                                                            int hour = int.Parse(timePublish.Split(':')[0]);
                                                            int minutes = int.Parse(timePublish.Split(':')[1]);
                                                            int second = 0;
                                                            model.DatePublish = new DateTime(year, month, day, hour, minutes, second);
                                                        }
                                                        catch
                                                        {
                                                            try
                                                            {
                                                                DateTime DateTimeStandard = new DateTime(1899, 12, 30);
                                                                model.DatePublish = DateTimeStandard.AddDays(int.Parse(datePublish));
                                                            }
                                                            catch
                                                            {
                                                            }
                                                        }
                                                        if (!string.IsNullOrEmpty(model.URLCode))
                                                        {
                                                            Product product = _productRepository.GetByURLCode(model.URLCode);
                                                            if (product == null)
                                                            {
                                                                model.MetaTitle = AppGlobal.SetName(model.Title);
                                                                _productRepository.Create(model);
                                                                product = model;
                                                            }
                                                            if (product.ID > 0)
                                                            {
                                                                List<ProductProperty> listProductProperty = new List<ProductProperty>();
                                                                _productRepository.FilterProduct(product, listProductProperty, RequestUserID);
                                                                if (listProductProperty.Count > 0)
                                                                {
                                                                    if (workSheet.Cells[i, 6].Value != null)
                                                                    {
                                                                        assessString = workSheet.Cells[i, 6].Value.ToString().Trim();
                                                                        switch (assessString)
                                                                        {
                                                                            case "-1":
                                                                                model.AssessID = AppGlobal.NegativeID;
                                                                                break;
                                                                            case "0":
                                                                                model.AssessID = AppGlobal.NeutralID;
                                                                                break;
                                                                            case "1":
                                                                                model.AssessID = AppGlobal.PositiveID;
                                                                                break;
                                                                            default:
                                                                                Config assess = _configResposistory.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.AssessType, assessString);
                                                                                if (assess == null)
                                                                                {
                                                                                    assess = new Config();
                                                                                    assess.CodeName = assessString;
                                                                                    assess.Initialization(InitType.Insert, RequestUserID);
                                                                                    _configResposistory.Create(assess);
                                                                                }
                                                                                model.AssessID = assess.ID;
                                                                                break;
                                                                        }
                                                                    }
                                                                    for (int m = 0; m < listProductProperty.Count; m++)
                                                                    {
                                                                        listProductProperty[m].ParentID = model.ID;
                                                                        listProductProperty[m].AssessID = model.AssessID;
                                                                        listProductProperty[m].IsDaily = true;
                                                                        if (listProductProperty[m].IndustryID > 0)
                                                                        {
                                                                        }
                                                                        else
                                                                        {
                                                                            listProductProperty[m].IndustryID = baseViewModel.IndustryIDUploadYounet;
                                                                        }
                                                                        if (_productPropertyRepository.IsExist(listProductProperty[m]) == true)
                                                                        {
                                                                            _productPropertyRepository.Create(listProductProperty[m]);
                                                                        }
                                                                    }
                                                                    _productPropertyRepository.Initialization();
                                                                }
                                                            }
                                                        }
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        string message = e.Message;
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
            catch
            {
            }
            if (string.IsNullOrEmpty(baseViewModel.ActionView))
            {
                baseViewModel.ActionView = "Upload";
            }
            return RedirectToAction(baseViewModel.ActionView);
        }
        public ActionResult UploadGoogleSearch(Commsights.MVC.Models.BaseViewModel baseViewModel)
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
                        fileName = AppGlobal.SourceGoogle;
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
                                                int totalRows = workSheet.Dimension.Rows;
                                                BaiVietUploadCount baiVietUploadCount = new BaiVietUploadCount();
                                                baiVietUploadCount.Count = totalRows - 1;
                                                baiVietUploadCount.IndustryID = baseViewModel.IndustryIDUploadGoogleSearch;
                                                baiVietUploadCount.Initialization(InitType.Insert, RequestUserID);
                                                _baiVietUploadCountRepository.Create(baiVietUploadCount);
                                                for (int i = 2; i <= totalRows; i++)
                                                {
                                                    try
                                                    {
                                                        Product model = new Product();
                                                        model.Note = fileName;
                                                        model.Initialization(InitType.Insert, RequestUserID);
                                                        string datePublish = "";
                                                        if (workSheet.Cells[i, 1].Value != null)
                                                        {
                                                            datePublish = workSheet.Cells[i, 1].Value.ToString().Trim();
                                                            try
                                                            {
                                                                model.DatePublish = DateTime.Parse(datePublish);
                                                            }
                                                            catch
                                                            {
                                                                try
                                                                {
                                                                    int year = int.Parse(datePublish.Split('/')[2]);
                                                                    int month = int.Parse(datePublish.Split('/')[0]);
                                                                    int day = int.Parse(datePublish.Split('/')[1]);
                                                                    model.DatePublish = new DateTime(year, month, day, 0, 0, 0);
                                                                }
                                                                catch
                                                                {
                                                                    try
                                                                    {
                                                                        int year = int.Parse(datePublish.Split('/')[2]);
                                                                        int month = int.Parse(datePublish.Split('/')[1]);
                                                                        int day = int.Parse(datePublish.Split('/')[0]);
                                                                        model.DatePublish = new DateTime(year, month, day, 0, 0, 0);
                                                                    }
                                                                    catch
                                                                    {
                                                                        try
                                                                        {
                                                                            DateTime DateTimeStandard = new DateTime(1899, 12, 30);
                                                                            model.DatePublish = DateTimeStandard.AddDays(int.Parse(datePublish));
                                                                        }
                                                                        catch
                                                                        {
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        if (workSheet.Cells[i, 4].Value != null)
                                                        {
                                                            model.Title = workSheet.Cells[i, 4].Value.ToString().Trim();
                                                            if (model.Title.Equals(model.Title.ToUpper()))
                                                            {
                                                                model.Title = AppGlobal.ToUpperFirstLetter(model.Title);
                                                            }
                                                            if (workSheet.Cells[i, 4].Hyperlink != null)
                                                            {
                                                                model.URLCode = workSheet.Cells[i, 4].Hyperlink.AbsoluteUri.Trim();
                                                            }
                                                        }
                                                        if (workSheet.Cells[i, 5].Value != null)
                                                        {
                                                            model.TitleEnglish = workSheet.Cells[i, 5].Value.ToString().Trim();
                                                            if (model.TitleEnglish.Equals(model.TitleEnglish.ToUpper()))
                                                            {
                                                                model.TitleEnglish = AppGlobal.ToUpperFirstLetter(model.TitleEnglish);
                                                            }
                                                            if (workSheet.Cells[i, 5].Hyperlink != null)
                                                            {
                                                                model.URLCode = workSheet.Cells[i, 5].Hyperlink.AbsoluteUri.Trim();
                                                            }
                                                        }
                                                        if (workSheet.Cells[i, 6].Value != null)
                                                        {
                                                            model.URLCode = workSheet.Cells[i, 6].Value.ToString().Trim();
                                                        }
                                                        if (workSheet.Cells[i, 7].Value != null)
                                                        {
                                                            model.Description = workSheet.Cells[i, 7].Value.ToString().Trim();
                                                        }
                                                        if (workSheet.Cells[i, 8].Value != null)
                                                        {
                                                            model.Author = workSheet.Cells[i, 8].Value.ToString().Trim();
                                                        }
                                                        if (!string.IsNullOrEmpty(model.URLCode))
                                                        {
                                                            BaiVietUpload baiVietUpload = new BaiVietUpload();
                                                            baiVietUpload.ParentID = baiVietUploadCount.ID;
                                                            baiVietUpload.Title = model.Title;
                                                            baiVietUpload.URLCode = model.URLCode;
                                                            baiVietUpload.Initialization(InitType.Insert, RequestUserID);
                                                            _baiVietUploadRepository.Create(baiVietUpload);
                                                            Product product = _productRepository.GetByURLCode(model.URLCode);
                                                            if (product == null)
                                                            {
                                                                model.MetaTitle = AppGlobal.SetName(model.Title);
                                                                model.Source = AppGlobal.SourceGoogle;
                                                                model.GUICode = AppGlobal.InitGuiCode;
                                                                model.FileName = AppGlobal.SetDomainByURL(model.URLCode);
                                                                Config website = _configResposistory.GetByGroupNameAndCodeAndTitle(AppGlobal.CRM, AppGlobal.Website, model.FileName);
                                                                if (website == null)
                                                                {
                                                                    website = new Config();
                                                                    website.GroupName = AppGlobal.CRM;
                                                                    website.Code = AppGlobal.Website;
                                                                    website.Title = model.FileName;
                                                                    website.URLFull = model.FileName;
                                                                    website.ParentID = AppGlobal.ParentID;
                                                                    website.Color = AppGlobal.AdvertisementValue;
                                                                    website.Initialization(InitType.Insert, RequestUserID);
                                                                    _configResposistory.Create(website);
                                                                }
                                                                model.ParentID = website.ID;
                                                                _productRepository.Create(model);
                                                                product = model;
                                                            }
                                                            if (product.ID > 0)
                                                            {
                                                                int membershipPermissionProductID = 0;
                                                                int membershipPermissionSegmentID = 0;
                                                                if (workSheet.Cells[i, 10].Value != null)
                                                                {
                                                                    string productName = workSheet.Cells[i, 10].Value.ToString().Trim();
                                                                    MembershipPermission membershipPermission = _membershipPermissionRepository.GetByProductName(productName);
                                                                    if (membershipPermission != null)
                                                                    {
                                                                        membershipPermissionProductID = membershipPermission.ID;
                                                                        membershipPermissionSegmentID = membershipPermission.SegmentID.Value;
                                                                    }
                                                                }

                                                                if (workSheet.Cells[i, 9].Value != null)
                                                                {
                                                                    string segmentName = workSheet.Cells[i, 9].Value.ToString().Trim();
                                                                    Config config = _configResposistory.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.Segment, segmentName);
                                                                    if (config != null)
                                                                    {
                                                                        membershipPermissionSegmentID = config.ID;
                                                                    }
                                                                }
                                                                int assessID = AppGlobal.AssessID;
                                                                if (workSheet.Cells[i, 3].Value != null)
                                                                {
                                                                    string assessString = workSheet.Cells[i, 3].Value.ToString().Trim();
                                                                    switch (assessString)
                                                                    {
                                                                        case "-1":
                                                                            assessID = AppGlobal.NegativeID;
                                                                            break;
                                                                        case "0":
                                                                            assessID = AppGlobal.NeutralID;
                                                                            break;
                                                                        case "1":
                                                                            assessID = AppGlobal.PositiveID;
                                                                            break;
                                                                        default:
                                                                            Config assess = _configResposistory.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.AssessType, assessString);
                                                                            if (assess == null)
                                                                            {
                                                                                assess = new Config();
                                                                                assess.CodeName = assessString;
                                                                                assess.Initialization(InitType.Insert, RequestUserID);
                                                                                _configResposistory.Create(assess);
                                                                            }
                                                                            assessID = assess.ID;
                                                                            break;
                                                                    }
                                                                }
                                                                bool isCompany = true;
                                                                if (workSheet.Cells[i, 2].Value != null)
                                                                {
                                                                    string companyName = workSheet.Cells[i, 2].Value.ToString().Trim();
                                                                    if ((companyName.Contains("ngành")) || (companyName.Contains("industry")))
                                                                    {
                                                                        isCompany = false;
                                                                    }
                                                                    else
                                                                    {
                                                                        Membership company = _membershipRepository.GetByAccount(companyName);
                                                                        if (company == null)
                                                                        {
                                                                            company = _membershipRepository.GetByCodeAndFullName(AppGlobal.CompanyName, companyName);
                                                                        }
                                                                        if (company == null)
                                                                        {
                                                                            company = new Membership();
                                                                            company.Active = true;
                                                                            company.Account = companyName;
                                                                            company.FullName = companyName;
                                                                            company.ParentID = AppGlobal.ParentIDCompetitor;
                                                                            company.Initialization(InitType.Insert, RequestUserID);
                                                                            _membershipRepository.Create(company);
                                                                            MembershipPermission membershipPermission = new MembershipPermission();
                                                                            membershipPermission.MembershipID = company.ID;
                                                                            membershipPermission.IndustryID = baseViewModel.IndustryIDUploadGoogleSearch;
                                                                            membershipPermission.Code = AppGlobal.Industry;
                                                                            membershipPermission.Initialization(InitType.Insert, RequestUserID);
                                                                            _membershipPermissionRepository.Create(membershipPermission);
                                                                        }
                                                                        if (baseViewModel.IsIndustryIDUploadGoogleSearch == true)
                                                                        {
                                                                            foreach (MembershipPermission item in _membershipPermissionRepository.GetByMembershipIDAndCodeToList(company.ID, AppGlobal.Industry))
                                                                            {
                                                                                ProductProperty productProperty = new ProductProperty();
                                                                                productProperty.Initialization(InitType.Insert, RequestUserID);
                                                                                productProperty.ProductID = membershipPermissionProductID;
                                                                                productProperty.SegmentID = membershipPermissionSegmentID;
                                                                                productProperty.ParentID = product.ID;
                                                                                productProperty.ParentID = product.ID;
                                                                                productProperty.GUICode = product.GUICode;
                                                                                productProperty.AssessID = assessID;
                                                                                productProperty.IndustryID = item.IndustryID;
                                                                                productProperty.CompanyID = company.ID;
                                                                                productProperty.ArticleTypeID = AppGlobal.TinDoanhNghiepID;
                                                                                productProperty.Code = AppGlobal.Company;
                                                                                productProperty.IsDaily = true;
                                                                                if (_productPropertyRepository.IsExist(productProperty) == true)
                                                                                {
                                                                                    _productPropertyRepository.Create(productProperty);
                                                                                }
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            ProductProperty productProperty = new ProductProperty();
                                                                            productProperty.Initialization(InitType.Insert, RequestUserID);
                                                                            productProperty.ProductID = membershipPermissionProductID;
                                                                            productProperty.SegmentID = membershipPermissionSegmentID;
                                                                            productProperty.ParentID = product.ID;
                                                                            productProperty.GUICode = product.GUICode;
                                                                            productProperty.AssessID = assessID;
                                                                            productProperty.IndustryID = baseViewModel.IndustryIDUploadGoogleSearch;
                                                                            productProperty.CompanyID = company.ID;
                                                                            productProperty.ArticleTypeID = AppGlobal.TinDoanhNghiepID;
                                                                            productProperty.Code = AppGlobal.Company;
                                                                            productProperty.IsDaily = true;
                                                                            if (_productPropertyRepository.IsExist(productProperty) == true)
                                                                            {
                                                                                _productPropertyRepository.Create(productProperty);
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    isCompany = false;
                                                                }
                                                                if (isCompany == false)
                                                                {
                                                                    ProductProperty productProperty = new ProductProperty();
                                                                    productProperty.Initialization(InitType.Insert, RequestUserID);
                                                                    productProperty.ProductID = membershipPermissionProductID;
                                                                    productProperty.SegmentID = membershipPermissionSegmentID;
                                                                    productProperty.ParentID = product.ID;
                                                                    productProperty.GUICode = product.GUICode;
                                                                    productProperty.ArticleTypeID = AppGlobal.TinNganhID;
                                                                    productProperty.AssessID = assessID;
                                                                    productProperty.Code = AppGlobal.Industry;
                                                                    productProperty.IndustryID = baseViewModel.IndustryIDUploadGoogleSearch;
                                                                    productProperty.IsDaily = true;
                                                                    if (_productPropertyRepository.IsExist(productProperty) == true)
                                                                    {
                                                                        _productPropertyRepository.Create(productProperty);
                                                                    }
                                                                }
                                                                _productPropertyRepository.Initialization();
                                                            }
                                                        }
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        string message = e.Message;
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
            catch
            {
            }
            if (string.IsNullOrEmpty(baseViewModel.ActionView))
            {
                baseViewModel.ActionView = "Upload";
            }
            return RedirectToAction(baseViewModel.ActionView);
        }
        public ActionResult UploadGoogleSearchAndAutoFilter(Commsights.MVC.Models.BaseViewModel baseViewModel)
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
                        fileName = AppGlobal.SourceGoogle;
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
                                                int totalRows = workSheet.Dimension.Rows;
                                                BaiVietUploadCount baiVietUploadCount = new BaiVietUploadCount();
                                                baiVietUploadCount.Count = totalRows - 1;
                                                baiVietUploadCount.IndustryID = baseViewModel.IndustryIDUploadGoogleSearchAndAutoFilter;
                                                baiVietUploadCount.Initialization(InitType.Insert, RequestUserID);
                                                _baiVietUploadCountRepository.Create(baiVietUploadCount);
                                                for (int i = 2; i <= totalRows; i++)
                                                {
                                                    try
                                                    {
                                                        Product model = new Product();
                                                        model.Note = fileName;
                                                        model.Initialization(InitType.Insert, RequestUserID);
                                                        if (workSheet.Cells[i, 1].Value != null)
                                                        {
                                                            model.URLCode = workSheet.Cells[i, 1].Value.ToString().Trim();
                                                        }
                                                        if (!string.IsNullOrEmpty(model.URLCode))
                                                        {
                                                            BaiVietUpload baiVietUpload = new BaiVietUpload();
                                                            baiVietUpload.ParentID = baiVietUploadCount.ID;
                                                            baiVietUpload.Title = model.Title;
                                                            baiVietUpload.URLCode = model.URLCode;
                                                            baiVietUpload.Initialization(InitType.Insert, RequestUserID);
                                                            _baiVietUploadRepository.Create(baiVietUpload);

                                                            Uri website = new Uri(model.URLCode);
                                                            Config config = _configResposistory.GetByGroupNameAndCodeAndTitle(AppGlobal.CRM, AppGlobal.Website, website.Authority);
                                                            if ((config == null) || (config.ID == 0))
                                                            {
                                                                config.GroupName = AppGlobal.CRM;
                                                                config.Code = AppGlobal.Website;
                                                                config.Title = website.Authority;
                                                                config.URLFull = website.Scheme + "/" + website.Authority;
                                                                config.Initialization(InitType.Insert, RequestUserID);
                                                                _configResposistory.Create(config);
                                                            }
                                                            if ((config != null) && (config.ID > 0))
                                                            {
                                                                Product product = _productRepository.GetByURLCode(model.URLCode);
                                                                if ((product == null) || (product.ID == 0))
                                                                {
                                                                    product = new Product();
                                                                    product.Title = model.Title;
                                                                    product.Description = model.Description;
                                                                    product.DatePublish = model.DatePublish;
                                                                    product.IsFilter = true;
                                                                    product.ParentID = config.ID;
                                                                    product.CategoryID = config.ID;
                                                                    product.Source = AppGlobal.SourceGoogle;
                                                                    product.URLCode = model.URLCode;
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
                                                                    product.Title = AppGlobal.FinderTitle(product.URLCode);
                                                                }
                                                                if (string.IsNullOrEmpty(product.Description))
                                                                {
                                                                    string html = AppGlobal.FinderHTMLContent(product.URLCode);
                                                                    AppGlobal.FinderContentAndDatePublish002(html, product);
                                                                }
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
                                                            }
                                                        }
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        string message = e.Message;
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
            catch
            {
            }
            if (string.IsNullOrEmpty(baseViewModel.ActionView))
            {
                baseViewModel.ActionView = "Upload";
            }
            return RedirectToAction("SearchByEmployeeID", "CodeData");
        }
        public ActionResult UploadAndiBad(Commsights.MVC.Models.BaseViewModel baseViewModel)
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
                        fileName = AppGlobal.SourceAndi;
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
                                                int totalRows = workSheet.Dimension.Rows;
                                                for (int i = 2; i <= totalRows; i++)
                                                {
                                                    Product model = new Product();
                                                    if (workSheet.Cells[i, 1].Value != null)
                                                    {
                                                        model.Title = workSheet.Cells[i, 1].Value.ToString().Trim();
                                                        if (workSheet.Cells[i, 1].Hyperlink != null)
                                                        {
                                                            model.URLCode = workSheet.Cells[i, 1].Hyperlink.AbsoluteUri.Trim();
                                                        }
                                                    }
                                                    if (workSheet.Cells[i, 2].Value != null)
                                                    {
                                                        model.URLCode = workSheet.Cells[i, 2].Value.ToString().Trim();
                                                        if (workSheet.Cells[i, 2].Hyperlink != null)
                                                        {
                                                            model.URLCode = workSheet.Cells[i, 1].Hyperlink.AbsoluteUri.Trim();
                                                        }
                                                    }
                                                    try
                                                    {
                                                        model.ID = int.Parse(model.URLCode.Split('/')[model.URLCode.Split('/').Length - 1]);
                                                        Product product = _productRepository.GetByTitleAndSource(model.Title, AppGlobal.SourceAndi);
                                                        if (product != null)
                                                        {
                                                            product.PriceUnitID = model.ID;
                                                            _productRepository.Update(product.ID, product);
                                                        }
                                                    }
                                                    catch (Exception e)
                                                    {
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
            catch
            {
            }
            if (string.IsNullOrEmpty(baseViewModel.ActionView))
            {
                baseViewModel.ActionView = "Upload";
            }
            return RedirectToAction(baseViewModel.ActionView);
        }
        public ActionResult UploadAdValue()
        {
            string action = "Upload";
            string controller = "Report";

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
                    fileName = "AdValue";
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
                                            int totalRows = workSheet.Dimension.Rows;
                                            for (int i = 2; i <= totalRows; i++)
                                            {
                                                string title = "";
                                                int color = 0;
                                                if (workSheet.Cells[i, 1].Value != null)
                                                {
                                                    title = workSheet.Cells[i, 1].Value.ToString().Trim();
                                                }
                                                if (workSheet.Cells[i, 2].Value != null)
                                                {
                                                    string adValue = workSheet.Cells[i, 2].Value.ToString().Trim();
                                                    try
                                                    {
                                                        color = int.Parse(adValue);
                                                    }
                                                    catch
                                                    {
                                                    }
                                                }
                                                if (!string.IsNullOrEmpty(title))
                                                {
                                                    title = title.Trim();
                                                    title = title.ToLower();
                                                    _configResposistory.UpdateByGroupNameAndCodeAndTitleAndColor(AppGlobal.CRM, AppGlobal.Website, title, color);
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

            return RedirectToAction(action, controller);
        }

        public string ExportExcelReportDailyByIDList(CancellationToken cancellationToken, string IDList)
        {
            string excelName = @"Search_" + AppGlobal.DateTimeCode + ".xlsx";
            var streamExport = new MemoryStream();
            using (var package = new ExcelPackage(streamExport))
            {
                Color color = Color.FromArgb(int.Parse("#c00000".Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier));
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells[1, 1].Value = "Date";
                workSheet.Cells[1, 1].Style.Font.Bold = true;
                workSheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
                workSheet.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(color);
                workSheet.Cells[1, 1].Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 1].Style.Font.Size = 11;
                workSheet.Cells[1, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 1].Style.Border.Top.Color.SetColor(Color.Black);
                workSheet.Cells[1, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 1].Style.Border.Left.Color.SetColor(Color.Black);
                workSheet.Cells[1, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 1].Style.Border.Right.Color.SetColor(Color.Black);
                workSheet.Cells[1, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

                workSheet.Cells[1, 2].Value = "Company";
                workSheet.Cells[1, 2].Style.Font.Bold = true;
                workSheet.Cells[1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 2].Style.Font.Color.SetColor(System.Drawing.Color.White);
                workSheet.Cells[1, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 2].Style.Fill.BackgroundColor.SetColor(color);
                workSheet.Cells[1, 2].Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 2].Style.Font.Size = 11;
                workSheet.Cells[1, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 2].Style.Border.Top.Color.SetColor(Color.Black);
                workSheet.Cells[1, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 2].Style.Border.Left.Color.SetColor(Color.Black);
                workSheet.Cells[1, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 2].Style.Border.Right.Color.SetColor(Color.Black);
                workSheet.Cells[1, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

                workSheet.Cells[1, 3].Value = "Sentiment";
                workSheet.Cells[1, 3].Style.Font.Bold = true;
                workSheet.Cells[1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 3].Style.Font.Color.SetColor(System.Drawing.Color.White);
                workSheet.Cells[1, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 3].Style.Fill.BackgroundColor.SetColor(color);
                workSheet.Cells[1, 3].Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 3].Style.Font.Size = 11;
                workSheet.Cells[1, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 3].Style.Border.Top.Color.SetColor(Color.Black);
                workSheet.Cells[1, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 3].Style.Border.Left.Color.SetColor(Color.Black);
                workSheet.Cells[1, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 3].Style.Border.Right.Color.SetColor(Color.Black);
                workSheet.Cells[1, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 3].Style.Border.Bottom.Color.SetColor(Color.Black);

                workSheet.Cells[1, 4].Value = "Headline (Vie)";
                workSheet.Cells[1, 4].Style.Font.Bold = true;
                workSheet.Cells[1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 4].Style.Font.Color.SetColor(System.Drawing.Color.White);
                workSheet.Cells[1, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 4].Style.Fill.BackgroundColor.SetColor(color);
                workSheet.Cells[1, 4].Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 4].Style.Font.Size = 11;
                workSheet.Cells[1, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 4].Style.Border.Top.Color.SetColor(Color.Black);
                workSheet.Cells[1, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 4].Style.Border.Left.Color.SetColor(Color.Black);
                workSheet.Cells[1, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 4].Style.Border.Right.Color.SetColor(Color.Black);
                workSheet.Cells[1, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 4].Style.Border.Bottom.Color.SetColor(Color.Black);

                workSheet.Cells[1, 5].Value = "Headline (Eng)";
                workSheet.Cells[1, 5].Style.Font.Bold = true;
                workSheet.Cells[1, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 5].Style.Font.Color.SetColor(System.Drawing.Color.White);
                workSheet.Cells[1, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 5].Style.Fill.BackgroundColor.SetColor(color);
                workSheet.Cells[1, 5].Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 5].Style.Font.Size = 11;
                workSheet.Cells[1, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 5].Style.Border.Top.Color.SetColor(Color.Black);
                workSheet.Cells[1, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 5].Style.Border.Left.Color.SetColor(Color.Black);
                workSheet.Cells[1, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 5].Style.Border.Right.Color.SetColor(Color.Black);
                workSheet.Cells[1, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 5].Style.Border.Bottom.Color.SetColor(Color.Black);

                workSheet.Cells[1, 6].Value = "URL";
                workSheet.Cells[1, 6].Style.Font.Bold = true;
                workSheet.Cells[1, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 6].Style.Font.Color.SetColor(System.Drawing.Color.White);
                workSheet.Cells[1, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 6].Style.Fill.BackgroundColor.SetColor(color);
                workSheet.Cells[1, 6].Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 6].Style.Font.Size = 11;
                workSheet.Cells[1, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 6].Style.Border.Top.Color.SetColor(Color.Black);
                workSheet.Cells[1, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 6].Style.Border.Left.Color.SetColor(Color.Black);
                workSheet.Cells[1, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 6].Style.Border.Right.Color.SetColor(Color.Black);
                workSheet.Cells[1, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 6].Style.Border.Bottom.Color.SetColor(Color.Black);

                workSheet.Cells[1, 7].Value = "Content";
                workSheet.Cells[1, 7].Style.Font.Bold = true;
                workSheet.Cells[1, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 7].Style.Font.Color.SetColor(System.Drawing.Color.White);
                workSheet.Cells[1, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 7].Style.Fill.BackgroundColor.SetColor(color);
                workSheet.Cells[1, 7].Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 7].Style.Font.Size = 11;
                workSheet.Cells[1, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 7].Style.Border.Top.Color.SetColor(Color.Black);
                workSheet.Cells[1, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 7].Style.Border.Left.Color.SetColor(Color.Black);
                workSheet.Cells[1, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 7].Style.Border.Right.Color.SetColor(Color.Black);
                workSheet.Cells[1, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 7].Style.Border.Bottom.Color.SetColor(Color.Black);

                workSheet.Cells[1, 8].Value = "Author";
                workSheet.Cells[1, 8].Style.Font.Bold = true;
                workSheet.Cells[1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 8].Style.Font.Color.SetColor(System.Drawing.Color.White);
                workSheet.Cells[1, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 8].Style.Fill.BackgroundColor.SetColor(color);
                workSheet.Cells[1, 8].Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 8].Style.Font.Size = 11;
                workSheet.Cells[1, 8].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 8].Style.Border.Top.Color.SetColor(Color.Black);
                workSheet.Cells[1, 8].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 8].Style.Border.Left.Color.SetColor(Color.Black);
                workSheet.Cells[1, 8].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 8].Style.Border.Right.Color.SetColor(Color.Black);
                workSheet.Cells[1, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 8].Style.Border.Bottom.Color.SetColor(Color.Black);

                workSheet.Cells[1, 9].Value = "Segment";
                workSheet.Cells[1, 9].Style.Font.Bold = true;
                workSheet.Cells[1, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 9].Style.Font.Color.SetColor(System.Drawing.Color.White);
                workSheet.Cells[1, 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 9].Style.Fill.BackgroundColor.SetColor(color);
                workSheet.Cells[1, 9].Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 9].Style.Font.Size = 11;
                workSheet.Cells[1, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 9].Style.Border.Top.Color.SetColor(Color.Black);
                workSheet.Cells[1, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 9].Style.Border.Left.Color.SetColor(Color.Black);
                workSheet.Cells[1, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 9].Style.Border.Right.Color.SetColor(Color.Black);
                workSheet.Cells[1, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 9].Style.Border.Bottom.Color.SetColor(Color.Black);

                workSheet.Cells[1, 10].Value = "Product";
                workSheet.Cells[1, 10].Style.Font.Bold = true;
                workSheet.Cells[1, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 10].Style.Font.Color.SetColor(System.Drawing.Color.White);
                workSheet.Cells[1, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 10].Style.Fill.BackgroundColor.SetColor(color);
                workSheet.Cells[1, 10].Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 10].Style.Font.Size = 11;
                workSheet.Cells[1, 10].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 10].Style.Border.Top.Color.SetColor(Color.Black);
                workSheet.Cells[1, 10].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 10].Style.Border.Left.Color.SetColor(Color.Black);
                workSheet.Cells[1, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 10].Style.Border.Right.Color.SetColor(Color.Black);
                workSheet.Cells[1, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 10].Style.Border.Bottom.Color.SetColor(Color.Black);

                workSheet.Cells[1, 11].Value = "Media";
                workSheet.Cells[1, 11].Style.Font.Bold = true;
                workSheet.Cells[1, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 11].Style.Font.Color.SetColor(System.Drawing.Color.White);
                workSheet.Cells[1, 11].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 11].Style.Fill.BackgroundColor.SetColor(color);
                workSheet.Cells[1, 11].Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 11].Style.Font.Size = 11;
                workSheet.Cells[1, 11].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 11].Style.Border.Top.Color.SetColor(Color.Black);
                workSheet.Cells[1, 11].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 11].Style.Border.Left.Color.SetColor(Color.Black);
                workSheet.Cells[1, 11].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 11].Style.Border.Right.Color.SetColor(Color.Black);
                workSheet.Cells[1, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 11].Style.Border.Bottom.Color.SetColor(Color.Black);

                if (!string.IsNullOrEmpty(IDList))
                {
                    List<ProductDataTransfer> listData = _reportRepository.GetByIDListToList(IDList);
                    int row = 2;
                    foreach (ProductDataTransfer item in listData)
                    {
                        for (int column = 1; column < 12; column++)
                        {
                            switch (column)
                            {
                                case 1:
                                    workSheet.Cells[row, column].Value = item.DatePublish;
                                    workSheet.Cells[row, column].Style.Numberformat.Format = "mm/dd/yyyy";
                                    break;
                                case 2:
                                    workSheet.Cells[row, column].Value = ".";
                                    break;
                                case 3:
                                    workSheet.Cells[row, column].Value = ".";
                                    break;
                                case 4:
                                    workSheet.Cells[row, column].Value = item.Title;
                                    if (!string.IsNullOrEmpty(item.Title))
                                    {
                                        workSheet.Cells[row, column].Hyperlink = new Uri(item.URLCode);
                                        workSheet.Cells[row, column].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                    }
                                    break;
                                case 5:
                                    workSheet.Cells[row, column].Value = item.TitleEnglish;
                                    if (!string.IsNullOrEmpty(item.TitleEnglish))
                                    {
                                        workSheet.Cells[row, column].Hyperlink = new Uri(item.URLCode);
                                        workSheet.Cells[row, column].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                    }
                                    else
                                    {
                                        workSheet.Cells[row, column].Value = ".";
                                    }
                                    break;
                                case 6:
                                    if (!string.IsNullOrEmpty(item.URLCode))
                                    {
                                        workSheet.Cells[row, column].Value = item.URLCode;
                                        workSheet.Cells[row, column].Hyperlink = new Uri(item.URLCode);
                                        workSheet.Cells[row, column].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                    }
                                    break;
                                case 7:
                                    workSheet.Cells[row, column].Value = item.Description;
                                    break;
                                case 8:
                                    workSheet.Cells[row, column].Value = ".";
                                    break;
                                case 9:
                                    workSheet.Cells[row, column].Value = ".";
                                    break;
                                case 10:
                                    workSheet.Cells[row, column].Value = ".";
                                    break;
                                case 11:
                                    workSheet.Cells[row, column].Value = item.Media;
                                    break;
                            }
                            workSheet.Cells[row, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            workSheet.Cells[row, column].Style.Font.Name = "Times New Roman";
                            workSheet.Cells[row, column].Style.Font.Size = 11;
                            workSheet.Cells[row, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[row, column].Style.Border.Top.Color.SetColor(Color.Black);
                            workSheet.Cells[row, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[row, column].Style.Border.Left.Color.SetColor(Color.Black);
                            workSheet.Cells[row, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[row, column].Style.Border.Right.Color.SetColor(Color.Black);
                            workSheet.Cells[row, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[row, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                        }

                        row = row + 1;
                    }
                }
                workSheet.Column(1).AutoFit();
                workSheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Column(2).AutoFit();
                workSheet.Column(3).AutoFit();
                workSheet.Column(4).Width = 50;
                workSheet.Column(5).Width = 50;
                workSheet.Column(6).Width = 50;
                workSheet.Column(7).Width = 50;
                workSheet.Column(8).AutoFit();
                workSheet.Column(9).AutoFit();
                workSheet.Column(10).AutoFit();
                workSheet.Column(11).AutoFit();
                package.Save();
            }
            streamExport.Position = 0;
            var physicalPathCreate = Path.Combine(_hostingEnvironment.WebRootPath, AppGlobal.FTPDownloadReprotDaily, excelName);
            using (var stream = new FileStream(physicalPathCreate, FileMode.Create))
            {
                streamExport.CopyTo(stream);
            }
            string result = AppGlobal.DomainSub + AppGlobal.URLDownloadReprotDaily + excelName;
            return result;
        }

        public async Task<string> AsyncExportExcelReportDailyByIDList(CancellationToken cancellationToken, string IDList)
        {
            string excelName = @"Search_" + AppGlobal.DateTimeCode + ".xlsx";
            var streamExport = new MemoryStream();
            using (var package = new ExcelPackage(streamExport))
            {
                Color color = Color.FromArgb(int.Parse("#c00000".Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier));
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells[1, 1].Value = "Date";
                workSheet.Cells[1, 1].Style.Font.Bold = true;
                workSheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
                workSheet.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(color);
                workSheet.Cells[1, 1].Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 1].Style.Font.Size = 11;
                workSheet.Cells[1, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 1].Style.Border.Top.Color.SetColor(Color.Black);
                workSheet.Cells[1, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 1].Style.Border.Left.Color.SetColor(Color.Black);
                workSheet.Cells[1, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 1].Style.Border.Right.Color.SetColor(Color.Black);
                workSheet.Cells[1, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

                workSheet.Cells[1, 2].Value = "Company";
                workSheet.Cells[1, 2].Style.Font.Bold = true;
                workSheet.Cells[1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 2].Style.Font.Color.SetColor(System.Drawing.Color.White);
                workSheet.Cells[1, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 2].Style.Fill.BackgroundColor.SetColor(color);
                workSheet.Cells[1, 2].Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 2].Style.Font.Size = 11;
                workSheet.Cells[1, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 2].Style.Border.Top.Color.SetColor(Color.Black);
                workSheet.Cells[1, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 2].Style.Border.Left.Color.SetColor(Color.Black);
                workSheet.Cells[1, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 2].Style.Border.Right.Color.SetColor(Color.Black);
                workSheet.Cells[1, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 2].Style.Border.Bottom.Color.SetColor(Color.Black);

                workSheet.Cells[1, 3].Value = "Sentiment";
                workSheet.Cells[1, 3].Style.Font.Bold = true;
                workSheet.Cells[1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 3].Style.Font.Color.SetColor(System.Drawing.Color.White);
                workSheet.Cells[1, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 3].Style.Fill.BackgroundColor.SetColor(color);
                workSheet.Cells[1, 3].Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 3].Style.Font.Size = 11;
                workSheet.Cells[1, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 3].Style.Border.Top.Color.SetColor(Color.Black);
                workSheet.Cells[1, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 3].Style.Border.Left.Color.SetColor(Color.Black);
                workSheet.Cells[1, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 3].Style.Border.Right.Color.SetColor(Color.Black);
                workSheet.Cells[1, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 3].Style.Border.Bottom.Color.SetColor(Color.Black);

                workSheet.Cells[1, 4].Value = "Headline (Vie)";
                workSheet.Cells[1, 4].Style.Font.Bold = true;
                workSheet.Cells[1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 4].Style.Font.Color.SetColor(System.Drawing.Color.White);
                workSheet.Cells[1, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 4].Style.Fill.BackgroundColor.SetColor(color);
                workSheet.Cells[1, 4].Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 4].Style.Font.Size = 11;
                workSheet.Cells[1, 4].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 4].Style.Border.Top.Color.SetColor(Color.Black);
                workSheet.Cells[1, 4].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 4].Style.Border.Left.Color.SetColor(Color.Black);
                workSheet.Cells[1, 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 4].Style.Border.Right.Color.SetColor(Color.Black);
                workSheet.Cells[1, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 4].Style.Border.Bottom.Color.SetColor(Color.Black);

                workSheet.Cells[1, 5].Value = "Headline (Eng)";
                workSheet.Cells[1, 5].Style.Font.Bold = true;
                workSheet.Cells[1, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 5].Style.Font.Color.SetColor(System.Drawing.Color.White);
                workSheet.Cells[1, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 5].Style.Fill.BackgroundColor.SetColor(color);
                workSheet.Cells[1, 5].Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 5].Style.Font.Size = 11;
                workSheet.Cells[1, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 5].Style.Border.Top.Color.SetColor(Color.Black);
                workSheet.Cells[1, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 5].Style.Border.Left.Color.SetColor(Color.Black);
                workSheet.Cells[1, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 5].Style.Border.Right.Color.SetColor(Color.Black);
                workSheet.Cells[1, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 5].Style.Border.Bottom.Color.SetColor(Color.Black);

                workSheet.Cells[1, 6].Value = "URL";
                workSheet.Cells[1, 6].Style.Font.Bold = true;
                workSheet.Cells[1, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 6].Style.Font.Color.SetColor(System.Drawing.Color.White);
                workSheet.Cells[1, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 6].Style.Fill.BackgroundColor.SetColor(color);
                workSheet.Cells[1, 6].Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 6].Style.Font.Size = 11;
                workSheet.Cells[1, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 6].Style.Border.Top.Color.SetColor(Color.Black);
                workSheet.Cells[1, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 6].Style.Border.Left.Color.SetColor(Color.Black);
                workSheet.Cells[1, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 6].Style.Border.Right.Color.SetColor(Color.Black);
                workSheet.Cells[1, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 6].Style.Border.Bottom.Color.SetColor(Color.Black);

                workSheet.Cells[1, 7].Value = "Content";
                workSheet.Cells[1, 7].Style.Font.Bold = true;
                workSheet.Cells[1, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 7].Style.Font.Color.SetColor(System.Drawing.Color.White);
                workSheet.Cells[1, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 7].Style.Fill.BackgroundColor.SetColor(color);
                workSheet.Cells[1, 7].Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 7].Style.Font.Size = 11;
                workSheet.Cells[1, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 7].Style.Border.Top.Color.SetColor(Color.Black);
                workSheet.Cells[1, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 7].Style.Border.Left.Color.SetColor(Color.Black);
                workSheet.Cells[1, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 7].Style.Border.Right.Color.SetColor(Color.Black);
                workSheet.Cells[1, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 7].Style.Border.Bottom.Color.SetColor(Color.Black);

                workSheet.Cells[1, 8].Value = "Author";
                workSheet.Cells[1, 8].Style.Font.Bold = true;
                workSheet.Cells[1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 8].Style.Font.Color.SetColor(System.Drawing.Color.White);
                workSheet.Cells[1, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 8].Style.Fill.BackgroundColor.SetColor(color);
                workSheet.Cells[1, 8].Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 8].Style.Font.Size = 11;
                workSheet.Cells[1, 8].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 8].Style.Border.Top.Color.SetColor(Color.Black);
                workSheet.Cells[1, 8].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 8].Style.Border.Left.Color.SetColor(Color.Black);
                workSheet.Cells[1, 8].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 8].Style.Border.Right.Color.SetColor(Color.Black);
                workSheet.Cells[1, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 8].Style.Border.Bottom.Color.SetColor(Color.Black);

                workSheet.Cells[1, 9].Value = "Segment";
                workSheet.Cells[1, 9].Style.Font.Bold = true;
                workSheet.Cells[1, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 9].Style.Font.Color.SetColor(System.Drawing.Color.White);
                workSheet.Cells[1, 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 9].Style.Fill.BackgroundColor.SetColor(color);
                workSheet.Cells[1, 9].Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 9].Style.Font.Size = 11;
                workSheet.Cells[1, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 9].Style.Border.Top.Color.SetColor(Color.Black);
                workSheet.Cells[1, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 9].Style.Border.Left.Color.SetColor(Color.Black);
                workSheet.Cells[1, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 9].Style.Border.Right.Color.SetColor(Color.Black);
                workSheet.Cells[1, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 9].Style.Border.Bottom.Color.SetColor(Color.Black);

                workSheet.Cells[1, 10].Value = "Product";
                workSheet.Cells[1, 10].Style.Font.Bold = true;
                workSheet.Cells[1, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 10].Style.Font.Color.SetColor(System.Drawing.Color.White);
                workSheet.Cells[1, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 10].Style.Fill.BackgroundColor.SetColor(color);
                workSheet.Cells[1, 10].Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 10].Style.Font.Size = 11;
                workSheet.Cells[1, 10].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 10].Style.Border.Top.Color.SetColor(Color.Black);
                workSheet.Cells[1, 10].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 10].Style.Border.Left.Color.SetColor(Color.Black);
                workSheet.Cells[1, 10].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 10].Style.Border.Right.Color.SetColor(Color.Black);
                workSheet.Cells[1, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 10].Style.Border.Bottom.Color.SetColor(Color.Black);

                workSheet.Cells[1, 11].Value = "Media";
                workSheet.Cells[1, 11].Style.Font.Bold = true;
                workSheet.Cells[1, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 11].Style.Font.Color.SetColor(System.Drawing.Color.White);
                workSheet.Cells[1, 11].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 11].Style.Fill.BackgroundColor.SetColor(color);
                workSheet.Cells[1, 11].Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 11].Style.Font.Size = 11;
                workSheet.Cells[1, 11].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 11].Style.Border.Top.Color.SetColor(Color.Black);
                workSheet.Cells[1, 11].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 11].Style.Border.Left.Color.SetColor(Color.Black);
                workSheet.Cells[1, 11].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 11].Style.Border.Right.Color.SetColor(Color.Black);
                workSheet.Cells[1, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 11].Style.Border.Bottom.Color.SetColor(Color.Black);

                workSheet.Cells[1, 12].Value = "Ad value";
                workSheet.Cells[1, 12].Style.Font.Bold = true;
                workSheet.Cells[1, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 12].Style.Font.Color.SetColor(System.Drawing.Color.White);
                workSheet.Cells[1, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 12].Style.Fill.BackgroundColor.SetColor(color);
                workSheet.Cells[1, 12].Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 12].Style.Font.Size = 11;
                workSheet.Cells[1, 12].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 12].Style.Border.Top.Color.SetColor(Color.Black);
                workSheet.Cells[1, 12].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 12].Style.Border.Left.Color.SetColor(Color.Black);
                workSheet.Cells[1, 12].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 12].Style.Border.Right.Color.SetColor(Color.Black);
                workSheet.Cells[1, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 12].Style.Border.Bottom.Color.SetColor(Color.Black);

                workSheet.Cells[1, 13].Value = "Search";
                workSheet.Cells[1, 13].Style.Font.Bold = true;
                workSheet.Cells[1, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 13].Style.Font.Color.SetColor(System.Drawing.Color.White);
                workSheet.Cells[1, 13].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 13].Style.Fill.BackgroundColor.SetColor(color);
                workSheet.Cells[1, 13].Style.Font.Name = "Times New Roman";
                workSheet.Cells[1, 13].Style.Font.Size = 11;
                workSheet.Cells[1, 13].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 13].Style.Border.Top.Color.SetColor(Color.Black);
                workSheet.Cells[1, 13].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 13].Style.Border.Left.Color.SetColor(Color.Black);
                workSheet.Cells[1, 13].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 13].Style.Border.Right.Color.SetColor(Color.Black);
                workSheet.Cells[1, 13].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[1, 13].Style.Border.Bottom.Color.SetColor(Color.Black);

                if (!string.IsNullOrEmpty(IDList))
                {
                    List<ProductDataTransfer> listData = await _reportRepository.AsyncGetByIDListToList(IDList);
                    int row = 2;
                    foreach (ProductDataTransfer item in listData)
                    {
                        for (int column = 1; column < 14; column++)
                        {
                            switch (column)
                            {
                                case 1:
                                    workSheet.Cells[row, column].Value = item.DatePublish;
                                    workSheet.Cells[row, column].Style.Numberformat.Format = "mm/dd/yyyy";
                                    break;
                                case 2:
                                    workSheet.Cells[row, column].Value = ".";
                                    break;
                                case 3:
                                    workSheet.Cells[row, column].Value = ".";
                                    break;
                                case 4:
                                    workSheet.Cells[row, column].Value = item.Title;
                                    if (!string.IsNullOrEmpty(item.Title))
                                    {
                                        workSheet.Cells[row, column].Hyperlink = new Uri(item.URLCode);
                                        workSheet.Cells[row, column].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                    }
                                    break;
                                case 5:
                                    workSheet.Cells[row, column].Value = item.TitleEnglish;
                                    if (!string.IsNullOrEmpty(item.TitleEnglish))
                                    {
                                        workSheet.Cells[row, column].Hyperlink = new Uri(item.URLCode);
                                        workSheet.Cells[row, column].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                    }
                                    else
                                    {
                                        workSheet.Cells[row, column].Value = ".";
                                    }
                                    break;
                                case 6:
                                    if (!string.IsNullOrEmpty(item.URLCode))
                                    {
                                        workSheet.Cells[row, column].Value = item.URLCode;
                                        workSheet.Cells[row, column].Hyperlink = new Uri(item.URLCode);
                                        workSheet.Cells[row, column].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                    }
                                    break;
                                case 7:
                                    workSheet.Cells[row, column].Value = item.Description;
                                    break;
                                case 8:
                                    workSheet.Cells[row, column].Value = ".";
                                    break;
                                case 9:
                                    workSheet.Cells[row, column].Value = ".";
                                    break;
                                case 10:
                                    workSheet.Cells[row, column].Value = ".";
                                    break;
                                case 11:
                                    workSheet.Cells[row, column].Value = item.Media;
                                    break;
                                case 12:
                                    workSheet.Cells[row, column].Value = item.AdvertisementValue;
                                    workSheet.Cells[row, column].Style.Numberformat.Format = "#,##0";
                                    break;
                                case 13:
                                    workSheet.Cells[row, column].Value = item.Summary;
                                    break;
                            }
                            workSheet.Cells[row, column].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            workSheet.Cells[row, column].Style.Font.Name = "Times New Roman";
                            workSheet.Cells[row, column].Style.Font.Size = 11;
                            workSheet.Cells[row, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[row, column].Style.Border.Top.Color.SetColor(Color.Black);
                            workSheet.Cells[row, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[row, column].Style.Border.Left.Color.SetColor(Color.Black);
                            workSheet.Cells[row, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[row, column].Style.Border.Right.Color.SetColor(Color.Black);
                            workSheet.Cells[row, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            workSheet.Cells[row, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                        }
                        row = row + 1;
                    }
                }
                workSheet.Column(1).AutoFit();
                workSheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Column(2).AutoFit();
                workSheet.Column(3).AutoFit();
                workSheet.Column(4).Width = 50;
                workSheet.Column(5).Width = 50;
                workSheet.Column(6).Width = 50;
                workSheet.Column(7).Width = 50;
                workSheet.Column(8).AutoFit();
                workSheet.Column(9).AutoFit();
                workSheet.Column(10).AutoFit();
                workSheet.Column(11).AutoFit();
                workSheet.Column(12).AutoFit();
                workSheet.Column(12).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                package.Save();
            }

            streamExport.Position = 0;
            var physicalPathCreate = Path.Combine(_hostingEnvironment.WebRootPath, AppGlobal.FTPDownloadReprotDaily, excelName);
            using (var stream = new FileStream(physicalPathCreate, FileMode.Create))
            {
                streamExport.CopyTo(stream);
            }
            string result = AppGlobal.DomainSub + AppGlobal.URLDownloadReprotDaily + excelName;
            return result;
        }
        public string DeleteByDatePublishBeginAndDatePublishEndAndIndustryIDAndIDList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, string IDList)
        {
            string result = "";
            return result;
        }
    }
}
