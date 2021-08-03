using Paybook.BusinessLayer.Invoice;
using Paybook.BusinessLayer.Payment;
using Paybook.DatabaseLayer.Common;
using Paybook.DatabaseLayer.Invoice;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.BusinessLayer.Common
{
    public interface IChartProcessor
    {
        ChartModel[] Dashboard_GetCountOfInvoiceAndPaymentByLastWeek();
        ChartModel[] Count_PaymentInvoice_Chart();
        ChartModel[] Customer_Chart();
    }

    public class ChartProcessor : IChartProcessor
    {
        private readonly ILogger _logger;
        private readonly IChartRepository _chartRepo;
        private readonly IInvoiceProcessor _invoiceRepo;
        private readonly IPaymentProcessor _paymentRepo;

        public ChartProcessor()
        {
            _logger = FileLogger.Instance;
            _chartRepo = new ChartRepository();
            _invoiceRepo = new InvoiceProcessor();
            _paymentRepo = new PaymentProcessor();
        }

        public ChartModel[] Dashboard_GetCountOfInvoiceAndPaymentByLastWeek()
        {
            List<ChartModel> oChart = new List<ChartModel>();
            try
            {
                DataTable dtPayment = _paymentRepo.Dashboard_GetPaymentCountByLastWeek();
                DataTable dtInvoice = _invoiceRepo.Dashboard_GetInvoiceCountByLastWeek();
                //if (dtInvoice != null && dtInvoice.Rows.Count > 0)
                //{
                //    foreach (DataRow dr in dtInvoice.Rows)
                //    {
                //        clsChart oDataRows = new clsChart();
                //        oDataRows.Date = Convert.ToDateTime(dr["Invoice_Date"]).ToString("dd/MM");
                //        oDataRows.PaymentAmount = dr["SumOfPaymentAmount"].ToString() == " " ? "0" : dr["SumOfPaymentAmount"].ToString();
                //        oDataRows.InvoiceAmount = dr["InvoiceAmount"].ToString();
                //        oChart.Add(oDataRows);
                //    }
                //}
                int iTotalDays = 6;
                DataTable dt = new DataTable();
                dt.Columns.Add("Date");
                dt.Columns.Add("InvoiceAmount");
                dt.Columns.Add("PaymentAmount");
                // dt.Columns.Add("Count");
                DateTime dTodayDate = Convert.ToDateTime(System.DateTime.Today.ToShortDateString());
                int i = 0;
                for (i = iTotalDays; i >= 0; i--)
                {
                    DataRow dr = dt.NewRow();

                    DateTime sDate = dTodayDate.AddDays(-i);
                    dr["Date"] = sDate.Date.ToString();

                    if (dtInvoice != null && dtInvoice.Rows.Count > 0)
                    {
                        foreach (DataRow drInvoice in dtInvoice.Rows)
                        {
                            string sInvoiceDate = Convert.ToDateTime(drInvoice["Invoice_Date"]).Date.ToString();
                            if (sInvoiceDate == sDate.Date.ToString())
                            {
                                dr["InvoiceAmount"] = drInvoice["InvoiceAmount"].ToString();
                                break;
                            }
                            else
                                dr["InvoiceAmount"] = "0";
                        }
                    }
                    else
                    {
                        dr["InvoiceAmount"] = "0";
                    }
                    //dt.Rows.Add(dr);


                    if (dtPayment != null && dtPayment.Rows.Count > 0)
                    {
                        foreach (DataRow drPayment in dtPayment.Rows)
                        {
                            string sPaymentDate = Convert.ToDateTime(drPayment["Payment_Date"]).Date.ToString();
                            if (sPaymentDate == sDate.Date.ToString())
                            {
                                dr["PaymentAmount"] = drPayment["PaymentAmount"].ToString();
                                break;
                            }
                            else
                                dr["PaymentAmount"] = "0";
                        }
                    }
                    else
                    {
                        dr["PaymentAmount"] = "0";
                    }
                    dt.Rows.Add(dr);
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ChartModel oDataRows = new ChartModel();
                        oDataRows.Date = Convert.ToDateTime(dr["Date"]).ToString("dd/MM");
                        oDataRows.InvoiceAmount = dr["InvoiceAmount"].ToString();
                        oDataRows.PaymentAmount = dr["PaymentAmount"].ToString();
                        oChart.Add(oDataRows);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
            return oChart.ToArray();

        }

        public ChartModel[] Count_PaymentInvoice_Chart()
        {
            try
            {
                List<ChartModel> charts = new List<ChartModel>();

                ChartModel chartRow0 = new ChartModel();
                chartRow0.Entity = "Invoices";
                chartRow0.Count = _invoiceRepo.Invoices_SelectCount().ToString();
                charts.Add(chartRow0);
                ChartModel chartRow1 = new ChartModel();
                chartRow1.Entity = "Payments";
                chartRow1.Count = _paymentRepo.Payments_SelectCount().ToString();
                charts.Add(chartRow1);

                return charts.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public ChartModel[] Customer_Chart()
        {
            List<ChartModel> charts = new List<ChartModel>();
            try
            {
                DataTable dtCustomer = _chartRepo.Customer_Chart();
                int iTotalDays = 7;
                DataTable dt = new DataTable();
                dt.Columns.Add("Date");
                dt.Columns.Add("Count");
                DateTime dTodayDate = Convert.ToDateTime(System.DateTime.Today.ToShortDateString());
                int i = 0;
                for (i = iTotalDays; i > 0; i--)
                {
                    DataRow dr = dt.NewRow();

                    DateTime sDate = dTodayDate.AddDays(-i);
                    string sDay = sDate.Day.ToString();

                    dr["Date"] = sDay;

                    if (dtCustomer != null && dtCustomer.Rows.Count > 0)
                    {
                        foreach (DataRow drCustomer in dtCustomer.Rows)
                        {
                            if (Convert.ToInt16(drCustomer["CreatedDT"]) == Convert.ToInt16(dr["Date"]))
                            {
                                dr["Count"] = drCustomer["Count"].ToString();
                                break;
                            }
                            else
                                dr["Count"] = "0";
                        }
                    }
                    else
                    {
                        dr["Count"] = "0";
                    }
                    dt.Rows.Add(dr);
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ChartModel oDataRows = new ChartModel();
                        oDataRows.Date = dr["Date"].ToString();
                        oDataRows.Count = dr["Count"].ToString() == " " ? "0" : dr["Count"].ToString();
                        charts.Add(oDataRows);
                    }
                }
                else
                {
                    charts.Add(new ChartModel
                    {
                        Date = DateTime.Now.ToShortDateString(),
                        Count = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
            return charts.ToArray();
        }
    }
}
