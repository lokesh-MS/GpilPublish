<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="OrganizationMaster.aspx.cs" Inherits="GPILWebApp.OrganizationMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="page-header">
        <h1>Organization Master				
        </h1>
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
                                        <label class="control-label" style="color: #555555">Select Option</label>
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <asp:RadioButton ID="rdbimport" runat="server" GroupName="rdbexporttype" Text="Import " AutoPostBack="True"
                                            OnCheckedChanged="rdbimport_CheckedChanged" ForeColor="Black" CssClass="form-control " />
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <asp:RadioButton ID="rdbmanual" runat="server" GroupName="rdbexporttype"
                                            Text="Manual" AutoPostBack="True"
                                            OnCheckedChanged="rdbmanual_CheckedChanged" ForeColor="Black" CssClass="form-control " />
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

                                        <asp:Button ID="btnUpload" runat="server" Text="Upload" class="btn btn-primary btn-round" OnClick="btnUpload_Click" />

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

                                                <asp:GridView ID="gvOrgn" runat="server" class="table table-success table-bordered"
                                                    OnPageIndexChanging="gvOrgn_PageIndexChanging"
                                                    OnRowCancelingEdit="gvOrgn_RowCancelingEdit"
                                                    OnRowCommand="gvOrgn_RowCommand"
                                                    OnRowUpdating="gvOrgn_RowUpdating" PageSize="10"
                                                    Width="90%" ForeColor="Black"
                                                    OnRowDataBound="gvOrgn_RowDataBound" AllowSorting="True" ShowFooter="True" EmptyDataText="No Data Found" AutoGenerateColumns="False" OnRowDeleting="gvOrgn_RowDeleting" OnRowEditing="gvOrgn_RowEditing">
                                                    <AlternatingRowStyle BackColor="#F0F0F0" />
                                                    <RowStyle Font-Bold="True" Font-Names="Calibri" Font-Size="10pt" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" SortExpression="NO.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Organization Code" SortExpression="Organization">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lOrganizationCode" ItemStyle-ForeColor="#ff0000" runat="server" Text='<%# Eval("ORGN_CODE") %>' />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddOrgn" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqOrgnCode" runat="server"
                                                                    ControlToValidate="txtAddOrgn" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Organization Name" SortExpression="ORGN_NAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lOrganizationName" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("ORGN_NAME") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtOrganizationName" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("ORGN_NAME") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddOrganizationName" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqvrytyp" runat="server"
                                                                    ControlToValidate="txtAddOrganizationName" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Organization Type" SortExpression="ORGN_TYPE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lOrganizationType" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("ORGN_TYPE") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtOrganizationType" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("ORGN_TYPE") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddOrganizationType" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqvrytyp" runat="server"
                                                                    ControlToValidate="txtAddOrganizationType" ErrorMessage="*"
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
                                                                <asp:RequiredFieldValidator ID="reqvrytyp" runat="server"
                                                                    ControlToValidate="txtAddVariety" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Orgn Address1" SortExpression="ORGN_ADDRESS1">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lOrgnAddress1" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("ORGN_ADDRESS1") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtOrgnAddress1" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("ORGN_ADDRESS1") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddOrgnAddress1" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqvrytyp" runat="server"
                                                                    ControlToValidate="txtAddOrgnAddress1" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Orgn Address2" SortExpression="ORGN_ADDRESS2">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lOrgnAddress2" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("ORGN_ADDRESS2") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtOrgnAddress2" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("ORGN_ADDRESS2") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddOrgnAddress2" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqvrytyp" runat="server"
                                                                    ControlToValidate="txtAddOrgnAddress2" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Orgn Address3" SortExpression="ORGN_ADDRESS3">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lOrgnAddress3" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("ORGN_ADDRESS3") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtOrgnAddress3" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("ORGN_ADDRESS3") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddOrgnAddress3" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqvrytyp" runat="server"
                                                                    ControlToValidate="txtAddOrgnAddress3" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Orgn Country" SortExpression="ORGN_COUNTRY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lOrgnCountry" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("ORGN_COUNTRY") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtOrgnCountry" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("ORGN_COUNTRY") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddOrgnCountry" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqvrytyp" runat="server"
                                                                    ControlToValidate="txtAddOrgnCountry" ErrorMessage="*"
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
                                                                <asp:RequiredFieldValidator ID="reqvrytyp" runat="server"
                                                                    ControlToValidate="txtAddPinCode" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Tel No" SortExpression="TEL_NO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lTelNo" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("TEL_NO") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtTelNo" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("TEL_NO") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddTelNo" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqvrytyp" runat="server"
                                                                    ControlToValidate="txtAddTelNo" ErrorMessage="*"
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
                                                                <asp:RequiredFieldValidator ID="reqvrytyp" runat="server"
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
                                                                <asp:RequiredFieldValidator ID="reqvrytyp" runat="server"
                                                                    ControlToValidate="txtAddEmailID" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Insurance Value" SortExpression="INSURANCE_VAL">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lInsuranceValue" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("INSURANCE_VAL") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtInsuranceValue" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("INSURANCE_VAL") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddInsuranceValue" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqvrytyp" runat="server"
                                                                    ControlToValidate="txtAddInsuranceValue" ErrorMessage="*"
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
                                    <asp:Button ID="btnImportSave" runat="server" Text="Save" class="btn btn-primary rounded-pill px-4" ValidationGroup="id1" OnClick="btnImportSave_Click" />
                                </div>
                            </div>
                        </div>


                        <div class="row" runat="server" id="idManual" visible="false">

                            <div class="row">
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">* Organization Code</label>
                                            <asp:TextBox ID="txtOrganizationCode" runat="server" CssClass="form-control border border-primary" required=''></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvOrgnCode" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtOrganizationCode" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <div class="form-floating mb-3">
                                                <label class="control-label" style="color: #555555">* Organization Name</label>
                                                <asp:TextBox ID="txtCOrganizationName" CssClass="form-control border border-primary" runat="server" required=''></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvOrganizationName" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtCOrganizationName" />
                                            </div>
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">* Organization Type</label>
                                            <asp:TextBox ID="txtOrganizationType" runat="server" CssClass="form-control border border-primary" required=''></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvOrganizationType" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtOrganizationType" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <div class="form-floating mb-3">
                                                <label class="control-label" style="color: #555555">* Organization Address1</label>
                                                <asp:TextBox ID="txtOrganizationAddress1" CssClass="form-control border border-primary" runat="server" required=''></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvOrganizationAddress1" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtOrganizationAddress1" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">* Organization Address2</label>
                                            <asp:TextBox ID="txtOrganizationAddress2" runat="server" CssClass="form-control border border-primary" required=''></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvOrganizationAddress2" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtOrganizationAddress2" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <div class="form-floating mb-3">
                                                <label class="control-label" style="color: #555555">Organization Address3</label>
                                                <asp:TextBox ID="txtOrganizationAddress3" CssClass="form-control border border-primary" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvOrganizationAddress3" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtOrganizationAddress3" />
                                            </div>
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">Organization Address4</label>
                                            <asp:TextBox ID="txtOrganizationAddress4" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvOrganizationAddress4" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtOrganizationAddress4" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <div class="form-floating mb-3">
                                                <label class="control-label" style="color: #555555">Organization Address5</label>
                                                <asp:TextBox ID="txtOrganizationAddress5" CssClass="form-control border border-primary" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvOrganizationAddress5" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtOrganizationAddress5" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">Organization Address6</label>
                                            <asp:TextBox ID="txtOrganizationAddress6" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvOrganizationAddress6" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtOrganizationAddress6" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <div class="form-floating mb-3">
                                                <label class="control-label" style="color: #555555">Organization Country</label>
                                                <asp:TextBox ID="txtOrganizationCountry" CssClass="form-control border border-primary" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvOrganizationCountry" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtOrganizationCountry" />
                                            </div>
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">Organization Pincode</label>
                                            <asp:TextBox ID="txtOrganizationPincode" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvOrganizationPincode" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtOrganizationPincode" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <div class="form-floating mb-3">
                                                <label class="control-label" style="color: #555555">* Telephone No</label>
                                                <asp:TextBox ID="txtTelephoneNo" CssClass="form-control border border-primary" runat="server" required=''></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvTelephoneNo" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtTelephoneNo" />
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
                                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control border border-primary" required=''></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvMobileNo" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtMobileNo" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <div class="form-floating mb-3">
                                                <label class="control-label" style="color: #555555">* Email ID</label>
                                                <asp:TextBox ID="txtEmailID" CssClass="form-control border border-primary" runat="server" required=''></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvEmailID" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtEmailID" />
                                            </div>
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">* Insurance Value</label>
                                            <asp:TextBox ID="txtInsuranceValue" runat="server" CssClass="form-control border border-primary" required=''></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvInsuranceValue" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtInsuranceValue" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <div class="form-floating mb-3">
                                                <label class="control-label" style="color: #555555">* Variety</label>
                                                <asp:DropDownList ID="ddlVariety" runat="server" class="form-control" />
                                                <asp:RequiredFieldValidator ID="rfvVariety" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="ddlVariety" />
                                            </div>
                                        </div>
                                    </div>
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

                        <div class="row" runat="server" id="idManualButton" visible="false">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-3">
                                        <div class="form-floating mb-3">
                                            <asp:Button ID="btnManualSave" runat="server" Text="Save" class="btn btn-primary btn-round" ValidationGroup="id1" OnClick="btnManualSave_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary btn-round" OnClick="btnCancel_Click" />
                                           <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red" />
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
