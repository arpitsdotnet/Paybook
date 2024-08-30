using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Paybook.BusinessLayer.Abstracts.Admins;
using Paybook.BusinessLayer.Abstracts.Customers;
using Paybook.BusinessLayer.Abstracts.Identity;
using Paybook.BusinessLayer.Abstracts.Outbox;
using Paybook.ServiceLayer.Constants;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Models.Activities;
using Paybook.ServiceLayer.Models.Dashboards;
using Paybook.ServiceLayer.Models.Invoices;
using Paybook.ServiceLayer.Models.ViewModels;
using Paybook.ServiceLayer.Services;

namespace Paybook.Web.MvcUI.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [Authorize]
    public class BusinessController : Controller
    {
        private readonly ILogger _logger;
        private readonly IDashboardProcessor _dashboard;
        private readonly IBusinessProcessor _business;
        private readonly IUserProcessor _user;
        private readonly IClientProcessor _client;
        private readonly ICategoryProcessor _category;
        private readonly ICountryProcessor _country;
        private readonly IStateProcessor _state;
        private readonly IActivityProcessor _activity;

        public BusinessController(
            ILogger logger,
            IDashboardProcessor dashboard,
            IBusinessProcessor business,
            IUserProcessor user,
            IClientProcessor client,
            ICategoryProcessor category,
            ICountryProcessor country,
            IStateProcessor state,
            IActivityProcessor activity)
        {
            _logger = logger;
            _dashboard = dashboard;
            _business = business;
            _user = user;
            _client = client;
            _category = category;
            _country = country;
            _state = state;
            _activity = activity;
        }

        private int BusinessId { get { return Convert.ToInt32(Request.Cookies[CookieNames.SelectedBusinessId].Value); } }


        [HttpGet]
        public ActionResult Dashboard()
        {
            DashboardViewModel dashboardVM = _dashboard.GetAllCounters(User.Identity.Name);

            return View(dashboardVM);
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<BusinessModel> businesses = _dashboard.GetAllBusinesses(User.Identity.Name);

            return View(businesses);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var countries = _country.GetAllByPage(0, "", "");
            int countryId = countries[0].Id;
            var states = _state.GetAllByPage(countryId, 0, "", "");
            var businessVM = new BusinessViewModel
            {
                Countries = SelectListItemGenerator.GetCountryItemList(countries),
                States = SelectListItemGenerator.GetStateItemList(states)
            };

            return View(businessVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(BusinessViewModel model)
        {
            var countries = _country.GetAllByPage(0, "", "");
            int countryId = countries[0].Id;
            var states = _state.GetAllByPage(countryId, 0, "", "");
            var businessVM = new BusinessViewModel
            {
                Countries = SelectListItemGenerator.GetCountryItemList(countries),
                States = SelectListItemGenerator.GetStateItemList(states)
            };

            if (!ModelState.IsValid)
            {
                businessVM.Business = model.Business;

                return View(businessVM);
            }

            model.Business.CreateBy = User.Identity.Name;
            BusinessModel output = _business.Create(model.Business);

            businessVM.Business = output;

            return View(businessVM);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            string username = User.Identity.Name;
            BusinessModel businessData = _business.GetById(id, username);

            var countries = _country.GetAllByPage(0, "", "");
            int countryId = countries[0].Id;
            var businessVM = new BusinessViewModel
            {
                Business = businessData,
                Countries = SelectListItemGenerator.GetCountryItemList(countries),
                States = SelectListItemGenerator.GetStateItemList(_state.GetAllByPage(countryId, 0, "", ""))
            };

            return View(businessVM);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            string username = User.Identity.Name;
            BusinessModel businessData = _business.Activate(id, username, false);

            return RedirectToAction(nameof(Index), businessData);
        }

        [HttpPost, ActionName("Selected")]
        [ValidateAntiForgeryToken]
        public ActionResult SelectedPost(int id)
        {
            BusinessModel output = _business.UpdateSelected(id, User.Identity.Name);

            if (output == null || output.IsSucceeded != true)
            {
                ModelState.AddModelError(string.Empty, output.ReturnMessage);
                List<BusinessModel> businesses = new List<BusinessModel>
                {
                    output
                };
                return View(nameof(Index), businesses);
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpGet, AllowAnonymous]
        public ActionResult GetCountOfInvoicesAndPaymentsByLastWeek(string username)
        {
            List<DashboardInvoiceChartModel> model = _dashboard.GetCountOfInvoicesAndPaymentsByLastWeek(username);
            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet, AllowAnonymous]
        public ActionResult GetInvoiceAmountsAndPaymentsByLastWeek(string username)
        {
            List<DashboardInvoiceChartModel> model = _dashboard.GetInvoiceAmountsAndPaymentsByDays(username, 7);
            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet, AllowAnonymous]
        public ActionResult GetPaymentsLast10(string username)
        {
            List<DashboardInvoiceChartModel> model = _dashboard.GetPaymentsLast10(username);
            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet, AllowAnonymous]
        public ActionResult GetClientCountByLastWeek(string username)
        {
            List<DashboardClientChartModel> model = _dashboard.GetClientCountByDays(username);
            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet, AllowAnonymous]
        public ActionResult GetLast5Invoices(string username)
        {
            var business = _business.GetSelectedByUsername(username);

            List<InvoiceModel> model = _dashboard.GetLast5Invoices(username);
            foreach (var item in model)
            {
                item.Client = _client.GetById(business.Id, item.ClientId);
                item.StatusCategoryMaster = _category.GetById(business.Id, item.StatusId);
            }
            return PartialView("_DashboardInvoiceTablePartial", model);
        }

        [HttpGet, AllowAnonymous]
        public ActionResult GetLast5Payments(string username)
        {
            var business = _business.GetSelectedByUsername(username);

            List<PaymentModel> model = _dashboard.GetLast5Payments(username);
            foreach (var item in model)
            {
                item.Client = _client.GetById(business.Id, item.ClientId);
            }
            return PartialView("_DashboardPaymentTablePartial", model);
        }


        [HttpGet, AllowAnonymous]
        public ActionResult GetAllActivities(string username)
        {
            var business = _business.GetSelectedByUsername(username);

            List<ActivityModel> model = _activity.GetAllByPage(business.Id, 0, "", "");

            return PartialView("_DashboardActivityTablePartial", model);
        }


    }
}