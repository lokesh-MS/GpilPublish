<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ClassificationCumulativeTodayVSTodateReport.aspx.cs" Inherits="GPILWebApp.ClassificationCumulativeTodayVSTodateReport" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-header">
        <h1>Classification Cumulative Today Vs Todate Report</h1>
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
                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">Classification Date</label>
                                        <asp:TextBox ID="txtClassificationDate" runat="server" class="form-control" TextMode="Date">
                            
                                        </asp:TextBox>
                                    </div>

                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">Select Crop</label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCrop">
                                            <asp:ListItem Value="0">SELECT CROP</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">Select Variety</label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlVariety">
                                            <asp:ListItem Value="0">SELECT VARIETY</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">Select OrganiZation</label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlOrganization">
                                            <asp:ListItem Value="0">SELECT ORGANIZATION</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-2 mb-2">
                                        <asp:Button ID="btnReport" runat="server" CssClass="btn btn-sm btn-success" Text="View Report" OnClick="btnReport_Click" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="btn btn-sm btn-success" Text="Clear" OnClick="btnClear_Click" />
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
