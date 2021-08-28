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
    public interface ICategoryProcessor
    {
        List<CategoryMasterModel> GetAllByTypeCore(string username, string typeCore);
        CategoryMasterModel GetByCore(string username, string core);
        List<CategoryMasterModel> GetAllByPage(string username, int page, string search, string orderBy);
        CategoryMasterModel GetById(string username, int id);
        CategoryMasterModel Create(CategoryMasterModel model);
        CategoryMasterModel Update(CategoryMasterModel model);
        CategoryMasterModel Activate(string username, int id, bool active);
        CategoryMasterModel Delete(string username, int id);
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

        public List<CategoryMasterModel> GetAllByTypeCore(string username, string typeCore)
        {
            try
            {

                var business = _business.GetSelectedByUsername(username);

                return _category.GetAllByTypeCore(business.Id, typeCore);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public CategoryMasterModel GetByCore(string username, string core)
        {
            try
            {

                var business = _business.GetSelectedByUsername(username);

                return _category.GetByCore(business.Id, core);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }


        public List<CategoryMasterModel> GetAllByPage(string username, int page, string search, string orderBy)
        {
            try
            {

                var business = _business.GetSelectedByUsername(username);

                return _category.GetAllByPage(business.Id, page, search, orderBy);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public CategoryMasterModel GetById(string username, int id)
        {
            try
            {

                var business = _business.GetSelectedByUsername(username);

                return _category.GetById(business.Id, id);
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
                var business = _business.GetSelectedByUsername(model.CreateBy);

                var output = new CategoryMasterModel();
                model.BusinessId = business.Id;

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
        public CategoryMasterModel Activate(string username, int id, bool active)
        {
            try
            {

                var business = _business.GetSelectedByUsername(username);

                var output = new CategoryMasterModel();
                int result = _category.Activate(business.Id, id, active);
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
        public CategoryMasterModel Delete(string username, int id)
        {
            try
            {
                var business = _business.GetSelectedByUsername(username);

                var output = new CategoryMasterModel();
                int result = _category.Delete(business.Id, id);
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
