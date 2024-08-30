using Paybook.BusinessLayer.Abstracts.Admins;
using Paybook.BusinessLayer.Abstracts.Customers;
using Paybook.BusinessLayer.Abstracts.Outbox;
using Paybook.BusinessLayer.Abstracts.Payments;
using Paybook.BusinessLayer.Invoice;
using Paybook.ServiceLayer.Constants;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Models.Activities;
using Paybook.ServiceLayer.Models.Clients;
using Paybook.ServiceLayer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Paybook.Web.MvcUI.Areas.Chief.Controllers
{
    [RouteArea("Chief")]
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IPaymentProcessor _paymentProcessor;
        private readonly IClientProcessor _clientProcessor;
        private readonly IStateProcessor _stateProcessor;
        private readonly ICountryProcessor _countryProcessor;
        private readonly IActivityProcessor _activityProcessor;

        public PaymentController(
            IPaymentProcessor payment,
            IClientProcessor client,
            IStateProcessor stateMaster,
            ICountryProcessor countryMaster,
            IActivityProcessor activity)
        {
            _paymentProcessor = payment;
            _clientProcessor = client;
            _stateProcessor = stateMaster;
            _countryProcessor = countryMaster;
            _activityProcessor = activity;
        }

        private int BusinessId { get { return Convert.ToInt32(Request.Cookies[CookieNames.SelectedBusinessId].Value); } }


        [HttpGet]
        public ActionResult Index()
        {
            List<PaymentModel> model = _paymentProcessor.GetAllByPage(BusinessId, 0, "", "");

            foreach (var item in model)
            {
                item.Client = _clientProcessor.GetById(BusinessId, item.ClientId);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Create(int? id)
        {
            //var invoices = _invoice.GetAllByPage(User.Identity.Name, 0, "", "");

            var client = new ClientModel();
            var payment = new PaymentModel();
            decimal balanceTotal = 0;
            if (id != null)
            {
                client = _clientProcessor.GetById(BusinessId, id.Value);
                client.StateMaster = _stateProcessor.GetById(client.StateId);
                client.CountryMaster = _countryProcessor.GetById(client.CountryId);
                payment.ClientId = id.Value;
                balanceTotal = _clientProcessor.GetBalanceTotalById(BusinessId, id.Value);
            }

            var clients = _clientProcessor.GetAllByPage(BusinessId, 0, "", "");

            var paymentVM = new PaymentViewModel
            {
                Clients = GetSelectListItemsClient(clients),
                Client = client,
                Payment = payment,
                BalanceTotal = balanceTotal
            };

            payment.Amount = 0;
            payment.PaymentDate = DateTime.Now;

            return View(paymentVM);
        }

        [HttpPost, ActionName("Create")]
        public ActionResult CreatePost(PaymentViewModel modelVM)
        {
            var client = _clientProcessor.GetById(BusinessId, modelVM.Payment.ClientId);
            client.StateMaster = _stateProcessor.GetById(client.StateId);
            client.CountryMaster = _countryProcessor.GetById(client.CountryId);

            var clients = _clientProcessor.GetAllByPage(BusinessId, 0, "", "");

            if (modelVM.Payment.Amount <= 0)
                ModelState.AddModelError("", "Payment amount cannot be negative or 0.");

            // Deposite amount can be greater then invoice amount . Comment below
            //if (modelVM.Payment.Amount > (invoice.Total - paidTotal))
            //    ModelState.AddModelError("", "Payment amount cannot be greater then Remaining invoice total.");

            var paymentVM = new PaymentViewModel
            {
                Clients = GetSelectListItemsClient(clients),
                Client = client,
                Payment = modelVM.Payment,
            };

            if (ModelState.IsValid)
            {
                modelVM.Payment.BusinessId = BusinessId;
                modelVM.Payment.CreateBy = User.Identity.Name;
                modelVM.Payment.Method = "Cash";

                var output = new PaymentModel();
                output = _paymentProcessor.Create(modelVM.Payment);

                if (output.IsSucceeded == true)
                {
                    _activityProcessor.Create(new ActivityBuilderModel
                    {
                        BusinessId = BusinessId,
                        CreateBy = User.Identity.Name,
                        Title = ActivityConst.TitleCreated,
                        TitleCss = ActivityConst.CssSuccess,
                        Date = DateTime.Now.ToString("dd/MMM/yyyy"),
                        Type = ActivityConst.TypePayment,
                        ClientName = client.Name,
                        Amount = modelVM.Payment.Amount.ToString("0.00")
                    });

                    return RedirectToAction(nameof(Index));
                }

                return View(paymentVM);
            }

            return View(paymentVM);
        }

        //[HttpGet]
        //public ActionResult Revert(int id)
        //{
        //    _payment.Revert(User.Identity.Name, id);
        //}

        //[HttpPost, ActionName("Revert")]
        //public async Task<ActionResult> RevertPost(PaymentViewModel modelVM)
        //{

        //}


        [NonAction]
        private IEnumerable<SelectListItem> GetSelectListItemsClient(List<ClientModel> elements)
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