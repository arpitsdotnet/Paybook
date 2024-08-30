using Paybook.ServiceLayer.Models.Invoices;
using System.Collections.Generic;

namespace Paybook.DatabaseLayer.Abstracts.Invoices
{
    public interface IInvoicePayRepository
    {
        List<InvoicePayModel> GetAllByInvoiceId(int businessId, int invoiceId, int page, string search, string orderBy);
        decimal GetPaidTotalByInvoiceId(int businessId, int invoiceId);
        int Create(InvoicePayModel model);
    }
}
