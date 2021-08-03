using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Paybook.BusinessLayer;

namespace Paybook.WebUI
{
    public partial class ageny_add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // txtCustomerDOB.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            try
            {
                if (Session["LoggedInUser"] == null || Session["LoggedInUser"].ToString() == "")
                {
                    Response.Redirect("login", false);
                }
                if (!IsPostBack)
                {
                    string[] sLoginUser = Session["LoggedInUser"].ToString().Split('/');
                    hfLogInUser.Value = sLoginUser[0];
                    SubCategories_SelectState();
                    if (Page.RouteData.Values["agency_id"] != null)
                    {
                        lblPageHeading.Text = "Edit Agency";
                        hfAgency_ID.Value = Page.RouteData.Values["agency_id"].ToString();
                        // hfCustomer_ID.Value = sCustomer_ID.Replace('_', '/');
                        lblAgency_ID.Text = hfAgency_ID.Value;
                        EditAgency();
                    }
                    else
                    {
                        List<clsParams> oParams = new List<clsParams>();
                        oParams.Add(new clsParams("sType", "Agency"));
                        string sAgency_ID = clsCommon.GetLastSavedID(oParams);
                        lblAgency_ID.Text = sAgency_ID;
                        lblPageHeading.Text = "Add New Agency";
                        hfAgency_ID.Value = "";
                    }

                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + ex.Message + "');});", true);

            }
        }
        protected void EditAgency()
        {

            try
            {
                DataTable dtAgency = clsCommon.Agency_Select(hfAgency_ID.Value);
                if (dtAgency.Rows.Count > 0)
                {

                    txtAgencyName.Text = dtAgency.Rows[0]["AgencyName"].ToString();
                    txtAgencyAddress1.Text = dtAgency.Rows[0]["Address1"].ToString();
                    txtAgencyAddress2.Text = dtAgency.Rows[0]["Address2"].ToString();
                    txtAgencyCity.Text = dtAgency.Rows[0]["City"].ToString();
                    ddlAgencyState.SelectedValue = dtAgency.Rows[0]["State_Core"].ToString();
                    txtAgencyCountry.Text = dtAgency.Rows[0]["Country_Core"].ToString();
                    txtAgencyPhoneNumber1.Text = dtAgency.Rows[0]["PhoneNumber1"].ToString();
                    txtAgencyPhoneNumber2.Text = dtAgency.Rows[0]["PhoneNumber2"].ToString();
                    txtAgencyEmail.Text = dtAgency.Rows[0]["EMail"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected Boolean Validation()
        {
            string sMessage = "";
            try
            {
                if (txtAgencyPhoneNumber1.Text == "" || txtAgencyPhoneNumber1.Text.Length>10)
                {
                    sMessage = clsCommon.ReadXmlFile("BSW010");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + sMessage + "');});", true);
                    return false;
                }

                else if (txtAgencyName.Text == "")
                {
                    sMessage = clsCommon.ReadXmlFile("AGE101");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + sMessage + "');});", true);
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        protected void SetDefault()
        {
            try
            {

                txtAgencyName.Text = "";
                txtAgencyAddress1.Text = "";
                txtAgencyAddress2.Text = "";
                txtAgencyCity.Text = "";
                ddlAgencyState.SelectedIndex = 0;
                txtAgencyCountry.Text = "INDIA";
                txtAgencyPhoneNumber1.Text = "";
                txtAgencyPhoneNumber2.Text = "";
                txtAgencyEmail.Text = "";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected void SubCategories_SelectState()
        {
            try
            {

                clsCategories[] arrCategory = clsCommon.SubCategories_Active_Select(clsCategory_Type._State);
                if (arrCategory != null && arrCategory.Length > 0)
                {
                    ddlAgencyState.Items.Insert(0, new ListItem("-Select State-", "0"));
                    for (int i = 0; i < arrCategory.Length; i++)
                    {

                        ddlAgencyState.Items.Add(new ListItem(arrCategory[i].SubCategory_Disp.ToString(), arrCategory[i].SubCategory_Core.ToString()));

                    }
                    ddlAgencyState.SelectedIndex = 0;
                }
                else
                {
                    ddlAgencyState.Items.Insert(0, new ListItem("-No State Found-", "0"));
                    ddlAgencyState.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string sMessage = "";
                if (Validation())
                {
                    List<clsParams> oParams = new List<clsParams>();
                    clsAgency oAgency = new clsAgency();
                    oAgency.CreatedBY = hfLogInUser.Value.Trim();
                    oAgency.Agency_ID = lblAgency_ID.Text.Trim();
                    oAgency.AgencyName = txtAgencyName.Text.Trim();
                    oAgency.Address1 = txtAgencyAddress1.Text.Trim();
                    oAgency.Address2 = txtAgencyAddress2.Text.Trim();
                    oAgency.City = txtAgencyCity.Text.Trim();
                    oAgency.State_Core = ddlAgencyState.SelectedValue.ToString();
                    oAgency.Country_Core = txtAgencyCountry.Text.Trim();
                    oAgency.PhoneNumber1 = txtAgencyPhoneNumber1.Text.Trim();
                    oAgency.PhoneNumber2 = txtAgencyPhoneNumber2.Text.Trim();
                    oAgency.EMail = txtAgencyEmail.Text.Trim();
                    if (hfAgency_ID.Value == "")
                    {
                        oParams.Add(new clsParams("sCreatedBY", oAgency.CreatedBY));
                        oParams.Add(new clsParams("sAgency_ID", oAgency.Agency_ID));
                        oParams.Add(new clsParams("sAgencyName", oAgency.AgencyName));
                        oParams.Add(new clsParams("sAgencyAddress1", oAgency.Address1));
                        oParams.Add(new clsParams("sAgencyAddress2", oAgency.Address2));
                        oParams.Add(new clsParams("sAgencyCity", oAgency.City));
                        oParams.Add(new clsParams("sAgencyState_Core", oAgency.State_Core));
                        oParams.Add(new clsParams("sAgencyCountry", oAgency.Country_Core));
                        oParams.Add(new clsParams("sAgencyPhoneNumber1", oAgency.PhoneNumber1));
                        oParams.Add(new clsParams("sAgencyPhoneNumber2", oAgency.PhoneNumber2));
                        oParams.Add(new clsParams("sAgencyEmail", oAgency.EMail));
                        //Insert New Agency  
                        sMessage = clsCommon.Agency_Insert(oParams);
                        //update LastSavedId                
                        clsCommon.LastSavedID_Update(oAgency.Agency_ID, "Agency");
                        SetDefault();
                        //show new Agency id
                        oParams.Clear();
                        oParams.Add(new clsParams("sType", "Agency"));
                        string sAgency_ID = clsCommon.GetLastSavedID(oParams);
                        lblAgency_ID.Text = sAgency_ID;
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + sMessage + "');});", true);

                    }
                    else
                    {
                        oParams.Add(new clsParams("sModifiedBY", oAgency.CreatedBY));
                        oParams.Add(new clsParams("sAgency_ID", oAgency.Agency_ID));
                        oParams.Add(new clsParams("sAgencyName", oAgency.AgencyName));
                        oParams.Add(new clsParams("sAgencyAddress1", oAgency.Address1));
                        oParams.Add(new clsParams("sAgencyAddress2", oAgency.Address2));
                        oParams.Add(new clsParams("sAgencyCity", oAgency.City));
                        oParams.Add(new clsParams("sAgencyState_Core", oAgency.State_Core));
                        oParams.Add(new clsParams("sAgencyCountry", oAgency.Country_Core));
                        oParams.Add(new clsParams("sAgencyPhoneNumber1", oAgency.PhoneNumber1));
                        oParams.Add(new clsParams("sAgencyPhoneNumber2", oAgency.PhoneNumber2));
                        oParams.Add(new clsParams("sAgencyEmail", oAgency.EMail));
                        ////Update Agency                           
                        sMessage = clsCommon.Agency_Update(oParams);

                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + sMessage + "');});", true);

                    }
                }
            }

            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + ex.Message + "');});", true);

            }
        }

        protected void btnCustomerCreate_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Application["Path"] + "customer", false);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + ex.Message + "');});", true);

            }
        }

        protected void btnBack_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect(Application["Path"] + "search_customer", false);
        }
    }
}