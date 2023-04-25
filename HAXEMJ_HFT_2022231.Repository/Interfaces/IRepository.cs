using System;
using System.Linq;

namespace HAXEMJ_HFT_2022231.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> ReadAll();
        T Read(int id);
        T Read(string id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Delete(string id);
    }
}

