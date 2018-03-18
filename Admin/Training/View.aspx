<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/site.master" AutoEventWireup="true" CodeFile="View.aspx.cs" Inherits="Admin_Training_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <i class="fa fa-list"></i> Coachings List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <form class="form-horizontal" runat="server">
        <asp:ScriptManager runat="server" />
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="col-lg-12">
                    <div class="panel panel-midnightblue">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-2">
                                    <div class="input-group">
                                        <asp:DropDownList ID="ddlGoal" runat="server" class="form-control"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlGoal_OnSelectedIndexChanged">
                                            <asp:ListItem>All Goals</asp:ListItem>
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
                                <div class="col-lg-10">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtSearch" runat="server" class="form-control autosuggest"
                                            placeholder="Keyword..." OnTextChanged="txtSearch_OnTextChanged" AutoPostBack="true" />
                                        <span class="input-group-btn">
                                            <asp:LinkButton ID="btnSearch" runat="server" class="btn btn-info"
                                                OnClick="btnSearch_OnClick">
                                      <i class="fa fa-search"></i>
                                            </asp:LinkButton>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <table class="table table-striped table-hover">
                                <thead>
                                    <th>#</th>
                                    <th>Client Name</th>
                                    <th>Fitness Goal</th>
                                    <th>Coach Name</th>
                                    <th>Training Package</th>
                                    <th>Training Fee</th>
                                    <th>Date Added</th>
                                    <th>Date Modified</th>
<%--                                    <th></th>--%>
                                </thead>
                                <tbody>
                                    <asp:ListView ID="lvTrainings" runat="server"
                                        OnPagePropertiesChanging="lvTrainings_OnPagePropertiesChanging"
                                        OnDataBound="lvTrainings_OnDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("TrainingID") %></td>
                                                <td><%# Eval("LastName") %>, <%# Eval("FirstName") %></td>
                                                <td><span class="label label-primary"><%# Eval("GoalSetting") %></span></td>
                                                <td><%# Eval("CoachName") %></td>
                                                <td><%# Eval("TrainingPackage") %></td>
                                                <td>₱ <%# Eval("TrainingFee","{0: #,###.00}") %></td>
                                                <td><%# Eval("DateAdded", "{0: MMMM d, yyyy}") %></td>
                                                <td><%# Eval("DateModified", "{0: MMMM d, yyyy}") %></td>
<%--                                                <td>
                                                    <a href='UpdateCoaching.aspx?ID=<%# Eval("TrainingID") %>'>
                                                        <asp:Label runat="server" ToolTip="Show Info"><i class="fa fa-edit"></i></asp:Label></a>
                                                </td>--%>
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
                                        <asp:DataPager id="dpTrainings" runat="server" pageSize="10" PagedControlID="lvTrainings">
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
    </form>
</asp:Content>

