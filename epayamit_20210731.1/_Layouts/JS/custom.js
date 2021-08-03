var __Messages = {
    _mob: /^(?:(\s*[\-]\s*)?|[0]?)?[0-9]\d{9}$/,
    _email: /^[a-zA-Z0-9._-]+@[a-zA-Z.-]+\.[a-zA-Z]{2,4}$/,

};

function startTime() {
    var mydate = new Date();
    var year = mydate.getYear();

    if (year < 1000)
        year += 1900

    var day = mydate.getDay(); // Current Day of week - 2
    var month = mydate.getMonth(); // Current Month 2
    var daym = mydate.getDate(); // Current Date -24
    var h = mydate.getHours(); //Hours
    var apm = "AM";
    if (h >= 12) {
        h = h - 12;
        apm = "PM";
    }
    else {
        apm = "AM";
    }
    var m = mydate.getMinutes();//Minutes
    var s = mydate.getSeconds();//Seconds

    h = checkTime(h);
    m = checkTime(m);
    s = checkTime(s);

    function checkTime(i) {
        if (i < 10) { i = "0" + i };  // add zero in front of numbers < 10
        return i;
    }

    var dayarray = new Array("Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat")
    var montharray = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec")

    $("#idDateTimeShow div.time").html(h + ":" + m);
    $("#idDateTimeShow div.apm").html(apm);
    $("#idDateTimeShow div.seconds").html(s);
    $("#idDateTimeShow div.day").html(montharray[month] + " " + daym + ", " + year + " (" + dayarray[day] + ")");

    var t = setTimeout(function () { startTime() }, 1000);
}
function SelectOnClick(sID) {
    $("#" + sID).click(function () {
        $(this).select();
    });
}

var sSiteUrlMain = location.protocol + "//" + location.hostname + (location.port && ":" + location.port) + "/";
var sSiteUrlValue = (sSiteUrlMain.indexOf("localhost") >= 0) ? sSiteUrlMain + "" : sSiteUrlMain;
var sLodingImagePath = "";
function CallAjaxMethod(sAjaxMethodToCall, sJsonVar, sMethodToExecute) {
    try {
        var sUrl = sSiteUrlValue + "_Layouts/WS_WebService.aspx/" + sAjaxMethodToCall;
        //        alert(sUrl);
        //        alert(sJsonVar);
        $.ajax({
            type: "POST",
            url: sUrl,
            data: JSON.stringify(sJsonVar),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("There is an issue processing your request in function " + sAjaxMethodToCall + ". Details: Request Status:" + XMLHttpRequest.status + "," + "Status: " + textStatus + "Error: " + errorThrown);
                //window[sMethodToExecute]("");
            },
            success: function (data) {
                try {
                    window[sMethodToExecute](data);
                }
                catch (err) {
                    alert("There is an issue executing the function " + sAjaxMethodToCall + ", Reason: " + err);
                    window[sMethodToExecute]("");
                }
            }
        });

    }
    catch (err) {
        alert("There is an issue calling the function " + sAjaxMethodToCall + ", Reason: " + err);
        window[sMethodToExecute]("");
    }
}
$(document).ready(function () {
    sXmlData = "";
});

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

function CreateDataTable(sTableName, sColumns, sData) {
    try {
        $("#" + sTableName + " .table-message").html("<i class=\"fa fa-refresh fwt-spin\"></i>&nbsp;Loading");
        $("#" + sTableName + " .table-loading").show();
        var sRow = "";
        if ($.trim($("#" + sTableName + " thead").html()).length == 0) {

            sRow = "<tr class='rowHead'>";
            for (var i = 0; i < sColumns.length; i++) {
                if (sColumns[i].Width != "0")
                    sRow += "<th class='rowHeadColumn LinkHeader' width='" + sColumns[i].Width + "%' align='center'>" + sColumns[i].Name + "</th>";
            }
            sRow += "</tr>";
            $("#" + sTableName + " thead").append(sRow);

            //fixHeader(sTableName, 'div_Header' + sTableName);
        }

        sRow = "<tr class='rowHover'>";
        for (var i = 0; i < sData.length; i++) {
            if (sColumns[i].Width == "0") {
                iParticularID = sData[i];
            }
            else {
                sRow += "<td style='width:" + sColumns[i].Width + "%;'>" + sData[i] + "</td>";

            }
        }
        sRow += "</tr>";
        $("#" + sTableName + " tbody").append(sRow);
        // iRowCount++;

        $('#divGridLoading' + sTableName).hide();
    }
    catch (exe) {
        alert("Error occured (CreateDataTable): " + exe);
    }
}

//function ShowDialog_Reason() {
//    try {

//        $("#idDialog_Reason").dialog({
//            modal: true,
//            title: "Status",
//            width: 400,
//            height: 300,
//            closeOnEscape: true,

//            open: function (event, ui) {
//                $(".ui-dialog").addClass("ui-dialog-shadow");
//                $("#txtReason, #btnSubmitIsActive").prop("disabled", false);
//            },
//            close: function () {
//            }

//        });

//    }
//    catch (err) {
//        ErrorMessage("WARNING",err);
//    }
//}

function PageActiveCall() {

    // alert($("#hfIsActive").val()+"ggf");
    if ($("#hfIsActive").val() == 1) {
        $("#idActive").html("Active").css({ 'color': "#7AB900" });
        $("input,select").prop("disabled", false);
    }
    else {
        $("#idActive").html("Inactive").css({ 'color': "#FF2525" });
        $("input,select").prop("disabled", true);
    }
}
function Name_Search_Complete(data) {
    try {
        var $Data = data.d;

        if ($Data.length > 0) {

            var sMethodCall = $Data[0].sSearchFrom;
            var idTxtSearch = $Data[0].idTxtSearch;
            var idSearchResult = $Data[0].idSearchResult;

            var $SearchFormValue = $('#' + idSearchResult);
            $SearchFormValue.html('');
            for (var i = 0; i < $Data.length; i++) {
                var sDiv = "<div class=\"fwt-pointer\" onclick=\"SearchName_Select('" + idTxtSearch + "','" + idSearchResult + "','" + $Data[i].AgentName + "');\">" + $Data[i].AgentName + "</div>";
                $SearchFormValue.append(sDiv);
            }
            $SearchFormValue.slideDown(400);
        }

    }
    catch (err) {
        alert("There is an issue calling the function (Name_Search_Complete)" + err);
    }
    return false;
}

function SearchName_Select(txtID, idSearch, txtName) {
    try {
        $("#" + txtID).val(txtName);
        $("#" + idSearch).slideUp(100);
    }
    catch (err) {
        alert("There is an issue calling the function (SearchName_Select)" + err);
    }
}
//function LastSavedID_Create(sType, sControlID) {
//    try {

//        var jsonVar = { 'sType': sType, 'sControlID': sControlID };
//        CallAjaxMethod("GetLastSavedID", jsonVar, "GetLastSavedID_Complete");

//    }
//    catch (err) {

//        alert("There is an issue calling the function (LastSavedID_Create)" + err);
//    }
//}
//function GetLastSavedID_Complete(data) {
//    try {
//        var $Data = data.d;

//        if ($Data.length > 0) {
//            // var sControlID = $Data[0].ControlID;
//            var sID = $Data[0].ID;
//            var sControlID = $Data[0].ControlID.split("/");
//            if (sControlID[0] == "Label")
//                $("#" + sControlID[1]).html(sID);
//            else
//                $("#" + sControlID[1]).val(sID);
//        }
//    }
//    catch (err) {
//        alert("There is an issue calling the function (GetLastSavedID_Complete)" + err);
//    }
//    return false;
//}

function OpenExportNavigations() {
    var x = document.getElementById("idExportNavigations");
    if (x.className.indexOf("fwt-show") == -1)
        x.className += " fwt-show";
    else
        x.className = x.className.replace(" fwt-show", "");

    return false;
}

var sOrderBy = "", sOrderByAsc = "0";
var bBlankInFirst = true;



//Grid Agent
function GetAllAgents(sOrderBy) {

    try {
        //        var sAllTabName = $('#' + sAllTabName_HiddenField + '').val().split('|');
        //        var sUserNameEmail = $('#' + sLoginUserNameEmailID_HiddenField + '').val();
        //        //alert(sGridPageNumberValue);

        $('#divAgents .table-message').html("<i class=\"fa fa-refresh fwt-spin\"></i>&nbsp;Loading");
        $('#divAgents .table-loading').show();

        //sOrderByAfterScroll = sOrderBy;

        var sAgentsGridPageNumberValue = $('#hfAgentsGridPageNumber').val();
        if (sAgentsGridPageNumberValue == "") {
            sAgentsGridPageNumberValue = "0";
            $('#hfAgentsGridPageNumber').val("0");
            $('#divAgents .table-header').html("");

            $("#divAgents .table-main thead").html("");
            $("#divAgents .table-main tbody").html("");
            //alert($("#tblProjects").parent().html());
        }


        var sIsActive = $("#rbtnIsActive input:checked").val();
        //alert('sSiteUrl' + sSiteUrl + '\nsTabSelected' + sTabName + '\nsOrderBy' + sOrderBy + '\nsOrderByAsc' + sOrderByAsc + '\nsGridPageNumber' + sGridPageNumber + '\nsUserNameEmail' + sUserNameEmail);
        var jsonVar = { 'sOrderBy': sOrderBy, 'sGridPageNumber': sAgentsGridPageNumberValue, 'sUserName': "", 'sIsActive': sIsActive };

        CallAjaxMethod("Agents_SelectAll", jsonVar, "Agents_SelectAll_Complete");

    }
    catch (err) {
        alert("Error occured in  function(GetAllAgents) " + err);
    }
}
function Agents_SelectAll_Complete(data) {

    try {
        if (data.d.length > 0) {
            if (data.d[0].ERROR != "") {
                $('#divAgents .table-message').html(data.d[0].ERROR);
                $('#hfAgentsGridPageNumber').val("End");
            }
            else if (data.d[0].ID == "0") {
                $('#divAgents .table-message').html("No more data available to read.");
                $('#hfAgentsGridPageNumber').val("End");
                bBlankInFirst = false;
            }
            else {
                $('#divAgents .table-message').html("");
                var sAgentsGridPageNumberValue = $('#hfAgentsGridPageNumber').val();
                var sRowCount = data.d[0].RowCount;
                var sPageNumber = parseInt(sAgentsGridPageNumberValue) + 1;

                //label show page number
                var sDisplayRowCount = (10 * (parseInt(sAgentsGridPageNumberValue) + 1)) - (10 - data.d.length);
                $("#lblAgentsPageNumber").html("Search Result: <b class=\"fwt-text-red\">" + sDisplayRowCount + "</b> of <b class=\"fwt-text-red\">" + sRowCount + "</b>");
                //        

                var sCssClass = "background:#D5E990;", sCssClassAgent_ID = "", sCssClassAgentName = "", sCssClassPrefix_Disp = "";
                var sCssClassAddress1 = "", sCssClassAddress2 = "", sCssClassCity = "", sCssClassState_Disp = "", sCssClassCountry_Core = "",
                sCssClassEMail = "", sCssClassPhoneNumber1 = "", sCssClassPhoneNumber2 = "";
                if (sOrderBy == "Customer_ID") sCssClassAgent_ID = sCssClass;
                else if (sOrderBy == "AgentName") sCssClassAgentName = sCssClass;
                else if (sOrderBy == "Address1") sCssClassAddress1 = sCssClass;
                else if (sOrderBy == "Address2") sCssClassAddress2 = sCssClass;
                else if (sOrderBy == "City") sCssClassCity = sCssClass;
                else if (sOrderBy == "State_Disp") sCssClassState_Disp = sCssClass;
                else if (sOrderBy == "Country_Core") sCssClassCountry_Core = sCssClass;
                else if (sOrderBy == "EMail") sCssClassEMail = sCssClass;
                else if (sOrderBy == "PhoneNumber1") sCssClassPhoneNumber1 = sCssClass;
                else if (sOrderBy == "PhoneNumber2") sCssClassPhoneNumber2 = sCssClass;

                for (var i = 0; i < data.d.length; i++) {
                    $("#divAgents .table-main tbody").append("<tr class='rowHover'><td align='center' style='width:2%;'><input id='RadioButton1' type='radio' name='RadioButton1' value='RadioButton1' onclick=\"javascript:SelectAgentsSingleRadiobutton('" + data.d[i].Agent_ID + "');\" /></td>" +
                                "<td align='center' style='" + sCssClassAgent_ID + "' title=\"View All Documents\">" + data.d[i].Agent_ID + "</td>" +
                                "<td  class='Cursor_Point fwt-center' style='" + sCssClassAgentName + "'>" + data.d[i].AgentName + "</td>" +
                                "<td align='center' style='" + sCssClassPhoneNumber1 + "'>" + data.d[i].PhoneNumber1 + "</td>" +
                                "<td align='center' style='" + sCssClassEMail + "'>" + data.d[i].EMail + "</td>" +
                                "<td align='center' style='" + sCssClassCity + "'>" + data.d[i].City + "</td>" +
                                "<td align='center' style='" + sCssClassState_Disp + "'>" + data.d[i].State_Disp + "</td>" +
                                "<td align='center' style='" + sCssClassCountry_Core + "'>" + data.d[i].Country_Core + "</td></tr>");
                }

                //var sAgentsGridPageNumberValue = $('#hfAgentsGridPageNumber').val();
                if (sAgentsGridPageNumberValue == "0") {
                    $("#divAgents .table-main thead").append("<tr class='rowHead'><th class='rowHeadColumn' style='width:2%;' title=\"Edit Agent\"><i class=\"fa fa-pencil\"></i></th>" +
                                "<th onclick='SetAgentsGridPageNumberBlank(); Agents_SelectAll(\"Agent_ID\");' class='rowHeadColumn LinkHeader' style='width:10%;' align='center'>Agent ID</th>" +
                                "<th onclick='SetAgentsGridPageNumberBlank(); Agents_SelectAll(\"AgentName\");' class='rowHeadColumn LinkHeader' style='width:20%;' align='center'>Agent Name</th>" +
                                "<th onclick='SetAgentsGridPageNumberBlank(); Agents_SelectAll(\"PhoneNumber1\");' class='rowHeadColumn LinkHeader' align='center' style='width:10%;'>Phone Number1 </th>" +
                                "<th onclick='SetAgentsGridPageNumberBlank(); Agents_SelectAll(\"EMail\");' class='rowHeadColumn LinkHeader' align='center' style='width:14%;'>Email</th>" +
                                "<th onclick='SetAgentsGridPageNumberBlank(); Agents_SelectAll(\"City\");' class='rowHeadColumn LinkHeader' align='center' style='width:13%;'>City</th>" +
                                "<th onclick='SetAgentsGridPageNumberBlank(); Agents_SelectAll(\"State_Disp\");' class='rowHeadColumn LinkHeader' align='center' style='width:10%;'>State</th>" +
                                "<th onclick='SetAgentsGridPageNumberBlank(); Agents_SelectAll(\"Country_Core\");' class='rowHeadColumn LinkHeader' align='center' style='width:10%;'>Country</th></tr>");
                    $('#divAgents .table-main').floatThead();

                }

                $('#divAgents .table-loading').hide();
                $('#hfAgentsGridPageNumber').val(new String(parseInt(sAgentsGridPageNumberValue) + 1));
                bBlankInFirst = false;
            }
        }
        else {
            $('#divAgents .table-loading').hide();
            sOrderByAsc = 0;
            sOrderBy = "";
            if (bBlankInFirst == true) {
                $('#divAgents .table-message').html("There is no data to display for this tab.")
                            .animate({ 'font-size': '+=3px' }, 100).delay(200).animate({ 'font-size': '-=3px' }, 50);
            }
            else {
                $('#divAgents .table-message').html("No more data available to read.");
            }
        }
        IsProcessingHomeGrid = false;

    }
    catch (err) { alert("Error occured in  function(Agents_SelectAll_Complete) " + err); }
}
function SetAgentsGridPageNumberBlank() {
    try {
        $('#hfAgentsGridPageNumber').val("");
    }
    catch (err) { alert("Error occured in  function(SetAgentsGridPageNumberBlank) " + err); }
}
function SelectAgentsSingleRadiobutton(id) {
    try {
        $('#hfAgent_ID').val(id);
    }
    catch (err) { alert("Error occured in  function(SelectAgentsSingleRadiobutton) " + err); }
}

//MessageBox
function ShowMessage(sMessage) {
    try {
        $("#divMessage").dialog({
            modal: true,
            title: "Message",
            width: 400,
            closeOnEscape: true,

            open: function (event, ui) {
                $(".ui-dialog").addClass("ui-dialog-shadow");
                $(this).parent().appendTo("form")
            },
            close: function () {
                var closeBtn = $('.ui-dialog-titlebar-close');

            },
            buttons: {
                "OK": function () {
                    $(this).dialog("close");
                }
            }
        });
        $("#divMessage #divMessageLabel").html(sMessage);
    }
    catch (err) { alert("Error occured in  function(ShowMessage) " + err); }
}
function ErrorMessage(sType, sMessage) {
    try {
        $("#idLabelError").removeClass("fwt-pale-red fwt-border-red fwt-pale-yellow fwt-border-yellow fwt-pale-green fwt-border-green");
        if (sType == "ERROR")
            $("#idLabelError").html("Error: " + sMessage).addClass("fwt-pale-red fwt-border-red");
        else if (sType == "WARNING")
            $("#idLabelError").html("Warning: " + sMessage).addClass("fwt-pale-yellow fwt-border-yellow");
        else if (sType == "SUCCESS")
            $("#idLabelError").html("Success: " + sMessage).addClass("fwt-pale-green fwt-border-green");
        $("#idLabelError").stop().slideDown(300).delay(5000).slideUp(100);
    }
    catch (err) {
        alert(err.Message);
    }
}