<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="FPSlip.aspx.cs" Inherits="GPILWebApp.FPSlip" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-header">
        <h1>Farmer Purchase Slip
								
        </h1>
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
                                        <label for="form-field-3">Select Orgn Name</label>

                                        <asp:DropDownList ID="ddlOrganizationCode" runat="server" class="form-control " AutoPostBack="true" OnSelectedIndexChanged="ddlOrganizationCode_SelectedIndexChanged" />
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">Select Farmer Name</label>

                                        <asp:DropDownList ID="ddlFarmerCode" runat="server" class="form-control " AutoPostBack="true" OnSelectedIndexChanged="ddlFarmerCode_SelectedIndexChanged" />
                                    </div>

                                     <div class="col-md-3 mb-3">
                                        <label for="form-field-3">Total No. Of Lots</label>
                                         <asp:Label ID="lblTotalLots" runat="server" CssClass="form-control label-default" Font-Bold="true" BackColor="#438EB9" BorderStyle="None" ForeColor="#FFFFFF" Font-Size="15px" Text="" />
                                        
                                    </div>

                                     <div class="col-md-3 mb-3">
                                        <label for="form-field-3">TotalNumberofBales</label>                                       
                                         <asp:Label ID="lblTotalNumberofBales" runat="server" CssClass="form-control label-default" BackColor="#438EB9" BorderStyle="None" Font-Bold="true" ForeColor="#FFFFFF" Font-Size="15px" Text="" />
                                    </div>

                                </div>
                            </div>

                        </div>
                    </div>
                </div>


            </div>
        </div>
    </div>
       <asp:UpdatePanel ID="upMain" runat="server" UpdateMode="Always">
        <ContentTemplate>
    <div class="container-fluid">
        <div class="row">
            <div class="col-12">
                <div class="card">                  
                    <div class="card-body">
                        <h3 class="card-title">Purchase Slip</h3>
                        <div class="row">
                            <%-- <iframe id="report" --%>
                            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server"
                                AutoDataBind="True" HasCrystalLogo="False" />
                            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                                <Report FileName="Reports\RptClassificationchart.rpt">
                                </Report>
                            </CR:CrystalReportSource>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
       </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
