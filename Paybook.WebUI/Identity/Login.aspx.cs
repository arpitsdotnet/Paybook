﻿using Paybook.BusinessLayer;
using Paybook.BusinessLayer.Identity;
using Paybook.ServiceLayer.Enums;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Data;

namespace Paybook.WebUI.Identity
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly ILogger _logger;
        private readonly ILoginProcessor _login;
        public Login()
        {
            _logger = FileLogger.Instance;
            _login = new LoginProcessor();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    idLabelError.InnerText = "";
                    hfCaptchaResult.Value = "";
                    GetCaptcha();
                    txtUserName.Focus();
                }
            }

            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                ExceptionMessage(ExceptionType.ERROR, XmlProcessor.ReadXmlFile("OTW901"));
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                LoginModel loginModel = new LoginModel();
                loginModel.Username = txtUserName.Text.Trim();
                loginModel.Password = txtPassword.Text.Trim();

                string message = IsModelValid(loginModel);
                if (!string.IsNullOrWhiteSpace(message))
                {
                    ExceptionMessage(ExceptionType.WARNING, message);
                    return;
                }

                DataTable dt = _login.Login_Isvalid(loginModel);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Session["LoggedInUser"] = dt.Rows[0]["CompanyName"].ToString() + "/" + dt.Rows[0]["UserID"].ToString();
                    Response.Redirect(Application["Path"] + "dashboard", false);
                }
                else
                {
                    ExceptionMessage(ExceptionType.WARNING, XmlProcessor.ReadXmlFile("BSW006"));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                ExceptionMessage(ExceptionType.ERROR, XmlProcessor.ReadXmlFile("OTW901"));
            }
        }
        protected void GetCaptcha()
        {
            try
            {
                Random r = new Random();

                int iFirstNo = r.Next(1, 10);
                int iSecondNo = r.Next(1, 10);

                int iResult = iFirstNo + iSecondNo;
                hfCaptchaResult.Value = iResult.ToString();
                lblCaptcha.Text = iFirstNo.ToString() + " + " + iSecondNo.ToString() + " = ";
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        private Boolean IsCaptchaValid()
        {
            int result;
            if (txtCaptcha.Text.Trim() == "")
            {
                return false;
            }
            else if (!int.TryParse(txtCaptcha.Text.Trim(), out result))
            {
                return false;
            }
            else if (Convert.ToInt32(hfCaptchaResult.Value) != Convert.ToInt32(txtCaptcha.Text.Trim()))
            {
                return false;
            }
            else
                return true;
        }

        private string IsModelValid(LoginModel loginModel)
        {
            if (loginModel.Username == "" || loginModel.Password == "")
            {
                return XmlProcessor.ReadXmlFile("BSW007");
            }
            if (!IsCaptchaValid())
            {
                return XmlProcessor.ReadXmlFile("BSW015");
            }

            return string.Empty;
        }

        private void ExceptionMessage(ExceptionType sType, string sMessage)
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