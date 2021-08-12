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
        InvoiceModel[] Invoices_Search(string sOrderBy, string sGridPageNumber, string sUserName, string sAgency_ID, string sCustomer_ID, string sReceiptID, string sCategory_Core, string sInvoiceDateTo, string sInvoiceDateFrom, string sInvoiceStatus_Core);
        bool Invoices_Update_CloseStatus(string sParticular, string sCreatedBY, string sCategory_Core, string sStatus_Core, string sReason, string sCustomer_ID);
        bool Invoices_Update_OverdueStatus(string sInvoice_ID, string sCategory_Core, string sStatus_Core);
        bool CreateInvoiceActivity(string sCreatedBY, string sStatus_Core);
        DataTable GetByStatusOverdue();
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

        public InvoiceModel[] Invoices_Search(string sOrderBy, string sGridPageNumber, string sUserName, string sAgency_ID, string sCustomer_ID, string sReceiptID, string sCategory_Core, string sInvoiceDateTo, string sInvoiceDateFrom, string sInvoiceStatus_Core)
        {
            List<InvoiceModel> oPayment = new List<InvoiceModel>();
            //try
            //{
            //    StringBuilder query = new StringBuilder();

            //    if (sInvoiceDateTo != "" && sInvoiceDateFrom != "")
            //    {
            //        string dInvoiceDateTo = "", dInvoiceDateFrom = "";
            //        sInvoiceDateFrom = $"{sInvoiceDateFrom} 00:00:00";
            //        sInvoiceDateTo = $"{sInvoiceDateTo} 23:59:59";
            //        dInvoiceDateFrom = Convert.ToDateTime(sInvoiceDateFrom).ToString("yyyy-MM-dd HH:mm:ss");
            //        dInvoiceDateTo = Convert.ToDateTime(sInvoiceDateTo).ToString("yyyy-MM-dd HH:mm:ss");
            //        query.Append($" AND (Invoice_Date BETWEEN '{dInvoiceDateFrom}' AND '{dInvoiceDateTo}')");
            //    }

            //    if (sAgency_ID != "All")
            //        query.Append($" AND TC.Agency_ID='{sAgency_ID}'");
            //    if (sCustomer_ID != "All")
            //        query.Append($" AND TIN.Customer_ID='{sCustomer_ID}'");
            //    if (sCategory_Core != "All")
            //        query.Append($" AND TIN.Category_Core='{sCategory_Core}'");
            //    if (sInvoiceStatus_Core != "All")
            //        query.Append($" AND TIN.InvoiceStatus_Core='{sInvoiceStatus_Core}'");
            //    if (sReceiptID != "")
            //        query.Append($" AND TP.ReceiptID='{sReceiptID}'");

            //    List<Parameter> oParams = new List<Parameter>();
            //    oParams.Add(new Parameter("Where", query.ToString()));
            //    DataTable dt = _dbContext.LoadDataByProcedure("sps_Invoice_Search", oParams);

            //    //start
            //    if (dt != null && dt.Rows.Count > 0)
            //    {
            //        string dtcount = dt.Rows.Count.ToString();
            //        //int drowtotal = int.Parse(dtcount);
            //        //getpagerange(convert.todouble(sgridpagenumber), drowtotal, out dpagenumber_start, out dpagenumber_end);

            //        int iPageNumber = Convert.ToInt32(sGridPageNumber);
            //        int iPageStart = iPageNumber == 0 ? 0 : (PagerSetting.iPageSizeDefault * iPageNumber);

            //        var list = (from e in dt.AsEnumerable()
            //                    select new
            //                    {
            //                        RowCount = dtcount,
            //                        ID = e.Field<int>("ID"),
            //                        Invoice_ID = e.Field<string>("Invoice_ID"),
            //                        Customer_ID = e.Field<string>("Customer_ID"),
            //                        CustomerName = e.Field<string>("CustomerName"),
            //                        Agent_ID = e.Field<string>("Agent_ID"),
            //                        AgentName = e.Field<string>("AgentName"),
            //                        Particular = e.Field<string>("Particular"),
            //                        Category_Disp = e.Field<string>("Category_Disp"),
            //                        Category_Core = e.Field<string>("Category_Core"),
            //                        CreatedBY = e.Field<string>("CreatedBY"),
            //                        CreatedDT = e.Field<DateTime>("CreatedDT").ToString(),
            //                        Amount = e.Field<string>("Amount").ToString(),
            //                        Paid = e.Field<double>("Paid").ToString(),
            //                        InvoiceStatus_Disp = e.Field<string>("InvoiceStatus_Disp"),
            //                        InvoiceStatus_Core = e.Field<string>("InvoiceStatus_Core")
            //                    }).Skip(iPageStart).Take(PagerSetting.iPageSizeDefault);

            //        dt = list.ToList().ToDataTable();

            //        foreach (DataRow dr in dt.Rows)
            //        {
            //            InvoiceModel oDataRows = new InvoiceModel();
            //            oDataRows.RowCount = dr["RowCount"].ToString();
            //            oDataRows.ID = dr["ID"].ToString();
            //            oDataRows.Invoice_ID = dr["Invoice_ID"].ToString();
            //            oDataRows.Customer_ID = dr["Customer_ID"].ToString();
            //            oDataRows.CustomerName = dr["CustomerName"].ToString();
            //            oDataRows.Agent_ID = dr["Agent_ID"].ToString();
            //            oDataRows.AgentName = dr["AgentName"].ToString();
            //            oDataRows.Particular = dr["Particular"].ToString();
            //            // oDataRows.ReceiptID = dr["ReceiptID"].ToString();
            //            oDataRows.Category_Disp = dr["Category_Disp"].ToString();
            //            oDataRows.Category_Core = dr["Category_Core"].ToString();
            //            oDataRows.CreatedBY = dr["CreatedBY"].ToString();
            //            oDataRows.CreatedDT = Convert.ToDateTime(dr["CreatedDT"]).ToString("yyyy-MM-dd HH:mm:ss");
            //            oDataRows.Amount = dr["Amount"].ToString();
            //            oDataRows.Paid = dr["Paid"].ToString();
            //            oDataRows.InvoiceStatus_Disp = dr["InvoiceStatus_Disp"].ToString();
            //            oDataRows.InvoiceStatus_Core = dr["InvoiceStatus_Core"].ToString();
            //            oPayment.Add(oDataRows);
            //        }
            //    }
            //    else
            //    {
            //        InvoiceModel oDataRows = new InvoiceModel();
            //        oDataRows.ID = "0";
            //        oPayment.Add(oDataRows);

            //    }
            //    //end                            

            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(_logger.MethodName, ex);

            //    throw;
            //}
            return oPayment.ToArray();
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
