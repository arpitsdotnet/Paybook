using System;
using System.ComponentModel.DataAnnotations;

namespace Paybook.ServiceLayer.Models
{
    public class PaymentModel : BaseResultStatusModel
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public virtual BusinessModel Business { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Create date")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "Create by")]
        public string CreateBy { get; set; }

        [Required]
        [Display(Name = "Client")]
        public int ClientId { get; set; }
        public virtual ClientModel Client { get; set; }

        [Display(Name = "Transaction Id")]
        public string TransactionId { get; set; }

        [Display(Name = "Payment date")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime? PaymentDate { get; set; }

        [Display(Name = "Success")]
        public bool IsSuccess { get; set; }

        public string Method { get; set; }

        [Display(Name = "Payment amount")]
        public decimal Amount { get; set; }

        [Display(Name = "Refund")]
        public bool IsRefund { get; set; }

        public int Attempts { get; set; }

    }
}
