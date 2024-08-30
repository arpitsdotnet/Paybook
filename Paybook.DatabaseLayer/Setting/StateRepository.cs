using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Paybook.ServiceLayer.Models;

namespace Paybook.DatabaseLayer.Setting
{

    public class StateRepository : IStateRepository
    {
        private readonly IDbContext _dbContext;

        public StateRepository()
        {
            _dbContext = DbContextFactory.Instance;
        }

        public List<StateMasterModel> GetAllByPage(int countryId, int page, string search, string orderBy)
        {
            var p = new { CountryId = countryId, Page = page, Search = search, OrderBy = orderBy };

            var result = _dbContext.LoadData<StateMasterModel, dynamic>("sps_StateMaster_GetAllByPage", p);

            return result;
        }
        public StateMasterModel GetById(int id)
        {
            var p = new { Id = id };

            var result = _dbContext.LoadData<StateMasterModel, dynamic>("sps_StateMaster_GetById", p);

            return result.FirstOrDefault();
        }
        public int Create(StateMasterModel model)
        {
            var result = _dbContext.SaveDataOutParam("spi_StateMaster_Insert", model, out int stateId, DbType.Int32, null, "Id");

            model.Id = stateId;
            return result;
        }
        public int Update(StateMasterModel model)
        {
            var result = _dbContext.SaveData("spu_StateMaster_Update", model);

            return result;
        }
        public int Activate(int id, bool active)
        {
            var p = new { Id = id, IsActive = active };

            var result = _dbContext.SaveData("spu_StateMaster_Activate", p);

            return result;
        }
        public int Delete(int id)
        {
            var p = new { Id = id };

            var result = _dbContext.SaveData("spd_StateMaster_Delete", p);

            return result;
        }

        public StateMasterModel GetById(int businessId, int id)
        {
            throw new NotImplementedException();
        }

        public int Activate(int businessId, string username, int id, bool active)
        {
            throw new NotImplementedException();
        }

        public int Delete(int businessId, string username, int id)
        {
            throw new NotImplementedException();
        }
    }
}
