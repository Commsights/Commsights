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
        public IActionResult ScanFiles()
        {
            CodeDataViewModel model = new CodeDataViewModel();
            model.DatePublishBegin = DateTime.Now;
            model.DatePublishEnd = DateTime.Now;
            model.IndustryID = AppGlobal.IndustryID;
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
        public ActionResult GetByIDAndCodeToList([DataSourceRequest] DataSourceRequest request, int ID)
        {
            var data = _productPropertyRepository.GetByIDAndCodeToList(ID, AppGlobal.ProductFeature);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetRequestUserIDAndParentIDAndCodeAndDateUpdatedAndFalseToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _productPropertyRepository.GetRequestUserIDAndParentIDAndCodeAndDateUpdatedAndActiveToList(RequestUserID, -1, AppGlobal.URLCode, DateTime.Now, false);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetRequestUserIDAndParentIDAndCodeAndFalseToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _productPropertyRepository.GetRequestUserIDAndParentIDAndCodeAndActiveToList(RequestUserID, -1, AppGlobal.URLCode, false);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetRequestUserIDAndParentIDAndCodeAndTrueToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _productPropertyRepository.GetRequestUserIDAndParentIDAndCodeAndActiveToList(RequestUserID, -1, AppGlobal.URLCode, true);
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
        public IActionResult ScanFilesUpdateTrue(int ID)
        {
            string note = AppGlobal.InitString;
            ProductProperty model = _productPropertyRepository.GetByID(ID);
            if (model != null)
            {
                model.Active = true;
                model.Initialization(InitType.Update, RequestUserID);
                _productPropertyRepository.Update(model.ID, model);
            }
            return Json(note);
        }
        public IActionResult ScanFilesUpdateFalse(int ID)
        {
            string note = AppGlobal.InitString;
            ProductProperty model = _productPropertyRepository.GetByID(ID);
            if (model != null)
            {
                model.Active = false;
                model.Initialization(InitType.Update, RequestUserID);
                _productPropertyRepository.Update(model.ID, model);
            }
            return Json(note);
        }
        public IActionResult UpdateProductPropertyAndProduct(CodeData model)
        {
            Config media = _configResposistory.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.PressList, model.MediaTitle);
            Product product = _productRepository.GetByID(model.ProductID.Value);
            if (product != null)
            {
                product.DatePublish = model.DatePublish;
                product.Title = model.Title;
                product.Advalue = (int)model.Advalue.Value;
                product.Page = model.Page;
                product.Duration = model.Duration;
                if (media != null)
                {
                    if (media.ID > 0)
                    {
                        product.ParentID = media.ID;
                        int advalue = 1;
                        if (media.Color > 0)
                        {
                            advalue = media.Color.Value;
                        }
                        int durationValue = int.Parse(product.Duration);
                        if (model.IsVideo == true)
                        {
                            advalue = advalue * durationValue / 30;
                        }
                        else
                        {
                            advalue = advalue * durationValue / 100;
                        }
                        product.Advalue = advalue;
                    }
                }
                if (model.Advalue < 0)
                {
                    model.Advalue = model.Advalue * -1;
                }
                product.Initialization(InitType.Update, RequestUserID);
                _productRepository.Update(product.ID, product);
            }
            Config industry = _configResposistory.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.Industry, model.Industry);
            if (industry != null)
            {
                if (industry.ID > 0)
                {
                    ProductProperty productProperty = _productPropertyRepository.GetByID(model.ProductPropertyID.Value);
                    if (productProperty != null)
                    {
                        productProperty.Advalue = 0;
                        productProperty.MediaTitle = "";
                        productProperty.IndustryID = industry.ID;
                        productProperty.Initialization(InitType.Update, RequestUserID);
                        _productPropertyRepository.Update(productProperty.ID, productProperty);
                    }
                }
            }
            string note = AppGlobal.InitString;
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
        public IActionResult DeleteProductProperty(int productPropertyID)
        {
            string note = AppGlobal.InitString;
            //_productPropertyRepository.DeleteItemsByID(productPropertyID);
            _productPropertyRepository.Delete(productPropertyID);
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
        public IActionResult Delete(int ID)
        {
            string note = AppGlobal.InitString;
            int result = _productPropertyRepository.Delete(ID);
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
        [HttpPost]
        public IActionResult DeleteByIDList(string IDList)
        {
            _productPropertyRepository.DeleteItemsByIDList(IDList);
            string note = AppGlobal.InitString;
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
        [HttpPost]
        public IActionResult CreateManyIndustry2021(int industryID, string title, int productParentID, string page, string totalSize, string timeLine, string duration, DateTime datePublish)
        {
            string note = AppGlobal.InitString;
            List<ProductProperty> listProductProperty = _productPropertyRepository.GetRequestUserIDAndParentIDAndCodeAndDateUpdatedAndActiveToList(RequestUserID, -1, AppGlobal.URLCode, DateTime.Now, true);
            string fileExtension = listProductProperty[0].Page.Replace(@".", @"");
            int check = 0;
            if (!string.IsNullOrEmpty(title))
            {
                check = check + 1;
            }
            if ((fileExtension == "mp4") || (fileExtension == "wmv"))
            {
                if (!string.IsNullOrEmpty(timeLine))
                {
                    check = check + 1;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(page))
                {
                    check = check + 1;
                }
            }
            if (check == 2)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
                Config parent = _configResposistory.GetByID(productParentID);
                Product model = new Product();
                model.Initialization(InitType.Insert, RequestUserID);
                model.ParentID = productParentID;
                model.Title = title;
                model.DatePublish = datePublish;

                if (listProductProperty.Count > 0)
                {
                    if ((fileExtension == "mp4") || (fileExtension == "wmv"))
                    {
                        model.IsVideo = true;
                        model.Image = listProductProperty[0].Note;
                        model.Page = timeLine;
                        model.Duration = duration;
                        model.Source = AppGlobal.TV;
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
                                    duration = duration.Replace(@"s", @"");
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
                        model.Source = AppGlobal.Newspage;
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
                                    totalSize = totalSize.Replace(@"%", @"");
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
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        [HttpPost]
        public IActionResult CreateAndNext2021(int industryID, string title, int productParentID, string page, string totalSize, string timeLine, string duration, DateTime datePublish)
        {
            string note = AppGlobal.InitString;
            List<ProductProperty> listProductProperty = _productPropertyRepository.GetRequestUserIDAndParentIDAndCodeAndDateUpdatedAndActiveToList(RequestUserID, -1, AppGlobal.URLCode, DateTime.Now, true);
            string fileExtension = listProductProperty[0].Page.Replace(@".", @"");
            int check = 0;
            if (!string.IsNullOrEmpty(title))
            {
                check = check + 1;
            }
            if ((fileExtension == "mp4") || (fileExtension == "wmv"))
            {
                if (!string.IsNullOrEmpty(timeLine))
                {
                    check = check + 1;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(page))
                {
                    check = check + 1;
                }
            }
            if (check == 2)
            {
                Config parent = _configResposistory.GetByID(productParentID);
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
                Product model = new Product();
                model.Initialization(InitType.Insert, RequestUserID);
                model.ParentID = productParentID;
                model.Title = title;
                model.DatePublish = datePublish;
                if (listProductProperty.Count > 0)
                {
                    if ((fileExtension == "mp4") || (fileExtension == "wmv"))
                    {
                        model.Source = AppGlobal.TV;
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
                                    duration = duration.Replace(@"s", @"");
                                    int durationValue = int.Parse(duration);
                                    advalue = advalue * durationValue / 30;
                                    model.Advalue = advalue;
                                }
                            }
                        }
                        catch
                        {
                        }
                        if (model.Advalue < 0)
                        {
                            model.Advalue = model.Advalue * -1;
                        }
                        _productRepository.Create(model);
                    }
                    else
                    {
                        model.Source = AppGlobal.Newspage;
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
                                    totalSize = totalSize.Replace(@"%", @"");
                                    int durationValue = int.Parse(totalSize);
                                    advalue = advalue * durationValue / 100;
                                    model.Advalue = advalue;
                                }
                            }
                        }
                        catch
                        {
                        }
                        if (model.Advalue < 0)
                        {
                            model.Advalue = model.Advalue * -1;
                        }
                        _productRepository.Create(model);
                        if (model.ID > 0)
                        {
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
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
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

        public IActionResult ScanFilesCopyProductPropertyAndProduct(int productPropertyID, int productID)
        {
            Product product = _productRepository.GetByID(productID);
            if (product != null)
            {
                if (product.ID > 0)
                {
                    product.ID = 0;
                    product.Initialization(InitType.Insert, RequestUserID);
                    _productRepository.Create(product);
                    if (product.ID > 0)
                    {
                        ProductProperty productProperty = _productPropertyRepository.GetByID(productPropertyID);
                        if (productProperty != null)
                        {
                            if (productProperty.ID > 0)
                            {
                                productProperty.ID = 0;
                                productProperty.ParentID = product.ID;
                                productProperty.Initialization(InitType.Insert, RequestUserID);
                                _productPropertyRepository.Create(productProperty);
                            }
                        }
                    }
                }
            }
            string note = AppGlobal.InitString;
            int result = 1;
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
                            fileName = file.FileName;
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
            return RedirectToAction("ScanFilesHandling");
        }
        public async Task<ActionResult> AsyncUploadScanFiles(Commsights.MVC.Models.BaseViewModel baseViewModel)
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
                            fileName = file.FileName;
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
                                await file.CopyToAsync(stream);
                                ProductProperty productProperty = new ProductProperty();
                                productProperty.Active = false;
                                productProperty.FileName = fileName;
                                productProperty.Page = fileExtension;
                                productProperty.Note = url;
                                productProperty.ParentID = -1;
                                productProperty.Code = AppGlobal.URLCode;
                                productProperty.Initialization(InitType.Insert, RequestUserID);
                                await _productPropertyRepository.AsyncCreate(productProperty);
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return RedirectToAction("ScanFilesHandling");
        }

        public ActionResult UploadScanFilesNoUploadFiles()
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
                            fileName = file.FileName;
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
            catch (Exception e)
            {
                string mes = e.Message;
            }
            return RedirectToAction("ScanFilesHandling");
        }
        public ActionResult UploadScanFilesByFileNameList(string fileNameList)
        {
            for (int i = 0; i < fileNameList.Split(';').Length; i++)
            {
                string fileName = fileNameList.Split(';')[i];
                if (!string.IsNullOrEmpty(fileName))
                {
                    string fileExtension = fileName.Split('.')[fileName.Split('.').Length - 1];
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
            return RedirectToAction("ScanFilesHandling");
        }
    }
}
