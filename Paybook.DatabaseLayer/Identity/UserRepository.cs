using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paybook.DatabaseLayer.Identity
{
    public interface IUserRepository
    {
        List<IdentityUserModel> GetAllByPage(int page, string search, string orderBy);
        IdentityUserModel GetById(int id);
        IdentityUserModel GetByUsername(string username);
        int Create(IdentityUserModel model);
        int Update(IdentityUserModel model);
        int Activate(int id, int userId, bool active);
        int Delete(int id, int userId);
    }
    public class UserRepository : IUserRepository
    {
        private readonly ILogger _logger;
        private readonly IDbContext _dbContext;

        public UserRepository()
        {
            _logger = LoggerFactory.Instance;
            _dbContext = DbContextFactory.Instance;
        }

        public List<IdentityUserModel> GetAllByPage(int page, string search, string orderBy)
        {
            try
            {
                var p = new { Page = page, Search = search, OrderBy = orderBy };

                var result = _dbContext.LoadData<IdentityUserModel, dynamic>("sps_IdentityUsers_GetAllByPage", p);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public IdentityUserModel GetById(int id)
        {
            try
            {
                var p = new { Id = id };

                var result = _dbContext.LoadData<IdentityUserModel, dynamic>("sps_IdentityUsers_GetById", p);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public IdentityUserModel GetByUsername(string username)
        {
            try
            {
                var p = new { Username = username };

                var result = _dbContext.LoadData<IdentityUserModel, dynamic>("sps_IdentityUsers_GetByUsername", p);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public int Create(IdentityUserModel model)
        {
            try
            {
                var result = _dbContext.SaveDataOutParam("spi_IdentityUsers_Insert", model, out int userId, DbType.Int32, null, "Id");

                model.Id = userId;

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }

        }
        public int Update(IdentityUserModel model)
        {
            try
            {
                var result = _dbContext.SaveData("spu_IdentityUsers_Update", model);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public int Activate(int id, int userId, bool active)
        {
            try
            {
                var p = new { Id = id, UserId = userId, IsActive = active };

                var result = _dbContext.SaveData("spu_IdentityUsers_Activate", p);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public int Delete(int id, int userId)
        {
            try
            {
                var p = new { Id = id, UserId = userId };

                var result = _dbContext.SaveData("spd_IdentityUsers_Delete", p);

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
