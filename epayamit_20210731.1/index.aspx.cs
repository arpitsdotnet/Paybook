using System;
using System.Data;
using Paybook.BusinessLayer;
using System.Text;

namespace Paybook.WebUI
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["LoggedInUser"] == null || Session["LoggedInUser"].ToString() == "")
                {
                    Response.Redirect("login", false);
                }
                if (!IsPostBack)
                {
                    //if (Properties.Settings.Default.FirstRun == true)
                    {
                        InvoiceCheckOnFirstRun();
                    }
                    // Get_CompanyProfile();
                    TotalMonthSale();
                    InvoiceActivities_SelectAll();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + ex.Message + "');});", true);

            }

        }
        protected void InvoiceCheckOnFirstRun()
        {
            try
            {
                if (Session["LoggedInUser"] == null)
                {
                    Session["LoggedInUser"] = "";

                }
                else
                {
                    string[] sLoginUser = Session["LoggedInUser"].ToString().Split('/');
                    hfLogInUser.Value = sLoginUser[0];
                    hfLogInUser_ID.Value = sLoginUser[1];
                    clsCommon.Activity_Insert_Overdue(hfLogInUser.Value, "IS_OVERDUE");
                    //Properties.Settings.Default["FirstRun"] = false;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void InvoiceActivities_SelectAll()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                DataTable dt = clsCommon.Activity_Select();
                if (dt != null && dt.Rows.Count > 0)
                {
                    sb.Append("<ul>");
                    foreach (DataRow dr in dt.Rows)
                    {
                        string sActivityDate = Convert.ToDateTime(dr["Activity_Date"].ToString()).ToString("d MMM");

                        if (dr["InvoiceStatus_Core"].ToString() == "IS_OVERDUE")
                        {
                            string sOverdue = ActivityView_Overdue(sActivityDate, dr["PaymentAmount"].ToString(), dr["CustomerName"].ToString(), dr["InvoiceStatus_Disp"].ToString(), dr["Category_Core"].ToString());
                            sb.Append("<li>" + sOverdue + "</li>");
                        }
                        if (dr["InvoiceStatus_Core"].ToString() == "IS_PAID" || dr["InvoiceStatus_Core"].ToString() == "IS_PAID_PARTIAL")
                        {
                            string sPayment = ActivityView_Paid(sActivityDate, dr["PaymentAmount"].ToString(), dr["CustomerName"].ToString(), dr["InvoiceStatus_Disp"].ToString(), dr["Category_Core"].ToString());
                            sb.Append("<li class=\"paid\">" + sPayment + "</li>");
                        }
                        if (dr["InvoiceStatus_Core"].ToString() == "IS_CLOSE")
                        {
                            string sPayment = ActivityView_Close(sActivityDate, dr["PaymentAmount"].ToString(), dr["CustomerName"].ToString(), dr["InvoiceStatus_Disp"].ToString(), dr["Category_Core"].ToString());
                            sb.Append("<li class=\"close\">" + sPayment + "</li>");
                        }
                    }
                    sb.Append("</ul>");
                }
                idActivitiesList.InnerHtml = sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected string ActivityView_Overdue(string sInvoiceDate, string sAmount, string sCustomerName, string InvoiceStatus_Disp, string Category_Core)
        {
            string sReturn = "";
            try
            {
                sReturn = "<span class=\"fwt-text-red fwt-large\">" + InvoiceStatus_Disp + " <span class=\"fwt-small fwt-text-grey\">"
                         + "(" + sInvoiceDate + ")</span></span>"
                            + "<div class=\"fwt-small\">"
                                + "Invoice <span class=\"fwt-text-blue\"></span>:<i class='fa fa-inr'></i>" + sAmount + " to <span class=\"fwt-text-blue\">"
                                    + sCustomerName + "</span>"
                            + "</div>";
                // +"Invoice <span class=\"fwt-text-blue\"><a href=\"particular/" + sParticular + "/" + Category_Core + "\"" + ">#" + sParticular + "</a></span>: ₹" + sAmount + " to <span class=\"fwt-text-blue\">"
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return sReturn;
        }

        protected string ActivityView_Paid(string sPaymentDate, string sAmount, string sCustomerName, string InvoiceStatus_Disp, string Category_Core)
        {
            string sReturn = "";
            try
            {
                sReturn = "<span class=\"fwt-text-green fwt-large\">" + InvoiceStatus_Disp + " <span class=\"fwt-small fwt-text-grey\">"
                       + "(" + sPaymentDate + ")</span></span>"
                          + "<div class=\"fwt-small\">"
                              + "Invoice <span class=\"fwt-text-blue\"></span>: <i class='fa fa-inr'></i>" + sAmount + " to <span class=\"fwt-text-blue\">"
                                  + sCustomerName + "</span>"
                          + "</div>";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return sReturn;
        }

        protected string ActivityView_Close(string sPaymentDate, string sAmount, string sCustomerName, string InvoiceStatus_Disp, string Category_Core)
        {
            string sReturn = "";
            try
            {
                sReturn = "<span class=\"fwt-text-grey fwt-large\">" + InvoiceStatus_Disp + " <span class=\"fwt-small fwt-text-grey\">"
                       + "(" + sPaymentDate + ")</span></span>"
                          + "<div class=\"fwt-small\">"
                              + "Invoice <span class=\"fwt-text-blue\"></span>: <i class='fa fa-inr'></i>" + sAmount + " to <span class=\"fwt-text-blue\">"
                                  + sCustomerName + "</span>"
                          + "</div>";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return sReturn;
        }

        protected void TotalMonthSale()
        {
            try
            {
                DataTable dt = clsCommon.Dashboard_SelectCounts();
                if (dt.Rows.Count > 0 && dt != null)
                {
                    lblCounts_OpenLastMonth.Text = dt.Rows[0]["CountTotalOpenInvoice"].ToString();
                    lblTotal_OpenLastMonth.Text = dt.Rows[0]["SumofTotalOpenInvoice"].ToString() == "" ? "0" : dt.Rows[0]["SumofTotalOpenInvoice"].ToString();

                    lblCounts_Overdue.Text = dt.Rows[0]["CountOfOverdue"].ToString();
                    lblTotal_Overdue.Text = dt.Rows[0]["SumOfOverdue"].ToString() == "" ? "0" : dt.Rows[0]["SumOfOverdue"].ToString();

                    lblCounts_PaidLastMonth.Text = dt.Rows[0]["CountOfPaidAmount"].ToString();
                    lblTotal_PaidLastMonth.Text = dt.Rows[0]["SumOfPaidAmount"].ToString() == "" ? "0" : dt.Rows[0]["SumOfPaidAmount"].ToString();

                    lblCounts_Paid_Partial.Text = dt.Rows[0]["CountOfPaidPartial"].ToString();
                    lblTotal_Paid_Partial.Text = dt.Rows[0]["SumOfPaidPartialAmount"].ToString() == "" ? "0" : dt.Rows[0]["SumOfPaidPartialAmount"].ToString();

                    lblCounts_OpenInvoice.Text = dt.Rows[0]["CountOfOpenInvoice"].ToString();
                    lblTotal_OpenInvoice.Text = dt.Rows[0]["SumOfOpenInvoice"].ToString() == "" ? "0" : dt.Rows[0]["SumOfOpenInvoice"].ToString();

                    lblCustomerCount.Text = dt.Rows[0]["CountofCustomers"].ToString();

                    lblTotalMonthSaleValue.Text = clsCommon.Payments_SelectMonthsales();
                   
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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