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
        ClientModel[] GetAllByText(string SearchText);
        ClientModel[] GetAllByPage(string sOrderBy, string sGridPageNumber, string sUserName, string sIsActive, string sSearchText, string sSearchBy);
        string Create(ClientModel customerModel);
        string IsExists(ClientModel customerModel);
        DataTable GetByClientID(string sCustomer_ID);
        DataTable GetCount();
        ClientModel[] GetAllNamesByAgencyID(string sAgency_ID);
        ClientModel[] Customer_SelectRemainingAmount(string sCustomer_ID);
        string Customer_UpdateRemainingAmount(string customerId, double amount);
        string Customer_Update(ClientModel customerModel);
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

        public ClientModel[] GetAllByText(string SearchText)
        {
            try
            {
                return _clientRepo.GetAllByText(SearchText);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public ClientModel[] GetAllByPage(string sOrderBy, string sGridPageNumber, string sUserName, string sIsActive, string sSearchText, string sSearchBy)
        {
            try
            {
                return _clientRepo.GetAllByPage(sOrderBy, sGridPageNumber, sUserName, sIsActive, sSearchText, sSearchBy);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public string Create(ClientModel customerModel)
        {
            try
            {
                bool result = _clientRepo.Create(customerModel);
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

        public string IsExists(ClientModel customerModel)
        {
            try
            {
                bool result = _clientRepo.IsExists(customerModel);
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

        public DataTable GetByClientID(string sCustomer_ID)
        {
            try
            {
                return _clientRepo.GetByClientID(sCustomer_ID);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public DataTable GetCount()
        {
            try
            {
                return _clientRepo.GetCount();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public ClientModel[] GetAllNamesByAgencyID(string sAgency_ID)
        {
            try
            {
                return _clientRepo.GetAllNamesByAgencyID(sAgency_ID);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public ClientModel[] Customer_SelectRemainingAmount(string sCustomer_ID)
        {
            try
            {
                return _clientRepo.GetPaymentByClientID(sCustomer_ID);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public string Customer_Update(ClientModel customerModel)
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
                bool result = _clientRepo.UpdatePayment(customerId, amount);
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
