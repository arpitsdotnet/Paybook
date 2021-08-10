using Paybook.BusinessLayer.Business;
using Paybook.BusinessLayer.Common;
using Paybook.BusinessLayer.Identity;
using Paybook.BusinessLayer.Setting;
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
    //[Authorize]    
    public class BusinessController : Controller
    {
        private readonly ILogger _logger;
        private readonly IDashboardProcessor _dashboard;
        private readonly IBusinessProcessor _business;
        private readonly IUserProcessor _user;
        private readonly ICountryProcessor _country;
        private readonly IStateProcessor _state;

        public BusinessController(ILogger logger, IDashboardProcessor dashboard, IBusinessProcessor business, IUserProcessor user, ICountryProcessor country, IStateProcessor state)
        {
            _logger = logger;
            _dashboard = dashboard;
            _business = business;
            _user = user;
            _country = country;
            _state = state;
        }

        public ActionResult Index()
        {
            List<BusinessModel> businesses = _business.GetAllByUsername(User.Identity.Name);
            return View(businesses);
        }

        public ActionResult Create()
        {
            var modelVM = new BusinessViewModel
            {
                Countries = GetSelectListItemsCountry(_country.GetAllByPage(0, "", "")),
                States = GetSelectListItemsState(_state.GetAllByPage(1, 0, "", ""))
            };

            return View(modelVM);
        }

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

        [HttpPost, ActionName("Create")]
        public ActionResult CreatePost(BusinessModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessModel output = _business.Create(model);
                return View(output);
            }
            return View(model);
        }

        private BusinessModel GetSelectedBusiness()
        {
            if (TempData.Peek("SelectedBusiness") == null)
            {
                TempData.Add("SelectedBusiness", _business.GetSelectedByUsername(User.Identity.Name));
            }

            return (BusinessModel)TempData.Peek("SelectedBusiness");
        }

        public ActionResult Dashboard()
        {
            BusinessModel business = GetSelectedBusiness();

            if (business == null)
            {
                return RedirectToAction(nameof(Create));
            }

            DashboardCountersModel model = _dashboard.GetAllCounters(business.Id);


            DashboardCounterWidgetModel counterInvoicesOpen = new DashboardCounterWidgetModel
            {
                BgColorClass = "fwt-blue-grey",
                BgColorHoverClass = "fwt-hover-grey",
                RsSymbolColor = "color: #4d5f68;",
                CountText = "Open Invoices",
                Count = model.CountTotalOpenInvoice,
                Total = model.SumofTotalOpenInvoice
            };
            DashboardCounterWidgetModel counterInvoicesOpenLastWeek = new DashboardCounterWidgetModel
            {
                BgColorClass = "fwt-deep-purple",
                BgColorHoverClass = "fwt-hover-grey",
                RsSymbolColor = "color: #562f9A;",
                CountText = "Open Invoices (Week)",
                Count = model.CountLastWeekOpenInvoice,
                Total = model.SumLastWeekOpenInvoice
            };
            DashboardCounterWidgetModel counterInvoicesOverdue = new DashboardCounterWidgetModel
            {
                BgColorClass = "fwt-red",
                BgColorHoverClass = "fwt-hover-grey",
                RsSymbolColor = "color: #bd4339;",
                CountText = "Overdues",
                Count = model.CountOfOverdue,
                Total = model.SumOfOverdue
            };
            DashboardCounterWidgetModel counterPaymentPaidPartial = new DashboardCounterWidgetModel
            {
                BgColorClass = "fwt-teal",
                BgColorHoverClass = "fwt-hover-grey",
                RsSymbolColor = "color: #007469;",
                CountText = "Payments (Month)",
                Count = model.CountOfPaidPartial,
                Total = model.SumOfPaidPartialAmount
            };
            DashboardCounterWidgetModel counterPaymentPaidLastMonth = new DashboardCounterWidgetModel
            {
                BgColorClass = "fwt-blue",
                BgColorHoverClass = "fwt-hover-grey",
                RsSymbolColor = "color: #1b76be;",
                CountText = "Paid Invoice (Month)",
                Count = model.CountOfPaidAmount,
                Total = model.SumOfPaidAmount
            };
            DashboardCounterWidgetModel counterPaymentTotal = new DashboardCounterWidgetModel
            {
                BgColorClass = "fwt-green",
                BgColorHoverClass = "fwt-hover-grey",
                RsSymbolColor = "color: #2d8630;",
                CountText = "Total Revenue",
                Count = model.CountOfPaymentTotal,
                Total = model.SumOfPaymentTotal
            };

            //_business.GetByUserId()
            DashboardViewModel dashboardVM = new DashboardViewModel
            {
                CounterInvoicesOpen = counterInvoicesOpen,
                CounterInvoicesOpenLastWeek = counterInvoicesOpenLastWeek,
                CounterInvoicesOverdue = counterInvoicesOverdue,
                CounterPaymentPaidPartial = counterPaymentPaidPartial,
                CounterPaymentPaidLastMonth = counterPaymentPaidLastMonth,
                CounterPaymentTotal = counterPaymentTotal,
            };

            return View();
        }
    }
}