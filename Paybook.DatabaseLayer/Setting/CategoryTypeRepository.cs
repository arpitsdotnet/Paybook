using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Paybook.ServiceLayer.Models;

namespace Paybook.DatabaseLayer.Setting
{
    public class CategoryTypeRepository : ICategoryTypeRepository
    {
        private readonly IDbContext _dbContext;

        public CategoryTypeRepository()
        {
            _dbContext = DbContextFactory.Instance;
        }

        public List<CategoryTypeMasterModel> GetAllByPage(int businessId, int page, string search, string orderBy)
        {
            var p = new { BusinessId = businessId, Page = page, Search = search, OrderBy = orderBy };

            var result = _dbContext.LoadData<CategoryTypeMasterModel, dynamic>("sps_CategoryTypeMaster_GetAllByPage", p);
            //return _dbContext.LoadDataByProcedure("sps_Agency_SelectName", null);

            return result;
        }
        public CategoryTypeMasterModel GetById(int businessId, int id)
        {
            var p = new { BusinessId = businessId, Id = id };

            var result = _dbContext.LoadData<CategoryTypeMasterModel, dynamic>("sps_CategoryTypeMaster_GetById", p);

            return result.FirstOrDefault();
        }
        public int Create(CategoryTypeMasterModel model)
        {
            var result = _dbContext.SaveDataOutParam("spi_CategoryTypeMaster_Insert", model, out int categoryId, DbType.Int32, null, "Id");
            //_dbContext.LoadDataByProcedure("sps_Agency_Insert", oParams);

            model.Id = categoryId;
            return result;
        }
        public int Update(CategoryTypeMasterModel model)
        {
            var result = _dbContext.SaveData("spu_CategoryTypeMaster_Update", model);
            //_dbContext.LoadDataByProcedure("sps_Agency_Update", oParams);

            return result;
        }
        public int Activate(int businessId, string username, int id, bool active)
        {
            var p = new { BusinessId = businessId, Username = username, Id = id, IsActive = active };

            var result = _dbContext.SaveData("spu_CategoryTypeMaster_Activate", p);
            //_dbContext.LoadDataByProcedure("sps_Agency_Update", oParams);

            return result;
        }
        public int Delete(int businessId, string username, int id)
        {
            var p = new { BusinessId = businessId, Username = username, Id = id };

            var result = _dbContext.SaveData("spd_CategoryTypeMaster_Delete", p);
            //_dbContext.LoadDataByProcedure("sps_Agency_Update", oParams);

            return result;
        }
    }
}
