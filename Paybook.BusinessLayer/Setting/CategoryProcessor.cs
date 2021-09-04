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
    public interface ICategoryProcessor : IBaseProcessor<CategoryMasterModel>
    {
        List<CategoryMasterModel> GetAllByTypeCore(int businessId, string typeCore);
        CategoryMasterModel GetByCore(int businessId, string core);
    }

    public class CategoryProcessor : ICategoryProcessor
    {
        private readonly ILogger _logger;
        private readonly ICategoryRepository _category;
        private readonly IBusinessProcessor _business;

        public CategoryProcessor()
        {
            _logger = LoggerFactory.Instance;
            _category = new CategoryRepository();
            _business = new BusinessProcessor();
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
        public CategoryMasterModel Create(CategoryMasterModel model)
        {
            try
            {
                var output = new CategoryMasterModel();
                int result = _category.Create(model);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = Messages.Get(MTypes.CategoryMaster, MStatus.InsertSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = Messages.Get(MTypes.CategoryMaster, MStatus.InsertFailure);
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
                int result = _category.Update(model);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = Messages.Get(MTypes.CategoryMaster, MStatus.UpdateSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = Messages.Get(MTypes.CategoryMaster, MStatus.UpdateFailure);
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
                int result = _category.Activate(businessId, username, id, active);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    if (active)
                        output.ReturnMessage = Messages.Get(MTypes.CategoryMaster, MStatus.ActivateSuccess);
                    else
                        output.ReturnMessage = Messages.Get(MTypes.CategoryMaster, MStatus.DeactivateSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                if (active)
                    output.ReturnMessage = Messages.Get(MTypes.CategoryMaster, MStatus.ActivateFailure);
                else
                    output.ReturnMessage = Messages.Get(MTypes.CategoryMaster, MStatus.DeactivateFailure);
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
                int result = _category.Delete(businessId, username, id);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = Messages.Get(MTypes.CategoryMaster, MStatus.DeleteSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = Messages.Get(MTypes.CategoryMaster, MStatus.DeleteFailure);
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
