using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using BeSide.Common.DependencyInjection;
using BeSide.Common.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Owin;

namespace BeSide.Presenter.WebSite
{
    public partial class Startup
    {
        public void ConfigureAutufac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            //var executingAssembly = Assembly.GetExecutingAssembly();
            //builder.RegisterControllers(executingAssembly);
            
            builder.RegisterModule(new DataAccessModule("DefaultConnection"));
            builder.RegisterModule(new BusinessModule());

            builder.RegisterControllers(typeof(MvcApplication).Assembly)
                .InstancePerRequest();

            var dataProtectionProvider = app.GetDataProtectionProvider();
            builder.Register<UserManager<ApplicationUser>>((c, p) => BuildUserManager(c, p, dataProtectionProvider));
            builder.RegisterType<UserStore<ApplicationUser>>().As<IUserStore<ApplicationUser>>().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }


        //builder.RegisterType<ApplicationSignInManager>().As<SignInManager<ApplicationUser, string>>().InstancePerRequest();



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