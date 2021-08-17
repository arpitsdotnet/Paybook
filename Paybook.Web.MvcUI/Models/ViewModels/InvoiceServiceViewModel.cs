using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paybook.Web.MvcUI.Models.ViewModels
{
    public class InvoiceServiceViewModel
    {
        public InvoiceServiceModel Service { get; set; }
        public IEnumerable<SelectListItem> WorkTypes { get; set; }
        public IEnumerable<SelectListItem> TaxTypes { get; set; }
    }
}