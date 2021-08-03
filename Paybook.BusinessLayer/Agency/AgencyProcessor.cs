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
        string Agency_Insert(AgencyModel agencyModel);
        DataTable Agency_Select(string sAgency_ID);
        DataTable Agency_SelectName();
        AgencyModel[] Agency_SelectRemainingAmount(string sAgency_ID);
        string Agency_UpdateRemainingAmount(string agencyId, double amount);
        string Agency_Update(AgencyModel agencyModel);
        string Agency_Update_AdvancePayment(string sTotalAdvancePayment, string sAgency_ID, string sTotalRemainigAmount);
    }

    public class AgencyProcessor : IAgencyProcessor
    {
        private readonly IAgencyRepository _agencyRepository;
        private readonly ILogger _logger;

        public AgencyProcessor()
        {
            _agencyRepository = new AgencyRepository();
            _logger = FileLogger.Instance;
        }
        public string Agency_Insert(AgencyModel agencyModel)
        {
            try
            {
                bool result = _agencyRepository.Agency_Insert(agencyModel);
                if (result)
                {
                    return XmlProcessor.ReadXmlFile("AGES103");
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public DataTable Agency_Select(string sAgency_ID)
        {
            try
            {
                return _agencyRepository.Agency_Select(sAgency_ID);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public DataTable Agency_SelectName()
        {
            try
            {
                return _agencyRepository.Agency_SelectName();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public AgencyModel[] Agency_SelectRemainingAmount(string sAgency_ID)
        {
            try
            {
                return _agencyRepository.Agency_SelectRemainingAmount(sAgency_ID);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        public string Agency_UpdateRemainingAmount(string agencyId, double amount)
        {
            try
            {
                bool result = _agencyRepository.Agency_UpdateRemainingAmount(agencyId, amount);
                if (result)
                {
                    return XmlProcessor.ReadXmlFile("AGES104");
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public string Agency_Update(AgencyModel agencyModel)
        {
            try
            {
                bool result = _agencyRepository.Agency_Update(agencyModel);
                if (result)
                {
                    return XmlProcessor.ReadXmlFile("AGES104");
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public string Agency_Update_AdvancePayment(string sTotalAdvancePayment, string sAgency_ID, string sTotalRemainigAmount)
        {
            try
            {
                bool result = _agencyRepository.Agency_Update_AdvancePayment(sTotalAdvancePayment, sAgency_ID, sTotalRemainigAmount);
                if (result)
                {
                    return XmlProcessor.ReadXmlFile("PTS501");
                }
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
