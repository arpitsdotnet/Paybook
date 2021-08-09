using System;

namespace Paybook.ServiceLayer.Models
{
    public class InvoiceServiceModel : BaseResultStatusModel
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public virtual BusinessModel Businesses { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string CreateBy { get; set; }
        public string ModifyBy { get; set; }

        public string Name { get; set; }
        public int WorkTypeId { get; set; }
        public virtual CategoryMasterModel WorkTypeCategoryMaster { get; set; }
        public string VehicleNumber { get; set; }
        public int Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal Subtotal { get; set; }
        public bool IsTaxable { get; set; }
        public int TaxTypeId { get; set; }
        public virtual CategoryMasterModel TaxTypeCategoryMaster { get; set; }
        public int IGSTPercentage { get; set; }
        public decimal IGSTAmount { get; set; }
        public int CGSTPercentage { get; set; }
        public decimal CGSTAmount { get; set; }
        public int SGSTPercentage { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal TaxableTotal { get; set; }
        public decimal Total { get; set; }
    }
}
