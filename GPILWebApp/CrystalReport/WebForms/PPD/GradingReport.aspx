<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="GradingReport.aspx.cs" Inherits="GPILWebApp.GradingReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="header">
        <h2 style="text-align: center; font-weight: bold; font-family: 'Times New Roman'; color: #438EB9"> Grading Report</h2>
    </div>
   
    <div class="page-content" style="background-color: white">


        <div class="row mb-0">
            <div class="col-sm-4">
                <div class="form-sm-3-md-4">
                    <label class="control-label">From Date</label>
                    <div class="form-control-sm">
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control border border-primary" MaxLength="10" TextMode="Date" required></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="col-sm-4">
                <div class="form-sm-3-md-4">
                    <label class="control-label">To Date</label>
                    <div class="form-control-sm">
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control border border-primary" MaxLength="10" TextMode="Date" required></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="col-sm-4">
                <div class="form-sm-3-md-4">
                    <label class="control-label">Crop Year</label>
                    <div class="form-control-sm">
                        <asp:DropDownList ID="ddlCropYear" runat="server" class="form-control" />
                    </div>
                </div>
            </div>
        </div>




        <div class="row mb-0">
            <div class="col-sm-4">
                <div class="form-sm-3-md-4">
                    <label class="control-label">Variety</label>
                    <div class="form-control-sm">
                        <asp:DropDownList ID="ddlVariety" runat="server" class="form-control" />
                    </div>
                </div>
            </div>

            <div class="col-sm-4">
                <div class="form-sm-3-md-4">
                    <label class="control-label">Operation Receipe</label>
                    <div class="form-control-sm">
                        <asp:DropDownList ID="ddlOperationReceipe" runat="server" class="form-control" />

                    </div>
                </div>
            </div>

            <div class="col-sm-4">
                <div class="form-sm-3-md-4">
                    <label class="control-label">Organization Code</label>
                    <div class="form-control-sm">
                        <asp:DropDownList ID="ddlOrganizationCode" runat="server" class="form-control" />
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-3">
                <div>
                    <label class="control-label"></label>
                </div>
                <div class="form-control-sm">
                    <asp:Button ID="btnViewReport" runat="server" Text="View" CssClass="btn btn-sm btn-success" OnClick="btnViewReport_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Clear" CssClass="btn btn-sm btn-danger" OnClick="btnCancel_Click" />

                </div>
            </div>

            <div class="col-sm-3">

                <div class="form-control-sm">
                </div>
            </div>
        </div>



        <hr />
        <div class="col-sm-12 mb-0" style="width: 100%">
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" HasCrystalLogo="False" />
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            </CR:CrystalReportSource>
        </div>
        <div>
            <asp:Label ID="lblMessage" ForeColor="Red" BackColor="Yellow" Font-Size="Large" Font-Bold="true" runat="server" Text=""></asp:Label>
        </div>
    </div>
</asp:Content>
