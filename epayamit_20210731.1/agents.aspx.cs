using System;
using Paybook.BusinessLayer;

namespace Paybook.WebUI
{
    public partial class agents : System.Web.UI.Page
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
                ExceptionMessage(ExceptionType.ERROR, "Error Found: " + ex.Message);
            }
        }
        protected void btnAgentCreate_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/agent", false);
            }
            catch(Exception ex)
            {
                ExceptionMessage(ExceptionType.ERROR, "Error Found: " + ex.Message);
            }
        }
        protected void btnAgentEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (hfAgent_ID.Value == "")
                {

                    string sMessage = clsCommon.ReadXmlFile("AGW201");
                    ExceptionMessage(ExceptionType.WARNING, sMessage);
               
              return;
                   // return;
                }
                else
                {
                    Response.Redirect("~/agent/=" + hfAgent_ID.Value, false);
                }
            }
            catch (Exception ex)
            {
                ExceptionMessage(ExceptionType.ERROR, "Error Found: " + ex.Message);
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