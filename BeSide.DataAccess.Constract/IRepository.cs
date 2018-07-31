using System;
using System.Collections.Generic;

namespace BeSide.DataAccess.Construct
{
    public interface IRepository<T>
        where T : class
    {
        void Create(T item);
        void Update(T item);
        void Delete(object id);
        T GetById(object id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        void Save();
    }
}
