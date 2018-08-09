using BeSide.BusinessLogic.Construct;
using BeSide.BusinessLogic.Construct.DTO;
using BeSide.BusinessLogic.Construct.Infrastructure;
using BeSide.Presenter.WebSite.Models;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BeSide.Common.Entities;
using BeSide.Presenter.WebSite.Models.Order;
using Microsoft.AspNet.Identity;

namespace BeSide.Presenter.WebSite.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly IOrderService orderService;

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public AccountController(IUserService userService, 
            IOrderService orderService)
        {
            this.userService = userService;
            this.orderService = orderService;
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
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            //await SetInitialDataAsync();

            if (ModelState.IsValid)
            {
                UserDto userDto = new UserDto
                {
                    Email = model.Email,
                    Password = model.Password,
                    Role = model.Role,
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Patronymic = model.Patronymic,
                    PhoneNumber = model.PhoneNumber
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

        [HttpGet]
        [Authorize(Roles = "client")]
        public ActionResult UserOrders()
        {
            var userOrders = orderService.GetUserOrders(User.Identity.GetUserId());

            OrderCollectionViewModel ordersModel = new OrderCollectionViewModel(userOrders); 

            return View(ordersModel);
        }

        #endregion
    }
}