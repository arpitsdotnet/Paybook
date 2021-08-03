<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="_CreateCategoryPartial.aspx.cs" Inherits="Paybook.WebUI.Setting._CreateCategoryPartial" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Cateogry</title>
    <link href="<%=Application["Path"] %>_Layouts/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="<%=Application["Path"] %>_Layouts/CSS/jquery-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="<%=Application["Path"] %>_Layouts/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="<%=Application["Path"] %>_Layouts/CSS/Site.css" rel="stylesheet" type="text/css" />
    <link href="/_Layouts/CSS/custom.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hfCategoryPrefix" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hfsIsActive" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hfLogInUser" runat="server" ClientIDMode="Static" />
        <div class="fwt-container">
            <div id="idLabelError" runat="server" clientidmode="Static" class="fwt-container fwt-padding-16 fwt-pale-yellow fwt-border fwt-border-yellow"
                style="display: none">
            </div>
            <div class="fwt-padding-4 fwt-row">
                <div class="fwt-col l4 fwt-padding-top">
                    Select Category :
                </div>
                <div class="fwt-col l8">
                    <asp:DropDownList ID="ddlCategories" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="fwt-padding-4 fwt-row">
                <div class="fwt-col l4 fwt-padding-top">
                    Subcategory Text<span class="fwt-small fwt-text-red">*</span> :
                </div>
                <div class="fwt-col l8">
                    <asp:TextBox ID="txtCategoryDisplayName" runat="server" ClientIDMode="Static" autocomplete="off"
                        CssClass="TextBoxNormal"></asp:TextBox>
                    <div class="ui-tooltip fwt-round-large fwt-red fwt-small" id="idDisplayNameMessage">
                    </div>
                </div>
            </div>
            <div class="fwt-padding-4 fwt-row">
                <div class="fwt-col l4 fwt-padding-top">
                    Core Text :
                </div>
                <div class="fwt-col l8">
                    <asp:TextBox ID="txtCategoryCoreName" runat="server" ClientIDMode="Static" autocomplete="off"
                        CssClass="TextBoxNormal"></asp:TextBox>
                </div>
            </div>
            <div class="fwt-padding-4 fwt-row">
                <div class="fwt-col l4 fwt-padding-top">
                    Order By<span class="fwt-small fwt-text-red">*</span> :
                </div>
                <div class="fwt-col l8">
                    <asp:TextBox ID="txtOrderBY" runat="server" ClientIDMode="Static" autocomplete="off"
                        CssClass="TextBoxNormal"></asp:TextBox>
                    <div class="ui-tooltip fwt-round-large fwt-red fwt-small" id="idOrderBYMessage">
                    </div>
                </div>
            </div>
            <div class="fwt-padding-4 fwt-center">
                <div class="fwt-col l4 fwt-padding-top">
                </div>
                <div class="fwt-col l8 ">
                    <asp:Button ID="btnSave" runat="server" CssClass="fwt-btn fwt-round fwt-btn-block fwt-green fwt-hover-indigo"
                        Text="Save" ClientIDMode="Static" OnClick="btnSave_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
