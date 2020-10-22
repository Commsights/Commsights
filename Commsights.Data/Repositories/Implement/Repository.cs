using Microsoft.EntityFrameworkCore;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commsights.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {

        private readonly CommsightsContext _context;

        public Repository(CommsightsContext context)
        {
            _context = context;
        }

        public async Task<int> AsyncCreate(T model)
        {
            if (model.Active == null)
            {
                model.Active = false;
            }
            await _context.Set<T>().AddAsync(model);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> AsyncRange(List<T> list)
        {
            await _context.Set<T>().AddRangeAsync(list);
            return await _context.SaveChangesAsync();
        }
        public int Range(List<T> list)
        {
            _context.Set<T>().AddRange(list);
            return _context.SaveChanges();
        }
        public async Task<int> AsyncDelete(int ID)
        {
            var existModel = await AsyncGetByID(ID);
            if (existModel != null)
            {
                _context.Set<T>().Remove(existModel);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<List<T>> AsyncGetAllToList()
        {
            var result = await _context.Set<T>().ToListAsync();
            return result ?? new List<T>();
        }

        public async Task<T> AsyncGetByID(int ID)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(model => model.ID == ID);
        }

        public async Task<int> AsyncUpdate(int ID, T model)
        {
            if (model.Active == null)
            {
                model.Active = false;
            }
            var existModel = await AsyncGetByID(ID);
            if (existModel != null)
            {
                existModel = model;
                _context.Set<T>().Update(existModel);
            }
            return await _context.SaveChangesAsync();
        }

        public int Create(T model)
        {
            if (model.Active == null)
            {
                model.Active = false;
            }
            _context.Set<T>().Add(model);
            return _context.SaveChanges();
        }

        public int Delete(int ID)
        {
            var existModel = GetByID(ID);
            if (existModel != null)
            {
                _context.Set<T>().Remove(existModel);
            }
            return _context.SaveChanges();
        }
        public int DeleteRange(List<T> list)
        {
            _context.Set<T>().RemoveRange(list);
            return _context.SaveChanges();
        }
        public List<T> GetAllToList()
        {
            var result = _context.Set<T>().OrderByDescending(model => model.ID).ToList();
            return result ?? new List<T>();
        }
        public List<T> GetByParentIDToList(int parentID)
        {
            var result = _context.Set<T>().Where(model => model.ParentID == parentID).OrderByDescending(model => model.DateUpdated).ToList();
            return result;
        }
        public T GetByID(int ID)
        {
            var result = _context.Set<T>().AsNoTracking().FirstOrDefault(model => model.ID == ID);
            return result;
        }

        public int Update(int ID, T model)
        {
            if (model.Active == null)
            {
                model.Active = false;
            }
            var existModel = GetByID(ID);
            if (existModel != null)
            {
                existModel = model;
                _context.Set<T>().Update(existModel);
            }
            return _context.SaveChanges();
        }
        public int UpdateRange(List<T> list)
        {
            _context.Set<T>().UpdateRange(list);
            return _context.SaveChanges();
        }

        public List<T> GetByPageAndPageSizeToList(int Page, int PageSize)
        {
            var result = _context.Set<T>().Skip(Page * PageSize).Take(PageSize).ToList();
            return result;
        }

        public int Create(T model, out T result)
        {
            if (model.Active == null)
            {
                model.Active = false;
            }
            _context.Set<T>().Add(model);
            var temp = _context.SaveChanges();
            result = model;
            return temp;
        }
    }
}
