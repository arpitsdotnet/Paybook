using Paybook.ServiceLayer.Models.Invoices;
using System.Collections.Generic;
using System.Data;

namespace Paybook.DatabaseLayer.Abstracts.Invoices
{
    public interface IInvoiceRepository : IBaseRepository<InvoiceModel>
    {
        InvoiceCountersModel GetAllCounters(int businessId);
        List<InvoiceModel> GetAllByClientId(int businessId, int clientId);
        bool Invoices_Update_CloseStatus(string sParticular, string sCreatedBY, string sCategory_Core, string sStatus_Core, string sReason, string sCustomer_ID);
        bool Invoices_Update_OverdueStatus(string sInvoice_ID, string sCategory_Core, string sStatus_Core);
        bool CreateInvoiceActivity(string sCreatedBY, string sStatus_Core);
        DataTable GetByStatusOverdue();
        int CreateWithServices(InvoiceModel invoice, List<InvoiceServiceModel> services);
        int UpdateVoid(int businessId, int invoiceId);
        int UpdateWriteOff(int businessId, int invoiceId);

        int GetAllPagesCount(int businessId, int page, string search, string orderBy);
    }
}
