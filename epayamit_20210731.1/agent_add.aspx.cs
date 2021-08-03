using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Paybook.BusinessLayer;

namespace Paybook.WebUI
{
    public partial class agent_add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // txtAgentDOB.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            if (Session["LoggedInUser"] == null || Session["LoggedInUser"].ToString() == "")
            {
                Response.Redirect("login", false);
            }
            if (!IsPostBack)
            {
                try
                {
                    SubCategories_SelectPrefix();
                    SubCategories_SelectState();
                    if (Page.RouteData.Values["agent_id"] != null)
                    {
                        lblPageHeading.Text = "Edit Agent";
                        string sAgent_ID = Page.RouteData.Values["agent_id"].ToString();
                        //hfAgent_ID.Value = sAgent_ID.Replace('_', '/');
                        lblAgent_ID.Text =hfAgent_ID.Value;
                        EditAgent();
                    }
                    else
                    {
                        List<clsParams> oParams = new List<clsParams>();
                        oParams.Add(new clsParams("sType", "Agent"));
                        string sAgent_ID = clsCommon.GetLastSavedID(oParams);
                      
                        lblAgent_ID.Text = sAgent_ID;
                        lblPageHeading.Text = "Add New Agent";
                        hfAgent_ID.Value = "";
                    }

                }
                catch (Exception ex)
                {
                    ExceptionMessage(ExceptionType.ERROR, "Error Found: " + ex.Message);
                }

            }
        }
        protected Boolean Validation()
        {
            try
            {

                if (txtAgentPhoneNumber1.Text == "")
                {
                    ExceptionMessage(ExceptionType.WARNING, "WARNING: Enter Agent Phone Number");
                    return false;
                }

                else if (txtAgentFirstName.Text == "")
                {
                    ExceptionMessage(ExceptionType.WARNING, "WARNING: Enter Agent First Name");
                    return false;
                }
                else if (txtAgentPhoneNumber1.Text.Length < 10)
                {
                    ExceptionMessage(ExceptionType.WARNING, "WARNING: Enter Correct Phone Number");
                    return false;
                }
                return true;

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
                    ddlAgentState.Items.Insert(0, new ListItem("-Select State-", "0"));
                    for (int i = 0; i < arrCategory.Length; i++)
                    {
                        ddlAgentState.Items.Add(new ListItem(arrCategory[i].SubCategory_Disp.ToString(), arrCategory[i].SubCategory_Core.ToString()));
                    }
                    ddlAgentState.SelectedIndex = 0;
                }
                else
                {
                    ddlAgentState.Items.Insert(0, new ListItem("-No State Found-", "0"));
                    ddlAgentState.SelectedIndex = 0;
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
                    ddlAgentPrefix.Items.Insert(0, new ListItem("-Slect Prefix-", "0"));
                    for (int i = 0; i < arrCategory.Length; i++)
                    {
                        ddlAgentPrefix.Items.Add(new ListItem(arrCategory[i].SubCategory_Disp.ToString(), arrCategory[i].SubCategory_Core.ToString()));
                    }
                    ddlAgentPrefix.SelectedIndex = 0;
                }
                else
                {
                    ddlAgentPrefix.Items.Insert(0, new ListItem("-No Prefix Found-", "0"));
                    ddlAgentPrefix.SelectedIndex = 0;
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
                ddlAgentPrefix.SelectedIndex = 0;
                txtAgentFirstName.Text = "";
                txtAgentMiddleName.Text = "";
                txtAgentLastName.Text = "";
                txtAgentDOB.Text = "";
                txtAgentAddress1.Text = "";
                txtAgentAddress2.Text = "";
                txtAgentCity.Text = "";
                ddlAgentState.SelectedIndex = 0;
                txtAgentCountry.Text = "";
                txtAgentPhoneNumber1.Text = "";
                txtAgentPhoneNumber2.Text = "";
                txtAgentEmail.Text = "";
                txtAgentDOB.Text = DateTime.Now.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected void EditAgent()
        {
            try
            {
                DataTable dtAgent = clsCommon.Agent_Select(hfAgent_ID.Value);
                if (dtAgent.Rows.Count > 0)
                {

                    string sActive = Convert.ToInt32(dtAgent.Rows[0]["IsActive"]) == 1 ? "Active" : "Inactive";
                    hfIsActive.Value = dtAgent.Rows[0]["IsActive"].ToString();

                    // PageActiveCall();

                    string sModifiedDT = dtAgent.Rows[0]["ModifiedDT"].ToString() == "" ? "-" : dtAgent.Rows[0]["ModifiedDT"].ToString();
                    string sModifiedBY = dtAgent.Rows[0]["ModifiedBY"].ToString() == "" ? "-" : dtAgent.Rows[0]["ModifiedBY"].ToString();

                    ddlAgentPrefix.SelectedValue = dtAgent.Rows[0]["Prefix_Core"].ToString();
                    txtAgentFirstName.Text = dtAgent.Rows[0]["FirstName"].ToString();
                    txtAgentMiddleName.Text = dtAgent.Rows[0]["MiddleName"].ToString();
                    txtAgentLastName.Text = dtAgent.Rows[0]["LastName"].ToString();
                    txtAgentDOB.Text = dtAgent.Rows[0]["DateOfBirth"].ToString();
                    txtAgentAddress1.Text = dtAgent.Rows[0]["Address1"].ToString();
                    txtAgentAddress2.Text = dtAgent.Rows[0]["Address2"].ToString();
                    txtAgentCity.Text = dtAgent.Rows[0]["City"].ToString();
                    ddlAgentState.SelectedValue = dtAgent.Rows[0]["State_Core"].ToString();
                    txtAgentCountry.Text = dtAgent.Rows[0]["Country_Core"].ToString();
                    txtAgentPhoneNumber1.Text = dtAgent.Rows[0]["PhoneNumber1"].ToString();
                    txtAgentPhoneNumber2.Text = dtAgent.Rows[0]["PhoneNumber2"].ToString();
                    txtAgentEmail.Text = dtAgent.Rows[0]["EMail"].ToString();
                    rbtnGender.SelectedValue = dtAgent.Rows[0]["Gender"].ToString();

                }
            }
            catch (Exception ex)
            {
                ExceptionMessage(ExceptionType.ERROR, "Error Found:" + ex.Message);
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string sMessage = "";
                if (Validation())
                {
                    string sDOB = Convert.ToDateTime(txtAgentDOB.Text.Trim()).ToString("yyyy-MM-dd");
                    string sGender = rbtnGender.SelectedValue.ToString();
                    List<clsParams> oParams = new List<clsParams>();
                    clsAgents oAgent = new clsAgents();
                    oAgent.CreatedBY = hfCreatedBy.Value.Trim();
                    oAgent.Agent_ID = lblAgent_ID.Text.Trim();
                    oAgent.Prefix_Core = ddlAgentPrefix.SelectedValue.ToString();
                    oAgent.FirstName = txtAgentFirstName.Text.Trim();
                    oAgent.MiddleName = txtAgentMiddleName.Text.Trim();
                    oAgent.LastName = txtAgentLastName.Text.Trim();
                    oAgent.DateOfBirth = sDOB;
                    oAgent.Address1 = txtAgentAddress1.Text.Trim();
                    oAgent.Address2 = txtAgentAddress2.Text.Trim();
                    oAgent.City = txtAgentCity.Text.Trim();
                    oAgent.State_Core = ddlAgentState.SelectedValue.ToString();
                    oAgent.Country_Core = txtAgentCountry.Text.Trim();
                    oAgent.PhoneNumber1 = txtAgentPhoneNumber1.Text.Trim();
                    oAgent.PhoneNumber2 = txtAgentPhoneNumber2.Text.Trim();
                    oAgent.EMail = txtAgentEmail.Text.Trim();
                    oAgent.Gender = sGender;
                
                    oParams.Add(new clsParams("sAgent_ID", oAgent.Agent_ID));
                    oParams.Add(new clsParams("sAgentPrefix_Core", oAgent.Prefix_Core));
                    oParams.Add(new clsParams("sAgentFirstName", oAgent.FirstName));
                    oParams.Add(new clsParams("sAgentMiddleName", oAgent.MiddleName));
                    oParams.Add(new clsParams("sAgentLastName", oAgent.LastName));
                    oParams.Add(new clsParams("sAgentDoB", oAgent.DateOfBirth));
                    oParams.Add(new clsParams("sAgentAddress1", oAgent.Address1));
                    oParams.Add(new clsParams("sAgentAddress2", oAgent.Address2));
                    oParams.Add(new clsParams("sAgentCity", oAgent.City));
                    oParams.Add(new clsParams("sAgentState_Core", oAgent.State_Core));
                    oParams.Add(new clsParams("sAgentCountry", oAgent.Country_Core));
                    oParams.Add(new clsParams("sAgentPhoneNumber1", oAgent.PhoneNumber1));
                    oParams.Add(new clsParams("sAgentPhoneNumber2", oAgent.PhoneNumber2));
                    oParams.Add(new clsParams("sAgentEmail", oAgent.EMail));
                    oParams.Add(new clsParams("sGender", oAgent.Gender));

                    if (hfAgent_ID.Value == "")
                    {
                        oParams.Add(new clsParams("sCreatedBY", oAgent.CreatedBY));
                        //Insert New Agent  
                        clsCommon.Agent_Insert(oParams);
                        //update LastSavedId                
                        clsCommon.LastSavedID_Update(oAgent.Agent_ID, "Agent");
                        SetDefault();
                        sMessage = clsCommon.ReadXmlFile("AGS203");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + sMessage + "');});", true);


                    }
                    else
                    {
                        oParams.Add(new clsParams("sModifiedBY", oAgent.CreatedBY));
                        ////Update Customer                           
                        clsCommon.Agent_Update(oParams);
                        sMessage = clsCommon.ReadXmlFile("AGS204");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + sMessage + "');});", true);

                    }
                }
            }

            catch (Exception ex)
            {
                idLabelError.InnerHtml = ex.Message;
                idLabelError.Style.Add("display", "block");
            }
        }
        protected void ExceptionMessage(ExceptionType sType, string sMessage)
        {
            try
            {
                idLabelError.InnerHtml = sMessage;
                idLabelError.Style.Add("display", "block");
                if (sType.ToString() == "WARNING")
                {
                    idLabelError.Attributes.Add("class", "fwt-container fwt-padding-16 fwt-pale-yellow fwt-border fwt-border-yellow");
                }
                else if (sType.ToString() == "ERROR")
                {
                    idLabelError.Attributes.Add("class", "fwt-container fwt-padding-16 fwt-pale-red fwt-border fwt-border-red");
                }
                else if (sType.ToString() == "SUCCESS")
                {
                    idLabelError.Attributes.Add("class", "fwt-container fwt-padding-16 fwt-pale-green fwt-border fwt-border-green");
                }
                else if (sType.ToString() == "INFO")
                {
                    idLabelError.Attributes.Add("class", "fwt-container fwt-padding-16 fwt-pale-blue fwt-border fwt-border-blue");
                }
            }

            catch (Exception ex)
            {
                idLabelError.InnerHtml = ex.Message;
                idLabelError.Style.Add("display", "block");
            }
        }
    }
}