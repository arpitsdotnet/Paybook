using System;
using System.Collections.Generic;
using Paybook.BusinessLayer.Abstracts.Admins;
using Paybook.DatabaseLayer.Abstracts.Admins;
using Paybook.DatabaseLayer.Features.Admins;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;

namespace Paybook.BusinessLayer.Features.Admins
{
    public class CountryProcessor : ICountryProcessor
    {
        private readonly ILogger _logger;
        private readonly ICountryRepository _country;

        public CountryProcessor(ILogger logger)
        {
            _logger = logger;
            _country = new CountryRepository();
        }

        public List<CountryMasterModel> GetAllByPage(int page, string search, string orderBy)
        {
            try
            {
                return _country.GetAllByPage(page, search, orderBy);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public CountryMasterModel GetById(int id)
        {
            try
            {
                return _country.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public CountryMasterModel Create(CountryMasterModel model)
        {
            try
            {
                var output = new CountryMasterModel();
                int result = _country.Create(model);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.CountryMaster, MStatus.InsertSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = XmlMessageHelper.Get(MTypes.CountryMaster, MStatus.InsertFailure);
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public CountryMasterModel Update(CountryMasterModel model)
        {
            try
            {
                var output = new CountryMasterModel { IsSucceeded = false };
                int result = _country.Update(model);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.CountryMaster, MStatus.UpdateSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = XmlMessageHelper.Get(MTypes.CountryMaster, MStatus.UpdateFailure);
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public CountryMasterModel Activate(int id, bool active)
        {
            try
            {
                var output = new CountryMasterModel { IsSucceeded = false };
                int result = _country.Activate(id, active);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    if (active)
                        output.ReturnMessage = XmlMessageHelper.Get(MTypes.CountryMaster, MStatus.ActivateSuccess);
                    else
                        output.ReturnMessage = XmlMessageHelper.Get(MTypes.CountryMaster, MStatus.DeactivateSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                if (active)
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.CountryMaster, MStatus.ActivateFailure);
                else
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.CountryMaster, MStatus.DeactivateFailure);
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public CountryMasterModel Delete(int id)
        {
            try
            {
                var output = new CountryMasterModel { IsSucceeded = false };
                int result = _country.Delete(id);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.CountryMaster, MStatus.DeleteSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = XmlMessageHelper.Get(MTypes.CountryMaster, MStatus.DeleteFailure);
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
