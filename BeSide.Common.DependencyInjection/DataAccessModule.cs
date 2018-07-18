using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using BeSide.Common.Entities;
using BeSide.DataAccess.SqlDataAccess.DataContexts;
using BeSide.DataAccess.SqlDataAccess.UnitOfWorks;
using BeSide.DataAccess.Construct;
using BeSide.DataAccess.SqlDataAccess.Repositories;
using Service = BeSide.Common.Entities.Service;

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
            
            //Универсальный метод для всех обобщенных репозиториев
            builder.RegisterGeneric(typeof(BaseRepository<>))
                .As(typeof(IRepository<>))
                .AsSelf().
                InstancePerRequest();


            #region Для всех репозиториев
            //builder.RegisterType<BaseRepository<Category>>()
            //    .As<IRepository<Category>>().AsSelf().InstancePerRequest();

            //builder.RegisterType<BaseRepository<Order>>()
            //    .As<IRepository<Order>>().AsSelf().InstancePerRequest();

            //builder.RegisterType<BaseRepository<ProviderServices>>()
            //    .As<IRepository<ProviderServices>>().AsSelf().InstancePerRequest();

            //builder.RegisterType<BaseRepository<Service>>()
            //    .As<IRepository<Service>>().AsSelf().InstancePerRequest();
            #endregion

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
