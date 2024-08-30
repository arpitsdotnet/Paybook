using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Paybook.DatabaseLayer.Abstracts.Customers;
using Paybook.ServiceLayer.Models.Agencies;

namespace Paybook.DatabaseLayer.Features.Customers
{
    public class AgencyRepository : IAgencyRepository
    {
        private readonly IDbContext _dbContext;

        public AgencyRepository()
        {
            _dbContext = DbContextFactory.Instance;
        }

        public List<AgencyModel> GetAllByPage(int businessId, int page, string search, string orderBy)
        {
            var p = new { BusinessId = businessId, Page = page, Search = search, OrderBy = orderBy };

            var result = _dbContext.LoadData<AgencyModel, dynamic>("sps_Agencies_GetAllByPage", p);
            //return _dbContext.LoadDataByProcedure("sps_Agency_SelectName", null);

            return result;
        }
        public List<AgencyModel> GetAllName(int businessId)
        {
            var p = new { BusinessId = businessId };

            var result = _dbContext.LoadData<AgencyModel, dynamic>("sps_Agencies_GetAllName", p);
            //return _dbContext.LoadDataByProcedure("sps_Agency_SelectName", null);

            return result;
        }
        public AgencyModel GetById(int businessId, int id)
        {
            var p = new { BusinessId = businessId, Id = id };

            var result = _dbContext.LoadData<AgencyModel, dynamic>("sps_Agencies_GetById", p);

            return result.FirstOrDefault();
        }
        public int Create(AgencyModel agencyModel)
        {
            var result = _dbContext.SaveDataOutParam("spi_Agencies_Insert", agencyModel, out int agencyId, DbType.Int32, null, "Id");
            //_dbContext.LoadDataByProcedure("sps_Agency_Insert", oParams);

            agencyModel.ID = agencyId;
            return result;
        }
        public int Update(AgencyModel agencyModel)
        {
            var result = _dbContext.SaveData("spu_Agencies_Update", agencyModel);
            //_dbContext.LoadDataByProcedure("sps_Agency_Update", oParams);

            return result;
        }
        public int Activate(int businessId, string username, int id, bool active)
        {
            var p = new { BusinessId = businessId, Username = username, Id = id, IsActive = active };

            var result = _dbContext.SaveData("spu_Agencies_Activate", p);
            //_dbContext.LoadDataByProcedure("sps_Agency_Update", oParams);

            return result;
        }
        public int Delete(int businessId, string username, int id)
        {
            var p = new { BusinessId = businessId, Username = username, Id = id };

            var result = _dbContext.SaveData("spd_Agencies_Delete", p);
            //_dbContext.LoadDataByProcedure("sps_Agency_Update", oParams);

            return result;
        }
    }
}
