using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Paybook.ServiceLayer.Models;

namespace Paybook.DatabaseLayer.Setting
{
    public class CountryRepository : ICountryRepository
    {
        private readonly IDbContext _dbContext;

        public CountryRepository()
        {
            _dbContext = DbContextFactory.Instance;
        }

        public List<CountryMasterModel> GetAllByPage(int page, string search, string orderBy)
        {
            var p = new { Page = page, Search = search, OrderBy = orderBy };

            var result = _dbContext.LoadData<CountryMasterModel, dynamic>("sps_CountryMaster_GetAllByPage", p);

            return result;
        }
        public CountryMasterModel GetById(int id)
        {
            var p = new { Id = id };

            var result = _dbContext.LoadData<CountryMasterModel, dynamic>("sps_CountryMaster_GetById", p);

            return result.FirstOrDefault();
        }
        public int Create(CountryMasterModel model)
        {
            var result = _dbContext.SaveDataOutParam("spi_CountryMaster_Insert", model, out int categoryId, DbType.Int32, null, "Id");

            model.Id = categoryId;
            return result;
        }
        public int Update(CountryMasterModel model)
        {
            var result = _dbContext.SaveData("spu_CountryMaster_Update", model);

            return result;
        }
        public int Activate(int id, bool active)
        {
            var p = new { Id = id, IsActive = active };

            var result = _dbContext.SaveData("spu_CountryMaster_Activate", p);

            return result;
        }
        public int Delete(int id)
        {
            var p = new { Id = id };

            var result = _dbContext.SaveData("spd_CountryMaster_Delete", p);

            return result;
        }
    }
}
