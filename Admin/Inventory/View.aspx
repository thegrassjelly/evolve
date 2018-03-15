<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/site.master" AutoEventWireup="true" CodeFile="View.aspx.cs" Inherits="Admin_Inventory_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <i class="fa fa-list"></i> Product List
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
                                        <asp:DropDownList ID="ddlCategory" runat="server" class="form-control"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged">
                                            <asp:ListItem Text="All Products" />
                                            <asp:ListItem Text="Consumable" Value="Consumable" />
                                            <asp:ListItem Text="Merchandise" Value="Merchandise" />
                                            <asp:ListItem Text="Supplement" Value="Supplement" />
                                            <asp:ListItem Text="Others" Value="Others" />
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
                                    <th>Product Name</th>
                                    <th>Product Description</th>
                                    <th>Current Inventory</th>
                                    <th>Purchase Price</th>
                                    <th>Selling Price</th>
                                    <th>Category</th>
                                    <th>Status</th>
                                    <th>Date Added</th>
                                    <th>Date Modified</th>
                                    <th></th>
                                </thead>
                                <tbody>
                                    <asp:ListView ID="lvProducts" runat="server"
                                        OnPagePropertiesChanging="lvProducts_OnPagePropertiesChanging"
                                        OnDataBound="lvProducts_OnDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("ProductID") %></td>
                                                <td><%# Eval("ProductName") %></td>
                                                <td><%# Eval("ProductDescription") %></td>
                                                <td><%# Eval("PIQty") %></td>
                                                <td>₱ <%# Eval("PurchasePrice","{0: #,###.00}") %></td>
                                                <td>₱ <%# Eval("SellingPrice","{0: #,###.00}") %></td>
                                                <td><span class="label label-primary"><%# Eval("ProductType") %></span></td>
                                                <td>
                                                    <span class='<%# Eval("Status").ToString() == "Inactive" ? "label label-danger" : "label label-success"%>'>
                                                        <%# Eval("Status") %>
                                                    </span>
                                                </td>
                                                <td><%# Eval("DateAdded", "{0: dddd, MMMM d, yyyy}") %></td>
                                                <td><%# Eval("DateModified", "{0: dddd, MMMM d, yyyy}") %></td>
                                                <td>
                                                    <a href='UpdateProducts.aspx?ID=<%# Eval("ProductID") %>'>
                                                        <asp:Label runat="server" ToolTip="Show Info"><i class="fa fa-edit"></i></asp:Label></a>
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
                                        <asp:DataPager id="dpProducts" runat="server" pageSize="10" PagedControlID="lvProducts">
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

