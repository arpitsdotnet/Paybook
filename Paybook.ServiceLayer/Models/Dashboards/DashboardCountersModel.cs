using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paybook.ServiceLayer.Models.Dashboards
{
    public class DashboardCountersModel
    {
        public int BusinessId { get; set; }

        public int CountTotalOpenInvoice { get; set; }
        public decimal SumofTotalOpenInvoice { get; set; }

        public int CountLastWeekOpenInvoice { get; set; }
        public decimal SumLastWeekOpenInvoice { get; set; }

        public int CountOfOverdue { get; set; }
        public decimal SumOfOverdue { get; set; }

        public int CountOfPaidPartial { get; set; }
        public decimal SumOfPaidPartialAmount { get; set; }

        public int CountOfPaidAmount { get; set; }
        public decimal SumOfPaidAmount { get; set; }

        public int CountOfPaymentTotal { get; set; }
        public decimal SumOfPaymentTotal { get; set; }

        public int CountofCustomers { get; set; }

    }
}
