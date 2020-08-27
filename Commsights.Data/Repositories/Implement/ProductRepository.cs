﻿using Commsights.Data.Models;
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
    }
}
