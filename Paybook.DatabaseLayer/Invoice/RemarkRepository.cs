using Paybook.ServiceLayer.Logger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.DatabaseLayer.Invoice
{
    public interface IRemarkRepository
    {
        string InvoiceRemark_Select(string sInvoice_ID);
        bool Invoice_RemarkUpdate(string sInvoice_ID, string sRemark);
    }

    public class RemarkRepository : IRemarkRepository
    {
        private readonly ILogger _logger;
        private readonly IDbContext _dbContext;

        public RemarkRepository()
        {
            _logger = LoggerFactory.Instance;
            _dbContext = DbContextFactory.Instance;
        }

        public bool Invoice_RemarkUpdate(string sInvoice_ID, string sRemark)
        {
            try
            {
                var parameters = new List<Parameter>
                {
                    new Parameter("sInvoice_ID",sInvoice_ID),
                    new Parameter("sRemark", sRemark)
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
        public string InvoiceRemark_Select(string sInvoice_ID)
        {
            try
            {
                var parameters = new List<Parameter>
                {
                    new Parameter("sInvoice_ID",sInvoice_ID)
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
