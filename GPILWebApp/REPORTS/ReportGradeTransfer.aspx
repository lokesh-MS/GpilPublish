<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ReportGradeTransfer.aspx.cs" Inherits="GPILWebApp.REPORTS.ReportGradeTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1 style="font-weight: bold; font-family: Cambria;">GRADE TRANSFER REPORT</h1>
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
                                                    <asp:TextBox ID="txtToDate" runat="server" class="form-control" TextMode="Date">                            
                                                    </asp:TextBox>

                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4">
                                            <div class="form-sm-3">
                                                <label class="control-label">Crop Year</label>
                                                <div>
                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCropYear">
                                                        <asp:ListItem Value="0">SELECT CROP </asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>


                                    </div>

                                    <div class="row mb-0">
                                        <div class="col-sm-4">
                                            <div class="form-sm-3">
                                                <label class="control-label">Variety</label>
                                                <div class="form-control-sm">
                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlVariety" AutoPostBack="True">
                                                        <asp:ListItem Value="0">SELECT VARIETY</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-sm-4">
                                            <div class="form-sm-3">
                                                <label class="control-label">Organization Code </label>
                                                <div>
                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlOrgnCode">
                                                        <asp:ListItem Value="0">SELECT ORGN CODE</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>




                        <div class="row mb-0">

                            <div class="col-sm-4">
                                <div class="form-sm-3">

                                    <div>
                                        <asp:Button ID="btnView" runat="server" CssClass="btn btn-sm btn-success" Text="View " OnClick="btnView_Click" BackColor="#0099FF" />
                                        <asp:Button ID="btnClose" runat="server" CssClass="btn btn-sm btn-success" Text="Close" OnClick="btnClose_Click" BackColor="#0099FF" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                </div>
            </div>
        </div>
    </div>
</asp:Content>
