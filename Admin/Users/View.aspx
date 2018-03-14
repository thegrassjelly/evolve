<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/site.master" AutoEventWireup="true" CodeFile="View.aspx.cs" Inherits="Admin_Users_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <i class="fa fa-list"></i> Users List
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
                                        <asp:DropDownList ID="ddlStatus" runat="server" class="form-control"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_OnSelectedIndexChanged">
                                            <asp:ListItem Text="Active" Value="Active" />
                                            <asp:ListItem Text="Inactive" Value="Inactive" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-1">
                                    <div class="input-group">
                                        <asp:DropDownList ID="ddlType" runat="server" class="form-control"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlType_OnSelectedIndexChanged">
                                            <asp:ListItem Text="All Users" />
                                            <asp:ListItem Text="Admin" Value="Admin" />
                                            <asp:ListItem Text="Staff" Value="Staff" />
                                            <asp:ListItem Text="Coach" Value="Coach" />
                                            <asp:ListItem Text="Walk-in" Value="Walk-in" />
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
                                    <th>Mobile No.</th>
                                    <th>Birthday</th>
                                    <th>User Type</th>
                                    <th>Status</th>
                                    <th>Date Added</th>
                                    <th>Date Modified</th>
                                    <th></th>
                                    <th></th>
                                </thead>
                                <tbody>
                                    <asp:ListView ID="lvUsers" runat="server"
                                        OnPagePropertiesChanging="lvUsers_OnPagePropertiesChanging"
                                        OnDataBound="lvUsers_OnDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("UserID") %></td>
                                                <td><%# Eval("LastName") %>, <%# Eval("FirstName") %> </td>
                                                <td><%# Eval("MobileNo") %></td>
                                                <td><%# Eval("Birthday", "{0: MMMM d, yyyy}") %></td>
                                                <td><span class="label label-primary"><%# Eval("UserType") %></span></td>
                                                <td>
                                                    <span class='<%# Eval("Status").ToString() == "Inactive" ? "label label-danger" : "label label-success"%>'>
                                                        <%# Eval("Status") %>
                                                    </span>
                                                </td>
                                                <td><%# Eval("DateAdded", "{0: dddd, MMMM d, yyyy}") %></td>
                                                <td><%# Eval("DateModified", "{0: dddd, MMMM d, yyyy}") %></td>
                                                <td>
                                                    <a href='UpdateUsers.aspx?ID=<%# Eval("UserID") %>'>
                                                        <asp:Label runat="server" ToolTip="Show Info"><i class="fa fa-edit"></i></asp:Label></a>
                                                </td>
                                                <td>
                                                    <a href='../Membership/MembershipDetails.aspx?ID=<%# Eval("UserID") %>'>
                                                        <asp:Label runat="server" ToolTip="Membership Details"><i class="fa fa-id-card"></i></asp:Label></a>
                                                </td>
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
                                        <asp:DataPager id="dpUsers" runat="server" pageSize="10" PagedControlID="lvUsers">
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

