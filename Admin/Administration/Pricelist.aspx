<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/site.master" AutoEventWireup="true" CodeFile="Pricelist.aspx.cs" Inherits="Admin_Administration_Pricelist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <i class="fa fa-edit"></i> Configure Pricelist
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <form class="form-horizontal" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="col-lg-12">
                    <div class="panel panel-midnightblue">
                        <div class="panel-heading">
                            Rates
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <label class="control-label col-lg-3">Regular Rate </label>
                                    <div class="col-lg-3">
                                        <div class="input-group">
                                            <span class="input-group-addon">₱</span>
                                            <asp:TextBox ID="txtRegRate" class="form-control" runat="server" 
                                                         TextMode="number" required/>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">1 month subscription</label>
                                    <div class="col-lg-3">
                                        <div class="input-group">
                                            <span class="input-group-addon">₱</span>
                                            <asp:TextBox ID="txtOneMReg" class="form-control" runat="server" 
                                                         TextMode="number" required />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">3 months subscription</label>
                                    <div class="col-lg-3">
                                        <div class="input-group">
                                            <span class="input-group-addon">₱</span>
                                            <asp:TextBox ID="txtThreeMReg" class="form-control" runat="server" 
                                                         TextMode="number" required/>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">6 months subscriptions</label>
                                    <div class="col-lg-3">
                                        <div class="input-group">
                                            <span class="input-group-addon">₱</span>
                                            <asp:TextBox ID="txtSixMReg" class="form-control" runat="server" 
                                                         TextMode="number" required/>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">1 Year subscription</label>
                                    <div class="col-lg-3">
                                        <div class="input-group">
                                            <span class="input-group-addon">₱</span>
                                            <asp:TextBox ID="txtOneYReg" class="form-control" runat="server" 
                                                         TextMode="number" required/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="form-group">
                                    <label class="control-label col-lg-3">Student Rate</label>
                                    <div class="col-lg-3">
                                        <div class="input-group">
                                            <span class="input-group-addon">₱</span>
                                            <asp:TextBox ID="txtStudRate" class="form-control" runat="server" 
                                                         TextMode="number" required/>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">1 month subscription</label>
                                    <div class="col-lg-3">
                                        <div class="input-group">
                                            <span class="input-group-addon">₱</span>
                                            <asp:TextBox ID="txtOneMStud" class="form-control" runat="server" 
                                                         TextMode="number" required/>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">3 months subscription</label>
                                    <div class="col-lg-3">
                                        <div class="input-group">
                                            <span class="input-group-addon">₱</span>
                                            <asp:TextBox ID="txtThreeMStud" class="form-control" runat="server" 
                                                         TextMode="number" required/>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">6 months subscriptions</label>
                                    <div class="col-lg-3">
                                        <div class="input-group">
                                            <span class="input-group-addon">₱</span>
                                            <asp:TextBox ID="txtSixMStud" class="form-control" runat="server"
                                                         TextMode="number" required/>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-lg-3">1 Year subscription</label>
                                    <div class="col-lg-3">
                                        <div class="input-group">
                                            <span class="input-group-addon">₱</span>
                                            <asp:TextBox ID="txtOneYStud" class="form-control" runat="server" 
                                                         TextMode="number" required/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-footer">
                            <div class="pull-right">
                                <asp:Button ID="btnSubmit" class="btn btn-success" runat="server" Text="Update" OnClick="btnSubmit_OnClick" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</asp:Content>

