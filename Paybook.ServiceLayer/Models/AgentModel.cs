namespace Paybook.ServiceLayer.Models
{
    public class AgentModel
    {
        public string ID { get; set; }
        public string CreatedDT { get; set; }
        public string CreatedBY { get; set; }
        public string ModifiedDT { get; set; }
        public string ModifiedBY { get; set; }
        public string IsActive { get; set; }
        public string AgentName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
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
        public string ImageFileName { get; set; }
        public string Prefix_Disp { get; set; }
        public string Prefix_Core { get; set; }
        public string Agent_ID { get; set; }
        public string Gender { get; set; }
        public string ERROR { get; set; }
        public string Message { get; set; }
        //public string idSearchResult { get; set; }
        //public string sSearchFrom { get; set; }
        public string RowCount { get; set; }

    }
}
