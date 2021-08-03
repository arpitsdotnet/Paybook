using Paybook.DatabaseLayer;
using Paybook.DatabaseLayer.Client;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.BusinessLayer.Client
{
    public interface IClientProcessor
    {
        CustomerModel[] Customers_Search(string SearchText);
        CustomerModel[] Customers_SelectAll(string sOrderBy, string sGridPageNumber, string sUserName, string sIsActive, string sSearchText, string sSearchBy);
        string Customer_Insert(CustomerModel customerModel);
        string Customer_IsExist(CustomerModel customerModel);
        DataTable Customer_Select(string sCustomer_ID);
        DataTable Customer_SelectCount();
        CustomerModel[] Customer_SelectName(string sAgency_ID);
        CustomerModel[] Customer_SelectRemainingAmount(string sCustomer_ID);
        string Customer_UpdateRemainingAmount(string customerId, double amount);
        string Customer_Update(CustomerModel customerModel);
        string Customer_UpdateIsActive(string sCustomer_ID, string sIsActive, string sCreatedBY, string sReason);
        string Customer_Update_AdvancePayment(string sTotalAdvancePayment, string sCustomer_ID, string sTotalRemainigAmount);
    }

    public class ClientProcessor : IClientProcessor
    {
        private readonly ILogger _logger;
        private readonly IClientRepository _clientRepo;

        public ClientProcessor()
        {
            _logger = FileLogger.Instance;
            _clientRepo = new ClientRepository();
        }

        public CustomerModel[] Customers_Search(string SearchText)
        {
            try
            {
                return _clientRepo.Customers_Search(SearchText);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public CustomerModel[] Customers_SelectAll(string sOrderBy, string sGridPageNumber, string sUserName, string sIsActive, string sSearchText, string sSearchBy)
        {
            try
            {
                return _clientRepo.Customers_SelectAll(sOrderBy, sGridPageNumber, sUserName, sIsActive, sSearchText, sSearchBy);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public string Customer_Insert(CustomerModel customerModel)
        {
            try
            {
                bool result = _clientRepo.Customer_Insert(customerModel);
                if (result)
                {
                    return XmlProcessor.ReadXmlFile("CUS103");
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public string Customer_IsExist(CustomerModel customerModel)
        {
            try
            {
                bool result = _clientRepo.Customer_IsExist(customerModel);
                if (result)
                {
                    return XmlProcessor.ReadXmlFile("CUW110");
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public DataTable Customer_Select(string sCustomer_ID)
        {
            try
            {
                return _clientRepo.Customer_Select(sCustomer_ID);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public DataTable Customer_SelectCount()
        {
            try
            {
                return _clientRepo.Customer_SelectCount();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public CustomerModel[] Customer_SelectName(string sAgency_ID)
        {
            try
            {
                return _clientRepo.Customer_SelectName(sAgency_ID);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public CustomerModel[] Customer_SelectRemainingAmount(string sCustomer_ID)
        {
            try
            {
                return _clientRepo.Customer_SelectRemainingAmount(sCustomer_ID);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public string Customer_Update(CustomerModel customerModel)
        {
            try
            {
                bool result = _clientRepo.Customer_Update(customerModel);
                if (result)
                {
                    return XmlProcessor.ReadXmlFile("CUS104");
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public string Customer_UpdateIsActive(string sCustomer_ID, string sIsActive, string sCreatedBY, string sReason)
        {
            try
            {
                bool result = _clientRepo.Customer_UpdateIsActive(sCustomer_ID, sIsActive, sCreatedBY, sReason);
                if (result)
                {
                    return XmlProcessor.ReadXmlFile("CUS106");
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public string Customer_UpdateRemainingAmount(string customerId, double amount)
        {
            try
            {
                bool result = _clientRepo.Customer_UpdateReminingAmount(customerId, amount);
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

        public string Customer_Update_AdvancePayment(string sTotalAdvancePayment, string sCustomer_ID, string sTotalRemainigAmount)
        {
            try
            {
                bool result = _clientRepo.Customer_Update_AdvancePayment(sTotalAdvancePayment, sCustomer_ID, sTotalRemainigAmount);
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
