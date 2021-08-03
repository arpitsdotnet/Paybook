using System;
using System.Web.UI;
using Paybook.BusinessLayer;
namespace Paybook.WebUI
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Session.Clear();
                Session.Abandon();
                if (!Page.IsPostBack)
                {
                    //    if (Request.UrlReferrer != null)
                    //    {
                    //        string RefUrl = Request.UrlReferrer.ToString();
                    //        Session["RefUrl"] = RefUrl;
                    //    }
                    //    else
                    //        Session["RefUrl"] = null;
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
                    idLabelError.Attributes.Add("class", "fwt-container fwt-padding-10 fwt-pale-yellow fwt-border fwt-border-yellow");
                }
                else if (sType.ToString() == "ERROR")
                {
                    idLabelError.Attributes.Add("class", "fwt-container fwt-padding-10 fwt-pale-red fwt-border fwt-border-red");
                }
                else if (sType.ToString() == "SUCCESS")
                {
                    idLabelError.Attributes.Add("class", "fwt-container fwt-padding-10 fwt-pale-green fwt-border fwt-border-green");
                }
                else if (sType.ToString() == "INFO")
                {
                    idLabelError.Attributes.Add("class", "fwt-container fwt-padding-10 fwt-pale-blue fwt-border fwt-border-blue");
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
