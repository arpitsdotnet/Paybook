<%@ Page Title="" Language="C#" MasterPageFile="~/loginmaster.master" AutoEventWireup="true"
    CodeBehind="login.aspx.cs" Inherits="Paybook.WebUI.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headLogin" runat="server">
    <link href="_Layouts/CSS/login.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainLoginContent" runat="server">
    <asp:HiddenField ID="hfPrevPage" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfCaptchaResult" runat="server" ClientIDMode="Static" />

    <div class="fwt-container">
        <div class="login">
            <div class="login-triangle"></div>
            <h2 class="login-header">Log in</h2>
            <div class="login-container">
                <div id="idLabelError" runat="server" clientidmode="Static" class="fwt-pale-yellow fwt-border fwt-border-yellow"
                    style="display: none">
                </div>
                <div class="fwt-padding-12">
                    <asp:TextBox ID="txtUserName" runat="server" autocomplete="off" CssClass="TextBoxNormal" placeholder="UserName"></asp:TextBox>
                </div>
                <div class="fwt-padding-12">
                    <asp:TextBox ID="txtPassword" runat="server" autocomplete="off" CssClass="TextBoxNormal" placeholder="Password"
                        TextMode="Password"></asp:TextBox>
                </div>
                <div class="fwt-padding-4">
                    <div>Result of Below Calculation :</div>
                    <div>
                        <div class="fwt-col l8 m8 s8 fwt-right-align">
                            <asp:Label ID="lblCaptcha" runat="server" Text="Label" CssClass="CaptchaText"></asp:Label>
                        </div>
                        <div class="fwt-col l4 m4 s4" style="padding: 0px 4px">
                            <asp:TextBox ID="txtCaptcha" runat="server" CssClass="TextBoxNormal" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="fwt-clear"></div>
                </div>
                <div class="fwt-padding-12">
                    <asp:Button ID="btnLogin" runat="server" Text="Log In" OnClick="btnLogin_Click" Class="loginsubmit" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            //google.charts.setOnLoadCallback(PaymentChartLoad_Select_Complete);

        });

    </script>
</asp:Content>
