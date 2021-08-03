
var sOrderBy = "", sOrderByAsc = "0";
var bBlankInFirst = true;
//Invoices Grid

function GetAll_Invoices(sOrderBy) {
    try {

        $('#tblInvoices .table-message').html("<i class=\"fa fa-refresh fwt-spin\"></i>&nbsp;Loading");
        $('#tblInvoices .table-loading').show();
     
        //sOrderByAfterScroll = sOrderBy;

        var sInvoicesGridPageNumberValue = $('#hfInvoicesGridPageNumber').val();
        if (sInvoicesGridPageNumberValue == "") {
            sInvoicesGridPageNumberValue = "0";
            $('#hfInvoicesGridPageNumber').val("0");
            $("#tblInvoices thead, #tblInvoices tbody").html("");
        }


        //var agentid = $("#ddlAgents").val() == null ? 'All' : $("#ddlAgents").val();
        var sAgencyId = $("#ddlAgency").val() == null ? 'All' : $("#ddlAgency").val();      
        var sCustomerId = $("#ddlCustomers").val() == null ? 'All' : $("#ddlCustomers").val();
        var jsonVar = {
            'sOrderBy': sOrderBy, 'sGridPageNumber': sInvoicesGridPageNumberValue, 'sUserName': "",
            'sAgency_ID': sAgencyId, 'sCustomer_ID': sCustomerId,
            'sReceiptID': $("#txtReceiptID").val(), 'sCategory_Core': $("#ddlCategories").val(),
            'sPaymentDateTo': $("#txtDateTo").val(), 'sPaymentDateFrom': $("#txtDateFrom").val(),
            'sInvoiceStatus_Core': $("#ddlInvoiceStatus").val()
        };

        CallAjaxMethod("Invoices_Search", jsonVar, "Invoices_Search_Complete");

    }
    catch (err) {
        alert("Error occured in  function(GetAll_Invoices)" + err);
    }

}
function Invoices_Search_Complete(data) {

    try {
        var sRow = "";
        $("#tblInvoices tbody").html("");//add line for back and privious button
        if (data.d.length > 0) {
            if (data.d[0].ERROR != "") {
                $('#tblInvoices .table-message').html(data.d[0].ERROR);
                $('#hfInvoicesGridPageNumber').val("End");
            }
            else if (data.d[0].ID == "0") {
                $('#tblInvoices .table-message').html("No more data available to read.");
                $('#hfInvoicesGridPageNumber').val("End");
                bBlankInFirst = false;
                $("#lblPageNumber").html("Search Result: <b class=\"fwt-text-red\">" + 0 + "</b> of <b class=\"fwt-text-red\">" + 0 + "</b>");
                //
            }
            else {
                $('#tblInvoices .table-message').html("");
                var sInvoicesGridPageNumberValue = $('#hfInvoicesGridPageNumber').val();
                //var sRowCount = data.d[0].RowCount;
                //var sPageNumber = parseInt(sInvoicesGridPageNumberValue) + 1;
                //var iTotalPageNumber = Math.ceil(iTotalRows / 10);
                ////label show page number
                //var sDisplayRowCount = (10 * (parseInt(sInvoicesGridPageNumberValue) + 1)) - (10 - data.d.length);
                var sRowCount = Math.ceil(data.d[0].RowCount / 10);
                var sDisplayRowCount = (parseInt(sInvoicesGridPageNumberValue) + 1);
                $("#lblPageNumber").html("Search Page No: <b class=\"fwt-text-red\">" + sDisplayRowCount + "</b> of <b class=\"fwt-text-red\">" + sRowCount + "</b>");
                //
                var sCssClass = "background:#D5E990;", sCssClassCustomer_ID = "", sCssClassInvoice_ID = "", sCssClassParticular = "width:14%;word-break: break-all;", sCssClassReceiptID = "";
                var sCssClassCreatedBY = "", sCssClassCreatedDT = "", sCssClassCategory_Disp = "", sCssClassAmount = "", sCssClassPaid = "", sCssClassInvoiceStatus_Disp = "";
                if (sOrderBy == "Customer_ID") sCssClassCustomer_ID = sCssClass;
                else if (sOrderBy == "Agent_ID") sCssClassAgent_ID = sCssClass;
                else if (sOrderBy == "Particular") sCssClassParticular = sCssClass;
                else if (sOrderBy == "ReceiptID") sCssClassReceiptID = sCssClass;
                else if (sOrderBy == "Category_Disp") sCssClassCategory_Disp = sCssClass;
                else if (sOrderBy == "CreatedBY") sCssClassCreatedBY = sCssClass;
                else if (sOrderBy == "CreatedDT") sCssClassCreatedDT = sCssClass;
                else if (sOrderBy == "InvoiceStatus_Disp") sCssClassInvoiceStatus_Disp = sCssClass;
                else if (sOrderBy == "Amount") sCssClassAmount = sCssClass;
                else if (sOrderBy == "Paid") sCssClassPaid = sCssClass;
                //if ($(window).width() <= 768) {
                //    if (data.d.length > 5) {
                //        //$("#div_ScrolltblInvoices").removeClass("table-Scroll");
                //        //$("#div_ScrolltblInvoices").addClass("table-Scroll_FixHeight");

                //        $('.table-Scroll').css({ "height": "300px","overflow-y": "auto","overflow-x": "auto" });
                //    }
                //    else
                //        $('.table-Scroll').css({ "height": "auto" });
                //}
                var sCustomer_ID = "", sAgent_ID = "";
                if (sInvoicesGridPageNumberValue == "0") {
                    $("#tblInvoices thead").html("");
                    sRow += "<tr class='rowHead'><th class='rowHeadColumn' style='width:2%;' title=\"Invoice\">View</th>" +
                         "<th class='rowHeadColumn' onclick='SelectInvoicesGridPageNumberBlank(); Invoices_Search(\"Particular\");' class='rowHeadColumn LinkHeader' style='width:8%;' align='center'>Invoice ID</th>" +
                               "<th class='rowHeadColumn' onclick='SelectInvoicesGridPageNumberBlank(); Invoices_Search(\"Particular\");' class='rowHeadColumn LinkHeader' style='width:25%;' align='center'>Particular</th>" +
                                //"<th onclick='SelectInvoicesGridPageNumberBlank(); Invoices_Search(\"Agent_ID\");' class='rowHeadColumn LinkHeader' style='width:15%;' align='center'>Agent ID<br>Agent Name(Mobile)</th>" +
                                "<th onclick='SelectInvoicesGridPageNumberBlank(); Invoices_Search(\"Customer_ID\");' class='rowHeadColumn LinkHeader' style='width:15%;' align='center'>Customer ID<br>Customer Name (Mobile)</th>" +
                                "<th onclick='SelectInvoicesGridPageNumberBlank(); Invoices_Search(\"Category_Disp\");' class='rowHeadColumn LinkHeader' align='center' style='width:10%;'>Type</th>" +
                                //"<th onclick='SelectInvoicesGridPageNumberBlank(); Invoices_Search(\"CreatedBY\");' class='rowHeadColumn LinkHeader' align='center' style='width:10%;'>Created BY</th>" +
                                "<th onclick='SelectInvoicesGridPageNumberBlank(); Invoices_Search(\"CreatedDT\");' class='rowHeadColumn LinkHeader' align='center' style='width:10%;'>Created DT</th>" +
                               "<th onclick='SelectInvoicesGridPageNumberBlank(); Invoices_Search(\"Amount\");' class='rowHeadColumn LinkHeader' align='center' style='width:10%;'>Amount</th>" +
                                "<th onclick='SelectInvoicesGridPageNumberBlank(); Invoices_Search(\"Paid\");' class='rowHeadColumn LinkHeader' align='center' style='width:10%;'>Paid</th>" +
                               "<th onclick='SelectInvoicesGridPageNumberBlank(); Invoices_Search(\"InvoiceStatus_Disp\");' class='rowHeadColumn LinkHeader' align='center' style='width:10%;'>Invoice Status</th></tr>";
                    $("#tblInvoices thead").append(sRow);
                }
                for (var i = 0; i < data.d.length; i++) {
                    sAgent_ID = data.d[i].Agent_ID;
                    sCustomer_ID = data.d[i].Customer_ID;
                    var dRemainingPayment = (parseFloat(data.d[i].Amount) - parseFloat(data.d[i].Paid)).toFixed(2);
                    if (data.d[i].InvoiceStatus_Core == "IS_OPEN")
                        sCssClassInvoiceStatus_Disp = "color:#00BDFD;";
                    else if (data.d[i].InvoiceStatus_Core == "IS_OVERDUE")
                        sCssClassInvoiceStatus_Disp = "color:#F44336;";
                    else if (data.d[i].InvoiceStatus_Core == "IS_PAID_PARTIAL")
                        sCssClassInvoiceStatus_Disp = "color:#4CAF50;";
                    else
                        sCssClassInvoiceStatus_Disp = "color:#4CAF50;font-weight:bold;";
                    var sStatusIsPaidStatement = data.d[i].InvoiceStatus_Core == "IS_PAID" ? "&nbsp;<i class=\"fa fa-caret-down fwt-text-red\"></i>" : "";

                    var sTimeString = jQuery.timeago(data.d[i].CreatedDT);//jQuery.timeago(new Date(data.d[i].CreatedDT));

                    var sParticularDisplay = (data.d[i].Particular.length > 90) ? data.d[i].Particular.substring(0, 86) + "...." : data.d[i].Particular;

                    sRow = "<tr class='rowHover'><td align='center' class=' fwt-hover-text-blue' style=' cursor:pointer;width:2%;' onclick=\"javascript:SelectInvoiceLink('" + data.d[i].Invoice_ID + "','" + data.d[i].Category_Core + "');\"><i class=\"fa fa-pencil\"></i></td>" +
                         "<td class='fwt-center' style='" + sCssClassInvoice_ID + "'>" + data.d[i].Invoice_ID + "</td>" +
                        "<td  style='" + sCssClassParticular + "'>" + sParticularDisplay + "</td>" +
                                "<td class='fwt-center' style='" + sCssClassCustomer_ID + "'>" + "<b>" + sCustomer_ID + "</b><br>" + data.d[i].CustomerName + "</td>" +
                                "<td align='center' style='" + sCssClassCategory_Disp + "'>" + data.d[i].Category_Disp + "</td>" +
                                //"<td align='center' style='" + sCssClassCreatedBY + "'>" + data.d[i].CreatedBY + "</td>" +
                                "<td align='center' style='" + sCssClassCreatedDT + "' title='" + data.d[i].CreatedDT + "'><i class='fa fa-clock-o'></i> " + sTimeString + "</td>" +
                                "<td align='center' style='" + sCssClassAmount + "'><i class='fa fa-inr'></i> " + parseFloat(data.d[i].Amount).toFixed(2) + "</td>" +
                                 "<td class='fwt-center' style='" + sCssClassPaid + "';\"><i class='fa fa-inr'></i> " + parseFloat(data.d[i].Paid).toFixed(2) + "</td>" +
                                "<td align='center' style='" + sCssClassInvoiceStatus_Disp + "'>" + data.d[i].InvoiceStatus_Disp + " </td></tr>";
                    $("#tblInvoices tbody").append(sRow);
                }

                $('#tblInvoices .table-loading').hide();
                // $('#hfInvoicesGridPageNumber').val(new String(parseInt(sInvoicesGridPageNumberValue) + 1));//comment line for back and privious button
                bBlankInFirst = false;
            }
        }
        else {
            $('#tblInvoices .table-loading').hide();
            sOrderByAsc = 0;
            sOrderBy = "";
            if (bBlankInFirst == true) {
                $('#tblInvoices .table-message').html("There is no data to display for this tab.")
                            .animate({ 'font-size': '+=3px' }, 100).delay(200).animate({ 'font-size': '-=3px' }, 50);
            }
            else {
                $('#tblInvoices .table-message').html("No more data available to read.");
            }
        }
        IsProcessingHomeGrid = false;

    }
    catch (err) { alert("Error occured in  function(Invoices_Search_Complete) " + err); }
}
function SelectInvoicesGridPageNumberBlank() {
    try {
        $('#hfInvoicesGridPageNumber').val("");
    }
    catch (err) { alert("Error occured in  function(SelectInvoicesGridPageNumberBlank) " + err); }
}
function SelectInvoiceLink(sInvoice_ID, sCategoryCore) {
    try {

        if (sInvoice_ID == "") {
            ReadXMLMessage("INS304", "ReadXMLMessage_Complete");
        }

        else
            window.location = "particular/" + sInvoice_ID + "/" + sCategoryCore;
    }
    catch (err) {
        alert("Error occured in  function(SelectParticularLink)" + err);
    }
}

function ReadXMLMessage(ID, callbackFunction) {
    try {
        var sUrl = sSiteUrlValue + "_Layouts/messages.xml";
        if (sXmlData == "") {
            $.ajax({
                type: "GET",
                url: sUrl,
                // data: JSON.stringify(sJsonVar),
                //contentType: "application/xml; charset=utf-8",
                dataType: 'text',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("There is an issue processing your request in function ReadXMLFile ");

                },
                success: function (data) {
                    sXmlData = data;
                    ReadXMLMessage(ID, callbackFunction)
                }
            });
        }
        else {
            $(sXmlData).find('Message').each(function () {
                if ($(this).find("MsgText").attr("id") == ID) {
                    window[callbackFunction]($(this).find("MsgText").html());
                }
            });
        }
    }
    catch (err) {
        alert("There is an issue calling the function " + ReadXMLFile + ", Reason: " + err);
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