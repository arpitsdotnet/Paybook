namespace Paybook.ServiceLayer.Models
{
    public class ActivityModel
    {
        public string ID { get; set; }
        public string CreatedDT { get; set; }
        public string CreatedBY { get; set; }
        public string ModifiedDT { get; set; }
        public string ModifiedBY { get; set; }
        public string IsActive { get; set; }
        public string Agency_ID { get; set; }
        public string Customer_ID { get; set; }
        public string Invoice_ID { get; set; }
        public string Activity_Date { get; set; }
        public string InvoiceStatus_Core { get; set; }
        public string Category_Core { get; set; }
        public string PaymentAmount { get; set; }
        public string Particular { get; set; }

        public string ERROR { get; set; }
        public string CustomerName { get; set; }
    }
}
