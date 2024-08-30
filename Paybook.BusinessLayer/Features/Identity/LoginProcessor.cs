using System;
using Paybook.BusinessLayer.Abstracts.Identity;
using Paybook.DatabaseLayer.Abstracts.Identity;
using Paybook.DatabaseLayer.Identity;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;

namespace Paybook.BusinessLayer.Features.Identity
{
    public class LoginProcessor : ILoginProcessor
    {
        private readonly ILogger _logger;
        private readonly ILoginRepository _loginRepo;

        public LoginProcessor(ILogger logger)
        {
            _logger = logger;
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
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }

    }
}
