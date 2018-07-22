using System;
using BeSide.Common.Entities;
using BeSide.DataAccess.Construct;
using BeSide.DataAccess.SqlDataAccess.DataContexts;
using BeSide.DataAccess.SqlDataAccess.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BeSide.DataAccess.SqlDataAccess.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EfDataContext context;

        private IRepository<Category> categories;
        private IRepository<Order> orders;
        private IRepository<ProviderServices> providerServices;
        private IRepository<Service> services;
        private IRepository<UserProfile> usersProfiles;

        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;

        private bool disposed;


        public UnitOfWork(string connectionString)
        {
            context = new EfDataContext(connectionString);
        }

        #region Implementation of IUnitOfWork

        public IdentityDbContext<ApplicationUser> IdentityUsers
        {
            get { return context; }
        }

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

        public IRepository<UserProfile> UsersProfiles
        {
            get { return usersProfiles ?? (usersProfiles = new BaseRepository<UserProfile>(context)); }
        }

        public UserManager<ApplicationUser> UserManager
        {
            get
            {
                return userManager ??
                       (userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context)));
            }
        }

        public RoleManager<ApplicationRole> RoleManager
        {
            get
            {
                return roleManager ??
                       (roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context)));
            }
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
