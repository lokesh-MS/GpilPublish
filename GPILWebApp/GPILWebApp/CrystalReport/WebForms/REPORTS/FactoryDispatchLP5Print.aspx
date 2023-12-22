<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="FactoryDispatchLP5Print.aspx.cs" Inherits="GPILWebApp.CrystalReport.WebForms.REPORTS.FactoryDispatchLP5Print" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="page-content" style="background-color: white">

    <div class="row mb-0">
        <div class="col-sm-3">
            <asp:Label ID="lblMessage" runat="server" />
        </div>

    </div>


    <div class="row mb-0">

        <div class="col-sm-3">
            <div class="form-sm-3-md-4">
                <label class="control-label">From Date</label>
                <div class="form-control-sm">
                    <asp:TextBox ID="txtFromDate" runat="server" class="form-control" TextMode="Date">
                    </asp:TextBox>
                  
                </div>
            </div>
        </div>


        <div class="col-sm-3">
            <div class="form-sm-3">
                <label class="control-label">Select From Orgn Name </label>
                <div>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlFromOrgnCode">
                        <asp:ListItem Value="0">Select From Orgn Name</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <div class="col-sm-3">
            <div class="form-sm-3">
                <label class="control-label">Select To Orgn Name </label>
                <div>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlToOrgnCode" AutoPostBack="true" OnSelectedIndexChanged="ddlToOrgnCode_SelectedIndexChanged">
                        <asp:ListItem Value="0">Select To Orgn Name</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <div class="col-sm-3">
            <div class="form-sm-3">
                <label class="control-label">Select Shipment No</label>
                <div class="form-control-sm">
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlShipmentNumber" AutoPostBack="True">
                        <asp:ListItem Value="0">SELECT SHIPMENT NO</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
    </div>


    <div class="row">
        <div class="col-sm-3">

            <div class="form-control-sm">
                <asp:Button ID="btnView" runat="server" CssClass="btn btn-sm btn-success" Text="View Report" />

            </div>
        </div>

        <%--  <div class="col-sm-3">

                <div class="form-control-sm">
                    <asp:Button ID="btnClose" runat="server" CssClass="btn btn-sm btn-success" Text="Close" OnClick="btnClose_Click" />

                </div>
            </div>--%>
    </div>




    <h4 class="header green">Factory Dispatch LP5 Print</h4>

    <div class="col-sm-12" style="width: 100%">
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" HasCrystalLogo="False" HasExportButton="True" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
        </CR:CrystalReportSource>
    </div>

    <div class="col-sm-12" style="width: 100%">
        <CR:CrystalReportViewer ID="CrystalReportViewer2" runat="server" AutoDataBind="true" ToolPanelView="None" HasCrystalLogo="False" HasExportButton="True" />
        <CR:CrystalReportSource ID="CrystalReportSource2" runat="server">
        </CR:CrystalReportSource>
    </div>

    <div class="col-sm-12" style="width: 100%">
        <CR:CrystalReportViewer ID="CrystalReportViewer3" runat="server" AutoDataBind="true" ToolPanelView="None" HasCrystalLogo="False" HasExportButton="True" />
        <CR:CrystalReportSource ID="CrystalReportSource3" runat="server">
        </CR:CrystalReportSource>
    </div>

    <div class="col-sm-12" style="width: 100%">
        <CR:CrystalReportViewer ID="CrystalReportViewer4" runat="server" AutoDataBind="true" ToolPanelView="None" HasCrystalLogo="False" HasExportButton="True" />
        <CR:CrystalReportSource ID="CrystalReportSource4" runat="server">
        </CR:CrystalReportSource>
    </div>

</div>


</asp:Content>



