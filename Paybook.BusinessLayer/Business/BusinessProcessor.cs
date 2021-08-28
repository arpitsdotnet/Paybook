using Paybook.DatabaseLayer;
using Paybook.DatabaseLayer.Business;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.BusinessLayer.Business
{
    public interface IBusinessProcessor
    {
        bool IsExist(string createBy, string businessName);

        List<BusinessModel> GetAllByUsername(string username);
        BusinessModel GetSelectedByUsername(string username);
        BusinessModel GetById(int id, string username);
        BusinessModel Create(BusinessModel model);
        BusinessModel Update(BusinessModel model);
        BusinessModel UpdateSelected(int id, string username);
        BusinessModel Activate(int id, string username, bool active);
        BusinessModel Delete(int id);
    }

    public class BusinessProcessor : IBusinessProcessor
    {
        private readonly ILogger _logger;
        private readonly IBusinessRepository _businessRepo;

        public BusinessProcessor()
        {
            _logger = LoggerFactory.Instance;
            _businessRepo = new BusinessRepository();
        }


        public bool IsExist(string createBy, string businessName)
        {
            try
            {
                return _businessRepo.IsExist(createBy, businessName);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
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
                var output = new BusinessModel();

                bool isExist = IsExist(model.CreateBy, model.Name);
                if (isExist)
                {
                    output.IsSucceeded = false;
                    output.ReturnMessage = Messages.Get(MTypes.Business, MStatus.IsExists);
                    return output;
                }

                int result = _businessRepo.Create(model);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = Messages.Get(MTypes.Business, MStatus.InsertSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = Messages.Get(MTypes.Business, MStatus.InsertFailure);
                return output;
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
                var output = new BusinessModel();
                int result = _businessRepo.Update(model);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = Messages.Get(MTypes.Business, MStatus.UpdateSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = Messages.Get(MTypes.Business, MStatus.UpdateFailure);
                return output;
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
                var output = new BusinessModel();
                int result = _businessRepo.UpdateSelected(id, username);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = Messages.Get(MTypes.Business, MStatusBusiness.SelectedUpdateSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = Messages.Get(MTypes.Business, MStatusBusiness.SelectedUpdateFailure);
                return output;
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
                var output = new BusinessModel();
                int result = _businessRepo.Activate(id, username, active);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    if (active)
                        output.ReturnMessage = Messages.Get(MTypes.Business, MStatus.ActivateSuccess);
                    else
                        output.ReturnMessage = Messages.Get(MTypes.Business, MStatus.DeactivateSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                if (active)
                    output.ReturnMessage = Messages.Get(MTypes.Business, MStatus.ActivateFailure);
                else
                    output.ReturnMessage = Messages.Get(MTypes.Business, MStatus.DeactivateFailure);
                return output;
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
                var output = new BusinessModel { IsSucceeded = false, ReturnMessage = "Current request failed due to technical issue, please try again later." };
                int result = _businessRepo.Delete(id);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = Messages.Get(MTypes.Business, MStatus.DeleteSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = Messages.Get(MTypes.Business, MStatus.DeleteFailure);
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
