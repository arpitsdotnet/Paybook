using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paybook.ServiceLayer.Models
{
    public class DashboardCustomerChartModel
    {
        public DateTime? Date { get; set; }
        public int Count { get; set; }
        public decimal Amount { get; set; }
    }
}
