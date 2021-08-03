using Paybook.DatabaseLayer.Identity;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.BusinessLayer.Identity
{

    public interface ILoginProcessor
    {
        DataTable Login_Isvalid(LoginModel loginModel);
    }

    public class LoginProcessor : ILoginProcessor
    {
        private readonly ILogger _logger;
        private readonly ILoginRepository _loginRepo;

        public LoginProcessor()
        {
            _logger = FileLogger.Instance;
            _loginRepo = new LoginRepository();
        }
        public DataTable Login_Isvalid(LoginModel loginModel)
        {
            try
            {
                return _loginRepo.Login_Isvalid(loginModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
    }
}
