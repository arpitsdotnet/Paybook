<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Paybook.WebUI.Invoice.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="hfInvoicesGridPageNumber" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfCustomer_ID" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfCategory_Core" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfParticular" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfSet_btnReset" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfLogInUser" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfLogInUser_ID" runat="server" ClientIDMode="Static" />
    <div class="fwt-container fwt-light-grey">
        <div class="fwt-row">
            <div class="fwt-col l2 ">
                <h2>INVOICES</h2>
            </div>
            <div class="fwt-right-align">
                <button clientidmode="Static" class="fwt-btn fwt-round fwt-dark-grey fwt-hover-green fwt-btn-height" title="Sync" onclick="location.href=location.href;">
                    &nbsp;<i class="fa fa-refresh fwt-large"></i>&nbsp;</button>
                <button clientidmode="Static" class="fwt-btn fwt-round fwt-dark-grey fwt-hover-green fwt-btn-height" title="Add a Note" onclick="return OpenPartialPagePopup('notes/create','ADD NOTE');">
                    &nbsp;<i class="fa fa-plus fwt-large"></i>&nbsp; ADD A INVOICE&nbsp;</button>
            </div>
        </div>
    </div>
    <div class="fwt-container">

        <div id="idLabelError" runat="server" clientidmode="Static" class="fwt-container fwt-padding-16 fwt-pale-yellow fwt-border fwt-border-yellow"
            style="display: none">
        </div>
        <div class="fwt-padding-4">
            <div class="fwt-col l3 m3 s12">
                <span>Agency:</span>
                <asp:DropDownList ID="ddlAgency" runat="server" ClientIDMode="Static" CssClass="DropdownNormal">
                </asp:DropDownList>
            </div>
            <div class="fwt-col l3 m3 s12">
                <div><span>Customer:</span></div>
                <div>
                    <asp:DropDownList ID="ddlCustomers" runat="server" ClientIDMode="Static" CssClass="DropdownNormal">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="fwt-col l3 m3 s12">
                <div><span>Type:</span></div>
                <div>
                    <asp:DropDownList ID="ddlCategories" runat="server" ClientIDMode="Static" CssClass="DropdownNormal">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="fwt-col l3 m3 s12">
                <div><span>Status:</span></div>
                <div>
                    <asp:DropDownList ID="ddlInvoiceStatus" runat="server" ClientIDMode="Static" CssClass="DropdownNormal">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="fwt-clear">
            </div>
        </div>
        <div class="fwt-padding-4">
            <div class="fwt-col l3 m3 s12">
                <%-- <div><span>Particular:</span></div>
                <div>
                    <asp:TextBox ID="txtParticular" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                        autocomplete="off"></asp:TextBox>
                </div>
                --%>
                <div><span>Receipt ID:</span></div>
                <div>
                    <asp:TextBox ID="txtReceiptID" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                        autocomplete="off"></asp:TextBox>
                </div>
            </div>
            <div class="fwt-col l3 m3 s12">
                <div><span>From:</span></div>
                <div>
                    <asp:TextBox ID="txtDateFrom" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                        autocomplete="off" Text=""></asp:TextBox>
                </div>
            </div>
            <div class="fwt-col l3 m3 s12">
                <div><span>To:</span></div>
                <div>
                    <asp:TextBox ID="txtDateTo" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                        autocomplete="off" Text=""></asp:TextBox>
                </div>
            </div>
            <div class="fwt-col l3 m3 s12" style="padding-top: 15px">
                <%--  <div class="fwt-left">
                <asp:LinkButton ID="lnkInvoiceCreate" runat="server" CssClass="fwt-btn fwt-deep-purple fwt-hover-indigo"
                    PostBackUrl="~/invoice_add.aspx" Text="<i class='fa fa-plus' ></i> Add Invoice"></asp:LinkButton>
            </div>--%>
                <asp:Button ID="btnSearch" runat="server" CssClass="fwt-btn fwt-green fwt-hover-indigo"
                    Text="Search Invoice" ClientIDMode="Static" />
                <asp:Button ID="btnReset" runat="server" CssClass="fwt-btn fwt-grey"
                    Text="Reset" ClientIDMode="Static" />

            </div>
            <div class="fwt-clear">
            </div>

        </div>
        <div class="fwt-padding-12 ">
            <div id="idSearchAbstracts" class="fwt-col l10 m10 s12">
                &nbsp;
            </div>
            <div class="fwt-clear"></div>
        </div>

        <div class="fwt-padding-4" id="idPageNumber">
            <div class="fwt-col l6 m12 s12">
                &nbsp;
            </div>
            <div class="fwt-col l6 m12 s12">
                <div class="w3-bar fwt-right-align">
                    <span class="fwt-button">
                        <button id="btnPrivious" clientidmode="Static" class="fwt-btn fwt-round fwt-ripple fwt-dropdown-click fwt-light-green">
                            &nbsp;<i class="fa fa-backward"></i>&nbsp;</button>
                    </span>
                    <span class="fwt-button">
                        <asp:Label ID="lblPageNumber" ClientIDMode="Static" runat="server"></asp:Label></span>
                    <span class="fwt-button">
                        <button id="btnNext" clientidmode="Static" class="fwt-btn fwt-round fwt-ripple fwt-dropdown-click fwt-light-green">
                            &nbsp;<i class="fa fa-forward"></i>&nbsp;</button>
                    </span>
                </div>

            </div>
            <div class="fwt-clear"></div>
        </div>
        <%--        <div class="fwt-padding-4 ">
            <div class="fwt-col l2 m3 s12" id="idPageNumber">
                <asp:Label ID="lblPageNumber" ClientIDMode="Static" runat="server"></asp:Label>

            </div>
            <div class="fwt-col l10 m9 s6 fwt-NavigationButtons">
                <button id="btnPrivious" clientidmode="Static" class="fwt-btn fwt-ripple fwt-dropdown-click fwt-amber">
                    <i class="fa fa-backward"></i>&nbsp;</button>
                <button id="btnNext" clientidmode="Static" class="fwt-btn fwt-ripple fwt-dropdown-click fwt-amber">
                    <i class="fa fa-forward"></i>&nbsp;</button>
            </div>
            <div class="fwt-clear">
            </div>
        </div>--%>
        <div class="fwt-padding-4">
            <div id="div_HeadertblInvoices">
            </div>
            <div class="table-Scroll" id="div_ScrolltblInvoices">
                <table id="tblInvoices" border="0" width="100%" style="word-wrap: break-word;" class="table-main fwt-table">
                    <thead>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
                <div id="divGridLoading" style="text-align: center;">
                </div>
                <asp:Label ID="lblGridMessage" ClientIDMode="Static" runat="server" CssClass="Error"></asp:Label>
            </div>
        </div>
        <div id="idDialog_Reason" style="display: none;">
            <div class="fwt-padding-4">
                <div id="divInvoiceStatusMessage">
                    You can close your invoice and it will not be searched.
                </div>
                <div class="fwt-padding-4">
                    <asp:TextBox ID="txtInvoiceStatusChangeReason" runat="server" ClientIDMode="Static"
                        CssClass="TextBoxNormal" autocomplete="off" Text=""></asp:TextBox>
                </div>
                <div class="fwt-right fwt-padding-4">
                    <asp:Button ID="btnSubmitCloseStatus" runat="server" Text="Submit" ClientIDMode="Static" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    
    <script src="<%=Application["Path"] %>_Layouts/JS/custom_invoice.js" type="text/javascript"></script>
    <script type="text/jscript">
        var sOrderBy = "";

        $(document).ready(function () {

            sJsonVar = { 'sAgency_ID': "0" };
            CallAjaxMethod("Customer_SelectName", sJsonVar, "Customer_SelectName_Complete");
            $("#ddlAgency").select2();
            $("#ddlCategories").select2();
            $("#ddlInvoiceStatus").select2();

            $("#txtDateFrom").datepicker({ dateFormat: 'dd-M-yy' }).datepicker("option", "maxDate", '+0m +0w');;
            $("#txtDateTo").datepicker({ dateFormat: 'dd-M-yy' }).datepicker("option", "maxDate", '+0m +0w');;

            SelectInvoicesGridPageNumberBlank();
            GetAll_Invoices(sOrderBy);
            $("#ddlAgency").change(function () {
                try {

                    var sAgency_ID = $("#ddlAgency").val();
                    if (sAgency_ID == "0" || sAgency_ID == "") {
                        $('#ddlCustomers').empty();
                        $('#ddlCustomers').append(new Option("-Select Customer-", "0"));
                    }
                    else {
                        sAgency_ID = sAgency_ID == "All" ? sAgency_ID = "" : sAgency_ID;
                        var sJsonVar = { 'sAgency_ID': sAgency_ID };
                        CallAjaxMethod("Customer_SelectName", sJsonVar, "Customer_SelectName_Complete");
                    }
                }
                catch (err) {
                    ShowMessage(err);
                }
            });
            $('#btnNext').click(function (e) {
                try {
                    //var iTotalRows = parseInt($("#lblPageNumber").text().split('of')[1]);
                    // var iTotalPageNumber = Math.ceil(iTotalRows / 10);                  
                    var iTotalPageNumber = parseInt($("#lblPageNumber").text().split('of')[1]);
                    var iCurrentPageNumber = parseInt($('#hfInvoicesGridPageNumber').val());
                    if (iCurrentPageNumber >= 0 && iCurrentPageNumber < iTotalPageNumber - 1) {
                        if (IsProcessingHomeGrid)
                            return false;
                        else {
                            IsProcessingHomeGrid = true;
                            var iInvoicesGridPageNumber = $('#hfInvoicesGridPageNumber').val() == "" ? 0 : parseInt($('#hfInvoicesGridPageNumber').val());
                            iInvoicesGridPageNumber = iInvoicesGridPageNumber + 1;
                            $('#hfInvoicesGridPageNumber').val(iInvoicesGridPageNumber);
                            GetAll_Invoices(sOrderBy);
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
                    if ($('#hfInvoicesGridPageNumber').val() > 0) {
                        if (IsProcessingHomeGrid)
                            return false;
                        else {
                            IsProcessingHomeGrid = true;
                            var iInvoicesGridPageNumber = parseInt($('#hfInvoicesGridPageNumber').val());
                            iInvoicesGridPageNumber = iInvoicesGridPageNumber - 1;
                            if (iInvoicesGridPageNumber == 0)
                                $("#tblInvoices thead").html("");
                            $('#hfInvoicesGridPageNumber').val(iInvoicesGridPageNumber);
                            GetAll_Invoices(sOrderBy);
                        }
                    }
                }
                catch (err) {
                    ShowMessage(err);
                }
                e.preventDefault();

            });

            $("#btnSearch").click(function (e) {
                try {
                    SelectInvoicesGridPageNumberBlank();
                    GetAll_Invoices(sOrderBy);
                }
                catch (err) {
                    ShowMessage(err);
                }
                e.preventDefault();
            });
            $("#btnReset").click(function (e) {
                try {
                    $("#hfSet_btnReset").val(1);
                    $("#ddlAgency").val("All").trigger("change");
                    $("#ddlCategories").val("All").trigger("change");
                    $("#ddlInvoiceStatus").val("All").trigger("change");
                    $("#ddlCustomers").val("All").trigger("change");
                    $('#hfInvoicesGridPageNumber').val("");

                    $("#hfSet_btnReset").val("");
                    var d = new Date();
                    d.setMonth(d.getMonth() - 1);
                    //$("#txtDateTo").datepicker({ dateFormat: 'dd-M-yy' }).datepicker("setDate", new Date());
                    // $("#txtPaymentDate").datepicker({ dateFormat: 'dd-M-yy' }).datepicker("setDate", new Date());
                    //$("#txtDateFrom").datepicker('setDate', d);

                    SelectInvoicesGridPageNumberBlank();
                    GetAll_Invoices(sOrderBy);

                }
                catch (err) {
                    ShowMessage(err);
                }
                e.preventDefault();
            });
            $("#txtDateFrom").change(function (e) {
                try {
                    SelectInvoicesGridPageNumberBlank();
                    GetAll_Invoices(sOrderBy);
                }
                catch (err) {
                    ShowMessage(err);
                }
                e.preventDefault();
            });

            $("#txtDateTo").change(function (e) {
                try {
                    SelectInvoicesGridPageNumberBlank();
                    GetAll_Invoices(sOrderBy);
                }
                catch (err) {
                    ShowMessage(err);
                }
                e.preventDefault();
            });
            //$("#btnSubmitCloseStatus").click(function (e) {
            //    try {

            //        var jsonVar = { "sParticular": $("#hfParticular").val(), "sCategory_Core": $("#hfCategory_Core").val(), "sStatus_Core": "IS_CLOSE", "sReason": $("#txtInvoiceStatusChangeReason").val(), "sCustomer_ID": $("#hfCustomer_ID").val() };
            //        CallAjaxMethod("Invoices_Update_CloseStatus", jsonVar, "Invoices_Update_CloseStatus_Complete");

            //    }
            //    catch (err) {
            //        ShowMessage(err);
            //    }
            //    e.preventDefault();
            //});

            //Updated Code By Arpit
            $("#ddlCustomers,#ddlCategories,#ddlInvoiceStatus,#ddlAgency").change(function () {
                try {

                    var sSelectedID = $(this).attr("id");
                    if ($("#id" + sSelectedID + "Selected").length == 0)
                        $("#idSearchAbstracts").append("<span id=\"id" + sSelectedID + "Selected\" class=\"fa fa-close div-search\" onclick=\"SearchAbstractsHide('" + sSelectedID + "');\">&nbsp;<b>" + $(this).parent().parent().find("span").html() + "</b> " + $("#" + sSelectedID + " option:selected").text() + "</span>");

                    else
                        $("#id" + sSelectedID + "Selected").html("&nbsp;<b>" + $(this).parent().parent().find("span").html() + "</b> " + $("#" + sSelectedID + " option:selected").text());

                    $("#id" + sSelectedID + "Selected").show();
                    if ($("#hfSet_btnReset").val() != 1)
                        $("#btnSearch").click();

                    //alert($("#idSearchAbstracts").html());
                }
                catch (err) {
                    ShowMessage(err);
                }
            });
            //$("#btnClose").click(function (e) {
            //    try {
            //        $("#divShowAllPayments").dialog("close");
            //    }
            //    catch (err) {
            //        ShowMessage(err);
            //    }
            //    e.preventDefault();
            //});
            //$("#btnClear").click(function (e) {
            //    try {
            //        //$("#txtPaymentDate").datepicker({ dateFormat: 'dd-M-yy' }).datepicker("setDate", new Date());
            //        $("#txtPaymentAmount").val("");
            //        $("#txtPaymentMethod").val("");
            //    }
            //    catch (err) {
            //        ShowMessage(err);
            //    }
            //    e.preventDefault();
            //});           

            //$("#btnSubmit").click(function (e) {
            //    try {
            //        if ((parseFloat($("#lblRemainingPayAmount").html()) >= parseFloat($("#txtPaymentAmount").val())) && parseFloat($("#txtPaymentAmount").val()) != 0) {

            //            if (parseFloat($("#lblRemainingPayAmount").html()) == parseFloat($("#txtPaymentAmount").val()))
            //                var sPaymentStatus_Core = "IS_PAID";
            //            else
            //                sPaymentStatus_Core = "IS_PAID_PARTIAL";

            //            var jsonVar = {
            //                "sCreatedBY": $("#hfLogInUser").val(), "sCustomer_ID": $("#lblCustomer_ID").html(), "sPaymentType": $("#txtPaymentMethod").val(), "sReceiptID": $("#txtReceipt_ID").val(), "sPaymentAmount": $("#txtPaymentAmount").val(), "sPaymentDate": $("#txtPaymentDate").val()
            //       , "sPaymentStatus_Core": sPaymentStatus_Core, "sCategory_Core": $("#hfCategory_Core").val()
            //            };
            //            CallAjaxMethod("Payment_Insert", jsonVar, "Payment_Insert_Complete");
            //        }
            //        else {
            //            ShowMessage(__Messages.Payment_Warrning);
            //        }
            //    }
            //    catch (err) {
            //        ShowMessage(err);
            //    }
            //    e.preventDefault();
            //});



        });

        function SearchAbstractsHide(sSelectedID) {
            try {
                var $DropdownID = $("#" + sSelectedID);
                var $SpanID = $("#id" + sSelectedID + "Selected");

                $("#idSearchAbstracts").find("span").each(function () {
                    if ($(this).attr("id") == $SpanID.attr("id")) {
                        $DropdownID.val("All").trigger('change');
                        $(this).hide();
                        $SpanID.html("");
                        // $("#btnSearch").click();
                    }
                });
            }
            catch (err) {
                ShowMessage("There is an issue calling the function (SearchAbstractsHide)" + err);
            }
        }

        function Customer_SelectName_Complete(data) {
            try {
                $('#ddlCustomers').empty();
                var $Data = data.d;
                if ($Data.length > 0) {
                    if ($Data[0].ERROR != null && $Data[0].ERROR != "") {
                        ShowMessage($Data[0].ERROR);
                    }
                    else {
                        $("#ddlCustomers").append($("<option></option>").val('All').html('-All-'));
                        for (var i = 0; i < $Data.length; i++) {
                            $("#ddlCustomers").append($("<option></option>").val($Data[i].Customer_ID).html($Data[i].CustomerName));
                        }
                        $("#ddlCustomers").select2();
                        if (location.href.indexOf("customer_id") >= 0) {
                            var sCustomer_ID = location.href.split('customer_id=');
                            $("#ddlCustomers").val(sCustomer_ID[1]).trigger('change');
                            $("#txtDateFrom").val("");
                            $("#txtDateTo").val("");
                        }
                    }
                }
                else
                    $("#ddlCustomers").append($("<option></option>").val(0).html('-No Customer Found-'));

            }//try
            catch (err) {
                ShowMessage("There is an issue calling the function (Customer_SelectName_Complete)" + err);
            }
        }
        //function Agent_SelectName_Complete(data) {
        //    try {
        //        $('#ddlAgents').empty();
        //        var $Data = data.d;
        //        if ($Data.length > 0) {
        //            if ($Data[0].ERROR != null && $Data[0].ERROR != "") {
        //                ShowMessage($Data[0].ERROR);
        //            }
        //            else {
        //                $("#ddlAgents").append($("<option></option>").val('All').html('-All-'));
        //                for (var i = 0; i < $Data.length; i++) {
        //                    $("#ddlAgents").append($("<option></option>").val($Data[i].Agent_ID).html($Data[i].AgentName));
        //                }
        //                //Set Agent dropdown with login user                   
        //                $("#ddlAgents").select2();
        //                if (location.href.indexOf("invoicestatuscore") >= 0 || location.href.indexOf("customer_id") >= 0) {
        //                    $("#ddlAgents").val("All").trigger('change');
        //                }
        //                else if ($("#hfLogInUser_ID").val() != "") {
        //                    $("#ddlAgents").val($("#hfLogInUser_ID").val()).trigger('change');
        //                }
        //            }
        //        }
        //        else
        //            $("#ddlAgents").append($("<option></option>").val(0).html('-No Agent Found-'));

        //    }
        //    catch (err) {
        //        ShowMessage("There is an issue calling the function (Agent_SelectName_Complete)" + err);
        //    }
        //}

        //function LastSavedID_Update_Complete() {
        //    try {
        //        LastSavedID_Create("Receipt", "txtReceipt_ID");

        //    }
        //    catch (err) {
        //        ShowMessage(err);
        //    }
        //}
        function Invoices_Update_CloseStatus_Complete(data) {
            try {
                var $Data = data.d;
                if ($Data.length > 0) {
                    if ($Data[0].ERROR != null && $Data[0].ERROR != "") {
                        ShowMessage($Data[0].ERROR);
                    }
                    else {
                        ShowMessage($Data[0].Message);
                        $("#idDialog_Reason").dialog("close");
                        GetAll_Invoices(sOrderBy);
                    }
                }
            }
            catch (err) {
                ShowMessage("There is an issue calling the function (Invoices_Update_CloseStatus_Complete)" + err);
            }
        }
        //function Payment_Insert_Complete(data) {
        //    try {
        //        var $Data = data.d;
        //        if ($Data.length > 0) {
        //            if ($Data[0].ERROR != null && $Data[0].ERROR != "") {
        //                ShowMessage($Data[0].ERROR);
        //            }
        //            else {
        //                var jsonVar = { "sID": $("#txtReceipt_ID").val(), "sType": "Receipt" }
        //                CallAjaxMethod("LastSavedID_Update", jsonVar, "LastSavedID_Update_Complete");
        //                ShowMessage($Data[0].Message);

        //                $("#txtPaymentDate").datepicker({ dateFormat: 'dd-M-yy' }).datepicker("setDate", new Date());
        //                var RemainingPayAmount = parseFloat($("#lblRemainingPayAmount").html()) - parseFloat($("#txtPaymentAmount").val());
        //                $("#lblRemainingPayAmount").html(RemainingPayAmount);
        //                $("#txtPaymentAmount").val("");
        //                $("#txtPaymentMethod").val("");
        //                Payments_Select($("#lblCustomer_ID").html(), $("#lblParticular").html(), $("#hfCategory_Core").val());
        //            }
        //        }
        //    }
        //    catch (err) {
        //        ShowMessage(err);
        //    }
        //}
    </script>

</asp:Content>
