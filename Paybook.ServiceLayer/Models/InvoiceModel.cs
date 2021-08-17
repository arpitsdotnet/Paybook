using System;
using System.ComponentModel.DataAnnotations;

namespace Paybook.ServiceLayer.Models
{
    public class InvoiceModel : BaseResultStatusModel
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public virtual BusinessModel Businesses { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }


        [Display(Name = "Create date")]
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModifyBy { get; set; }

        [Display(Name = "Invoice number")]
        public string InvoiceNumber { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name = "Invoice date")]
        public DateTime InvoiceDate { get; set; }

        [Required]
        [Display(Name = "Status")]
        public int StatusId { get; set; }
        public virtual CategoryMasterModel StatusCategoryMaster { get; set; }
        public int AgencyId { get; set; }
        public virtual AgencyModel Agencies { get; set; }

        [Required]
        [Display(Name = "Client")]
        public int ClientId { get; set; }
        public virtual ClientModel Clients { get; set; }

        [Required]
        [Display(Name = "Client's email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Please enter a valid email address.")]
        public string ClientEmail { get; set; }

        [Display(Name = "Send email invoice")]
        public bool? IsEmailSend { get; set; }
        public bool? IsEmailSentSuccess { get; set; }

        [Required]
        [Display(Name = "Billing address")]
        public string BillingAddress { get; set; }

        [Required]
        [Display(Name = "Terms")]
        public int TermsId { get; set; }
        public virtual CategoryMasterModel TermsCategoryMaster { get; set; }

        [Required]
        [Display(Name = "Due date")]
        public DateTime DueDate { get; set; }

        [Display(Name = "Overdue")]
        public bool IsOverdue { get; set; } = false;
        public int? OverdueSteps { get; set; }
        public string Message { get; set; }

        [DataType(DataType.Currency)]
        [Range(1, double.MaxValue, ErrorMessage = "The Subtotal field cannot be 0 or negative.")]
        public decimal Subtotal { get; set; } = 0;

        [DataType(DataType.Currency)]
        public decimal TaxableTotal { get; set; } = 0;

        public int? DiscountTypeId { get; set; }
        public virtual CategoryMasterModel DiscountTypeCategoryMaster { get; set; }

        [DataType(DataType.Currency)]
        public decimal DiscountAmount { get; set; } = 0;

        [DataType(DataType.Currency)]
        public decimal DiscountTotal { get; set; } = 0;

        [Required]
        [DataType(DataType.Currency)]
        [Range(1, double.MaxValue, ErrorMessage = "The Total field cannot be 0 or negative.")]
        public decimal Total { get; set; }

        public decimal TotalCalculate => Subtotal + TaxableTotal - DiscountTotal;
    }
}
