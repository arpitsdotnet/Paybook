using System;
using System.Collections.Generic;
using Paybook.BusinessLayer.Abstracts.Customers;
using Paybook.BusinessLayer.Abstracts.Payments;
using Paybook.DatabaseLayer.Payment;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;

namespace Paybook.BusinessLayer.Payment
{
    public class PaymentProcessor : IPaymentProcessor
    {
        private readonly ILogger _logger;
        private readonly IPaymentRepository _paymentRepo;
        private readonly IBusinessProcessor _business;

        public PaymentProcessor(
            ILogger logger,
            IBusinessProcessor business)
        {
            _logger = logger;
            _business = business;
            _paymentRepo = new PaymentRepository();
        }

        [Obsolete]
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
        public List<PaymentModel> GetAllByClientId(int businessId, int clientId)
        {
            try
            {
                return _paymentRepo.GetAllByClientId(businessId, clientId);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public PaymentModel Revert(int businessId, string username, int id)
        {
            try
            {
                var output = new PaymentModel();
                var result = _paymentRepo.Revert(businessId, username, id);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.Payment, MStatusPayment.RevertSuccess);
                    return output;
                }
                output.IsSucceeded = false;
                output.ReturnMessage = XmlMessageHelper.Get(MTypes.Payment, MStatusPayment.RevertFailure);
                return output;
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
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.Payment, MStatus.InsertSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = XmlMessageHelper.Get(MTypes.Payment, MStatus.InsertFailure);
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
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.Payment, MStatus.UpdateSuccess);
                    return output;
                }
                output.IsSucceeded = false;
                output.ReturnMessage = XmlMessageHelper.Get(MTypes.Payment, MStatus.UpdateFailure);
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public PaymentModel Activate(int businessId, string username, int id, bool active)
        {
            try
            {
                var output = new PaymentModel();
                int result = _paymentRepo.Activate(businessId, username, id, active);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    if (active == true)
                        output.ReturnMessage = XmlMessageHelper.Get(MTypes.Payment, MStatus.ActivateSuccess);
                    else
                        output.ReturnMessage = XmlMessageHelper.Get(MTypes.Payment, MStatus.DeactivateSuccess);
                    return output;
                }
                output.IsSucceeded = false;
                if (active == true)
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.Payment, MStatus.ActivateFailure);
                else
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.Payment, MStatus.DeactivateFailure);
                return output;

            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public PaymentModel Delete(int businessId, string username, int id)
        {
            try
            {
                var output = new PaymentModel();
                int result = _paymentRepo.Delete(businessId, username, id);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.Payment, MStatus.DeleteSuccess);
                    return output;
                }
                output.IsSucceeded = false;
                output.ReturnMessage = XmlMessageHelper.Get(MTypes.Payment, MStatus.DeleteFailure);
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
