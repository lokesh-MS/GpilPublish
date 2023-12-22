<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeFile="FumigationReceipt.aspx.cs" Inherits="GPILWebApp.FumigationReceipt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1 style="font-weight: bold; font-family: Cambria;">Fumigation Receipt</h1>
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
                                        <asp:Label ID="lblLocationCode" runat="server" Text="Location Code"></asp:Label>
                                        <asp:DropDownList ID="ddlLocationCode" runat="server" class="form-control" />                                          
                                        </div> 
                                    <div class="col-md-3 mb-3">
                                        <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-info rounded-pill px-4 mt-3" OnClick ="btnView_Click"  />
                                        </div> 
                                        </div> </div> </div> 
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
