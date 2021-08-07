<%@ Page Title="" Language="C#" MasterPageFile="~/Identity/Identity.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Paybook.WebUI.Identity.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headLogin" runat="server">

    <link href="/_Layouts/CSS/login.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainLoginContent" runat="server">

    <asp:HiddenField ID="hfPrevPage" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfCaptchaResult" runat="server" ClientIDMode="Static" />

    <div class="container">
        <div id="idLabelError" runat="server" clientidmode="Static" class="fwt-pale-yellow fwt-border fwt-border-yellow"
            style="display: none">
        </div>
    </div>
    <div class="container card fwt-round bg-secondary m-auto ui-dialog-shadow mb-3" style="width: 30rem;">
        <div class="card-header h2 text-primary text-center">Welcome Back!</div>
        <div class="card-body">
            <div class="form-group">
                    <label class="text-primary" for="Username">Username :</label>
                    <asp:TextBox ID="txtUserName" runat="server" autocomplete="off" CssClass="TextBoxNormal" placeholder="Username ..."></asp:TextBox>
                </div>
                <div class="form-group">
                    <label class="text-primary" for="Password">Password :</label>
                    <asp:TextBox ID="txtPassword" runat="server" autocomplete="off" CssClass="TextBoxNormal" placeholder="Password ..."
                        TextMode="Password"></asp:TextBox>
                </div>
                <div class="form-group row">
                    <div class="col-lg-5">
                        <label class="text-primary" for="Captcha">Captcha :</label>
                    </div>
                    <div class="col-lg-3 text-right">
                        <asp:Label ID="lblCaptcha" runat="server" Text="Label" CssClass="CaptchaText"></asp:Label>
                    </div>
                    <div class="col-lg-4">
                        <asp:TextBox ID="txtCaptcha" runat="server" CssClass="TextBoxNormal" autocomplete="off" placeholder="?"></asp:TextBox>
                    </div>
                </div>
                <div class="fwt-padding-12">
                    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" Class="btn-info btn-sm text-uppercase  fwt-round btn-block" />
                </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            //google.charts.setOnLoadCallback(PaymentChartLoad_Select_Callback);

        });

    </script>

</asp:Content>
