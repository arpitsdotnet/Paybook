using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.DatabaseLayer.Invoice
{
    public interface IInvoiceServiceRepository:IBaseRepository<InvoiceServiceModel>
    {
        List<InvoiceServiceModel> GetAllByInvoiceId(int businessId, int invoiceId);
        bool IsExist(string businessId, int id);
    }

    public class InvoiceServiceRepository : IInvoiceServiceRepository
    {
        private readonly ILogger _logger;
        private readonly IDbContext _dbContext;

        public InvoiceServiceRepository()
        {
            _logger = LoggerFactory.Instance;
            _dbContext = DbContextFactory.Instance;
        }

        public List<InvoiceServiceModel> GetAllByInvoiceId(int businessId, int invoiceId)
        {
            try
            {
                var p = new { BusinessId = businessId, InvoiceId = invoiceId };

                var result = _dbContext.LoadData<InvoiceServiceModel, dynamic>("sps_InvoiceServices_GetAllByInvoiceId", p);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public bool IsExist(string businessId, int id)
        {
            try
            {
                var p = new { BusinessId = businessId, Id = id };

                var result = _dbContext.SaveDataOutParam("sps_InvoiceServices_IsExist", p, out bool isExist, DbType.Boolean, null, "IsExist");
                //DataTable dt = _dbContext.LoadDataByProcedure("sps_Particular_IsExist", oParams);
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    foreach (DataRow dr in dt.Rows)
                //    {
                //        InvoiceServiceModel oDataRows = new InvoiceServiceModel();

                //        oDataRows.ParticularCount = dr["ParticularCount"].ToString();
                //        oParticular.Add(oDataRows);
                //    }
                //}

                return isExist;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }

        public List<InvoiceServiceModel> GetAllByPage(int businessId, int page, string search, string orderBy)
        {
            throw new NotImplementedException();
        }
        public InvoiceServiceModel GetById(int businessId, int id)
        {
            try
            {
                var p = new { BusinessId = businessId, Id = id };

                var result = _dbContext.LoadData<InvoiceServiceModel, dynamic>("sps_InvoiceServices_GetById", p);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public int Create(InvoiceServiceModel model)
        {
            try
            {
                var result = _dbContext.SaveDataOutParam("spi_InvoiceServices_Insert", model, out int serviceId, DbType.Int32, null, "Id");

                model.Id = serviceId;

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }

        }
        public int Update(InvoiceServiceModel model)
        {
            try
            {
                var result = _dbContext.SaveData("spu_InvoiceServices_Update", model);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public int Activate(int businessId, int id, bool active)
        {
            try
            {
                var p = new { BusinessId = businessId, Id = id, IsActive = active };

                var result = _dbContext.SaveData("spu_InvoiceServices_Activate", p);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public int Delete(int businessId, int id)
        {
            try
            {
                var p = new { BusinessId = businessId, Id = id };

                var result = _dbContext.SaveData("spd_InvoiceServices_Delete", p);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }

    }
}
