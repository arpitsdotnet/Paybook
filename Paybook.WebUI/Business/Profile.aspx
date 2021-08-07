<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Paybook.WebUI.Business.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="hfCompanyLogo_Image" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfLogInUser" runat="server" ClientIDMode="Static" />
    <div class="fwt-container">
       <h4>Company Profile:
        </h4>
        <div id="idLabelError" runat="server" clientidmode="Static" class="fwt-container fwt-padding-16 fwt-pale-yellow fwt-border fwt-border-yellow"
            style="display: none">
        </div>
        <div class="fwt-clear fwt-padding-4"></div>
        <div class="fwt-col l8 m8 s12">
            <%--    <h5 class="fwt-container fwt-light-grey">Company Information: </h5>
            <div class="fwt-padding-8"></div>--%>
            <div class="fwt-col l4 m4 s12">
                <div class="fwt-container">Company Name: </div>
            </div> 
            <div class="fwt-col l8 m8 s12">
                <asp:TextBox ID="txtCompanyName" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" autocomplete="off"></asp:TextBox>
            </div>
            <div class="fwt-clear fwt-padding-4"></div>
            <div class="fwt-col l4 m4 s12">
                <div class="fwt-container">Founded Date:</div>
            </div>
            <div class="fwt-col l8 m8 s12">
                <asp:TextBox ID="txtCompanyFounderDate" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" autocomplete="off"></asp:TextBox>
            </div>
            <div class="fwt-clear fwt-padding-4"></div>
            <div class="fwt-col l4 m4 s12">
                <div class="fwt-container">Address: </div>
            </div>
            <div class="fwt-col l8 m8 s12">
                <asp:TextBox ID="txtCompanyAddress1" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" autocomplete="off"></asp:TextBox>
            </div>
            <div class="fwt-clear fwt-padding-4"></div>
            <div class="fwt-col l4 m4 s12">
                <div class="fwt-container">City: </div>
            </div>
            <div class="fwt-col l8 m8 s12">
                <asp:TextBox ID="txtCompanyCity" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" autocomplete="off" Text=""></asp:TextBox>
            </div>
            <div class="fwt-clear fwt-padding-4"></div>
            <div class="fwt-col l4 m4 s12">
                <div class="fwt-container">State: </div>
            </div>
            <div class="fwt-col l8 m8 s12">
                <asp:DropDownList ID="ddlCompanyState" runat="server" CssClass="DropdownNormal" ClientIDMode="Static">
                </asp:DropDownList>
            </div>
            <div class="fwt-clear fwt-padding-4"></div>
            <div class="fwt-col l4 m4 s12">
                <div class="fwt-container">Country: </div>
            </div>
            <div class="fwt-col l8 m8 s12">
                <asp:TextBox ID="txtCompanyCountry" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                    Text="India" Enabled="false"></asp:TextBox>
            </div>
            <div class="fwt-clear fwt-padding-4"></div>
            <div class="fwt-col l4 m4 s12">
                <div class="fwt-container">Phone Number: </div>
            </div>
            <div class="fwt-col l8 m8 s12">
                <asp:TextBox ID="txtCompanyPhoneNumber1" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                    autocomplete="off" Text=""></asp:TextBox>
            </div>
            <div class="fwt-clear fwt-padding-4"></div>
            <div class="fwt-col l4 m4 s12">
                <div class="fwt-container">Email: </div>
            </div>
            <div class="fwt-col l8 m8 s12">
                <asp:TextBox ID="txtCompanyEmail" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                    autocomplete="off"></asp:TextBox>
            </div>
            <div class="fwt-clear fwt-padding-4"></div>
            <div class="fwt-col l4 m4 s12">
                <div class="fwt-container">Fax No: </div>
            </div>
            <div class="fwt-col l8 m8 s12">
                <asp:TextBox ID="txtCompanyFaxNumber" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                    autocomplete="off" Text=""></asp:TextBox>
            </div>
             <div class="fwt-clear fwt-padding-4"></div>
            <div class="fwt-col l4 m4 s12">
                <div class="fwt-container">GSTIN No: </div>
            </div>
            <div class="fwt-col l8 m8 s12">
                <asp:TextBox ID="txtGSTIN" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                    autocomplete="off" Text=""></asp:TextBox>
            </div>
            <div class="fwt-clear fwt-padding-4"></div>
            <div class="fwt-col l4 m4 s12">
                <div class="fwt-container">Company Logo: </div>
            </div>
            <div class="fwt-col l6 m6 s8">
                <asp:TextBox ID="txtImageUrl" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" autocomplete="off"></asp:TextBox>

            </div>
            <div class="fwt-col l2 m2 s4">
                <asp:Button ID="btnUpload" runat="server" ClientIDMode="Static" CssClass="fwt-btn fwt-hover-indigo fwt-amber" OnClientClick="javascript:return BrowseImage();" Text="Browse" />
                <asp:FileUpload ID="fuImageUpload" runat="server" ClientIDMode="Static" Style="display: none;" />
            </div>
            <div class="fwt-clear fwt-padding-16"></div>
            <h5 id="idLogin" class="fwt-container">Login Information: </h5>
            <div class="fwt-padding-4"></div>
            <div class="fwt-col l4 m4 s12">
                <div class="fwt-container">Username: </div>
            </div>
            <div class="fwt-col l8 m8 s12">
                <asp:TextBox ID="txtUsername" runat="server" CssClass="TextBoxNormal" autocomplete="off"></asp:TextBox>
            </div>
            <div class="fwt-clear fwt-padding-4"></div>
            <div class="fwt-col l4 m4 s12">
                <div class="fwt-container">Password: </div>
            </div>
            <div class="fwt-col l8 m8 s12">
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="TextBoxNormal"></asp:TextBox>
            </div>
            <div class="fwt-clear fwt-padding-4"></div>
            <div class="fwt-col l4 m4 s12">
                <div class="fwt-container">Confirm Password: </div>
            </div>
            <div class="fwt-col l8 m8 s12">
                <asp:TextBox ID="txtPasswordConfirm" runat="server" TextMode="Password" CssClass="TextBoxNormal"></asp:TextBox>
            </div>
            <div class="fwt-clear fwt-padding-4"></div>
            <div class="fwt-col l4 m4 s4">
            </div>
            <div class="fwt-col l6 m6 s4">
            </div>
            <div class="fwt-col l2 m2 s4 fwt-right">
                <asp:Button ID="btnSubmitAndUpdate" runat="server" CssClass="fwt-btn fwt-green fwt-hover-indigo"
                    Text="Update" ClientIDMode="Static" OnClick="btnSubmitAndUpdate_Click" />
            </div>
        </div>
        <div class="fwt-col l4 m4 s12">
            <div class="fwt-container">
                <div class="fwt-padding-4 fwt-center">
                </div>
                <div class="fwt-clear fwt-padding-12"></div>
                <div class="fwt-card-8 fwt-center">
                    <asp:Image ID="imgCompanyLogo" runat="server" ImageUrl="_Layouts/IMG/img_avatar.png"
                        AlternateText="Person" Width="30%" ClientIDMode="Static" />
                </div>
                <div class="fwt-padding-4">
                </div>

            </div>
        </div>
        <div class="fwt-clear"></div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtCompanyFounderDate").datepicker({ dateFormat: 'dd-M-yy' }).datepicker("setDate", new Date());

            $("#fuImageUpload").change(function () {
                if ($("#fuImageUpload").val() != "") {
                   var sImgFakePath = $("#fuImageUpload").val();
                   var sImgName = sImgFakePath.split("\\");               
                    sImgName = sImgName.pop();                 
                    $("#txtImageUrl").val(sImgName);
                }
            });
            //$("#btnSubmitAndUpdate").click(function (e) {
            //    try {
            //        if (Validation()) {
            //            var jsonVar = { "sCreatedBY": $("#hfLogInUser").val(), "sCompanyName": $("#txtCompanyName").val(), "sFounded_Date": $("#txtCompanyFounderDate").val(), "sCompanyAddress1": $("#txtCompanyAddress1").val(), "sCompanyAddress2": $("#txtCompanyAddress2").val(), "sCompanyCity": $("#txtCompanyCity").val(), "sCompanyState_Core": $("#ddlCompanyState").val(), "sCompanyCountry": $("#txtCompanyCountry").val(), "sCompanyPhoneNumber1": $("#txtCompanyPhoneNumber1").val(), "sCompanyPhoneNumber2": $("#txtCompanyPhoneNumber2").val(), "sCompanyFaxNumber": $("#txtCompanyFaxNumber").val(), "sCompanyEmail": $("#txtCompanyEmail").val(), "sImageFileName": $("#hfCompanyLogo_Image").val() };
            //            //if ($("#btnSubmitAndUpdate").val() == "Submit") {

            //            //    CallAjaxMethod("CompanyProfile_Insert", jsonVar, "CompanyProfile_Insert_Callback");
            //            //    SetDefault();
            //            //}
            //            //else {

            //            CallAjaxMethod("CompanyProfile_Update", jsonVar, "CompanyProfile_Update_Callback");
            //            //}

            //        }
            //    }
            //    catch (err) {
            //        ShowMessage(err);
            //    }
            //    e.preventDefault();
            //});
        });
        function BrowseImage() {
            $("#fuImageUpload").click();
            return false;
        }
        //function SetDefault() {
        //    try {
        //        $("#txtCompanyName").val("");
        //        $("#txtCompanyFounderDate").datepicker({ dateFormat: 'dd-M-yy' }).datepicker("setDate", new Date());
        //        $("#txtCompanyAddress1").val("");
        //        $("#txtCompanyAddress2").val("");
        //        $("#txtCompanyCity").val("");
        //        $("#ddlCompanyState").val(0);
        //        $("#txtCompanyPhoneNumber1").val("");
        //        $("#txtCompanyPhoneNumber2").val("");
        //        $("#txtCompanyFaxNumber").val("");
        //        $("#txtCompanyEmail").val("");
        //        $("#hfCompanyLogo_Image").val("");
        //        $("#imgCompanyLogo").attr('src', '_Layouts/IMG/img_avatar.png');
        //    }
        //    catch (err) {
        //        ShowMessage("There is an issue calling the function (SetDefault)" + err);
        //    }
        //}

        //function Validation() {
        //    try {

        //        if ($("#txtCompanyPhoneNumber1").val() == "") {
        //            ReadXMLMessage("BSW009", "ReadXMLMessage_Callback");
        //            return false;
        //        }
        //        else if (__Messages._mob.test($.trim($("#txtCompanyPhoneNumber1").val())) == false) {
        //            ReadXMLMessage("BSW010", "ReadXMLMessage_Callback");
        //            return false;
        //        }
        //        else if ($("#txtCompanyEmail").val() == "") {
        //            ReadXMLMessage("BSW011", "ReadXMLMessage_Callback");
        //            return false;
        //        }
        //        else if (__Messages._email.test($.trim($("#txtCompanyEmail").val())) == false) {
        //            ReadXMLMessage("BSW012", "ReadXMLMessage_Callback");
        //            return false;
        //        }
        //        else if ($("#txtCompanyName").val() == "") {
        //            ReadXMLMessage("BSW008", "ReadXMLMessage_Callback");

        //            return false;
        //        }
        //        return true;
        //    }
        //    catch (err) {
        //        ShowMessage("There is an issue calling the function (Validation)" + err);
        //    }
        //}

        //function CompanyProfile_Insert_Callback(data) {
        //    try {
        //        var $Data = data.d;
        //        if ($Data.length > 0) {
        //            if ($Data[0].ERROR != null && $Data[0].ERROR != "") {
        //                ShowMessage($Data[0].ERROR);
        //            }
        //            else {
        //                ShowMessage($Data[0].Message);
        //            }
        //        }
        //    }
        //    catch (err) {
        //        ShowMessage("There is an issue calling the function (CompanyProfile_Insert_Callback)" + err);
        //    }
        //}
        //function CompanyProfile_Update_Callback(data) {
        //    try {
        //        var $Data = data.d;
        //        if ($Data.length > 0) {
        //            if ($Data[0].ERROR != null && $Data[0].ERROR != "") {
        //                ShowMessage($Data[0].ERROR);
        //            }
        //            else {
        //                ShowMessage($Data[0].Message);
        //            }
        //        }
        //    }
        //    catch (err) {
        //        ShowMessage("There is an issue calling the function (CompanyProfile_Update_Callback)" + err);
        //    }
        //}       
        function ReadXMLMessage_Callback(data) {
            try {
                ShowMessage(data);
            }
            catch (err) {
                ShowMessage("ERROR", "There is an issue calling the function (SetDefault)" + err);
            }
        }
    </script>

</asp:Content>
