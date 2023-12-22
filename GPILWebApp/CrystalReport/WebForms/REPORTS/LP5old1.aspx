<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="LP5old1.aspx.cs" Inherits="GPILWebApp.CrystalReport.WebForms.REPORTS.LP5old1" %>
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
                            Text="*"> </asp:RequiredFieldValidator><%--<asp:RegularExpressionValidator ID="RevMDate"
                                runat="server" ControlToValidate="txtFromDate" ErrorMessage="Date Format Invalid,Enter Valid Date (DD-MM-YYYY)"
                                Text="*" ValidationExpression="^(([1-9])|(0[1-9])|[12][0-9]|3[01])[- /.](([1-9])|(0[1-9])|1[012])[- /.](19|20)\d\d$"
                                Display="Dynamic" ForeColor="Red"> </asp:RegularExpressionValidator>--%>
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
                        <asp:DropDownList runat="server" AutoPostBack="true" CssClass="form-control" ID="ddlToOrgnCode" OnSelectedIndexChanged ="ddlToOrgnCode_SelectedIndexChanged">
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
                    <asp:Button ID="btnView" runat="server" CssClass="btn btn-sm btn-success" Text="View Report" OnClick="btnView_Click" />

                </div>
            </div>
        </div>




        <h4 class="header green">Farmer Purchase Slip</h4>

        <div class="col-sm-12" style="width: 100%">
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                AutoDataBind="True" Height="1123px" 
                Width="773px" ToolPanelView="None"  />
               
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                <Report FileName="Reports\RptClassificationchart.rpt">
                </Report>
            </CR:CrystalReportSource>

              <CR:CrystalReportViewer ID="CrystalReportViewer2" runat="server" 
                AutoDataBind="True" Height="1123px" 
                Width="773px" ToolPanelView="None"  />
               
            <CR:CrystalReportSource ID="CrystalReportSource2" runat="server">
                <Report FileName="Reports\RptClassificationchart.rpt">
                </Report>
            </CR:CrystalReportSource>


        </div>

       

    </div>
</asp:Content>
