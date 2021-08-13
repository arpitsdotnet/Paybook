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

        [Required]
        [Display(Name = "Invoice number")]
        public string InvoiceNumber { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Invoice date")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? InvoiceDate { get; set; }

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
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? DueDate { get; set; }

        [Display(Name = "Overdue")]
        public bool IsOverdue { get; set; }
        public int OverdueSteps { get; set; }
        public string Message { get; set; }
        public decimal Subtotal { get; set; }
        public decimal TaxableTotal { get; set; }
        public int DiscountTypeId { get; set; }
        public virtual CategoryMasterModel DiscountTypeCategoryMaster { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountTotal { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [MinLength(1)]
        public decimal Total { get; set; }
    }
}
