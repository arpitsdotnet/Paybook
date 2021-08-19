using Paybook.BusinessLayer;
using Paybook.BusinessLayer.Agency;
using Paybook.BusinessLayer.Client;
using Paybook.BusinessLayer.Setting;
using Paybook.ServiceLayer;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace Paybook.WebUI.Invoice
{
    public partial class Index : System.Web.UI.Page
    {
        private readonly ILogger _logger;
        private readonly ICategoryProcessor _category;
        private readonly IAgencyProcessor _agency;
        private readonly IClientProcessor _client;

        public Index(ILogger logger, ICategoryProcessor category, IAgencyProcessor agency, IClientProcessor client)
        {
            _logger = logger;
            _category = category;
            _agency = agency;
            _client = client;
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
                    string[] sLoginUser = Session["LoggedInUser"].ToString().Split('/');
                    hfLogInUser.Value = sLoginUser[0];
                    hfLogInUser_ID.Value = sLoginUser[1];

                    txtDateFrom.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
                    txtDateTo.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    // Agents_Active_SelectAll();
                    Categories_Active_SelectAll();
                    Agency_Select();
                    //  Customers_Active_SelectAll();
                    // ReceiptNumber_Create();
                    SubCategories_Select_InvoiceStatus();
                    //if (Request.QueryString.Count > 0)
                    //{
                    //    txtDateFrom.Text = "";
                    //    txtDateTo.Text = "";
                    //}

                }
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + ex.Message + "');});", true);
            }
        }
        protected void btnInvoiceCreate_Click(object sender, EventArgs e)
        {
            // DataTable dt = clsCommon.InvoicePayment_Report_Select(txtDateFrom.Text, txtDateTo.Text, ddlCustomers.SelectedValue);
            try
            {
                Response.Redirect("invoice/create", false);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + ex.Message + "');});", true);
            }
        }
        protected void Agency_Select()
        {
            try
            {
                DataTable dt = _agency.Agency_SelectName();
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlAgency.Items.Insert(0, new ListItem("-All-", "All"));
                    foreach (DataRow dr in dt.Rows)
                    {

                        ddlAgency.Items.Add(new ListItem(dr["AgencyName"].ToString(), dr["Agency_ID"].ToString()));

                    }

                    ddlAgency.SelectedIndex = 0;
                }
                else
                {
                    ddlAgency.Items.Insert(0, new ListItem("-No Agency Found-", "0"));
                    ddlAgency.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }

        protected void Categories_Active_SelectAll()
        {
            try
            {
                CategoryModel[] categories = _category.SubCategories_Active_Select(CategoryTypes.Work);

                if (categories != null && categories.Length > 0)
                {
                    ddlCategories.Items.Insert(0, new ListItem("-All-", "All"));
                    foreach (CategoryModel category in categories)
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
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }

        protected void SubCategories_Select_InvoiceStatus()
        {
            try
            {
                CategoryModel[] arrCategory = _category.SubCategories_Active_Select(CategoryTypes.Invoice);

                if (arrCategory != null && arrCategory.Length > 0)
                {
                    ddlInvoiceStatus.Items.Insert(0, new ListItem("-All-", "All"));
                    for (int i = 0; i < arrCategory.Length; i++)
                    {
                        ddlInvoiceStatus.Items.Add(new ListItem(arrCategory[i].SubCategory_Disp.ToString(), arrCategory[i].SubCategory_Core.ToString()));
                    }

                    ddlInvoiceStatus.SelectedIndex = 0;
                }
                else
                {
                    ddlInvoiceStatus.Items.Insert(0, new ListItem("-No Category Found-", "0"));
                    ddlInvoiceStatus.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }

        protected void Customers_Active_SelectAll()
        {
            try
            {
                ClientModel[] customers = _client.GetAllNamesByAgencyID("");

                if (customers != null && customers.Length > 0)
                {
                    ddlCustomers.Items.Insert(0, new ListItem("-All-", "All"));
                    foreach (ClientModel customer in customers)
                    {
                        ddlCustomers.Items.Add(new ListItem(customer.CustomerName, customer.Customer_ID.ToString()));
                    }

                    if (Request.QueryString.Count > 0)
                    {
                        if (Request.QueryString["customer_id"] != null || Request.QueryString["customer_id"].ToString() != "")
                        {
                            string[] sCustomer_ID = Request.QueryString["customer_id"].Split('=');
                            ddlCustomers.SelectedValue = sCustomer_ID[0];
                            txtDateFrom.Text = "";
                            txtDateTo.Text = "";
                        }
                    }
                    else
                        ddlCustomers.SelectedIndex = 0;
                }
                else
                {
                    ddlCustomers.Items.Insert(0, new ListItem("-No Customer Name Found-", "0"));
                    ddlCustomers.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        //protected void Agents_Active_SelectAll()
        //{
        //    try
        //    {
        //        clsAgents[] arrAgents = clsCommon.Agents_Active_SelectAll();

        //        if (arrAgents != null && arrAgents.Length > 0)
        //        {
        //            ddlAgents.Items.Insert(0, new ListItem("-All-", "All"));
        //            for (int i = 0; i < arrAgents.Length; i++)
        //            {

        //                ddlAgents.Items.Add(new ListItem(arrAgents[i].AgentName.ToString(), arrAgents[i].Agent_ID.ToString()));

        //            }
        //            ddlAgents.SelectedIndex = 0;
        //        }
        //        else
        //        {
        //            ddlAgents.Items.Insert(0, new ListItem("-No Category Found-", "0"));
        //            ddlAgents.SelectedIndex = 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}


    }
}