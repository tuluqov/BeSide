using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeSide.BusinessLogic.Construct;
using BeSide.Common.Entities;

namespace BeSide.Presenter.WebSite.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService adminService;
        private readonly ISeviceService seviceService;
        private readonly ICategoryService categoryService;

        public AdminController(IAdminService adminService,
            ISeviceService seviceService,
            ICategoryService categoryService)
        {
            this.adminService = adminService;
            this.seviceService = seviceService;
            this.categoryService = categoryService;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Users()
        {
            var allUsers = adminService.GetAllUsers();

            return View(allUsers);
        }

        [HttpGet]
        public ActionResult Category()
        {
            var allCategory = categoryService.GetAllCategory();

            return View(allCategory);
        }

        [HttpPost]
        public void DeleteCategory(Category category)
        {
            categoryService.DeleteCategory(category);

            RedirectToAction("Category");
        }
    }
}