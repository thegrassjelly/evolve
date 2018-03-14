<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/site.master" AutoEventWireup="true" CodeFile="UpdateUsers.aspx.cs" Inherits="Admin_Users_UpdateUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <i class="fa fa-edit"></i> Update User Information
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <form class="form-horizontal" runat="server">
        <div class="col-lg-12">
            <div class="panel panel-midnightblue">
                <div class="panel-heading">
                    User Details
                </div>
                <div class="panel-body">
                    <div class="col-lg-6">
                        <div class="form-group">
                            <label class="control-label col-lg-3">User ID</label>
                            <div class="col-lg-2">
                                <asp:TextBox ID="txtID" class="form-control" runat="server" disabled />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-lg-3">First Name</label>
                            <div class="col-lg-6">
                                <asp:TextBox ID="txtFN" class="form-control" runat="server" required />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-lg-3">Last Name</label>
                            <div class="col-lg-6">
                                <asp:TextBox ID="txtLN" class="form-control" runat="server" required />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-lg-3">Date of Birth</label>
                            <div class="col-lg-6">
                                <div class="input-group date" id="datepicker-pastdisabled">
                                    <asp:TextBox ID="txtBday" class="form-control" runat="server" TextMode="Date" required />
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-lg-3">Email Address</label>
                            <div class="col-lg-6">
                                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"
                                    class="form-control" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="form-group">
                            <label class="control-label col-lg-3">Mobile No.</label>
                            <div class="col-lg-6">
                                <asp:TextBox ID="txtMNo" class="form-control" runat="server"
                                    TextMode="Number" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-lg-3">Address</label>
                            <div class="col-lg-6">
                                <asp:TextBox ID="txtAddr" class="form-control" runat="server"
                                    TextMode="Multiline" style="max-width: 100%;"/>
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
                        <div class="form-group">
                            <label class="control-label col-lg-3">User Type</label>
                            <div class="col-lg-6">
                                <asp:DropDownList ID="ddlType" class="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-footer">
                    <div class="pull-right">
                        <asp:Button ID="btnBack" class="btn btn-primary" runat="server" Text="Back" OnClick="btnBack_OnClick" />
                        <asp:Button ID="btnUpdate" class="btn btn-success" runat="server" Text="Update" OnClick="btnUpdate_OnClick" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>

