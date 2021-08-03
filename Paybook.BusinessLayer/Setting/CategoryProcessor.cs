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
        DataTable Categories_Select();
        CategoryModel[] SubCategories_Active_Select(string sCategory_Core);
        string SubCategories_Insert(CategoryModel categoryModel);
        CategoryModel[] SubCategories_SelectAll(string sCategory_Core);
        CategoryModel[] SubCategories_SelectGrid(string sOrderBy, string sGridPageNumber, string sUserName, string sCategory_Core);
        string SubCategories_Update(CategoryModel categoryModel);
        string SubCategories_UpdateIsActive(string sID, string sISActive);
        CategoryModel[] SubCategory_IsExist(string sSubCategory_Core, string sCategory_Core);
        DataTable SubCategory_SelectGstValues(string sCategoryCore);
    }

    public class CategoryProcessor : ICategoryProcessor
    {
        private ILogger _logger;
        private readonly ICategoryRepository _category;

        public CategoryProcessor()
        {
            _logger = FileLogger.Instance;
            _category = new CategoryRepository();
        }

        public DataTable Categories_Select()
        {
            try
            {
                return _category.Categories_Select();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }

        public CategoryModel[] SubCategories_Active_Select(string sCategory_Core)
        {
            try
            {
                return _category.SubCategories_Active_Select(sCategory_Core);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }

        public string SubCategories_Insert(CategoryModel categoryModel)
        {
            try
            {
                bool result= _category.SubCategories_Insert(categoryModel);
                if (result)
                    return XmlProcessor.ReadXmlFile("BSS003");

                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }

        public CategoryModel[] SubCategories_SelectAll(string sCategory_Core)
        {
            try
            {
                return _category.SubCategories_SelectAll(sCategory_Core);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }

        public CategoryModel[] SubCategories_SelectGrid(string sOrderBy, string sGridPageNumber, string sUserName, string sCategory_Core)
        {
            try
            {
                return _category.SubCategories_SelectGrid(sOrderBy, sGridPageNumber, sUserName, sCategory_Core);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }

        public string SubCategories_Update(CategoryModel categoryModel)
        {
            try
            {
                bool result = _category.SubCategories_Update(categoryModel);
                if (result)
                    return XmlProcessor.ReadXmlFile("BSS004");

                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }

        public string SubCategories_UpdateIsActive(string sID, string sISActive)
        {
            try
            {
                bool result = _category.SubCategories_UpdateIsActive(sID, sISActive);
                if (result)
                    return XmlProcessor.ReadXmlFile("BSS004");

                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }

        public CategoryModel[] SubCategory_IsExist(string sSubCategory_Core, string sCategory_Core)
        {
            try
            {
                return _category.SubCategory_IsExist(sSubCategory_Core, sCategory_Core);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }

        public DataTable SubCategory_SelectGstValues(string sCategoryCore)
        {
            try
            {
                return _category.SubCategory_SelectGstValues(sCategoryCore);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
    }
}
