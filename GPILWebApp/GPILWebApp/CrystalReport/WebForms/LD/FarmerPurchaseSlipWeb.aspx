<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="FarmerPurchaseSlipWeb.aspx.cs" Inherits="GPILWebApp.FarmerPurchaseSlipWeb" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="header">
        <h2 style="text-align: center; font-weight: bold; font-family: 'Times New Roman'; color: #438EB9">Farmer Purchase Slip</h2>
    </div>

    <%-- <div class="page-header">
        <h1>Farmer Purchase Slip
								
        </h1>
    </div>--%>

    <div class="row">
        <div class="col-md-3">

            <label>Select Orgn Name</label>

            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlOrgnCode" AutoPostBack="True" OnSelectedIndexChanged="ddlOrgnCode_SelectedIndexChanged"></asp:DropDownList>
        </div>

        <div class="col-md-3">

            <label>Select Farmer Code</label>

            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlFarmerCode" AutoPostBack="True" OnSelectedIndexChanged="ddlFarmerCode_SelectedIndexChanged">
                <asp:ListItem Value="0">SELECT FAMER CODE</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-md-3">
            <label class="control-label">Total Number of Lots :  </label>
            <asp:Label ID="lblTotalLot" Font-Bold="true" Font-Size="18px" ForeColor="Green" runat="server" CssClass="control-label" />

        </div>
        <div class="col-md-3">
            <label class="control-label">Total Number Of Bales : </label>
            <asp:Label ID="lblTotalBales" runat="server" Font-Bold="true" Font-Size="18px" ForeColor="Green" CssClass="control-label" />
        </div>




    </div>
    <div class="row">
        <label class="control-label"></label>
    </div>

    <div class="row">
        <div class="col-md-3">
            <label class="control-label"></label>
            <asp:Button ID="btnClear" runat="server" CssClass="btn btn-sm btn-danger" Text="Clear" OnClick="btnClear_Click" />
            <%--<asp:Button ID="btnClear" Text="CLEAR" runat="server" Width="111px" Height="30px" OnClick="btnClear_Click" />--%>
        </div>
    </div>


    <%--<div class="row">

            <div class="col-md-3">
                <div>
                    <label></label>
                </div>
                <button type="submit" value="Submit" id="btnSubmit" class="btn btn-sm btn-success">
                    View

                </button>
                <button type="reset" class="btn btn-sm btn-default">
                    Clear

                </button>
            </div>
        </div>--%>


    <%--<div class="row">
        <div class="col-xs-12">
           
            <div class="form-group">
                <label class="col-sm-3 control-label no-padding-right" for="form-field-1">Select Orgn Name </label>

                <div class="col-sm-3">
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlOrgnCode" AutoPostBack="True" OnSelectedIndexChanged="ddlOrgnCode_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
             <div class="form-group">
                <label class="col-sm-3 control-label no-padding-right" for="form-field-1">Select Orgn Name </label>

                <div class="col-sm-3">
                   <asp:DropDownList runat="server" CssClass="form-control" ID="ddlFarmerCode" AutoPostBack="True" OnSelectedIndexChanged="ddlFarmerCode_SelectedIndexChanged">
                            <asp:ListItem Value="0">SELECT FAMER CODE</asp:ListItem>
                        </asp:DropDownList>
                </div>
            </div>
        </div>
    </div>--%>











    <%--<div class="page-content" style="background-color: white">--%>

    <%--<div class="row mb-0">
            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label"></label>
                    <div>
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label"></label>
                    <div class="form-control-sm">
                    </div>
                </div>
            </div>


            <div class="col-sm-3">
                <div class="form-sm-3">

                    <div>
                        
                        <label class="control-label">Total Number of Lots :  </label>
                        <asp:Label ID="lblTotalLot" Font-Bold="true" Font-Size="18px" ForeColor="Green" runat="server" CssClass="control-label" />
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-sm-3">

                    <div class="form-control-sm">
                      
                        <label class="control-label">Farmer Code :</label>
                        <asp:Label ID="lblTotalBales" runat="server" Font-Bold="true" Font-Size="18px" ForeColor="Green" CssClass="control-label" />
                    </div>
                </div>
            </div>
        </div>--%>
    <%--  <asp:DropDownList runat="server" CssClass="form-control" ID="DropDownList1" AutoPostBack="True" OnSelectedIndexChanged="ddlOrgnCode_SelectedIndexChanged"></asp:DropDownList>--%>
    <%-- <asp:DropDownList runat="server" CssClass="form-control" ID="DropDownList2" AutoPostBack="True" OnSelectedIndexChanged="ddlFarmerCode_SelectedIndexChanged">
                            <asp:ListItem Value="0">SELECT FAMER CODE</asp:ListItem>
                        </asp:DropDownList>--%>
    <%--  <asp:TextBox ID="txtTotalBales" runat="server" CssClass="form-control" />--%>

    <hr />
    <%--<h4 class="header green">Farmer Purchase Slip</h4>--%>

    <div class="col-sm-12" style="width: 100%">
        <CR:CrystalReportViewer ID="FarmerPurchaseSlip" runat="server" AutoDataBind="true" ToolPanelView="None" HasCrystalLogo="False" HasExportButton="True" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
        </CR:CrystalReportSource>
    </div>





</asp:Content>
