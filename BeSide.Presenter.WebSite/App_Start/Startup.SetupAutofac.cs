using BeSide.Common.DependencyInjection;

namespace BeSide.Presenter.WebSite
{
    public partial class Startup
    {
        public void ConfigureAutufac()
        {
            DataAccessModule dataAccessModule = new DataAccessModule("DefaultConnection");
            dataAccessModule.Configurate();
        }
    }
}