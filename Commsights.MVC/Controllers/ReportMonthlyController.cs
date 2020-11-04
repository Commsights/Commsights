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

namespace Commsights.MVC.Controllers
{
    public class ReportMonthlyController : BaseController
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IProductRepository _productRepository;
        private readonly IProductPropertyRepository _productPropertyRepository;
        private readonly IReportMonthlyRepository _reportMonthlyRepository;
        private readonly IReportMonthlyPropertyRepository _reportMonthlyPropertyRepository;
        private readonly IMembershipRepository _membershipRepository;
        public ReportMonthlyController(IMembershipRepository membershipRepository, IReportMonthlyPropertyRepository reportMonthlyPropertyRepository, IReportMonthlyRepository reportMonthlyRepository, IProductPropertyRepository productPropertyRepository, IProductRepository productRepository, IWebHostEnvironment hostingEnvironment, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _membershipRepository = membershipRepository;
            _productRepository = productRepository;
            _productPropertyRepository = productPropertyRepository;
            _reportMonthlyRepository = reportMonthlyRepository;
            _reportMonthlyPropertyRepository = reportMonthlyPropertyRepository;
        }
        public IActionResult Data()
        {
            return View();
        }
        public IActionResult Upload()
        {
            ReportMonthly model = new ReportMonthly();
            model.Year = DateTime.Now.Year;
            model.Month = DateTime.Now.Month;
            return View(model);
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
                                                        if (workSheet.Cells[i, 5].Value != null)
                                                        {
                                                            datePublish = workSheet.Cells[i, 5].Value.ToString().Trim();
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
                                                        if (workSheet.Cells[i, 19].Value != null)
                                                        {
                                                            product.Title = workSheet.Cells[i, 19].Value.ToString().Trim();
                                                            if (workSheet.Cells[i, 19].Hyperlink != null)
                                                            {
                                                                product.URLCode = workSheet.Cells[i, 19].Hyperlink.AbsoluteUri.Trim();
                                                            }
                                                        }
                                                        if (workSheet.Cells[i, 20].Value != null)
                                                        {
                                                            product.TitleEnglish = workSheet.Cells[i, 20].Value.ToString().Trim();
                                                            if (workSheet.Cells[i, 20].Hyperlink != null)
                                                            {
                                                                product.URLCode = workSheet.Cells[i, 19].Hyperlink.AbsoluteUri.Trim();
                                                            }
                                                        }
                                                        if (workSheet.Cells[i, 21].Value != null)
                                                        {
                                                            product.URLCode = workSheet.Cells[i, 21].Value.ToString().Trim();
                                                        }
                                                        if (workSheet.Cells[i, 22].Value != null)
                                                        {
                                                            product.Page = workSheet.Cells[i, 22].Value.ToString().Trim();
                                                        }
                                                        if (workSheet.Cells[i, 23].Value != null)
                                                        {
                                                            product.Page = workSheet.Cells[i, 23].Value.ToString().Trim();
                                                        }
                                                        Product product001 = _productRepository.GetByURLCode(product.URLCode);
                                                        if (product001 != null)
                                                        {
                                                            product001.DatePublish = product.DatePublish;
                                                            product001.TitleEnglish = product.TitleEnglish;
                                                            product001.Title = product.Title;
                                                            _productRepository.Update(product001.ID, product001);
                                                        }
                                                        else
                                                        {
                                                            product001 = product;
                                                            _productRepository.Create(product001);
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
                                                            if (workSheet.Cells[i, 2].Value != null)
                                                            {
                                                                productProperty.Week = workSheet.Cells[i, 2].Value.ToString().Trim();
                                                            }
                                                            if (workSheet.Cells[i, 3].Value != null)
                                                            {
                                                                productProperty.Month = workSheet.Cells[i, 3].Value.ToString().Trim();
                                                            }
                                                            if (workSheet.Cells[i, 4].Value != null)
                                                            {
                                                                productProperty.Quarter = workSheet.Cells[i, 4].Value.ToString().Trim();
                                                            }
                                                            if (workSheet.Cells[i, 6].Value != null)
                                                            {
                                                                productProperty.CategoryMain = workSheet.Cells[i, 6].Value.ToString().Trim();

                                                            }
                                                            if (workSheet.Cells[i, 7].Value != null)
                                                            {
                                                                productProperty.CategorySub = workSheet.Cells[i, 7].Value.ToString().Trim();
                                                            }
                                                            if (workSheet.Cells[i, 8].Value != null)
                                                            {
                                                                productProperty.Industry = workSheet.Cells[i, 8].Value.ToString().Trim();
                                                            }
                                                            if (workSheet.Cells[i, 9].Value != null)
                                                            {
                                                                productProperty.CompanyName = workSheet.Cells[i, 9].Value.ToString().Trim();
                                                            }
                                                            if (workSheet.Cells[i, 10].Value != null)
                                                            {
                                                                productProperty.CorpCopy = workSheet.Cells[i, 10].Value.ToString().Trim();
                                                            }
                                                            if (workSheet.Cells[i, 11].Value != null)
                                                            {
                                                                string sOECompany = workSheet.Cells[i, 11].Value.ToString().Trim();
                                                            }
                                                            if (workSheet.Cells[i, 12].Value != null)
                                                            {
                                                                productProperty.FeatureCorp = workSheet.Cells[i, 12].Value.ToString().Trim();
                                                            }
                                                            if (workSheet.Cells[i, 13].Value != null)
                                                            {
                                                                productProperty.Segment = workSheet.Cells[i, 13].Value.ToString().Trim();
                                                            }
                                                            if (workSheet.Cells[i, 14].Value != null)
                                                            {
                                                                productProperty.SegmentProduct = workSheet.Cells[i, 14].Value.ToString().Trim();
                                                            }
                                                            if (workSheet.Cells[i, 15].Value != null)
                                                            {
                                                                productProperty.ProductName_ProjectName = workSheet.Cells[i, 15].Value.ToString().Trim();
                                                            }
                                                            if (workSheet.Cells[i, 16].Value != null)
                                                            {
                                                                string sOEProduct = workSheet.Cells[i, 16].Value.ToString().Trim();
                                                            }
                                                            if (workSheet.Cells[i, 17].Value != null)
                                                            {
                                                                productProperty.FeatureProduct = workSheet.Cells[i, 17].Value.ToString().Trim();
                                                            }
                                                            if (workSheet.Cells[i, 18].Value != null)
                                                            {
                                                                productProperty.SegmentProduct = workSheet.Cells[i, 18].Value.ToString().Trim();
                                                            }
                                                            if (workSheet.Cells[i, 24].Value != null)
                                                            {
                                                                productProperty.TierCustomer = workSheet.Cells[i, 24].Value.ToString().Trim();
                                                            }
                                                            if (workSheet.Cells[i, 25].Value != null)
                                                            {
                                                                productProperty.SpokePersonName = workSheet.Cells[i, 25].Value.ToString().Trim();
                                                            }
                                                            if (workSheet.Cells[i, 26].Value != null)
                                                            {
                                                                productProperty.SpokePersonTitle = workSheet.Cells[i, 26].Value.ToString().Trim();
                                                            }
                                                            if (workSheet.Cells[i, 27].Value != null)
                                                            {
                                                                try
                                                                {
                                                                    productProperty.ToneValue = decimal.Parse(workSheet.Cells[i, 27].Value.ToString().Trim());
                                                                }
                                                                catch
                                                                {
                                                                }
                                                            }
                                                            if (workSheet.Cells[i, 28].Value != null)
                                                            {
                                                                try
                                                                {
                                                                    productProperty.HeadlineValue = decimal.Parse(workSheet.Cells[i, 28].Value.ToString().Trim());
                                                                }
                                                                catch
                                                                {
                                                                }
                                                            }
                                                            if (workSheet.Cells[i, 29].Value != null)
                                                            {
                                                                try
                                                                {
                                                                    productProperty.SpokePersonValue = decimal.Parse(workSheet.Cells[i, 29].Value.ToString().Trim());
                                                                }
                                                                catch
                                                                {
                                                                }
                                                            }
                                                            if (workSheet.Cells[i, 30].Value != null)
                                                            {
                                                                try
                                                                {
                                                                    productProperty.FeatureValue = decimal.Parse(workSheet.Cells[i, 30].Value.ToString().Trim());
                                                                }
                                                                catch
                                                                {
                                                                }
                                                            }
                                                            if (workSheet.Cells[i, 31].Value != null)
                                                            {
                                                                try
                                                                {
                                                                    productProperty.TierValue = decimal.Parse(workSheet.Cells[i, 31].Value.ToString().Trim());
                                                                }
                                                                catch
                                                                {
                                                                }
                                                            }
                                                            if (workSheet.Cells[i, 32].Value != null)
                                                            {
                                                                try
                                                                {
                                                                    productProperty.PictureValue = decimal.Parse(workSheet.Cells[i, 32].Value.ToString().Trim());
                                                                }
                                                                catch
                                                                {
                                                                }
                                                            }
                                                            if (workSheet.Cells[i, 33].Value != null)
                                                            {
                                                                try
                                                                {
                                                                    productProperty.MPS = decimal.Parse(workSheet.Cells[i, 33].Value.ToString().Trim());
                                                                }
                                                                catch
                                                                {
                                                                }
                                                            }
                                                            if (workSheet.Cells[i, 34].Value != null)
                                                            {
                                                                try
                                                                {
                                                                    productProperty.ROME_Corp_VND = decimal.Parse(workSheet.Cells[i, 34].Value.ToString().Trim());
                                                                }
                                                                catch
                                                                {
                                                                }
                                                            }
                                                            if (workSheet.Cells[i, 35].Value != null)
                                                            {
                                                                try
                                                                {
                                                                    productProperty.ROME_Product_VND = decimal.Parse(workSheet.Cells[i, 35].Value.ToString().Trim());
                                                                }
                                                                catch
                                                                {
                                                                }
                                                            }
                                                            _productPropertyRepository.Create(productProperty);
                                                            if (productProperty.ID > 0)
                                                            {
                                                                ReportMonthlyProperty reportMonthlyProperty = new ReportMonthlyProperty();
                                                                reportMonthlyProperty.Initialization(InitType.Insert, RequestUserID);
                                                                reportMonthlyProperty.ParentID = model.ID;
                                                                reportMonthlyProperty.ProductID = product.ID;
                                                                reportMonthlyProperty.ProductPropertyID = productProperty.ID;
                                                                _reportMonthlyPropertyRepository.Create(reportMonthlyProperty);
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
            return RedirectToAction(action, controller);
        }
    }
}
