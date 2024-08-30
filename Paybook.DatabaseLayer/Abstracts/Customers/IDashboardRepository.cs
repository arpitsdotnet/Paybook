using System.Collections.Generic;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Models.Dashboards;
using Paybook.ServiceLayer.Models.Invoices;

namespace Paybook.DatabaseLayer.Abstracts.Customers
{
    public interface IDashboardRepository
    {
        DashboardCountersModel GetAllCounters(int businessId);
        List<DashboardChartModel> GetClientCounterByDays(int businessId, int days = 7);
        List<DashboardChartModel> GetInvoiceCountAndTotalByDays(int businessId, int days = 7);
        List<DashboardChartModel> GetPaymentCountAndTotalByDays(int businessId, int days = 7);
        List<DashboardChartModel> GetPaymentTotalByLast10(int businessId);

        int GetInvoiceCountByDays(int businessId, int days = 7);
        int GetPaymentCountByDays(int businessId, int days = 7);

        List<InvoiceModel> GetLast5Invoices(int businessId);
        List<PaymentModel> GetLast5Payments(int businessId);
    }
}
