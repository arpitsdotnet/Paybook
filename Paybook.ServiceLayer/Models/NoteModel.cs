using System;
using System.ComponentModel.DataAnnotations;

namespace Paybook.ServiceLayer.Models
{
    public class NoteModel : BaseResultStatusModel
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

        [Display(Name = "Modify date")]
        public DateTime? ModifyDate { get; set; }

        [Display(Name = "Modify by")]
        public string ModifyBy { get; set; }

        [Display(Name = "Note details")]
        public string Text { get; set; }

        [Display(Name = "Work type")]
        public int WorkTypeId { get; set; }
        public virtual CategoryMasterModel WorkTypeCategoryMaster { get; set; }

        [Display(Name = "Vehicle number")]
        public string VehicleNumber { get; set; }

        [Display(Name = "Client's name")]
        public string ClientName { get; set; }

        [Display(Name = "Client's mobile number")]
        public string MobileNumber { get; set; }

        public string Amount { get; set; }
        public string Awak { get; set; }
        public string Jawak { get; set; }
        public string Balance { get; set; }

    }
}
