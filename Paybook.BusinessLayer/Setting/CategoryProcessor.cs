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
    public interface ICategoryProcessor
    {
        List<CategoryMasterModel> GetAllByTypeCore(int businessId, string typeCore);
        CategoryMasterModel GetByCore(int businessId, string core);
        List<CategoryMasterModel> GetAllByPage(int businessId, int page, string search, string orderBy);
        CategoryMasterModel GetById(int businessId, int id);
        string Create(CategoryMasterModel model);
        string Update(CategoryMasterModel model);
        string Activate(int businessId, int id, bool active);
        string Delete(int businessId, int id);
    }

    public class CategoryProcessor : ICategoryProcessor
    {
        private readonly ILogger _logger;
        private readonly ICategoryRepository _category;

        public CategoryProcessor()
        {
            _logger = LoggerFactory.Instance;
            _category = new CategoryRepository();
        }

        public List<CategoryMasterModel> GetAllByTypeCore(int businessId, string typeCore)
        {
            try
            {
                return _category.GetAllByTypeCore(businessId, typeCore);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public CategoryMasterModel GetByCore(int businessId, string core)
        {
            try
            {
                return _category.GetByCore(businessId, core);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }


        public List<CategoryMasterModel> GetAllByPage(int businessId, int page, string search, string orderBy)
        {
            try
            {
                return _category.GetAllByPage(businessId, page, search, orderBy);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public CategoryMasterModel GetById(int businessId, int id)
        {
            try
            {
                return _category.GetById(businessId, id);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public string Create(CategoryMasterModel model)
        {
            try
            {
                int result = _category.Create(model);
                if (result > 0)
                    return XmlProcessor.ReadXmlFile("BSS003");

                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public string Update(CategoryMasterModel model)
        {
            try
            {
                int result = _category.Update(model);
                if (result > 0)
                    return XmlProcessor.ReadXmlFile("BSS004");

                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public string Activate(int businessId, int id, bool active)
        {
            try
            {
                int result = _category.Activate(businessId, id, active);
                if (result > 0)
                    return XmlProcessor.ReadXmlFile("BSS004");

                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public string Delete(int businessId, int id)
        {
            try
            {
                int result = _category.Delete(businessId, id);
                if (result > 0)
                    return XmlProcessor.ReadXmlFile("BSS004");

                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
    }
}
