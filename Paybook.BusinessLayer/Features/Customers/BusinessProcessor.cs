using System;
using System.Collections.Generic;
using Paybook.BusinessLayer.Abstracts.Customers;
using Paybook.DatabaseLayer.Business;
using Paybook.ServiceLayer.Abstracts;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Services;
using Paybook.ServiceLayer.Xml;

namespace Paybook.BusinessLayer.Features.Customers
{
    public class BusinessProcessor : IBusinessProcessor
    {
        private readonly ILogger _logger;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IMessageProvider _messageProvider;
        private readonly IBusinessRepository _businessRepo;


        public BusinessProcessor(
            ILogger logger,
            IDateTimeProvider dateTimeProvider,
            IMessageProvider messageProvider)
        {
            _logger = logger;
            _dateTimeProvider = dateTimeProvider;
            _messageProvider = messageProvider;
            _businessRepo = new BusinessRepository();
        }

        public List<BusinessModel> GetAllByUsername(string username)
        {
            try
            {
                return _businessRepo.GetAllByUsername(username);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public BusinessModel GetSelectedByUsername(string username)
        {
            try
            {
                return _businessRepo.GetSelectedByUsername(username);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public BusinessModel GetById(int id, string username)
        {
            try
            {
                return _businessRepo.GetById(id, username);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public BusinessModel Create(BusinessModel model)
        {
            try
            {
                bool isUserNameExist = _businessRepo.IsExist(model.CreateBy, model.Name);
                if (isUserNameExist)
                {
                    return new BusinessModel
                    {
                        IsSucceeded = false,
                        //output.ReturnMessage = XmlMessageHelper.Get(MTypes.Business, MStatus.IsExists);
                        ReturnMessage = _messageProvider.GetBusinessMessage(MStatus.IsExists)
                    };
                }

                model.CreateDate = _dateTimeProvider.Now;
                model.ModifyDate = _dateTimeProvider.Now;

                int result = _businessRepo.Create(model);
                if (result <= 0)
                {
                    return new BusinessModel
                    {
                        IsSucceeded = false,
                        //output.ReturnMessage = XmlMessageHelper.Get(MTypes.Business, MStatus.InsertFailure);
                        ReturnMessage = _messageProvider.GetBusinessMessage(MStatus.InsertFailure)
                    };
                }

                return new BusinessModel
                {
                    IsSucceeded = true,
                    //output.ReturnMessage = XmlMessageHelper.Get(MTypes.Business, MStatus.InsertSuccess);
                    ReturnMessage = _messageProvider.GetBusinessMessage(MStatus.InsertSuccess)
                };
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public BusinessModel Update(BusinessModel model)
        {
            try
            {
                model.ModifyDate = _dateTimeProvider.Now;

                int result = _businessRepo.Update(model);

                if (result <= 0)
                {
                    return new BusinessModel
                    {
                        IsSucceeded = false,
                        //ReturnMessage = XmlMessageHelper.Get(MTypes.Business, MStatus.UpdateFailure)
                        ReturnMessage = _messageProvider.GetBusinessMessage(MStatus.UpdateFailure)
                    };
                }

                return new BusinessModel
                {
                    IsSucceeded = true,
                    //ReturnMessage = XmlMessageHelper.Get(MTypes.Business, MStatus.UpdateSuccess)
                    ReturnMessage = _messageProvider.GetBusinessMessage(MStatus.UpdateSuccess)
                };
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public BusinessModel UpdateSelected(int id, string username)
        {
            try
            {
                int result = _businessRepo.UpdateSelected(id, username);
                if (result <= 0)
                {
                    return new BusinessModel
                    {
                        IsSucceeded = false,
                        //ReturnMessage = XmlMessageHelper.Get(MTypes.Business, MStatusBusiness.SelectedUpdateFailure)
                        ReturnMessage = _messageProvider.GetBusinessMessage(MStatusBusiness.SelectedUpdateFailure)
                    };
                }

                return new BusinessModel
                {
                    IsSucceeded = true,
                    //ReturnMessage = XmlMessageHelper.Get(MTypes.Business, MStatusBusiness.SelectedUpdateSuccess)
                    ReturnMessage = _messageProvider.GetBusinessMessage(MStatusBusiness.SelectedUpdateSuccess)
                };
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public BusinessModel Activate(int id, string username, bool active)
        {
            try
            {
                int result = _businessRepo.Activate(id, username, active);
                if (result <= 0)
                {
                    return new BusinessModel
                    {
                        IsSucceeded = false,
                        ReturnMessage = _messageProvider.GetBusinessMessage(active == true ? MStatus.ActivateFailure : MStatus.DeactivateFailure)
                    };
                }
                return new BusinessModel
                {
                    IsSucceeded = true,
                    ReturnMessage = _messageProvider.GetBusinessMessage(active == true ? MStatus.ActivateSuccess : MStatus.DeactivateSuccess)
                };
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public BusinessModel Delete(int id)
        {
            try
            {
                int result = _businessRepo.Delete(id);
                if (result <= 0)
                {
                    return new BusinessModel
                    {
                        IsSucceeded = false,
                        //ReturnMessage = XmlMessageHelper.Get(MTypes.Business, MStatus.DeleteFailure)
                        ReturnMessage = _messageProvider.GetBusinessMessage(MStatus.DeleteFailure)
                    };
                }
                return new BusinessModel
                {
                    IsSucceeded = true,
                    //ReturnMessage = XmlMessageHelper.Get(MTypes.Business, MStatus.DeleteSuccess)
                    ReturnMessage = _messageProvider.GetBusinessMessage(MStatus.DeleteSuccess)
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
