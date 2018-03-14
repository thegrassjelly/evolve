<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/site.master" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Admin_Inventory_Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <i class="fa fa-plus"></i> Add Products
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
                                <asp:TextBox ID="txtPurPrice" class="form-control" runat="server"
                                    TextMode="Number" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-lg-3">Selling Price</label>
                            <div class="col-lg-6">
                                <asp:TextBox ID="txtSellPrice" class="form-control" runat="server"
                                             TextMode="Number" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-lg-3">Initial Inventory</label>
                            <div class="col-lg-6">
                                <asp:TextBox ID="txtProdInvty" class="form-control" runat="server"
                                             TextMode="Number" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-footer">
                    <div class="pull-right">
                        <asp:Button ID="btnSubmit" class="btn btn-success" runat="server" Text="Add" OnClick="btnSubmit_OnClick" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>

