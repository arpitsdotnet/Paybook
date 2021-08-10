using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.DatabaseLayer.Setting
{
    public interface ICountryRepository
    {
        List<CountryMasterModel> GetAllByPage(int page, string search, string orderBy);
        CountryMasterModel GetById(int id);
        int Create(CountryMasterModel model);
        int Update(CountryMasterModel model);
        int Activate(int id, bool active);
        int Delete(int id);
    }

    public class CountryRepository : ICountryRepository
    {
        private readonly ILogger _logger;
        private readonly IDbContext _dbContext;

        public CountryRepository()
        {
            _logger = LoggerFactory.Instance;
            _dbContext = DbContextFactory.Instance;
        }

        public List<CountryMasterModel> GetAllByPage(int page, string search, string orderBy)
        {
            try
            {
                var p = new { Page = page, Search = search, OrderBy = orderBy };

                var result = _dbContext.LoadData<CountryMasterModel, dynamic>("sps_CountryMaster_GetAllByPage", p);
                //return _dbContext.LoadDataByProcedure("sps_Agency_SelectName", null);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public CountryMasterModel GetById(int id)
        {
            try
            {
                var p = new { Id = id };

                var result = _dbContext.LoadData<CountryMasterModel, dynamic>("sps_CountryMaster_GetById", p);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public int Create(CountryMasterModel model)
        {
            try
            {
                var result = _dbContext.SaveDataOutParam("spi_CountryMaster_Insert", model, out int categoryId, DbType.Int32, null, "Id");
                //_dbContext.LoadDataByProcedure("sps_Agency_Insert", oParams);

                model.Id = categoryId;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }

        }
        public int Update(CountryMasterModel model)
        {
            try
            {
                var result = _dbContext.SaveData("spu_CountryMaster_Update", model);
                //_dbContext.LoadDataByProcedure("sps_Agency_Update", oParams);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public int Activate(int id, bool active)
        {
            try
            {
                var p = new { Id = id, IsActive = active };

                var result = _dbContext.SaveData("spu_CountryMaster_Activate", p);
                //_dbContext.LoadDataByProcedure("sps_Agency_Update", oParams);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public int Delete(int id)
        {
            try
            {
                var p = new { Id = id };

                var result = _dbContext.SaveData("spd_CountryMaster_Delete", p);
                //_dbContext.LoadDataByProcedure("sps_Agency_Update", oParams);

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
