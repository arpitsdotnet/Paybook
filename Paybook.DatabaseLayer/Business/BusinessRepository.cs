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
        bool IsExist(string createBy, string businessName);

        List<BusinessModel> GetAllByUsername(string username);
        BusinessModel GetSelectedByUsername(string username);
        BusinessModel GetById(int id, string username);
        int Create(BusinessModel model);
        int Update(BusinessModel model);
        int UpdateSelected(int id, string username);
        int Activate(int id, string username, bool active);
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

        public bool IsExist(string createBy, string businessName)
        {
            try
            {
                var p = new { CreateBy = createBy, Name = businessName };

                var result = _dbContext.SaveDataOutParam("sps_Businesses_IsExist", p, out dynamic isExist, DbType.Boolean, null, "IsExist");
                //return _dbContext.LoadDataByProcedure("sps_CompanyProfile_IsExist", null);

                return (bool)isExist;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
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
                _logger.Error(_logger.GetMethodName(), ex);
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
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public BusinessModel GetById(int id, string username)
        {
            try
            {
                var p = new { Id = id, Username = username };

                var result = _dbContext.LoadData<BusinessModel, dynamic>("sps_Businesses_GetById", p);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }

        public int Create(BusinessModel model)
        {
            try
            {
                var p = new
                {
                    model.CreateBy,
                    model.Name,
                    model.Description,
                    model.Image,
                    model.GSTNumber,
                    model.PhoneNumber,
                    model.Email,
                    model.AddressLine1,
                    model.AddressLine2,
                    model.City,
                    model.StateId,
                    model.CountryId,
                    model.Pincode
                };

                var result = _dbContext.SaveDataOutParam("spi_Businesses_Insert", p, out int businessId, DbType.Int32, null, "Id");
                //_dbContext.LoadDataByProcedure("sps_Agency_Insert", oParams);

                model.Id = businessId;

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
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
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public int UpdateSelected(int id, string username)
        {
            try
            {
                var p = new { Id = id, CreateBy = username };

                var result = _dbContext.SaveData("spu_Businesses_UpdateSelected", p);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public int Activate(int id, string username, bool active)
        {
            try
            {
                var p = new { Id = id, Username = username, IsActive = active };

                var result = _dbContext.SaveData("spu_Businesses_Activate", p);
                //_dbContext.LoadDataByProcedure("sps_Agency_Update", oParams);

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

                var result = _dbContext.SaveData("spd_Businesses_Delete", p);
                //_dbContext.LoadDataByProcedure("sps_Agency_Update", oParams);

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
