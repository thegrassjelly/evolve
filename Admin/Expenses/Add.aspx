<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/site.master" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Admin_Expenses_Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <i class="fa fa-plus"></i> Add Other Expenses
    
    <script type='text/javascript' src='<%= Page.ResolveUrl("~/js/newjs/jquery.min.js") %>'></script>
    <script type='text/javascript' src='<%= Page.ResolveUrl("~/js/newjs/jquery-ui.min.js") %>'></script>

    <script type="text/javascript">
        $(document).ready(function () {
            SearchUser();
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
            SearchUser();
        }

        function SearchUser() {
            $("#<%=txtName.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Add.aspx/GetName",
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
                    $("#<%=hfName.ClientID %>").val(i.item.val);
                },
                minLength: 2
            });
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <form class="form-horizontal" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="col-lg-12">
                    <div class="panel panel-midnightblue">
                        <div class="panel-heading">
                            Add Expense
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <label class="control-label col-lg-3">Expense Type</label>
                                    <div class="col-lg-3">
                                        <asp:DropDownList ID="ddlExpense" class="form-control" runat="server"
                                                          AutoPostback="True" OnSelectedIndexChanged="ddlExpense_OnSelectedIndexChanged" >
                                            <asp:ListItem>Salaries</asp:ListItem>
                                            <asp:ListItem>Operations</asp:ListItem>
                                            <asp:ListItem>Rent</asp:ListItem>
                                            <asp:ListItem>Utilities</asp:ListItem>
                                            <asp:ListItem>Others</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div id="pnlExpName" runat="server" class="form-group">
                                    <label class="control-label col-lg-3">Expense Name</label>
                                    <div class="col-lg-5">
                                        <asp:TextBox ID="txtExpName" class="form-control" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">Expense Description</label>
                                    <div class="col-lg-5">
                                        <asp:TextBox ID="txtExpDesc" class="form-control" runat="server" />
                                    </div>
                                </div>
                                <div id="pnlSalary" runat="server" visible="false">
                                    <div class="form-group">
                                        <label class="control-label col-lg-3">Search Name</label>
                                        <div class="col-lg-5">
                                            <asp:TextBox ID="txtName" class="form-control" runat="server" required />
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:LinkButton ID="btnUser" runat="server" OnClick="btnUser_OnClick" CssClass="btn btn-success"> 
                                        <i class="fa fa-refresh"></i></asp:LinkButton>
                                        </div>
                                        <asp:HiddenField runat="server" Value="0" ID="hfName" />
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-lg-3">First Name</label>
                                        <div class="col-lg-4">
                                            <asp:TextBox ID="txtFN" class="form-control" runat="server" disabled />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-lg-3">Last Name</label>
                                        <div class="col-lg-4">
                                            <asp:TextBox ID="txtLN" class="form-control" runat="server" disabled />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <label class="control-label col-lg-3">OR No.</label>
                                    <div class="col-lg-2">
                                        <asp:TextBox ID="txtORNo" class="form-control" runat="server"
                                                     TextMode="number" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">Expense Amount</label>
                                    <div class="col-lg-3">
                                        <div class="input-group">
                                            <span class="input-group-addon">₱</span>
                                            <asp:TextBox ID="txtPurAmnt" class="form-control" runat="server" 
                                                         Text="0" TextMode="number" required />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-footer">
                            <div class="pull-right">
                                <asp:Button ID="btnSubmit" class="btn btn-success" runat="server" Text="Add" 
                                            OnClick="btnSubmit_OnClick" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</asp:Content>

