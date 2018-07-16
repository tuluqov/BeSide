using System.Data.Entity;
using BeSide.Common.Entities;

namespace BeSide.DataAccess.SqlDataAccess.DataContexts
{
    public class EfDataContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProviderServices> ProviderServiceses { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<User> Users { get; set; }

        public EfDataContext(string connectionString) : base(connectionString)
        {
            //добавить строку подключения
        }
    }
}
