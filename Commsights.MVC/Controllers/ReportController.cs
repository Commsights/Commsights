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
        public ReportController(IHostingEnvironment hostingEnvironment, IProductRepository productRepository, IProductPropertyRepository productPropertyRepository, IReportRepository reportRepository, IProductSearchRepository productSearchRepository, IProductSearchPropertyRepository productSearchPropertyRepository, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _reportRepository = reportRepository;
            _productRepository = productRepository;
            _productPropertyRepository = productPropertyRepository;
            _productSearchRepository = productSearchRepository;
            _productSearchPropertyRepository = productSearchPropertyRepository;
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
    }
}
