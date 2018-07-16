using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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