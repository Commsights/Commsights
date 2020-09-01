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
        public List<Product> GetByCategoryIDAndDatePublishToList(int categoryID, DateTime datePublish)
        {
            return _context.Product.Where(item => item.CategoryId == categoryID && item.DatePublish.Year == datePublish.Year && item.DatePublish.Month == datePublish.Month && item.DatePublish.Day == datePublish.Day).OrderByDescending(item => item.DateUpdated).ToList();
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
                item = _context.Set<Product>().FirstOrDefault(item => item.Urlcode.Equals(url));
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
            if(productSearchID>0)
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
    }
}
