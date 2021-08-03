using System;
using System.Web.Services;
using Paybook.BusinessLayer;

namespace Paybook.WebUI._Layouts
{
    public partial class WS_WebService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        //Customer
        [WebMethod]
        public static clsCustomers[] Customers_SelectAll(string sOrderBy, string sGridPageNumber, string sUserName, string sIsActive, string sSearchText,string sSearchBY)
        {
            return clsCommon.Customers_SelectAll(sOrderBy, sGridPageNumber, sUserName, sIsActive, sSearchText,sSearchBY);
        }
        [WebMethod]
        public static clsCustomers[] Customer_SelectRemainingAmount(string sCustomer_ID)
        {
            return clsCommon.Customer_SelectRemainingAmount(sCustomer_ID);
        }
        [WebMethod]
        public static clsCustomers[] Customer_SelectName(string sAgency_ID)
        {
            return clsCommon.Customer_SelectName(sAgency_ID);
        }
        [WebMethod]
        public static clsCustomers[] Customer_UpdateIsActive(string sCustomer_ID, string sIsActive, string sCreatedBY, string sReason)
        {
            return clsCommon.Customer_UpdateIsActive(sCustomer_ID, sIsActive, sCreatedBY, sReason);
        }
        [WebMethod]
        public static clsCustomers[] Customer_Update_AdvancePayment(string sTotalAdvancePayment, string sCustomer_ID, string sTotalRemainigAmount)
        {
            return clsCommon.Customer_Update_AdvancePayment(sTotalAdvancePayment, sCustomer_ID, sTotalRemainigAmount);
        }
        //agency
        [WebMethod]
        public static clsAgency[] Agency_SelectRemainingAmount(string sAgency_ID)
        {
            return clsCommon.Agency_SelectRemainingAmount(sAgency_ID);
        }
        [WebMethod]
        public static clsAgency[] Agency_Update_AdvancePayment(string sTotalAdvancePayment, string sAgency_ID, string sTotalRemainigAmount)
        {
            return clsCommon.Agency_Update_AdvancePayment(sTotalAdvancePayment, sAgency_ID, sTotalRemainigAmount);
        }
        //AdvancePayment
         [WebMethod]
        public static clsPayments[] AdvancePayment_Insert(string sCurrentAdvancePayment,string sAgency_ID, string sCustomer_ID, string sAdvancePayment_Date, string sCreatedBy, string sTotalAdvancePayment, string sAdvancePaymentType)
        {
           return clsCommon.AdvancePayment_Insert(sCurrentAdvancePayment, sAgency_ID, sCustomer_ID, sAdvancePayment_Date, sCreatedBy, sTotalAdvancePayment, sAdvancePaymentType);
        }

        //Agent
        [WebMethod]
        public static clsAgents[] Agent_SelectName()
        {
            return clsCommon.Agent_SelectName();
        }

        [WebMethod]
        public static clsAgents[] Agents_SelectAll(string sOrderBy, string sGridPageNumber, string sUserName, string sIsActive)
        {
            return clsCommon.Agents_SelectAll(sOrderBy, sGridPageNumber, sUserName, sIsActive);
        }


        [WebMethod]
        public static clsAgents[] Agent_UpdateIsActive(string sAgent_ID, string sIsActive, string sCreatedBY, string sReason)
        {
            return clsCommon.Agent_UpdateIsActive(sAgent_ID, sIsActive, sCreatedBY, sReason);
        }
        //Invoice
        [WebMethod]
        public static clsInvoices[] Invoice_Insert(string sCreatedBY,string sAgency_ID, string sCustomer_ID, string sCategory_Core, string sParticular, string sAmount, string sInvoice_Date, string sRemainingAmount, string sInvoiceStatus_Core, string sAgent_ID,string sRemark,string sMRP,string sGST_Type,string sVehicleNo)
        {
            return clsCommon.Invoice_Insert(sCreatedBY, sAgency_ID,sCustomer_ID, sCategory_Core, sParticular, sAmount, sInvoice_Date, sRemainingAmount, sInvoiceStatus_Core, sAgent_ID,sRemark, sMRP, sGST_Type, sVehicleNo);
        }       

        [WebMethod]
        public static clsInvoices[] Invoices_Search(string sOrderBy, string sGridPageNumber, string sUserName, string sAgency_ID, string sCustomer_ID, string sReceiptID, string sCategory_Core, string sPaymentDateTo, string sPaymentDateFrom, string sInvoiceStatus_Core)
        {
            return clsCommon.Invoices_Search(sOrderBy, sGridPageNumber, sUserName, sAgency_ID, sCustomer_ID, sReceiptID, sCategory_Core, sPaymentDateTo, sPaymentDateFrom, sInvoiceStatus_Core);
        }

        [WebMethod]
        public static clsInvoices[] Invoices_Update_CloseStatus(string sParticular, string sCategory_Core, string sStatus_Core, string sReason, string sCustomer_ID)
        {
            return clsCommon.Invoices_Update_CloseStatus(sParticular, sCategory_Core, sStatus_Core, sReason, sCustomer_ID);
        }
      
        [WebMethod]
        public static clsPayments[] Payments_ForInvoice(string sOrderBy, string sGridPageNumber, string sUserName, string sCustomer_ID, string sInvoice_ID, string sCategory_Core)
        {
            return clsCommon.Payments_ForInvoice(sOrderBy, sGridPageNumber, sUserName,sCustomer_ID, sInvoice_ID, sCategory_Core);
        }
         [WebMethod]
        public static clsPayments[] Payment_Insert(string sCreatedBY,string sAgency_ID, string sCustomer_ID, string sPaymentAmount, string sPaymentDate, string sPaymentStatus_Core, string sCategory_Core, string sAgent_ID, string sInvoice_ID)
        {
            return clsCommon.Payment_Insert(sCreatedBY, sAgency_ID, sCustomer_ID, sPaymentAmount, sPaymentDate, sPaymentStatus_Core, sCategory_Core, sAgent_ID, sInvoice_ID);
        }
        [WebMethod]
        public static clsInvoices[] Payments_Search(string sOrderBy, string sGridPageNumber, string sUserName, string sAgency_ID, string sCustomer_ID, string sPaymentDateTo, string sPaymentDateFrom)
        {
            return clsCommon.Payments_Search(sOrderBy, sGridPageNumber, sUserName, sAgency_ID, sCustomer_ID, sPaymentDateTo, sPaymentDateFrom);
        }
        //Category
        [WebMethod]
        public static clsCategories[] SubCategories_Insert(string sCreatedBY, string sCategory_Core, string sCategory_Disp, string sSubCategory_Core, string sSubCategory_Disp, string sSubCategory_Prefix, string sOrderBy)
        {
            return clsCommon.SubCategories_Insert(sCreatedBY, sCategory_Core, sCategory_Disp, sSubCategory_Core, sSubCategory_Disp, sSubCategory_Prefix, sOrderBy);
        }
        [WebMethod]
        public static clsCategories[] SubCategories_Update(string sCreatedBY, string sCategory_Core, string sCategory_Disp, string sSubCategory_Core, string sSubCategory_Disp, string sSubCategory_Prefix, string sOrderBy)
        {
            return clsCommon.SubCategories_Update(sCreatedBY, sCategory_Core, sCategory_Disp, sSubCategory_Core, sSubCategory_Disp, sSubCategory_Prefix, sOrderBy);
        }
        [WebMethod]
        public static clsCategories[] SubCategories_Active_Select(string sCategory_Core)
        {
            return clsCommon.SubCategories_Active_Select(sCategory_Core);
        }
        [WebMethod]
        public static clsCategories[] SubCategories_SelectGrid(string sOrderBy, string sGridPageNumber, string sUserName, string sCategory_Core)
        {
            return clsCommon.SubCategories_SelectGrid(sOrderBy, sGridPageNumber, sUserName, sCategory_Core);
        }

        [WebMethod]
        public static clsCategories[] SubCategories_SelectAll( string sCategory_Core)
        {
            return clsCommon.SubCategories_SelectAll(sCategory_Core);
        }
        [WebMethod]
        public static clsCategories[] SubCategories_UpdateIsActive(string sID, string sISActive)
        {
           return clsCommon.SubCategories_UpdateIsActive(sID, sISActive);
        }
        [WebMethod]
        public static void LastSavedID_Update(string sID, string sType)
        {
            clsCommon.LastSavedID_Update(sID, sType);
        }
       
        [WebMethod]
        public static clsCategories[] SubCategory_IsExist(string sSubCategory_Core, string sCategory_Core)
        {
            return clsCommon.SubCategory_IsExist(sSubCategory_Core, sCategory_Core);
        }

        // Particular
        [WebMethod]
        public static clsParticular[] Particular_IsExist(string sParticular, string sCategory_Core)
        {
            return clsCommon.Particular_IsExist(sParticular, sCategory_Core);
        }

        //Chart
        [WebMethod]
        public static clsChart[] Count_PaymentInvoice_Chart()
        {
            return clsCommon.Count_PaymentInvoice_Chart();
        }
        [WebMethod]
        public static clsChart[] Amount_PaymentInvoice_Chart()
        {
            return clsCommon.Amount_PaymentInvoice_Chart();
        }
        [WebMethod]
        public static clsChart[] Customer_Chart()
        {
            return clsCommon.Customer_Chart();
        }
        // Company
        [WebMethod]
        public static clsCompany[] CompanyProfile_Insert(string sCreatedBY, string sCompanyName, string sFounded_Date, string sCompanyAddress1, string sCompanyAddress2, string sCompanyCity, string sCompanyState_Core, string sCompanyCountry, string sCompanyPhoneNumber1, string sCompanyPhoneNumber2, string sCompanyFaxNumber, string sCompanyEmail, string sImageFileName,string sGSTIN)
        {
            return clsCommon.CompanyProfile_Insert(sCreatedBY, sCompanyName, sFounded_Date, sCompanyAddress1, sCompanyAddress2, sCompanyCity, sCompanyState_Core, sCompanyCountry, sCompanyPhoneNumber1, sCompanyPhoneNumber2, sCompanyFaxNumber, sCompanyEmail, sImageFileName, sGSTIN);
        }
       
        [WebMethod]
        public static clsReport[] GenrateReport(string sPaymentDateTo, string sPaymentDateFrom,string sAgency_ID, string sCustomer_ID, string sRemainingAmount)
        {
            return clsCommon.GenrateReport(sPaymentDateTo, sPaymentDateFrom, sAgency_ID, sCustomer_ID, sRemainingAmount);
        }
        [WebMethod]
        public static clsReport[] GenrateReportForAgency(string sPaymentDateTo, string sPaymentDateFrom, string sAgency_ID, string sRemainingAmount)
        {
            return clsCommon.GenrateReportForAgency(sPaymentDateTo, sPaymentDateFrom, sAgency_ID, sRemainingAmount);
        }
        //dailynotes
        [WebMethod]
        public static clsDailyNotes[] DailyNotes_SelectGrid(string sGridPageNumber)
        {
            return clsCommon.DailyNotes_SelectGrid(sGridPageNumber);
        }
        [WebMethod]
        public static clsDailyNotes[] DailyNotes_Edit(string sDataID)
        {
            return clsCommon.DailyNotes_Edit(sDataID);
        }
        [WebMethod]
        public static clsDailyNotes[] DailyNotes_Delete(string sDataID)
        {
            return clsCommon.DailyNotes_Delete(sDataID);
        }
    }
}