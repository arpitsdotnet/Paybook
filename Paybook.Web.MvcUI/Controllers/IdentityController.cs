using Paybook.BusinessLayer.Business;
using Paybook.BusinessLayer.Identity;
using Paybook.ServiceLayer.Constants;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.Web.MvcUI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
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
        private readonly IBusinessProcessor _business;

        public IdentityController(ILogger logger, ILoginProcessor login, IUserProcessor user, IBusinessProcessor business)
        {
            _logger = logger;
            _login = login;
            _user = user;
            _business = business;
        }

        public ActionResult Login(string returnUrl)
        {
            if (!string.IsNullOrEmpty(User.Identity.Name))
            {
                SaveUserdataIntoCookies(User.Identity.Name);
                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);
                return RedirectToAction("Dashboard", "Business", new { area = "Chief" });
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(IdentityUserViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                IdentityUserModel identityUser = new IdentityUserModel { Username = model.Username, PasswordHash = model.Password };
                string result = _login.IsValid(identityUser);
                if (result == LoginResultConst.UserNotFound)
                    ModelState.AddModelError(string.Empty, "We did not find the associated user you are trying to login.");
                else if (result == LoginResultConst.UserNotMatch)
                    ModelState.AddModelError(string.Empty, "Username and Password does not match in our system.");
                else if (result == LoginResultConst.UserMatch)
                {
                    FormsAuthentication.SetAuthCookie(model.Username, model.IsPersistent);

                    SaveUserdataIntoCookies(model.Username);

                    //var claimsIdentity = (ClaimsIdentity)User.Identity;
                    //claimsIdentity.AddClaim(new Claim("FullName", $"{userData.FirstName} {userData.LastName}"));
                    //claimsIdentity.AddClaim(new Claim("Image", (string.IsNullOrEmpty(userData.Image) == true ? "Default.png" : userData.Image)));

                    if (!string.IsNullOrEmpty(returnUrl))
                        return Redirect(returnUrl);
                    return RedirectToAction("Dashboard", "Business", new { area = "Chief" });
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            //Remove all keys within tempdata
            //foreach (var key in TempData.Keys.ToList())
            //{
            //    TempData.Remove(key);
            //}
            HttpContext.Response.Cookies.Remove(CookieNames.LoginUserFullname);
            HttpContext.Response.Cookies.Remove(CookieNames.LoginUserImage);
            HttpContext.Response.Cookies.Remove(CookieNames.InvoiceServices);
            HttpContext.Response.Cookies.Remove(CookieNames.InvoiceServices);

            FormsAuthentication.SignOut();

            // BUG: Page.User.Identity.IsAuthenticated still true after FormsAuthentication.SignOut()
            HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

            return View();
        }

        private void SaveUserdataIntoCookies(string username)
        {
            // USER DATA
            IdentityUserModel userData = _user.GetByUsername(username);

            HttpCookie LoginUserFullname = new HttpCookie(CookieNames.LoginUserFullname, $"{userData.FirstName} {userData.LastName}");
            LoginUserFullname.Expires.AddDays(30);
            HttpContext.Response.Cookies.Add(LoginUserFullname);

            HttpCookie LoginUserImage = new HttpCookie(CookieNames.LoginUserImage, "/" + _FolderPath.UserLogo + (string.IsNullOrEmpty(userData.Image) == true ? "Default.png" : userData.Image));
            LoginUserImage.Expires.AddDays(30);
            HttpContext.Response.Cookies.Add(LoginUserImage);

            // SELECTED BUSINESS DATA
            BusinessModel business = _business.GetSelectedByUsername(username);

            HttpCookie SelectedBusinessId = new HttpCookie(CookieNames.SelectedBusinessId, business.Id.ToString());
            SelectedBusinessId.Expires.AddDays(30);
            HttpContext.Response.Cookies.Add(SelectedBusinessId);

            HttpCookie SelectedBusinessName = new HttpCookie(CookieNames.SelectedBusinessName, business.Name);
            SelectedBusinessName.Expires.AddDays(30);
            HttpContext.Response.Cookies.Add(SelectedBusinessName);
        }
    }
}