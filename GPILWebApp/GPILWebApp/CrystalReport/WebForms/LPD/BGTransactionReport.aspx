<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="BGTransactionReport.aspx.cs" Inherits="GPILWebApp.CrystalReport.WebForms.BGTransactionReport" %>


<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="header">
        <h2 style="text-align: center; font-weight: bold; font-family: 'Times New Roman'; color: #438EB9">BG Report</h2>
    </div>

    <div class="page-content" style="background-color: white">

    


        <div class="row mb-0">
            <div class="col-sm-4">
                <div class="form-sm-3-md-4">
                    <label class="control-label">Report Date</label>
                    <div class="form-control-sm">
                        <asp:TextBox ID="txt_Report_Date" runat="server" class="form-control" TextMode="Date">
                            
                        </asp:TextBox>
                        <%-- <span class="input-group-addon">
                            <i class="fa fa-calendar bigger-110"></i>
                        </span> --%>
                    </div>
                </div>
            </div>





            <div class="col-sm-4">
                <div class="form-sm-4">
                    <label class="control-label">Select Crop Year</label>
                    <div>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="cbxcrop">
                            <asp:ListItem Value="0">SELECT Crop</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="col-sm-4">
                <div class="form-sm-4">
                    <label class="control-label">Select Variety</label>
                    <div class="form-control-sm">
                        <asp:DropDownList runat="server" CssClass="form-control" ID="cbxvariety">
                            <asp:ListItem Value="0">SELECT Variety</asp:ListItem>
                        </asp:DropDownList>
                            
                    
                    </div>
                </div>
            </div>

          <%--  <div class="col-sm-4">
                <div class="form-sm-4">
                    <label class="control-label">Select Organization</label>
                    <div class="form-control-sm">
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlOrganizationCodeT">
                            <asp:ListItem Value="0">SELECT ORGN CODE</asp:ListItem>
                        </asp:DropDownList>
                            
                    
                    </div>
                </div>
            </div>--%>

          <%--  <div class="col-sm-4">
                <div class="form-sm-4">
                    <label class="control-label">Transaction Type</label>
                    <div class="form-control-sm">
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlTransactionType">
                            <asp:ListItem Value="0">Select Transaction Type</asp:ListItem>
                            <asp:ListItem Value="1">ADDITION</asp:ListItem>
                            <asp:ListItem Value="2">TRANSFER</asp:ListItem>
                        </asp:DropDownList>
                            
                    
                    </div>
                </div>
            </div>--%>
            
        </div>

        <div class="row mb-0">
            


            <div class="col-sm-4">
                <div class="form-sm-3">
                    <div><label class="control-label"></label></div>
                    
                    <div class="form-control-sm">
                       <asp:Button ID="btnReport" runat="server" CssClass="btn btn-sm btn-success" Text="View Report" OnClick="btnReport_Click" />
                         <asp:Button ID="btnClear" runat="server" CssClass="btn btn-sm btn-success" Text="Clear" OnClick="btnClear_Click" />
                    </div>
                </div>
            </div>

            <div class="col-sm-4">
                <div class="form-sm-3">
                    <label class="control-label"></label>
                    <div class="form-control-sm">
                       
                    </div>
                </div>
            </div>
        </div>

     <h4 class="header green">BG Information Report</h4>

        <div class="col-sm-12 mb-0" style="width:100%">
            <CR:CrystalReportViewer ID="BGInformation" runat="server" AutoDataBind="true" ToolPanelView="None" HasCrystalLogo="False" />
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            </CR:CrystalReportSource>
        </div>

    </div>

</asp:Content>
