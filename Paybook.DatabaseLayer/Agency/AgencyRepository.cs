using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.DatabaseLayer.Agency
{
    public interface IAgencyRepository : IRepository<AgencyModel>
    {
        List<AgencyModel> GetAllName(int businessId);
    }

    public class AgencyRepository : IAgencyRepository
    {
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;

        public AgencyRepository()
        {
            _dbContext = DbContextFactory.Instance;
            _logger = LoggerFactory.Instance;
        }

        public List<AgencyModel> GetAllByPage(int businessId, int page, string search, string orderBy)
        {
            try
            {
                var p = new { BusinessId = businessId, Page = page, Search = search, OrderBy = orderBy };

                var result = _dbContext.LoadData<AgencyModel, dynamic>("sps_Agencies_GetAllByPage", p);
                //return _dbContext.LoadDataByProcedure("sps_Agency_SelectName", null);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public List<AgencyModel> GetAllName(int businessId)
        {
            try
            {
                var p = new { BusinessId = businessId };

                var result = _dbContext.LoadData<AgencyModel, dynamic>("sps_Agencies_GetAllName", p);
                //return _dbContext.LoadDataByProcedure("sps_Agency_SelectName", null);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public AgencyModel GetById(int businessId, int id)
        {
            try
            {
                var p = new { BusinessId = businessId, Id = id };

                var result = _dbContext.LoadData<AgencyModel, dynamic>("sps_Agencies_GetById", p);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public int Create(AgencyModel agencyModel)
        {
            try
            {
                var result = _dbContext.SaveDataOutParam("spi_Agencies_Insert", agencyModel, out int agencyId, DbType.Int32, "Id");
                //_dbContext.LoadDataByProcedure("sps_Agency_Insert", oParams);

                agencyModel.ID = agencyId;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }

        }
        public int Update(AgencyModel agencyModel)
        {
            try
            {
                var result = _dbContext.SaveData("spu_Agencies_Update", agencyModel);
                //_dbContext.LoadDataByProcedure("sps_Agency_Update", oParams);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public int Activate(int businessId, int id, bool active)
        {
            try
            {
                var p = new { BusinessId = businessId, Id = id, IsActive = active };

                var result = _dbContext.SaveData("spu_Agencies_Activate", p);
                //_dbContext.LoadDataByProcedure("sps_Agency_Update", oParams);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public int Delete(int businessId, int id)
        {
            try
            {
                var p = new { BusinessId = businessId, Id = id };

                var result = _dbContext.SaveData("spd_Agencies_Delete", p);
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
