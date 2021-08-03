using Paybook.BusinessLayer;
using Paybook.BusinessLayer.Agent;
using Paybook.BusinessLayer.Common;
using Paybook.BusinessLayer.Setting;
using Paybook.ServiceLayer;
using Paybook.ServiceLayer.Enums;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Paybook.WebUI.Agent
{
    public partial class Create : System.Web.UI.Page
    {
        private readonly ILogger _logger;
        private readonly ILastSavedIdProcessor _lastSavedID;
        private readonly ICategoryProcessor _category;
        private readonly IAgentProcessor _agent;

        public Create()
        {
            _logger = FileLogger.Instance;
            _lastSavedID = new LastSavedIdProcessor();
            _category = new CategoryProcessor();
            _agent = new AgentProcessor();

        }
        protected void Page_Load(object sender, EventArgs e)
        {

            // txtAgentDOB.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            if (Session["LoggedInUser"] == null || Session["LoggedInUser"].ToString() == "")
            {
                Response.Redirect("~/identity/login", false);
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
                        lblAgent_ID.Text = hfAgent_ID.Value;
                        EditAgent();
                    }
                    else
                    {
                        string sAgent_ID = _lastSavedID.GetLastSavedID(LastIdTypes.Agent);

                        lblAgent_ID.Text = sAgent_ID;
                        lblPageHeading.Text = "Add New Agent";
                        hfAgent_ID.Value = "";
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError(_logger.MethodName, ex);

                    ExceptionMessage(ExceptionType.ERROR, XmlProcessor.ReadXmlFile("OTW901"));
                }

            }
        }
        protected Boolean Validation()
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
        protected void SubCategories_SelectState()
        {
            try
            {
                CategoryModel[] categories = _category.SubCategories_Active_Select(CategoryTypes.State);

                if (categories != null && categories.Length > 0)
                {
                    ddlAgentState.Items.Insert(0, new ListItem("-Select State-", "0"));
                    foreach (var category in categories)
                    {
                        ddlAgentState.Items.Add(new ListItem(category.SubCategory_Disp, category.SubCategory_Core));
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
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        protected void SubCategories_SelectPrefix()
        {
            try
            {
                CategoryModel[] categories = _category.SubCategories_Active_Select(CategoryTypes.Prefix);

                if (categories != null && categories.Length > 0)
                {
                    ddlAgentPrefix.Items.Insert(0, new ListItem("-Select Prefix-", "0"));
                    foreach (var category in categories)
                    {
                        ddlAgentPrefix.Items.Add(new ListItem(category.SubCategory_Disp, category.SubCategory_Core));
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
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        protected void SetDefault()
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
        protected void EditAgent()
        {
            try
            {
                DataTable dtAgent = _agent.Agent_Select(hfAgent_ID.Value);
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
                _logger.LogError(_logger.MethodName, ex);

                throw;
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
                    List<Parameter> oParams = new List<Parameter>();
                    AgentModel agentModel = new AgentModel();
                    agentModel.Agent_ID = lblAgent_ID.Text.Trim();
                    agentModel.Prefix_Core = ddlAgentPrefix.SelectedValue.ToString();
                    agentModel.FirstName = txtAgentFirstName.Text.Trim();
                    agentModel.MiddleName = txtAgentMiddleName.Text.Trim();
                    agentModel.LastName = txtAgentLastName.Text.Trim();
                    agentModel.DateOfBirth = sDOB;
                    agentModel.Address1 = txtAgentAddress1.Text.Trim();
                    agentModel.Address2 = txtAgentAddress2.Text.Trim();
                    agentModel.City = txtAgentCity.Text.Trim();
                    agentModel.State_Core = ddlAgentState.SelectedValue.ToString();
                    agentModel.Country_Core = txtAgentCountry.Text.Trim();
                    agentModel.PhoneNumber1 = txtAgentPhoneNumber1.Text.Trim();
                    agentModel.PhoneNumber2 = txtAgentPhoneNumber2.Text.Trim();
                    agentModel.EMail = txtAgentEmail.Text.Trim();
                    agentModel.Gender = sGender;

                    if (hfAgent_ID.Value == "")
                    {
                        //Insert New Agent  
                        agentModel.CreatedBY = hfCreatedBy.Value.Trim();
                        string message = _agent.Agent_Insert(agentModel);

                        if (!string.IsNullOrWhiteSpace(message))
                        {
                            //update LastSavedId                
                            _lastSavedID.LastSavedID_Update(agentModel.Agent_ID, LastIdTypes.Agent);
                            SetDefault();
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + message + "');});", true);
                        }
                    }
                    else
                    {
                        agentModel.ModifiedBY = hfCreatedBy.Value.Trim();
                        ////Update Customer                           
                        string message = _agent.Agent_Update(agentModel);
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + sMessage + "');});", true);

                    }
                }
            }

            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                ExceptionMessage(ExceptionType.ERROR, XmlProcessor.ReadXmlFile("OTW901"));
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