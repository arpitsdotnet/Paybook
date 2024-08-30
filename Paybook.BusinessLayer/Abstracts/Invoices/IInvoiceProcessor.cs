using Paybook.ServiceLayer.Models.Invoices;
using System.Collections.Generic;

namespace Paybook.BusinessLayer.Abstracts.Invoices
{
    public interface IInvoiceProcessor : IBaseProcessor<InvoiceModel>
    {
        InvoiceCountersModel GetAllCounters(int businessId);
        List<InvoiceModel> GetAllByClientId(int businessId, int clientId);
        string Invoices_Update_CloseStatus(string sParticular, string sCreatedBY, string sCategory_Core, string sStatus_Core, string sReason, string sCustomer_ID);
        string Activity_Insert_Overdue(string sCreatedBY, string sStatus_Core);
        InvoiceModel CreateWithServices(InvoiceModel invoice, List<InvoiceServiceModel> services);
        InvoiceModel UpdateVoid(int businessId, int invoiceId);
        InvoiceModel UpdateWriteOff(int businessId, int invoiceId);

        int GetAllPagesCount(int businessId, int page, string search, string orderBy);
    }
}
