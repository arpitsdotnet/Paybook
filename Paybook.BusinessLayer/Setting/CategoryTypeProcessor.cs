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
    public interface ICategoryTypeProcessor
    {
        List<CategoryTypeMasterModel> GetAllByPage(int businessId, int page, string search, string orderBy);
        CategoryTypeMasterModel GetById(int businessId, int id);
        CategoryTypeMasterModel Create(CategoryTypeMasterModel model);
        CategoryTypeMasterModel Update(CategoryTypeMasterModel model);
        CategoryTypeMasterModel Activate(int businessId, int id, bool active);
        CategoryTypeMasterModel Delete(int businessId, int id);
    }

    public class CategoryTypeProcessor : ICategoryTypeProcessor
    {
        private readonly ILogger _logger;
        private readonly ICategoryTypeRepository _type;

        public CategoryTypeProcessor()
        {
            _logger = LoggerFactory.Instance;
            _type = new CategoryTypeRepository();
        }

        public List<CategoryTypeMasterModel> GetAllByPage(int businessId, int page, string search, string orderBy)
        {
            try
            {
                return _type.GetAllByPage(businessId, page, search, orderBy);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public CategoryTypeMasterModel GetById(int businessId, int id)
        {
            try
            {
                return _type.GetById(businessId, id);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public CategoryTypeMasterModel Create(CategoryTypeMasterModel model)
        {
            try
            {
                CategoryTypeMasterModel output = new CategoryTypeMasterModel { IsSucceeded = false };
                int result = _type.Create(model);
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
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public CategoryTypeMasterModel Update(CategoryTypeMasterModel model)
        {
            try
            {
                CategoryTypeMasterModel output = new CategoryTypeMasterModel { IsSucceeded = false };
                int result = _type.Update(model);
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
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public CategoryTypeMasterModel Activate(int businessId, int id, bool active)
        {
            try
            {
                CategoryTypeMasterModel output = new CategoryTypeMasterModel { IsSucceeded = false };
                int result = _type.Activate(businessId, id, active);
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
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public CategoryTypeMasterModel Delete(int businessId, int id)
        {
            try
            {
                CategoryTypeMasterModel output = new CategoryTypeMasterModel { IsSucceeded = false };
                int result = _type.Delete(businessId, id);
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
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
    }
}
