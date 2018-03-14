<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/site.master" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Admin_Membership_Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <i class="fa fa-plus"></i> Add Membership
    
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
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <form class="form-horizontal" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="col-lg-12">
                    <div class="panel panel-midnightblue">
                        <div class="panel-heading">
                            Membership Details
                        </div>
                        <div class="panel-body">
                            <div id="pnlMember" runat="server" visible="false">
                                <div class="form-group">
                                    <label class="control-label col-lg-1 col-xs-12">Start Date</label>
                                    <div class="col-lg-2 col-xs-12">
                                        <asp:TextBox ID="txtStartDate" class="form-control" runat="server" disabled />
                                    </div>
                                    <label class="control-label col-lg-2 col-xs-12">Membership Length</label>
                                    <div class="col-lg-1 col-xs-12">
                                        <asp:TextBox ID="txtMembershipLength" class="form-control text-center" runat="server" disabled />
                                    </div>
                                    <label class="col-lg-1 col-xs-12">Year</label>
                                    <label class="control-label col-lg-2 col-xs-12">Membership Type</label>
                                    <div class="col-lg-2 col-xs-12">
                                        <asp:TextBox ID="txtMembershipType2" class="form-control text-center" runat="server" disabled />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-1 col-xs-12">End Date</label>
                                    <div class="col-lg-2 col-xs-12">
                                        <asp:TextBox ID="txtEndDate" class="form-control" runat="server" disabled />
                                    </div>
                                    <label class="control-label col-lg-2 col-xs-12">Payment</label>
                                    <div class="col-lg-2 text-center">
                                        <div class="alert alert-success" runat="server">
                                            Paid
                                        </div>                                        
                                    </div>
                                    <label class="control-label col-lg-2 col-xs-12">Membership Status</label>
                                    <div class="col-lg-2 text-center">
                                        <div id="memInactive" class="alert alert-danger" runat="server">
                                            <b><asp:Literal ID="txtMembershipStatus" runat="server" /></b>
                                        </div>            
                                        <div id="memActive" class="alert alert-success" runat="server">
                                            <b><asp:Literal ID="txtMembershipStatus2" runat="server" /></b>
                                        </div>    
                                    </div>
                                </div>
                            </div>
                            <div id="pnlNonMember" runat="server" visible="false">
                                <div class="alert alert-warning">
                                    <b>No ongoing membership/Membership have expired</b>
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
                            User Details
                        </div>
                        <div class="panel-body">
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
                            <div class="form-group">
                                <label class="control-label col-lg-3">Date of Birth</label>
                                <div class="col-lg-3">
                                    <div class="input-group date" id="datepicker-pastdisabled">
                                        <asp:TextBox ID="txtBday" class="form-control" runat="server" TextMode="Date" disabled />
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-lg-3">Email</label>
                                <div class="col-lg-4">
                                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"
                                        class="form-control" disabled />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-lg-3">Mobile No.</label>
                                <div class="col-lg-4">
                                    <asp:TextBox ID="txtMNo" class="form-control" runat="server"
                                        TextMode="Number" disabled />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-lg-3">Address</label>
                                <div class="col-lg-6">
                                    <asp:TextBox ID="txtAddr" class="form-control" runat="server"
                                        TextMode="Multiline" Style="max-width: 100%;" disabled />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-lg-3">Status</label>
                                <div class="col-lg-3">
                                    <asp:TextBox runat="server" class="form-control" ID="txtStatus" disabled />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-lg-3">User Type</label>
                                <div class="col-lg-3">
                                    <asp:TextBox runat="server" class="form-control" ID="txtUserType" disabled />
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
                            Add New Membership
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <label class="control-label col-lg-3">Rate</label>
                                <div class="col-lg-3">
                                 <asp:DropDownList id="ddlRate" class="form-control" runat="server"
                                                   AutoPostBack="True" OnSelectedIndexChanged="ddlRate_OnSelectedIndexChanged">
                                     <asp:ListItem>Regular</asp:ListItem>
                                     <asp:ListItem>Student</asp:ListItem>
                                 </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-lg-3">Start Date</label>
                                <div class="col-lg-4">
                                    <div class="input-group date">
                                        <asp:TextBox ID="txtSDate" class="form-control" runat="server" disabled />
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-lg-3">End Date</label>
                                <div class="col-lg-4">
                                    <div class="input-group date">
                                        <asp:TextBox ID="txtEDate" class="form-control" runat="server" disabled />
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-lg-3">Total Amount</label>
                                <div class="col-lg-3">
                                    <button type="button" id="btnPriceLit" class="btn btn-success">
                                        <asp:Literal ID="ltTotal" runat="server" />
                                    </button>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-lg-3">OR Number</label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txtORNo" class="form-control" TextMode="number"
                                                 runat="server" />
                                    <div id="errorORNo" runat="server" class="alert alert-danger" visible="false" >
                                        <b>Add official receipt no.</b>
                                    </div>
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
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="col-lg-6">
                    <div class="panel panel-midnightblue">
                        <div class="panel-heading">
                            Add Current Membership
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <label class="control-label col-lg-3">Rate</label>
                                <div class="col-lg-3">
                                 <asp:DropDownList id="ddlRate2" class="form-control" runat="server"
                                                   AutoPostBack="True" OnSelectedIndexChanged="ddlRate2_OnSelectedIndexChanged">
                                     <asp:ListItem>Regular</asp:ListItem>
                                     <asp:ListItem>Student</asp:ListItem>
                                 </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-lg-3">Start Date</label>
                                <div class="col-lg-4">
                                    <div class="input-group date">
                                        <asp:TextBox ID="txtSDate2" class="form-control"
                                                     TextMode="date" runat="server" />
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-lg-3">End Date</label>
                                <div class="col-lg-4">
                                    <div class="input-group date">
                                        <asp:TextBox ID="txtEDate2" class="form-control" 
                                                     runat="server" disabled />
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-lg-3">OR Number</label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txtORNo2" class="form-control" TextMode="number"
                                                 runat="server" />
                                    <div id="errorORNo2" runat="server" class="alert alert-danger" visible="false" >
                                        <b>Add official receipt no.</b>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-footer">
                            <div class="pull-right">
                                <asp:Button ID="btnAddCurrent" class="btn btn-success" runat="server" Text="Add" 
                                            OnClientClick="return confirm('Are you sure?')" OnClick="btnAddCurrent_OnClick" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</asp:Content>

