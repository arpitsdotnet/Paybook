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
        List<DashboardCustomerChartModel> GetClientCountByDays(int businessId, int days = 7);
        List<DashboardCustomerChartModel> GetInvoiceCountAndAmountByDays(int businessId, int days = 7);
        List<DashboardCustomerChartModel> GetPaymentCountAndAmountByDays(int businessId, int days = 7);
        List<DashboardCustomerChartModel> GetPaymentAmountByLast10(int businessId);

        [Obsolete]
        DataTable Invoices_SelectCount();

        [Obsolete]
        DataTable Payments_SelectCount();
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
        public List<DashboardCustomerChartModel> GetClientCountByDays(int businessId, int days = 7)
        {
            try
            {
                var p = new { BusinessId = businessId, Days = days };

                var result = _dbContext.LoadData<DashboardCustomerChartModel, dynamic>("sps_Dashboard_GetClientCountByDays", p);
                //return _dbContext.LoadDataByProcedure("sps_CustomerChart", null);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public List<DashboardCustomerChartModel> GetInvoiceCountAndAmountByDays(int businessId, int days = 7)
        {
            try
            {
                var p = new { BusinessId = businessId, Days = days };

                var result = _dbContext.LoadData<DashboardCustomerChartModel, dynamic>("sps_Dashboard_GetInvoiceCountAndAmountByDays", p);
                //return _dbContext.LoadDataByProcedure("sps_Dashboard_GetInvoiceCountByLastWeek", null);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public List<DashboardCustomerChartModel> GetPaymentCountAndAmountByDays(int businessId, int days = 7)
        {
            try
            {
                var p = new { BusinessId = businessId, Days = days };

                var result = _dbContext.LoadData<DashboardCustomerChartModel, dynamic>("sps_Dashboard_GetPaymentCountAndAmountByDays", p);
                //return _dbContext.LoadDataByProcedure("sps_Dashboard_GetInvoiceAmountsByLastWeek", null);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public List<DashboardCustomerChartModel> GetPaymentAmountByLast10(int businessId)
        {
            try
            {
                var p = new { BusinessId = businessId };

                var result = _dbContext.LoadData<DashboardCustomerChartModel, dynamic>("sps_Dashboard_GetPaymentsLast10", p);
                //return _dbContext.LoadDataByProcedure("sps_Dashboard_GetPaymentsLast20", null);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public DataTable Invoices_SelectCount()
        {
            try
            {
                return new DataTable();// _dbContext.LoadDataByProcedure("sps_Invoices_SelectCount", null);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public DataTable Payments_SelectCount()
        {
            try
            {
                return new DataTable();//_dbContext.LoadDataByProcedure("sps_Payments_SelectCount", null);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
    }
}
