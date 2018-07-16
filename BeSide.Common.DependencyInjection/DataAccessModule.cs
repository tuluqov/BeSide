using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using System.Web.Mvc;

using BeSide.DataAccess.SqlDataAccess.DataContexts;
using BeSide.DataAccess.SqlDataAccess.UnitOfWorks;
using BeSide.DataAccess.Construct;

namespace BeSide.Common.DependencyInjection
{
    public class DataAccessModule
    {
        private readonly string connectionString;

        public DataAccessModule(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Configurate()
        {
            var builder = new ContainerBuilder();

            //builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .WithParameter("connectionString", connectionString);

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
