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
                .As<IUserService>();
        }
    }
}
