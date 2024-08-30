using System.ComponentModel.DataAnnotations;
using Paybook.ServiceLayer.Models.Invoices;

namespace Paybook.ServiceLayer.Models.ViewModels
{
    public class InvoicePayViewModel
    {
        public InvoicePayModel InvoicePay { get; set; }

        [Display(Name = "Invoice number")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "Invoice total")]
        public decimal InvoiceTotal { get; set; }

        [Display(Name = "Client")]
        public string ClientName { get; set; }

        public decimal Balance { get; set; }

    }
}