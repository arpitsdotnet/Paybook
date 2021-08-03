<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="categories.aspx.cs" Inherits="Paybook.WebUI.categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%=Application["Path"] %>_Layouts/JS/custom_category.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfCategoryPrefix" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfCategoriesGridPageNumber" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfsIsActive" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfLogInUser" runat="server" ClientIDMode="Static" />
    <div class="fwt-container">
      <h4>Add Category
        </h4>
        <div id="idLabelError" runat="server" clientidmode="Static" class="fwt-container fwt-padding-16 fwt-pale-yellow fwt-border fwt-border-yellow"
            style="display: none">
        </div>
        <div class="fwt-padding-4">
            <div class="fwt-col l4 m4 s12 ">
                <span>Select Category:</span>
                <asp:DropDownList ID="ddlCategories" runat="server" ClientIDMode="Static" CssClass="DropdownNormal">
                </asp:DropDownList>

            </div>
            <div class="fwt-col l8 m8 s12">
            </div>
            <div class="fwt-clear">
            </div>
        </div>
        <div class="fwt-padding-4">
            <div class="fwt-col l4 m4 s12" style='position: relative'>
                <div>Display Text:<span style="color: Red; font-size: small">*</span></div>
                <div>
                    <asp:TextBox ID="txtCategoryDisplayName" runat="server" ClientIDMode="Static" autocomplete="off"
                        CssClass="TextBoxNormal"></asp:TextBox><div>
                        </div>
                </div>
                <div class="ui-tooltip fwt-round-large fwt-red fwt-small" id="idDisplayNameMessage">
                </div>
            </div>
            <div class="fwt-col l4 m4 s12">
                <div><span>Core Text:</span></div>
                <div>
                    <asp:TextBox ID="txtCategoryCoreName" runat="server" ClientIDMode="Static" autocomplete="off"
                        CssClass="TextBoxNormal"></asp:TextBox>
                </div>
            </div>
            <div class="fwt-col l4 m4 s12" style='position: relative'>
                <div>Order BY :<span style="color: Red; font-size: small">*</span></div>
                <div>
                    <asp:TextBox ID="txtOrderBY" runat="server" ClientIDMode="Static" autocomplete="off"
                        CssClass="TextBoxNormal"></asp:TextBox>
                </div>
                <div class="ui-tooltip fwt-round-large fwt-red fwt-small" id="idOrderBYMessage">
                </div>
            </div>
            <div class="fwt-clear">
            </div>
        </div>
        <div class="fwt-right fwt-padding-4">
            <asp:Button ID="btnSubmitAndUpdate" runat="server" CssClass="fwt-btn fwt-green fwt-hover-indigo"
                Text="Submit" ClientIDMode="Static" />
            <asp:Button ID="btnclear" runat="server" CssClass="fwt-btn fwt-green fwt-hover-indigo"
                Text="Clear" ClientIDMode="Static" />
            <div class="fwt-clear">
            </div>
        </div>
        <div class="fwt-clear">
        </div>
        <div class="fwt-padding-4 " id="idPageNumber">
            <div class="fwt-col l2 m2 s12">
                <asp:Label ID="lblPageNumber" ClientIDMode="Static" runat="server"></asp:Label>

            </div>
            <div class="fwt-col l10 m10 s6 fwt-NavigationButtons">
                <button id="btnPrivious" clientidmode="Static" class="fwt-btn fwt-ripple fwt-dropdown-click fwt-amber">
                    <i class="fa fa-backward"></i>&nbsp;</button>
                <button id="btnNext" clientidmode="Static" class="fwt-btn fwt-ripple fwt-dropdown-click fwt-amber">
                    <i class="fa fa-forward"></i>&nbsp;</button>
            </div>

            <div class="fwt-clear">
            </div>

        </div>
        <div class="fwt-padding-4">
            <div id="div_HeadertblCategories">
            </div>
            <div class="table-Scroll" id="div_ScrolltblCategories">
             <table id="tblCategories" border="0" width="100%" style="word-wrap: break-word;" class="table-main fwt-table">
                    <thead>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
                <div id="divGridLoadingtblCategories" style="text-align: center;">
                </div>
                <asp:Label ID="lblGridMessagetblCategories" runat="server" CssClass="Error" ClientIDMode="Static"></asp:Label>
            </div>
        </div>

        <div class="fwt-clear">
        </div>
    </div>
    <script type="text/jscript">
        $(document).ready(function () {

            $("#txtCategoryCoreName").attr('disabled', 'disabled');
            $("#ddlCategories").select2();
            $("#idPageNumber").hide();
               $('#btnNext').click(function (e) {
                try {
                    var iTotalPageNumber = parseInt($("#lblPageNumber").text().split('of')[1]);
                    var iCurrentPageNumber = parseInt($('#hfCategoriesGridPageNumber').val());                    
                    if (iCurrentPageNumber >= 0 && iCurrentPageNumber < iTotalPageNumber - 1) {
                        if (IsProcessingHomeGrid)
                            return false;
                        else {
                            IsProcessingHomeGrid = true;
                            var iCategoryGridPageNumber = $('#hfCategoriesGridPageNumber').val() == "" ? 0 : parseInt($('#hfCategoriesGridPageNumber').val());
                            iCategoryGridPageNumber = iCategoryGridPageNumber + 1;

                            $('#hfCategoriesGridPageNumber').val(iCategoryGridPageNumber);
                            GetAllSubCategories(sOrderBy);
                        }
                    }
                }  
                catch (err) {
                    ShowMessage(err);
                }
                e.preventDefault();

            });
            $('#btnPrivious').click(function (e) {
                try {
                    if ($('#hfCategoriesGridPageNumber').val() > 0) {
                        if (IsProcessingHomeGrid)
                            return false;
                        else {
                            IsProcessingHomeGrid = true;
                            var iCategoryGridPageNumber = parseInt($('#hfCategoriesGridPageNumber').val());
                            iCategoryGridPageNumber = iCategoryGridPageNumber - 1;
                            if (iCategoryGridPageNumber == 0)
                                $("#tblCategories thead").html("");
                            $('#hfCategoriesGridPageNumber').val(iCategoryGridPageNumber);
                            GetAllSubCategories(sOrderBy);
                        }
                    }
                }
                catch (err) {
                    ShowMessage(err);
                }
                e.preventDefault();

            });
           
            $("#ddlCategories").change(function () {
                try {
                    $('#hfCategoriesGridPageNumber').val("");
                    $("#txtCategoryDisplayName,#txtOrderBY,#txtCategoryCoreName").val("");
                    $("#btnSubmitAndUpdate").val("Submit");
                    GetAllSubCategories();

                }
                catch (err) {
                    ShowMessage("ERROR", err);
                }
            });
             $("#txtCategoryDisplayName").focusout(function () {
                try {
                    if (($("#txtCategoryDisplayName").val() != "") && ($("#btnSubmitAndUpdate").val() != "Update")) {
                        var sCategoryDisplayName = $("#txtCategoryDisplayName").val().replace(/ /g, "_");
                        var sCategory_Core = ($("#hfCategoryPrefix").val() + "_" + sCategoryDisplayName).toUpperCase();
                        $("#txtCategoryCoreName").val(sCategory_Core);                       
                    }
                }
                catch (err) {
                    ShowMessage("ERROR", err);
                }
            });
             $("#btnSubmitAndUpdate").click(function (e) {
                try {
                    if (Validation()) {                       
                        
                        if ($("#btnSubmitAndUpdate").val() == "Submit") {
                            var sJsonVar = { "sSubCategory_Core": $("#txtCategoryCoreName").val(), "sCategory_Core": $('#ddlCategories').val() };
                            CallAjaxMethod("SubCategory_IsExist", sJsonVar, "SubCategory_IsExist_Complete");
                        }
                        else {
                            var jsonVar = {
                                "sCreatedBY": $("#hfLogInUser").val(), "sCategory_Core": $("#ddlCategories").val(),
                                "sCategory_Disp": $("#ddlCategories option:selected").text(),
                                "sSubCategory_Core": $("#txtCategoryCoreName").val(),
                                "sSubCategory_Disp": $("#txtCategoryDisplayName").val(),
                                "sSubCategory_Prefix": $("#hfCategoryPrefix").val(),
                                "sOrderBy": $("#txtOrderBY").val(), "ButtonText": $("#btnSubmitAndUpdate").val()
                            };                         
                            CallAjaxMethod("SubCategories_Update", jsonVar, "SubCategories_Update_Complete");
                        }
                    }
                }
                catch (err) {
                    ShowMessage("ERROR", err);
                }
                e.preventDefault();
            });

            $("#btnclear").click(function (e) {
                try {
                    SetDefault();
                    $("#ddlCategories").val("0").trigger('change');
                    if ($("#hfCategoryPrefix").val() == "") {
                        $("#txtCategoryDisplayName,#txtOrderBY,#txtCategoryCoreName").attr('disabled', true);
                    }
                    else {
                        $("#btnSubmitAndUpdate").val("Submit");
                    }

                }
                catch (err) {
                    ShowMessage("ERROR", err);
                }
                e.preventDefault();
            });

        });
        function SetDefault() {
            try {
                $("#txtCategoryDisplayName,#txtOrderBY,#txtCategoryCoreName").val("");
            }
            catch (err) {
                ShowMessage("ERROR", err);
            }

        }
        function Validation() {
            try {
                if ($("#ddlCategories").val() == 0) {
                    ReadXMLMessage("CAW803", "ReadXMLMessage_Complete");
                    return false;
                }
                else if ($("#txtCategoryDisplayName").val() == "") {
                    ReadXMLMessage("CAW804", "ReadXMLMessage_Complete");
                    return false;
                }
                else if ($("#txtOrderBY").val() == "") {
                    ReadXMLMessage("CAW802", "ReadXMLMessage_Complete");
                    return false;
                }
                else
                    return true;
            }
            catch (err) {
                ShowMessage(err);
            }
        }

         function SubCategory_IsExist_Complete(data) {
            try {
                $Data = data.d;
                if ($Data.length > 0) {
                    if ($Data[0].ERROR != "") {
                        ShowMessage($Data[0].ERROR);
                    }
                    else {                       
                        var sSubCategoryCount = $Data[0].SubCategoryCount;                       
                        if (sSubCategoryCount != "0") { 
                            ReadXMLMessage("CAW801", "ReadXMLMessage_Complete");
                        }
                        else
                        {
                            var jsonVar = {
                                "sCreatedBY": $("#hfLogInUser").val(), "sCategory_Core": $("#ddlCategories").val(),
                                "sCategory_Disp": $("#ddlCategories option:selected").text(),
                                "sSubCategory_Core": $("#txtCategoryCoreName").val(),
                                "sSubCategory_Disp": $("#txtCategoryDisplayName").val(),
                                "sSubCategory_Prefix": $("#hfCategoryPrefix").val(),
                                "sOrderBy": $("#txtOrderBY").val(), "ButtonText": $("#btnSubmitAndUpdate").val()
                            };
                            CallAjaxMethod("SubCategories_Insert", jsonVar, "SubCategories_Insert_Complete");
                        }
                    }
                }
            }
            catch (err) {
                ShowMessage(err);
            }
        }
        function SubCategories_Insert_Complete(data) {
            try {
                $Data = data.d;
                if ($Data.length > 0) {
                    if ($Data[0].ERROR != "") {
                        ShowMessage($Data[0].ERROR);
                    }
                    else {
                        //SetCategoriesGridPageNumberBlank();
                        GetAllSubCategories();
                        SetDefault();
                        $("#ddlCategories").val("0").trigger('change');
                        ShowMessage($Data[0].Message);
                    }
                }
            }
            catch (err) {
                ShowMessage(err);
            }
        }
        function SubCategories_Update_Complete(data) {
            try {
                $Data = data.d;
                if ($Data.length > 0) {
                    if ($Data[0].ERROR != "") {
                        ShowMessage($Data[0].ERROR);
                    }
                    else {
                        //SetCategoriesGridPageNumberBlank();
                        GetAllSubCategories();
                        SetDefault();
                        ShowMessage($Data[0].Message);
                    }
                }
            }
            catch (err) {
                ShowMessage(err);
            }
        }
        function ReadXMLMessage_Complete(data) {
            try {
                ShowMessage(data);
            }
            catch (err) {
                ShowMessage("ERROR", "There is an issue calling the function (SetDefault)" + err);
            }
        }
    </script>
</asp:Content>
