using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Paybook.DatabaseLayer.Abstracts.Identity;
using Paybook.ServiceLayer.Models;

namespace Paybook.DatabaseLayer.Identity
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContext _dbContext;

        public UserRepository()
        {
            _dbContext = DbContextFactory.Instance;
        }

        public List<IdentityUserModel> GetAllByPage(int page, string search, string orderBy)
        {
            var p = new { Page = page, Search = search, OrderBy = orderBy };

            var result = _dbContext.LoadData<IdentityUserModel, dynamic>("sps_IdentityUsers_GetAllByPage", p);

            return result;
        }
        public IdentityUserModel GetById(int id)
        {
            var p = new { Id = id };

            var result = _dbContext.LoadData<IdentityUserModel, dynamic>("sps_IdentityUsers_GetById", p);

            return result.FirstOrDefault();
        }
        public IdentityUserModel GetByUsername(string username)
        {
            var p = new { Username = username };

            var result = _dbContext.LoadData<IdentityUserModel, dynamic>("sps_IdentityUsers_GetByUsername", p);

            return result.FirstOrDefault();
        }
        public int Create(IdentityUserModel model)
        {
            var result = _dbContext.SaveDataOutParam("spi_IdentityUsers_Insert", model, out int userId, DbType.Int32, null, "Id");

            model.Id = userId;

            return result;
        }
        public int Update(IdentityUserModel model)
        {
            var result = _dbContext.SaveData("spu_IdentityUsers_Update", model);

            return result;
        }
        public int Activate(int id, int userId, bool active)
        {
            var p = new { Id = id, UserId = userId, IsActive = active };

            var result = _dbContext.SaveData("spu_IdentityUsers_Activate", p);

            return result;
        }
        public int Delete(int id, int userId)
        {
            var p = new { Id = id, UserId = userId };

            var result = _dbContext.SaveData("spd_IdentityUsers_Delete", p);

            return result;
        }
    }
}
