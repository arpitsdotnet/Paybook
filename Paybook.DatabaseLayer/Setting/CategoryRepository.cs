using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Constants;
using Paybook.ServiceLayer.Extensions;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Paybook.ServiceLayer.Logger;

namespace Paybook.DatabaseLayer.Setting
{
    public interface ICategoryRepository : IRepository<CategoryMasterModel>
    {
        CategoryMasterModel GetByCore(int businessId, string core);
        List<CategoryMasterModel> GetByTypeCore(int businessId, string typeCore);
    }

    public class CategoryRepository : ICategoryRepository
    {
        private readonly ILogger _logger;
        private readonly IDbContext _dbContext;

        public CategoryRepository()
        {
            _logger = LoggerFactory.Instance;
            _dbContext = DbContextFactory.Instance;
        }

        public List<CategoryMasterModel> GetByTypeCore(int businessId, string typeCore)
        {
            try
            {
                var p = new { BusinessId = businessId, TypeCore = typeCore };

                var result = _dbContext.LoadData<CategoryMasterModel, dynamic>("sps_CategoryMaster_GetByTypeCore", p);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public CategoryMasterModel GetByCore(int businessId, string core)
        {
            try
            {
                var p = new { BusinessId = businessId, Core = core };

                var result = _dbContext.LoadData<CategoryMasterModel, dynamic>("sps_CategoryMaster_GetByCore", p);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }

        public List<CategoryMasterModel> GetAllByPage(int businessId, int page, string search, string orderBy)
        {
            try
            {
                var p = new { BusinessId = businessId, Page = page, Search = search, OrderBy = orderBy };

                var result = _dbContext.LoadData<CategoryMasterModel, dynamic>("sps_CategoryMaster_GetAllByPage", p);
                //return _dbContext.LoadDataByProcedure("sps_Agency_SelectName", null);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public CategoryMasterModel GetById(int businessId, int id)
        {
            try
            {
                var p = new { BusinessId = businessId, Id = id };

                var result = _dbContext.LoadData<CategoryMasterModel, dynamic>("sps_CategoryMaster_GetById", p);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public int Create(CategoryMasterModel model)
        {
            try
            {
                var result = _dbContext.SaveDataOutParam("spi_CategoryMaster_Insert", model, out int categoryId, DbType.Int32, "Id");
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
        public int Update(CategoryMasterModel model)
        {
            try
            {
                var result = _dbContext.SaveData("spu_CategoryMaster_Update", model);
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

                var result = _dbContext.SaveData("spu_CategoryMaster_Activate", p);
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

                var result = _dbContext.SaveData("spd_CategoryMaster_Delete", p);
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
