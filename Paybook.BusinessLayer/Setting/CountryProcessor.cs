using Paybook.DatabaseLayer;
using Paybook.DatabaseLayer.Setting;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.BusinessLayer.Setting
{
    public interface ICountryProcessor
    {
        List<CountryMasterModel> GetAllByPage(int page, string search, string orderBy);
        CountryMasterModel GetById(int id);
        CountryMasterModel Create(CountryMasterModel model);
        CountryMasterModel Update(CountryMasterModel model);
        CountryMasterModel Activate(int id, bool active);
        CountryMasterModel Delete(int id);
    }

    public class CountryProcessor : ICountryProcessor
    {
        private readonly ILogger _logger;
        private readonly ICountryRepository _country;

        public CountryProcessor()
        {
            _logger = LoggerFactory.Instance;
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
                _logger.LogError(_logger.MethodName, ex);

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
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        public CountryMasterModel Create(CountryMasterModel model)
        {
            try
            {
                var output = new CountryMasterModel { IsSucceeded = false };
                int result = _country.Create(model);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = XmlProcessor.ReadXmlFile("NDS902");
                }
                output.ReturnMessage = XmlProcessor.ReadXmlFile("NoteUpdateFail");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

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
                    output.ReturnMessage = XmlProcessor.ReadXmlFile("NDS902");
                }
                output.ReturnMessage = XmlProcessor.ReadXmlFile("NoteUpdateFail");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

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
                    output.ReturnMessage = XmlProcessor.ReadXmlFile("NDS902");
                }
                output.ReturnMessage = XmlProcessor.ReadXmlFile("NoteActivateFail");
                return output;

            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

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
                    output.ReturnMessage = XmlProcessor.ReadXmlFile("NDS902");
                }
                output.ReturnMessage = XmlProcessor.ReadXmlFile("NoteDeleteFail");
                return output;

            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
    }
}
