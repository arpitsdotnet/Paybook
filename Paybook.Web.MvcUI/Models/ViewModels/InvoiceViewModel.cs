using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paybook.Web.MvcUI.Models.ViewModels
{
    public class InvoiceViewModel
    {
        public InvoiceModel Invoice { get; set; }
        public List<InvoiceServiceModel> InvoiceServices { get; set; }
        public List<PaymentModel> InvoicePayments { get; set; }
        public IEnumerable<SelectListItem> Clients { get; set; }
        public IEnumerable<SelectListItem> Terms { get; set; }
        public IEnumerable<SelectListItem> DiscountTypes { get; set; }
    }
}