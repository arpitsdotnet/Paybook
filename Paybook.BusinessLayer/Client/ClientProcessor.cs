﻿using Paybook.DatabaseLayer;
using Paybook.DatabaseLayer.Business;
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
        int GetCount(string username);
        ClientModel[] GetAllByText(string SearchText);
        ClientModel[] GetAllNamesByAgencyID(string sAgency_ID);
        ClientModel[] Customer_SelectRemainingAmount(string sCustomer_ID);
        decimal GetRemainingAmountById(string username, int id);


        List<ClientModel> GetAllByPage(string username, int page, string search, string orderBy);
        ClientModel GetById(string username, int id);
        ClientModel Create(ClientModel customerModel);
        ClientModel Update(ClientModel customerModel);
        ClientModel Activate(string username, int id, bool active);
        ClientModel Delete(string username, int id);
    }

    public class ClientProcessor : IClientProcessor
    {
        private readonly ILogger _logger;
        private readonly IClientRepository _clientRepo;
        private readonly IBusinessRepository _businessRepo;

        public ClientProcessor()
        {
            _logger = LoggerFactory.Instance;
            _clientRepo = new ClientRepository();
            _businessRepo = new BusinessRepository();
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
        public int GetCount(string username)
        {
            try
            {
                var business = _businessRepo.GetSelectedByUsername(username);

                return _clientRepo.GetCount(business.Id);
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

        public decimal GetRemainingAmountById(string username, int id)
        {
            try
            {
                var business = _businessRepo.GetSelectedByUsername(username);

                return _clientRepo.GetRemainingAmountById(business.Id, id);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }

        public List<ClientModel> GetAllByPage(string username, int page, string search, string orderBy)
        {
            try
            {
                var business = _businessRepo.GetSelectedByUsername(username);

                return _clientRepo.GetAllByPage(business.Id, page, search, orderBy);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public ClientModel GetById(string username, int id)
        {
            try
            {
                var business = _businessRepo.GetSelectedByUsername(username);

                return _clientRepo.GetById(business.Id, id);
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
                var business = _businessRepo.GetSelectedByUsername(model.CreateBy);

                model.BusinessId = business.Id;

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

                var business = _businessRepo.GetSelectedByUsername(model.ModifyBy);

                model.BusinessId = business.Id;

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
        public ClientModel Activate(string username, int id, bool active)
        {
            try
            {
                var output = new ClientModel { IsSucceeded = false };

                var business = _businessRepo.GetSelectedByUsername(username);

                int result = _clientRepo.Activate(business.Id, id, active);
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
        public ClientModel Delete(string username, int id)
        {
            try
            {
                var output = new ClientModel { IsSucceeded = false };

                var business = _businessRepo.GetSelectedByUsername(username);

                int result = _clientRepo.Delete(business.Id, id);
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
