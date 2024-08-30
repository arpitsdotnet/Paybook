using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Paybook.BusinessLayer.Abstracts.Admins;
using Paybook.BusinessLayer.Abstracts.Customers;
using Paybook.BusinessLayer.Abstracts.Invoices;
using Paybook.BusinessLayer.Abstracts.Outbox;
using Paybook.BusinessLayer.Abstracts.Payments;
using Paybook.ServiceLayer.Constants;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Models.Activities;
using Paybook.ServiceLayer.Models.Clients;
using Paybook.ServiceLayer.Models.ViewModels;

namespace Paybook.Web.MvcUI.Areas.Chief.Controllers
{
    [RouteArea("Chief")]
    [Authorize]
    public class ClientController : Controller
    {
        private readonly IClientProcessor _clientProcessor;
        private readonly IInvoiceProcessor _invoiceProcessor;
        private readonly IPaymentProcessor _paymentProcessor;
        private readonly IInvoicePayProcessor _invoicePayProcessor;
        private readonly ICategoryProcessor _categoryProcessor;
        private readonly ICountryProcessor _countryProcessor;
        private readonly IStateProcessor _stateProcessor;
        private readonly IActivityProcessor _activityProcessor;

        public ClientController(
            IClientProcessor clientProcessor,
            IInvoiceProcessor invoiceProcessor,
            IPaymentProcessor paymentProcessor,
            IInvoicePayProcessor invoicePayProcessor,
            ICategoryProcessor categoryProcessor,
            ICountryProcessor countryProcessor,
            IStateProcessor stateProcessor,
            IActivityProcessor activityProcessor)
        {
            _clientProcessor = clientProcessor;
            _invoiceProcessor = invoiceProcessor;
            _paymentProcessor = paymentProcessor;
            _invoicePayProcessor = invoicePayProcessor;
            _categoryProcessor = categoryProcessor;
            _countryProcessor = countryProcessor;
            _stateProcessor = stateProcessor;
            _activityProcessor = activityProcessor;
        }

        private int BusinessId { get { return Convert.ToInt32(Request.Cookies[CookieNames.SelectedBusinessId].Value); } }


        [HttpGet]
        public ActionResult Index()
        {
            List<ClientModel> model = _clientProcessor.GetAllByPage(BusinessId, 0, "", "");

            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var client = _clientProcessor.GetById(BusinessId, id);
            client.StateMaster = _stateProcessor.GetById(client.StateId);
            client.CountryMaster = _countryProcessor.GetById(client.CountryId);

            var invoices = _invoiceProcessor.GetAllByClientId(BusinessId, id);
            foreach (var item in invoices)
            {
                item.StatusCategoryMaster = _categoryProcessor.GetById(BusinessId, item.StatusId);
                item.PaidTotal = _invoicePayProcessor.GetPaidTotalByInvoiceId(BusinessId, item.Id);
            }
            var payments = _paymentProcessor.GetAllByClientId(BusinessId, id);

            var clientCounters = _clientProcessor.GetCountersById(BusinessId, id);

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
            var countries = _countryProcessor.GetAllByPage(0, "", "");
            int countryId = countries[0].Id;
            var clientVM = new ClientViewModel
            {
                Countries = GetSelectListItemsCountry(countries),
                States = GetSelectListItemsState(_stateProcessor.GetAllByPage(countryId, 0, "", ""))
            };

            return View(clientVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(ClientViewModel modelVM)
        {
            var countries = _countryProcessor.GetAllByPage(0, "", "");
            int countryId = countries[0].Id;
            var clientVM = new ClientViewModel
            {
                Client = modelVM.Client,
                Countries = GetSelectListItemsCountry(countries),
                States = GetSelectListItemsState(_stateProcessor.GetAllByPage(countryId, 0, "", ""))
            };

            if (ModelState.IsValid)
            {
                clientVM.Client.BusinessId = BusinessId;
                clientVM.Client.CreateBy = User.Identity.Name;

                ClientModel output = new ClientModel();
                output = _clientProcessor.Create(modelVM.Client);

                if (output.IsSucceeded == true)
                {
                    _activityProcessor.Create(new ActivityBuilderModel
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
            var clientData = _clientProcessor.GetById(BusinessId, id);

            var countries = _countryProcessor.GetAllByPage(0, "", "");
            int countryId = countries[0].Id;
            var clientVM = new ClientViewModel
            {
                Client = clientData,
                Countries = GetSelectListItemsCountry(countries),
                States = GetSelectListItemsState(_stateProcessor.GetAllByPage(countryId, 0, "", ""))
            };

            return View(clientVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(ClientViewModel model)
        {
            var countries = _countryProcessor.GetAllByPage(0, "", "");
            int countryId = countries[0].Id;
            var clientVM = new ClientViewModel
            {
                Countries = GetSelectListItemsCountry(countries),
                States = GetSelectListItemsState(_stateProcessor.GetAllByPage(countryId, 0, "", ""))
            };

            if (ModelState.IsValid)
            {
                model.Client.ModifyBy = User.Identity.Name;

                ClientModel output = _clientProcessor.Update(model.Client);
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
            ClientModel client = _clientProcessor.GetById(BusinessId, id);
            client.StateMaster = _stateProcessor.GetById(client.StateId);
            client.CountryMaster = _countryProcessor.GetById(client.CountryId);

            ClientViewModel clientVM = new ClientViewModel
            {
                Client = client,
                BalanceTotal = _clientProcessor.GetBalanceTotalById(BusinessId, id)
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