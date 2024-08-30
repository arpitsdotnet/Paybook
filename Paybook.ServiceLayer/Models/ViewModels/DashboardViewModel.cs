using Paybook.ServiceLayer.Models.Dashboards;

namespace Paybook.ServiceLayer.Models.ViewModels
{
    public class DashboardViewModel
    {
        public BusinessModel Business { get; set; }
        public DashboardCounterWidgetModel CounterInvoicesOpen { get; set; }
        public DashboardCounterWidgetModel CounterInvoicesOpenLastWeek { get; set; }
        public DashboardCounterWidgetModel CounterInvoicesOverdue { get; set; }
        public DashboardCounterWidgetModel CounterPaymentPaidPartial { get; set; }
        public DashboardCounterWidgetModel CounterPaymentPaidLastMonth { get; set; }
        public DashboardCounterWidgetModel CounterPaymentTotal { get; set; }

        public DashboardCounterWidgetModel ClientCounter { get; set; }
    }
}