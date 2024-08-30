using System;
using System.Collections.Generic;
using Paybook.BusinessLayer.Abstracts.Admins;
using Paybook.BusinessLayer.Abstracts.Invoices;
using Paybook.DatabaseLayer.Abstracts.Invoices;
using Paybook.DatabaseLayer.Features.Invoices;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models.Invoices;

namespace Paybook.BusinessLayer.Invoice
{
    public class InvoiceServiceProcessor : IInvoiceServiceProcessor
    {
        private readonly ILogger _logger;
        private readonly IBusinessProcessor _business;
        private readonly IInvoiceServiceRepository _invoiceServiceRepo;

        public InvoiceServiceProcessor(
            ILogger logger,
            IBusinessProcessor business)
        {
            _logger = logger;
            _business = business;
            _invoiceServiceRepo = new InvoiceServiceRepository();
        }

        public bool IsExist(string businessId, int id)
        {
            try
            {
                return _invoiceServiceRepo.IsExist(businessId, id);
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

                return _invoiceServiceRepo.GetAllByInvoiceId(business.Id, invoiceId);
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
                return _invoiceServiceRepo.GetById(businessId, id);
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
                return _invoiceServiceRepo.Create(model);
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
                return _invoiceServiceRepo.Update(model);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public int Activate(int businessId, string username, int id, bool active)
        {
            try
            {
                return _invoiceServiceRepo.Activate(businessId, username, id, active);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public int Delete(int businessId, string username, int id)
        {
            try
            {
                return _invoiceServiceRepo.Delete(businessId, username, id);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
    }
}
