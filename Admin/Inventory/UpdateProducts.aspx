<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/site.master" AutoEventWireup="true" CodeFile="UpdateProducts.aspx.cs" Inherits="Admin_Inventory_UpdateProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <i class="fa fa-edit"></i> Update Product Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <form class="form-horizontal" runat="server">
        <div class="col-lg-12">
            <div class="panel panel-midnightblue">
                <div class="panel-heading">
                    Product Details
                </div>
                <div class="panel-body">
                    <div class="col-lg-6">
                        <div class="form-group">
                            <label class="control-label col-lg-3">Product Name</label>
                            <div class="col-lg-6">
                                <asp:TextBox ID="txtProdName" class="form-control" runat="server" required />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-lg-3">Product Description</label>
                            <div class="col-lg-6">
                                <asp:TextBox ID="txtProdDesc" class="form-control" runat="server"
                                             TextMode="Multiline" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-lg-3">Product Type</label>
                            <div class="col-lg-6">
                                <asp:DropDownList ID="ddlProdType" class="form-control" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="form-group">
                            <label class="control-label col-lg-3">Purchase Price</label>
                            <div class="col-lg-6">
                                <div class="input-group">
                                    <span class="input-group-addon">₱</span>
                                    <asp:TextBox ID="txtPurPrice" class="form-control" runat="server" 
                                                 TextMode="number"/>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-lg-3">Selling Price</label>
                            <div class="col-lg-6">
                                <div class="input-group">
                                    <span class="input-group-addon">₱</span>
                                    <asp:TextBox ID="txtSellPrice" class="form-control" runat="server" 
                                                 TextMode="number" required/>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-lg-3">Status</label>
                            <div class="col-lg-6">
                                <asp:DropDownList ID="ddlStatus" class="form-control" runat="server">
                                    <asp:ListItem>Active</asp:ListItem>
                                    <asp:ListItem>Inactive</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-footer">
                    <div class="pull-right">
                        <asp:Button ID="btnBack" class="btn btn-primary" runat="server" Text="Back" OnClick="btnBack_OnClick" />
                        <asp:Button ID="btnSubmit" class="btn btn-success" runat="server" Text="Update" OnClick="btnSubmit_OnClick" />
                    </div>
                </div>
            </div>
        </div>
    </form>

</asp:Content>

