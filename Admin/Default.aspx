<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Dashboard
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <form class="form-horizontal" runat="server">
        <asp:ScriptManager runat="server" />
        <div class="row">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:Timer ID="tmrDaily" runat="server" OnTick="tmrDaily_OnTick" Interval="1000" />
                    <div id="pnlDaily" class="row" runat="server">
                        <div class="panel panel-midnightblue">
                            <div class="panel panel-heading">
                                <h4>Daily Stats</h4>
                            </div>
                            <div class="col-md-3 col-xs-12 col-sm-6">
                                <a class="info-tiles tiles-sky" href="../Admin/Users/View.aspx">
                                    <div class="tiles-heading">Total Users</div>
                                    <div class="tiles-body-alt">
                                        <i class="fa fa-group"></i>
                                        <div class="text-center">
                                            <asp:Literal ID="ltDailyUsers" runat="server" />
                                        </div>
                                        <small>For today
                            <asp:Literal ID="ltToday" runat="server" />
                                        </small>
                                    </div>
                                    <div class="tiles-footer">go to users</div>
                                </a>
                            </div>
                            <div class="col-md-3 col-xs-12 col-sm-6">
                                <a class="info-tiles tiles-orange" href="../Admin/Membership/View.aspx">
                                    <div class="tiles-heading">New Memberships</div>
                                    <div class="tiles-body-alt">
                                        <i class="fa fa-id-card"></i>
                                        <div class="text-center">
                                            <asp:Literal ID="ltDailyMem" runat="server" />
                                        </div>
                                        <small>For today
                            <asp:Literal ID="ltToday2" runat="server" />
                                        </small>
                                    </div>
                                    <div class="tiles-footer">go to memberships</div>
                                </a>
                            </div>
                            <div class="col-md-3 col-xs-12 col-sm-6">
                                <a class="info-tiles tiles-brown" href="../Admin/Subscription/View.aspx">
                                    <div class="tiles-heading">New Subscriptions</div>
                                    <div class="tiles-body-alt">
                                        <i class="fa fa-id-card"></i>
                                        <div class="text-center">
                                            <span class="text-top"></span>
                                            <asp:Literal ID="ltDailySubs" runat="server" />
                                        </div>
                                        <small>For today
                            <asp:Literal ID="ltToday3" runat="server" />
                                        </small>
                                    </div>
                                    <div class="tiles-footer">go to subscriptions</div>
                                </a>
                            </div>
                            <div class="col-md-3 col-xs-12 col-sm-6">
                                <a class="info-tiles tiles-primary" href="../Admin/Logs/View.aspx">
                                    <div class="tiles-heading">Total Logs</div>
                                    <div class="tiles-body-alt">
                                        <i class="fa fa-gear"></i>
                                        <div class="text-center">
                                            <asp:Literal ID="ltDailyLogs" runat="server" />
                                        </div>
                                        <small>For today
                            <asp:Literal ID="ltToday4" runat="server" />
                                        </small>
                                    </div>
                                    <div class="tiles-footer">go to logs</div>
                                </a>
                            </div>
                        </div>
                    </div>
                    <div id="pnlMoney" class="row" runat="server">
                        <div class="panel panel-midnightblue">
                            <div class="panel panel-heading">
                                <h4>Daily Sales and Expenses</h4>
                            </div>
                            <div class="col-md-3 col-xs-12 col-sm-6">
                                <a class="info-tiles tiles-green" href="../Admin/Users/View.aspx">
                                    <div class="tiles-heading">Total Sales from Memberships</div>
                                    <div class="tiles-body-alt">
                                        <i class="fa fa-money-bill-alt"></i>
                                        <div class="text-center">
                                            ₱ <asp:Literal ID="ltSalesMem" runat="server" />
                                        </div>
                                        <small>For today
                            <asp:Literal ID="ltDailyS1" runat="server" />
                                        </small>
                                    </div>
                                    <div class="tiles-footer">go to users</div>
                                </a>
                            </div>
                            <div class="col-md-3 col-xs-12 col-sm-6">
                                <a class="info-tiles tiles-green" href="../Admin/Membership/View.aspx">
                                    <div class="tiles-heading">Total Sales from Subscriptions</div>
                                    <div class="tiles-body-alt">
                                        <i class="fa fa-money-bill-alt"></i>
                                        <div class="text-center">
                                            ₱ <asp:Literal ID="ltSalesSub" runat="server" />
                                        </div>
                                        <small>For today
                            <asp:Literal ID="ltDailyS2" runat="server" />
                                        </small>
                                    </div>
                                    <div class="tiles-footer">go to memberships</div>
                                </a>
                            </div>
                            <div class="col-md-3 col-xs-12 col-sm-6">
                                <a class="info-tiles tiles-green" href="../Admin/Subscription/View.aspx">
                                    <div class="tiles-heading">Total Sales from Workouts</div>
                                    <div class="tiles-body-alt">
                                        <i class="fa fa-money-bill-alt"></i>
                                        <div class="text-center">
                                            <span class="text-top"></span>
                                            ₱ <asp:Literal ID="ltSalesWork" runat="server" />
                                        </div>
                                        <small>For today
                            <asp:Literal ID="ltDailyS3" runat="server" />
                                        </small>
                                    </div>
                                    <div class="tiles-footer">go to subscriptions</div>
                                </a>
                            </div>
                            <div class="col-md-3 col-xs-12 col-sm-6">
                                <a class="info-tiles tiles-magenta" href="../Admin/Logs/View.aspx">
                                    <div class="tiles-heading">Total Expenses</div>
                                    <div class="tiles-body-alt">
                                        <i class="fa fa-level-down-alt"></i>
                                        <div class="text-center">
                                            ₱ <asp:Literal ID="ltExp" runat="server" />
                                        </div>
                                        <small>For today
                            <asp:Literal ID="ltDailyS4" runat="server" />
                                        </small>
                                    </div>
                                    <div class="tiles-footer">go to logs</div>
                                </a>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="col-lg-6">
                    <div class="panel panel-midnightblue">
                        <div class="panel-heading">
                            Check In
                        </div>
                        <div class="panel-body">

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
                            Check Out
                        </div>
                        <div class="panel-body">

                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</asp:Content>

