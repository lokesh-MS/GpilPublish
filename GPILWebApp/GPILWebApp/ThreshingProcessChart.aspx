<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ThreshingProcessChart.aspx.cs" Inherits="GPILWebApp.ThreshingProcessChart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-header">
        <h1 style="font-weight: bold; font-family: Cambria;">Threshing Process Chart</h1>
    </div>


    <div class="container-fluid">
        <div class="row">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <h6 class="card-title">Searching Criteria</h6>

                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-4 mb-4">
                                        <label for="form-field-3">From Date</label>
                                        <asp:TextBox ID="txtFromDate" runat="server" class="form-control" TextMode="Date">
                                        </asp:TextBox>
                                    </div>


                                    <div class="col-md-4 mb-4">
                                        <label for="form-field-3">To Date</label>
                                        <asp:TextBox ID="txtToDate" runat="server" class="form-control" TextMode="Date">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-md-4 mb-4">
                                        <label for="form-field-3">Process Organization</label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlProcessOrganization">
                                            <asp:ListItem Value="0">SELECT </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-4 mb-4">
                                        <label for="form-field-3">Crop Year</label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCropYear">
                                            <asp:ListItem Value="0">SELECT </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-4 mb-4">
                                        <label for="form-field-3">Variety </label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlVariety">
                                            <asp:ListItem Value="0">SELECT </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-4 mb-4">
                                        <label for="form-field-3">Batch Number</label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlBatchNumber">
                                            <asp:ListItem Value="0">SELECT </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-4 mb-4">
                                        <asp:RadioButton ID="ThreshingIssueDetails" runat="server" GroupName="rdbexporttype" Text="Threshing IssueD etails" AutoPostBack="True"
                                            OnCheckedChanged="ThreshingIssueDetails_CheckedChanged" ForeColor="Black" CssClass="form-control " />
                                    </div>


                                    <div class="col-md-4 mb-4">
                                        <asp:RadioButton ID="rdbProcessChart" runat="server" GroupName="rdbexporttype" Text="Process Chart" AutoPostBack="True"
                                            OnCheckedChanged="rdbProcessChart_CheckedChanged" ForeColor="Black" CssClass="form-control " />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <asp:Button ID="btnView" runat="server" CssClass="btn btn-sm btn-success" Text="View " OnClick ="btnView_Click" BackColor="#0099FF"   />
                                    <asp:Button ID="btnClose" runat="server" CssClass="btn btn-sm btn-success" Text="Close" OnClick ="btnClose_Click" BackColor="#0099FF"   />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
