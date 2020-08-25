using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Commsights.Data.Models;

namespace Commsights.Data.Repositories
{
    public interface IRepository<T> where T : BaseModel
    {
        public Task<int> AsyncCreate(T model);
        public Task<int> AsyncRange(List<T> list);
        public int Range(List<T> list);

        public Task<int> AsyncUpdate(int ID, T model);

        public Task<int> AsyncDelete(int ID);

        public Task<List<T>> AsyncGetAllToList();

        public Task<T> AsyncGetByID(int ID);

        public int Create(T model);

        public int Create(T model, out T result);

        public int Update(int ID, T model);
        public int UpdateRange(List<T> list);
        public int Delete(int ID);
        public int DeleteRange(List<T> list);

        public List<T> GetAllToList();

        public List<T> GetByParentIDToList(int parentID);

        public T GetByID(int ID);

        public List<T> GetByPageAndPageSizeToList(int Page, int PageSzie);
    }
}
