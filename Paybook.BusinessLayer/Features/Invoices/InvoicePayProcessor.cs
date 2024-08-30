using System;
using System.Collections.Generic;
using Paybook.BusinessLayer.Abstracts.Invoices;
using Paybook.DatabaseLayer.Abstracts.Invoices;
using Paybook.DatabaseLayer.Features.Invoices;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models.Invoices;
using Paybook.ServiceLayer.Xml;

namespace Paybook.BusinessLayer.Invoice
{
    public class InvoicePayProcessor : IInvoicePayProcessor
    {
        private readonly ILogger _logger;
        private readonly IInvoicePayRepository _payRepo;
       
        public InvoicePayProcessor(
            ILogger logger)
        {
            _logger = logger;
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
