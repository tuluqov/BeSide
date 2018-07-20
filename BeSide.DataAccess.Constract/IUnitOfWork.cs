using System;
using BeSide.Common.Entities;

namespace BeSide.DataAccess.Construct
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Category> Categories { get; }
        IRepository<Order> Orders { get; }
        IRepository<ProviderServices> ProviderServices { get; }
        IRepository<Service> Services { get; }
        IRepository<User> Users { get; }
        IRepository<ApplicationUser> ApplicationUsers { get; set; }

        void Save();
    }
}
