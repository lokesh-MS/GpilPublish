<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ReportLotWiseStock.aspx.cs" Inherits="GPILWebApp.REPORTS.ReportLotWiseStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">




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

                                                <asp:RadioButton ID="rdbtapstk" AutoPostBack="True" Text="Tap Stock" OnCheckedChanged="rdbtapstk_CheckedChanged" runat="server" />

                                            </div>
                                        </div>



                                        <div class="col-sm-3">
                                            <div class="form-sm-3-md-4">

                                                <asp:RadioButton ID="rdbotrstk" AutoPostBack="True" Text="Other Stock" runat="server" OnCheckedChanged="rdbotrstk_CheckedChanged" />

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
                                                <label class="control-label">Crop Year</label>
                                                <div>
                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="DropDownList1">
                                                        <asp:ListItem Value="0">SELECT CROP </asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-sm-4">
                                            <div class="form-sm-3">
                                                <label class="control-label">Variety</label>
                                                <div class="form-control-sm">
                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="DropDownList2" AutoPostBack="True">
                                                        <asp:ListItem Value="0">SELECT VARIETY</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-sm-4">
                                            <div class="form-sm-3">
                                                <label class="control-label">Organization Code </label>
                                                <div>
                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="DropDownList3">
                                                        <asp:ListItem Value="0">SELECT ORGN CODE</asp:ListItem>
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
                                <div class="widget-main">

                                    <div class="row mb-0">

                            <div class="col-sm-4">
                                <div class="form-sm-3">

                                    <div>
                                        <asp:Button ID="btnView" runat="server" CssClass="btn btn-sm btn-success" Text="View" />


                                        <asp:Button ID="btnClose" runat="server" CssClass="btn btn-sm btn-success" Text="Close" />
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
        </div>
    </div>






</asp:Content>
