using System;

namespace Paybook.WebUI
{
    public partial class customers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["LoggedInUser"] == null || Session["LoggedInUser"].ToString() == "")
                {
                    Response.Redirect("login", false);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + ex.Message + "');});", true);

            }
        }
        protected void btnCustomerCreate_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/customer", false);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + ex.Message + "');});", true);

            }
        }

        protected void btnAgencyCreate_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/agency", false);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + ex.Message + "');});", true);

            }
        }
        //protected void btnCustomerEdit_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (hfCustomer_ID.Value == "")
        //        {

        //            string sMessage = clsErrorMessage._SelectCustomerError;
        //            ExceptionMessage(ExceptionType.ERROR, sMessage);
        //            return;
        //        }
        //        else
        //        {
        //          string sCustomer_ID = hfCustomer_ID.Value;
        //            Response.Redirect("~/customer/" +sCustomer_ID, false);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionMessage(ExceptionType.ERROR, "Error Found:" + ex.Message);
        //    }
        //}


        //protected void lnkAddPayment_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (hfCustomer_ID.Value == "")
        //        {

        //            string sMessage = clsErrorMessage._SelectCustomerError;
        //            ExceptionMessage(ExceptionType.ERROR, sMessage);
        //            return;
        //        }
        //        else
        //        {
        //            Response.Redirect("~/payment/" + hfCustomer_ID.Value, false);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionMessage(ExceptionType.ERROR, "Error Found:" + ex.Message);
        //    }
        //}


    }
}