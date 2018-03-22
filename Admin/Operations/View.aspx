<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/site.master" AutoEventWireup="true" CodeFile="View.aspx.cs" Inherits="Admin_Operations_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <i class="fa fa-list"></i> View List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <form class="form-horizontal" runat="server">
    <asp:ScriptManager runat="server" />
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="col-lg-12">
                    <div class="panel panel-midnightblue">
                        <div class="panel-heading">
                            Check In/Out
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtSearchCust" runat="server" class="form-control"
                                                     placeholder="Search Customer" AutoPostback="True"
                                                     OnTextChanged="txtSearchCust_OnTextChanged" />
                                        <span class="input-group-btn">
                                            <asp:LinkButton ID="btnSearchCust" runat="server" class="btn btn-info"
                                                            OnClick="btnSearchCust_OnClick">
                                                <i class="fa fa-search"></i>
                                            </asp:LinkButton>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <table class="table table-striped table-hover">
                                <thead>
                                    <th>Customer Name</th>
                                    <th>Status</th>
                                    <th>Check-In Time</th>
                                    <th>Check-Out Time</th>
                                    <th>Membership Status</th>
                                    <th>Subscription Status</th>
                                    <th>Check-Out Customer</th>
                                </thead>
                                <tbody>
                                    <asp:ListView ID="lvOperations" runat="server"
                                        OnPagePropertiesChanging="lvOperations_OnPagePropertiesChanging"
                                        OnDataBound="lvOperations_OnDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Eval("LastName") %>, <%# Eval("FirstName") %>
                                                </td>
                                                <td><%# Eval("CheckOut").ToString() != "" ? "Checked-Out" : "Checked-In" %></td>
                                                <td><%# Eval("CheckIn") %></td>
                                                <td><%# Eval("CheckOut") %></td>
                                                <td>
                                                    <span class='<%# Eval("MemStatus").ToString() == "Inactive" ? "label label-danger" : "label label-success"%>'>
                                                        <%# Eval("MemStatus").ToString() == "Inactive" ? "Inactive" : "Active"%>
                                                    </span>
                                                </td>
                                                <td>
                                                    <span class='<%# Eval("SubStatus").ToString() == "Inactive" ? "label label-danger" : "label label-success"%>'>
                                                        <%# Eval("SubStatus").ToString() == "Inactive" ? "No Active Subscriptions" 
                                                                 : Eval("SubStart", "{0: MMMM d, yyyy}") + " - " + Eval("SubEnd", "{0: MMMM d, yyyy}") %>
                                                    </span>
                                                </td>
                                                <td>
                                                    <a href='<%= Page.ResolveUrl("~/Admin/CheckIn/CheckOut.aspx?ID=") %><%# Eval("OperationID") %>&&UserID=<%# Eval("UserID") %>'>
                                                        <i class="fas fa-sign-out-alt"></i>
                                                     </a>
                                                    
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <tr>
                                                <td colspan="12">
                                                    <h2 class="text-center">No Check-In's for today</h2>
                                                </td>
                                            </tr>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                </tbody>
                            </table>
                        </div>
                        <div class="panel-footer">
                            <center>
                                        <asp:DataPager id="dpOperations" runat="server" pageSize="10" PagedControlID="lvOperations">
                                            <Fields>
                                                <asp:NumericPagerField Buttontype="Button"
                                                    NumericButtonCssClass="btn btn-default"
                                                    CurrentPageLabelCssClass="btn btn-success"
                                                    NextPreviousButtonCssClass ="btn btn-default" 
                                                    ButtonCount="10" />
                                            </Fields>
                                        </asp:DataPager>
                                    </center>
                        </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</asp:Content>

