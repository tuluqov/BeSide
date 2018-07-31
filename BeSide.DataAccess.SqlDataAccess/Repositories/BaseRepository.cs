using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using BeSide.DataAccess.Construct;
using System;

namespace BeSide.DataAccess.SqlDataAccess.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        protected readonly DbSet<T> Items;

        public BaseRepository(DbContext context)
        {
            Context = context;
            Items = context.Set<T>();
        }

        #region Implementation of Repository<T> 

        public void Create(T item)
        {
            Items.Add(item);
            Save();
        }

        public void Delete(object id)
        {
            T resault = Items.Find(id);

            if (resault != null)
            {
                Items.Remove(resault);
            }

            Save();
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            IEnumerable<T> result = Items.Where(predicate).ToList();
            return result;
        }

        public IEnumerable<T> GetAll()
        {
            return Items.ToList();
        }

        public T GetById(object id)
        {
            T resault = Items.Find(id);
            return resault;

        }

        public void Update(T item)
        {
            Context.Entry(item).State = EntityState.Modified;

            Save();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        #endregion
    }
}
