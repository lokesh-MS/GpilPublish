<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="BaleTrack.aspx.cs" Inherits="GPILWebApp.CrystalReport.WebForms.BaleTrack" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="header">
        <h2 style="text-align: center; font-weight: bold; font-family: 'Times New Roman'; color: #438EB9">Bale Track</h2>
    </div>

    <div class="row">
        <div class="col-sm-3">
            <div class="form-sm-3-md-4">
                <label class="control-label">Bale Number</label>
                <div class="form-control-sm">
                    <asp:TextBox ID="txtBaleNumber" runat="server" class="form-control">                            
                    </asp:TextBox>
                </div>
            </div>
        </div>
         <label></label>
        <div class="col-sm-3">
            <div class="form-sm-3-md-4">
                 <label></label>
                <div class="form-control-sm">
                   
                    <asp:Button ID="btnView" runat="server" CssClass="btn btn-sm btn-success" Text="View" OnClick="btnView_Click" />
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 mb-0" style="width: 100%">
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" HasCrystalLogo="False" />
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            </CR:CrystalReportSource>
        </div>
</asp:Content>
