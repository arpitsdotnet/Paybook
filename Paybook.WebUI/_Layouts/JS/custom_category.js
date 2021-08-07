iCategoryID = 0;
var sOrderBy = "", sOrderByAsc = "0";
var bBlankInFirst = true;
function GetAllSubCategories() {
    try {

        $('#tblCategories .table-message').html("<i class=\"fa fa-refresh fwt-spin\"></i>&nbsp;Loading");
        $('#tblCategories .table-loading').show();

        var sCategoriesGridPageNumberValue = $('#hfCategoriesGridPageNumber').val();
        if (sCategoriesGridPageNumberValue == "") {
            sCategoriesGridPageNumberValue = "0";
            $('#hfCategoriesGridPageNumber').val("0");
            $("#tblCategories thead, #tblCategories tbody").html("");
            //alert($("#tblProjects").parent().html());
        }
        var jsonVar = {
            'sOrderBy': sOrderBy, 'sGridPageNumber': sCategoriesGridPageNumberValue, 'sUserName': "", "sCategory_Core": $("#ddlCategories").val()
        };
        // var jsonVar = { "sCategory_Core": $("#ddlCategories").val() };
        CallAjaxMethod("SubCategories_SelectGrid", jsonVar, "SubCategories_SelectGrid_Callback");

    }
    catch (err) {
        alert("Error occured in  function(GetAllSubCategories) " + err);
    }
}
function SubCategories_SelectGrid_Callback(data) {
    try {
        var sRow = "";
        $("#tblCategories tbody").html("");//add line for back and privious button
        if (data.d.length > 0) {
            $("#idPageNumber").show();//add line for back and privious button
            if (data.d[0].ID == "0") {
                $('#tblCategories .table-message').html("No more data available to read.");
                $('#hfCategoriesGridPageNumber').val("End");
                bBlankInFirst = false;
                $("#lblPageNumber").html("");
            }
            else {
                $('#tblCategories .table-message').html("");
                var sCategoriesGridPageNumber = $('#hfCategoriesGridPageNumber').val();
                var sRowCount = data.d[0].RowCount;
                var sPageNumber = parseInt(sCategoriesGridPageNumber) + 1;

                //label show page number
                var sRowCount = Math.ceil(data.d[0].RowCount / 10);
                var sDisplayRowCount = (parseInt(sCategoriesGridPageNumber) + 1);
                $("#lblPageNumber").html(" &nbsp; Page : <b class=\"fwt-text-teal\">" + sDisplayRowCount + "</b> of <b class=\"fwt-text-teal\">" + sRowCount + "</b> &nbsp; ");
                //

                //get prefix and last order
                $("#hfCategoryPrefix").val(data.d[0].SubCategory_Prefix);
                if ($("#hfCategoryPrefix").val() == "") {
                    $("#txtCategoryDisplayName,#txtOrderBY").attr('disabled', true);
                    $("#btnSubmitAndUpdate").attr('disabled', true);
                }
                else {
                    $("#txtCategoryDisplayName,#txtOrderBY,#btnSubmitAndUpdate").attr('disabled', false);
                }

                var iLastOrderBy = data.d[0].LastOrderBy;

                $("#txtOrderBY").val(parseInt(iLastOrderBy) + 1);
                //
                var lastindex = 0;
                var sIsActive = "";
                if (sCategoriesGridPageNumber == "0") {
                    $("#tblCategories thead").html("");
                    sRow += "<tr class='rowHead'>" +
                        "<th class='rowHeadColumn' align='center' style='width:25%;'>CATEGORY</th>" +
                        "<th class='rowHeadColumn' align='center' style='width:25%;'>SUBCATEGORY TEXT</th>" +
                        "<th class='rowHeadColumn' style='width:25%;' align='center'>CORE TEXT</th>" +
                        "<th class='rowHeadColumn' align='center' style='width:10%;'>ORDER BY</th>" +
                        "<th class='rowHeadColumn' align='center'>ACTIVE</th>" +
                        "<th class='' align='center' style='width:100px!important;'>ACTION</th></tr > ";
                    $("#tblCategories thead").append(sRow);
                }
                for (var i = 0; i < data.d.length; i++) {

                    if (data.d[i].IsActive == "1") {
                        sIsActive = "Active";
                        sCssClassIsActiveButton = style = 'background-color:#4CAF50;color:white';

                    }
                    else {
                        sIsActive = "InActive";
                        sCssClassIsActiveButton = style = 'background-color:red;color:white';
                    }
                    sRow = "";
                    sRow += "<tr class='rowHover'>" +
                        "<td class='fwt-center' align='center'>" + $("#ddlCategories :selected").text() + "</td>" +
                        "<td class='fwt-center' align='center'>" + data.d[i].SubCategory_Disp + "</td>" +
                        "<td class='fwt-center' >" + data.d[i].SubCategory_Core + "</td>" +
                        "<td class='fwt-center' >" + data.d[i].OrderBy + "</td>" +
                        "<td align='center' style='width:10%;'><button type='button' class='fwt-btn fwt-round' name='Button1' style='" + sCssClassIsActiveButton + "' onclick=\"javascript:SelectCategoriesButton('" + data.d[i].ID + "','" + data.d[i].IsActive + "');\">" + sIsActive + "</button></td>" +
                        "<td class='fwt-center fwt-btn-group'>" +
                        "   <button class='fwt-btn fwt-round fwt-text-green fwt-white fwt-hover-green pointer' onclick=\"return OpenPartialPagePopup('category/update/" + data.d[i].ID + "', 'UPDATE SUBCATEGORY');\" title='Edit'> <i class=\"fa fa-pencil fa-2x\"></i> </button>" +
                        "   <button class='fwt-btn fwt-round fwt-text-red fwt-white fwt-hover-red' id='btnDeleteNote_" + i + "' type='button' name='btnDeleteNote" + i + "' onclick='Note_Delete(this.id, \"" + data.d[i].ID + "\");' title='Delete'> <i class=\"fa fa-trash fa-2x\"></i> </button>" +
                        "</td></tr > ";
                    $("#tblCategories tbody").append(sRow);

                }

                $('#tblCategories .table-loading').hide();
                bBlankInFirst = false;
            }
        }
        else {
            $('#tblCategories .table-loading').hide();
            sOrderByAsc = 0;
            sOrderBy = "";
            if (bBlankInFirst == true) {
                $('#tblCategories .table-message').html("There is no data to display for this tab.")
                    .animate({ 'font-size': '+=3px' }, 100).delay(200).animate({ 'font-size': '-=3px' }, 50);
            }
            else {
                $('#tblCategories .table-message').html("No more data available to read.");
            }
        }
        IsProcessingHomeGrid = false;
    }
    catch (err) { alert("Error occured in  function(SubCategories_Select_Callback) " + err); }
}
function SelectSubCategoryDisp_Link(sSubCategory_Core, sSubCategory_Disp, sOrderBy) {

    $("#txtCategoryDisplayName").val(sSubCategory_Disp);
    $("#txtCategoryCoreName").val(sSubCategory_Core);
    $("#txtOrderBY").val(sOrderBy);
    $("#btnSubmitAndUpdate").val("Update");
    $("#txtCategoryDisplayName,#txtOrderBY,#btnSubmitAndUpdate").attr('disabled', false);
}
function SelectCategoriesButton(sID, sISActive) {
    try {
        if (sISActive == 1)
            sISActive = 0;
        else
            sISActive = 1;
        $('#hfsIsActive').val(sISActive);
        var jsonVar = { "sID": sID, "sISActive": sISActive };
        CallAjaxMethod("SubCategories_UpdateIsActive", jsonVar, "SubCategories_UpdateIsActive_Callback");
    }
    catch (err) { alert("Error occured in  function(SelectCategoriesButton) " + err); }
}
function SubCategories_UpdateIsActive_Callback(data) {
    try {
        var $Data = data.d;
        if ($Data.length > 0) {
            if ($Data[0].ERROR != null && $Data[0].ERROR != "") {
                ShowMessage($Data[0].ERROR);
            }
            else {
                if ($('#hfsIsActive').val() == 0)
                    ShowMessage("Categoryhas been inactived successfully !");
                else {
                    ShowMessage("Category has been Actived successfully !");

                }
            }
        }
        GetAllSubCategories();
    }
    catch (err) { alert("Error occured in  function(SubCategories_UpdateIsActive_Callback) " + err); }
}