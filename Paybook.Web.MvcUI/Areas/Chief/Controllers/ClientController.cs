using Paybook.BusinessLayer.Client;
using Paybook.BusinessLayer.Common;
using Paybook.BusinessLayer.Identity;
using Paybook.BusinessLayer.Invoice;
using Paybook.BusinessLayer.Payment;
using Paybook.BusinessLayer.Setting;
using Paybook.ServiceLayer.Constants;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.Web.MvcUI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paybook.Web.MvcUI.Areas.Chief.Controllers
{
    [RouteArea("Chief")]
    [Authorize]
    public class ClientController : Controller
    {
        private readonly IClientProcessor _client;
        private readonly IInvoiceProcessor _invoice;
        private readonly IPaymentProcessor _payment;
        private readonly IInvoicePayProcessor _invoicePay;
        private readonly ICategoryProcessor _category;
        private readonly ICountryProcessor _country;
        private readonly IStateProcessor _state;
        private readonly IActivityProcessor _activity;

        public ClientController(IClientProcessor client,
                                    IInvoiceProcessor invoice,
                                    IPaymentProcessor payment,
                                    IInvoicePayProcessor invoicePay,
                                    ICategoryProcessor category,
                                    ICountryProcessor country,
                                    IStateProcessor state,
                                    IActivityProcessor activity)
        {
            _client = client;
            _invoice = invoice;
            _payment = payment;
            _invoicePay = invoicePay;
            _category = category;
            _country = country;
            _state = state;
            _activity = activity;
        }

        private int BusinessId { get { return Convert.ToInt32(Request.Cookies[CookieNames.SelectedBusinessId].Value); } }


        [HttpGet]
        public ActionResult Index()
        {
            List<ClientModel> model = _client.GetAllByPage(BusinessId, 0, "", "");

            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var client = _client.GetById(BusinessId, id);
            client.StateMaster = _state.GetById(client.StateId);
            client.CountryMaster = _country.GetById(client.CountryId);

            var invoices = _invoice.GetAllByClientId(BusinessId, id);
            foreach (var item in invoices)
            {
                item.StatusCategoryMaster = _category.GetById(BusinessId, item.StatusId);
                item.PaidTotal = _invoicePay.GetPaidTotalByInvoiceId(BusinessId, item.Id);
            }
            var payments = _payment.GetAllByClientId(BusinessId, id);

            var clientCounters = _client.GetCountersById(BusinessId, id);

            var clientVM = new ClientViewModel
            {
                Client = client,
                Invoices = invoices,
                Payments = payments,
                Counters = clientCounters
            };

            return View(clientVM);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var countries = _country.GetAllByPage(0, "", "");
            int countryId = countries[0].Id;
            var clientVM = new ClientViewModel
            {
                Countries = GetSelectListItemsCountry(countries),
                States = GetSelectListItemsState(_state.GetAllByPage(countryId, 0, "", ""))
            };

            return View(clientVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(ClientViewModel modelVM)
        {
            var countries = _country.GetAllByPage(0, "", "");
            int countryId = countries[0].Id;
            var clientVM = new ClientViewModel
            {
                Client = modelVM.Client,
                Countries = GetSelectListItemsCountry(countries),
                States = GetSelectListItemsState(_state.GetAllByPage(countryId, 0, "", ""))
            };

            if (ModelState.IsValid)
            {
                clientVM.Client.BusinessId = BusinessId;
                clientVM.Client.CreateBy = User.Identity.Name;

                ClientModel output = new ClientModel();
                output = _client.Create(modelVM.Client);

                if (output.IsSucceeded == true)
                {
                    _activity.Create(new ActivityBuilderModel
                    {
                        BusinessId = BusinessId,
                        CreateBy = User.Identity.Name,
                        Title = ActivityConst.TitleCreated,
                        TitleCss = ActivityConst.CssSuccess,
                        Date = DateTime.Now.ToString("dd/MMM/yyyy"),
                        Type = ActivityConst.TypeClient,
                        ClientName = clientVM.Client.Name
                    });

                    return RedirectToAction(nameof(Index));
                }

                clientVM.Client = output;
                return View(clientVM);
            }

            return View(clientVM);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var clientData = _client.GetById(BusinessId, id);

            var countries = _country.GetAllByPage(0, "", "");
            int countryId = countries[0].Id;
            var clientVM = new ClientViewModel
            {
                Client = clientData,
                Countries = GetSelectListItemsCountry(countries),
                States = GetSelectListItemsState(_state.GetAllByPage(countryId, 0, "", ""))
            };

            return View(clientVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(ClientViewModel model)
        {
            var countries = _country.GetAllByPage(0, "", "");
            int countryId = countries[0].Id;
            var clientVM = new ClientViewModel
            {
                Countries = GetSelectListItemsCountry(countries),
                States = GetSelectListItemsState(_state.GetAllByPage(countryId, 0, "", ""))
            };

            if (ModelState.IsValid)
            {
                model.Client.ModifyBy = User.Identity.Name;

                ClientModel output = _client.Update(model.Client);
                output.Name = model.Client.Name;
                clientVM.Client = output;

                return View(clientVM);
            }

            clientVM.Client = model.Client;

            return View(clientVM);
        }



        [HttpGet]
        public ActionResult GetClientById(int id)
        {
            ClientModel client = _client.GetById(BusinessId, id);
            client.StateMaster = _state.GetById(client.StateId);
            client.CountryMaster = _country.GetById(client.CountryId);

            ClientViewModel clientVM = new ClientViewModel
            {
                Client = client,
                BalanceTotal = _client.GetBalanceTotalById(BusinessId, id)
            };

            return Json(clientVM, JsonRequestBehavior.AllowGet);
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