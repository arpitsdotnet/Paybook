using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paybook.ServiceLayer.Models
{
    public class InvoiceCountersModel
    {
        //CountOfInvoices | SumOfInvoices | CountOfOverdue | SumOfOverdue | CountOfUnpaid | SumOfUnpaid | CountOfPaid | SumOfPaid
        public int CountOfInvoices { get; set; }
        public decimal SumOfInvoices { get; set; }

        public int CountOfOverdue { get; set; }
        public decimal SumOfOverdue { get; set; }

        public int CountOfUnpaid { get; set; }
        public decimal SumOfUnpaid { get; set; }

        public int CountOfPaid { get; set; }
        public decimal SumOfPaid { get; set; }
    }
}
