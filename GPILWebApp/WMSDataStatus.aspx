<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeFile="WMSDataStatus.aspx.cs" Inherits="GPILWebApp.WMSDataStatus" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1 style="font-weight: bold; font-family: Cambria;">WMS Data Status</h1>
    </div>
    <div class="container-fluid">
        <div class="row">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">
                        <h4 class="card-title">Searching Criteria</h4>
                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-4 mb-4">
                                        <asp:Label ID="lblFromDate" runat="server" Text="From Date"></asp:Label>
                                        <asp:TextBox ID="txtFromDate" runat="server" class="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4 mb-4">
                                        <asp:Label ID="lblToDate" runat="server" Text="To Date"></asp:Label>
                                        <asp:TextBox ID="txtToDate" runat="server" class="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2 mb-2">
                                    </div>
                                    <div class="col-md-2 mb-2">
                                        <asp:Button ID="btnGetLPP5" runat="server" Text="Get LP5" CssClass="btn btn-info rounded-pill px-4 mt-3" OnClick="btnGetLPP5_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-4 mb-4">
                                        <asp:Label ID="lblSelectLP5No" runat="server" Text="Select LP5 No"></asp:Label>
                                        <asp:DropDownList ID="ddlSelectLP5No" runat="server" class="form-control" />
                                    </div>
                                    <div class="col-md-2 mb-2">
                                    </div>
                                    <div class="col-md-4 mb-4">
                                        <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-info rounded-pill px-4 mt-3" OnClick="btnView_Click" />
                                    </div>
                                    <div class="col-md-2 mb-2">
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:GridView ID="GridViewSamp" runat="server" class="table table-success table-bordered"
                            OnRowCancelingEdit="GridViewSamp_RowCancelingEdit"
                            OnRowCommand="GridViewSamp_RowCommand"
                            OnRowUpdating="GridViewSamp_RowUpdating" PageSize="10"
                            Width="90%" ForeColor="Black"
                            OnRowDataBound="GridViewSamp_RowDataBound" AllowSorting="True" ShowFooter="True" EmptyDataText="No Data Found" AutoGenerateColumns="False" OnRowDeleting="GridViewSamp_RowDeleting" OnRowEditing="GridViewSamp_RowEditing">
                            <AlternatingRowStyle BackColor="#F0F0F0" />
                            <RowStyle Font-Bold="True" Font-Names="Calibri" Font-Size="10pt" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" SortExpression="NO.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LP5 Number" SortExpression="LP5No">
                                    <ItemTemplate>
                                        <asp:Label ID="lLP5Number" ItemStyle-ForeColor="#ff0000" runat="server" Text='<%# Eval("LP5No") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtLP5Number" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqLP5Number" runat="server"
                                            ControlToValidate="txtLP5Number" ErrorMessage="*"
                                            ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Case Number" SortExpression="CaseBarCode">
                                    <ItemTemplate>
                                        <asp:Label ID="lCaseNumber" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("CaseBarCode") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCaseNumber" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("CaseBarCode") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtAddCaseNumber" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqCaseNumber" runat="server"
                                            ControlToValidate="txtAddCaseNumber" ErrorMessage="*"
                                            ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lStatus" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("Status") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("Status") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtAddStatus" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqStatus" runat="server"
                                            ControlToValidate="txtAddStatus" ErrorMessage="*"
                                            ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-3 mb-3">
                                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-info rounded-pill px-4 mt-3" OnClick="btnClear_Click" />
                                        <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="btn btn-info rounded-pill px-4 mt-3" OnClick="btnExit_Click" />
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
