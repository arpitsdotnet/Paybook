using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Models.Dashboards;
using Paybook.ServiceLayer.Models.Invoices;
using Paybook.ServiceLayer.Models.ViewModels;
using System.Collections.Generic;

namespace Paybook.BusinessLayer.Abstracts.Customers
{
    public interface IDashboardProcessor
    {
        List<BusinessModel> GetAllBusinesses(string username);
        DashboardViewModel GetAllCounters(int businessId);
        List<DashboardClientChartModel> GetClientCountByDays(int businessId, int days = 7);
        List<DashboardInvoiceChartModel> GetInvoiceAmountsAndPaymentsByDays(int businessId, int days = 7);
        List<DashboardInvoiceChartModel> GetCountOfInvoicesAndPaymentsByLastWeek(int businessId);
        List<DashboardInvoiceChartModel> GetPaymentsLast10(int businessId);
        List<InvoiceModel> GetLast5Invoices(int businessId);
        List<PaymentModel> GetLast5Payments(int businessId);
    }
}
