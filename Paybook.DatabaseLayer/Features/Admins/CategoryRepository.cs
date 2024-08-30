using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Paybook.DatabaseLayer.Abstracts.Admins;
using Paybook.ServiceLayer.Models;

namespace Paybook.DatabaseLayer.Features.Admins
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDbContext _dbContext;

        public CategoryRepository()
        {
            _dbContext = DbContextFactory.Instance;
        }

        public List<CategoryMasterModel> GetAllByTypeCore(int businessId, string typeCore)
        {
            var p = new { BusinessId = businessId, TypeCore = typeCore };

            var result = _dbContext.LoadData<CategoryMasterModel, dynamic>("sps_CategoryMaster_GetAllByTypeCore", p);

            return result;
        }
        public CategoryMasterModel GetByCore(int businessId, string core)
        {
            var p = new { BusinessId = businessId, Core = core };

            var result = _dbContext.LoadData<CategoryMasterModel, dynamic>("sps_CategoryMaster_GetByCore", p);

            return result.FirstOrDefault();
        }

        public List<CategoryMasterModel> GetAllByPage(int businessId, int page, string search, string orderBy)
        {
            var p = new { BusinessId = businessId, Page = page, Search = search, OrderBy = orderBy };

            var result = _dbContext.LoadData<CategoryMasterModel, dynamic>("sps_CategoryMaster_GetAllByPage", p);
            //return _dbContext.LoadDataByProcedure("sps_Agency_SelectName", null);

            return result;
        }
        public CategoryMasterModel GetById(int businessId, int id)
        {
            var p = new { BusinessId = businessId, Id = id };

            var result = _dbContext.LoadData<CategoryMasterModel, dynamic>("sps_CategoryMaster_GetById", p);

            return result.FirstOrDefault();
        }
        public int Create(CategoryMasterModel model)
        {
            var result = _dbContext.SaveDataOutParam("spi_CategoryMaster_Insert", model, out int categoryId, DbType.Int32, null, "Id");
            //_dbContext.LoadDataByProcedure("sps_Agency_Insert", oParams);

            model.Id = categoryId;
            return result;
        }
        public int Update(CategoryMasterModel model)
        {
            var result = _dbContext.SaveData("spu_CategoryMaster_Update", model);
            //_dbContext.LoadDataByProcedure("sps_Agency_Update", oParams);

            return result;
        }
        public int Activate(int businessId, string username, int id, bool active)
        {
            var p = new { BusinessId = businessId, Username = username, Id = id, IsActive = active };

            var result = _dbContext.SaveData("spu_CategoryMaster_Activate", p);
            //_dbContext.LoadDataByProcedure("sps_Agency_Update", oParams);

            return result;
        }
        public int Delete(int businessId, string username, int id)
        {
            var p = new { BusinessId = businessId, Username = username, Id = id };

            var result = _dbContext.SaveData("spd_CategoryMaster_Delete", p);
            //_dbContext.LoadDataByProcedure("sps_Agency_Update", oParams);

            return result;
        }
    }
}
