using Paybook.BusinessLayer.Business;
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
    public interface IDashboardProcessor
    {
        DashboardCountersModel GetAllCounters(string username);
        List<DashboardClientChartModel> GetClientCountByDays(string username, int days = 7);
        List<DashboardInvoiceChartModel> GetInvoiceAmountsAndPaymentsByDays(string username, int days = 7);
        List<DashboardInvoiceChartModel> GetCountOfInvoicesAndPaymentsByLastWeek(string username);
        List<DashboardInvoiceChartModel> GetPaymentsLast10(string username);
        DataTable GetPaymentsByLastWeek();
        DataTable GetInvoiceCountByLastWeek();
    }

    public class DashboardProcessor : IDashboardProcessor
    {
        private readonly ILogger _logger;
        private readonly IDashboardRepository _dashboardRepo;
        private readonly IBusinessProcessor _business;
        private readonly IInvoiceProcessor _invoice;

        public DashboardProcessor()
        {
            _logger = LoggerFactory.Instance;
            _dashboardRepo = new DashboardRepository();
            _business = new BusinessProcessor();
            _invoice = new InvoiceProcessor();
        }

        public DashboardCountersModel GetAllCounters(string username)
        {
            try
            {
                var business = _business.GetSelectedByUsername(username);

                return _dashboardRepo.GetAllCounters(business.Id);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }

        public List<DashboardClientChartModel> GetClientCountByDays(string username, int days = 7)
        {
            try
            {
                var business = _business.GetSelectedByUsername(username);

                var clients = _dashboardRepo.GetClientCountByDays(business.Id, days);

                var charts = new List<DashboardClientChartModel>();

                DateTime dTodayDate = Convert.ToDateTime(System.DateTime.Today.ToShortDateString());
                int i = 0, iTotalDays = 6;
                for (i = iTotalDays; i >= 0; i--)
                {
                    var datetimeModel = new DashboardClientChartModel();

                    DateTime sDate = dTodayDate.AddDays(-i);
                    string sDay = sDate.ToShortDateString();

                    datetimeModel.Date = sDay;

                    if (clients != null && clients.Count > 0)
                    {
                        foreach (var client in clients)
                        {
                            if (client.Date == Convert.ToDateTime(datetimeModel.Date))
                            {
                                datetimeModel.Count = client.Count.ToString();
                                break;
                            }
                            else
                                datetimeModel.Count = "0";
                        }
                    }
                    else
                    {
                        datetimeModel.Count = "0";
                    }
                    charts.Add(datetimeModel);
                }

                return charts;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public List<DashboardInvoiceChartModel> GetInvoiceAmountsAndPaymentsByDays(string username, int days = 7)
        {
            List<DashboardInvoiceChartModel> charts = new List<DashboardInvoiceChartModel>();
            try
            {
                var business = _business.GetSelectedByUsername(username);

                List<DashboardChartModel> invoices = _dashboardRepo.GetInvoiceCountAndAmountByDays(business.Id, days);
                List<DashboardChartModel> payments = _dashboardRepo.GetPaymentCountAndAmountByDays(business.Id, days);

                DataTable dt = new DataTable();
                dt.Columns.Add("Date");
                dt.Columns.Add("InvoiceAmount");
                dt.Columns.Add("PaymentAmount");
                // dt.Columns.Add("Count");
                DateTime dTodayDate = Convert.ToDateTime(DateTime.Today.ToShortDateString());
                int i = 0 , iTotalDays = 6;
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
                        DashboardInvoiceChartModel oDataRows = new DashboardInvoiceChartModel
                        {
                            Date = Convert.ToDateTime(dr["Date"]).ToString("dd/MM/yyyy"),
                            InvoiceAmount = dr["InvoiceAmount"].ToString(),
                            PaymentAmount = dr["PaymentAmount"].ToString()
                        };
                        charts.Add(oDataRows);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
            return charts;
        }
        public List<DashboardInvoiceChartModel> GetCountOfInvoicesAndPaymentsByLastWeek(string username)
        {
            try
            {
                List<DashboardInvoiceChartModel> charts = new List<DashboardInvoiceChartModel>();

                //var business = _business.GetSelectedByUsername(username);
                var invoiceCount = 0;// _invoice.GetCount(business.Id);
                var paymentCount = 0;// _payment.GetCount(business.Id);

                var chart = new DashboardInvoiceChartModel
                {
                    Entity = "Invoices",
                    Count = invoiceCount.ToString() //Invoices_SelectCount().ToString()
                };
                charts.Add(chart);
                chart = new DashboardInvoiceChartModel
                {
                    Entity = "Payments",
                    Count = paymentCount.ToString()// Payments_SelectCount().ToString()
                };
                charts.Add(chart);

                return charts;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public List<DashboardInvoiceChartModel> GetPaymentsLast10(string username)
        {
            List<DashboardInvoiceChartModel> charts = new List<DashboardInvoiceChartModel>();
            try
            {
                var business = _business.GetSelectedByUsername(username);

                List<DashboardChartModel> payments = _dashboardRepo.GetPaymentAmountByLast10(business.Id);

                if (payments != null && payments.Count > 0)
                {
                    foreach (var pay in payments)
                    {
                        DashboardInvoiceChartModel chart = new DashboardInvoiceChartModel
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
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
            return charts;

        }
        public DataTable GetPaymentsByLastWeek()
        {
            try
            {
                return null;// _dashboardRepo.Dashboard_GetPaymentsByLastWeek();
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

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
                _logger.Error(_logger.GetMethodName(), ex);

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
                _logger.Error(_logger.GetMethodName(), ex);

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
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
    }
}
