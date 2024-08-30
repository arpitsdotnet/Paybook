using System.Data;

namespace Paybook.DatabaseLayer.Abstracts.Reports
{
    public interface IReportRepository
    {
        DataTable InvoicePayment_AgencyReport_Select(string sPaymentDateTo, string sPaymentDateFrom, string sAgencyID);
        DataTable InvoicePayment_CustomerReport_Select(string sPaymentDateTo, string sPaymentDateFrom, string sCustomer_ID);
        DataTable RemainingAmount_BeforeFromDate_Select(string sPaymentDateFrom, string sCustomer_ID, string sAgency_ID);
        string ReportVersionNumber_Select(string sCustomer_ID);
        bool ReportVersion_Insert(string iVersionNumber, string sCustomer_ID, string sPaymentDateFrom, string sPaymentDateTo, string sFilePath);
        string GetLastFileNameByDateAndCustomerID(string sPaymentDateTo, string sPaymentDateFrom, string sCustomer_ID);
    }
}
