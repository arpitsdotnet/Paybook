using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Models.Dashboards;
using Paybook.ServiceLayer.Models.Invoices;
using Paybook.ServiceLayer.Models.ViewModels;
using System.Collections.Generic;

namespace Paybook.BusinessLayer.Abstracts.Customers
{
    public interface IDashboardProcessor
    {
        DashboardViewModel GetAllCounters(string username);
        List<BusinessModel> GetAllBusinesses(string username);
        List<DashboardClientChartModel> GetClientCountByDays(string username, int days = 7);
        List<DashboardInvoiceChartModel> GetInvoiceAmountsAndPaymentsByDays(string username, int days = 7);
        List<DashboardInvoiceChartModel> GetCountOfInvoicesAndPaymentsByLastWeek(string username);
        List<DashboardInvoiceChartModel> GetPaymentsLast10(string username);
        List<InvoiceModel> GetLast5Invoices(string username);
        List<PaymentModel> GetLast5Payments(string username);
    }
}
