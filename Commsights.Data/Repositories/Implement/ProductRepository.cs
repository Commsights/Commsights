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
            return _context.Product.Where(item => item.ParentId == parentID && item.DatePublish.Year == datePublish.Year && item.DatePublish.Month == datePublish.Month && item.DatePublish.Day == datePublish.Day).OrderByDescending(item => item.DateUpdated).ToList();
        }
        public bool IsValid(string url)
        {
            Product item = _context.Set<Product>().FirstOrDefault(item => item.Urlcode.Equals(url));
            return item == null ? true : false;
        }
    }
}
