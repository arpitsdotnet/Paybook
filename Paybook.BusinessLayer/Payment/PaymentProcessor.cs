using Paybook.BusinessLayer.Agency;
using Paybook.BusinessLayer.Client;
using Paybook.BusinessLayer.Common;
using Paybook.BusinessLayer.Invoice;
using Paybook.DatabaseLayer;
using Paybook.DatabaseLayer.Payment;
using Paybook.ServiceLayer;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Services;
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
        string Payments_SelectMonthsales();
        List<PaymentModel> GetAllByInvoiceId(int businessId, int invoiceId, int page, string search, string orderBy);
        List<PaymentModel> GetAllByPage(int businessId, int page, string search, string orderBy);
        PaymentModel GetById(int businessId, int id);
        PaymentModel Create(PaymentModel model);
        PaymentModel Update(PaymentModel model);
        PaymentModel Activate(int businessId, int id, bool active);
        PaymentModel Delete(int businessId, int id);
    }

    public class PaymentProcessor : IPaymentProcessor
    {
        private readonly ILogger _logger;
        private readonly IPaymentRepository _paymentRepo;

        private readonly ILastSavedNumberProcessor _lastSavedIdProcessor;
        private readonly IAgencyProcessor _agencyProcessor;
        private readonly IClientProcessor _clientProcessor;
        private readonly IInvoiceProcessor _invoiceProcessor;

        public PaymentProcessor()
        {
            _logger = LoggerFactory.Instance;
            _paymentRepo = new PaymentRepository();

            _lastSavedIdProcessor = new LastSavedNumberProcessor();
            _agencyProcessor = new AgencyProcessor();
            _clientProcessor = new ClientProcessor();
            _invoiceProcessor = new InvoiceProcessor();
        }

        public string Payments_SelectMonthsales()
        {
            try
            {
                return _paymentRepo.Payments_SelectMonthsales();
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }

        public List<PaymentModel> GetAllByInvoiceId(int businessId, int invoiceId, int page, string search, string orderBy)
        {
            try
            {
                return _paymentRepo.GetAllByInvoiceId(businessId, invoiceId, page, search, orderBy);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public List<PaymentModel> GetAllByPage(int businessId, int page, string search, string orderBy)
        {
            try
            {
                return _paymentRepo.GetAllByPage(businessId, page, search, orderBy);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public PaymentModel GetById(int businessId, int id)
        {
            try
            {
                return _paymentRepo.GetById(businessId, id);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public PaymentModel Create(PaymentModel model)
        {
            try
            {
                PaymentModel output = new PaymentModel { IsSucceeded = false };

                int result = _paymentRepo.Create(model);
                if (result > 0)
                {

                    //Insert Into Activity Table
                    //ActivityBuilder activityBuilder = new ActivityBuilder();
                    //var activity = activityBuilder
                    //    .AddHeader(sPaymentStatus_Core, invoiceModel.Invoice_Date, statusClass)
                    //    .AddBody("Payment", sInvoice_ID, invoiceModel.CustomerName, invoiceModel.InvoiceStatus_Disp, invoiceModel.Amount);

                    //string text = activity.ToString();

                    //string textHtml = activity.ToStringHtml();

                    //var model = new ActivityModel
                    //{
                    //    CreateBy = sCreatedBY,
                    //    UserID = invoiceModel.CreatedBY,
                    //    //BusinessID = invoiceModel.BusinessID,
                    //    Status = invoiceModel.InvoiceStatus_Disp,
                    //    Text = text,
                    //    HtmlText = textHtml
                    //};

                    //_activityProcessor.Create(new ActivityModel
                    //{
                    //    CreatedBY = sCreatedBY,
                    //    Activity_Date = sPaymentDate,
                    //    Agency_ID = sAgency_ID,
                    //    Customer_ID = sCustomer_ID,
                    //    PaymentAmount = sPaymentAmount,
                    //    Category_Core = sCategory_Core,
                    //    Invoice_ID = sInvoice_ID,
                    //    InvoiceStatus_Core = sPaymentStatus_Core
                    //});
                    output.IsSucceeded = true;
                    output.ReturnMessage = XmlProcessor.ReadXmlFile("PTS501");
                }
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public PaymentModel Update(PaymentModel model)
        {
            try
            {
                PaymentModel output = new PaymentModel { IsSucceeded = false };
                int result = _paymentRepo.Update(model);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = XmlProcessor.ReadXmlFile("NDS902");
                }
                output.ReturnMessage = XmlProcessor.ReadXmlFile("NoteUpdateFail");
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public PaymentModel Activate(int businessId, int id, bool active)
        {
            try
            {
                PaymentModel output = new PaymentModel { IsSucceeded = false };
                int result = _paymentRepo.Activate(businessId, id, active);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = XmlProcessor.ReadXmlFile("NDS902");
                }
                output.ReturnMessage = XmlProcessor.ReadXmlFile("NoteActivateFail");
                return output;

            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public PaymentModel Delete(int businessId, int id)
        {
            try
            {
                PaymentModel output = new PaymentModel { IsSucceeded = false };
                int result = _paymentRepo.Delete(businessId, id);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = XmlProcessor.ReadXmlFile("NDS902");
                }
                output.ReturnMessage = XmlProcessor.ReadXmlFile("NoteDeleteFail");
                return output;

            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
    }
}
