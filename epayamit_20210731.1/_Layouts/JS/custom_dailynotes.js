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
        CallAjaxMethod("DailyNotes_SelectGrid", jsonVar, "DailyNotes_SelectGrid_Complete");

    }
    catch (err) {
        alert("Error occured in  function(GetAllSubDailyNotes) " + err);
    }
}
function DailyNotes_SelectGrid_Complete(data) {
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
                $("#lblPageNumber").html("Search Page No: <b class=\"fwt-text-red\">" + sDisplayRowCount + "</b> of <b class=\"fwt-text-red\">" + sRowCount + "</b>");


                var sCssClass = "background:#D5E990;", sCssClassCreatedDate = "", sCssClassNote = "", sCssClassVehicleNumber = "", sCssClassName = "",
                    sCssClassMobileNumber = "", sCssClassWork = "", sCssClassTotalAmount = "", sCssClassAwak = "", sCssClassJawak = "", sCssClassBalance = "";
                var sOrderBy = "";
                if (sOrderBy == "CreatedDT") sCssClassIsActive = sCssClass;
                else if (sOrderBy == "Note") sCssClassSubCategory_Core = sCssClass;
                else if (sOrderBy == "VehicleNumber") sCssClassVehicleNumber = sCssClass;
                else if (sOrderBy == "Name") sCssClassName = sCssClass;
                else if (sOrderBy == "MobileNumber") sCssClassMobileNumber = sCssClass;
                else if (sOrderBy == "Work") sCssClassWork = sCssClass;
                else if (sOrderBy == "TotalAmount") sCssClassTotalAmount = sCssClass;
                else if (sOrderBy == "Awak") sCssClassAwak = sCssClass;
                else if (sOrderBy == "Jawak") sCssClassJawak = sCssClass;
                else if (sOrderBy == "Balance") sCssClassBalance = sCssClass;
                //if (sDailyNotesGridPageNumber == "0") {
                $("#tblDailyNotes thead").html("");
                sRow += "<tr class='rowHead'><th class='rowHeadColumn' style='width:2%;' title=\"Invoice\">View</th>" +
                    "<th  class='rowHeadColumn LinkHeader' style='width:10%;' align='center'>Created Date</th>" +
          "<th class='rowHeadColumn LinkHeader' align='center' style='width:10%;'>Name </th>" +
           "<th class='rowHeadColumn LinkHeader' align='center' style='width:6%;'>VehicleNumber </th>" +
            "<th class='rowHeadColumn LinkHeader' align='center' style='width:6%;'>MobileNumber </th>" +
             "<th class='rowHeadColumn LinkHeader' align='center' style='width:10%;'>Work </th>" +
              "<th class='rowHeadColumn LinkHeader' align='center' style='width:6%;'>TotalAmount </th>" +
               "<th class='rowHeadColumn LinkHeader' align='center' style='width:10%;'>Awak </th>" +
                "<th class='rowHeadColumn LinkHeader' align='center' style='width:10%;'>Jawak </th>" +
                 "<th class='rowHeadColumn LinkHeader' align='center' style='width:6%;'>Balance </th>" +
                  "<th class='rowHeadColumn LinkHeader' align='center' style='width:19%;'>Note </th>" +
                "<th onclick='SelectPaymentsGridPageNumberBlank(); ' class='rowHeadColumn LinkHeader' align='center' style='width:5%;'> Delete </th></tr>";
                $("#tblDailyNotes thead").append(sRow);
                //}
                for (var i = 0; i < data.d.length; i++) {
                    sRow = "";
                    var sTimeString = jQuery.timeago(data.d[i].CreatedDT);
                    var sNoteDisplay = (data.d[i].Note.length > 70) ? data.d[i].Note.substring(0, 66) + "...." : data.d[i].Note;
                    var sAwak = (data.d[i].Awak.length > 50) ? data.d[i].Awak.substring(0, 46) + "...." : data.d[i].Awak;
                    var sJawak = (data.d[i].Jawak.length > 50) ? data.d[i].Jawak.substring(0, 46) + "...." : data.d[i].Jawak;
                    sRow += "<tr class='rowHover'>" +
                        "<td align='center' class=' fwt-hover-text-blue' style=' cursor:pointer;width:2%;'onclick=\"javascript:SelectDailyNotesLink('" + data.d[i].ID + "');\"><i class=\"fa fa-pencil\"></i></td>" +
                        "<td  class='fwt-center' style='" + sCssClassCreatedDate + "' title='" + data.d[i].CreatedDT + "' style=' cursor:pointer;'>" + sTimeString + "</td>" +
                                 "<td class=' fwt-hover-text-blue' style=''>" + data.d[i].Name + "</td>" +
                                   "<td class=' fwt-hover-text-blue' style=''>" + data.d[i].VehicleNumber + "</td>" +
                                     "<td class=' fwt-hover-text-blue' style=''>" + data.d[i].MobileNumber + "</td>" +
                                       "<td class=' fwt-hover-text-blue' style=''>" + data.d[i].Work + "</td>" +
                                         "<td class=' fwt-hover-text-blue' style=''>" + data.d[i].TotalAmount + "</td>" +
                                           "<td class=' fwt-hover-text-blue' style=''>" + sAwak + "</td>" +
                                             "<td class=' fwt-hover-text-blue' style=''>" + sJawak + "</td>" +
                                               "<td class=' fwt-hover-text-blue' style=''>" + data.d[i].Balance + "</td>" +
                                                  "<td class=' fwt-hover-text-blue'>" + sNoteDisplay + "</td>" +
                                "<td class='fwt-center' style='cursor:pointer;width:2%;'><input id='btnDeleteNote_" + i + "' type='button' name='btnDeleteNote" + i + "' onclick='Note_Delete(this.id, \"" + data.d[i].ID + "\");' value='Delete'/></td></tr>";
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
    catch (err) { alert("Error occured in  function(SubDailyNotes_Select_Complete) " + err); }
}
function SelectDailyNotesLink(sDataID) {
    try {

        var jsonVar = { 'sDataID': sDataID };
        CallAjaxMethod("DailyNotes_Edit", jsonVar, "DailyNotes_Edit_Complete");
    }
    catch (err) {
        alert("There is an issue calling the function (SelectDailyNotesLink)" + err);
    }

}
function Note_Delete(btnID, sDataID) {
    try {

        $("#" + btnID).parent().parent().remove();
        var sJsonVar = { 'sDataID': sDataID };
        CallAjaxMethod("DailyNotes_Delete", sJsonVar, "DailyNotes_Delete_Complete");
        $('#hfDailyNotesGridPageNumber').val("0");
        //if (iDailyNotesGridPageNumber == 0)
        //    $("#tblDailyNotes thead").html("");        
    }
    catch (err) {
        alert("There is an issue calling the function (Note_Delete)" + err);
    }
}
function DailyNotes_Edit_Complete(data) {
    try {
        var $Data = data.d;
        if ($Data.length > 0) {
            if ($Data[0].ERROR != "") {
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
        ShowMessage("There is an issue calling the function (DailyNotes_Edit_Complete)" + err);
    }
}
function DailyNotes_Delete_Complete(data) {
    try {
        var $Data = data.d;
        if ($Data.length > 0) {
            if ($Data[0].ERROR != "") {
                ShowMessage($Data[0].ERROR);
            }
            else {
                ShowMessage($Data[0].Message);
                $("#idDialog_Reason").dialog("close");
                GetAllDailyNotes(sOrderBy);
            }
        }

    } catch (err) {
        ShowMessage("There is an issue calling the function (DailyNotes_Delete_Complete)" + err);
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