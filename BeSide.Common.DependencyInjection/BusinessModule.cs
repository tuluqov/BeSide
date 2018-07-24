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
using BeSide.Common.Entities;
using BeSide.DataAccess.Construct;
using Owin;

namespace BeSide.Common.DependencyInjection
{
    public class BusinessModule
    {
        public void RegisterComponent(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>()
                .As<IUserService>();
        }
    }
}
