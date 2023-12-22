<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ThreshingHourlyBaseBlendRatios.aspx.cs" Inherits="GPILWebApp.ThreshingHourlyBaseBlend_Ratios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1 style="font-weight: bold; font-family: Cambria;">Threshing Hourly Base Blend Ratio's Report</h1>
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
                                        <label for="form-field-3">Crop Year</label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCropYear">
                                            <asp:ListItem Value="0">SELECT </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                        <div class="col-md-3 mb-3">
                                        <label for="form-field-3">Variety </label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlVariety">
                                            <asp:ListItem Value="0">SELECT </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">Organization Code </label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlOrganizationCode">
                                            <asp:ListItem Value="0">SELECT </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">Batch Ref No </label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlBatchRefNo">
                                            <asp:ListItem Value="0">SELECT </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                    
                    </div> </div> </div> 


                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <asp:Button ID="btnView" runat="server" CssClass="btn btn-sm btn-success" Text="View " OnClick ="btnView_Click"    BackColor="#0099FF"   />
                                    <asp:Button ID="btnClose" runat="server" CssClass="btn btn-sm btn-success" Text="Close" OnClick ="btnClose_Click"   BackColor="#0099FF"   />
                                </div>
                            </div>
                        </div>



                    </div> </div> </div> </div> </div> 


</asp:Content>
