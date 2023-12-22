<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="DispatchReport.aspx.cs" Inherits="GPILWebApp.DispatchReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="page-header">

        <h2 style="text-align: center; color: #438EB9">DISPATCH REPORT</h2>

    </div>


    <div class="row">
        <div class="col-sm-4">
            <div class="form-sm-3-md-4">
                <label class="control-label">From Date</label>
                <div class="form-control-sm">
                    <asp:TextBox ID="txt_From_Date" runat="server" class="form-control" TextMode="Date">
                    </asp:TextBox>
                </div>
            </div>
        </div>
        <div class="col-sm-4">
            <div class="form-sm-3-md-4">
                <label class="control-label">To Date</label>
                <div class="form-control-sm">
                    <asp:TextBox ID="txt_To_Date" runat="server" class="form-control" TextMode="Date">
                    </asp:TextBox>
                </div>
            </div>
        </div>
        <div class="col-sm-4">
            <div class="form-sm-3-md-4">
                <label class="control-label">Organization Code</label>
                <div>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlOrgnCode">
                        <asp:ListItem Value="0">Select To Organization</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-4">
            <div class="form-sm-3-md-4">
                <label class="control-label">Crop Year</label>
                <div>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCrop">
                        <asp:ListItem Value="0">Select To Crop</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="col-sm-4">
            <div class="form-sm-3-md-4">
                <label class="control-label">Variety</label>
                <div>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlVariety">
                        <asp:ListItem Value="0">Select To Variety</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <%--<div class="col-sm-4">
              <div class="form-sm-3-md-4">
                <label class="control-label">Batch Number</label>
                <div>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlBatchNumber" AutoPostBack="true">
                        <asp:ListItem Value="0">Select To Batch Number</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>--%>
    </div>
    <div class="row">
        <label></label>
    </div>
    <%--<div class="row">
        <div class="col-sm-4">
            </div>
         <div class="col-sm-4">
        <asp:RadioButton ID="rdbThreshingIssueDetails" runat="server" Text="Threshing Issue Details" AutoPostBack="true" Checked="true" />
        <asp:RadioButton ID="rdbProcessChart" runat="server" Text="Threshing Issue Details" AutoPostBack="true" />
             </div>
        <div class="col-sm-4">
            </div>
    </div>--%>
    <div class="row">
        <div class="col-sm-3">
            <div>
                <label></label>
            </div>
            <div class="form-control-sm">
                <asp:Button ID="btnview" runat="server" CssClass="btn btn-sm btn-success" Text="View" OnClick="btnview_Click" />
                <asp:Button ID="btnclose" runat="server" CssClass="btn btn-sm btn-success" Text="Clear" OnClick="btnclose_Click" />
            </div>
        </div>
        <div class="col-sm-3">
            <div class="form-control-sm">
            </div>
        </div>
    </div>

    <hr />



    <div class="col-sm-12" style="width: 100%">
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" HasCrystalLogo="False" HasExportButton="True" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
        </CR:CrystalReportSource>
    </div>
</asp:Content>
