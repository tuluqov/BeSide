using BeSide.BusinessLogic.Construct;
using BeSide.BusinessLogic.Construct.DTO;
using BeSide.BusinessLogic.Construct.Infrastructure;
using BeSide.Presenter.WebSite.Models;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BeSide.Common.Entities;
using BeSide.Presenter.WebSite.Models.Order;
using BeSide.Presenter.WebSite.Models.Service;
using BeSide.Presenter.WebSite.Models.User;
using Microsoft.AspNet.Identity;

namespace BeSide.Presenter.WebSite.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly IOrderService orderService;
        private readonly IImageService imageService;
        private readonly ICategoryService categoryService;
        private readonly ISeviceService serviceService;

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public AccountController(IUserService userService,
            IOrderService orderService,
            IImageService imageService,
            ICategoryService categoryService,
            ISeviceService serviceService)
        {
            this.userService = userService;
            this.orderService = orderService;
            this.imageService = imageService;
            this.categoryService = categoryService;
            this.serviceService = serviceService;
        }

        #region LoginRigester

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            await SetInitialDataAsync();

            if (ModelState.IsValid)
            {
                UserDto userDto = new UserDto
                {
                    Email = model.Email,
                    Password = model.Password
                };

                ClaimsIdentity claim = await userService.Authenticate(userDto);

                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);

                    if (User.IsInRole("admin"))
                    {
                        return RedirectToAction("Index", "Admin");
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model, HttpPostedFileBase upload)
        {
            await SetInitialDataAsync();

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

                UserDto userDto = new UserDto
                {
                    Email = model.Email,
                    Password = model.Password,
                    Role = model.Role,
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Patronymic = model.Patronymic,
                    PhoneNumber = model.PhoneNumber,
                    Images = model.Images
                };

                OperationDetails operationDetails = userService.Create(userDto);

                if (operationDetails.Succedeed)
                {
                    await Login(new LoginViewModel
                    {
                        Email = model.Email,
                        Password = model.Password
                    });

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                }
            }

            return View(model);
        }

        private async Task SetInitialDataAsync()
        {
            await userService.SetInitialData(new UserDto
            {
                Email = "admin@gmail.com",
                UserName = "admin@gmail.com",
                Password = "123456Qq_",
                Role = "admin"
            }, new List<string> { "client", "provider", "admin" });
        }


        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Profile

        [HttpGet]
        [Authorize(Roles = "provider, client")]
        public ActionResult UserProfile()
        {
            string userId = User.Identity.GetUserId();

            ApplicationUser user = userService.GetById(userId);

            return View(user);
        }

        #region Edit

        [HttpGet]
        [Authorize(Roles = "provider")]
        public ActionResult EditProvider()
        {
            var provider = (ProviderProfile)userService.GetById(User.Identity.GetUserId()).UserProfile;

            EditProviderProfileViewModel model = new EditProviderProfileViewModel(provider);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "provider")]
        [ValidateAntiForgeryToken]
        public ActionResult EditProvider(EditProviderProfileViewModel model, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                var appUser = userService.GetById(model.Id);

                appUser.UserProfile = model.GetProfile();

                appUser.UserProfile.Id = appUser.Id;
                appUser.Email = model.Email;
                appUser.PhoneNumber = model.PhoneNumber;

                if (upload != null && upload.ContentLength > 0)
                {
                    appUser.UserProfile.FileName = System.IO.Path.GetFileName(upload.FileName);
                    appUser.UserProfile.FileType = FileType.Avatar;
                    appUser.UserProfile.ContentType = upload.ContentType;

                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        appUser.UserProfile.Content = reader.ReadBytes(upload.ContentLength);
                    }
                }

                appUser.Email = model.Email;
                appUser.PhoneNumber = model.PhoneNumber;

                userService.UpdateProvider(appUser.UserProfile);

                return RedirectToAction("UserProfile", "Account");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "client")]
        public ActionResult EditClient()
        {
            var client = (ClientProfile)userService.GetById(User.Identity.GetUserId()).UserProfile;

            EditClientViewModel model = new EditClientViewModel(client);

            return View(model);

        }

        [HttpPost]
        [Authorize(Roles = "client")]
        [ValidateAntiForgeryToken]
        public ActionResult EditClient(EditClientViewModel model, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                var appUser = userService.GetById(model.Id);

                appUser.UserProfile = model.GetProfile();

                appUser.UserProfile.Id = appUser.Id;
                appUser.Email = model.Email;
                appUser.PhoneNumber = model.PhoneNumber;

                if (upload != null && upload.ContentLength > 0)
                {
                    appUser.UserProfile.FileName = System.IO.Path.GetFileName(upload.FileName);
                    appUser.UserProfile.FileType = FileType.Avatar;
                    appUser.UserProfile.ContentType = upload.ContentType;

                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        appUser.UserProfile.Content = reader.ReadBytes(upload.ContentLength);
                    }
                }

                userService.UpdateClient(appUser.UserProfile);

                return RedirectToAction("UserProfile", "Account");
            }

            return View(model);
        }

        public ActionResult GetImage(string id)
        {
            var user = userService.GetById(id);

            return File(user.UserProfile.Content, user.UserProfile.ContentType);
        }

        #endregion

        [HttpGet]
        [Authorize(Roles = "client")]
        public ActionResult UserOrders()
        {
            var userOrders = orderService.GetUserOrders(User.Identity.GetUserId()).OrderByDescending(m => m.CreateDate);

            OrderCollectionViewModel ordersModel = new OrderCollectionViewModel(userOrders);

            return View(ordersModel);
        }

        [HttpGet]
        [Authorize(Roles = "client")]
        public ActionResult Feedbacks()
        {
            var userOrders = orderService.GetUserOrders(User.Identity.GetUserId()).OrderByDescending(m => m.CreateDate);

            OrderCollectionViewModel ordersModel = new OrderCollectionViewModel(userOrders);

            return View(ordersModel);
        }

        [HttpGet]
        [Authorize(Roles = "client, provider")]
        public ActionResult UserOrdersStatused(OrderStatus orderStatus)
        {
            IEnumerable<Order> orders;

            switch (orderStatus)
            {
                case OrderStatus.Active:
                    {
                        orders = orderService.Find(m => m.OrderStatus == OrderStatus.Active &&
                                                        m.ClientProfileId == User.Identity.GetUserId())
                            .OrderByDescending(m => m.CreateDate);

                        OrderCollectionViewModel ordersModel = new OrderCollectionViewModel(orders);

                        return View(ordersModel);
                    }


                case OrderStatus.Accepted:
                    {
                        orders = orderService.Find(m => m.OrderStatus == OrderStatus.Accepted &&
                                                        (m.ClientProfileId == User.Identity.GetUserId()
                                                        || m.ProviderProfileId == User.Identity.GetUserId()))
                            .OrderByDescending(m => m.CreateDate);

                        OrderCollectionViewModel ordersModel = new OrderCollectionViewModel(orders);

                        return View(ordersModel);
                    }

                case OrderStatus.Complited:
                    {
                        orders = orderService.Find(m => m.OrderStatus == OrderStatus.Complited &&
                                                        (m.ClientProfileId == User.Identity.GetUserId()
                                                         || m.ProviderProfileId == User.Identity.GetUserId()))
                            .OrderByDescending(m => m.CreateDate);

                        OrderCollectionViewModel ordersModel = new OrderCollectionViewModel(orders);

                        return View(ordersModel);
                    }


                case OrderStatus.NotComplited:
                    {
                        orders = orderService.Find(m => m.OrderStatus == OrderStatus.NotComplited &&
                                                        (m.ClientProfileId == User.Identity.GetUserId()
                                                         || m.ProviderProfileId == User.Identity.GetUserId()))
                            .OrderByDescending(m => m.CreateDate);

                        OrderCollectionViewModel ordersModel = new OrderCollectionViewModel(orders);

                        return View(ordersModel);
                    }

                default:
                    return HttpNotFound();
            }
        }

        [HttpGet]
        [Authorize(Roles = "provider")]
        public ActionResult AddService()
        {
            ViewBag.Category = categoryService.GetAllCategory();

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddService(int? idService)
        {
            if (idService == null)
            {
                return HttpNotFound();
            }

            //var user = userService.GetById(User.Identity.GetUserId());

            //var provider = (ProviderProfile)user.UserProfile;

            var provider = userService.FindProviders(m => m.Id == User.Identity.GetUserId()).FirstOrDefault();

            Service service = serviceService.GetById((int)idService);

            if (service == null)
            {
                return HttpNotFound();
            }

            //if (provider.Services.Any(m => m.Id == idService))
            //{
            //    return HttpNotFound();
            //}

            provider.Services.Add(service);

            userService.UpdateProvider(provider);

            return RedirectToAction("UserProfile", "Account");
        }

        [HttpGet]
        public ActionResult Services()
        {
            var user = userService.GetById(User.Identity.GetUserId());

            var provider = (ProviderProfile)user.UserProfile;

            var allServices = provider.Services;

            return View(allServices);
        }

        [HttpGet]
        public ActionResult DeleteService(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var user = userService.GetById(User.Identity.GetUserId());

            var provider = (ProviderProfile)user.UserProfile;

            var service = provider.Services.First(m => m.Id == id);

            if (service == null)
            {
                return HttpNotFound();
            }

            provider.Services.Remove(service);

            return RedirectToAction("Services");
        }

        #endregion
    }
}