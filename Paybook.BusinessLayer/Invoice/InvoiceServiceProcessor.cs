using Paybook.BusinessLayer.Business;
using Paybook.DatabaseLayer.Invoice;
using Paybook.DatabaseLayer.Setting;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paybook.BusinessLayer.Invoice
{
    public interface IInvoiceServiceProcessor
    {
        List<InvoiceServiceModel> GetAllByInvoiceId(string username, int invoiceId);
        InvoiceServiceModel GetById(int businessId, int id);
        bool IsExist(string businessId, int id);
    }
    public class InvoiceServiceProcessor : IInvoiceServiceProcessor
    {
        private readonly ILogger _logger;
        private readonly IInvoiceServiceRepository _serviceRepo;
        private readonly IBusinessProcessor _business;

        public InvoiceServiceProcessor()
        {
            _logger = LoggerFactory.Instance;
            _serviceRepo = new InvoiceServiceRepository();
            _business = new BusinessProcessor();
        }
        public bool IsExist(string businessId, int id)
        {
            try
            {
                return _serviceRepo.IsExist(businessId, id);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }


        public List<InvoiceServiceModel> GetAllByInvoiceId(string username, int invoiceId)
        {
            try
            {
                var business = _business.GetSelectedByUsername(username);

                return _serviceRepo.GetAllByInvoiceId(business.Id, invoiceId);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public InvoiceServiceModel GetById(int businessId, int id)
        {
            try
            {
                return _serviceRepo.GetById(businessId, id);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public int Create(InvoiceServiceModel model)
        {
            try
            {
                return _serviceRepo.Create(model);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public int Update(InvoiceServiceModel model)
        {
            try
            {
                return _serviceRepo.Update(model);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public int Activate(int businessId, int id, bool active)
        {
            try
            {
                return _serviceRepo.Activate(businessId, id, active);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public int Delete(int businessId, int id)
        {
            try
            {
                return _serviceRepo.Delete(businessId, id);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
    }
}
