using Paybook.BusinessLayer;
using Paybook.BusinessLayer.Agency;
using Paybook.BusinessLayer.Common;
using Paybook.BusinessLayer.Setting;
using Paybook.ServiceLayer;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Paybook.WebUI.Agency
{
    public partial class Create : System.Web.UI.Page
    {
        private readonly ILogger _logger;
        private readonly ILastSavedNumberProcessor _lastSavedId;
        private readonly ICategoryProcessor _category;
        private readonly IAgencyProcessor _agency;

        public Create(ILogger logger, ILastSavedNumberProcessor lastSavedId, ICategoryProcessor category, IAgencyProcessor agency)
        {
            _logger = logger;
            _lastSavedId = lastSavedId;
            _category = category;
            _agency = agency;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            // txtCustomerDOB.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            try
            {
                if (Session["LoggedInUser"] == null || Session["LoggedInUser"].ToString() == "")
                {
                    Response.Redirect("~/identity/login", false);
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
                        string sAgency_ID = _lastSavedId.GetLastSavedID(LastSavedNumberTypes.Agency);
                        lblAgency_ID.Text = sAgency_ID;
                        lblPageHeading.Text = "Add New Agency";
                        hfAgency_ID.Value = "";
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + XmlProcessor.ReadXmlFile("OTW901") + "');});", true);

            }
        }
        protected void EditAgency()
        {

            try
            {
                DataTable dtAgency = _agency.Agency_Select(hfAgency_ID.Value);
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
            try
            {
                if (txtAgencyPhoneNumber1.Text == "" || txtAgencyPhoneNumber1.Text.Length > 10)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + XmlProcessor.ReadXmlFile("BSW010") + "');});", true);
                    return false;
                }

                else if (txtAgencyName.Text == "")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + XmlProcessor.ReadXmlFile("AGE101") + "');});", true);
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

                CategoryModel[] categories = _category.SubCategories_Active_Select(CategoryTypes.State);
                if (categories != null && categories.Length > 0)
                {
                    ddlAgencyState.Items.Insert(0, new ListItem("-Select State-", "0"));
                    foreach (CategoryModel category in categories)
                    {
                        ddlAgencyState.Items.Add(new ListItem(category.SubCategory_Disp, category.SubCategory_Core));
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
                    var oAgency = new AgencyModel
                    {
                        AgencyNumber = lblAgency_ID.Text.Trim(),
                        AgencyName = txtAgencyName.Text.Trim(),
                        AddressLine1 = txtAgencyAddress1.Text.Trim(),
                        AddressLine2 = txtAgencyAddress2.Text.Trim(),
                        City = txtAgencyCity.Text.Trim(),
                        State = ddlAgencyState.SelectedValue.ToString(),
                        Country_Core = txtAgencyCountry.Text.Trim(),
                        PhoneNumber1 = txtAgencyPhoneNumber1.Text.Trim(),
                        PhoneNumber2 = txtAgencyPhoneNumber2.Text.Trim(),
                        EMail = txtAgencyEmail.Text.Trim()
                    };

                    if (hfAgency_ID.Value == "")
                    {
                        oAgency.CreatedBY = hfLogInUser.Value.Trim();
                        //Insert New Agency  
                        sMessage = _agency.Agency_Insert(oAgency);
                        //update LastSavedId                
                        _lastSavedId.LastSavedID_Update(oAgency.AgencyNumber, "Agency");
                        SetDefault();
                        //show new Agency id
                        string sAgency_ID = _lastSavedId.GetLastSavedID(LastSavedNumberTypes.Agency);
                        lblAgency_ID.Text = sAgency_ID;
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + sMessage + "');});", true);

                    }
                    else
                    {
                        oAgency.ModifiedBY = hfLogInUser.Value.Trim();
                        ////Update Agency                           
                        sMessage = _agency.Agency_Update(oAgency);

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
                Response.Redirect(Application["Path"] + "customer/create", false);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + ex.Message + "');});", true);

            }
        }

        protected void btnBack_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect(Application["Path"] + "customer", false);
        }
    }
}