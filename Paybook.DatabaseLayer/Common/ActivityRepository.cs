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
        int Create(ActivityModel model);
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
                var p = new { BusinessId = businessId, CreateBy = ""};

                var result = _dbContext.LoadData<ActivityModel, dynamic>("sps_Activities_GetAll", p);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }

        public int Create(ActivityModel model)
        {
            try
            {
                var p = new { model.BusinessId, model.CreateBy, model.Status, model.Text, model.TextHtml };
                int result = _dbContext.SaveData("spi_Activities_Insert", p);

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
