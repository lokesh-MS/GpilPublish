<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="SupplierPurchaseInfoReports.aspx.cs" Inherits="GPILWebApp.SupplierPurchaseInfoReports" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content" style="background-color: white">

         <div class="header">
        <h2 style="text-align: center; font-weight: bold; font-family: 'Times New Roman'; color: #438EB9">Supplier Purchase Info</h2>
    </div>
        <div class="row mb-0">
            <div class="col-sm-4">
                <div class="form-sm-3-md-4">
                    <label class="control-label">From Date</label>
                    <div class="form-control-sm">
                        <asp:TextBox ID="txtFromDate" runat="server" class="form-control" TextMode="Date">                            
                        </asp:TextBox>
                       
                    </div>
                </div>
            </div>
            <div class="col-sm-4">
                <div class="form-sm-3-md-4">
                    <label class="control-label">To Date</label>
                    <div class="form-control-sm">
                       
                        <asp:TextBox ID="txt_To_Date" runat="server" class="form-control" TextMode="Date" ontextchanged="txt_To_Date_TextChanged" AutoPostBack="True">
                         </asp:TextBox>
                        
                 
                        
                 
                       
                      
                     
                    </div>
                </div>
            </div>
            <div class="col-sm-4">
                <div class="form-sm-3">
                    <label class="control-label">Purchase Doc No</label>
                    <div class="form-control-sm">
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlPurchaseDocNo">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
           

           

        </div>



        <div></div>
        <div class="row">
            <div class="col-sm-3">
                <div><label class="control-label"></label></div>
                <div class="form-control-sm">
                    <asp:Button ID="btnview" runat="server" CssClass="btn btn-sm btn-success" Text="View Report" OnClick="btnview_Click"/>
                    <asp:Button ID="btnclose" runat="server" CssClass="btn btn-sm btn-danger" Text="Clear" OnClick="btnclose_Click"/>

                </div>
            </div>

            <div class="col-sm-3">

                <div class="form-control-sm">
                    

                </div>
            </div>

        </div>



      <hr />

        <div class="col-sm-12 mb-0" style="width: 100%">
            <CR:CrystalReportViewer ID="SupplierPurchaseInfoRpt" runat="server" AutoDataBind="true" ToolPanelView="None" HasCrystalLogo="False" />
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            </CR:CrystalReportSource>
        </div>


      

        <div>
            <asp:Label ID="lblMessage" ForeColor="Red" BackColor="Yellow" Font-Size="Large" Font-Bold="true" runat="server" Text=""></asp:Label>
        </div>

    </div>
</asp:Content>
