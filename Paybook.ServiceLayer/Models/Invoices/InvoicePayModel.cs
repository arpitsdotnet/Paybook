using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paybook.ServiceLayer.Models.Invoices
{
    public class InvoicePayModel : BaseResultStatusModel
    {
        public int Id { get; set; }

        [Display(Name = "Business")]
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

        public int InvoiceId { get; set; }
        public virtual InvoiceModel Invoice { get; set; }

        [Display(Name = "Pay date")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime? PayDate { get; set; }

        [Display(Name = "Pay amount")]
        public decimal PayAmount { get; set; } = 0;
    }
}
