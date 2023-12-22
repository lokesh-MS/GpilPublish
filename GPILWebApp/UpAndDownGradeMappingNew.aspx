<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="UpAndDownGradeMappingNew.aspx.cs" Inherits="GPILWebApp.UpAndDownGradeMappingNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-header">
        <h1>Up & Down Grade Mapping New</h1>
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
                                        <label for="form-field-3">Crop Year</label>
                                        <asp:DropDownList ID="ddlCropYear" runat="server" class="form-control" />
                                    </div>


                                    <div class="col-md-4 mb-4">
                                        <label for="form-field-3">Variety</label>
                                        <asp:DropDownList ID="ddlVariety" runat="server" class="form-control" />
                                    </div>


                                </div>
                            </div>
                        </div>


                         <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-2 mb-2">
                                        <asp:Button ID="btnView" runat="server" Text="View" class="btn btn-info rounded-pill px-4 mt-3" OnClick="btnView_Click"    />
                                        
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
