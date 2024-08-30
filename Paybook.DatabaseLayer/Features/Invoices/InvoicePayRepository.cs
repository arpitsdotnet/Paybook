using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Paybook.DatabaseLayer.Abstracts.Invoices;
using Paybook.ServiceLayer.Models.Invoices;

namespace Paybook.DatabaseLayer.Features.Invoices
{
    public class InvoicePayRepository : IInvoicePayRepository
    {
        private readonly IDbContext _dbContext;

        public InvoicePayRepository()
        {
            _dbContext = DbContextFactory.Instance;
        }

        public List<InvoicePayModel> GetAllByInvoiceId(int businessId, int invoiceId, int page, string search, string orderBy)
        {
            var p = new { BusinessId = businessId, InvoiceId = invoiceId, Page = page, Search = search, OrderBy = orderBy };

            var result = _dbContext.LoadData<InvoicePayModel, dynamic>("sps_InvoicePayments_GetAllByInvoiceId", p);

            return result;
        }
        public decimal GetPaidTotalByInvoiceId(int businessId, int invoiceId)
        {
            var p = new { BusinessId = businessId, InvoiceId = invoiceId };

            var result = _dbContext.LoadData<decimal?, dynamic>("sps_InvoicePayments_GetPaidTotalByInvoiceId", p);

            if (result.FirstOrDefault() != null)
                return result.FirstOrDefault().Value;

            return 0;
        }
        public int Create(InvoicePayModel model)
        {
            var p = new { model.BusinessId, model.CreateBy, model.InvoiceId, model.PayDate, model.PayAmount };

            var result = _dbContext.SaveDataOutParam("spi_InvoicePayments_Insert", p, out int payId, DbType.Int32, null, "Id");

            model.Id = payId;

            return result;
        }
    }
}
