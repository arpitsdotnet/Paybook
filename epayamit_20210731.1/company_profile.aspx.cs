using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Paybook.BusinessLayer;

namespace Paybook.WebUI
{
    public partial class company_profile : System.Web.UI.Page
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
                    SubCategories_SelectState();
                    CompanyProfile_IsExist();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + ex.Message + "');});", true);

            }
        }
        protected void CompanyProfile_IsExist()
        {
            try
            {
                DataTable dt = clsCommon.CompanyProfile_IsExist();
                if (dt.Rows.Count > 0 && dt != null)
                {
                    if (Int32.Parse(dt.Rows[0]["Company_ID"].ToString()) == 0)
                    {
                       string sMessage = clsCommon.ReadXmlFile("CPS403");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + sMessage + "');});", true);
                    }
                    else
                    {
                        btnSubmitAndUpdate.Text = "Update";
                        DataTable dtCompanyProfile = clsCommon.CompanyProfile_Select();
                        if (dtCompanyProfile.Rows.Count > 0 && dtCompanyProfile != null)
                        {
                            txtCompanyName.Text = dtCompanyProfile.Rows[0]["CompanyName"].ToString();
                            txtCompanyFounderDate.Text = dtCompanyProfile.Rows[0]["Founded_Date"].ToString();
                            txtCompanyAddress1.Text = dtCompanyProfile.Rows[0]["Address1"].ToString();
                            // txtCompanyAddress2.Text = dtCompanyProfile.Rows[0]["Address2"].ToString();
                            txtCompanyCity.Text = dtCompanyProfile.Rows[0]["City"].ToString();
                            ddlCompanyState.SelectedValue = dtCompanyProfile.Rows[0]["State_Core"].ToString();
                            txtCompanyCountry.Text = dtCompanyProfile.Rows[0]["Country_Core"].ToString();
                            txtCompanyPhoneNumber1.Text = dtCompanyProfile.Rows[0]["PhoneNumber1"].ToString();
                            // txtCompanyPhoneNumber2.Text = dtCompanyProfile.Rows[0]["PhoneNumber2"].ToString();
                            txtCompanyFaxNumber.Text = dtCompanyProfile.Rows[0]["FaxNumber"].ToString();
                            txtCompanyEmail.Text = dtCompanyProfile.Rows[0]["EMail"].ToString();
                            txtGSTIN.Text = dtCompanyProfile.Rows[0]["GSTIN"].ToString();
                            txtImageUrl.Text  = dtCompanyProfile.Rows[0]["ImageFileName"].ToString();
                            imgCompanyLogo.ImageUrl = Application["Path"]+ _FolderPath.CompanyLogo_Path + dtCompanyProfile.Rows[0]["ImageFileName"].ToString();
                            Image imgLogo = new Image();
                            imgLogo = (Image)(Page.Master.FindControl("hlLoggedInControlsProfile"));
                            if (imgLogo != null)
                            {
                                imgLogo.ImageUrl = Application["Path"] + _FolderPath.CompanyLogo_Path + dtCompanyProfile.Rows[0]["ImageFileName"].ToString(); 
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected void SubCategories_SelectState()
        {
            try
            {

                clsCategories[] arrCategory = clsCommon.SubCategories_Active_Select(clsCategory_Type._State);
                if (arrCategory != null && arrCategory.Length > 0)
                {
                    ddlCompanyState.Items.Insert(0, new ListItem("-Select State-", "0"));
                    for (int i = 0; i < arrCategory.Length; i++)
                    {

                        ddlCompanyState.Items.Add(new ListItem(arrCategory[i].SubCategory_Disp.ToString(), arrCategory[i].SubCategory_Core.ToString()));

                    }
                    ddlCompanyState.SelectedIndex = 0;
                }
                else
                {
                    ddlCompanyState.Items.Insert(0, new ListItem("-No State Found-", "0"));
                    ddlCompanyState.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected Boolean Validation()
        {
            try
            {
                string sMessage = "";
                if (txtCompanyPhoneNumber1.Text == "")
                {
                    sMessage = clsCommon.ReadXmlFile("BSW009");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + sMessage + "');});", true);
                    return false;
                }
                else if (txtCompanyPhoneNumber1.Text.Length < 10)
                {
                    sMessage = clsCommon.ReadXmlFile("BSW010");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + sMessage + "');});", true);
                    return false;
                }
                else if (txtCompanyEmail.Text == "")
                {
                    sMessage = clsCommon.ReadXmlFile("BSW011");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + sMessage + "');});", true);
                    return false;
                }
                else if (txtCompanyName.Text == "")
                {
                    sMessage = clsCommon.ReadXmlFile("BSW008");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + sMessage + "');});", true);
                    return false;                   
                }
                else if(txtUsername.Text!="" && txtPassword.Text!="")
                {
                    if(txtPassword.Text!=txtPasswordConfirm.Text)
                    {
                        sMessage = clsCommon.ReadXmlFile("BSW014");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + sMessage + "');});", true);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }

      
        protected void btnSubmitAndUpdate_Click(object sender, EventArgs e)
        {
            if (Validation())
            {

                string extension = Path.GetExtension(fuImageUpload.PostedFile.FileName).ToLower();
                string sFileName = Path.GetFileName(fuImageUpload.PostedFile.FileName);

                extension = extension.Replace(".", "");
                if (extension == _FileExtension.jpg.ToString() || extension == _FileExtension.jpeg.ToString()
                    || extension == _FileExtension.png.ToString() || extension == _FileExtension.bmp.ToString() || extension == _FileExtension.gif.ToString())
                {

                    string imagePath = HttpContext.Current.Server.MapPath("~/" + _FolderPath.CompanyLogo_Path) + Path.GetFileName(fuImageUpload.PostedFile.FileName);
                    fuImageUpload.SaveAs(imagePath);
                   // hfCompanyLogo_Image.Value = fuImageUpload.PostedFile.FileName;
                    //imgCompanyLogo.ImageUrl = _FolderPath.CompanyLogo_Path + sFileName;
                    txtImageUrl.Text = sFileName;
                }

                List<clsParams> oParams = new List<clsParams>();
                clsCompany oCompany = new clsCompany();
                oCompany.ModifiedBY = hfLogInUser.Value.Trim();
                oCompany.CompanyName = txtCompanyName.Text.Trim();
                oCompany.Founded_Date = txtCompanyFounderDate.Text.Trim();
                oCompany.Address1 = txtCompanyAddress1.Text.Trim();
                //oCompany.Address2 = txtCompanyAddress2.Text.Trim();
                oCompany.City = txtCompanyCity.Text.Trim();
                oCompany.State_Core = ddlCompanyState.SelectedValue.ToString();
                oCompany.Country_Core = txtCompanyCountry.Text.Trim();
                oCompany.PhoneNumber1 = txtCompanyPhoneNumber1.Text.Trim();
                //oCompany.PhoneNumber2 = txtCompanyPhoneNumber2.Text.Trim();
                oCompany.FaxNumber = txtCompanyFaxNumber.Text.Trim();
                oCompany.EMail = txtCompanyEmail.Text.Trim();
                oCompany.ImageFileName = txtImageUrl.Text.Trim();
                oCompany.UserName = txtUsername.Text.Trim();
                oCompany.Password = txtPassword.Text.Trim();
                oCompany.GSTIN = txtGSTIN.Text.Trim();

                oParams.Add(new clsParams("sModifiedBY", oCompany.ModifiedBY));
                oParams.Add(new clsParams("sCompanyName", oCompany.CompanyName));
                oParams.Add(new clsParams("sFounded_Date", oCompany.Founded_Date));
                     oParams.Add(new clsParams("sCompanyAddress1", oCompany.Address1));
                oParams.Add(new clsParams("sCompanyAddress2", oCompany.Address2));
                oParams.Add(new clsParams("sCompanyCity", oCompany.City));
                oParams.Add(new clsParams("sCompanyState_Core", oCompany.State_Core));
                oParams.Add(new clsParams("sCompanyCountry", oCompany.Country_Core));
                oParams.Add(new clsParams("sCompanyPhoneNumber1", oCompany.PhoneNumber1));
                oParams.Add(new clsParams("sCompanyPhoneNumber2", ""));
                oParams.Add(new clsParams("sCompanyFaxNumber", oCompany.FaxNumber));
                oParams.Add(new clsParams("sCompanyEmail", oCompany.EMail));
                oParams.Add(new clsParams("sImageFileName", oCompany.ImageFileName));
                oParams.Add(new clsParams("sGSTIN", oCompany.GSTIN));
                oParams.Add(new clsParams("sUserName", oCompany.UserName));
                oParams.Add(new clsParams("sPassword", oCompany.Password));
                string sMessage=clsCommon.CompanyProfile_Update(oParams);            
                
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + sMessage + "');});", true);
                CompanyProfile_IsExist();
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
        //    }
        //    catch (Exception ex)
        //    {
        //        idLabelError.InnerHtml = ex.Message;
        //        idLabelError.Style.Add("display", "block");
        //    }
        //}


    }
}