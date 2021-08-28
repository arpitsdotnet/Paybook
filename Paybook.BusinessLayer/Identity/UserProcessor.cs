using Paybook.DatabaseLayer.Identity;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paybook.BusinessLayer.Identity
{
    public interface IUserProcessor
    {
        List<IdentityUserModel> GetAllByPage(int page, string search, string orderBy);
        IdentityUserModel GetById(int id);
        IdentityUserModel GetByUsername(string username);
        IdentityUserModel Create(IdentityUserModel model);
        IdentityUserModel Update(IdentityUserModel model);
        IdentityUserModel Activate(int id, int userId, bool active);
        IdentityUserModel Delete(int id, int userId);
    }

    public class UserProcessor : IUserProcessor
    {
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepo;

        public UserProcessor()
        {
            _logger = LoggerFactory.Instance;
            _userRepo = new UserRepository();
        }

        public List<IdentityUserModel> GetAllByPage(int page, string search, string orderBy)
        {
            try
            {
                return _userRepo.GetAllByPage(page, search, orderBy);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public IdentityUserModel GetById(int id)
        {
            try
            {
                return _userRepo.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public IdentityUserModel GetByUsername(string username)
        {
            try
            {
                return _userRepo.GetByUsername(username);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public IdentityUserModel Create(IdentityUserModel model)
        {
            try
            {
                IdentityUserModel output = new IdentityUserModel { IsSucceeded = false };
                int result = _userRepo.Create(model);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = Messages.Get(MTypes.User, MStatus.InsertSuccess);
                    return output;
                }
                output.IsSucceeded = false;
                output.ReturnMessage = Messages.Get(MTypes.User, MStatus.InsertFailure);
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public IdentityUserModel Update(IdentityUserModel model)
        {
            try
            {
                IdentityUserModel output = new IdentityUserModel { IsSucceeded = false };
                int result = _userRepo.Update(model);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = Messages.Get(MTypes.User, MStatus.UpdateSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = Messages.Get(MTypes.User, MStatus.UpdateFailure);
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public IdentityUserModel Activate(int id, int userId, bool active)
        {
            try
            {
                IdentityUserModel output = new IdentityUserModel { IsSucceeded = false };
                int result = _userRepo.Activate(id, userId, active);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    if (active)
                        output.ReturnMessage = Messages.Get(MTypes.User, MStatus.ActivateSuccess);
                    else
                        output.ReturnMessage = Messages.Get(MTypes.User, MStatus.DeactivateSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                if (active)
                    output.ReturnMessage = Messages.Get(MTypes.User, MStatus.ActivateFailure);
                else
                    output.ReturnMessage = Messages.Get(MTypes.User, MStatus.DeactivateFailure);
                return output;

            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public IdentityUserModel Delete(int id, int userId)
        {
            try
            {
                IdentityUserModel output = new IdentityUserModel { IsSucceeded = false };
                int result = _userRepo.Delete(id, userId);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = Messages.Get(MTypes.User, MStatus.DeleteSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = Messages.Get(MTypes.User, MStatus.DeleteFailure);
                return output;

            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
    }
}
