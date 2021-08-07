using Paybook.BusinessLayer;
using Paybook.BusinessLayer.Note;
using Paybook.BusinessLayer.Setting;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;

namespace Paybook.WebUI.Notes
{
    public partial class Index : System.Web.UI.Page
    {
        private readonly ILogger _logger;

        public Index(ILogger logger)
        {
            _logger = logger;
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
                    hfLogInUser.Value = Session["LoggedInUser"].ToString().Split('/')[0];
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + XmlProcessor.ReadXmlFile("OTW901") + "');});", true);
            }
        }
    }
}