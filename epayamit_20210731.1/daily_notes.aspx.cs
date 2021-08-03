using System;
using System.Collections.Generic;
using Paybook.BusinessLayer;

namespace Paybook.WebUI
{
    public partial class notes : System.Web.UI.Page
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
                    txtDailyNotes.Text = "";
                    string[] sLoginUser = Session["LoggedInUser"].ToString().Split('/');
                    hfLogInUser.Value = sLoginUser[0];
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + ex.Message + "');});", true);

            }
        }
        protected void DefaultValues()
        {
            try
            {
                txtVehicleNo.Text = "";
                txtName.Text = "";
                txtMobileNumber.Text = "";
                txtAwak.Text = "";
                txtJawak.Text = "";
                txtBalance.Text = "";
                txtTotalAmount.Text = "";
                txtWork.Text = "";
                txtDailyNotes.Text = "";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string sMessage = "";
                if (txtDailyNotes.Text != "")
                {

                    List<clsParams> oParams = new List<clsParams>();
                    clsDailyNotes oNote = new clsDailyNotes();
                    oNote.CreatedBY = hfLogInUser.Value.Trim();
                    oNote.Note = txtDailyNotes.Text.Trim();
                    oNote.VehicleNumber = txtVehicleNo.Text.Trim();
                    oNote.Name = txtName.Text.Trim();
                    oNote.MobileNumber = txtMobileNumber.Text.Trim();
                    oNote.Awak = txtAwak.Text.Trim();
                    oNote.Jawak = txtJawak.Text.Trim();
                    oNote.Balance = txtBalance.Text.Trim();
                    oNote.TotalAmount = txtTotalAmount.Text.Trim();
                    oNote.Work = txtWork.Text.Trim();
                    oParams.Add(new clsParams("sNote", oNote.Note));
                    oParams.Add(new clsParams("sVehicleNumber", oNote.VehicleNumber));
                    oParams.Add(new clsParams("sName", oNote.Name));
                    oParams.Add(new clsParams("sMobileNumber", oNote.MobileNumber));
                    oParams.Add(new clsParams("sAwak", oNote.Awak));
                    oParams.Add(new clsParams("sJawak", oNote.Jawak));
                    oParams.Add(new clsParams("sBalance", oNote.Balance));
                    oParams.Add(new clsParams("sTotalAmount", oNote.TotalAmount));
                    oParams.Add(new clsParams("sWork", oNote.Work));
                    if (hfDataID.Value == "")
                    {
                        oParams.Add(new clsParams("sCreatedBY", oNote.CreatedBY));
                        sMessage = clsCommon.DailyNotes_Insert(oParams);
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + sMessage + "');});", true);
                        DefaultValues();
                    }
                    else
                    {
                        oNote.ID = hfDataID.Value.Trim();
                        oParams.Add(new clsParams("sModifiedBY", oNote.CreatedBY));
                        oParams.Add(new clsParams("sDataID", oNote.ID));
                        sMessage = clsCommon.DailyNotes_Update(oParams);
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + sMessage + "');});", true);
                        DefaultValues();
                        hfDataID.Value = "";
                    }
                }
                else
                {
                    sMessage = clsCommon.ReadXmlFile("BSW016");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + sMessage + "');});", true);

                }
            }

            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + ex.Message + "');});", true);

            }
        }
    }
}