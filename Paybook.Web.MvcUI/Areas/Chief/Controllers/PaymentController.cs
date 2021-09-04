using Paybook.BusinessLayer.Client;
using Paybook.BusinessLayer.Common;
using Paybook.BusinessLayer.Invoice;
using Paybook.BusinessLayer.Payment;
using Paybook.BusinessLayer.Setting;
using Paybook.ServiceLayer.Constants;
using Paybook.ServiceLayer.Models;
using Paybook.Web.MvcUI.Models.ViewModels;
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
        private readonly IPaymentProcessor _payment;
        private readonly IClientProcessor _client;
        private readonly IStateProcessor _stateMaster;
        private readonly ICountryProcessor _countryMaster;
        private readonly IActivityProcessor _activity;

        public PaymentController(IPaymentProcessor payment,
                                    IClientProcessor client,
                                    IStateProcessor stateMaster,
                                    ICountryProcessor countryMaster,
                                    IActivityProcessor activity)
        {
            _payment = payment;
            _client = client;
            _stateMaster = stateMaster;
            _countryMaster = countryMaster;
            _activity = activity;
        }

        private int BusinessId { get { return Convert.ToInt32(Request.Cookies[CookieNames.SelectedBusinessId].Value); } }


        [HttpGet]
        public ActionResult Index()
        {
            List<PaymentModel> model = _payment.GetAllByPage(BusinessId, 0, "", "");

            foreach (var item in model)
            {
                item.Client = _client.GetById(BusinessId, item.ClientId);
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
                client = _client.GetById(BusinessId, id.Value);
                client.StateMaster = _stateMaster.GetById(client.StateId);
                client.CountryMaster = _countryMaster.GetById(client.CountryId);
                payment.ClientId = id.Value;
                balanceTotal = _client.GetBalanceTotalById(BusinessId, id.Value);
            }

            var clients = _client.GetAllByPage(BusinessId, 0, "", "");

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
            var client = _client.GetById(BusinessId, modelVM.Payment.ClientId);
            client.StateMaster = _stateMaster.GetById(client.StateId);
            client.CountryMaster = _countryMaster.GetById(client.CountryId);

            var clients = _client.GetAllByPage(BusinessId, 0, "", "");

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
                output = _payment.Create(modelVM.Payment);

                if (output.IsSucceeded == true)
                {
                    _activity.Create(new ActivityBuilderModel
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