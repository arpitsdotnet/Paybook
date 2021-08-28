﻿using Paybook.BusinessLayer.Client;
using Paybook.BusinessLayer.Invoice;
using Paybook.BusinessLayer.Payment;
using Paybook.BusinessLayer.Setting;
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
        private readonly IInvoiceProcessor _invoice;
        private readonly IClientProcessor _client;
        private readonly IStateProcessor _stateMaster;
        private readonly ICountryProcessor _countryMaster;

        public PaymentController(IPaymentProcessor payment,
                                    IInvoiceProcessor invoice,
                                    IClientProcessor client,
                                    IStateProcessor stateMaster,
                                    ICountryProcessor countryMaster)
        {
            _payment = payment;
            _invoice = invoice;
            _client = client;
            _stateMaster = stateMaster;
            _countryMaster = countryMaster;
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<PaymentModel> model = _payment.GetAllByPage(User.Identity.Name, 0, "", "");

            foreach (var item in model)
            {
                item.Invoice = _invoice.GetById(User.Identity.Name, item.InvoiceId);
                item.Invoice.Client = _client.GetById(User.Identity.Name, item.Invoice.ClientId);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Create(int? id)
        {
            //var invoices = _invoice.GetAllByPage(User.Identity.Name, 0, "", "");

            var invoice = new InvoiceModel();
            var client = new ClientModel();
            var payment = new PaymentModel();
            if (id != null)
            {
                invoice = _invoice.GetById(User.Identity.Name, id.Value);
                client = _client.GetById(User.Identity.Name, invoice.ClientId);
                client.StateMaster = _stateMaster.GetById(client.StateId);
                client.CountryMaster = _countryMaster.GetById(client.CountryId);
                payment.InvoiceId = id.Value;
            }

            var paymentVM = new PaymentViewModel
            {
                Client = client,
                Invoice = invoice,
                Payment = payment,
                //Invoices = GetSelectListItemsInvoice(invoices),
            };

            var paidTotal = _payment.GetPaidAmountByInvoiceId(User.Identity.Name, id.Value);
            payment.Amount = (invoice.Total - paidTotal);
            payment.PaymentDate = DateTime.Now;

            return View(paymentVM);
        }

        [HttpPost, ActionName("Create")]
        public ActionResult CreatePost(PaymentViewModel modelVM)
        {
            var invoice = _invoice.GetById(User.Identity.Name, modelVM.Payment.InvoiceId);
            var client = _client.GetById(User.Identity.Name, invoice.ClientId);
            client.StateMaster = _stateMaster.GetById(client.StateId);
            client.CountryMaster = _countryMaster.GetById(client.CountryId);
            var paidTotal = _payment.GetPaidAmountByInvoiceId(User.Identity.Name, modelVM.Payment.InvoiceId);

            if (modelVM.Payment.Amount <= 0)
                ModelState.AddModelError("", "Payment amount cannot be negative or 0.");

            if (modelVM.Payment.Amount > (invoice.Total - paidTotal))
                ModelState.AddModelError("", "Payment amount cannot be greater then Remaining invoice total.");

            var paymentVM = new PaymentViewModel
            {
                Invoice = invoice,
                Client = client,
                Payment = modelVM.Payment,
            };

            if (ModelState.IsValid)
            {
                modelVM.Payment.CreateBy = User.Identity.Name;
                modelVM.Payment.Method = "CASH";

                var output = new PaymentModel();
                output = _payment.Create(modelVM.Payment);

                if (output.IsSucceeded == true)
                    return RedirectToAction(nameof(Index));

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
        private IEnumerable<SelectListItem> GetSelectListItemsInvoice(List<InvoiceModel> elements)
        {
            var selectList = new List<SelectListItem>();
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.Id.ToString(),
                    Text = element.InvoiceNumber
                });
            }
            return selectList;
        }
    }
}