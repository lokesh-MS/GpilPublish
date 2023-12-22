﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="FarmerPurchase.aspx.cs" Inherits="GPILWebApp.FarmerPurchase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1>Farmer Purchase</h1>
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
                                            OnCheckedChanged="rdbimport_CheckedChanged" ForeColor="Black" CssClass="form-control " />
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

                                        <asp:Button ID="btnUpload" runat="server" Text="Upload" class="btn btn-primary btn-round" OnClick="btnUpload_Click"   />

                                    </div>
                                </div>
                            </div>
                        </div>



                       <asp:UpdatePanel ID="upMain" runat="server">
                            <ContentTemplate>

                                <div class="row" id="GridViewFPurchase" runat="server" visible="false">
                                    <div class="widget-body">
                                        <h3 class="card-title">Details</h3>
                                        <div class="widget-main">
                                            <div class="col-md-12 mb-12">

                                                <asp:GridView ID="gvFPurchase" runat="server" class="table table-success table-bordered"
                                                    OnPageIndexChanging="gvFPurchase_PageIndexChanging"  
                                                    OnRowCancelingEdit="gvFPurchase_RowCancelingEdit"  
                                                    OnRowCommand="gvFPurchase_RowCommand"  
                                                    OnRowUpdating="gvFPurchase_RowUpdating"   PageSize="10"
                                                    Width="90%" ForeColor="Black"
                                                    OnRowDataBound="gvFPurchase_RowDataBound"   AllowSorting="True" ShowFooter="True" EmptyDataText="No Data Found" AutoGenerateColumns="False" OnRowDeleting="gvFPurchase_RowDeleting"   OnRowEditing="gvFPurchase_RowEditing"  >
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

                                                        <asp:TemplateField HeaderText="Lot No" SortExpression="LOT_NO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lLotNo" ItemStyle-ForeColor="#ff0000" runat="server" Text='<%# Eval("LOT_NO") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtLotNo" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("LOT_NO") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddCrop" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqLotNo" runat="server"
                                                                    ControlToValidate="txtAddCrop" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Farmer Code" SortExpression="FARMER_CODE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lFarmerCode" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("FARMER_CODE") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtFarmerCode" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("FARMER_CODE") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddFarmerCode" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqFarmerCode" runat="server"
                                                                    ControlToValidate="txtAddFarmerCode" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>


                                                        

                                                        <asp:TemplateField HeaderText="Form Grade" SortExpression="FORM_GRADE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lFormGrade" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("FORM_GRADE") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtFormGrade" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("FORM_GRADE") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddFormGrade" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqFormGrade" runat="server"
                                                                    ControlToValidate="txtAddFormGrade" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Net Wt" SortExpression="NET_WT">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lNetWt" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("NET_WT") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtNetWt" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("NET_WT") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddNetWt" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqNetWt" runat="server"
                                                                    ControlToValidate="txtAddNetWt" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Rate" SortExpression="RATE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lRate" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("RATE") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtRate" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("RATE") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddRate" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqRate" runat="server"
                                                                    ControlToValidate="txtAddRate" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Buyer Grade" SortExpression="BUYER_GRADE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lBuyerGrade" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("BUYER_GRADE") %>' />
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

                                                        <asp:TemplateField HeaderText="Rejection Type" SortExpression="REJE_TYPE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lRejectionType" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("REJE_TYPE") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtRejectionType" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("REJE_TYPE") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddRejectionType" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqRejectionType" runat="server"
                                                                    ControlToValidate="txtAddRejectionType" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Organization Code" SortExpression="FARMER_ADDRESS1">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lOrganizationCode" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("ORGN_CODE") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtOrganizationCode" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("ORGN_CODE") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddOrganizationCode" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqOrganizationCode" runat="server"
                                                                    ControlToValidate="txtAddOrganizationCode" ErrorMessage="*"
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

                                                         <asp:TemplateField HeaderText="Purchase Doc No" SortExpression="PURCH_DOC_NO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lPurchaseDocNo" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("PURCH_DOC_NO") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtPurchaseDocNo" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("PURCH_DOC_NO") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddPurchaseDocNo" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqPurchaseDocNo" runat="server"
                                                                    ControlToValidate="txtAddPurchaseDocNo" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="Purchase Date" SortExpression="PURCHASE_DATE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lPurchaseDate" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("PURCHASE_DATE") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtPurchaseDate" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("PURCHASE_DATE") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddPurchaseDate" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqPurchaseDate" runat="server"
                                                                    ControlToValidate="txtAddPurchaseDate" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Crop" SortExpression="CROP">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lCrop" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("CROP") %>' />
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
                                    <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary rounded-pill px-4" ValidationGroup="id1" Onclick="btnSave_Click"   />
                                    <asp:Button ID="btnClear" runat="server" Text="Clear" class="btn btn-primary rounded-pill px-4" ValidationGroup="id1" OnClick="btnClear_Click"  />
                                </div>
                            </div>
                        </div>



                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
