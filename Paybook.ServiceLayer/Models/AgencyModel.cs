using System;

namespace Paybook.ServiceLayer.Models
{
    public class AgencyModel : BaseResultStatusModel
    {
        public int ID { get; set; }
        public int BusinessId { get; set; }
        public virtual BusinessModel Businesses { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string CreateBy { get; set; }
        public string ModifyBy { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string Email { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateId { get; set; }
        public virtual StateMasterModel StateMaster { get; set; }
        public string CountryId { get; set; }
        public virtual CountryMasterModel CountryMaster { get; set; }
        public string Pincode { get; set; }
        public string AddressComplete => $"{AddressLine1}, {AddressLine2}, {City}, ({StateMaster.Name}, {CountryMaster.Name}) - {Pincode}, ";
    }
}
