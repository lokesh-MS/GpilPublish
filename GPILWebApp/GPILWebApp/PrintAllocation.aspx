<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="PrintAllocation.aspx.cs" Inherits="GPILWebApp.PrintAllocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1 style="font-weight: bold; font-family: Cambria;">Printing Allocation</h1>
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
                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">Running Yeare</label>
                                        <asp:DropDownList ID="ddlRunningYear" runat="server" class="form-control" />
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">Grade Code</label>
                                        <asp:DropDownList ID="ddlGradeCode" runat="server" class="form-control" />
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">Run Number</label>
                                        <asp:TextBox ID="txtRunNumber" runat="server" CssClass="form-control border border-primary" MaxLength="10" required></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">Location Code</label>
                                        <asp:DropDownList ID="ddlLocationCode" runat="server" class="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-3 mb-3">
                                        <asp:Label ID="lblRunYear" runat="server" align="right" Text="Run Year"></asp:Label>
                                        <asp:Label ID="lblTRunYear" runat="server" align="left" ForeColor="Red" Visible="true"></asp:Label>
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <asp:Label ID="lblGradeName" runat="server" align="right" Text="Grade Name"></asp:Label>
                                        <asp:Label ID="lblTGradeName" runat="server" align="left" ForeColor="Red" Visible="true"></asp:Label>
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <asp:Label ID="lblLocationName" runat="server" align="right" Text="Location Name"></asp:Label>
                                        <asp:Label ID="lblTLocationName" runat="server" align="left" ForeColor="Red" Visible="true"></asp:Label>
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">From</label>
                                        <asp:TextBox ID="txtFrom" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">To</label>
                                        <asp:TextBox ID="txtTo" runat="server" class="form-control"></asp:TextBox>

                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">No Of Cases To Print</label>
                                        <asp:TextBox ID="txtNoOfCasesToPrint" runat="server" class="form-control"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <asp:Button ID="btnAllocate" runat="server" Text="Allocate" CssClass="btn btn-info rounded-pill px-4 mt-3" OnClick="btnAllocate_Click" />
                                    <asp:Button ID="btnNewRun" runat="server" Text="NewRun" CssClass="btn btn-info rounded-pill px-4 mt-3" OnClick="btnNewRun_Click" />
                                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-info rounded-pill px-4 mt-3" OnClick="btnClear_Click" />
                                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-info rounded-pill px-4 mt-3" OnClick="btnBack_Click" />
                                </div>
                            </div>
                        </div>


                    </div>
                </div>
            </div>
        </div>
    </div>







</asp:Content>
