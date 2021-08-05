using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paybook.ServiceLayer.Constants
{
    public class InvoiceStatusConst
    {
        public const string Open = "IS_OPEN";
        public const string Overdue = "IS_OVERDUE";
        public const string PaidPartial = "IS_PAID_PARTIAL";
        public const string Paid = "IS_PAID";
        public const string Close = "IS_CLOSE";
    }
}
