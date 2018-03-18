<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/site.master" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Admin_Training_Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <i class="fa fa-plus"></i> Add Coaching
    
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
                            Coaching Record
                        </div>
                        <div class="panel-body">
                            <table class="table table-striped table-hover">
                                <thead>
                                    <th>Date of Record</th>
                                    <th>Fitness Goal</th>
                                    <th>Coach Name</th>
                                    <th>Coaching Package</th>
                                    <th>Weight</th>
                                    <th></th>
                                </thead>
                                <tbody>
                                    <asp:ListView ID="lvCoaching" runat="server"
                                        OnPagePropertiesChanging="lvCoaching_OnPagePropertiesChanging"
                                        OnDataBound="lvCoaching_OnDataBound"
                                        OnItemCommand="lvCoaching_OnItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("DateAdded", "{0: MMMM d, yyyy}") %></td>
                                                <asp:Literal ID="ltTrainID" runat="server" Text='<%# Eval("TrainingID") %>' Visible="false" />
                                                <td><span class="label label-primary"><%# Eval("GoalSetting") %></span></td>
                                                <td><%# Eval("CoachName") %></td>
                                                <td><%# Eval("TrainingPackage") %></td>
                                                <td><%# Eval("Weight") %> kg</td>
                                                <td>
                                                    <asp:Button ID="btnCoachingInfo" CommandName="coachingDetails"
                                                                class="btn btn-sm btn-primary" runat="server" Text='View Coaching Details'
                                                                OnSubmitBehavior="false" 
                                                                formnovalidate />
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
                            <asp:DataPager id="dpCoachHist" runat="server" pageSize="5" PagedControlID="lvCoaching">
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
                            Coaching Details
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <label class="control-label col-lg-3">Coach Name</label>
                                    <div class="col-lg-7">
                                        <asp:DropDownList ID="ddlCoach" class="form-control" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">Age</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtAge" class="form-control"
                                            TextMode="number" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">Weight (kg)</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtWeight" class="form-control"
                                                     TextMode="number" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">Height (cm)</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtHeight" class="form-control"
                                            TextMode="number" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">Arms (cm)</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtArms" class="form-control"
                                            TextMode="number" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">Chest (cm)</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtChst" class="form-control"
                                            TextMode="number" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">Waist (cm)</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtWst" class="form-control"
                                            TextMode="number" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">Hip (cm)</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtHip" class="form-control"
                                            TextMode="number" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">Thigh (cm)</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtThgh" class="form-control"
                                            TextMode="number" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">Legs (cm)</label>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txtLgs" class="form-control"
                                            TextMode="number" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <label class="control-label col-lg-3">Fitness Goal</label>
                                    <div class="col-lg-7">
                                        <asp:DropDownList ID="ddlGoal" class="form-control" runat="server">
                                            <asp:ListItem>Weight Loss</asp:ListItem>
                                            <asp:ListItem>Weight Gain</asp:ListItem>
                                            <asp:ListItem>Tone and Shape</asp:ListItem>
                                            <asp:ListItem>Muscle Gain</asp:ListItem>
                                            <asp:ListItem>Endurance</asp:ListItem>
                                            <asp:ListItem>Health Improvement</asp:ListItem>
                                            <asp:ListItem>Decrease BP</asp:ListItem>
                                            <asp:ListItem>Improve Posture</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">Coaching Package</label>
                                    <div class="col-lg-7">
                                        <asp:DropDownList ID="ddlPackage" class="form-control" runat="server">
                                            <asp:ListItem>Single Session</asp:ListItem>
                                            <asp:ListItem>Monthly Session</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">Coaching Fee</label>
                                    <div class="col-lg-5">
                                        <asp:TextBox ID="txtCoachFee" class="form-control"
                                            TextMode="number" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">Time of record</label>
                                    <div class="col-lg-7">
                                        <asp:TextBox ID="txtCurrentDate" class="form-control" runat="server" 
                                                     TextMode="date" required />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-5"><b>Training Schedule</b></label>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">Monday</label>
                                    <div class="col-lg-5">
                                        <asp:CheckBox ID="chkMon" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">Tuesday</label>
                                    <div class="col-lg-5">
                                        <asp:CheckBox ID="chkTues" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">Wednesday</label>
                                    <div class="col-lg-5">
                                        <asp:CheckBox ID="chkWed" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">Thursday</label>
                                    <div class="col-lg-5">
                                        <asp:CheckBox ID="chkThurs" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">Friday</label>
                                    <div class="col-lg-5">
                                        <asp:CheckBox ID="chkFri" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">Saturday</label>
                                    <div class="col-lg-5">
                                        <asp:CheckBox ID="chkSat" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-footer">
                            <div class="pull-right">
                                <asp:Button ID="btnBack" class="btn btn-primary" runat="server" Text="Back" OnClick="btnBack_OnClick" formnovalidate />
                                <asp:Button ID="btnSubmit" class="btn btn-success" runat="server" Text="Add"
                                    OnClientClick="return confirm('Are you sure?')" OnClick="btnSubmit_OnClick" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="coachingDetails" class="modal fade">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Coaching Details</h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="panel-body">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label class="control-label col-lg-3">Coach Name</label>
                                        <div class="col-lg-7">
                                            <asp:TextBox ID="txtCoachName" class="form-control" runat="server" disabled />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-lg-3">Age</label>
                                        <div class="col-lg-3">
                                            <asp:TextBox ID="txtAge2" class="form-control"
                                                runat="server" disabled />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-lg-3">Weight (kg)</label>
                                        <div class="col-lg-3">
                                            <asp:TextBox ID="txtWght2" class="form-control"
                                                         runat="server" disabled />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-lg-3">Height (cm)</label>
                                        <div class="col-lg-3">
                                            <asp:TextBox ID="txtHght2" class="form-control"
                                                         runat="server" disabled />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-lg-3">Arms (cm)</label>
                                        <div class="col-lg-3">
                                            <asp:TextBox ID="txtArms2" class="form-control"
                                                         runat="server" disabled />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-lg-3">Chest (cm)</label>
                                        <div class="col-lg-3">
                                            <asp:TextBox ID="txtChst2" class="form-control"
                                                         runat="server" disabled />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-lg-3">Waist (cm)</label>
                                        <div class="col-lg-3">
                                            <asp:TextBox ID="txtWst2" class="form-control"
                                                         runat="server" disabled />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-lg-3">Hip (cm)</label>
                                        <div class="col-lg-3">
                                            <asp:TextBox ID="txtHip2" class="form-control"
                                                         runat="server" disabled />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-lg-3">Thigh (cm)</label>
                                        <div class="col-lg-3">
                                            <asp:TextBox ID="txtThgh2" class="form-control"
                                                         runat="server" disabled />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-lg-3">Legs (cm)</label>
                                        <div class="col-lg-3">
                                            <asp:TextBox ID="txtLgs2" class="form-control"
                                                         runat="server" disabled />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label class="control-label col-lg-3">Fitness Goal</label>
                                        <div class="col-lg-7">
                                            <asp:TextBox ID="txtGoal" class="form-control" runat="server" disabled />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-lg-3">Coaching Package</label>
                                        <div class="col-lg-7">
                                            <asp:TextBox ID="txtPackage" class="form-control" runat="server" disabled />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-lg-3">Coaching Fee</label>
                                        <div class="col-lg-5">
                                            <asp:TextBox ID="txtCoachFee2" class="form-control"
                                                disabled runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-lg-3">Time of record</label>
                                        <div class="col-lg-7">
                                            <asp:TextBox ID="txtTOR" class="form-control" runat="server"
                                                disabled />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-lg-5"><b>Training Schedule</b></label>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-lg-3">Monday</label>
                                        <div class="col-lg-5">
                                            <asp:CheckBox ID="chkMon2" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-lg-3">Tuesday</label>
                                        <div class="col-lg-5">
                                            <asp:CheckBox ID="chkTue2" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-lg-3">Wednesday</label>
                                        <div class="col-lg-5">
                                            <asp:CheckBox ID="chkWed2" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-lg-3">Thursday</label>
                                        <div class="col-lg-5">
                                            <asp:CheckBox ID="chkThu2" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-lg-3">Friday</label>
                                        <div class="col-lg-5">
                                            <asp:CheckBox ID="chkFri2" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-lg-3">Saturday</label>
                                        <div class="col-lg-5">
                                            <asp:CheckBox ID="chkSat2" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>

