<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" EnableEventValidation="false"  codefile="FarmerMaster.aspx.cs" Inherits="GPILWebApp.FarmerMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


      <div class="page-header">
        <h1>Farmer Master</h1>
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
                                        <label class="control-label" style="color: #1989b9">Select Option</label>
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <asp:RadioButton ID="rdbimport" runat="server" GroupName="rdbexporttype" Text="Import" AutoPostBack="True"
                                            OnCheckedChanged="rdbimport_CheckedChanged"  ForeColor="Black" CssClass="form-control " />
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <asp:RadioButton ID="rdbmanual" runat="server" GroupName="rdbexporttype"
                                            Text="Manual" AutoPostBack="True"
                                            OnCheckedChanged="rdbmanual_CheckedChanged"  ForeColor="Black" CssClass="form-control " />
                                    </div>
                                    <div class="col-md-3 mb-3"></div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-3 mb-3">
                                        <label class="control-label" style="color: #1989b9"></label>
                                    </div>
                                    <div class="col-md-3 mb-3">
                                    </div>
                                    <div class="col-md-3 mb-3">
                                    </div>
                                    <div class="col-md-3 mb-3"></div>
                                </div>
                            </div>
                        </div>

                        <div class="row" id="idUpload" runat="server" visible="false">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-3 mb-3">
                                        <label class="control-label" style="color: #555555">Excel Upload</label>

                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <asp:FileUpload ID="excelUpload" CssClass="form-control" runat="server" />
                                    </div>
                                    <div class="col-md-3 mb-3">

                                        <asp:Button ID="btnUpload" runat="server" Text="Upload" class="btn btn-primary btn-round" OnClick="btnUpload_Click"  />

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

                                                <asp:GridView ID="gvFarmer" runat="server" class="table table-success table-bordered"
                                                    OnPageIndexChanging="gvFarmer_PageIndexChanging" 
                                                    OnRowCancelingEdit="gvFarmer_RowCancelingEdit" 
                                                    OnRowCommand="gvFarmer_RowCommand" 
                                                    OnRowUpdating="gvFarmer_RowUpdating"  PageSize="10"
                                                    Width="90%" ForeColor="Black"
                                                    OnRowDataBound="gvFarmer_RowDataBound"  AllowSorting="True" ShowFooter="True" EmptyDataText="No Data Found" AutoGenerateColumns="False" OnRowDeleting="gvFarmer_RowDeleting"  OnRowEditing="gvFarmer_RowEditing" >
                                                    <AlternatingRowStyle BackColor="#F0F0F0" />
                                                    <RowStyle Font-Bold="True" Font-Names="Calibri" Font-Size="10pt" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" SortExpression="NO.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Farmer Code" SortExpression="FARMER_CODE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lFarmerCode" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("FARMER_CODE") %>' />
                                                            </ItemTemplate>
                                                            
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddFarmerCode" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqFarmerCode" runat="server"
                                                                    ControlToValidate="txtAddFarmerCode" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Crop" SortExpression="CROP">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lCrop" ItemStyle-ForeColor="#ff0000" runat="server" Text='<%# Eval("CROP") %>' />
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


                                                        

                                                        <asp:TemplateField HeaderText="Farmer Category" SortExpression="FARMER_CATEGORY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lFarmerCategory" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("FARMER_CATEGORY") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtFarmerCategory" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("FARMER_CATEGORY") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddFarmerCategory" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqFarmerCategory" runat="server"
                                                                    ControlToValidate="txtAddFarmerCategory" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Farmer Name" SortExpression="FARMER_NAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lFarmerName" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("FARMER_NAME") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtFarmerName" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("FARMER_NAME") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddFarmerName" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqFarmerName" runat="server"
                                                                    ControlToValidate="txtAddFarmerName" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Farmer Father Name" SortExpression="FARMER_FATHER_NAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lFarmerFatherName" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("FARMER_FATHER_NAME") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtFarmerFatherName" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("FARMER_FATHER_NAME") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddFarmerFatherName" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqFarmerFatherName" runat="server"
                                                                    ControlToValidate="txtAddFarmerFatherName" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Village Code " SortExpression="VILLAGE_CODE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lVillageCode" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("VILLAGE_CODE") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtVillageCode" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("VILLAGE_CODE") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddVillageCode" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqVillageCode" runat="server"
                                                                    ControlToValidate="txtAddVillageCode" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Soil Type" SortExpression="SOIL_TYPE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lSoilType" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("SOIL_TYPE") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtSoilType" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("SOIL_TYPE") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddSoilType" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqSoilType" runat="server"
                                                                    ControlToValidate="txtAddSoilType" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Farmer Address1" SortExpression="FARMER_ADDRESS1">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lFarmerAddress1" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("FARMER_ADDRESS1") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtFarmerAddress1" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("FARMER_ADDRESS1") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddFarmerAddress1" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqFarmerAddress1" runat="server"
                                                                    ControlToValidate="txtAddFarmerAddress1" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Farmer Address2" SortExpression="FARMER_ADDRESS2">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lFarmerAddress2" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("FARMER_ADDRESS2") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtFarmerAddress2" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("FARMER_ADDRESS2") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddFarmerAddress2" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqFarmerAddress2" runat="server"
                                                                    ControlToValidate="txtAddFarmerAddress2" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="Country" SortExpression="COUNTRY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lCountry" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("COUNTRY") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("COUNTRY") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddCountry" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqCountry" runat="server"
                                                                    ControlToValidate="txtAddCountry" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="Pin Code" SortExpression="PIN_CODE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lPinCode" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("PIN_CODE") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtPinCode" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("PIN_CODE") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddPinCode" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqPinCode" runat="server"
                                                                    ControlToValidate="txtAddPinCode" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="TelePhone No" SortExpression="TEL_NO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lTelePhoneNo" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("TEL_NO") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtTelePhoneNo" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("TEL_NO") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddTelePhoneNo" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqTelePhoneNo" runat="server"
                                                                    ControlToValidate="txtAddTelePhoneNo" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Mobile No" SortExpression="MOBILE_NO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lMobileNo" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("MOBILE_NO") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("MOBILE_NO") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddMobileNo" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqMobileNo" runat="server"
                                                                    ControlToValidate="txtAddMobileNo" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>



                                                         <asp:TemplateField HeaderText="Email ID" SortExpression="EMAIL_ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lEmailID" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("EMAIL_ID") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtEmailID" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("EMAIL_ID") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddEmailID" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqEmailID" runat="server"
                                                                    ControlToValidate="txtAddEmailID" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="Bank Acc No" SortExpression="BANK_ACCOUNT_NO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lBankAccNo" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("BANK_ACCOUNT_NO") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtBankAccNo" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("BANK_ACCOUNT_NO") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddBankAccNo" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqBankAccNo" runat="server"
                                                                    ControlToValidate="txtAddBankAccNo" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="Bank Name" SortExpression="BANK_NAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lBankName" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("BANK_NAME") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("BANK_NAME") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddBankName" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqBankName" runat="server"
                                                                    ControlToValidate="txtAddBankName" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="Branch Name" SortExpression="BRANCH_NAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lBranchName" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("BRANCH_NAME") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtBranchName" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("BRANCH_NAME") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddBranchName" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqBranchName" runat="server"
                                                                    ControlToValidate="txtAddBranchName" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="IFSC Code" SortExpression="IFSC_CODE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lIFSCCode" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("IFSC_CODE") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtIFSCCode" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("IFSC_CODE") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddIFSCCode" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqIFSCCode" runat="server"
                                                                    ControlToValidate="txtAddIFSCCode" ErrorMessage="*"
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


                        <div class="row" runat="server" id="divImportSave" visible="false">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <asp:Button ID="btnImportSave" runat="server" Text="Save" class="btn btn-primary rounded-pill px-4" ValidationGroup="id1" OnClick="btnImportSave_Click"  />
                                </div>
                            </div>
                        </div>


                        <div class="row" runat="server" id="idManual" visible="false">

                            <div class="row">
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">* Farmer Code</label>
                                            <asp:TextBox ID="txtFarmerCode" runat="server" CssClass="form-control border border-primary" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvFarmerCode" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtFarmerCode" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <div class="form-floating mb-3">
                                                <label class="control-label" style="color: #555555">* Farmer Name</label>
                                                <asp:TextBox ID="txtFarmerName" CssClass="form-control border border-primary" runat="server" MaxLength="15"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvFarmerName" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtFarmerName" />
                                            </div>
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">* Farmer Category</label>
                                            <asp:TextBox ID="txtFarmerCategory" runat="server" CssClass="form-control border border-primary" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvFarmerCategory" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtFarmerCategory" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <div class="form-floating mb-3">
                                                <label class="control-label" style="color: #555555">* Village Code</label>
                                                <asp:TextBox ID="txtVillageCode" CssClass="form-control border border-primary" runat="server" MaxLength="15"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvVillageCode" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtVillageCode" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">*Father Name</label>
                                            <asp:TextBox ID="txtFatherName" runat="server" CssClass="form-control border border-primary" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvFatherName" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtFatherName" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <div class="form-floating mb-3">
                                                <label class="control-label" style="color: #555555">* Red-mark (Fine / Alert)</label>
                                                <asp:TextBox ID="txtRedMark" CssClass="form-control border border-primary" runat="server" MaxLength="15"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvRedMark" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtRedMark" />
                                            </div>
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">* Soil Type</label>
                                           <asp:DropDownList runat="server" CssClass="form-control" ID="ddlSoilType">
                                            <asp:ListItem Value="0">SELECT </asp:ListItem>
                                        </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <div class="form-floating mb-3">
                                                <label class="control-label" style="color: #555555">* Farmer Address1</label>
                                                <asp:TextBox ID="txtFarmerAddress1" CssClass="form-control border border-primary" runat="server" MaxLength="15"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvFarmerAddress1" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtFarmerAddress1" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">* Farmer Address2</label>
                                            <asp:TextBox ID="txtFarmerAddress2" runat="server" CssClass="form-control border border-primary" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvFarmerAddress2" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtFarmerAddress2" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <div class="form-floating mb-3">
                                                <label class="control-label" style="color: #555555">* Farmer Address3</label>
                                                <asp:TextBox ID="txtFarmerAddress3" CssClass="form-control border border-primary" runat="server" MaxLength="15"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvFarmerAddress3" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtFarmerAddress3" />
                                            </div>
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">*Farmer Address4</label>
                                            <asp:TextBox ID="txtFarmerAddress4" runat="server" CssClass="form-control border border-primary" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvFarmerAddress4" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtFarmerAddress4" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <div class="form-floating mb-3">
                                                <label class="control-label" style="color: #555555">* Farmer Address5</label>
                                                <asp:TextBox ID="txtFarmerAddress5" CssClass="form-control border border-primary" runat="server" MaxLength="15"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvFarmerAddress5" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtFarmerAddress5" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">* Farmer Address6</label>
                                            <asp:TextBox ID="txtFarmerAddress6" runat="server" CssClass="form-control border border-primary" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvFarmerAddress6" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtFarmerAddress6" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <div class="form-floating mb-3">
                                                <label class="control-label" style="color: #555555">* Country</label>
                                                <asp:TextBox ID="txtCountry" CssClass="form-control border border-primary" runat="server" MaxLength="15"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvCountry" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtCountry" />
                                            </div>
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">* Pin Code</label>
                                            <asp:TextBox ID="txtPinCode" runat="server" CssClass="form-control border border-primary" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPinCode" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtPinCode" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <div class="form-floating mb-3">
                                                <label class="control-label" style="color: #555555">* TelePhone No</label>
                                                <asp:TextBox ID="txtTelePhoneNo" CssClass="form-control border border-primary" runat="server" MaxLength="15"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvTelePhoneNo" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtTelePhoneNo" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">* Mobile No</label>
                                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control border border-primary" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvMobileNo" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtMobileNo" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <div class="form-floating mb-3">
                                                <label class="control-label" style="color: #555555">* Email ID</label>
                                                <asp:TextBox ID="txtEmailID" CssClass="form-control border border-primary" runat="server" MaxLength="15"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvEmailID" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtEmailID" />
                                            </div>
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">* Bank Account No</label>
                                            <asp:TextBox ID="txtBankAccNo" runat="server" CssClass="form-control border border-primary" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvBankAccNo" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtBankAccNo" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <div class="form-floating mb-3">
                                                <label class="control-label" style="color: #555555">* Bank Name</label>
                                                <asp:TextBox ID="txtBankName" CssClass="form-control border border-primary" runat="server" MaxLength="15"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvBankName" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtBankName" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">* Branch Name</label>
                                            <asp:TextBox ID="txtBranchName" runat="server" CssClass="form-control border border-primary" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvBranchName" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtBranchName" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <div class="form-floating mb-3">
                                                <label class="control-label" style="color: #555555">* IFSC Code</label>
                                                <asp:TextBox ID="txtIFSCCode" CssClass="form-control border border-primary" runat="server" MaxLength="15"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvIFSCCodetxtIFSCCode" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtIFSCCode" />
                                            </div>
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">* Status</label>
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlStatus">
                                            <asp:ListItem Value="0">SELECT</asp:ListItem>
                                        </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <div class="form-floating mb-3">
                                                <label class="control-label" style="color: #555555">* Variety</label>
                                               <asp:DropDownList runat="server" CssClass="form-control" ID="ddlVariety">
                                            <asp:ListItem Value="0">SELECT</asp:ListItem>
                                        </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">* Adhaar Number</label>
                                            <asp:TextBox ID="txtAdhaarNo" runat="server" CssClass="form-control border border-primary" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAdhaarNo" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtAdhaarNo" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <div class="form-floating mb-3">
                                                <label class="control-label" style="color: #555555">* Crop</label>
                                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCrop">
                                            <asp:ListItem Value="0">SELECT</asp:ListItem>
                                        </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <div class="form-floating mb-3">
                                                
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                        </div>






                        <div class="row" runat="server" id="idManualButton" visible="false">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-3">
                                        <div class="form-floating mb-3">
                                            <asp:Button ID="btnManualSave" runat="server" Text="Save" class="btn btn-primary btn-round" ValidationGroup="id1" OnClick="btnManualSave_Click"  />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary btn-round" OnClick="btnCancel_Click"  />
                                            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-floating mb-3">
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
