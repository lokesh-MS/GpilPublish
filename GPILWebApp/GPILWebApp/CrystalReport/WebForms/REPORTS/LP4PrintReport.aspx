<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="LP4PrintReport.aspx.cs" Inherits="GPILWebApp.CrystalReport.WebForms.REPORTS.LP4PrintReport" %>

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
                        <asp:RequiredFieldValidator ID="rfvMDate" runat="server" ControlToValidate="txtFromDate"
                            ForeColor="Red" ValidationGroup="AddEdit" Display="Dynamic" ErrorMessage="Select Date"
                            Text="*"> </asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>


            <div class="col-sm-3">
                <div class="form-sm-3-md-4">
                    <label class="control-label">To Date</label>
                    <div class="form-control-sm">
                        <asp:TextBox ID="txtToDate" runat="server" class="form-control" TextMode="Date">                            
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtToDate"
                            ForeColor="Red" ValidationGroup="AddEdit" Display="Dynamic" ErrorMessage="Select Date"
                            Text="*"> </asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>


            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label">Purchase Org.</label>
                    <div>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlPurchaseOrgnCode">
                            <asp:ListItem Value="0">Select From Orgn Name</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label">Select Supplier Name </label>
                    <div>
                        <asp:DropDownList runat="server" AutoPostBack="true" CssClass="form-control" ID="ddlSupplierCode" OnSelectedIndexChanged="ddlSupplierCode_SelectedIndexChanged">
                            <asp:ListItem Value="0">Select Supplier Name</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label">Select LP4 No</label>
                    <div class="form-control-sm">
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlLP4Number" AutoPostBack="True">
                            <asp:ListItem Value="0">SELECT LP4 NO</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>


        <div class="row">
            <div class="col-sm-3">

                <div class="form-control-sm">
                    <asp:Button ID="btnView" runat="server" CssClass="btn btn-sm btn-success" Text="View Report" OnClick="btnView_Click" />

                </div>
            </div>
        </div>

        <hr />


        <h4 class="header green">LP4 Print</h4>
        <hr />

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


    </div>

</asp:Content>
