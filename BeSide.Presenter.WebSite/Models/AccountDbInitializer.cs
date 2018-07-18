using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BeSide.Presenter.WebSite.Models
{
    public class AccountDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var adminRole = new IdentityRole("admin");
            var userRole = new IdentityRole("user");
            var providerRole = new IdentityRole("provider");

            roleManager.Create(adminRole);
            roleManager.Create(userRole);
            roleManager.Create(providerRole);

            var admin = new ApplicationUser
            {
                Email = "admin@gmail.com",
                UserName = "Admin"
            };
            string password = "Admin_1234";
            var result = userManager.Create(admin, password);

            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, adminRole.Name);
            }

            base.Seed(context);
        }
    }
}