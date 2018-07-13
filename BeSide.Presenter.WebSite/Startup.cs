using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BeSide.Presenter.WebSite.Startup))]
namespace BeSide.Presenter.WebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
