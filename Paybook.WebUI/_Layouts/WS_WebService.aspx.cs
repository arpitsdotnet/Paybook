using System;
using System.Web.Services;
using Paybook.BusinessLayer;
using Paybook.BusinessLayer.Agency;
using Paybook.BusinessLayer.Agent;
using Paybook.BusinessLayer.Business;
using Paybook.BusinessLayer.Client;
using Paybook.BusinessLayer.Common;
using Paybook.BusinessLayer.Invoice;
using Paybook.BusinessLayer.Note;
using Paybook.BusinessLayer.Payment;
using Paybook.BusinessLayer.Report;
using Paybook.BusinessLayer.Setting;
using Paybook.ServiceLayer.Models;

namespace Paybook.WebUI._Layouts
{
    public partial class WS_WebService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        //Customer
        [WebMethod]
        public static CustomerModel[] Customers_SelectAll(string sOrderBy, string sGridPageNumber, string sUserName, string sIsActive, string sSearchText, string sSearchBY)
        {
            IClientProcessor _processor = new ClientProcessor();

            return _processor.Customers_SelectAll(sOrderBy, sGridPageNumber, sUserName, sIsActive, sSearchText, sSearchBY);
        }
        [WebMethod]
        public static CustomerModel[] Customer_SelectRemainingAmount(string sCustomer_ID)
        {
            IClientProcessor _processor = new ClientProcessor();

            return _processor.Customer_SelectRemainingAmount(sCustomer_ID);
        }
        [WebMethod]
        public static CustomerModel[] Customer_SelectName(string sAgency_ID)
        {
            IClientProcessor _processor = new ClientProcessor();

            return _processor.Customer_SelectName(sAgency_ID);
        }
        [WebMethod]
        public static string Customer_UpdateIsActive(string sCustomer_ID, string sIsActive, string sCreatedBY, string sReason)
        {
            IClientProcessor _processor = new ClientProcessor();

            return _processor.Customer_UpdateIsActive(sCustomer_ID, sIsActive, sCreatedBY, sReason);
        }
        [WebMethod]
        public static string Customer_Update_AdvancePayment(string sTotalAdvancePayment, string sCustomer_ID, string sTotalRemainigAmount)
        {
            IClientProcessor _processor = new ClientProcessor();

            return _processor.Customer_Update_AdvancePayment(sTotalAdvancePayment, sCustomer_ID, sTotalRemainigAmount);
        }
        //agency
        [WebMethod]
        public static AgencyModel[] Agency_SelectRemainingAmount(string sAgency_ID)
        {
            IAgencyProcessor _processor = new AgencyProcessor();

            return _processor.Agency_SelectRemainingAmount(sAgency_ID);
        }
        [WebMethod]
        public static string Agency_Update_AdvancePayment(string sTotalAdvancePayment, string sAgency_ID, string sTotalRemainigAmount)
        {
            IAgencyProcessor _processor = new AgencyProcessor();

            return _processor.Agency_Update_AdvancePayment(sTotalAdvancePayment, sAgency_ID, sTotalRemainigAmount);
        }
        //AdvancePayment
        [WebMethod]
        public static string AdvancePayment_Insert(string sCurrentAdvancePayment, string sAgency_ID, string sCustomer_ID, string sAdvancePayment_Date, string sCreatedBy, string sTotalAdvancePayment, string sAdvancePaymentType)
        {
            IPaymentProcessor _processor = new PaymentProcessor();

            return _processor.AdvancePayment_Insert(sCurrentAdvancePayment, sAgency_ID, sCustomer_ID, sAdvancePayment_Date, sCreatedBy, sTotalAdvancePayment, sAdvancePaymentType);
        }

        //Agent
        [WebMethod]
        public static AgentModel[] Agent_SelectName()
        {
            IAgentProcessor _processor = new AgentProcessor();

            return _processor.GetAllActiveIdAndName();
        }

        [WebMethod]
        public static AgentModel[] Agent_GetAllByPage(string sOrderBy, string sGridPageNumber, string sUserName, string sIsActive)
        {
            IAgentProcessor _processor = new AgentProcessor();

            return _processor.GetAllByPage(sOrderBy, sGridPageNumber, sUserName, sIsActive);
        }


        [WebMethod]
        public static string Agent_UpdateIsActive(string sAgent_ID, string sIsActive, string sCreatedBY, string sReason)
        {
            IAgentProcessor _processor = new AgentProcessor();

            return _processor.Activate(sAgent_ID, sIsActive, sCreatedBY, sReason);
        }
        //Invoice
        [WebMethod]
        public static InvoiceModel Invoice_Insert(string sCreatedBY, string sAgency_ID, string sCustomer_ID, string sCategory_Core, string sParticular, string sAmount, string sInvoice_Date, string sRemainingAmount, string sInvoiceStatus_Core, string sAgent_ID, string sRemark, string sMRP, string sGST_Type, string sVehicleNo)
        {
            IInvoiceProcessor _processor = new InvoiceProcessor();

            return _processor.Invoice_Insert(sCreatedBY, sAgency_ID, sCustomer_ID, sCategory_Core, sParticular, sAmount, sInvoice_Date, sRemainingAmount, sInvoiceStatus_Core, sAgent_ID, sRemark, sMRP, sGST_Type, sVehicleNo);
        }

        [WebMethod]
        public static InvoiceModel[] Invoices_Search(string sOrderBy, string sGridPageNumber, string sUserName, string sAgency_ID, string sCustomer_ID, string sReceiptID, string sCategory_Core, string sPaymentDateTo, string sPaymentDateFrom, string sInvoiceStatus_Core)
        {
            IInvoiceProcessor _processor = new InvoiceProcessor();

            return _processor.Invoices_Search(sOrderBy, sGridPageNumber, sUserName, sAgency_ID, sCustomer_ID, sReceiptID, sCategory_Core, sPaymentDateTo, sPaymentDateFrom, sInvoiceStatus_Core);
        }

        [WebMethod]
        public static string Invoices_Update_CloseStatus(string sParticular, string sCategory_Core, string sStatus_Core, string sReason, string sCustomer_ID)
        {
            IInvoiceProcessor _processor = new InvoiceProcessor();

            return _processor.Invoices_Update_CloseStatus(sParticular, "Administrator", sCategory_Core, sStatus_Core, sReason, sCustomer_ID);
        }

        [WebMethod]
        public static PaymentModel[] Payments_ForInvoice(string sOrderBy, string sGridPageNumber, string sUserName, string sCustomer_ID, string sInvoice_ID, string sCategory_Core)
        {
            IPaymentProcessor _processor = new PaymentProcessor();

            return _processor.Payments_ForInvoice(sOrderBy, sGridPageNumber, sUserName, sCustomer_ID, sInvoice_ID, sCategory_Core);
        }
        [WebMethod]
        public static string Payment_Insert(string sCreatedBY, string sAgency_ID, string sCustomer_ID, string sPaymentAmount, string sPaymentDate, string sPaymentStatus_Core, string sCategory_Core, string sAgent_ID, string sInvoice_ID)
        {
            IPaymentProcessor _processor = new PaymentProcessor();

            return _processor.Payment_Insert(sCreatedBY, sAgency_ID, sCustomer_ID, sPaymentAmount, sPaymentDate, sPaymentStatus_Core, sCategory_Core, sAgent_ID, sInvoice_ID);
        }
        [WebMethod]
        public static InvoiceModel[] Payments_Search(string sOrderBy, string sGridPageNumber, string sUserName, string sAgency_ID, string sCustomer_ID, string sPaymentDateTo, string sPaymentDateFrom)
        {
            IPaymentProcessor _processor = new PaymentProcessor();

            return _processor.Payments_Search(sOrderBy, sGridPageNumber, sUserName, sAgency_ID, sCustomer_ID, sPaymentDateTo, sPaymentDateFrom);
        }
        //Category
        [WebMethod]
        public static string SubCategories_Insert(string sCreatedBY, string sCategory_Core, string sCategory_Disp, string sSubCategory_Core, string sSubCategory_Disp, string sSubCategory_Prefix, string sOrderBy)
        {
            ICategoryProcessor _processor = new CategoryProcessor();

            var model = new CategoryModel
            {
                CreatedBY = sCreatedBY,
                Category_Core = sCategory_Core,
                Category_Disp = sCategory_Disp,
                SubCategory_Core = sSubCategory_Core,
                SubCategory_Disp = sSubCategory_Disp,
                SubCategory_Prefix = sSubCategory_Prefix,
                OrderBy = sOrderBy
            };

            return _processor.SubCategories_Insert(model);
        }
        [WebMethod]
        public static string SubCategories_Update(string sCreatedBY, string sCategory_Core, string sCategory_Disp, string sSubCategory_Core, string sSubCategory_Disp, string sSubCategory_Prefix, string sOrderBy)
        {
            ICategoryProcessor _processor = new CategoryProcessor();

            var model = new CategoryModel
            {
                CreatedBY = sCreatedBY,
                Category_Core = sCategory_Core,
                Category_Disp = sCategory_Disp,
                SubCategory_Core = sSubCategory_Core,
                SubCategory_Disp = sSubCategory_Disp,
                SubCategory_Prefix = sSubCategory_Prefix,
                OrderBy = sOrderBy
            };

            return _processor.SubCategories_Update(model);
        }
        [WebMethod]
        public static CategoryModel[] SubCategories_Active_Select(string sCategory_Core)
        {
            ICategoryProcessor _processor = new CategoryProcessor();

            return _processor.SubCategories_Active_Select(sCategory_Core);
        }
        [WebMethod]
        public static CategoryModel[] SubCategories_SelectGrid(string sOrderBy, string sGridPageNumber, string sUserName, string sCategory_Core)
        {
            ICategoryProcessor _processor = new CategoryProcessor();

            return _processor.SubCategories_SelectGrid(sOrderBy, sGridPageNumber, sUserName, sCategory_Core);
        }

        [WebMethod]
        public static CategoryModel[] SubCategories_SelectAll(string sCategory_Core)
        {
            ICategoryProcessor _processor = new CategoryProcessor();

            return _processor.SubCategories_SelectAll(sCategory_Core);
        }
        [WebMethod]
        public static string SubCategories_UpdateIsActive(string sID, string sISActive)
        {
            ICategoryProcessor _processor = new CategoryProcessor();

            return _processor.SubCategories_UpdateIsActive(sID, sISActive);
        }
        [WebMethod]
        public static void LastSavedID_Update(string sID, string sType)
        {
            ILastSavedIdProcessor _processor = new LastSavedIdProcessor();

            _processor.LastSavedID_Update(sID, sType);
        }

        [WebMethod]
        public static CategoryModel[] SubCategory_IsExist(string sSubCategory_Core, string sCategory_Core)
        {
            ICategoryProcessor _processor = new CategoryProcessor();

            return _processor.SubCategory_IsExist(sSubCategory_Core, sCategory_Core);
        }

        // Particular
        [WebMethod]
        public static ParticularModel[] Particular_IsExist(string sParticular, string sCategory_Core)
        {
            IParticularProcessor _processor = new ParticularProcessor();

            return _processor.Particular_IsExist(sParticular, sCategory_Core);
        }

        //Chart
        [WebMethod]
        public static ChartModel[] Count_PaymentInvoice_Chart()
        {
            IChartProcessor _processor = new ChartProcessor();

            return _processor.Count_PaymentInvoice_Chart();
        }
        [WebMethod]
        public static ChartModel[] Dashboard_GetCountOfInvoiceAndPaymentByLastWeek()
        {
            IChartProcessor _processor = new ChartProcessor();

            return _processor.Dashboard_GetCountOfInvoiceAndPaymentByLastWeek();
        }
        [WebMethod]
        public static ChartModel[] Customer_Chart()
        {
            IChartProcessor _processor = new ChartProcessor();

            return _processor.Customer_Chart();
        }
        // Company
        [WebMethod]
        public static BusinessModel[] CompanyProfile_Insert(string sCreatedBY, string sCompanyName, string sFounded_Date, string sCompanyAddress1, string sCompanyAddress2,
            string sCompanyCity, string sCompanyState_Core, string sCompanyCountry, string sCompanyPhoneNumber1, string sCompanyPhoneNumber2, string sCompanyFaxNumber,
            string sCompanyEmail, string sImageFileName, string sGSTIN)
        {
            IBusinessProcessor _processor = new BusinessProcessor();

            BusinessModel businessModel = new BusinessModel
            {
                CreatedBY = sCreatedBY,
                CompanyName = sCompanyName,
                Founded_Date = sFounded_Date,
                Address1 = sCompanyAddress1,
                Address2 = sCompanyAddress2,
                City = sCompanyCity,
                State_Core = sCompanyState_Core,
                Country_Core = sCompanyCountry,
                PhoneNumber1 = sCompanyPhoneNumber1,
                PhoneNumber2 = sCompanyPhoneNumber2,
                FaxNumber = sCompanyFaxNumber,
                EMail = sCompanyEmail,
                ImageFileName = sImageFileName,
                GSTIN = sGSTIN
            };

            return _processor.CompanyProfile_Insert(businessModel);
        }

        [WebMethod]
        public static ReportModel[] GenrateReport(string sPaymentDateTo, string sPaymentDateFrom, string sAgency_ID, string sCustomer_ID, string sRemainingAmount)
        {
            IReportProcessor _processor = new ReportProcessor();

            return _processor.GenrateReport(sPaymentDateTo, sPaymentDateFrom, sAgency_ID, sCustomer_ID, sRemainingAmount);
        }
        [WebMethod]
        public static ReportModel[] GenrateReportForAgency(string sPaymentDateTo, string sPaymentDateFrom, string sAgency_ID, string sRemainingAmount)
        {
            IReportProcessor _processor = new ReportProcessor();

            return _processor.GenrateReportForAgency(sPaymentDateTo, sPaymentDateFrom, sAgency_ID, sRemainingAmount);
        }
        //dailynotes
        [WebMethod]
        public static NoteModel[] DailyNotes_SelectGrid(string sGridPageNumber)
        {
            INoteProcessor _processor = new NoteProcessor();

            return _processor.GetAllByPage(sGridPageNumber);
        }
        [WebMethod]
        public static NoteModel DailyNotes_Edit(string sDataID)
        {
            INoteProcessor _processor = new NoteProcessor();

            return _processor.GetByNoteID(sDataID);
        }
        [WebMethod]
        public static string DailyNotes_Delete(string sDataID)
        {
            INoteProcessor _processor = new NoteProcessor();

            return _processor.Delete(sDataID);
        }
    }
}