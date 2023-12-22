<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CompetetionConsolitate.aspx.cs" Inherits="GPILWebApp.CompetetionConsolitate" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="header">
        <h2 style="text-align: center; font-weight: bold; font-family: 'Times New Roman'; color: #438EB9">Competition Report</h2>
    </div>

    <div class="page-content" style="background-color: white">

        <div class="row mb-0">


            <div class="col-sm-3">
                <div class="form-sm-3-md-3">
                    <label class="control-label"></label>
                    <div class="form-control-sm">
                        <asp:RadioButton runat="server" ID="rdbtodayrpt" Checked="true" GroupName="rpttype" Text="Today" ForeColor="#34a4eb" Font-Bold="true" />
                        <%-- <span class="input-group-addon">
                            <i class="fa fa-calendar bigger-110"></i>
                        </span> --%>
                    </div>
                </div>
            </div>
             <div class="col-sm-3">
     <div class="form-sm-3-md-3">
         <label class="control-label"></label>
         <div class="form-control-sm">
             <asp:RadioButton runat="server" ID="rdbtodaycomrpt" Text="Previous-Today Comp" Checked="false" GroupName="rpttype" ForeColor="#34a4eb" Font-Bold="true" />
             <%-- <span class="input-group-addon">
                 <i class="fa fa-calendar bigger-110"></i>
             </span> --%>
         </div>
     </div>
 </div>

            <div class="col-sm-3">
                <div class="form-sm-3-md-3">
                    <label class="control-label"></label>
                    <div class="form-control-sm">
                        <asp:RadioButton runat="server" ID="rdbtodaysummary" Text="Competiton Summary" Checked="false" GroupName="rpttype" ForeColor="#34a4eb" Font-Bold="true" />
                        <%-- <span class="input-group-addon">
                            <i class="fa fa-calendar bigger-110"></i>
                        </span> --%>
                    </div>
                </div>
            </div>


           




        </div>


        <div class="row mb-0">
            <div class="col-sm-3">
                <div class="form-sm-3-md-3">
                    <label class="control-label">Report Date</label>
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
                    <label class="control-label">Select Crop Year</label>
                    <div>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCrop"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label">Select Variety</label>
                    <div class="form-control-sm">
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlVariety">
                            <asp:ListItem Value="0">SELECT VARIETY</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

        </div>

        <div class="row mb-0">



            <div class="col-sm-4">
                <div class="form-sm-3">
                    <div>
                        <label class="control-label"></label>
                    </div>

                    <div class="form-control-sm">
                        <asp:Button ID="btnReport" runat="server" CssClass="btn btn-sm btn-success" Text="View Report" OnClick="btnReport_Click" />
                        <asp:Button ID="btnClear" runat="server" CssClass="btn btn-sm btn-danger" Text="Clear" OnClick="btnClear_Click" />
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

        <%--  <div class="row mb-0">
            <div class="col-sm-12">
                <div class="form-control-sm">
                    <asp:Label ID="lblText" Text="Click the View Report button to show the Reports" runat="server" />
                </div>
            </div>
        </div>
        <div class="row mb-0">

            <div class="col-sm-1">

                <div class="form-control-sm">
                    

                </div>
            </div>

            <div class="col-sm-9">

                <div class="form-control-sm">
                   

                </div>
            </div>
        </div>--%>


        <h4 class="header green">Competition Report</h4>

        <div class="col-sm-12 mb-0" style="width: 100%">
            <CR:CrystalReportViewer ID="CompetitionConsoliateRpt" runat="server" AutoDataBind="true" ToolPanelView="None" HasCrystalLogo="False" />
            <CR:CrystalReportSource ID="CompetitionConsReport" runat="server">
            </CR:CrystalReportSource>
        </div>

    </div>



</asp:Content>
