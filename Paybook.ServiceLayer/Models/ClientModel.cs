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

        [Display(Name = "Create Date")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "Create By")]
        public string CreateBy { get; set; }

        [Display(Name = "Modify Date")]
        public DateTime? ModifyDate { get; set; }

        [Display(Name = "Modify By")]
        public string ModifyBy { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Agency Name")]
        public string AgencyName { get; set; }

        [Required]
        [Display(Name = "Phone Number (Primary)")]
        public string PhoneNumber1 { get; set; }

        [Display(Name = "Phone Number (Other)")]
        public string PhoneNumber2 { get; set; }

        [Required]
        public string Email { get; set; }


        [Display(Name = "House No / Building Name")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Street Address")]
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
