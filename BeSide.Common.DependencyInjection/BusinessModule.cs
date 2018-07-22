using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using BeSide.BusinessLogic.BusinessComponents;
using BeSide.BusinessLogic.Construct;
using BeSide.DataAccess.Construct;

namespace BeSide.Common.DependencyInjection
{
    public class BusinessModule
    {
        public void Configurate()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<UserService>()
                .As<IUserService>();



            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
