using Commsights.Data.Models;
using System;
using System.Collections.Generic;
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
        public List<Product> GetByCategoryIDAndDatePublishToList(int categoryID, DateTime datePublish)
        {
            return _context.Product.Where(item => item.CategoryId == categoryID && item.DatePublish.Year == datePublish.Year && item.DatePublish.Month == datePublish.Month && item.DatePublish.Day == datePublish.Day).OrderByDescending(item => item.DateUpdated).ToList();
        }
        public List<Product> GetByParentIDAndDatePublishToList(int parentID, DateTime datePublish)
        {
            return _context.Product.Where(item => item.ParentID == parentID && item.DatePublish.Year == datePublish.Year && item.DatePublish.Month == datePublish.Month && item.DatePublish.Day == datePublish.Day).OrderByDescending(item => item.DateUpdated).ToList();
        }
        public List<Product> GetBySearchToList(string search)
        {
            List<Product> list = new List<Product>();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim();
                list = _context.Product.Where(item => item.Title.Contains(search) || item.Description.Contains(search)).OrderByDescending(item => item.DatePublish).ToList();
            }
            return list;
        }
        public bool IsValid(string url)
        {
            Product item = _context.Set<Product>().FirstOrDefault(item => item.Urlcode.Equals(url));
            return item == null ? true : false;
        }
    }
}
