using Paybook.BusinessLayer.Agency;
using Paybook.BusinessLayer.Client;
using Paybook.BusinessLayer.Common;
using Paybook.BusinessLayer.Setting;
using Paybook.DatabaseLayer;
using Paybook.DatabaseLayer.Invoice;
using Paybook.ServiceLayer;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.BusinessLayer.Invoice
{

    public interface IInvoiceProcessor
    {
        InvoiceModel[] Invoices_Search(string sOrderBy, string sGridPageNumber, string sUserName, string sAgency_ID, string sCustomer_ID, string sReceiptID, string sCategory_Core, string sInvoiceDateTo, string sInvoiceDateFrom, string sInvoiceStatus_Core);
        int Invoices_SelectCount();
        string Invoices_Update_CloseStatus(string sParticular, string sCreatedBY, string sCategory_Core, string sStatus_Core, string sReason, string sCustomer_ID);
        string Invoices_Update_OverdueStatus(string sInvoice_ID, string sCategory_Core, string sStatus_Core);
        string Invoices_Update_PaymentStatus(string sModifiedBY, string sCategory_Core, string sInvoiceStatus_Core, string sLastPayment_Date, string sInvoice_ID);
        InvoiceModel Invoice_Insert(string sCreatedBY, string sAgency_ID, string sCustomer_ID, string sCategory_Core, string sParticular, string sAmount, string sInvoice_Date, string sRemainingAmount, string sInvoiceStatus_Core, string sAgent_ID, string sRemark, string sMRP, string sGST_Type, string sVehicleNo);
        DataTable GetById(string sInvoice_ID);
        DataTable Dashboard_GetInvoiceAmountsByLastWeek();
        string Activity_Insert_Overdue(string sCreatedBY, string sStatus_Core);
    }

    public class InvoiceProcessor : IInvoiceProcessor
    {
        private readonly ILogger _logger;
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly ILastSavedIdProcessor _lastSavedIdProcessor;
        private readonly ICategoryProcessor _categoryProcessor;
        private readonly IClientProcessor _clientProcessor;
        private readonly IAgencyProcessor _agencyProcessor;

        public InvoiceProcessor()
        {
            _logger = FileLogger.Instance;
            _invoiceRepo = new InvoiceRepository();
            _lastSavedIdProcessor = new LastSavedIdProcessor();
            _categoryProcessor = new CategoryProcessor();
            _clientProcessor = new ClientProcessor();
            _agencyProcessor = new AgencyProcessor();
        }

        public DataTable Dashboard_GetInvoiceAmountsByLastWeek()
        {
            try
            {
                return _invoiceRepo.Dashboard_GetInvoiceCountByLastWeek();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public InvoiceModel[] Invoices_Search(string sOrderBy, string sGridPageNumber, string sUserName, string sAgency_ID, string sCustomer_ID, string sReceiptID, string sCategory_Core, string sInvoiceDateTo, string sInvoiceDateFrom, string sInvoiceStatus_Core)
        {
            try
            {
                return _invoiceRepo.Invoices_Search(sOrderBy, sGridPageNumber, sUserName, sAgency_ID, sCustomer_ID, sReceiptID, sCategory_Core, sInvoiceDateTo, sInvoiceDateFrom, sInvoiceStatus_Core);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public int Invoices_SelectCount()
        {
            try
            {
                DataTable dt = _invoiceRepo.Invoices_SelectCount();
                if (dt != null || dt.Rows.Count > 0)
                    return (int)dt.Rows[0]["IDCount"];

                return 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public string Invoices_Update_CloseStatus(string sParticular, string sCreatedBY, string sCategory_Core, string sStatus_Core, string sReason, string sCustomer_ID)
        {
            try
            {
                bool result = _invoiceRepo.Invoices_Update_CloseStatus(sParticular, sCreatedBY, sCategory_Core, sStatus_Core, sReason, sCustomer_ID);
                if (result)
                {
                    return XmlProcessor.ReadXmlFile("INS303");
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public string Invoices_Update_OverdueStatus(string sInvoice_ID, string sCategory_Core, string sStatus_Core)
        {
            try
            {
                bool result = _invoiceRepo.Invoices_Update_OverdueStatus(sInvoice_ID, sCategory_Core, sStatus_Core);
                if (result)
                {
                    return XmlProcessor.ReadXmlFile("CUW110");
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public string Invoices_Update_PaymentStatus(string sModifiedBY, string sCategory_Core, string sInvoiceStatus_Core, string sLastPayment_Date, string sInvoice_ID)
        {
            try
            {
                bool result = _invoiceRepo.Invoices_Update_PaymentStatus(sModifiedBY, sCategory_Core, sInvoiceStatus_Core, sLastPayment_Date, sInvoice_ID);
                if (result)
                {
                    return XmlProcessor.ReadXmlFile("CUW110");
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public InvoiceModel Invoice_Insert(string sCreatedBY, string sAgencyID, string sCustomerID, string sCategory_Core, string sParticular, string sAmount,
            string sInvoice_Date, string sRemainingAmount, string sInvoiceStatus_Core, string sAgent_ID, string sRemark, string sMRP, string sGST_Type, string sVehicleNo)
        {
            InvoiceModel output = new InvoiceModel();
            try
            {
                string sInvoiceID = _lastSavedIdProcessor.GetLastSavedID(LastIdTypes.Invoice);
                if (sInvoiceID == "")
                    output.Message = XmlProcessor.ReadXmlFile("OTW901");


                bool result = _invoiceRepo.Invoice_Insert(sInvoiceID, sCreatedBY, sAgencyID, sCustomerID, sCategory_Core, sParticular, sAmount, sInvoice_Date, sRemainingAmount, sInvoiceStatus_Core, sAgent_ID, sRemark, sMRP, sGST_Type, sVehicleNo);
                if (result == false)
                    output.Message = XmlProcessor.ReadXmlFile("OTW901");


                if (output.Message.Length == 0)
                {
                    //Update invoice id
                    _lastSavedIdProcessor.LastSavedID_Update(sInvoiceID, LastIdTypes.Invoice);

                    //insert into invoice_tx table
                    DataTable dt = _categoryProcessor.SubCategory_SelectGstValues(sGST_Type);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            string sTaxType = dr["SubCategory_Core"].ToString();
                            double dPercentage = Convert.ToDouble(dr["SubCategory_Disp"].ToString());
                            double dAmount = (Convert.ToDouble(sMRP) * dPercentage) / 100;

                            _invoiceRepo.CreateTax(new InvoiceTaxModel
                            {
                                CreatedBY = sCreatedBY,
                                TaxType = sTaxType,
                                Percentage = dPercentage,
                                Amount = dAmount,
                                CustomerID = sCustomerID,
                                InvoiceID = sInvoiceID,
                                Invoice_Date = sInvoice_Date,
                                AgencyID = sAgencyID
                            });
                        }
                    }
                    //Amount Insert Into Customer/Agency Table 
                    double dTotalRemainingAmount = Convert.ToDouble(sRemainingAmount) + Convert.ToDouble(sAmount);

                    if (sAgencyID == "0")
                    {
                        _clientProcessor.Customer_UpdateRemainingAmount(sCustomerID, dTotalRemainingAmount);
                    }
                    else
                    {
                        _agencyProcessor.Agency_UpdateRemainingAmount(sAgencyID, dTotalRemainingAmount);

                    }
                    output.Message = XmlProcessor.ReadXmlFile("INS302");
                    output.Invoice_ID = sInvoiceID;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
            return output;
        }


        public DataTable GetById(string invoiceId)
        {
            try
            {
                return _invoiceRepo.GetByInvoiceID(invoiceId);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public string Activity_Insert_Overdue(string sCreatedBY, string sStatus_Core)
        {
            try
            {
                bool result = _invoiceRepo.Activity_Insert_Overdue(sCreatedBY, sStatus_Core);
                if (result)
                {
                    return XmlProcessor.ReadXmlFile("CUW110");
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
