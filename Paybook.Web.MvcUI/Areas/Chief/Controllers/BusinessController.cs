using Paybook.BusinessLayer.Business;
using Paybook.BusinessLayer.Common;
using Paybook.BusinessLayer.Identity;
using Paybook.BusinessLayer.Setting;
using Paybook.ServiceLayer.Constants;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.Web.MvcUI.Models;
using Paybook.Web.MvcUI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Paybook.Web.MvcUI.Areas.Chief.Controllers
{
    [RouteArea("Chief")]
    [Authorize]
    public class BusinessController : Controller
    {
        private readonly IDashboardProcessor _dashboard;
        private readonly IBusinessProcessor _business;
        private readonly IUserProcessor _user;
        private readonly ICountryProcessor _country;
        private readonly IStateProcessor _state;

        public BusinessController(IDashboardProcessor dashboard, IBusinessProcessor business, IUserProcessor user, ICountryProcessor country, IStateProcessor state)
        {
            _dashboard = dashboard;
            _business = business;
            _user = user;
            _country = country;
            _state = state;
        }

        [HttpGet]
        public ActionResult Dashboard()
        {
            DashboardCountersModel model = _dashboard.GetAllCounters(User.Identity.Name);

            DashboardViewModel dashboardVM = new DashboardViewModel
            {
                CounterInvoicesOpen = new DashboardCounterWidgetModel
                {
                    BgColorClass = "fwt-blue-grey",
                    BgColorHoverClass = "fwt-hover-grey",
                    RsSymbolColor = "color: #4d5f68;",
                    CountText = "Open Invoices",
                    Count = model.CountTotalOpenInvoice,
                    Total = model.SumofTotalOpenInvoice
                },
                CounterInvoicesOpenLastWeek = new DashboardCounterWidgetModel
                {
                    BgColorClass = "fwt-deep-purple",
                    BgColorHoverClass = "fwt-hover-grey",
                    RsSymbolColor = "color: #562f9A;",
                    CountText = "Open Invoices (Week)",
                    Count = model.CountLastWeekOpenInvoice,
                    Total = model.SumLastWeekOpenInvoice
                },
                CounterInvoicesOverdue = new DashboardCounterWidgetModel
                {
                    BgColorClass = "fwt-red",
                    BgColorHoverClass = "fwt-hover-grey",
                    RsSymbolColor = "color: #bd4339;",
                    CountText = "Overdues",
                    Count = model.CountOfOverdue,
                    Total = model.SumOfOverdue
                },
                CounterPaymentPaidPartial = new DashboardCounterWidgetModel
                {
                    BgColorClass = "fwt-teal",
                    BgColorHoverClass = "fwt-hover-grey",
                    RsSymbolColor = "color: #007469;",
                    CountText = "Payments (Month)",
                    Count = model.CountOfPaidPartial,
                    Total = model.SumOfPaidPartialAmount
                },
                CounterPaymentPaidLastMonth = new DashboardCounterWidgetModel
                {
                    BgColorClass = "fwt-blue",
                    BgColorHoverClass = "fwt-hover-grey",
                    RsSymbolColor = "color: #1b76be;",
                    CountText = "Paid Invoice (Month)",
                    Count = model.CountOfPaidAmount,
                    Total = model.SumOfPaidAmount
                },
                CounterPaymentTotal = new DashboardCounterWidgetModel
                {
                    BgColorClass = "fwt-green",
                    BgColorHoverClass = "fwt-hover-grey",
                    RsSymbolColor = "color: #2d8630;",
                    CountText = "Total Revenue",
                    Count = model.CountOfPaymentTotal,
                    Total = model.SumOfPaymentTotal
                },

                ClientCounter = new DashboardCounterWidgetModel
                {
                    Count = model.CountofCustomers,
                    Total = model.SumOfPaymentTotal
                }
            };

            return View(dashboardVM);
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<BusinessModel> businesses = _business.GetAllByUsername(User.Identity.Name);
            foreach (var business in businesses)
            {
                business.CountryMaster = _country.GetById(business.CountryId);
                business.StateMaster = _state.GetById(business.StateId);
            }

            return View(businesses);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var countries = _country.GetAllByPage(0, "", "");
            int countryId = countries[0].Id;
            var businessVM = new BusinessViewModel
            {
                Countries = GetSelectListItemsCountry(countries),
                States = GetSelectListItemsState(_state.GetAllByPage(countryId, 0, "", ""))
            };

            return View(businessVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(BusinessViewModel model)
        {
            var countries = _country.GetAllByPage(0, "", "");
            int countryId = countries[0].Id;
            var businessVM = new BusinessViewModel
            {
                Countries = GetSelectListItemsCountry(countries),
                States = GetSelectListItemsState(_state.GetAllByPage(countryId, 0, "", ""))
            };

            if (ModelState.IsValid)
            {
                model.Business.CreateBy = User.Identity.Name;
                BusinessModel output = _business.Create(model.Business);

                businessVM.Business = output;

                return View(businessVM);
            }

            businessVM.Business = model.Business;

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
                Countries = GetSelectListItemsCountry(countries),
                States = GetSelectListItemsState(_state.GetAllByPage(countryId, 0, "", ""))
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

            if (output != null && output.IsSucceeded == true)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, output.ReturnMessage);
            List<BusinessModel> businesses = new List<BusinessModel>
            {
                output
            };
            return View(nameof(Index), businesses);
        }


        [HttpGet, AllowAnonymous]
        public ActionResult GetCountOfInvoicesAndPaymentsByLastWeek(string username)
        {
            IDashboardProcessor dashboard = new DashboardProcessor();
            List<DashboardInvoiceChartModel> model = dashboard.GetCountOfInvoicesAndPaymentsByLastWeek(username);
            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet, AllowAnonymous]
        public ActionResult GetInvoiceAmountsAndPaymentsByLastWeek(string username)
        {
            IDashboardProcessor dashboard = new DashboardProcessor();
            List<DashboardInvoiceChartModel> model = dashboard.GetInvoiceAmountsAndPaymentsByDays(username, 7);
            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet, AllowAnonymous]
        public ActionResult GetPaymentsLast10(string username)
        {
            IDashboardProcessor dashboard = new DashboardProcessor();
            List<DashboardInvoiceChartModel> model = dashboard.GetPaymentsLast10(username);
            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet,AllowAnonymous]
        public ActionResult GetClientCountByLastWeek(string username)
        {
            IDashboardProcessor dashboard = new DashboardProcessor();
            List<DashboardClientChartModel> model = dashboard.GetClientCountByDays(username);
            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
        }


        [NonAction]
        private IEnumerable<SelectListItem> GetSelectListItemsState(List<StateMasterModel> elements)
        {
            var selectList = new List<SelectListItem>();
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.Id.ToString(),
                    Text = element.Name
                });
            }
            return selectList;
        }
        [NonAction]
        private IEnumerable<SelectListItem> GetSelectListItemsCountry(List<CountryMasterModel> elements)
        {
            var selectList = new List<SelectListItem>();
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.Id.ToString(),
                    Text = element.Name
                });
            }
            return selectList;
        }
    }
}