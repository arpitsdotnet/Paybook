<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="Paybook.WebUI.Agent.Create" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="hfAgent_ID" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfIsActive" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hfCreatedBy" runat="server" ClientIDMode="Static" />
    <div class="fwt-container">
        <h4 class="fwt-bold fwt-text-deep-orange"><asp:Label ID="lblPageHeading" runat="server" ClientIDMode="Static"></asp:Label></h4>
        <div id="idLabelError" runat="server" clientidmode="Static" class="fwt-container fwt-padding-16 fwt-pale-yellow fwt-border fwt-border-yellow"
            style="display: none">
        </div>
        <div id="idAgents" style="display: block">
            AgentID:
            <asp:Label ID="lblAgent_ID" runat="server" ClientIDMode="Static" Style="font-size: 15px; color: Blue"></asp:Label>
            <fieldset>
                <legend style="font-size: 15px; color: Blue"></legend>
                <div class="fwt-padding-4">
                    <div class="fwt-col l4 m4 s12">
                        <span>Prefix:</span>
                        <asp:DropDownList ID="ddlAgentPrefix" runat="server" CssClass="DropdownNormal" ClientIDMode="Static">
                        </asp:DropDownList>
                    </div>
                    <div class="fwt-clear">
                    </div>
                </div>
                <div class="fwt-padding-4">
                    <div class="fwt-col l4 m4 s12">
                        First Name:<span style="color: Red; font-size: medium">*</span>
                        <asp:TextBox ID="txtAgentFirstName" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                            autocomplete="off" Text=""></asp:TextBox>
                    </div>
                    <div class="fwt-col l4 m4 s12">
                        Middle Name:<span style="color: Red; font-size: medium">&nbsp;</span>
                        <asp:TextBox ID="txtAgentMiddleName" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                            autocomplete="off" Text=""></asp:TextBox>
                    </div>
                    <div class="fwt-col l4 m4 s12">
                        Last Name:<span style="color: Red; font-size: medium">*</span>
                        <asp:TextBox ID="txtAgentLastName" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                            autocomplete="off"></asp:TextBox>
                    </div>
                    <div class="fwt-clear">
                    </div>
                </div>
            </fieldset>
            <div class="fwt-padding-12">
            </div>
            <fieldset>
                <legend style="font-size: 15px; color: Blue">Address Details:</legend>
                <div class="fwt-padding-4">
                    <div class="fwt-col l6 m6 s12">
                        Address1:
                        <asp:TextBox ID="txtAgentAddress1" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                            autocomplete="off" Text=""></asp:TextBox>
                    </div>
                    <div class="fwt-col l6 m6 s12">
                        Address2:
                        <asp:TextBox ID="txtAgentAddress2" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                            autocomplete="off" Text=""></asp:TextBox>
                    </div>
                    <div class="fwt-clear">
                    </div>
                </div>
                <div class="fwt-padding-4">
                    <div class="fwt-col l4 m4 s12">
                        City:
                        <asp:TextBox ID="txtAgentCity" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                            autocomplete="off" Text=""></asp:TextBox>
                    </div>
                    <div class="fwt-col l4 m4 s12">
                        State:
                        <asp:DropDownList ID="ddlAgentState" runat="server" CssClass="DropdownNormal" ClientIDMode="Static">
                        </asp:DropDownList>
                    </div>
                    <div class="fwt-col l4 m4 s12">
                        Country:
                        <asp:TextBox ID="txtAgentCountry" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                            autocomplete="off" Text="India" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="fwt-clear">
                    </div>
                </div>
            </fieldset>
            <div class="fwt-padding-12">
            </div>
            <fieldset>
                <legend style="font-size: 15px; color: Blue">Contact Details:</legend>
                <div class="fwt-padding-4">
                    <div class="fwt-col l4 m4 s12">
                        Phone Number1:<span style="color: Red; font-size: medium">*</span>
                        <asp:TextBox ID="txtAgentPhoneNumber1" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                            autocomplete="off" Text=""></asp:TextBox>
                    </div>
                    <div class="fwt-col l4 m4 s12">
                        Phone Number2:<span style="color: Red; font-size: medium">&nbsp;</span>
                        <asp:TextBox ID="txtAgentPhoneNumber2" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                            autocomplete="off" Text=""></asp:TextBox>
                    </div>
                    <div class="fwt-col l4 m4 s12">
                        Email:<span style="color: Red; font-size: medium">&nbsp;</span>
                        <asp:TextBox ID="txtAgentEmail" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                            autocomplete="off" Text=""></asp:TextBox>
                    </div>
                    <div class="fwt-clear">
                    </div>
                </div>
            </fieldset>
            <fieldset>
                <legend style="font-size: 15px; color: Blue">Personal Details:</legend>
                <div class="fwt-padding-4">
                    <div class="fwt-col l4 m4 s12">
                        Date Of Birth:
                        <asp:TextBox ID="txtAgentDOB" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                            autocomplete="off" Text=""></asp:TextBox>
                    </div>
                    <div class="fwt-col l8 m8 s12">
                        Gender:
                        <asp:RadioButtonList ID="rbtnGender" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal" CssClass="RadioButtonNormal">
                            <asp:ListItem Value="1" Selected="True">Male</asp:ListItem>
                            <asp:ListItem Value="0">Female</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
            </fieldset>
            <div class="fwt-padding-4">
                <!-- for image -->
                <img id="imgAgentImageFileName" runat="server" src="" alt="" />
            </div>
            <div class="fwt-clear">
            </div>
            <div id="idBottomPanel" class="fwt-pale-yellow fwt-text-dark-grey fwt-padding-4 fwt-normal">
                <div class="fwt-col l2 m2 s12">
                    <div class="fwt-bold">
                        Active:
                    </div>
                    <div id="idActive" class="fwt-pointer fwt-hover-indigo fwt-container " onclick="ShowDialog_Reason()">
                    </div>
                </div>
                <div class="fwt-col l2 m2 s12">
                    <div class="fwt-bold">
                        Created Date:
                    </div>
                    <div id="idCreatedDT">
                    </div>
                </div>
                <div class="fwt-col l2 m2 s12">
                    <div class="fwt-bold">
                        CreatedBY:
                    </div>
                    <div id="idCreatedBY">
                    </div>
                </div>
                <div class="fwt-col l2 m2 s12">
                    <div class="fwt-bold">
                        Modified Date:
                    </div>
                    <div id="idModifiedDT">
                    </div>
                </div>
                <div class="fwt-col l2 m2 s12">
                    <div class="fwt-bold">
                        ModifiedBY:
                    </div>
                    <div id="idModifiedBY">
                    </div>
                </div>
                <div class="fwt-col l2 m2 s12">
                    &nbsp;
                    <div id="Div2">
                        &nbsp;
                    </div>
                </div>
                <div class="fwt-clear">
                </div>
            </div>
            <div class="fwt-clear">
            </div>
            <div class="fwt-padding-12 fwt-right ">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ClientIDMode="Static" CssClass="fwt-btn fwt-green fwt-hover-indigo" OnClick="btnSubmit_Click" />
            </div>
            <div class="fwt-clear">
            </div>
        </div>
        <div id="idDialog_Reason" style="display: none;">
            <div class="fwt-padding-4">
                <div id="idDialogReason_Message">
                </div>
                <div class="fwt-padding-4">
                    <asp:TextBox ID="txtReason" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                        autocomplete="off" Text=""></asp:TextBox>
                </div>
                <div class="fwt-right fwt-padding-4">
                    <asp:Button ID="btnSubmitIsActive" runat="server" Text="Submit" ClientIDMode="Static" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">

    <script type="text/javascript">
        var sCreatedBY = "";
        var sAgentID = "";
        $(document).ready(function () {

            $("#txtAgentDOB").datepicker({ dateFormat: 'dd-M-yy' }).datepicker("setDate", new Date());

            if ('<%= Session["LoggedInUser"] %>' != null || '<%= Session["LoggedInUser"] %>' != "") {
                var SessionValue = '<%= Session["LoggedInUser"] %>';
                SessionValue = SessionValue.split('/');
                $("#hfCreatedBy").val(SessionValue[0]);
                sAgentID = SessionValue[1];
            }

            //var jsonVar = {};
            //var url = window.location.toString();
            //if (url.indexOf("agent_id") >= 0) {

            //    url = url.split("agent_id=");
            //    $("#idPageHeading").html("Edit Agent");
            //    $("#hfAgent_ID").val(url[1]);
            //    $("#lblAgent_ID").html($("#hfAgent_ID").val());
            //    jsonVar = { "sAgent_ID": $("#hfAgent_ID").val() };
            //    CallAjaxMethod("Agent_Select", jsonVar, "Agent_Select_Callback");

            //}
            //else {
            //    LastSavedID_Create("Agent", "Label/lblAgent_ID");
            //    $("#idPageHeading").html("Add New Agent");
            //    $("#hfAgent_ID").val("");
            //}
            //$("#idDialogReason_Message").html(__Messages.DialogReason_Message);

            //$("#btnSubmit").click(function (e) {
            //    try {
            //        if (Validation()) {
            //            var sGender = $("#rbtnGender input:checked").val();
            //            var jsonVar = {
            //                "sCreatedBY": sCreatedBY, "sAgent_ID": $("#lblAgent_ID").html(), "sAgentPrefix_Core": $("#ddlAgentPrefix").val(), "sAgentFirstName": $("#txtAgentFirstName").val(), "sAgentMiddleName": $("#txtAgentMiddleName").val(),
            //                "sAgentLastName": $("#txtAgentLastName").val(), "sAgentDOB": $("#txtAgentDOB").val(), "sAgentAddress1": $("#txtAgentAddress1").val(), "sAgentAddress2": $("#txtAgentAddress2").val(),
            //                "sAgentCity": $("#txtAgentCity").val(), "sAgentState_Core": $("#ddlAgentState").val(), "sAgentCountry": $("#txtAgentCountry").val(),
            //                "sAgentPhoneNumber1": $("#txtAgentPhoneNumber1").val(), "sAgentPhoneNumber2": $("#txtAgentPhoneNumber2").val(), "sAgentEmail": $("#txtAgentEmail").val(), "sGender": sGender
            //            }

            //            if ($("#hfAgent_ID").val() != "" && $("#hfAgent_ID").val() != undefined) {
            //                //Update Agent
            //                CallAjaxMethod("Agent_Update", jsonVar, "Agent_Update_Callback");
            //            }
            //            else {
            //                //Insert New Agent
            //                string sMessage =CallAjaxMethod("Agent_Insert", jsonVar, "Agent_Insert_Callback");
            //                SetDefault();
            //            }

            //        }
            //        e.preventDefault();
            //    }
            //    catch (err) {
            //        ErrorMessage("WARNING",err);
            //    }
            //});
            //$("#btnSubmitIsActive").click(function (e) {
            //    try {
            //        var sIsActive = ($("#hfIsActive").val()) == 1 ? 0 : 1;
            //        var jsonVar = { "sAgent_ID": $("#hfAgent_ID").val(), "sIsActive": sIsActive, "sCreatedBY": sCreatedBY, "sReason": $("#txtReason").val() };
            //        CallAjaxMethod("Agent_UpdateIsActive", jsonVar, "Agent_UpdateIsActive_Callback");

            //    }
            //    catch (err) {
            //        ErrorMessage("WARNING",err);
            //    }
            //});
        });
        //function SetDefault() {
        //    try {
        //        $("#ddlAgentPrefix").val(0);
        //        $("#txtAgentFirstName").val("");
        //        $("#txtAgentMiddleName").val("");
        //        $("#txtAgentLastName").val("");
        //        $("#txtAgentDOB").val("");
        //        $("#txtAgentAddress1").val("");
        //        $("#txtAgentAddress2").val("");
        //        $("#txtAgentCity").val("");
        //        $("#ddlAgentState").val(0);
        //        $("#txtAgentCountry").val("");
        //        $("#txtAgentPhoneNumber1").val("");
        //        $("#txtAgentPhoneNumber2").val("");
        //        $("#txtAgentEmail").val("");
        //        $("#txtAgentDOB").datepicker({ dateFormat: 'dd-M-yy' }).datepicker("setDate", new Date());
        //    }
        //    catch (err) {
        //        ErrorMessage("WARNING",err);
        //    }
        //}
        //function Validation() {
        //    try {

        //        if ($("#txtAgentPhoneNumber1").val() == "") {
        //            ErrorMessage("WARNING",__Messages.PhoneNumberBlank_Warrning);
        //            return false;
        //        }
        //        else if (__Messages._mob.test($.trim($("#txtAgentPhoneNumber1").val())) == false) {
        //            ErrorMessage("WARNING",__Messages.PhoneNumberIncorrect_Warrning);
        //            return false;
        //        }

        //        else if ($.trim($("#txtAgentEmail").val())) {
        //            if (__Messages._email.test($.trim($("#txtAgentEmail").val())) == false) {
        //                ErrorMessage("WARNING",__Messages.EmailInValid_Warrning);
        //                return false;
        //            }
        //        }
        //        else if ($("#txtAgentFirstName").val() == "" || $("#txtAgentLastName").val() == "") {
        //            ErrorMessage("WARNING",__Messages.RequiredField_Warrning);
        //            return false;
        //        }
        //        return true;
        //    }
        //    catch (err) {
        //        ErrorMessage("WARNING",err);
        //    }
        //}
        //function Agent_UpdateIsActive_Callback(data) {
        //    try {
        //        var $Data = data.d;
        //        if ($Data.length > 0) {
        //            if ($Data[0].ERROR != null && $Data[0].ERROR != "") {
        //                ErrorMessage("WARNING",$Data[0].ERROR);
        //            }
        //            else {
        //                $("#idDialog_Reason").dialog("close");
        //                $("#txtReason").val("");
        //                $("#hfIsActive").val($("#hfIsActive").val() == 1 ? 0 : 1);
        //                PageActiveCall();
        //                ErrorMessage("WARNING",$Data[0].Message);
        //            }
        //        }
        //    }
        //    catch (err) {
        //        ErrorMessage("WARNING",err);
        //    }
        //}

        //function Agent_Select_Callback(data) {
        //    try {
        //        $Data = data.d;
        //        if ($Data.length > 0) {
        //            if ($Data[0].ERROR != null && $Data[0].ERROR != "") {
        //                //ErrorMessage("WARNING",$Data[0].ERROR);
        //                $("#idAgents").css("display", "none");
        //                $("#idLabelError").css("display", "block");

        //                $("#idLabelError").html($Data[0].ERROR);

        //            }
        //            else {
        //                var sActive = $Data[0].IsActive == 1 ? "Active" : "Inactive";
        //                $("#hfIsActive").val($Data[0].IsActive);

        //                PageActiveCall();
        //                var sModifiedDT = $Data[0].ModifiedDT == "" ? "-" : $Data[0].ModifiedDT;
        //                var sModifiedBY = $Data[0].ModifiedBY == "" ? "-" : $Data[0].ModifiedBY;

        //                $("#idAgentIsActive").css("display", "block").html("Created Date:" + $Data[0].CreatedDT + "&nbsp;&nbsp;&nbsp;" + "CreatedBY:" + $Data[0].CreatedBY + "&nbsp;&nbsp;&nbsp;" + "Active:" + sActive);
        //                $("#ddlAgentPrefix").val($Data[0].Prefix_Core);
        //                $("#txtAgentFirstName").val($Data[0].FirstName);
        //                $("#txtAgentMiddleName").val($Data[0].MiddleName);
        //                $("#txtAgentLastName").val($Data[0].LastName);
        //                $("#txtAgentDOB").val($Data[0].DateOfBirth);
        //                $("#txtAgentAddress1").val($Data[0].Address1);
        //                $("#txtAgentAddress2").val($Data[0].Address2);
        //                $("#txtAgentCity").val($Data[0].City);
        //                $("#ddlAgentState").val($Data[0].State_Core);
        //                $("#txtAgentCountry").val($Data[0].Country_Core);
        //                $("#txtAgentPhoneNumber1").val($Data[0].PhoneNumber1);
        //                $("#txtAgentPhoneNumber2").val($Data[0].PhoneNumber2);
        //                $("#txtAgentEmail").val($Data[0].EMail);
        //                $("#idCreatedDT").html($Data[0].CreatedDT);
        //                $("#idCreatedBY").html($Data[0].CreatedBY);
        //                $("#idModifiedDT").html(sModifiedDT);
        //                $("#idModifiedBY").html(sModifiedBY);
        //                $("#rbtnGender").find("input[value='" + $Data[0].Gender + "']").attr("checked", "checked");
        //            }
        //        }
        //    }
        //    catch (err) {
        //        ErrorMessage("WARNING",err);
        //    }
        //}

        //function Agent_Insert_Callback(data) {
        //    try {
        //        var $Data = data.d;
        //        if ($Data.length > 0) {
        //            if ($Data[0].ERROR != null && $Data[0].ERROR != "") {
        //                ErrorMessage("WARNING",$Data[0].ERROR);
        //            }
        //            else {
        //                SetDefault();
        //                //var jsonVar = { "sID": $("#lblAgent_ID").html(), "sType": "Agent" }
        //                //CallAjaxMethod("LastSavedID_Update", jsonVar, "LastSavedID_Update_Callback");
        //                LastSavedID_Create("Agent", "Label/lblAgent_ID");
        //                ErrorMessage("WARNING",$Data[0].Message);
        //            }
        //        }
        //    }
        //    catch (err) {
        //        ErrorMessage("WARNING",err);
        //    }
        //}
        //function Agent_Update_Callback(data) {
        //    try {

        //        var $Data = data.d;
        //        if ($Data.length > 0) {
        //            if ($Data[0].ERROR != null && $Data[0].ERROR != "") {
        //                ErrorMessage("WARNING",$Data[0].ERROR);
        //            }
        //            else {
        //                ErrorMessage("WARNING",$Data[0].Message);
        //            }
        //        }
        //    }
        //    catch (err) {
        //        ErrorMessage("WARNING",err);
        //    }
        //}
        //function LastSavedID_Update_Callback() {
        //    try {
        //        LastSavedID_Create("Agent", "Label/lblAgent_ID");
        //    }
        //    catch (err) {
        //        ErrorMessage("WARNING",err);
        //    }
        //}
    </script>

</asp:Content>
