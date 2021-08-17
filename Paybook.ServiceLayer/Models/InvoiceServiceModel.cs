using System;
using System.ComponentModel.DataAnnotations;

namespace Paybook.ServiceLayer.Models
{
    public class InvoiceServiceModel : BaseResultStatusModel
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public virtual BusinessModel Businesses { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Create Date")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "Create By")]
        public string CreateBy { get; set; }

        [Display(Name = "Modify Date")]
        public DateTime? ModifyDate { get; set; }

        [Display(Name = "Modify By")]
        public string ModifyBy { get; set; }

        public int InvoiceId { get; set; }
        public virtual InvoiceModel Invoices { get; set; }

        [Required]
        [Display(Name = "Service")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Work Type")]
        public int WorkTypeId { get; set; }
        public virtual CategoryMasterModel WorkTypeCategoryMaster { get; set; }

        [Display(Name = "Vehicle Number")]
        public string VehicleNumber { get; set; }

        [Required]
        public int Qty { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Rate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Subtotal { get; set; }

        [Display(Name = "Order")]
        public int OrderBy { get; set; }

        [Display(Name = "Taxable")]
        public bool IsTaxable { get; set; }

        [Display(Name = "Tax Type")]
        public int? TaxTypeId { get; set; }
        public virtual CategoryMasterModel TaxTypeCategoryMaster { get; set; }

        [Display(Name = "IGST Percentage")]
        public int IGSTPercentage { get; set; }

        [Display(Name = "IGST Amount")]
        [DataType(DataType.Currency)]
        public decimal IGSTAmount { get; set; }

        [Display(Name = "CGST Percentage")]
        public int CGSTPercentage { get; set; }

        [Display(Name = "CGST Amount")]
        [DataType(DataType.Currency)]
        public decimal CGSTAmount { get; set; }

        [Display(Name = "SGST Percentage")]
        public int SGSTPercentage { get; set; }

        [Display(Name = "SGST Amount")]
        [DataType(DataType.Currency)]
        public decimal SGSTAmount { get; set; }

        [Display(Name = "Taxable Total")]
        [DataType(DataType.Currency)]
        public decimal TaxableTotal { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }
    }
}
