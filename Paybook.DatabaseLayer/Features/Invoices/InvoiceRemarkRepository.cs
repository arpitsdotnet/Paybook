using Paybook.ServiceLayer.Logger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.DatabaseLayer.Features.Invoices
{
    public interface IInvoiceRemarkRepository
    {
        string GetById(string invoiceId);
        bool Update(string invoiceId, string remarks);
    }

    public class InvoiceRemarkRepository : IInvoiceRemarkRepository
    {
        private readonly ILogger _logger;
        private readonly IDbContext _dbContext;

        public RemarkRepository()
        {
            _logger = LoggerFactory.Instance;
            _dbContext = DbContextFactory.Instance;
        }

        public bool Update(string invoiceId, string remarks)
        {
            try
            {
                var parameters = new List<Parameter>
                {
                    new Parameter("sInvoice_ID",invoiceId),
                    new Parameter("sRemark", remarks)
                };

                _dbContext.LoadDataByProcedure("sps_InvoiceRemak_Update", parameters);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        public string GetById(string invoiceId)
        {
            try
            {
                var parameters = new List<Parameter>
                {
                    new Parameter("sInvoice_ID",invoiceId)
                };

                DataTable dt = _dbContext.LoadDataByProcedure("sps_InvoiceRemak_Select", parameters);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Remark"].ToString();
                }
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
