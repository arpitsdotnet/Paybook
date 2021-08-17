using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paybook.ServiceLayer.Constants
{
    public class InvoiceStatusConst
    {
        public const string Open = "Open"; // You’ve created an invoice and it hasn’t been sent to the customer.
        public const string Sent = "Sent"; // Invoice has been sent to the customer
        public const string PaidPartial = "PaidPartial"; // Invoice has been partially paid by the customer and amount is still remaining
        public const string Paid = "Paid"; // Invoice has been fully paid by the customer
        public const string Void = "Void"; // You will void an invoice if it has been raised incorrectly. Customers cannot pay for a voided invoice.
        public const string WriteOff = "WriteOff"; // You can Write Off an invoice only you’re sure that the amount the customer owes is uncollectible.
    }
}
