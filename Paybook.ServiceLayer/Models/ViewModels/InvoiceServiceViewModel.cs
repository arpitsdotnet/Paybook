using System.Collections.Generic;
using System.Web.Mvc;
using Paybook.ServiceLayer.Models.Invoices;

namespace Paybook.ServiceLayer.Models.ViewModels
{
    public class InvoiceServiceViewModel
    {
        public InvoiceServiceModel Service { get; set; }
        public IEnumerable<SelectListItem> WorkTypes { get; set; }
        public IEnumerable<SelectListItem> TaxTypes { get; set; }
    }
}