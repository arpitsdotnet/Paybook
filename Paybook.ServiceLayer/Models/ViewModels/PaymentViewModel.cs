using System.Collections.Generic;
using System.Web.Mvc;
using Paybook.ServiceLayer.Models.Clients;

namespace Paybook.ServiceLayer.Models.ViewModels
{
    public class PaymentViewModel
    {
        public IEnumerable<SelectListItem> Clients { get; set; }
        public ClientModel Client { get; set; }
        public PaymentModel Payment { get; set; }
        public decimal BalanceTotal { get; set; } = 0;

    }
}