using Paybook.DatabaseLayer.Identity;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.BusinessLayer.Identity
{

    public interface ILoginProcessor
    {
        /// <summary>
        /// Check if username valid
        /// </summary>
        /// <param name="model"></param>
        /// <returns>string: UserMatch | UserNotMatch | UserNotExist</returns>
        string IsValid(IdentityUserModel loginModel);

    }

    public class LoginProcessor : ILoginProcessor
    {
        private readonly ILogger _logger;
        private readonly ILoginRepository _loginRepo;

        public LoginProcessor()
        {
            _logger = LoggerFactory.Instance;
            _loginRepo = new LoginRepository();
        }

        public string IsValid(IdentityUserModel loginModel)
        {
            try
            {
                return _loginRepo.IsValid(loginModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

    }
}
