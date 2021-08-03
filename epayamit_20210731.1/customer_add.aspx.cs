using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using Paybook.BusinessLayer;

namespace Paybook.WebUI
{
    public partial class customer_add : System.Web.UI.Page
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
                    SubCategories_SelectPrefix();
                    AgencyName_Select();                   
                    SubCategories_SelectState();
                    if (Page.RouteData.Values["customer_id"] != null)
                    {
                        lblPageHeading.Text = "Edit Customer";
                        hfCustomer_ID.Value = Page.RouteData.Values["customer_id"].ToString();
                        // hfCustomer_ID.Value = sCustomer_ID.Replace('_', '/');
                        lblCustomer_ID.Text = hfCustomer_ID.Value;
                        EditCustomer();
                    }
                    else
                    {
                        List<clsParams> oParams = new List<clsParams>();
                        oParams.Add(new clsParams("sType", "Customer"));
                        string sCustomer_ID = clsCommon.GetLastSavedID(oParams);
                        lblCustomer_ID.Text = sCustomer_ID;
                        lblPageHeading.Text = "Add New Customer";
                        hfCustomer_ID.Value = "";
                    }

                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + ex.Message + "');});", true);

            }
        }
        protected void EditCustomer()
        {

            try
            {
                DataTable dtCustomer = clsCommon.Customer_Select(hfCustomer_ID.Value);
                if (dtCustomer.Rows.Count > 0)
                {

                    string sActive = Convert.ToInt32(dtCustomer.Rows[0]["IsActive"]) == 1 ? "Active" : "Inactive";
                    hfIsActive.Value = dtCustomer.Rows[0]["IsActive"].ToString();

                    // PageActiveCall();

                    string sModifiedDT = dtCustomer.Rows[0]["ModifiedDT"].ToString() == "" ? "-" : dtCustomer.Rows[0]["ModifiedDT"].ToString();
                    string sModifiedBY = dtCustomer.Rows[0]["ModifiedBY"].ToString() == "" ? "-" : dtCustomer.Rows[0]["ModifiedBY"].ToString();

                    ddlCustomerPrefix.SelectedValue = dtCustomer.Rows[0]["Prefix_Core"].ToString();
                    txtCustomerFirstName.Text = dtCustomer.Rows[0]["FirstName"].ToString();
                    txtCustomerMiddleName.Text = dtCustomer.Rows[0]["MiddleName"].ToString();
                    txtCustomerLastName.Text = dtCustomer.Rows[0]["LastName"].ToString();
                    txtCustomerDOB.Text = Convert.ToDateTime(dtCustomer.Rows[0]["DateOfBirth"]).ToString("dd-MMM-yyyy");
                    txtCustomerAddress1.Text = dtCustomer.Rows[0]["Address1"].ToString();
                    txtCustomerAddress2.Text = dtCustomer.Rows[0]["Address2"].ToString();
                    txtCustomerCity.Text = dtCustomer.Rows[0]["City"].ToString();
                    ddlCustomerState.SelectedValue = dtCustomer.Rows[0]["State_Core"].ToString();
                    txtCustomerCountry.Text = dtCustomer.Rows[0]["Country_Core"].ToString();
                    txtCustomerPhoneNumber1.Text = dtCustomer.Rows[0]["PhoneNumber1"].ToString();
                     txtCustomerPhoneNumber2.Text = dtCustomer.Rows[0]["PhoneNumber2"].ToString();
                    txtCustomerEmail.Text = dtCustomer.Rows[0]["EMail"].ToString();

                    rbtnCustomer_Type.SelectedValue = dtCustomer.Rows[0]["Customer_Type"].ToString();

                    if (dtCustomer.Rows[0]["Customer_Type"].ToString() == "Customer")
                    {
                        ddlCustomerPrefix.Enabled = true;
                    }
                    else
                        ddlCustomerPrefix.Enabled = false;
                    rbtnGender.SelectedValue = dtCustomer.Rows[0]["Gender"].ToString();
                    ddlAgencyName.SelectedValue = dtCustomer.Rows[0]["Agency_ID"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected void AgencyName_Select()
        {
            try
            {
                DataTable dt = clsCommon.Agency_SelectName();
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlAgencyName.Items.Insert(0, new ListItem("-Select Agency-", "0"));
                    foreach (DataRow dr in dt.Rows)
                    {

                        ddlAgencyName.Items.Add(new ListItem(dr["AgencyName"].ToString(), dr["Agency_ID"].ToString()));

                    }
                    ddlAgencyName.SelectedIndex = 0;
                }
                else
                {
                    ddlAgencyName.Items.Insert(0, new ListItem("-No Agency Found-", "0"));
                    ddlAgencyName.SelectedIndex = 0;
                }
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
                    ddlCustomerState.Items.Insert(0, new ListItem("-Select State-", "0"));
                    for (int i = 0; i < arrCategory.Length; i++)
                    {

                        ddlCustomerState.Items.Add(new ListItem(arrCategory[i].SubCategory_Disp.ToString(), arrCategory[i].SubCategory_Core.ToString()));

                    }
                    ddlCustomerState.SelectedIndex = 0;
                }
                else
                {
                    ddlCustomerState.Items.Insert(0, new ListItem("-No State Found-", "0"));
                    ddlCustomerState.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected void SubCategories_SelectPrefix()
        {
            try
            {

                clsCategories[] arrCategory = clsCommon.SubCategories_Active_Select(clsCategory_Type._Prefix);

                if (arrCategory != null && arrCategory.Length > 0)
                {
                    ddlCustomerPrefix.Items.Insert(0, new ListItem("-Select Prefix-", "0"));
                    for (int i = 0; i < arrCategory.Length; i++)
                    {

                        ddlCustomerPrefix.Items.Add(new ListItem(arrCategory[i].SubCategory_Disp.ToString(), arrCategory[i].SubCategory_Core.ToString()));

                    }
                    ddlCustomerPrefix.SelectedIndex = 0;
                }
                else
                {
                    ddlCustomerPrefix.Items.Insert(0, new ListItem("-No Prefix Found-", "0"));
                    ddlCustomerPrefix.SelectedIndex = 0;
                }
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
                ddlCustomerPrefix.SelectedIndex = 0;
                txtCustomerFirstName.Text = "";
                txtCustomerMiddleName.Text = "";
                txtCustomerLastName.Text = "";
                txtCustomerDOB.Text = "";
                txtCustomerAddress1.Text = "";
                txtCustomerAddress2.Text = "";
                txtCustomerCity.Text = "";
                ddlCustomerState.SelectedIndex = 0;
                txtCustomerCountry.Text = "INDIA";
                txtCustomerPhoneNumber1.Text = "";
                txtCustomerPhoneNumber2.Text = "";
                txtCustomerEmail.Text = "";
                txtCustomerDOB.Text = DateTime.Now.ToString();
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
                if (txtCustomerPhoneNumber1.Text == "" || txtCustomerPhoneNumber1.Text.Length > 10)
                {
                    sMessage = clsCommon.ReadXmlFile("BSW010");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + sMessage + "');});", true);
                    return false;
                }

                else if (txtCustomerFirstName.Text == "")
                {
                    sMessage = clsCommon.ReadXmlFile("CU101");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + sMessage + "');});", true);
                    return false;
                }
                else
                {
                    if (hfCustomer_ID.Value == "")
                    {
                        string sAgencyID = "0";
                        List<clsParams> oParams = new List<clsParams>();
                        if (ddlAgencyName.SelectedIndex != 0)
                            sAgencyID = ddlAgencyName.SelectedValue.ToString();
                        oParams.Add(new clsParams("sFirstName", txtCustomerFirstName.Text.Trim()));
                        oParams.Add(new clsParams("sAgencyID", sAgencyID));
                        oParams.Add(new clsParams("sPhoneNumber1", txtCustomerPhoneNumber1.Text.Trim()));
                        sMessage = clsCommon.Customer_IsExist(oParams);
                        if (sMessage != "")
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + sMessage + "');});", true);
                            return false;
                        }
                    }

                }
                return true;

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
                    string sCustomer_Type = rbtnCustomer_Type.SelectedValue.ToString();
                    string sDOB = Convert.ToDateTime(txtCustomerDOB.Text.Trim()).ToString("yyyy-MM-dd HH:mm:ss");

                    var sGender = rbtnGender.SelectedValue.ToString();
                    List<clsParams> oParams = new List<clsParams>();
                    clsCustomers oCustomer = new clsCustomers();
                    oCustomer.CreatedBY = hfLogInUser.Value.Trim();
                    oCustomer.Customer_ID = lblCustomer_ID.Text.Trim();
                    oCustomer.Prefix_Core = ddlCustomerPrefix.SelectedValue.ToString();
                    oCustomer.FirstName = txtCustomerFirstName.Text.Trim();
                    oCustomer.MiddleName = txtCustomerMiddleName.Text.Trim();
                    oCustomer.LastName = txtCustomerLastName.Text.Trim();
                    oCustomer.DateOfBirth = sDOB;
                    oCustomer.Address1 = txtCustomerAddress1.Text.Trim();
                    oCustomer.Address2 = txtCustomerAddress2.Text.Trim();
                    oCustomer.City = txtCustomerCity.Text.Trim();
                    oCustomer.State_Core = ddlCustomerState.SelectedValue.ToString();
                    oCustomer.Country_Core = txtCustomerCountry.Text.Trim();
                    oCustomer.PhoneNumber1 = txtCustomerPhoneNumber1.Text.Trim();
                    oCustomer.PhoneNumber2 = txtCustomerPhoneNumber2.Text.Trim();
                    oCustomer.EMail = txtCustomerEmail.Text.Trim();
                    oCustomer.Customer_Type = sCustomer_Type;
                    oCustomer.Gender = sGender;
                    oCustomer.Agency_ID = ddlAgencyName.SelectedValue.ToString();

                    oParams.Add(new clsParams("sCustomer_ID", oCustomer.Customer_ID));
                    oParams.Add(new clsParams("sCustomerPrefix_Core", oCustomer.Prefix_Core));
                    oParams.Add(new clsParams("sCustomerFirstName", oCustomer.FirstName));
                    oParams.Add(new clsParams("sCustomerMiddleName", oCustomer.MiddleName));
                    oParams.Add(new clsParams("sCustomerLastName", oCustomer.LastName));
                    oParams.Add(new clsParams("sCustomerDoB", oCustomer.DateOfBirth));
                    oParams.Add(new clsParams("sCustomerAddress1", oCustomer.Address1));
                    oParams.Add(new clsParams("sCustomerAddress2", oCustomer.Address2));
                    oParams.Add(new clsParams("sCustomerCity", oCustomer.City));
                    oParams.Add(new clsParams("sCustomerState_Core", oCustomer.State_Core));
                    oParams.Add(new clsParams("sCustomerCountry", oCustomer.Country_Core));
                    oParams.Add(new clsParams("sCustomerPhoneNumber1", oCustomer.PhoneNumber1));
                    oParams.Add(new clsParams("sCustomerPhoneNumber2", oCustomer.PhoneNumber2));
                        oParams.Add(new clsParams("sCustomerEmail", oCustomer.EMail));
                    oParams.Add(new clsParams("sCustomer_Type", oCustomer.Customer_Type));
                    oParams.Add(new clsParams("sGender", oCustomer.Gender));
                    oParams.Add(new clsParams("sAgency_ID", oCustomer.Agency_ID));
                    if (hfCustomer_ID.Value == "")
                    {
                        oParams.Add(new clsParams("sCreatedBY", oCustomer.CreatedBY));
                      
                        
                        //Insert New Customer  
                        sMessage = clsCommon.Customer_Insert(oParams);
                        //update LastSavedId                
                        clsCommon.LastSavedID_Update(oCustomer.Customer_ID, "Customer");
                        SetDefault();
                        //show new customer id
                        oParams.Clear();
                        oParams.Add(new clsParams("sType", "Customer"));
                        string sCustomer_ID = clsCommon.GetLastSavedID(oParams);
                        lblCustomer_ID.Text = sCustomer_ID;

                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + sMessage + "');});", true);

                    }
                    else
                    {
                        oParams.Add(new clsParams("sModifiedBY", oCustomer.CreatedBY));                     
                      
                        ////Update Customer                           
                        sMessage = clsCommon.Customer_Update(oParams);
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + sMessage + "');});", true);

                    }
                }
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
        //protected void ExceptionMessage(ExceptionType sType, string sMessage)
        //{
        //    try
        //    {
        //        idLabelError.InnerHtml = sMessage;
        //        idLabelError.Style.Add("display", "block");
        //        if (sType.ToString() == "WARNING")
        //        {
        //            idLabelError.Attributes.Add("class", "fwt-container fwt-padding-16 fwt-pale-yellow fwt-border fwt-border-yellow");
        //        }
        //        else if (sType.ToString() == "ERROR")
        //        {
        //            idLabelError.Attributes.Add("class", "fwt-container fwt-padding-16 fwt-pale-red fwt-border fwt-border-red");
        //        }
        //        else if (sType.ToString() == "SUCCESS")
        //        {
        //            idLabelError.Attributes.Add("class", "fwt-container fwt-padding-16 fwt-pale-green fwt-border fwt-border-green");
        //        }
        //        else if (sType.ToString() == "INFO")
        //        {
        //            idLabelError.Attributes.Add("class", "fwt-container fwt-padding-16 fwt-pale-blue fwt-border fwt-border-blue");
        //        }
        //        //idLabelError.InnerHtml= "<script type=\"text/javascript\">$('#idLabelError').delay('4000')</script>";
        //    }

        //    catch (Exception ex)
        //    {
        //        idLabelError.InnerHtml = ex.Message;
        //        idLabelError.Style.Add("display", "block");
        //    }
        //}
    }
}