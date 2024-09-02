using System;
using System.Collections.Generic;
using System.Data;
using Paybook.BusinessLayer.Abstracts.Admins;
using Paybook.BusinessLayer.Abstracts.Customers;
using Paybook.DatabaseLayer.Abstracts.Customers;
using Paybook.DatabaseLayer.Features.Customers;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Models.Dashboards;
using Paybook.ServiceLayer.Models.Invoices;
using Paybook.ServiceLayer.Models.ViewModels;

namespace Paybook.BusinessLayer.Features.Customers
{
    public class DashboardProcessor : IDashboardProcessor
    {
        private readonly ILogger _logger;
        private readonly IBusinessProcessor _business;
        private readonly ICountryProcessor _country;
        private readonly IStateProcessor _state;
        private readonly IDashboardRepository _dashboardRepo;

        public DashboardProcessor(
            ILogger logger,
            IBusinessProcessor business,
            ICountryProcessor country,
            IStateProcessor state)
        {
            _logger = logger;
            _business = business;
            _country = country;
            _state = state;
            _dashboardRepo = new DashboardRepository();
        }

        public DashboardViewModel GetAllCounters(string username)
        {
            try
            {
                var business = _business.GetSelectedByUsername(username);

                DashboardCountersModel model = _dashboardRepo.GetAllCounters(business.Id);

                DashboardViewModel dashboardVM = new DashboardViewModel
                {
                    Business = business,

                    CounterInvoicesOpen = new DashboardCounterWidgetModel
                    {
                        BgColorClass = "fwt-blue",
                        BgColorHoverClass = "fwt-hover-black",
                        WidgetIcon = "fa-rupee",
                        WidgetIconColor = "color: #4d5f68;",
                        CountText = "Invoices",
                        Count = model.OpenInvoiceCount,
                        Total = model.OpenInvoiceTotal
                    },
                    //CounterInvoicesOpenLastWeek = new DashboardCounterWidgetModel
                    //{
                    //    BgColorClass = "fwt-deep-purple",
                    //    BgColorHoverClass = "fwt-hover-black",
                    //    RsSymbolColor = "color: #562f9A;",
                    //    CountText = "Open Invoices (Week)",
                    //    Count = model.CountLastWeekOpenInvoice,
                    //    Total = model.SumLastWeekOpenInvoice
                    //},
                    CounterInvoicesOverdue = new DashboardCounterWidgetModel
                    {
                        BgColorClass = "fwt-deep-orange",
                        BgColorHoverClass = "fwt-hover-black",
                        WidgetIcon = "fa-exclamation-triangle",
                        WidgetIconColor = "color: #bd4339;",
                        CountText = "Overdues",
                        Count = model.OverdueInvoiceCount,
                        Total = model.OverdueInvoiceTotal
                    },
                    CounterPaymentPaidPartial = new DashboardCounterWidgetModel
                    {
                        BgColorClass = "fwt-blue-grey",
                        BgColorHoverClass = "fwt-hover-black",
                        WidgetIcon = "fa-file-invoice",
                        WidgetIconColor = "color: #007469;",
                        CountText = "Invoice Paid",
                        Count = model.PartialPaidInvoiceCount,
                        Total = model.PartialPaidInvoiceTotal
                    },
                    //CounterPaymentPaidLastMonth = new DashboardCounterWidgetModel
                    //{
                    //    BgColorClass = "fwt-blue",
                    //    BgColorHoverClass = "fwt-hover-black",
                    //    RsSymbolColor = "color: #1b76be;",
                    //    CountText = "Paid Invoice (Month)",
                    //    Count = model.CountOfPaidAmount,
                    //    Total = model.SumOfPaidAmount
                    //},
                    CounterPaymentTotal = new DashboardCounterWidgetModel
                    {
                        BgColorClass = "fwt-green",
                        BgColorHoverClass = "fwt-hover-black",
                        WidgetIcon = "fa-circle-down",
                        WidgetIconColor = "color: #2d8630;",
                        CountText = "Deposits",
                        Count = model.DepositCount,
                        Total = model.DepositTotal
                    },

                    ClientCounter = new DashboardCounterWidgetModel
                    {
                        Count = model.CustomerCount,
                        Total = model.DepositTotal
                    }
                };

                return dashboardVM;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }

        public List<BusinessModel> GetAllBusinesses(string username)
        {
            List<BusinessModel> businesses = _business.GetAllByUsername(username);
            foreach (var business in businesses)
            {
                business.CountryMaster = _country.GetById(business.CountryId);
                business.StateMaster = _state.GetById(business.StateId);
            }

            return businesses;
        }

        public List<DashboardClientChartModel> GetClientCountByDays(string username, int days = 7)
        {
            try
            {
                var business = _business.GetSelectedByUsername(username);

                var clients = _dashboardRepo.GetClientCounterByDays(business.Id, days);

                var charts = new List<DashboardClientChartModel>();

                DateTime dTodayDate = Convert.ToDateTime(DateTime.Today.ToShortDateString());
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

                List<DashboardChartModel> invoices = _dashboardRepo.GetInvoiceCountAndTotalByDays(business.Id, days);
                List<DashboardChartModel> payments = _dashboardRepo.GetPaymentCountAndTotalByDays(business.Id, days);

                DataTable dt = new DataTable();
                dt.Columns.Add("Date");
                dt.Columns.Add("InvoiceAmount");
                dt.Columns.Add("PaymentAmount");
                // dt.Columns.Add("Count");
                DateTime dTodayDate = Convert.ToDateTime(DateTime.Today.ToShortDateString());
                int i = 0, iTotalDays = 6;
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

                var business = _business.GetSelectedByUsername(username);
                var invoiceCount = _dashboardRepo.GetInvoiceCountByDays(business.Id, 7);
                var paymentCount = _dashboardRepo.GetPaymentCountByDays(business.Id, 7);

                var chart = new DashboardInvoiceChartModel
                {
                    Entity = "Invoices",
                    Count = invoiceCount.ToString()
                };
                charts.Add(chart);
                chart = new DashboardInvoiceChartModel
                {
                    Entity = "Deposits",
                    Count = paymentCount.ToString()
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

                List<DashboardChartModel> payments = _dashboardRepo.GetPaymentTotalByLast10(business.Id);

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
        public List<InvoiceModel> GetLast5Invoices(string username)
        {
            try
            {
                var business = _business.GetSelectedByUsername(username);

                return _dashboardRepo.GetLast5Invoices(business.Id);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public List<PaymentModel> GetLast5Payments(string username)
        {
            try
            {
                var business = _business.GetSelectedByUsername(username);

                return _dashboardRepo.GetLast5Payments(business.Id);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
    }
}
