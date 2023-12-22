<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="FarmerPurchaseSlipDatewise.aspx.cs" Inherits="GPILWebApp.FarmerPurchaseSlipDatewise" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="header">
        <h2 style="text-align: center; font-weight: bold; font-family: 'Times New Roman'; color: #438EB9">Farmer Purchase Slip with Loan</h2>
    </div>

    <div class="page-content" style="background-color: white">
        <%--  <div class="page-header">
        <h1>
								
        </h1>
    </div>--%>
        <div class="row mb-0">
            <div class="col-sm-3">
                <div class="form-sm-3-md-4">
                    <label class="control-label">Date</label>
                    <div class="form-control-sm">
                        <asp:TextBox ID="txtDate" runat="server" class="form-control" TextMode="Date">
                            
                        </asp:TextBox>
                       <%-- <span class="input-group-addon">
                            <i class="fa fa-calendar bigger-110"></i>
                        </span> --%>
                    </div>
                </div>
            </div>

            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label">Select Orgn Name </label>
                    <div>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlOrgnCode" AutoPostBack="True" OnSelectedIndexChanged="ddlOrgnCode_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label">Farmer Code</label>
                    <div class="form-control-sm">
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlFarmerCode" AutoPostBack="True" OnSelectedIndexChanged="ddlFarmerCode_SelectedIndexChanged">
                            <asp:ListItem Value="0">SELECT FAMER CODE</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>


        </div>
        <hr/>
        <%--<h4 class="header green">Farmer Purchase Slip</h4>--%>

        <div class="col-sm-12 mb-0" style="width:100%;height:100%">
            <CR:CrystalReportViewer ID="FarmerPurchaseSlipDateWise" runat="server" AutoDataBind="true" ToolPanelView="None" HasCrystalLogo="False" />
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            </CR:CrystalReportSource>
        </div>

    </div>


 <%--   <script src="~/assets/js/jquery-2.1.4.min.js"></script>
    <script>
        $(document).ready(function () {

            //datepicker plugin
            //link
            $('.date-picker').datepicker({
                autoclose: true,
                todayHighlight: true
            })
            //show datepicker when clicking on the icon
            .next().on(ace.click_event, function () {
                $(this).prev().focus();
            });
        })
    </script>--%>

</asp:Content>
