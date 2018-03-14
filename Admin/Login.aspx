<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Admin_Login" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>EVOLVE | Login</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="PBAP">
    <meta name="author" content="The Red Team">

    <!-- <link href="assets/less/styles.less" rel="stylesheet/less" media="all"> -->
    <link rel="stylesheet" href="assets/css/styles.css">
    <link href='<%= Page.ResolveUrl("~/Admin/assets/fonts/web-fonts-with-css/css/fontawesome-all.min.css") %>' rel="stylesheet" />
    <link href='http://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600' rel='stylesheet' type='text/css'>

    <!-- <script type="text/javascript" src="assets/js/less.js"></script> -->
</head>
<body class="focusedform">
    <form action="#" class="form-horizontal" runat="server" style="margin-bottom: 0px !important;">
        <asp:ScriptManager runat="server" />
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="verticalcenter">
                    <img src="assets/img/adminlogo.jpg" alt="Logo" class="brand" style="width: 400px;" />
                    <div class="panel panel-primary">
                        <div class="panel-body">
                            <div id="loginerror" runat="server" class="alert alert-danger" visible="false">
                                Wrong Email/Password entered.
                            </div>
                            <div id="servererror" runat="server" class="alert alert-warning" visible="false">
                                Server error. Try again later.
                            </div>
                            <h4 class="text-center" style="margin-bottom: 25px;">Admin Login</h4>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="fa fa-user"></i></span>
                                        <asp:TextBox ID="txtEmail" class="form-control" runat="server"
                                            Placeholder="Email Address" required />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="fa fa-lock"></i></span>
                                        <asp:TextBox ID="txtPassword" class="form-control" runat="server"
                                            Placeholder="Password" TextMode="Password" required />
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix">
                                <div class="pull-right">
                                    <label>
                                        <input type="checkbox" style="margin-bottom: 20px" checked="">
                                        Remember Me</label>
                                </div>
                            </div>
                        </div>
                        <div class="panel-footer">
                            <div class="pull-right">
                                <asp:Button ID="btnReset" class="btn btn-default"
                                    Text="Reset" runat="server" OnClick="btnReset_OnClick" CausesValidation="False" />
                                <asp:Button ID="btnLogin" class="btn btn-success"
                                    Text="Login" runat="server" OnClick="btnLogin_OnClick" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
