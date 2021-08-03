<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Particular.aspx.cs" Inherits="Paybook.WebUI.Invoice.Particular" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="hfPayments_ForInvoice_PageNumber" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfCustomer_ID" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfInvoice_ID" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfCategory" runat="server" ClientIDMode="Static" />
    <div class="fwt-container">
        <h4>Particular
        </h4>
        <div class="fwt-right">
            <button runat="server" id="btnBack" class="fwt-btn fwt-hover-indigo"
                title="Add Customer" style="background-color: #3a4f63;" onserverclick="btnBack_ServerClick1">
                > Back
            </button>
        </div>
        <div class="fwt-clear">
        </div>
        <div id="divParticular" class="fwt-text-dark-grey " style="display: block" runat="server">
            <fieldset>
                <legend style="font-size: 15px; color: Blue">...</legend>
                <div class="fwt-padding-4">
                    <div class="fwt-col l6 m6 s12">
                        Invoice ID:
                    </div>
                    <div class="fwt-col l6 m6 s12">
                        <asp:Label ID="lblInvoiceID" runat="server" ClientIDMode="Static" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="fwt-clear">
                    </div>
                </div>
                <div class="fwt-padding-4">
                    <div class="fwt-col l6 m6 s12">
                        Particular:
                    </div>
                    <div class="fwt-col l6 m6 s12">
                        <asp:Label ID="lblParticular" runat="server" ClientIDMode="Static" Style="word-wrap: normal; word-break: break-all;"></asp:Label>
                    </div>
                    <div class="fwt-clear">
                    </div>
                </div>
                <div class="fwt-padding-4">
                    <div class="fwt-col l6 m6 s12">
                        Invoice Date:
                    </div>
                    <div class="fwt-col l6 m6 s12">
                        <asp:Label ID="lblInvoiceDate" runat="server" ClientIDMode="Static"></asp:Label>
                    </div>
                    <div class="fwt-clear">
                    </div>
                </div>
                <div class="fwt-padding-4">
                    <div class="fwt-col l6 m6 s12">
                        Type:
                    </div>
                    <div class="fwt-col l6 m6 s12">
                        <asp:Label ID="lblCategory" runat="server" ClientIDMode="Static"></asp:Label>
                    </div>
                    <div class="fwt-clear">
                    </div>
                </div>
                <div class="fwt-padding-4">
                    <div class="fwt-col l6 m6 s12">
                        Amount:
                    </div>
                    <div class="fwt-col l6 m6 s12">
                        <asp:Label ID="lblAmount" runat="server" ClientIDMode="Static"></asp:Label>
                    </div>
                    <div class="fwt-clear">
                    </div>
                </div>
                <div class="fwt-padding-4">
                    <div class="fwt-col l6 m6 s12">
                        Invoice Status:
                    </div>
                    <div class="fwt-col l6 m6 s12">
                        <asp:Label ID="lblInvoiceStatus" runat="server" ClientIDMode="Static" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="fwt-clear">
                    </div>
                </div>
                <div class="fwt-padding-4">
                    <div class="fwt-col l6 m6 s12">
                        Last Payment Date:
                    </div>
                    <div class="fwt-col l6 m6 s12">
                        <asp:Label ID="lblLastPaymentDate" runat="server" ClientIDMode="Static"></asp:Label>
                    </div>
                    <div class="fwt-clear">
                    </div>
                </div>
                <div class="fwt-padding-4">
                    <div class="fwt-col l6 m6 s12">
                        Is Active:
                    </div>
                    <div class="fwt-col l6 m6 s12">
                        <asp:Label ID="lblIsActive" runat="server" ClientIDMode="Static"></asp:Label>
                    </div>
                    <div class="fwt-clear">
                    </div>
                </div>
                <div class="fwt-padding-4">
                    <div class="fwt-col l6 m6 s12">
                        Created BY:
                    </div>
                    <div class="fwt-col l6 m6 s12">
                        <asp:Label ID="lblCreatedBY" runat="server" ClientIDMode="Static"></asp:Label>
                    </div>
                    <div class="fwt-clear">
                    </div>
                </div>
                <div class="fwt-padding-4">
                    <div class="fwt-col l6 m6 s12">
                        Created Date:
                    </div>
                    <div class="fwt-col l6 m6 s12">
                        <asp:Label ID="lblCreatedDT" runat="server" ClientIDMode="Static"></asp:Label>
                    </div>
                    <div class="fwt-clear">
                    </div>
                </div>
                <div class="fwt-padding-4">
                    <div class="fwt-col l6 m6 s12">
                        Last Modified BY:
                    </div>
                    <div class="fwt-col l6 m6 s12">
                        <asp:Label ID="lblModifiedBY" runat="server" ClientIDMode="Static"></asp:Label>
                    </div>
                    <div class="fwt-clear">
                    </div>
                </div>
                <div class="fwt-padding-4">
                    <div class="fwt-col l6 m6 s12">
                        Last Modified Date:
                    </div>
                    <div class="fwt-col l6 m6 s12">
                        <asp:Label ID="lblModifiedDT" runat="server" ClientIDMode="Static"></asp:Label>
                    </div>
                    <div class="fwt-clear">
                    </div>
                </div>
                <div class="fwt-padding-4">
                    <div class="fwt-col l6 m6 s12">
                        Remark:
                    </div>
                    <div class="fwt-col l6 m6 s12">
                        <asp:TextBox ID="txtRemak" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" TextMode="MultiLine"  height="150px"
                            autocomplete="off" Text=""></asp:TextBox>
                    </div>                  
                    <div class="fwt-clear">
                    </div>
                </div>
                 <div class="fwt-padding-4" style="text-align:right">                
                        <asp:Button ID="btnSubmitRemark" runat="server" Text="Update Remark" ClientIDMode="Static" CssClass="fwt-btn fwt-green fwt-hover-indigo" OnClick="btnSubmitRemark_Click" />
                    
                    <div class="fwt-clear">
                    </div>
                </div>
            </fieldset>
            <div class="fwt-clear">
            </div>
        </div>
        <div id="menutabs" style="width: 100%;">
            <ul>
                <li><a href="#divCustomerDeatils" id="CustomerDeatils" clientidmode="Static">Customer</a></li>
                <li><a href="#divPayment_ForInvoice" id="PaymentsGrid_ForInvoice" clientidmode="Static">Payments</a></li>
            </ul>
            <div id="divCustomerDetails" class="fwt-container fwt-text-dark-grey ">
                <fieldset>
                    <legend style="font-size: 15px; color: Blue">...</legend>
                    <div class="fwt-padding-4">
                        <div class="fwt-col l6 m6 s12">
                            Customer Name:
                        </div>
                        <div class="fwt-col l6 m6 s12">
                            <asp:Label ID="lblCustomerName" runat="server" ClientIDMode="Static"></asp:Label>
                        </div>
                        <div class="fwt-clear">
                        </div>
                    </div>
                       <div class="fwt-padding-4">
                        <div class="fwt-col l6 m6 s12">
                            Agency Name:
                        </div>
                        <div class="fwt-col l6 m6 s12">
                            <asp:Label ID="lblAgencyName" runat="server" ClientIDMode="Static"></asp:Label>
                        </div>
                        <div class="fwt-clear">
                        </div>
                    </div>
                    <div class="fwt-padding-4">
                        <div class="fwt-col l6 m6 s12">
                            Date Of Birth:
                        </div>
                        <div class="fwt-col l6 m6 s12">
                            <asp:Label ID="lblCustomerDOB" runat="server" ClientIDMode="Static"></asp:Label>
                        </div>
                        <div class="fwt-clear">
                        </div>
                    </div>
                    <div class="fwt-padding-4">
                        <div class="fwt-col l6 m6 s12">
                            Address:
                        </div>
                        <div class="fwt-col l6 m6 s12">
                            <asp:Label ID="lblCustomerAddress" runat="server" ClientIDMode="Static"></asp:Label>
                        </div>
                        <div class="fwt-clear">
                        </div>
                    </div>
                    <div class="fwt-padding-4">
                        <div class="fwt-col l6 m6 s12">
                            Phone Number Primary:
                        </div>
                        <div class="fwt-col l6 m6 s12">
                            <asp:Label ID="lblCustomerPhoneNumberPrimary" runat="server" ClientIDMode="Static"></asp:Label>
                        </div>
                        <div class="fwt-clear">
                        </div>
                    </div>
                    <div class="fwt-padding-4">
                        <div class="fwt-col l6 m6 s12">
                            Phone Number Secondary:
                        </div>
                        <div class="fwt-col l6 m6 s12">
                            <asp:Label ID="lblCustomerPhoneNumberSecondary" runat="server" ClientIDMode="Static"></asp:Label>
                        </div>
                        <div class="fwt-clear">
                        </div>
                    </div>
                    <div class="fwt-padding-4">
                        <div class="fwt-col l6 m6 s12">
                            Email:
                        </div>
                        <div class="fwt-col l6 m6 s12">
                            <asp:Label ID="lblCustomerEmail" runat="server" ClientIDMode="Static"></asp:Label>
                        </div>
                        <div class="fwt-clear">
                        </div>
                    </div>
                    <div class="fwt-padding-4">
                        <div class="fwt-col l6 m6 s12">
                            Remaining Amount:
                        </div>
                        <div class="fwt-col l6 m6 s12">
                            <asp:Label ID="lblRemainingAmount" runat="server" ClientIDMode="Static"></asp:Label>
                        </div>
                        <div class="fwt-clear">
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="fwt-container fwt-text-dark-grey" id="divPayment_ForInvoice">
                <div class="fwt-padding-4 " id="idPageNumber">
                    <div class="fwt-col l2 m3 s12">
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
                </div>
                <div class="fwt-padding-4 " id="idpayment_ForInvoice_Grid">
                    <div id="div_HeadertblPayments_ForInvoice">
                    </div>
                    <div class="table-Scroll" id="div_ScrolltblPayments_ForInvoice">
                        <table id="tblPayments_ForInvoice" border="0" width="100%" style="word-wrap: break-word;" class="table-main fwt-table">
                            <thead>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                        <div id="divGridLoadingtblPayments_ForInvoice" style="text-align: center;">
                        </div>
                        <asp:Label ID="lblGridMessagetblPayments_ForInvoice" runat="server" CssClass="Error" ClientIDMode="Static"></asp:Label>
                    </div>
                </div>
            </div>
        </div>

    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    
    <script src="<%=Application["Path"] %>_Layouts/JS/custom_payment.js" type="text/javascript"></script>
    <script type="text/javascript">
        var sOrderBy = "";
        $(document).ready(function () {
            $(function () {
                $("#menutabs").tabs();
            });
            $("#PaymentsGrid_ForInvoice").click(function () {

                $("#divCustomerDetails").attr("style", "display:none");
                $("#divPayment_ForInvoice").attr("style", "display:block");
                SelectPayments_ForInvoice_PageNumberBlank();
                PaymentGrid_ForInvoice(sOrderBy);
            });
            $("#CustomerDeatils").click(function () {
                $("#divCustomerDetails").attr("style", "display:block");
                $("#divPayment_ForInvoice").attr("style", "display:none");
            });
            $('#btnNext').click(function (e) {
                try {
                    // var iTotalRows = parseInt($("#lblPageNumber").text().split('of')[1]);
                    // var iTotalPageNumber = Math.ceil(iTotalRows / 10);
                    var iTotalPageNumber = parseInt($("#lblPageNumber").text().split('of')[1]);
                    var iCurrentPageNumber = parseInt($('#hfPayments_ForInvoice_PageNumber').val());
                    if (iCurrentPageNumber >= 0 && iCurrentPageNumber < iTotalPageNumber - 1) {
                        if (IsProcessingHomeGrid)
                            return false;
                        else {
                            IsProcessingHomeGrid = true;
                            var iPayments_ForInvoice_PageNumber = $('#hfPayments_ForInvoice_PageNumber').val() == "" ? 0 : parseInt($('#hfPayments_ForInvoice_PageNumber').val());
                            iPayments_ForInvoice_PageNumber = iPayments_ForInvoice_PageNumber + 1;
                            $('#hfPayments_ForInvoice_PageNumber').val(iPayments_ForInvoice_PageNumber);
                            PaymentGrid_ForInvoice(sOrderBy);
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
                    if ($('#hfPayments_ForInvoice_PageNumber').val() > 0) {
                        if (IsProcessingHomeGrid)
                            return false;
                        else {
                            IsProcessingHomeGrid = true;
                            var iPayments_ForInvoice_PageNumber = parseInt($('#hfPayments_ForInvoice_PageNumber').val());
                            iPayments_ForInvoice_PageNumber = iPayments_ForInvoice_PageNumber - 1;
                            if (iPayments_ForInvoice_PageNumber == 0)
                                $("#tblPayments_ForInvoice thead").html("");
                            $('#hfPayments_ForInvoice_PageNumber').val(iPayments_ForInvoice_PageNumber);
                            PaymentGrid_ForInvoice(sOrderBy);
                        }
                    }
                }
                catch (err) {
                    ShowMessage(err);
                }
                e.preventDefault();

            });
        });
    </script>

</asp:Content>
