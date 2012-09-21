using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Todo_DataAccess.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> All { get; }
        T Find(Int64 id);
        void Insert(T item);
        void Delete(Int64 id);
        void Save();
    }
}