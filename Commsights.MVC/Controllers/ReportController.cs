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

namespace Commsights.MVC.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IReportRepository _reportRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductPropertyRepository _productPropertyRepository;
        private readonly IProductSearchRepository _productSearchRepository;
        private readonly IProductSearchPropertyRepository _productSearchPropertyRepository;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMembershipPermissionRepository _membershipPermissionRepository;
        private readonly IConfigRepository _configResposistory;
        private readonly IMailService _mailService;
        public ReportController(IHostingEnvironment hostingEnvironment, IMailService mailService, IConfigRepository configResposistory, IMembershipRepository membershipRepository, IMembershipPermissionRepository membershipPermissionRepository, IProductRepository productRepository, IProductPropertyRepository productPropertyRepository, IReportRepository reportRepository, IProductSearchRepository productSearchRepository, IProductSearchPropertyRepository productSearchPropertyRepository, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
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
        public IActionResult Index()
        {
            BaseViewModel model = new BaseViewModel();
            model.DatePublishBegin = DateTime.Now.AddDays(-1);
            model.DatePublishEnd = DateTime.Now;
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
            return View(model);
        }
        public IActionResult Upload(int ID)
        {
            return View();
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
        public IActionResult SendMailReportDaily(int industryID, DateTime datePublishBegin, DateTime datePublishEnd)
        {
            List<ProductSearchDataTransfer> list = _productSearchRepository.GetByDatePublishBeginAndDatePublishEndAndIndustryIDToList(datePublishBegin, datePublishEnd, industryID);
            foreach (ProductSearchDataTransfer item in list)
            {
                ProductSearchDataTransfer model = InitializationReportDailyHTMLSendMail(item, "ReportDaily.html");
                Commsights.Service.Mail.Mail mail = new Service.Mail.Mail();
                mail.Initialization();
                mail.Content = model.Note;
                mail.Subject = "Daily Report (" + item.CompanyName + " - CommSights ) " + item.DateSearch.ToString("dd.MM.yyyy");
                mail.ToMail = "vietnam.commsights@gmail.com";
                //mail.ToMail = "khanh.nguyen@commsightsvn.com,ngoc.huynh@commsightsvn.com";
                _mailService.Send(mail);
            }
            string note = AppGlobal.Success + " - " + AppGlobal.SendMailSuccess;
            return Json(note);
        }
        public ProductSearchDataTransfer InitializationReportDailyHTMLSendMail(ProductSearchDataTransfer model, string fileName)
        {
            string html = "";
            var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, "html", fileName);
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
                List<ProductSearchPropertyDataTransfer> listData = _reportRepository.ReportDailyByProductSearchIDAndActiveToListToHTML(model.ID, true);
                foreach (MembershipPermission dailyReportSection in listDailyReportSection)
                {
                    if ((dailyReportSection.CategoryID == AppGlobal.DailyReportSectionSummaryID) && (dailyReportSection.Active == true))
                    {
                        if (listData.Count > 0)
                        {
                            reportSummary.AppendLine(@"<b style='color: #ed7d31; font-size:14px;'>I - HIGHLIGHT NEWS OF THE DAY</b>");
                            reportSummary.AppendLine(@"<br />");
                            reportSummary.AppendLine(@"<br />");
                            reportSummary.AppendLine(@"<div style='font-size:12px;'>");
                            foreach (ProductSearchPropertyDataTransfer data in listData)
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
                            int no = 0;
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
                            List<MembershipPermission> listDailyReportColumn = _membershipPermissionRepository.GetByMembershipIDAndIndustryIDAndCodeToList(model.CompanyID.Value, model.IndustryID.Value, AppGlobal.DailyReportColumn);
                            reportData.AppendLine(@"<b style='color: #ed7d31; font-size:14px;'>II - INFORMATION</b>");
                            reportData.AppendLine(@"<br />");
                            reportData.AppendLine(@"<br />");
                            reportData.AppendLine(@"<table style='font-size:12px; border-color: #000000; border-style: solid;border-width: 1px; padding: 4px; border-collapse: collapse;'>");
                            reportData.AppendLine(@"<thead>");
                            reportData.AppendLine(@"<tr>");
                            foreach (MembershipPermission dailyReportColumn in listDailyReportColumn)
                            {
                                if (dailyReportColumn.Active == true)
                                {
                                    if ((dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnHeadlineVietnameseID) || (dailyReportColumn.CategoryID == AppGlobal.DailyReportColumnHeadlineEnglishID))
                                    {
                                        reportData.Append(@"<th style='width:300px; text-align:center; background-color:#c00000; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;'><a style='cursor:pointer; color:#ffffff;'>");
                                    }
                                    else
                                    {
                                        reportData.Append(@"<th style='text-align:center; background-color:#c00000; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;white-space: nowrap;overflow: hidden;'><a style='cursor:pointer; color:#ffffff;'>");
                                    }

                                    if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                    {
                                        reportData.Append(@"" + dailyReportColumn.Phone);
                                    }
                                    else
                                    {
                                        reportData.Append(@"" + dailyReportColumn.Email);
                                    }
                                    reportData.Append(@"</a></th>");

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
                            }
                            reportData.AppendLine(@"</tr>");
                            reportData.AppendLine(@"</thead>");
                            reportData.AppendLine(@"<tbody>");
                            foreach (ProductSearchPropertyDataTransfer data in listData)
                            {
                                string title = "<a target='_blank' style='color: blue; cursor:pointer;' href='" + data.URLCode + "' title='" + data.URLCode + "'>" + data.Title + "</a></td>";
                                string titleEnglish = "<a target='_blank' style='color: blue; cursor:pointer;' href='" + data.URLCode + "' title='" + data.URLCode + "'>" + data.TitleEnglish + "</a></td>";
                                no = no + 1;
                                if (no % 2 == 0)
                                {
                                    reportData.AppendLine(@"<tr style='background-color:#ffffff;'>");
                                }
                                else
                                {
                                    reportData.AppendLine(@"<tr style='background-color:#f1f1f1;'>");
                                }
                                for (int i = 1; i < 12; i++)
                                {
                                    if ((DailyReportColumnDatePublishID > 0) && (DailyReportColumnDatePublishIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='text-align: right; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;white-space: nowrap;overflow: hidden;'>" + data.DatePublishString + "</td>");
                                    }
                                    if ((DailyReportColumnCategoryID > 0) && (DailyReportColumnCategoryIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;white-space: nowrap;overflow: hidden;'>" + data.ArticleTypeName + "</td>");
                                    }
                                    if ((DailyReportColumnCompanyID > 0) && (DailyReportColumnCompanyIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;white-space: nowrap;overflow: hidden;'>" + data.CompanyName + "</td>");
                                    }
                                    if ((DailyReportColumnSentimentID > 0) && (DailyReportColumnSentimentIDSortOrder == i))
                                    {
                                        reportData.Append(@"<td style='text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;white-space: nowrap;overflow: hidden;'>");
                                        if (data.AssessID == AppGlobal.NegativeID)
                                        {
                                            reportData.Append(@"<span style='color:red;'>" + data.AssessName + "</span>");
                                        }
                                        else
                                        {
                                            reportData.Append(@"" + data.AssessName);
                                        }
                                        reportData.Append(@"</td>");
                                    }
                                    if ((DailyReportColumnHeadlineVietnameseID > 0) && (DailyReportColumnHeadlineVietnameseIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;'>" + title + "</td>");
                                    }
                                    if ((DailyReportColumnHeadlineEnglishID > 0) && (DailyReportColumnHeadlineEnglishIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;'>" + titleEnglish + "</td>");
                                    }
                                    if ((DailyReportColumnMediaTitleID > 0) && (DailyReportColumnMediaTitleIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;white-space: nowrap;overflow: hidden;'>" + data.Media + "</td>");
                                    }
                                    if ((DailyReportColumnMediaTypeID > 0) && (DailyReportColumnMediaTypeIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;white-space: nowrap;overflow: hidden;'>" + data.MediaType + "</td>");
                                    }
                                    if ((DailyReportColumnPageID > 0) && (DailyReportColumnPageIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;white-space: nowrap;overflow: hidden;'>" + data.Page + "</td>");
                                    }
                                    if ((DailyReportColumnAdvertisementID > 0) && (DailyReportColumnAdvertisementIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='text-align: right; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;white-space: nowrap;overflow: hidden;'>" + data.AdvertisementValueString + "</td>");
                                    }
                                    if ((DailyReportColumnSummaryID > 0) && (DailyReportColumnSummaryIDSortOrder == i))
                                    {
                                        reportData.Append(@"<td style='text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;white-space: nowrap;overflow: hidden;'>");
                                        if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                        {
                                            reportData.Append(@"" + data.Description);
                                        }
                                        else
                                        {
                                            reportData.Append(@"" + data.DescriptionEnglish);
                                        }
                                        reportData.Append(@"</td>");
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
        public ProductSearchDataTransfer InitializationReportDailyHTML(ProductSearchDataTransfer model, string fileName)
        {
            string html = "";
            var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, "html", fileName);
            using (var stream = new FileStream(physicalPath, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }
            }
            if (!string.IsNullOrEmpty(html))
            {
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
                List<ProductSearchPropertyDataTransfer> listData = _reportRepository.ReportDailyByProductSearchIDAndActiveToListToHTML(model.ID, true);
                foreach (MembershipPermission dailyReportSection in listDailyReportSection)
                {
                    if ((dailyReportSection.CategoryID == AppGlobal.DailyReportSectionSummaryID) && (dailyReportSection.Active == true))
                    {
                        if (listData.Count > 0)
                        {
                            reportSummary.AppendLine(@"<b style='color: #ed7d31; font-size:14px;'>I - HIGHLIGHT NEWS OF THE DAY</b>");
                            reportSummary.AppendLine(@"<br />");
                            reportSummary.AppendLine(@"<br />");
                            reportSummary.AppendLine(@"<div style='font-size:12px;'>");
                            foreach (ProductSearchPropertyDataTransfer data in listData)
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
                            List<MembershipPermission> listDailyReportColumn = _membershipPermissionRepository.GetByMembershipIDAndIndustryIDAndCodeToList(model.CompanyID.Value, model.IndustryID.Value, AppGlobal.DailyReportColumn);
                            reportData.AppendLine(@"<b style='color: #ed7d31; font-size:14px;'>II - INFORMATION</b>");
                            reportData.AppendLine(@"<br />");
                            reportData.AppendLine(@"<br />");
                            reportData.AppendLine(@"<table class='border' style='font-size:12px; width:100%;'>");
                            reportData.AppendLine(@"<thead>");
                            reportData.AppendLine(@"<tr>");
                            //reportData.AppendLine(@"<th style='text-align:center; background-color:#c00000;'><a style='cursor:pointer; color:#ffffff;'>No</a></th>");
                            foreach (MembershipPermission dailyReportColumn in listDailyReportColumn)
                            {
                                if (dailyReportColumn.Active == true)
                                {
                                    reportData.Append(@"<th style='text-align:center; background-color:#c00000;'><a style='cursor:pointer; color:#ffffff;'>");
                                    if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                    {
                                        reportData.Append(@"" + dailyReportColumn.Phone);
                                    }
                                    else
                                    {
                                        reportData.Append(@"" + dailyReportColumn.Email);
                                    }
                                    reportData.Append(@"</a></th>");
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
                            }
                            reportData.AppendLine(@"</tr>");
                            reportData.AppendLine(@"</thead>");
                            reportData.AppendLine(@"<tbody>");

                            foreach (ProductSearchPropertyDataTransfer data in listData)
                            {
                                string title = "<a target='_blank' style='color: blue; cursor:pointer;' href='" + data.URLCode + "' title='" + data.URLCode + "'>" + data.Title + "</a></td>";
                                string titleEnglish = "<a target='_blank' style='color: blue; cursor:pointer;' href='" + data.URLCode + "' title='" + data.URLCode + "'>" + data.TitleEnglish + "</a></td>";
                                no = no + 1;
                                if (no % 2 == 0)
                                {
                                    reportData.AppendLine(@"<tr style='background-color:#ffffff;'>");
                                }
                                else
                                {
                                    reportData.AppendLine(@"<tr style='background-color:#f1f1f1;'>");
                                }
                                for (int i = 1; i < 12; i++)
                                {
                                    if ((DailyReportColumnDatePublishID > 0) && (DailyReportColumnDatePublishIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='text-align: right; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;white-space: nowrap;overflow: hidden;'>" + data.DatePublishString + "</td>");
                                    }
                                    if ((DailyReportColumnCategoryID > 0) && (DailyReportColumnCategoryIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;white-space: nowrap;overflow: hidden;'>" + data.ArticleTypeName + "</td>");
                                    }
                                    if ((DailyReportColumnCompanyID > 0) && (DailyReportColumnCompanyIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;white-space: nowrap;overflow: hidden;'>" + data.CompanyName + "</td>");
                                    }
                                    if ((DailyReportColumnSentimentID > 0) && (DailyReportColumnSentimentIDSortOrder == i))
                                    {
                                        reportData.Append(@"<td style='text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;white-space: nowrap;overflow: hidden;'>");
                                        if (data.AssessID == AppGlobal.NegativeID)
                                        {
                                            reportData.Append(@"<span style='color:red;'>" + data.AssessName + "</span>");
                                        }
                                        else
                                        {
                                            reportData.Append(@"" + data.AssessName);
                                        }
                                        reportData.Append(@"</td>");
                                    }
                                    if ((DailyReportColumnHeadlineVietnameseID > 0) && (DailyReportColumnHeadlineVietnameseIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;white-space: nowrap;overflow: hidden;'>" + title + "</td>");
                                    }
                                    if ((DailyReportColumnHeadlineEnglishID > 0) && (DailyReportColumnHeadlineEnglishIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;white-space: nowrap;overflow: hidden;'>" + titleEnglish + "</td>");
                                    }
                                    if ((DailyReportColumnMediaTitleID > 0) && (DailyReportColumnMediaTitleIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;white-space: nowrap;overflow: hidden;'>" + data.Media + "</td>");
                                    }
                                    if ((DailyReportColumnMediaTypeID > 0) && (DailyReportColumnMediaTypeIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;white-space: nowrap;overflow: hidden;'>" + data.MediaType + "</td>");
                                    }
                                    if ((DailyReportColumnPageID > 0) && (DailyReportColumnPageIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;white-space: nowrap;overflow: hidden;'>" + data.Page + "</td>");
                                    }
                                    if ((DailyReportColumnAdvertisementID > 0) && (DailyReportColumnAdvertisementIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='text-align: right; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;white-space: nowrap;overflow: hidden;'>" + data.AdvertisementValueString + "</td>");
                                    }
                                    if ((DailyReportColumnSummaryID > 0) && (DailyReportColumnSummaryIDSortOrder == i))
                                    {
                                        reportData.Append(@"<td style='text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;white-space: nowrap;overflow: hidden;'>");
                                        if (dailyReportSection.LanguageID == AppGlobal.LanguageID)
                                        {
                                            reportData.Append(@"" + data.Description);
                                        }
                                        else
                                        {
                                            reportData.Append(@"" + data.DescriptionEnglish);
                                        }
                                        reportData.Append(@"</td>");
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
        public ActionResult InitializationByDatePublishBeginAndDatePublishEndAndIndustryIDToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd, int industryID)
        {
            var data = _reportRepository.InitializationByDatePublishBeginAndDatePublishEndAndIndustryIDToList(datePublishBegin, datePublishEnd, industryID);
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
        public IActionResult UpdateDataTransfer(ProductDataTransfer model)
        {
            Initialization(model);
            model.CompanyID = model.Company.ID;
            model.ArticleTypeID = model.ArticleType.ID;
            model.AssessID = model.AssessType.ID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _productRepository.Update(model.ID, model);
            if (result > 0)
            {
                ProductProperty productProperty = _productPropertyRepository.GetByProductIDAndCodeAndCompanyID(model.ID, AppGlobal.Company, model.CompanyID.Value);
                if (productProperty == null)
                {
                    productProperty = new ProductProperty();
                }
                productProperty.Code = AppGlobal.Company;
                productProperty.GUICode = model.GUICode;
                productProperty.ParentID = model.ID;
                productProperty.ArticleTypeID = model.ArticleTypeID;
                productProperty.AssessID = model.AssessID;
                productProperty.CompanyID = model.CompanyID;
                if (productProperty.ID > 0)
                {
                    productProperty.Initialization(InitType.Update, RequestUserID);
                    _productPropertyRepository.Update(productProperty.ID, productProperty);
                }
                else
                {
                    productProperty.Initialization(InitType.Insert, RequestUserID);
                    _productPropertyRepository.Create(productProperty);
                }
                note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.EditFail;
            }
            return Json(note);
        }
        public IActionResult UpdateByIndustryIDAndDatePublishBeginAndDatePublishEndAndAllData(int industryID, DateTime datePublishBegin, DateTime datePublishEnd, bool allData)
        {
            if (allData == true)
            {
                _reportRepository.UpdateByDatePublishBeginAndDatePublishEndAndIndustryIDAndAllData(datePublishBegin, datePublishEnd, industryID, allData, RequestUserID);
            }
            string note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            return Json(note);
        }
        public async Task<IActionResult> ExportExcelReportDailyByProductSearchIDAndActive(CancellationToken cancellationToken, int ID)
        {
            await Task.Yield();
            var list = _reportRepository.ReportDailyByProductSearchIDAndActiveToListToHTML(ID, true);
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
                workSheet.Cells[1, 4].Value = "Company";
                workSheet.Cells[1, 4].Style.Font.Bold = true;
                workSheet.Cells[1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 5].Value = "Sentiment";
                workSheet.Cells[1, 5].Style.Font.Bold = true;
                workSheet.Cells[1, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 6].Value = "Headline (Vie)";
                workSheet.Cells[1, 6].Style.Font.Bold = true;
                workSheet.Cells[1, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 7].Value = "Headline (Eng)";
                workSheet.Cells[1, 7].Style.Font.Bold = true;
                workSheet.Cells[1, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 8].Value = "Media";
                workSheet.Cells[1, 8].Style.Font.Bold = true;
                workSheet.Cells[1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 9].Value = "Media type";
                workSheet.Cells[1, 9].Style.Font.Bold = true;
                workSheet.Cells[1, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 10].Value = "Ad value";
                workSheet.Cells[1, 10].Style.Font.Bold = true;
                workSheet.Cells[1, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 11].Value = "Summary";
                workSheet.Cells[1, 11].Style.Font.Bold = true;
                workSheet.Cells[1, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                int i = 0;
                int no = 0;
                for (int row = 2; row <= list.Count + 1; row++)
                {
                    no = no + 1;
                    workSheet.Cells[row, 1].Value = no.ToString();
                    workSheet.Cells[row, 2].Value = list[i].DatePublishString;
                    workSheet.Cells[row, 3].Value = list[i].ArticleTypeName;
                    workSheet.Cells[row, 4].Value = list[i].CompanyName;
                    workSheet.Cells[row, 5].Value = list[i].AssessName;
                    string assessName = list[i].AssessName.ToLower();
                    if (assessName == "negative")
                    {
                        workSheet.Cells[row, 5].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                    }
                    workSheet.Cells[row, 6].Value = list[i].Title;
                    if (!string.IsNullOrEmpty(list[i].Title))
                    {
                        workSheet.Cells[row, 6].Hyperlink = new Uri(list[i].URLCode);
                        workSheet.Cells[row, 6].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                    }
                    workSheet.Cells[row, 7].Value = list[i].TitleEnglish;
                    if (!string.IsNullOrEmpty(list[i].TitleEnglish))
                    {
                        workSheet.Cells[row, 7].Hyperlink = new Uri(list[i].URLCode);
                        workSheet.Cells[row, 7].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                    }
                    workSheet.Cells[row, 8].Value = list[i].Media;
                    workSheet.Cells[row, 9].Value = list[i].MediaType;
                    workSheet.Cells[row, 10].Value = list[i].AdvertisementValueString;
                    workSheet.Cells[row, 11].Value = list[i].Summary;
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
            int result = 0;
            string action = "Upload";
            string controller = "Report";
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
                        fileName = "Scan";
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
                                                    model.ArticleTypeID = AppGlobal.TinDoanhNghiepID;
                                                    model.AssessID = AppGlobal.AssessID;
                                                    model.GUICode = AppGlobal.InitGuiCode;
                                                    model.IndustryID = baseViewModel.IndustryIDUploadScan;

                                                    if (workSheet.Cells[i, 8].Value != null)
                                                    {
                                                        model.Title = workSheet.Cells[i, 8].Value.ToString().Trim();
                                                        model.URLCode = workSheet.Cells[i, 8].Hyperlink.AbsoluteUri.Trim();
                                                    }
                                                    if (_productRepository.IsValid(model.URLCode))
                                                    {
                                                        if (baseViewModel.IsIndustryIDUploadScan == true)
                                                        {
                                                            model.IndustryID = AppGlobal.IndustryID;
                                                        }
                                                        else
                                                        {
                                                            ProductProperty productProperty = new ProductProperty();
                                                            productProperty.GUICode = model.GUICode;
                                                            productProperty.IndustryID = model.IndustryID;
                                                            productProperty.ArticleTypeID = model.ArticleTypeID;
                                                            productProperty.AssessID = model.AssessID;
                                                            productProperty.ParentID = 0;
                                                            productProperty.Code = AppGlobal.Industry;
                                                            productProperty.Initialization(InitType.Insert, RequestUserID);
                                                            _productPropertyRepository.Create(productProperty);
                                                        }
                                                        try
                                                        {
                                                            string company = "";
                                                            string mediaTitle = "";
                                                            model.DatePublish = DateTime.Now;
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
                                                            if (workSheet.Cells[i, 3].Value != null)
                                                            {
                                                                company = workSheet.Cells[i, 3].Value.ToString().Trim();
                                                            }
                                                            int no = 0;
                                                            foreach (string item in company.Split(','))
                                                            {
                                                                string companyName = item.Trim();
                                                                Membership membership = _membershipRepository.GetByAccount(companyName);
                                                                if (membership == null)
                                                                {
                                                                    membership = new Membership();
                                                                    membership.Active = true;
                                                                    membership.Account = companyName;
                                                                    membership.FullName = companyName;
                                                                    membership.ParentID = AppGlobal.ParentIDCustomer;
                                                                    membership.Initialization(InitType.Insert, RequestUserID);
                                                                    _membershipRepository.Create(membership);
                                                                }
                                                                if (membership.ID > 0)
                                                                {
                                                                    ProductProperty productProperty = new ProductProperty();
                                                                    productProperty.ParentID = 0;
                                                                    productProperty.GUICode = model.GUICode;
                                                                    productProperty.CompanyID = membership.ID;
                                                                    productProperty.ArticleTypeID = AppGlobal.TinDoanhNghiepID;
                                                                    productProperty.AssessID = AppGlobal.AssessID;
                                                                    productProperty.Code = AppGlobal.Company;
                                                                    productProperty.Initialization(InitType.Insert, RequestUserID);
                                                                    _productPropertyRepository.Create(productProperty);
                                                                    if (no == 0)
                                                                    {
                                                                        model.CompanyID = membership.ID;
                                                                        List<MembershipPermissionDataTransfer> listMembershipPermissionDataTransfer = _membershipPermissionRepository.GetDataTransferIndustryByMembershipIDAndCodeAndActiveToList(membership.ID, AppGlobal.Industry, true);
                                                                        if (listMembershipPermissionDataTransfer.Count > 0)
                                                                        {
                                                                            model.IndustryID = listMembershipPermissionDataTransfer[0].ID;
                                                                        }
                                                                    }
                                                                }
                                                                no = no + 1;
                                                            }
                                                            if (workSheet.Cells[i, 8].Value != null)
                                                            {
                                                                model.Title = workSheet.Cells[i, 8].Value.ToString().Trim();
                                                                model.URLCode = workSheet.Cells[i, 8].Hyperlink.AbsoluteUri.Trim();
                                                            }
                                                            if (workSheet.Cells[i, 10].Value != null)
                                                            {
                                                                model.FileName = workSheet.Cells[i, 10].Value.ToString().Trim();
                                                            }
                                                            if (workSheet.Cells[i, 11].Value != null)
                                                            {
                                                                mediaTitle = workSheet.Cells[i, 11].Value.ToString().Trim();
                                                            }
                                                            if (workSheet.Cells[i, 12].Value != null)
                                                            {
                                                                string type = workSheet.Cells[i, 12].Value.ToString().Trim();
                                                            }
                                                            if (workSheet.Cells[i, 22].Value != null)
                                                            {
                                                                model.Page = workSheet.Cells[i, 22].Value.ToString().Trim();
                                                            }
                                                            model.ParentID = AppGlobal.WebsiteID;
                                                            Config parent = _configResposistory.GetByGroupNameAndCodeAndTitle(AppGlobal.CRM, AppGlobal.PressList, mediaTitle);
                                                            if (parent == null)
                                                            {
                                                                parent = new Config();
                                                                parent.Title = mediaTitle;
                                                                parent.CodeName = mediaTitle;
                                                                if (workSheet.Cells[i, 12].Value != null)
                                                                {
                                                                    string type = workSheet.Cells[i, 12].Value.ToString().Trim();
                                                                    Config mediaType = _configResposistory.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.WebsiteType, type);
                                                                    if (mediaType == null)
                                                                    {
                                                                        mediaType = new Config();
                                                                        mediaType.CodeName = type;
                                                                        mediaType.Initialization(InitType.Insert, RequestUserID);
                                                                        _configResposistory.Create(mediaType);
                                                                    }
                                                                    parent.ParentID = mediaType.ID;
                                                                }
                                                                if (workSheet.Cells[i, 13].Value != null)
                                                                {
                                                                    string type = workSheet.Cells[i, 13].Value.ToString().Trim();
                                                                    Config country = _configResposistory.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.WebsiteType, type);
                                                                    if (country == null)
                                                                    {
                                                                        country = new Config();
                                                                        country.CodeName = type;
                                                                        country.Initialization(InitType.Insert, RequestUserID);
                                                                        _configResposistory.Create(country);
                                                                    }
                                                                    parent.CountryID = country.ID;
                                                                }
                                                                if (workSheet.Cells[i, 16].Value != null)
                                                                {
                                                                    string type = workSheet.Cells[i, 16].Value.ToString().Trim();
                                                                    Config language = _configResposistory.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.WebsiteType, type);
                                                                    if (language == null)
                                                                    {
                                                                        language = new Config();
                                                                        language.CodeName = type;
                                                                        language.Initialization(InitType.Insert, RequestUserID);
                                                                        _configResposistory.Create(language);
                                                                    }
                                                                    parent.LanguageID = language.ID;
                                                                }
                                                                if (workSheet.Cells[i, 17].Value != null)
                                                                {
                                                                    string type = workSheet.Cells[i, 17].Value.ToString().Trim();
                                                                    Config frequency = _configResposistory.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.WebsiteType, type);
                                                                    if (frequency == null)
                                                                    {
                                                                        frequency = new Config();
                                                                        frequency.CodeName = type;
                                                                        frequency.Initialization(InitType.Insert, RequestUserID);
                                                                        _configResposistory.Create(frequency);
                                                                    }
                                                                    parent.FrequencyID = frequency.ID;
                                                                }
                                                                if (workSheet.Cells[i, 21].Value != null)
                                                                {
                                                                    string type = workSheet.Cells[i, 21].Value.ToString().Trim();
                                                                    Config colorType = _configResposistory.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.WebsiteType, type);
                                                                    if (colorType == null)
                                                                    {
                                                                        colorType = new Config();
                                                                        colorType.CodeName = type;
                                                                        colorType.Initialization(InitType.Insert, RequestUserID);
                                                                        _configResposistory.Create(colorType);
                                                                    }
                                                                    parent.ColorTypeID = colorType.ID;
                                                                }
                                                                if (workSheet.Cells[i, 25].Value != null)
                                                                {
                                                                    string type = workSheet.Cells[i, 25].Value.ToString().Trim();
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
                                                                _configResposistory.Create(parent);
                                                            }
                                                            if (parent != null)
                                                            {
                                                                model.ParentID = parent.ID;
                                                            }
                                                            model.Initialization(InitType.Insert, RequestUserID);
                                                            model.MetaTitle = AppGlobal.SetName(model.Title);
                                                            model.CategoryID = model.ParentID;
                                                            model.TitleEnglish = "";
                                                            model.Description = "";
                                                            model.Source = "Scan";
                                                            if (!string.IsNullOrEmpty(model.URLCode))
                                                            {
                                                                Product product = _productRepository.GetByURLCode(model.URLCode);
                                                                if (product == null)
                                                                {
                                                                    _productRepository.Create(model);
                                                                }
                                                                else
                                                                {
                                                                    if (product.ID > 0)
                                                                    {
                                                                        if (baseViewModel.IsUploadScanOverride == true)
                                                                        {
                                                                            model.ID = product.ID;
                                                                            model.DateCreated = product.DateCreated;
                                                                            model.Initialization(InitType.Update, RequestUserID);
                                                                            _productRepository.Update(model.ID, model);
                                                                        }
                                                                    }
                                                                }
                                                                if (model.ID > 0)
                                                                {
                                                                    _productPropertyRepository.UpdateItemsWithParentIDIsZero();
                                                                }
                                                                result = model.ID;
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
            }
            catch
            {
            }
            action = "Upload";
            controller = "Report";
            return RedirectToAction(action, controller);
        }
        public ActionResult UploadAndiSource(Commsights.MVC.Models.BaseViewModel baseViewModel)
        {
            int result = 0;
            string action = "Upload";
            string controller = "Report";
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
                        fileName = "AndiSource";
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
                                                for (int i = 6; i <= totalRows; i++)
                                                {
                                                    List<ProductProperty> listProductProperty = new List<ProductProperty>();
                                                    Membership membership = new Membership();
                                                    Product model = new Product();
                                                    model.ArticleTypeID = AppGlobal.TinNganhID;
                                                    model.AssessID = AppGlobal.AssessID;
                                                    model.GUICode = AppGlobal.InitGuiCode;
                                                    model.IndustryID = baseViewModel.IndustryIDUploadAndiSource;
                                                    if (baseViewModel.IsIndustryIDUploadAndiSource == true)
                                                    {
                                                        model.IndustryID = AppGlobal.IndustryID;
                                                    }
                                                    else
                                                    {
                                                        ProductProperty productProperty = new ProductProperty();
                                                        productProperty.GUICode = model.GUICode;
                                                        productProperty.IndustryID = model.IndustryID;
                                                        productProperty.ArticleTypeID = model.ArticleTypeID;
                                                        productProperty.AssessID = model.AssessID;
                                                        productProperty.ParentID = 0;
                                                        productProperty.Code = AppGlobal.Industry;
                                                        productProperty.Initialization(InitType.Insert, RequestUserID);
                                                        _productPropertyRepository.Create(productProperty);
                                                    }
                                                    model.Initialization(InitType.Insert, RequestUserID);
                                                    model.DatePublish = DateTime.Now;
                                                    model.ParentID = AppGlobal.WebsiteID;
                                                    model.CategoryID = AppGlobal.WebsiteID;
                                                    try
                                                    {
                                                        string datePublish = "";
                                                        if (workSheet.Cells[i, 11].Value != null)
                                                        {
                                                            model.Page = workSheet.Cells[i, 11].Value.ToString().Trim();
                                                        }
                                                        if (workSheet.Cells[i, 1].Value != null)
                                                        {
                                                            datePublish = workSheet.Cells[i, 1].Value.ToString().Trim();
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
                                                        if (workSheet.Cells[i, 3].Value != null)
                                                        {
                                                            string company = workSheet.Cells[i, 3].Value.ToString().Trim();
                                                            membership = _membershipRepository.GetByAccount(company);
                                                            if (membership == null)
                                                            {
                                                                membership = new Membership();
                                                                membership.Account = company;
                                                                membership.FullName = company;
                                                                membership.ParentID = AppGlobal.ParentIDCustomer;
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
                                                            }
                                                            if (membership.ParentID == AppGlobal.ParentIDCustomer)
                                                            {
                                                                model.CompanyID = membership.ParentID;
                                                                model.ArticleTypeID = AppGlobal.TinDoanhNghiepID;
                                                                ProductProperty productProperty = new ProductProperty();
                                                                productProperty.GUICode = model.GUICode;
                                                                productProperty.CompanyID = model.CompanyID;
                                                                productProperty.ParentID = 0;
                                                                productProperty.Code = AppGlobal.Company;
                                                                productProperty.Initialization(InitType.Insert, RequestUserID);
                                                                _productPropertyRepository.Create(productProperty);
                                                            }
                                                        }
                                                        if (workSheet.Cells[i, 5].Value != null)
                                                        {
                                                            model.Title = workSheet.Cells[i, 5].Value.ToString().Trim();
                                                            model.ImageThumbnail = workSheet.Cells[i, 5].Hyperlink.AbsoluteUri.Trim();
                                                            AppGlobal.GetURLByURLAndi(model, listProductProperty, RequestUserID);
                                                        }
                                                        if (workSheet.Cells[i, 6].Value != null)
                                                        {
                                                            model.TitleEnglish = workSheet.Cells[i, 6].Value.ToString().Trim();
                                                        }
                                                        if (workSheet.Cells[i, 7].Value != null)
                                                        {
                                                            model.FileName = workSheet.Cells[i, 7].Value.ToString().Trim();
                                                        }
                                                        if (workSheet.Cells[i, 8].Value != null)
                                                        {
                                                            string mediaTitle = workSheet.Cells[i, 8].Value.ToString().Trim();
                                                            string mediaType = "Online";
                                                            string code = AppGlobal.Website;
                                                            if (workSheet.Cells[i, 9].Value != null)
                                                            {
                                                                mediaType = workSheet.Cells[i, 9].Value.ToString().Trim();
                                                            }
                                                            if (mediaType.Contains("Online") == false)
                                                            {
                                                                code = AppGlobal.PressList;
                                                            }
                                                            Config parent = _configResposistory.GetByGroupNameAndCodeAndTitle(AppGlobal.CRM, code, mediaTitle);
                                                            if (parent == null)
                                                            {
                                                                parent = new Config();
                                                                parent.Title = mediaTitle;
                                                                parent.CodeName = mediaTitle;
                                                                Config parentOfParent = _configResposistory.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.WebsiteType, mediaType);
                                                                if (parentOfParent == null)
                                                                {
                                                                    parentOfParent = new Config();
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
                                                                        frequency.CodeName = frequencyName;
                                                                        frequency.Initialization(InitType.Insert, RequestUserID);
                                                                        _configResposistory.Create(parentOfParent);
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
                                                                _configResposistory.Create(parent);
                                                            }
                                                            model.ParentID = parent.ID;
                                                        }

                                                        if (workSheet.Cells[i, 12].Value != null)
                                                        {
                                                            model.Duration = workSheet.Cells[i, 12].Value.ToString().Trim();
                                                        }
                                                        if (workSheet.Cells[i, 14].Value != null)
                                                        {
                                                            model.Author = workSheet.Cells[i, 14].Value.ToString().Trim();
                                                        }
                                                        model.MetaTitle = AppGlobal.SetName(model.Title);
                                                        model.CategoryID = model.ParentID;
                                                        model.TitleEnglish = "";
                                                        model.Description = "";
                                                        model.Source = "Andi";
                                                        if (!string.IsNullOrEmpty(model.URLCode))
                                                        {
                                                            Product product = _productRepository.GetByURLCode(model.URLCode);
                                                            if (product == null)
                                                            {
                                                                bool saveModel = true;
                                                                saveModel = _productRepository.IsValid(model.URLCode);
                                                                if (model.IsVideo != null)
                                                                {
                                                                    saveModel = _productRepository.IsValidByFileNameAndDatePublish(model.URLCode, model.DatePublish);
                                                                }
                                                                if (saveModel)
                                                                {
                                                                    _productRepository.Create(model);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (product.ID > 0)
                                                                {
                                                                    if (baseViewModel.IsUploadAndiSourceOverride == true)
                                                                    {
                                                                        model.ID = product.ID;
                                                                        model.DateCreated = product.DateCreated;
                                                                        model.Initialization(InitType.Update, RequestUserID);
                                                                        _productRepository.Update(model.ID, model);
                                                                    }
                                                                }
                                                            }
                                                            if (model.ID > 0)
                                                            {
                                                                if (listProductProperty.Count > 0)
                                                                {
                                                                    if (model.URLCode.Contains("http") == false)
                                                                    {
                                                                        model.URLCode = AppGlobal.DomainMain + "Product/ViewContent/" + model.ID;
                                                                        _productRepository.Update(model.ID, model);
                                                                    }
                                                                    for (int j = 0; j < listProductProperty.Count; j++)
                                                                    {
                                                                        listProductProperty[j].ParentID = model.ID;
                                                                    }
                                                                    _productPropertyRepository.Range(listProductProperty);
                                                                    _productPropertyRepository.UpdateItemsWithParentIDIsZero();
                                                                }
                                                            }
                                                        }
                                                        result = result + 1;
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

            action = "Upload";
            controller = "Report";

            return RedirectToAction(action, controller);
        }
        public ActionResult UploadYounet(Commsights.MVC.Models.BaseViewModel baseViewModel)
        {
            int result = 0;
            string action = "Upload";
            string controller = "Report";
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
                        fileName = "Younet";
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
                                                    model.Source = "Younet";
                                                    model.ArticleTypeID = AppGlobal.ArticleTypeID;
                                                    model.AssessID = AppGlobal.AssessID;
                                                    model.IndustryID = baseViewModel.IndustryIDUploadYounet;
                                                    if (baseViewModel.IsIndustryIDUploadYounet == true)
                                                    {
                                                        model.IndustryID = AppGlobal.IndustryID;
                                                    }
                                                    model.Initialization(InitType.Insert, RequestUserID);
                                                    model.DatePublish = DateTime.Now;
                                                    model.ParentID = AppGlobal.WebsiteID;
                                                    model.CategoryID = AppGlobal.WebsiteID;
                                                    model.PriceUnitId = 0;
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
                                                                parent.Initialization(InitType.Insert, RequestUserID);
                                                                parent.ParentID = AppGlobal.ParentID;
                                                                parent.CodeName = source;
                                                                parent.Title = source;
                                                                parent.URLFull = source;
                                                                _configResposistory.Create(parent);
                                                            }
                                                            model.ParentID = parent.ID;
                                                            model.CategoryID = parent.ID;
                                                        }
                                                        if (workSheet.Cells[i, 5].Value != null)
                                                        {
                                                            model.URLCode = workSheet.Cells[i, 5].Value.ToString().Trim();
                                                        }
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
                                                        if (workSheet.Cells[i, 10].Value != null)
                                                        {
                                                            model.Author = workSheet.Cells[i, 10].Value.ToString().Trim();
                                                        }
                                                        if (workSheet.Cells[i, 14].Value != null)
                                                        {
                                                            model.Title = workSheet.Cells[i, 14].Value.ToString().Trim();
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
                                                            model.GUICode = AppGlobal.InitGuiCode;
                                                            model.Description = "";
                                                            model.MetaTitle = AppGlobal.SetName(model.Title);
                                                            model.CategoryID = model.ParentID;
                                                            model.TitleEnglish = "";
                                                            List<ProductProperty> listProductProperty = new List<ProductProperty>();
                                                            _productRepository.FilterProduct(model, listProductProperty, RequestUserID);
                                                            Product product = _productRepository.GetByURLCode(model.URLCode);
                                                            if (product == null)
                                                            {
                                                                _productRepository.Create(model);
                                                            }
                                                            else
                                                            {
                                                                if (product.ID > 0)
                                                                {
                                                                    if (baseViewModel.IsUploadAndiSourceOverride == true)
                                                                    {
                                                                        model.ID = product.ID;
                                                                        model.DateCreated = product.DateCreated;
                                                                        model.Initialization(InitType.Update, RequestUserID);
                                                                        _productRepository.Update(model.ID, model);
                                                                    }
                                                                }
                                                            }
                                                            if (model.ID > 0)
                                                            {
                                                                if (listProductProperty.Count > 0)
                                                                {
                                                                    for (int m = 0; m < listProductProperty.Count; m++)
                                                                    {
                                                                        listProductProperty[m].ParentID = model.ID;
                                                                    }
                                                                    _productPropertyRepository.Range(listProductProperty);
                                                                }
                                                                result = result + 1;
                                                            }
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
            action = "Upload";
            controller = "Report";
            return RedirectToAction(action, controller);
        }
        public ActionResult UploadGoogleSearch(Commsights.MVC.Models.BaseViewModel baseViewModel)
        {
            int result = 0;
            string action = "Upload";
            string controller = "Report";
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
                        fileName = "GoogleSearch";
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
                                                    List<ProductProperty> listProductProperty = new List<ProductProperty>();
                                                    ProductProperty productProperty = new ProductProperty();
                                                    Product model = new Product();
                                                    model.Initialization(InitType.Insert, RequestUserID);
                                                    model.Source = "Google";
                                                    model.GUICode = AppGlobal.InitGuiCode;
                                                    model.DatePublish = DateTime.Now;
                                                    model.ParentID = AppGlobal.WebsiteID;
                                                    model.CategoryID = AppGlobal.WebsiteID;
                                                    model.ArticleTypeID = AppGlobal.ArticleTypeID;
                                                    model.CompanyID = AppGlobal.CompetitorID;
                                                    model.AssessID = AppGlobal.AssessID;
                                                    model.IndustryID = baseViewModel.IndustryIDUploadGoogleSearch;

                                                    try
                                                    {
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
                                                        if (workSheet.Cells[i, 3].Value != null)
                                                        {
                                                            string assessString = workSheet.Cells[i, 3].Value.ToString().Trim();
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
                                                        if (baseViewModel.IsIndustryIDUploadGoogleSearch == true)
                                                        {
                                                            model.IndustryID = AppGlobal.IndustryID;
                                                        }
                                                        else
                                                        {
                                                            model.ArticleTypeID = AppGlobal.TinNganhID;
                                                            productProperty = new ProductProperty();
                                                            productProperty.Code = AppGlobal.Industry;
                                                            productProperty.Initialization(InitType.Insert, RequestUserID);
                                                            productProperty.GUICode = model.GUICode;
                                                            productProperty.IndustryID = model.IndustryID;
                                                            productProperty.ArticleTypeID = model.ArticleTypeID;
                                                            productProperty.AssessID = model.AssessID;
                                                            listProductProperty.Add(productProperty);
                                                        }
                                                        if (workSheet.Cells[i, 2].Value != null)
                                                        {
                                                            string companyName = workSheet.Cells[i, 2].Value.ToString().Trim();
                                                            Membership company = _membershipRepository.GetByAccount(companyName);
                                                            if (company == null)
                                                            {
                                                                company = new Membership();
                                                                company.Active = true;
                                                                company.Account = companyName;
                                                                company.FullName = companyName;
                                                                company.ParentID = AppGlobal.ParentIDCustomer;
                                                                company.Initialization(InitType.Insert, RequestUserID);
                                                                _membershipRepository.Create(company);
                                                            }
                                                            model.CompanyID = company.ID;
                                                            model.ArticleTypeID = AppGlobal.TinDoanhNghiepID;
                                                            productProperty = new ProductProperty();
                                                            productProperty.Code = AppGlobal.Company;
                                                            productProperty.Initialization(InitType.Insert, RequestUserID);
                                                            productProperty.GUICode = model.GUICode;
                                                            productProperty.CompanyID = model.CompanyID;
                                                            productProperty.ArticleTypeID = model.ArticleTypeID;
                                                            productProperty.AssessID = model.AssessID;
                                                            productProperty.IndustryID = model.IndustryID;
                                                            MembershipPermission membershipPermission = _membershipPermissionRepository.GetByMembershipIDAndAndCodeAndActive(productProperty.CompanyID.Value, AppGlobal.Industry, true);
                                                            if (productProperty.IndustryID == AppGlobal.IndustryID)
                                                            {
                                                                if (membershipPermission != null)
                                                                {
                                                                    productProperty.IndustryID = membershipPermission.IndustryID;
                                                                }
                                                            }
                                                            if (model.IndustryID == AppGlobal.IndustryID)
                                                            {
                                                                if (membershipPermission != null)
                                                                {
                                                                    productProperty.IndustryID = membershipPermission.IndustryID;
                                                                }
                                                            }
                                                            listProductProperty.Add(productProperty);
                                                        }

                                                        if (workSheet.Cells[i, 4].Value != null)
                                                        {
                                                            model.Title = workSheet.Cells[i, 4].Value.ToString().Trim();
                                                            model.URLCode = workSheet.Cells[i, 4].Hyperlink.AbsoluteUri.Trim();
                                                        }
                                                        if (workSheet.Cells[i, 5].Value != null)
                                                        {
                                                            model.TitleEnglish = workSheet.Cells[i, 5].Value.ToString().Trim();
                                                            model.URLCode = workSheet.Cells[i, 5].Hyperlink.AbsoluteUri.Trim();
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
                                                            model.FileName = AppGlobal.SetDomainByURL(model.URLCode);
                                                            Config website = _configResposistory.GetByGroupNameAndCodeAndTitle(AppGlobal.CRM, AppGlobal.Website, model.FileName);
                                                            if (website == null)
                                                            {
                                                                website = new Config();
                                                                website.Title = model.FileName;
                                                                website.URLFull = model.FileName;
                                                                website.ParentID = AppGlobal.ParentID;
                                                                website.Initialization(InitType.Insert, RequestUserID);
                                                                _configResposistory.Create(website);
                                                            }
                                                            model.ParentID = website.ID;
                                                        }
                                                        if (!string.IsNullOrEmpty(model.URLCode))
                                                        {
                                                            model.MetaTitle = AppGlobal.SetName(model.Title);
                                                            model.CategoryID = model.ParentID;
                                                            _productRepository.FilterProduct(model, listProductProperty, RequestUserID);
                                                            Product product = _productRepository.GetByURLCode(model.URLCode);
                                                            if (product == null)
                                                            {
                                                                _productRepository.Create(model);
                                                            }
                                                            else
                                                            {
                                                                if (product.ID > 0)
                                                                {
                                                                    if (baseViewModel.IsUploadGoogleSearchOverride == true)
                                                                    {
                                                                        model.ID = product.ID;
                                                                        model.DateCreated = product.DateCreated;
                                                                        model.Initialization(InitType.Update, RequestUserID);
                                                                        _productRepository.Update(model.ID, model);
                                                                    }
                                                                }

                                                            }
                                                            if (model.ID > 0)
                                                            {
                                                                if (listProductProperty.Count > 0)
                                                                {
                                                                    for (int m = 0; m < listProductProperty.Count; m++)
                                                                    {
                                                                        listProductProperty[m].ParentID = model.ID;
                                                                    }
                                                                    _productPropertyRepository.Range(listProductProperty);
                                                                }

                                                            }
                                                        }
                                                        result = result + 1;
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        result = 0;
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
            if (result > 0)
            {
                action = "Upload";
                controller = "Report";
            }
            return RedirectToAction(action, controller);
        }
        public ActionResult UploadGoogleSearch001(Commsights.MVC.Models.BaseViewModel baseViewModel)
        {
            int result = 0;
            string action = "Upload";
            string controller = "Report";
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
                        fileName = "GoogleSearch001";
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
                                                    model.Initialization(InitType.Insert, RequestUserID);
                                                    model.DatePublish = DateTime.Now;
                                                    model.ParentID = AppGlobal.WebsiteID;
                                                    model.CategoryID = AppGlobal.WebsiteID;
                                                    model.ArticleTypeID = AppGlobal.ArticleTypeID;
                                                    model.CompanyID = AppGlobal.CompetitorID;
                                                    model.AssessID = AppGlobal.AssessID;
                                                    model.IndustryID = baseViewModel.IndustryIDUploadGoogleSearch001;
                                                    int point = 0;
                                                    if (baseViewModel.IsIndustryIDUploadGoogleSearch001 == true)
                                                    {
                                                        model.IndustryID = AppGlobal.IndustryID;
                                                    }
                                                    try
                                                    {
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
                                                        if (workSheet.Cells[i, 2].Value != null)
                                                        {
                                                            string companyName = workSheet.Cells[i, 2].Value.ToString().Trim();
                                                            Membership company = _membershipRepository.GetByAccount(companyName);
                                                            if (company == null)
                                                            {
                                                                company = new Membership();
                                                                company.Account = companyName;
                                                                company.FullName = companyName;
                                                                company.ParentID = AppGlobal.ParentIDCustomer;
                                                                company.Initialization(InitType.Insert, RequestUserID);
                                                                _membershipRepository.Create(company);
                                                            }
                                                            model.CompanyID = company.ID;
                                                        }
                                                        if (workSheet.Cells[i, 3].Value != null)
                                                        {
                                                            string pointName = workSheet.Cells[i, 3].Value.ToString().Trim();
                                                            pointName = pointName.Replace(@"%", @"");
                                                            try
                                                            {
                                                                point = int.Parse(pointName);
                                                            }
                                                            catch
                                                            {
                                                            }
                                                        }
                                                        if (workSheet.Cells[i, 4].Value != null)
                                                        {
                                                            string articleType = workSheet.Cells[i, 4].Value.ToString().Trim();
                                                            articleType = articleType.ToLower();
                                                            switch (articleType)
                                                            {
                                                                case "0":
                                                                    model.ArticleTypeID = AppGlobal.CompanyFeatureID;
                                                                    break;
                                                                case "1":
                                                                    model.ArticleTypeID = AppGlobal.CompanyMentionID;
                                                                    break;
                                                                case "feature":
                                                                    model.ArticleTypeID = AppGlobal.CompanyFeatureID;
                                                                    break;
                                                                case "mention":
                                                                    model.ArticleTypeID = AppGlobal.CompanyMentionID;
                                                                    break;
                                                            }
                                                        }
                                                        if (workSheet.Cells[i, 5].Value != null)
                                                        {
                                                            string assessString = workSheet.Cells[i, 5].Value.ToString().Trim();
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
                                                        if (workSheet.Cells[i, 6].Value != null)
                                                        {
                                                            model.Title = workSheet.Cells[i, 6].Value.ToString().Trim();
                                                        }
                                                        if (workSheet.Cells[i, 7].Value != null)
                                                        {
                                                            model.Title = workSheet.Cells[i, 7].Value.ToString().Trim();
                                                        }
                                                        if (workSheet.Cells[i, 8].Value != null)
                                                        {
                                                            model.URLCode = workSheet.Cells[i, 8].Value.ToString().Trim();
                                                        }
                                                        if (workSheet.Cells[i, 9].Value != null)
                                                        {
                                                            model.ContentMain = workSheet.Cells[i, 9].Value.ToString().Trim();
                                                        }
                                                        if (!string.IsNullOrEmpty(model.URLCode))
                                                        {
                                                            model.Source = "Google";
                                                            model.GUICode = AppGlobal.InitGuiCode;
                                                            model.Description = "";
                                                            model.MetaTitle = AppGlobal.SetName(model.Title);
                                                            string source = AppGlobal.SetDomainByURL(model.URLCode);
                                                            Config parent = _configResposistory.GetByGroupNameAndCodeAndTitle(AppGlobal.CRM, AppGlobal.Website, source);
                                                            if (parent == null)
                                                            {
                                                                parent = _configResposistory.GetByGroupNameAndCodeAndTitle(AppGlobal.CRM, AppGlobal.PressList, source);
                                                            }
                                                            if (parent == null)
                                                            {
                                                                parent = new Config();
                                                                parent.Initialization(InitType.Insert, RequestUserID);
                                                                parent.ParentID = AppGlobal.ParentID;
                                                                parent.CodeName = source;
                                                                parent.Title = source;
                                                                parent.URLFull = source;
                                                                _configResposistory.Create(parent);
                                                            }
                                                            model.ParentID = parent.ID;
                                                            model.CategoryID = parent.ID;
                                                            Product product = _productRepository.GetByURLCode(model.URLCode);
                                                            if (product == null)
                                                            {
                                                                _productRepository.Create(model);
                                                            }
                                                            else
                                                            {
                                                                if (product.ID > 0)
                                                                {
                                                                    if (baseViewModel.IsIndustryIDUploadGoogleSearch001 == true)
                                                                    {
                                                                        model.ID = product.ID;
                                                                        model.DateCreated = product.DateCreated;
                                                                        model.Initialization(InitType.Update, RequestUserID);
                                                                        _productRepository.Update(model.ID, model);
                                                                    }
                                                                }
                                                            }
                                                            if (model.ID > 0)
                                                            {
                                                                List<ProductProperty> listProductProperty = new List<ProductProperty>();
                                                                ProductProperty productProperty = new ProductProperty();
                                                                productProperty.Code = AppGlobal.Company;
                                                                productProperty.ProductID = model.ID;
                                                                productProperty.CompanyID = model.CompanyID;
                                                                productProperty.ArticleTypeID = model.ArticleTypeID;
                                                                productProperty.AssessID = model.AssessID;
                                                                productProperty.Point = point;
                                                                _productPropertyRepository.Create(productProperty);
                                                                result = result + 1;
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
            if (result > 0)
            {
                action = "Index";
                controller = "Report";
            }
            return RedirectToAction(action, controller);
        }
    }
}
