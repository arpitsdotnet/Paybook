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

        [Display(Name = "Create date")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "Create by")]
        public string CreateBy { get; set; }

        [Display(Name = "Modify date")]
        public DateTime? ModifyDate { get; set; }

        [Display(Name = "Modify by")]
        public string ModifyBy { get; set; }

        public int InvoiceId { get; set; }

        [Required]
        [Display(Name = "Product/Service")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Work type")]
        public int WorkTypeId { get; set; }

        [Display(Name = "Vehicle number")]
        public string VehicleNumber { get; set; }

        [Required]
        public int Qty { get; set; }

        [Required]
        public decimal Rate { get; set; }
        public decimal Subtotal { get; set; }

        [Display(Name = "Order")]
        public int OrderBy { get; set; }

        [Required]
        [Display(Name = "Taxable")]
        public bool IsTaxable { get; set; }

        [Display(Name = "Tax type")]
        public int? TaxTypeId { get; set; }

        [Display(Name = "IGST percentage")]
        public int IGSTPercentage { get; set; }

        [Display(Name = "IGST amount")]
        public decimal IGSTAmount { get; set; }

        [Display(Name = "CGST percentage")]
        public int CGSTPercentage { get; set; }

        [Display(Name = "CGST amount")]
        public decimal CGSTAmount { get; set; }

        [Display(Name = "SGST percentage")]
        public int SGSTPercentage { get; set; }

        [Display(Name = "SGST amount")]
        public decimal SGSTAmount { get; set; }

        [Display(Name = "Taxable total")]
        public decimal TaxableTotal { get; set; }

        [Required]
        public decimal Total { get; set; }
    }
}
