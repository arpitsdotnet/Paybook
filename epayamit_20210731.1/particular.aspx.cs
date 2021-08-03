using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Data;
using Paybook.BusinessLayer;

namespace Paybook.WebUI
{
    public partial class particular : System.Web.UI.Page
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
                    if (Page.RouteData.Values["Invoice_ID"].ToString() != null && Page.RouteData.Values["category"].ToString() != null)
                    {
                        string strInvoice_ID = Page.RouteData.Values["Invoice_ID"].ToString();
                        hfInvoice_ID.Value = strInvoice_ID;
                        string strCategory = Page.RouteData.Values["category"].ToString();
                        hfCategory.Value = strCategory;
                        InvoiceSelectAll(strInvoice_ID, strCategory);

                    }
                    else
                    {
                        Response.Redirect("~/search_invoice", false);
                    }

                }
            }
            catch (Exception ex)
            {
                // ExceptionMessage(ExceptionType.ERROR, "Error Found: " + ex.Message);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + ex.Message + "');});", true);

            }
        }
        protected void InvoiceSelectAll(string sInvoice_ID, string sCategory)
        {
            try
            {
                string sCustomer_ID = "", sAgent_ID = "";
                List<clsParams> oParams = new List<clsParams>();

                oParams.Add(new clsParams("sInvoice_ID", sInvoice_ID));
                oParams.Add(new clsParams("sCategory_Core", sCategory));
                DataTable dt = clsCommon.Invoice_SelectAll(oParams);
                if (dt != null && dt.Rows.Count > 0)
                {
                    lblInvoiceID.Text = dt.Rows[0]["Invoice_ID"].ToString();
                    lblParticular.Text = dt.Rows[0]["Particular"].ToString() == "" ? "-" : dt.Rows[0]["Particular"].ToString().ToUpper();
                    lblInvoiceDate.Text = dt.Rows[0]["Invoice_Date"].ToString();
                    lblCategory.Text = dt.Rows[0]["Category_Disp"].ToString();
                    lblAmount.Text = dt.Rows[0]["Amount"].ToString() == "" ? "-" : dt.Rows[0]["Amount"].ToString();
                    lblIsActive.Text = dt.Rows[0]["IsActive"].ToString() == "" ? "-" : dt.Rows[0]["IsActive"].ToString();
                    lblCreatedBY.Text = dt.Rows[0]["CreatedBY"].ToString() == "" ? "-" : dt.Rows[0]["CreatedBY"].ToString();
                    lblCreatedDT.Text = dt.Rows[0]["CreatedDT"].ToString() == "" ? "-" : dt.Rows[0]["CreatedDT"].ToString();
                    lblModifiedBY.Text = dt.Rows[0]["ModifiedBY"].ToString() == "" ? "-" : dt.Rows[0]["ModifiedBY"].ToString();
                    lblModifiedDT.Text = dt.Rows[0]["ModifiedDT"].ToString() == "" ? "-" : dt.Rows[0]["ModifiedDT"].ToString();
                    lblInvoiceStatus.Text = dt.Rows[0]["InvoiceStatus_Disp"].ToString() == "" ? "-" : dt.Rows[0]["InvoiceStatus_Disp"].ToString();
                    lblLastPaymentDate.Text = dt.Rows[0]["LastPayment_Date"].ToString() == "" ? "-" : dt.Rows[0]["LastPayment_Date"].ToString();
                    sCustomer_ID = dt.Rows[0]["Customer_ID"].ToString();
                    hfCustomer_ID.Value = sCustomer_ID;
                    sAgent_ID = dt.Rows[0]["Agent_ID"].ToString();

                    if (dt.Rows[0]["InvoiceStatus_Core"].ToString() == "IS_OPEN")
                        lblInvoiceStatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#00BDFD");
                    else if (dt.Rows[0]["InvoiceStatus_Core"].ToString() == "IS_OVERDUE")
                        lblInvoiceStatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#F44336");
                    else if (dt.Rows[0]["InvoiceStatus_Core"].ToString() == "IS_PAID_PARTIAL")
                        lblInvoiceStatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#4CAF50");
                    else if (dt.Rows[0]["InvoiceStatus_Core"].ToString() == "IS_CLOSE")
                        lblInvoiceStatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#757575");
                    else
                        lblInvoiceStatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#4CAF50");
                }
                else
                {
                    string sMessage = clsCommon.ReadXmlFile("PAW601");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + sMessage + "');});", true);

                    return;
                }

                dt.Clear();
                oParams.Clear();
                oParams.Add(new clsParams("sCustomer_ID", sCustomer_ID));
                dt = clsCommon.Customer_Select(sCustomer_ID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string Address = string.Concat(dt.Rows[0]["Address1"].ToString(), " ", dt.Rows[0]["Address2"].ToString(), " ", dt.Rows[0]["City"].ToString());
                    lblCustomerName.Text = string.Concat(dt.Rows[0]["FirstName"].ToString(), " ", dt.Rows[0]["MiddleName"].ToString(), " ", dt.Rows[0]["LastName"].ToString());
                    lblAgencyName.Text = dt.Rows[0]["AgencyName"].ToString()==""?"-": dt.Rows[0]["AgencyName"].ToString();
                    lblCustomerDOB.Text = dt.Rows[0]["DateOfBirth"].ToString() == "" ? "-" : dt.Rows[0]["DateOfBirth"].ToString();
                    lblCustomerAddress.Text = Address == "  " || Address == null||Address =="" ? "-" : Address;
                    lblCustomerPhoneNumberPrimary.Text = dt.Rows[0]["PhoneNumber1"].ToString() == "" ? "-" : dt.Rows[0]["PhoneNumber1"].ToString();
                    lblCustomerPhoneNumberSecondary.Text = dt.Rows[0]["PhoneNumber2"].ToString() == "" ? "-" : dt.Rows[0]["PhoneNumber2"].ToString();
                    lblCustomerEmail.Text = dt.Rows[0]["EMail"].ToString() == "" ? "-" : dt.Rows[0]["EMail"].ToString();
                    lblRemainingAmount.Text = dt.Rows[0]["RemainingAmount"].ToString() == "" ? "-" : dt.Rows[0]["RemainingAmount"].ToString();

                }
               
                oParams.Clear();

                oParams.Add(new clsParams("sInvoice_ID", sInvoice_ID));
                string sRemak = clsCommon.InvoiceRemak_Select(oParams);
                txtRemak.Text = sRemak;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void btnSubmitRemark_Click(object sender, EventArgs e)
        {
            try
            {
                List<clsParams> oParams = new List<clsParams>();
                string sInvoice_ID = hfInvoice_ID.Value;
                string sRemark = txtRemak.Text.Trim();
                oParams.Add(new clsParams("sInvoice_ID", sInvoice_ID));
                oParams.Add(new clsParams("sRemark", sRemark));
                string sMessage = clsCommon.Invoice_RemarkUpdate(oParams);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + sMessage + "');});", true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        protected void btnBack_ServerClick1(object sender, EventArgs e)
        {
            Response.Redirect(Application["Path"]+ "search_invoice", false);
        }
    }
}