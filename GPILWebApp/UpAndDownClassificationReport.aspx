<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true"  EnableEventValidation="false" CodeBehind="UpAndDownClassificationReport.aspx.cs" Inherits="GPILWebApp.UpAndDownClassificationReport" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="header">
        <h2 style="text-align: center; font-weight: bold; font-family: 'Times New Roman'; color: #438EB9">Up & Down Classification Report</h2>
    </div>

    <div class="container-fluid">
        <div class="row">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                      <%--  <h6 class="card-title">Searching Criteria</h6>--%>
                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">From Date</label>
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control border border-primary" MaxLength="10" TextMode="Date" required></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">ToDate</label>
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control border border-primary" MaxLength="10" TextMode="Date" required></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">Crop Year</label>
                                        <asp:DropDownList ID="ddlCropYear" runat="server" class="form-control" />
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">Variety</label>
                                        <asp:DropDownList ID="ddlVariety" runat="server" class="form-control" />
                                    </div>

                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-3 mb-3">
                                        <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-sm btn-success" OnClick="btnView_Click"  />
                                        <asp:Button ID="btnClose" runat="server" Text="Clear" CssClass="btn btn-sm btn-danger" OnClick="btnClose_Click" /><%----%>

                                        <asp:Label ID="lblMessage"  ForeColor="Red" BackColor="Yellow" Font-Size=Large Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>


                         <hr />

                         <asp:UpdatePanel ID="upMain" runat="server">
                            <ContentTemplate>

                                <div class="row">
                                    <div class="widget-body">
                                        <%--<h6 class="card-title">Details</h6>--%>
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
