﻿using Paybook.BusinessLayer;
using Paybook.BusinessLayer.Business;
using Paybook.BusinessLayer.Setting;
using Paybook.ServiceLayer;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Paybook.WebUI.Business
{
    public partial class Profile : System.Web.UI.Page
    {
        private readonly ILogger _logger;
        private readonly ICategoryProcessor _category;
        private readonly IBusinessProcessor _business;

        public Profile()
        {
            _logger = FileLogger.Instance;
            _category = new CategoryProcessor();
            _business = new BusinessProcessor();
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
                    SubCategories_SelectState();
                    CompanyProfile_IsExist();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + XmlProcessor.ReadXmlFile("") + "');});", true);
            }
        }
        protected void btnSubmitAndUpdate_Click(object sender, EventArgs e)
        {
            string message = IsModelValid();
            if (!string.IsNullOrWhiteSpace(message))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + message + "');});", true);
                return;
            }

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

            BusinessModel businessModel = new BusinessModel();
            businessModel.ModifiedBY = hfLogInUser.Value.Trim();
            businessModel.CompanyName = txtCompanyName.Text.Trim();
            businessModel.Founded_Date = txtCompanyFounderDate.Text.Trim();
            businessModel.Address1 = txtCompanyAddress1.Text.Trim();
            //oCompany.Address2 = txtCompanyAddress2.Text.Trim();
            businessModel.City = txtCompanyCity.Text.Trim();
            businessModel.State_Core = ddlCompanyState.SelectedValue.ToString();
            businessModel.Country_Core = txtCompanyCountry.Text.Trim();
            businessModel.PhoneNumber1 = txtCompanyPhoneNumber1.Text.Trim();
            //oCompany.PhoneNumber2 = txtCompanyPhoneNumber2.Text.Trim();
            businessModel.FaxNumber = txtCompanyFaxNumber.Text.Trim();
            businessModel.EMail = txtCompanyEmail.Text.Trim();
            businessModel.ImageFileName = txtImageUrl.Text.Trim();
            businessModel.UserName = txtUsername.Text.Trim();
            businessModel.Password = txtPassword.Text.Trim();
            businessModel.GSTIN = txtGSTIN.Text.Trim();

            string sMessage = _business.CompanyProfile_Update(businessModel);

            ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + sMessage + "');});", true);
            CompanyProfile_IsExist();

        }

        private void CompanyProfile_IsExist()
        {
            try
            {
                DataTable dt = _business.CompanyProfile_IsExist();
                if (dt.Rows.Count > 0 && dt != null)
                {
                    if (Int32.Parse(dt.Rows[0]["Company_ID"].ToString()) == 0)
                    {
                        string sMessage = XmlProcessor.ReadXmlFile("CPS403");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + sMessage + "');});", true);
                    }
                    else
                    {
                        btnSubmitAndUpdate.Text = "Update";
                        DataTable dtCompanyProfile = _business.CompanyProfile_Select();
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
                            txtImageUrl.Text = dtCompanyProfile.Rows[0]["ImageFileName"].ToString();
                            imgCompanyLogo.ImageUrl = Application["Path"] + _FolderPath.CompanyLogo_Path + dtCompanyProfile.Rows[0]["ImageFileName"].ToString();
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
        private void SubCategories_SelectState()
        {
            try
            {

                CategoryModel[] categories = _category.SubCategories_Active_Select(CategoryTypes.State);
                if (categories != null && categories.Length > 0)
                {
                    ddlCompanyState.Items.Insert(0, new ListItem("-Select State-", "0"));
                    for (int i = 0; i < categories.Length; i++)
                    {

                        ddlCompanyState.Items.Add(new ListItem(categories[i].SubCategory_Disp.ToString(), categories[i].SubCategory_Core.ToString()));

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
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        private string IsModelValid()
        {
            if (txtCompanyPhoneNumber1.Text == "")
            {
                return XmlProcessor.ReadXmlFile("BSW009");
            }
            else if (txtCompanyPhoneNumber1.Text.Length < 10)
            {
                return XmlProcessor.ReadXmlFile("BSW010");
            }
            else if (txtCompanyEmail.Text == "")
            {
                return XmlProcessor.ReadXmlFile("BSW011");
            }
            else if (txtCompanyName.Text == "")
            {
                return XmlProcessor.ReadXmlFile("BSW008");
            }
            else if (txtUsername.Text != "" && txtPassword.Text != "")
            {
                if (txtPassword.Text != txtPasswordConfirm.Text)
                {
                    return XmlProcessor.ReadXmlFile("BSW014");
                }
            }
            return string.Empty;
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