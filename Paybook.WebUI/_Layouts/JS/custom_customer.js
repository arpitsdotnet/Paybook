var sOrderBy = "", sOrderByAsc = "0";
var bBlankInFirst = true;
function GetAllCustomers(sOrderBy) {
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

        CallAjaxMethod("Customers_SelectAll", jsonVar, "Customers_SelectAll_Complete");
    }
    catch (err) {
        alert("Error occured in  function(GetAllCustomers)" + err);
    }

}
function Customers_SelectAll_Complete(data) {

    try {

        var sRow = "";
        $("#tblCustomers tbody").html("");//add line for back and privious button
        if (data.d.length > 0) {
            if (data.d[0].ID == "0") {
                $("#tblCustomers .table-message").html("No more data available to read.");
                $('#hfCustomersGridPageNumber').val("End");
                bBlankInFirst = false;
            }
            else if (data.d[0].ERROR != "") {
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

                var sCssClass = "background:#D5E990;", sCssClassCustomer_ID = "", sCssClassCustomerName = "", sCssClassPrefix_Disp = "", sCssClassAgencyName = "";
                var sCssClassAddress1 = "", sCssClassAddress2 = "", sCssClassCity = "", sCssClassState_Disp = "", sCssClassCountry_Core = "",
                    sCssClassEMail = "", sCssClassPhoneNumber1 = "", sCssClassPhoneNumber2 = "", sCssClassRemainingAmount = "", sCssClassPandingInvoices = "";
                if (sOrderBy == "Customer_ID") sCssClassCustomer_ID = sCssClass;
                else if (sOrderBy == "CustomerName") sCssClassCustomerName = sCssClass;
                else if (sOrderBy == "AgencyName") sCssClassAgencyName = sCssClass;
                else if (sOrderBy == "PandingInvoices") sCssClassPandingInvoices = sCssClass;
                else if (sOrderBy == "Address1") sCssClassAddress1 = sCssClass;
                else if (sOrderBy == "Address2") sCssClassAddress2 = sCssClass;
                else if (sOrderBy == "City") sCssClassCity = sCssClass;
                else if (sOrderBy == "State_Disp") sCssClassState_Disp = sCssClass;
                else if (sOrderBy == "Country_Core") sCssClassCountry_Core = sCssClass;
                else if (sOrderBy == "EMail") sCssClassEMail = sCssClass;
                else if (sOrderBy == "PhoneNumber1") sCssClassPhoneNumber1 = sCssClass;
                else if (sOrderBy == "PhoneNumber2") sCssClassPhoneNumber2 = sCssClass;
                else if (sOrderBy == "RemainingAmount") sCssClassRemainingAmount = sCssClass;

                if (sCustomersGridPageNumberValue == "0") {
                    $("#tblCustomers thead").html("");
                    sRow += "<tr class='rowHead'><th onclick='SetCustomersGridPageNumberBlank(); Customer_SelectALL(\"Customer_ID\");' class='rowHeadColumn LinkHeader' style='width:8%;' align='center'>Customer ID</th>" +
                        "<th onclick='SetCustomersGridPageNumberBlank(); Customer_SelectALL(\"CustomerName\");' class='rowHeadColumn LinkHeader' style='width:15%;' align='center'>Customer Name</th>" +
                        "<th onclick='SetCustomersGridPageNumberBlank(); Customer_SelectALL(\"AgencyName\");' class='rowHeadColumn LinkHeader' style='width:15%;' align='center'>Agency Name</th>" +
                        "<th onclick='SetCustomersGridPageNumberBlank(); Customer_SelectALL(\"PandingInvoices\");' class='rowHeadColumn LinkHeader' style='width:12%;' align='center'>Pending Invoices</th>" +
                        "<th onclick='SetCustomersGridPageNumberBlank(); Customer_SelectALL(\"PhoneNumber1\");' class='rowHeadColumn LinkHeader' align='center' style='width:11%;'>Phone Number</th>" +
                        "<th onclick='SetCustomersGridPageNumberBlank(); Customer_SelectALL(\"EMail\");' class='rowHeadColumn LinkHeader' align='center' style='width:11%;'>Email</th>" +
                        "<th onclick='SetCustomersGridPageNumberBlank(); Customer_SelectALL(\"City\");' class='rowHeadColumn LinkHeader' align='center' style='width:11%;'>Address</th>" +
                        //"<th onclick='SetCustomersGridPageNumberBlank(); Customer_SelectALL(\"State_Disp\");' class='rowHeadColumn LinkHeader' align='center' style='width:10%;'>State</th>" +
                        //"<th onclick='SetCustomersGridPageNumberBlank(); Customer_SelectALL(\"Country_Core\");' class='rowHeadColumn LinkHeader' align='center' style='width:8%;'>Country</th>" +
                        "<th onclick='SetCustomersGridPageNumberBlank(); Customer_SelectALL(\"RemainingAmount\");' class='rowHeadColumn LinkHeader' align='center' style='width:12%;'>Remaining Amount</th></tr>";
                    $("#tblCustomers thead").append(sRow);
                }

                for (var i = 0; i < data.d.length; i++) {
                    sRow = "";
                    var sAgencyName = "-";
                    var sAgencyID = "0";
                    if (data.d[i].AgencyName != "" && data.d[i].AgencyName != "0") {
                        sAgencyName = data.d[i].AgencyName + " <i class=\"fa fa-pencil\"></i>";
                        sAgencyID = data.d[i].Agency_ID;
                    }
                    sRow += "<tr class='rowHover'><td class='fwt-center' style='" + sCssClassCustomer_ID + "' title=\"View All Documents\">" + data.d[i].Customer_ID + "</td>" +
                        "<td align='center' style='cursor:pointer;" + sCssClassCustomerName + "'onclick=\"javascript:SelectCustomersEditButton('" + data.d[i].Customer_ID + "');\" >" + data.d[i].CustomerName + " <i class=\"fa fa-pencil\"></i></td>" +
                        "<td align='center' style='cursor:pointer;" + sCssClassAgencyName + "'onclick=\"javascript:SelectAgencyEditButton('" + sAgencyID + "');\" >" + sAgencyName + " </td>" +
                        "<td align='center' style='" + sCssClassPandingInvoices + "'>" + "<span class=\"fwt-text-blue\">" + data.d[i].Invoices_Open_Count + " Open Invoices</span>" + "<br><b><span class=\"fwt-text-red\">" + data.d[i].Invoices_Overdue_Count + " Overdue Invoices" + "</span></b></td>" +
                        "<td align='center' style='" + sCssClassPhoneNumber1 + "'>" + data.d[i].PhoneNumber1 + "</td>" +
                        "<td align='center' style='" + sCssClassEMail + "'>" + data.d[i].EMail + "</td>" +
                        "<td align='center' style='" + sCssClassCity + "'>" + data.d[i].City + "<br>" + data.d[i].State_Disp + "</td>" +
                        //"<td align='center' style='" + sCssClassState_Disp + "'>" + data.d[i].State_Disp + "</td>" +
                        //"<td align='center' style='" + sCssClassCountry_Core + "'>" + data.d[i].Country_Core + "</td>" +
                        "<td align='center' style='" + sCssClassRemainingAmount + "'><i class='fa fa-inr'></i> " + parseFloat(data.d[i].RemainingAmount).toFixed(2) + "</td></tr>";
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

    }
    catch (err) { alert("Error occured in  function(Customers_SelectAll_Complete) " + err); }
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

