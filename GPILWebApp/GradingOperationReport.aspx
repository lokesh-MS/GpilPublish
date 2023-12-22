<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"  EnableEventValidation="false" CodeBehind="GradingOperationReport.aspx.cs" Inherits="GPILWebApp.GradingOperationReport" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1>Grading Operation Report</h1>
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
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control border border-primary" MaxLength="10" TextMode="Date" required></asp:TextBox>
                                    </div>
                                    <div class="col-md-4 mb-4">
                                        <label for="form-field-3">ToDate</label>
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control border border-primary" MaxLength="10" TextMode="Date" required></asp:TextBox>
                                    </div>

                                    <div class="col-md-4 mb-4">
                                        <label for="form-field-3">Crop Year</label>
                                        <asp:DropDownList ID="ddlCropYear" runat="server" class="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-4 mb-4">
                                        <label for="form-field-3">Variety</label>
                                        <asp:DropDownList ID="ddlVariety" runat="server" class="form-control" />
                                    </div>
                                    <div class="col-md-4 mb-4">
                                        <label for="form-field-3">Operation Receipe</label>
                                        <asp:DropDownList ID="ddlOperationReceipe" runat="server" class="form-control" />
                                    </div>
                                    <div class="col-md-4 mb-4">
                                        <label for="form-field-3">Organization Code</label>
                                        <asp:DropDownList ID="ddlOrganizationCode" runat="server" class="form-control" />
                                    </div>

                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-2 mb-2">
                                        <asp:Button ID="btnView" runat="server" Text="View" class="btn btn-info rounded-pill px-4 mt-3" OnClick="btnView_Click"  />
                                        <asp:Button ID="btnClose" runat="server" Text="Close" class="btn btn-info rounded-pill px-4 mt-3" OnClick ="btnClose_Click" /><%----%>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:UpdatePanel ID="upMain" runat="server">
                            <ContentTemplate>

                                <div class="row">
                                    <div class="widget-body">
                                        <h6 class="card-title">Details</h6>
                                        <div class="widget-main">
                                            <div class="col-md-12 mb-12">

                                                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" HasCrystalLogo="False" />
                                                <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                                                </CR:CrystalReportSource>



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
    </div>



</asp:Content>



