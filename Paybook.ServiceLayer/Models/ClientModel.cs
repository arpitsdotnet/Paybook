using System;
using System.ComponentModel.DataAnnotations;

namespace Paybook.ServiceLayer.Models
{
    public class ClientModel : BaseResultStatusModel
    {
        public int Id { get; set; }

        [Display(Name = "Business")]
        public int BusinessId { get; set; }
        public virtual BusinessModel Businesses { get; set; }

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

        [Required]
        public string Name { get; set; }

        [Display(Name = "Agency name")]
        public string AgencyName { get; set; }

        [Required]
        [Display(Name = "Phone number (primary)")]
        public string PhoneNumber1 { get; set; }

        [Display(Name = "Phone number (other)")]
        public string PhoneNumber2 { get; set; }

        [Required]
        public string Email { get; set; }


        [Display(Name = "House no / Building name")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Street address")]
        public string AddressLine2 { get; set; }

        [Required]
        public string City { get; set; }

        [Display(Name = "State")]
        public int StateId { get; set; }
        public virtual StateMasterModel StateMaster { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }
        public virtual CountryMasterModel CountryMaster { get; set; }
        public string Pincode { get; set; }
    }
}
