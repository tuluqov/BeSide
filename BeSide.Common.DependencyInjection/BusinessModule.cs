using Autofac;
using BeSide.BusinessLogic.BusinessComponents;
using BeSide.BusinessLogic.Construct;

namespace BeSide.Common.DependencyInjection
{
    public class BusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>()
                .As<IUserService>()
                .InstancePerRequest();

            builder.RegisterType<AdminService>()
                .As<IAdminService>()
                .InstancePerRequest();

            builder.RegisterType<CategoryService>()
                .As<ICategoryService>()
                .InstancePerRequest();

            builder.RegisterType<ServiceService>()
                .As<ISeviceService>()
                .InstancePerRequest();
        }
    }
}
