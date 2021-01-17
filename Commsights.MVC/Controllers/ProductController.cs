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
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace Commsights.MVC.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IProductRepository _productRepository;
        private readonly IProductPropertyRepository _productPropertyRepository;
        private readonly IConfigRepository _configResposistory;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMembershipPermissionRepository _membershipPermissionRepository;
        private readonly IProductSearchRepository _productSearchRepository;
        private readonly IProductSearchPropertyRepository _productSearchPropertyRepository;
        public ProductController(IWebHostEnvironment hostingEnvironment, IProductRepository productRepository, IProductPropertyRepository productPropertyRepository, IProductSearchRepository productSearchRepository, IProductSearchPropertyRepository productSearchPropertyRepository, IConfigRepository configResposistory, IMembershipRepository membershipRepository, IMembershipPermissionRepository membershipPermissionRepository, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _productRepository = productRepository;
            _productPropertyRepository = productPropertyRepository;
            _productSearchRepository = productSearchRepository;
            _productSearchPropertyRepository = productSearchPropertyRepository;
            _configResposistory = configResposistory;
            _membershipRepository = membershipRepository;
            _membershipPermissionRepository = membershipPermissionRepository;
        }
        private void Initialization(Product model)
        {
            if (!string.IsNullOrEmpty(model.Title))
            {
                model.Title = model.Title.Trim();
            }
            if (!string.IsNullOrEmpty(model.URLCode))
            {
                model.URLCode = model.URLCode.Trim();
            }
            if (!string.IsNullOrEmpty(model.Description))
            {
                model.Description = model.Description.Trim();
            }
        }
        private void Initialization(ProductDataTransfer model)
        {
            if (!string.IsNullOrEmpty(model.Title))
            {
                model.Title = model.Title.Trim();
            }
            if (!string.IsNullOrEmpty(model.URLCode))
            {
                model.URLCode = model.URLCode.Trim();
            }
            if (!string.IsNullOrEmpty(model.Description))
            {
                model.Description = model.Description.Trim();
            }
            if (!string.IsNullOrEmpty(model.ContentMain))
            {
                model.ContentMain = model.ContentMain.Trim();
            }
            if (!string.IsNullOrEmpty(model.Author))
            {
                model.Author = model.Author.Trim();
            }
            if (model.ArticleType != null)
            {
                model.ArticleTypeID = model.ArticleType.ID;
            }
            if (model.Company != null)
            {
                model.CompanyID = model.Company.ID;
            }
            if (model.AssessType != null)
            {
                model.AssessID = model.AssessType.ID;
            }
        }
        public IActionResult Index()
        {
            BaseViewModel model = new BaseViewModel();
            model.DatePublish = DateTime.Now;
            return View(model);
        }
        public IActionResult Search()
        {
            ProductSearch model = new ProductSearch();
            DateTime now = DateTime.Now;
            model.DatePublishBegin = new DateTime(now.Year, now.Month, 1);
            model.DatePublishEnd = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));
            return View(model);
        }
        public IActionResult Upload()
        {
            return View();
        }
        public IActionResult Article()
        {
            BaseViewModel model = new BaseViewModel();
            model.DatePublish = DateTime.Now;
            return View(model);
        }
        public IActionResult ArticleByCompany()
        {
            BaseViewModel model = new BaseViewModel();
            model.DatePublish = DateTime.Now;
            return View(model);
        }
        public IActionResult ArticleByIndustry()
        {
            BaseViewModel model = new BaseViewModel();
            model.DatePublish = DateTime.Now;
            return View(model);
        }
        public IActionResult ArticleByProduct()
        {
            BaseViewModel model = new BaseViewModel();
            model.DatePublish = DateTime.Now;
            return View(model);
        }
        public IActionResult GoogleSearch()
        {
            ProductSearch model = new ProductSearch();
            model.DateSearch = DateTime.Now;
            model.DatePublishBegin = DateTime.Now;
            model.DatePublishEnd = DateTime.Now;
            model.SearchString = "Daily " + AppGlobal.DateTimeCode;
            model.Initialization(InitType.Insert, RequestUserID);
            return View(model);
        }
        public IActionResult ViewContent(int ID)
        {
            ProductViewContentViewModel model = new ProductViewContentViewModel();
            List<ProductProperty> listProductProperty = new List<ProductProperty>();
            if (ID > 0)
            {
                model.Product = _productRepository.GetByID001(ID);
                if (model.Product != null)
                {
                    listProductProperty = _productPropertyRepository.GetByParentIDAndCodeToList(model.Product.ID, AppGlobal.URLCode).OrderBy(item => item.Note).ToList();
                    model.Parent = _configResposistory.GetByID(model.Product.ParentID.Value);
                }

            }
            if (model.Product == null)
            {
                model.Product = _productRepository.GetByPriceUnitID(ID);
                if (model.Product != null)
                {
                    listProductProperty = _productPropertyRepository.GetByParentIDAndCodeToList(model.Product.ID, AppGlobal.URLCode).OrderBy(item => item.Note).ToList();
                }
            }
            if (model.ListProductProperty == null)
            {
                model.ListProductProperty = new List<ProductProperty>();
            }
            if (model.Product.IsVideo == false)
            {
                ProductProperty header = listProductProperty.FirstOrDefault(item => !string.IsNullOrEmpty(item.Note) && item.Note.Contains("getHeader.ashx") == true);
                if (header != null)
                {
                    ProductProperty productProperty01 = new ProductProperty();
                    productProperty01.ID = 1;
                    productProperty01.Note = header.Note;
                    model.ListProductProperty.Add(productProperty01);
                    int no = 2;
                    foreach (ProductProperty item in listProductProperty)
                    {
                        if (item.ID != header.ID)
                        {
                            ProductProperty productProperty02 = new ProductProperty();
                            productProperty02.ID = no;
                            productProperty02.Note = item.Note;
                            model.ListProductProperty.Add(productProperty02);
                            no = no + 1;
                        }
                    }
                }
                else
                {
                    model.ListProductProperty = listProductProperty;
                }
            }
            if (model.Product == null)
            {
                model.Product = new Product();
                model.Product.Title = "";
                model.Product.IsVideo = true;
                model.Product.Image = "";
            }
            return View(model);
        }
        public ActionResult GetByCategoryIDAndDatePublishToList([DataSourceRequest] DataSourceRequest request, int CategoryID, DateTime datePublish)
        {
            var data = _productRepository.GetByCategoryIDAndDatePublishToList(CategoryID, datePublish);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetBySearchToList([DataSourceRequest] DataSourceRequest request, string search)
        {
            var data = _productRepository.GetBySearchToList(search);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetBySearchAndDatePublishBeginAndDatePublishEndToList([DataSourceRequest] DataSourceRequest request, string search, DateTime datePublishBegin, DateTime datePublishEnd)
        {
            var data = _productRepository.GetByDatePublishBeginAndDatePublishEndAndSearchAndSourceToList(datePublishBegin, datePublishEnd, search, AppGlobal.SourceAuto);
            return Json(data.ToDataSourceResult(request));
        }
        public async Task<ActionResult> AsyncGetByDatePublishBeginAndDatePublishEndAndSearchAndSourceToList([DataSourceRequest] DataSourceRequest request, string search, DateTime datePublishBegin, DateTime datePublishEnd)
        {
            var data = await _productRepository.AsyncGetByDatePublishBeginAndDatePublishEndAndSearchAndSourceToList(datePublishBegin, datePublishEnd, search, AppGlobal.SourceAuto);
            return Json(data.ToDataSourceResult(request));
        }
        public async Task<ActionResult> AsyncGetProductCompactByDatePublishBeginAndDatePublishEndAndSearchAndSourceToList([DataSourceRequest] DataSourceRequest request, string search, DateTime datePublishBegin, DateTime datePublishEnd)
        {
            var data = await _productRepository.AsyncGetProductCompactByDatePublishBeginAndDatePublishEndAndSearchAndSourceToList(datePublishBegin, datePublishEnd, search, AppGlobal.SourceAuto);
            return Json(data.ToDataSourceResult(request));
        }
        public async Task<ActionResult> AsyncGetProductCompactByDatePublishBeginAndDatePublishEndAndSearchAndIsTitleAndIsDescriptionAndSourceAndIsPublishToList([DataSourceRequest] DataSourceRequest request, string search, DateTime datePublishBegin, DateTime datePublishEnd, bool isTitle, bool isDescription, bool isPublish)
        {
            var data = await _productRepository.AsyncGetProductCompactByDatePublishBeginAndDatePublishEndAndSearchAndIsTitleAndIsDescriptionAndSourceAndIsPublishToList(datePublishBegin, datePublishEnd, search, AppGlobal.SourceAuto, isTitle, isDescription, isPublish);
            return Json(data.ToDataSourceResult(request));
        }
        public async Task<ActionResult> AsyncGetProductCompactByDatePublishBeginAndDatePublishEndAndSearchAndIsTitleAndIsDescriptionAndSourceAndIsUploadToList([DataSourceRequest] DataSourceRequest request, string search, DateTime datePublishBegin, DateTime datePublishEnd, bool isTitle, bool isDescription, bool isUpload)
        {
            var data = await _productRepository.AsyncGetProductCompactByDatePublishBeginAndDatePublishEndAndSearchAndIsTitleAndIsDescriptionAndSourceAndIsUploadToList(datePublishBegin, datePublishEnd, search, AppGlobal.SourceAuto, isTitle, isDescription, isUpload);
            return Json(data.ToDataSourceResult(request));
        }
        public async Task<ActionResult> AsyncGetProductCompactByDatePublishBeginAndDatePublishEndAndSearchAndIsTitleAndIsDescriptionAndSourceToList([DataSourceRequest] DataSourceRequest request, string search, DateTime datePublishBegin, DateTime datePublishEnd, bool isTitle, bool isDescription)
        {
            var data = await _productRepository.AsyncGetProductCompactByDatePublishBeginAndDatePublishEndAndSearchAndIsTitleAndIsDescriptionAndSourceToList(datePublishBegin, datePublishEnd, search, AppGlobal.SourceAuto, isTitle, isDescription);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetByParentIDAndDatePublishToList([DataSourceRequest] DataSourceRequest request, int parentID, DateTime datePublish)
        {
            var data = _productRepository.GetByParentIDAndDatePublishToList(parentID, datePublish);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetByDatePublishToList([DataSourceRequest] DataSourceRequest request, DateTime datePublish)
        {
            var data = _productRepository.GetByDatePublishToList(datePublish);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferByDatePublishToList([DataSourceRequest] DataSourceRequest request, DateTime datePublish)
        {
            var data = _productRepository.GetDataTransferByDatePublishToList(datePublish);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferByDatePublishAndArticleTypeIDAndIndustryIDAndActionToList([DataSourceRequest] DataSourceRequest request, DateTime datePublish, int industryID, int action)
        {
            var data = _productRepository.GetDataTransferByDatePublishAndArticleTypeIDAndIndustryIDAndActionToList(datePublish, AppGlobal.TinNganhID, industryID, action);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferByDatePublishAndArticleTypeIDAndProductIDAndActionToList([DataSourceRequest] DataSourceRequest request, DateTime datePublish, int productID, int action)
        {
            var data = _productRepository.GetDataTransferByDatePublishAndArticleTypeIDAndProductIDAndActionToList(datePublish, AppGlobal.TinSanPhamID, productID, action);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferByDatePublishAndArticleTypeIDAndCompanyIDAndActionToList([DataSourceRequest] DataSourceRequest request, DateTime datePublish, int companyID, int action)
        {
            var data = _productRepository.GetDataTransferByDatePublishAndArticleTypeIDAndCompanyIDAndActionToList(datePublish, AppGlobal.TinDoanhNghiepID, companyID, action);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetByDateUpdatedToList([DataSourceRequest] DataSourceRequest request, DateTime dateUpdated)
        {
            var data = _productRepository.GetByDateUpdatedToList(dateUpdated);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferByProductSearchIDToList([DataSourceRequest] DataSourceRequest request, int productSearchID)
        {
            var data = _productRepository.GetDataTransferByProductSearchIDToList(productSearchID);
            return Json(data.ToDataSourceResult(request));
        }

        public IActionResult CreateDataTransfer(ProductDataTransfer model, int productSearchID)
        {
            Initialization(model);
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_productRepository.IsValid(model.URLCode))
            {
                result = _productRepository.Create(model);
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult UpdateDataTransfer(ProductDataTransfer model)
        {
            Initialization(model);
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _productRepository.Update(model.ID, model);
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
        public IActionResult UpdateReportDataTransfer(ProductDataTransfer model)
        {
            Initialization(model);
            model.AssessID = model.AssessType.ID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _productRepository.Update(model.ID, model);
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
        public IActionResult Update(Product model)
        {
            Initialization(model);
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _productRepository.Update(model.ID, model);
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
        public IActionResult UpdateProductCompact(ProductCompact model)
        {
            string note = AppGlobal.InitString;
            _productRepository.UpdateProductCompactSingleItem(model);
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
        public IActionResult Delete(int ID)
        {
            string note = AppGlobal.InitString;
            int result = _productRepository.Delete(ID);
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


        public void InitializationProduct(Product product)
        {
            product.IndustryID = AppGlobal.IndustryID;
            product.ArticleTypeID = AppGlobal.ArticleTypeID;
            product.AssessID = AppGlobal.AssessID;
            product.GUICode = AppGlobal.InitGuiCode;
            product.Initialization(InitType.Insert, RequestUserID);
        }
        public void ParseRSS(List<Product> list, Config item)
        {
            XmlDocument rssXmlDoc = new XmlDocument();
            rssXmlDoc.Load(item.URLFull.Trim());
            XmlNodeList rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");
            StringBuilder rssContent = new StringBuilder();
            foreach (XmlNode rssNode in rssNodes)
            {
                Product product = new Product();
                this.InitializationProduct(product);
                product.ParentID = item.ParentID;
                product.CategoryID = item.ID;
                product.Source = "Auto";
                XmlNode rssSubNode = rssNode.SelectSingleNode("title");
                product.Title = rssSubNode != null ? rssSubNode.InnerText : "";
                product.MetaTitle = AppGlobal.SetName(product.Title);
                product.MetaTitle = AppGlobal.SetName(product.Title);
                rssSubNode = rssNode.SelectSingleNode("link");
                product.URLCode = rssSubNode != null ? rssSubNode.InnerText : "";
                switch (product.ParentID)
                {
                    case 301:
                        rssSubNode = rssNode.SelectSingleNode("id");
                        product.URLCode = rssSubNode != null ? rssSubNode.InnerText : "";
                        break;
                }
                AppGlobal.GetURL(product);
                //this.GetAuthorFromURL(product);
                rssSubNode = rssNode.SelectSingleNode("description");
                product.Description = rssSubNode != null ? rssSubNode.InnerText : "";
                switch (product.ParentID)
                {
                    case 301:
                        rssSubNode = rssNode.SelectSingleNode("id");
                        product.URLCode = rssSubNode != null ? rssSubNode.InnerText : "";
                        rssSubNode = rssNode.SelectSingleNode("summary");
                        product.Description = rssSubNode != null ? rssSubNode.InnerText : "";
                        break;
                }
                rssSubNode = rssNode.SelectSingleNode("pubDate");
                string pubDate = rssSubNode != null ? rssSubNode.InnerText : "";
                try
                {
                    product.DatePublish = DateTime.Parse(pubDate);
                }
                catch
                {
                    product.DatePublish = DateTime.Now;
                }
                if (!string.IsNullOrEmpty(product.Title))
                {
                    product.Title = product.Title.Trim();
                }
                if (!string.IsNullOrEmpty(product.Description))
                {
                    product.Description = AppGlobal.RemoveHTMLTags(product.Description);
                    product.Description = product.Description.Trim();
                }
                if (!string.IsNullOrEmpty(product.URLCode))
                {
                    product.URLCode = product.URLCode.Trim();
                }
                if (!string.IsNullOrEmpty(product.Author))
                {
                    product.Author = product.Author.Trim();
                }
                if ((product.DatePublish.Year > 2019) && (product.DatePublish.Month > 6))
                {
                    if (_productRepository.IsValid(product.URLCode) == true)
                    {
                        product.ContentMain = AppGlobal.GetContentByURL(product.URLCode, product.ParentID.Value);
                        List<ProductProperty> listProductProperty = new List<ProductProperty>();
                        _productRepository.FilterProduct(product, listProductProperty, RequestUserID);
                        if (listProductProperty.Count > 0)
                        {
                            _productPropertyRepository.Range(listProductProperty);
                        }
                        list.Add(product);
                    }
                }
            }
        }
        public void ParseRSSNoFilterProduct(List<Product> list, Config item)
        {
            XmlDocument rssXmlDoc = new XmlDocument();
            rssXmlDoc.Load(item.URLFull.Trim());
            XmlNodeList rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");
            StringBuilder rssContent = new StringBuilder();
            foreach (XmlNode rssNode in rssNodes)
            {
                Product product = new Product();
                this.InitializationProduct(product);
                product.ParentID = item.ParentID;
                product.CategoryID = item.ID;
                product.Source = "Auto";
                XmlNode rssSubNode = rssNode.SelectSingleNode("title");
                product.Title = rssSubNode != null ? rssSubNode.InnerText : "";
                product.MetaTitle = AppGlobal.SetName(product.Title);
                product.MetaTitle = AppGlobal.SetName(product.Title);
                rssSubNode = rssNode.SelectSingleNode("link");
                product.URLCode = rssSubNode != null ? rssSubNode.InnerText : "";
                switch (product.ParentID)
                {
                    case 301:
                        rssSubNode = rssNode.SelectSingleNode("id");
                        product.URLCode = rssSubNode != null ? rssSubNode.InnerText : "";
                        break;
                }
                AppGlobal.GetURL(product);
                //this.GetAuthorFromURL(product);
                rssSubNode = rssNode.SelectSingleNode("description");
                product.Description = rssSubNode != null ? rssSubNode.InnerText : "";
                switch (product.ParentID)
                {
                    case 301:
                        rssSubNode = rssNode.SelectSingleNode("id");
                        product.URLCode = rssSubNode != null ? rssSubNode.InnerText : "";
                        rssSubNode = rssNode.SelectSingleNode("summary");
                        product.Description = rssSubNode != null ? rssSubNode.InnerText : "";
                        break;
                }
                rssSubNode = rssNode.SelectSingleNode("pubDate");
                string pubDate = rssSubNode != null ? rssSubNode.InnerText : "";
                try
                {
                    product.DatePublish = DateTime.Parse(pubDate);
                }
                catch
                {
                    product.DatePublish = DateTime.Now;
                }
                if (!string.IsNullOrEmpty(product.Title))
                {
                    product.Title = product.Title.Trim();
                }
                if (!string.IsNullOrEmpty(product.Description))
                {
                    product.Description = AppGlobal.RemoveHTMLTags(product.Description);
                    product.Description = product.Description.Trim();
                }
                if (!string.IsNullOrEmpty(product.URLCode))
                {
                    product.URLCode = product.URLCode.Trim();
                }
                if (!string.IsNullOrEmpty(product.Author))
                {
                    product.Author = product.Author.Trim();
                }
                if ((product.DatePublish.Year > 2019) && (product.DatePublish.Month > 6))
                {
                    if (_productRepository.IsValid(product.URLCode) == true)
                    {
                        product.ContentMain = AppGlobal.GetContentByURL(product.URLCode, product.ParentID.Value);
                        list.Add(product);
                    }
                }
            }
        }
        public IActionResult ScanFull()
        {
            //Product product = new Product();
            //product.Title = "Đại học Ngân hàng tăng điểm sàn thi THPT";
            //product.Description = "Đại học Ngân hàng TP HCM nâng sàn ở chương trình đại trà, chính quy chất lượng cao 1-2 điểm; giảm điểm sàn đánh giá năng lực.";
            //product.URLCode = "https://vnexpress.net/dai-hoc-ngan-hang-tang-diem-san-thi-thpt-4159752.html";
            //product.ContentMain = AppGlobal.GetContentByURL(product.URLCode);
            //List<ProductProperty> listProductProperty = new List<ProductProperty>();
            //this.FilterProduct(product, listProductProperty);

            List<Config> listConfig = _configResposistory.GetByGroupNameAndCodeAndActiveAndIsMenuLeftToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Website, false, true);
            foreach (Config item in listConfig)
            {
                if (item.IsMenuLeft == true)
                {
                    List<Product> list = new List<Product>();
                    try
                    {
                        this.ParseRSS(list, item);
                    }
                    catch (Exception e)
                    {
                        string message = e.Message;
                    }
                    if (list.Count > 0)
                    {
                        _productRepository.AddRange(list);
                        _productPropertyRepository.UpdateItemsWithParentIDIsZero();
                    }
                }
            }
            string note = AppGlobal.Success + " - " + AppGlobal.ScanFinish;
            return Json(note);
        }
        public IActionResult ScanWebsite(int websiteID)
        {
            List<Config> listConfig = _configResposistory.GetByParentIDToList(websiteID);
            foreach (Config item in listConfig)
            {
                if (item.IsMenuLeft == true)
                {
                    List<Product> list = new List<Product>();
                    try
                    {
                        this.ParseRSS(list, item);
                    }
                    catch (Exception e)
                    {
                        string message = e.Message;
                    }
                    if (list.Count > 0)
                    {
                        _productRepository.AddRange(list);
                        _productPropertyRepository.UpdateItemsWithParentIDIsZero();
                    }
                }
            }
            string note = AppGlobal.Success + " - " + AppGlobal.ScanFinish;
            return Json(note);
        }
        public IActionResult ScanFullNoFilterProduct()
        {
            List<Config> listConfig = _configResposistory.GetByGroupNameAndCodeAndActiveToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Website, true);
            foreach (Config item in listConfig)
            {
                this.CreateProductScanWebsiteNoFilterProduct0001(item);
            }
            string note = AppGlobal.Success + " - " + AppGlobal.ScanFinish;
            return Json(note);
        }
        public IActionResult ScanFullNoFilterProductByIndexBegin(int indexBegin)
        {
            List<Config> listConfig = _configResposistory.GetByGroupNameAndCodeAndActiveToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Website, true);
            int listConfigCount = listConfig.Count;
            int indexEnd = indexBegin + 10;
            for (int i = indexBegin; i < indexEnd; i++)
            {
                if (i == listConfigCount)
                {
                    i = indexEnd;
                }
                this.CreateProductScanWebsiteNoFilterProduct001(listConfig[i]);
            }
            string note = AppGlobal.Success + " - " + AppGlobal.ScanFinish;
            return Json(note);
        }
        public IActionResult ScanWebsiteNoFilterProduct(int websiteID)
        {
            if (websiteID > 0)
            {
                Config config = _configResposistory.GetByID(websiteID);
                this.CreateProductScanWebsiteNoFilterProduct001(config);
                //this.CreateProductScanWebsiteNoFilterProduct002();
            }

            //HttpWebRequest request;
            //WebResponse response;
            //try
            //{
            //    string urlAddress = "http://news.andi.vn/NewsDetail.aspx?12604079.349.2349875";
            //    request = (HttpWebRequest)WebRequest.Create(urlAddress);
            //    request.AllowAutoRedirect = true;
            //    HttpWebResponse response001 = (HttpWebResponse)request.GetResponse();
            //}
            //catch (WebException e)
            //{
            //    response = e.Response;
            //}
            string note = AppGlobal.Success + " - " + AppGlobal.ScanFinish;
            return Json(note);
        }
        public async Task<string> AsyncScanWebsiteNoFilterProduct(int websiteID)
        {
            if (websiteID > 0)
            {
                Config config = _configResposistory.GetByID(websiteID);
                await this.AsyncCreateProductScanWebsiteNoFilterProduct0001(config);
            }
            string note = AppGlobal.Success + " - " + AppGlobal.ScanFinish;
            return note;
        }
        public void ScanWebsiteNoFilterProductVoid(int websiteID)
        {
            if (websiteID > 0)
            {
                Config config = _configResposistory.GetByID(websiteID);
                this.CreateProductScanWebsiteNoFilterProduct001(config);
            }
        }
        public async Task<string> AsyncScanWebsiteNoFilterProductVoid(int websiteID)
        {
            if (websiteID > 0)
            {
                Config config = _configResposistory.GetByID(websiteID);
                await this.AsyncCreateProductScanWebsiteNoFilterProduct0001(config);
            }
            return "";
        }
        public void ScanWebsiteNoFilterProductByIndexBeginVoid(int indexBegin)
        {
            List<Config> listConfig = _configResposistory.GetByGroupNameAndCodeAndActiveToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Website, true);
            int listConfigCount = listConfig.Count;
            int indexEnd = indexBegin + 10;
            for (int i = indexBegin; i < indexEnd; i++)
            {
                if (i == listConfigCount)
                {
                    i = indexEnd;
                }
                this.CreateProductScanWebsiteNoFilterProduct001(listConfig[i]);
            }
        }
        public async Task<string> AsyncScanWebsiteNoFilterProductByIndexBeginVoid(int indexBegin)
        {
            indexBegin = indexBegin + 1;
            int indexEnd = indexBegin + 49;
            List<Config> listConfig = _configResposistory.GetSQLWebsiteByGroupNameAndCodeAndActiveAndRowBeginAndRowEndToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Website, true, indexBegin, indexEnd);
            foreach (Config item in listConfig)
            {
                await this.AsyncCreateProductScanWebsiteNoFilterProduct0001(item);
            }
            return "";
        }
        public async Task<string> AsyncScanWebsiteNoFilterProductByIndexBeginVoid001(int indexBegin)
        {
            List<Config> listConfig = _configResposistory.GetWebsiteToList();
            int listConfigCount = listConfig.Count;
            int indexEnd = indexBegin + 10;
            for (int i = indexBegin; i < indexEnd; i++)
            {
                if (i == listConfigCount)
                {
                    i = indexEnd;
                }
                await this.AsyncCreateProductScanWebsiteNoFilterProduct0001(listConfig[i]);
            }
            return "";
        }
        public async Task<string> AsyncScanWebsitePriorityNoFilterProductByIndexBeginVoid001(int indexBegin)
        {
            indexBegin = indexBegin + 1;
            int indexEnd = indexBegin + 5;
            List<Config> listConfig = _configResposistory.GetSQLWebsiteByGroupNameAndCodeAndActiveAndIsMenuLeftAndRowBeginAndRowEndToList(AppGlobal.CRM, AppGlobal.Website, true, true, indexBegin, indexEnd);
            foreach (Config item in listConfig)
            {
                await this.AsyncCreateProductScanWebsiteNoFilterProduct0001(item);
            }
            return "";
        }
        public async Task<string> AsyncDecode()
        {
            List<ProductCompact001> list = _productRepository.GetProductCompact001BySourceWithIDAndTitleToList(AppGlobal.SourceAuto);
            foreach (ProductCompact001 item in list)
            {
                item.Title = AppGlobal.ConvertStringToUnicode(item.Title);
                await _productRepository.AsyncUpdateProductCompact001SingleItem(item);
            }
            return "";
        }
        public async Task<string> AsyncDescription()
        {
            //string result = "B&#7843;n&#32;tin&#32;GolfNews&#32;360:&#32;K&#7923;&#32;156";
            //result = AppGlobal.Decode(result);
            //result = AppGlobal.ConvertASCIIToUnicode(result);
            //result = AppGlobal.ConvertLatin1ToUnicode(result);
            //result = AppGlobal.ConvertWind1252ToUnicode(result);
            //result = AppGlobal.TCVN3ToUnicode(result);
            List<ProductCompact001> list = _productRepository.GetProductCompact001BySourceWithIDAndTitleToList(AppGlobal.SourceAuto);
            foreach (ProductCompact001 item in list)
            {
                if ((item.URLCode.Contains(@"zing.vn") == false) && (item.URLCode.Contains(@"zingnews.vn") == false))
                {
                    string html = "";
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(item.URLCode);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Stream receiveStream = response.GetResponseStream();
                        StreamReader readStream = null;
                        readStream = new StreamReader(receiveStream, Encoding.UTF8);
                        html = readStream.ReadToEnd();
                    }
                    if (!string.IsNullOrEmpty(html))
                    {
                        item.Description = AppGlobal.GetDescription(html);
                        if (!string.IsNullOrEmpty(item.Description))
                        {
                            item.Description = AppGlobal.ConvertStringToUnicode(item.Description);
                            await _productRepository.AsyncUpdateProductCompact001SingleItemWithIDAndDescription(item);
                        }
                    }
                }
            }
            //List<ProductCompact001> list = _productRepository.GetProductCompact001BySourceWithIDAndTitleToList(AppGlobal.SourceAuto);
            //foreach (ProductCompact001 item in list)
            //{
            //    item.Title = AppGlobal.Decode(item.Title);
            //    await _productRepository.AsyncUpdateProductCompact001SingleItem(item);
            //}
            return "";
        }
        public async Task<string> AsyncDescriptionBySourceAndRowBeginAndRowEnd(int rowBegin, int rowEnd)
        {
            List<ProductCompact001> list = _productRepository.GetProductCompact001BySourceAndRowBeginAndRowEndWithIDAndDescriptionToList(AppGlobal.SourceAuto, rowBegin, rowEnd);
            foreach (ProductCompact001 item in list)
            {
                string description = "";
                if ((item.URLCode.Contains(@"zing.vn") == false) && (item.URLCode.Contains(@"zingnews.vn") == false))
                {
                    string html = "";
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(item.URLCode);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Stream receiveStream = response.GetResponseStream();
                        StreamReader readStream = null;
                        readStream = new StreamReader(receiveStream, Encoding.UTF8);
                        html = readStream.ReadToEnd();
                    }
                    if (!string.IsNullOrEmpty(html))
                    {
                        description = AppGlobal.GetDescription(html);
                    }
                }
                if (!string.IsNullOrEmpty(description))
                {
                    item.Description = AppGlobal.Decode(description);
                    await _productRepository.AsyncUpdateProductCompact001SingleItemWithIDAndDescription(item);
                }
            }
            return "";
        }
        public void CreateProductScanWebsiteNoFilterProduct(Config config)
        {
            if (config != null)
            {
                List<Config> listConfig = _configResposistory.GetByParentIDAndGroupNameAndCodeToList(config.ID, AppGlobal.CRM, AppGlobal.Website);
                foreach (Config item in listConfig)
                {
                    WebClient webClient = new WebClient();
                    webClient.Encoding = System.Text.Encoding.UTF8;
                    string html = "";
                    try
                    {
                        html = webClient.DownloadString(item.URLFull);
                        html = html.Replace(@"~", @"-");
                        html = html.Replace(@"<body", @"~<body");
                        if (html.Split('~').Length > 1)
                        {
                            html = html.Split('~')[1];
                        }
                        html = html.Replace(@"'", @"""");
                        html = html.Replace(@"</a>", @"</a>~");
                        html = html.Replace(@"<a", @"~<a");
                        int length = html.Split('~').Length;
                        for (int i = 1; i < length; i++)
                        {
                            string itemA = html.Split('~')[i];
                            if (itemA.Contains("</a>"))
                            {
                                if (itemA.Contains("href"))
                                {
                                    string title = AppGlobal.RemoveHTMLTags(itemA);
                                    if (!string.IsNullOrEmpty(title))
                                    {
                                        title = title.Replace(@"&nbsp;", @"");
                                        title = title.Trim();
                                        itemA = itemA.Replace(@"href=""", @"~");
                                        if (itemA.Split('~').Length > 1)
                                        {
                                            itemA = itemA.Split('~')[1];
                                            itemA = itemA.Split('"')[0];
                                            string url = itemA;
                                            if (!string.IsNullOrEmpty(url))
                                            {
                                                if (url.Contains("http") == false)
                                                {
                                                    string urlRoot = config.URLFull;
                                                    string lastChar = urlRoot[urlRoot.Length - 1].ToString();
                                                    if (lastChar.Contains(@"/") == true)
                                                    {
                                                        urlRoot = urlRoot.Substring(0, urlRoot.Length - 2);
                                                    }
                                                    url = urlRoot + url;
                                                }
                                                if ((url.Contains(";") == true) || (url.Contains("(") == true) || (url.Contains(")") == true) || (url.Contains("{") == true) || (url.Contains("}") == true))
                                                {

                                                }
                                                else
                                                {
                                                    if (_productRepository.IsValid(url) == true)
                                                    {
                                                        try
                                                        {
                                                            WebClient webClient001 = new WebClient();
                                                            webClient001.Encoding = System.Text.Encoding.UTF8;
                                                            string html001 = webClient001.DownloadString(url);
                                                            html001 = html001.Replace(@"~", @"-");
                                                            html001 = html001.Replace(@"<body", @"~<body");
                                                            if (html001.Split('~').Length > 1)
                                                            {
                                                                html001 = html001.Split('~')[1];
                                                            }
                                                            html001 = html001.Replace(@"<p", @"~<p");
                                                            html001 = html001.Replace(@"</p>", @"</p>~");
                                                            string description = "";
                                                            string content = "";
                                                            foreach (string content001 in html001.Split('~'))
                                                            {
                                                                if (content001.Contains("</p>"))
                                                                {
                                                                    string content002 = AppGlobal.RemoveHTMLTags(content001);
                                                                    if (!string.IsNullOrEmpty(content002))
                                                                    {
                                                                        description = description + " " + content002;
                                                                        content = content + "<br/>" + content002;
                                                                    }
                                                                }
                                                            }
                                                            Product product = new Product();
                                                            product.ParentID = config.ID;
                                                            product.CategoryID = item.ID;
                                                            product.Source = AppGlobal.SourceAuto;
                                                            product.Title = title;
                                                            product.MetaTitle = AppGlobal.SetName(product.Title);
                                                            product.Description = description;
                                                            product.ContentMain = content;
                                                            product.URLCode = url;
                                                            product.DatePublish = DateTime.Now;
                                                            product.Initialization(InitType.Insert, RequestUserID);
                                                            _productRepository.AsyncInsertSingleItem(product);
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
                    catch (Exception e)
                    {
                    }
                }
            }
        }
        public void CreateProductScanWebsiteNoFilterProduct001(Config config)
        {

            if (config != null)
            {
                List<Config> listConfig = _configResposistory.GetByParentIDAndGroupNameAndCodeToList(config.ID, AppGlobal.CRM, AppGlobal.Website);
                if (listConfig.Count == 0)
                {
                    try
                    {
                        string filename = DateTime.Now.ToString("yyyyMMdd") + "-" + config.ID + "-" + config.Title + "-0.txt";
                        var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, "Error", filename);
                        System.IO.File.Create(physicalPath);
                    }
                    catch
                    {
                    }
                }
                foreach (Config item in listConfig)
                {
                    WebClient webClient = new WebClient();
                    webClient.Encoding = System.Text.Encoding.UTF8;
                    string html = "";
                    try
                    {
                        html = webClient.DownloadString(item.URLFull);
                        List<LinkItem> list = AppGlobal.LinkFinder(html, config.URLFull);
                        foreach (LinkItem linkItem in list)
                        {
                            //if (_productRepository.IsValidBySQL(linkItem.Href) == true)
                            //{
                            try
                            {
                                WebClient webClient001 = new WebClient();
                                webClient001.Encoding = System.Text.Encoding.UTF8;
                                string html001 = webClient001.DownloadString(linkItem.Href);
                                Product product = new Product();
                                product.ParentID = config.ID;
                                product.CategoryID = config.ID;
                                product.Source = AppGlobal.SourceAuto;
                                product.Title = linkItem.Text;
                                product.MetaTitle = AppGlobal.SetName(product.Title);
                                product.URLCode = linkItem.Href;
                                product.DatePublish = DateTime.Now;
                                product.Initialization(InitType.Insert, RequestUserID);
                                product.DatePublish = DateTime.Now;
                                AppGlobal.FinderContentAndDatePublish(html001, product);
                                _productRepository.AsyncInsertSingleItem(product);
                            }
                            catch (Exception e1)
                            {
                                string mes1 = e1.Message;
                                try
                                {
                                    string filename = DateTime.Now.ToString("yyyyMMdd") + "-" + config.ID + "-" + config.Title + "-" + item.ID + "-" + item.URLFull + "-" + mes1 + ".txt";
                                    var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, "Error", filename);
                                    System.IO.File.Create(physicalPath);
                                }
                                catch
                                {
                                }
                            }
                            //}
                        }
                    }
                    catch (Exception e)
                    {
                        string mes = e.Message;
                        try
                        {
                            string filename = DateTime.Now.ToString("yyyyMMdd") + "-" + config.ID + "-" + config.Title + "-" + mes + ".txt";
                            var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, "Error", filename);
                            System.IO.File.Create(physicalPath);
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }
        public void CreateProductScanWebsiteNoFilterProduct0001(Config config)
        {
            if (config != null)
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = System.Text.Encoding.UTF8;
                string html = "";
                try
                {
                    html = webClient.DownloadString(config.URLFull);
                    List<LinkItem> list = AppGlobal.LinkFinder(html, config.URLFull);
                    foreach (LinkItem linkItem in list)
                    {
                        try
                        {
                            WebClient webClient001 = new WebClient();
                            webClient001.Encoding = System.Text.Encoding.UTF8;
                            string html001 = webClient001.DownloadString(linkItem.Href);
                            Product product = new Product();
                            product.ParentID = config.ID;
                            product.CategoryID = config.ID;
                            product.Source = AppGlobal.SourceAuto;
                            product.Title = linkItem.Text;
                            product.MetaTitle = AppGlobal.SetName(product.Title);
                            product.URLCode = linkItem.Href;
                            product.DatePublish = DateTime.Now;
                            product.Initialization(InitType.Insert, RequestUserID);
                            product.DatePublish = DateTime.Now;
                            AppGlobal.FinderContentAndDatePublish(html001, product);
                            if (product.Active == true)
                            {
                                _productRepository.AsyncInsertSingleItem(product);
                            }
                        }
                        catch (Exception e1)
                        {
                            string mes1 = e1.Message;

                        }
                    }
                }
                catch (Exception e)
                {
                    string mes = e.Message;
                }

            }
        }
        public async Task<string> AsyncCreateProductScanWebsiteNoFilterProduct001(Config config)
        {
            if (config != null)
            {
                List<Config> listConfig = _configResposistory.GetByParentIDAndGroupNameAndCodeToList(config.ID, AppGlobal.CRM, AppGlobal.Website);
                if (listConfig.Count == 0)
                {
                    try
                    {
                        string filename = DateTime.Now.ToString("yyyyMMdd") + "-" + config.ID + "-" + config.Title + "-0.txt";
                        var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, "Error", filename);
                        System.IO.File.Create(physicalPath);
                    }
                    catch
                    {
                    }
                }
                foreach (Config item in listConfig)
                {
                    WebClient webClient = new WebClient();
                    webClient.Encoding = System.Text.Encoding.UTF8;
                    string html = "";
                    try
                    {
                        html = webClient.DownloadString(item.URLFull);
                        List<LinkItem> list = AppGlobal.LinkFinder(html, config.URLFull);
                        foreach (LinkItem linkItem in list)
                        {
                            try
                            {
                                Uri myUri = new Uri(linkItem.Href);
                                string extend = myUri.LocalPath;
                                string domain = myUri.Host;
                                if (extend.Contains(".") == true)
                                {
                                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(linkItem.Href);
                                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                                    if (response.StatusCode == HttpStatusCode.OK)
                                    {
                                        Stream receiveStream = response.GetResponseStream();
                                        StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                                        string html001 = readStream.ReadToEnd();
                                        string htmlspan001 = html001;
                                        htmlspan001 = AppGlobal.HTMLReplaceAndSplit(htmlspan001);
                                        if ((domain.Contains(@"nhipcaudoanhnghiep.vn") == true) || (domain.Contains(@"vov.vn") == true))
                                        {
                                            if (htmlspan001.Contains(@"</h2>") == true)
                                            {
                                                string htmlspan = htmlspan001;
                                                MatchCollection m1 = Regex.Matches(htmlspan, @"(<h2.*?>.*?</h2>)", RegexOptions.Singleline);
                                                await AsyncInsertSingleItem(m1, config, linkItem, html001);
                                            }
                                        }
                                        else
                                        {
                                            if (htmlspan001.Contains(@"</h1>") == true)
                                            {
                                                string htmlspan = htmlspan001;
                                                MatchCollection m1 = Regex.Matches(htmlspan, @"(<h1.*?>.*?</h1>)", RegexOptions.Singleline);
                                                await AsyncInsertSingleItem(m1, config, linkItem, html001);
                                            }
                                        }
                                        response.Close();
                                        readStream.Close();
                                    }
                                }
                            }
                            catch (Exception e1)
                            {
                                string mes1 = e1.Message;
                                try
                                {
                                    string filename = DateTime.Now.ToString("yyyyMMdd") + "-" + config.ID + "-" + config.Title + "-" + item.ID + "-" + item.URLFull + "-" + mes1 + ".txt";
                                    var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, "Error", filename);
                                    System.IO.File.Create(physicalPath);
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        string mes = e.Message;
                        try
                        {
                            string filename = DateTime.Now.ToString("yyyyMMdd") + "-" + config.ID + "-" + config.Title + "-" + mes + ".txt";
                            var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, "Error", filename);
                            System.IO.File.Create(physicalPath);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return "";
        }
        //public async Task<string> AsyncCreateProductScanWebsiteNoFilterProduct0001(Config config)
        //{
        //    try
        //    {
        //        if (config != null)
        //        {
        //            List<LinkItem> list = new List<LinkItem>();
        //            AppGlobal.LinkFinder001(config.URLFull, config.URLFull, true, list);
        //            foreach (LinkItem linkItem in list)
        //            {
        //                try
        //                {
        //                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(linkItem.Href);
        //                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //                    if (response.StatusCode == HttpStatusCode.OK)
        //                    {
        //                        Stream receiveStream = response.GetResponseStream();
        //                        StreamReader readStream = null;
        //                        readStream = new StreamReader(receiveStream, Encoding.UTF8);
        //                        string html = readStream.ReadToEnd();
        //                        html = html.Replace(@"~", @"");
        //                        html = AppGlobal.HTMLReplaceAndSplit(html);
        //                        string title = "";
        //                        string htmlTitle = html;
        //                        if ((htmlTitle.Contains(@"<meta property=""og:title"" content=""") == true) || (htmlTitle.Contains(@"<meta property='og:title' content='") == true))
        //                        {
        //                            htmlTitle = htmlTitle.Replace(@"<meta property=""og:title"" content=""", @"~");
        //                            htmlTitle = htmlTitle.Replace(@"<meta property='og:title' content='", @"~");
        //                            if (htmlTitle.Split('~').Length > 1)
        //                            {
        //                                htmlTitle = htmlTitle.Split('~')[1];
        //                                htmlTitle = htmlTitle.Replace(@"""", @"~");
        //                                htmlTitle = htmlTitle.Replace(@"'", @"~");
        //                                htmlTitle = htmlTitle.Split('~')[0];
        //                                title = htmlTitle.Trim();
        //                            }
        //                        }
        //                        else
        //                        {
        //                            MatchCollection m1 = Regex.Matches(htmlTitle, @"(<title>.*?</title>)", RegexOptions.Singleline);
        //                            if (m1.Count > 0)
        //                            {
        //                                string value = m1[m1.Count - 1].Groups[1].Value;
        //                                if (!string.IsNullOrEmpty(value))
        //                                {
        //                                    value = value.Replace(@"<title>", @"");
        //                                    value = value.Replace(@"</title>", @"");
        //                                    title = value.Trim();
        //                                }
        //                            }
        //                        }
        //                        bool isUnicode = AppGlobal.ContainsUnicodeCharacter(title);
        //                        if ((title.Contains(@"&#") == true) || (isUnicode == false))
        //                        {
        //                            MatchCollection m1 = Regex.Matches(htmlTitle, @"(<title>.*?</title>)", RegexOptions.Singleline);
        //                            if (m1.Count > 0)
        //                            {
        //                                string value = m1[m1.Count - 1].Groups[1].Value;
        //                                if (!string.IsNullOrEmpty(value))
        //                                {
        //                                    value = value.Replace(@"<title>", @"");
        //                                    value = value.Replace(@"</title>", @"");
        //                                    title = value.Trim();
        //                                }
        //                            }
        //                        }
        //                        if (title.Split('|').Length > 2)
        //                        {
        //                            title = title.Split('|')[1];
        //                        }
        //                        if (title.Split('|').Length > 1)
        //                        {
        //                            title = title.Split('|')[0];
        //                        }
        //                        title = title.Trim();
        //                        Product product = new Product();
        //                        product.Description = "";
        //                        product.Title = title;
        //                        product.ParentID = config.ID;
        //                        product.CategoryID = config.ID;
        //                        product.Source = AppGlobal.SourceAuto;
        //                        if (string.IsNullOrEmpty(product.Title))
        //                        {
        //                            product.Title = linkItem.Text;
        //                        }
        //                        product.URLCode = linkItem.Href;
        //                        product.DatePublish = DateTime.Now;
        //                        product.Initialization(InitType.Insert, RequestUserID);
        //                        product.DatePublish = DateTime.Now;
        //                        AppGlobal.FinderContentAndDatePublish001(html, product);
        //                        if ((product.DatePublish.Year > 2019) && (product.Active == true))
        //                        {
        //                            if (!string.IsNullOrEmpty(product.Title))
        //                            {
        //                                product.Title = AppGlobal.Decode(product.Title);
        //                                product.MetaTitle = AppGlobal.SetName(product.Title);
        //                            }
        //                            if (!string.IsNullOrEmpty(product.Description))
        //                            {
        //                                product.Description = AppGlobal.Decode(product.Description);
        //                            }
        //                            await _productRepository.AsyncInsertSingleItem(product);
        //                        }
        //                        response.Close();
        //                        readStream.Close();
        //                    }
        //                }
        //                catch (Exception e1)
        //                {
        //                    string mes1 = e1.Message;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        string mes = e.Message;
        //    }
        //    return "";
        //}
        public async Task<string> AsyncCreateProductScanWebsiteNoFilterProduct0001(Config config)
        {
            try
            {
                if (config != null)
                {
                    List<LinkItem> list = new List<LinkItem>();
                    AppGlobal.LinkFinder002(config.URLFull, config.URLFull, true, list);
                    foreach (LinkItem linkItem in list)
                    {
                        Product product = new Product();
                        product.Description = "";
                        product.Title = AppGlobal.FinderTitle001(linkItem.Href);
                        product.ParentID = config.ID;
                        product.CategoryID = config.ID;
                        product.Source = AppGlobal.SourceAuto;
                        product.URLCode = linkItem.Href;
                        product.DatePublish = DateTime.Now;
                        product.Initialization(InitType.Insert, RequestUserID);
                        product.DatePublish = DateTime.Now;
                        string html = AppGlobal.FinderHTMLContent(linkItem.Href);
                        AppGlobal.FinderContentAndDatePublish002(html, product);
                        if ((product.DatePublish.Year > 2020) && (product.Active == true))
                        {
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
                            await _productRepository.AsyncInsertSingleItemAutoNoFilter(product);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string mes = e.Message;
            }
            return "";
        }
        public async Task<string> AsyncInsertSingleItem(MatchCollection m1, Config config, LinkItem linkItem, string html001)
        {
            if (m1.Count > 0)
            {
                string value = m1[m1.Count - 1].Groups[1].Value;
                if (!string.IsNullOrEmpty(value))
                {
                    if ((value.Contains("</span>") == false) && (value.Contains("</p>") == false) && (value.Contains("</a>") == false) && (value.Contains("</div>") == false))
                    {
                        string title = Regex.Replace(value, @"\s*<.*?>\s*", "", RegexOptions.Singleline);
                        title = title.Trim();
                        Product product = new Product();
                        product.Title = title;
                        product.ParentID = config.ID;
                        product.CategoryID = config.ID;
                        product.Source = AppGlobal.SourceAuto;
                        if (string.IsNullOrEmpty(product.Title))
                        {
                            product.Title = linkItem.Text;
                        }
                        product.MetaTitle = AppGlobal.SetName(product.Title);
                        product.URLCode = linkItem.Href;
                        product.DatePublish = DateTime.Now;
                        product.Initialization(InitType.Insert, RequestUserID);
                        product.DatePublish = DateTime.Now;
                        AppGlobal.FinderContentAndDatePublish(html001, product);
                        if ((product.DatePublish.Year > 2019) && (product.Active == true))
                        {
                            await _productRepository.AsyncInsertSingleItem(product);
                        }
                    }
                }
            }
            return "";
        }
        public void CreateProductScanWebsiteNoFilterProduct002()
        {

            LinkItem linkItem = new LinkItem();
            linkItem.Text = "More tourism festivals to occur as Covid-19 contained";
            linkItem.Href = "https://congthuong.vn/bo-cong-thuong-dieu-chinh-chinh-sach-quan-ly-phat-trien-cum-cong-nghiep-126465.html";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(linkItem.Href);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;
                    if (response.CharacterSet == null)
                    {
                        readStream = new StreamReader(receiveStream);
                    }
                    else
                    {
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }
                    string html001 = readStream.ReadToEnd();
                    html001 = html001.Replace(@"class=""tags", @"~");
                    html001 = html001.Split('~')[0];
                    html001 = html001.Replace(@"class='tags", @"~");
                    html001 = html001.Split('~')[0];
                    html001 = html001.Replace(@"tags"">", @"~");
                    html001 = html001.Split('~')[0];
                    html001 = html001.Replace(@"tags'>", @"~");
                    html001 = html001.Split('~')[0];
                    if (html001.Contains(@"</h1>") == true)
                    {
                        string htmlspan = html001;
                        MatchCollection m1 = Regex.Matches(htmlspan, @"(<h1.*?>.*?</h1>)", RegexOptions.Singleline);
                        if (m1.Count > 0)
                        {
                            string value = m1[m1.Count - 1].Groups[1].Value;
                            if (!string.IsNullOrEmpty(value))
                            {
                                if ((value.Contains("</span>") == false) && (value.Contains("</p>") == false) && (value.Contains("</a>") == false) && (value.Contains("</div>") == false))
                                {
                                    string title = Regex.Replace(value, @"\s*<.*?>\s*", "", RegexOptions.Singleline);
                                    title = title.Trim();
                                    Product product = new Product();
                                    product.Title = title;
                                    product.ParentID = 0;
                                    product.CategoryID = 0;
                                    product.Source = AppGlobal.SourceAuto;
                                    if (string.IsNullOrEmpty(product.Title))
                                    {
                                        product.Title = linkItem.Text;
                                    }
                                    product.MetaTitle = AppGlobal.SetName(product.Title);
                                    product.URLCode = linkItem.Href;
                                    product.DatePublish = DateTime.Now;
                                    product.Initialization(InitType.Insert, RequestUserID);
                                    product.DatePublish = DateTime.Now;
                                    AppGlobal.FinderContentAndDatePublish(html001, product);
                                }
                            }
                        }
                    }
                    response.Close();
                    readStream.Close();
                }
            }
            catch (Exception e1)
            {
                string mes1 = e1.Message;
            }
        }
    }
}

