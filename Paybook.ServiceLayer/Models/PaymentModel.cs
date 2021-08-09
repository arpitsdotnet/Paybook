using System;

namespace Paybook.ServiceLayer.Models
{
    public class PaymentModel : BaseResultStatusModel
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public virtual BusinessModel Businesses { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string CreateBy { get; set; }
        public string ModifyBy { get; set; }

        public DateTime? PaymentDate { get; set; }
        public bool IsSuccess { get; set; }
        public string Method { get; set; }
        public string Amount { get; set; }
        public bool IsRefund { get; set; }
        public int Attempts { get; set; }

    }
}
