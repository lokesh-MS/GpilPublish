<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ThreshingBatchComplete.aspx.cs" Inherits="GPILWebApp.ThreshingBatchComplete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-header">
        <h1 style="font-weight: bold; font-family: Cambria;">Threshing Batch Completeion</h1>
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


                                    <div class="col-md-10 mb-10">
                                        <label for="form-field-3">Batch Ref No</label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlBatchRefNo">
                                            <asp:ListItem Value="0">SELECT </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 mb-2">
                                        <asp:Button ID="btnView" runat="server" CssClass="btn btn-sm btn-success" Text="View " OnClick="btnView_Click"  BackColor="#0099FF" />
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
