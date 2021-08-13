using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.DatabaseLayer.Common
{
    public interface ILastSavedNumberRepository
    {
        LastSavedNumberModel GetNewNumberByType(int businessId, string type);
        int Update(LastSavedNumberModel model);
    }

    public class LastSavedNumberRepository : ILastSavedNumberRepository
    {
        private readonly ILogger _logger;
        private readonly IDbContext _dbContext;

        public LastSavedNumberRepository()
        {
            _logger = LoggerFactory.Instance;
            _dbContext = DbContextFactory.Instance;
        }

        public LastSavedNumberModel GetNewNumberByType(int businessId, string type)
        {
            try
            {
                var p = new { BusinessId = businessId, Type = type };

                var result = _dbContext.LoadData<LastSavedNumberModel, dynamic>("sps_LastSavedNumbers_GetNewNumberByType", p);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }


        public int Update(LastSavedNumberModel model)
        {
            try
            {
                var result = _dbContext.SaveData("spu_LastSavedNumber_Update", model);
                //var result = _dbContext.SaveData("sps_LastSavedID_Update", model);

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
