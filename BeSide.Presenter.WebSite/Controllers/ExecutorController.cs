using System.Web.Mvc;
using BeSide.BusinessLogic.Construct;
using BeSide.Common.Entities;
using BeSide.Presenter.WebSite.Models.Category;
using BeSide.Presenter.WebSite.Models.User;
using PagedList;

namespace BeSide.Presenter.WebSite.Controllers
{
    public class ExecutorController : Controller
    {
        private readonly IUserService userService;
        private readonly ICategoryService categoryService;
        private readonly ISeviceService seviceService;

        public ExecutorController(IUserService userService,
            ICategoryService categoryService,
            ISeviceService seviceService)
        {
            this.userService = userService;
            this.categoryService = categoryService;
            this.seviceService = seviceService;
        }

        // GET: Executor
        [HttpGet]
        public ActionResult Index(int? ServiceId, int? page, string find)
        {
            if (ServiceId == null && find == null)
            {
                var allProviders = userService.GetAllProviders();
                ProviderCollectionViewModel providerCollection = new ProviderCollectionViewModel(allProviders);

                ViewBag.Categoryes = new CategoryCollectionViewModel(categoryService.GetAllCategory());

                return View(providerCollection.ToPagedList(page ?? 1, 10));
            }
            else if (find != null)
            {
                var findProviders = userService.FindProviders(m => m.CompanyName.ToLower().Contains(find.ToLower())
                                                                   || m.Discription.ToLower().Contains(find.ToLower()));

                ProviderCollectionViewModel providerCollection = new ProviderCollectionViewModel(findProviders);


                ViewBag.Categoryes = new CategoryCollectionViewModel(categoryService.GetAllCategory());

                return View(providerCollection.ToPagedList(page ?? 1, 10));
            }
            else
            {


                var service = seviceService.GetById((int)ServiceId);

                var profiles = userService.GetAllProviders();

                var searchProfiles = profiles;

                ProviderCollectionViewModel providerCollection = new ProviderCollectionViewModel(searchProfiles);

                ViewBag.Categoryes = new CategoryCollectionViewModel(categoryService.GetAllCategory());

                return View(providerCollection.ToPagedList(page ?? 1, 10));
            }
        }

        // GET: Order/Details/5
        [HttpGet]
        public ActionResult Details(string id)
        {
            ProviderViewModel provider = new ProviderViewModel((ProviderProfile)userService.GetById(id).UserProfile);

            return View(provider);
        }
    }
}