<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AddOldStockMaster.aspx.cs" Inherits="GPILWebApp.AddOldStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-header">
        <h1 style="font-family: Cambria; font-weight: bold;">Add Old Stock</h1>
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
                                        <label for="form-field-3" style="font-family: Cambria">Old Bale Number</label>
                                        <asp:TextBox ID="txtOldBaleNumber" runat="server"  CssClass="form-control border border-primary" MaxLength="10"  required></asp:TextBox>
                                    </div>
                                    <div class="col-md-4 mb-4">
                                        <label for="form-field-3" style="font-family: Cambria">Grade Code</label>
                                        <asp:TextBox ID="txtGradeCode" runat="server"  CssClass="form-control border border-primary" MaxLength="10"  required></asp:TextBox>
                                    </div>
                                    <div class="col-md-4 mb-4">
                                        <label for="form-field-3" style="font-family: Cambria">Crop</label>
                                        <asp:TextBox ID="txtCrop" runat="server" CssClass="form-control border border-primary" MaxLength="10"  required></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-4 mb-4">
                                        <label for="form-field-3" style="font-family: Cambria">Variety</label>
                                        <asp:TextBox ID="txtVariety" runat="server" CssClass="form-control border border-primary" MaxLength="10"  required></asp:TextBox>
                                    </div>
                                    <div class="col-md-4 mb-4">
                                        <label for="form-field-3" style="font-family: Cambria">Market Weight</label>
                                        <asp:TextBox ID="txtMarketWeight" runat="server" CssClass="form-control border border-primary" MaxLength="10"  required></asp:TextBox>
                                    </div>
                                    <div class="col-md-4 mb-4">
                                        <label for="form-field-3" style="font-family: Cambria">Sub Inventory</label>
                                        <asp:DropDownList ID="ddlSubInventory" runat="server" class="form-control" />
                                    </div>

                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-3 mb-3">
                                        <asp:Button ID="btnView" style="font-family: Cambria" runat="server" Text="View" class="btn btn-info rounded-pill px-4 mt-3" OnClick="btnView_Click" />
                                        <asp:Button ID="btnClose" style="font-family: Cambria" runat="server" Text="Close" class="btn btn-info rounded-pill px-4 mt-3" OnClick="btnClose_Click" />
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
