using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paybook.Web.MvcUI.Models.ViewModels
{
    public class PaymentViewModel
    {
        public IEnumerable<SelectListItem> Clients { get; set; }
        public ClientModel Client { get; set; }
        public PaymentModel Payment { get; set; }
        public decimal BalanceTotal { get; set; } = 0;

    }
}