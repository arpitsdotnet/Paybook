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
        string IsExist(string createBy, string clientName);
        int GetCount(int businessId);
        ClientModel[] GetAllByText(string SearchText);
        ClientModel[] GetAllNamesByAgencyID(string sAgency_ID);
        ClientModel[] Customer_SelectRemainingAmount(string sCustomer_ID);


        List<ClientModel> GetAllByPage(int businessId, int page, string search, string orderBy);
        ClientModel GetById(int businessId, int id);
        ClientModel Create(ClientModel customerModel);
        ClientModel Update(ClientModel customerModel);
        ClientModel Activate(int businessId, int id, bool active);
        ClientModel Delete(int businessId, int id);
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

        public string IsExist(string createBy, string clientName)
        {
            try
            {
                bool result = _clientRepo.IsExist(createBy, clientName);
                if (result)
                {
                    return XmlProcessor.ReadXmlFile("CUW110");
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

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
                _logger.Error(_logger.GetMethodName(), ex);

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
                _logger.Error(_logger.GetMethodName(), ex);

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
                _logger.Error(_logger.GetMethodName(), ex);

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
                _logger.Error(_logger.GetMethodName(), ex);

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
                _logger.Error(_logger.GetMethodName(), ex);

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
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public ClientModel Create(ClientModel model)
        {
            try
            {
                var output = new ClientModel { IsSucceeded = false };

                string returnMessage = IsExist(model.CreateBy, model.Name);
                if (!string.IsNullOrEmpty(returnMessage))
                {
                    output.ReturnMessage = "Client name already exist, please enter a new name";
                    return output;
                }
                int result = _clientRepo.Create(model);
                if (result == 0)
                {
                    output.ReturnMessage = "Current request failed due to technical issue, please try again later.";
                    return output;
                }

                output.IsSucceeded = true;
                output.ReturnMessage = "Client has been created successfully.";// XmlProcessor.ReadXmlFile("CPS401");
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public ClientModel Update(ClientModel model)
        {
            try
            {
                var output = new ClientModel { IsSucceeded = false };
                int result = _clientRepo.Update(model);
                if (result == 0)
                {
                    output.ReturnMessage = "Current request failed due to technical issue, please try again later.";
                    //return XmlProcessor.ReadXmlFile("CUS104");
                    return output;
                }

                output.IsSucceeded = true;
                output.ReturnMessage = "Client has been updated successfully.";// XmlProcessor.ReadXmlFile("CPS401");
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public ClientModel Activate(int businessId, int id, bool active)
        {
            try
            {
                var output = new ClientModel { IsSucceeded = false };
                int result = _clientRepo.Activate(businessId, id, active);
                if (result > 0)
                {
                    output.ReturnMessage = "Current request failed due to technical issue, please try again later.";
                    //return XmlProcessor.ReadXmlFile("CUS104");
                    return output;
                }

                output.IsSucceeded = true;
                output.ReturnMessage = "Client has been activated/deactivated successfully.";// XmlProcessor.ReadXmlFile("CPS401");
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public ClientModel Delete(int businessId, int id)
        {
            try
            {
                var output = new ClientModel { IsSucceeded = false };
                int result = _clientRepo.Delete(businessId, id);
                if (result > 0)
                {
                    output.ReturnMessage = "Current request failed due to technical issue, please try again later.";
                    //return XmlProcessor.ReadXmlFile("CUS104");
                    return output;
                }

                output.IsSucceeded = true;
                output.ReturnMessage = "Client has been deleted successfully.";// XmlProcessor.ReadXmlFile("CPS401");
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
    }
}
