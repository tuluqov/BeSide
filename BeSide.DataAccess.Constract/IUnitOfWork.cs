using System;
using BeSide.Common.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BeSide.DataAccess.Construct
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Category> Categories { get; }
        IRepository<Order> Orders { get; }
        IRepository<ProviderServices> ProviderServices { get; }
        IRepository<Service> Services { get; }
        IRepository<UserProfile> UsersProfiles { get; }
        IdentityDbContext<ApplicationUser> IdentityUsers { get; }
        UserManager<ApplicationUser> UserManager { get; }
        RoleManager<ApplicationRole> RoleManager { get; }

        void Save();
    }
}
