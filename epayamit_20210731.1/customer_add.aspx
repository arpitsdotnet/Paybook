<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="customer_add.aspx.cs" Inherits="Paybook.WebUI.customer_add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfCustomer_ID" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfIsActive" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfLogInUser" runat="server" ClientIDMode="Static" />
    <div class="fwt-container">
      <h4>
            <asp:Label ID="lblPageHeading" runat="server" ClientIDMode="Static"></asp:Label></h4>
        <div id="idLabelError" runat="server" clientidmode="Static" class="fwt-container fwt-padding-16 fwt-pale-yellow fwt-border fwt-border-yellow"
            style="display: none">
        </div>
        <div class="fwt-padding-8">
            <div class="fwt-col l6 m6 s6">
          Customer ID: <asp:Label ID="lblCustomer_ID" runat="server" ClientIDMode="Static" CssClass="IdLabel"></asp:Label></div>
        <div class="fwt-col l6 m6 s6 fwt-right-align">
            <button runat="server" id="btnBack" class="fwt-btn fwt-hover-indigo" onserverclick="btnBack_ServerClick"
                title="Add Customer" style="background-color: #3a4f63;">> Back</button>
            </div>
             <div class="fwt-clear">
        </div>
        </div>
        <div id="idCustomers" class="fwt-padding-4">         
            <fieldset>
                <legend></legend>
                <div class="fwt-padding-4">
                    <div class="fwt-col l4 m4 s12">
                        Customer Type:
                        <asp:RadioButtonList ID="rbtnCustomer_Type" runat="server" CssClass="RadioButtonNormal"
                            ClientIDMode="Static" RepeatDirection="Horizontal">
                            <asp:ListItem Value="Agency" Selected="True"> Agency</asp:ListItem>
                            <asp:ListItem Value="Firm"> Firm</asp:ListItem>
                            <asp:ListItem Value="Customer"> Customer</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="fwt-col l4 m4 s12">
                        <span>Agency: </span>
                        <asp:DropDownList ID="ddlAgencyName" runat="server" CssClass="DropdownNormal"
                            ClientIDMode="Static">
                        </asp:DropDownList>
                    </div>
                    <div class="fwt-col l4 m4 s12">
                        <span>Prefix: </span>
                        <asp:DropDownList ID="ddlCustomerPrefix" runat="server" CssClass="DropdownNormal"
                            ClientIDMode="Static">
                        </asp:DropDownList>
                    </div>
                    <div class="fwt-clear">
                    </div>
                </div>
                <div class="fwt-padding-4">
                    <div class="fwt-col l4 m4 s12">
                        <div>First Name:<span style="color: Red; font-size: small">*</span> </div>
                        <div>
                            <asp:TextBox ID="txtCustomerFirstName" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                                autocomplete="off" Text=""></asp:TextBox>
                        </div>
                    </div>
                    <div class="fwt-col l4 m4 s12">
                        <div>Middle Name: </div>
                        <div>
                            <asp:TextBox ID="txtCustomerMiddleName" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                                autocomplete="off" Text=""></asp:TextBox>
                        </div>
                    </div>
                    <div class="fwt-col l4 m4 s12">
                        <div>Last Name: </div>
                        <div>
                            <asp:TextBox ID="txtCustomerLastName" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                                autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="fwt-clear">
                    </div>
                </div>
            </fieldset>
            <div class="fwt-padding-12">
            </div>
            <fieldset>
                <legend>Contact Details:</legend>
                <div class="fwt-padding-4">
                    <div class="fwt-col l4 m4 s12">
                        <div>Mobile Number:<span style="color: Red; font-size: small">*</span></div>
                        <div>
                            <asp:TextBox ID="txtCustomerPhoneNumber1" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                                autocomplete="off" Text=""></asp:TextBox>
                        </div>
                    </div>
                    <div class="fwt-col l4 m4 s12">
                        <div>Phone Number:</div>
                        <div>
                            <asp:TextBox ID="txtCustomerPhoneNumber2" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                                autocomplete="off" Text=""></asp:TextBox>
                        </div>
                    </div>
                    <div class="fwt-col l4 m4 s12">
                        <div>Email:</div>
                        <div>
                            <asp:TextBox ID="txtCustomerEmail" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                                autocomplete="off" Text=""></asp:TextBox>
                        </div>
                    </div>
                    <div class="fwt-clear">
                    </div>
                </div>
            </fieldset>
            <div class="fwt-padding-12">
            </div>
            <fieldset>
                <legend>Address Details:</legend>
                <div class="fwt-padding-4">
                    <div class="fwt-col l6 m6 s12">
                        <div>Address1:</div>
                        <div>
                            <asp:TextBox ID="txtCustomerAddress1" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                                autocomplete="off" Text=""></asp:TextBox>
                        </div>
                    </div>
                    <div class="fwt-col l6 m6 s12">
                        <div>Address2:</div>
                        <div>
                            <asp:TextBox ID="txtCustomerAddress2" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                                autocomplete="off" Text=""></asp:TextBox>
                        </div>
                    </div>
                    <div class="fwt-clear">
                    </div>
                </div>
                <div class="fwt-padding-4">
                    <div class="fwt-col l4 m4 s12">
                        <div>City:</div>
                        <div>
                            <asp:TextBox ID="txtCustomerCity" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                                autocomplete="off" Text=""></asp:TextBox>
                        </div>
                    </div>
                    <div class="fwt-col l4 m4 s12">
                        <div><span>State: </span></div>
                        <div>
                            <asp:DropDownList ID="ddlCustomerState" runat="server" CssClass="DropdownNormal"
                                ClientIDMode="Static">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="fwt-col l4 m4 s12">
                        Country:
                        <asp:TextBox ID="txtCustomerCountry" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                            autocomplete="off" Text="India" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="fwt-clear">
                    </div>
                </div>
            </fieldset>
            <div class="fwt-padding-12">
            </div>
            <fieldset>
                <legend>Personal Details:</legend>
                <div class="fwt-padding-4">
                    <div class="fwt-col l4 m4 s12">
                        Date Of Birth:
                        <asp:TextBox ID="txtCustomerDOB" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                            autocomplete="off" Text=""></asp:TextBox>
                    </div>
                    <div class="fwt-col l8 m8 s12">
                        Gender:
                        <asp:RadioButtonList ID="rbtnGender" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal" CssClass="RadioButtonNormal">
                            <asp:ListItem Value="Male" Selected="True"> Male</asp:ListItem>
                            <asp:ListItem Value="Female"> Female</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
            </fieldset>
            <div class="fwt-padding-4">
                <!-- for image -->
                <img id="imgCustomerImageFileName" runat="server" src="" alt="" />
            </div>
            <div class="fwt-clear">
            </div>
            <%--      <div id="idBottomPanel" class="fwt-pale-yellow fwt-text-dark-grey fwt-padding-4 fwt-normal">
                <div class="fwt-col l2 m2 s12">
                    <div class="fwt-bold">
                        Is Active:
                    </div>
                    <div id="idActive" class="fwt-pointer fwt-hover-indigo fwt-container " onclick="ShowDialog_Reason()">
                    </div>
                </div>
                <div class="fwt-col l2 m2 s12">
                    <div class="fwt-bold">
                        Created Date:
                    </div>
                    <div id="idCreatedDT">
                    </div>
                </div>
                <div class="fwt-col l2 m2 s12">
                    <div class="fwt-bold">
                        CreatedBY:
                    </div>
                    <div id="idCreatedBY">
                    </div>
                </div>
                <div class="fwt-col l2 m2 s12">
                    <div class="fwt-bold">
                        Modified Date:
                    </div>
                    <div id="idModifiedDT">
                    </div>
                </div>
                <div class="fwt-col l2 m2 s12">
                    <div class="fwt-bold">
                        ModifiedBY:
                    </div>
                    <div id="idModifiedBY">
                    </div>
                </div>
                <div class="fwt-col l2 m2 s12">
                    &nbsp;
                    <div id="Div2">
                        &nbsp;
                    </div>
                </div>
                <div class="fwt-clear">
                </div>
            </div>--%>
            <div class="fwt-padding-12 fwt-right ">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ClientIDMode="Static" CssClass="fwt-btn fwt-green fwt-hover-indigo" OnClick="btnSubmit_Click" />
            </div>
            <div class="fwt-clear">
            </div>
        </div>
        <div id="idDialog_Reason" style="display: none;">
            <div class="fwt-padding-4">
                <div id="idDialogReason_Message">
                </div>
                <div class="fwt-padding-4">
                    <asp:TextBox ID="txtReason" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                        autocomplete="off" Text=""></asp:TextBox>
                </div>
                <div class="fwt-right fwt-padding-4">
                    <asp:Button ID="btnSubmitIsActive" runat="server" Text="Submit" ClientIDMode="Static" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">

        $(document).ready(function () {
            var pathname = window.location.pathname.split("/");
            var filename = pathname[pathname.length - 1];
            if (filename == "customer") {
                $("#txtCustomerDOB").datepicker({ dateFormat: 'dd-M-yy' }).datepicker("setDate", new Date()).datepicker("option", "maxDate", '+0m +0w');;
            }
            else
                $("#txtCustomerDOB").datepicker({ dateFormat: 'dd-M-yy' }).datepicker("option", "maxDate", '+0m +0w');;
            // $("#idDialogReason_Message").html(__Messages.DialogReason_Message);
            var sCustomer_Type = $("#rbtnCustomer_Type input:checked").val();
            if (sCustomer_Type == 'Customer') {
                $("#ddlAgencyName").prop('disabled', true);
            }
            else
                $("#ddlAgencyName").prop('disabled', false);

            $("#rbtnCustomer_Type").change(function () {
                try {
                    var sSelectedValue = $(this).find('input[type="radio"]:checked');
                    if (sSelectedValue.val() == 'Customer') {
                        $("#ddlAgencyName").val(0);
                        $("#ddlAgencyName").prop('disabled', true);
                    }
                    else
                        $("#ddlAgencyName").prop('disabled', false);
                }
                catch (err) {
                    ShowMessage(err);
                }
            });          
            $("#txtCustomerPhoneNumber1").change(function () {
                try {                  
                        if (__Messages._mob.test($.trim($("#txtCustomerPhoneNumber1").val())) == false) {                         
                            ReadXMLMessage("BSW010", "ReadXMLMessage_Complete");
                        }                    
                }
                catch (err) {
                    ShowMessage(err);
                }
            });          
           
        });
        function ReadXMLMessage_Complete(data) {
            try {
                ShowMessage(data);
            }
            catch (err) {
                ShowMessage("There is an issue calling the function (SetDefault)" + err);
            }
        }      
    </script>
</asp:Content>
