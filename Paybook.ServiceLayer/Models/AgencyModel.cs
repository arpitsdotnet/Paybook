namespace Paybook.ServiceLayer.Models
{
    public class AgencyModel
    {
        public string ID { get; set; }
        public string CreatedDT { get; set; }
        public string CreatedBY { get; set; }
        public string ModifiedDT { get; set; }
        public string ModifiedBY { get; set; }
        public string IsActive { get; set; }
        public string AgencyName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State_Core { get; set; }
        public string State_Disp { get; set; }
        public string Country_Core { get; set; }
        public string Country_Disp { get; set; }
        public string EMail { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string ERROR { get; set; }
        public string Agency_ID { get; set; }
        public string RowCount { get; set; }
        public string RemainingAmount { get; set; }
        public string AdvancePayment { get; set; }
        public string Message { get; set; }
    }
}
