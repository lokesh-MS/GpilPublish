<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ChemistryReports.aspx.cs" Inherits="GPILWebApp.ChemistryReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1 style="font-weight: bold; font-family: Cambria;">Chemistry Report</h1>
    </div>
    <div class="container-fluid">
        <div class="row">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <h6 class="card-title">Searching Criteria</h6>
                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-4 mb-4">
                                        <label for="form-field-3">From Date</label>
                                        <asp:TextBox ID="txtFromDate" runat="server" class="form-control" TextMode="Date">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-md-4 mb-4">
                                        <label for="form-field-3">To Date</label>
                                        <asp:TextBox ID="txtToDate" runat="server" class="form-control" TextMode="Date">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-md-4 mb-4">
                                        <label for="form-field-3">Crop</label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCrop">
                                            <asp:ListItem Value="0">SELECT </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-4 mb-4">
                                        <label for="form-field-3">Grade</label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlGrade">
                                            <asp:ListItem Value="0">SELECT </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                     <div class="col-md-4 mb-4">
                                        <label for="form-field-3">Variety</label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlVariety">
                                            <asp:ListItem Value="0">SELECT </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-sm btn-success" Text="Submit " OnClick ="btnSubmit_Click"  BackColor="#0099FF"   />
                                    <asp:Button ID="btnClose" runat="server" CssClass="btn btn-sm btn-success" Text="Close" OnClick ="btnClose_Click"  BackColor="#0099FF"   />
                                    <asp:Button ID="btnExportToExcel" runat="server" CssClass="btn btn-sm btn-success" Text="Export To Excel" OnClick="btnExportToExcel_Click"   BackColor="#0099FF"   />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    Submit
</asp:Content>
