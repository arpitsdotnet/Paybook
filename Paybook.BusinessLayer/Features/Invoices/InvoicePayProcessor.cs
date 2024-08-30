using System;
using System.Collections.Generic;
using Paybook.BusinessLayer.Abstracts.Customers;
using Paybook.BusinessLayer.Abstracts.Invoices;
using Paybook.DatabaseLayer.Invoice;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models.Invoices;
using Paybook.ServiceLayer.Xml;

namespace Paybook.BusinessLayer.Invoice
{
    public class InvoicePayProcessor : IInvoicePayProcessor
    {
        private readonly ILogger _logger;
        private readonly IBusinessProcessor _business;
        private readonly IInvoicePayRepository _payRepo;
       
        public InvoicePayProcessor(
            ILogger logger,
            IBusinessProcessor business)
        {
            _logger = logger;
            _business = business;
            _payRepo = new InvoicePayRepository();
        }

        public List<InvoicePayModel> GetAllByInvoiceId(int businessId, int invoiceId, int page, string search, string orderBy)
        {
            try
            {
                return _payRepo.GetAllByInvoiceId(businessId, invoiceId, page, search, orderBy);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public decimal GetPaidTotalByInvoiceId(int businessId, int invoiceId)
        {
            try
            {
                return _payRepo.GetPaidTotalByInvoiceId(businessId, invoiceId);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public InvoicePayModel Create(InvoicePayModel model)
        {
            try
            {
                var output = new InvoicePayModel { IsSucceeded = false };

                int result = _payRepo.Create(model);
                if (result <= 0)
                {
                    output.IsSucceeded = false;
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.Payment, MStatus.InsertFailure);
                    return output;
                }
                output.IsSucceeded = true;
                output.ReturnMessage = XmlMessageHelper.Get(MTypes.Payment, MStatus.InsertSuccess);
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
