﻿using Paybook.BusinessLayer.Invoice;
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
    public interface IDashboardProcessor
    {
        DashboardCountersModel GetAllCounters(int businessId);
        List<DashboardChartModel> GetClientCountByDays(int businessId, int days = 7);
        ChartModel[] GetInvoiceAmountsAndPaymentsByDays(int businessId, int days = 7);
        ChartModel[] GetCountOfInvoicesAndPaymentsByLastWeek(int businessId);
        ChartModel[] GetPaymentsLast10(int businessId);
        DataTable GetPaymentsByLastWeek();
        DataTable GetInvoiceCountByLastWeek();
    }

    public class DashboardProcessor : IDashboardProcessor
    {
        private readonly ILogger _logger;
        private readonly IDashboardRepository _dashboardRepo;

        public DashboardProcessor()
        {
            _logger = LoggerFactory.Instance;
            _dashboardRepo = new DashboardRepository();
        }

        public DashboardCountersModel GetAllCounters(int businessId)
        {
            try
            {
                return _dashboardRepo.GetAllCounters(businessId);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public List<DashboardChartModel> GetClientCountByDays(int businessId, int days = 7)
        {
            try
            {
                List<DashboardChartModel> clients = _dashboardRepo.GetClientCountByDays(businessId, days);

                List<DashboardChartModel> charts = new List<DashboardChartModel>();

                DateTime dTodayDate = Convert.ToDateTime(System.DateTime.Today.ToShortDateString());
                int i = 0, iTotalDays = 7;
                for (i = iTotalDays; i > 0; i--)
                {
                    var datetimeModel = new DashboardChartModel();

                    DateTime sDate = dTodayDate.AddDays(-i);
                    string sDay = sDate.ToShortDateString();

                    datetimeModel.Date = Convert.ToDateTime(sDay);

                    if (clients != null && clients.Count > 0)
                    {
                        foreach (var client in clients)
                        {
                            if (client.Date == datetimeModel.Date)
                            {
                                datetimeModel.Count = client.Count;
                                break;
                            }
                            else
                                datetimeModel.Count = 0;
                        }
                    }
                    else
                    {
                        datetimeModel.Count = 0;
                    }
                    charts.Add(datetimeModel);
                }

                return charts;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public ChartModel[] GetInvoiceAmountsAndPaymentsByDays(int businessId, int days = 7)
        {
            List<ChartModel> oChart = new List<ChartModel>();
            try
            {
                List<DashboardChartModel> invoices = _dashboardRepo.GetInvoiceCountAndAmountByDays(businessId, days);
                List<DashboardChartModel> payments = _dashboardRepo.GetPaymentCountAndAmountByDays(businessId, days);

                int iTotalDays = 6;
                DataTable dt = new DataTable();
                dt.Columns.Add("Date");
                dt.Columns.Add("InvoiceAmount");
                dt.Columns.Add("PaymentAmount");
                // dt.Columns.Add("Count");
                DateTime dTodayDate = Convert.ToDateTime(DateTime.Today.ToShortDateString());
                int i = 0;
                for (i = iTotalDays; i >= 0; i--)
                {
                    DataRow dr = dt.NewRow();

                    DateTime sDate = dTodayDate.AddDays(-i);
                    dr["Date"] = sDate.Date.ToString();

                    if (invoices != null && invoices.Count > 0)
                    {
                        foreach (var inv in invoices)
                        {
                            if (inv.Date == sDate.Date)
                            {
                                dr["InvoiceAmount"] = inv.Amount.ToString();
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


                    if (payments != null && payments.Count > 0)
                    {
                        foreach (var pay in payments)
                        {
                            if (pay.Date == sDate.Date)
                            {
                                dr["PaymentAmount"] = pay.Amount.ToString();
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
                        ChartModel oDataRows = new ChartModel
                        {
                            Date = Convert.ToDateTime(dr["Date"]).ToString("dd/MM/yyyy"),
                            InvoiceAmount = dr["InvoiceAmount"].ToString(),
                            PaymentAmount = dr["PaymentAmount"].ToString()
                        };
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
        public ChartModel[] GetCountOfInvoicesAndPaymentsByLastWeek(int businessId)
        {
            try
            {
                List<ChartModel> charts = new List<ChartModel>();

                var chart = new ChartModel
                {
                    Entity = "Invoices",
                    Count = Invoices_SelectCount().ToString()
                };
                charts.Add(chart);
                chart = new ChartModel
                {
                    Entity = "Payments",
                    Count = Payments_SelectCount().ToString()
                };
                charts.Add(chart);

                return charts.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        public ChartModel[] GetPaymentsLast10(int businessId)
        {
            List<ChartModel> charts = new List<ChartModel>();
            try
            {
                List<DashboardChartModel> payments = _dashboardRepo.GetPaymentAmountByLast10(businessId);

                if (payments != null && payments.Count > 0)
                {
                    foreach (var pay in payments)
                    {
                        ChartModel chart = new ChartModel
                        {
                            Date = pay.Date.Value.ToString("dd/MM/yyyy"),
                            PaymentAmount = pay.Amount.ToString()
                        };
                        charts.Add(chart);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
            return charts.ToArray();

        }
        public DataTable GetPaymentsByLastWeek()
        {
            try
            {
                return null;// _dashboardRepo.Dashboard_GetPaymentsByLastWeek();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        //public DataTable Dashboard_GetInvoiceAmountsByLastWeek()
        //{
        //    try
        //    {
        //        return _dashboardRepo.Dashboard_GetInvoiceAmountsByLastWeek();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(_logger.MethodName, ex);

        //        throw;
        //    }
        //}
        public DataTable GetInvoiceCountByLastWeek()
        {
            try
            {
                return null; // _dashboardRepo.Dashboard_GetInvoiceCountByLastWeek();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        private int Payments_SelectCount()
        {
            try
            {
                DataTable dt = _dashboardRepo.Payments_SelectCount();
                if (dt != null && dt.Rows.Count > 0)
                    return (int)dt.Rows[0]["IDCount"];

                return 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        private int Invoices_SelectCount()
        {
            try
            {
                DataTable dt = _dashboardRepo.Invoices_SelectCount();
                if (dt != null || dt.Rows.Count > 0)
                    return (int)dt.Rows[0]["IDCount"];

                return 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
    }
}
