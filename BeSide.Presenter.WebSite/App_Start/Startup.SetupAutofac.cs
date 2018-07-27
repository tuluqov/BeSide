using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using BeSide.Common.DependencyInjection;
using BeSide.Common.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using Owin;

namespace BeSide.Presenter.WebSite
{
    public partial class Startup
    {
        public void ConfigureAutufac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterModule(new DataAccessModule());
            builder.RegisterModule(new BusinessModule());


            builder.RegisterType<UserStore<ApplicationUser>>().
                As<IUserStore<ApplicationUser>>()
                .InstancePerRequest();

            builder.RegisterType<RoleStore<ApplicationRole>>().
                As<IRoleStore<ApplicationRole, string>>()
                .InstancePerRequest();

            builder.RegisterType<ApplicationRoleManager>()
                .As<RoleManager<ApplicationRole>>()
                .InstancePerRequest();

            builder.RegisterType<ApplicationUserManager>()
                .As<UserManager<ApplicationUser>>()
                .InstancePerRequest();


            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        
        private UserManager<ApplicationUser> BuildUserManager(IComponentContext context, IEnumerable<Parameter> parameters, IDataProtectionProvider dataProtectionProvider)
        {
            var manager = new UserManager<ApplicationUser>(context.Resolve<IUserStore<ApplicationUser>>());

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }
    }
}