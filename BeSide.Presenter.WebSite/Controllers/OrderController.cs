using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeSide.BusinessLogic.Construct;
using BeSide.Common.Entities;
using BeSide.Presenter.WebSite.Models.Category;
using BeSide.Presenter.WebSite.Models.Feedback;
using BeSide.Presenter.WebSite.Models.Order;
using Microsoft.AspNet.Identity;
using PagedList;

namespace BeSide.Presenter.WebSite.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly ICategoryService categoryService;
        private readonly IUserService userService;
        private readonly IFeedbackService feedbackService;
        private readonly IImageService imageService;

        public OrderController(IOrderService orderService,
            ICategoryService categoryService,
            IUserService userService,
            IFeedbackService feedbackService,
            IImageService imageService)
        {
            this.orderService = orderService;
            this.categoryService = categoryService;
            this.userService = userService;
            this.feedbackService = feedbackService;
            this.imageService = imageService;
        }

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
        [ValidateAntiForgeryToken]
        public ActionResult AddFeedback(FeedbackViewModel model)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<Feedback> checkFeedback = feedbackService.Find(m =>
                    m.OrderId == model.OrderId && m.ProviderProfileId == User.Identity.GetUserId());

                if (!checkFeedback.Any())
                {
                    Feedback feedback = model.GetFeedback();

                    feedback.ProviderProfileId = User.Identity.GetUserId();
                    feedback.CreateDate = DateTime.Now;

                    feedbackService.Add(feedback);

                    return RedirectToAction($"Details/{model.OrderId}");
                }

                return RedirectToAction("Error", "Home");

            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "provider")]
        public ActionResult EditFeedback(int id)
        {
            var feedback = feedbackService.GetById(id);

            FeedbackViewModel model = new FeedbackViewModel(feedback);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "provider")]
        [ValidateAntiForgeryToken]
        public ActionResult EditFeedback(FeedbackViewModel model)
        {
            if (ModelState.IsValid)
            {
                var feedback = model.GetFeedback();

                feedback.ProviderProfileId = User.Identity.GetUserId();

                feedbackService.Update(feedback);

                return RedirectToAction($"Details/{feedback.OrderId}");
            }

            return HttpNotFound();
        }

        [HttpGet]
        [Authorize(Roles = "provider")]
        public ActionResult DeleteFeedback(int id)
        {
            var feedback = feedbackService.GetById(id);

            if (feedback != null && feedback.ProviderProfileId == User.Identity.GetUserId())
            {
                var idOrder = feedback.OrderId;

                feedbackService.Delete(id);

                var order = orderService.GetById(idOrder);
                order.OrderStatus = OrderStatus.Active;
                order.ProviderProfileId = null;
                orderService.Update(order);

                return RedirectToAction($"Details/{idOrder}");
            }

            return HttpNotFound();
        }

        [HttpGet]
        [Authorize(Roles = "client")]
        public ActionResult FeedbacksOrders()
        {
            var feedbacks = feedbackService.GetUserOrdersFeedbacks(User.Identity.GetUserId());

            FeedbacksCollectionViewModel model = new FeedbacksCollectionViewModel(feedbacks);

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "provider")]
        public ActionResult FeedbacksProvider()
        {
            var feedbacks = feedbackService.GetProviderFeedbacks(User.Identity.GetUserId());

            FeedbacksCollectionViewModel model = new FeedbacksCollectionViewModel(feedbacks);

            return View("FeedbacksOrders", model);
        }

        [HttpGet]
        [Authorize(Roles = "provider")]
        public ActionResult SelectedFeedbacks()
        {
            var feedbacks = feedbackService.GetProviderFeedbacks(User.Identity.GetUserId()).Where(m => m.Order.OrderStatus == OrderStatus.Accepted);

            FeedbacksCollectionViewModel model = new FeedbacksCollectionViewModel(feedbacks);

            return View("FeedbacksOrders", model);
        }


        //выбор исполнителя
        [HttpGet]
        [Authorize(Roles = "client")]
        public ActionResult SelectPerfomer(int orderId, string userId)
        {
            var order = orderService.GetById(orderId);

            order.OrderStatus = OrderStatus.Accepted;
            order.ProviderProfileId = userId;

            orderService.Update(order);

            return RedirectToAction($"Details/{orderId}");
        }

        //отмена выбора исполнителя
        [HttpGet]
        [Authorize(Roles = "client")]
        public ActionResult Deselected(int orderId, OrderStatus status)
        {
            if (status == OrderStatus.Accepted)
            {
                var order = orderService.GetById(orderId);

                order.OrderStatus = OrderStatus.Active;
                order.ProviderProfileId = null;

                orderService.Update(order);

                return RedirectToAction($"Details/{orderId}");
            }

            return HttpNotFound();
        }



        #endregion

        #region Orders

        // GET: Order
        [HttpGet]
        public ActionResult Index(int? ServiceId, int? page, string find)
        {
            if (ServiceId == null && find == null)
            {
                var allOrders = orderService.GetAll();
                OrderCollectionViewModel collectionOrders = new OrderCollectionViewModel(allOrders);

                ViewBag.Categoryes = new CategoryCollectionViewModel(categoryService.GetAllCategory());

                return View(collectionOrders.ToPagedList(page ?? 1, 10));
            }
            else if (find != null)
            {
                var findOrders = orderService.Find(m => m.ShortDescription.ToLower().Contains(find.ToLower())
                                                        || m.FullDescription.ToLower().Contains(find.ToLower()));

                OrderCollectionViewModel collectionOrders = new OrderCollectionViewModel(findOrders);

                ViewBag.Categoryes = new CategoryCollectionViewModel(categoryService.GetAllCategory());

                return View(collectionOrders.ToPagedList(page ?? 1, 10));
            }
            else
            {
                var searchOrders = orderService.Find(m => m.ServiceId == ServiceId);
                OrderCollectionViewModel collectionOrders = new OrderCollectionViewModel(searchOrders);

                ViewBag.Categoryes = new CategoryCollectionViewModel(categoryService.GetAllCategory());

                return View(collectionOrders.ToPagedList(page ?? 1, 10));
            }
        }

        // GET: Order/Details/5
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderViewModel model = new OrderViewModel(orderService.GetById((int)id));

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
        public ActionResult Create(OrderViewModel model, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var avatar = new Image
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Avatar,
                        ContentType = upload.ContentType
                    };

                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    model.Images = new List<Image> { avatar };
                }

                model.ClientProfileId = User.Identity.GetUserId();
                model.CreateDate = DateTime.Now;
                model.OrderStatus = OrderStatus.Active;

                orderService.Add(model.GetOrder());

                return RedirectToAction("Index", "Order");
            }

            ViewBag.Category = categoryService.GetAllCategory();

            return View(model);
        }

        // GET: Order/Edit/5
        [HttpGet]
        [Authorize(Roles = "client")]
        public ActionResult Edit(int id)
        {
            var order = orderService.GetById(id);

            if (order == null)
            {
                return HttpNotFound();
            }

            OrderViewModel model = new OrderViewModel(order);

            ViewBag.Categoryes = new CategoryCollectionViewModel(categoryService.GetAllCategory());

            return View(model);
        }

        // POST: Order/Edit/5
        [HttpPost]
        [Authorize(Roles = "client")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderViewModel model, HttpPostedFileBase upload)
        {
            if (model.ClientProfileId == User.Identity.GetUserId())
            {
                if (ModelState.IsValid)
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        //if (model.Images.Any(f => f.FileType == FileType.Avatar))
                        //{
                        //    imageService.Delete(model.Images.First(p => p.FileType == FileType.Avatar).Id);
                        //}

                        var avatar = new Image
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.Avatar,
                            ContentType = upload.ContentType
                        };

                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            avatar.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        
                        model.Images = new List<Image> { avatar };
                    }

                    orderService.Update(model.GetOrder());

                    return RedirectToAction("UserOrders", "Account");
                }

                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        // POST: Order/Delete/5
        [HttpGet]
        [Authorize(Roles = "client")]
        public ActionResult Delete(int id)
        {
            Order order = orderService.GetById(id);

            if (order == null)
            {
                return HttpNotFound();
            }

            if (order.ClientProfileId == User.Identity.GetUserId())
            {
                //foreach (Image image in order.Galery)
                //{
                //    imageService.Delete(image.Id);
                //}

                orderService.Delete(id);

                return RedirectToAction("UserOrders", "Account");
            }

            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}
