using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paybook.Web.MvcUI.Models.ViewModels
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