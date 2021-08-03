<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="daily_notes.aspx.cs" Inherits="Paybook.WebUI.notes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%=Application["Path"] %>_Layouts/JS/custom_dailynotes.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="fwt-container">
        <asp:HiddenField ID="hfLogInUser" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hfDailyNotesGridPageNumber" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hfDataID" runat="server" ClientIDMode="Static" />
        <h4>daily Notes
        </h4>
        <div class="fwt-padding-4">
            <div class="fwt-col l3 m3 s12">
                Vehicle Number:            
                <asp:TextBox ID="txtVehicleNo" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" AutoComplete="off"></asp:TextBox>
            </div>
            <div class="fwt-col l3 m3 s12">
                Name
                <asp:TextBox ID="txtName" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" AutoComplete="off"></asp:TextBox>
            </div>
            <div class="fwt-col l3 m3 s12">
                Mobile Number:
                <asp:TextBox ID="txtMobileNumber" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" AutoComplete="off"></asp:TextBox>
            </div>
            <div class="fwt-col l3 m3 s12">
                Work:
                <asp:TextBox ID="txtWork" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" AutoComplete="off"></asp:TextBox>
            </div>
        </div>
        <div class="fwt-padding-4">
            <div class="fwt-col l3 m3 s12">
                Total Amount:
                <asp:TextBox ID="txtTotalAmount" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" AutoComplete="off"></asp:TextBox>
            </div>
            <div class="fwt-col l3 m3 s12">
                Awak:
                <asp:TextBox ID="txtAwak" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" AutoComplete="off"></asp:TextBox>
            </div>    
            <div class="fwt-col l3 m3 s12">
               Jawak:
                <asp:TextBox ID="txtJawak" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" AutoComplete="off"></asp:TextBox>
            </div>    
            <div class="fwt-col l3 m3 s12">
                Balance:
                <asp:TextBox ID="txtBalance" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" AutoComplete="off"></asp:TextBox>
            </div>
                
            <div class="fwt-clear">
            </div>
        </div>
        <div class="fwt-padding-4">
            <div>Notes:</div>
            <div>
                <asp:TextBox ID="txtDailyNotes" runat="server" TextMode="MultiLine" Width="80%" Height="100px" ClientIDMode="Static" AutoComplete="off"></asp:TextBox>
            </div>
            <div class="fwt-clear">
            </div>
        </div>
        <div class="fwt-padding-4 fwt-center">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="fwt-btn fwt-hover-indigo" Style="background-color: #3a4f63;" />
        </div>
        <div class="fwt-padding-4 " id="idPageNumber">
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
            <div id="div_HeadertblDailyNotes">
            </div>
            <div class="table-Scroll" id="div_ScrolltblDailyNotes">
                <table id="tblDailyNotes" border="0" width="100%" style="word-wrap: break-word;" class="table-main fwt-table">
                    <thead>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
                <div id="divGridLoadingtblDailyNotes" style="text-align: center;">
                </div>
                <asp:Label ID="lblGridMessagetblDailyNotes" runat="server" CssClass="Error" ClientIDMode="Static"></asp:Label>
            </div>
        </div>

        <div class="fwt-clear">
        </div>
    </div>
    <script type="text/javascript">
        var sOrderBy = "";
        XmlData = "";
        $(document).ready(function () {
            // SetCustomersGridPageNumberBlank();
            GetAllDailyNotes(sOrderBy);
            $("#idPageNumber").hide();
            $('#btnNext').click(function (e) {
                try {
                    var iTotalPageNumber = parseInt($("#lblPageNumber").text().split('of')[1]);
                    var iCurrentPageNumber = parseInt($('#hfDailyNotesGridPageNumber').val());
                    if (iCurrentPageNumber >= 0 && iCurrentPageNumber < iTotalPageNumber - 1) {
                        if (IsProcessingHomeGrid)
                            return false;
                        else {
                            IsProcessingHomeGrid = true;
                            var iDailyNotesGridPageNumber = $('#hfDailyNotesGridPageNumber').val() == "" ? 0 : parseInt($('#hfDailyNotesGridPageNumber').val());
                            iDailyNotesGridPageNumber = iDailyNotesGridPageNumber + 1;
                            $('#hfDailyNotesGridPageNumber').val(iDailyNotesGridPageNumber);
                            GetAllDailyNotes(sOrderBy);
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
                    if ($('#hfDailyNotesGridPageNumber').val() > 0) {
                        if (IsProcessingHomeGrid)
                            return false;
                        else {
                            IsProcessingHomeGrid = true;
                            var iDailyNotesGridPageNumber = parseInt($('#hfDailyNotesGridPageNumber').val());
                            iDailyNotesGridPageNumber = iDailyNotesGridPageNumber - 1;
                            if (iDailyNotesGridPageNumber == 0)
                                $("#tblDailyNotes thead").html("");
                            $('#hfDailyNotesGridPageNumber').val(iDailyNotesGridPageNumber);
                            GetAllDailyNotes(sOrderBy);
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
