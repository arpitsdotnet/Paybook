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
        string IsExists(ClientModel customerModel);
        int GetCount(int businessId);
        ClientModel[] GetAllByText(string SearchText);
        ClientModel[] GetAllNamesByAgencyID(string sAgency_ID);
        ClientModel[] Customer_SelectRemainingAmount(string sCustomer_ID);


        List<ClientModel> GetAllByPage(int businessId, int page, string search, string orderBy);
        ClientModel GetById(int businessId, int id);
        string Create(ClientModel customerModel);
        string Update(ClientModel customerModel);
        string Activate(int businessId, int id, bool active);
        string Delete(int businessId, int id);
    }

    public class ClientProcessor : IClientProcessor
    {
        private readonly ILogger _logger;
        private readonly IClientRepository _clientRepo;

        public ClientProcessor()
        {
            _logger = LoggerFactory.Instance;
            _clientRepo = new ClientRepository();
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
        public int GetCount(int businessId)
        {
            try
            {
                return _clientRepo.GetCount(businessId);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        public ClientModel[] GetAllByText(string SearchText)
        {
            try
            {
                return null;// _clientRepo.GetAllByText(SearchText);
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
                return null;// _clientRepo.GetAllNamesByAgencyID(sAgency_ID);
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

        public List<ClientModel> GetAllByPage(int businessId, int page, string search, string orderBy)
        {
            try
            {
                return _clientRepo.GetAllByPage(businessId, page, search, orderBy);
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
                return _clientRepo.GetById(businessId, id);
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
                int result = _clientRepo.Create(customerModel);
                if (result > 0)
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
        public string Update(ClientModel customerModel)
        {
            try
            {
                int result = _clientRepo.Update(customerModel);
                if (result > 0)
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
        public string Activate(int businessId, int id, bool active)
        {
            try
            {
                int result = _clientRepo.Activate(businessId, id, active);
                if (result > 0)
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
        public string Delete(int businessId, int id)
        {
            try
            {
                int result = _clientRepo.Delete(businessId, id);
                if (result > 0)
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
    }
}
