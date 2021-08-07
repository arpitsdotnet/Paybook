<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DashboardCounterUserControl.ascx.cs" Inherits="Paybook.WebUI.UserControls.DashboardCounterUserControl" %>

<div class="col-lg-2 col-md-12 <%=BgColorClass%> <%=BgColorHoverClass%>" style="border-bottom: solid 5px rgba(0, 0, 0, .3);">
    <div class="fwt-container-small fwt-padding-8" style="position: relative;">
        <div class="fwt-bold fwt-large fwt-padding-8">
            <%=Total%>
        </div>
        <div class="fwt-tiny">
            <%=Count%>&nbsp;<%=CountText%>
        </div>
        <div style="font-size: 30px; position: absolute; top: 0px; right: 10px; <%=RsSymbolColor%>"><i class="fa fa-inr"></i></div>
    </div>
</div>
