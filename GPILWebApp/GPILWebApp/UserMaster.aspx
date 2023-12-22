<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="UserMaster.aspx.cs" Inherits="GPILWebApp.UserMaster1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="page-header">
        <h1>User Master</h1>
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
                                        <asp:RadioButton ID="rdbimport" runat="server" GroupName="rdbexporttype" Text="Import " AutoPostBack="True" OnCheckedChanged="rdbimport_CheckedChanged"
                                            ForeColor="Black" CssClass="form-control " />
                                    </div>
                                    <div class="col-md-3 mb-3">
                                        <asp:RadioButton ID="rdbmanual" runat="server" GroupName="rdbexporttype"
                                            Text="Manual" AutoPostBack="True" OnCheckedChanged="rdbmanual_CheckedChanged" ForeColor="Black" CssClass="form-control " />
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
                                        <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-primary btn-round" OnClick="btnUpload_Click" />
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

                                                 <%--OnPageIndexChanging="gvUser_PageIndexChanging"
                                                    OnRowCancelingEdit="gvUser_RowCancelingEdit"
                                                    OnRowCommand="gvUser_RowCommand"
                                                    OnRowUpdating="gvUser_RowUpdating"
                                                     OnRowDataBound="gvUser_RowDataBound"
                                                     OnRowDeleting="gvUser_RowDeleting" OnRowEditing="gvUser_RowEditing --%>

                                                <asp:GridView ID="gvUser" runat="server" class="table table-success table-bordered" OnPageIndexChanging="gvUser_PageIndexChanging"
                                                     OnRowCancelingEdit="gvUser_RowCancelingEdit" OnRowCommand="gvUser_RowCommand" OnRowUpdating="gvUser_RowUpdating"
                                                   PageSize="10"
                                                    Width="90%" ForeColor="Black" OnRowDataBound="gvUser_RowDataBound"
                                                     AllowSorting="True" ShowFooter="True" EmptyDataText="No Data Found" AutoGenerateColumns="False" OnRowDeleting="gvUser_RowDeleting" OnRowEditing="gvUser_RowEditing">
                                                    <AlternatingRowStyle BackColor="#F0F0F0" />
                                                    <RowStyle Font-Bold="True" Font-Names="Calibri" Font-Size="10pt" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" SortExpression="NO.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="User ID" SortExpression="USER_ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lUserID" ItemStyle-ForeColor="#ff0000" runat="server" Text='<%# Eval("USER_ID") %>' />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddUserID" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqUserID" runat="server"
                                                                    ControlToValidate="txtAddUserID" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="User Name" SortExpression="USER_NAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lUserName" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("USER_NAME") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("USER_NAME") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddUserName" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqUserName" runat="server"
                                                                    ControlToValidate="txtAddUserName" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Password" SortExpression="PASSWORD">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lPassword" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("PASSWORD") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("PASSWORD") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddPassword" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqPassword" runat="server"
                                                                    ControlToValidate="txtAddPassword" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="User ERP Name" SortExpression="USER_ERP_NAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lUserERPName" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("USER_ERP_NAME") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtUserERPName" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("USER_ERP_NAME") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddUserERPName" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqUserERPName" runat="server"
                                                                    ControlToValidate="txtAddUserERPName" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Emp Code" SortExpression="EMP_CODE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lEmpCode" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("EMP_CODE") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtEmpCode" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("EMP_CODE") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddEmpCode" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqEmpCode" runat="server"
                                                                    ControlToValidate="txtAddEmpCode" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Designation" SortExpression="DESIGNATION">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lDesignation" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("DESIGNATION") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("DESIGNATION") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddDesignation" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqDesignation" runat="server"
                                                                    ControlToValidate="txtAddDesignation" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Department" SortExpression="DEPARTMENT">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lDepartment" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("DEPARTMENT") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtDepartment" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("DEPARTMENT") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddDepartment" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqDepartment" runat="server"
                                                                    ControlToValidate="txtAddDepartment" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="User Rights" SortExpression="USER_RIGHTS">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lUserRights" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("USER_RIGHTS") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtUserRights" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("USER_RIGHTS") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddUserRights" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqUserRights" runat="server"
                                                                    ControlToValidate="txtAddUserRights" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Mobile Number" SortExpression="MOBILE_NO">
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
                                    <asp:Button ID="btnImportSave" runat="server" Text="Save" class="btn btn-primary rounded-pill px-4" ValidationGroup="id1" OnClick="btnImportSave_Click"/>
                                </div>
                            </div>
                        </div>



                        <div class="row" runat="server" id="idManual" visible="false">

                            <div class="row">
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">* User ID</label>
                                            <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control border border-primary" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvUserID" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtUserID" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <div class="form-floating mb-3">
                                                <label class="control-label" style="color: #555555">* User Name</label>
                                                <asp:TextBox ID="txtUserName" CssClass="form-control border border-primary" runat="server" MaxLength="15"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvUserName" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtUserName" />
                                            </div>
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">* Password</label>
                                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control border border-primary" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPassword" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtPassword" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <div class="form-floating mb-3">
                                                <label class="control-label" style="color: #555555">* Confirm Password</label>
                                                <asp:TextBox ID="txtConfirmPassword" CssClass="form-control border border-primary" runat="server" MaxLength="15"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvConfirmPassword" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtConfirmPassword" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">* User ERP Name</label>
                                            <asp:TextBox ID="txtUserERPName" runat="server" CssClass="form-control border border-primary" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvUserERPName" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtUserERPName" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <div class="form-floating mb-3">
                                                <label class="control-label" style="color: #555555">* Emp Code</label>
                                                <asp:TextBox ID="txtEmpCode" CssClass="form-control border border-primary" runat="server" MaxLength="15"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvEmpCode" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtEmpCode" />
                                            </div>
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">* Deisgnation</label>
                                            <asp:TextBox ID="txtDeisgnation" runat="server" CssClass="form-control border border-primary" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvDeisgnation" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtDeisgnation" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <div class="form-floating mb-3">
                                                <label class="control-label" style="color: #555555">* Department</label>
                                                <asp:TextBox ID="txtDepartment" CssClass="form-control border border-primary" runat="server" MaxLength="15"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvDepartment" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtDepartment" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="widget-body">
                                    <div class="widget-main">
                                        <div class="col-md-3 mb-3">
                                            <label class="control-label" style="color: #555555">* User Rights</label>
                                            <asp:TextBox ID="txtUserRights" runat="server" CssClass="form-control border border-primary" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvUserRights" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtUserRights" />
                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <div class="form-floating mb-3">
                                                <label class="control-label" style="color: #555555">* Mobile No</label>
                                                <asp:TextBox ID="txtMobileNo" CssClass="form-control border border-primary" runat="server" MaxLength="15"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvMobileNo" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtMobileNo" />
                                            </div>
                                        </div>
                                        <div class="col-md-3 mb-3">
                                           <%-- <label class="control-label" style="color: #555555">* Sync ID</label>
                                            <asp:TextBox ID="txtSyncID" runat="server" CssClass="form-control border border-primary" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvSyncID" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtSyncID" />--%>

                                             <label class="control-label" style="color: #555555">* Email ID</label>
                                            <asp:TextBox ID="txtEmailID" runat="server" CssClass="form-control border border-primary" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvEmailID" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtEmailID" />

                                        </div>
                                        <div class="col-md-3 mb-3">
                                            <div class="form-floating mb-3">
                                                <%--<label class="control-label" style="color: #555555">* Sync Password</label>
                                                <asp:TextBox ID="txtSyncPassword" CssClass="form-control border border-primary" runat="server" MaxLength="15"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvSyncPassword" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtSyncPassword" />--%>

                                                 <%--<label class="control-label" style="color: #555555">* Status</label>
                                                <asp:DropDownList ID="ddlStatus" runat="server" class="form-control" />
                                                <asp:RequiredFieldValidator ID="rfvStatus" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="ddlStatus" />--%>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="widget-body">
                                    <div class="widget-main">
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
                                            <asp:Button ID="btnManualSave" runat="server" Text="Save" class="btn btn-primary btn-round" ValidationGroup="id1" OnClick="btnManualSave_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-primary btn-round" OnClick="btnCancel_Click" />
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
