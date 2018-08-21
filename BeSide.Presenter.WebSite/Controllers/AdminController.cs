using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeSide.BusinessLogic.Construct;
using BeSide.Common.Entities;
using BeSide.Presenter.WebSite.Models;
using BeSide.Presenter.WebSite.Models.Category;
using BeSide.Presenter.WebSite.Models.Order;
using BeSide.Presenter.WebSite.Models.Service;

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
            return RedirectToAction("Users");
        }

        #region Users

        [HttpGet]
        public ActionResult Users()
        {
            var allUsers = adminService.GetAllUsers();

            return View(allUsers);
        }

        #endregion

        #region Category


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
        public ActionResult DeleteCategory(int id)
        {

            var category = categoryService.GetById(id);

            if (category != null)
            {
                categoryService.DeleteById(id);

                return RedirectToAction("Category");
            }

            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            var category = categoryService.Find(m => m.Id == id).FirstOrDefault();

            if (category != null)
            {
                CategoryViewModel model = new CategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Name
                };

                return View(model);
            }

            return RedirectToAction("Category", "Admin");
        }

        [HttpPost]
        public ActionResult EditCategory(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Category category = categoryService.GetById(model.Id);

                category.Name = model.Name;

                categoryService.UpdateCategory(category);

                return RedirectToAction("Category", "Admin");

            }

            return View(model);
        }

        #endregion

        #region Service

        [HttpGet]
        public ActionResult Service()
        {
            var allCategory = categoryService.GetAllCategory();

            return View(allCategory);
        }

        [HttpGet]
        public ActionResult AddService()
        {
            ViewBag.Categoryes = new SelectList(categoryService.GetAllCategory(), "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddService(AddServiceViewModel addServiceView)
        {
            if (ModelState.IsValid)
            {
                Common.Entities.Service service = new Service
                {
                    CategoryId = addServiceView.CategoryId,
                    Name = addServiceView.ServiceName
                };

                seviceService.AddService(service);

                return RedirectToAction("Service", "Admin");
            }

            return View(addServiceView);
        }

        [HttpGet]
        public ActionResult EditService(int id)
        {
            Common.Entities.Service service = seviceService.GetById(id);

            ServiceViewModel model = new ServiceViewModel(service);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditService(ServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var service = seviceService.GetById(model.Id);

                service.Name = model.Name;

                seviceService.UpdateService(service);

                return RedirectToAction("Service", "Admin");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult DeleteService(int id)
        {
            var service = seviceService.GetById(id);

            if (service != null)
            {
                seviceService.DeleteById(id);

                return RedirectToAction("Service");
            }

            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult Logger()
        {
            return View();
        }

        #endregion
    }
}