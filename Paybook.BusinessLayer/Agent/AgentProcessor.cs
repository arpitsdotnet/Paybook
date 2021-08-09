using Paybook.DatabaseLayer;
using Paybook.DatabaseLayer.Agent;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.BusinessLayer.Agent
{
    public interface IAgentProcessor
    {
        AgentModel[] GetAllActive();
        AgentModel[] GetAllByPage(string sOrderBy, string sGridPageNumber, string sUserName, string sIsActive);
        string Create(AgentModel agentModel);
        DataTable GetByAgentID(string sAgent_ID);
        AgentModel[] GetAllActiveIdAndName();
        string Update(AgentModel agentModel);
        string Activate(string sAgent_ID, string sIsActive, string sCreatedBY, string sReason);
    }

    public class AgentProcessor : IAgentProcessor
    {
        private readonly ILogger _logger;
        private readonly IAgentRepository _agentRepo;

        public AgentProcessor()
        {
            _logger = LoggerFactory.Instance;
            _agentRepo = new AgentRepository();
        }
        public AgentModel[] GetAllActive()
        {
            try
            {
                return _agentRepo.GetAllActive();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public AgentModel[] GetAllByPage(string sOrderBy, string sGridPageNumber, string sUserName, string sIsActive)
        {
            try
            {
                return _agentRepo.GetAllByPage(sOrderBy, sGridPageNumber, sUserName, sIsActive);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public string Create(AgentModel agentModel)
        {
            try
            {
                bool result = _agentRepo.Create(agentModel);
                if (result)
                    return XmlProcessor.ReadXmlFile("AGS203");

                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public DataTable GetByAgentID(string sAgent_ID)
        {
            try
            {
                return _agentRepo.GetByAgentID(sAgent_ID);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public AgentModel[] GetAllActiveIdAndName()
        {
            try
            {
                return _agentRepo.GetAllActiveIdAndName();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public string Update(AgentModel agentModel)
        {
            try
            {
                bool result = _agentRepo.Update(agentModel);
                if (result)
                    return XmlProcessor.ReadXmlFile("AGS204");

                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public string Activate(string agentID, string isActive, string createBY, string reason)
        {
            try
            {
                bool result = _agentRepo.Activate(agentID, isActive, createBY, reason);
                if (result)
                    return XmlProcessor.ReadXmlFile("AGS206");

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
