using System;
using System.ComponentModel.DataAnnotations;

namespace Paybook.ServiceLayer.Models
{
    public class BusinessModel : BaseResultStatusModel
    {
        public int Id { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string CreateBy { get; set; }
        public string ModifyBy { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Display(Name = "Selected")]
        public bool IsSelected { get; set; }

        [Display(Name = "Logo")]
        public string Image { get; set; }

        [Display(Name = "GST Number")]
        public string GSTNumber { get; set; }
        
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        
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

