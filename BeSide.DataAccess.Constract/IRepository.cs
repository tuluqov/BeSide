using System;
using System.Collections.Generic;

namespace BeSide.DataAccess.Construct
{
    public interface IRepository<T>
        where T : class
    {
        T Create(T item);
        T Update(T item);
        T Delete(int id);
        T GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Func<T, Boolean> predicate);
    }
}
