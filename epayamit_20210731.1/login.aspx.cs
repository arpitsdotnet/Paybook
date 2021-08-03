using System;
using System.Collections.Generic;
using System.Data;
using Paybook.BusinessLayer;

namespace Paybook.WebUI
{
    public partial class login : System.Web.UI.Page
    {
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
                ExceptionMessage(ExceptionType.ERROR, "Error Found: " + ex.Message);
            }
        }
        protected void GetCaptcha()
        {
            Random r = new Random();

            int iFirstNo = r.Next(1, 10);
            int iSecondNo = r.Next(1, 10);

            int iResult = iFirstNo + iSecondNo;
            hfCaptchaResult.Value = iResult.ToString();
            lblCaptcha.Text = iFirstNo.ToString() + " + " + iSecondNo.ToString() + " = ";
        }
        protected Boolean Vaildation_Captcha()
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
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string sMessage = "";


                string sUserName = txtUserName.Text.Trim();
                string sPassword = txtPassword.Text.Trim();
                if (sUserName == "" || sPassword == "")
                {
                    sMessage = clsCommon.ReadXmlFile("BSW007");
                    ExceptionMessage(ExceptionType.WARNING, sMessage);
                }
                else if (!Vaildation_Captcha())
                {
                    sMessage = clsCommon.ReadXmlFile("BSW015");
                    ExceptionMessage(ExceptionType.WARNING, sMessage);
                }
                else
                {
                    List<clsParams> oParams = new List<clsParams>();
                    oParams.Add(new clsParams("sUserName", sUserName));
                    oParams.Add(new clsParams("sPassword", sPassword));
                    DataTable dt = clsCommon.Login_Isvalid(oParams);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Session["LoggedInUser"] = dt.Rows[0]["CompanyName"].ToString() + "/" + dt.Rows[0]["UserID"].ToString();
                        Response.Redirect(Application["Path"] + "home", false);
                    }
                    else
                    {
                        sMessage = clsCommon.ReadXmlFile("BSW006");
                        ExceptionMessage(ExceptionType.WARNING, sMessage);
                    }
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