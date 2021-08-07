
var sOrderBy = "", sOrderByAsc = "0";
var bBlankInFirst = true;
//Payment Grid
var iCheckCount = 0;
function GetAll_CustomerPayments(sOrderBy) {
    try {
        $('#tblPayments .table-message').html("<i class=\"fa fa-refresh fwt-spin\"></i>&nbsp;Loading");
        $('#tblPayments .table-loading').show();

        //sOrderByAfterScroll = sOrderBy;
      
        var sPaymentsGridPageNumberValue = $('#hfPaymentsGridPageNumber').val();
        if (sPaymentsGridPageNumberValue == "") {
            sPaymentsGridPageNumberValue = "0";
            $('#hfPaymentsGridPageNumber').val("0");
            $("#tblPayments thead, #tblPayments tbody").html("");
        }
        var sAgency_ID = $("#ddlAgency").val();      
        sAgency_ID= sAgency_ID == "NONE" ? sAgency_ID = "0" : sAgency_ID;
        var jsonVar = {
            'sOrderBy': sOrderBy, 'sGridPageNumber': sPaymentsGridPageNumberValue, 'sUserName': "",'sAgency_ID':sAgency_ID,
            'sCustomer_ID': $("#ddlCustomers").val(), 'sPaymentDateTo': $("#txtDateTo").val(), 'sPaymentDateFrom': $("#txtDateFrom").val()
        };

        CallAjaxMethod("Payments_Search", jsonVar, "Payments_Search_Callback");

    }
    catch (err) {
        alert("Error occured in  function(GetAll_Payments)" + err);
    }

}
function Payments_Search_Callback(data) {

    try {
     
        var sRow = "";
        $("#tblPayments tbody").html("");//add line for back and privious button
        if (data.d.length > 0) {
            $("#idPageNumber").show();//add line for back and privious button
            if (data.d[0].ERROR != "") {
                $('#tblPayments .table-message').html(data.d[0].ERROR);
                $('#hfPaymentsGridPageNumber').val("End");
            }
            else if (data.d[0].ID == "0") {
                $('#tblPayments .table-message').html("No more data available to read.");
                $('#hfPaymentsGridPageNumber').val("End");
                $("#lblPageNumber").html("");
                bBlankInFirst = false;
            }
            else {
                $('#tblPayments .table-message').html("");
                var sPaymentsGridPageNumberValue = $('#hfPaymentsGridPageNumber').val();
                //var sRowCount = data.d[0].RowCount;
                //var sPageNumber = parseInt(sPaymentsGridPageNumberValue) + 1;

                ////label show page number
                //var sDisplayRowCount = (10 * (parseInt(sPaymentsGridPageNumberValue) + 1)) - (10 - data.d.length);
                var sRowCount = Math.ceil(data.d[0].RowCount / 10);
                var sDisplayRowCount = (parseInt(sPaymentsGridPageNumberValue) + 1);
                
                $("#lblPageNumber").html(" &nbsp; Page : <b class=\"fwt-text-teal\">" + sDisplayRowCount + "</b> of <b class=\"fwt-text-teal\">" + sRowCount + "</b> &nbsp; ");
                //

                var sCssClass = "background:#D5E990;", sCssClassInvoice_ID = "width:5%;", sCssClassInvoice_Date = "width:10%;", sCssClassParticular = "width:20%;word-break: break-all;", sCssClassCategory_Disp = "width:10%;"
                    , sCssClassAmount = "width:10%;", sCssClassPaid = "width:10%;", sCssClassRemainingPayment = "width:10%;", sCssClassPaymentMethod = "width:10%;", sCssClassCurrentPayment = "width:10%;";
                var sCssClassCreatedBY = "", sCssClassCreatedDT = "";
                if  (sOrderBy == "Invoice_ID") sCssClassInvoice_ID = sCssClass;
                else if (sOrderBy == "Invoice_Date") sCssClassInvoice_Date = sCssClass;
                else if (sOrderBy == "Particular") sCssClassParticular = sCssClass;
                else if (sOrderBy == "Category_Disp") sCssClassCategory_Disp = sCssClass;
                //else if (sOrderBy == "PaymentMethod") sCssClassPaymentMethod = sCssClass;
                else if (sOrderBy == "Amount") sCssClassAmount = sCssClass;
                else if (sOrderBy == "Paid") sCssClassPaid = sCssClass;
                else if (sOrderBy == "RemainingPayment") sCssClassRemainingPayment = sCssClass;
                else if (sOrderBy == "CurrentPayment") sCssClassCurrentPayment = sCssClass;
                
                var sCustomer_ID = $("#hfCustomer_ID").val();

                if (sPaymentsGridPageNumberValue == "0") {
                    $("#tblPayments thead").html("");
                    sRow += "<tr class='rowHead'><th onclick='SelectPaymentsGridPageNumberBlank(); ' class='rowHeadColumn LinkHeader' style='width:5%;' align='center'>Invoice ID</th>" +
                       "<th onclick='SelectPaymentsGridPageNumberBlank(); ' class='rowHeadColumn LinkHeader' style='width:10%;' align='center'>Invoice Date</th>" +
                            "<th onclick='SelectPaymentsGridPageNumberBlank(); ' class='rowHeadColumn LinkHeader' style='width:8%;' align='center'>Customer</th>" +
                                "<th onclick='SelectPaymentsGridPageNumberBlank(); ' class='rowHeadColumn LinkHeader' style='width:22%;' align='center'>Particular</th>" +
                               "<th onclick='SelectPaymentsGridPageNumberBlank(); ' class='rowHeadColumn LinkHeader' align='center' style='width:10%;'>Type</th>" +
                                //"<th onclick='SelectPaymentsGridPageNumberBlank(); ' class='rowHeadColumn LinkHeader' align='center' style='width:10%;'>Payment Method</th>" +
                               "<th onclick='SelectPaymentsGridPageNumberBlank(); ' class='rowHeadColumn LinkHeader' align='center' style='width:10%;'>Amount</th>" +
                                "<th onclick='SelectPaymentsGridPageNumberBlank(); ' class='rowHeadColumn LinkHeader' align='center' style='width:10%;'>Paid</th>" +
                                 "<th onclick='SelectPaymentsGridPageNumberBlank(); ' class='rowHeadColumn LinkHeader' align='center' style='width:10%;'>Remaining Payment</th>" +
                                  "<th onclick='SelectPaymentsGridPageNumberBlank(); ' class='rowHeadColumn LinkHeader' align='center' style='width:10%;'>Current Payment</th>" +
                               "<th onclick='SelectPaymentsGridPageNumberBlank(); ' class='rowHeadColumn LinkHeader' align='center' style='width:5%;'> Select </th></tr>";
                    $("#tblPayments thead").append(sRow);
                }

                for (var i = 0; i < data.d.length; i++) {
                    sCustomer_ID = data.d[i].Customer_ID;
                    var dRemainingPayment = (parseFloat(data.d[i].Amount) - parseFloat(data.d[i].Paid)).toFixed(2);
                    var sTimeString = jQuery.timeago(data.d[i].Invoice_Date);
                    var sParticularDisplay = (data.d[i].Particular.length > 90) ? data.d[i].Particular.substring(0, 86) + "...." : data.d[i].Particular;
                    var sPaymentMethod = (data.d[i].PaymentMethod.length > 70) ? data.d[i].PaymentMethod.substring(0, 86) + "...." : data.d[i].PaymentMethod;
                    sRow = "<tr class='rowHover'><td align='center' style='" + sCssClassInvoice_ID + "'><span id=\"idInvoice_id" + i + "\">" + data.d[i].Invoice_ID + "</span></td>" +
                          "<td align='center' style='" + sCssClassInvoice_Date + "' title='" + data.d[i].Invoice_Date + "'><i class='fa fa-clock-o'></i> " + sTimeString + "</td>" +
                              "<td style='" + sCssClassParticular + "'>" + data.d[i].CustomerName + "<span id=\"idCustomer" + i + "\" style='display: none;'>" + "|" + sCustomer_ID + "</span></td>" +
                        "<td style='" + sCssClassParticular + "' id=\"idParticular" + i + "\">" + sParticularDisplay + "</td>" +
                        "<td align='center' style='" + sCssClassCategory_Disp + "'>" + data.d[i].Category_Disp + " <span id=\"idCategory_Core" + i + "\" style='display: none;'>" + "|" + data.d[i].Category_Core + "</span></td>" +
                        //"<td align='center' style='" + sCssClassPaymentMethod + "'>" + sPaymentMethod + "</td>" +
                        "<td align='center' style='" + sCssClassAmount + "'><i class='fa fa-inr'></i> " + parseFloat(data.d[i].Amount).toFixed(2) + "</td>" +
                        "<td align='center' style='" + sCssClassPaid + "'><i class='fa fa-inr'></i> " + parseFloat(data.d[i].Paid).toFixed(2) + "</td>" +
                        "<td align='center' style='" + sCssClassRemainingPayment + "'><i class='fa fa-inr'></i> <span id=\"idRemainingPayment" + i + "\">" + dRemainingPayment + "</span></td>" +
                        "<td align='center' style='" + sCssClassCurrentPayment + "'><i class='fa fa-inr'></i> <span id=\"idCurrentPayment" + i + "\">" + (0).toFixed(2) + "</span></td>" +
                        "<td class='fwt-center' style='cursor:pointer;width:2%;'><input id='chkPayments" + i + "' type='checkbox' name='chkPayments" + i + "' value='checkbox1' onclick=\"javascript:InsertPayments(this.id,'idRemainingPayment" + i + "','idCurrentPayment" + i + "');\" /></td></tr>";
                    $("#tblPayments tbody").append(sRow);
                }

                if ($("#rbtnlSelectOperation input:checked").val() == "CreateInvoice") {
                    $("#tblPayments tbody").find('input').each(function () {
                        $('input:checkbox').prop('disabled', true);
                        $('input:checkbox').prop('checked', false);
                    });
                }
                else {

                    $("#tblPayments tbody").find('input').each(function () {
                        $('input:checkbox').prop('disabled', false);
                    });
                }
                $('#tblPayments .table-loading').hide();
                //  $('#hfPaymentsGridPageNumber').val(new String(parseInt(sPaymentsGridPageNumberValue) + 1));//comment line for back and privious button
                bBlankInFirst = false;
            }
        }
        else {
            $('#tblPayments .table-loading').hide();
            sOrderByAsc = 0;
            sOrderBy = "";
            if (bBlankInFirst == true) {
                $('#tblPayments .table-message').html("There is no data to display for this tab.")
                            .animate({ 'font-size': '+=3px' }, 100).delay(200).animate({ 'font-size': '-=3px' }, 50);
            }
            else {
                $('#tblPayments .table-message').html("No more data available to read.");
            }
        }
        IsProcessingHomeGrid = false;

    }
    catch (err) { alert("Error occured in  function(Payments_Select_Callback) " + err); }
}
function SelectPaymentsGridPageNumberBlank() {
    try {
        $('#hfPaymentsGridPageNumber').val("");
    }
    catch (err) { alert("Error occured in  function(SelectPaymentsGridPageNumberBlank) " + err); }
}
function InsertPayments(btnID, idRemainingPayment, idCurrentPayment) {
    try {
var iDeselect = 0;
        var $RemainingPayment = $("#" + idRemainingPayment);
        var $CurrentPayment = $("#" + idCurrentPayment);
        var dRemainingPayment = parseFloat($RemainingPayment.html());

        var fAdvancePayment = parseFloat($("#txtAdvancePayment").val());
        var fTotalRemainingAmount = parseFloat($("#txtRemainingAmount").val());

        if ($("#" + btnID).is(":checked") == true) {
            // if ($("#hfAdvancePayment").val() == "" || parseFloat($("#hfAdvancePayment").val()) == 0) { $("#hfAdvancePayment").val(fAdvancePayment); }        

            var fDiffrenceAmount = 0.0;

            if (fAdvancePayment > 0 && fAdvancePayment != "") {
                iCheckCount = iCheckCount + 1;

                if (fAdvancePayment > dRemainingPayment) {
                    fDiffrenceAmount = fAdvancePayment - dRemainingPayment;
                    $("#txtAdvancePayment").val(fDiffrenceAmount.toFixed(2));
                    fTotalRemainingAmount = fTotalRemainingAmount - dRemainingPayment;
                    $RemainingPayment.html((0).toFixed(2));
                    $CurrentPayment.html(dRemainingPayment.toFixed(2));
                }
                else {
                    $("#txtAdvancePayment").val((0).toFixed(2));
                    fDiffrenceAmount = dRemainingPayment - fAdvancePayment;
                    fTotalRemainingAmount = fTotalRemainingAmount - fAdvancePayment;
                    $RemainingPayment.html(fDiffrenceAmount.toFixed(2));
                    $CurrentPayment.html(fAdvancePayment.toFixed(2));
                }
                $("#txtRemainingAmount").val(fTotalRemainingAmount.toFixed(2));

            }
            else {
                $("#" + btnID).prop('checked', false);
                ShowMessage("Not Sufficient Balance");
            }
        }
        else {
            $("#" + btnID).prop('checked', true);
            iCheckCount = 0;
             ShowMessage(" Click clear button for cancle process");
            //iCheckCount = iCheckCount - 1;
            //$("#txtPayment").val(0.00);
            //$("#txtRemainingAmount").val($("#hfTotalRemainingAmount").val());
            //$("#txtAdvancePayment").val($("#hfAdvancePayment").val());
            //$("#btnPayment").val("ADD");
            //$("#tblPayments tbody").find('input').each(function () {
            //    $('input:checkbox').prop('checked', false);
            //});
           
            
        }


        //if One of Checkbox is selected
        if (iCheckCount > 0) {
            // $("#txtAdvancePayment").attr("disabled", true)
            $("#btnPayment").val("PAY");
        }
        else if (iDeselect!=1) {
            //$("#txtAdvancePayment").attr("disabled", false);
            $("#btnPayment").val("ADD");
        }
    }
    catch (err) { alert("Error occured in  function(InsertPayments) " + err); }
}

//function SelectCheckBoxAll(btnID) {
//    //Loop for Search Grid all checkbox
//    var i = 0;
//    var sOrderBy = "";
//    var dRemainingPayment = 0.00;
//    if ($("#" + btnID).is(":checked") == true) {
//        $("#divPayments .table-main tbody").find('input').each(function () {
//            var $RemainingPayment = $("#idRemainingPayment" + i);
//            dRemainingPayment = parseFloat($RemainingPayment.html()) + dRemainingPayment;
//            i++;

//        });
//        i = 0;
//        if (dRemainingPayment <= parseFloat($("#txtAdvancePayment").val())) {

//            $("#divPayments .table-main tbody").find('input').each(function () {
//                $('input:checkbox').prop('checked', true);
//                var idRemainingPayment = "idRemainingPayment" + i;
//                var idCurrentPayment = "idCurrentPayment" + i;

//                InsertPayments($(this).attr("id"), idRemainingPayment, idCurrentPayment);
//                i++;
//            });
//            $("#btnPayment").val("PAY");

//        }
//        else {
//            $("#" + btnID).prop('checked', false);
//            // $("#txtAdvancePayment").val($("#hfAdvancePayment").val());
//            //$("#txtRemainingAmount").val($("#hfTotalRemainingAmount").val());          
//            ErrorMessage("WARNING","Payment should be Equal or greater than selected Remaining Amount  ");
//        }
//    }
//    else {
//        //$("#divPayments .table-main tbody").find('input').each(function () {
//        $('input:checkbox').prop('checked', false);
//        // });
//        SelectPaymentsGridPageNumberBlank();
//        GetAll_CustomerPayments(sOrderBy);
//        $("#txtRemainingAmount").val($("#hfTotalRemainingAmount").val());
//        $("#txtAdvancePayment").val($("#hfAdvancePayment").val());
//        $("#btnPayment").val("ADD");
//    }

//}
function PaymentGrid_ForInvoice(sOrderBy) {
    try {
      
        $('#tblPayments_ForInvoice .table-message').html("<i class=\"fa fa-refresh fwt-spin\"></i>&nbsp;Loading");
        $('#tblPayments_ForInvoice .table-loading').show();

        //sOrderByAfterScroll = sOrderBy;

        var sPayments_ForInvoice_PageNumberValue = $('#hfPayments_ForInvoice_PageNumber').val();
        if (sPayments_ForInvoice_PageNumberValue == "") {
            sPayments_ForInvoice_PageNumberValue = "0";
            $('#hfPayments_ForInvoice_PageNumber').val("0");
            $("#tblPayments_ForInvoice thead, #tblPayments_ForInvoice tbody").html("");
        }       
        var jsonVar = {'sOrderBy': sOrderBy, 'sGridPageNumber': sPayments_ForInvoice_PageNumberValue, 'sUserName': "",
            'sCustomer_ID': $("#hfCustomer_ID").val(), 'sInvoice_ID': $("#hfInvoice_ID").val(), 'sCategory_Core': $("#hfCategory").val()
        };
     
        CallAjaxMethod("Payments_ForInvoice", jsonVar, "Payments_ForInvoice_Callback");

    }
    catch (err) {
        alert("Error occured in  function(PaymentGrid_ForInvoice)" + err);
    }

}
function Payments_ForInvoice_Callback(data) {

    try {
        var sRow = "";
        $("#tblPayments_ForInvoice tbody").html("");//add line for back and privious button
        if (data.d.length > 0) {
            $("#idPageNumber").show();//add line for back and privious button
            if (data.d[0].ERROR != "") {
                $('#tblPayments_ForInvoice .table-message').html(data.d[0].ERROR);
                $('#hfPayments_ForInvoice_PageNumber').val("End");
            }
            else if (data.d[0].ID == "0") {
                $('#tblPayments_ForInvoice .table-message').html("No more data available to read.");
                $('#hfPayments_ForInvoice_PageNumber').val("End");
                $("#lblPageNumber").html("");
                bBlankInFirst = false;
            }
            else {
                $('#tblPayments_ForInvoice .table-message').html("");
                var sPayments_ForInvoice_PageNumberValue = $('#hfPayments_ForInvoice_PageNumber').val();
               
                var sRowCount = Math.ceil(data.d[0].RowCount / 10);
                var sDisplayRowCount = (parseInt(sPayments_ForInvoice_PageNumberValue) + 1);
                $("#lblPageNumber").html(" &nbsp; Page : <b class=\"fwt-text-teal\">" + sDisplayRowCount + "</b> of <b class=\"fwt-text-teal\">" + sRowCount + "</b> &nbsp; ");

                var sCssClass = "background:#D5E990;", sCssClassReceiptID = "", sCssClassPayment_Date = "", sCssClassPaymentAmount = "", sCssClassPaymentType = "";
                if (sOrderBy == "ReceiptID") sCssClassCustomer_ID = sCssClass;
                else if (sOrderBy == "Payment_Date") sCssClassParticular = sCssClass;
                else if (sOrderBy == "PaymentAmount") sCssClassCategory_Disp = sCssClass;
                //else if (sOrderBy == "PaymentType") sCssClassAmount = sCssClass;

                if (sPayments_ForInvoice_PageNumberValue == "0") {
                    $("#tblPayments_ForInvoice thead").html("");
                    sRow += "<tr class='rowHead'><th onclick='SelectPayments_ForInvoice_PageNumberBlank(); ' class='rowHeadColumn LinkHeader';' align='center'>Receipt ID</th>" +
                       "<th onclick='SelectPayments_ForInvoice_PageNumberBlank(); ' class='rowHeadColumn LinkHeader' align='center'>Payment Date</th>" +
                                "<th onclick='SelectPayments_ForInvoice_PageNumberBlank(); ' class='rowHeadColumn LinkHeader' align='center'>Payment Amount</th></tr>";
                               //"<th onclick='SelectPayments_ForInvoice_PageNumberBlank(); ' class='rowHeadColumn LinkHeader' align='center'>Payment Type</th></tr>";
                    $("#tblPayments_ForInvoice thead").append(sRow);
                }

                for (var i = 0; i < data.d.length; i++) {                  
                
                    var sTimeString = jQuery.timeago(data.d[i].Payment_Date);
                    sRow = "";
                    sRow += "<tr class='rowHover'><td align='center' style='" + sCssClassReceiptID + "'>" + data.d[i].ReceiptID + "</td>" +
                          "<td align='center' style='" + sCssClassPayment_Date + "' title='" + data.d[i].Payment_Date + "'><i class='fa fa-clock-o'></i> " + sTimeString + "</td>" +
                        "<td class='fwt-center' style='" + sCssClassPaymentAmount + "'><i class='fa fa-inr'></i> " + data.d[i].PaymentAmount + "</td></tr>";
                        //"<td align='center' style='" + sCssClassPaymentType + "'>" + data.d[i].PaymentType + "</td></tr>";
                    $("#tblPayments_ForInvoice tbody").append(sRow);
                }

                $('#tblPayments_ForInvoice .table-loading').hide();
                //  $('#hfPayments_ForInvoice_PageNumber').val(new String(parseInt(sPayments_ForInvoice_PageNumberValue) + 1));//comment line for back and privious button
                bBlankInFirst = false;
            }
        }
        else {
            $('#tblPayments_ForInvoice .table-loading').hide();
            sOrderByAsc = 0;
            sOrderBy = "";
            if (bBlankInFirst == true) {
                $('#tblPayments_ForInvoice .table-message').html("There is no data to display for this tab.")
                            .animate({ 'font-size': '+=3px' }, 100).delay(200).animate({ 'font-size': '-=3px' }, 50);
            }
            else {
                $('#tblPayments_ForInvoice .table-message').html("No more data available to read.");
            }
        }
        IsProcessingHomeGrid = false;

    }
    catch (err) { alert("Error occured in  function(Payments_ForInvoice_Callback) " + err); }
}
function SelectPayments_ForInvoice_PageNumberBlank() {
    try {
        $('#hfPayments_ForInvoice_PageNumber').val("");
    }
    catch (err) { alert("Error occured in  function(SelectPayments_ForInvoice_PageNumberBlank) " + err); }
}