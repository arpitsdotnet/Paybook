using Paybook.BusinessLayer;
using Paybook.BusinessLayer.Client;
using Paybook.BusinessLayer.Common;
using Paybook.BusinessLayer.Invoice;
using Paybook.BusinessLayer.Setting;
using Paybook.ServiceLayer.Constants;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;

namespace Paybook.WebUI.Invoice
{
    public partial class Particular : System.Web.UI.Page
    {
        private readonly ILogger _logger;
        private readonly IInvoiceProcessor _invoice;
        private readonly IClientProcessor _client;
        private readonly IRemarkProcessor _remark;

        public Particular(ILogger logger, IInvoiceProcessor invoice, IClientProcessor client, IRemarkProcessor remark)
        {
            _logger = logger;
            _invoice = invoice;
            _client = client;
            _remark = remark;
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
                    if (Page.RouteData.Values["id"].ToString() != null)
                    {
                        string invoiceId = Page.RouteData.Values["id"].ToString();
                        hfInvoice_ID.Value = invoiceId;
                        InvoiceSelectAll(invoiceId);

                    }
                    else
                    {
                        Response.Redirect("invoice", false);
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + XmlProcessor.ReadXmlFile("OTW901") + "');});", true);
            }
        }
        protected void btnSubmitRemark_Click(object sender, EventArgs e)
        {
            try
            {
                string sInvoice_ID = hfInvoice_ID.Value;
                string sRemark = txtRemak.Text.Trim();
                string message = _remark.Invoice_RemarkUpdate(sInvoice_ID, sRemark);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + message + "');});", true);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + XmlProcessor.ReadXmlFile("OTW901") + "');});", true);
            }

        }
        protected void btnBack_ServerClick1(object sender, EventArgs e)
        {
            Response.Redirect(Application["Path"] + "invoice", false);
        }

        protected void InvoiceSelectAll(string invoiceId)
        {
            try
            {
                string sCustomer_ID = "", sAgent_ID = "";

                DataTable dt = _invoice.GetById(invoiceId);
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

                    string invoiceStatusCore = dt.Rows[0]["InvoiceStatus_Core"].ToString();
                    if (invoiceStatusCore == InvoiceStatusConst.Open)
                        lblInvoiceStatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#00BDFD");
                    else if (invoiceStatusCore == InvoiceStatusConst.Overdue)
                        lblInvoiceStatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#F44336");
                    else if (invoiceStatusCore == InvoiceStatusConst.PaidPartial)
                        lblInvoiceStatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#4CAF50");
                    else if (invoiceStatusCore == InvoiceStatusConst.Close)
                        lblInvoiceStatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#757575");
                    else
                        lblInvoiceStatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#4CAF50");
                }
                else
                {
                    string sMessage = XmlProcessor.ReadXmlFile("PAW601");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + sMessage + "');});", true);

                    return;
                }

                dt.Clear();
                dt = _client.GetByClientID(sCustomer_ID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string Address = string.Concat(dt.Rows[0]["Address1"].ToString(), " ", dt.Rows[0]["Address2"].ToString(), " ", dt.Rows[0]["City"].ToString());
                    lblCustomerName.Text = string.Concat(dt.Rows[0]["FirstName"].ToString(), " ", dt.Rows[0]["MiddleName"].ToString(), " ", dt.Rows[0]["LastName"].ToString());
                    lblAgencyName.Text = dt.Rows[0]["AgencyName"].ToString() == "" ? "-" : dt.Rows[0]["AgencyName"].ToString();
                    lblCustomerDOB.Text = dt.Rows[0]["DateOfBirth"].ToString() == "" ? "-" : dt.Rows[0]["DateOfBirth"].ToString();
                    lblCustomerAddress.Text = Address == "  " || Address == null || Address == "" ? "-" : Address;
                    lblCustomerPhoneNumberPrimary.Text = dt.Rows[0]["PhoneNumber1"].ToString() == "" ? "-" : dt.Rows[0]["PhoneNumber1"].ToString();
                    lblCustomerPhoneNumberSecondary.Text = dt.Rows[0]["PhoneNumber2"].ToString() == "" ? "-" : dt.Rows[0]["PhoneNumber2"].ToString();
                    lblCustomerEmail.Text = dt.Rows[0]["EMail"].ToString() == "" ? "-" : dt.Rows[0]["EMail"].ToString();
                    lblRemainingAmount.Text = dt.Rows[0]["RemainingAmount"].ToString() == "" ? "-" : dt.Rows[0]["RemainingAmount"].ToString();

                }

                txtRemak.Text = _remark.InvoiceRemark_Select(invoiceId); ;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }

    }
}