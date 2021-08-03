using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Paybook.BusinessLayer;


namespace Paybook.WebUI
{
    public partial class invoice_add : System.Web.UI.Page
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
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + ex.Message + "');});", true);

            }
        }
        protected void GetGst_Value()
        {
            try
            {
                List<clsParams> oParams = new List<clsParams>();
                oParams.Add(new clsParams("sGST_Type", ""));
                DataTable dt = clsCommon.SubCategory_SelectGstValues(oParams);
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
                throw new Exception(ex.Message);
            }
        }
        protected void SubCategory_Select()
        {
            try
            {
                // DefaultValue();
                clsCategories[] arrCategory = clsCommon.SubCategories_Active_Select(clsCategory_Type._WorkType);

                if (arrCategory != null && arrCategory.Length > 0)
                {
                    ddlCategories.Items.Insert(0, new ListItem("-Select Work Type-", "0"));
                    for (int i = 0; i < arrCategory.Length; i++)
                    {
                        ddlCategories.Items.Add(new ListItem(arrCategory[i].SubCategory_Disp.ToString(), arrCategory[i].SubCategory_Core.ToString()));
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
                throw new Exception(ex.Message);
            }
        }
        protected void Agency_Select()
        {
            try
            {
                DataTable dt = clsCommon.Agency_SelectName();
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
                throw new Exception(ex.Message);
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