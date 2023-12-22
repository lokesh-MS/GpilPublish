<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="GradingBatchDetails.aspx.cs" Inherits="GPILWebApp.GradingBatchDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-header">
        <h1>Grading Issue & Out-Turn Details</h1>
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
                                    <div class="col-mb-3">
                                        <label for="form-field-3">Batch No</label>
                                        <asp:TextBox ID="txtBatchNo" runat="server" class="form-control">
                                        </asp:TextBox>
                                    </div>
                                    <div class="row">
                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <div class="col-md-3 mb-3">
                                                    <asp:Button ID="btnView" runat="server" CssClass="btn btn-sm btn-success" Text="View" OnClick="btnView_Click" Style="left: 0px; top: 0px; width: 149px" />
                                                </div>
                                                <div class="col-md-3 mb-3">
                                                    <asp:Button ID="btnClear" runat="server" CssClass="btn btn-sm btn-success" Text="Clear" OnClick="btnClear_Click" Style="left: 0px; top: 0px; width: 149px" />
                                                </div>
                                                <div class="col-md-3 mb-3" style="left: 0px; top: 0px">
                                                    <asp:Button ID="btnExportToExcel" runat="server" CssClass="btn btn-sm btn-success" Text="Export To Excel" OnClick="btnExportToExcel_Click" Style="left: 0px; top: 0px; width: 146px" />
                                                </div>
                                            </div>
                                            <%--<asp:Button ID="btnView" runat="server" CssClass ="btn btn-sm btn-success" Text="View"  OnClick ="btnView_Click" />--%>
                                            <%-- <asp:Button ID="btnClose" runat="server" CssClass="btn btn-sm btn-success"   Text="Close" OnClick ="btnClose_Click"  />--%>
                                        </div>
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
