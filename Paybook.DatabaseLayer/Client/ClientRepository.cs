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
    public interface IClientRepository
    {
        ClientModel[] GetAllByText(string SearchText);
        ClientModel[] GetAllByPage(string sOrderBy, string sGridPageNumber, string sUserName, string sIsActive, string sSearchText, string sSearchBy);
        bool Create(ClientModel customerModel);
        bool IsExists(ClientModel customerModel);
        DataTable GetByClientID(string sCustomer_ID);
        DataTable GetCount();
        ClientModel[] GetAllNamesByAgencyID(string sAgency_ID);
        ClientModel[] GetPaymentByClientID(string sCustomer_ID);
        bool UpdatePayment(string customerId, double amount);
        bool Customer_Update(ClientModel customerModel);
        bool Customer_UpdateIsActive(string sCustomer_ID, string sIsActive, string sCreatedBY, string sReason);
        bool Customer_Update_AdvancePayment(string sTotalAdvancePayment, string sCustomer_ID, string sTotalRemainigAmount);
    }

    public class ClientRepository : IClientRepository
    {
        private readonly ILogger _logger;
        private readonly IDbContext _dbContext;

        public ClientRepository()
        {
            _logger = FileLogger.Instance;
            _dbContext = DbContextFactory.Instance;
        }

        public ClientModel[] GetAllByText(string SearchText)
        {

            List<ClientModel> oCustomer = new List<ClientModel>();
            try
            {
                List<Parameter> oParams = new List<Parameter>();
                oParams.Add(new Parameter("SearchText", SearchText));
                DataTable dt = _dbContext.LoadDataByProcedure("sps_Customers_Search", oParams);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ClientModel oDataRows = new ClientModel();

                        oDataRows.CustomerName = dr["CustomerName"].ToString();
                        oDataRows.Customer_ID = dr["Customer_ID"].ToString();
                        oCustomer.Add(oDataRows);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
            return oCustomer.ToArray();
        }

        public ClientModel[] GetAllNamesByAgencyID(string sAgency_ID)
        {
            List<ClientModel> oCustomer = new List<ClientModel>();

            try
            {
                List<Parameter> oParams = new List<Parameter>();
                oParams.Add(new Parameter("sAgency_ID", sAgency_ID));
                DataTable dt = _dbContext.LoadDataByProcedure("sps_Client_GetAllNamesByAgencyID", oParams);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ClientModel oDataRows = new ClientModel
                        {
                            CustomerName = dr["CustomerName"].ToString(),
                            Customer_ID = dr["Customer_ID"].ToString()
                        };
                        oCustomer.Add(oDataRows);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
            return oCustomer.ToArray();
        }

        public ClientModel[] GetPaymentByClientID(string sCustomer_ID)
        {
            List<ClientModel> clients = new List<ClientModel>();

            try
            {
                List<Parameter> oParams = new List<Parameter>();
                oParams.Add(new Parameter("sCustomer_ID", sCustomer_ID));
                DataTable dt = _dbContext.LoadDataByProcedure("sps_Customers_SelectRemainingAmount", oParams);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ClientModel client = new ClientModel
                    {
                        RemainingAmount = dt.Rows[0]["RemainingAmount"].ToString() == "" ? "0" : dt.Rows[0]["RemainingAmount"].ToString(),
                        AdvancePayment = dt.Rows[0]["AdvancePayment"].ToString(),
                        CustomerName = dt.Rows[0]["CustomerName"].ToString()
                    };

                    clients.Add(client);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
            return clients.ToArray();
        }


        public bool UpdatePayment(string customerId, double amount)
        {
            try
            {
                List<Parameter> oParams = new List<Parameter>
                {
                    new Parameter("sCustomer_ID", customerId),
                    new Parameter("sTotalRemainigAmount", amount.ToString()),
                };

                _dbContext.LoadDataByProcedure("sps_Customers_UpdateRemainingAmount", oParams);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
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
        public DataTable GetByClientID(string sCustomer_ID)
        {
            DataTable dt;
            try
            {
                List<Parameter> oParams = new List<Parameter>();
                //sCustomer_ID = sCustomer_ID.Replace('_', '/');
                oParams.Add(new Parameter("sCustomer_ID", sCustomer_ID));
                dt = _dbContext.LoadDataByProcedure("sps_Customers_Select", oParams);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
            return dt;

        }
        public ClientModel[] GetAllByPage(string sOrderBy, string sGridPageNumber, string sUserName, string sIsActive, string sSearchText, string sSearchBy)
        {
            DataTable dt = new DataTable();
            List<ClientModel> clients = new List<ClientModel>();
            try
            {
                List<Parameter> oParams = new List<Parameter>();
                oParams.Add(new Parameter("sIsActive", sIsActive));
                oParams.Add(new Parameter("sSearchText", sSearchText));
                if (sSearchBy == LastIdTypes.Customer)
                    dt = _dbContext.LoadDataByProcedure("sps_Customers_SelectAll", oParams);
                else
                    dt = _dbContext.LoadDataByProcedure("sps_Agency_Search", oParams);
                if (dt != null && dt.Rows.Count > 0)
                {
                    int dRowTotal = dt.Rows.Count;
                    string dtCount = dRowTotal.ToString();
                    int iPageNumber = Convert.ToInt32(sGridPageNumber);
                    int iPageStart = iPageNumber == 0 ? 0 : (PagerSetting.iPageSizeDefault * iPageNumber);

                    var list = (from e in dt.AsEnumerable()
                                select new
                                {
                                    RowCount = dtCount,
                                    Customer_ID = e.Field<string>("Customer_ID"),
                                    CustomerName = e.Field<string>("CustomerName"),
                                    City = e.Field<string>("City"),
                                    State_Disp = e.Field<string>("State_Disp"),
                                    Country_Core = e.Field<string>("Country_Core"),
                                    EMail = e.Field<string>("EMail"),
                                    PhoneNumber1 = e.Field<string>("PhoneNumber1"),
                                    RemainingAmount = e.Field<string>("RemainingAmount"),
                                    Invoices_Overdue_Count = e.Field<int>("Invoices_Overdue_Count"),
                                    Invoices_Open_Count = e.Field<int>("Invoices_Open_Count"),
                                    AgencyName = e.Field<string>("AgencyName"),
                                    Agency_ID = e.Field<string>("Agency_ID"),

                                }).Skip(iPageStart).Take(PagerSetting.iPageSizeDefault);

                    if (list != null && list.Count() > 0)
                    {
                        foreach (var dr in list)
                        {
                            var client = new ClientModel
                            {
                                RowCount = dr.RowCount,
                                Customer_ID = dr.Customer_ID,
                                CustomerName = dr.CustomerName,
                                City = dr.City,
                                State_Disp = dr.State_Disp,
                                Country_Core = dr.Country_Core,
                                EMail = dr.EMail,
                                PhoneNumber1 = dr.PhoneNumber1,
                                RemainingAmount = dr.RemainingAmount,
                                Invoices_Overdue_Count = dr.Invoices_Overdue_Count.ToString(),
                                Invoices_Open_Count = dr.Invoices_Open_Count.ToString(),
                                AgencyName = dr.AgencyName,
                                Agency_ID = dr.Agency_ID
                            };

                            clients.Add(client);
                        }
                    }
                    else
                    {
                        ClientModel client = new ClientModel();
                        client.ID = "0";
                        clients.Add(client);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
            return clients.ToArray();

        }

        public DataTable GetCount()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = _dbContext.LoadDataByProcedure("sps_Client_GetCount", null);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
            return dt;
        }

        public bool Customer_Update_AdvancePayment(string sTotalAdvancePayment, string sCustomer_ID, string sTotalRemainigAmount)
        {
            try
            {
                // double dTotalAdvancePayment = Convert.ToDouble(sAdvancePayment);
                List<Parameter> oParams = new List<Parameter>();
                //string sTotalAdvancePayment = dTotalAdvancePayment.ToString();
                oParams.Add(new Parameter("sAdvancePayment", sTotalAdvancePayment));
                oParams.Add(new Parameter("sCustomer_ID", sCustomer_ID));
                oParams.Add(new Parameter("sTotalRemainigAmount", sTotalRemainigAmount));
                _dbContext.LoadDataByProcedure("sps_Customers_Update_AdvancePayment", oParams);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public bool Customer_UpdateIsActive(string sCustomer_ID, string sIsActive, string sCreatedBY, string sReason)
        {
            try
            {
                List<Parameter> oParams = new List<Parameter>();
                oParams.Add(new Parameter("sCustomer_ID", sCustomer_ID));
                oParams.Add(new Parameter("sIsActive", sIsActive));
                _dbContext.LoadDataByProcedure("sps_Customers_UpdateIsActive", oParams);

                //Insert Customer IsActive Status
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
        public bool IsExists(ClientModel customerModel)
        {
            try
            {
                var oParams = new List<Parameter>();
                oParams.Add(new Parameter("sFirstName", customerModel.FirstName));
                oParams.Add(new Parameter("sAgencyID", customerModel.Agency_ID));
                oParams.Add(new Parameter("sPhoneNumber1", customerModel.PhoneNumber1));

                DataTable dt = _dbContext.LoadDataByProcedure("sps_Customer_IsExist", oParams);
                if (dt.Rows.Count > 0 && dt != null)
                {
                    if (Int32.Parse(dt.Rows[0]["CustomerID"].ToString()) != 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }

        }
        public bool Customer_Update(ClientModel customerModel)
        {
            try
            {
                var oParams = new List<Parameter>
                {
                    new Parameter("sCustomer_ID", customerModel.Customer_ID),
                    new Parameter("sCustomerPrefix_Core", customerModel.Prefix_Core),
                    new Parameter("sCustomerFirstName", customerModel.FirstName),
                    new Parameter("sCustomerMiddleName", customerModel.MiddleName),
                    new Parameter("sCustomerLastName", customerModel.LastName),
                    new Parameter("sCustomerDoB", customerModel.DateOfBirth),
                    new Parameter("sCustomerAddress1", customerModel.Address1),
                    new Parameter("sCustomerAddress2", customerModel.Address2),
                    new Parameter("sCustomerCity", customerModel.City),
                    new Parameter("sCustomerState_Core", customerModel.State_Core),
                    new Parameter("sCustomerCountry", customerModel.Country_Core),
                    new Parameter("sCustomerPhoneNumber1", customerModel.PhoneNumber1),
                    new Parameter("sCustomerPhoneNumber2", customerModel.PhoneNumber2),
                    new Parameter("sCustomerEmail", customerModel.EMail),
                    new Parameter("sCustomer_Type", customerModel.Customer_Type),
                    new Parameter("sGender", customerModel.Gender),
                    new Parameter("sAgency_ID", customerModel.Agency_ID),
                    new Parameter("sModifiedBY", customerModel.ModifiedBY)
                };

                _dbContext.LoadDataByProcedure("sps_Customers_Update", oParams);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public bool Create(ClientModel customerModel)
        {

            try
            {
                var oParams = new List<Parameter>
                {
                    new Parameter("sCustomer_ID", customerModel.Customer_ID),
                    new Parameter("sCustomerPrefix_Core", customerModel.Prefix_Core),
                    new Parameter("sCustomerFirstName", customerModel.FirstName),
                    new Parameter("sCustomerMiddleName", customerModel.MiddleName),
                    new Parameter("sCustomerLastName", customerModel.LastName),
                    new Parameter("sCustomerDoB", customerModel.DateOfBirth),
                    new Parameter("sCustomerAddress1", customerModel.Address1),
                    new Parameter("sCustomerAddress2", customerModel.Address2),
                    new Parameter("sCustomerCity", customerModel.City),
                    new Parameter("sCustomerState_Core", customerModel.State_Core),
                    new Parameter("sCustomerCountry", customerModel.Country_Core),
                    new Parameter("sCustomerPhoneNumber1", customerModel.PhoneNumber1),
                    new Parameter("sCustomerPhoneNumber2", customerModel.PhoneNumber2),
                    new Parameter("sCustomerEmail", customerModel.EMail),
                    new Parameter("sCustomer_Type", customerModel.Customer_Type),
                    new Parameter("sGender", customerModel.Gender),
                    new Parameter("sAgency_ID", customerModel.Agency_ID),
                    new Parameter("sCreatedBY", customerModel.CreatedBY)
                };

                _dbContext.LoadDataByProcedure("sps_Customer_Insert", oParams);
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
