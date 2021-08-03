namespace Paybook.ServiceLayer.Models
{
    public class PaymentModel
    {
        public string RowCount { get; set; }
        public string ID { get; set; }
        public string CreatedDT { get; set; }
        public string CreatedBY { get; set; }
        public string ModifiedDT { get; set; }
        public string ModifiedBY { get; set; }
        public string IsActive { get; set; }
        public string Payment_Date { get; set; }
        public string PaymentAmount { get; set; }
        public string PaymentType { get; set; }
        public string Particular { get; set; }
        public string ReceiptID { get; set; }
        public string Customer_ID { get; set; }
        public string Category_Disp { get; set; }
        public string Category_Core { get; set; }
        public string Message { get; set; }
        public string ERROR { get; set; }

    }
}
