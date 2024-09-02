using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paybook.ServiceLayer.Models.Dashboards
{
    public class DashboardCountersModel
    {
        public int BusinessId { get; set; }

        public int OpenInvoiceCount { get; set; }
        public decimal OpenInvoiceTotal { get; set; }

        public int OpenInvoiceLastWeekCount { get; set; }
        public decimal OpenInvoiceLastWeekTotal { get; set; }

        public int OverdueInvoiceCount { get; set; }
        public decimal OverdueInvoiceTotal { get; set; }

        public int PartialPaidInvoiceCount { get; set; }
        public decimal PartialPaidInvoiceTotal { get; set; }

        public int PaidInvoiceCount { get; set; }
        public decimal PaidInvoiceTotal { get; set; }

        public int DepositCount { get; set; }
        public decimal DepositTotal { get; set; }

        public int CustomerCount { get; set; }

    }
}
