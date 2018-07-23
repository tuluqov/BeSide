using System.Data.Entity;
using BeSide.Common.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BeSide.DataAccess.SqlDataAccess.DataContexts
{
    public class EfDataContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProviderServices> ProviderServiceses { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<UserProfile> UsersProfiles { get; set; }

        public EfDataContext(string connectionString) : base(connectionString)
        {
        }

        public EfDataContext() : base("Data Source=DESKTOP-8T3R9H9;Initial Catalog=Test;Integrated Security=True;Pooling=False")
        {
        }
    }
}
