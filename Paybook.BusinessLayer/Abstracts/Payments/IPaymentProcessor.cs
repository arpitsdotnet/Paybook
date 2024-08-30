using Paybook.ServiceLayer.Models;
using System.Collections.Generic;

namespace Paybook.BusinessLayer.Abstracts.Payments
{
    public interface IPaymentProcessor : IBaseProcessor<PaymentModel>
    {
        string Payments_SelectMonthsales();
        List<PaymentModel> GetAllByInvoiceId(int businessId, int invoiceId, int page, string search, string orderBy);
        List<PaymentModel> GetAllByClientId(int businessId, int clientId);
        PaymentModel Revert(int businessId, string username, int id);
    }
}
