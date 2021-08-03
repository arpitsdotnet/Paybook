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
        AgentModel[] Agents_Active_SelectAll();
        AgentModel[] Agents_SelectAll(string sOrderBy, string sGridPageNumber, string sUserName, string sIsActive);
        string Agent_Insert(AgentModel agentModel);
        DataTable Agent_Select(string sAgent_ID);
        AgentModel[] Agent_SelectName();
        string Agent_Update(AgentModel agentModel);
        string Agent_UpdateIsActive(string sAgent_ID, string sIsActive, string sCreatedBY, string sReason);
    }

    public class AgentProcessor : IAgentProcessor
    {
        private readonly ILogger _logger;
        private readonly IAgentRepository _agentRepo;

        public AgentProcessor()
        {
            _logger = FileLogger.Instance;
            _agentRepo = new AgentRepository();
        }
        public AgentModel[] Agents_Active_SelectAll()
        {
            try
            {
                return _agentRepo.Agents_Active_SelectAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public AgentModel[] Agents_SelectAll(string sOrderBy, string sGridPageNumber, string sUserName, string sIsActive)
        {
            try
            {
                return _agentRepo.Agents_SelectAll(sOrderBy, sGridPageNumber, sUserName, sIsActive);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public string Agent_Insert(AgentModel agentModel)
        {
            try
            {
                bool result = _agentRepo.Agent_Insert(agentModel);
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

        public DataTable Agent_Select(string sAgent_ID)
        {
            try
            {
                return _agentRepo.Agent_Select(sAgent_ID);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public AgentModel[] Agent_SelectName()
        {
            try
            {
                return _agentRepo.Agent_SelectName();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public string Agent_Update(AgentModel agentModel)
        {
            try
            {
                bool result = _agentRepo.Agent_Update(agentModel);
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

        public string Agent_UpdateIsActive(string sAgent_ID, string sIsActive, string sCreatedBY, string sReason)
        {
            try
            {
                bool result = _agentRepo.Agent_UpdateIsActive(sAgent_ID, sIsActive, sCreatedBY, sReason);
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
