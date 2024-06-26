﻿using Paybook.DatabaseLayer.Common;
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
    public interface IPaymentRepository : IBaseRepository<PaymentModel>
    {
        string Payments_SelectMonthsales();
        List<PaymentModel> GetAllByInvoiceId(int businessId, int invoiceId, int page, string search, string orderBy);
        List<PaymentModel> GetAllByClientId(int businessId, int clientId);
        int Revert(int businessId, string username, int id);
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

        [Obsolete]
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
                _logger.Error(_logger.GetMethodName(), ex);

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
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public List<PaymentModel> GetAllByClientId(int businessId, int clientId)
        {
            try
            {
                var p = new { BusinessId = businessId, ClientId = clientId };

                var result = _dbContext.LoadData<PaymentModel, dynamic>("sps_Payments_GetAllByClientId", p);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public int Revert(int businessId, string username, int id)
        {
            try
            {
                var p = new { BusinessId = businessId, Username = username, Id = id };

                var result = _dbContext.SaveData("sps_Payments_Revert", p);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
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
                _logger.Error(_logger.GetMethodName(), ex);
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
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public int Create(PaymentModel model)
        {
            try
            {
                var p = new { model.BusinessId, model.CreateBy, model.ClientId, model.TransactionId, model.PaymentDate, model.Method, model.Amount };
                var result = _dbContext.SaveDataOutParam("spi_Payments_Insert", p, out int paymentId, DbType.Int32, null, "Id");

                model.Id = paymentId;

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
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
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public int Activate(int businessId, string username, int id, bool active)
        {
            try
            {
                var p = new { BusinessId = businessId, Username = username, Id = id, IsActive = active };

                var result = _dbContext.SaveData("spu_Payments_Activate", p);

                return result;
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
                var p = new { BusinessId = businessId, Username = username, Id = id };

                var result = _dbContext.SaveData("spd_Payments_Delete", p);

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
