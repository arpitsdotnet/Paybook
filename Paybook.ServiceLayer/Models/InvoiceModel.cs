using System;

namespace Paybook.ServiceLayer.Models
{
    public class InvoiceModel : BaseResultStatusModel
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public virtual BusinessModel Businesses { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string CreateBy { get; set; }
        public string ModifyBy { get; set; }

        public string InvoiceNumber { get; set; }
        public string Description { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public int StatusId { get; set; }
        public virtual CategoryMasterModel StatusCategoryMaster { get; set; }
        public int UserId { get; set; }
        public virtual IdentityUserModel Users { get; set; }
        public int AgencyId { get; set; }
        public virtual AgencyModel Agencies { get; set; }
        public int ClientId { get; set; }
        public virtual ClientModel Clients { get; set; }
        public string ClientEmail { get; set; }
        public bool IsEmailSend { get; set; }
        public bool IsEmailSentSuccess { get; set; }
        public string BillingAddress { get; set; }
        public int TermsId { get; set; }
        public virtual CategoryMasterModel TermsCategoryMaster { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsOverdue { get; set; }
        public int OverdueSteps { get; set; }
        public string Message { get; set; }
        public decimal Subtotal { get; set; }
        public decimal TaxableTotal { get; set; }
        public int DiscountTypeId { get; set; }
        public virtual CategoryMasterModel DiscountTypeCategoryMaster { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountTotal { get; set; }
        public decimal Total { get; set; }
    }
}
