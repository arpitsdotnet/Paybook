using System.Collections.Generic;
using System.Web.Mvc;
using Paybook.ServiceLayer.Models.Invoices;

namespace Paybook.ServiceLayer.Models.ViewModels
{
    public class InvoiceViewModel
    {
        public InvoiceModel Invoice { get; set; }
        public List<InvoiceServiceModel> InvoiceServices { get; set; }
        public List<InvoicePayModel> InvoicePayments { get; set; }
        public IEnumerable<SelectListItem> Clients { get; set; }
        public IEnumerable<SelectListItem> Terms { get; set; }
        public IEnumerable<SelectListItem> DiscountTypes { get; set; }
    }
}