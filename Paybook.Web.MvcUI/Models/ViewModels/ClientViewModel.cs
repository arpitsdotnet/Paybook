using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paybook.Web.MvcUI.Models.ViewModels
{
    public class ClientViewModel
    {
        public ClientModel Client { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
        public IEnumerable<SelectListItem> States { get; set; }
        public decimal OpenTotal { get; set; }
        public decimal OverdueTotal { get; set; }
        public decimal RemainingTotal { get; set; }
        public List<InvoiceModel> Invoices { get; set; }
        public List<PaymentModel> Payments { get; set; }
    }
}