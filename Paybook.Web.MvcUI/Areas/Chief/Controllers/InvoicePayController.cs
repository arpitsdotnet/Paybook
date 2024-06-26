﻿using Paybook.BusinessLayer.Client;
using Paybook.BusinessLayer.Common;
using Paybook.BusinessLayer.Invoice;
using Paybook.ServiceLayer.Constants;
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
    public class InvoicePayController : Controller
    {
        private readonly IInvoicePayProcessor _invoicePay;
        private readonly IInvoiceProcessor _invoice;
        private readonly IClientProcessor _client;
        private readonly IActivityProcessor _activity;

        public InvoicePayController(IInvoicePayProcessor invoicePay,
            IInvoiceProcessor invoice,
            IClientProcessor client,
            IActivityProcessor activity)
        {
            _invoicePay = invoicePay;
            _invoice = invoice;
            _client = client;
            _activity = activity;
        }

        private int BusinessId { get { return Convert.ToInt32(Request.Cookies[CookieNames.SelectedBusinessId].Value); } }


        [HttpGet]
        public ActionResult Create(int id)
        {
            var invoicePay = new InvoicePayModel();
            invoicePay.InvoiceId = id;

            var invoice = _invoice.GetById(BusinessId, id);
            var client = _client.GetById(BusinessId, invoice.ClientId);
            var paidTotal = _invoicePay.GetPaidTotalByInvoiceId(BusinessId, invoice.Id);
            var clientBalance = _client.GetBalanceTotalById(BusinessId, invoice.ClientId);

            var invpayVM = new InvoicePayViewModel
            {
                InvoicePay = invoicePay,
                InvoiceNumber = invoice.InvoiceNumber,
                InvoiceTotal = invoice.Total - paidTotal,
                ClientName = client.Name,
                Balance = clientBalance,
            };

            invpayVM.InvoicePay.PayDate = DateTime.Now;
            invpayVM.InvoicePay.PayAmount = invpayVM.InvoiceTotal > clientBalance ? clientBalance : invpayVM.InvoiceTotal;

            return PartialView("_InvoicePaymentFormPartial", invpayVM);
        }

        [HttpPost, ActionName("Create")]
        public ActionResult CreatePost(InvoicePayViewModel modelVM)
        {

            if (modelVM.Balance <= 0)
                ModelState.AddModelError("", "You must first add payment received from the client.");

            if (modelVM.InvoicePay.PayAmount <= 0)
                ModelState.AddModelError("", "Payment amount cannot be negative or 0.");

            // Deposite amount can be greater then client balance total
            if (modelVM.InvoicePay.PayAmount > modelVM.Balance)
                ModelState.AddModelError("", "Payment amount cannot be greater then balance total.");

            // Deposite amount can be greater then invoice amount
            if (modelVM.InvoicePay.PayAmount > modelVM.InvoiceTotal)
                ModelState.AddModelError("", "Payment amount cannot be greater then invoice total.");

            if (ModelState.IsValid)
            {
                modelVM.InvoicePay.BusinessId = BusinessId;
                modelVM.InvoicePay.CreateBy = User.Identity.Name;

                var output = new InvoicePayModel();
                output = _invoicePay.Create(modelVM.InvoicePay);

                if (output.IsSucceeded == true)
                {
                    _activity.Create(new ActivityBuilderModel
                    {
                        BusinessId = BusinessId,
                        CreateBy = User.Identity.Name,
                        Title = ActivityConst.TitlePaid,
                        TitleCss = ActivityConst.CssSuccess,
                        Date = modelVM.InvoicePay.PayDate.GetValueOrDefault().ToString("dd/MMM/yyyy"),
                        Type = ActivityConst.TypeInvoice,
                        TypeNumber = modelVM.InvoiceNumber,
                        ClientName = modelVM.ClientName,
                        Amount = modelVM.InvoicePay.PayAmount.ToString("0.00")
                    });

                    modelVM.InvoicePay = output;
                    return PartialView("_InvoicePaymentFormPartial", modelVM);
                }

                modelVM.InvoicePay = output;
                return PartialView("_InvoicePaymentFormPartial", modelVM);
            }

            return PartialView("_InvoicePaymentFormPartial", modelVM);
        }
    }
}