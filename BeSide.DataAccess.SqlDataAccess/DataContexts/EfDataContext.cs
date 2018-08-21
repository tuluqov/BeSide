using System.Data.Entity;
using BeSide.Common.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BeSide.DataAccess.SqlDataAccess.DataContexts
{
    public class EfDataContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ClientProfile> ClientProfiles { get; set; }
        public DbSet<ProviderProfile> ProviderProfiles { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Image> Images { get; set; }

        public EfDataContext(string connectionString) : base(connectionString)
        {
        }

        public EfDataContext() : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProviderProfile>()
                .Map(m =>
                {
                    m.MapInheritedProperties();
                    m.ToTable("ProviderProfiles");
                });

            modelBuilder.Entity<ClientProfile>()
                .Map(m =>
                {
                    m.MapInheritedProperties();
                    m.ToTable("ClientProfiles");
                });

            modelBuilder.Entity<ProviderProfile>().HasMany(c => c.Services)
                .WithMany(s => s.ProviderProfiles)
                .Map(t => t.MapLeftKey("ProviderProfileId")
                    .MapRightKey("ServiceId")
                    .ToTable("ProviderServices"));

            base.OnModelCreating(modelBuilder);
        }
    }
}
