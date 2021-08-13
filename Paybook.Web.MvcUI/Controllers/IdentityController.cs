using Paybook.BusinessLayer.Identity;
using Paybook.ServiceLayer.Constants;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.Web.MvcUI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Paybook.Web.MvcUI.Controllers
{
    public class IdentityController : Controller
    {
        private readonly ILogger _logger;
        private readonly ILoginProcessor _login;
        private readonly IUserProcessor _user;

        public IdentityController(ILogger logger, ILoginProcessor login, IUserProcessor user)
        {
            _logger = logger;
            _login = login;
            _user = user;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(IdentityUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUserModel identityUser = new IdentityUserModel { Username = model.Username, PasswordHash = model.Password };
                string result = _login.IsValid(identityUser);
                if (result == LoginResultConst.UserNotFound)
                    ModelState.AddModelError(string.Empty, "We did not find the associated user you are trying to login.");
                else if (result == LoginResultConst.UserNotMatch)
                    ModelState.AddModelError(string.Empty, "Username and Password does not match in our system. " + model.Password);
                else if (result == LoginResultConst.UserMatch)
                {
                    IdentityUserModel userData = _user.GetByUsername(model.Username);
                    TempData[TempdataNames.LoginUserFullname] = $"{userData.FirstName} {userData.LastName}";

                    FormsAuthentication.SetAuthCookie(model.Username, model.IsPersistent);

                    return RedirectToAction("Dashboard", "Business", new { area = "Chief" });
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View();
        }
    }
}