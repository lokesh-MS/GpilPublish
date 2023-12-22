<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="GreenStockReport.aspx.cs" Inherits="GPILWebApp.GreenStockReport" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="header">
        <h2 style="text-align: center; font-weight: bold; font-family: 'Times New Roman'; color: #438EB9">Green Stock Report</h2>
    </div>

    <div class="page-content" style="background-color: white">

        <div class="row mb-0">
            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label">Crop</label>
                    <div class="form-control-sm">
                        <asp:DropDownList ID="ddlCropYear" runat="server" class="form-control" required />
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label">Variety</label>
                    <div class="form-control-sm">
                        <asp:DropDownList ID="ddlVariety" runat="server" class="form-control" required />
                    </div>
                </div>
            </div>

            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label">Organization Code</label>
                    <div>
                        <asp:DropDownList ID="ddlSelectOrgCode" runat="server" class="form-control" required />
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
                    <asp:Button ID="btnView" runat="server" CssClass="btn btn-sm btn-success" Text="View" OnClick="btnView_Click" />
                    <asp:Button ID="btnClose" runat="server" CssClass="btn btn-sm btn-danger" Text="clear" OnClick="btnClose_Click" />


                </div>
            </div>

           

        </div>

         <hr />

            <div class="col-sm-12 mb-0" style="width: 100%">
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" HasCrystalLogo="False" />
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            </CR:CrystalReportSource>

        </div>

    </div>



</asp:Content>
