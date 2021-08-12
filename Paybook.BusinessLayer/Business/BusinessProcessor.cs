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
                var output = new BusinessModel { IsSucceeded = false };

                bool isExist = IsExist(model.CreateBy, model.Name);
                if (isExist)
                {
                    output.ReturnMessage = "Business name already exist, please enter a new name";
                    return output;
                }
                int result = _businessRepo.Create(model);
                if (result == 0)
                {
                    output.ReturnMessage = "Current request failed due to technical issue, please try again later.";
                    return output;
                }

                output.IsSucceeded = true;
                output.ReturnMessage = "Business has been created successfully.";// XmlProcessor.ReadXmlFile("CPS401");
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
                var output = new BusinessModel { IsSucceeded = false, ReturnMessage = "Current request failed due to technical issue, please try again later." };
                int result = _businessRepo.Update(model);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = "Business has been updated successfully.";// XmlProcessor.ReadXmlFile("CPS401");
                }
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
                var output = new BusinessModel { IsSucceeded = false, ReturnMessage = "Current request failed due to technical issue, please try again later." };
                int result = _businessRepo.UpdateSelected(id, username);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = "Business has been deleted successfully.";// XmlProcessor.ReadXmlFile("CPS401");
                }
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
                var output = new BusinessModel { IsSucceeded = false, ReturnMessage = "Current request failed due to technical issue, please try again later." };
                int result = _businessRepo.Activate(id, username, active);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = "Business has been activated/deactivated successfully.";// XmlProcessor.ReadXmlFile("CPS401");
                }
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
                    output.ReturnMessage = "Business has been deleted successfully.";// XmlProcessor.ReadXmlFile("CPS401");
                }
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
