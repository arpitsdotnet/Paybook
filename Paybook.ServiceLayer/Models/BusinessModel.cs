namespace Paybook.ServiceLayer.Models
{
    public class BusinessModel
    {
        public string ID { get; set; }
        public string CreatedDT { get; set; }
        public string CreatedBY { get; set; }
        public string ModifiedDT { get; set; }
        public string ModifiedBY { get; set; }
        public string IsActive { get; set; }
        public string CompanyName { get; set; }
        public string GSTIN { get; set; }
        public string Founded_Date { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State_Core { get; set; }
        public string State_Disp { get; set; }
        public string Country_Core { get; set; }
        public string EMail { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string FaxNumber { get; set; }
        public string ImageFileName { get; set; }
        public string Message { get; set; }
        public string ERROR { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
