using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.DatabaseLayer.Business
{
    public interface IBusinessRepository
    {
        bool IsExist(int businessId);

        List<BusinessModel> GetAllByUsername(string username); 
        BusinessModel GetSelectedByUsername(string username); 
         BusinessModel GetById(int id);
        int Create(BusinessModel model);
        int Update(BusinessModel model);
        int Activate(int id, bool active);
        int Delete(int id);
    }

    public class BusinessRepository : IBusinessRepository
    {
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;

        public BusinessRepository()
        {

            _dbContext = DbContextFactory.Instance;
            _logger = LoggerFactory.Instance;
        }

        public bool IsExist(int businessId)
        {
            try
            {
                var p = new { BusinessId = businessId };

                var result = _dbContext.SaveDataOutParam<dynamic, bool>("sps_Businesses_IsExist", p, out bool isExist, DbType.Boolean, null, "IsExist");
                //return _dbContext.LoadDataByProcedure("sps_CompanyProfile_IsExist", null);

                return isExist;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }

        public List<BusinessModel> GetAllByUsername(string username)
        {
            try
            {
                var p = new { Username = username };

                var result = _dbContext.LoadData<BusinessModel, dynamic>("sps_Businesses_GetAllByUsername", p);
                //return _dbContext.LoadDataByProcedure("sps_Agency_SelectName", null);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public BusinessModel GetSelectedByUsername(string username)
        {
            try
            {
                var p = new { Username = username };

                var result = _dbContext.LoadData<BusinessModel, dynamic>("sps_Businesses_GetSelectedByUsername", p);
                //return _dbContext.LoadDataByProcedure("sps_Agency_SelectName", null);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public BusinessModel GetById(int id)
        {
            try
            {
                var p = new { Id = id };

                var result = _dbContext.LoadData<BusinessModel, dynamic>("sps_Businesses_GetById", p);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public int Create(BusinessModel model)
        {
            try
            {
                var result = _dbContext.SaveDataOutParam("spi_Businesses_Insert", model, out int categoryId, DbType.Int32, null, "Id");
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
        public int Update(BusinessModel model)
        {
            try
            {
                var result = _dbContext.SaveData("spu_Businesses_Update", model);
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

                var result = _dbContext.SaveData("spu_Businesses_Activate", p);
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

                var result = _dbContext.SaveData("spd_Businesses_Delete", p);
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
