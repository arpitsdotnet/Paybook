using System;
using System.Data;
using System.Web.UI.WebControls;
using Paybook.BusinessLayer;

namespace Paybook.WebUI
{
    public partial class categories : System.Web.UI.Page
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
                    string[] sLoginUser = Session["LoggedInUser"].ToString().Split('/');
                    hfLogInUser.Value = sLoginUser[0];
                    //hfLogInUser_ID.Value = sLoginUser[1];
                    Categories_Active_SelectAll();                   
                }
            }
            catch (Exception ex)
            {               
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + ex.Message + "');});", true);

            }
        }
        protected void Categories_Active_SelectAll()
        {
            try
            {

                DataTable dt = clsCommon.Categories_Select();

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
                throw new Exception(ex.Message);
            }
        }     
     
    }
}