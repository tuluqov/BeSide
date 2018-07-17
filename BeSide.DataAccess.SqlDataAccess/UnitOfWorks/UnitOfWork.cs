using System;
using BeSide.Common.Entities;
using BeSide.DataAccess.Construct;
using BeSide.DataAccess.SqlDataAccess.DataContexts;
using BeSide.DataAccess.SqlDataAccess.Repositories;

namespace BeSide.DataAccess.SqlDataAccess.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EfDataContext context;
        private IRepository<Category> categories;
        private IRepository<Order> orders;
        private IRepository<ProviderServices> providerServices;
        private IRepository<Service> services;
        private IRepository<User> users;

        private bool disposed;

        public UnitOfWork(string connectionString)
        {
            context = new EfDataContext(connectionString);
        }

        #region Implementation of IUnitOfWork

        public IRepository<Category> Categories
        {
            get { return categories ?? (categories = new BaseRepository<Category>(context)); }
        }

        public IRepository<Order> Orders
        {
            get { return orders ?? (orders = new BaseRepository<Order>(context)); }
        }

        public IRepository<ProviderServices> ProviderServices
        {
            get { return providerServices ?? (providerServices = new BaseRepository<ProviderServices>(context)); }
        }

        public IRepository<Service> Services
        {
            get { return services ?? (services = new BaseRepository<Service>(context)); }
        }

        public IRepository<User> Users
        {
            get { return users ?? (users = new BaseRepository<User>(context)); }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        #endregion

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
