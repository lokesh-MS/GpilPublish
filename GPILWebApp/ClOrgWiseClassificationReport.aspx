<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ClOrgWiseClassificationReport.aspx.cs" Inherits="GPILWebApp.ClOrgWiseClassificationReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1>Classification Chart Report</h1>
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
                                        <label for="form-field-3">Crop Year</label>
                                        <asp:DropDownList ID="ddlCropYear" runat="server" class="form-control" required='' />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-4 mb-4">
                                        <label for="form-field-3">Variety</label>
                                        <asp:DropDownList ID="ddlVariety" runat="server" class="form-control" required />
                                    </div>
                                    <div class="col-md-4 mb-4">
                                        <label for="form-field-3">Select Org Code</label>
                                        <asp:DropDownList ID="ddlSelectOrgCode" runat="server" class="form-control" required />
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-2 mb-2">
                                       <asp:Button ID="btnView" runat="server" CssClass ="btn btn-sm btn-success" Text="View"  OnClick ="btnView_Click" />
                                      <asp:Button ID="btnClose" runat="server" CssClass="btn btn-sm btn-success"   Text="Close" OnClick ="btnClose_Click"  />
                                        
                                        
                                          <%--<asp:Button ID="btnReport" runat="server" CssClass="btn btn-sm btn-success" Text="View Report" OnClick="btnReport_Click" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="btn btn-sm btn-success" Text="Clear" OnClick="btnClear_Click" />--%>
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
