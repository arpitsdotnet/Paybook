using Paybook.DatabaseLayer.Common;
using Paybook.ServiceLayer;
using Paybook.ServiceLayer.Constants;
using Paybook.ServiceLayer.Extensions;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.DatabaseLayer.Payment
{
    public interface IPaymentRepository : IRepository<PaymentModel>
    {
        string Payments_SelectMonthsales();
        List<PaymentModel> GetAllByInvoiceId(int businessId, int invoiceId, int page, string search, string orderBy);
    }

    public class PaymentRepository : IPaymentRepository
    {
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;

        public PaymentRepository()
        {
            _dbContext = DbContextFactory.Instance;
            _logger = LoggerFactory.Instance;
        }

        public string Payments_SelectMonthsales()
        {
            string TotalMonthSale = "";
            try
            {
                //List<Parameter> oParams = new List<Parameter>();
                //oParams.Add(new Parameter("sCategory_Core", "FISCAL_DATE"));
                //DataTable dt = _dbContext.LoadDataByProcedure("sps_SubCategories_SelectAll", oParams);

                //int iFiscalMonth = Convert.ToInt32(dt.Rows[0]["SubCategory_Disp"].ToString());
                //int iCurrentMonth = Convert.ToInt32(DateTime.Now.Month.ToString());
                //int iYear;
                //DateTime dDateStart, dDateEnd;

                ////If Fiscal Month is less than Current Month
                //if (iFiscalMonth <= iCurrentMonth)
                //{
                //    iYear = DateTime.Now.Year;
                //    dDateStart = new DateTime(iYear, iFiscalMonth, 1);
                //    dDateEnd = DateTime.Now;
                //}
                ////If Fiscal Month is greater than Current Month
                //else
                //{
                //    iYear = DateTime.Now.Year - 1;
                //    dDateStart = new DateTime(iYear, iFiscalMonth, 1);
                //    dDateEnd = DateTime.Now;
                //}
                //oParams.Clear();
                //oParams.Add(new Parameter("dDateStart", dDateStart.ToString("yyyy-MM-dd")));
                //oParams.Add(new Parameter("dDateEnd", dDateEnd.ToString("yyyy-MM-dd")));
                //dt.Clear();
                //dt = _dbContext.LoadDataByProcedure("sps_Paymets_SelectYearSales", oParams);
                //if (dt != null && dt.Rows.Count > 0)
                //{

                //    TotalMonthSale = dt.Rows[0]["SumOfPaymentAmount"].ToString();

                //}
            }

            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
            return TotalMonthSale;
        }
        public List<PaymentModel> GetAllByInvoiceId(int businessId, int invoiceId, int page, string search, string orderBy)
        {
            try
            {
                var p = new { BusinessId = businessId, InvoiceId = invoiceId, Page = page, Search = search, OrderBy = orderBy };

                var result = _dbContext.LoadData<PaymentModel, dynamic>("sps_Payments_GetAllByInvoiceId", p);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }

        public List<PaymentModel> GetAllByPage(int businessId, int page, string search, string orderBy)
        {
            try
            {
                var p = new { BusinessId = businessId, Page = page, Search = search, OrderBy = orderBy };

                var result = _dbContext.LoadData<PaymentModel, dynamic>("sps_Payments_GetAllByPage", p);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public PaymentModel GetById(int businessId, int id)
        {
            try
            {
                var p = new { BusinessId = businessId, Id = id };

                var result = _dbContext.LoadData<PaymentModel, dynamic>("sps_Payments_GetById", p);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public int Create(PaymentModel model)
        {
            try
            {
                var result = _dbContext.SaveDataOutParam("spi_Payments_Insert", model, out int paymentId, DbType.Int32, "Id");

                model.Id = paymentId;

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }

        }
        public int Update(PaymentModel model)
        {
            try
            {
                var result = _dbContext.SaveData("spu_Payments_Update", model);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public int Activate(int businessId, int id, bool active)
        {
            try
            {
                var p = new { BusinessId = businessId, Id = id, IsActive = active };

                var result = _dbContext.SaveData("spu_Payments_Activate", p);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public int Delete(int businessId, int id)
        {
            try
            {
                var p = new { BusinessId = businessId, Id = id };

                var result = _dbContext.SaveData("spd_Payments_Delete", p);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
    }
}
