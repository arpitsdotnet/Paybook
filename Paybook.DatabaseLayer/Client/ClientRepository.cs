using Paybook.ServiceLayer;
using Paybook.ServiceLayer.Constants;
using Paybook.ServiceLayer.Extensions;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.DatabaseLayer.Client
{
    public interface IClientRepository : IBaseRepository<ClientModel>
    {
        bool IsExists(ClientModel customerModel);
        int GetCount(int businessId);

        [Obsolete]
        ClientModel[] GetPaymentByClientID(string sCustomer_ID);
    }

    public class ClientRepository : IClientRepository
    {
        private readonly ILogger _logger;
        private readonly IDbContext _dbContext;

        public ClientRepository()
        {
            _logger = LoggerFactory.Instance;
            _dbContext = DbContextFactory.Instance;
        }
        public ClientModel[] GetPaymentByClientID(string sCustomer_ID)
        {
            List<ClientModel> clients = new List<ClientModel>();

            //try
            //{
            //    List<Parameter> oParams = new List<Parameter>();
            //    oParams.Add(new Parameter("sCustomer_ID", sCustomer_ID));
            //    DataTable dt = _dbContext.LoadDataByProcedure("sps_Customers_SelectRemainingAmount", oParams);
            //    if (dt != null && dt.Rows.Count > 0)
            //    {
            //        ClientModel client = new ClientModel
            //        {
            //            RemainingAmount = dt.Rows[0]["RemainingAmount"].ToString() == "" ? "0" : dt.Rows[0]["RemainingAmount"].ToString(),
            //            AdvancePayment = dt.Rows[0]["AdvancePayment"].ToString(),
            //            CustomerName = dt.Rows[0]["CustomerName"].ToString()
            //        };

            //        clients.Add(client);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(_logger.MethodName, ex);

            //    throw;
            //}
            return clients.ToArray();
        }
        //public static clsCustomers[] Customer_Select(string sCustomer_ID)
        //{
        //    List<clsCustomers> oCustomer = new List<clsCustomers>();

        //    try
        //    {
        //        List<clsParams> oParams = new List<clsParams>();
        //        oParams.Add(new clsParams("sCustomer_ID", sCustomer_ID));
        //        DataTable dt = clsCommon.ToLoad_MySqlDB_ByProc("sps_Customers_Select", oParams);
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                clsCustomers oDataRows = new clsCustomers();
        //                oDataRows.CreatedDT = dr["CreatedDT"].ToString();
        //                oDataRows.CreatedBY = dr["CreatedBY"].ToString();
        //                oDataRows.ModifiedDT = dr["ModifiedDT"].ToString();
        //                oDataRows.ModifiedBY = dr["ModifiedBY"].ToString();
        //                oDataRows.IsActive = dr["IsActive"].ToString();
        //                oDataRows.Customer_ID = dr["Customer_ID"].ToString();
        //                oDataRows.Prefix_Core = dr["Prefix_Core"].ToString();
        //                oDataRows.Prefix_Disp = dr["Prefix_Disp"].ToString();
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
        //                oDataRows.RemainingAmount = dr["RemainingAmount"].ToString();
        //                oDataRows.Customer_Type = dr["Customer_Type"].ToString();
        //                oDataRows.Gender = dr["Gender"].ToString();
        //                oCustomer.Add(oDataRows);
        //            }
        //        }
        //        else
        //        {
        //            clsCustomers oDataRows = new clsCustomers();
        //            oDataRows.ERROR = clsErrorMessage._CustomerIDError;
        //            oCustomer.Add(oDataRows);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clsCustomers oDataRows = new clsCustomers();
        //        oDataRows.ERROR = ex.Message;
        //        oCustomer.Add(oDataRows);
        //    }
        //    return oCustomer.ToArray();
        //}

        public int GetCount(int businessId)
        {
            try
            {
                var p = new { BusinessId = businessId };

                var result = _dbContext.SaveDataOutParam("sps_Clients_GetCount", p, out int count, DbType.Int32, null, "Count");

                return count;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public bool IsExists(ClientModel customerModel)
        {
            try
            {
                var result = _dbContext.SaveDataOutParam("sps_Clients_IsExist", customerModel, out bool isExist, DbType.Boolean, null, "IsExist");
                //DataTable dt = _dbContext.LoadDataByProcedure("sps_Customer_IsExist", oParams);
                //if (dt.Rows.Count > 0 && dt != null)
                //{
                //    if (Int32.Parse(dt.Rows[0]["CustomerID"].ToString()) != 0)
                //    {
                //        return true;
                //    }
                //}

                return isExist;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }

        }

        public List<ClientModel> GetAllByPage(int businessId, int page, string search, string orderBy)
        {
            try
            {
                var p = new { BusinessId = businessId, Page = page, Search = search, OrderBy = orderBy };

                var result = _dbContext.LoadData<ClientModel, dynamic>("sps_Clients_GetAllByPage", p);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public ClientModel GetById(int businessId, int id)
        {
            try
            {
                var p = new { BusinessId = businessId, Id = id };

                var result = _dbContext.LoadData<ClientModel, dynamic>("sps_Clients_GetById", p);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public int Create(ClientModel model)
        {
            try
            {
                var result = _dbContext.SaveDataOutParam("spi_Clients_Insert", model, out int categoryId, DbType.Int32, null, "Id");
                //_dbContext.LoadDataByProcedure("sps_Customer_Insert", oParams);

                model.Id = categoryId;

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }

        }
        public int Update(ClientModel model)
        {
            try
            {
                var result = _dbContext.SaveData("spu_Clients_Update", model);
                //_dbContext.LoadDataByProcedure("sps_Customers_Update", oParams);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public int Activate(int businessId, int id, bool active)
        {
            try
            {
                var p = new { BusinessId = businessId, Id = id, IsActive = active };

                var result = _dbContext.SaveData("spu_Clients_Activate", p);
                //_dbContext.LoadDataByProcedure("sps_Agency_Update", oParams);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public int Delete(int businessId, int id)
        {
            try
            {
                var p = new { BusinessId = businessId, Id = id };

                var result = _dbContext.SaveData("spd_Clients_Delete", p);
                //_dbContext.LoadDataByProcedure("sps_Agency_Update", oParams);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
    }
}
