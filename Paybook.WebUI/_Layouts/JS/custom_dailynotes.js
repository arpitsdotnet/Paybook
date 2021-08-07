var sOrderBy = "", sOrderByAsc = "0";
var bBlankInFirst = true;
function GetAllDailyNotes() {
    try {

        $('#tblDailyNotes .table-message').html("<i class=\"fa fa-refresh fwt-spin\"></i>&nbsp;Loading");
        $('#tblDailyNotes .table-loading').show();

        var sDailyNotesGridPageNumberValue = $('#hfDailyNotesGridPageNumber').val();
        if (sDailyNotesGridPageNumberValue == "") {
            sDailyNotesGridPageNumberValue = "0";
            $('#hfDailyNotesGridPageNumber').val("0");
            sOrderByAsc = 0;
            bBlankInFirst = true;
            $("#tblDailyNotes thead, #tblDailyNotes tbody").html("");
        }

        var jsonVar = { 'sGridPageNumber': sDailyNotesGridPageNumberValue };
        // var jsonVar = { "sCategory_Core": $("#ddlDailyNotes").val() };
        CallAjaxMethod("DailyNotes_SelectGrid", jsonVar, "DailyNotes_SelectGrid_Callback");

    }
    catch (err) {
        alert("Error occured in  function(GetAllSubDailyNotes) " + err);
    }
}
function DailyNotes_SelectGrid_Callback(data) {
    try {
        var sRow = "";
        $("#tblDailyNotes tbody").html("");//add line for back and privious button
        if (data.d.length > 0) {
            $("#idPageNumber").show();//add line for back and privious button
            if (data.d[0].ID == "0") {
                $('#tblDailyNotes .table-message').html("No more data available to read.");
                $('#hfDailyNotesGridPageNumber').val("End");
                bBlankInFirst = false;
                $("#lblPageNumber").html("");
            }
            else {
                var sDailyNotesGridPageNumber = $('#hfDailyNotesGridPageNumber').val();
                var sRowCount = Math.ceil(data.d[0].RowCount / 10);
                var sDisplayRowCount = (parseInt(sDailyNotesGridPageNumber) + 1);
                $("#lblPageNumber").html(" &nbsp; Page : <b class=\"fwt-text-teal\">" + sDisplayRowCount + "</b> of <b class=\"fwt-text-teal\">" + sRowCount + "</b> &nbsp; ");

                //if (sDailyNotesGridPageNumber == "0") {
                $("#tblDailyNotes thead").html("");
                sRow += "<tr class='rowHead'><th  class='rowHeadColumn' style='width:10%;' align='center'>CREATE DATE</th>" +
                    "<th class='rowHeadColumn' align='center' style='width:10%;'>NAME</th>" +
                    "<th class='rowHeadColumn' align='center' style='width:5%;'>VEHICLE NUMBER</th>" +
                    "<th class='rowHeadColumn' align='center' style='width:5%;'>MOBILE NUMBER</th>" +
                    "<th class='rowHeadColumn' align='center' style='width:10%;'>WORK</th>" +
                    "<th class='rowHeadColumn' align='center' style='width:10%;'>TOTAL AMOUNT</th>" +
                    "<th class='rowHeadColumn' align='center' style='width:5%;'>AWAK</th>" +
                    "<th class='rowHeadColumn' align='center' style='width:5%;'>JAWAK</th>" +
                    "<th class='rowHeadColumn' align='center' style='width:10%;'>BALANCE</th>" +
                    "<th class='rowHeadColumn' align='center' style=''>NOTE</th>" +
                    "<th class='' align='center' style='width:100px!important;'>ACTION</th></tr>";
                $("#tblDailyNotes thead").append(sRow);
                //}
                for (var i = 0; i < data.d.length; i++) {
                    sRow = "";
                    var sTimeString = jQuery.timeago(data.d[i].CreatedDT);
                    var sNoteDisplay = (data.d[i].Note.length > 100) ? data.d[i].Note.substring(0, 97) + " ..." : data.d[i].Note;
                    sRow += "<tr class='rowHover'>" +
                        "<td class='fwt-center' style='' title='" + data.d[i].CreatedDT + "'><i class='fa fa-clock-o'></i>&nbsp; " + sTimeString + "</td>" +
                        "<td class='' style=''>" + data.d[i].Name + "</td>" +
                        "<td class='' style=''>" + data.d[i].VehicleNumber + "</td>" +
                        "<td class='' style=''>" + data.d[i].MobileNumber + "</td>" +
                        "<td class='' style=''>" + data.d[i].Work + "</td>" +
                        "<td class='fwt-right-align' style=''>" + data.d[i].TotalAmount + "</td>" +
                        "<td class='fwt-right-align' style=''>" + data.d[i].Awak + "</td>" +
                        "<td class='fwt-right-align' style=''>" + data.d[i].Jawak + "</td>" +
                        "<td class='fwt-right-align' style=''>" + data.d[i].Balance + "</td>" +
                        "<td class=''>" + sNoteDisplay + "</td>" +
                        "<td class='fwt-center fwt-btn-group'>" +
                        "   <button class='fwt-btn fwt-round fwt-text-green fwt-white fwt-hover-green pointer' onclick=\"return OpenPartialPagePopup('note/update/" + data.d[i].ID + "', 'EDIT NOTE');\" title='Edit'> <i class=\"fa fa-pencil fa-2x\"></i> </button>" +
                        "   <button class='fwt-btn fwt-round fwt-text-red fwt-white fwt-hover-red' id='btnDeleteNote_" + i + "' type='button' name='btnDeleteNote" + i + "' onclick='Note_Delete(this.id, \"" + data.d[i].ID + "\");' title='Delete'> <i class=\"fa fa-trash fa-2x\"></i> </button>" +
                        "</td></tr>";
                    //SelectDailyNotesLink('" + data.d[i].ID + "');
                    $("#tblDailyNotes tbody").append(sRow);

                }

                $('#tblDailyNotes .table-loading').hide();
                bBlankInFirst = false;
            }
        }
        else {
            $('#tblDailyNotes .table-loading').hide();
            sOrderByAsc = 0;
            sOrderBy = "";
            if (bBlankInFirst == true) {
                $('#tblDailyNotes .table-message').html("There is no data to display for this tab.")
                    .animate({ 'font-size': '+=3px' }, 100).delay(200).animate({ 'font-size': '-=3px' }, 50);
            }
            else {
                $('#tblDailyNotes .table-message').html("No more data available to read.");
            }
        }
        IsProcessingHomeGrid = false;
    }
    catch (err) { alert("Error occured in  function(SubDailyNotes_Select_Callback) " + err); }
}
function SelectDailyNotesLink(sDataID) {
    try {
        var jsonVar = { 'sDataID': sDataID };
        CallAjaxMethod("DailyNotes_Edit", jsonVar, "DailyNotes_Edit_Callback");
    }
    catch (err) {
        alert("There is an issue calling the function (SelectDailyNotesLink)" + err);
    }

}
function Note_Delete(btnID, sDataID) {
    try {
        if (confirm("Are you sure, you want to delete the record permanently, this will not be recoverable?")) {
            $("#" + btnID).parent().parent().remove();
            var sJsonVar = { 'sDataID': sDataID };
            CallAjaxMethod("DailyNotes_Delete", sJsonVar, "DailyNotes_Delete_Callback");
            $('#hfDailyNotesGridPageNumber').val("0");
            //if (iDailyNotesGridPageNumber == 0)
            //    $("#tblDailyNotes thead").html("");        
        }
        return false;
    }
    catch (err) {
        alert("There is an issue calling the function (Note_Delete)" + err);
    }
}
function DailyNotes_Edit_Callback(data) {
    try {
        var $Data = data.d;
        if ($Data.length > 0) {
            if ($Data[0].ERROR != null && $Data[0].ERROR != "") {
                ShowMessage($Data[0].ERROR);
            }
            else {
                $("#txtDailyNotes").val($Data[0].Note);
                $("#hfDataID").val($Data[0].ID);
                $("#txtVehicleNo").val($Data[0].VehicleNumber);
                $("#txtName").val($Data[0].Name);
                $("#txtMobileNumber").val($Data[0].MobileNumber);
                $("#txtAwak").val($Data[0].Awak);
                $("#txtJawak").val($Data[0].Jawak);
                $("#txtBalance").val($Data[0].Balance);
                $("#txtTotalAmount").val($Data[0].TotalAmount);
                $("#txtWork").val($Data[0].Work);

            }
        }

    } catch (err) {
        ShowMessage("There is an issue calling the function (DailyNotes_Edit_Callback)" + err);
    }
}
function DailyNotes_Delete_Callback(data) {
    try {
        var $Data = data.d;
        if ($Data.length > 0) {
            if ($Data[0].ERROR != null && $Data[0].ERROR != "") {
                ShowMessage($Data[0].ERROR);
            }
            else {
                ShowMessage($Data[0].Message);
                $("#idDialog_Reason").dialog("close");
                GetAllDailyNotes(sOrderBy);
            }
        }

    } catch (err) {
        ShowMessage("There is an issue calling the function (DailyNotes_Delete_Callback)" + err);
    }
}
function SetDailyNotesGridPageNumberBlank() {
    try {
        $('#hfDailyNotesGridPageNumber').val("");
    }
    catch (err) {
        alert("Error occured in  function(SetDailyNotesGridPageNumberBlank) " + err);
    }
}