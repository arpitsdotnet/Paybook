using System;
using System.ComponentModel.DataAnnotations;

namespace Paybook.ServiceLayer.Models.Clients
{
    public class ClientBalanceModel
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

        [Required]
        [Display(Name = "Client")]
        public string ClientId { get; set; }
        public virtual ClientModel Client { get; set; }

        [Display(Name = "Balance")]
        public string Balance { get; set; }

    }
}
