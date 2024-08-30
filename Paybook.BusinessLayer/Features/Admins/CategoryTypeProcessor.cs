using System;
using System.Collections.Generic;
using Paybook.BusinessLayer.Abstracts.Admins;
using Paybook.DatabaseLayer.Setting;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;

namespace Paybook.BusinessLayer.Features.Admins
{
    public class CategoryTypeProcessor : ICategoryTypeProcessor
    {
        private readonly ILogger _logger;
        private readonly ICategoryTypeRepository _type;

        public CategoryTypeProcessor(
            ILogger logger)
        {
            _logger = logger;
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
                CategoryTypeMasterModel output = new CategoryTypeMasterModel();

                int result = _type.Create(model);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.CategoryTypeMaster, MStatus.InsertSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = XmlMessageHelper.Get(MTypes.CategoryTypeMaster, MStatus.InsertFailure);
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
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.CategoryTypeMaster, MStatus.UpdateSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = XmlMessageHelper.Get(MTypes.CategoryTypeMaster, MStatus.UpdateFailure);
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public CategoryTypeMasterModel Activate(int businessId, string username, int id, bool active)
        {
            try
            {
                CategoryTypeMasterModel output = new CategoryTypeMasterModel { IsSucceeded = false };

                int result = _type.Activate(businessId, username, id, active);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    if (active)
                        output.ReturnMessage = XmlMessageHelper.Get(MTypes.CategoryTypeMaster, MStatus.ActivateSuccess);
                    else
                        output.ReturnMessage = XmlMessageHelper.Get(MTypes.CategoryTypeMaster, MStatus.DeactivateSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                if (active)
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.CategoryTypeMaster, MStatus.ActivateFailure);
                else
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.CategoryTypeMaster, MStatus.DeactivateFailure);
                return output;

            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public CategoryTypeMasterModel Delete(int businessId, string username, int id)
        {
            try
            {
                CategoryTypeMasterModel output = new CategoryTypeMasterModel { IsSucceeded = false };

                int result = _type.Delete(businessId, username, id);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.CategoryTypeMaster, MStatus.DeleteSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = XmlMessageHelper.Get(MTypes.CategoryTypeMaster, MStatus.DeleteFailure);
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
