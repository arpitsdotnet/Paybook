using Paybook.BusinessLayer.Business;
using Paybook.DatabaseLayer.Invoice;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paybook.BusinessLayer.Invoice
{
    public interface IInvoicePayProcessor
    {
        List<InvoicePayModel> GetAllByInvoiceId(int businessId, int invoiceId, int page, string search, string orderBy);
        decimal GetPaidTotalByInvoiceId(int businessId, int invoiceId);
        InvoicePayModel Create(InvoicePayModel model);
    }

    public class InvoicePayProcessor : IInvoicePayProcessor
    {
        private readonly ILogger _logger;
        private readonly IInvoicePayRepository _payRepo;
        private readonly IBusinessProcessor _business;
        public InvoicePayProcessor()
        {
            _logger = LoggerFactory.Instance;
            _payRepo = new InvoicePayRepository();
            _business = new BusinessProcessor();
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
                if (result > 0)
                {
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
    }
}
