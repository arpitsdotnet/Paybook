<%@ Page Title="" Language="C#" MasterPageFile="~/Identity/Identity.Master" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="Paybook.WebUI.Identity.Logout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headLogin" runat="server">

     <link href="_Layouts/CSS/login.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainLoginContent" runat="server"><div class="fwt-container">

        <div class="login">
            <div class="login-triangle"></div>
            <h2 class="login-header">Log out</h2>
            <div class="login-container" style="padding:26px">
                <div id="idLabelError" runat="server" clientidmode="Static" class="fwt-container fwt-padding-16 fwt-pale-yellow fwt-border fwt-border-yellow"
                    style="display: none">
                </div>
                <div>
                    You are successfully logged out. If you want to login again, please <a href="login">click here</a>
                </div>
            </div>
        </div>      
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
