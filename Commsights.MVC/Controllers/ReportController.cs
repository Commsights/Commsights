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
        public IActionResult Index(int industryID, string datePublishBeginString, string datePublishEndString)
        {
            BaseViewModel model = new BaseViewModel();
            model.DatePublishBegin = DateTime.Now.AddDays(-1);
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
        public IActionResult Upload(int ID)
        {
            return View();
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
            List<MembershipPermission> listDailyReportColumn = _membershipPermissionRepository.GetDailyReportColumnByMembershipIDAndIndustryIDAndCodeAndActiveFormSQLToList(model.CompanyID.Value, model.IndustryID.Value, AppGlobal.DailyReportColumn, true);
            List<MembershipPermission> listDailyReportSection = _membershipPermissionRepository.GetByMembershipIDAndIndustryIDAndCodeToList(model.CompanyID.Value, model.IndustryID.Value, AppGlobal.DailyReportSection);
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
                            int index = 0;
                            for (int row = 4; row <= listData.Count + 3; row++)
                            {
                                for (int i = 1; i < 13; i++)
                                {
                                    if ((DailyReportColumnDatePublishID > 0) && (DailyReportColumnDatePublishIDSortOrder == i))
                                    {
                                        workSheet.Cells[row, i].Value = listData[index].DatePublishString;
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
                                    if ((DailyReportColumnCategoryID > 0) && (DailyReportColumnCategoryIDSortOrder == i))
                                    {
                                        workSheet.Cells[row, i].Value = listData[index].ArticleTypeName;
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
                                        workSheet.Cells[row, i].Value = listData[index].CompanyName;
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
                                        workSheet.Cells[row, i].Value = listData[index].AssessName;
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
                                        if (!string.IsNullOrEmpty(listData[index].Title))
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
                                    if ((DailyReportColumnHeadlineEnglishID > 0) && (DailyReportColumnHeadlineEnglishIDSortOrder == i))
                                    {
                                        workSheet.Cells[row, i].Value = listData[index].TitleEnglish;
                                        if (!string.IsNullOrEmpty(listData[index].TitleEnglish))
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
                                        workSheet.Cells[row, i].Value = listData[index].MediaType;
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
            List<MembershipPermission> listDailyReportColumn = _membershipPermissionRepository.GetDailyReportColumnByMembershipIDAndIndustryIDAndCodeAndActiveFormSQLToList(model.CompanyID.Value, model.IndustryID.Value, AppGlobal.DailyReportColumn, true);
            List<MembershipPermission> listDailyReportSection = _membershipPermissionRepository.GetByMembershipIDAndIndustryIDAndCodeToList(model.CompanyID.Value, model.IndustryID.Value, AppGlobal.DailyReportSection);
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
                                if (dailyReportColumn.Active == true)
                                {
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
                                    if ((DailyReportColumnDatePublishID > 0) && (DailyReportColumnDatePublishIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='width: 100px; height:20px; text-align: right; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;'>" + data.DatePublishString + "</td>");
                                    }
                                    if ((DailyReportColumnCategoryID > 0) && (DailyReportColumnCategoryIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='width: 100px; height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;'>" + data.ArticleTypeName + "</td>");
                                    }
                                    if ((DailyReportColumnCompanyID > 0) && (DailyReportColumnCompanyIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='width: 100px; height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;'>" + data.CompanyName + "</td>");
                                    }
                                    if ((DailyReportColumnSentimentID > 0) && (DailyReportColumnSentimentIDSortOrder == i))
                                    {
                                        reportData.Append(@"<td style='width: 100px; height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;'>");
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
                                        if (DailyReportColumnSummaryID > 0)
                                        {
                                            reportData.AppendLine(@"<td style='width:200px; height:20px; border-color: #000000; border-style: solid; border-width: 1px; padding: 2px;'>");
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
                                            reportData.AppendLine(@"<td style='width:200px; height:20px; border-color: #000000; border-style: solid; border-width: 1px; padding: 2px;'>");
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
                                        reportData.AppendLine(@"<td style='width: 100px; height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 4px;white-space: nowrap;'>" + data.MediaType + "</td>");
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
                                            reportData.Append(@"<td rowspan='" + rowspan + "' style='height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;'>");
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
                                        workSheet.Cells[row, i].Value = listData[index].DatePublishString;
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
                                    if ((DailyReportColumnCategoryID > 0) && (DailyReportColumnCategoryIDSortOrder == i))
                                    {
                                        workSheet.Cells[row, i].Value = listData[index].ArticleTypeName;
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
                                        workSheet.Cells[row, i].Value = listData[index].CompanyName;
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
                                        workSheet.Cells[row, i].Value = listData[index].AssessName;
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
                                        if (!string.IsNullOrEmpty(listData[index].Title))
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
                                    if ((DailyReportColumnHeadlineEnglishID > 0) && (DailyReportColumnHeadlineEnglishIDSortOrder == i))
                                    {
                                        workSheet.Cells[row, i].Value = listData[index].TitleEnglish;
                                        if (!string.IsNullOrEmpty(listData[index].TitleEnglish))
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
                                        workSheet.Cells[row, i].Value = listData[index].MediaType;
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
                            List<MembershipPermission> listDailyReportColumn = _membershipPermissionRepository.GetDailyReportColumnByMembershipIDAndIndustryIDAndCodeAndActiveFormSQLToList(model.CompanyID.Value, model.IndustryID.Value, AppGlobal.DailyReportColumn, true);
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
                                if (dailyReportColumn.Active == true)
                                {
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
                                for (int i = 1; i < 12; i++)
                                {
                                    if ((DailyReportColumnDatePublishID > 0) && (DailyReportColumnDatePublishIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='width: 80px; height:20px; text-align: right; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;'>" + data.DatePublishString + "</td>");
                                    }
                                    if ((DailyReportColumnCategoryID > 0) && (DailyReportColumnCategoryIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='width: 80px; height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;'>" + data.ArticleTypeName + "</td>");
                                    }
                                    if ((DailyReportColumnCompanyID > 0) && (DailyReportColumnCompanyIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='width: 80px; height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;'>" + data.CompanyName + "</td>");
                                    }
                                    if ((DailyReportColumnSentimentID > 0) && (DailyReportColumnSentimentIDSortOrder == i))
                                    {
                                        reportData.Append(@"<td style='width: 80px; height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;'>");
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
                                        if (DailyReportColumnSummaryID > 0)
                                        {
                                            reportData.AppendLine(@"<td style='width:200px; height:20px; border-color: #000000; border-style: solid; border-width: 1px; padding: 2px;'>");
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
                                            reportData.AppendLine(@"<td style='width:200px; height:20px; border-color: #000000; border-style: solid; border-width: 1px; padding: 2px;'>");
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
                                        reportData.AppendLine(@"<td style='width: 80px; height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;'>" + data.MediaType + "</td>");
                                    }
                                    if ((DailyReportColumnPageID > 0) && (DailyReportColumnPageIDSortOrder == i))
                                    {
                                        reportData.AppendLine(@"<td style='width: 80px; height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;'>" + data.Page + "</td>");
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
                                            reportData.Append(@"<td rowspan='" + rowspan + "' style='height:20px; text-align: left; border-color: #000000; border-style: solid;border-width: 1px;padding: 2px;'>");
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
                productProperty.Initialization(InitType.Update, RequestUserID);
                _productPropertyRepository.Update(productProperty.ID, productProperty);
            }
            Product product = _productRepository.GetByID001(model.ProductID.Value);
            if (product != null)
            {
                product.IsSummary = model.IsSummary;
                product.IsData = model.IsData;
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

                    }
                }
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
            ProductProperty productProperty = _productPropertyRepository.GetByID001(ID);
            if (productProperty != null)
            {
                productProperty.ID = 0;
                productProperty.Initialization(InitType.Insert, RequestUserID);
                _productPropertyRepository.Create(productProperty);
            }
            string note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            return Json(note);
        }
        public IActionResult DeleteProductAndProductProperty(ProductDataTransfer model)
        {
            int result = _productPropertyRepository.Delete(model.ID);
            result = result + _productRepository.Delete(model.ProductID.Value);
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
                                                    model.Source = "Scan";
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
                                                        if (workSheet.Cells[i, 8].Value != null)
                                                        {
                                                            model.Title = workSheet.Cells[i, 8].Value.ToString().Trim();
                                                            if (workSheet.Cells[i, 8].Hyperlink != null)
                                                            {
                                                                model.URLCode = workSheet.Cells[i, 8].Hyperlink.AbsoluteUri.Trim();
                                                            }
                                                        }
                                                        if (workSheet.Cells[i, 10].Value != null)
                                                        {
                                                            model.FileName = workSheet.Cells[i, 10].Value.ToString().Trim();
                                                        }
                                                        if (workSheet.Cells[i, 11].Value != null)
                                                        {
                                                            mediaTitle = workSheet.Cells[i, 11].Value.ToString().Trim();
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

                                                            if (workSheet.Cells[i, 3].Value != null)
                                                            {
                                                                string companyName = workSheet.Cells[i, 3].Value.ToString().Trim();
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
                                                                if (baseViewModel.IsIndustryIDUploadScan == true)
                                                                {
                                                                    foreach (MembershipPermission item in _membershipPermissionRepository.GetByMembershipIDAndCodeToList(company.ID, AppGlobal.Industry))
                                                                    {
                                                                        ProductProperty productProperty = new ProductProperty();
                                                                        productProperty.Initialization(InitType.Insert, RequestUserID);
                                                                        productProperty.IsData = true;
                                                                        productProperty.ParentID = product.ID;
                                                                        productProperty.GUICode = product.GUICode;
                                                                        productProperty.AssessID = AppGlobal.AssessID;
                                                                        productProperty.IndustryID = item.IndustryID;
                                                                        productProperty.CompanyID = company.ID;
                                                                        productProperty.ArticleTypeID = AppGlobal.TinDoanhNghiepID;
                                                                        productProperty.Code = AppGlobal.Company;
                                                                        _productPropertyRepository.Create(productProperty);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    ProductProperty productProperty = new ProductProperty();
                                                                    productProperty.Initialization(InitType.Insert, RequestUserID);
                                                                    productProperty.IsData = true;
                                                                    productProperty.ParentID = product.ID;
                                                                    productProperty.GUICode = product.GUICode;
                                                                    productProperty.AssessID = AppGlobal.AssessID;
                                                                    productProperty.IndustryID = baseViewModel.IndustryIDUploadScan;
                                                                    productProperty.CompanyID = company.ID;
                                                                    productProperty.ArticleTypeID = AppGlobal.TinDoanhNghiepID;
                                                                    productProperty.Code = AppGlobal.Company;
                                                                    _productPropertyRepository.Create(productProperty);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                ProductProperty productProperty = new ProductProperty();
                                                                productProperty.Initialization(InitType.Insert, RequestUserID);
                                                                productProperty.IsData = true;
                                                                productProperty.ParentID = product.ID;
                                                                productProperty.GUICode = product.GUICode;
                                                                productProperty.ArticleTypeID = AppGlobal.TinNganhID;
                                                                productProperty.AssessID = AppGlobal.AssessID;
                                                                productProperty.Code = AppGlobal.Industry;
                                                                productProperty.IndustryID = baseViewModel.IndustryIDUploadScan;
                                                                _productPropertyRepository.Create(productProperty);
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
            return RedirectToAction(action, controller);
        }
        public ActionResult UploadAndiSource(Commsights.MVC.Models.BaseViewModel baseViewModel)
        {
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
                                                int totalRows = workSheet.Dimension.Rows + 1;
                                                for (int i = 6; i <= totalRows; i++)
                                                {
                                                    List<ProductProperty> listProductPropertyURLCode = new List<ProductProperty>();
                                                    Product model = new Product();
                                                    model.Initialization(InitType.Insert, RequestUserID);
                                                    model.AssessID = AppGlobal.AssessID;
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
                                                            if (workSheet.Cells[i, 5].Hyperlink != null)
                                                            {
                                                                model.ImageThumbnail = workSheet.Cells[i, 5].Hyperlink.AbsoluteUri.Trim();
                                                                AppGlobal.GetURLByURLAndi(model, listProductPropertyURLCode, RequestUserID);
                                                            }
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
                                                                parent.GroupName = AppGlobal.CRM;
                                                                parent.Code = code;
                                                                parent.Title = mediaTitle;
                                                                parent.CodeName = mediaTitle;
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
                                                        Product product = _productRepository.GetByFileNameAndDatePublish(model.FileName, model.DatePublish);
                                                        if (product == null)
                                                        {
                                                            model.GUICode = AppGlobal.InitGuiCode;
                                                            model.Source = "Andi";
                                                            model.MetaTitle = AppGlobal.SetName(model.Title);
                                                            _productRepository.Create(model);
                                                            product = model;
                                                        }
                                                        if (product.ID > 0)
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
                                                                }
                                                                _productPropertyRepository.Range(listProductPropertyURLCode);
                                                            }

                                                            if (workSheet.Cells[i, 3].Value != null)
                                                            {
                                                                string company = workSheet.Cells[i, 3].Value.ToString().Trim();
                                                                Membership membership = _membershipRepository.GetByAccount(company);
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
                                                                if (baseViewModel.IsIndustryIDUploadAndiSource == true)
                                                                {
                                                                    foreach (MembershipPermission item in _membershipPermissionRepository.GetByMembershipIDAndCodeToList(membership.ID, AppGlobal.Industry))
                                                                    {
                                                                        ProductProperty productProperty = new ProductProperty();
                                                                        productProperty.Initialization(InitType.Insert, RequestUserID);
                                                                        productProperty.IsData = true;
                                                                        productProperty.AssessID = product.AssessID;
                                                                        productProperty.ParentID = product.ID;
                                                                        productProperty.GUICode = product.GUICode;
                                                                        productProperty.CompanyID = membership.ID;
                                                                        productProperty.ArticleTypeID = AppGlobal.TinDoanhNghiepID;
                                                                        productProperty.Code = AppGlobal.Company;
                                                                        productProperty.IndustryID = item.IndustryID;
                                                                        _productPropertyRepository.Create(productProperty);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    ProductProperty productProperty = new ProductProperty();
                                                                    productProperty.Initialization(InitType.Insert, RequestUserID);
                                                                    productProperty.IsData = true;
                                                                    productProperty.AssessID = product.AssessID;
                                                                    productProperty.ParentID = product.ID;
                                                                    productProperty.GUICode = product.GUICode;
                                                                    productProperty.IndustryID = baseViewModel.IndustryIDUploadAndiSource;
                                                                    productProperty.CompanyID = membership.ID;
                                                                    productProperty.ArticleTypeID = AppGlobal.TinDoanhNghiepID;
                                                                    productProperty.Code = AppGlobal.Company;
                                                                    _productPropertyRepository.Create(productProperty);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                ProductProperty productProperty = new ProductProperty();
                                                                productProperty.Initialization(InitType.Insert, RequestUserID);
                                                                productProperty.IsData = true;
                                                                productProperty.Code = AppGlobal.Industry;
                                                                productProperty.ArticleTypeID = AppGlobal.TinNganhID;
                                                                productProperty.AssessID = product.AssessID;
                                                                productProperty.ParentID = product.ID;
                                                                productProperty.GUICode = product.GUICode;
                                                                productProperty.IndustryID = baseViewModel.IndustryIDUploadAndiSource;
                                                                _productPropertyRepository.Create(productProperty);
                                                            }
                                                            _productPropertyRepository.Initialization();
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
            return RedirectToAction(action, controller);
        }
        public ActionResult UploadYounet(Commsights.MVC.Models.BaseViewModel baseViewModel)
        {
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
                                                    model.Initialization(InitType.Insert, RequestUserID);
                                                    model.GUICode = AppGlobal.InitGuiCode;
                                                    model.Source = "Younet";
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
                                                                        listProductProperty[m].IsData = true;
                                                                        listProductProperty[m].ParentID = model.ID;
                                                                        listProductProperty[m].AssessID = model.AssessID;
                                                                        if (listProductProperty[m].IndustryID > 0)
                                                                        {
                                                                        }
                                                                        else
                                                                        {
                                                                            listProductProperty[m].IndustryID = baseViewModel.IndustryIDUploadYounet;
                                                                        }
                                                                    }
                                                                    _productPropertyRepository.Range(listProductProperty);
                                                                    _productPropertyRepository.Initialization();
                                                                }
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
                                                    try
                                                    {
                                                        Product model = new Product();
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
                                                            if (workSheet.Cells[i, 4].Hyperlink != null)
                                                            {
                                                                model.URLCode = workSheet.Cells[i, 4].Hyperlink.AbsoluteUri.Trim();
                                                            }
                                                        }
                                                        if (workSheet.Cells[i, 5].Value != null)
                                                        {
                                                            model.TitleEnglish = workSheet.Cells[i, 5].Value.ToString().Trim();
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
                                                            Product product = _productRepository.GetByURLCode(model.URLCode);
                                                            if (product == null)
                                                            {
                                                                model.MetaTitle = AppGlobal.SetName(model.Title);
                                                                model.Source = "Google";
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
                                                                    website.Initialization(InitType.Insert, RequestUserID);
                                                                    _configResposistory.Create(website);
                                                                }
                                                                model.ParentID = website.ID;
                                                                _productRepository.Create(model);
                                                                product = model;
                                                            }
                                                            if (product.ID > 0)
                                                            {
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
                                                                    if (baseViewModel.IsIndustryIDUploadGoogleSearch == true)
                                                                    {
                                                                        foreach (MembershipPermission item in _membershipPermissionRepository.GetByMembershipIDAndCodeToList(company.ID, AppGlobal.Industry))
                                                                        {
                                                                            ProductProperty productProperty = new ProductProperty();
                                                                            productProperty.Initialization(InitType.Insert, RequestUserID);
                                                                            productProperty.IsData = true;
                                                                            productProperty.ParentID = product.ID;
                                                                            productProperty.GUICode = product.GUICode;
                                                                            productProperty.AssessID = assessID;
                                                                            productProperty.IndustryID = item.IndustryID;
                                                                            productProperty.CompanyID = company.ID;
                                                                            productProperty.ArticleTypeID = AppGlobal.TinDoanhNghiepID;
                                                                            productProperty.Code = AppGlobal.Company;
                                                                            _productPropertyRepository.Create(productProperty);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        ProductProperty productProperty = new ProductProperty();
                                                                        productProperty.Initialization(InitType.Insert, RequestUserID);
                                                                        productProperty.IsData = true;
                                                                        productProperty.ParentID = product.ID;
                                                                        productProperty.GUICode = product.GUICode;
                                                                        productProperty.AssessID = assessID;
                                                                        productProperty.IndustryID = baseViewModel.IndustryIDUploadGoogleSearch;
                                                                        productProperty.CompanyID = company.ID;
                                                                        productProperty.ArticleTypeID = AppGlobal.TinDoanhNghiepID;
                                                                        productProperty.Code = AppGlobal.Company;
                                                                        _productPropertyRepository.Create(productProperty);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    ProductProperty productProperty = new ProductProperty();
                                                                    productProperty.Initialization(InitType.Insert, RequestUserID);
                                                                    productProperty.IsData = true;
                                                                    productProperty.ParentID = product.ID;
                                                                    productProperty.GUICode = product.GUICode;
                                                                    productProperty.ArticleTypeID = AppGlobal.TinNganhID;
                                                                    productProperty.AssessID = assessID;
                                                                    productProperty.Code = AppGlobal.Industry;
                                                                    productProperty.IndustryID = baseViewModel.IndustryIDUploadGoogleSearch;
                                                                    _productPropertyRepository.Create(productProperty);
                                                                }
                                                                _productPropertyRepository.Initialization();
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
            return RedirectToAction(action, controller);
        }
    }
}
