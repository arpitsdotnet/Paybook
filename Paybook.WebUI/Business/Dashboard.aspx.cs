using Paybook.BusinessLayer;
using Paybook.BusinessLayer.Business;
using Paybook.BusinessLayer.Common;
using Paybook.BusinessLayer.Invoice;
using Paybook.BusinessLayer.Payment;
using Paybook.ServiceLayer.Constants;
using Paybook.ServiceLayer.Logger;
using System;
using System.Data;
using System.Text;

namespace Paybook.WebUI.Business
{
    public partial class Dashboard : System.Web.UI.Page
    {
        private readonly ILogger _logger;
        private readonly IActivityProcessor _activityProcessor;
        private readonly IDashboardProcessor _dashboardProcessor;
        private readonly IPaymentProcessor _paymentProcessor;
        public Dashboard(ILogger logger, IActivityProcessor activityProcessor, IDashboardProcessor dashboardProcessor, IPaymentProcessor paymentProcessor)
        {
            _logger = logger;
            _activityProcessor = activityProcessor;
            _dashboardProcessor = dashboardProcessor;
            _paymentProcessor = paymentProcessor;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["LoggedInUser"] == null || Session["LoggedInUser"].ToString() == "")
                {
                    Response.Redirect("~/identity/login", false);
                }
                if (!IsPostBack)
                {
                    GetAllCounters();
                    GetAllActivities();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + ex.Message + "');});", true);
            }

        }

        protected void GetAllActivities()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                DataTable dt = _activityProcessor.GetAll();
                if (dt != null && dt.Rows.Count > 0)
                {
                    sb.Append("<ul>");
                    foreach (DataRow dr in dt.Rows)
                    {
                        string sActivityDate = Convert.ToDateTime(dr["Activity_Date"].ToString()).ToString("d MMM");

                        string invoiceID = dr["Invoice_ID"].ToString();
                        string invoiceStatusCore = dr["InvoiceStatus_Core"].ToString();
                        string invoiceStatus = dr["InvoiceStatus_Disp"].ToString();
                        string paymentAmount = dr["PaymentAmount"].ToString();
                        string clientName = dr["CustomerName"].ToString();
                        string categoryCore = dr["Category_Core"].ToString();

                        string statusClass = ActivityStatusCss.DEFAULT;

                        if (invoiceStatusCore == InvoiceStatusConst.Overdue)
                            statusClass = ActivityStatusCss.DANGER;
                        else if (invoiceStatusCore == InvoiceStatusConst.Paid || invoiceStatusCore == InvoiceStatusConst.PaidPartial)
                            statusClass = ActivityStatusCss.SUCCESS;
                        else if (invoiceStatusCore == InvoiceStatusConst.Open)
                            statusClass = ActivityStatusCss.INFO;
                        else if (invoiceStatusCore == InvoiceStatusConst.Close)
                            statusClass = ActivityStatusCss.DEFAULT;

                        sb.AppendFormat("<li>{0}</li>", ActivityListViewGenerate(statusClass, invoiceID, sActivityDate, paymentAmount, clientName, invoiceStatus, categoryCore));
                    }
                    sb.Append("</ul>");
                }
                else
                {
                    sb.AppendLine("<div class=\"" + ActivityStatusCss.INFO + " pt-5\">Ooh! We did not find any activity till last month. </div>");
                }
                idActivitiesList.InnerHtml = sb.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }


        private string ActivityListViewGenerate(string statusClass, string invoiceID, string sInvoiceDate, string sAmount, string sCustomerName, string InvoiceStatus_Disp, string Category_Core)
        {
            try
            {
                return "<div class=\"" + statusClass + " fwt-large\"><i class='fa fa-info-circle'></i>&nbsp;" + InvoiceStatus_Disp + " <span class=\"fwt-small fwt-text-grey\">"
                         + "(" + sInvoiceDate + ")</span></div>"
                            + "<div class=\"fwt-small\">"
                                //+ "Invoice <span class=\"fwt-text-blue\">" + invoiceID + "</span> : <i class='fa fa-inr'></i>" + sAmount + " to <span class=\"fwt-text-blue\">" + sCustomerName + "</span>"
                                + "Invoice <span class=\"fwt-text-blue\">" + invoiceID + "</span> to <span class=\"fwt-text-blue\">" + sCustomerName + "</span> of <span class='nowrap'><i class='fa fa-inr'></i>" + sAmount + "</span> is " + InvoiceStatus_Disp + "."
                            + "</div>";
                // +"Invoice <span class=\"fwt-text-blue\"><a href=\"particular/" + sParticular + "/" + Category_Core + "\"" + ">#" + sParticular + "</a></span>: ₹" + sAmount + " to <span class=\"fwt-text-blue\">"
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void GetAllCounters()
        {
            try
            {
                DataTable dt = _dashboardProcessor.Dashboard_GetAllCounters();
                if (dt.Rows.Count > 0 && dt != null)
                {
                    idInvoicesOpen.Count = dt.Rows[0]["CountTotalOpenInvoice"].ToString();
                    idInvoicesOpen.Total = dt.Rows[0]["SumofTotalOpenInvoice"].ToString() == "" ? "0" : dt.Rows[0]["SumofTotalOpenInvoice"].ToString();

                    idInvoicesOpenLastWeek.Count = dt.Rows[0]["CountLastWeekOpenInvoice"].ToString();
                    idInvoicesOpenLastWeek.Total = dt.Rows[0]["SumLastWeekOpenInvoice"].ToString() == "" ? "0" : dt.Rows[0]["SumLastWeekOpenInvoice"].ToString();

                    idInvoicesOverdue.Count = dt.Rows[0]["CountOfOverdue"].ToString();
                    idInvoicesOverdue.Total = dt.Rows[0]["SumOfOverdue"].ToString() == "" ? "0" : dt.Rows[0]["SumOfOverdue"].ToString();

                    idPaymentPaidPartial.Count = dt.Rows[0]["CountOfPaidPartial"].ToString();
                    idPaymentPaidPartial.Total = dt.Rows[0]["SumOfPaidPartialAmount"].ToString() == "" ? "0" : dt.Rows[0]["SumOfPaidPartialAmount"].ToString();

                    idPaymentPaidLastMonth.Count = dt.Rows[0]["CountOfPaidAmount"].ToString();
                    idPaymentPaidLastMonth.Total = dt.Rows[0]["SumOfPaidAmount"].ToString() == "" ? "0" : dt.Rows[0]["SumOfPaidAmount"].ToString();

                    idPaymentTotal.Count = dt.Rows[0]["CountOfPaymentTotal"].ToString();
                    idPaymentTotal.Total = dt.Rows[0]["SumOfPaymentTotal"].ToString() == "" ? "0" : dt.Rows[0]["SumOfPaymentTotal"].ToString();

                    //lblCounts_OpenInvoice.Text = dt.Rows[0]["CountOfOpenInvoice"].ToString();
                    //lblTotal_OpenInvoice.Text = dt.Rows[0]["SumOfOpenInvoice"].ToString() == "" ? "0" : dt.Rows[0]["SumOfOpenInvoice"].ToString();

                    //lblCounts_OpenLastMonth.Text = dt.Rows[0]["CountTotalOpenInvoice"].ToString();
                    //lblTotal_OpenLastMonth.Text = dt.Rows[0]["SumofTotalOpenInvoice"].ToString() == "" ? "0" : dt.Rows[0]["SumofTotalOpenInvoice"].ToString();

                    //lblCounts_Overdue.Text = dt.Rows[0]["CountOfOverdue"].ToString();
                    //lblTotal_Overdue.Text = dt.Rows[0]["SumOfOverdue"].ToString() == "" ? "0" : dt.Rows[0]["SumOfOverdue"].ToString();

                    //lblCounts_Paid_Partial.Text = dt.Rows[0]["CountOfPaidPartial"].ToString();
                    //lblTotal_Paid_Partial.Text = dt.Rows[0]["SumOfPaidPartialAmount"].ToString() == "" ? "0" : dt.Rows[0]["SumOfPaidPartialAmount"].ToString();

                    //lblCounts_PaidLastMonth.Text = dt.Rows[0]["CountOfPaidAmount"].ToString();
                    //lblTotal_PaidLastMonth.Text = dt.Rows[0]["SumOfPaidAmount"].ToString() == "" ? "0" : dt.Rows[0]["SumOfPaidAmount"].ToString();

                    lblCustomerCount.Text = dt.Rows[0]["CountofCustomers"].ToString();

                    lblTotalMonthSaleValue.Text = _paymentProcessor.Payments_SelectMonthsales();

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }

        //protected void Get_CompanyProfile()
        //{
        //    DataTable dtCompanyProfile = clsCommon.CompanyProfile_Select();
        //    if (dtCompanyProfile.Rows.Count > 0 && dtCompanyProfile != null)
        //    {
        //        lblCompanyName.Text = dtCompanyProfile.Rows[0]["CompanyName"].ToString();
        //        imgCompanyLogo.ImageUrl = _FolderPath.CompanyLogo_Path + dtCompanyProfile.Rows[0]["ImageFileName"].ToString();
        //        //hfCompanyLogo_Image.Value = dtCompanyProfile.Rows[0]["ImageFileName"].ToString();               

        //    }
        //}

    }
}