<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Paybook.WebUI.Agent.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="hfAgentsGridPageNumber" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfAgent_ID" runat="server" ClientIDMode="Static" />
    <div class="fwt-container">
        <h4 class="fwt-bold fwt-text-deep-orange">Agents
        </h4>
        <div id="idLabelError" runat="server" clientidmode="Static" class="fwt-container fwt-padding-16 fwt-pale-yellow fwt-border fwt-border-yellow"
            style="display: none">
        </div>
        <div class="fwt-padding-12 ">
            <div class="fwt-col l6 m6 s12">
                <div class="">
                    <button class="fwt-btn fwt-ripple fwt-dropdown-click fwt-amber" onclick="return OpenExportNavigations()">
                        <i class="fa fa-bars"></i>&nbsp;Export Table Data</button>
                    <div id="idExportNavigations" class="fwt-dropdown-content fwt-hide">
                        <a href="#" onclick="$('#divAgents .table-main').tableExport({type:'xml',escape:'false',ignoreColumn:'[0]'});">
                            <img src='_Layouts/IMG/export-icons/xml.png' width='24px' alt="" />
                            XML</a> <a href="#" onclick="$('#divAgents .table-main').tableExport({type:'excel',escape:'false',ignoreColumn:'[0]'});">
                                <img src='_Layouts/IMG/export-icons/xls.png' width='24px' alt="" />
                                XLS</a><a href="#" onclick="$('#divAgents .table-main').tableExport({type:'pdf',pdfFontSize:'7',escape:'false',ignoreColumn:'[0]'});">
                                    <img src='_Layouts/IMG/export-icons/pdf.png' width='24px' alt="" />
                                    PDF</a>
                    </div>
                </div>
            </div>
            <div class="fwt-right-align fwt-col l6 m6 s12">
                <button runat="server" id="btnAgentEdit" class="fwt-btn fwt-hover-indigo" onserverclick="btnAgentEdit_Click"
                    title="Edit Agent" style="background-color: #3a4f63;">
                    > Edit Agent
                </button>
                <button runat="server" id="btnAgentCreate" class="fwt-btn fwt-hover-indigo" onserverclick="btnAgentCreate_Click"
                    title="Add Agent" style="background-color: #3a4f63;">
                    > Add Agent
                </button>
            </div>
            <div class="fwt-clear ">
            </div>
        </div>
        <div class="fwt-padding-12" id="idPageNumber">
            <div class="fwt-col l2 m2 s12">
                <asp:Label ID="lblAgentsPageNumber" ClientIDMode="Static" runat="server"></asp:Label>
            </div>
            <div class="fwt-col l10 m10 s12">
                <asp:RadioButtonList ID="rbtnIsActive" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                    <asp:ListItem Value="0">In Active</asp:ListItem>
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
