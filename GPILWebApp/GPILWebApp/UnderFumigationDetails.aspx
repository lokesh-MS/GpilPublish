<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeFile="UnderFumigationDetails.aspx.cs" Inherits="GPILWebApp.UnderFumigationDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1 style="font-weight: bold; font-family: Cambria;">Under Fumigation Details</h1>
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
                                    <div class="col-md-3 mb-3">
                                        <asp:Label ID="lblLocationCode" runat="server" Text="Location Code "></asp:Label>
                                        <asp:DropDownList ID="ddlLocationCode" runat="server" class="form-control" />
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        </div> 
                                    <div class="col-md-3 mb-3">
                                        <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-info rounded-pill px-4 mt-3" OnClick="btnView_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                           <asp:GridView ID="gvViewSamp" runat="server" class="table table-success table-bordered"
                                                    OnRowCancelingEdit="gvViewSamp_RowCancelingEdit"  
                                                    OnRowCommand="gvViewSamp_RowCommand"   
                                                    OnRowUpdating="gvViewSamp_RowUpdating"    PageSize="10"
                                                    Width="90%" ForeColor="Black"
                                                    OnRowDataBound="gvViewSamp_RowDataBound"    AllowSorting="True" ShowFooter="True" EmptyDataText="No Data Found" AutoGenerateColumns="False" OnRowDeleting="gvViewSamp_RowDeleting"    OnRowEditing="gvViewSamp_RowEditing"   >
                                                    <AlternatingRowStyle BackColor="#F0F0F0" />
                                                    <RowStyle Font-Bold="True" Font-Names="Calibri" Font-Size="10pt" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" SortExpression="NO.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Location Code" SortExpression="ORGN_CODE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lLocationCode" ItemStyle-ForeColor="#ff0000" runat="server" Text='<%# Eval("ORGN_CODE") %>' />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtLocationCode" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqLocationCode" runat="server"
                                                                    ControlToValidate="txtLocationCode" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Case Number" SortExpression="GPIL_BALE_NUMBER">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lCaseNumber" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("GPIL_BALE_NUMBER") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtCaseNumber" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("GPIL_BALE_NUMBER") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddCaseNumber" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqCaseNumber" runat="server"
                                                                    ControlToValidate="txtAddCaseNumber" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Fumigation Starting Date" SortExpression="FUMIGATION_START_DATE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lFumigationStartingDate" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("FUMIGATION_START_DATE") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtFumigationStartingDate" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("FUMIGATION_START_DATE") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddFumigationStartingDate" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqFumigationStartingDate" runat="server"
                                                                    ControlToValidate="txtAddFumigationStartingDate" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Fumigation Ending Date" SortExpression="GradeCode">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lFumigationEndingDate" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("GradeCode") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtFumigationEndingDate" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("GradeCode") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddFumigationEndingDate" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqFumigationEndingDate" runat="server"
                                                                    ControlToValidate="txtAddFumigationEndingDate" ErrorMessage="*"
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
