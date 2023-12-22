<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ThreshingBatchLiveBlendRatios.aspx.cs" Inherits="GPILWebApp.ThreshingBatchLiveBlendRatios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1 style="font-weight: bold; font-family: Cambria;">Threshing Batch Live Blend Ratio's Report</h1>
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
                                        <label for="form-field-3">Organization Code </label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlOrganizationCode">
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
                                        <asp:RadioButton ID="rdbissueBlendRatio" runat="server" GroupName="rdbexporttype" Text="Issue Blend Ratio" AutoPostBack="True"
                                            OnCheckedChanged="rdbissueBlendRatio_CheckedChanged" ForeColor="Black" CssClass="form-control " />
                                    </div>
                                    <div class="col-md-4 mb-4">
                                        <asp:RadioButton ID="rdbOutturnBlendRatio" runat="server" GroupName="rdbexporttype" Text="Outturn Blend Ratio" AutoPostBack="True"
                                            OnCheckedChanged="rdbOutturnBlendRatio_CheckedChanged" ForeColor="Black" CssClass="form-control " />
                                    </div>

                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <asp:Button ID="btnView" runat="server" CssClass="btn btn-sm btn-success" Text="View " OnClick ="btnView_Click"   BackColor="#0099FF"   />
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
