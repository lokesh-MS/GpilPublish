<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ReportsLP5Print.aspx.cs" Inherits="GPILWebApp.ReportsLP5Print1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <div class="page-header">
        <h1 style="font-weight: bold; font-family: Cambria;">LP5 PRINT</h1>
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
                                  <%--  <div class="col-md-3 mb-3">
                                       
                                        <label for="form-field-3">From Date</label>
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control border border-primary" MaxLength="10" TextMode="Date" required></asp:TextBox>
                                    </div>

                                   

                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">From Org</label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlFromOrg">
                                            <asp:ListItem Value="0">SELECT </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">To Org</label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlToOrg">
                                            <asp:ListItem Value="0">SELECT </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                     <div class="col-md-3 mb-3">
                                        <label for="form-field-3">Select Shipment No</label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlShipmentNo">
                                            <asp:ListItem Value="0">SELECT </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>--%>

                                    <div class="row mb-0">

            <div class="col-sm-3">
                <div class="form-sm-3-md-4">
                    <label class="control-label">From Date</label>
                    <div class="form-control-sm">
                        <asp:TextBox ID="txtFromDate" runat="server" class="form-control" TextMode="Date">                            
                        </asp:TextBox>
                       <%-- <asp:RequiredFieldValidator ID="rfvMDate" runat="server" ControlToValidate="txtFromDate"
                            ForeColor="Red" ValidationGroup="AddEdit" Display="Dynamic" ErrorMessage="Select Date"
                            Text="*"> </asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RevMDate"
                                runat="server" ControlToValidate="txtFromDate" ErrorMessage="Date Format Invalid,Enter Valid Date (DD-MM-YYYY)"
                                Text="*" ValidationExpression="^(([1-9])|(0[1-9])|[12][0-9]|3[01])[- /.](([1-9])|(0[1-9])|1[012])[- /.](19|20)\d\d$"
                                Display="Dynamic" ForeColor="Red"> </asp:RegularExpressionValidator>--%>
                    </div>
                </div>
            </div>


            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label">Select From Orgn Name </label>
                    <div>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlFromOrgnCode">
                             <asp:ListItem Value="0">Select From Orgn Name</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label">Select To Orgn Name </label>
                    <div>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlToOrgnCode" OnSelectedIndexChanged="ddlToOrgnCode_SelectedIndexChanged" >
                             <asp:ListItem Value="0">Select To Orgn Name</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label">Select Shipment No</label>
                    <div class="form-control-sm">
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlShipmentNumber" AutoPostBack="True">
                            <asp:ListItem Value="0">SELECT SHIPMENT NO</asp:ListItem>
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
                                    <asp:Button ID="btnView" runat="server" CssClass="btn btn-sm btn-success" Text="View " OnClick="btnView_Click"  BackColor="#0099FF"   />
                                    <asp:Button ID="btnClose" runat="server" CssClass="btn btn-sm btn-success" Text="Close" OnClick="btnClose_Click"  BackColor="#0099FF"   />
                                </div>
                            </div>
                        </div>







                </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
