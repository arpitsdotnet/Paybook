<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="index.aspx.cs" Inherits="Paybook.WebUI.index" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfLogInUser" runat="server" />
    <asp:HiddenField ID="hfLogInUser_ID" runat="server" />

    <div>
        <div id="idLabelError" runat="server" clientidmode="Static" class="fwt-container fwt-padding-16 fwt-pale-yellow fwt-border fwt-border-yellow"
            style="display: none; padding-bottom: 4px;">
        </div>
        <div class="fwt-col l9 m9 s12">
            <div class="fwt-padding-16">
                <div>
                    <div class="fwt-col l20 m12 s12 fwt-blue-grey fwt-hover-grey">
                        <div class="fwt-container-small fwt-padding-8" style="position: relative;">
                            <div class="fwt-bold fwt-medium fwt-padding-8">
                                <%-- ₹--%>
                                <asp:Label ID="lblTotal_OpenInvoice" runat="server"></asp:Label>
                            </div>
                            <div class="fwt-tiny" id="idCounts_OpenInvoice">
                                <asp:Label ID="lblCounts_OpenInvoice" runat="server"></asp:Label>
                                Open Invoices  
                            </div>
                            <div style="font-size: 30px; position: absolute; top: 0px; right: 10px; color: #4d5f68;"><i class="fa fa-inr"></i></div>
                        </div>
                        <div style="background: #4d5f68; height: 5px;"></div>
                    </div>
                    <div class="fwt-col l20 m12 s12 fwt-deep-purple fwt-hover-grey">
                        <div class="fwt-container-small fwt-padding-8" style="position: relative;">
                            <div class="fwt-bold fwt-medium fwt-padding-8">
                                <%-- ₹--%>
                                <asp:Label ID="lblTotal_OpenLastMonth" runat="server"></asp:Label>
                            </div>
                            <div class="fwt-tiny" id="idCounts_OpenInvoice_LastMonth">
                                <asp:Label ID="lblCounts_OpenLastMonth" runat="server"></asp:Label>
                                Open Invoices Last Month 
                            </div>
                            <div style="font-size: 30px; position: absolute; top: 0px; right: 10px; color: #562f9a;"><i class="fa fa-inr"></i></div>
                        </div>
                        <div style="background: #562f9a; height: 5px;"></div>
                    </div>
                    <div class="fwt-col l20 m12 s12 fwt-red fwt-hover-grey">
                        <div class="fwt-container-small fwt-padding-8" style="position: relative;">
                            <div class="fwt-bold fwt-medium fwt-padding-8">
                                <%-- ₹--%>
                                <asp:Label ID="lblTotal_Overdue" runat="server"></asp:Label>
                            </div>
                            <div class="fwt-tiny" id="idCounts_Overdue">
                                <asp:Label ID="lblCounts_Overdue" runat="server"></asp:Label>
                                Overdue 
                            </div>
                            <div style="font-size: 30px; position: absolute; top: 0px; right: 10px; color: #bd4339;"><i class="fa fa-inr"></i></div>
                        </div>
                        <div style="background: #bd4339; height: 5px;"></div>
                    </div>
                    <div class="fwt-col l20 m12 s12 fwt-teal fwt-hover-grey">
                        <div class="fwt-container-small fwt-padding-8" style="position: relative;">
                            <div class="fwt-bold fwt-medium fwt-padding-8">
                                <%--   ₹--%>
                                <asp:Label ID="lblTotal_Paid_Partial" runat="server"></asp:Label>
                            </div>
                            <div class="fwt-tiny" id="idCounts_Paid_Partial">
                                <asp:Label ID="lblCounts_Paid_Partial" runat="server"></asp:Label>
                                Paid Partial Last Month
                            </div>
                            <div style="font-size: 30px; position: absolute; top: 0px; right: 10px; color: #007469;"><i class="fa fa-inr"></i></div>
                        </div>
                        <div style="background: #007469; height: 5px;"></div>
                    </div>
                    <div class="fwt-col l20 m12 s12 fwt-green fwt-hover-grey">
                        <div class="fwt-container-small fwt-padding-8" style="position: relative;">
                            <div class="fwt-bold fwt-medium fwt-padding-8">
                                <%--₹--%>
                                <asp:Label ID="lblTotal_PaidLastMonth" runat="server"></asp:Label>
                            </div>
                            <div class="fwt-tiny" id="idCounts_PaidLast30Days">
                                <asp:Label ID="lblCounts_PaidLastMonth" runat="server"></asp:Label>
                                Paid Last Month
                            </div>
                            <div style="font-size: 30px; position: absolute; top: 0px; right: 10px; color: #2d8630;"><i class="fa fa-inr"></i></div>
                        </div>
                        <div style="background: #2d8630; height: 5px;"></div>
                    </div>
                    <div class="fwt-clear"></div>
                </div>
                <div class="fwt-clear fwt-padding-8 fwt-bottombar fwt-border-grey">
                </div>
            </div>
            <div>
                <div class="fwt-col l5 m12 s12">
                    <div class="fwt-container-small">
                        <h4 class="fwt-bold fwt-text-deep-orange">Summary of last 7 days</h4>
                        <div id="idInvoicePaymentCount_Chart"></div>
                    </div>
                </div>
                <div class="fwt-col l7 m12 s12" style="border-left: 1px solid #ccc;">
                    <div class="fwt-container-small">
                        <h4 class="fwt-bold fwt-text-deep-orange">Amount of Invoice / Payment</h4>
                        <div id="idIPaymentAgainstInvoice_Chart"></div>
                    </div>
                </div>
                <div class="fwt-clear"></div>
            </div>
            <div class="fwt-clear fwt-padding-12 fwt-bottombar fwt-border-grey">
            </div>
        </div>
        <div class="fwt-col l3 m3 s12">
            <div class="fwt-container-small fwt-padding-16">
                <div id="idLatestCustomer" class="Dashboard_CustomerCount">
                    <div>
                        <div class="fwt-col l6 m6 s6">
                            <div>Customer<span class="fwt-tiny"> (Last 7 days)</span></div>
                            <div class="fwt-tiny">
                                <asp:Label ID="lblCustomerCount" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="fwt-col l6 m6 s6">
                            <div id="idCustomer_Chart"></div>
                        </div>
                        <div class="fwt-clear"></div>
                    </div>
                    <div class="fwt-right-align">
                        Total Payment: <i class="fa fa-inr"></i>
                        <asp:Label ID="lblTotalMonthSaleValue" runat="server" CssClass="fwt-bold"></asp:Label>
                    </div>                    
                </div>
                <div style="background: #0b1115; height: 5px;"></div>
                <div class="fwt-light-grey fwt-border fwt-border-khaki">
                    <h3 class="fwt-center">Need Attention</h3>
                    <div id="idActivitiesList" runat="server" class="activity-timeline">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            var sDate = new Date();
            var month = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
            $(".today-date").html(month[sDate.getMonth()] + " " + sDate.getDate() + ", " + sDate.getFullYear());

            //SaleChart Display
            var sJsonVar = {};
            CallAjaxMethod("Count_PaymentInvoice_Chart", sJsonVar, "Count_PaymentInvoice_Chart_Complete");
            CallAjaxMethod("Amount_PaymentInvoice_Chart", sJsonVar, "Amount_PaymentInvoice_Chart_Complete");
            CallAjaxMethod("Customer_Chart", sJsonVar, "Customer_Chart_Complete");
        });
        function Customer_Chart_Complete(data) {
            try {
                var $Data = data.d;
                if ($Data.length > 0) {
                    if ($Data[0].ERROR != "") {
                        ShowMessage($Data[0].ERROR);
                    }
                    else {
                        var $Data = data.d;
                        google.charts.load('current', { 'packages': ['corechart', 'bar'] });
                        google.charts.setOnLoadCallback(drawChart);
                        function drawChart() {
                            var sData = new Array($Data.length + 1);
                            sData[0] = ["Last 7 Days", "Count"];
                            for (var i = 0; i < $Data.length; i++) {
                                $Data[i].Date == "" ? 0 : $Data[i].Date;
                                $Data[i].Count == "" ? 0 : $Data[i].Count;
                                sData[i + 1] = [$Data[i].Date, parseInt($Data[i].Count)];
                            }
                            var chartdata = new google.visualization.arrayToDataTable(sData);
                            var options = {
                                // vAxis: { format: 'long', title: 'Number Of Payments / Invoices' },
                                height: 35, 
                                colors: ['#fff'],
                                legend: { position: 'none' },
                                bar: { groupWidth: "25%" },
                                backgroundColor: 'transparent',
                                vAxis: {
                                    gridlines: {
                                        color: 'transparent'
                                    }
                                },
                            };

                            var chart = new google.visualization.ColumnChart(document.getElementById('idCustomer_Chart'));
                            chart.draw(chartdata, options);
                        }
                    }
                }
            }
            catch (err) {
                ShowMessage(err);
            }
        }
        function Count_PaymentInvoice_Chart_Complete(data) {
            try {
                var $Data = data.d;
                if ($Data.length > 0) {
                    if ($Data[0].ERROR != "") {
                        ShowMessage($Data[0].ERROR);
                    }
                    else {
                        var $Data = data.d;
                        google.charts.load("current", { packages: ["corechart"] });
                        google.charts.setOnLoadCallback(drawChart);
                        function drawChart() {
                            var sData = new Array($Data.length + 1);
                            sData[0] = ["Entity", "Count"];
                            for (var i = 0; i < $Data.length; i++) {
                                sData[i + 1] = [$Data[i].Entity, parseInt($Data[i].Count)];
                            }
                            var chartdata = new google.visualization.arrayToDataTable(sData);
                            var options = {
                                title: '',
                                pieHole: 0.4,
                                height: 300,
                            };
                            var chart = new google.visualization.PieChart(document.getElementById('idInvoicePaymentCount_Chart'));
                            chart.draw(chartdata, options);
                        }
                    }
                }
            }
            catch (err) {
                ShowMessage(err);
            }
        }
        function Amount_PaymentInvoice_Chart_Complete(data) {
            try {

                var $Data = data.d;
                if ($Data.length > 0) {
                    if ($Data[0].ERROR != "") {
                        ShowMessage($Data[0].ERROR);
                    }
                    else {
                        var $Data = data.d;
                        google.charts.load('current', { 'packages': ['corechart'] });
                        google.charts.setOnLoadCallback(drawChart);
                        function drawChart() {
                            var sData = new Array($Data.length + 1);

                            sData[0] = ["Date", "InvoiceAmount", "PaymentAmount"];
                            for (var i = 0; i < $Data.length; i++) {
                                var sDate = new Date($Data[i].Date);
                                //$Data[i].InvoiceAmount == "" ? 0 : $Data[i].InvoiceAmount;
                                //$Data[i].PaymentAmount == "" ? 0 : $Data[i].PaymentAmount;
                                sData[i + 1] = [$Data[i].Date, parseInt($Data[i].InvoiceAmount), parseInt($Data[i].PaymentAmount)];
                            }
                            var chartdata = new google.visualization.arrayToDataTable(sData);
                            var options = {
                                title: '',
                                vAxis: { format: '₹#,###.##', title: 'Amount' },
                                height: 300,
                                colors: ['#1b9e77', '#7570b3'],
                                legend: { position: 'top' },
                            };
                            var chart = new google.visualization.LineChart(document.getElementById('idIPaymentAgainstInvoice_Chart'));
                            chart.draw(chartdata, options);
                        }
                    }
                }
            }
            catch (err) {
                ShowMessage(err);
            }
        }

    </script>
</asp:Content>
