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
    public interface IInvoiceRepository
    {
        InvoiceModel[] Invoices_Search(string sOrderBy, string sGridPageNumber, string sUserName, string sAgency_ID, string sCustomer_ID, string sReceiptID, string sCategory_Core, string sInvoiceDateTo, string sInvoiceDateFrom, string sInvoiceStatus_Core);
        DataTable Invoices_SelectCount();
        bool Invoices_Update_CloseStatus(string sParticular, string sCreatedBY, string sCategory_Core, string sStatus_Core, string sReason, string sCustomer_ID);
        bool Invoices_Update_OverdueStatus(string sInvoice_ID, string sCategory_Core, string sStatus_Core);
        bool Invoices_Update_PaymentStatus(string sModifiedBY, string sCategory_Core, string sInvoiceStatus_Core, string sLastPayment_Date, string sInvoice_ID);
        bool Invoice_Insert(string sInvoice_ID, string sCreatedBY, string sAgency_ID, string sCustomer_ID, string sCategory_Core, string sParticular, string sAmount, string sInvoice_Date, string sRemainingAmount, string sInvoiceStatus_Core, string sAgent_ID, string sRemark, string sMRP, string sGST_Type, string sVehicleNo);
        DataTable GetByInvoiceID(string sInvoice_ID);
        DataTable Dashboard_GetInvoiceCountByLastWeek();
        bool CreateTax(InvoiceTaxModel invoiceModel);
        bool Activity_Insert_Overdue(string sCreatedBY, string sStatus_Core);
    }

    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ILogger _logger;
        private readonly IDbContext _dbContext;
        private readonly IActivityRepository _activityRepo;

        public InvoiceRepository()
        {
            _logger = FileLogger.Instance;
            _dbContext = DbContextFactory.Instance;
            _activityRepo = new ActivityRepository();
        }

        public InvoiceModel[] Invoices_Search(string sOrderBy, string sGridPageNumber, string sUserName, string sAgency_ID, string sCustomer_ID, string sReceiptID, string sCategory_Core, string sInvoiceDateTo, string sInvoiceDateFrom, string sInvoiceStatus_Core)
        {
            List<InvoiceModel> oPayment = new List<InvoiceModel>();
            try
            {
                StringBuilder query = new StringBuilder();

                if (sInvoiceDateTo != "" && sInvoiceDateFrom != "")
                {
                    string dInvoiceDateTo = "", dInvoiceDateFrom = "";
                    sInvoiceDateFrom = $"{sInvoiceDateFrom} 00:00:00";
                    sInvoiceDateTo = $"{sInvoiceDateTo} 23:59:59";
                    dInvoiceDateFrom = Convert.ToDateTime(sInvoiceDateFrom).ToString("yyyy-MM-dd HH:mm:ss");
                    dInvoiceDateTo = Convert.ToDateTime(sInvoiceDateTo).ToString("yyyy-MM-dd HH:mm:ss");
                    query.Append($" AND (Invoice_Date BETWEEN '{dInvoiceDateFrom}' AND '{dInvoiceDateTo}')");
                }

                if (sAgency_ID != "All")
                    query.Append($" AND TC.Agency_ID='{sAgency_ID}'");
                if (sCustomer_ID != "All")
                    query.Append($" AND TIN.Customer_ID='{sCustomer_ID}'");
                if (sCategory_Core != "All")
                    query.Append($" AND TIN.Category_Core='{sCategory_Core}'");
                if (sInvoiceStatus_Core != "All")
                    query.Append($" AND TIN.InvoiceStatus_Core='{sInvoiceStatus_Core}'");
                if (sReceiptID != "")
                    query.Append($" AND TP.ReceiptID='{sReceiptID}'");

                List<Parameter> oParams = new List<Parameter>();
                oParams.Add(new Parameter("Where", query.ToString()));
                DataTable dt = _dbContext.LoadDataByProcedure("sps_Invoice_Search", oParams);

                //start
                if (dt != null && dt.Rows.Count > 0)
                {
                    string dtcount = dt.Rows.Count.ToString();
                    //int drowtotal = int.Parse(dtcount);
                    //getpagerange(convert.todouble(sgridpagenumber), drowtotal, out dpagenumber_start, out dpagenumber_end);

                    int iPageNumber = Convert.ToInt32(sGridPageNumber);
                    int iPageStart = iPageNumber == 0 ? 0 : (PagerSetting.iPageSizeDefault * iPageNumber);

                    var list = (from e in dt.AsEnumerable()
                                select new
                                {
                                    RowCount = dtcount,
                                    ID = e.Field<int>("ID"),
                                    Invoice_ID = e.Field<string>("Invoice_ID"),
                                    Customer_ID = e.Field<string>("Customer_ID"),
                                    CustomerName = e.Field<string>("CustomerName"),
                                    Agent_ID = e.Field<string>("Agent_ID"),
                                    AgentName = e.Field<string>("AgentName"),
                                    Particular = e.Field<string>("Particular"),
                                    Category_Disp = e.Field<string>("Category_Disp"),
                                    Category_Core = e.Field<string>("Category_Core"),
                                    CreatedBY = e.Field<string>("CreatedBY"),
                                    CreatedDT = e.Field<DateTime>("CreatedDT").ToString(),
                                    Amount = e.Field<string>("Amount").ToString(),
                                    Paid = e.Field<double>("Paid").ToString(),
                                    InvoiceStatus_Disp = e.Field<string>("InvoiceStatus_Disp"),
                                    InvoiceStatus_Core = e.Field<string>("InvoiceStatus_Core")
                                }).Skip(iPageStart).Take(PagerSetting.iPageSizeDefault);

                    dt = list.ToList().ToDataTable();

                    foreach (DataRow dr in dt.Rows)
                    {
                        InvoiceModel oDataRows = new InvoiceModel();
                        oDataRows.RowCount = dr["RowCount"].ToString();
                        oDataRows.ID = dr["ID"].ToString();
                        oDataRows.Invoice_ID = dr["Invoice_ID"].ToString();
                        oDataRows.Customer_ID = dr["Customer_ID"].ToString();
                        oDataRows.CustomerName = dr["CustomerName"].ToString();
                        oDataRows.Agent_ID = dr["Agent_ID"].ToString();
                        oDataRows.AgentName = dr["AgentName"].ToString();
                        oDataRows.Particular = dr["Particular"].ToString();
                        // oDataRows.ReceiptID = dr["ReceiptID"].ToString();
                        oDataRows.Category_Disp = dr["Category_Disp"].ToString();
                        oDataRows.Category_Core = dr["Category_Core"].ToString();
                        oDataRows.CreatedBY = dr["CreatedBY"].ToString();
                        oDataRows.CreatedDT = Convert.ToDateTime(dr["CreatedDT"]).ToString("yyyy-MM-dd HH:mm:ss");
                        oDataRows.Amount = dr["Amount"].ToString();
                        oDataRows.Paid = dr["Paid"].ToString();
                        oDataRows.InvoiceStatus_Disp = dr["InvoiceStatus_Disp"].ToString();
                        oDataRows.InvoiceStatus_Core = dr["InvoiceStatus_Core"].ToString();
                        oPayment.Add(oDataRows);
                    }
                }
                else
                {
                    InvoiceModel oDataRows = new InvoiceModel();
                    oDataRows.ID = "0";
                    oPayment.Add(oDataRows);

                }
                //end                            

            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
            return oPayment.ToArray();
        }

        public bool Invoice_Insert(string sInvoice_ID, string sCreatedBY, string sAgency_ID, string sCustomer_ID, string sCategory_Core, string sParticular, string sAmount,
            string sInvoice_Date, string sRemainingAmount, string sInvoiceStatus_Core, string sAgent_ID, string sRemark, string sMRP, string sGST_Type, string sVehicleNo)
        {
            try
            {
                string sTime = DateTime.Now.ToString("HH:mm:ss");
                sInvoice_Date = sInvoice_Date + " " + sTime;
                string dInvoice_Date = Convert.ToDateTime(sInvoice_Date).ToString("yyyy-MM-dd HH:mm:ss").ToString();
                sCustomer_ID = sCustomer_ID == "0" || sCustomer_ID == "" ? "0" : sCustomer_ID;

                List<Parameter> oParams = new List<Parameter>();
                oParams.Add(new Parameter("sCreatedBY", sCreatedBY));
                oParams.Add(new Parameter("sAgency_ID", sAgency_ID));
                oParams.Add(new Parameter("sCustomer_ID", sCustomer_ID));
                oParams.Add(new Parameter("sCategory_Core", sCategory_Core));
                oParams.Add(new Parameter("sParticular", sParticular));
                oParams.Add(new Parameter("sAmount", sAmount));
                oParams.Add(new Parameter("dInvoice_Date", dInvoice_Date));
                oParams.Add(new Parameter("sInvoiceStatus_Core", sInvoiceStatus_Core));
                oParams.Add(new Parameter("sAgent_ID", sAgent_ID));
                oParams.Add(new Parameter("sInvoice_ID", sInvoice_ID));
                oParams.Add(new Parameter("sRemark", sRemark));
                oParams.Add(new Parameter("sInvoice_MRP", sMRP));
                oParams.Add(new Parameter("sVehicleNo", sVehicleNo));

                _dbContext.LoadDataByProcedure("sps_Invoice_Insert", oParams);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
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

        public DataTable Invoices_SelectCount()
        {
            try
            {
                return _dbContext.LoadDataByProcedure("sps_Invoices_SelectCount", null);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        internal DataTable Invoices_SelectCounts_OpenInvoice()
        {
            try
            {
                return _dbContext.LoadDataByProcedure("sps_Dashboard_SelectCounts_OpenInvoice", null);

            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        internal DataTable Invoices_SelectCounts_Overdue()
        {
            try
            {
                return _dbContext.LoadDataByProcedure("sps_Dashboard_SelectCounts_Overdue", null);

            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

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
                _dbContext.LoadDataByProcedure("sps_Invoice_UpdateCloseStatus", oParams);

                //Insert Into Activity Table
                _activityRepo.Activity_Insert(new ActivityModel
                {
                    CreatedBY = sCreatedBY,
                    Activity_Date = DateTime.Now.ToString("dd/MM/yyyy"),
                    InvoiceStatus_Core = sStatus_Core,
                    Customer_ID = sCustomer_ID,
                    PaymentAmount = " ",
                    Category_Core = sCategory_Core,
                    Particular = sParticular
                });

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

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
                _dbContext.LoadDataByProcedure("sps_Invoices_UpdateOverdueStatus", oParams);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }

        }

        public bool Invoices_Update_PaymentStatus(string sModifiedBY, string sCategory_Core, string sInvoiceStatus_Core, string sLastPayment_Date, string sInvoice_ID)
        {
            try
            {
                //Update Invoice Table Status
                //oParams.Add(new clsParams("sParticular", sParticular));
                List<Parameter> oParams = new List<Parameter>
                {
                    new Parameter("sModifiedBY", sModifiedBY),
                    new Parameter("sCategory_Core", sCategory_Core),
                    new Parameter("sInvoiceStatus_Core", sInvoiceStatus_Core),
                    new Parameter("sLastPayment_Date", sLastPayment_Date),
                    new Parameter("sInvoice_ID", sInvoice_ID)
                };

                _dbContext.LoadDataByProcedure("sps_Invoices_UpdatePaymentStatus", oParams);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public DataTable GetByInvoiceID(string invoiceID)
        {
            try
            {
                var oParams = new List<Parameter>
                {
                    new Parameter("sInvoice_ID", invoiceID)
                };
                return _dbContext.LoadDataByProcedure("sps_Invoice_GetByInvoiceID", oParams);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public DataTable Dashboard_GetInvoiceCountByLastWeek()
        {
            try
            {
                return _dbContext.LoadDataByProcedure("sps_Dashboard_GetInvoiceCountByLastWeek", null);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }

        public bool CreateTax(InvoiceTaxModel taxModel)
        {
            try
            {
                List<Parameter> oParams = new List<Parameter>
                {
                    new Parameter("sCreatedBY", taxModel.CreatedBY),
                    new Parameter("sTaxType", taxModel.TaxType),
                    new Parameter("sPercentage", taxModel.Percentage),
                    new Parameter("sAmount", taxModel.Amount),
                    new Parameter("sCustomer_ID", taxModel.CustomerID),
                    new Parameter("sInvoice_ID", taxModel.InvoiceID),
                    new Parameter("sInvoice_Date", taxModel.Invoice_Date),
                    new Parameter("sAgency_ID", taxModel.AgencyID)
                };

                _dbContext.LoadDataByProcedure("sps_InvoiceTax_Insert", oParams);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }

        public bool Activity_Insert_Overdue(string sCreatedBY, string sStatus_Core)
        {
            try
            {
                DataTable dt = GetByStatusOverdue();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var model = new ActivityModel
                        {
                            CreatedBY = sCreatedBY,
                            Activity_Date = DateTime.Now.ToString(),
                            InvoiceStatus_Core = sStatus_Core,
                            Agency_ID = dr["Agency_ID"].ToString(),
                            Customer_ID = dr["Customer_ID"].ToString(),
                            PaymentAmount = dr["Amount"].ToString(),
                            Category_Core = dr["Category_Core"].ToString(),
                            Invoice_ID = dr["Invoice_ID"].ToString()
                        };

                        _activityRepo.Activity_Insert(model);

                        Invoices_Update_OverdueStatus(model.Invoice_ID, model.Category_Core, model.InvoiceStatus_Core);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        private DataTable GetByStatusOverdue()
        {
            return _dbContext.LoadDataByProcedure("sps_Invoices_SelectOverdue", null);
        }
    }
}
