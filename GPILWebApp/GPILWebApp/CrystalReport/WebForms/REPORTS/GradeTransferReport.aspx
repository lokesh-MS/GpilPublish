<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="GradeTransferReport.aspx.cs" Inherits="GPILWebApp.GradeTransferReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="page-content" style="background-color: white">


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
                <div class="form-sm-3-md-4">
                    <label class="control-label">To Date</label>
                    <div class="form-control-sm">
                        <asp:TextBox ID="txtToDate" runat="server" class="form-control" TextMode="Date">                            
                        </asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label">Crop</label>
                    <div class="form-control-sm">
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCrop">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label">Variety</label>
                    <div class="form-control-sm">
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlVariety">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label">Select Orgn Name </label>
                    <div>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlOrgnCode"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <div></div>
        <div class="row">
            <div class="col-sm-3">
                <div>
                    <label class="control-label"></label>
                </div>
                <div class="form-control-sm">
                    <asp:Button ID="btnview" runat="server" CssClass="btn btn-sm btn-success" Text="View Report" OnClick="btnview_Click" />
                    <asp:Button ID="btnclose" runat="server" CssClass="btn btn-sm btn-success" Text="Clear" OnClick="btnclose_Click" />
                </div>
            </div>

            <div class="col-sm-3">

                <div class="form-control-sm">
                </div>
            </div>

        </div>

        <hr />

        <h4 class="header green">Grade Transfer Report</h4>
        <hr />



        <div class="col-sm-12 mb-0" style="width: 100%">
            <CR:CrystalReportViewer ID="rdlcGradeTransfer" runat="server" AutoDataBind="true" ToolPanelView="None" HasCrystalLogo="False" />
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            </CR:CrystalReportSource>
        </div>




        <div>
            <asp:Label ID="lblMessage" ForeColor="Red" BackColor="Yellow" Font-Size="Large" Font-Bold="true" runat="server" Text=""></asp:Label>
        </div>

    </div>

</asp:Content>
