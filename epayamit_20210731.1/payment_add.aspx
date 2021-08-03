<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="payment_add.aspx.cs" Inherits="Paybook.WebUI.payment_add" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfCustomer_ID" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfAdvancePayment" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfTotalRemainingAmount" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfPaymentsGridPageNumber" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfReceipt_ID" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfLogInUser" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfFilePath" runat="server" ClientIDMode="Static" />
    <div class="fwt-container">
        <div class="fwt-padding-4">
            <div class="fwt-col l6 m6 s12">
                <h4>Payment
                </h4>
            </div>
            <div id="idLabelError" runat="server" clientidmode="Static" class="fwt-container fwt-padding-16 fwt-pale-yellow fwt-border fwt-border-yellow"
                style="display: none">
            </div>
            <div class="fwt-col l6 m6 s12">
                <span>Remaining Amount:</span> <span style="font-size: 24px;">₹</span><asp:Label
                    ID="lblRemainingAmount" runat="server" ClientIDMode="Static" autocomplete="off"
                    Text="" Font-Size="24px"></asp:Label>
            </div>

            <div class="fwt-clear">
            </div>
        </div>
        <div class="fwt-padding-4">
            <div class="fwt-col l3 m3 s12">
                <span>Customer:</span><span style="color: Red; font-size: small">*</span>
                <asp:DropDownList ID="ddlCustomers" runat="server" ClientIDMode="Static" CssClass="DropdownNormal">
                </asp:DropDownList>
            </div>
            <div class="fwt-col l3 m3 s12 ">
                <span>Payment Date:</span><span style="color: Red; font-size: small">*</span>
                <asp:TextBox ID="txtPaymentDate" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                    autocomplete="off" Text=""></asp:TextBox>
            </div>
            <div class="fwt-col l3 m3 s12 ">
                <span>From:</span>
                <asp:TextBox ID="txtDateFrom" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                    autocomplete="off" Text=""></asp:TextBox>
            </div>
            <div class="fwt-col l3 m3 s12">
                <span>To: &nbsp; &nbsp;</span>
                <asp:TextBox ID="txtDateTo" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                    autocomplete="off" Text=""></asp:TextBox>
            </div>
            <div class="fwt-clear">
            </div>
        </div>
        <div class="fwt-padding-4">
            <div class="fwt-col l3 m3 s12">
                <span>Payment Method:</span>
                <asp:TextBox ID="txtPaymentMethod" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                    autocomplete="off" Text=""></asp:TextBox>
            </div>
            <div class="fwt-col l3 m3 s12">
                <span>Advance Payment:</span>
                <asp:TextBox ID="txtAdvancePayment" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                    autocomplete="off" Text=""></asp:TextBox>
            </div>
            <div class="fwt-col l3 m3 s12">
                <span>Payment:</span>
                <asp:TextBox ID="txtPayment" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                    autocomplete="off" Text=""></asp:TextBox>
            </div>
            <div class="fwt-col l3 m3 s12">
            </div>
            <div class="fwt-clear">
            </div>
        </div>
        <div class="fwt-padding-4 fwt-right">
            <div class="fwt-col m3 s3">
                <asp:Button ID="btnPay_Add" runat="server" CssClass="fwt-btn fwt-green fwt-hover-indigo"
                    Text="ADD" ClientIDMode="Static" />
            </div>
            <div class="fwt-col m3 s3">
                <asp:Button ID="btnClear" runat="server" CssClass="fwt-btn fwt-green fwt-hover-indigo"
                    Text="Clear" ClientIDMode="Static" />
            </div>
            <div class="fwt-col m6 s6">
                <button runat="server" id="btnExportTableData" class="fwt-btn fwt-ripple fwt-dropdown-click fwt-amber"
                    clientidmode="Static">
                    <i class="fa fa-bars"></i>&nbsp;Export Table Data</button>
            </div>

            <div class="fwt-clear">
            </div>
        </div>
        <div class="fwt-padding-4 ">
            <div class="fwt-col l2 m2 s12" id="idPageNumber">
                <asp:Label ID="lblPageNumber" ClientIDMode="Static" runat="server"></asp:Label>
            </div>
            <div class="fwt-clear">
            </div>
        </div>
        
        <div class="fwt-padding-8 ">
            <div id="divPayments" style="display: block;">
                <div class="table-header">
                </div>
                <div class="table-scroll">
                    <table class="table-main fwt-table" border="0" width="100%">
                        <thead>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                    <div class="table-message">
                    </div>
                </div>
            </div>
        </div>
        <div id="irmGenrateReport" style="display: none; height: 100%; width: 100%;">
            <div class="fwt-padding-8">
                <div id="idMsg" style="font-size: 12px; font-weight: bold;"></div>

                <button runat="server" id="idPrint" class="fwt-btn fwt-ripple fwt-dropdown-click fwt-amber"
                    clientidmode="Static">
                    Print</button>
            </div>
            <iframe id="ifrGenrateReport" clientidmode="Static" style="width: 100%; height: 500px; border: 0;"
                border="0" src="" runat="server"></iframe>

        </div>
    </div>
    <script type="text/jscript">
        var sOrderBy = "";
        $(document).ready(function () {

            $("#txtDateFrom").datepicker({ dateFormat: 'dd-M-yy' });
            $("#txtDateTo").datepicker({ dateFormat: 'dd-M-yy' });
            $("#txtPaymentDate").datepicker({ dateFormat: 'dd-M-yy' }).datepicker("setDate", new Date());
            $("#txtAdvancePayment").attr('disabled', true)
            LastSavedID_Create("Receipt", "Text/hfReceipt_ID");
            sJsonVar = { 'sAgencyName': "" };
            CallAjaxMethod("Customer_SelectName", sJsonVar, "Customer_SelectName_Complete");

            $("#btnPay_Add").click(function (e) {
                try {
                    if (Validation()) {
                        LastSavedID_Create("Receipt", "Text/hfReceipt_ID");
                        if ($("#txtAdvancePayment").val() != "" && $("#btnPay_Add").val() == "PAY") {
                            var i = 0;
                            $("#divPayments .table-main tbody").find('tr').each(function () {
                                if ($(this).find('input[type="checkbox"]').is(':checked')) {
                                    var $idRemaingPayment = $("#idRemainingPayment" + i);;
                                    var $idCurrentPayment = $("#idCurrentPayment" + i);
                                    var $idParticular = $("#idParticular" + i);
                                    var $idCategory_Core = $("#idCategory_Core" + i);

                                    var sCategory_Core = $idCategory_Core.html().split('|')[1];
                                    var sParticular = $idParticular.html();
                                    var sCurrentPayment = $idCurrentPayment.html();

                                    if (parseFloat($idRemaingPayment.html()) == 0)
                                        var sPaymentStatus_Core = "IS_PAID";
                                    else
                                        sPaymentStatus_Core = "IS_PAID_PARTIAL";

                                    //Insert Payments
                                    var jsonVar = { "sCreatedBY": $("#hfLogInUser").val(), "sCustomer_ID": $("#hfCustomer_ID").val(), "sPaymentType": $("#txtPaymentMethod").val(),"sReceiptID": $("#hfReceipt_ID").val(), "sPaymentAmount": sCurrentPayment, "sPaymentDate": $("#txtPaymentDate").val(), "sPaymentStatus_Core": sPaymentStatus_Core, "sCategory_Core": sCategory_Core };
                                    CallAjaxMethod("Payment_Insert", jsonVar, "Payment_Insert_Complete");
                                }
                                i++;
                            });


                            $("#hfTotalRemainingAmount").val($("#lblRemainingAmount").html());
                            $("#hfAdvancePayment").val($("#txtAdvancePayment").val());
                        }
                        else {

                            //Update Advance Payment direct
                            if ($("#txtPayment").val() != "" && parseFloat($("#txtPayment").val()) != 0) {
                                var dTotalAdvancePAyment = parseFloat($("#txtAdvancePayment").val()) + parseFloat($("#txtPayment").val());
                                $("#txtAdvancePayment").val(dTotalAdvancePAyment);

                                var jsonVar = { "sCurrentAdvancePayment": $("#txtPayment").val(), "sCustomer_ID": $("#hfCustomer_ID").val(), "sAdvancePayment_Date": $("#txtPaymentDate").val(), "sCreatedBy": $("#hfLogInUser").val(), "sTotalAdvancePayment": dTotalAdvancePAyment };
                                CallAjaxMethod("AdvancePayment_Insert", jsonVar, "AdvancePayment_Insert_Complete");
                                $("#hfAdvancePayment").val($("#txtAdvancePayment").val());
                                $("#txtPayment").val(0.00);
                                ReadXMLMessage("PTS504", "ReadXMLMessage_Complete");
                            }
                            else
                                ReadXMLMessage("PTS502", "ReadXMLMessage_Complete");
                        }
                    }
                }
                catch (err) {
                    ShowMessage(err);
                }
                e.preventDefault();
            });
            $("#btnClear").click(function (e) {
                try {
                    //$("#txtPayment").attr("disabled", true);
                    $("#txtPayment").val(0.00);
                    $("#lblRemainingAmount").html($("#hfTotalRemainingAmount").val());
                    $("#txtAdvancePayment").val($("#hfAdvancePayment").val());
                    $("#divPayments .table-main tbody").find('input').each(function () {
                        $('input:checkbox').prop('checked', false);
                    });
                    $("#btnPay_Add").val("ADD");
                    SelectPaymentsGridPageNumberBlank();
                    GetAll_CustomerPayments(sOrderBy);
                }
                catch (err) {
                    ShowMessage(err);
                }
                e.preventDefault();
            });
            $("#txtDateFrom").change(function (e) {
                try {

                    SelectPaymentsGridPageNumberBlank();
                    GetAll_CustomerPayments(sOrderBy);
                }
                catch (err) {
                    ShowMessage(err);
                }
                e.preventDefault();
            });
            $("#txtDateTo").change(function (e) {
                try {

                    SelectPaymentsGridPageNumberBlank();
                    GetAll_CustomerPayments(sOrderBy);
                }
                catch (err) {
                    ShowMessage(err);
                }
                e.preventDefault();
            });
            $("#ddlCustomers").change(function (e) {
                try {

                    var sCustomer_ID = $("#ddlCustomers").val();
                    if (sCustomer_ID == 0) {
                        $("#hfTotalRemainingAmount").val("");
                        $("#hfAdvancePayment").val("");
                        $("#btnClear").click();
                    }
                    else {
                        $("#hfCustomer_ID").val(sCustomer_ID);
                        $("#txtPayment").val(0.00);
                        // $("#txtAdvancePayment").attr("disabled", false);
                        SelectPaymentsGridPageNumberBlank();
                        GetAll_CustomerPayments(sOrderBy);


                        var sJsonVar = { "sCustomer_ID": $("#ddlCustomers").val() }
                        CallAjaxMethod("Customer_SelectRemainingAmount", sJsonVar, "Customer_SelectRemainingAmount_Complete");
                    }
                }
                catch (err) {
                    ShowMessage(err);
                }
                e.preventDefault();
            });

            $("#btnExportTableData").click(function (e) {
                try {
                    var sPaymentDateTo = $("#txtDateTo").val();
                    var sPaymentDateFrom = $("#txtDateFrom").val();
                    var sCustomer_ID = $("#hfCustomer_ID").val();
                    var sRemainingAmount = $("#hfTotalRemainingAmount").val();
                    var sJsonVar = { "sPaymentDateTo": sPaymentDateTo, "sPaymentDateFrom": sPaymentDateFrom, "sCustomer_ID": sCustomer_ID, "sRemainingAmount": sRemainingAmount }

                    CallAjaxMethod("GenrateReport", sJsonVar, "GenrateReport_Complete");
                    e.preventDefault();
                }
                catch (err) {
                    ShowMessage(err);
                }

            });

            $("#idPrint").click(function (e) {
                try {
                    if ($("#idPrint").html() == "Print") {
                        var tempFrame = $('#ifrGenrateReport')[0];
                        var tempFrameWindow = tempFrame.contentWindow ? tempFrame.contentWindow : tempFrame.contentDocument.defaultView;
                        tempFrameWindow.focus();
                        tempFrameWindow.print();
                    }
                    //else {                 
                    //    $("#ifrGenrateReport").attr("src", $("#hfFilePath").val());
                    //    $("#idPrint").html("Print");
                    //}
                    e.preventDefault();
                }
                catch (err) {
                    ShowMessage(err);
                }

            });
        });
        function GenrateReport_Complete(data) {
            try {
                var $Data = data.d;
                if ($Data.length > 0) {
                    if ($Data[0].ERROR != "") {
                        ShowMessage($Data[0].ERROR);
                        return;
                    }
                    $("#irmGenrateReport").dialog({
                        modal: true,
                        title: "Invoice/Payment Report",
                        width: 1000,
                        hight: 1000,
                        closeOnEscape: true,
                        open: function (event, ui) {
                            $(".ui-dialog").addClass("ui-dialog-shadow");
                        },
                        close: function () {
                        }
                    });

                    //if ($Data[0].Message == "") {
                    $("#idMsg").html($Data[0].Message + " Report Name:" + $Data[0].FileName);
                    $("#hfFilePath").val("");
                    $("#idPrint").html("Print");
                    $("#ifrGenrateReport").attr("src", $Data[0].FilePath);
                    //}
                    //else {

                    //    $("#idPrint").html("Open");
                    //    $("#idMsg").html($Data[0].Message);
                    //    $("#hfFilePath").val($Data[0].FilePath);
                    //}
                    //$("#ifrGenrateReport").attr("src", "_Documents\\html\\Mr._Arpit_Shrivastava_r10_20170527.htm");
                }

            }
            catch (err) {
                ShowMessage("There is an issue calling the function (GenrateReport_Complete)" + err);
            }

        }
        function Validation() {
            try {
                if ($("#hfCustomer_ID").val() == "" || $("#ddlCustomers").val() == 'All') {
                    ReadXMLMessage("CUS107", "ReadXMLMessage_Complete");
                    return false;
                }
                    //else if ($("#txtDateTo").val() == "" || $("#txtDateFrom").val() == "") {
                    //    ShowMessage(__Messages.CustomerNameSelect_Warrning);
                    //    return false;
                    //}
                else if ($("#txtPaymentDate").val() == "") {
                    ReadXMLMessage("BSW008", "ReadXMLMessage_Complete");
                    return false;
                }
                else
                    return true;

            }
            catch (err) {
                ShowMessage("There is an issue calling the function (Validation)" + err);
            }
        }
        function Customer_SelectRemainingAmount_Complete(data) {
            try {
                var $Data = data.d;
                if ($Data.length > 0) {
                    if ($Data[0].ERROR != "") {
                        ShowMessage($Data[0].ERROR);
                    }
                    else {
                        var fRemainingAmount = $Data[0].RemainingAmount == "" ? "0.00" : parseFloat($Data[0].RemainingAmount).toFixed(2);
                        $("#lblRemainingAmount").html(fRemainingAmount);
                        $("#hfTotalRemainingAmount").val($Data[0].RemainingAmount);
                        var fAdvancePayment = $Data[0].AdvancePayment == "" ? "0.00" : parseFloat($Data[0].AdvancePayment).toFixed(2);
                        $("#hfAdvancePayment").val(fAdvancePayment);
                        $("#txtAdvancePayment").val(fAdvancePayment);
                    }
                }
            }
            catch (err) {
                ShowMessage("There is an issue calling the function (Customer_SelectRemainingAmount_Complete)" + err);
            }
        }
        function Customer_Update_AdvancePayment_Complete(data) {
            try {
                // $("#txtAdvancePayment").attr("disabled", false);
                var $Data = data.d;
                if ($Data.length > 0) {
                    if ($Data[0].ERROR != "") {
                        ShowMessage($Data[0].ERROR);
                    }
                    else {
                        $("#btnPay_Add").val("ADD");
                        $("#txtPayment").val(0.00);
                        SelectPaymentsGridPageNumberBlank();
                        GetAll_CustomerPayments(sOrderBy);
                        ShowMessage($Data[0].Message);
                    }
                }
            }
            catch (err) {
                ShowMessage("There is an issue calling the function (Customer_Update_AdvancePayment_Complete)" + err);
            }
        }
        function AdvancePayment_Insert_Complete() {
            try {

            }
            catch (err) {
                ShowMessage("There is an issue calling the function (AdvancePayment_Insert_Complete)" + err);
            }
        }

        function Customer_SelectName_Complete(data) {
            try {
                $('#ddlCustomers').empty();
                var $Data = data.d;
                if ($Data.length > 0) {
                    if ($Data[0].ERROR != "") {
                        ShowMessage($Data[0].ERROR);
                    }
                    else {
                        $("#ddlCustomers").append($("<option></option>").val(0).html('-Select Customer-'));
                        for (var i = 0; i < $Data.length; i++) {
                            $("#ddlCustomers").append($("<option></option>").val($Data[i].Customer_ID).html($Data[i].CustomerName));
                        }
                        if (location.href.indexOf("customer_id") >= 0) {
                            var sCustomer_ID = location.href.split('customer_id=');
                            $("#ddlCustomers").val(sCustomer_ID[1]).trigger('change');

                        }

                        $("#ddlCustomers").select2();
                    }
                }
                else
                    $("#ddlCustomers").append($("<option></option>").val(0).html('-No Customer Found-'));

            }
            catch (err) {
                ShowMessage("There is an issue calling the function (Customer_SelectName_Complete)" + err);

            }
        }
        var varCountPayment = 0;
        function Payment_Insert_Complete(data) {
            try {
                var $Data = data.d;
                if ($Data.length > 0) {
                    if ($Data[0].ERROR != "") {
                        ShowMessage($Data[0].ERROR);
                    }
                    else {

                        varCountPayment++;
                        if ($("#divPayments .table-main tbody").find('tr').length == varCountPayment) {
                            //Update Advance Payment after Payment
                            jsonVar = { "sTotalAdvancePayment": $("#txtAdvancePayment").val(), "sCustomer_ID": $("#hfCustomer_ID").val(),"sTotalRemainigAmount":"" };
                            CallAjaxMethod("Customer_Update_AdvancePayment", jsonVar, "Customer_Update_AdvancePayment_Complete");
                        }

                        //Update Advance
                        //var jsonVar = { "sAdvancePayment": $("#txtAdvancePayment").val(), "sCustomer_ID": $("#hfCustomer_ID").val() };
                    }
                }

            }
            catch (err) {
                ShowMessage("There is an issue calling the function (Payment_Insert_Complete)" + err);

            }
        }
        function LastSavedID_Update_Complete() {
            try {
                LastSavedID_Create("Receipt", "Text/hfReceipt_ID");
            }
            catch (err) {
                ShowMessage("There is an issue calling the function (LastSavedID_Update_Complete)" + err);

            }
        }
        function ReadXMLMessage_Complete(data) {
            try {
                ShowMessage(data);
            }
            catch (err) {
                ShowMessage("ERROR", "There is an issue calling the function (ReadXMLMessage_Complete)" + err);
            }
        }
    </script>
</asp:Content>
