using Paybook.BusinessLayer;
using Paybook.BusinessLayer.Agency;
using Paybook.BusinessLayer.Client;
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

namespace Paybook.WebUI.Client
{
    public partial class Create : System.Web.UI.Page
    {
        private readonly ILogger _logger;
        private readonly ICategoryProcessor _category;
        private readonly ILastSavedIdProcessor _lastSavedId;
        private readonly IClientProcessor _client;
        private readonly IAgencyProcessor _agency;

        public Create()
        {
            _logger = FileLogger.Instance;
            _category = new CategoryProcessor();
            _lastSavedId = new LastSavedIdProcessor();
            _client = new ClientProcessor();
            _agency = new AgencyProcessor();
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
                        lblCustomer_ID.Text = _lastSavedId.GetLastSavedID(LastIdTypes.Customer); ;
                        lblPageHeading.Text = "Add New Customer";
                        hfCustomer_ID.Value = "";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + XmlProcessor.ReadXmlFile("OTW901") + "');});", true);
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string message = IsModelValid();
                if (!string.IsNullOrWhiteSpace(message))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + message + "');});", true);
                    return;
                }

                string sCustomer_Type = rbtnCustomer_Type.SelectedValue.ToString();
                string sDOB = Convert.ToDateTime(txtCustomerDOB.Text.Trim()).ToString("yyyy-MM-dd HH:mm:ss");

                var sGender = rbtnGender.SelectedValue.ToString();
                CustomerModel customerModel = new CustomerModel
                {
                    Customer_ID = lblCustomer_ID.Text.Trim(),
                    Prefix_Core = ddlCustomerPrefix.SelectedValue.ToString(),
                    FirstName = txtCustomerFirstName.Text.Trim(),
                    MiddleName = txtCustomerMiddleName.Text.Trim(),
                    LastName = txtCustomerLastName.Text.Trim(),
                    DateOfBirth = sDOB,
                    Address1 = txtCustomerAddress1.Text.Trim(),
                    Address2 = txtCustomerAddress2.Text.Trim(),
                    City = txtCustomerCity.Text.Trim(),
                    State_Core = ddlCustomerState.SelectedValue.ToString(),
                    Country_Core = txtCustomerCountry.Text.Trim(),
                    PhoneNumber1 = txtCustomerPhoneNumber1.Text.Trim(),
                    PhoneNumber2 = txtCustomerPhoneNumber2.Text.Trim(),
                    EMail = txtCustomerEmail.Text.Trim(),
                    Customer_Type = sCustomer_Type,
                    Gender = sGender,
                    Agency_ID = ddlAgencyName.SelectedValue.ToString()
                };


                if (hfCustomer_ID.Value == "")
                {
                    customerModel.CreatedBY = hfLogInUser.Value.Trim();
                    message = _client.Customer_Insert(customerModel);

                    //update LastSavedId                
                    _lastSavedId.LastSavedID_Update(customerModel.Customer_ID, LastIdTypes.Customer);
                    SetDefault();

                    //show new customer id
                    lblCustomer_ID.Text = _lastSavedId.GetLastSavedID(LastIdTypes.Customer); ;

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + message + "');});", true);

                }
                else
                {
                    customerModel.ModifiedBY = hfLogInUser.Value.Trim();

                    ////Update Customer                           
                    message = _client.Customer_Update(customerModel);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + message + "');});", true);

                }
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
        protected void EditCustomer()
        {

            try
            {
                DataTable dtCustomer = _client.Customer_Select(hfCustomer_ID.Value);
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
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        protected void AgencyName_Select()
        {
            try
            {
                DataTable dt = _agency.Agency_SelectName();
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
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        protected void SubCategories_SelectState()
        {
            try
            {
                CategoryModel[] categories = _category.SubCategories_Active_Select(CategoryTypes.State);
                if (categories != null && categories.Length > 0)
                {
                    ddlCustomerState.Items.Insert(0, new ListItem("-Select State-", "0"));
                    foreach (var category in categories)
                    {
                        ddlCustomerState.Items.Add(new ListItem(category.SubCategory_Disp, category.SubCategory_Core));
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
                    ddlCustomerPrefix.Items.Insert(0, new ListItem("-Select Prefix-", "0"));
                    foreach (CategoryModel category in categories)
                    {
                        ddlCustomerPrefix.Items.Add(new ListItem(category.SubCategory_Disp, category.SubCategory_Core));
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
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        protected void SetDefault()
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
        protected string IsModelValid()
        {
            try
            {
                if (txtCustomerPhoneNumber1.Text == "" || txtCustomerPhoneNumber1.Text.Length > 10)
                    return XmlProcessor.ReadXmlFile("BSW010");


                else if (txtCustomerFirstName.Text == "")
                    return XmlProcessor.ReadXmlFile("CU101");

                else
                {
                    if (hfCustomer_ID.Value == "")
                    {
                        string sAgencyID = "0";
                        if (ddlAgencyName.SelectedIndex != 0)
                            sAgencyID = ddlAgencyName.SelectedValue.ToString();

                        CustomerModel customerModel = new CustomerModel();
                        customerModel.FirstName = txtCustomerFirstName.Text.Trim();
                        customerModel.Agency_ID = sAgencyID;
                        customerModel.PhoneNumber1 = txtCustomerPhoneNumber1.Text.Trim();

                        return _client.Customer_IsExist(customerModel);
                    }

                }
                return string.Empty;

            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
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