using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Paybook.Web.MvcUI.Models.ViewModels
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