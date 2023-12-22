<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ThreshingHourBasedBlendRatio.aspx.cs" Inherits="GPILWebApp.ThreshingHourBasedBlendRatio" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="page-header">

        <h2 style="text-align: center; color: #438EB9">Threshing Hourly-Base Blend Ratio's Report</h2>
        <%--<h1>Threshing Hourly-Base Blend Ratio's Report
								
        </h1>--%>
    </div>



    <div class="row">
        <div class="col-md-3">

            <label>Crop</label>

            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCrop" AutoPostBack="True" OnSelectedIndexChanged="ddlCrop_SelectedIndexChanged"></asp:DropDownList>
        </div>

        <div class="col-md-3">

            <label>Variety</label>

            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlVariety" AutoPostBack="True" OnSelectedIndexChanged="ddlVariety_SelectedIndexChanged"></asp:DropDownList>
        </div>

        <div class="col-md-3">

            <label>Orgn Name</label>

            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlOrgnCode" AutoPostBack="True" OnSelectedIndexChanged="ddlOrgnCode_SelectedIndexChanged"></asp:DropDownList>
        </div>

        <div class="col-md-3">

            <label>Batch Number</label>

            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlBatchNumber" AutoPostBack="True" OnSelectedIndexChanged="ddlBatchNumber_SelectedIndexChanged"></asp:DropDownList>
        </div>

    </div>

    <div>
        <asp:RadioButton runat="server" ID="rdoIssue" 
                GroupName="BlendRatio" Text="Issue Blend Ratio" ForeColor="Black" 
                AutoPostBack="True" Checked="True" Visible="False"/>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton runat="server" ID="rdoOutturn" 
                 GroupName="BlendRatio" Text="Outturn Blend Ratio"  ForeColor="Black" 
                 AutoPostBack="True" Visible="False"/>
    </div>

    <div class="row">

        <div class="col-md-3">
            <div>
                <label></label>
            </div>
            
                    <asp:Button ID="btnview" runat="server" CssClass="btn btn-sm btn-success" Text="View" onclick="btnview_Click"/>
                    <asp:Button ID="btnclose" runat="server" CssClass="btn btn-sm btn-danger" Text="Clear" onclick="btnclose_Click"/>
<%--            <asp:Button ID="btnview" Text="View" runat="server" OnClick="btnview_Click" Width="67px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <asp:Button ID="btnclose" Text="Close" runat="server" Width="67px" OnClick="btnclose_Click" />--%>
        </div>
    </div>



      <h4 class="header green">Threshing Hourly-Base Blend Ratio's Report</h4>

        <div class="col-sm-12" style="width: 100%">
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" HasCrystalLogo="False" HasExportButton="True" />
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            </CR:CrystalReportSource>
        </div>


</asp:Content>
