using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paybook.ServiceLayer.Models.Clients
{
    public class ClientDetailsCountersModel
    {
        public decimal OpenTotal { get; set; } = 0;
        public decimal UnpaidTotal { get; set; } = 0;
        public decimal OverdueTotal { get; set; } = 0;
        public decimal PaymentTotal { get; set; } = 0;
        public decimal BalanceTotal { get; set; } = 0;
    }
}
