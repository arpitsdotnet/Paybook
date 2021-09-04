using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paybook.ServiceLayer.Constants
{
    public class ActivityConst
    {
        public const string TypeInvoice = "Invoice";
        public const string TypeClient = "Client";
        public const string TypePayment = "Payment";
        public const string TypeNote = "Note";

        public const string TitleCreated = "created";
        public const string TitleUpdate = "updated";
        public const string TitleActivated = "activated";
        public const string TitleDeactivated = "deactivated";
        public const string TitleDeleted = "deleted";

        public const string TitleOverdue = "overdue";
        public const string TitlePaid = "paid";

        public const string CssPrimary = "text-primary";
        public const string CssSuccess = "text-success";
        public const string CssInfo = "text-info";
        public const string CssWarning = "text-warning";
        public const string CssDanger = "text-danger";
        public const string CssDefault = "text-default";
    }
}
