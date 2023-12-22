<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" Codefile="ViewPrintAllocation.aspx.cs" Inherits="GPILWebApp.ViewPrintAllocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1 style="font-weight: bold; font-family: Cambria;">View Print Allocation</h1>
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
                                        <asp:Label ID="lblLocationCode" runat="server" Text="Location Code"></asp:Label>
                                        <asp:DropDownList ID="ddlLocationCode" runat="server" class="form-control" />
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <label for="form-field-3">Date Of Packing</label>
                                        <asp:TextBox ID="txtDateOfPacking" runat="server" class="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>



                        <asp:UpdatePanel ID="upMain" runat="server">
                            <ContentTemplate>

                                <div class="row" id="divGrid" runat="server" visible="false">
                                    <div class="widget-body">
                                        <h3 class="card-title">Details</h3>
                                        <div class="widget-main">
                                            <div class="col-md-12 mb-12">

                                                <asp:GridView ID="gvVPrinting" runat="server" class="table table-success table-bordered"
                                                    OnPageIndexChanging="gvVPrinting_PageIndexChanging" 
                                                    OnRowCancelingEdit="gvVPrinting_RowCancelingEdit" 
                                                    OnRowCommand="gvVPrinting_RowCommand" 
                                                    OnRowUpdating="gvVPrinting_RowUpdating"  PageSize="10"
                                                    Width="90%" ForeColor="Black"
                                                    OnRowDataBound="gvVPrinting_RowDataBound"  AllowSorting="True" ShowFooter="True" EmptyDataText="No Data Found" AutoGenerateColumns="False" OnRowDeleting="gvVPrinting_RowDeleting"  OnRowEditing="gvVPrinting_RowEditing" >
                                                    <AlternatingRowStyle BackColor="#F0F0F0" />
                                                    <RowStyle Font-Bold="True" Font-Names="Calibri" Font-Size="10pt" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" SortExpression="NO.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ID" SortExpression="ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lID" ItemStyle-ForeColor="#ff0000" runat="server" Text='<%# Eval("ID") %>' />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtID" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqID" runat="server"
                                                                    ControlToValidate="txtID" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Crop Year Name" SortExpression="CropYearName">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lCropYearName" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("CropYearName") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtCropYearName" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("CropYearName") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddCropYearName" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqCropYearName" runat="server"
                                                                    ControlToValidate="txtAddCropYearName" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="PM Run No" SortExpression="PMRunNo">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lPMRunNo" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("PMRunNo") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtPMRunNo" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("PMRunNo") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddPMRunNo" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqPMRunNo" runat="server"
                                                                    ControlToValidate="txtAddPMRunNo" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Grade Code" SortExpression="GradeCode">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lGradeCode" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("GradeCode") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtGradeCode" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("GradeCode") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddGradeCode" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqGradeCode" runat="server"
                                                                    ControlToValidate="txtAddGradeCode" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Case No From" SortExpression="CaseNoFrom">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lCaseNoFrom" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("CaseNoFrom") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtCaseNoFrom" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("CaseNoFrom") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddCaseNoFrom" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqCaseNoFrom" runat="server"
                                                                    ControlToValidate="txtAddCaseNoFrom" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Case No To" SortExpression="CaseNoTo">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lCaseNoTo" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("CaseNoTo") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtCaseNoTo" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("CaseNoTo") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddCaseNoTo" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqCaseNoTo" runat="server"
                                                                    ControlToValidate="txtAddCaseNoTo" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Cases To Print" SortExpression="CasesToPrint">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lCasesToPrint" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("CasesToPrint") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtCasesToPrint" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("CasesToPrint") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddCasesToPrint" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqCasesToPrint" runat="server"
                                                                    ControlToValidate="txtAddCasesToPrint" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Is Existing Stock" SortExpression="IsExistingStock">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lIsExistingStock" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("IsExistingStock") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtIsExistingStock" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("IsExistingStock") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddIsExistingStock" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqIsExistingStock" runat="server"
                                                                    ControlToValidate="txtAddIsExistingStock" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                 <%--       <asp:TemplateField HeaderText="Mobile Number" SortExpression="MOBILE_NO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lMobileNumber" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("MOBILE_NO") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("MOBILE_NO") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddMobileNumber" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqMobileNumber" runat="server"
                                                                    ControlToValidate="txtAddMobileNumber" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Mail ID" SortExpression="EMAIL_ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lMailID" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("EMAIL_ID") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtMailID" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("EMAIL_ID") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddMailID" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqMailID" runat="server"
                                                                    ControlToValidate="txtAddMailID" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Status" SortExpression="Insert Sts">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblupdatests" runat="server" Text='<%#Eval("INS_STS") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtupdatests" runat="server" CssClass="form-control border border-primary"
                                                                    Text='<%#Eval("INS_STS") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddupdatests" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqtupdatests" runat="server"
                                                                    ControlToValidate="txtAddupdatests" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>

                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="Edit/Delete">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgEdit" ImageUrl="~/images/blueEdit.png" runat="server" ToolTip="Edit" Width="20" Height="20" CommandName="Edit" CausesValidation="false" />

                                                                <asp:ImageButton ID="imgDelete" ImageUrl="~/images/blueDelete1.png" runat="server" Width="20" Height="20" CommandName="Delete" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete this record?')" ToolTip="Delete" />

                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:LinkButton ID="btnUpdate0" runat="server" CommandName="Update" Text="Update" />

                                                                <asp:LinkButton ID="btnCancel0" runat="server" CommandName="Cancel" Text="Cancel" />
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnInsertRecord0" runat="server" CommandName="Insert" Text="Add" ValidationGroup="ValgrpCust" />
                                                            </FooterTemplate>

                                                        </asp:TemplateField>--%>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>













                    </div>
                </div>
            </div>
        </div>
    </div>



</asp:Content>
