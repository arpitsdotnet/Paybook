using Paybook.BusinessLayer;
using Paybook.BusinessLayer.Agency;
using Paybook.BusinessLayer.Setting;
using Paybook.ServiceLayer;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Paybook.WebUI.Invoice
{
    public partial class Create : System.Web.UI.Page
    {
        private readonly ILogger _logger;
        private readonly ICategoryProcessor _category;
        private readonly IAgencyProcessor _agency;

        public Create()
        {
            _logger = FileLogger.Instance;
            _category = new CategoryProcessor();
            _agency = new AgencyProcessor();
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
                    if (Page.RouteData.Values["MakePayment"] != null)
                    {
                        rbtnlSelectOperation.SelectedValue = Page.RouteData.Values["MakePayment"].ToString();
                    }
                    string[] sLoginUser = Session["LoggedInUser"].ToString().Split('/');
                    hfLogInUser.Value = sLoginUser[0];
                    hfLogInUser_ID.Value = sLoginUser[1];
                    txtDateFrom.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
                    txtDateTo.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    SubCategory_Select();
                    Agency_Select();
                    GetGst_Value();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + ex.Message + "');});", true);
            }
        }
        protected void GetGst_Value()
        {
            try
            {
                DataTable dt = _category.SubCategory_SelectGstValues(rbtn_GST_Value.SelectedValue);
                if (dt != null)
                {
                    hfGSTValue.Value = "";
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (hfGSTValue.Value != "")
                            hfGSTValue.Value += "/";
                        //GstType|GstName|Percentage like GST_STATE|IGST|18
                        hfGSTValue.Value += dr["Category_Core"].ToString() + "|" + dr["SubCategory_Core"].ToString() + "|" + dr["SubCategory_Disp"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        protected void SubCategory_Select()
        {
            try
            {
                // DefaultValue();
                CategoryModel[] categories = _category.SubCategories_Active_Select(CategoryTypes.Work);

                if (categories != null && categories.Length > 0)
                {
                    ddlCategories.Items.Insert(0, new ListItem("-Select Work Type-", "0"));
                    foreach (var category in categories)
                    {
                        ddlCategories.Items.Add(new ListItem(category.SubCategory_Disp, category.SubCategory_Core));
                    }
                    ddlCategories.SelectedIndex = 0;
                }
                else
                {
                    ddlCategories.Items.Insert(0, new ListItem("-No Work Type Found-", "0"));
                    ddlCategories.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        protected void Agency_Select()
        {
            try
            {
                DataTable dt = _agency.Agency_SelectName();
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlAgency.Items.Insert(0, new ListItem("-Select Agency-", "0"));
                    foreach (DataRow dr in dt.Rows)
                    {

                        ddlAgency.Items.Add(new ListItem(dr["AgencyName"].ToString(), dr["Agency_ID"].ToString()));

                    }
                    ddlAgency.Items.Insert(1, new ListItem("All Customer", "NONE"));
                    ddlAgency.SelectedIndex = 0;
                }
                else
                {
                    ddlAgency.Items.Insert(0, new ListItem("-No Agency Found-", "0"));
                    ddlAgency.Items.Insert(1, new ListItem("All Customer", "NONE"));
                    ddlAgency.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        protected void DefaultValue()
        {

            txtAmount.Text = "0";
            lblTotalAmount_WithGST.Text = "";
            txtRemainingAmount.Text = "";
            txtParticular.Text = "";
            txtInvoiceDate.Text = "";
            //  $("#ddlCustomers").val("0").trigger('change');
            //$("#ddlAgents").val("0").trigger('change');              

            lblGST_Value.Text = "";

            btnSubmit.Enabled = true;
        }
    }
}