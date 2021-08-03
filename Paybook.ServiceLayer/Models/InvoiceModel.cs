namespace Paybook.ServiceLayer.Models
{
    public class InvoiceModel
    {
        public string RowCount { get; set; }

        public string ID { get; set; }
        public string CreatedDT { get; set; }
        public string CreatedBY { get; set; }
        public string ModifiedDT { get; set; }
        public string ModifiedBY { get; set; }
        public string IsActive { get; set; }
        public string Customer_ID { get; set; }
        public string CustomerName { get; set; }
        public string Agent_ID { get; set; }
        public string AgentName { get; set; }
        public string ReceiptID { get; set; }
        public string Category_Core { get; set; }
        public string Category_Disp { get; set; }
        public string Particular { get; set; }
        public string Amount { get; set; }
        public string ERROR { get; set; }
        public string idSearchResult { get; set; }
        public string sSearchFrom { get; set; }
        public string idTxtSearch { get; set; }
        public string FirstName { get; set; }
        public string Paid { get; set; }
        public string InvoiceStatus_Disp { get; set; }
        public string InvoiceStatus_Core { get; set; }
        public string Invoice_Date { get; set; }
        public string Invoice_ID { get; set; }
        public string Message { get; set; }
        public string PaymentMethod { get; set; }
    }
}
