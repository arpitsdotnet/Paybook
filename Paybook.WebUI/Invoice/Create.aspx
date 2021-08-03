<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="Paybook.WebUI.Invoice.Create" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="hfCustomer_ID" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfAgency_ID" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfCategory_Core" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfLogInUser" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfLogInUser_ID" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfAdvancePayment" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfTotalRemainingAmount" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfPaymentsGridPageNumber" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfGridSelectedRow_Count" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfGSTValue" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="HiddenField1" runat="server" ClientIDMode="Static" />

    <div class="fwt-container">
        <div id="idLabelError" runat="server" clientidmode="Static" class="fwt-container fwt-padding-16 fwt-pale-yellow fwt-border fwt-border-yellow"
            style="display: none">
        </div>
        <div>
            <div class="fwt-col l2 m2 s12">
                <h4>Invoice </h4>
            </div>
            <div class="fwt-col l3 m3 s12" id="divSelectOperation">
                <asp:RadioButtonList ID="rbtnlSelectOperation" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal" CssClass="RadioButtonNormal">
                    <asp:ListItem Value="CreateInvoice" Selected="True"> New Invoice </asp:ListItem>
                    <asp:ListItem Value="MakePayment"> Add Payment </asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div class="fwt-col l1 m1">
                &nbsp;
            </div>
            <div class="fwt-col l3 m3 s12">
                <div>Agency:<span class="requied">*</span></div>
                <div>
                    <asp:DropDownList ID="ddlAgency" runat="server" ClientIDMode="Static" CssClass="DropdownNormal">
                    </asp:DropDownList>
                </div>
                <div class="fwt-clear">
                </div>
            </div>
            <div class="fwt-col l3 m3 s12">
                <div>Customer:<span class="requied">*</span></div>
                <div>
                    <asp:DropDownList ID="ddlCustomers" runat="server" ClientIDMode="Static" CssClass="DropdownNormal">
                    </asp:DropDownList>
                </div>
                <div class="fwt-clear">
                </div>
            </div>

            <div class="fwt-clear">
            </div>
        </div>
        <div id="idCreateInvoice">
            <fieldset>
                <legend>Create New Invoice</legend>
                <div class="fwt-col l9 m9 s12" style="border-right: 1px solid #ccc">
                    <div class="fwt-padding-4">
                        <div class="fwt-col l3 m3 s12">
                            <div>Type:<span class="requied">*</span></div>
                            <div>
                                <asp:DropDownList ID="ddlCategories" runat="server" ClientIDMode="Static" CssClass="DropdownNormal">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="fwt-col l3 m3 s12">
                            <div>Amount:<span class="requied">*</span></div>
                            <div>
                                <asp:TextBox ID="txtAmount" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                                    autocomplete="off" Text=""></asp:TextBox>
                            </div>
                        </div>
                        <div class="fwt-col l3 m3 s12">
                            <div>Date:<span class="requied">*</span></div>
                            <div>
                                <asp:TextBox ID="txtInvoiceDate" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                                    autocomplete="off" Text=""></asp:TextBox>
                            </div>
                        </div>
                        <div class="fwt-col l3 m3 s12">
                            <div>Vehicle No:<span class="requied">*</span></div>
                            <div>
                                <asp:TextBox ID="txtVehicleNo" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                                    autocomplete="off" Text=""></asp:TextBox>
                            </div>
                        </div>
                        <div class="fwt-clear"></div>
                    </div>
                    <div class="fwt-padding-4">
                        <div class="fwt-col l6 m6 s12">
                            <div>Remark:</div>
                            <div>
                                <asp:TextBox ID="txtRemark" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" TextMode="MultiLine"
                                    autocomplete="off" Text="" Height="80px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="fwt-col l6 m6 s12">
                            <div>Particular:</div>
                            <div>
                                <asp:TextBox ID="txtParticular" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                                    autocomplete="off" Text="" TextMode="MultiLine" Height="80px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="fwt-clear"></div>
                </div>
                <div class="fwt-col l3 m3 s12" style="padding-left: 8px">
                    <div class="fwt-padding-4">
                        <div>GST:</div>
                        <div>
                            <asp:RadioButtonList ID="rbtn_GST_Value" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal" CssClass="RadioButtonNormal">
                                <asp:ListItem Value="GST_NONE">None </asp:ListItem>
                                <asp:ListItem Value="GST_CENTRE">Centre </asp:ListItem>
                                <asp:ListItem Value="GST_STATE">State </asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="fwt-padding-4">
                        <div>
                            <asp:Label ID="lblGST_Value" runat="server" ClientIDMode="Static" Font-Size="Small"></asp:Label>
                        </div>
                        <div>
                            <span style="font-weight: bold; font-size: 12px">Total: <i class="fa fa-inr"></i></span>
                            <asp:Label ID="lblTotalAmount_WithGST" runat="server" ClientIDMode="Static" Font-Size="small" Font-Bold="true"></asp:Label>
                        </div>
                    </div>
                    <div class="fwt-padding-4" style="text-align: right">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ClientIDMode="Static" CssClass="fwt-btn fwt-green fwt-hover-indigo" />
                    </div>
                </div>
            </fieldset>
            <div class="fwt-clear"></div>
        </div>
        <div id="idMakePayment" style="display: none">
            <fieldset>
                <legend>Payment</legend>
                <div class="fwt-padding-4">
                    <div class="fwt-col l3 m3 s12 ">
                        <div>Remaining Amount:</div>
                        <div>
                            <asp:TextBox ID="txtRemainingAmount" runat="server" ClientIDMode="Static" autocomplete="off" CssClass="TextBoxNormal"
                                Text="" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="fwt-col l3 m3 s12 ">
                        <div><span>Payment Method:</span> </div>
                        <div>
                            <asp:TextBox ID="txtPaymentMethod" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                                autocomplete="off" Text=""></asp:TextBox>
                        </div>
                    </div>
                    <div class="fwt-col l3 m3 s12 ">
                        <div><span>From:</span></div>
                        <div>
                            <asp:TextBox ID="txtDateFrom" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                                autocomplete="off" Text=""></asp:TextBox>
                        </div>
                    </div>
                    <div class="fwt-col l3 m3 s12 ">
                        <div><span>To:</span></div>
                        <div>
                            <asp:TextBox ID="txtDateTo" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                                autocomplete="off" Text=""></asp:TextBox>
                        </div>
                    </div>
                    <div class="fwt-clear">
                    </div>
                </div>
                <div class="fwt-padding-4">
                    <div class="fwt-col l3 m3 s12">
                        <div><span>Payment Date:</span><span class="requied">*</span></div>
                        <div>
                            <asp:TextBox ID="txtPaymentDate" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                                autocomplete="off" Text=""></asp:TextBox>
                        </div>
                    </div>
                    <div class="fwt-col l3 m3 s12">
                        <div><span>Advance Payment:</span> </div>
                        <div>
                            <asp:TextBox ID="txtAdvancePayment" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                                autocomplete="off" Text=""></asp:TextBox>
                        </div>
                    </div>
                    <div class="fwt-col l3 m3 s12">
                        <div><span>Payment:</span> </div>
                        <div>
                            <asp:TextBox ID="txtPayment" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                                autocomplete="off" Text=""></asp:TextBox>
                        </div>
                    </div>
                    <div class="fwt-col l3 m3 s12" style="padding-top: 14px">
                        <asp:Button ID="btnPayment" runat="server" CssClass="fwt-btn fwt-green fwt-hover-indigo"
                            Text="ADD" ClientIDMode="Static" />
                        <asp:Button ID="btnClear" runat="server" CssClass="fwt-btn fwt-purple fwt-hover-indigo"
                            Text="Clear" ClientIDMode="Static" />
                        <button runat="server" id="btnExportTableData" class="fwt-btn fwt-hover-indigo fwt-amber"
                            clientidmode="Static">
                            Export Table</button>
                    </div>
                    <div class="fwt-clear">
                    </div>
                </div>
                <%--    <div class="fwt-padding-4 fwt-right">

                    <div class="fwt-clear">
                    </div>
                </div>--%>
            </fieldset>
        </div>
        <div class="fwt-padding-8 " id="idPageNumber">
            <div class="fwt-col l2 m2 s12">
                <asp:Label ID="lblPageNumber" ClientIDMode="Static" runat="server"></asp:Label>
            </div>
            <div class="fwt-col l10 m10 s6 fwt-NavigationButtons">
                <button id="btnPrivious" clientidmode="Static" class="fwt-btn fwt-ripple fwt-dropdown-click fwt-amber">
                    <i class="fa fa-backward"></i>&nbsp;</button>
                <button id="btnNext" clientidmode="Static" class="fwt-btn fwt-ripple fwt-dropdown-click fwt-amber">
                    <i class="fa fa-forward"></i>&nbsp;</button>
            </div>

            <div class="fwt-clear">
            </div>
        </div>
        <div class="fwt-padding-4">
            <div id="div_HeadertblPayments">
            </div>
            <div class="table-Scroll" id="div_ScrolltblPayments">
                <table id="tblPayments" border="0" width="100%" style="word-wrap: break-word;" class="table-main fwt-table">
                    <thead>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
                <div id="divGridLoadingtblPayments" style="text-align: center;">
                </div>
                <asp:Label ID="lblGridMessagetblPayments" runat="server" CssClass="Error" ClientIDMode="Static"></asp:Label>
            </div>
        </div>
        <div id="irmGenrateReport" style="display: none; height: 100%; width: 100%;">
            <div class="fwt-padding-8">
                <div id="idMsg" style="font-size: 12px; font-weight: bold;"></div>
                <button runat="server" id="btnPrint" class="fwt-btn fwt-ripple fwt-dropdown-click fwt-amber"
                    clientidmode="Static">
                    Print</button>
                <%--   <button runat="server" id="btnEmail" class="fwt-btn fwt-ripple fwt-dropdown-click fwt-dark-blue"
                    clientidmode="Static">
                    Email</button>--%>
            </div>
            <iframe id="ifrGenrateReport" clientidmode="Static" style="width: 100%; height: 500px; border: 0;"
                border="0" src="" runat="server"></iframe>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    
    <script src="<%=Application["Path"] %>_Layouts/JS/custom_payment.js" type="text/javascript"></script>
    <script type="text/javascript">
        var sOrderBy = "";
        $(document).ready(function () {
            // Create invoice code
            $("#idPageNumber").hide();
            //sJsonVar = { 'sAgencyName':""};
            //CallAjaxMethod("Customer_SelectName", sJsonVar, "Customer_SelectName_Complete");
            $('#ddlCustomers').append(new Option("-Select Customer-", "0"));
            $("#txtInvoiceDate").datepicker({ dateFormat: 'dd-M-yy', timeFormat: 'hh:mm tt' }).datepicker("setDate", new Date()).datepicker("option", "maxDate", '+0m +0w');
            $("#ddlCategories").select2();
            $("#ddlAgency").select2();
            $("#ddlCustomers").select2();
            $("#txtDateFrom").datepicker({ dateFormat: 'dd-M-yy' }).datepicker("option", "maxDate", '+0m +0w');
            $("#txtDateTo").datepicker({ dateFormat: 'dd-M-yy' }).datepicker("option", "maxDate", '+0m +0w');
            $("#txtPaymentDate").datepicker({ dateFormat: 'dd-M-yy' }).datepicker("setDate", new Date()).datepicker("option", "maxDate", '+0m +0w');
            $("#txtAdvancePayment").attr('disabled', true);
            //if ($("#ddlCustomers").val() == 0 || $("#ddlCustomers").val() == 'NONE') {
            //    $("#btnSubmit,#btnPayment,#btnClear").attr('disabled', true);
            //}
            SelectOnClick("txtAdvancePayment");
            SelectOnClick("txtAmount");
            SelectOnClick("txtPayment");

            //hide and show invoice/payment option
            ChangeOperation();

            $("#lblTotalAmount_WithGST").html("0.0");
            GetGst_Value($("#rbtn_GST_Value input:checked").val());
            $("#rbtn_GST_Value").change(function () {
                var sGST_Type = $("#rbtn_GST_Value input:checked").val();
                GetGst_Value(sGST_Type);
            });
            $("#rbtnlSelectOperation").change(function () {
                ChangeOperation();
            });
            $("#txtAmount").change(function (e) {
                try {
                    if (!$.isNumeric($('#txtAmount').val()) || $('#txtAmount').val() == "") {
                        $('#txtAmount').val(0.0);
                    }
                    $("#rbtn_GST_Value").change();
                }
                catch (err) {
                    ShowMessage(err);
                }
                e.preventDefault();
            });
            $("#btnSubmit").click(function (e) {
                try {
                    if (Validation_Invoice()) {
                        var sGST_Type = $("#rbtn_GST_Value input:checked").val();
                        GetGst_Value(sGST_Type);
                        var sAgency_ID = $("#ddlAgency").val() == "NONE" ? "0" : $("#ddlAgency").val();
                        var sJsonVar = {
                            "sCreatedBY": $("#hfLogInUser").val(), "sAgency_ID": sAgency_ID, "sCustomer_ID": $("#ddlCustomers").val(), "sCategory_Core": $('#ddlCategories').val(), "sParticular": $("#txtParticular").val(), "sAmount": $('#lblTotalAmount_WithGST').html(), "sInvoice_Date": $("#txtInvoiceDate").val(), "sRemainingAmount": $("#txtRemainingAmount").val()
                            , "sInvoiceStatus_Core": "IS_OPEN", "sAgent_ID": $("#hfLogInUser_ID").val(), "sRemark": $("#txtRemark").val(), "sMRP": $("#txtAmount").val()
                            , "sGST_Type": sGST_Type, "sVehicleNo": $("#txtVehicleNo").val()
                        };
                        CallAjaxMethod("Invoice_Insert", sJsonVar, "Invoice_Insert_Complete");
                    }
                }
                catch (err) {
                    ShowMessage(err);
                }
                e.preventDefault();
            });

            $("#ddlAgency").change(function (e) {
                try {

                    $("#hfTotalRemainingAmount").val("0");
                    $("#hfAdvancePayment").val("0");
                    $("#txtRemainingAmount").val($("#hfTotalRemainingAmount").val());
                    $("#txtAdvancePayment").val($("#hfAdvancePayment").val());
                    $("#txtPaymentMethod").val("");
                    SetDefault();
                    //$("#btnSubmit,#btnPayment,#btnClear").attr('disabled', true);
                    var sAgency_ID = $("#ddlAgency").val();
                    if (sAgency_ID == "0") {
                        $('#ddlCustomers').empty();
                        $('#ddlCustomers').append(new Option("-Select Customer-", "0"));
                        $("#tblPayments thead, #tblPayments tbody").html("");

                    }
                    else {
                        sAgency_ID = sAgency_ID == "NONE" ? sAgency_ID = "0" : sAgency_ID;
                        var sJsonVar = { 'sAgency_ID': sAgency_ID };
                        CallAjaxMethod("Customer_SelectName", sJsonVar, "Customer_SelectName_Complete");
                    }
                }
                catch (err) {
                    ShowMessage("Error occured: " + err);
                }
                e.preventDefault();
            });

            $("#ddlCustomers").change(function (e) {
                try {
                    var sCustomer_ID = $("#ddlCustomers").val();
                    var sAgency_ID = $("#ddlAgency").val();
                    $("#txtPaymentMethod").val("");
                    if (sAgency_ID == 'NONE' && sCustomer_ID == 0) {

                        $("#hfTotalRemainingAmount").val("0");
                        $("#hfAdvancePayment").val("0");
                        $("#txtRemainingAmount").val($("#hfTotalRemainingAmount").val());
                        $("#txtAdvancePayment").val($("#hfAdvancePayment").val());
                        $("#tblPayments thead, #tblPayments tbody").html("");
                    }
                    else {
                        $("#hfCustomer_ID").val(sCustomer_ID);
                        $("#txtPayment").val(0.00);
                        if (sAgency_ID == "NONE") {
                            SetDefault();
                            var sJsonVar = { "sCustomer_ID": sCustomer_ID }
                            CallAjaxMethod("Customer_SelectRemainingAmount", sJsonVar, "SelectRemainingAmount_Complete");
                        }
                    }
                }
                catch (err) {
                    ShowMessage("Error occured: " + err);
                }
                e.preventDefault();
            });

            //payment code
            $('#btnNext').click(function (e) {
                try {
                    // var iTotalRows = parseInt($("#lblPageNumber").text().split('of')[1]);
                    // var iTotalPageNumber = Math.ceil(iTotalRows / 10);
                    var iTotalPageNumber = parseInt($("#lblPageNumber").text().split('of')[1]);
                    var iCurrentPageNumber = parseInt($('#hfPaymentsGridPageNumber').val());
                    if (iCurrentPageNumber >= 0 && iCurrentPageNumber < iTotalPageNumber - 1) {
                        if (IsProcessingHomeGrid)
                            return false;
                        else {
                            IsProcessingHomeGrid = true;
                            var iPaymentGridPageNumber = $('#hfPaymentsGridPageNumber').val() == "" ? 0 : parseInt($('#hfPaymentsGridPageNumber').val());
                            iPaymentGridPageNumber = iPaymentGridPageNumber + 1;

                            $('#hfPaymentsGridPageNumber').val(iPaymentGridPageNumber);
                            GetAll_CustomerPayments(sOrderBy);
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
                    if ($('#hfPaymentsGridPageNumber').val() > 0) {
                        if (IsProcessingHomeGrid)
                            return false;
                        else {
                            IsProcessingHomeGrid = true;
                            var iPaymentGridPageNumber = parseInt($('#hfPaymentsGridPageNumber').val());
                            iPaymentGridPageNumber = iPaymentGridPageNumber - 1;
                            if (iPaymentGridPageNumber == 0)
                                $("#tblPayments thead").html("");
                            $('#hfPaymentsGridPageNumber').val(iPaymentGridPageNumber);
                            GetAll_CustomerPayments(sOrderBy);
                        }
                    }
                }
                catch (err) {
                    ShowMessage(err);
                }
                e.preventDefault();

            });

            $("#btnPayment").click(function (e) {
                try {
                    if (Validation_Payment()) {
                        // LastSavedID_Create("Receipt", "Text/hfReceipt_ID");
                        if ($("#txtAdvancePayment").val() != "" && $("#btnPayment").val() == "PAY") {
                            //if ($("#ddlCustomers").val() == '0' && $("#ddlAgency").val() != "NONE") {
                            //    ReadXMLMessage("CUS107", "ReadXMLMessage_Complete");
                            //    return false;
                            //}
                            // else {
                            var i = 0, SeleteRowCount = 1;
                            $("#tblPayments tbody").find('tr').each(function () {
                                if ($(this).find('input[type="checkbox"]').is(':checked')) {
                                    var $idRemaingPayment = $("#idRemainingPayment" + i);
                                    var $idCurrentPayment = $("#idCurrentPayment" + i);
                                    var $idParticular = $("#idParticular" + i);
                                    var $idCategory_Core = $("#idCategory_Core" + i);
                                    var $idInvoice_id = $("#idInvoice_id" + i);
                                    var $idCustomer = $("#idCustomer" + i);

                                    var sCategory_Core = $idCategory_Core.html().split('|')[1];
                                    var sCustomerID = $idCustomer.html().split('|')[1];
                                    var sParticular = $idParticular.html();
                                    var sCurrentPayment = $idCurrentPayment.html();
                                    var sInvoice_ID = $idInvoice_id.html();
                                    if (parseFloat($idRemaingPayment.html()) == 0)
                                        var sPaymentStatus_Core = "IS_PAID";
                                    else
                                        sPaymentStatus_Core = "IS_PAID_PARTIAL";

                                    $("#hfGridSelectedRow_Count").val(SeleteRowCount);
                                    SeleteRowCount++;
                                    //Insert Payments
                                    var jsonVar = {
                                        "sCreatedBY": $("#hfLogInUser").val(), "sAgency_ID": $("#ddlAgency").val(), "sCustomer_ID": sCustomerID, "sPaymentAmount": sCurrentPayment, "sPaymentDate": $("#txtPaymentDate").val(), "sPaymentStatus_Core": sPaymentStatus_Core, "sCategory_Core": sCategory_Core, "sAgent_ID": $("#hfLogInUser_ID").val(), "sInvoice_ID": sInvoice_ID, "sTotalAdvancePayment": $("#txtAdvancePayment").val()
                                    };
                                    CallAjaxMethod("Payment_Insert", jsonVar, "Payment_Insert_Complete");

                                }
                                i++;
                            });

                            $("#hfTotalRemainingAmount").val($("#txtRemainingAmount").val());
                            $("#hfAdvancePayment").val($("#txtAdvancePayment").val());
                            // }
                        }
                        else {

                            //Update Advance Payment direct
                            if (Validation_Advance()) {
                                //if ($("#txtPayment").val() != "" && parseFloat($("#txtPayment").val()) != 0) {
                                var dTotalAdvancePAyment = parseFloat($("#txtAdvancePayment").val()) + parseFloat($("#txtPayment").val());
                                $("#txtAdvancePayment").val(dTotalAdvancePAyment);

                                var jsonVar = {
                                    "sCurrentAdvancePayment": $("#txtPayment").val(), "sAgency_ID": $("#ddlAgency").val(), "sCustomer_ID": $("#hfCustomer_ID").val(),
                                    "sAdvancePayment_Date": $("#txtPaymentDate").val(), "sCreatedBy": $("#hfLogInUser").val(),
                                    "sTotalAdvancePayment": dTotalAdvancePAyment, "sAdvancePaymentType": $("#txtPaymentMethod").val()
                                };
                                CallAjaxMethod("AdvancePayment_Insert", jsonVar, "AdvancePayment_Insert_Complete");

                            }
                            //else
                            //    ReadXMLMessage("PTS502", "ReadXMLMessage_Complete");
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
                    $("#txtRemainingAmount").val($("#hfTotalRemainingAmount").val());
                    $("#txtAdvancePayment").val($("#hfAdvancePayment").val());
                    $("#divPayments .table-main tbody").find('input').each(function () {
                        $('input:checkbox').prop('checked', false);
                    });
                    $("#btnPayment").val("ADD");
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
                    if ($("#ddlCustomers").val() == 0 && $("#ddlAgency").val() == "NONE") {
                        var d = new Date();
                        d.setMonth(d.getMonth() - 1);
                        $("#txtDateFrom").datepicker('setDate', d);
                        $("#txtDateFrom").datepicker({ dateFormat: 'dd-M-yy' }).datepicker("option", "maxDate", '+0m +0w');
                        ReadXMLMessage("CUS107", "ReadXMLMessage_Complete");
                    }
                    else {
                        SelectPaymentsGridPageNumberBlank();
                        GetAll_CustomerPayments(sOrderBy);
                    }
                }
                catch (err) {
                    ShowMessage(err);
                }
                e.preventDefault();
            });
            $("#txtDateTo").change(function (e) {
                try {
                    if ($("#ddlCustomers").val() == 0 && $("#ddlAgency").val() == "NONE") {
                        $("#txtDateTo").datepicker('setDate', new Date());
                        $("#txtDateTo").datepicker({ dateFormat: 'dd-M-yy' }).datepicker("option", "maxDate", '+0m +0w');
                        ReadXMLMessage("CUS107", "ReadXMLMessage_Complete");
                    }
                    else {
                        SelectPaymentsGridPageNumberBlank();
                        GetAll_CustomerPayments(sOrderBy);
                    }
                }
                catch (err) {
                    ShowMessage(err);
                }
                e.preventDefault();
            });

            $("#btnExportTableData").click(function (e) {
                try {

                    if ($("#txtDateTo").val() == "" || $("#txtDateFrom").val() == "") {
                        ReadXMLMessage("REW701", "ReadXMLMessage_Complete");
                    }
                    else {
                        var sPaymentDateTo = $("#txtDateTo").val();
                        var sPaymentDateFrom = $("#txtDateFrom").val();
                        var sCustomer_ID = $("#ddlCustomers").val();
                        var sAgency_ID = $("#ddlAgency").val();
                        var sRemainingAmount = $("#hfTotalRemainingAmount").val();
                        sAgency_ID = sAgency_ID == "0" || sAgency_ID == "NONE" ? "0" : sAgency_ID;
                        if (sAgency_ID != "0") {
                            var sJsonVar = { "sPaymentDateTo": sPaymentDateTo, "sPaymentDateFrom": sPaymentDateFrom, "sAgency_ID": sAgency_ID, "sRemainingAmount": sRemainingAmount }
                            CallAjaxMethod("GenrateReportForAgency", sJsonVar, "GenrateReport_Complete");
                        }
                        else {
                            if (sCustomer_ID == "0") {
                                ReadXMLMessage("CUS107", "ReadXMLMessage_Complete");
                                return false;
                            }
                            else {
                                var sJsonVar = { "sPaymentDateTo": sPaymentDateTo, "sPaymentDateFrom": sPaymentDateFrom, "sAgency_ID": sAgency_ID, "sCustomer_ID": sCustomer_ID, "sRemainingAmount": sRemainingAmount }
                                CallAjaxMethod("GenrateReport", sJsonVar, "GenrateReport_Complete");
                            }
                        }
                    }


                    e.preventDefault();
                }
                catch (err) {
                    ShowMessage(err);
                }

            });

            $("#btnPrint").click(function (e) {
                try {
                    var sText = $(this).html().replace(/\s+/g, '');
                    if (sText == "Print") {
                        $(this).html("Printing...");
                        var tempFrame = $('#ifrGenrateReport')[0];
                        var tempFrameWindow = tempFrame.contentWindow ? tempFrame.contentWindow : tempFrame.contentDocument.defaultView;
                        tempFrameWindow.focus();
                        tempFrameWindow.print();
                        $(this).html("Print");
                    }
                }
                catch (err) {
                    ShowMessage(err);
                }
            });
            //$("#btnEmail").click(function (e) {
            //    try {                    

            //        var tempTable = $('#ifrGenrateReport').contents().find('#tblReport');

            //        $(tempTable).tableExport({ type: 'pdf', pdfmake: { enabled: true } });
            //    }
            //    catch (err) {
            //        ShowMessage(err);
            //    }
            //});
        });

        //comman function
        function ChangeOperation() {
            if ($("#rbtnlSelectOperation input:checked").val() == "CreateInvoice") {
                $("#tblPayments tbody").find('input').each(function () {
                    $('input:checkbox').prop('disabled', true);
                    $('input:checkbox').prop('checked', false);
                });
                $("#ddlCustomers").attr("disabled", false);
                $("#idMakePayment").hide();
                $("#idCreateInvoice").show();
            }
            else {
                $("#tblPayments tbody").find('input').each(function () {
                    $('input:checkbox').prop('disabled', false);
                });
                var sAgency_ID = $("#ddlAgency").val();
                if (sAgency_ID != 'NONE') {
                    $("#ddlCustomers").attr("disabled", true);
                }
                $("#ddlCustomers").val("0").trigger('change');
                $("#idCreateInvoice").hide();
                $("#idMakePayment").show();
            }
        }

        // invoice functions
        function GetGst_Value(sGST_Type) {
            try {
                $("#lblGST_Value").html("");
                $("#lblTotalAmount_WithGST").html("");
                var slblValue = "";
                var dAmount = 0.0, dAmount_WithGSt = 0.0, dTotalAmount_WithGST = 0.0;
                var arrGstType = $("#hfGSTValue").val().split('/'); //GstType|GstName|Percentage like GST_STATE|IGST|18
                for (var i = 0; i < arrGstType.length; i++) {
                    if (arrGstType[i].includes(sGST_Type)) {
                        var dGStPer = 0.0;
                        var sGstValues = arrGstType[i].split('|');
                        if ($("#txtAmount").val() != "") {
                            dAmount = parseFloat($("#txtAmount").val());
                            dGStPer = parseFloat(sGstValues[2]);//percentage value like 18
                            dAmount_WithGSt = ((dAmount * dGStPer) / 100);
                            dTotalAmount_WithGST = (dAmount_WithGSt + dTotalAmount_WithGST);
                        }
                        slblValue = slblValue + sGstValues[1] + " " + sGstValues[2] + "%=" + dAmount_WithGSt.toFixed(2) + " ";
                    }
                }
                $("#lblGST_Value").html(slblValue);
                dTotalAmount_WithGST = dTotalAmount_WithGST + dAmount;

                $("#lblTotalAmount_WithGST").html(dTotalAmount_WithGST.toFixed(2));

            }//try close
            catch (err) {
                ShowMessage(err);
            }
        }

        function SetDefault() {
            try {
                $("#txtPayment").val(0.00);
                $("#btnPayment").val("ADD");
                $("#txtAmount").val("0");
                $("#lblTotalAmount_WithGST").html("0");
                $("#txtRemainingAmount").val("0");
                $("#txtParticular").val("");
                $("#txtInvoiceDate").val("");
                $("#ddlCategories").val("0").trigger('change');
                $("#txtInvoiceDate").datepicker({ dateFormat: 'dd-M-yy' }).datepicker("setDate", new Date());
                $("#lblGST_Value").html("");
                $('#<%=rbtn_GST_Value.ClientID %>').find("input[value='GST_NONE']").prop("checked", true);
                if ($("#ddlCustomers").val() != 0 || $("#ddlCustomers").val() != 'NONE') {
                    $("#btnSubmit").prop("disabled", false);
                }
            }
            catch (err) {
                ShowMessage("Error occured: " + err);
            }

        }
        function Validation_Invoice() {
            try {
                if ($("#ddlAgency").val() == 0) {
                    ReadXMLMessage("AGES107", "ReadXMLMessage_Complete");
                    return false;
                }
                else if ($("#ddlCustomers").val() == 0) {
                    ReadXMLMessage("CUS107", "ReadXMLMessage_Complete");
                    return false;
                }
                else if ($("#ddlCategories").val() == 0) {
                    ReadXMLMessage("BSW017", "ReadXMLMessage_Complete");
                    return false;
                }
                else if ($("#txtAmount").val() == "" || parseFloat($("#txtAmount").val()) <= 0.0) {
                    ReadXMLMessage("PTS503", "ReadXMLMessage_Complete");
                    return false;
                }
                else if ($("#txtInvoiceDate").val() == "") {
                    ReadXMLMessage("BSW008", "ReadXMLMessage_Complete");
                    return false;
                }
                else if (!$.isNumeric($('#txtAmount').val())) {
                    ReadXMLMessage("PTS503", "ReadXMLMessage_Complete");
                    return false;
                }
                else
                    return true;
            }
            catch (err) {
                ShowMessage(err);
            }
        }
        function Invoice_Insert_Complete(data) {
            try {
                var $Data = data.d;
                if ($Data.length > 0) {
                    if ($Data[0].ERROR != null && $Data[0].ERROR != "") {
                        ShowMessage($Data[0].ERROR);
                    }
                    else {
                        SelectPaymentsGridPageNumberBlank();
                        GetAll_CustomerPayments(sOrderBy);
                        var sAgency_ID = $("#ddlAgency").val();
                        if (sAgency_ID != "0" && sAgency_ID != "NONE") {
                            var sJsonVar = { 'sAgency_ID': sAgency_ID };
                            CallAjaxMethod("Agency_SelectRemainingAmount", sJsonVar, "SelectRemainingAmount_Complete");
                        }
                        else {
                            var sJsonVar = { "sCustomer_ID": $("#hfCustomer_ID").val() }
                            CallAjaxMethod("Customer_SelectRemainingAmount", sJsonVar, "SelectRemainingAmount_Complete");
                        }
                        ShowMessage("Invoice " + $Data[0].Invoice_ID + " " + $Data[0].Message);
                        SetDefault();
                    }
                }
            }
            catch (err) {
                ShowMessage(err);
            }
        }
        function Customer_SelectName_Complete(data) {
            try {
                console.log(data.d);
                $('#ddlCustomers').empty();
                var sAgency_ID = $("#ddlAgency").val();
                var $Data = data.d;
                if ($Data.length > 0) {
                    if ($Data[0].ERROR != null && $Data[0].ERROR != "") {
                        ShowMessage($Data[0].ERROR);
                    }
                    else {
                        console.log($Data);
                        $("#ddlCustomers").append($("<option></option>").val(0).html('-Select Customer-'));
                        for (var i = 0; i < $Data.length; i++) {
                            $("#ddlCustomers").append($("<option></option>").val($Data[i].Customer_ID).html($Data[i].CustomerName));
                        }
                        $("#ddlCustomers").select2();

                        //select remaning amount of agency
                        if (sAgency_ID != "0" && sAgency_ID != "NONE") {
                            var sJsonVar = { 'sAgency_ID': sAgency_ID };
                            CallAjaxMethod("Agency_SelectRemainingAmount", sJsonVar, "SelectRemainingAmount_Complete");
                            if ($("#rbtnlSelectOperation input:checked").val() != "CreateInvoice")
                                $("#ddlCustomers").attr("disabled", true);
                        }
                        else {
                            $("#tblPayments thead, #tblPayments tbody").html("");
                            if ($("#rbtnlSelectOperation input:checked").val() != "CreateInvoice")
                                $("#ddlCustomers").attr("disabled", false);
                        }
                    }
                }
                else
                    $("#ddlCustomers").append($("<option></option>").val(0).html('-No Customer Found-'));
            }
            catch (err) {
                ShowMessage("There is an issue calling the function (Customer_SelectName_Complete)" + err);
            }
        }
        //

        // payment functions
        function GenrateReport_Complete(data) {
            try {
                var $Data = data.d;
                if ($Data.length > 0) {
                    if ($Data[0].ERROR != null && $Data[0].ERROR != "") {
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
                }

            }
            catch (err) {
                ShowMessage(err);
            }

        }
        function Validation_Payment() {
            try {

                if ($("#ddlAgency").val() == 0) {
                    ReadXMLMessage("AGES107", "ReadXMLMessage_Complete");
                    return false;
                }
                else if ($("#ddlAgency").val() == "NONE" && $("#ddlCustomers").val() == 0) {
                    ReadXMLMessage("CUS107", "ReadXMLMessage_Complete");
                    return false;
                }
                else if ($("#txtPaymentDate").val() == "") {
                    ReadXMLMessage("BSW008", "ReadXMLMessage_Complete");
                    return false;
                }
                else
                    return true;
            }
            catch (err) {
                ShowMessage(err);
            }
        }
        function Validation_Advance() {
            try {
                if ($("#txtPayment").val() == "" || parseFloat($("#txtPayment").val()) == 0) {
                    ReadXMLMessage("PTS502", "ReadXMLMessage_Complete");
                    return false;
                }
                else if ($("#txtPaymentMethod").val() == "") {
                    ShowMessage("Enter advance payment method");
                    return false;
                }

                else
                    return true;
            }
            catch (err) {
                ShowMessage(err);
            }
        }
        function SelectRemainingAmount_Complete(data) {
            try {
                var $Data = data.d;
                if ($Data.length > 0) {
                    if ($Data[0].ERROR != null && $Data[0].ERROR != "") {
                        ShowMessage($Data[0].ERROR);
                    }
                    else {
                        var fRemainingAmount = $Data[0].RemainingAmount == "" ? "0.00" : parseFloat($Data[0].RemainingAmount).toFixed(2);
                        $("#txtRemainingAmount").val(fRemainingAmount);
                        $("#hfTotalRemainingAmount").val(fRemainingAmount);
                        var fAdvancePayment = $Data[0].AdvancePayment == "" ? "0.00" : parseFloat($Data[0].AdvancePayment).toFixed(2);
                        $("#hfAdvancePayment").val(fAdvancePayment);
                        $("#txtAdvancePayment").val(fAdvancePayment);
                    }

                    SelectPaymentsGridPageNumberBlank();
                    GetAll_CustomerPayments(sOrderBy);
                }
            }
            catch (err) {
                ShowMessage("There is an issue calling the function (SelectRemainingAmount_Complete)" + err);
            }
        }

        function Customer_Update_AdvancePayment_Complete(data) {
            try {
                var $Data = data.d;
                if ($Data.length > 0) {
                    if ($Data[0].ERROR != null && $Data[0].ERROR != "") {
                        ShowMessage($Data[0].ERROR);
                    }
                    else {
                        $("#btnPayment").val("ADD");
                        $("#txtPayment").val(0.00);
                        SelectPaymentsGridPageNumberBlank();
                        GetAll_CustomerPayments(sOrderBy);
                        ShowMessage($Data[0].Message);
                    }
                }
            }
            catch (err) {
                ShowMessage(err);
            }
        }
        function AdvancePayment_Insert_Complete(data) {
            try {
                $Data = data.d;
                if ($Data.length > 0) {
                    if ($Data[0].ERROR != null && $Data[0].ERROR != "") {
                        ShowMessage($Data[0].ERROR);
                    }
                    else {
                        $("#hfAdvancePayment").val($("#txtAdvancePayment").val());
                        $("#txtPayment").val(0.00);
                        $("#txtPaymentMethod").val("");
                        ShowMessage($Data[0].Message);
                    }
                }
            }
            catch (err) {
                ShowMessage(err);
            }
        }
        var varCountPayment = 0;
        function Payment_Insert_Complete(data) {
            try {

                var $Data = data.d;
                if ($Data.length > 0) {
                    if ($Data[0].ERROR != null && $Data[0].ERROR != "") {
                        ShowMessage($Data[0].ERROR);
                    }
                    else {
                        varCountPayment++;
                        var sAgency_ID = $("#ddlAgency").val();
                        var varRowCount = $("#hfGridSelectedRow_Count").val();

                        if (varRowCount == varCountPayment) {
                            //var sTotalRemainigAmount = parseInt($("#txtRemainingAmount").val()) == 0 ? "" : $("#txtRemainingAmount").val();
                            var sTotalRemainigAmount = $("#txtRemainingAmount").val();
                            if (sAgency_ID != "0" && sAgency_ID != "NONE") {
                                //Update Advance Payment after agency Payment 
                                jsonVar = { "sTotalAdvancePayment": $("#txtAdvancePayment").val(), "sAgency_ID": sAgency_ID, "sTotalRemainigAmount": sTotalRemainigAmount };
                                CallAjaxMethod("Agency_Update_AdvancePayment", jsonVar, "Customer_Update_AdvancePayment_Complete");
                            }
                            else {
                                //Update Advance Payment after customer Payment 
                                jsonVar = { "sTotalAdvancePayment": $("#txtAdvancePayment").val(), "sCustomer_ID": $("#hfCustomer_ID").val(), "sTotalRemainigAmount": sTotalRemainigAmount };
                                CallAjaxMethod("Customer_Update_AdvancePayment", jsonVar, "Customer_Update_AdvancePayment_Complete");
                            }
                            varCountPayment = 0;
                        }
                    }
                }

            }
            catch (err) {
                ShowMessage(err);
            }
        }
        function ReadXMLMessage_Complete(data) {
            try {

                ShowMessage(data);
            }
            catch (err) {
                ShowMessage("There is an issue calling the function (SetDefault)" + err);
            }
        }
    </script>

</asp:Content>
