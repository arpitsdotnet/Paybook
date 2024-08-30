using Paybook.ServiceLayer.Models;
using System.Collections.Generic;

namespace Paybook.DatabaseLayer.Payment
{
    public interface IPaymentRepository : IBaseRepository<PaymentModel>
    {
        string Payments_SelectMonthsales();
        List<PaymentModel> GetAllByInvoiceId(int businessId, int invoiceId, int page, string search, string orderBy);
        List<PaymentModel> GetAllByClientId(int businessId, int clientId);
        int Revert(int businessId, string username, int id);
    }
}
