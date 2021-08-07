<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="Paybook.WebUI.Invoice.Create" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <div class="row bg-secondary">
            <div class="col-lg-5 col-xs-12">
                <h2><i class="fa fa-clock-o"></i>&nbsp; Invoice &nbsp; <span class="h4 text-secondary">INV/2019/002</span></h2>
            </div>
            <div class="col-lg-7 col-xs-12 text-right">
                <div class="btn-group" role="group">
                    <button type="button" clientidmode="Static" class="btn btn-primary fwt-btn-height" title="Add Service"
                        onclick="return OpenPartialPagePopup('invoice/service/create','ADD SERVICE');">
                        <i class="fa fa-plus"></i>&nbsp; ADD SERVICE</button>
                    <button type="button" clientidmode="Static" class="btn btn-primary fwt-btn-height" title="Add a Note"
                        onclick="location.href='/invoices'">
                        <i class="fa fa-close"></i>&nbsp; CANCEL</button>
                    <button type="submit" clientidmode="Static" class="btn btn-success fwt-btn-height" title="Save">
                        <i class="fa fa-check"></i>&nbsp; SAVE</button>
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid">
        <div class="row pt-3">
            <div class="col-lg-9 col-md-9 col-sm-12">
                <div class="row pt-3">
                    <div class="col-lg-4 col-md-4 col-sm-12">
                        <label for="ddlAgencies">Agencies</label>
                        <div style="position: relative;">
                            <asp:TextBox ID="txtAgencySearch" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" placeholder="Agency"
                                autocomplete="off"></asp:TextBox>
                            <div id="idAgencyCross" class="div-cross fwt-right"><i class="fa fa-times"></i></div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-12">
                        <label for="ddlClients">Clients</label>
                        <div style="position: relative;">
                            <asp:TextBox ID="txtSearchText" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" placeholder="Client"
                                autocomplete="off"></asp:TextBox>
                            <div id="idCross" class="div-cross fwt-right"><i class="fa fa-times"></i></div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-12">
                        <label for="txtClientEmail">Client's email</label>
                        <asp:TextBox ID="txtClientEmail" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" placeholder="Client's email"
                            autocomplete="off"></asp:TextBox>
                        <div class="pt-3">
                            <asp:CheckBox Text="&nbsp;Send email invoice" runat="server" />
                        </div>
                    </div>
                </div>
                <hr />
                <div class="row pt-5">
                    <div class="col-lg-4 col-md-4 col-sm-12">
                        <label for="txtBillingAddress">Billing address</label>
                        <asp:TextBox ID="txtBillingAddress" runat="server" ClientIDMode="Static" TextMode="MultiLine" CssClass="TextBoxNormal HeightMultiLine" placeholder="Billing address"
                            autocomplete="off"></asp:TextBox>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-12">
                        <label for="ddlTerms">Terms</label>
                        <asp:DropDownList ID="ddlTerms" runat="server" CssClass="TextBoxNormal">
                            <asp:ListItem Text="Net30" Value="30" Selected="True" />
                            <asp:ListItem Text="Net60" Value="60" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-12">
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-12">
                                <label for="txtInvoiceDate">Invoice date</label>
                                <asp:TextBox ID="txtInvoiceDate" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" placeholder="Invoice date"
                                    autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-12">
                                <label for="txtDueDate">Due date</label>
                                <asp:TextBox ID="txtDueDate" runat="server" ClientIDMode="Static" CssClass="TextBoxNormal" placeholder="Due date"
                                    autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-12 text-right">
                <div style="position: relative;">
                    <div style="position: absolute; right: 0; top: -5px;">Balance Due</div>
                    <h1 class="pt-5 bold text-success">₹ 3132.00</h1>
                </div>
            </div>
        </div>
        <hr />
        <div class="container-fluid fwt-white p-2">
            <table class="table table-bordered table-striped table-hover">
                <thead>
                    <tr style="height: 40px;">
                        <th class="text-right">#</th>
                        <th class="text-left">SERVICE</th>
                        <th class="text-left">DESCRIPTION</th>
                        <th class="text-right">QTY</th>
                        <th class="text-right">RATE</th>
                        <th class="text-right">AMOUNT</th>
                        <th class="text-right">TAX</th>
                        <th class="text-right">TOTAL</th>
                        <th class="text-right" style="width: 100px;">ACTION</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td class="text-right">1</td>
                        <td class="text-left">Design: Design</td>
                        <td class="text-left">Custom Design</td>
                        <td class="text-right">2</td>
                        <td class="text-right">75</td>
                        <td class="text-right">150.00</td>
                        <td class="text-right">STATE 18%</td>
                        <td class="text-right">174.00</td>
                        <td class="text-right"><i class="fa fa-pencil-square fa-2x"></i>&nbsp;<i class="fa fa-trash fa-2x"></i></td>
                    </tr>
                    <tr>
                        <td class="text-right">2</td>
                        <td class="text-left">Development: Create Page</td>
                        <td class="text-left">Custom Page Development</td>
                        <td class="text-right">12</td>
                        <td class="text-right">100</td>
                        <td class="text-right">1200.00</td>
                        <td class="text-right">CENTER 18%</td>
                        <td class="text-right">1392.00</td>
                        <td class="text-right"><i class="fa fa-pencil-square fa-2x"></i>&nbsp;<i class="fa fa-trash fa-2x"></i></td>
                    </tr>
                    <tr>
                        <td class="text-right">3</td>
                        <td class="text-left">Design: Design</td>
                        <td class="text-left">Custom Design</td>
                        <td class="text-right">2</td>
                        <td class="text-right">75</td>
                        <td class="text-right">150.00</td>
                        <td class="text-right">CENTER 18%</td>
                        <td class="text-right">174.00</td>
                        <td class="text-right"><i class="fa fa-pencil-square fa-2x"></i>&nbsp;<i class="fa fa-trash fa-2x"></i></td>
                    </tr>
                    <tr>
                        <td class="text-right">4</td>
                        <td class="text-left">Development: Create Page</td>
                        <td class="text-left">Custom Page Development</td>
                        <td class="text-right">12</td>
                        <td class="text-right">100</td>
                        <td class="text-right">1200.00</td>
                        <td class="text-right">STATE 18%</td>
                        <td class="text-right">1392.00</td>
                        <td class="text-right"><i class="fa fa-pencil-square fa-2x"></i>&nbsp;<i class="fa fa-trash fa-2x"></i></td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="row pt-5">
            <div class="col-lg-4 col-md-12 col-sm-12">
                <button type="button" class="btn btn-sm btn-primary" onclick="return false;"><i class="fa fa-plus"></i>&nbsp;ADD SERVICE</button>
                <button type="button" class="btn btn-sm btn-danger" onclick="return false;"><i class="fa fa-trash-o"></i>&nbsp;CLEAR ALL SERVICES</button>
                <hr />
                <label for="txtInvoiceMessage">Message on invoice</label>
                <asp:TextBox ID="txtInvoiceMessage" runat="server" ClientIDMode="Static" TextMode="MultiLine" CssClass="TextBoxNormal HeightMultiLine" Text="Thank you for your business and have a great day!"
                    autocomplete="off"></asp:TextBox>
            </div>
            <div class="col-lg-offset-2 col-lg-6 col-md-12 col-sm-12">
                <div class="row">
                    <div class="col-lg-8 text-right">
                        <label for="txtSubtotal" class="fwt-large">Subtotal :</label>
                    </div>
                    <div class="col-lg-4 text-right">
                        <label class="fwt-large">₹ 3132.00</label>
                    </div>
                </div>
                <div class="row pt-5">
                    <div class="col-lg-8 text-right">
                        <label for="txtSubtotal" class="fwt-medium">Taxable total : ₹ 432.00</label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-offset-3 col-lg-3 text-right">
                        <asp:DropDownList ID="ddlDiscountType" runat="server" CssClass="TextBoxNormal">
                            <asp:ListItem Text="Discount Percentage" />
                            <asp:ListItem Text="Discount Amount" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-2 text-right">
                        <asp:TextBox ID="txtDiscountValue" runat="server" CssClass="TextBoxNormal text-right" Text="0"></asp:TextBox>
                    </div>
                    <div class="col-lg-4 text-right">
                        <label class="fwt-large">₹ 0.00</label>
                    </div>
                </div>
                <div class="row pt-5">
                    <div class="col-lg-8 text-right">
                        <label for="txtSubtotal" class="fwt-large">Total :</label>
                    </div>
                    <div class="col-lg-4 text-right">
                        <label class="fwt-large">₹ 3132.00</label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
