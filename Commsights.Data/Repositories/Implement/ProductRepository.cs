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
        public List<Product> GetByAndiToList()
        {
            return _context.Product.Where(item => item.Source == AppGlobal.SourceAndi && item.URLCode.Contains("andi.vn")).OrderByDescending(item => item.IsVideo).ToList();
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
        public List<Product> GetByTitleToList(string title)
        {
            return _context.Product.Where(item => item.Title.Equals(title)).OrderByDescending(item => item.DatePublish).ToList();
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
                list = _context.Product.Where(item => (item.Title.Contains(search) || item.TitleEnglish.Contains(search) || item.Description.Contains(search)) && (datePublishBegin <= item.DatePublish && item.DatePublish <= datePublishEnd) && ((item.Source == AppGlobal.SourceAuto) || (item.Source == AppGlobal.SourceGoogle))).OrderByDescending(item => item.DatePublish).ThenBy(item => item.Title).ToList();
            }
            return list;
        }

        public List<Product> GetByDatePublishBeginAndDatePublishEndAndSearchAndSourceToList(DateTime datePublishBegin, DateTime datePublishEnd, string search, string source)
        {
            List<Product> list = new List<Product>();
            if (!string.IsNullOrEmpty(search))
            {
                datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
                datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
                SqlParameter[] parameters =
                {
                    new SqlParameter("@DatePublishBegin",datePublishBegin),
                    new SqlParameter("@DatePublishEnd",datePublishEnd),
                    new SqlParameter("@Search",search),
                    new SqlParameter("@Source",source),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductSelectByDatePublishBeginAndDatePublishEndAndSearchAndSource", parameters);
                list = SQLHelper.ToList<Product>(dt);
            }
            return list;
        }
        public async Task<List<Product>> AsyncGetByDatePublishBeginAndDatePublishEndAndSourceToList(DateTime datePublishBegin, DateTime datePublishEnd, string source)
        {
            List<Product> list = new List<Product>();
            datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
            datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
            SqlParameter[] parameters =
            {
                    new SqlParameter("@DatePublishBegin",datePublishBegin),
                    new SqlParameter("@DatePublishEnd",datePublishEnd),
                    new SqlParameter("@Source",source),
                };
            DataTable dt = await SQLHelper.FillAsync(AppGlobal.ConectionString, "sp_ProductSelectByDatePublishBeginAndDatePublishEndAndSource", parameters);
            foreach (DataRow row in dt.Rows)
            {
                Product item = new Product();
                item.ID = row["ID"] == DBNull.Value ? 0 : (int)row["ID"];
                item.DatePublish = row["DatePublish"] == DBNull.Value ? DateTime.Now : (DateTime)row["DatePublish"];
                item.Title = row["Title"] == DBNull.Value ? "" : (string)row["Title"];
                item.TitleEnglish = row["TitleEnglish"] == DBNull.Value ? "" : (string)row["TitleEnglish"];
                item.Description = row["Description"] == DBNull.Value ? "" : (string)row["Description"];
                item.DescriptionEnglish = row["DescriptionEnglish"] == DBNull.Value ? "" : (string)row["DescriptionEnglish"];
                item.URLCode = row["URLCode"] == DBNull.Value ? "" : (string)row["URLCode"];
                list.Add(item);
            }
            return list;
        }
        public async Task<List<Product>> AsyncGetByDatePublishBeginAndDatePublishEndAndSearchAndSourceToList(DateTime datePublishBegin, DateTime datePublishEnd, string search, string source)
        {
            List<Product> list = new List<Product>();
            if (!string.IsNullOrEmpty(search))
            {
                datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
                datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
                SqlParameter[] parameters =
                {
                    new SqlParameter("@DatePublishBegin",datePublishBegin),
                    new SqlParameter("@DatePublishEnd",datePublishEnd),
                    new SqlParameter("@Search",search),
                    new SqlParameter("@Source",source),
                };
                DataTable dt = await SQLHelper.FillAsync(AppGlobal.ConectionString, "sp_ProductSelectByDatePublishBeginAndDatePublishEndAndSearchAndSource", parameters);
                foreach (DataRow row in dt.Rows)
                {
                    Product item = new Product();
                    item.ID = row["ID"] == DBNull.Value ? 0 : (int)row["ID"];
                    item.DatePublish = row["DatePublish"] == DBNull.Value ? DateTime.Now : (DateTime)row["DatePublish"];
                    item.Title = row["Title"] == DBNull.Value ? "" : (string)row["Title"];
                    item.TitleEnglish = row["TitleEnglish"] == DBNull.Value ? "" : (string)row["TitleEnglish"];
                    item.Description = row["Description"] == DBNull.Value ? "" : (string)row["Description"];
                    item.DescriptionEnglish = row["DescriptionEnglish"] == DBNull.Value ? "" : (string)row["DescriptionEnglish"];
                    item.URLCode = row["URLCode"] == DBNull.Value ? "" : (string)row["URLCode"];
                    list.Add(item);
                }
            }
            return list;
        }
        public List<ProductCompact001> GetAllProductCompact001ToList()
        {
            List<ProductCompact001> list = new List<ProductCompact001>();
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductSelectAll");
            list = SQLHelper.ToList<ProductCompact001>(dt);
            return list;
        }
        public List<ProductCompact001> GetProductCompactBySourceToList(string source)
        {
            List<ProductCompact001> list = new List<ProductCompact001>();
            SqlParameter[] parameters =
                {
                    new SqlParameter("@Source",source),
                };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductSelectBySource", parameters);
            list = SQLHelper.ToList<ProductCompact001>(dt);
            return list;
        }
        public async Task<List<ProductCompact>> AsyncGetProductCompactByDatePublishBeginAndDatePublishEndAndSearchAndSourceToList(DateTime datePublishBegin, DateTime datePublishEnd, string search, string source)
        {
            List<ProductCompact> list = new List<ProductCompact>();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.Replace(@"""", @"");
                datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
                datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
                SqlParameter[] parameters =
                {
                    new SqlParameter("@DatePublishBegin",datePublishBegin),
                    new SqlParameter("@DatePublishEnd",datePublishEnd),
                    new SqlParameter("@Search",search),
                    new SqlParameter("@Source",source),
                };
                DataTable dt = await SQLHelper.FillAsync(AppGlobal.ConectionString, "sp_ProductSelectByDatePublishBeginAndDatePublishEndAndSearchAndSource", parameters);
                list = SQLHelper.ToList<ProductCompact>(dt);
            }
            return list;
        }
        public async Task<List<ProductCompact>> AsyncGetProductCompactByDateUpdatedBeginAndDateUpdatedEndAndSearchAndSourceToList(DateTime datePublishBegin, DateTime datePublishEnd, string search, string source)
        {
            List<ProductCompact> list = new List<ProductCompact>();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.Replace(@"""", @"");
                datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
                datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
                SqlParameter[] parameters =
                {
                    new SqlParameter("@DatePublishBegin",datePublishBegin),
                    new SqlParameter("@DatePublishEnd",datePublishEnd),
                    new SqlParameter("@Search",search),
                    new SqlParameter("@Source",source),
                };
                DataTable dt = await SQLHelper.FillAsync(AppGlobal.ConectionString, "sp_ProductSelectByDateUpdatedBeginAndDateUpdatedEndAndSearchAndSource", parameters);
                list = SQLHelper.ToList<ProductCompact>(dt);
            }
            return list;
        }
        public async Task<List<ProductCompact>> AsyncGetProductCompactByDatePublishBeginAndDatePublishEndAndSourceToList(DateTime datePublishBegin, DateTime datePublishEnd, string source)
        {
            List<ProductCompact> list = new List<ProductCompact>();
            datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
            datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
            SqlParameter[] parameters =
            {
                    new SqlParameter("@DatePublishBegin",datePublishBegin),
                    new SqlParameter("@DatePublishEnd",datePublishEnd),
                    new SqlParameter("@Source",source),
                };
            DataTable dt = await SQLHelper.FillAsync(AppGlobal.ConectionString, "sp_ProductSelectByDatePublishBeginAndDatePublishEndAndSource", parameters);
            list = SQLHelper.ToList<ProductCompact>(dt);

            return list;
        }
        public async Task<List<ProductCompact>> AsyncGetProductCompactByDateUpdatedBeginAndDateUpdatedEndAndSourceToList(DateTime datePublishBegin, DateTime datePublishEnd, string source)
        {
            List<ProductCompact> list = new List<ProductCompact>();
            datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
            datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
            SqlParameter[] parameters =
            {
                    new SqlParameter("@DatePublishBegin",datePublishBegin),
                    new SqlParameter("@DatePublishEnd",datePublishEnd),
                    new SqlParameter("@Source",source),
                };
            DataTable dt = await SQLHelper.FillAsync(AppGlobal.ConectionString, "sp_ProductSelectByDateUpdatedBeginAndDateUpdatedEndAndSource", parameters);
            list = SQLHelper.ToList<ProductCompact>(dt);
            return list;
        }
        public async Task<List<ProductCompact>> AsyncGetProductCompactByDatePublishBeginAndDatePublishEndAndSearchAndIsTitleAndIsDescriptionAndSourceAndIsPublishToList(DateTime datePublishBegin, DateTime datePublishEnd, string search, string source, bool isTitle, bool isDescription, bool isPublish)
        {
            List<ProductCompact> list = new List<ProductCompact>();
            if (isPublish == true)
            {
                list = await AsyncGetProductCompactByDatePublishBeginAndDatePublishEndAndSearchAndIsTitleAndIsDescriptionAndSourceToList(datePublishBegin, datePublishEnd, search, source, isTitle, isDescription);
            }
            else
            {
                list = await AsyncGetProductCompactByDateUpdatedBeginAndDateUpdatedEndAndSearchAndIsTitleAndIsDescriptionAndSourceToList(datePublishBegin, datePublishEnd, search, source, isTitle, isDescription);
            }
            return list;
        }
        public async Task<List<ProductCompact>> AsyncGetProductCompactByDatePublishBeginAndDatePublishEndAndSearchAndIsTitleAndIsDescriptionAndSourceAndIsUploadToList(DateTime datePublishBegin, DateTime datePublishEnd, string search, string source, bool isTitle, bool isDescription, bool isUpload)
        {
            List<ProductCompact> list = new List<ProductCompact>();
            if (isUpload == false)
            {
                list = await AsyncGetProductCompactByDatePublishBeginAndDatePublishEndAndSearchAndIsTitleAndIsDescriptionAndSourceToList(datePublishBegin, datePublishEnd, search, source, isTitle, isDescription);
            }
            else
            {
                list = await AsyncGetProductCompactByDateUpdatedBeginAndDateUpdatedEndAndSearchAndIsTitleAndIsDescriptionAndSourceToList(datePublishBegin, datePublishEnd, search, source, isTitle, isDescription);
            }
            return list;
        }
        public async Task<List<ProductCompact>> AsyncGetProductCompactByDatePublishBeginAndDatePublishEndAndSearchAndIsTitleAndIsDescriptionAndSourceToList(DateTime datePublishBegin, DateTime datePublishEnd, string search, string source, bool isTitle, bool isDescription)
        {
            List<ProductCompact> list = new List<ProductCompact>();
            if (!string.IsNullOrEmpty(search))
            {
                if (search.Equals("*") == true)
                {
                    list = await AsyncGetProductCompactByDatePublishBeginAndDatePublishEndAndSourceToList(datePublishBegin, datePublishEnd, source);
                }
                if ((isTitle == true) && (isDescription == true))
                {
                    list = await AsyncGetProductCompactByDatePublishBeginAndDatePublishEndAndSearchAndSourceToList(datePublishBegin, datePublishEnd, search, source);
                }
                else
                {
                    if (isTitle == true)
                    {
                        list = await AsyncGetProductCompactByDatePublishBeginAndDatePublishEndAndSearchTitleAndSourceToList(datePublishBegin, datePublishEnd, search, source);
                    }
                    if (isDescription == true)
                    {
                        list = await AsyncGetProductCompactByDatePublishBeginAndDatePublishEndAndSearchDescriptionAndSourceToList(datePublishBegin, datePublishEnd, search, source);
                    }
                }
            }
            return list;
        }
        public async Task<List<ProductCompact>> AsyncGetProductCompactByDateUpdatedBeginAndDateUpdatedEndAndSearchAndIsTitleAndIsDescriptionAndSourceToList(DateTime datePublishBegin, DateTime datePublishEnd, string search, string source, bool isTitle, bool isDescription)
        {
            List<ProductCompact> list = new List<ProductCompact>();
            if (!string.IsNullOrEmpty(search))
            {
                if (search.Equals("*") == true)
                {
                    list = await AsyncGetProductCompactByDateUpdatedBeginAndDateUpdatedEndAndSourceToList(datePublishBegin, datePublishEnd, source);
                }
                if ((isTitle == true) && (isDescription == true))
                {
                    list = await AsyncGetProductCompactByDateUpdatedBeginAndDateUpdatedEndAndSearchAndSourceToList(datePublishBegin, datePublishEnd, search, source);
                }
                else
                {
                    if (isTitle == true)
                    {
                        list = await AsyncGetProductCompactByDateUpdatedBeginAndDateUpdatedEndAndSearchTitleAndSourceToList(datePublishBegin, datePublishEnd, search, source);
                    }
                    if (isDescription == true)
                    {
                        list = await AsyncGetProductCompactByDateUpdatedBeginAndDateUpdatedEndAndSearchDescriptionAndSourceToList(datePublishBegin, datePublishEnd, search, source);
                    }
                }
            }
            return list;
        }
        public async Task<List<ProductCompact>> AsyncGetProductCompactByDatePublishBeginAndDatePublishEndAndSearchTitleAndSourceToList(DateTime datePublishBegin, DateTime datePublishEnd, string search, string source)
        {
            List<ProductCompact> list = new List<ProductCompact>();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.Replace(@"""", @"");
                datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
                datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
                SqlParameter[] parameters =
                {
                    new SqlParameter("@DatePublishBegin",datePublishBegin),
                    new SqlParameter("@DatePublishEnd",datePublishEnd),
                    new SqlParameter("@Search",search),
                    new SqlParameter("@Source",source),
                };
                DataTable dt = await SQLHelper.FillAsync(AppGlobal.ConectionString, "sp_ProductSelectByDatePublishBeginAndDatePublishEndAndSearchTitleAndSource", parameters);
                list = SQLHelper.ToList<ProductCompact>(dt);
            }
            return list;
        }
        public async Task<List<ProductCompact>> AsyncGetProductCompactByDateUpdatedBeginAndDateUpdatedEndAndSearchTitleAndSourceToList(DateTime datePublishBegin, DateTime datePublishEnd, string search, string source)
        {
            List<ProductCompact> list = new List<ProductCompact>();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.Replace(@"""", @"");
                datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
                datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
                SqlParameter[] parameters =
                {
                    new SqlParameter("@DatePublishBegin",datePublishBegin),
                    new SqlParameter("@DatePublishEnd",datePublishEnd),
                    new SqlParameter("@Search",search),
                    new SqlParameter("@Source",source),
                };
                DataTable dt = await SQLHelper.FillAsync(AppGlobal.ConectionString, "sp_ProductSelectByDateUpdatedBeginAndDateUpdatedEndAndSearchTitleAndSource", parameters);
                list = SQLHelper.ToList<ProductCompact>(dt);
            }
            return list;
        }
        public async Task<List<ProductCompact>> AsyncGetProductCompactByDatePublishBeginAndDatePublishEndAndSearchDescriptionAndSourceToList(DateTime datePublishBegin, DateTime datePublishEnd, string search, string source)
        {
            List<ProductCompact> list = new List<ProductCompact>();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.Replace(@"""", @"");
                datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
                datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
                SqlParameter[] parameters =
                {
                    new SqlParameter("@DatePublishBegin",datePublishBegin),
                    new SqlParameter("@DatePublishEnd",datePublishEnd),
                    new SqlParameter("@Search",search),
                    new SqlParameter("@Source",source),
                };
                DataTable dt = await SQLHelper.FillAsync(AppGlobal.ConectionString, "sp_ProductSelectByDatePublishBeginAndDatePublishEndAndSearchDescriptionAndSource", parameters);
                list = SQLHelper.ToList<ProductCompact>(dt);
            }
            return list;
        }
        public async Task<List<ProductCompact>> AsyncGetProductCompactByDateUpdatedBeginAndDateUpdatedEndAndSearchDescriptionAndSourceToList(DateTime datePublishBegin, DateTime datePublishEnd, string search, string source)
        {
            List<ProductCompact> list = new List<ProductCompact>();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.Replace(@"""", @"");
                datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
                datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
                SqlParameter[] parameters =
                {
                    new SqlParameter("@DatePublishBegin",datePublishBegin),
                    new SqlParameter("@DatePublishEnd",datePublishEnd),
                    new SqlParameter("@Search",search),
                    new SqlParameter("@Source",source),
                };
                DataTable dt = await SQLHelper.FillAsync(AppGlobal.ConectionString, "sp_ProductSelectByDateUpdatedBeginAndDateUpdatedEndAndSearchDescriptionAndSource", parameters);
                list = SQLHelper.ToList<ProductCompact>(dt);
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
                item = null;
            }
            return item == null ? true : false;
        }
        public bool IsValidBySQL(string url)
        {
            Product item = null;
            if (!string.IsNullOrEmpty(url))
            {
                SqlParameter[] parameters =
                  {
                    new SqlParameter("@URLCode",url),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductSelectByURLCode", parameters);
                if ((dt.Rows.Count == 0) || (dt == null))
                {
                    item = null;
                }
                else
                {
                    item = new Product();
                }
            }
            else
            {
                item = null;
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
            Product model = new Product();
            SqlParameter[] parameters =
            {
                    new SqlParameter("@URLCode",uRLCode),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductSelectByURLCode", parameters);
            model = SQLHelper.ToList<Product>(dt).FirstOrDefault();
            if (model == null)
            {
                if (uRLCode.Contains(@"http://") == true)
                {
                    uRLCode = uRLCode.Replace(@"http://", @"https://");
                }
                else
                {
                    if (uRLCode.Contains(@"https://") == true)
                    {
                        uRLCode = uRLCode.Replace(@"https://", @"http://");
                    }
                }    
                SqlParameter[] parameters01 =
                {
                        new SqlParameter("@URLCode",uRLCode),
                };
                DataTable dt01 = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductSelectByURLCode", parameters01);
                model = SQLHelper.ToList<Product>(dt01).FirstOrDefault();
            }
            return model;
        }
        public Product GetByByDatePublishBeginAndDatePublishEndAndIndustryIDAndSourceID(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int sourceID)
        {
            datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
            datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
            SqlParameter[] parameters =
            {
                    new SqlParameter("@DatePublishBegin",datePublishBegin),
                    new SqlParameter("@DatePublishEnd",datePublishEnd),
                    new SqlParameter("@IndustryID",industryID),
                    new SqlParameter("@SourceID",sourceID),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductSelectByDatePublishBeginAndDatePublishEndAndIndustryIDAndSourceID", parameters);
            return SQLHelper.ToList<ProductDataTransfer>(dt).FirstOrDefault();
        }
        public Product GetByURLCode001(string uRLCode)
        {
            return _context.Set<Product>().FirstOrDefault(item => item.URLCode.Equals(uRLCode));
        }
        public Product GetByFileName(string fileName)
        {
            return _context.Set<Product>().FirstOrDefault(item => item.FileName.Equals(fileName));
        }
        public Product GetByFileNameAndDatePublish(string fileName, DateTime datePublish)
        {
            return _context.Set<Product>().FirstOrDefault(item => item.FileName.Equals(fileName) && item.DatePublish.Equals(datePublish));
        }
        public Product GetByImageThumbnail(string imageThumbnail)
        {
            return _context.Set<Product>().FirstOrDefault(item => item.ImageThumbnail.Equals(imageThumbnail));
        }
        public Product GetByTitle(string title)
        {
            return _context.Set<Product>().FirstOrDefault(item => item.Title.Equals(title));
        }
        public Product GetByTitleAndSource(string title, string source)
        {
            return _context.Set<Product>().FirstOrDefault(item => item.Title.Equals(title) && item.Source.Equals(source) && (item.PriceUnitID == 0 || item.PriceUnitID == null));
        }
        public Product GetByProductID(int productID)
        {
            return _context.Set<Product>().FirstOrDefault(item => item.ProductID == productID);
        }
        public Product GetByPriceUnitID(int priceUnitID)
        {
            return _context.Set<Product>().FirstOrDefault(item => item.PriceUnitID == priceUnitID);
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
        public List<ProductDataTransfer> GetByIDListToList(string iDList)
        {
            List<ProductDataTransfer> list = new List<ProductDataTransfer>();
            if (!string.IsNullOrEmpty(iDList))
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@IDList",iDList),
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductSelectByIDList", parameters);
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
        public List<ProductDataTransfer> GetDataTransferByDatePublishBeginAndDatePublishEndAndIndustryIDToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID)
        {
            List<ProductDataTransfer> list = new List<ProductDataTransfer>();
            if (industryID > 0)
            {
                SqlParameter[] parameters =
                       {
                    new SqlParameter("@DatePublishBegin",datePublishBegin),
                    new SqlParameter("@DatePublishEnd",datePublishEnd),
                    new SqlParameter("@IndustryID",industryID)
                    };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductSelectDataTransferByDatePublishBeginAndDatePublishEndAndIndustryID", parameters);
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
        public List<ProductCompact001> GetProductCompact001BySourceWithIDAndTitleToList(string source)
        {
            List<ProductCompact001> list = new List<ProductCompact001>();
            SqlParameter[] parameters =
                   {
                new SqlParameter("@Source",source),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductSelectBySourceWithIDAndTitle", parameters);
            list = SQLHelper.ToList<ProductCompact001>(dt);
            return list;
        }
        public List<ProductCompact001> GetProductCompact001BySourceAndRowBeginAndRowEndWithIDAndDescriptionToList(string source, int rowBegin, int rowEnd)
        {
            List<ProductCompact001> list = new List<ProductCompact001>();
            SqlParameter[] parameters =
                   {
                new SqlParameter("@Source",source),
                new SqlParameter("@RowBegin",rowBegin),
                new SqlParameter("@RowEnd",rowEnd),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductSelectBySourceAndRowBeginAndRowEndWithIDAndDescription", parameters);
            list = SQLHelper.ToList<ProductCompact001>(dt);
            return list;
        }
        public Product GetByID001(int ID)
        {
            Product model = new Product();
            SqlParameter[] parameters =
                   {
                new SqlParameter("@ID",ID),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductSelectByID", parameters);
            model = SQLHelper.ToList<Product>(dt).FirstOrDefault();
            return model;
        }
        public void FilterProduct(Product product, List<ProductProperty> listProductProperty, int RequestUserID)
        {
            if (string.IsNullOrEmpty(product.Title))
            {
                product.Title = "";
            }
            if (string.IsNullOrEmpty(product.Description))
            {
                product.Description = "";
            }
            if (string.IsNullOrEmpty(product.ContentMain))
            {
                product.ContentMain = "";
            }
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
                        productProperty.Code = AppGlobal.Product;
                        productProperty.Initialization(InitType.Insert, RequestUserID);
                        productProperty.ParentID = 0;
                        productProperty.ArticleTypeID = AppGlobal.TinSanPhamID;
                        productProperty.AssessID = product.AssessID;
                        productProperty.GUICode = product.GUICode;
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
                        productProperty.Code = AppGlobal.Segment;
                        productProperty.Initialization(InitType.Insert, RequestUserID);
                        productProperty.ParentID = 0;
                        productProperty.ArticleTypeID = AppGlobal.TinNganhID;
                        productProperty.AssessID = product.AssessID;
                        productProperty.GUICode = product.GUICode;
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
                        productProperty.Code = AppGlobal.Industry;
                        productProperty.Initialization(InitType.Insert, RequestUserID);
                        productProperty.ParentID = 0;
                        productProperty.ArticleTypeID = AppGlobal.TinNganhID;
                        productProperty.AssessID = product.AssessID;
                        productProperty.GUICode = product.GUICode;
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
                List<MembershipPermission> listCompanyName = _membershipPermissionRepository.GetByMembershipIDAndCodeToList(listCompany[i].ID, AppGlobal.CompanyName);
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
                    else
                    {
                        foreach (MembershipPermission membershipPermission in listCompanyName)
                        {

                            if (product.Title.Contains(membershipPermission.FullName))
                            {
                                check = check + AppGlobal.CheckContentAndKeyword(product.Title, keyword);
                                title = true;
                                companyID = listCompany[i].ID;
                                listCompanyID.Add(companyID);
                            }
                        }
                    }
                    if (product.Description.Contains(keyword))
                    {
                        check = check + AppGlobal.CheckContentAndKeyword(product.Description, keyword);
                    }
                    else
                    {
                        foreach (MembershipPermission membershipPermission in listCompanyName)
                        {
                            if (product.Description.Contains(membershipPermission.FullName))
                            {
                                check = check + AppGlobal.CheckContentAndKeyword(product.Description, keyword);
                            }
                        }
                    }
                    if (product.ContentMain.Contains(keyword))
                    {
                        check = check + AppGlobal.CheckContentAndKeyword(product.ContentMain, keyword);
                    }
                    else
                    {
                        foreach (MembershipPermission membershipPermission in listCompanyName)
                        {
                            if (product.ContentMain.Contains(membershipPermission.FullName))
                            {
                                check = check + AppGlobal.CheckContentAndKeyword(product.Description, keyword);
                            }
                        }
                    }
                    if (check > 0)
                    {
                        ProductProperty productProperty = new ProductProperty();
                        productProperty.Code = AppGlobal.Company;
                        productProperty.Initialization(InitType.Insert, RequestUserID);
                        productProperty.ParentID = 0;
                        productProperty.ArticleTypeID = AppGlobal.TinDoanhNghiepID;
                        productProperty.AssessID = product.AssessID;
                        productProperty.GUICode = product.GUICode;
                        if (order == 0)
                        {
                            product.CompanyID = listCompany[i].ID;
                        }
                        productProperty.CompanyID = listCompany[i].ID;
                        MembershipPermission membershipPermissionIndustry = _membershipPermissionRepository.GetByMembershipIDAndAndCodeAndActive(productProperty.CompanyID.Value, AppGlobal.Industry, true);
                        if (membershipPermissionIndustry != null)
                        {
                            productProperty.IndustryID = membershipPermissionIndustry.IndustryID;
                        }
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
        public string InsertSingleItem(Product product)
        {
            SqlParameter[] parameters =
            {
new SqlParameter("@ID",product.ID),
new SqlParameter("@UserCreated",product.UserCreated),
new SqlParameter("@DateCreated",product.DateCreated),
new SqlParameter("@UserUpdated",product.UserUpdated),
new SqlParameter("@DateUpdated",product.DateUpdated),
new SqlParameter("@ParentID",product.ParentID),
new SqlParameter("@Note",product.Note),
new SqlParameter("@Active",product.Active),
new SqlParameter("@CategoryID",product.CategoryID),
new SqlParameter("@Title",product.Title),
new SqlParameter("@URLCode",product.URLCode),
new SqlParameter("@MetaTitle",product.MetaTitle),
new SqlParameter("@MetaKeyword",product.MetaKeyword),
new SqlParameter("@MetaDescription",product.MetaDescription),
new SqlParameter("@Tags",product.Tags),
new SqlParameter("@Author",product.Author),
new SqlParameter("@Image",product.Image),
new SqlParameter("@ImageThumbnail",product.ImageThumbnail),
new SqlParameter("@Description",product.Description),
new SqlParameter("@ContentMain",product.ContentMain),
new SqlParameter("@Price",product.Price),
new SqlParameter("@PriceUnitID",product.PriceUnitID),
new SqlParameter("@DatePublish",product.DatePublish),
new SqlParameter("@Page",product.Page),
new SqlParameter("@TitleEnglish",product.TitleEnglish),
new SqlParameter("@FileName",product.FileName),
new SqlParameter("@Liked",product.Liked),
new SqlParameter("@Comment",product.Comment),
new SqlParameter("@Share",product.Share),
new SqlParameter("@Reach",product.Reach),
new SqlParameter("@Duration",product.Duration),
new SqlParameter("@IsVideo",product.IsVideo),
new SqlParameter("@ArticleTypeID",product.ArticleTypeID),
new SqlParameter("@CompanyID",product.CompanyID),
new SqlParameter("@AssessID",product.AssessID),
new SqlParameter("@IndustryID",product.IndustryID),
new SqlParameter("@SegmentID",product.SegmentID),
new SqlParameter("@ProductID",product.ProductID),
new SqlParameter("@GUICode",product.GUICode),
new SqlParameter("@Source",product.Source),
new SqlParameter("@DescriptionEnglish",product.DescriptionEnglish),
new SqlParameter("@IsSummary",product.IsSummary),
new SqlParameter("@IsData",product.IsData),
new SqlParameter("@SourceID",product.SourceID),
new SqlParameter("@TargetID",product.TargetID),
};
            string result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ProductInsertSingleItem", parameters);
            return result;
        }
        public async Task<string> AsyncInsertSingleItem(Product product)
        {
            string result = "";
            if (!string.IsNullOrEmpty(product.Title))
            {
                SqlParameter[] parameters =
            {
new SqlParameter("@ID",product.ID),
new SqlParameter("@UserCreated",product.UserCreated),
new SqlParameter("@DateCreated",product.DateCreated),
new SqlParameter("@UserUpdated",product.UserUpdated),
new SqlParameter("@DateUpdated",product.DateUpdated),
new SqlParameter("@ParentID",product.ParentID),
new SqlParameter("@Note",product.Note),
new SqlParameter("@Active",product.Active),
new SqlParameter("@CategoryID",product.CategoryID),
new SqlParameter("@Title",product.Title),
new SqlParameter("@URLCode",product.URLCode),
new SqlParameter("@MetaTitle",product.MetaTitle),
new SqlParameter("@MetaKeyword",product.MetaKeyword),
new SqlParameter("@MetaDescription",product.MetaDescription),
new SqlParameter("@Tags",product.Tags),
new SqlParameter("@Author",product.Author),
new SqlParameter("@Image",product.Image),
new SqlParameter("@ImageThumbnail",product.ImageThumbnail),
new SqlParameter("@Description",product.Description),
new SqlParameter("@ContentMain",product.ContentMain),
new SqlParameter("@Price",product.Price),
new SqlParameter("@PriceUnitID",product.PriceUnitID),
new SqlParameter("@DatePublish",product.DatePublish),
new SqlParameter("@Page",product.Page),
new SqlParameter("@TitleEnglish",product.TitleEnglish),
new SqlParameter("@FileName",product.FileName),
new SqlParameter("@Liked",product.Liked),
new SqlParameter("@Comment",product.Comment),
new SqlParameter("@Share",product.Share),
new SqlParameter("@Reach",product.Reach),
new SqlParameter("@Duration",product.Duration),
new SqlParameter("@IsVideo",product.IsVideo),
new SqlParameter("@ArticleTypeID",product.ArticleTypeID),
new SqlParameter("@CompanyID",product.CompanyID),
new SqlParameter("@AssessID",product.AssessID),
new SqlParameter("@IndustryID",product.IndustryID),
new SqlParameter("@SegmentID",product.SegmentID),
new SqlParameter("@ProductID",product.ProductID),
new SqlParameter("@GUICode",product.GUICode),
new SqlParameter("@Source",product.Source),
new SqlParameter("@DescriptionEnglish",product.DescriptionEnglish),
new SqlParameter("@IsSummary",product.IsSummary),
new SqlParameter("@IsData",product.IsData),
new SqlParameter("@SourceID",product.SourceID),
new SqlParameter("@TargetID",product.TargetID),
};
                result = await SQLHelper.ExecuteNonQueryAsync(AppGlobal.ConectionString, "sp_ProductInsertSingleItem", parameters);
            }
            return result;
        }
        public async Task<string> AsyncInsertSingleItemAuto(Product product)
        {
            string result = "";
            if (!string.IsNullOrEmpty(product.Title))
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@UserCreated",product.UserCreated),
                    new SqlParameter("@DateCreated",product.DateCreated),
                    new SqlParameter("@UserUpdated",product.UserUpdated),
                    new SqlParameter("@DateUpdated",product.DateUpdated),
                    new SqlParameter("@ParentID",product.ParentID),
                    new SqlParameter("@Active",product.Active),
                    new SqlParameter("@CategoryID",product.CategoryID),
                    new SqlParameter("@Title",product.Title),
                    new SqlParameter("@URLCode",product.URLCode),
                    new SqlParameter("@MetaTitle",product.MetaTitle),
                    new SqlParameter("@Description",product.Description),
                    new SqlParameter("@ContentMain",product.ContentMain),
                    new SqlParameter("@DatePublish",product.DatePublish),
                    new SqlParameter("@GUICode",product.GUICode),
                    new SqlParameter("@Source",product.Source),
                };
                result = await SQLHelper.ExecuteNonQueryAsync(AppGlobal.ConectionString, "sp_ProductInsertSingleItemAuto", parameters);
            }
            return result;
        }
        public async Task<string> AsyncInsertSingleItemAutoNoFilter(Product product)
        {
            string result = "";
            if (!string.IsNullOrEmpty(product.Title))
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@UserCreated",product.UserCreated),
                    new SqlParameter("@DateCreated",product.DateCreated),
                    new SqlParameter("@UserUpdated",product.UserUpdated),
                    new SqlParameter("@DateUpdated",product.DateUpdated),
                    new SqlParameter("@ParentID",product.ParentID),
                    new SqlParameter("@Active",product.Active),
                    new SqlParameter("@CategoryID",product.CategoryID),
                    new SqlParameter("@Title",product.Title),
                    new SqlParameter("@URLCode",product.URLCode),
                    new SqlParameter("@MetaTitle",product.MetaTitle),
                    new SqlParameter("@Description",product.Description),
                    new SqlParameter("@ContentMain",product.ContentMain),
                    new SqlParameter("@DatePublish",product.DatePublish),
                    new SqlParameter("@GUICode",product.GUICode),
                    new SqlParameter("@Source",product.Source),
                };
                result = await SQLHelper.ExecuteNonQueryAsync(AppGlobal.ConectionString, "sp_ProductInsertSingleItemAutoNoFilter", parameters);
            }
            return result;
        }
        public string InsertSingleItemAuto(Product product)
        {
            string result = "";
            if (!string.IsNullOrEmpty(product.Title))
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@UserCreated",product.UserCreated),
                    new SqlParameter("@DateCreated",product.DateCreated),
                    new SqlParameter("@UserUpdated",product.UserUpdated),
                    new SqlParameter("@DateUpdated",product.DateUpdated),
                    new SqlParameter("@ParentID",product.ParentID),
                    new SqlParameter("@Active",product.Active),
                    new SqlParameter("@CategoryID",product.CategoryID),
                    new SqlParameter("@Title",product.Title),
                    new SqlParameter("@URLCode",product.URLCode),
                    new SqlParameter("@MetaTitle",product.MetaTitle),
                    new SqlParameter("@Description",product.Description),
                    new SqlParameter("@ContentMain",product.ContentMain),
                    new SqlParameter("@DatePublish",product.DatePublish),
                    new SqlParameter("@GUICode",product.GUICode),
                    new SqlParameter("@Source",product.Source),
                };
                result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ProductInsertSingleItemAuto", parameters);
            }
            return result;
        }
        public async Task<string> AsyncUpdateSingleItem(Product product)
        {
            SqlParameter[] parameters =
            {
new SqlParameter("@ID",product.ID),
new SqlParameter("@Title",product.Title),
new SqlParameter("@DatePublish",product.DatePublish),
new SqlParameter("@TitleEnglish",product.TitleEnglish),
};
            string result = await SQLHelper.ExecuteNonQueryAsync(AppGlobal.ConectionString, "sp_ProductUpdateSingleItem", parameters);
            return result;
        }
        public async Task<string> AsyncUpdateProductCompact001SingleItem(ProductCompact001 product)
        {
            SqlParameter[] parameters =
            {
new SqlParameter("@ID",product.ID),
new SqlParameter("@Title",product.Title),
};
            string result = await SQLHelper.ExecuteNonQueryAsync(AppGlobal.ConectionString, "sp_ProductUpdateSingleItemWithIDAndTitle", parameters);
            return result;
        }
        public async Task<string> AsyncUpdateProductCompact001SingleItemWithIDAndDescription(ProductCompact001 product)
        {
            SqlParameter[] parameters =
            {
new SqlParameter("@ID",product.ID),
new SqlParameter("@Description",product.Description),
};
            string result = await SQLHelper.ExecuteNonQueryAsync(AppGlobal.ConectionString, "sp_ProductUpdateSingleItemWithIDAndDescription", parameters);
            return result;
        }
        public string UpdateProductCompactSingleItem(ProductCompact product)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@ID",product.ID),
                new SqlParameter("@Title",product.Title),
                new SqlParameter("@DatePublish",product.DatePublish),
                new SqlParameter("@TitleEnglish",product.TitleEnglish),
                new SqlParameter("@IsError",product.IsError),
            };
            string result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ProductUpdateSingleItem", parameters);
            return result;
        }
        public string Initialization()
        {
            string result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ProductInitialization");
            return result;
        }
        public string UpdateSingleItemByCodeData(CodeData model)
        {

            SqlParameter[] parameters =
            {
new SqlParameter("@ID",model.ProductID),
new SqlParameter("@Title",model.Title),
new SqlParameter("@TitleEnglish",model.TitleEnglish),
new SqlParameter("@Description",model.Description),
new SqlParameter("@DescriptionEnglish",model.DescriptionEnglish),
new SqlParameter("@Author",model.Author),
new SqlParameter("@UserUpdated",model.UserUpdated),
new SqlParameter("@TitleProperty",model.TitleProperty),
new SqlParameter("@SourceProperty",model.SourceProperty),
new SqlParameter("@DatePublish",model.DatePublish),
};
            string result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ProductUpdateSingleItemByCodeData", parameters);
            return result;
        }
    }
}
