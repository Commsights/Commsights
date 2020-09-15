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
    public class ProductController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IProductRepository _productRepository;
        private readonly IProductPropertyRepository _productPropertyRepository;
        private readonly IConfigRepository _configResposistory;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMembershipPermissionRepository _membershipPermissionRepository;
        private readonly IProductSearchRepository _productSearchRepository;
        private readonly IProductSearchPropertyRepository _productSearchPropertyRepository;
        public ProductController(IHostingEnvironment hostingEnvironment, IProductRepository productRepository, IProductPropertyRepository productPropertyRepository, IProductSearchRepository productSearchRepository, IProductSearchPropertyRepository productSearchPropertyRepository, IConfigRepository configResposistory, IMembershipRepository membershipRepository, IMembershipPermissionRepository membershipPermissionRepository, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
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
            model.ListProductSearchProperty = new List<ProductSearchProperty>();
            if (ID > 0)
            {
                model.Product = _productRepository.GetByID(ID);
                model.ListProductSearchProperty = _productSearchPropertyRepository.GetByProductIDToList(ID);
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
            var data = _productRepository.GetBySearchAndDatePublishBeginAndDatePublishEndToList(search, datePublishBegin, datePublishEnd);
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
        public void GetAuthorFromURL(Product product)
        {
            string html = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(product.URLCode);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            html = reader.ReadToEnd();
                        }
                        string author = html;
                        switch (product.ParentID)
                        {
                            case 1:
                                author = author.Replace(@"</article>", @"~");
                                author = author.Split('~')[0];
                                author = author.Replace(@"</h1>", @"~");
                                if (author.Split('~').Length > 1)
                                {
                                    author = author.Split('~')[1];
                                    author = author.Replace(@"<p class=""Normal"" style=""text-align:right;"">", @"~");
                                    author = author.Split('~')[author.Split('~').Length - 1];
                                    author = AppGlobal.RemoveHTMLTags(author);
                                }
                                break;
                            case 5:
                                author = author.Replace(@"<script type=""application/ld+json"">", @"~");
                                author = author.Split('~')[0];
                                author = author.Replace(@"dable:author", @"~");
                                if (author.Split('~').Length > 1)
                                {
                                    author = author.Split('~')[1];
                                    author = AppGlobal.SetContent(author);
                                    author = author.Replace(@"""", @"");
                                }
                                break;
                            case 6:
                                author = author.Replace(@"<div class=""tagandnetwork"" id=""tagandnetwork"">", @"~");
                                author = author.Split('~')[0];
                                author = author.Replace(@"<div class=""author"">", @"~");
                                if (author.Split('~').Length > 1)
                                {
                                    author = author.Split('~')[1];
                                    author = AppGlobal.RemoveHTMLTags(author);
                                }
                                break;
                            case 8:
                                author = author.Replace(@"<div class=""inner-article"">", @"~");
                                author = author.Split('~')[0];
                                author = author.Replace(@"<div class=""VnnAdsPos clearfix"" data-pos=""vt_article_bottom"">", @"~");
                                author = author.Split('~')[0];
                                author = author.Replace(@"<div class=""bold ArticleLead"">", @"~");
                                if (author.Split('~').Length > 1)
                                {
                                    author = author.Split('~')[1];
                                    author = author.Replace(@"<span class=""bold"">", @"~");
                                    author = author.Split('~')[author.Split('~').Length - 1];
                                    author = author.Replace(@"<p align=""justify"">", @"~");
                                    author = author.Split('~')[author.Split('~').Length - 1];
                                    author = author.Replace(@"</table>", @"~");
                                    author = author.Split('~')[author.Split('~').Length - 1];
                                    author = AppGlobal.RemoveHTMLTags(author);
                                }
                                break;
                            case 263:
                                author = author.Replace(@"<time class=""op-published""", @"~");
                                author = author.Split('~')[0];
                                author = author.Replace(@"<b  data-field=""author"" >", @"~");
                                if (author.Split('~').Length > 1)
                                {
                                    author = author.Split('~')[1];
                                    author = AppGlobal.RemoveHTMLTags(author);
                                    author = author.Replace(@"|", @"");
                                }
                                break;
                            case 278:
                                author = author.Replace(@"<div class=""inner-article"">", @"~");
                                author = author.Split('~')[0];
                                author = author.Replace(@"<strong>", @"~");
                                if (author.Split('~').Length > 1)
                                {
                                    author = author.Split('~')[author.Split('~').Length - 1];
                                    author = AppGlobal.RemoveHTMLTags(author);
                                }
                                break;
                            case 294:
                                author = author.Replace(@"<span class=""afcba-source"">", @"~");
                                author = author.Split('~')[0];
                                author = author.Replace(@"data-role=""author"">", @"~");
                                if (author.Split('~').Length > 1)
                                {
                                    author = author.Split('~')[author.Split('~').Length - 1];
                                    author = AppGlobal.RemoveHTMLTags(author);
                                    author = author.Replace(@",", @"");
                                }
                                break;
                            case 295:
                                author = author.Replace(@"<div class=""soucre_news"">", @"~");
                                author = author.Split('~')[0];
                                author = author.Replace(@"<p style=""text-align: right;"">", @"~");
                                if (author.Split('~').Length > 1)
                                {
                                    author = author.Split('~')[author.Split('~').Length - 1];
                                    author = AppGlobal.RemoveHTMLTags(author);
                                }
                                break;
                        }
                        author = author.Trim();
                        product.Author = author;
                    }
                }
            }
            catch
            {
            }
        }
        public void GetURL(Product product)
        {
            string url = product.URLCode;
            string f0 = "";
            string f1 = "";
            switch (product.ParentID)
            {
                case 295:
                    f0 = url.Split('-')[url.Split('-').Length - 1];
                    f1 = f0;
                    f1 = f1.Replace(@".html", @".htm");
                    f1 = f1.Replace(@"n", @"");
                    url = url.Replace(f0, f1);
                    product.URLCode = url;
                    break;
                case 182:
                    url = "https://baobinhphuoc.com.vn/Content/" + url;
                    product.URLCode = url;
                    break;
                case 395:
                    url = url.Replace(@".vn", @".vn/");
                    product.URLCode = url;
                    break;
                case 506:
                    f0 = url.Split('=')[url.Split('=').Length - 1];
                    url = "https://danang.gov.vn/web/guest/chi-tiet?id=" + f0;
                    product.URLCode = url;
                    break;
            }
        }
        public void InitializationProduct(Product product)
        {
            product.IndustryID = AppGlobal.IndustryID;
            product.ArticleTypeID = AppGlobal.ArticleTypeID;
            product.AssessID = AppGlobal.AssessID;
            product.GUICode = AppGlobal.InitGuiCode;
            product.Initialization(InitType.Insert, RequestUserID);
        }
        public void FilterProduct(Product product, List<ProductProperty> listProductProperty)
        {
            int order = 0;
            string keyword = "";
            bool title = false;
            int industryID = 0;
            int segmentID = 0;
            int productID = 0;
            int companyID = 0;
            Config segment = new Config();
            List<int> listProductID = new List<int>();
            List<int> listSegmentID = new List<int>();
            List<int> listIndustryID = new List<int>();
            List<int> listCompanyID = new List<int>();
            List<MembershipPermission> listProduct = _membershipPermissionRepository.GetByProductCodeToList(AppGlobal.Product);
            for (int i = 0; i < listProduct.Count; i++)
            {
                if (!string.IsNullOrEmpty(listProduct[i].ProductName))
                {
                    keyword = listProduct[i].ProductName.Trim();
                    int check = 0;
                    if (product.Title.Contains(keyword))
                    {
                        check = check + AppGlobal.CheckContentAndKeyword(product.Title, keyword);
                        title = true;
                        productID = listProduct[i].ID;
                        segmentID = listProduct[i].SegmentID.Value;
                        companyID = listProduct[i].MembershipID.Value;
                        segment = _configResposistory.GetByID(listProduct[i].SegmentID.Value);
                        if (segment != null)
                        {
                            industryID = segment.ParentID.Value;
                        }
                        listProductID.Add(productID);
                        listSegmentID.Add(segmentID);
                        listIndustryID.Add(industryID);
                        listCompanyID.Add(companyID);
                    }
                    if (product.Description.Contains(keyword))
                    {
                        check = check + AppGlobal.CheckContentAndKeyword(product.Description, keyword);
                    }
                    if (product.ContentMain.Contains(keyword))
                    {
                        check = check + AppGlobal.CheckContentAndKeyword(product.ContentMain, keyword);
                    }
                    if (check > 0)
                    {
                        ProductProperty productProperty = new ProductProperty();
                        productProperty.Initialization(InitType.Insert, RequestUserID);
                        productProperty.ParentID = 0;
                        productProperty.AssessID = AppGlobal.AssessID;
                        productProperty.GUICode = product.GUICode;
                        productProperty.Code = AppGlobal.Product;
                        segment = _configResposistory.GetByID(listProduct[i].SegmentID.Value);
                        if (order == 0)
                        {
                            product.ProductID = listProduct[i].ID;
                            product.SegmentID = listProduct[i].SegmentID;
                            if (segment != null)
                            {
                                product.IndustryID = segment.ParentID;
                            }
                        }
                        productProperty.ProductID = listProduct[i].ID;
                        productProperty.SegmentID = listProduct[i].SegmentID;
                        if (segment != null)
                        {
                            productProperty.IndustryID = segment.ParentID;
                        }
                        if (_productPropertyRepository.IsExistByGUICodeAndCodeAndProductID(productProperty.GUICode, productProperty.Code, productProperty.ProductID.Value) == false)
                        {
                            listProductProperty.Add(productProperty);
                        }
                        order = order + 1;
                    }
                }
            }
            if (title == true)
            {
                if (listProductID.Count > 0)
                {
                    product.ProductID = listProductID[0];
                }
                if (listSegmentID.Count > 0)
                {
                    product.SegmentID = listSegmentID[0];
                }
                if (listIndustryID.Count > 0)
                {
                    product.IndustryID = listIndustryID[0];
                }
                if (listCompanyID.Count > 0)
                {
                    product.CompanyID = listCompanyID[0];
                }
            }
            order = 0;
            title = false;
            listProductID = new List<int>();
            listSegmentID = new List<int>();
            listIndustryID = new List<int>();
            listCompanyID = new List<int>();
            List<Config> listSegment = _configResposistory.GetByGroupNameAndCodeToList(AppGlobal.CRM, AppGlobal.Segment);
            for (int i = 0; i < listSegment.Count; i++)
            {
                if (listSegment[i].ID != AppGlobal.SegmentID)
                {
                    int check = 0;
                    if (!string.IsNullOrEmpty(listSegment[i].Note))
                    {
                        keyword = listSegment[i].Note.Trim();

                        if (product.Title.Contains(keyword))
                        {
                            check = check + AppGlobal.CheckContentAndKeyword(product.Title, keyword);
                            title = true;
                            segmentID = listSegment[i].ID;
                            industryID = listSegment[i].ParentID.Value;
                            listSegmentID.Add(segmentID);
                            listIndustryID.Add(industryID);
                        }
                        if (product.Description.Contains(keyword))
                        {
                            check = check + AppGlobal.CheckContentAndKeyword(product.Description, keyword);
                        }
                        if (product.ContentMain.Contains(keyword))
                        {
                            check = check + AppGlobal.CheckContentAndKeyword(product.ContentMain, keyword);
                        }

                    }
                    if (!string.IsNullOrEmpty(listSegment[i].CodeName))
                    {
                        keyword = listSegment[i].CodeName.Trim();
                        if (product.Title.Contains(keyword))
                        {
                            check = check + AppGlobal.CheckContentAndKeyword(product.Title, keyword);
                            title = true;
                            segmentID = listSegment[i].ID;
                            industryID = listSegment[i].ParentID.Value;
                        }
                        if (product.Description.Contains(keyword))
                        {
                            check = check + AppGlobal.CheckContentAndKeyword(product.Description, keyword);
                        }
                        if (product.ContentMain.Contains(keyword))
                        {
                            check = check + AppGlobal.CheckContentAndKeyword(product.ContentMain, keyword);
                        }
                    }
                    if (check > 0)
                    {
                        ProductProperty productProperty = new ProductProperty();
                        productProperty.Initialization(InitType.Insert, RequestUserID);
                        productProperty.ParentID = 0;
                        productProperty.AssessID = AppGlobal.AssessID;
                        productProperty.GUICode = product.GUICode;
                        productProperty.Code = AppGlobal.Segment;
                        segment = _configResposistory.GetByID(listSegment[i].ParentID.Value);
                        if (order == 0)
                        {
                            product.SegmentID = listSegment[i].ID;
                            product.IndustryID = listSegment[i].ParentID;

                        }
                        productProperty.SegmentID = listSegment[i].ID;
                        productProperty.IndustryID = listSegment[i].ParentID;
                        if (_productPropertyRepository.IsExistByGUICodeAndCodeAndIndustryIDAndSegmentID(productProperty.GUICode, productProperty.Code, productProperty.IndustryID.Value, productProperty.SegmentID.Value) == false)
                        {
                            listProductProperty.Add(productProperty);
                        }
                        order = order + 1;
                    }
                }
            }
            if (title == true)
            {
                if (listSegmentID.Count > 0)
                {
                    product.SegmentID = listSegmentID[0];
                }
                if (listIndustryID.Count > 0)
                {
                    product.IndustryID = listIndustryID[0];
                }
            }
            order = 0;
            title = false;
            listProductID = new List<int>();
            listSegmentID = new List<int>();
            listIndustryID = new List<int>();
            listCompanyID = new List<int>();
            List<Config> listIndustry = _configResposistory.GetByGroupNameAndCodeToList(AppGlobal.CRM, AppGlobal.Industry);
            for (int i = 0; i < listIndustry.Count; i++)
            {
                if (listIndustry[i].ID != AppGlobal.IndustryID)
                {
                    int check = 0;
                    if (!string.IsNullOrEmpty(listIndustry[i].Note))
                    {
                        keyword = listIndustry[i].Note.Trim();

                        if (product.Title.Contains(keyword))
                        {
                            check = check + AppGlobal.CheckContentAndKeyword(product.Title, keyword);
                            title = true;
                            industryID = listIndustry[i].ID;
                            listIndustryID.Add(industryID);
                        }
                        if (product.Description.Contains(keyword))
                        {
                            check = check + AppGlobal.CheckContentAndKeyword(product.Description, keyword);
                        }
                        if (product.ContentMain.Contains(keyword))
                        {
                            check = check + AppGlobal.CheckContentAndKeyword(product.ContentMain, keyword);
                        }

                    }
                    if (!string.IsNullOrEmpty(listIndustry[i].CodeName))
                    {
                        keyword = listIndustry[i].CodeName.Trim();
                        if (product.Title.Contains(keyword))
                        {
                            check = check + AppGlobal.CheckContentAndKeyword(product.Title, keyword);
                            title = true;
                            industryID = listIndustry[i].ID;
                        }
                        if (product.Description.Contains(keyword))
                        {
                            check = check + AppGlobal.CheckContentAndKeyword(product.Description, keyword);
                        }
                        if (product.ContentMain.Contains(keyword))
                        {
                            check = check + AppGlobal.CheckContentAndKeyword(product.ContentMain, keyword);
                        }
                    }
                    if (check > 0)
                    {
                        ProductProperty productProperty = new ProductProperty();
                        productProperty.Initialization(InitType.Insert, RequestUserID);
                        productProperty.ParentID = 0;
                        productProperty.AssessID = AppGlobal.AssessID;
                        productProperty.GUICode = product.GUICode;
                        productProperty.Code = AppGlobal.Industry;
                        if (order == 0)
                        {
                            product.IndustryID = listIndustry[i].ID;
                        }
                        productProperty.IndustryID = listIndustry[i].ID;
                        if (_productPropertyRepository.IsExistByGUICodeAndCodeAndIndustryID(productProperty.GUICode, productProperty.Code, productProperty.IndustryID.Value) == false)
                        {
                            listProductProperty.Add(productProperty);
                        }
                        order = order + 1;
                    }
                }
            }
            if (title == true)
            {
                if (listIndustryID.Count > 0)
                {
                    product.IndustryID = listIndustryID[0];
                }
            }
            order = 0;
            title = false;
            listProductID = new List<int>();
            listSegmentID = new List<int>();
            listIndustryID = new List<int>();
            listCompanyID = new List<int>();
            List<Membership> listCompany = _membershipRepository.GetByCompanyToList();
            for (int i = 0; i < listCompany.Count; i++)
            {
                if (!string.IsNullOrEmpty(listCompany[i].Account))
                {
                    keyword = listCompany[i].Account.Trim();
                    int check = 0;
                    if (product.Title.Contains(keyword))
                    {
                        check = check + AppGlobal.CheckContentAndKeyword(product.Title, keyword);
                        title = true;
                        companyID = listCompany[i].ID;
                        listCompanyID.Add(companyID);
                    }
                    if (product.Description.Contains(keyword))
                    {
                        check = check + AppGlobal.CheckContentAndKeyword(product.Description, keyword);
                    }
                    if (product.ContentMain.Contains(keyword))
                    {
                        check = check + AppGlobal.CheckContentAndKeyword(product.ContentMain, keyword);
                    }
                    if (check > 0)
                    {
                        ProductProperty productProperty = new ProductProperty();
                        productProperty.Initialization(InitType.Insert, RequestUserID);
                        productProperty.ParentID = 0;
                        productProperty.AssessID = AppGlobal.AssessID;
                        productProperty.GUICode = product.GUICode;
                        productProperty.Code = AppGlobal.Company;
                        if (order == 0)
                        {
                            product.CompanyID = listCompany[i].ID;
                        }
                        productProperty.CompanyID = listCompany[i].ID;
                        if (_productPropertyRepository.IsExistByGUICodeAndCodeAndCompanyID(productProperty.GUICode, productProperty.Code, productProperty.CompanyID.Value) == false)
                        {
                            listProductProperty.Add(productProperty);
                        }
                        order = order + 1;
                    }
                }
            }
            if (title == true)
            {
                if (listCompanyID.Count > 0)
                {
                    product.CompanyID = listCompanyID[0];
                }
            }
            if (product.ProductID > 0)
            {
                product.ArticleTypeID = AppGlobal.TinSanPhamID;
            }
            else
            {
                if (product.CompanyID > 0)
                {
                    product.ArticleTypeID = AppGlobal.TinDoanhNghiepID;
                }
                else
                {
                    if ((product.SegmentID > 0) || (product.IndustryID != AppGlobal.IndustryID))
                    {
                        product.ArticleTypeID = AppGlobal.TinNganhID;
                    }
                }
            }
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
                this.GetURL(product);
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
                        this.FilterProduct(product, listProductProperty);
                        if (listProductProperty.Count > 0)
                        {
                            _productPropertyRepository.Range(listProductProperty);

                        }
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
        public ActionResult UploadScan(Commsights.MVC.Models.BaseViewModel baseViewModel)
        {
            int result = 0;
            string action = "Upload";
            string controller = "Product";
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
                                                ProductSearch productSearch = new ProductSearch();
                                                DateTime now = DateTime.Now;
                                                productSearch.DateSearch = now;
                                                productSearch.DatePublishBegin = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
                                                productSearch.DatePublishEnd = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
                                                productSearch.SearchString = "Scan" + AppGlobal.DateTimeCode;
                                                productSearch.Initialization(InitType.Insert, RequestUserID);
                                                _productSearchRepository.Create(productSearch);
                                                for (int i = 2; i <= totalRows; i++)
                                                {
                                                    Product model = new Product();
                                                    model.IndustryID = baseViewModel.IndustryIDUploadScan;
                                                    model.Initialization(InitType.Insert, RequestUserID);
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
                                                            //model.URLCode = AppGlobal.URLScan.Replace(@"[FileName]", model.FileName);
                                                            //model.URLCode = model.URLCode.Replace(@"[Type]", type);
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
                                                            _productRepository.Create(model);
                                                        }
                                                        if (model.ID > 0)
                                                        {
                                                            if (productSearch.ID > 0)
                                                            {
                                                                ProductSearchProperty productSearchProperty = new ProductSearchProperty();
                                                                productSearchProperty.ParentID = 0;
                                                                productSearchProperty.ProductID = model.ID;
                                                                productSearchProperty.ProductSearchID = productSearch.ID;
                                                                productSearchProperty.ArticleTypeID = AppGlobal.ArticleTypeID;
                                                                productSearchProperty.Initialization(InitType.Insert, RequestUserID);
                                                                _productSearchPropertyRepository.Create(productSearchProperty);
                                                                if (productSearchProperty.ID > 0)
                                                                {
                                                                    foreach (string item in company.Split(','))
                                                                    {
                                                                        string companyName = item.Trim();
                                                                        Membership membership = _membershipRepository.GetByAccount(companyName);
                                                                        if (membership == null)
                                                                        {
                                                                            membership = new Membership();
                                                                            membership.Account = companyName;
                                                                            membership.FullName = companyName;
                                                                            membership.ParentID = AppGlobal.ParentIDCompetitor;
                                                                            membership.Initialization(InitType.Insert, RequestUserID);
                                                                            _membershipRepository.Create(membership);
                                                                        }
                                                                        if (membership.ID > 0)
                                                                        {
                                                                            ProductSearchProperty productSearchPropertySub = new ProductSearchProperty();
                                                                            productSearchPropertySub.ParentID = productSearchProperty.ID;
                                                                            productSearchPropertySub.CompanyID = membership.ID;
                                                                            productSearchPropertySub.AssessID = AppGlobal.AssessID;
                                                                            productSearchPropertySub.Initialization(InitType.Insert, RequestUserID);
                                                                            _productSearchPropertyRepository.Create(productSearchPropertySub);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        result = productSearch.ID;
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
                action = "Detail";
                controller = "Product";
            }
            return RedirectToAction(action, controller, new { ID = result });
        }
        public ActionResult UploadYounet(Commsights.MVC.Models.BaseViewModel baseViewModel)
        {
            int result = 0;
            string action = "Upload";
            string controller = "Product";
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
                                                ProductSearch productSearch = new ProductSearch();
                                                DateTime now = DateTime.Now;
                                                productSearch.DateSearch = now;
                                                productSearch.DatePublishBegin = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
                                                productSearch.DatePublishEnd = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
                                                productSearch.SearchString = "Younet" + AppGlobal.DateTimeCode;
                                                productSearch.Initialization(InitType.Insert, RequestUserID);
                                                _productSearchRepository.Create(productSearch);
                                                for (int i = 2; i <= totalRows; i++)
                                                {
                                                    Product model = new Product();
                                                    model.IndustryID = baseViewModel.IndustryIDUploadYounet;
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
                                                            Config assess = _configResposistory.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.AssessType, assessString);
                                                            if (assess == null)
                                                            {
                                                                assess = new Config();
                                                                assess.CodeName = source;
                                                                _configResposistory.Create(assess);
                                                            }
                                                            model.PriceUnitId = assess.ID;
                                                        }
                                                        if (workSheet.Cells[i, 14].Value != null)
                                                        {
                                                            model.Title = workSheet.Cells[i, 14].Value.ToString().Trim();
                                                        }
                                                        if (workSheet.Cells[i, 15].Value != null)
                                                        {
                                                            model.Description = workSheet.Cells[i, 15].Value.ToString().Trim();
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
                                                        if (_productRepository.IsValid(model.URLCode))
                                                        {
                                                            model.MetaTitle = AppGlobal.SetName(model.Title);
                                                            model.CategoryID = model.ParentID;
                                                            model.TitleEnglish = "";
                                                            _productRepository.Create(model);
                                                        }
                                                        if (model.ID > 0)
                                                        {
                                                            if (productSearch.ID > 0)
                                                            {
                                                                ProductSearchProperty productSearchProperty = new ProductSearchProperty();
                                                                productSearchProperty.ParentID = 0;
                                                                productSearchProperty.ProductID = model.ID;
                                                                productSearchProperty.ProductSearchID = productSearch.ID;
                                                                productSearchProperty.ArticleTypeID = AppGlobal.ArticleTypeID;
                                                                productSearchProperty.Initialization(InitType.Insert, RequestUserID);
                                                                _productSearchPropertyRepository.Create(productSearchProperty);
                                                                if (productSearchProperty.ID > 0)
                                                                {
                                                                    if (model.PriceUnitId > 0)
                                                                    {
                                                                        ProductSearchProperty productSearchPropertySub = new ProductSearchProperty();
                                                                        productSearchPropertySub.ParentID = productSearchProperty.ID;
                                                                        productSearchPropertySub.CompanyID = AppGlobal.CompetitorID;
                                                                        productSearchPropertySub.AssessID = model.PriceUnitId;
                                                                        productSearchPropertySub.Initialization(InitType.Insert, RequestUserID);
                                                                        _productSearchPropertyRepository.Create(productSearchPropertySub);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        result = productSearch.ID;
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
                action = "Detail";
                controller = "ProductSearch";
            }
            return RedirectToAction(action, controller, new { ID = result });
        }
        public void GetURLByURLAndi(Product model, List<ProductSearchProperty> listProductSearchProperty)
        {
            string html = "";
            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = System.Text.Encoding.UTF8;
                html = webClient.DownloadString(model.ImageThumbnail);
                if (html.Contains(@"andi.vn"))
                {
                    model.IsVideo = false;
                    string content = html;
                    if (content.Contains(@"onclick=""showVideo('"))
                    {
                        content = content.Replace(@"onclick=""showVideo('", @"~");
                        if (content.Split('~').Length > 1)
                        {
                            content = content.Split('~')[1];
                            content = content.Replace(@"'", @"~");
                            content = content.Split('~')[0];
                            model.Image = "http://video.andi.vn/" + content;
                            model.IsVideo = true;
                        }
                    }
                    html = html.Replace(@"<div style=""text-align:center;"">", @"~");
                    if (html.Split('~').Length > 1)
                    {
                        html = html.Split('~')[1];
                        html = html.Replace(@"</div>", @"~");
                        html = html.Split('~')[0];
                        html = html.Replace(@"src='", @"~");
                        html = html.Replace(@"'", @"~");
                        foreach (string url in html.Split('~'))
                        {
                            if (url.Contains(@"http://"))
                            {
                                ProductSearchProperty productSearchProperty = new ProductSearchProperty();
                                productSearchProperty.Note = url;
                                productSearchProperty.ProductSearchID = 0;
                                model.Initialization(InitType.Insert, RequestUserID);
                                listProductSearchProperty.Add(productSearchProperty);
                            }
                        }
                    }
                }
                else
                {

                    html = html.Replace(@"rel=""canonical""", @"~");
                    if (html.Split('~').Length > 1)
                    {
                        html = html.Split('~')[1];
                        html = html.Replace(@"href=""", @"~");
                        if (html.Split('~').Length > 1)
                        {
                            html = html.Split('~')[1];
                            html = html.Split('"')[0];
                            model.URLCode = html.Trim();
                        }
                    }
                }
            }
            catch
            {
            }
        }
        public ActionResult UploadAndiSource(Commsights.MVC.Models.BaseViewModel baseViewModel)
        {
            int result = 0;
            string action = "Upload";
            string controller = "Product";
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
                                                ProductSearch productSearch = new ProductSearch();
                                                DateTime now = DateTime.Now;
                                                productSearch.DateSearch = now;
                                                productSearch.DatePublishBegin = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
                                                productSearch.DatePublishEnd = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
                                                productSearch.SearchString = "AndiSource" + AppGlobal.DateTimeCode;
                                                productSearch.Initialization(InitType.Insert, RequestUserID);
                                                _productSearchRepository.Create(productSearch);
                                                for (int i = 6; i <= totalRows; i++)
                                                {
                                                    List<ProductSearchProperty> listProductSearchProperty = new List<ProductSearchProperty>();
                                                    Membership membership = new Membership();
                                                    Product model = new Product();
                                                    model.IndustryID = baseViewModel.IndustryIDUploadAndiSource;
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
                                                        }
                                                        if (workSheet.Cells[i, 5].Value != null)
                                                        {
                                                            model.Title = workSheet.Cells[i, 5].Value.ToString().Trim();
                                                            model.ImageThumbnail = workSheet.Cells[i, 5].Hyperlink.AbsoluteUri.Trim();
                                                            this.GetURLByURLAndi(model, listProductSearchProperty);
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
                                                            _productRepository.Create(model);
                                                        }
                                                        if (model.ID > 0)
                                                        {
                                                            if (listProductSearchProperty.Count > 0)
                                                            {
                                                                model.URLCode = "/Product/ViewContent/" + model.ID;
                                                                _productRepository.Update(model.ID, model);
                                                                for (int j = 0; j < listProductSearchProperty.Count; j++)
                                                                {
                                                                    listProductSearchProperty[j].ProductID = model.ID;
                                                                }
                                                                _productSearchPropertyRepository.Range(listProductSearchProperty);
                                                            }
                                                            if (productSearch.ID > 0)
                                                            {
                                                                ProductSearchProperty productSearchProperty = new ProductSearchProperty();
                                                                productSearchProperty.ParentID = 0;
                                                                productSearchProperty.ProductID = model.ID;
                                                                productSearchProperty.ProductSearchID = productSearch.ID;
                                                                productSearchProperty.ArticleTypeID = AppGlobal.ArticleTypeID;
                                                                productSearchProperty.Initialization(InitType.Insert, RequestUserID);
                                                                _productSearchPropertyRepository.Create(productSearchProperty);
                                                                if (productSearchProperty.ID > 0)
                                                                {
                                                                    ProductSearchProperty productSearchPropertySub = new ProductSearchProperty();
                                                                    productSearchPropertySub.ParentID = productSearchProperty.ID;
                                                                    productSearchPropertySub.CompanyID = membership.ID;
                                                                    productSearchPropertySub.AssessID = AppGlobal.AssessID;
                                                                    productSearchPropertySub.Initialization(InitType.Insert, RequestUserID);
                                                                    _productSearchPropertyRepository.Create(productSearchPropertySub);
                                                                }
                                                            }
                                                        }
                                                        result = productSearch.ID;
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
                action = "Detail";
                controller = "ProductSearch";
            }
            return RedirectToAction(action, controller, new { ID = result });
        }

        public ActionResult UploadGoogleSearch(Commsights.MVC.Models.BaseViewModel baseViewModel)
        {
            int result = 0;
            string action = "Upload";
            string controller = "Product";
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
                                                ProductSearch productSearch = new ProductSearch();
                                                DateTime now = DateTime.Now;
                                                productSearch.DateSearch = now;
                                                productSearch.DatePublishBegin = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
                                                productSearch.DatePublishEnd = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
                                                productSearch.SearchString = "GoogleSearch" + AppGlobal.DateTimeCode;
                                                productSearch.Initialization(InitType.Insert, RequestUserID);
                                                _productSearchRepository.Create(productSearch);
                                                for (int i = 4; i <= totalRows; i++)
                                                {
                                                    Product model = new Product();
                                                    model.IndustryID = baseViewModel.IndustryIDUploadGoogleSearch;
                                                    model.Initialization(InitType.Insert, RequestUserID);
                                                    model.DatePublish = DateTime.Now;
                                                    model.ParentID = AppGlobal.WebsiteID;
                                                    model.CategoryID = AppGlobal.WebsiteID;
                                                    model.ArticleTypeID = AppGlobal.ArticleTypeID;
                                                    model.CompanyID = AppGlobal.CompetitorID;
                                                    model.AssessID = AppGlobal.AssessID;
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
                                                            string articleTypeName = workSheet.Cells[i, 2].Value.ToString().Trim();
                                                            Config articleType = _configResposistory.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.ArticleType, articleTypeName);
                                                            if (articleType == null)
                                                            {
                                                                articleType = new Config();
                                                                articleType.CodeName = articleTypeName;
                                                                articleType.Initialization(InitType.Insert, RequestUserID);
                                                                _configResposistory.Create(articleType);
                                                            }
                                                            model.ArticleTypeID = articleType.ID;
                                                        }
                                                        if (workSheet.Cells[i, 3].Value != null)
                                                        {
                                                            string companyName = workSheet.Cells[i, 3].Value.ToString().Trim();
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
                                                        if (workSheet.Cells[i, 4].Value != null)
                                                        {
                                                            string assessName = workSheet.Cells[i, 4].Value.ToString().Trim();
                                                            Config assess = _configResposistory.GetByGroupNameAndCodeAndCodeName(AppGlobal.CRM, AppGlobal.AssessType, assessName);
                                                            if (assess == null)
                                                            {
                                                                assess = new Config();
                                                                assess.CodeName = assessName;
                                                                assess.Initialization(InitType.Insert, RequestUserID);
                                                                _configResposistory.Create(assess);
                                                            }
                                                            model.AssessID = assess.ID;
                                                        }
                                                        if (workSheet.Cells[i, 5].Value != null)
                                                        {
                                                            model.Title = workSheet.Cells[i, 5].Value.ToString().Trim();
                                                            model.URLCode = workSheet.Cells[i, 5].Hyperlink.AbsoluteUri;


                                                        }
                                                        if (workSheet.Cells[i, 6].Value != null)
                                                        {
                                                            if (string.IsNullOrEmpty(model.URLCode))
                                                            {
                                                                model.URLCode = workSheet.Cells[i, 6].Value.ToString().Trim();
                                                            }
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
                                                        bool saveModel = true;
                                                        saveModel = _productRepository.IsValid(model.URLCode);
                                                        if (saveModel)
                                                        {
                                                            model.MetaTitle = AppGlobal.SetName(model.Title);
                                                            model.CategoryID = model.ParentID;
                                                            _productRepository.Create(model);
                                                        }
                                                        if (model.ID > 0)
                                                        {
                                                            if (productSearch.ID > 0)
                                                            {
                                                                ProductSearchProperty productSearchProperty = new ProductSearchProperty();
                                                                productSearchProperty.ParentID = 0;
                                                                productSearchProperty.ProductID = model.ID;
                                                                productSearchProperty.ProductSearchID = productSearch.ID;
                                                                productSearchProperty.ArticleTypeID = AppGlobal.ArticleTypeID;
                                                                productSearchProperty.Initialization(InitType.Insert, RequestUserID);
                                                                _productSearchPropertyRepository.Create(productSearchProperty);
                                                            }
                                                        }
                                                        result = productSearch.ID;
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
                action = "Detail";
                controller = "ProductSearch";
            }
            return RedirectToAction(action, controller, new { ID = result });
        }
    }
}
