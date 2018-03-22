<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/site.master" AutoEventWireup="true" CodeFile="CheckOut.aspx.cs" Inherits="Admin_CheckIn_CheckOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    Check-Out
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <form class="form-horizontal" runat="server">
    <asp:ScriptManager runat="server" />
        <div class="col-lg-4">
            <div class="panel panel-midnightblue">
                <div class="panel-heading">
                    <h4>Main Information</h4>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <div class="form-group">
                            <label class="control-label col-lg-3">Customer Name</label>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txtCustName" runat="server" class="form-control" ReadOnly="true" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-lg-3">Status</label>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txtStatus" runat="server" class="form-control" ReadOnly="true" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-lg-3">Check-In Time</label>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txtChkIn" runat="server" class="form-control" ReadOnly="true" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-lg-3">Check-Out Time</label>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txtChkOut" runat="server" class="form-control" ReadOnly="true" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-lg-3">Membership Status</label>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txtMemStatus" runat="server" class="form-control" ReadOnly="true" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-lg-3">Subscription Status</label>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txtSubStatus" runat="server" class="form-control" ReadOnly="true" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-8">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="panel panel-midnightblue">
                        <div class="panel-heading">
                            Add Consumables
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-12">
                                <div id="errorProd" runat="server" class="alert alert-danger" visible="false">
                                    Product already exist.
                                </div>
                                <table class="table table-hover">
                                    <thead>
                                        <tr>
                                            <th>Product Name</th>
                                            <th>Selling Price</th>
                                            <th>Quantity</th>
                                            <th>Total Price</th>
                                            <th>Date Added</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:ListView ID="lvCheckOut" OnItemCommand="lvCheckOut_OnItemCommand" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("ProductName") %></td>
                                                    <asp:Literal ID="ltODID" runat="server" Text='<%# Eval("ODID") %>' Visible="false" />
                                                    <td>₱ <%# Eval("SellingPrice", "{0: #,###.00}") %></td>
                                                    <td><%# Eval("Qty") %></td>
                                                    <td>₱ <%# Eval("TotalPrice", "{0: #,###.00}") %>
                                                    <td><%# Eval("DateAdded", "{0: MMMM d, yyyy}") %></td>
                                                    <td>
                                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" Class="btn btn-success"
                                                            CommandName="delitem" ToolTip="Remove Item" UseSubmitBehavior="false"
                                                                    Visible='<%# _isCheckedOut %>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <tr>
                                                    <td colspan="10">
                                                        <h2 class="text-center">No Consumables Added.</h2>
                                                    </td>
                                                </tr>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="panel-footer">
                            <div class="pull-left">
                                <div class="form-group">
                                    <label class="control-label col-lg-3">Product</label>
                                    <div class="col-lg-6">
                                        <asp:DropDownList ID="ddlProduct" class="form-control" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">Quantity</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtQty" class="form-control"
                                                     TextMode="number" runat="server" />
                                    </div>
                                    <div class="col-lg-2">
                                        <asp:Button ID="btnAdd" class="btn btn-success" runat="server" Text="Add"
                                                    OnClick="btnAdd_OnClick" />
                                    </div>
                                </div>
                                <hr />
                                <div class="form-group">
                                    <label class="control-label col-lg-3">Customer Rate</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtRate" runat="server" class="form-control" disabled />
                                    </div>
                                    <label class="control-label col-lg-3">Workout Fee</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtAmntPaid" runat="server" class="form-control" TextMode="Number"
                                            AutoPostback="true" OnTextChanged="txtAmntPaid_OnTextChanged"
                                                     Text="0.00" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <h4>Consumable Total:
                                                <asp:Literal ID="ltConsumeTotal" runat="server" /></h4>
                                <h4>Total Amount:
                                                <asp:Literal ID="ltTotalAmnt" runat="server" /></h4>
                                <asp:HiddenField ID="hfTotalAmnt" runat="server" />
                            </div>
                            <div class="pull-right">
                                <asp:Button ID="btnCheckOut" runat="server" Visible="true" class="btn btn-success pull-right" Text="Check-Out"
                                    OnClientClick='return confirm("Are you sure?");' OnClick="btnCheckOut_OnClick" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</asp:Content>

