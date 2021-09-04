using Paybook.BusinessLayer.Business;
using Paybook.BusinessLayer.Common;
using Paybook.BusinessLayer.Setting;
using Paybook.DatabaseLayer.Invoice;
using Paybook.ServiceLayer.Constants;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Services;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Data;

namespace Paybook.BusinessLayer.Invoice
{

    public interface IInvoiceProcessor : IBaseProcessor<InvoiceModel>
    {
        InvoiceCountersModel GetAllCounters(int businessId);
        List<InvoiceModel> GetAllByClientId(int businessId, int clientId);
        string Invoices_Update_CloseStatus(string sParticular, string sCreatedBY, string sCategory_Core, string sStatus_Core, string sReason, string sCustomer_ID);
        string Activity_Insert_Overdue(string sCreatedBY, string sStatus_Core);
        InvoiceModel CreateWithServices(InvoiceModel invoice, List<InvoiceServiceModel> services);
        InvoiceModel UpdateVoid(int businessId, int invoiceId);
        InvoiceModel UpdateWriteOff(int businessId, int invoiceId);

        int GetAllPagesCount(int businessId, int page, string search, string orderBy);
    }

    public partial class InvoiceProcessor : IInvoiceProcessor
    {
        private readonly ILogger _logger;
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IBusinessProcessor _business;
        private readonly ICategoryProcessor _category;

        public InvoiceProcessor()
        {
            _logger = LoggerFactory.Instance;
            _invoiceRepo = new InvoiceRepository();
            _business = new BusinessProcessor();
            _category = new CategoryProcessor();
        }

        public InvoiceCountersModel GetAllCounters(int businessId)
        {
            try
            {
                return _invoiceRepo.GetAllCounters(businessId);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public List<InvoiceModel> GetAllByClientId(int businessId, int clientId)
        {
            try
            {
                return _invoiceRepo.GetAllByClientId(businessId, clientId);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

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
                    return Messages.Get(MTypes.Invoice, MStatus.UpdateSuccess);
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public string Activity_Insert_Overdue(string sCreatedBY, string sStatus_Core)
        {
            try
            {
                DataTable dt = _invoiceRepo.GetByStatusOverdue();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        InvoiceModel model = new InvoiceModel
                        {

                        };

                        // TODO
                        //_activityRepo.Create(activityModel);
                        _invoiceRepo.Invoices_Update_OverdueStatus(model.InvoiceNumber, "", "");
                    }
                }

                return Messages.Get(MTypes.Invoice, MStatus.InsertFailure);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
            //try
            //{
            //    bool result = _invoiceRepo.CreateInvoiceActivity(sCreatedBY, sStatus_Core);
            //    if (result)
            //    {
            //        return XmlProcessor.ReadXmlFile("CUW110");
            //    }
            //    return string.Empty;
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(_logger.MethodName, ex);

            //    throw;
            //}
        }
        public InvoiceModel CreateWithServices(InvoiceModel invoice, List<InvoiceServiceModel> services)
        {
            try
            {
                var output = new InvoiceModel();

                var status = _category.GetByCore(invoice.BusinessId, InvoiceStatusConst.Open);

                invoice.StatusId = status.Id;

                int result = _invoiceRepo.CreateWithServices(invoice, services);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = Messages.Get(MTypes.Invoice, MStatus.InsertSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = Messages.Get(MTypes.Invoice, MStatus.InsertFailure);
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public InvoiceModel UpdateVoid(int businessId, int invoiceId)
        {
            try
            {
                var output = new InvoiceModel();
                int result = _invoiceRepo.UpdateVoid(businessId, invoiceId);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = Messages.Get(MTypes.Invoice, MStatus.ActivateSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = Messages.Get(MTypes.Invoice, MStatus.ActivateFailure);
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public InvoiceModel UpdateWriteOff(int businessId, int invoiceId)
        {
            try
            {
                var output = new InvoiceModel();
                int result = _invoiceRepo.UpdateWriteOff(businessId, invoiceId);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = Messages.Get(MTypes.Invoice, MStatus.ActivateSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = Messages.Get(MTypes.Invoice, MStatus.ActivateFailure);
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }


        public int GetAllPagesCount(int businessId, int page, string search, string orderBy)
        {
            try
            {
                return _invoiceRepo.GetAllPagesCount(businessId, page, search, orderBy);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public List<InvoiceModel> GetAllByPage(int businessId, int page, string search, string orderBy)
        {
            try
            {
                return _invoiceRepo.GetAllByPage(businessId, page, search, orderBy);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public InvoiceModel GetById(int businessId, int id)
        {
            try
            {
                return _invoiceRepo.GetById(businessId, id);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public InvoiceModel Create(InvoiceModel model)
        {
            try
            {
                var output = new InvoiceModel { IsSucceeded = false };

                var business = _business.GetSelectedByUsername(model.CreateBy);

                model.BusinessId = business.Id;

                int result = _invoiceRepo.Create(model);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = Messages.Get(MTypes.Invoice, MStatus.InsertSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = Messages.Get(MTypes.Invoice, MStatus.InsertFailure);
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public InvoiceModel Update(InvoiceModel model)
        {
            try
            {
                var output = new InvoiceModel { IsSucceeded = false };
                int result = _invoiceRepo.Update(model);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = Messages.Get(MTypes.Invoice, MStatus.UpdateSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = Messages.Get(MTypes.Invoice, MStatus.UpdateFailure);
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public InvoiceModel Activate(int businessId, string username, int id, bool active)
        {
            try
            {
                var output = new InvoiceModel();
                int result = _invoiceRepo.Activate(businessId, username, id, active);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    if (active)
                        output.ReturnMessage = Messages.Get(MTypes.Invoice, MStatus.ActivateSuccess);
                    else
                        output.ReturnMessage = Messages.Get(MTypes.Invoice, MStatus.DeactivateSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                if (active)
                    output.ReturnMessage = Messages.Get(MTypes.Invoice, MStatus.ActivateFailure);
                else
                    output.ReturnMessage = Messages.Get(MTypes.Invoice, MStatus.DeactivateFailure);
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public InvoiceModel Delete(int businessId, string username, int id)
        {
            try
            {
                var output = new InvoiceModel();
                int result = _invoiceRepo.Delete(businessId, username, id);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = Messages.Get(MTypes.Invoice, MStatus.DeleteSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = Messages.Get(MTypes.Invoice, MStatus.DeleteFailure);
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
