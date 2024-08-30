using System.Collections.Generic;
using System.Web.Mvc;
using Paybook.ServiceLayer.Models.Clients;
using Paybook.ServiceLayer.Models.Invoices;

namespace Paybook.ServiceLayer.Models.ViewModels
{
    public class ClientViewModel
    {
        public ClientModel Client { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
        public IEnumerable<SelectListItem> States { get; set; }
        public List<InvoiceModel> Invoices { get; set; }
        public List<PaymentModel> Payments { get; set; }
        public decimal BalanceTotal { get; set; }
        public ClientDetailsCountersModel Counters { get; set; }
    }
}