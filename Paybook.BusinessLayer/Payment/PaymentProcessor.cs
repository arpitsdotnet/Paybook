using Paybook.BusinessLayer.Business;
using Paybook.BusinessLayer.Client;
using Paybook.BusinessLayer.Common;
using Paybook.BusinessLayer.Invoice;
using Paybook.DatabaseLayer.Payment;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;

namespace Paybook.BusinessLayer.Payment
{
    public interface IPaymentProcessor : IBaseProcessor<PaymentModel>
    {
        string Payments_SelectMonthsales();
        List<PaymentModel> GetAllByInvoiceId(string username, int invoiceId, int page, string search, string orderBy);
        List<PaymentModel> GetAllByClientId(string username, int clientId);
        decimal GetPaidAmountByInvoiceId(string username, int invoiceId);
        PaymentModel Revert(string username, int id);
    }

    public class PaymentProcessor : IPaymentProcessor
    {
        private readonly ILogger _logger;
        private readonly IPaymentRepository _paymentRepo;
        private readonly IBusinessProcessor _business;
        private readonly ILastSavedNumberProcessor _lastSavedIdProcessor;
        private readonly IClientProcessor _clientProcessor;
        private readonly IInvoiceProcessor _invoiceProcessor;

        public PaymentProcessor()
        {
            _logger = LoggerFactory.Instance;
            _paymentRepo = new PaymentRepository();

            _business = new BusinessProcessor();
            _lastSavedIdProcessor = new LastSavedNumberProcessor();
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
        public List<PaymentModel> GetAllByInvoiceId(string username, int invoiceId, int page, string search, string orderBy)
        {
            try
            {
                var business = _business.GetSelectedByUsername(username);

                return _paymentRepo.GetAllByInvoiceId(business.Id, invoiceId, page, search, orderBy);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public List<PaymentModel> GetAllByClientId(string username, int clientId)
        {
            try
            {
                var business = _business.GetSelectedByUsername(username);

                return _paymentRepo.GetAllByClientId(business.Id, clientId);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public decimal GetPaidAmountByInvoiceId(string username, int invoiceId)
        {
            try
            {
                var business = _business.GetSelectedByUsername(username);

                return _paymentRepo.GetPaidAmountByInvoiceId(business.Id, invoiceId);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public PaymentModel Revert(string username, int id)
        {
            try
            {
                var business = _business.GetSelectedByUsername(username);

                var output = new PaymentModel();
                var result = _paymentRepo.Revert(business.Id, id);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = Messages.Get(MTypes.Payment, MStatusPayment.RevertSuccess);
                    return output;
                }
                output.IsSucceeded = false;
                output.ReturnMessage = Messages.Get(MTypes.Payment, MStatusPayment.RevertFailure);
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }

        public List<PaymentModel> GetAllByPage(string username, int page, string search, string orderBy)
        {
            try
            {
                var business = _business.GetSelectedByUsername(username);

                return _paymentRepo.GetAllByPage(business.Id, page, search, orderBy);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public PaymentModel GetById(string username, int id)
        {
            try
            {
                var business = _business.GetSelectedByUsername(username);

                return _paymentRepo.GetById(business.Id, id);
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
                var business = _business.GetSelectedByUsername(model.CreateBy);

                model.BusinessId = business.Id;

                var output = new PaymentModel();
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
                    output.ReturnMessage = Messages.Get(MTypes.Payment, MStatus.InsertSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = Messages.Get(MTypes.Payment, MStatus.InsertFailure);
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
                var output = new PaymentModel();
                int result = _paymentRepo.Update(model);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = Messages.Get(MTypes.Payment, MStatus.UpdateSuccess);
                    return output;
                }
                output.IsSucceeded = false;
                output.ReturnMessage = Messages.Get(MTypes.Payment, MStatus.UpdateFailure);
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public PaymentModel Activate(string username, int id, bool active)
        {
            try
            {
                var business = _business.GetSelectedByUsername(username);

                var output = new PaymentModel();
                int result = _paymentRepo.Activate(business.Id, id, active);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    if (active == true)
                        output.ReturnMessage = Messages.Get(MTypes.Payment, MStatus.ActivateSuccess);
                    else
                        output.ReturnMessage = Messages.Get(MTypes.Payment, MStatus.DeactivateSuccess);
                    return output;
                }
                output.IsSucceeded = false;
                if (active == true)
                    output.ReturnMessage = Messages.Get(MTypes.Payment, MStatus.ActivateFailure);
                else
                    output.ReturnMessage = Messages.Get(MTypes.Payment, MStatus.DeactivateFailure);
                return output;

            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public PaymentModel Delete(string username, int id)
        {
            try
            {
                var business = _business.GetSelectedByUsername(username);

                var output = new PaymentModel();
                int result = _paymentRepo.Delete(business.Id, id);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = Messages.Get(MTypes.Payment, MStatus.DeleteSuccess);
                    return output;
                }
                output.IsSucceeded = false;
                output.ReturnMessage = Messages.Get(MTypes.Payment, MStatus.DeleteFailure);
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
