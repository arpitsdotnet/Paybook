using System;
using System.Collections.Generic;
using Paybook.BusinessLayer.Abstracts.Customers;
using Paybook.DatabaseLayer.Client;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models.Clients;
using Paybook.ServiceLayer.Xml;

namespace Paybook.BusinessLayer.Features.Customers
{
    public class ClientProcessor : IClientProcessor
    {
        private readonly ILogger _logger;
        private readonly IClientRepository _clientRepo;

        public ClientProcessor(ILogger logger)
        {
            _logger = logger;
            _clientRepo = new ClientRepository();
        }

        public bool IsExist(string createBy, string clientName)
        {
            try
            {
                return _clientRepo.IsExist(createBy, clientName);
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
        public ClientDetailsCountersModel GetCountersById(int businessId, int Id)
        {
            try
            {
                return _clientRepo.GetCountersById(businessId, Id);
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

        public decimal GetBalanceTotalById(int businessId, int id)
        {
            try
            {
                return _clientRepo.GetBalanceTotalById(businessId, id);
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

                bool resultIsExists = IsExist(model.CreateBy, model.Name);
                if (resultIsExists)
                {
                    output.IsSucceeded = false;
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.Client, MStatus.IsExists);
                    return output;
                }

                int result = _clientRepo.Create(model);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.Client, MStatus.InsertSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = XmlMessageHelper.Get(MTypes.Client, MStatus.InsertFailure);
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
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.Client, MStatus.UpdateSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = XmlMessageHelper.Get(MTypes.Client, MStatus.UpdateFailure);
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public ClientModel Activate(int businessId, string username, int id, bool active)
        {
            try
            {
                var output = new ClientModel { IsSucceeded = false };

                int result = _clientRepo.Activate(businessId, username, id, active);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    if (active)
                        output.ReturnMessage = XmlMessageHelper.Get(MTypes.Client, MStatus.ActivateSuccess);
                    else
                        output.ReturnMessage = XmlMessageHelper.Get(MTypes.Client, MStatus.DeactivateSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                if (active)
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.Client, MStatus.ActivateFailure);
                else
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.Client, MStatus.DeactivateFailure);
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public ClientModel Delete(int businessId, string username, int id)
        {
            try
            {
                var output = new ClientModel { IsSucceeded = false };

                int result = _clientRepo.Delete(businessId, username, id);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.Client, MStatus.DeleteSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = XmlMessageHelper.Get(MTypes.Client, MStatus.DeleteFailure);
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
