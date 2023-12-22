<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ThreshingBlendRatios.aspx.cs" Inherits="GPILWebApp.ThreshingBlendRatios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1 style="font-weight: bold; font-family: Cambria;">Threshing Blend Ratio's</h1>
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
                                        <label for="form-field-3">Crop Year</label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCropYear">
                                            <asp:ListItem Value="0">SELECT </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>


                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-6 mb-6">
                                        <label for="form-field-3">Variety </label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlVariety">
                                            <asp:ListItem Value="0">SELECT </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-6 mb-6">
                                        <label for="form-field-3">Packed Grade</label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlPackedGrade">
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

                                        <asp:CheckBox ID="chkPercentage" runat="server" Checked="True"
                                            OnCheckedChanged="chkPercentage_CheckedChanged" ForeColor="Black"
                                            Text="With Percentage" />
                                    </div>
                                         <div class="col-md-4 mb-4">
                                        <asp:RadioButton ID="rdbssueBlendRatio" runat="server" GroupName="rdbexporttype" Text="issue Blend Ratio" AutoPostBack="True"
                                            OnCheckedChanged="rdbssueBlendRatio_CheckedChanged"  ForeColor="Black" CssClass="form-control " />
                                    </div>                                                                   


                                    <div class="col-md-4 mb-4">
                                        <asp:RadioButton ID="rdbOutturnBlendRatio" runat="server" GroupName="rdbexporttype" Text="Outturn Blend Ratio" AutoPostBack="True"
                                            OnCheckedChanged="rdbOutturnBlendRatio_CheckedChanged"  ForeColor="Black" CssClass="form-control " />
                                    </div>

                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <asp:Button ID="btnView" runat="server" CssClass="btn btn-sm btn-success" Text="View " OnClick ="btnView_Click"  BackColor="#0099FF"   />
                                    <asp:Button ID="btnClose" runat="server" CssClass="btn btn-sm btn-success" Text="Close" OnClick ="btnClose_Click"  BackColor="#0099FF"   />
                                </div>
                            </div>
                        </div>


                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
