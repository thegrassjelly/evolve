<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/site.master" AutoEventWireup="true" CodeFile="View.aspx.cs" Inherits="Admin_Expenenses_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <i class="fa fa-list"></i> Expenses List
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
                                <div class="col-lg-2">
                                    <div class="input-group">
                                        <asp:DropDownList ID="ddlExpenseType" runat="server" class="form-control"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlExpenseType_OnSelectedIndexChanged" />
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
                                    <th>Expense Name</th>
                                    <th>Expense Description</th>
                                    <th>Expense Type</th>
                                    <th>OR No.</th>
                                    <th>Amount</th>
                                    <th>Purchase Date</th>
                                </thead>
                                <tbody>
                                    <asp:ListView ID="lvExpenses" runat="server"
                                        OnPagePropertiesChanging="lvExpenses_OnPagePropertiesChanging"
                                        OnDataBound="lvExpenses_OnDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("ExpenseID") %></td>
                                                <td><%# Eval("ExpenseName") %></td>
                                                <td><%# Eval("ExpenseDescription") %></td>
                                                <td><span class="label label-primary"><%# Eval("ExpenseType") %></span></td>
                                                <td><%# Eval("ORNo") %></td>
                                                <td>
                                                    ₱ <%# Eval("Cost", "{0: #,###.00}") %>
                                                </td>
                                                <td><%# Eval("DateAdded", "{0: MMMM d, yyyy}") %></td>
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
                                        <asp:DataPager id="dpExpenses" runat="server" pageSize="10" PagedControlID="lvExpenses">
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

