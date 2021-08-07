<%@ Page Title="Daily Notes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Paybook.WebUI.Notes.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="hfLogInUser" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfDataID" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfDailyNotesGridPageNumber" runat="server" ClientIDMode="Static" />
    <div class="container-fluid">
        <div class="row bg-secondary">
            <div class="col-lg-2">
                <h2>Daily Notes</h2>
            </div>
            <div class="col-lg-10 text-right">
                <div class="btn-group" role="group">
                    <button type="button" clientidmode="Static" class="btn btn-primary fwt-btn-height" title="Setting" onclick="">
                        &nbsp;<i class="fa fa-ellipsis-h fwt-large"></i>&nbsp;</button>
                    <button type="button" clientidmode="Static" class="btn btn-primary fwt-btn-height" title="Sync" onclick="location.href=location.href;">
                        &nbsp;<i class="fa fa-refresh fwt-large"></i>&nbsp;</button>
                    <button type="button" clientidmode="Static" class="btn btn-primary fwt-btn-height" title="Add a Note" onclick="return OpenPartialPagePopup('note/create','CREATE NOTE');">
                        &nbsp;<i class="fa fa-plus fwt-large"></i>&nbsp; CREATE NOTE&nbsp;</button>
                </div>
            </div>
        </div>
    </div>

    <div class="container-fluid pt-3">
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
                        <asp:Label ID="lblPageNumber" ClientIDMode="Static" runat="server"></asp:Label></span>
                    <span class="fwt-button">
                        <button id="btnNext" clientidmode="Static" class="fwt-btn fwt-round fwt-ripple fwt-dropdown-click fwt-light-green">
                            &nbsp;<i class="fa fa-forward"></i>&nbsp;</button>
                    </span>
                </div>
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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">

    <script src="<%=Application["Path"] %>_Layouts/JS/custom_dailynotes.js" type="text/javascript"></script>
    <script type="text/javascript">
        var sOrderBy = "";
        var XmlData = "";
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
