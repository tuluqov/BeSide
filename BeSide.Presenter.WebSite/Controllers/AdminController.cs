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
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryService.AddCategory(category);

                return RedirectToAction("Category", "Admin");
            }

            return View(category);
        }

        [HttpGet]
        public ActionResult Category()
        {
            var allCategory = categoryService.GetAllCategory();

            return View(allCategory);
        }

        [HttpGet]
        public ActionResult DeleteCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryService.DeleteCategory(category);

                return RedirectToAction("Category");
            }

            return RedirectToAction("NotFound", "Error");
        }

        [HttpGet]
        public ActionResult EditCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditCategory(Category category)
        {

            return RedirectToAction("Category");
        }
    }
}