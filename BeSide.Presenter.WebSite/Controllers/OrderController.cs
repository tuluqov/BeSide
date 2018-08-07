using System;
using System.Linq;
using System.Web.Mvc;
using BeSide.BusinessLogic.Construct;
using BeSide.Presenter.WebSite.Models.Category;
using BeSide.Presenter.WebSite.Models.Feedback;
using BeSide.Presenter.WebSite.Models.Order;
using Microsoft.AspNet.Identity;

namespace BeSide.Presenter.WebSite.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly ICategoryService categoryService;
        private readonly IUserService userService;
        private readonly IFeedbackService feedbackService;

        public OrderController(IOrderService orderService,
            ICategoryService categoryService,
            IUserService userService,
            IFeedbackService feedbackService)
        {
            this.orderService = orderService;
            this.categoryService = categoryService;
            this.userService = userService;
            this.feedbackService = feedbackService;
        }

        // GET: Order
        [HttpGet]
        public ActionResult Index()
        {
            var allOrders = orderService.GetAll();
            OrderCollectionViewModel collectionOrders = new OrderCollectionViewModel(allOrders);

            ViewBag.Categoryes = new CategoryCollectionViewModel(categoryService.GetAllCategory());

            return View(collectionOrders);
        }

        //[HttpGet]
        //public ActionResult Index(int ServiceId)
        //{
        //    var searchOrders = orderService.Find(m => m.ServiceId == ServiceId);
        //    OrderCollectionViewModel collectionOrders = new OrderCollectionViewModel(searchOrders);

        //    ViewBag.Categoryes = new CategoryCollectionViewModel(categoryService.GetAllCategory());

        //    return View(collectionOrders);
        //}

        #region Feedbacks

        [HttpGet]
        public ActionResult Feedbacks(int id)
        {
            FeedbacksCollectionViewModel feedbacks =
                new FeedbacksCollectionViewModel(orderService.GetFeedbacks(id));

            return View(feedbacks);
        }

        [HttpGet]
        [Authorize(Roles = "provider")]
        public ActionResult AddFeedback(int OrderId)
        {
            FeedbackViewModel model = new FeedbackViewModel { OrderId = OrderId };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "provider")]
        public ActionResult AddFeedback(FeedbackViewModel model)
        {
            Common.Entities.Feedback feedback = model.GetFeedback();

            feedback.ProviderProfileId = User.Identity.GetUserId();

            feedbackService.Add(feedback);

            return RedirectToAction($"Details/{model.OrderId}");
        }

        #endregion

        #region Orders

        // GET: Order/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            OrderViewModel model = new OrderViewModel(orderService.GetById(id));

            ViewBag.User = userService.GetById(model.ClientProfileId);

            return View(model);
        }

        // GET: Order/Create
        [HttpGet]
        [Authorize(Roles = "client")]
        public ActionResult Create()
        {
            ViewBag.Category = categoryService.GetAllCategory();

            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [Authorize(Roles = "client")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.ClientProfileId = User.Identity.GetUserId();
                model.CreateDate = DateTime.Now;

                orderService.Add(model.GetOrder());

                return RedirectToAction("Index", "Order");
            }

            return View();
        }

        #endregion
        

        // GET: Order/Edit/5
        [HttpGet]
        [Authorize(Roles = "client")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
        [HttpPost]
        [Authorize(Roles = "client")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        [Authorize(Roles = "client")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
        [HttpPost]
        [Authorize(Roles = "client")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
