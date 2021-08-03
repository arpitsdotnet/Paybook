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
        CallAjaxMethod("SubCategories_SelectGrid", jsonVar, "SubCategories_SelectGrid_Complete");

    }
    catch (err) {
        alert("Error occured in  function(GetAllSubCategories) " + err);
    }
}
function SubCategories_SelectGrid_Complete(data) {
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
                $("#lblPageNumber").html("Search Page No: <b class=\"fwt-text-red\">" + sDisplayRowCount + "</b> of <b class=\"fwt-text-red\">" + sRowCount + "</b>");
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

                var sCssClass = "background:#D5E990;", sCssClassIsActive = "", sCssClassSubCategory_Core = "", sCssClassSubCategory_Disp = "";
                var sCssClassCreatedBY = "", sCssClassCreatedDT = "", sCssClassSubCategory_Prefix = "", sCssClassOrderBy = "";
                var sOrderBy = "";
                if (sOrderBy == "IsActive") sCssClassIsActive = sCssClass;
                else if (sOrderBy == "SubCategory_Core") sCssClassSubCategory_Core = sCssClass;
                else if (sOrderBy == "SubCategory_Disp") sCssClassSubCategory_Disp = sCssClass;
                else if (sOrderBy == "OrderBy") sCssClassOrderBy = sCssClass;
                else if (sOrderBy == "SubCategory_Prefix") sCssClassSubCategory_Prefix = sCssClass;
                var lastindex = 0;
                var sIsActive = "";
                if (sCategoriesGridPageNumber == "0") {
                    $("#tblCategories thead").html("");
                    sRow += "<tr class='rowHead'><th  class='rowHeadColumn LinkHeader' style='width:10%;' align='center'>IsActive</th>" +
             "<th class='rowHeadColumn LinkHeader' align='center' style='width:40%;'>Display Text </th>" +
                            "<th class='rowHeadColumn LinkHeader' style='width:40%;' align='center'>Core Text</th>" +
                            "<th class='rowHeadColumn LinkHeader' align='center' style='width:10%;'>OrderBy</th></tr>";
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
                    sRow += "<tr class='rowHover'><td align='center' style='width:10%;'><button type='button' name='Button1'style='" + sCssClassIsActiveButton + "' onclick=\"javascript:SelectCategoriesButton('" + data.d[i].ID + "','" + data.d[i].IsActive + "');\">" + sIsActive + "</button></td>" +
                                  "<td  class='fwt-center Cursor_Point' align='center' style='" + sCssClassSubCategory_Disp + "' onclick=\"SelectSubCategoryDisp_Link('" + data.d[i].SubCategory_Core + "','" + data.d[i].SubCategory_Disp + "','" + data.d[i].OrderBy + "');\">" + data.d[i].SubCategory_Disp + "</td>" +
                                  "<td  class='fwt-center' style='" + sCssClassSubCategory_Core + "'>" + data.d[i].SubCategory_Core + "</td>" +
                                "<td class='fwt-center' align='center' style='" + sCssClassOrderBy + "'>" + data.d[i].OrderBy + "</td></tr>";
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
    catch (err) { alert("Error occured in  function(SubCategories_Select_Complete) " + err); }
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
        CallAjaxMethod("SubCategories_UpdateIsActive", jsonVar, "SubCategories_UpdateIsActive_Complete");
    }
    catch (err) { alert("Error occured in  function(SelectCategoriesButton) " + err); }
}
function SubCategories_UpdateIsActive_Complete(data) {
    try {
        var $Data = data.d;
        if ($Data.length > 0) {
            if ($Data[0].ERROR != "") {
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
    catch (err) { alert("Error occured in  function(SubCategories_UpdateIsActive_Complete) " + err); }
}