using Paybook.ServiceLayer.Models.Invoices;
using System.Collections.Generic;

namespace Paybook.BusinessLayer.Abstracts.Invoices
{
    public interface IInvoiceServiceProcessor
    {
        List<InvoiceServiceModel> GetAllByInvoiceId(string username, int invoiceId);
        InvoiceServiceModel GetById(int businessId, int id);
        bool IsExist(string businessId, int id);
    }
}
