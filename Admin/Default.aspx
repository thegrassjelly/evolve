<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
            $(".autosuggest").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Default.aspx/GetName",
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
        <asp:ScriptManager runat="server" />
        <div class="row">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
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
                                <a class="info-tiles tiles-green" href="../Admin/Membership/View.aspx">
                                    <div class="tiles-heading">Total Sales from Memberships</div>
                                    <div class="tiles-body-alt">
                                        <i class="fa fa-money-bill-alt"></i>
                                        <div class="text-center">
                                            ₱
                                            <asp:Literal ID="ltSalesMem" runat="server" />
                                        </div>
                                        <small>For today
                            <asp:Literal ID="ltDailyS1" runat="server" />
                                        </small>
                                    </div>
                                    <div class="tiles-footer">go to memberships</div>
                                </a>
                            </div>
                            <div class="col-md-3 col-xs-12 col-sm-6">
                                <a class="info-tiles tiles-green" href="../Admin/Subscription/View.aspx">
                                    <div class="tiles-heading">Total Sales from Subscriptions</div>
                                    <div class="tiles-body-alt">
                                        <i class="fa fa-money-bill-alt"></i>
                                        <div class="text-center">
                                            ₱
                                            <asp:Literal ID="ltSalesSub" runat="server" />
                                        </div>
                                        <small>For today
                            <asp:Literal ID="ltDailyS2" runat="server" />
                                        </small>
                                    </div>
                                    <div class="tiles-footer">go to subscriptions</div>
                                </a>
                            </div>
                            <div class="col-md-3 col-xs-12 col-sm-6">
                                <a class="info-tiles tiles-green" href="../Admin/Operations/View.aspx">
                                    <div class="tiles-heading">Total Sales from Workouts</div>
                                    <div class="tiles-body-alt">
                                        <i class="fa fa-money-bill-alt"></i>
                                        <div class="text-center">
                                            <span class="text-top"></span>
                                            ₱
                                            <asp:Literal ID="ltSalesWork" runat="server" />
                                        </div>
                                        <small>For today
                            <asp:Literal ID="ltDailyS3" runat="server" />
                                        </small>
                                    </div>
                                    <div class="tiles-footer">go to operations</div>
                                </a>
                            </div>
                            <div class="col-md-3 col-xs-12 col-sm-6">
                                <a class="info-tiles tiles-magenta" href="../Admin/Expenses/View.aspx">
                                    <div class="tiles-heading">Total Expenses</div>
                                    <div class="tiles-body-alt">
                                        <i class="fa fa-level-down-alt"></i>
                                        <div class="text-center">
                                            ₱
                                            <asp:Literal ID="ltExp" runat="server" />
                                        </div>
                                        <small>For today
                            <asp:Literal ID="ltDailyS4" runat="server" />
                                        </small>
                                    </div>
                                    <div class="tiles-footer">go to expenses</div>
                                </a>
                            </div>
                        </div>
                    </div>
                    
                                        <div id="Div1" class="row" runat="server">
                        <div class="panel panel-midnightblue">
                            <div class="panel panel-heading">
                                <h4>Monthly Sales and Expenses</h4>
                            </div>
                            <div class="col-md-3 col-xs-12 col-sm-6">
                                <a class="info-tiles tiles-green" href="../Admin/Membership/View.aspx">
                                    <div class="tiles-heading">Total Sales from Memberships</div>
                                    <div class="tiles-body-alt">
                                        <i class="fa fa-money-bill-alt"></i>
                                        <div class="text-center">
                                            ₱
                                            <asp:Literal ID="ltMonthMem" runat="server" />
                                        </div>
                                        <small>For the Month of
                            <asp:Literal ID="ltMonth" runat="server" />
                                        </small>
                                    </div>
                                    <div class="tiles-footer">go to memberships</div>
                                </a>
                            </div>
                            <div class="col-md-3 col-xs-12 col-sm-6">
                                <a class="info-tiles tiles-green" href="../Admin/Subscription/View.aspx">
                                    <div class="tiles-heading">Total Sales from Subscriptions</div>
                                    <div class="tiles-body-alt">
                                        <i class="fa fa-money-bill-alt"></i>
                                        <div class="text-center">
                                            ₱
                                            <asp:Literal ID="ltMonthSub" runat="server" />
                                        </div>
                                        <small>For the Month of
                            <asp:Literal ID="ltMonth2" runat="server" />
                                        </small>
                                    </div>
                                    <div class="tiles-footer">go to subscriptions</div>
                                </a>
                            </div>
                            <div class="col-md-3 col-xs-12 col-sm-6">
                                <a class="info-tiles tiles-green" href="../Admin/Operations/View.aspx">
                                    <div class="tiles-heading">Total Sales from Workouts</div>
                                    <div class="tiles-body-alt">
                                        <i class="fa fa-money-bill-alt"></i>
                                        <div class="text-center">
                                            <span class="text-top"></span>
                                            ₱
                                            <asp:Literal ID="ltMonthOp" runat="server" />
                                        </div>
                                        <small>For the Month of
                            <asp:Literal ID="ltMonth3" runat="server" />
                                        </small>
                                    </div>
                                    <div class="tiles-footer">go to operations</div>
                                </a>
                            </div>
                            <div class="col-md-3 col-xs-12 col-sm-6">
                                <a class="info-tiles tiles-magenta" href="../Admin/Expenses/View.aspx">
                                    <div class="tiles-heading">Total Expenses</div>
                                    <div class="tiles-body-alt">
                                        <i class="fa fa-level-down-alt"></i>
                                        <div class="text-center">
                                            ₱
                                            <asp:Literal ID="ltMonthExp" runat="server" />
                                        </div>
                                        <small>For the Month of
                            <asp:Literal ID="ltMonth4" runat="server" />
                                        </small>
                                    </div>
                                    <div class="tiles-footer">go to expenses</div>
                                </a>
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="col-lg-12">
                    <div class="panel panel-midnightblue">
                        <div class="panel-heading">
                            Check In/Out
                        </div>
                        <div class="panel-body">
                            <div id="errorCheckIn" runat="server" class="alert alert-warning" visible="false">
                                <b>The customer is already checked-in</b>
                            </div>
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtSearch" runat="server" class="form-control autosuggest"
                                                     placeholder="Check-In Customer" required />
                                        <span class="input-group-btn">
                                            <asp:LinkButton ID="btnSearch" runat="server" class="btn btn-info"
                                                            OnClick="btnSearch_OnClick">
                                                <i class="fa fa-stopwatch"></i>
                                            </asp:LinkButton>
                                        </span>
                                    </div>
                                    <asp:HiddenField runat="server" Value="0" ID="hfName" />
                                </div>
                                <div class="col-lg-6">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtSearchCust" runat="server" class="form-control"
                                                     placeholder="Search Customer" AutoPostback="True"
                                                     OnTextChanged="txtSearchCust_OnTextChanged" />
                                        <span class="input-group-btn">
                                            <asp:LinkButton ID="btnSearchCust" runat="server" class="btn btn-info"
                                                            OnClick="btnSearchCust_OnClick">
                                                <i class="fa fa-search"></i>
                                            </asp:LinkButton>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <table class="table table-striped table-hover">
                                <thead>
                                    <th>Customer Name</th>
                                    <th>Status</th>
                                    <th>Check-In Time</th>
                                    <th>Check-Out Time</th>
                                    <th>Membership Status</th>
                                    <th>Subscription Status</th>
                                    <th>Check-Out Customer</th>
                                </thead>
                                <tbody>
                                    <asp:ListView ID="lvOperations" runat="server"
                                        OnPagePropertiesChanging="lvOperations_OnPagePropertiesChanging"
                                        OnDataBound="lvOperations_OnDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Eval("LastName") %>, <%# Eval("FirstName") %>
                                                </td>
                                                <td><%# Eval("CheckOut").ToString() != "" ? "Checked-Out" : "Checked-In" %></td>
                                                <td><%# Eval("CheckIn") %></td>
                                                <td><%# Eval("CheckOut") %></td>
                                                <td>
                                                    <span class='<%# Eval("MemStatus").ToString() == "Inactive" ? "label label-danger" : "label label-success"%>'>
                                                        <%# Eval("MemStatus").ToString() == "Inactive" ? "Inactive" : "Active"%>
                                                    </span>
                                                </td>
                                                <td>
                                                    <span class='<%# Eval("SubStatus").ToString() == "Inactive" ? "label label-danger" : "label label-success"%>'>
                                                        <%# Eval("SubStatus").ToString() == "Inactive" ? "No Active Subscriptions" 
                                                                 : Eval("SubStart", "{0: MMMM d, yyyy}") + " - " + Eval("SubEnd", "{0: MMMM d, yyyy}") %>
                                                    </span>
                                                </td>
                                                <td>
                                                    <a href='../Admin/CheckIn/CheckOut.aspx?ID=<%# Eval("OperationID") %>&&UserID=<%# Eval("UserID") %>'>
                                                        <i class="fas fa-sign-out-alt"></i>
                                                     </a>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <tr>
                                                <td colspan="12">
                                                    <h2 class="text-center">No Check-In's for today</h2>
                                                </td>
                                            </tr>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                </tbody>
                            </table>
                        </div>
                        <div class="panel-footer">
                            <center>
                                        <asp:DataPager id="dpOperations" runat="server" pageSize="10" PagedControlID="lvOperations">
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
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</asp:Content>

