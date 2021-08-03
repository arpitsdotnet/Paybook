using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.DatabaseLayer.Common
{
    public interface IChartRepository
    {
        DataTable Customer_Chart();
    }

    public class ChartRepository : IChartRepository
    {
        private readonly ILogger _logger;
        private readonly IDbContext _dbContext;

        public ChartRepository()
        {
            _logger = FileLogger.Instance;
            _dbContext = DbContextFactory.Instance;
        }

        public DataTable Customer_Chart()
        {
            try
            {
                return _dbContext.LoadDataByProcedure("sps_CustomerChart", null);

            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
    }
}
