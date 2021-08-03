<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="_CreateNotePartial.aspx.cs" Inherits="Paybook.WebUI.Note._CreateNotePartial" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Note Partial</title>
    <link href="<%=Application["Path"] %>_Layouts/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="<%=Application["Path"] %>_Layouts/CSS/jquery-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="<%=Application["Path"] %>_Layouts/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="<%=Application["Path"] %>_Layouts/CSS/Site.css" rel="stylesheet" type="text/css" />
    <link href="/_Layouts/CSS/custom.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <asp:HiddenField ID="hfLogInUser" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hfDataID" runat="server" ClientIDMode="Static" />
        <div class="fwt-container">
            <div class="fwt-padding-4 fwt-row">
                <div class="fwt-col l4 fwt-padding-top">
                    Vehicle Number : 
                </div>
                <div class="fwt-col l8">
                    <asp:TextBox ID="txtVehicleNo" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" AutoComplete="off"></asp:TextBox>
                </div>
            </div>
            <div class="fwt-padding-4 fwt-row">
                <div class="fwt-col l4 fwt-padding-top">
                    Name :
                </div>
                <div class="fwt-col l8">
                    <asp:TextBox ID="txtName" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" AutoComplete="off"></asp:TextBox>
                </div>
            </div>
            <div class="fwt-padding-4 fwt-row">
                <div class="fwt-col l4 fwt-padding-top">
                    Mobile Number :
                </div>
                <div class="fwt-col l8">
                    <asp:TextBox ID="txtMobileNumber" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" AutoComplete="off"></asp:TextBox>
                </div>
            </div>
            <div class="fwt-padding-4 fwt-row">
                <div class="fwt-col l4 fwt-padding-top">
                    Work :
                </div>
                <div class="fwt-col l8">
                    <asp:TextBox ID="txtWork" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" AutoComplete="off"></asp:TextBox>
                </div>
            </div>
            <div class="fwt-padding-4 fwt-row">
                <div class="fwt-col l4 fwt-padding-top">
                    Total Amount :
                </div>
                <div class="fwt-col l8">
                    <asp:TextBox ID="txtTotalAmount" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" AutoComplete="off"></asp:TextBox>
                </div>
            </div>
            <div class="fwt-padding-4 fwt-row">
                <div class="fwt-col l4 fwt-padding-top">
                    Awak :
                </div>
                <div class="fwt-col l8">
                    <asp:TextBox ID="txtAwak" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" AutoComplete="off"></asp:TextBox>
                </div>
            </div>
            <div class="fwt-padding-4 fwt-row">
                <div class="fwt-col l4 fwt-padding-top">
                    Jawak :
                </div>
                <div class="fwt-col l8">
                    <asp:TextBox ID="txtJawak" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" AutoComplete="off"></asp:TextBox>
                </div>
            </div>
            <div class="fwt-padding-4 fwt-row">
                <div class="fwt-col l4 fwt-padding-top">
                    Balance :
                </div>
                <div class="fwt-col l8">
                    <asp:TextBox ID="txtBalance" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" AutoComplete="off"></asp:TextBox>
                </div>
            </div>
            <div class="fwt-padding-4">
                <div class="fwt-col l4 fwt-padding-top">
                    Notes :
                </div>
                <div class="fwt-col l8">
                    <asp:TextBox ID="txtDailyNotes" runat="server" CssClass="TextBoxNormal" TextMode="MultiLine" Height="100px" ClientIDMode="Static" AutoComplete="off"></asp:TextBox>
                </div>
            </div>
            <div class="fwt-padding-4 fwt-center">
                <div class="fwt-col l4 fwt-padding-top">
                </div>
                <div class="fwt-col l8 ">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="fwt-btn fwt-round fwt-btn-block fwt-green fwt-hover-indigo" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
