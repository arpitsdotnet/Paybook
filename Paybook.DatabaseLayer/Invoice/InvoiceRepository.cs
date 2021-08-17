using AutoMapper;
using Paybook.DatabaseLayer.Common;
using Paybook.ServiceLayer.Constants;
using Paybook.ServiceLayer.Extensions;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.DatabaseLayer.Invoice
{
    public interface IInvoiceRepository : IBaseRepository<InvoiceModel>
    {
        bool Invoices_Update_CloseStatus(string sParticular, string sCreatedBY, string sCategory_Core, string sStatus_Core, string sReason, string sCustomer_ID);
        bool Invoices_Update_OverdueStatus(string sInvoice_ID, string sCategory_Core, string sStatus_Core);
        bool CreateInvoiceActivity(string sCreatedBY, string sStatus_Core);
        DataTable GetByStatusOverdue();
        int CreateWithServices(InvoiceModel invoice, List<InvoiceServiceModel> services);
    }

    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ILogger _logger;
        private readonly IDbContext _dbContext;
        private readonly IActivityRepository _activityRepo;

        public InvoiceRepository()
        {
            _logger = LoggerFactory.Instance;
            _dbContext = DbContextFactory.Instance;
            _activityRepo = new ActivityRepository();
        }

        //public static clsInvoices[] Invoice_SelectParticular()
        //{
        //    List<clsInvoices> oInvoice = new List<clsInvoices>();
        //    try
        //    {
        //        DataTable dt = clsCommon.ToLoad_MySqlDB_ByProc("sps_Invoices_SelectParticular", null);
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                clsInvoices oDataRows = new clsInvoices();
        //                oDataRows.Particular = dr["Particular"].ToString();
        //                oInvoice.Add(oDataRows);
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        clsInvoices oDataRows = new clsInvoices();
        //        oDataRows.ERROR = ex.Message;
        //        oInvoice.Add(oDataRows);
        //    }
        //    return oInvoice.ToArray();
        //}

        //public static clsInvoices[] Invoice_SelectReceiptID()
        //{
        //    List<clsInvoices> oInvoice = new List<clsInvoices>();
        //    try
        //    {

        //        DataTable dt = clsCommon.ToLoad_MySqlDB_ByProc("sps_Invoices_SelectReceiptID", null);
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                clsInvoices oDataRows = new clsInvoices();

        //                oDataRows.ReceiptID = dr["ReceiptID"].ToString();
        //                oInvoice.Add(oDataRows);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clsInvoices oDataRows = new clsInvoices();
        //        oDataRows.ERROR = ex.Message;
        //        oInvoice.Add(oDataRows);
        //    }
        //    return oInvoice.ToArray();
        //}
        internal DataTable Invoices_SelectCounts_OpenInvoice()
        {
            try
            {
                return new DataTable();// _dbContext.LoadDataByProcedure("sps_Dashboard_SelectCounts_OpenInvoice", null);

            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        internal DataTable Invoices_SelectCounts_Overdue()
        {
            try
            {
                return new DataTable();// _dbContext.LoadDataByProcedure("sps_Dashboard_SelectCounts_Overdue", null);

            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public bool Invoices_Update_CloseStatus(string sParticular, string sCreatedBY, string sCategory_Core, string sStatus_Core, string sReason, string sCustomer_ID)
        {
            try
            {
                //Update Invoice Table Status
                List<Parameter> oParams = new List<Parameter>
                {
                    new Parameter("sParticular", sParticular),
                    new Parameter("sCategory_Core", sCategory_Core),
                    new Parameter("sInvoiceStatus_Core", sStatus_Core),
                    new Parameter("sReason", sReason)
                };
                //_dbContext.LoadDataByProcedure("sps_Invoice_UpdateCloseStatus", oParams);

                //Insert Into Activity Table
                //_activityRepo.Create(new ActivityModel
                //{
                //    CreatedBY = sCreatedBY,
                //    Activity_Date = DateTime.Now.ToString("dd/MM/yyyy"),
                //    InvoiceStatus_Core = sStatus_Core,
                //    Customer_ID = sCustomer_ID,
                //    PaymentAmount = " ",
                //    Category_Core = sCategory_Core,
                //    Particular = sParticular
                //});

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }

        }
        public bool Invoices_Update_OverdueStatus(string sInvoice_ID, string sCategory_Core, string sStatus_Core)
        {
            try
            {
                //Update Invoice Table Status
                List<Parameter> oParams = new List<Parameter>();
                oParams.Add(new Parameter("sInvoice_ID", sInvoice_ID));
                oParams.Add(new Parameter("sCategory_Core", sCategory_Core));
                oParams.Add(new Parameter("sInvoiceStatus_Core", sStatus_Core));
                //_dbContext.LoadDataByProcedure("sps_Invoices_UpdateOverdueStatus", oParams);

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }

        }
        public bool CreateInvoiceActivity(string sCreatedBY, string sStatus_Core)
        {
            try
            {
                //DataTable dt = GetByStatusOverdue();
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    foreach (DataRow dr in dt.Rows)
                //    {
                //        var model = new ActivityModel
                //        {
                //            CreateBy = sCreatedBY,
                //            UserId = sStatus_Core,
                //            BusinessID = dr["Agency_ID"].ToString(),
                //            Status = dr["Customer_ID"].ToString(),
                //            Text = dr["Amount"].ToString(),
                //            HtmlText = dr["Category_Core"].ToString()
                //        };

                //        _activityRepo.Create(model);

                //        //Invoices_Update_OverdueStatus(model.Invoice_ID, model.Category_Core, model.InvoiceStatus_Core);
                //    }
                //}

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public DataTable GetByStatusOverdue()
        {
            return new DataTable();// _dbContext.LoadDataByProcedure("sps_Invoices_SelectOverdue", null);
        }
        public int CreateWithServices(InvoiceModel invoice, List<InvoiceServiceModel> services)
        {
            try
            {
                var p = new
                {
                    invoice.BusinessId,
                    invoice.CreateBy,
                    invoice.InvoiceNumber,
                    invoice.Description,
                    invoice.InvoiceDate,
                    invoice.StatusId,
                    invoice.ClientId,
                    invoice.ClientEmail,
                    invoice.BillingAddress,
                    invoice.TermsId,
                    invoice.DueDate,
                    IsOverdue = 0,
                    invoice.Message,
                    invoice.Subtotal,
                    invoice.TaxableTotal,
                    invoice.DiscountTypeId,
                    invoice.DiscountAmount,
                    invoice.DiscountTotal,
                    invoice.Total
                };

                var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<InvoiceServiceModel, InvoiceServiceMiniModel>()
                );

                var mapper = new Mapper(config);

                var mapperContext = mapper.DefaultContext;
                var submodel = mapperContext.Mapper.Map<List<InvoiceServiceMiniModel>>(services);

                var result = _dbContext.SaveDataWithSubdata("spi_Invoices_Insert", "spi_InvoiceServices_Insert", p, submodel, "InvoiceId", out int invoiceId, DbType.Int32, null, "Id");

                invoice.Id = invoiceId;

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }



        public List<InvoiceModel> GetAllByPage(int businessId, int page, string search, string orderBy)
        {
            try
            {
                var p = new { BusinessId = businessId, Page = page, Search = search, OrderBy = orderBy };

                var result = _dbContext.LoadData<InvoiceModel, dynamic>("sps_Invoices_GetAllByPage", p);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public InvoiceModel GetById(int businessId, int id)
        {
            try
            {
                var p = new { BusinessId = businessId, Id = id };

                var result = _dbContext.LoadData<InvoiceModel, dynamic>("sps_Invoices_GetById", p);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public int Create(InvoiceModel model)
        {
            try
            {
                var result = _dbContext.SaveDataOutParam("spi_Invoices_Insert", model, out int invoiceId, DbType.Int32, null, "Id");

                model.Id = invoiceId;

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public int Update(InvoiceModel model)
        {
            try
            {
                var result = _dbContext.SaveData("spu_Invoices_Update", model);

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

                var result = _dbContext.SaveData("spu_Invoices_Activate", p);

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

                var result = _dbContext.SaveData("spd_Invoices_Delete", p);

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
