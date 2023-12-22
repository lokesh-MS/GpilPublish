<%@ page title="" language="C#" masterpagefile="~/Master.Master" autoeventwireup="true" codebehind="ReconciliationReport.aspx.cs" inherits="GPILWebApp.CrystalReport.WebForms.REPORTS.ReconciliationReport" %>

<%@ register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="cr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="header">
        <h2 style="text-align: center; font-weight: bold; font-family: 'Times New Roman'; color: #438EB9">Reconcilation</h2>
    </div>




    <div class="row">
        <div class="col-sm-4">
            <div class="form-sm-3-md-4">
                <label class="control-label">Crop Year</label>
                <div>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCrop">
                        <asp:ListItem Value="0">Select To Crop</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <label></label>
        <div class="col-sm-4">
            <div class="form-sm-3-md-4">
                <label class="control-label">Variety</label>
                <div>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlVariety">
                        <asp:ListItem Value="0">Select To Variety</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
            <div class="col-sm-4">
        <div class="form-sm-3-md-4">
            <label class="control-label">Orgn Code</label>
  <div>
      <asp:DropDownList runat="server" CssClass="form-control" ID="ddlOrgnCode">
          <asp:ListItem Value="0">Select To OrgnCode</asp:ListItem>
      </asp:DropDownList>
  </div>
        </div>
    </div>
    </div>
<%--    <div class="row">
        <div class="col-sm-2">
            <div class="form-sm-3-md-2">
            </div>
        </div>

        <div class="col-sm-8">
            <div class="form-sm-3-md-8">
              
            </div>
        </div>

        <div class="col-sm-2">
            <div class="form-sm-3-md-2">
            </div>
        </div>
    </div>--%>

    <div class="row">
        <div class="col-sm-3">
            <div>
                <label></label>
            </div>
            <div class="form-control-sm">
                <asp:Button ID="btnview" runat="server" CssClass="btn btn-sm btn-success" Text="View" OnClick="btnview_Click" />
                <asp:Button ID="btnclose" runat="server" CssClass="btn btn-sm btn-success" Text="Close" OnClick="btnclose_Click" />

            </div>
        </div>

        <div class="col-sm-3">

            <div class="form-control-sm">
            </div>
        </div>

    </div>




    <div class="col-sm-12 mb-0" style="width: 100%">
        <cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="true" toolpanelview="None" hascrystallogo="False" />
        <cr:crystalreportsource id="CrystalReportSource1" runat="server">
        </cr:crystalreportsource>
    </div>
 

</asp:Content>
