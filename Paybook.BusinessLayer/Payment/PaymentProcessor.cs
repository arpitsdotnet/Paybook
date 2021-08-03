using Paybook.BusinessLayer.Agency;
using Paybook.BusinessLayer.Client;
using Paybook.BusinessLayer.Common;
using Paybook.BusinessLayer.Invoice;
using Paybook.DatabaseLayer;
using Paybook.DatabaseLayer.Payment;
using Paybook.ServiceLayer;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.BusinessLayer.Payment
{
    public interface IPaymentProcessor
    {
        string AdvancePayment_Insert(string sCurrentAdvancePayment, string sAgency_ID, string sCustomer_ID, string sAdvancePayment_Date, string sCreatedBy, string sTotalAdvancePayment, string sAdvancePaymentType);
        PaymentModel[] Payments_ForInvoice(string sOrderBy, string sGridPageNumber, string sUserName, string sCustomer_ID, string sInvoice_ID, string sCategory_Core);
        InvoiceModel[] Payments_Search(string sOrderBy, string sGridPageNumber, string sUserName, string sAgency_ID, string sCustomer_ID, string sPaymentDateTo, string sPaymentDateFrom);
        int Payments_SelectCount();
        string Payments_SelectMonthsales();
        string Payment_Insert(string sCreatedBY, string sAgency_ID, string sCustomer_ID, string sPaymentAmount, string sPaymentDate, string sPaymentStatus_Core, string sCategory_Core, string sAgent_ID, string sInvoice_ID);
        DataTable Dashboard_GetPaymentCountByLastWeek();
    }

    public class PaymentProcessor : IPaymentProcessor
    {
        private readonly ILogger _logger;
        private readonly IPaymentRepository _paymentRepo;

        private readonly ILastSavedIdProcessor _lastSavedIdProcessor;
        private readonly IAgencyProcessor _agencyProcessor;
        private readonly IClientProcessor _clientProcessor;
        private readonly IInvoiceProcessor _invoiceProcessor;
        private readonly IActivityProcessor _activityProcessor;

        public PaymentProcessor()
        {
            _logger = FileLogger.Instance;
            _paymentRepo = new PaymentRepository();

            _lastSavedIdProcessor = new LastSavedIdProcessor();
            _agencyProcessor = new AgencyProcessor();
            _clientProcessor = new ClientProcessor();
            _invoiceProcessor = new InvoiceProcessor();
            _activityProcessor = new ActivityProcessor();
        }
        public string AdvancePayment_Insert(string sCurrentAdvancePayment, string sAgency_ID, string sCustomer_ID, string sAdvancePayment_Date, string sCreatedBy, string sTotalAdvancePayment, string sAdvancePaymentType)
        {
            try
            {
                string sAdvance_ID = _lastSavedIdProcessor.GetLastSavedID(LastIdTypes.Advance);

                bool result = _paymentRepo.AdvancePayment_Insert(sAdvance_ID, sCurrentAdvancePayment, sAgency_ID, sCustomer_ID, sAdvancePayment_Date, sCreatedBy, sTotalAdvancePayment, sAdvancePaymentType);

                if (result == true)
                {

                    //Update Advance into customer/agency table
                    string sTotalRemainigAmount = "";
                    if (sAgency_ID != "0")
                        _agencyProcessor.Agency_Update_AdvancePayment(sTotalAdvancePayment, sAgency_ID, sTotalRemainigAmount);
                    else
                        _clientProcessor.Customer_Update_AdvancePayment(sTotalAdvancePayment, sCustomer_ID, sTotalRemainigAmount);

                    //Update LastSavedID
                    _lastSavedIdProcessor.LastSavedID_Update(sAdvance_ID, LastIdTypes.Advance);

                    return XmlProcessor.ReadXmlFile("PTS504"); //Success
                }
                else
                {
                    return XmlProcessor.ReadXmlFile("OTW901"); //Internal Error
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public DataTable Dashboard_GetPaymentCountByLastWeek()
        {
            try
            {
                return _paymentRepo.Dashboard_GetPaymentCountByLastWeek();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public PaymentModel[] Payments_ForInvoice(string sOrderBy, string sGridPageNumber, string sUserName, string sCustomer_ID, string sInvoice_ID, string sCategory_Core)
        {
            try
            {
                return _paymentRepo.Payments_ForInvoice(sOrderBy, sGridPageNumber, sUserName, sCustomer_ID, sInvoice_ID, sCategory_Core);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public InvoiceModel[] Payments_Search(string sOrderBy, string sGridPageNumber, string sUserName, string sAgency_ID, string sCustomer_ID, string sPaymentDateTo, string sPaymentDateFrom)
        {
            try
            {
                return _paymentRepo.Payments_Search(sOrderBy, sGridPageNumber, sUserName, sAgency_ID, sCustomer_ID, sPaymentDateTo, sPaymentDateFrom);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public int Payments_SelectCount()
        {
            try
            {
                DataTable dt = _paymentRepo.Payments_SelectCount();
                if (dt != null && dt.Rows.Count > 0)
                    return (int)dt.Rows[0]["IDCount"];

                return 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public string Payments_SelectMonthsales()
        {
            try
            {
                return _paymentRepo.Payments_SelectMonthsales();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public string Payment_Insert(string sCreatedBY, string sAgency_ID, string sCustomer_ID, string sPaymentAmount, string sPaymentDate, string sPaymentStatus_Core, string sCategory_Core, string sAgent_ID, string sInvoice_ID)
        {
            try
            {
                string sReceipt_ID = _lastSavedIdProcessor.GetLastSavedID(LastIdTypes.Receipt);
                string sReceiptID = sReceipt_ID;


                bool result = _paymentRepo.Payment_Insert(sReceipt_ID, sCreatedBY, sAgency_ID, sCustomer_ID, sPaymentAmount, sPaymentDate, sPaymentStatus_Core, sCategory_Core, sAgent_ID, sInvoice_ID);
                if (result)
                {

                    //Update LastSavedID
                    _lastSavedIdProcessor.LastSavedID_Update(sReceiptID, LastIdTypes.Receipt);

                    //Update Invoice Table status            
                    _invoiceProcessor.Invoices_Update_PaymentStatus(sCreatedBY, sCategory_Core, sPaymentStatus_Core, sPaymentDate, sInvoice_ID);

                    //Insert Into Activity Table
                    _activityProcessor.Activity_Insert(new ActivityModel
                    {
                        CreatedBY = sCreatedBY,
                        Activity_Date = sPaymentDate,
                        Agency_ID = sAgency_ID,
                        Customer_ID = sCustomer_ID,
                        PaymentAmount = sPaymentAmount,
                        Category_Core = sCategory_Core,
                        Invoice_ID = sInvoice_ID,
                        InvoiceStatus_Core = sPaymentStatus_Core
                    });

                    return XmlProcessor.ReadXmlFile("PTS501");
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
    }
}
