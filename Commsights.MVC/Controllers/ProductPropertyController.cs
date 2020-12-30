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
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;
using Commsights.Data.DataTransferObject;
using System.Diagnostics.Eventing.Reader;

namespace Commsights.MVC.Controllers
{
    public class ProductPropertyController : BaseController
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IProductRepository _productRepository;
        private readonly IProductPropertyRepository _productPropertyRepository;
        private readonly IConfigRepository _configResposistory;
        public ProductPropertyController(IWebHostEnvironment hostingEnvironment, IConfigRepository configResposistory, IProductRepository productRepository, IProductPropertyRepository productPropertyRepository, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _productRepository = productRepository;
            _productPropertyRepository = productPropertyRepository;
            _configResposistory = configResposistory;
        }
        public IActionResult Company(int ID)
        {
            Product model = new Product();
            if (ID > 0)
            {
                model = _productRepository.GetByID(ID);
            }
            return View(model);
        }
        public IActionResult ScanFilesHandling()
        {
            CodeData model = new CodeData();
            model.DatePublish = DateTime.Now;
            return View(model);
        }
        public IActionResult ViewContent(string fileName, string extension, string url)
        {
            ViewContentViewModel model = new ViewContentViewModel();
            extension = extension.Replace(@".", @"");
            model.Extension = extension;
            model.FileName = fileName;
            StringBuilder txt = new StringBuilder();
            switch (extension)
            {
                case "jpg":
                case "png":
                case "jpeg":
                    txt.AppendLine(@"<img src='" + url + "' class='img-thumbnail' />");
                    break;
                case "mp4":
                case "wmv":
                    txt.AppendLine(@"<video width='100%' height='80%' controls>");
                    txt.AppendLine(@"<source src='" + url + "' type='video/mp4'>");
                    txt.AppendLine(@"</video>");
                    break;
            }
            model.Extension = txt.ToString();
            return View(model);
        }
        public IActionResult ViewContentFull(string fileName, string extension, string url)
        {
            ViewContentViewModel model = new ViewContentViewModel();
            extension = extension.Replace(@".", @"");
            model.Extension = extension;
            model.FileName = fileName;
            StringBuilder txt = new StringBuilder();
            switch (extension)
            {
                case "jpg":
                case "png":
                case "jpeg":
                    txt.AppendLine(@"<img src='" + url + "' class='img-thumbnail' />");
                    break;
                case "mp4":
                case "wmv":
                    txt.AppendLine(@"<video width='100%' height='80%' controls>");
                    txt.AppendLine(@"<source src='" + url + "' type='video/mp4'>");
                    txt.AppendLine(@"</video>");
                    break;
            }
            model.Extension = txt.ToString();
            return View(model);
        }
        public IActionResult Industry(int ID)
        {
            Product model = new Product();
            if (ID > 0)
            {
                model = _productRepository.GetByID(ID);
            }
            return View(model);
        }
        public IActionResult Product(int ID)
        {
            Product model = new Product();
            if (ID > 0)
            {
                model = _productRepository.GetByID(ID);
            }
            return View(model);
        }
        public ActionResult GetRequestUserIDAndParentIDAndCodeAndDateUpdatedToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _productPropertyRepository.GetRequestUserIDAndParentIDAndCodeAndDateUpdatedToList(RequestUserID, -1, AppGlobal.URLCode, DateTime.Now);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetRequestUserIDAndParentIDAndCodeAndDateUpdatedAndFalseToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _productPropertyRepository.GetRequestUserIDAndParentIDAndCodeAndDateUpdatedAndActiveToList(RequestUserID, -1, AppGlobal.URLCode, DateTime.Now, false);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetRequestUserIDAndParentIDAndCodeAndDateUpdatedAndTrueToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _productPropertyRepository.GetRequestUserIDAndParentIDAndCodeAndDateUpdatedAndActiveToList(RequestUserID, -1, AppGlobal.URLCode, DateTime.Now, true);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferCompanyByParentIDToList([DataSourceRequest] DataSourceRequest request, int parentID)
        {
            var data = _productPropertyRepository.GetDataTransferCompanyByParentIDToList(parentID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferIndustryByParentIDToList([DataSourceRequest] DataSourceRequest request, int parentID)
        {
            var data = _productPropertyRepository.GetDataTransferIndustryByParentIDToList(parentID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferProductByParentIDToList([DataSourceRequest] DataSourceRequest request, int parentID)
        {
            var data = _productPropertyRepository.GetDataTransferProductByParentIDToList(parentID);
            return Json(data.ToDataSourceResult(request));
        }
        public IActionResult ScanFilesUpdateTrue(ProductProperty model)
        {
            string note = AppGlobal.InitString;
            model.Active = true;
            _productPropertyRepository.Update(model.ID, model);
            return Json(note);
        }
        public IActionResult ScanFilesUpdateFalse(ProductProperty model)
        {
            string note = AppGlobal.InitString;
            model.Active = false;
            _productPropertyRepository.Update(model.ID, model);
            return Json(note);
        }
        public IActionResult CreateManyIndustry(int industryID, string title, int productParentID, string page, string totalSize, string timeLine, string duration, DateTime datePublish)
        {
            Config parent = _configResposistory.GetByID(productParentID);
            string note = AppGlobal.InitString;
            note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            Product model = new Product();
            model.Initialization(InitType.Insert, RequestUserID);
            model.ParentID = productParentID;
            model.Title = title;
            model.DatePublish = datePublish;
            List<ProductProperty> listProductProperty = _productPropertyRepository.GetRequestUserIDAndParentIDAndCodeAndDateUpdatedAndActiveToList(RequestUserID, -1, AppGlobal.URLCode, DateTime.Now, true);
            if (listProductProperty.Count > 0)
            {
                string fileExtension = listProductProperty[0].Page.Replace(@".", @"");
                if ((fileExtension == "mp4") || (fileExtension == "wmv"))
                {
                    model.IsVideo = true;
                    model.Image = listProductProperty[0].Note;
                    model.Page = timeLine;
                    model.Duration = duration;
                    try
                    {
                        if (parent != null)
                        {
                            if (parent.ID > 0)
                            {
                                int advalue = 1;
                                if (parent.Color > 0)
                                {
                                    advalue = parent.Color.Value;
                                }
                                int durationValue = int.Parse(duration);
                                advalue = advalue * durationValue / 30;
                                model.Advalue = advalue;
                            }
                        }
                    }
                    catch
                    {
                    }
                    _productRepository.Create(model);
                }
                else
                {
                    model.IsVideo = false;
                    model.Page = page;
                    model.Duration = totalSize;
                    try
                    {
                        if (parent != null)
                        {
                            if (parent.ID > 0)
                            {
                                int advalue = 1;
                                if (parent.Color > 0)
                                {
                                    advalue = parent.Color.Value;
                                }
                                int durationValue = int.Parse(totalSize);
                                advalue = advalue * durationValue / 100;
                                model.Advalue = advalue;                               
                            }
                        }
                    }
                    catch
                    {
                    }
                    _productRepository.Create(model);
                    if (model.ID > 0)
                    {
                        try
                        {
                            if (parent != null)
                            {
                                if (parent.ID > 0)
                                {                                   
                                    ProductProperty productProperty = new ProductProperty();
                                    productProperty.ParentID = model.ID;
                                    productProperty.Code = AppGlobal.URLCode;
                                    productProperty.Note = parent.Note;
                                    productProperty.Initialization(InitType.Insert, RequestUserID);
                                    _productPropertyRepository.Create(productProperty);
                                }
                            }
                        }
                        catch
                        {
                        }
                        foreach (ProductProperty item in listProductProperty)
                        {
                            ProductProperty productProperty = new ProductProperty();
                            productProperty.Active = false;
                            productProperty.FileName = item.FileName;
                            productProperty.Page = item.Page;
                            productProperty.Note = item.Note;
                            productProperty.ParentID = model.ID;
                            productProperty.Code = AppGlobal.URLCode;
                            productProperty.Initialization(InitType.Insert, RequestUserID);
                            _productPropertyRepository.Create(productProperty);
                        }
                    }
                }
                if (model.ID > 0)
                {
                    model.URLCode = AppGlobal.DomainMain + "Product/ViewContent/" + model.ID;
                    _productRepository.Update(model.ID, model);
                    ProductProperty productProperty = new ProductProperty();
                    productProperty.Initialization(InitType.Insert, RequestUserID);
                    productProperty.ParentID = model.ID;
                    productProperty.IndustryID = industryID;
                    productProperty.Code = AppGlobal.Industry;
                    _productPropertyRepository.Create(productProperty);
                }
            }
            return Json(note);
        }
        public IActionResult CreateAndNext(int industryID, string title, int productParentID, string page, string totalSize, string timeLine, string duration, DateTime datePublish)
        {
            Config parent = _configResposistory.GetByID(productParentID);
            string note = AppGlobal.InitString;
            note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            Product model = new Product();
            model.Initialization(InitType.Insert, RequestUserID);
            model.ParentID = productParentID;
            model.Title = title;
            model.DatePublish = datePublish;
            List<ProductProperty> listProductProperty = _productPropertyRepository.GetRequestUserIDAndParentIDAndCodeAndDateUpdatedAndActiveToList(RequestUserID, -1, AppGlobal.URLCode, DateTime.Now, true);
            if (listProductProperty.Count > 0)
            {
                string fileExtension = listProductProperty[0].Page.Replace(@".", @"");
                if ((fileExtension == "mp4") || (fileExtension == "wmv"))
                {
                    model.IsVideo = true;
                    model.Image = listProductProperty[0].Note;
                    model.Page = timeLine;
                    model.Duration = duration;
                    try
                    {
                        if (parent != null)
                        {
                            if (parent.ID > 0)
                            {
                                int advalue = 1;
                                if (parent.Color > 0)
                                {
                                    advalue = parent.Color.Value;
                                }
                                int durationValue = int.Parse(duration);
                                advalue = advalue * durationValue / 30;
                                model.Advalue = advalue;
                            }
                        }
                    }
                    catch
                    {
                    }
                    _productRepository.Create(model);
                }
                else
                {
                    model.IsVideo = false;
                    model.Page = page;
                    model.Duration = totalSize;
                    try
                    {
                        if (parent != null)
                        {
                            if (parent.ID > 0)
                            {
                                int advalue = 1;
                                if (parent.Color > 0)
                                {
                                    advalue = parent.Color.Value;
                                }
                                int durationValue = int.Parse(totalSize);
                                advalue = advalue * durationValue / 100;
                                model.Advalue = advalue;
                            }
                        }
                    }
                    catch
                    {
                    }
                    _productRepository.Create(model);
                    if (model.ID > 0)
                    {
                        if (parent != null)
                        {
                            if (parent.ID > 0)
                            {
                                ProductProperty productProperty = new ProductProperty();
                                productProperty.ParentID = model.ID;
                                productProperty.Code = AppGlobal.URLCode;
                                productProperty.Note = parent.Note;
                                productProperty.Initialization(InitType.Insert, RequestUserID);
                                _productPropertyRepository.Create(productProperty);
                            }
                        }
                        foreach (ProductProperty item in listProductProperty)
                        {
                            ProductProperty productProperty = new ProductProperty();
                            productProperty.Active = false;
                            productProperty.FileName = item.FileName;
                            productProperty.Page = item.Page;
                            productProperty.Note = item.Note;
                            productProperty.ParentID = model.ID;
                            productProperty.Code = AppGlobal.URLCode;
                            productProperty.Initialization(InitType.Insert, RequestUserID);
                            _productPropertyRepository.Create(productProperty);
                        }
                    }
                }
                if (model.ID > 0)
                {
                    model.URLCode = AppGlobal.DomainMain + "Product/ViewContent/" + model.ID;
                    _productRepository.Update(model.ID, model);
                    ProductProperty productProperty = new ProductProperty();
                    productProperty.Initialization(InitType.Insert, RequestUserID);
                    productProperty.ParentID = model.ID;
                    productProperty.IndustryID = industryID;
                    productProperty.Code = AppGlobal.Industry;
                    _productPropertyRepository.Create(productProperty);
                }
                foreach (ProductProperty item in listProductProperty)
                {
                    _productPropertyRepository.Delete(item.ID);
                }
            }
            return Json(note);
        }

        public IActionResult UpdateDataTransfer(ProductPropertyDataTransfer model)
        {
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            model.AssessID = model.AssessType.ID;
            int result = _productPropertyRepository.Update(model.ID, model);
            if (result > 0)
            {
                Product product = _productRepository.GetByID(model.ParentID.Value);
                if (product != null)
                {
                    switch (model.Code)
                    {
                        case "Industry":
                            if (product.IndustryID == model.IndustryID)
                            {
                                product.AssessID = model.AssessID;
                            }
                            break;
                        case "Product":
                            if (product.ProductID == model.ProductID)
                            {
                                product.AssessID = model.AssessID;
                            }
                            break;
                        case "Segment":
                            if (product.SegmentID == model.SegmentID)
                            {
                                product.AssessID = model.AssessID;
                            }
                            break;
                        case "Company":
                            if (product.CompanyID == model.CompanyID)
                            {
                                product.AssessID = model.AssessID;
                            }
                            break;
                    }
                    product.Initialization(InitType.Update, RequestUserID);
                    _productRepository.Update(product.ID, product);

                }
                note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.EditFail;
            }
            return Json(note);
        }

        public ActionResult UploadScanFiles(Commsights.MVC.Models.BaseViewModel baseViewModel)
        {
            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    for (int i = 0; i < Request.Form.Files.Count; i++)
                    {
                        var file = Request.Form.Files[i];
                        if (file == null || file.Length == 0)
                        {
                        }
                        if (file != null)
                        {
                            string fileExtension = Path.GetExtension(file.FileName);
                            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                            fileName = fileName + "-" + AppGlobal.DateTimeCode + fileExtension;
                            fileName = fileName.Replace(@"%", @"p");
                            string directoryDay = AppGlobal.DateTimeCodeYearMonthDay;
                            string mainPath = AppGlobal.FTPScanFiles;
                            string url = AppGlobal.URLScanFiles;
                            if (Directory.Exists(mainPath) == false)
                            {
                                mainPath = _hostingEnvironment.WebRootPath;
                                url = AppGlobal.Domain;
                            }
                            url = url + AppGlobal.SourceScan + "/" + directoryDay + "/" + fileName;
                            string subPath = AppGlobal.SourceScan + @"\" + directoryDay;
                            string fullPath = mainPath + @"\" + subPath;
                            if (!Directory.Exists(fullPath))
                            {
                                Directory.CreateDirectory(fullPath);
                            }
                            var physicalPath = Path.Combine(mainPath, subPath, fileName);
                            using (var stream = new FileStream(physicalPath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                                ProductProperty productProperty = new ProductProperty();
                                productProperty.Active = false;
                                productProperty.FileName = fileName;
                                productProperty.Page = fileExtension;
                                productProperty.Note = url;
                                productProperty.ParentID = -1;
                                productProperty.Code = AppGlobal.URLCode;
                                productProperty.Initialization(InitType.Insert, RequestUserID);
                                _productPropertyRepository.Create(productProperty);
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
                baseViewModel.ActionView = "ScanFilesHandling";
            }
            return RedirectToAction(baseViewModel.ActionView);
        }
    }
}
