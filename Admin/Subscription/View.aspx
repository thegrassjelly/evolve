<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/site.master" AutoEventWireup="true" CodeFile="View.aspx.cs" Inherits="Admin_Subscription_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <i class="fa fa-list"></i> Subscription List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <form class="form-horizontal" runat="server">
        <asp:ScriptManager runat="server" />
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="col-lg-12">
                    <div class="panel panel-midnightblue">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-1">
                                    <div class="input-group">
                                        <asp:DropDownList ID="ddlRate" runat="server" class="form-control"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlRate_OnSelectedIndexChanged">
                                            <asp:ListItem Text="All Users" />
                                            <asp:ListItem Text="Regular" Value="Regular" />
                                            <asp:ListItem Text="Student" Value="Student" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-1">
                                    <div class="input-group">
                                        <asp:DropDownList ID="ddlStatus" runat="server" class="form-control"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_OnSelectedIndexChanged">
                                            <asp:ListItem Text="Active" />
                                            <asp:ListItem Text="Inactive" Value="Inactive" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-10">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtSearch" runat="server" class="form-control autosuggest"
                                            placeholder="Keyword..." OnTextChanged="txtSearch_OnTextChanged" AutoPostBack="true" />
                                        <span class="input-group-btn">
                                            <asp:LinkButton ID="btnSearch" runat="server" class="btn btn-info"
                                                OnClick="btnSearch_OnClick">
                                      <i class="fa fa-search"></i>
                                            </asp:LinkButton>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <table class="table table-striped table-hover">
                                <thead>
                                    <th>#</th>
                                    <th>Name</th>
                                    <th>Subscription Start</th>
                                    <th>Subscription End</th>
                                    <th>Subscription Length</th>
                                    <th>Subscription Type</th>
                                    <th>Status</th>
                                    <th>OR No.</th>
                                    <th>Payment Date</th>
                                </thead>
                                <tbody>
                                    <asp:ListView ID="lvSubscriptions" runat="server"
                                        OnPagePropertiesChanging="lvSubscriptions_OnPagePropertiesChanging"
                                        OnDataBound="lvSubscriptions_OnDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("SubID") %></td>
                                                <td><%# Eval("LastName") %>, <%# Eval("FirstName") %> </td>
                                                <td><%# Eval("SubStart", "{0: MMMM d, yyyy}") %></td>
                                                <td><%# Eval("SubEnd", "{0: MMMM d, yyyy}") %></td>
                                                <td>
                                                    <%# Eval("SubSpan").ToString() == "12" ? Eval("SubSpan") + " Year" : Eval("SubSpan") + " Month(s)" %>
                                                </td> 
                                                <td><span class="label label-primary"><%# Eval("SubType") %></span></td>
                                                <td>
                                                    <span class='<%# Convert.ToDateTime(Eval("SubEnd")) < dateNow ? "label label-danger" : "label label-success"%>'>
                                                        <%# Convert.ToDateTime(Eval("SubEnd")) < dateNow ? "Inactive" : "Active" %>
                                                    </span>
                                                </td>
                                                <td><%# Eval("ORNo") %></td>
                                                <td><%# Eval("PaymentDate", "{0: dddd, MMMM d, yyyy}") %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <tr>
                                                <td colspan="12">
                                                    <h2 class="text-center">No records found.</h2>
                                                </td>
                                            </tr>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                </tbody>
                            </table>
                        </div>
                        <div class="panel-footer">
                            <center>
                                        <asp:DataPager id="dpSubscriptions" runat="server" pageSize="10" 
                                                       PagedControlID="lvSubscriptions">
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</asp:Content>

