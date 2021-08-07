
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

        CallAjaxMethod("Invoices_Search", jsonVar, "Invoices_Search_Callback");

    }
    catch (err) {
        alert("Error occured in  function(GetAll_Invoices)" + err);
    }

}
function Invoices_Search_Callback(data) {
    console.log(data);

    try {
        var sRow = "";
        $("#tblInvoices tbody").html("");//add line for back and privious button
        if (data.d.length > 0) {
            if (data.d[0].ERROR != null && data.d[0].ERROR != "") {
                $('#lblGridMessage').show().html("Error occured: " + data.d[0].ERROR);
                $('#hfInvoicesGridPageNumber').val("End");
            }
            else if (data.d[0].ID == null || data.d[0].ID == "0") {
                $('#lblGridMessage').show().html("Oops! We did not find any data to display here.");
                $('#hfInvoicesGridPageNumber').val("End");
                bBlankInFirst = false;
                $("#lblPageNumber").html("Search Result: <b class=\"fwt-text-red\">" + 0 + "</b> of <b class=\"fwt-text-red\">" + 0 + "</b>");
                //
            }
            else {
                $('#lblGridMessage').hide();
                var sInvoicesGridPageNumberValue = $('#hfInvoicesGridPageNumber').val();
                //var sRowCount = data.d[0].RowCount;
                //var sPageNumber = parseInt(sInvoicesGridPageNumberValue) + 1;
                //var iTotalPageNumber = Math.ceil(iTotalRows / 10);
                ////label show page number
                //var sDisplayRowCount = (10 * (parseInt(sInvoicesGridPageNumberValue) + 1)) - (10 - data.d.length);
                var sRowCount = Math.ceil(data.d[0].RowCount / 10);
                var sDisplayRowCount = (parseInt(sInvoicesGridPageNumberValue) + 1);
                $("#lblPageNumber").html(" &nbsp; Page : <b class=\"fwt-text-teal\">" + sDisplayRowCount + "</b> of <b class=\"fwt-text-teal\">" + sRowCount + "</b> &nbsp; ");
                
                var sCustomer_ID = "", sAgent_ID = "";
                if (sInvoicesGridPageNumberValue == "0") {
                    $("#tblInvoices thead").html("");
                    sRow += "<tr class='rowHead'>" +
                        "<th class='rowHeadColumn' align='center' style='width:8%;'>INVOICE ID</th>" +
                        "<th class='rowHeadColumn' align='center' style='width:25%;'>PARTICULAR</th>" +
                        "<th class='rowHeadColumn' align='center' style='width:15%;'>CUSTOMER NAME (MOBILE)</th>" +
                        "<th class='rowHeadColumn' align='center' style='width:10%;'>TYPE</th>" +
                        "<th class='rowHeadColumn' align='center' style='width:10%;'>INVOICE DATE</th>" +
                        "<th class='rowHeadColumn' align='center' style='width:10%;'>TOTAL AMOUNT</th>" +
                        "<th class='rowHeadColumn' align='center' style='width:10%;'>PAID AMOUNT</th>" +
                        "<th class='rowHeadColumn' align='center' style='width:10%;'>STATUS</th>" +
                        "<th class='' style='width:150px;' title=\"Invoice\">ACTION</th></tr > ";
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

                    sRow = "<tr class='rowHover'>" +
                        "<td class='fwt-center' style=''>" + data.d[i].Invoice_ID + "</td>" +
                        "<td  style=''>" + sParticularDisplay + "</td>" +
                        "<td class='fwt-left-align' style='' title='" + sCustomer_ID + "'>" + data.d[i].CustomerName + "</td>" +
                        "<td align='center' style=''>" + data.d[i].Category_Disp + "</td>" +
                        //"<td align='center' style='" + sCssClassCreatedBY + "'>" + data.d[i].CreatedBY + "</td>" +
                        "<td align='center' style='' title='" + data.d[i].CreatedDT + "'><i class='fa fa-clock-o'></i> " + sTimeString + "</td>" +
                        "<td align='center' style=''><i class='fa fa-inr'></i> " + parseFloat(data.d[i].Amount).toFixed(2) + "</td>" +
                        "<td class='fwt-center' style='';\"><i class='fa fa-inr'></i> " + parseFloat(data.d[i].Paid).toFixed(2) + "</td>" +
                        "<td align='center' style='" + sCssClassInvoiceStatus_Disp + "'>" + data.d[i].InvoiceStatus_Disp + " </td>" +
                        "<td class='fwt-center fwt-btn-group'>" +
                        "   <a class='fwt-btn fwt-round fwt-text-green fwt-white fwt-hover-green pointer' href=\"/invoice/particular/" + data.d[i].Invoice_ID + "\" title='Edit'> <i class=\"fa fa-pencil fa-2x\"></i> </a>" +
                        "</td></tr > ";
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
    catch (err) { alert("Error occured in  function(Invoices_Search_Callback) " + err); }
}
function SelectInvoicesGridPageNumberBlank() {
    try {
        $('#hfInvoicesGridPageNumber').val("");
    }
    catch (err) { alert("Error occured in  function(SelectInvoicesGridPageNumberBlank) " + err); }
}
function SelectInvoiceLink(sInvoice_ID) {
    try {

        if (sInvoice_ID == "") {
            ReadXMLMessage("INS304", "ReadXMLMessage_Callback");
        }

        else
            window.location = sSiteUrlValue + "invoice/particular/" + sInvoice_ID;
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
function ReadXMLMessage_Callback(data) {
    try {
        ShowMessage(data);
    }
    catch (err) {
        ShowMessage("There is an issue calling the function (SetDefault)" + err);
    }
}