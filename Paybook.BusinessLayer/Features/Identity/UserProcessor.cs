using System;
using System.Collections.Generic;
using Paybook.BusinessLayer.Abstracts.Identity;
using Paybook.DatabaseLayer.Identity;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;

namespace Paybook.BusinessLayer.Features.Identity
{
    public class UserProcessor : IUserProcessor
    {
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepo;

        public UserProcessor(ILogger logger)
        {
            _logger = logger;
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
                int result = _userRepo.Create(model);

                if (result <= 0)
                {
                    return new IdentityUserModel
                    {
                        IsSucceeded = false,
                        ReturnMessage = XmlMessageHelper.Get(MTypes.User, MStatus.InsertFailure)
                    };
                }

                return new IdentityUserModel
                {
                    IsSucceeded = true,
                    ReturnMessage = XmlMessageHelper.Get(MTypes.User, MStatus.InsertSuccess)
                };
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
                int result = _userRepo.Update(model);

                if (result <= 0)
                {
                    return new IdentityUserModel
                    {
                        IsSucceeded = false,
                        ReturnMessage = XmlMessageHelper.Get(MTypes.User, MStatus.UpdateFailure)
                    };
                }

                return new IdentityUserModel
                {
                    IsSucceeded = true,
                    ReturnMessage = XmlMessageHelper.Get(MTypes.User, MStatus.UpdateSuccess)
                };
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
                int result = _userRepo.Activate(id, userId, active);

                if (result <= 0)
                {
                    return new IdentityUserModel
                    {
                        IsSucceeded = false,
                        ReturnMessage = XmlMessageHelper.Get(MTypes.User, active ? MStatus.ActivateFailure : MStatus.DeactivateFailure)
                    };
                }

                return new IdentityUserModel
                {
                    IsSucceeded = true,
                    ReturnMessage = XmlMessageHelper.Get(MTypes.User, active ? MStatus.ActivateSuccess : MStatus.DeactivateSuccess)
                };
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
                int result = _userRepo.Delete(id, userId);

                if (result <= 0)
                {
                    return new IdentityUserModel
                    {
                        IsSucceeded = false,
                        ReturnMessage = XmlMessageHelper.Get(MTypes.User, MStatus.DeleteFailure)
                    };
                }

                return new IdentityUserModel
                {
                    IsSucceeded = true,
                    ReturnMessage = XmlMessageHelper.Get(MTypes.User, MStatus.DeleteSuccess)
                };
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
    }
}
