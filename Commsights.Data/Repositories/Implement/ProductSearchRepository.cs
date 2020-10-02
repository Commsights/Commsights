using Commsights.Data.DataTransferObject;
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
    public class ProductSearchRepository : Repository<ProductSearch>, IProductSearchRepository
    {
        private readonly CommsightsContext _context;
        public ProductSearchRepository(CommsightsContext context) : base(context)
        {
            _context = context;
        }
        public List<ProductSearchDataTransfer> GetByDatePublishBeginAndDatePublishEndAndIndustryIDToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID)
        {
            List<ProductSearchDataTransfer> list = new List<ProductSearchDataTransfer>();
            if (industryID > 0)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@DatePublishBegin",datePublishBegin),
                new SqlParameter("@DatePublishEnd",datePublishEnd),
                new SqlParameter("@IndustryID",industryID),
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ReportDailyByDatePublishBeginAndDatePublishEndAndIndustryID", parameters);
                list = SQLHelper.ToList<ProductSearchDataTransfer>(dt);
            }
            return list;
        }
        
        public string UpdateByID(int ID, int userUpdated, bool isSend)
        {
            string result = "";
            if (ID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ID",ID),
                    new SqlParameter("@UserUpdated",userUpdated),
                    new SqlParameter("@IsSend",isSend),
                };
                result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ProductSearchUpdateByID", parameters);
            }
            return result;
        }
        public ProductSearchDataTransfer GetDataTransferByID(int ID)
        {
            ProductSearchDataTransfer model = new ProductSearchDataTransfer();
            if (ID > 0)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@ID",ID),
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductSearchDataTransferByID", parameters);
                model = SQLHelper.ToList<ProductSearchDataTransfer>(dt).FirstOrDefault();
            }
            return model;
        }
        public List<ProductSearchDataTransfer> InitializationByDatePublishToList(DateTime datePublish)
        {
            List<ProductSearchDataTransfer> list = new List<ProductSearchDataTransfer>();
            if (datePublish.Year > 2019)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@DatePublish",datePublish),
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductSearchInitializationByDatePublish", parameters);
                list = SQLHelper.ToList<ProductSearchDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Company = new ModelTemplate();
                    list[i].Company.ID = list[i].CompanyID;
                    list[i].Company.TextName = list[i].CompanyName;
                }
            }

            return list;
        }
        public List<ProductSearch> GetByDateSearchBeginAndDateSearchEndToList(DateTime dateSearchBegin, DateTime dateSearchEnd)
        {
            List<ProductSearch> list = new List<ProductSearch>();
            if (dateSearchBegin.Year > 2000)
            {
                dateSearchBegin = new DateTime(dateSearchBegin.Year, dateSearchBegin.Month, dateSearchBegin.Day, 0, 0, 0);
                dateSearchEnd = new DateTime(dateSearchEnd.Year, dateSearchEnd.Month, dateSearchEnd.Day, 23, 59, 59);
                list = _context.ProductSearch.Where(item => (dateSearchBegin <= item.DateSearch && item.DateSearch <= dateSearchEnd)).OrderByDescending(item => item.DateSearch).ToList();
            }
            return list;
        }
        public ProductSearch SaveProductSearch(string search, DateTime datePublishBegin, DateTime datePublishEnd, int requestUserID, bool isAll)
        {
            ProductSearch productSearch = new ProductSearch();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim();
                productSearch.SearchString = search;
                productSearch.DateSearch = DateTime.Now;
                productSearch.DatePublishBegin = datePublishBegin;
                productSearch.DatePublishEnd = datePublishEnd;
                _context.Set<ProductSearch>().Add(productSearch);
                _context.SaveChanges();
                List<Product> listProduct = new List<Product>();
                List<ProductSearchProperty> listProductSearchProperty = new List<ProductSearchProperty>();
                datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
                datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
                listProduct = _context.Product.Where(item => (item.Title.Contains(search) || item.Description.Contains(search)) && (datePublishBegin <= item.DatePublish && item.DatePublish <= datePublishEnd)).OrderByDescending(item => item.DatePublish).ToList();
                for (int i = 0; i < listProduct.Count; i++)
                {
                    if (isAll == true)
                    {
                        listProduct[i].Active = isAll;
                    }
                    if (listProduct[i].Active == true)
                    {
                        ProductSearchProperty productSearchProperty = new ProductSearchProperty();
                        productSearchProperty.Initialization(InitType.Insert, requestUserID);
                        productSearchProperty.ProductID = listProduct[i].ID;
                        productSearchProperty.ProductSearchID = productSearch.ID;
                        productSearchProperty.ArticleTypeID = AppGlobal.ArticleTypeID;
                        productSearchProperty.Active = true;
                        listProductSearchProperty.Add(productSearchProperty);
                    }
                }
                if (isAll == true)
                {
                    _context.Set<Product>().AddRange(listProduct);
                }
                _context.SaveChanges();
            }
            return productSearch;
        }
    }
}
