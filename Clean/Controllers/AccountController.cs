using Clean.Constants;
using Clean.Models;
using Clean.Services;
using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Clean.Controllers
{
    public sealed class AccountController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController()
            : this(new InMemoryAuthenticationService())
        {
        }

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            if (this.Request.IsAuthenticated)
            {
                return this.RedirectToAction(RouteConstants.HomeAction, RouteConstants.HomeController);
            }

            this.ViewBag.ReturnUrl = returnUrl;
            return this.View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (!_authenticationService.ValidateCredentials(model.Username, model.Password))
            {
                this.ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return this.View(model);
            }

            ClaimsIdentity identity = _authenticationService.CreateIdentity(model.Username);
            IAuthenticationManager authenticationManager = this.HttpContext.GetOwinContext().Authentication;

            authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);

            if (!string.IsNullOrWhiteSpace(returnUrl) && this.Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            return this.RedirectToAction(RouteConstants.HomeAction, RouteConstants.HomeController);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            IAuthenticationManager authenticationManager = this.HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();

            return this.RedirectToAction(RouteConstants.HomeAction, RouteConstants.HomeController);
        }
    }
}
