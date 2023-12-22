<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="FarmerLoanLoader.aspx.cs" Inherits="GPILWebApp.FarmerLoanLoader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1>Farmer Loan Loader</h1>
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
                                            OnCheckedChanged="rdbimport_CheckedChanged"  ForeColor="Black" CssClass="form-control " />
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

                                        <asp:Button ID="btnUpload" runat="server" Text="Upload" class="btn btn-primary btn-round" OnClick="btnUpload_Click"    />

                                    </div>
                                </div>
                            </div>
                        </div>


                        <asp:UpdatePanel ID="upMain" runat="server">
                            <ContentTemplate>

                                <div class="row" id="GridViewFLLoader" runat="server" visible="false">
                                    <div class="widget-body">
                                        <h3 class="card-title">Details</h3>
                                        <div class="widget-main">
                                            <div class="col-md-12 mb-12">

                                                <asp:GridView ID="gvFLLoader" runat="server" class="table table-success table-bordered"
                                                    OnPageIndexChanging="gvFLLoader_PageIndexChanging"   
                                                    OnRowCancelingEdit="gvFLLoader_RowCancelingEdit"   
                                                    OnRowCommand="gvFLLoader_RowCommand"   
                                                    OnRowUpdating="gvFLLoader_RowUpdating"    PageSize="10"
                                                    Width="90%" ForeColor="Black"
                                                    OnRowDataBound="gvFLLoader_RowDataBound"    AllowSorting="True" ShowFooter="True" EmptyDataText="No Data Found" AutoGenerateColumns="False" OnRowDeleting="gvFLLoader_RowDeleting"    OnRowEditing="gvFLLoader_RowEditing"   >
                                                    <AlternatingRowStyle BackColor="#F0F0F0" />
                                                    <RowStyle Font-Bold="True" Font-Names="Calibri" Font-Size="10pt" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" SortExpression="NO.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Crop" SortExpression="CROP">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lFarmerCode" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("CROP") %>' />
                                                            </ItemTemplate>
                                                            
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddFarmerCode" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqFarmerCode" runat="server"
                                                                    ControlToValidate="txtAddFarmerCode" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Variety" SortExpression="VARIETY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lCrop" ItemStyle-ForeColor="#ff0000" runat="server" Text='<%# Eval("VARIETY") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtCrop" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("VARIETY") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddCrop" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqCrop" runat="server"
                                                                    ControlToValidate="txtAddCrop" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Farmer Code" SortExpression="FARMER_CODE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lVariety" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("FARMER_CODE") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtVariety" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("FARMER_CODE") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddVariety" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqVariety" runat="server"
                                                                    ControlToValidate="txtAddVariety" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>


                                                        

                                                        <asp:TemplateField HeaderText="Loan Amount" SortExpression="LOAN_AMOUNT">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lLoanAmount" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("LOAN_AMOUNT") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtLoanAmount" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("LOAN_AMOUNT") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddLoanAmount" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqLoanAmount" runat="server"
                                                                    ControlToValidate="txtAddLoanAmount" ErrorMessage="*"
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
                                    <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary rounded-pill px-4" ValidationGroup="id1" onclick="btnSave_Click"    />
                                    <asp:Button ID="btnClear" runat="server" Text="Clear" class="btn btn-primary rounded-pill px-4" ValidationGroup="id1" onclick="btnClear_Click"  />
                                </div>
                            </div>
                        </div>



                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
