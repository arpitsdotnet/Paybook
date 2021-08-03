using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.DatabaseLayer.Agency
{
    public interface IAgencyRepository
    {
        bool Agency_Insert(AgencyModel agencyModel);
        DataTable Agency_Select(string sAgency_ID);
        DataTable Agency_SelectName();
        AgencyModel[] Agency_SelectRemainingAmount(string sAgency_ID);
        bool Agency_Update(AgencyModel agencyModel);
        bool Agency_Update_AdvancePayment(string sTotalAdvancePayment, string sAgency_ID, string sTotalRemainigAmount);
        bool Agency_UpdateRemainingAmount(string agencyId, double amount);
    }

    public class AgencyRepository : IAgencyRepository
    {
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;

        public AgencyRepository()
        {
            _dbContext = DbContextFactory.Instance;
            _logger = FileLogger.Instance;
        }

        public DataTable Agency_Select(string sAgency_ID)
        {
            try
            {
                List<Parameter> oParams = new List<Parameter>();
                //sCustomer_ID = sCustomer_ID.Replace('_', '/');
                oParams.Add(new Parameter("sAgency_ID", sAgency_ID));
                return _dbContext.LoadDataByProcedure("sps_Agency_Select", oParams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Agency_Insert(AgencyModel agencyModel)
        {
            try
            {
                var oParams = new List<Parameter>();
                oParams.Add(new Parameter("sAgency_ID", agencyModel.Agency_ID));
                oParams.Add(new Parameter("sAgencyName", agencyModel.AgencyName));
                oParams.Add(new Parameter("sAgencyAddress1", agencyModel.Address1));
                oParams.Add(new Parameter("sAgencyAddress2", agencyModel.Address2));
                oParams.Add(new Parameter("sAgencyCity", agencyModel.City));
                oParams.Add(new Parameter("sAgencyState_Core", agencyModel.State_Core));
                oParams.Add(new Parameter("sAgencyCountry", agencyModel.Country_Core));
                oParams.Add(new Parameter("sAgencyPhoneNumber1", agencyModel.PhoneNumber1));
                oParams.Add(new Parameter("sAgencyPhoneNumber2", agencyModel.PhoneNumber2));
                oParams.Add(new Parameter("sAgencyEmail", agencyModel.EMail));
                oParams.Add(new Parameter("sCreatedBY", agencyModel.CreatedBY));

                _dbContext.LoadDataByProcedure("sps_Agency_Insert", oParams);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }

        }
        public bool Agency_Update(AgencyModel agencyModel)
        {
            try
            {
                var oParams = new List<Parameter>();
                oParams.Add(new Parameter("sAgency_ID", agencyModel.Agency_ID));
                oParams.Add(new Parameter("sAgencyName", agencyModel.AgencyName));
                oParams.Add(new Parameter("sAgencyAddress1", agencyModel.Address1));
                oParams.Add(new Parameter("sAgencyAddress2", agencyModel.Address2));
                oParams.Add(new Parameter("sAgencyCity", agencyModel.City));
                oParams.Add(new Parameter("sAgencyState_Core", agencyModel.State_Core));
                oParams.Add(new Parameter("sAgencyCountry", agencyModel.Country_Core));
                oParams.Add(new Parameter("sAgencyPhoneNumber1", agencyModel.PhoneNumber1));
                oParams.Add(new Parameter("sAgencyPhoneNumber2", agencyModel.PhoneNumber2));
                oParams.Add(new Parameter("sAgencyEmail", agencyModel.EMail));
                oParams.Add(new Parameter("sModifiedBY", agencyModel.ModifiedBY));

                _dbContext.LoadDataByProcedure("sps_Agency_Update", oParams);
                return true;
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
                return _dbContext.LoadDataByProcedure("sps_Agency_SelectName", null);

            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        public bool Agency_Update_AdvancePayment(string sTotalAdvancePayment, string sAgency_ID, string sTotalRemainigAmount)
        {
            List<AgencyModel> oAgency = new List<AgencyModel>();
            try
            {
                // double dTotalAdvancePayment = Convert.ToDouble(sAdvancePayment);
                List<Parameter> oParams = new List<Parameter>();
                //string sTotalAdvancePayment = dTotalAdvancePayment.ToString();
                oParams.Add(new Parameter("sAgency_ID", sAgency_ID));
                oParams.Add(new Parameter("sAdvancePayment", sTotalAdvancePayment));
                oParams.Add(new Parameter("sTotalRemainigAmount", sTotalRemainigAmount));
                _dbContext.LoadDataByProcedure("sps_Agency_Update_AdvancePayment", oParams);

                return true;
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
                List<AgencyModel> agencies = new List<AgencyModel>();

                List<Parameter> oParams = new List<Parameter>();
                oParams.Add(new Parameter("sAgency_ID", sAgency_ID));

                DataTable dt = _dbContext.LoadDataByProcedure("sps_Agency_SelectRemainingAmount", oParams);
                if (dt != null && dt.Rows.Count > 0)
                {
                    AgencyModel agency = new AgencyModel();
                    agency.RemainingAmount = dt.Rows[0]["RemainingAmount"].ToString() == "" ? "0" : dt.Rows[0]["RemainingAmount"].ToString();
                    agency.AdvancePayment = dt.Rows[0]["AdvancePayment"].ToString() == "" ? "0" : dt.Rows[0]["AdvancePayment"].ToString();
                    agency.AgencyName = dt.Rows[0]["AgencyName"].ToString();
                    agencies.Add(agency);
                }

                return agencies.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public bool Agency_UpdateRemainingAmount(string agencyId, double amount)
        {
            try
            {
                List<Parameter> oParams = new List<Parameter>
                {
                    new Parameter("sAgency_ID", agencyId),
                    new Parameter("sTotalRemainigAmount", amount.ToString()),
                };

                _dbContext.LoadDataByProcedure("sps_Agency_UpdateRemainingAmount", oParams);
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
