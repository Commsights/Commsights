using Commsights.Data.Enum;
using Commsights.Data.Helpers;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
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
        public ProductSearch SaveProductSearch(string search, DateTime datePublishBegin, DateTime datePublishEnd, int requestUserID)
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
                foreach (Product product in listProduct)
                {
                    ProductSearchProperty productSearchProperty = new ProductSearchProperty();
                    productSearchProperty.Initialization(InitType.Insert, requestUserID);
                    productSearchProperty.ProductID = product.ID;
                    productSearchProperty.ProductSearchID = productSearch.ID;
                    productSearchProperty.ArticleTypeID = AppGlobal.ArticleTypeID;
                    productSearchProperty.Active = true;
                    listProductSearchProperty.Add(productSearchProperty);
                }
                _context.Set<ProductSearchProperty>().AddRange(listProductSearchProperty);
                _context.SaveChanges();
            }
            return productSearch;
        }
    }
}
