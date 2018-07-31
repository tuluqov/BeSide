using System;
using System.Data.Entity;
using BeSide.Common.Entities;
using BeSide.DataAccess.Construct;
using Microsoft.AspNet.Identity;

namespace BeSide.DataAccess.SqlDataAccess.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;
        private bool disposed;

        public UnitOfWork(DbContext context,
            IRepository<Category> categories,
            IRepository<Order> orders,
            IRepository<ProviderServices> providerServices,
            IRepository<Service> services,
            IClientProfileRepository clientProfiles,
            IRepository<Feedback> feedbacks,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            this.context = context;
            Categories = categories;
            Orders = orders;
            ProviderServices = providerServices;
            Services = services;
            ClientProfiles = clientProfiles;
            Feedbacks = feedbacks;
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public DbContext EfContext => context;

        public IRepository<Category> Categories { get; }

        public IRepository<Order> Orders { get; }

        public IRepository<ProviderServices> ProviderServices { get; }

        public IRepository<Service> Services { get; }

        public IClientProfileRepository ClientProfiles { get; }

        public IRepository<Feedback> Feedbacks { get; }
        
        public UserManager<ApplicationUser> UserManager { get; }

        public RoleManager<ApplicationRole> RoleManager { get; }

        public void Save()
        {
            context.SaveChanges();
        }

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
