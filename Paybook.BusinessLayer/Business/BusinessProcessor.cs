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
        bool IsExist(int businessId);

        List<BusinessModel> GetAllByUsername(string username);
        BusinessModel GetSelectedByUsername(string username);
        BusinessModel GetById(int id);
        BusinessModel Create(BusinessModel model);
        BusinessModel Update(BusinessModel model);
        BusinessModel Activate(int id, bool active);
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


        public bool IsExist(int businessId)
        {
            try
            {
                return _businessRepo.IsExist(businessId);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

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
                _logger.LogError(_logger.MethodName, ex);

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
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        public BusinessModel GetById(int id)
        {
            try
            {
                return _businessRepo.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        public BusinessModel Create(BusinessModel businessModel)
        {
            try
            {
                var output = new BusinessModel { IsSucceeded = false, ReturnMessage = "Current request failed due to technical issue, please try again later." };
                int result = _businessRepo.Create(businessModel);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = "Business has been created successfully.";// XmlProcessor.ReadXmlFile("CPS401");
                }
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        public BusinessModel Update(BusinessModel businessModel)
        {
            try
            {
                var output = new BusinessModel { IsSucceeded = false, ReturnMessage = "Current request failed due to technical issue, please try again later." };
                int result = _businessRepo.Update(businessModel);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = "Business has been updated successfully.";// XmlProcessor.ReadXmlFile("CPS401");
                }
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        public BusinessModel Activate(int id, bool active)
        {
            try
            {
                var output = new BusinessModel { IsSucceeded = false, ReturnMessage = "Current request failed due to technical issue, please try again later." };
                int result = _businessRepo.Activate(id, active);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = "Business has been activated/deactivated successfully.";// XmlProcessor.ReadXmlFile("CPS401");
                }
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

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
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
    }
}
