<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="SupplierPurchase.aspx.cs" Inherits="GPILWebApp.SupplierPurchase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-header">
        <h1 style="font-family: Cambria; font-weight: bold">Supplier Purchase</h1>
    </div>

    <div class="container-fluid">
        <div class="row">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">

                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-3 mb-3">
                                        <asp:RadioButton ID="rdbimport" runat="server" GroupName="rdbexporttype" Text="Import" AutoPostBack="True"
                                            OnCheckedChanged="rdbimport_CheckedChanged"    ForeColor="Black" CssClass="form-control " />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row" id="idUpload" runat="server" visible="true">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-3 mb-3">
                                        <label class="control-label" style="color: #555555">Excel Upload</label>

                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <asp:FileUpload ID="excelUpload" CssClass="form-control" runat="server" />
                                    </div>
                                    <div class="col-md-3 mb-3">

                                        <asp:Button ID="btnUpload" runat="server" Text="Upload" class="btn btn-primary btn-round" OnClick="btnUpload_Click"      />

                                    </div>
                                </div>
                            </div>
                        </div>


                        <asp:UpdatePanel ID="upMain" runat="server">
                            <ContentTemplate>

                                <div class="row" id="GridViewSPurchase" runat="server" visible="false">
                                    <div class="widget-body">
                                        <h3 class="card-title">Details</h3>
                                        <div class="widget-main">
                                            <div class="col-md-12 mb-12">

                                                <asp:GridView ID="gvSPurchase" runat="server" class="table table-success table-bordered"
                                                    OnPageIndexChanging="gvSPurchase_PageIndexChanging"     
                                                    OnRowCancelingEdit="gvSPurchase_RowCancelingEdit"     
                                                    OnRowCommand="gvSPurchase_RowCommand"     
                                                    OnRowUpdating="gvSPurchase_RowUpdating"      PageSize="10"
                                                    Width="90%" ForeColor="Black"
                                                    OnRowDataBound="gvSPurchase_RowDataBound"       AllowSorting="True" ShowFooter="True" EmptyDataText="No Data Found" AutoGenerateColumns="False" OnRowDeleting="gvSPurchase_RowDeleting"      OnRowEditing="gvSPurchase_RowEditing"     >
                                                    <AlternatingRowStyle BackColor="#F0F0F0" />
                                                    <RowStyle Font-Bold="True" Font-Names="Calibri" Font-Size="10pt" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" SortExpression="NO.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="GPIL Bale Number" SortExpression="GPIL_BALE_NUMBER">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lGPILBaleNumber" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("GPIL_BALE_NUMBER") %>' />
                                                            </ItemTemplate>
                                                            
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddGPILBaleNumber" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqGPILBaleNumber" runat="server"
                                                                    ControlToValidate="txtAddGPILBaleNumber" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Buyer Grade" SortExpression="BUYER_GRADE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lBuyerGrade" ItemStyle-ForeColor="#ff0000" runat="server" Text='<%# Eval("BUYER_GRADE") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtBuyerGrade" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("BUYER_GRADE") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddBuyerGrade" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqBuyerGrade" runat="server"
                                                                    ControlToValidate="txtAddBuyerGrade" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Net wt" SortExpression="NET_WEIGHT">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lNetwt" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("NET_WEIGHT") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtNetwt" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("NET_WEIGHT") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddNetwt" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqNetwt" runat="server"
                                                                    ControlToValidate="txtAddNetwt" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>


                                                        

                                                        <asp:TemplateField HeaderText="SubInventory Code" SortExpression="SUBINVENTORY_CODE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lSubInventoryCode" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("SUBINVENTORY_CODE") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtSubInventoryCode" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("SUBINVENTORY_CODE") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddSubInventoryCode" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqSubInventoryCode" runat="server"
                                                                    ControlToValidate="txtAddSubInventoryCode" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Supplier Code" SortExpression="SUPP_CODE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lSupplierCode" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("SUPP_CODE") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtSupplierCode" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("SUPP_CODE") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddSupplierCode" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqSupplierCode" runat="server"
                                                                    ControlToValidate="txtAddSupplierCode" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Recev Organization Code" SortExpression="RECEV_ORG_CODE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lRecevOrganizationCode" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("RECEV_ORG_CODE") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtRecevOrganizationCode" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("RECEV_ORG_CODE") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddRecevOrganizationCode" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqRecevOrganizationCode" runat="server"
                                                                    ControlToValidate="txtAddRecevOrganizationCode" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Buyer Code" SortExpression="BUYER_CODE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lBuyerCode" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("BUYER_CODE") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtBuyerCode" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("BUYER_CODE") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddBuyerCode" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqBuyerCode" runat="server"
                                                                    ControlToValidate="txtAddBuyerCode" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Purchase Doc No" SortExpression="LP4_NUMBER">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lPurchaseDocNo" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("LP4_NUMBER") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtPurchaseDocNo" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("LP4_NUMBER") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddPurchaseDocNo" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqPurchaseDocNo" runat="server"
                                                                    ControlToValidate="txtAddPurchaseDocNo" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Crop" SortExpression="CROP">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lBankName" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("CROP") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtCrop" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("CROP") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddCrop" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqCrop" runat="server"
                                                                    ControlToValidate="txtAddCrop" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Variety" SortExpression="VARIETY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lVariety" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("VARIETY") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtVariety" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("VARIETY") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddVariety" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqVariety" runat="server"
                                                                    ControlToValidate="txtAddVariety" ErrorMessage="*"
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

                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <div class="row" runat="server" id="divImportSave" visible="true">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary rounded-pill px-4" ValidationGroup="id1" onclick="btnSave_Click"     />
                                    <asp:Button ID="btnClear" runat="server" Text="Clear" class="btn btn-primary rounded-pill px-4" ValidationGroup="id1" onclick="btnClear_Click" />
                                </div>
                            </div>
                        </div>





                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
