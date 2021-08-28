using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.DatabaseLayer.Common
{
    public interface IDashboardRepository
    {
        DashboardCountersModel GetAllCounters(int businessId);
        List<DashboardChartModel> GetClientCounterByDays(int businessId, int days = 7);
        List<DashboardChartModel> GetInvoiceCountAndTotalByDays(int businessId, int days = 7);
        List<DashboardChartModel> GetPaymentCountAndTotalByDays(int businessId, int days = 7);
        List<DashboardChartModel> GetPaymentTotalByLast10(int businessId);

        int GetInvoiceCountByDays(int businessId, int days = 7);
        int GetPaymentCountByDays(int businessId, int days = 7);

        List<InvoiceModel> GetLast5Invoices(int businessId);
        List<PaymentModel> GetLast5Payments(int businessId);
    }

    public class DashboardRepository : IDashboardRepository
    {
        private readonly ILogger _logger;
        private readonly IDbContext _dbContext;

        public DashboardRepository()
        {
            _logger = LoggerFactory.Instance;
            _dbContext = DbContextFactory.Instance;
        }
        public DashboardCountersModel GetAllCounters(int businessId)
        {
            try
            {
                var p = new { BusinessId = businessId };

                var result = _dbContext.LoadData<DashboardCountersModel, dynamic>("sps_Dashboard_GetCounters", p);
                //dt = _dbContext.LoadDataByProcedure("sps_Dashboard_SelectCounts", null);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public List<DashboardChartModel> GetClientCounterByDays(int businessId, int days = 7)
        {
            try
            {
                var p = new { BusinessId = businessId, Days = days };

                var result = _dbContext.LoadData<DashboardChartModel, dynamic>("sps_Dashboard_GetClientCountByDays", p);
                //return _dbContext.LoadDataByProcedure("sps_CustomerChart", null);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public List<DashboardChartModel> GetInvoiceCountAndTotalByDays(int businessId, int days = 7)
        {
            try
            {
                var p = new { BusinessId = businessId, Days = days };

                var result = _dbContext.LoadData<DashboardChartModel, dynamic>("sps_Dashboard_GetInvoiceCountAndAmountByDays", p);
                //return _dbContext.LoadDataByProcedure("sps_Dashboard_GetInvoiceCountByLastWeek", null);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public List<DashboardChartModel> GetPaymentCountAndTotalByDays(int businessId, int days = 7)
        {
            try
            {
                var p = new { BusinessId = businessId, Days = days };

                var result = _dbContext.LoadData<DashboardChartModel, dynamic>("sps_Dashboard_GetPaymentCountAndAmountByDays", p);
                //return _dbContext.LoadDataByProcedure("sps_Dashboard_GetInvoiceAmountsByLastWeek", null);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public List<DashboardChartModel> GetPaymentTotalByLast10(int businessId)
        {
            try
            {
                var p = new { BusinessId = businessId };

                var result = _dbContext.LoadData<DashboardChartModel, dynamic>("sps_Dashboard_GetPaymentsLast10", p);
                //return _dbContext.LoadDataByProcedure("sps_Dashboard_GetPaymentsLast20", null);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public int GetInvoiceCountByDays(int businessId, int days = 7)
        {
            try
            {
                var p = new { BusinessId = businessId, Days = days };
                var result = _dbContext.LoadData<int, dynamic>("sps_Dashboard_GetInvoiceCountByDays", p);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public int GetPaymentCountByDays(int businessId, int days = 7)
        {
            try
            {
                var p = new { BusinessId = businessId, Days = days };
                var result = _dbContext.LoadData<int, dynamic>("sps_Dashboard_GetPaymentCountByDays", p);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }

        public List<InvoiceModel> GetLast5Invoices(int businessId)
        {
            try
            {
                var p = new { BusinessId = businessId};
                var result = _dbContext.LoadData<InvoiceModel, dynamic>("sps_Dashboard_GetLast5Invoices", p);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public List<PaymentModel> GetLast5Payments(int businessId)
        {
            try
            {
                var p = new { BusinessId = businessId };
                var result = _dbContext.LoadData<PaymentModel, dynamic>("sps_Dashboard_GetLast5Payments", p);

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
