using System;
using System.ComponentModel.DataAnnotations;
using Paybook.ServiceLayer.Models.Agencies;
using Paybook.ServiceLayer.Models.Clients;

namespace Paybook.ServiceLayer.Models.Invoices
{
    public class InvoiceModel : BaseResultStatusModel
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public virtual BusinessModel Business { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }


        [Display(Name = "Create date")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "Create by")]
        public string CreateBy { get; set; }

        [Display(Name = "Modify date")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime? ModifyDate { get; set; }

        [Display(Name = "Modify by")]
        public string ModifyBy { get; set; }

        [Display(Name = "Invoice number")]
        public string InvoiceNumber { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name = "Invoice date")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime InvoiceDate { get; set; }

        [Required]
        [Display(Name = "Status")]
        public int StatusId { get; set; }
        public virtual CategoryMasterModel StatusCategoryMaster { get; set; }
        public int AgencyId { get; set; }
        public virtual AgencyModel Agency { get; set; }

        [Required]
        [Display(Name = "Client")]
        public int ClientId { get; set; }
        public virtual ClientModel Client { get; set; }

        [Display(Name = "Client's email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Please enter a valid email address.")]
        public string ClientEmail { get; set; }

        [Display(Name = "Send email invoice")]
        public bool IsEmailSend { get; set; }
        public bool IsEmailSentSuccess { get; set; }

        [Required]
        [Display(Name = "Billing address")]
        public string BillingAddress { get; set; }

        [Required]
        [Display(Name = "Terms")]
        public int TermsId { get; set; }
        public virtual CategoryMasterModel TermsCategoryMaster { get; set; }

        [Required]
        [Display(Name = "Due date")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime DueDate { get; set; }

        [Display(Name = "Overdue")]
        public bool IsOverdue { get; set; } = false;

        [Display(Name = "Overdue steps")]
        public int? OverdueSteps { get; set; }

        [Display(Name = "Message on invoice")]
        public string Message { get; set; }

        //[DataType(DataType.Currency)]
        [Range(1, double.MaxValue, ErrorMessage = "The Subtotal field cannot be 0 or negative.")]
        public decimal Subtotal { get; set; } = 0;

        [Display(Name = "Taxable total")]
        //[DataType(DataType.Currency)]
        public decimal TaxableTotal { get; set; } = 0;

        [Display(Name = "Discount type")]
        public int? DiscountTypeId { get; set; }
        public virtual CategoryMasterModel DiscountTypeCategoryMaster { get; set; }

        [Display(Name = "Discount amount")]
        //[DataType(DataType.Currency)]
        public decimal DiscountAmount { get; set; } = 0;

        [Display(Name = "Discount total")]
        //[DataType(DataType.Currency)]
        public decimal DiscountTotal { get; set; } = 0;

        [Required]
        //[DataType(DataType.Currency,)]
        [Range(1, double.MaxValue, ErrorMessage = "The Total field cannot be 0 or negative.")]
        public decimal Total { get; set; }

        [Display(Name = "Paid total")]
        public decimal PaidTotal { get; set; }

        public decimal TotalCalculate => Subtotal + TaxableTotal - DiscountTotal;
    }
}
