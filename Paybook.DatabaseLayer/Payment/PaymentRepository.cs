using Paybook.DatabaseLayer.Common;
using Paybook.ServiceLayer;
using Paybook.ServiceLayer.Constants;
using Paybook.ServiceLayer.Extensions;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.DatabaseLayer.Payment
{
    public interface IPaymentRepository
    {
        bool CreateAdvance(string sAdvance_ID, string sCurrentAdvancePayment, string sAgency_ID, string sCustomer_ID, string sAdvancePayment_Date, string sCreatedBy, string sTotalAdvancePayment, string sAdvancePaymentType);
        PaymentModel[] GetAllByInvoiceID(string sOrderBy, string sGridPageNumber, string sUserName, string sCustomer_ID, string sInvoice_ID, string sCategory_Core);
        InvoiceModel[] GetAllByPage(string sOrderBy, string sGridPageNumber, string sUserName, string sAgency_ID, string sCustomer_ID, string sPaymentDateTo, string sPaymentDateFrom);
        DataTable Payments_SelectCount();
        string Payments_SelectMonthsales();
        bool Create(string sReceipt_ID, string sCreatedBY, string sAgency_ID, string sCustomer_ID, string sPaymentAmount, string sPaymentDate, string sPaymentStatus_Core, string sCategory_Core, string sAgent_ID, string sInvoice_ID);
        DataTable Dashboard_GetPaymentsByLastWeek();
        DataTable Dashboard_GetPaymentsLast20();
    }

    public class PaymentRepository : IPaymentRepository
    {
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly ILastSavedIdRepository _lastSavedIdRepository;
        private readonly IActivityRepository _activityRepo;

        public PaymentRepository()
        {
            _dbContext = DbContextFactory.Instance;
            _logger = FileLogger.Instance;
            _lastSavedIdRepository = new LastSavedIdRepository();
            _activityRepo = new ActivityRepository();
        }



        public bool CreateAdvance(string sAdvance_ID, string sCurrentAdvancePayment, string sAgency_ID, string sCustomer_ID, string sAdvancePayment_Date, string sCreatedBy, string sTotalAdvancePayment, string sAdvancePaymentType)
        {
            try
            {
                List<Parameter> oParams = new List<Parameter>();

                //convert date into mysql date formate
                string sTime = DateTime.Now.ToString("HH:mm:ss");
                string dAdvancePayment_Date = Convert.ToDateTime(sAdvancePayment_Date + " " + sTime).ToString("yyyy-MM-dd HH:mm:ss").ToString();
                //  sCustomer_ID = sCustomer_ID == "0" || sCustomer_ID == "" || sCustomer_ID == null ? "0" : sCustomer_ID;
                sAgency_ID = sAgency_ID == "NONE" ? "0" : sAgency_ID;
                //Insert Into Advance Table
                oParams.Clear();
                oParams.Add(new Parameter("sCreatedBy", sCreatedBy));
                oParams.Add(new Parameter("sAgency_ID", sAgency_ID));
                oParams.Add(new Parameter("sCustomer_ID", sCustomer_ID));
                oParams.Add(new Parameter("sAdvancePayment", sCurrentAdvancePayment));
                oParams.Add(new Parameter("dAdvancePayment_Date", dAdvancePayment_Date));
                oParams.Add(new Parameter("sAdvance_ID", sAdvance_ID));
                oParams.Add(new Parameter("sAdvancePaymentType", sAdvancePaymentType));
                _dbContext.LoadDataByProcedure("sps_Advancepayment_Insert", oParams);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        public PaymentModel[] GetAllByInvoiceID(string sOrderBy, string sGridPageNumber, string sUserName, string sCustomer_ID, string sInvoice_ID, string sCategory_Core)
        {
            DataTable dt = new DataTable();
            List<PaymentModel> oPayment = new List<PaymentModel>();
            try
            {
                List<Parameter> oParams = new List<Parameter>();
                oParams.Add(new Parameter("sInvoice_ID", sInvoice_ID));
                oParams.Add(new Parameter("sCustomer_ID", sCustomer_ID));
                oParams.Add(new Parameter("sCategory_Core", sCategory_Core));
                dt = _dbContext.LoadDataByProcedure("sps_Payments_ForInvoice", oParams);
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
                                    ReceiptID = e.Field<string>("ReceiptID"),
                                    Payment_Date = e.Field<DateTime>("Payment_Date"),
                                    PaymentAmount = e.Field<string>("PaymentAmount"),
                                    // PaymentType = e.Field<string>("PaymentType"),
                                }).Skip(iPageStart).Take(PagerSetting.iPageSizeDefault);

                    dt = list.ToList().ToDataTable();
                    foreach (DataRow dr in dt.Rows)
                    {
                        PaymentModel oDataRows = new PaymentModel();
                        oDataRows.RowCount = dr["RowCount"].ToString();
                        oDataRows.ReceiptID = dr["ReceiptID"].ToString();
                        oDataRows.Payment_Date = Convert.ToDateTime(dr["Payment_Date"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        oDataRows.PaymentAmount = dr["PaymentAmount"].ToString();
                        // oDataRows.PaymentType = dr["PaymentType"].ToString();
                        oPayment.Add(oDataRows);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                PaymentModel oDataRows = new PaymentModel();
                oDataRows.ERROR = ex.Message;
                oPayment.Add(oDataRows);
            }
            return oPayment.ToArray();
        }
        public InvoiceModel[] GetAllByPage(string sOrderBy, string sGridPageNumber, string sUserName, string sAgency_ID, string sCustomer_ID, string sPaymentDateTo, string sPaymentDateFrom)
        {
            List<InvoiceModel> oPayment = new List<InvoiceModel>();
            try
            {
                string dPaymentDateTo = "", dPaymentDateFrom = "";
                if (sPaymentDateTo != "" && sPaymentDateFrom != "")
                {
                    string sTime = DateTime.Now.ToString("HH:mm:ss");
                    sPaymentDateTo = sPaymentDateTo + " " + "23:59:59";
                    sPaymentDateFrom = sPaymentDateFrom + " " + "00:01:01";
                    dPaymentDateTo = Convert.ToDateTime(sPaymentDateTo).ToString("yyyy-MM-dd HH:mm:ss").ToString();
                    dPaymentDateFrom = Convert.ToDateTime(sPaymentDateFrom).ToString("yyyy-MM-dd HH:mm:ss").ToString();
                }
                //  sCustomer_ID=sCustomer_ID == "All" ? "" : sCustomer_ID ;

                //search is invoice exist correspond to selected customer
                List<Parameter> oParams = new List<Parameter>();
                oParams.Add(new Parameter("dPaymentDateFrom", dPaymentDateFrom));
                oParams.Add(new Parameter("dPaymentDateTo", dPaymentDateTo));
                oParams.Add(new Parameter("sAgency_ID", sAgency_ID));
                oParams.Add(new Parameter("sCustomer_ID", sCustomer_ID));

                DataTable dtRowExist = _dbContext.LoadDataByProcedure("sps_Invoice_IsExist", oParams);

                //if rows exist then search payment informaions
                if (dtRowExist != null && dtRowExist.Rows.Count > 0)
                {
                    DataTable dt = _dbContext.LoadDataByProcedure("sps_Payments_Search", oParams);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string dtcount = dt.Rows.Count.ToString();
                        int iPageNumber = Convert.ToInt32(sGridPageNumber);
                        int iPageStart = iPageNumber == 0 ? 0 : (PagerSetting.iPageSizeDefault * iPageNumber);

                        var list = (from e in dt.AsEnumerable()
                                    select new
                                    {
                                        RowCount = dtcount,
                                        Invoice_ID = e.Field<string>("Invoice_ID"),
                                        Invoice_Date = e.Field<DateTime>("Invoice_Date"),
                                        Customer_ID = e.Field<string>("Customer_ID"),
                                        Particular = e.Field<string>("Particular"),
                                        Category_Disp = e.Field<string>("Category_Disp"),
                                        Category_Core = e.Field<string>("Category_Core"),
                                        Amount = e.Field<string>("Amount").ToString(),
                                        Paid = e.Field<double>("Paid").ToString(),
                                        CustomerName = e.Field<string>("CustomerName")

                                    }).Skip(iPageStart).Take(PagerSetting.iPageSizeDefault);

                        dt = list.ToList().ToDataTable();

                        foreach (DataRow dr in dt.Rows)
                        {
                            InvoiceModel oDataRows = new InvoiceModel();
                            oDataRows.RowCount = dr["RowCount"].ToString();
                            oDataRows.Invoice_ID = dr["Invoice_ID"].ToString();
                            oDataRows.Invoice_Date = Convert.ToDateTime(dr["Invoice_Date"]).ToString("yyyy-MM-dd HH:mm:ss");
                            oDataRows.Customer_ID = dr["Customer_ID"].ToString();
                            oDataRows.Particular = dr["Particular"].ToString();
                            //// oDataRows.ReceiptID = dr["ReceiptID"].ToString();
                            oDataRows.Category_Disp = dr["Category_Disp"].ToString();
                            oDataRows.Category_Core = dr["Category_Core"].ToString();
                            oDataRows.Amount = dr["Amount"].ToString();
                            oDataRows.Paid = dr["Paid"].ToString();
                            oDataRows.CustomerName = dr["CustomerName"].ToString() == "" ? "-" : dr["CustomerName"].ToString();
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
                else
                {
                    InvoiceModel oDataRows = new InvoiceModel();
                    oDataRows.ID = "0";
                    oPayment.Add(oDataRows);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                InvoiceModel oDataRows = new InvoiceModel();
                oDataRows.ERROR = ex.Message;
                oPayment.Add(oDataRows);
            }
            return oPayment.ToArray();
        }
        public bool Create(string sReceiptID, string sCreatedBY, string sAgency_ID, string sCustomer_ID, string sPaymentAmount, string sPaymentDate, string sPaymentStatus_Core, string sCategory_Core, string sAgent_ID, string sInvoice_ID)
        {
            try
            {
                //convert date into mwsql date formate
                string sTime = DateTime.Now.ToString("HH:mm:ss");
                sPaymentDate = sPaymentDate + " " + sTime;
                string dPaymentDate = Convert.ToDateTime(sPaymentDate).ToString("yyyy-MM-dd HH:mm:ss").ToString();
                sAgency_ID = sAgency_ID == "NONE" ? "0" : sAgency_ID;

                List<Parameter> oParams = new List<Parameter>();
                oParams.Clear();
                oParams.Add(new Parameter("sCreatedBY", sCreatedBY));
                oParams.Add(new Parameter("sAgency_ID", sAgency_ID));
                oParams.Add(new Parameter("sCustomer_ID", sCustomer_ID));
                //oParams.Add(new clsParams("sPaymentType", sPaymentType));
                oParams.Add(new Parameter("sReceiptID", sReceiptID));
                oParams.Add(new Parameter("sPaymentAmount", sPaymentAmount));
                oParams.Add(new Parameter("dPaymentDate", dPaymentDate));
                oParams.Add(new Parameter("sCategory_Core", sCategory_Core));
                oParams.Add(new Parameter("sAgent_ID", sAgent_ID));
                oParams.Add(new Parameter("sInvoice_ID", sInvoice_ID));
                _dbContext.LoadDataByProcedure("sps_Payments_Insert", oParams);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }

        }
        public DataTable Payments_SelectCount()
        {
            try
            {
                return _dbContext.LoadDataByProcedure("sps_Payments_SelectCount", null);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        public string Payments_SelectMonthsales()
        {
            string TotalMonthSale = "";
            try
            {
                List<Parameter> oParams = new List<Parameter>();
                oParams.Add(new Parameter("sCategory_Core", "FISCAL_DATE"));
                DataTable dt = _dbContext.LoadDataByProcedure("sps_SubCategories_SelectAll", oParams);

                int iFiscalMonth = Convert.ToInt32(dt.Rows[0]["SubCategory_Disp"].ToString());
                int iCurrentMonth = Convert.ToInt32(DateTime.Now.Month.ToString());
                int iYear;
                DateTime dDateStart, dDateEnd;

                //If Fiscal Month is less than Current Month
                if (iFiscalMonth <= iCurrentMonth)
                {
                    iYear = DateTime.Now.Year;
                    dDateStart = new DateTime(iYear, iFiscalMonth, 1);
                    dDateEnd = DateTime.Now;
                }
                //If Fiscal Month is greater than Current Month
                else
                {
                    iYear = DateTime.Now.Year - 1;
                    dDateStart = new DateTime(iYear, iFiscalMonth, 1);
                    dDateEnd = DateTime.Now;
                }
                oParams.Clear();
                oParams.Add(new Parameter("dDateStart", dDateStart.ToString("yyyy-MM-dd")));
                oParams.Add(new Parameter("dDateEnd", dDateEnd.ToString("yyyy-MM-dd")));
                dt.Clear();
                dt = _dbContext.LoadDataByProcedure("sps_Paymets_SelectYearSales", oParams);
                if (dt != null && dt.Rows.Count > 0)
                {

                    TotalMonthSale = dt.Rows[0]["SumOfPaymentAmount"].ToString();

                }
            }

            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
            return TotalMonthSale;
        }
        public DataTable Dashboard_GetPaymentsByLastWeek()
        {
            return _dbContext.LoadDataByProcedure("sps_Dashboard_GetPaymentsByLastWeek", null);
        }
        public DataTable Dashboard_GetPaymentsLast20()
        {
            return _dbContext.LoadDataByProcedure("sps_Dashboard_GetPaymentsLast20", null);
        }

    }
}
