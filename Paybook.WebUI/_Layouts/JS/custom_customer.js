var sOrderBy = "", sOrderByAsc = "0";
var bBlankInFirst = true;
function GetAllCustomers(sOrderBy) {
    console.log("GetAllCustomers Started");
    try {
        //        var sAllTabName = $('#' + sAllTabName_HiddenField + '').val().split('|');
        //        var sUserNameEmail = $('#' + sLoginUserNameEmailID_HiddenField + '').val();
        //        //alert(sGridPageNumberValue);
        $("#tblCustomers .table-message").html("<i class=\"fa fa-refresh fwt-spin\"></i>&nbsp;Loading");
        $("#tblCustomers .table-loading").show();

        //sOrderByAfterScroll = sOrderBy;

        var sCustomersGridPageNumberValue = $('#hfCustomersGridPageNumber').val();
        if (sCustomersGridPageNumberValue == "") {
            sCustomersGridPageNumberValue = "0";
            $('#hfCustomersGridPageNumber').val("0");
            $("#tblCustomers thead, #tblCustomers tbody").html("");
            //alert($("#tblProjects").parent().html());
            sOrderByAsc = 0;
            bBlankInFirst = true;
        }
        var sIsActive = 1;//$("#rbtnIsActive input:checked").val();       
        var jsonVar = { 'sOrderBy': sOrderBy, 'sGridPageNumber': sCustomersGridPageNumberValue, 'sUserName': "", 'sIsActive': sIsActive, 'sSearchText': $("#hfSearchText").val(), 'sSearchBY': $("#hfSearchBy").val() };
        console.log(jsonVar)
        CallAjaxMethod("Client_GetAllByPage", jsonVar, "Client_GetAllByPage_Callback");

        console.log("GetAllCustomers Ended");
    }
    catch (err) {
        alert("Error occured in  function(GetAllCustomers)" + err);
    }

}
function Client_GetAllByPage_Callback(data) {

    try {

        console.log("Client_GetAllByPage_Callback Started");
        var sRow = "";
        $("#tblCustomers tbody").html("");//add line for back and privious button
        if (data.d.length > 0) {
            if (data.d[0].ID == "0") {
                $("#tblCustomers .table-message").html("No more data available to read.");
                $('#hfCustomersGridPageNumber').val("End");
                bBlankInFirst = false;
            }
            else if (data.d[0].ERROR != null && data.d[0].ERROR != "") {
                $('#tblCustomers .table-message').html(data.d[0].ERROR);
                $('#hfCustomersGridPageNumber').val("End");
                $("#lblCustomersPageNumber").html("");
                bBlankInFirst = false;
            }
            else {
                $('#tblCustomers .table-message').html("");
                var sCustomersGridPageNumberValue = $('#hfCustomersGridPageNumber').val();
                var sRowCount = Math.ceil(data.d[0].RowCount / 10);
                var sDisplayRowCount = (parseInt(sCustomersGridPageNumberValue) + 1);
                $("#lblCustomersPageNumber").html(" &nbsp; Page : <b class=\"fwt-text-teal\">" + sDisplayRowCount + "</b> of <b class=\"fwt-text-teal\">" + sRowCount + "</b> &nbsp; ");

                if (sCustomersGridPageNumberValue == "0") {
                    $("#tblCustomers thead").html("");
                    sRow += "<tr class='rowHead'>" +
                        "<th class='rowHeadColumn' style='width:8%;' align='center'>CUSTOMER ID</th>" +
                        "<th class='rowHeadColumn' style='width:15%;' align='center'>CUSTOMER NAME</th>" +
                        "<th class='rowHeadColumn' style='width:15%;' align='center'>AGENCY NAME</th>" +
                        "<th class='rowHeadColumn' style='width:12%;' align='center'>PENDING INVOICE</th>" +
                        "<th class='rowHeadColumn' align='center' style='width:12%;'>REMAINING AMOUNT</th>" +
                        "<th class='rowHeadColumn' align='center' style='width:11%;'>ADDRESS</th>" +
                        "<th class='rowHeadColumn' align='center' style='width:11%;'>PHONE NUMBER</th>" +
                        "<th class='rowHeadColumn' align='center' style=''>EMAIL</th>" +
                        "<th class='' align='center' style='width:100px!important;'>ACTION</th>";
                        "</tr> ";
                    $("#tblCustomers thead").append(sRow);
                }

                for (var i = 0; i < data.d.length; i++) {
                    sRow = "";
                    var sAgencyName = "-";
                    var sAgencyID = "0";
                    if (data.d[i].AgencyName != "" && data.d[i].AgencyName != "0") {
                        sAgencyID = data.d[i].Agency_ID;
                    }
                    data.d[i].AgencyName = data.d[i].AgencyName || "";
                    data.d[i].EMail = data.d[i].EMail || "";
                    data.d[i].City = data.d[i].City || "";
                    data.d[i].State_Disp = data.d[i].State_Disp || "";
                    sRow += "<tr class='rowHover'>" +
                        "<td class='fwt-center' title=\"View All Documents\">" + data.d[i].Customer_ID + "</td>" +
                        "<td align='center' onclick=\"javascript:SelectCustomersEditButton('" + data.d[i].Customer_ID + "');\" >" + data.d[i].CustomerName + "</td>" +
                        "<td align='center' onclick=\"javascript:SelectAgencyEditButton('" + sAgencyID + "');\" >" + data.d[i].AgencyName + " </td>" +
                        "<td align='center'>" + "<span class=\"fwt-text-blue\">" + data.d[i].Invoices_Open_Count + " Open Invoice(s)</span>" + "<br><b><span class=\"fwt-text-red\">" + data.d[i].Invoices_Overdue_Count + " Overdue Invoice(s)" + "</span></b></td>" +
                        "<td align='center'><i class='fa fa-inr'></i> " + parseFloat(data.d[i].RemainingAmount).toFixed(2) + "</td>" +
                        "<td align='center'>" + data.d[i].City + "<br>" + data.d[i].State_Disp + "</td>" +
                        "<td align='center'>" + data.d[i].PhoneNumber1 + "</td>" +
                        "<td align='center'>" + data.d[i].EMail + "</td>" +
                        "<td class='fwt-center fwt-btn-group'>" +
                        "   <button class='fwt-btn fwt-round fwt-text-green fwt-white fwt-hover-green pointer' onclick=\"return OpenPartialPagePopup('client/update/" + data.d[i].ID + "', 'EDIT CLIENT');\" title='Edit'> <i class=\"fa fa-pencil fa-2x\"></i> </button>" +
                        "   <button class='fwt-btn fwt-round fwt-text-red fwt-white fwt-hover-red' id='btnDeleteNote_" + i + "' type='button' name='btnDeleteNote" + i + "' onclick='Note_Delete(this.id, \"" + data.d[i].ID + "\");' title='Delete'> <i class=\"fa fa-trash fa-2x\"></i> </button>" +
                        "</td>" +
                        "</tr>";
                    $("#tblCustomers tbody").append(sRow);
                }
                $('#tblCustomers .table-loading').hide();
                // $('#hfCustomersGridPageNumber').val(new String(parseInt(sCustomersGridPageNumberValue) + 1));//comment line for back and privious button
                bBlankInFirst = false;
            }
        }
        else {

            $('#tblCustomers .table-loading').hide();
            sOrderByAsc = 0;
            sOrderBy = "";
            if (bBlankInFirst == true) {
                $('#tblCustomers .table-message').html("There is no data to display for this tab.")
                    .animate({ 'font-size': '+=3px' }, 100).delay(200).animate({ 'font-size': '-=3px' }, 50);
            }
            else {
                $('#tblCustomers .table-message').html("No more data available to read.");
            }
        }
        IsProcessingHomeGrid = false;

        console.log("Client_GetAllByPage_Callback Ended");
    }
    catch (err) { alert("Error occured in  function(Client_GetAllByPage_Callback) " + err); }
}
function SetCustomersGridPageNumberBlank() {
    try {
        $('#hfCustomersGridPageNumber').val("");
    }
    catch (err) {
        alert("Error occured in  function(SetCustomersGridPageNumberBlank) " + err);
    }
}
function SelectCustomersEditButton(id) {
    try {
        $('#hfCustomer_ID').val(id);
        window.location.href = "customer/" + id;
    }
    catch (err) {
        alert("Error occured in  function(SelectCustomersSingleRadiobutton) " + err);
    }
}
function SelectAgencyEditButton(id) {
    try {
        if (id != "0") {
            window.location.href = "agency/" + id;
        }
    }
    catch (err) {
        alert("Error occured in  function(SelectAgencyEditButton) " + err);
    }
}
//function SelectCustomersStatusLink(sCustomer_ID) {
//    try {
//        window.location = "invoices.aspx?customer_id=" + sCustomer_ID;
//    }
//    catch (err) {
//        alert("Error occured in  function(SelectCustomersStatusLink) " + err);
//    }
//}

