using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paybook.DatabaseLayer.Invoice
{
    public interface IInvoicePayRepository
    {
        List<InvoicePayModel> GetAllByInvoiceId(int businessId, int invoiceId, int page, string search, string orderBy);
        decimal GetPaidTotalByInvoiceId(int businessId, int invoiceId);
        int Create(InvoicePayModel model);
    }

    public class InvoicePayRepository : IInvoicePayRepository
    {
        private readonly ILogger _logger;
        private readonly IDbContext _dbContext;

        public InvoicePayRepository()
        {
            _logger = LoggerFactory.Instance;
            _dbContext = DbContextFactory.Instance;
        }

        public List<InvoicePayModel> GetAllByInvoiceId(int businessId, int invoiceId, int page, string search, string orderBy)
        {
            try
            {
                var p = new { BusinessId = businessId, InvoiceId = invoiceId, Page = page, Search = search, OrderBy = orderBy };

                var result = _dbContext.LoadData<InvoicePayModel, dynamic>("sps_InvoicePayments_GetAllByInvoiceId", p);

                return result;
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
                var p = new { BusinessId = businessId, InvoiceId = invoiceId };

                var result = _dbContext.LoadData<decimal?, dynamic>("sps_InvoicePayments_GetPaidTotalByInvoiceId", p);

                if (result.FirstOrDefault() != null)
                    return result.FirstOrDefault().Value;

                return 0;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public int Create(InvoicePayModel model)
        {
            try
            {
                var p = new
                {
                    model.BusinessId,
                    model.CreateBy,
                    model.InvoiceId,
                    model.PayDate,
                    model.PayAmount
                };

                var result = _dbContext.SaveDataOutParam("spi_InvoicePayments_Insert", p, out int payId, DbType.Int32, null, "Id");

                model.Id = payId;

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }

        }
    }
}
