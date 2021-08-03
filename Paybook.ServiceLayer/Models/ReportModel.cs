namespace Paybook.ServiceLayer.Models
{
    public class ReportModel
    {
        public string Customer_ID { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string ERROR { get; set; }
        public string Message { get; set; }
        public string VersionNumber { get; set; }
        public string PaymentDateFrom { get; set; }
        public string PaymentDateTo { get; set; }

    }
}
