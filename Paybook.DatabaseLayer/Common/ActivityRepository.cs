using Paybook.DatabaseLayer.Invoice;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.DatabaseLayer.Common
{
    public interface IActivityRepository
    {
        List<ActivityModel> GetAllByPage(int businessId, int page, string search, string orderBy);
        int Create(ActivityModel activityModel);
    }

    public class ActivityRepository : IActivityRepository
    {
        private readonly ILogger _logger;
        private readonly IDbContext _dbContext;

        public ActivityRepository()
        {
            _logger = LoggerFactory.Instance;
            _dbContext = DbContextFactory.Instance;
        }

        public List<ActivityModel> GetAllByPage(int businessId, int page, string search, string orderBy)
        {
            try
            {
                var p = new { BusinessId = businessId, Page = page, Search = search, OrderBy = orderBy };

                var result = _dbContext.LoadData<ActivityModel, dynamic>("sps_Activities_GetAllByPage", p);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }

        public int Create(ActivityModel activityModel)
        {
            try
            {
                int result = _dbContext.SaveData("spi_Activities_Insert", activityModel);

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
