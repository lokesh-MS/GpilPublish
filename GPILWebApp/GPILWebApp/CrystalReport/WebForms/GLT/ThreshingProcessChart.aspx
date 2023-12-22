<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ThreshingProcessChart.aspx.cs" Inherits="GPILWebApp.CrystalReport.WebForms.GLT.ThreshingProcessChart" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="header">
        <h2 style="text-align:center; font-weight:bold;font-family:'Times New Roman'; color:#438EB9">Threshing Process Chart</h2>
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
                <label class="control-label">Process Organization</label>
                <div>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlToOrgnCode" AutoPostBack="true" OnSelectedIndexChanged="ddlToOrgnCode_SelectedIndexChanged">
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
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCrop" AutoPostBack="true" OnSelectedIndexChanged="ddlCrop_SelectedIndexChanged">
                        <asp:ListItem Value="0">Select To Crop</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>


        <div class="col-sm-4">
              <div class="form-sm-3-md-4">
                <label class="control-label">Variety</label>
                <div>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlVariety" AutoPostBack="true" OnSelectedIndexChanged="ddlVariety_SelectedIndexChanged">
                        <asp:ListItem Value="0">Select To Variety</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>



        <div class="col-sm-4">
              <div class="form-sm-3-md-4">
                <label class="control-label">Batch Number</label>
                <div>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlBatchNumber" AutoPostBack="true">
                        <asp:ListItem Value="0">Select To Batch Number</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
    </div>


     <div class="row">
         <label></label>
         </div>

    <div class="row">
        <div class="col-sm-4">
            </div>
         <div class="col-sm-4">
        <asp:RadioButton ID="rdbThreshingIssueDetails" runat="server" Text="Threshing Issue Details" AutoPostBack="true"  />
        <asp:RadioButton ID="rdbProcessChart" runat="server" Text="Process Chart" AutoPostBack="true" />
             </div>
        <div class="col-sm-4">
            </div>

    </div>


     <div class="row">
            <div class="col-sm-3">
                <div><label></label></div>
                <div class="form-control-sm">
                    <asp:Button ID="btnview" runat="server" CssClass="btn btn-sm btn-success" Text="View" onclick="btnview_Click"/>
                    <asp:Button ID="btnclose" runat="server" CssClass="btn btn-sm btn-success" Text="Clear" onclick="btnclose_Click"/>

                </div>
            </div>

            <div class="col-sm-3">

                <div class="form-control-sm">
                    

                </div>
            </div>

        </div>




    <div class="col-sm-12 mb-0" style="width: 100%">
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" HasCrystalLogo="False" />
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            </CR:CrystalReportSource>
        </div>
</asp:Content>
