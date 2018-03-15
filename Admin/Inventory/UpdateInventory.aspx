<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/site.master" AutoEventWireup="true" CodeFile="UpdateInventory.aspx.cs" Inherits="Admin_Inventory_UpdateInventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <i class="fa fa-edit"></i> Update Inventory
    
    <script type='text/javascript' src='<%= Page.ResolveUrl("~/js/newjs/jquery.min.js") %>'></script>
    <script type='text/javascript' src='<%= Page.ResolveUrl("~/js/newjs/jquery-ui.min.js") %>'></script>

    <script type="text/javascript">
        $(document).ready(function () {
            SearchProduct();
        });
        // if you use jQuery, you can load them when dom is read.
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);

        });

        function InitializeRequest(sender, args) {
        }

        function EndRequest(sender, args) {
            // after update occur on UpdatePanel re-init the Autocomplete
            SearchProduct();
        }

        function SearchProduct() {
            $("#<%=txtProdName.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "UpdateInventory.aspx/GetProducts",
                        data: "{'prefixText':'" + request.term + "'}",
                        dataType: "json",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('/vn/')[0],
                                    val: item.split('/vn/')[1]
                                }
                            }))
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                },
                select: function (e, i) {
                    $("#<%=hfProduct.ClientID %>").val(i.item.val);
                },
                minLength: 2
            });
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <form class="form-horizontal" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="col-lg-6">
                    <div class="panel panel-midnightblue">
                        <div class="panel-heading">
                            Product Details
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <label class="control-label col-lg-3">Product Name</label>
                                <div class="col-lg-5">
                                    <asp:TextBox ID="txtProdName" class="form-control" runat="server" required />
                                    <asp:HiddenField ID="hfProduct" runat="server" />
                                </div>
                                <div class="col-lg-2">
                                    <asp:LinkButton ID="btnProduct" runat="server" OnClick="btnProduct_OnClick" Class="btn btn-success"> 
                                <i class="fa fa-refresh"></i></asp:LinkButton>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-lg-3">Product Description</label>
                                <div class="col-lg-6">
                                    <asp:TextBox ID="txtProdDesc" class="form-control" runat="server"
                                        TextMode="Multiline" disabled />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-lg-3">Product Type</label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txtProductType" class="form-control" runat="server" disabled />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-lg-3">Purchase Price</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <span class="input-group-addon">₱</span>
                                        <asp:TextBox ID="txtPurPrice" class="form-control" runat="server"
                                            disabled />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-lg-3">Selling Price</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <span class="input-group-addon">₱</span>
                                        <asp:TextBox ID="txtSellPrice" class="form-control" runat="server"
                                            disabled />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-lg-3">Current Inventory</label>
                                <div class="col-lg-2">
                                    <asp:TextBox ID="txtProdInvty" class="form-control" runat="server"
                                        disabled />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-lg-3">Status</label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txtStatus" class="form-control" runat="server" disabled />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="col-lg-6">
                    <div class="panel panel-midnightblue">
                        <div class="panel-heading">
                            Purchase Details
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <label class="control-label col-lg-3">Quantity Received</label>
                                <div class="col-lg-2">
                                    <asp:TextBox ID="txtQty" class="form-control" runat="server"
                                                 Text="0" TextMode="number" required />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-lg-3">Delivery Date</label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txtDDate" class="form-control" runat="server"
                                                 TextMode="date" required />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-lg-3">Other Remarks</label>
                                <div class="col-lg-6">
                                    <asp:TextBox ID="txtRemarks" class="form-control" runat="server"
                                                 TextMode="multiline" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-lg-3">Total Purchase Amount</label>
                                <div class="col-lg-3">
                                    <div class="input-group">
                                        <span class="input-group-addon">₱</span>
                                        <asp:TextBox ID="txtPurAmnt" class="form-control" runat="server" 
                                                     Text="0" TextMode="number" required />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-lg-3">OR No.</label>
                                <div class="col-lg-2">
                                    <asp:TextBox ID="txtORNo" class="form-control" runat="server"
                                                 TextMode="number" required />
                                </div>
                            </div>
                        </div>
                        <div class="panel-footer">
                            <div class="pull-right">
                                <asp:Button ID="btnAdd" class="btn btn-success" runat="server" Text="Add" 
                                            OnClientClick="return confirm('Are you sure?')" OnClick="btnAdd_OnClick" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</asp:Content>

