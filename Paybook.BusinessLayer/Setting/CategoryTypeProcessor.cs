using Paybook.BusinessLayer.Business;
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
    public interface ICategoryTypeProcessor : IBaseProcessor<CategoryTypeMasterModel>
    {
    }

    public class CategoryTypeProcessor : ICategoryTypeProcessor
    {
        private readonly ILogger _logger;
        private readonly ICategoryTypeRepository _type;
        private readonly IBusinessProcessor _business;

        public CategoryTypeProcessor()
        {
            _logger = LoggerFactory.Instance;
            _type = new CategoryTypeRepository();
            _business = new BusinessProcessor();
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
                    output.ReturnMessage = Messages.Get(MTypes.CategoryTypeMaster, MStatus.InsertSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = Messages.Get(MTypes.CategoryTypeMaster, MStatus.InsertFailure);
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
                    output.ReturnMessage = Messages.Get(MTypes.CategoryTypeMaster, MStatus.UpdateSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = Messages.Get(MTypes.CategoryTypeMaster, MStatus.UpdateFailure);
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
                        output.ReturnMessage = Messages.Get(MTypes.CategoryTypeMaster, MStatus.ActivateSuccess);
                    else
                        output.ReturnMessage = Messages.Get(MTypes.CategoryTypeMaster, MStatus.DeactivateSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                if (active)
                    output.ReturnMessage = Messages.Get(MTypes.CategoryTypeMaster, MStatus.ActivateFailure);
                else
                    output.ReturnMessage = Messages.Get(MTypes.CategoryTypeMaster, MStatus.DeactivateFailure);
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
                    output.ReturnMessage = Messages.Get(MTypes.CategoryTypeMaster, MStatus.DeleteSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = Messages.Get(MTypes.CategoryTypeMaster, MStatus.DeleteFailure);
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
