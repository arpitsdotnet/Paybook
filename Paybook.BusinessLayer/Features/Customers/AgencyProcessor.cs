using Paybook.DatabaseLayer;
using Paybook.DatabaseLayer.Agency;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.BusinessLayer.Agency
{

    public interface IAgencyProcessor
    {
        AgencyModel[] Agency_SelectRemainingAmount(string sAgency_ID);
        List<AgencyModel> GetAllName(int businessId);

        List<AgencyModel> GetAllByPage(int businessId, int page, string search, string orderBy);
        AgencyModel GetById(int businessId, int agencyId);
        string Create(AgencyModel agencyModel);
        string Update(AgencyModel agencyModel);
        string Activate(int businessId, int id, bool active);
        string Delete(int businessId, int id);
    }

    public class AgencyProcessor : IAgencyProcessor
    {
        private readonly IAgencyRepository _agencyRepo;
        private readonly ILogger _logger;

        public AgencyProcessor()
        {
            _agencyRepo = new AgencyRepository();
            _logger = LoggerFactory.Instance;
        }

        public AgencyModel[] Agency_SelectRemainingAmount(string sAgency_ID)
        {
            try
            {
                return null;// _agencyRepo.Agency_SelectRemainingAmount(sAgency_ID);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public List<AgencyModel> GetAllName(int businessId)
        {
            try
            {
                return _agencyRepo.GetAllName(businessId);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }

        public List<AgencyModel> GetAllByPage(int businessId, int page, string search, string orderBy)
        {
            try
            {
                return _agencyRepo.GetAllByPage(businessId, page, search, orderBy);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public AgencyModel GetById(int businessId, int agencyId)
        {
            try
            {
                return _agencyRepo.GetById(businessId, agencyId);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public string Create(AgencyModel agencyModel)
        {
            try
            {
                int result = _agencyRepo.Create(agencyModel);
                if (result > 0)
                {
                    return XmlProcessor.ReadXmlFile("AGES103");
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public string Update(AgencyModel agencyModel)
        {
            try
            {
                int result = _agencyRepo.Update(agencyModel);
                if (result > 0)
                {
                    return XmlProcessor.ReadXmlFile("AGES104");
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public string Activate(int businessId, int id, bool active)
        {
            try
            {
                int result= _agencyRepo.Activate(businessId, id, active);
                if (result > 0)
                {
                    return XmlProcessor.ReadXmlFile("AgencyActivateSuccess");
                }
                return XmlProcessor.ReadXmlFile("AgencyActivateFail");
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public string Delete(int businessId, int id)
        {
            try
            {
                int result = _agencyRepo.Delete(businessId, id);
                if (result > 0)
                {
                    return XmlProcessor.ReadXmlFile("AgencyDeleteSuccess");
                }
                return XmlProcessor.ReadXmlFile("AgencyDeleteFail");
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
    }
}
