using System;
using System.ComponentModel.DataAnnotations;

namespace Paybook.ServiceLayer.Models
{
    /// <summary>
    /// Class is a copy of InvoiceServiceModel but without Foreign Keys Elements
    /// </summary>
    public class InvoiceServiceMiniModel
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }

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

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Work Type")]
        public int WorkTypeId { get; set; }

        [Display(Name = "Vehicle Number")]
        public string VehicleNumber { get; set; }

        [Required]
        public int Qty { get; set; }

        [Required]
        public decimal Rate { get; set; }
        public decimal Subtotal { get; set; }

        [Required]
        [Display(Name = "Taxable")]
        public bool IsTaxable { get; set; }

        [Display(Name = "Tax Type")]
        public int TaxTypeId { get; set; }

        [Display(Name = "IGST Percentage")]
        public int IGSTPercentage { get; set; }

        [Display(Name = "IGST Amount")]
        public decimal IGSTAmount { get; set; }

        [Display(Name = "CGST Percentage")]
        public int CGSTPercentage { get; set; }

        [Display(Name = "CGST Amount")]
        public decimal CGSTAmount { get; set; }

        [Display(Name = "SGST Percentage")]
        public int SGSTPercentage { get; set; }

        [Display(Name = "SGST Amount")]
        public decimal SGSTAmount { get; set; }

        [Display(Name = "Taxable Total")]
        public decimal TaxableTotal { get; set; }

        [Required]
        public decimal Total { get; set; }
    }
}
