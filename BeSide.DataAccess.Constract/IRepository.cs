using System.Collections.Generic;

namespace BeSide.DataAccess.Construct
{
    public interface IRepository<T>
        where T : class
    {
        void Create(T item);
        void Update(T item);
        bool Delete(int id);
        T GetById(int id);
        IEnumerable<T> GetAll();
    }
}
