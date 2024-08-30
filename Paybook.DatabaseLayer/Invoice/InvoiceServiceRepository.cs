using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Paybook.ServiceLayer.Models.Invoices;

namespace Paybook.DatabaseLayer.Invoice
{
    public class InvoiceServiceRepository : IInvoiceServiceRepository
    {
        private readonly IDbContext _dbContext;

        public InvoiceServiceRepository()
        {
            _dbContext = DbContextFactory.Instance;
        }

        public List<InvoiceServiceModel> GetAllByInvoiceId(int businessId, int invoiceId)
        {
            var p = new { BusinessId = businessId, InvoiceId = invoiceId };

            var result = _dbContext.LoadData<InvoiceServiceModel, dynamic>("sps_InvoiceServices_GetAllByInvoiceId", p);

            return result;
        }
        public bool IsExist(string businessId, int id)
        {
            var p = new { BusinessId = businessId, Id = id };
            _ = _dbContext.SaveDataOutParam("sps_InvoiceServices_IsExist", p, out bool isExist, DbType.Boolean, null, "IsExist");
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

        public List<InvoiceServiceModel> GetAllByPage(int businessId, int page, string search, string orderBy)
        {
            throw new NotImplementedException();
        }
        public InvoiceServiceModel GetById(int businessId, int id)
        {
            var p = new { BusinessId = businessId, Id = id };

            var result = _dbContext.LoadData<InvoiceServiceModel, dynamic>("sps_InvoiceServices_GetById", p);

            return result.FirstOrDefault();
        }
        public int Create(InvoiceServiceModel model)
        {
            var result = _dbContext.SaveDataOutParam("spi_InvoiceServices_Insert", model, out int serviceId, DbType.Int32, null, "Id");

            model.Id = serviceId;

            return result;
        }
        public int Update(InvoiceServiceModel model)
        {
            var result = _dbContext.SaveData("spu_InvoiceServices_Update", model);

            return result;
        }
        public int Activate(int businessId, string username, int id, bool active)
        {
            var p = new { BusinessId = businessId, Username = username, Id = id, IsActive = active };

            var result = _dbContext.SaveData("spu_InvoiceServices_Activate", p);

            return result;
        }
        public int Delete(int businessId, string username, int id)
        {
            var p = new { BusinessId = businessId, Username = username, Id = id };

            var result = _dbContext.SaveData("spd_InvoiceServices_Delete", p);

            return result;
        }

    }
}
