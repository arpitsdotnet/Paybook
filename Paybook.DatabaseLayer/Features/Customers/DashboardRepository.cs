using System.Collections.Generic;
using System.Linq;
using Paybook.DatabaseLayer.Abstracts.Customers;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Models.Dashboards;
using Paybook.ServiceLayer.Models.Invoices;

namespace Paybook.DatabaseLayer.Features.Customers
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly IDbContext _dbContext;

        public DashboardRepository()
        {
            _dbContext = DbContextFactory.Instance;
        }
        public DashboardCountersModel GetAllCounters(int businessId)
        {
            var p = new { BusinessId = businessId };

            var result = _dbContext.LoadData<DashboardCountersModel, dynamic>("sps_Dashboard_GetCounters", p);
            //dt = _dbContext.LoadDataByProcedure("sps_Dashboard_SelectCounts", null);

            return result.FirstOrDefault();
        }
        public List<DashboardChartModel> GetClientCounterByDays(int businessId, int days = 7)
        {
            var p = new { BusinessId = businessId, Days = days };

            var result = _dbContext.LoadData<DashboardChartModel, dynamic>("sps_Dashboard_GetClientCountByDays", p);
            //return _dbContext.LoadDataByProcedure("sps_CustomerChart", null);

            return result;
        }
        public List<DashboardChartModel> GetInvoiceCountAndTotalByDays(int businessId, int days = 7)
        {
            var p = new { BusinessId = businessId, Days = days };

            var result = _dbContext.LoadData<DashboardChartModel, dynamic>("sps_Dashboard_GetInvoiceCountAndAmountByDays", p);
            //return _dbContext.LoadDataByProcedure("sps_Dashboard_GetInvoiceCountByLastWeek", null);

            return result;
        }
        public List<DashboardChartModel> GetPaymentCountAndTotalByDays(int businessId, int days = 7)
        {
            var p = new { BusinessId = businessId, Days = days };

            var result = _dbContext.LoadData<DashboardChartModel, dynamic>("sps_Dashboard_GetPaymentCountAndAmountByDays", p);
            //return _dbContext.LoadDataByProcedure("sps_Dashboard_GetInvoiceAmountsByLastWeek", null);

            return result;
        }
        public List<DashboardChartModel> GetPaymentTotalByLast10(int businessId)
        {
            var p = new { BusinessId = businessId };

            var result = _dbContext.LoadData<DashboardChartModel, dynamic>("sps_Dashboard_GetPaymentsLast10", p);
            //return _dbContext.LoadDataByProcedure("sps_Dashboard_GetPaymentsLast20", null);

            return result;
        }
        public int GetInvoiceCountByDays(int businessId, int days = 7)
        {
            var p = new { BusinessId = businessId, Days = days };
            var result = _dbContext.LoadData<int, dynamic>("sps_Dashboard_GetInvoiceCountByDays", p);

            return result.FirstOrDefault();
        }
        public int GetPaymentCountByDays(int businessId, int days = 7)
        {
            var p = new { BusinessId = businessId, Days = days };
            var result = _dbContext.LoadData<int, dynamic>("sps_Dashboard_GetPaymentCountByDays", p);

            return result.FirstOrDefault();
        }

        public List<InvoiceModel> GetLast5Invoices(int businessId)
        {
            var p = new { BusinessId = businessId };
            var result = _dbContext.LoadData<InvoiceModel, dynamic>("sps_Dashboard_GetLast5Invoices", p);

            return result;
        }
        public List<PaymentModel> GetLast5Payments(int businessId)
        {
            var p = new { BusinessId = businessId };
            var result = _dbContext.LoadData<PaymentModel, dynamic>("sps_Dashboard_GetLast5Payments", p);

            return result;
        }
    }
}
