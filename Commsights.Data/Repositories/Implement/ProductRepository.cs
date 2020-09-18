﻿using Commsights.Data.DataTransferObject;
using Commsights.Data.Enum;
using Commsights.Data.Helpers;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commsights.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly CommsightsContext _context;
        private readonly IMembershipPermissionRepository _membershipPermissionRepository;
        private readonly IConfigRepository _configResposistory;
        private readonly IProductPropertyRepository _productPropertyRepository;
        private readonly IMembershipRepository _membershipRepository;

        public ProductRepository(CommsightsContext context, IMembershipRepository membershipRepository, IMembershipPermissionRepository membershipPermissionRepository, IConfigRepository configResposistory, IProductPropertyRepository productPropertyRepository) : base(context)
        {
            _context = context;
            _membershipPermissionRepository = membershipPermissionRepository;
            _configResposistory = configResposistory;
            _productPropertyRepository = productPropertyRepository;
            _membershipRepository = membershipRepository;
        }
        public int AddRange(List<Product> list)
        {
            int result = 0;
            try
            {
                _context.Set<Product>().AddRange(list);
                result = _context.SaveChanges();
            }
            catch
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Product product = list[i];
                    product.ContentMain = "";
                    if (IsValid(product.URLCode) == true)
                    {
                        _context.Set<Product>().Add(product);
                        try
                        {
                            result = result + _context.SaveChanges();
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return result;
        }
        public List<Product> GetByCategoryIDAndDatePublishToList(int CategoryID, DateTime datePublish)
        {
            return _context.Product.Where(item => item.CategoryID == CategoryID && item.DatePublish.Year == datePublish.Year && item.DatePublish.Month == datePublish.Month && item.DatePublish.Day == datePublish.Day).OrderByDescending(item => item.DateUpdated).ToList();
        }
        public List<Product> GetByParentIDAndDatePublishToList(int parentID, DateTime datePublish)
        {
            return _context.Product.Where(item => item.ParentID == parentID && item.DatePublish.Year == datePublish.Year && item.DatePublish.Month == datePublish.Month && item.DatePublish.Day == datePublish.Day).OrderByDescending(item => item.DateUpdated).ToList();
        }
        public List<Product> GetByDatePublishToList(DateTime datePublish)
        {
            return _context.Product.Where(item => item.DatePublish.Year == datePublish.Year && item.DatePublish.Month == datePublish.Month && item.DatePublish.Day == datePublish.Day).OrderByDescending(item => item.DateUpdated).ToList();
        }
        public List<Product> GetByDateUpdatedToList(DateTime dateUpdated)
        {
            return _context.Product.Where(item => item.DateUpdated.Year == dateUpdated.Year && item.DateUpdated.Month == dateUpdated.Month && item.DateUpdated.Day == dateUpdated.Day).OrderByDescending(item => item.DateUpdated).ToList();
        }
        public List<Product> GetBySearchToList(string search)
        {
            List<Product> list = new List<Product>();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim();
                list = _context.Product.Where(item => item.Title.Contains(search) || item.Description.Contains(search) || item.ContentMain.Contains(search)).OrderByDescending(item => item.DatePublish).ToList();
            }
            return list;
        }
        public List<Product> GetBySearchAndDatePublishBeginAndDatePublishEndToList(string search, DateTime datePublishBegin, DateTime datePublishEnd)
        {
            List<Product> list = new List<Product>();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim();
                datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
                datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
                list = _context.Product.Where(item => (item.Title.Contains(search) || item.TitleEnglish.Contains(search) || item.MetaTitle.Contains(search) || item.Description.Contains(search) || item.Author.Contains(search)) && (datePublishBegin <= item.DatePublish && item.DatePublish <= datePublishEnd)).OrderByDescending(item => item.DatePublish).ToList();
            }
            return list;
        }
        public bool IsValid(string url)
        {
            Product item = null;
            if (!string.IsNullOrEmpty(url))
            {
                item = _context.Set<Product>().FirstOrDefault(item => item.URLCode.Equals(url));
            }
            else
            {
                item = new Product();
            }
            return item == null ? true : false;
        }
        public bool IsValidByFileNameAndDatePublish(string fileName, DateTime datePublish)
        {
            Product item = null;
            if (!string.IsNullOrEmpty(fileName))
            {
                item = _context.Set<Product>().FirstOrDefault(item => item.FileName.Equals(fileName) && item.DatePublish.Equals(datePublish));
            }
            return item == null ? true : false;
        }
        public Product GetByURLCode(string uRLCode)
        {
            Product product = new Product();
            if (!string.IsNullOrEmpty(uRLCode))
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@URLCode",uRLCode),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductSelectByURLCode", parameters);
                product = SQLHelper.ToList<ProductDataTransfer>(dt).FirstOrDefault();
            }
            return product;
        }
        public List<ProductDataTransfer> GetDataTransferByProductSearchIDToList(int productSearchID)
        {
            List<ProductDataTransfer> list = new List<ProductDataTransfer>();
            if (productSearchID > 0)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@ProductSearchID",productSearchID),
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductSelectDataTransferByProductSearchID", parameters);
                list = SQLHelper.ToList<ProductDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].ArticleType = new ModelTemplate();
                    list[i].ArticleType.ID = list[i].ArticleTypeID;
                    list[i].ArticleType.TextName = list[i].ArticleTypeName;
                    list[i].Company = new ModelTemplate();
                    list[i].Company.ID = list[i].CompanyID;
                    list[i].Company.TextName = list[i].CompanyName;
                    list[i].AssessType = new ModelTemplate();
                    list[i].AssessType.ID = list[i].AssessID;
                    list[i].AssessType.TextName = list[i].AssessName;
                }
            }

            return list;
        }
        public List<ProductDataTransfer> GetDataTransferByDatePublishToList(DateTime datePublish)
        {
            List<ProductDataTransfer> list = new List<ProductDataTransfer>();
            if (datePublish != null)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@DatePublish",datePublish),
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductSelectDataTransferByDatePublish", parameters);
                list = SQLHelper.ToList<ProductDataTransfer>(dt);
            }

            return list;
        }
        public List<ProductDataTransfer> GetDataTransferByDatePublishAndArticleTypeIDToList(DateTime datePublish, int articleTypeID)
        {
            List<ProductDataTransfer> list = new List<ProductDataTransfer>();
            if ((datePublish != null) && (articleTypeID > 0))
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@DatePublish",datePublish),
                new SqlParameter("@ArticleTypeID",articleTypeID)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductSelectDataTransferByDatePublishAndArticleTypeID", parameters);
                list = SQLHelper.ToList<ProductDataTransfer>(dt);
            }

            return list;
        }
        public List<ProductDataTransfer> GetDataTransferCompanyByDatePublishAndArticleTypeIDToList(DateTime datePublish, int articleTypeID)
        {
            List<ProductDataTransfer> list = new List<ProductDataTransfer>();
            if ((datePublish != null) && (articleTypeID > 0))
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@DatePublish",datePublish),
                new SqlParameter("@ArticleTypeID",articleTypeID)
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductSelectDataTransferCompanyByDatePublishAndArticleTypeID", parameters);
                list = SQLHelper.ToList<ProductDataTransfer>(dt);
            }

            return list;
        }
        public List<ProductDataTransfer> GetDataTransferProductByDatePublishAndArticleTypeIDToList(DateTime datePublish, int articleTypeID)
        {
            List<ProductDataTransfer> list = new List<ProductDataTransfer>();
            if ((datePublish != null) && (articleTypeID > 0))
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@DatePublish",datePublish),
                new SqlParameter("@ArticleTypeID",articleTypeID)
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductSelectDataTransferProductByDatePublishAndArticleTypeID", parameters);
                list = SQLHelper.ToList<ProductDataTransfer>(dt);
            }

            return list;
        }
        public List<ProductDataTransfer> GetDataTransferByDatePublishAndArticleTypeIDAndCompanyIDToList(DateTime datePublish, int articleTypeID, int companyID)
        {
            List<ProductDataTransfer> list = new List<ProductDataTransfer>();
            if ((datePublish != null) && (articleTypeID > 0) && (companyID > 0))
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@DatePublish",datePublish),
                new SqlParameter("@ArticleTypeID",articleTypeID),
                new SqlParameter("@CompanyID",companyID)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductSelectDataTransferByDatePublishAndArticleTypeIDAndCompanyID", parameters);
                list = SQLHelper.ToList<ProductDataTransfer>(dt);
            }

            return list;
        }
        public List<ProductDataTransfer> GetDataTransferByDatePublishAndArticleTypeIDAndIndustryIDToList(DateTime datePublish, int articleTypeID, int industryID)
        {
            List<ProductDataTransfer> list = new List<ProductDataTransfer>();
            if (datePublish != null)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@DatePublish",datePublish),
                new SqlParameter("@ArticleTypeID",articleTypeID),
                new SqlParameter("@IndustryID",industryID)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductSelectDataTransferByDatePublishAndArticleTypeIDAndIndustryID", parameters);
                list = SQLHelper.ToList<ProductDataTransfer>(dt);
            }

            return list;
        }
        public List<ProductDataTransfer> GetDataTransferByDatePublishAndArticleTypeIDAndProductIDToList(DateTime datePublish, int articleTypeID, int productID)
        {
            List<ProductDataTransfer> list = new List<ProductDataTransfer>();
            if (datePublish != null)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@DatePublish",datePublish),
                new SqlParameter("@ArticleTypeID",articleTypeID),
                new SqlParameter("@ProductID",productID)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductSelectDataTransferByDatePublishAndArticleTypeIDAndProductID", parameters);
                list = SQLHelper.ToList<ProductDataTransfer>(dt);
            }

            return list;
        }
        public List<ProductDataTransfer> GetDataTransferByDatePublishAndArticleTypeIDAndIndustryIDAndActionToList(DateTime datePublish, int articleTypeID, int industryID, int action)
        {
            List<ProductDataTransfer> list = new List<ProductDataTransfer>();
            switch (action)
            {
                case 0:
                    list = GetDataTransferByDatePublishAndArticleTypeIDToList(datePublish, articleTypeID);
                    break;
                case 1:
                    list = GetDataTransferByDatePublishAndArticleTypeIDAndIndustryIDToList(datePublish, articleTypeID, industryID);
                    break;
            }
            return list;
        }
        public List<ProductDataTransfer> GetDataTransferByDatePublishAndArticleTypeIDAndProductIDAndActionToList(DateTime datePublish, int articleTypeID, int productID, int action)
        {
            List<ProductDataTransfer> list = new List<ProductDataTransfer>();
            switch (action)
            {
                case 0:
                    list = GetDataTransferProductByDatePublishAndArticleTypeIDToList(datePublish, articleTypeID);
                    break;
                case 1:
                    list = GetDataTransferByDatePublishAndArticleTypeIDAndProductIDToList(datePublish, articleTypeID, productID);
                    break;
            }
            return list;
        }
        public List<ProductDataTransfer> GetDataTransferByDatePublishAndArticleTypeIDAndCompanyIDAndActionToList(DateTime datePublish, int articleTypeID, int companyID, int action)
        {
            List<ProductDataTransfer> list = new List<ProductDataTransfer>();
            switch (action)
            {
                case 0:
                    list = GetDataTransferCompanyByDatePublishAndArticleTypeIDToList(datePublish, articleTypeID);
                    break;
                case 1:
                    list = GetDataTransferByDatePublishAndArticleTypeIDAndCompanyIDToList(datePublish, articleTypeID, companyID);
                    break;
            }
            return list;
        }
        public List<ProductDataTransfer> ReportDailyByDatePublishAndCompanyIDToList(DateTime datePublish, int companyID)
        {
            List<ProductDataTransfer> list = new List<ProductDataTransfer>();
            if ((datePublish != null) && (companyID > 0))
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@DatePublish",datePublish),
                new SqlParameter("@CompanyID",companyID)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportDailyByDatePublishAndCompanyID", parameters);
                list = SQLHelper.ToList<ProductDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].AssessType = new ModelTemplate();
                    list[i].AssessType.ID = list[i].AssessID;
                    list[i].AssessType.TextName = list[i].AssessName;
                }
            }
            return list;
        }
        public List<ProductDataTransfer> ReportDailyProductByDatePublishAndCompanyIDToList(DateTime datePublish, int companyID)
        {
            List<ProductDataTransfer> list = new List<ProductDataTransfer>();
            if ((datePublish != null) && (companyID > 0))
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@DatePublish",datePublish),
                new SqlParameter("@CompanyID",companyID)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportDailyProductByDatePublishAndCompanyID", parameters);
                list = SQLHelper.ToList<ProductDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].AssessType = new ModelTemplate();
                    list[i].AssessType.ID = list[i].AssessID;
                    list[i].AssessType.TextName = list[i].AssessName;
                }
            }
            return list;
        }
        public List<ProductDataTransfer> ReportDailyIndustryByDatePublishAndCompanyIDToList(DateTime datePublish, int companyID)
        {
            List<ProductDataTransfer> list = new List<ProductDataTransfer>();
            if ((datePublish != null) && (companyID > 0))
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@DatePublish",datePublish),
                new SqlParameter("@CompanyID",companyID)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportDailyIndustryByDatePublishAndCompanyID", parameters);
                list = SQLHelper.ToList<ProductDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].AssessType = new ModelTemplate();
                    list[i].AssessType.ID = list[i].AssessID;
                    list[i].AssessType.TextName = list[i].AssessName;
                }
            }
            return list;
        }
        public List<ProductDataTransfer> ReportDailyCompetitorByDatePublishAndCompanyIDToList(DateTime datePublish, int companyID)
        {
            List<ProductDataTransfer> list = new List<ProductDataTransfer>();
            if ((datePublish != null) && (companyID > 0))
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@DatePublish",datePublish),
                new SqlParameter("@CompanyID",companyID)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportDailyCompetitorByDatePublishAndCompanyID", parameters);
                list = SQLHelper.ToList<ProductDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].AssessType = new ModelTemplate();
                    list[i].AssessType.ID = list[i].AssessID;
                    list[i].AssessType.TextName = list[i].AssessName;
                }
            }
            return list;
        }
        public void FilterProduct(Product product, List<ProductProperty> listProductProperty, int RequestUserID)
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
                        productProperty.ArticleTypeID = AppGlobal.TinSanPhamID;
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
                        productProperty.CompanyID = listProduct[i].MembershipID;
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
                        productProperty.ArticleTypeID = AppGlobal.TinNganhID;
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
                        productProperty.ArticleTypeID = AppGlobal.TinNganhID;
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
                    if ((keyword == "Tiki") || (keyword == "Lazada"))
                    {

                    }
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
                        productProperty.ArticleTypeID = AppGlobal.TinDoanhNghiepID;
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
    }
}
