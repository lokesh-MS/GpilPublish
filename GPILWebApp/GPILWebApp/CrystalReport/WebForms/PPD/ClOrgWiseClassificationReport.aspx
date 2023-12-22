<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ClOrgWiseClassificationReport.aspx.cs" Inherits="GPILWebApp.ClOrgWiseClassificationReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="header">
        <h2 style="text-align: center; font-weight: bold; font-family: 'Times New Roman'; color: #438EB9"> CL Org Wise Classification Report</h2>
    </div>


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
                        <asp:TextBox ID="txtTodate" runat="server" class="form-control" TextMode="Date">                            
                        </asp:TextBox>

                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label">Crop</label>
                    <div class="form-control-sm">
                        <asp:DropDownList runat="server" CssClass="form-control" ID="cbxcrop">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label">Variety</label>
                    <div class="form-control-sm">
                        <asp:DropDownList runat="server" CssClass="form-control" ID="cbxvariety">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label">Select Orgn Name </label>
                    <div>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="cbxorgcd"></asp:DropDownList>
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
                    <asp:Button ID="btnViewReport" runat="server" CssClass="btn btn-sm btn-success" Text="View Report" OnClick="btnView_Click" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-sm btn-success" Text="Clear" OnClick="btnClose_Click" />

                </div>
            </div>

            <div class="col-sm-3">

                <div class="form-control-sm">
                </div>
            </div>

        </div>



         <hr />

        <div class="col-sm-12 mb-0" style="width: 100%">
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" HasCrystalLogo="False" />
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            </CR:CrystalReportSource>
        </div>




        <div>
            <asp:Label ID="lblMessage" ForeColor="Red" BackColor="Yellow" Font-Size="Large" Font-Bold="true" runat="server" Text=""></asp:Label>
        </div>

    </div>



</asp:Content>
