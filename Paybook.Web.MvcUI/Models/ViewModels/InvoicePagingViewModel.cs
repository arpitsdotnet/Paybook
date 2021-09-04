using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paybook.Web.MvcUI.Models.ViewModels
{
    public class InvoicePagingViewModel
    {
        public IEnumerable<InvoiceModel> Invoices { get; set; }
        public PagingEntity Paging { get; set; }
    }
}