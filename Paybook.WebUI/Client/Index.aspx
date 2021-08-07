<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Paybook.WebUI.Client.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="hfCustomersGridPageNumber" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfCustomer_ID" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfSearchBy" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfSearchText" runat="server" ClientIDMode="Static" />
    <div class="container-fluid">
        <div class="row bg-secondary">
            <div class="col-lg-1 col-xs-4">
                <h2>Clients</h2>
            </div>
            <div class="col-lg-2 col-xs-4 pt-3">
                <div style="position: relative;">
                    <asp:TextBox ID="txtAgencySearch" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" placeholder="Agency Name Search"
                        autocomplete="off"></asp:TextBox>
                    <div id="idAgencyCross" class="div-cross fwt-right"><i class="fa fa-times"></i></div>
                </div>
            </div>
            <div class="col-lg-2 col-xs-4 pt-3">
                <div style="position: relative;">
                    <asp:TextBox ID="txtSearchText" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" placeholder="Customer Name Search"
                        autocomplete="off"></asp:TextBox>
                    <div id="idCross" class="div-cross fwt-right"><i class="fa fa-times"></i></div>
                </div>
            </div>
            <div class="col-lg-7 col-xs-12 text-right">
                <div class="btn-group" role="group">
                    <button type="button" clientidmode="Static" class="btn btn-primary fwt-btn-height" title="Setting" onclick="">
                        <i class="fa fa-ellipsis-h fwt-large"></i></button>
                    <button type="button" clientidmode="Static" class="btn btn-primary fwt-btn-height" title="Sync" onclick="location.href=location.href;">
                        <i class="fa fa-refresh fwt-large"></i></button>
                    <button type="button" clientidmode="Static" class="btn btn-primary fwt-btn-height" title="Add a Note" onclick="return OpenPartialPagePopup('agency/create','CREATE AGENCY');">
                        <i class="fa fa-plus fwt-large"></i>&nbsp; CREATE AGENCY</button>
                    <button type="button" clientidmode="Static" class="btn btn-primary fwt-btn-height"
                        title="Add a Note" onclick="return OpenPartialPagePopup('client/create','CREATE CLIENT');">
                        <i class="fa fa-plus fwt-large"></i>&nbsp; CREATE CLIENT</button>
                </div>
            </div>
        </div>
    </div>

    <div class="container-fluid pt-3">
        <div id="idLabelError" runat="server" clientidmode="Static" class="fwt-container fwt-padding-16 fwt-pale-yellow fwt-border fwt-border-yellow"
            style="display: none">
        </div>
        <div class="row fwt-padding-4 " id="idPageNumber">
            <div class="col-lg-6 col-md-12">
                &nbsp;
            </div>
            <div class="col-lg-6 col-md-12">
                <div class="w3-bar fwt-right-align">
                    <span class="fwt-button">
                        <button id="btnPrivious" clientidmode="Static" class="fwt-btn fwt-round fwt-ripple fwt-dropdown-click fwt-light-green">
                            &nbsp;<i class="fa fa-backward"></i>&nbsp;</button>
                    </span>
                    <span class="fwt-button">
                        <asp:Label ID="lblCustomersPageNumber" ClientIDMode="Static" runat="server"></asp:Label></span>
                    <span class="fwt-button">
                        <button id="btnNext" clientidmode="Static" class="fwt-btn fwt-round fwt-ripple fwt-dropdown-click fwt-light-green">
                            &nbsp;<i class="fa fa-forward"></i>&nbsp;</button>
                    </span>
                </div>
            </div>
        </div>
        <div class="fwt-padding-4">
            <div id="div_HeadertblCustomers">
            </div>
            <div class="table-Scroll" id="div_ScrolltblCustomers">
                <table id="tblCustomers" border="0" width="100%" style="word-wrap: break-word;" class="table-main fwt-table">
                    <thead>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
                <div id="divGridLoadingtblCustomers" style="text-align: center;">
                </div>
                <asp:Label ID="lblGridMessagetblCustomers" runat="server" CssClass="Error" ClientIDMode="Static"></asp:Label>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">

    <script src="<%=Application["Path"] %>_Layouts/JS/custom_customer.js" type="text/javascript"></script>
    <script type="text/javascript">
        var sOrderBy = "";
        XmlData = "";
        $(document).ready(function () {
            $("#hfSearchBy").val("Customer");
            SetCustomersGridPageNumberBlank();
            GetAllCustomers(sOrderBy);
            $('#btnNext').click(function (e) {
                try {
                    //var iTotalRows = parseInt($("#lblCustomersPageNumber").text().split('of')[1]);

                    //var iTotalPageNumber = Math.ceil(iTotalRows / 10);
                    //var iCurrentPageNumber = parseInt($('#hfCustomersGridPageNumber').val());
                    var iTotalPageNumber = parseInt($("#lblCustomersPageNumber").text().split('of')[1]);
                    var iCurrentPageNumber = parseInt($('#hfCustomersGridPageNumber').val());

                    if (iCurrentPageNumber >= 0 && iCurrentPageNumber < iTotalPageNumber - 1) {
                        if (IsProcessingHomeGrid)
                            return false;
                        else {
                            IsProcessingHomeGrid = true;
                            var iCustomerGridPageNumber = $('#hfCustomersGridPageNumber').val() == "" ? 0 : parseInt($('#hfCustomersGridPageNumber').val());
                            iCustomerGridPageNumber = iCustomerGridPageNumber + 1;
                            $('#hfCustomersGridPageNumber').val(iCustomerGridPageNumber);
                            GetAllCustomers(sOrderBy);
                        }
                    }
                }
                catch (err) {
                    ShowMessage(err);
                }
                e.preventDefault();

            });
            $('#btnPrivious').click(function (e) {
                try {
                    if ($('#hfCustomersGridPageNumber').val() > 0) {
                        if (IsProcessingHomeGrid)
                            return false;
                        else {
                            IsProcessingHomeGrid = true;
                            var iCustomerGridPageNumber = parseInt($('#hfCustomersGridPageNumber').val());
                            iCustomerGridPageNumber = iCustomerGridPageNumber - 1;
                            if (iCustomerGridPageNumber == 0)
                                $("#tblCustomers thead").html("");
                            $('#hfCustomersGridPageNumber').val(iCustomerGridPageNumber);
                            GetAllCustomers(sOrderBy);
                        }
                    }
                }
                catch (err) {
                    ShowMessage(err);
                }
                e.preventDefault();

            });
            $("#txtAgencySearch").keyup(function (e) {
                try {
                    $("#txtSearchText").val("");
                    var sSearchBY = $("#txtAgencySearch").val().length == 0 ? "Customer" : "Agency";
                    if ($("#txtAgencySearch").val().length > 1 || $("#txtAgencySearch").val().length == 0) {
                        $("#hfSearchBy").val(sSearchBY);
                        $("#hfSearchText").val($("#txtAgencySearch").val());
                        SetCustomersGridPageNumberBlank();
                        GetAllCustomers(sOrderBy);
                    }
                }
                catch (err) {
                    ShowMessage(err);
                }
                e.preventDefault();
            });
            //Search by customer
            $("#txtSearchText").keyup(function (e) {
                try {
                    $("#txtAgencySearch").val("");
                    if ($("#txtSearchText").val().length > 1 || $("#txtSearchText").val().length == 0) {
                        $("#hfSearchBy").val("Customer");
                        $("#hfSearchText").val($("#txtSearchText").val());
                        SetCustomersGridPageNumberBlank();
                        GetAllCustomers(sOrderBy);
                    }
                }
                catch (err) {
                    ShowMessage(err);
                }
                e.preventDefault();
            });
            $("#idCross").click(function (e) {
                try {
                    $("#txtSearchText").val("");
                    $("#hfSearchText").val("");
                    $("#hfSearchBy").val("Customer");
                    SetCustomersGridPageNumberBlank();
                    GetAllCustomers(sOrderBy);
                }
                catch (err) {
                    ShowMessage(err);
                }
                e.preventDefault();
            });

            $("#idAgencyCross").click(function (e) {
                try {
                    $("#txtAgencySearch").val("");
                    $("#hfSearchText").val("");
                    $("#hfSearchBy").val("Customer");
                    SetCustomersGridPageNumberBlank();
                    GetAllCustomers(sOrderBy);
                }
                catch (err) {
                    ShowMessage(err);
                }
                e.preventDefault();
            });
        });
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
