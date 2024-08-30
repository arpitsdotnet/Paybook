using Paybook.ServiceLayer.Models.Invoices;
using System.Collections.Generic;

namespace Paybook.BusinessLayer.Abstracts.Invoices
{
    public interface IInvoicePayProcessor
    {
        List<InvoicePayModel> GetAllByInvoiceId(int businessId, int invoiceId, int page, string search, string orderBy);
        decimal GetPaidTotalByInvoiceId(int businessId, int invoiceId);
        InvoicePayModel Create(InvoicePayModel model);
    }
}
