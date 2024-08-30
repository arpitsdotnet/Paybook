using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.DatabaseLayer.Business
{

    public class BusinessRepository : IBusinessRepository
    {
        private readonly IDbContext _dbContext;

        public BusinessRepository()
        {
            _dbContext = DbContextFactory.Instance;
        }

        public bool IsExist(string createBy, string businessName)
        {
            var p = new { CreateBy = createBy, Name = businessName };
            _ = _dbContext.SaveDataOutParam("sps_Businesses_IsExist", p, out dynamic isExist, DbType.Boolean, null, "IsExist");
            //return _dbContext.LoadDataByProcedure("sps_CompanyProfile_IsExist", null);

            return (bool)isExist;
        }

        public List<BusinessModel> GetAllByUsername(string username)
        {
            var p = new { Username = username };

            var result = _dbContext.LoadData<BusinessModel, dynamic>("sps_Businesses_GetAllByUsername", p);
            //return _dbContext.LoadDataByProcedure("sps_Agency_SelectName", null);

            return result;
        }

        public BusinessModel GetSelectedByUsername(string username)
        {
            var p = new { Username = username };

            var result = _dbContext.LoadData<BusinessModel, dynamic>("sps_Businesses_GetSelectedByUsername", p);
            //return _dbContext.LoadDataByProcedure("sps_Agency_SelectName", null);

            return result.FirstOrDefault();
        }

        public BusinessModel GetById(int id, string username)
        {
            var p = new { Id = id, Username = username };

            var result = _dbContext.LoadData<BusinessModel, dynamic>("sps_Businesses_GetById", p);

            return result.FirstOrDefault();
        }

        public int Create(BusinessModel model)
        {
            var p = new
            {
                model.CreateBy,
                model.Name,
                model.Description,
                model.Image,
                model.GSTNumber,
                model.PhoneNumber,
                model.Email,
                model.AddressLine1,
                model.AddressLine2,
                model.City,
                model.StateId,
                model.CountryId,
                model.Pincode
            };

            var result = _dbContext.SaveDataOutParam("spi_Businesses_Insert", p, out int businessId, DbType.Int32, null, "Id");
            //_dbContext.LoadDataByProcedure("sps_Agency_Insert", oParams);

            model.Id = businessId;

            return result;

        }

        public int Update(BusinessModel model)
        {
            var result = _dbContext.SaveData("spu_Businesses_Update", model);
            //_dbContext.LoadDataByProcedure("sps_Agency_Update", oParams);

            return result;
        }

        public int UpdateSelected(int id, string username)
        {
            var p = new { Id = id, CreateBy = username };

            var result = _dbContext.SaveData("spu_Businesses_UpdateSelected", p);

            return result;
        }

        public int Activate(int id, string username, bool active)
        {
            var p = new { Id = id, Username = username, IsActive = active };

            var result = _dbContext.SaveData("spu_Businesses_Activate", p);
            //_dbContext.LoadDataByProcedure("sps_Agency_Update", oParams);

            return result;
        }

        public int Delete(int id)
        {
            var p = new { Id = id };

            var result = _dbContext.SaveData("spd_Businesses_Delete", p);
            //_dbContext.LoadDataByProcedure("sps_Agency_Update", oParams);

            return result;
        }
    }
}
