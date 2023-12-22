<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeFile="FumigatedCaseDetails.aspx.cs" Inherits="GPILWebApp.FumigatedCaseDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1 style="font-weight: bold; font-family: Cambria;">Fumigated Case Details</h1>
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
                                        <asp:Label ID="lblFumigatedCases" runat="server" Text="Fumigated Cases "></asp:Label>
                                        <asp:DropDownList ID="ddlFumigatedCases" runat="server" class="form-control" />
                                    </div>
                                    <div class="col-md-4 mb-4">
                                        <asp:Label ID="lblGrade" runat="server" Text="Grade  "></asp:Label>
                                        <asp:DropDownList ID="ddlGrade" runat="server" class="form-control" />
                                    </div>
                                    <div class="col-md-4 mb-4">
                                        <asp:Label ID="lblWithExpiryDays" runat="server" Text="With Expiry Days"></asp:Label>
                                        <asp:TextBox ID="txtWithExpiryDays" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-4 mb-4">
                                        <asp:Label ID="lblNoofCases" runat="server" Text="No of Cases"></asp:Label>
                                        <asp:TextBox ID="txtNoofCases" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4 mb-4">
                                        <asp:Label ID="lblFumigationExpiryDate" runat="server" Text="Fumigation Expiry Date "></asp:Label>
                                        <asp:TextBox ID="txtFumigationExpiryDate" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4 mb-4">
                                        <asp:Label ID="lblLocationCode" runat="server" Text="Location Code"></asp:Label>
                                        <asp:DropDownList ID="ddlLocationCode" runat="server" class="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-4 mb-4">
                                    </div>
                                    <div class="col-md-4 mb-4">
                                        <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-info rounded-pill px-4 mt-3" OnClick="btnView_Click" />
                                    </div>
                                    <div class="col-md-4 mb-4">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:GridView ID="gvViewSamp" runat="server" class="table table-success table-bordered"
                            OnPageIndexChanging="gvViewSamp_PageIndexChanging"
                            OnRowCancelingEdit="gvViewSamp_RowCancelingEdit"
                            OnRowCommand="gvViewSamp_RowCommand"
                            OnRowUpdating="gvViewSamp_RowUpdating" PageSize="10"
                            Width="90%" ForeColor="Black"
                            OnRowDataBound="gvViewSamp_RowDataBound" AllowSorting="True" ShowFooter="True" EmptyDataText="No Data Found" AutoGenerateColumns="False" OnRowDeleting="gvViewSamp_RowDeleting" OnRowEditing="gvViewSamp_RowEditing">
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
                                            ControlToValidate="txtID" ErrorMessage="*"
                                            ValidationGroup="txtLocationCode"></asp:RequiredFieldValidator>
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

                                <asp:TemplateField HeaderText="Grade" SortExpression="GRADE">
                                    <ItemTemplate>
                                        <asp:Label ID="lGrade" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("GRADE") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtGrade" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("GRADE") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtAddGrade" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqGrade" runat="server"
                                            ControlToValidate="txtAddGrade" ErrorMessage="*"
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
                                <asp:TemplateField HeaderText="Fumigation Expiry Date" SortExpression="FUMIGATION_EXPIRED_DATE">
                                    <ItemTemplate>
                                        <asp:Label ID="lFumigationExpiryDate" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("FUMIGATION_EXPIRED_DATE") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtFumigationExpiryDate" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("FUMIGATION_EXPIRED_DATE") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtAddFumigationExpiryDate" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqFumigationExpiryDate" runat="server"
                                            ControlToValidate="txtAddFumigationExpiryDate" ErrorMessage="*"
                                            ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Fumigation Expires in (Days)" SortExpression="FUMIGATION_EXPIRES_IN">
                                    <ItemTemplate>
                                        <asp:Label ID="lFumigationExpiresin" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("FUMIGATION_EXPIRES_IN") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtFumigationExpiresin" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("FUMIGATION_EXPIRES_IN") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtAddFumigationExpiresin" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqFumigationExpiresin" runat="server"
                                            ControlToValidate="txtAddFumigationExpiresin" ErrorMessage="*"
                                            ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                    </FooterTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
    </div>
    </div>
    </div>
        </div>
    </div>

</asp:Content>
