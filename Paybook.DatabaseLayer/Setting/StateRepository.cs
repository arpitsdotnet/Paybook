using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.DatabaseLayer.Setting
{
    public interface IStateRepository
    {
        List<StateMasterModel> GetAllByPage(int countryId, int page, string search, string orderBy);
        StateMasterModel GetById(int id);
        int Create(StateMasterModel model);
        int Update(StateMasterModel model);
        int Activate(int id, bool active);
        int Delete(int id);
    }

    public class StateRepository : IStateRepository
    {
        private readonly ILogger _logger;
        private readonly IDbContext _dbContext;

        public StateRepository()
        {
            _logger = LoggerFactory.Instance;
            _dbContext = DbContextFactory.Instance;
        }

        public List<StateMasterModel> GetAllByPage(int countryId, int page, string search, string orderBy)
        {
            try
            {
                var p = new { CountryId = countryId, Page = page, Search = search, OrderBy = orderBy };

                var result = _dbContext.LoadData<StateMasterModel, dynamic>("sps_StateMaster_GetAllByPage", p);
                //return _dbContext.LoadDataByProcedure("sps_Agency_SelectName", null);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public StateMasterModel GetById(int id)
        {
            try
            {
                var p = new { Id = id };

                var result = _dbContext.LoadData<StateMasterModel, dynamic>("sps_StateMaster_GetById", p);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public int Create(StateMasterModel model)
        {
            try
            {
                var result = _dbContext.SaveDataOutParam("spi_StateMaster_Insert", model, out int stateId, DbType.Int32, null, "Id");

                model.Id = stateId;
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }

        }
        public int Update(StateMasterModel model)
        {
            try
            {
                var result = _dbContext.SaveData("spu_StateMaster_Update", model);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public int Activate(int id, bool active)
        {
            try
            {
                var p = new { Id = id, IsActive = active };

                var result = _dbContext.SaveData("spu_StateMaster_Activate", p);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public int Delete(int id)
        {
            try
            {
                var p = new { Id = id };

                var result = _dbContext.SaveData("spd_StateMaster_Delete", p);

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
