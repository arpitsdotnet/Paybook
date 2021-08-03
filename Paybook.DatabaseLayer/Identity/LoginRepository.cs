using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.DatabaseLayer.Identity
{
    public interface ILoginRepository
    {
        DataTable Login_Isvalid(LoginModel loginModel);
    }

    public class LoginRepository : ILoginRepository
    {
        private readonly ILogger _logger;
        private readonly IDbContext _dbContext;

        public LoginRepository()
        {
            _logger = FileLogger.Instance;
            _dbContext = DbContextFactory.Instance;
        }

        public DataTable Login_Isvalid(LoginModel loginModel)
        {
            DataTable dt = new DataTable();
            try
            {
                var parameters = new List<Parameter>();
                parameters.Add(new Parameter(nameof(loginModel.Username), loginModel.Username));
                parameters.Add(new Parameter(nameof(loginModel.Password), loginModel.Password));

                dt = _dbContext.LoadDataByProcedure("sps_Login_IsValid", parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
            return dt;
        }
    }
}
