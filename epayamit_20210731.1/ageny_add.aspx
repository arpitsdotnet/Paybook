<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ageny_add.aspx.cs" Inherits="Paybook.WebUI.ageny_add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfAgency_ID" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfLogInUser" runat="server" ClientIDMode="Static" />
    <div class="fwt-container">
        <h4>
            <asp:Label ID="lblPageHeading" runat="server" ClientIDMode="Static"></asp:Label></h4>
        <div id="idLabelError" runat="server" clientidmode="Static" class="fwt-container fwt-padding-16 fwt-pale-yellow fwt-border fwt-border-yellow"
            style="display: none">
        </div>
        <div class="fwt-padding-8">
            <div class="fwt-col l6 m6 s6">
                Agency/Firm ID:
            <asp:Label ID="lblAgency_ID" runat="server" ClientIDMode="Static" CssClass="IdLabel"></asp:Label>
            </div>
            <div class="fwt-col l6 m6 s6 fwt-right-align">
                <button runat="server" id="btnBack" class="fwt-btn fwt-hover-indigo" onserverclick="btnBack_ServerClick"
                    title="Back" style="background-color: #3a4f63;">
                    > Back
                </button>
                <button runat="server" id="btnCustomerCreate" class="fwt-btn fwt-hover-indigo" onserverclick="btnCustomerCreate_ServerClick"
                    title="Add Customer" style="background-color: #3a4f63;">
                    > Add Customer
                </button>
            </div>
            <div class="fwt-clear">
            </div>
        </div>

        <div id="idCustomers" class="fwt-padding-4">
            <fieldset>
                <legend></legend>
                <div class="fwt-padding-4">
                    <div class="fwt-col l4 m4 s12">
                        <div>Agency/Firm Name:<span style="color: Red; font-size: small">*</span> </div>
                        <div>
                            <asp:TextBox ID="txtAgencyName" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
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
                <legend>Contact Details:</legend>
                <div class="fwt-padding-4">
                    <div class="fwt-col l4 m4 s12">
                        <div>Phone Number1:<span style="color: Red; font-size: small">*</span></div>
                        <div>
                            <asp:TextBox ID="txtAgencyPhoneNumber1" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                                autocomplete="off" Text=""></asp:TextBox>
                        </div>
                    </div>
                    <div class="fwt-col l4 m4 s12">
                        <div>Phone Number2:</div>
                        <div>
                            <asp:TextBox ID="txtAgencyPhoneNumber2" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                                autocomplete="off" Text=""></asp:TextBox>
                        </div>
                    </div>
                    <div class="fwt-col l4 m4 s12">
                        <div>Email:</div>
                        <div>
                            <asp:TextBox ID="txtAgencyEmail" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
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
                            <asp:TextBox ID="txtAgencyAddress1" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                                autocomplete="off" Text=""></asp:TextBox>
                        </div>
                    </div>
                    <div class="fwt-col l6 m6 s12">
                        <div>Address2:</div>
                        <div>
                            <asp:TextBox ID="txtAgencyAddress2" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
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
                            <asp:TextBox ID="txtAgencyCity" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                                autocomplete="off" Text=""></asp:TextBox>
                        </div>
                    </div>
                    <div class="fwt-col l4 m4 s12">
                        <div><span>State: </span></div>
                        <div>
                            <asp:DropDownList ID="ddlAgencyState" runat="server" CssClass="DropdownNormal"
                                ClientIDMode="Static">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="fwt-col l4 m4 s12">
                        Country:
                        <asp:TextBox ID="txtAgencyCountry" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal"
                            autocomplete="off" Text="India" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="fwt-clear">
                    </div>
                </div>
            </fieldset>
            <div class="fwt-padding-12">
            </div>
            <div class="fwt-clear">
            </div>
            <div class="fwt-padding-12 fwt-right ">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ClientIDMode="Static" CssClass="fwt-btn fwt-green fwt-hover-indigo" OnClick="btnSubmit_Click" />
            </div>
            <div class="fwt-clear">
            </div>
        </div>
    </div>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#txtAgencyPhoneNumber1").change(function () {
                try {

                    if (__Messages._mob.test($.trim($("#txtAgencyPhoneNumber1").val())) == false) {
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

