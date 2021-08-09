using Paybook.ServiceLayer.Constants;
using Paybook.ServiceLayer.Extensions;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Logger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Paybook.ServiceLayer.Xml;

namespace Paybook.DatabaseLayer.Agent
{
    public interface IAgentRepository
    {
        AgentModel[] GetAllActive();
        AgentModel[] GetAllByPage(string sOrderBy, string sGridPageNumber, string sUserName, string sIsActive);
        bool Create(AgentModel agentModel);
        DataTable GetByAgentID(string sAgent_ID);
        AgentModel[] GetAllActiveIdAndName();
        bool Update(AgentModel agentModel);
        bool Activate(string sAgent_ID, string sIsActive, string sCreatedBY, string sReason);
    }

    public class AgentRepository : IAgentRepository
    {
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;

        public AgentRepository()
        {
            _dbContext = DbContextFactory.Instance;
            _logger = LoggerFactory.Instance;
        }

        public AgentModel[] GetAllActive()
        {
            try
            {
                List<AgentModel> oAgents = new List<AgentModel>();
                DataTable dt = _dbContext.LoadDataByProcedure("sps_Agents_Active_SelectAll", null);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AgentModel oDataRows = new AgentModel
                        {
                            Agent_ID = dr["Agent_ID"].ToString(),
                            AgentName = dr["AgentName"].ToString()
                        };
                        oAgents.Add(oDataRows);
                    }
                }
                return oAgents.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }

        public AgentModel[] GetAllActiveIdAndName()
        {
            List<AgentModel> oAgent = new List<AgentModel>();
            try
            {
                DataTable dt = _dbContext.LoadDataByProcedure("sps_Agents_SelectName", null);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AgentModel oDataRows = new AgentModel
                        {
                            AgentName = dr["AgentName"].ToString(),
                            Agent_ID = dr["Agent_ID"].ToString()
                        };
                        oAgent.Add(oDataRows);
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
            return oAgent.ToArray();
        }

        //public static clsAgents[] Agent_Select(string sAgent_ID)
        //{
        //    List<clsAgents> oAgent = new List<clsAgents>();

        //    try
        //    {
        //        List<clsParams> oParams = new List<clsParams>();
        //        oParams.Add(new clsParams("sAgent_ID", sAgent_ID));
        //        DataTable dt = clsCommon.ToLoad_MySqlDB_ByProc("sps_Agents_Select", oParams);
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                clsAgents oDataRows = new clsAgents();
        //                oDataRows.CreatedDT = dr["CreatedDT"].ToString();
        //                oDataRows.CreatedBY = dr["CreatedBY"].ToString();
        //                oDataRows.IsActive = dr["IsActive"].ToString();
        //                oDataRows.Agent_ID = dr["Agent_ID"].ToString();
        //                oDataRows.Prefix_Core = dr["Prefix_Core"].ToString();
        //                oDataRows.FirstName = dr["FirstName"].ToString();
        //                oDataRows.MiddleName = dr["MiddleName"].ToString();
        //                oDataRows.LastName = dr["LastName"].ToString();
        //                oDataRows.DateOfBirth = dr["DateOfBirth"].ToString();
        //                oDataRows.Address1 = dr["Address1"].ToString();
        //                oDataRows.Address2 = dr["Address2"].ToString();
        //                oDataRows.City = dr["City"].ToString();
        //                oDataRows.State_Core = dr["State_Core"].ToString();
        //                oDataRows.Country_Core = dr["Country_Core"].ToString();
        //                oDataRows.EMail = dr["EMail"].ToString();
        //                oDataRows.PhoneNumber1 = dr["PhoneNumber1"].ToString();
        //                oDataRows.PhoneNumber2 = dr["PhoneNumber2"].ToString();
        //                oDataRows.Gender = dr["Gender"].ToString();

        //                oAgent.Add(oDataRows);
        //            }
        //        }
        //        else
        //        {
        //            clsAgents oDataRows = new clsAgents();
        //            oDataRows.ERROR = clsErrorMessage._AgentIDError;
        //            oAgent.Add(oDataRows);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clsAgents oDataRows = new clsAgents();
        //        oDataRows.ERROR = ex.Message;
        //        oAgent.Add(oDataRows);
        //    }
        //    return oAgent.ToArray();
        //}
        public DataTable GetByAgentID(string sAgent_ID)
        {
            try
            {
                List<Parameter> oParams = new List<Parameter>();
                oParams.Add(new Parameter("sAgent_ID", sAgent_ID));
                return _dbContext.LoadDataByProcedure("sps_Agents_Select", oParams);
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
                List<AgentModel> agents = new List<AgentModel>();

                List<Parameter> parameters = new List<Parameter>();
                parameters.Add(new Parameter("IsActive", sIsActive));
                DataTable dt = _dbContext.LoadDataByProcedure("sps_Agents_GetAllByPage", parameters);

                if (dt != null && dt.Rows.Count > 0)
                {
                    string dtCount = dt.Rows.Count.ToString();
                    int dRowTotal = int.Parse(dtCount);
                    //GetPageRange(Convert.ToDouble(sGridPageNumber), dRowTotal, out dPageNumber_Start, out dPageNumber_End);

                    int iPageNumber = Convert.ToInt32(sGridPageNumber);
                    int iPageStart = iPageNumber == 0 ? 0 : (PagerSetting.iPageSizeDefault * iPageNumber);

                    var list = (from e in dt.AsEnumerable()
                                select new AgentModel
                                {
                                    RowCount = dtCount,
                                    Agent_ID = e.Field<string>("Agent_ID"),
                                    AgentName = e.Field<string>("AgentName"),
                                    City = e.Field<string>("City"),
                                    State_Disp = e.Field<string>("State_Disp"),
                                    Country_Core = e.Field<string>("Country_Core"),
                                    EMail = e.Field<string>("EMail"),
                                    PhoneNumber1 = e.Field<string>("PhoneNumber1"),
                                }).Skip(iPageStart).Take(PagerSetting.iPageSizeDefault);

                    agents.AddRange(list);
                }
                return agents.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }

        }

        public bool Create(AgentModel agentModel)
        {
            try
            {
                var oParams = new List<Parameter>();
                oParams.Add(new Parameter("sAgent_ID", agentModel.Agent_ID));
                oParams.Add(new Parameter("sAgentPrefix_Core", agentModel.Prefix_Core));
                oParams.Add(new Parameter("sAgentFirstName", agentModel.FirstName));
                oParams.Add(new Parameter("sAgentMiddleName", agentModel.MiddleName));
                oParams.Add(new Parameter("sAgentLastName", agentModel.LastName));
                oParams.Add(new Parameter("sAgentDoB", agentModel.DateOfBirth));
                oParams.Add(new Parameter("sAgentAddress1", agentModel.Address1));
                oParams.Add(new Parameter("sAgentAddress2", agentModel.Address2));
                oParams.Add(new Parameter("sAgentCity", agentModel.City));
                oParams.Add(new Parameter("sAgentState_Core", agentModel.State_Core));
                oParams.Add(new Parameter("sAgentCountry", agentModel.Country_Core));
                oParams.Add(new Parameter("sAgentPhoneNumber1", agentModel.PhoneNumber1));
                oParams.Add(new Parameter("sAgentPhoneNumber2", agentModel.PhoneNumber2));
                oParams.Add(new Parameter("sAgentEmail", agentModel.EMail));
                oParams.Add(new Parameter("sGender", agentModel.Gender));
                oParams.Add(new Parameter("sCreatedBY", agentModel.CreatedBY));

                _dbContext.LoadDataByProcedure("sps_Agents_Insert", oParams);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public bool Update(AgentModel agentModel)
        {
            try
            {
                var oParams = new List<Parameter>();
                oParams.Add(new Parameter("sAgent_ID", agentModel.Agent_ID));
                oParams.Add(new Parameter("sAgentPrefix_Core", agentModel.Prefix_Core));
                oParams.Add(new Parameter("sAgentFirstName", agentModel.FirstName));
                oParams.Add(new Parameter("sAgentMiddleName", agentModel.MiddleName));
                oParams.Add(new Parameter("sAgentLastName", agentModel.LastName));
                oParams.Add(new Parameter("sAgentDoB", agentModel.DateOfBirth));
                oParams.Add(new Parameter("sAgentAddress1", agentModel.Address1));
                oParams.Add(new Parameter("sAgentAddress2", agentModel.Address2));
                oParams.Add(new Parameter("sAgentCity", agentModel.City));
                oParams.Add(new Parameter("sAgentState_Core", agentModel.State_Core));
                oParams.Add(new Parameter("sAgentCountry", agentModel.Country_Core));
                oParams.Add(new Parameter("sAgentPhoneNumber1", agentModel.PhoneNumber1));
                oParams.Add(new Parameter("sAgentPhoneNumber2", agentModel.PhoneNumber2));
                oParams.Add(new Parameter("sAgentEmail", agentModel.EMail));
                oParams.Add(new Parameter("sGender", agentModel.Gender));
                oParams.Add(new Parameter("sModifiedBY", agentModel.ModifiedBY));

                _dbContext.LoadDataByProcedure("sps_Agents_Update", oParams);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }

        }

        public bool Activate(string sAgent_ID, string sIsActive, string sCreatedBY, string sReason)
        {
            try
            {
                List<Parameter> oParams = new List<Parameter>();
                oParams.Add(new Parameter("sAgent_ID", sAgent_ID));
                oParams.Add(new Parameter("sIsActive", sIsActive));
                DataTable dt = _dbContext.LoadDataByProcedure("sps_Agents_UpdateIsActive", oParams);

                //Insert Agent Isactive Status
                oParams.Add(new Parameter("sCreatedBY", sCreatedBY));
                oParams.Add(new Parameter("sReason", sReason));
                _dbContext.LoadDataByProcedure("sps_Customers_Status_Insert", oParams);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
    }
}
