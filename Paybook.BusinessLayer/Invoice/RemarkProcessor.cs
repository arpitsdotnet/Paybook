using Paybook.DatabaseLayer;
using Paybook.DatabaseLayer.Common;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paybook.BusinessLayer.Invoice
{
    public interface IRemarkProcessor
    {
        string InvoiceRemark_Select(string sInvoice_ID);
        string Invoice_RemarkUpdate(string sInvoice_ID, string sRemark);
    }
    public class RemarkProcessor : IRemarkProcessor
    {
        private readonly ILogger _logger;
        private readonly IRemarkRepository _remarkRepo;

        public RemarkProcessor()
        {
            _logger = LoggerFactory.Instance;
            _remarkRepo = new RemarkRepository();
        }
        public string InvoiceRemark_Select(string sInvoice_ID)
        {
            try
            {
                return _remarkRepo.InvoiceRemark_Select(sInvoice_ID);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public string Invoice_RemarkUpdate(string sInvoice_ID, string sRemark)
        {
            try
            {
                bool result = _remarkRepo.Invoice_RemarkUpdate(sInvoice_ID, sRemark);
                if (result)
                    return XmlProcessor.ReadXmlFile("INS305");

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
