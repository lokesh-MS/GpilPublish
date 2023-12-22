<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ThreshingBatchTempDetails.aspx.cs" Inherits="GPILWebApp.ThreshingBatchTempDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1 style="font-weight: bold; font-family: Cambria;">THRESHING ISSUE & OUT-TURN DETAILS IN TEMP STORAGE</h1>
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
                                        <label for="form-field-3">Batch Number</label>
                                        <asp:TextBox ID="txtBatchNumber" runat="server" class="form-control">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-3 mb-3">
                                        <asp:Button ID="btnView" runat="server" CssClass="btn btn-sm btn-success" Text="View " OnClick="btnView_Click" BackColor="#0099FF" />
                                    </div>

                                    <div class="col-md-3 mb-3">
                                        <asp:Button ID="btnClose" runat="server" CssClass="btn btn-sm btn-success" Text="Close" OnClick="btnClose_Click" BackColor="#0099FF" />
                                    </div>

                                    <div class="col-md-3 mb-3">
                                        <asp:Button ID="btnExportToClose" runat="server" CssClass="btn btn-sm btn-success" Text="Export To Close" OnClick="btnExportToClose_Click" BackColor="#0099FF" />
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <asp:Button ID="btnAllIssue" runat="server" CssClass="btn btn-sm btn-success" Text="All Issue" OnClick="btnAllIssue_Click" BackColor="#0099FF" />
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
