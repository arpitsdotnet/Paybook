using Paybook.BusinessLayer.Note;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Paybook.WebUI.Note
{
    public partial class _CreateNotePartial : System.Web.UI.Page
    {
        private readonly ILogger _logger;
        private readonly INoteProcessor _note;
        public _CreateNotePartial()
        {
            _logger = FileLogger.Instance;
            _note = new NoteProcessor();
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
                    txtDailyNotes.Text = "";
                    hfLogInUser.Value = Session["LoggedInUser"].ToString().Split('/')[0];

                    if (Page.RouteData.Values["id"] != null)
                    {
                        string noteId = Page.RouteData.Values["id"].ToString();
                        hfDataID.Value = noteId;
                        GetByNoteID(noteId);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + XmlProcessor.ReadXmlFile("OTW901") + "');});", true);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDailyNotes.Text != "")
                {
                    NoteModel noteModel = new NoteModel
                    {
                        Note = txtDailyNotes.Text.Trim(),
                        VehicleNumber = txtVehicleNo.Text.Trim(),
                        Name = txtName.Text.Trim(),
                        MobileNumber = txtMobileNumber.Text.Trim(),
                        Awak = txtAwak.Text.Trim(),
                        Jawak = txtJawak.Text.Trim(),
                        Balance = txtBalance.Text.Trim(),
                        TotalAmount = txtTotalAmount.Text.Trim(),
                        Work = txtWork.Text.Trim()
                    };

                    if (hfDataID.Value == "")
                    {
                        noteModel.CreatedBY = hfLogInUser.Value.Trim();
                        string message = _note.Create(noteModel);
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + message + "');});", true);
                        DefaultValues();
                    }
                    else
                    {
                        noteModel.ModifiedBY = hfLogInUser.Value.Trim();
                        noteModel.ID = hfDataID.Value.Trim();
                        string message = _note.Update(noteModel);
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + message + "');});", true);
                        DefaultValues();
                        hfDataID.Value = "";
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + XmlProcessor.ReadXmlFile("BSW016") + "');});", true);
                }
            }

            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", "$(document).ready(function () {ShowMessage('" + XmlProcessor.ReadXmlFile("OTW901") + "');});", true);
            }
        }

        private void GetByNoteID(string noteId)
        {
            NoteModel note = _note.GetByNoteID(noteId);
            if (note != null)
            {
                txtVehicleNo.Text = note.VehicleNumber;
                txtName.Text = note.Name;
                txtMobileNumber.Text = note.MobileNumber;
                txtAwak.Text = note.Awak;
                txtJawak.Text = note.Jawak;
                txtBalance.Text = note.Balance;
                txtTotalAmount.Text = note.TotalAmount;
                txtWork.Text = note.Work;
                txtDailyNotes.Text = note.Note;
            }
        }
        protected void DefaultValues()
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
    }
}