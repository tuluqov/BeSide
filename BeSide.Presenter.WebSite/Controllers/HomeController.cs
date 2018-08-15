using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeSide.BusinessLogic.Construct;
using BeSide.Common.Entities;
using BeSide.Presenter.WebSite.Models;

namespace BeSide.Presenter.WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContactMessageService messageService;

        public HomeController(IContactMessageService messageService)
        {
            this.messageService = messageService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactMessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                messageService.SendMessage(model.GetMessage());

                return View();
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }
    }
}