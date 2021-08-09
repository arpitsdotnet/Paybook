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
        BusinessModel GetByUserId(int userId);
        string Create(BusinessModel businessModel);
        string Update(BusinessModel businessModel);
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
        public BusinessModel GetByUserId(int userId)
        {
            try
            {
                return _businessRepo.GetByUserId(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public string Create(BusinessModel businessModel)
        {
            try
            {
                int result = _businessRepo.Create(businessModel);
                if (result > 0)
                    return XmlProcessor.ReadXmlFile("CPS401");

                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        public string Update(BusinessModel businessModel)
        {
            try
            {
                int result = _businessRepo.Update(businessModel);
                if (result > 0)
                    return XmlProcessor.ReadXmlFile("CPS401");

                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
    }
}
