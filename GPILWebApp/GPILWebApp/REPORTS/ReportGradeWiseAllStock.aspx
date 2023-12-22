<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ReportGradeWiseAllStock.aspx.cs" Inherits="GPILWebApp.REPORTS.ReportGradeWiseAllStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-header">
        <h1 style="font-weight: bold; font-family: Cambria;">GRADE WISE ALL STOCK</h1>
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
                                        <div class="col-sm-3">
                                            <div class="form-sm-3-md-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-sm-3-md-4">

                                                <asp:RadioButton ID="rdbtnTapStock" Text="Tap Stock" runat="server" />

                                            </div>
                                        </div>



                                        <div class="col-sm-3">
                                            <div class="form-sm-3-md-4">

                                                <asp:RadioButton ID="rdbtnOtherStock" Text="Other Stock" runat="server" />

                                            </div>
                                        </div>

                                        <div class="col-sm-3">
                                            <div class="form-sm-3-md-4">
                                            </div>
                                        </div>


                                    </div>
                                </div>
                            </div>
                        </div>



                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">


                                    <div class="row mb-0">

                                        <div class="col-sm-4">
                                            <div class="form-sm-3">
                                                <label class="control-label">Crop Year </label>
                                                <div>
                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCropYear">
                                                        <asp:ListItem Value="0">Select Crop Year </asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4">
                                            <div class="form-sm-3">
                                                <label class="control-label">Variety </label>
                                                <div>
                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlVariety">
                                                        <asp:ListItem Value="0">Select Variety </asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4">
                                            <div class="form-sm-3">
                                                <label class="control-label">Select  Orgn Code </label>
                                                <div>
                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlOrgnCode">
                                                        <asp:ListItem Value="0">Select  Orgn Code </asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>


                                    </div>
                                </div>
                            </div>
                        </div>



                        <div class="row">
                            <div class="widget-body">
                            </div>
                        </div>


                        <div class="row">
                            <div class="widget-body">
                                <label></label>
                            </div>
                        </div>



                        <div class="row">
                            <div class="widget-body">
                                <label></label>
                            </div>
                        </div>


                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">

                                    <div class="col-sm-4">
                                    </div>
                                    <div class="col-sm-1">
                                    </div>

                                    <div class="col-sm-2">

                                        <asp:Button ID="btnView" runat="server" CssClass="btn btn-sm btn-success" Text="View " OnClick="btnView_Click" BackColor="#0099FF" />


                                        <asp:Button ID="btnClose" runat="server" CssClass="btn btn-sm btn-success" Text="Close" OnClick="btnClose_Click" BackColor="#0099FF" />


                                    </div>

                                </div>
                                <div class="col-sm-1">
                                </div>


                                <div class="col-sm-4">
                                </div>
                            </div>
                        </div>









                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
