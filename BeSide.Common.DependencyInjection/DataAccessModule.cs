using System.Data.Entity;
using System.Reflection;
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
using Module = Autofac.Module;
using Service = BeSide.Common.Entities.Service;

namespace BeSide.Common.DependencyInjection
{
    public class DataAccessModule : Module
    {
        private readonly string connectionString;

        public DataAccessModule(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EfDataContext>()
                .As<DbContext>()
                .InstancePerRequest();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .WithParameter("connectionString", connectionString);

            //Универсальный метод для всех обобщенных репозиториев
            builder.RegisterGeneric(typeof(BaseRepository<>))
                .As(typeof(IRepository<>))
                .AsSelf().
                InstancePerRequest();
        }
    }
}
