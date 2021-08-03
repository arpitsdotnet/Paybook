<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Paybook.WebUI.Agent.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="hfAgentsGridPageNumber" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfAgent_ID" runat="server" ClientIDMode="Static" />
    <div class="container-fluid">
        <div class="row bg-secondary">
            <div class="col-lg-2">
                <h2>Agents</h2>
            </div>
            <div class="col-lg-10 text-right">
                <div class="btn-group" role="group">
                    <button type="button" clientidmode="Static" class="btn btn-primary fwt-btn-height" title="Setting" onclick="">
                        &nbsp;<i class="fa fa-ellipsis-h fwt-large"></i>&nbsp;</button>
                    <button type="button" clientidmode="Static" class="btn btn-primary fwt-btn-height" title="Sync" onclick="location.href=location.href;">
                        &nbsp;<i class="fa fa-refresh fwt-large"></i>&nbsp;</button>
                    <button type="button" clientidmode="Static" class="btn btn-primary fwt-btn-height" title="Add a Note" onclick="return OpenPartialPagePopup('agent/create','CREATE AGENT');">
                        &nbsp;<i class="fa fa-plus fwt-large"></i>&nbsp; CREATE AGENT&nbsp;</button>
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid">
        <div id="idLabelError" runat="server" clientidmode="Static" class="fwt-container fwt-padding-16 fwt-pale-yellow fwt-border fwt-border-yellow"
            style="display: none">
        </div>
        <div class="fwt-padding-12" id="idPageNumber">
            <div class="fwt-col l2 m2 s12">
                <asp:Label ID="lblAgentsPageNumber" ClientIDMode="Static" runat="server"></asp:Label>
            </div>
            <div class="fwt-col l10 m10 s12">
                <asp:RadioButtonList ID="rbtnIsActive" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Selected="True">&nbsp;Active&nbsp;</asp:ListItem>
                    <asp:ListItem Value="0">&nbsp;Inactive&nbsp;</asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div class="fwt-clear ">
            </div>
        </div>
        <div>
            <div id="divAgents" style="display: block;">
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
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            var sOrderBy = "";
            SetAgentsGridPageNumberBlank();
            GetAllAgents(sOrderBy);
            // if ($("#idLabelError").css("display") == "block") {
            //   $("#idLabelError").delay(5000).slideUp(300);
            // }
            $('#divAgents .table-scroll').scroll(function (e) {
                try {
                    if ($('#hfAgentsGridPageNumber').val() > 0) {
                        if (IsProcessingHomeGrid)
                            return false;

                        if ($("#divAgents .table-scroll").scrollTop() >= ($("#divAgents .table-main tbody").height() - $("#divAgents .table-scroll").height())) {
                            IsProcessingHomeGrid = true;
                            GetAllAgents(sOrderBy);
                        }
                    }
                }
                catch (err) {
                    ShowMessage(err);
                }
            });
        });
        $("#rbtnIsActive").change(function (e) {
            try {
                sOrderBy = "";
                SetAgentsGridPageNumberBlank();
                GetAllAgents(sOrderBy);
            }
            catch (err) {
                ShowMessage(err);
            }
        });
    </script>

</asp:Content>
