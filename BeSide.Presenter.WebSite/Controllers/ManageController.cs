using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BeSide.BusinessLogic.Construct;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using BeSide.Presenter.WebSite.Models;

namespace BeSide.Presenter.WebSite.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        
    }
}