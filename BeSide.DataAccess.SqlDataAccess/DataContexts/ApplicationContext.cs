using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeSide.Common.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BeSide.DataAccess.SqlDataAccess.DataContexts
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<User> UsersProfile { get; set; }

        public ApplicationContext(string connectionString)
            : base(connectionString)
        {
        }
    }
}
