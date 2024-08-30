using System;
using System.Collections.Generic;
using Paybook.BusinessLayer.Abstracts.Admins;
using Paybook.BusinessLayer.Abstracts.Customers;
using Paybook.DatabaseLayer.Setting;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;

namespace Paybook.BusinessLayer.Features.Admins
{
    public class CategoryProcessor : ICategoryProcessor
    {
        private readonly ILogger _logger;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryProcessor(
            ILogger logger)
        {
            _logger = logger;
            _categoryRepository = new CategoryRepository();
        }

        public List<CategoryMasterModel> GetAllByTypeCore(int businessId, string typeCore)
        {
            try
            {
                return _categoryRepository.GetAllByTypeCore(businessId, typeCore);
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
                return _categoryRepository.GetByCore(businessId, core);
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
                return _categoryRepository.GetAllByPage(businessId, page, search, orderBy);
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
                return _categoryRepository.GetById(businessId, id);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public CategoryMasterModel Create(CategoryMasterModel model)
        {
            try
            {
                var output = new CategoryMasterModel();
                int result = _categoryRepository.Create(model);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.CategoryMaster, MStatus.InsertSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = XmlMessageHelper.Get(MTypes.CategoryMaster, MStatus.InsertFailure);
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public CategoryMasterModel Update(CategoryMasterModel model)
        {
            try
            {
                var output = new CategoryMasterModel();
                int result = _categoryRepository.Update(model);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.CategoryMaster, MStatus.UpdateSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = XmlMessageHelper.Get(MTypes.CategoryMaster, MStatus.UpdateFailure);
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public CategoryMasterModel Activate(int businessId, string username, int id, bool active)
        {
            try
            {
                var output = new CategoryMasterModel();
                int result = _categoryRepository.Activate(businessId, username, id, active);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    if (active)
                        output.ReturnMessage = XmlMessageHelper.Get(MTypes.CategoryMaster, MStatus.ActivateSuccess);
                    else
                        output.ReturnMessage = XmlMessageHelper.Get(MTypes.CategoryMaster, MStatus.DeactivateSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                if (active)
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.CategoryMaster, MStatus.ActivateFailure);
                else
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.CategoryMaster, MStatus.DeactivateFailure);
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public CategoryMasterModel Delete(int businessId, string username, int id)
        {
            try
            {
                var output = new CategoryMasterModel();
                int result = _categoryRepository.Delete(businessId, username, id);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.CategoryMaster, MStatus.DeleteSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = XmlMessageHelper.Get(MTypes.CategoryMaster, MStatus.DeleteFailure);
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
