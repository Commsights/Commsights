﻿using System;
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
            model.Product = new Product();
            model.Product.Title = "";
            model.ListProductProperty = new List<ProductProperty>();
            if (ID > 0)
            {
                model.Product = _productRepository.GetByID(ID);
                model.ListProductProperty = _productPropertyRepository.GetByParentIDAndCodeToList(ID, AppGlobal.URLCode);
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
                this.CreateProductScanWebsiteNoFilterProduct(item);
            }
            string note = AppGlobal.Success + " - " + AppGlobal.ScanFinish;
            return Json(note);
        }
        public IActionResult ScanWebsiteNoFilterProduct(int websiteID)
        {
            if (websiteID > 0)
            {
                Config config = _configResposistory.GetByID(websiteID);
                this.CreateProductScanWebsiteNoFilterProduct(config);
            }
            string note = AppGlobal.Success + " - " + AppGlobal.ScanFinish;
            return Json(note);
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
        public void ScanWebsiteNoFilterProductVoid(int websiteID)
        {
            if (websiteID > 0)
            {
                Config config = _configResposistory.GetByID(websiteID);
                this.CreateProductScanWebsiteNoFilterProduct(config);
            }
        }
    }
}
