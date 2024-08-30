using Paybook.BusinessLayer;
using Paybook.BusinessLayer.Features.Admins;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Xml;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace Paybook.WebUI.Setting
{
    public partial class Category : System.Web.UI.Page
    {
        private readonly ILogger _logger;
        private readonly CategoryProcessor _category;

        public Category(ILogger logger, CategoryProcessor category)
        {
            _logger = logger;
            _category = category;
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
                    //hfLogInUser_ID.Value = sLoginUser[1];
                    Categories_Active_SelectAll();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + XmlMessageHelper.Get("") + "');});", true);
            }
        }
        protected void Categories_Active_SelectAll()
        {
            try
            {

                DataTable dt = _category.Categories_Select();

                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlCategories.Items.Insert(0, new ListItem("-Select Category-", "0"));
                    foreach (DataRow dr in dt.Rows)
                    {
                        ddlCategories.Items.Add(new ListItem(dr["Category_Disp"].ToString(), dr["Category_Core"].ToString()));
                    }
                    ddlCategories.SelectedIndex = 0;
                }
                else
                {
                    ddlCategories.Items.Insert(0, new ListItem("-No Category Found-", "0"));
                    ddlCategories.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }

    }
}