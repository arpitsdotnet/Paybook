using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Constants;
using Paybook.ServiceLayer.Extensions;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Paybook.ServiceLayer.Logger;

namespace Paybook.DatabaseLayer.Setting
{
    public interface ICategoryRepository
    {
        DataTable Categories_Select();
        CategoryModel[] SubCategories_Active_Select(string sCategory_Core);
        bool SubCategories_Insert(CategoryModel categoryModel);
        CategoryModel[] SubCategories_SelectAll(string sCategory_Core);
        CategoryModel[] SubCategories_SelectGrid(string sOrderBy, string sGridPageNumber, string sUserName, string sCategory_Core);
        bool SubCategories_Update(CategoryModel categoryModel);
        bool SubCategories_UpdateIsActive(string sID, string sISActive);
        CategoryModel[] SubCategory_IsExist(string sSubCategory_Core, string sCategory_Core);
        DataTable SubCategory_SelectGstValues(string sCategoryCore);
    }

    public class CategoryRepository : ICategoryRepository
    {
        private readonly ILogger _logger;
        private readonly IDbContext _dbContext;

        public CategoryRepository()
        {
            _logger = FileLogger.Instance;
            _dbContext = DbContextFactory.Instance;
        }

        public CategoryModel[] SubCategories_Active_Select(string sCategory_Core)
        {
            try
            {
                List<Parameter> oParams = new List<Parameter>();
                oParams.Add(new Parameter("sCategory_Core", sCategory_Core));

                List<CategoryModel> categories = new List<CategoryModel>();
                DataTable dt = _dbContext.LoadDataByProcedure("sps_SubCategories_Active_Select", oParams);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        CategoryModel category = new CategoryModel
                        {
                            CreatedBY = dr["CreatedBY"].ToString(),
                            CreatedDT = dr["CreatedDT"].ToString(),
                            SubCategory_Core = dr["SubCategory_Core"].ToString(),
                            SubCategory_Disp = dr["SubCategory_Disp"].ToString()
                        };
                        categories.Add(category);
                    }
                }
                return categories.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public CategoryModel[] SubCategories_SelectGrid(string sOrderBy, string sGridPageNumber, string sUserName, string sCategory_Core)
        {
            List<CategoryModel> categories = new List<CategoryModel>();
            try
            {
                List<Parameter> oParams = new List<Parameter>();
                oParams.Add(new Parameter("sCategory_Core", sCategory_Core));

                DataTable dt = _dbContext.LoadDataByProcedure("sps_SubCategories_SelectAll", oParams);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string dtCount = dt.Rows.Count.ToString();
                    // int dRowTotal = int.Parse(dtCount);
                    int iPageNumber = Convert.ToInt32(sGridPageNumber);
                    int iPageStart = iPageNumber == 0 ? 0 : (PagerSetting.iPageSizeDefault * iPageNumber);

                    var list = (from e in dt.AsEnumerable()
                                select new
                                {
                                    RowCount = dtCount,
                                    ID = e.Field<int>("ID"),
                                    SubCategory_Core = e.Field<string>("SubCategory_Core"),
                                    SubCategory_Disp = e.Field<string>("SubCategory_Disp"),
                                    SubCategory_Prefix = e.Field<string>("SubCategory_Prefix"),
                                    CreatedDT = e.Field<DateTime>("CreatedDT").ToString(),
                                    CreatedBY = e.Field<string>("CreatedBY"),
                                    OrderBy = e.Field<Int32>("OrderBy"),
                                    IsActive = e.Field<Int32>("IsActive"),
                                    LastOrderBy = e.Field<string>("LastOrderBy")

                                }).Skip(iPageStart).Take(PagerSetting.iPageSizeDefault);

                    dt = list.ToList().ToDataTable();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            CategoryModel category = new CategoryModel
                            {
                                RowCount = dr["RowCount"].ToString(),
                                ID = dr["ID"].ToString(),
                                SubCategory_Core = dr["SubCategory_Core"].ToString(),
                                SubCategory_Disp = dr["SubCategory_Disp"].ToString(),
                                SubCategory_Prefix = dr["SubCategory_Prefix"].ToString(),
                                CreatedDT = Convert.ToDateTime(dr["CreatedDT"]).ToString("yyyy-MM-dd HH:mm:ss"),
                                CreatedBY = dr["CreatedBY"].ToString(),
                                OrderBy = dr["OrderBy"].ToString(),
                                IsActive = dr["IsActive"].ToString(),
                                LastOrderBy = dr["LastOrderBy"].ToString()
                            };
                            categories.Add(category);
                        }
                    }
                    else
                    {
                        CategoryModel category = new CategoryModel();
                        category.ID = "0";
                        categories.Add(category);

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
            return categories.ToArray();
        }
        public CategoryModel[] SubCategories_SelectAll(string sCategory_Core)
        {
            List<CategoryModel> oCategory = new List<CategoryModel>();
            try
            {
                List<Parameter> oParams = new List<Parameter>();
                oParams.Add(new Parameter("sCategory_Core", sCategory_Core));

                DataTable dt = _dbContext.LoadDataByProcedure("sps_SubCategories_SelectAll", oParams);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        CategoryModel oDataRows = new CategoryModel();
                        oDataRows.ID = dr["ID"].ToString();
                        oDataRows.SubCategory_Core = dr["SubCategory_Core"].ToString();
                        oDataRows.SubCategory_Disp = dr["SubCategory_Disp"].ToString();
                        oDataRows.SubCategory_Prefix = dr["SubCategory_Prefix"].ToString();
                        oDataRows.OrderBy = dr["OrderBy"].ToString();
                        oDataRows.IsActive = dr["IsActive"].ToString();
                        oDataRows.CreatedDT = dr["CreatedDT"].ToString();
                        oDataRows.CreatedBY = dr["CreatedBY"].ToString();
                        oCategory.Add(oDataRows);
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
            return oCategory.ToArray();
        }
        public bool SubCategories_Insert(CategoryModel categoryModel)
        {
            try
            {
                List<Parameter> oParams = new List<Parameter>
                {
                    new Parameter("sCreatedBY", categoryModel.CreatedBY),
                    new Parameter("sCategory_Core", categoryModel.Category_Core),
                    new Parameter("sCategory_Disp", categoryModel.Category_Disp),
                    new Parameter("sSubCategory_Core", categoryModel.SubCategory_Core),
                    new Parameter("sSubCategory_Disp", categoryModel.SubCategory_Disp),
                    new Parameter("sSubCategory_Prefix", categoryModel.SubCategory_Prefix),
                    new Parameter("sOrderBy", categoryModel.OrderBy)
                };

                _dbContext.LoadDataByProcedure("sps_SubCategories_Insert", oParams);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public bool SubCategories_Update(CategoryModel categoryModel)
        {
            try
            {
                List<Parameter> oParams = new List<Parameter>
                {
                    new Parameter("sModifiedBY", categoryModel.ModifiedBY),
                    new Parameter("sCategory_Core", categoryModel.Category_Core),
                    new Parameter("sSubCategory_Core", categoryModel.SubCategory_Core),
                    new Parameter("sSubCategory_Disp", categoryModel.SubCategory_Disp),
                    new Parameter("sOrderBy", categoryModel.OrderBy)
                };

                _dbContext.LoadDataByProcedure("sps_SubCategories_Update", oParams);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public bool SubCategories_UpdateIsActive(string sID, string sISActive)
        {
            try
            {
                List<Parameter> oParams = new List<Parameter>();
                oParams.Add(new Parameter("sID", sID));
                oParams.Add(new Parameter("sISActive", sISActive));
                _dbContext.LoadDataByProcedure("sps_SubCategories_UpdateIsActive", oParams);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public DataTable Categories_Select()
        {
            try
            {
                return _dbContext.LoadDataByProcedure("sps_Categories_Select", null);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public CategoryModel[] SubCategory_IsExist(string sSubCategory_Core, string sCategory_Core)
        {
            List<CategoryModel> oCategory = new List<CategoryModel>();
            try
            {
                List<Parameter> oParams = new List<Parameter>();
                oParams.Add(new Parameter("sSubCategory_Core", sSubCategory_Core));
                oParams.Add(new Parameter("sCategory_Core", sCategory_Core));
                DataTable dt = _dbContext.LoadDataByProcedure("sps_SubCategory_IsExist", oParams);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        CategoryModel oDataRows = new CategoryModel();
                        oDataRows.SubCategoryCount = dr["SubCategoryCount"].ToString();
                        oCategory.Add(oDataRows);
                    }
                }
                else
                {
                    CategoryModel oDataRows = new CategoryModel();
                    oDataRows.SubCategoryCount = "0";
                    oCategory.Add(oDataRows);
                }
            }
            catch (Exception ex)
            {
                CategoryModel oDataRows = new CategoryModel();
                oDataRows.ERROR = ex.Message;
                oCategory.Add(oDataRows);
            }
            return oCategory.ToArray();
        }
        public DataTable SubCategory_SelectGstValues(string sCategoryCore)
        {
            try
            {
                List<Parameter> parameters = new List<Parameter>
                {
                    new Parameter("sGST_Type", sCategoryCore)
                };

                return _dbContext.LoadDataByProcedure("sps_SubCategory_SelectGstValues", parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }

    }
}
