using System;
using System.ComponentModel.DataAnnotations;

namespace Paybook.ServiceLayer.Models
{
    public class PaymentModel : BaseResultStatusModel
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public virtual BusinessModel Businesses { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Create date")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "Create by")]
        public string CreateBy { get; set; }


        [Display(Name = "Payment date")]
        public DateTime? PaymentDate { get; set; }

        [Display(Name = "Success")]
        public bool IsSuccess { get; set; }

        public string Method { get; set; }
        public string Amount { get; set; }

        [Display(Name = "Refund")]
        public bool IsRefund { get; set; }

        public int Attempts { get; set; }

    }
}
