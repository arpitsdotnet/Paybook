using Paybook.ServiceLayer.Models.Invoices;
using Paybook.ServiceLayer.Paging;
using System.Collections.Generic;

namespace Paybook.ServiceLayer.Models.ViewModels
{
    public class InvoicePagingViewModel
    {
        public IEnumerable<InvoiceModel> Invoices { get; set; }
        public PagingEntity Paging { get; set; }
    }
}