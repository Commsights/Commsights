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
        public ReportController(IHostingEnvironment hostingEnvironment, IConfigRepository configResposistory, IMembershipRepository membershipRepository, IMembershipPermissionRepository membershipPermissionRepository, IProductRepository productRepository, IProductPropertyRepository productPropertyRepository, IReportRepository reportRepository, IProductSearchRepository productSearchRepository, IProductSearchPropertyRepository productSearchPropertyRepository, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
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
            List<ProductSearchPropertyDataTransfer> ListProductSearchPropertyDataTransfer = _reportRepository.ReportDaily02ByProductSearchIDAndActiveToList(productSearchID, true);
            return Json(ListProductSearchPropertyDataTransfer);
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
                    workSheet.Cells[row, 8].Hyperlink = new Uri(list[i].URLCode);
                    workSheet.Cells[row, 8].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                    workSheet.Cells[row, 9].Value = list[i].TitleEnglish;
                    workSheet.Cells[row, 9].Hyperlink = new Uri(list[i].URLCode);
                    workSheet.Cells[row, 9].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
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
                    workSheet.Cells[row, 8].Hyperlink = new Uri(list[i].URLCode);
                    workSheet.Cells[row, 8].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                    workSheet.Cells[row, 9].Value = list[i].TitleEnglish;
                    workSheet.Cells[row, 9].Hyperlink = new Uri(list[i].URLCode);
                    workSheet.Cells[row, 9].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
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
                    workSheet.Cells[row, 8].Hyperlink = new Uri(list[i].URLCode);
                    workSheet.Cells[row, 8].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                    workSheet.Cells[row, 9].Value = list[i].TitleEnglish;
                    workSheet.Cells[row, 9].Hyperlink = new Uri(list[i].URLCode);
                    workSheet.Cells[row, 9].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
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
                    workSheet.Cells[row, 8].Hyperlink = new Uri(list[i].URLCode);
                    workSheet.Cells[row, 8].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                    workSheet.Cells[row, 9].Value = list[i].TitleEnglish;
                    workSheet.Cells[row, 9].Hyperlink = new Uri(list[i].URLCode);
                    workSheet.Cells[row, 9].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
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
                    workSheet.Cells[row, 8].Hyperlink = new Uri(list[i].URLCode);
                    workSheet.Cells[row, 8].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                    workSheet.Cells[row, 9].Value = list[i].TitleEnglish;
                    workSheet.Cells[row, 9].Hyperlink = new Uri(list[i].URLCode);
                    workSheet.Cells[row, 9].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
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
                                                            if (_productRepository.IsValid(model.URLCode))
                                                            {
                                                                model.MetaTitle = AppGlobal.SetName(model.Title);
                                                                model.CategoryID = model.ParentID;
                                                                model.TitleEnglish = "";
                                                                model.Description = "";
                                                                model.Source = "Scan";
                                                                _productRepository.Create(model);
                                                            }
                                                            if (model.ID > 0)
                                                            {
                                                                _productPropertyRepository.UpdateItemsWithParentIDIsZero();
                                                            }
                                                            result = model.ID;
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
            if (result > 0)
            {
                action = "Daily";
                controller = "Report";
            }
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
                                                            if(membership.ParentID == AppGlobal.ParentIDCustomer)
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
                                                        bool saveModel = true;
                                                        saveModel = _productRepository.IsValid(model.URLCode);
                                                        if (model.IsVideo != null)
                                                        {
                                                            saveModel = _productRepository.IsValidByFileNameAndDatePublish(model.URLCode, model.DatePublish);
                                                        }
                                                        if (saveModel)
                                                        {
                                                            model.MetaTitle = AppGlobal.SetName(model.Title);
                                                            model.CategoryID = model.ParentID;
                                                            model.TitleEnglish = "";
                                                            model.Source = "Andi";
                                                            _productRepository.Create(model);
                                                        }
                                                        if (model.ID > 0)
                                                        {                                                            
                                                            if (listProductProperty.Count > 0)
                                                            {
                                                                model.URLCode = "/Product/ViewContent/" + model.ID;
                                                                _productRepository.Update(model.ID, model);
                                                                for (int j = 0; j < listProductProperty.Count; j++)
                                                                {
                                                                    listProductProperty[j].ParentID = model.ID;
                                                                }
                                                                _productPropertyRepository.Range(listProductProperty);
                                                                _productPropertyRepository.UpdateItemsWithParentIDIsZero();
                                                            }
                                                        }
                                                        result = model.ID;
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
            if (result > 0)
            {
                action = "Daily";
                controller = "Report";
            }
            return RedirectToAction(action, controller);
        }
    }
}
