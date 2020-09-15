using Commsights.Data.DataTransferObject;
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
        public ProductRepository(CommsightsContext context) : base(context)
        {
            _context = context;
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
    }
}
