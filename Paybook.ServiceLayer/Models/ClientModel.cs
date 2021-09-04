using System;
using System.ComponentModel.DataAnnotations;

namespace Paybook.ServiceLayer.Models
{
    public class ClientModel : BaseResultStatusModel
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
        public string Name { get; set; }

        [Display(Name = "Agency name")]
        public string AgencyName { get; set; }

        [Required]
        [Display(Name = "Phone number (primary)")]
        public string PhoneNumber1 { get; set; }

        [Display(Name = "Phone number (other)")]
        public string PhoneNumber2 { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Display(Name = "House no / Building name")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Street address")]
        public string AddressLine2 { get; set; }

        [Required]
        public string City { get; set; }

        [Display(Name = "State")]
        public int StateId { get; set; }
        public virtual StateMasterModel StateMaster { get; set; } = new StateMasterModel();

        [Display(Name = "Country")]
        public int CountryId { get; set; }
        public virtual CountryMasterModel CountryMaster { get; set; } = new CountryMasterModel();
        public string Pincode { get; set; }

        public string CompleteAddressHtml
        {
            get
            {
                string address = string.Empty;

                address += (string.IsNullOrEmpty(AddressLine1)) ? "" : AddressLine1;
                address += (string.IsNullOrEmpty(AddressLine2)) ? "" : (address == "" ? AddressLine2 : "<br/>" + AddressLine2);
                address += (string.IsNullOrEmpty(City)) ? "" : (address == "" ? City : "<br/>" + City);
                address += (string.IsNullOrEmpty(StateMaster.Name)) ? "" : (address == "" ? StateMaster.Name : ", " + StateMaster.Name);
                address += (string.IsNullOrEmpty(CountryMaster.Name)) ? "" : (address == "" ? CountryMaster.Name : "<br/>" + CountryMaster.Name);
                address += (string.IsNullOrEmpty(Pincode)) ? "" : (address == "" ? "(" + Pincode + ")" : " (" + Pincode + ")");

                return address;
            }
        }
        public string CompleteAddress
        {
            get
            {
                string address = string.Empty;

                address += (string.IsNullOrEmpty(AddressLine1)) ? "" : AddressLine1;
                address += (string.IsNullOrEmpty(AddressLine2)) ? "" : (address == "" ? AddressLine2 : "," + Environment.NewLine + AddressLine2);
                address += (string.IsNullOrEmpty(City)) ? "" : (address == "" ? City : "," + Environment.NewLine + City);
                address += (string.IsNullOrEmpty(StateMaster.Name)) ? "" : (City == "" ? StateMaster.Name : ", " + StateMaster.Name);
                address += (string.IsNullOrEmpty(CountryMaster.Name)) ? "" : (address == "" ? CountryMaster.Name : "," + Environment.NewLine + CountryMaster.Name);
                address += (string.IsNullOrEmpty(Pincode)) ? "" : (string.IsNullOrEmpty(CountryMaster.Name) ? "(" + Pincode + ")" : " (" + Pincode + ")");

                return address;
            }
        }

        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; }

        [Display(Name = "Opening Balance")]
        public decimal? OpeningBalance { get; set; }
    }
}
