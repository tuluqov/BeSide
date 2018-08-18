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

            builder.RegisterType<OrderService>()
                .As<IOrderService>()
                .InstancePerRequest();

            builder.RegisterType<FeedbackService>()
                .As<IFeedbackService>()
                .InstancePerRequest();

            builder.RegisterType<ContactMessageService>()
                .As<IContactMessageService>()
                .InstancePerRequest();

            builder.RegisterType<ImageService>()
                .As<IImageService>()
                .InstancePerRequest();
        }
    }
}
