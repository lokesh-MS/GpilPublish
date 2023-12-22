<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="OperationReceipeMaster.aspx.cs" Inherits="GPILWebApp.OperationReceipeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="page-header">
        <h1>Operation Receipe Master</h1>
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

                                                <asp:GridView ID="gvOprReceipe" runat="server" class="table table-success table-bordered"
                                                    OnPageIndexChanging="gvOprReceipe_PageIndexChanging"
                                                    OnRowCancelingEdit="gvOprReceipe_RowCancelingEdit"
                                                    OnRowCommand="gvOprReceipe_RowCommand"
                                                    OnRowUpdating="gvOprReceipe_RowUpdating" PageSize="10"
                                                    Width="90%" ForeColor="Black"
                                                    OnRowDataBound="gvOprReceipe_RowDataBound" AllowSorting="True" ShowFooter="True" EmptyDataText="No Data Found" AutoGenerateColumns="False" OnRowDeleting="gvOprReceipe_RowDeleting" OnRowEditing="gvOprReceipe_RowEditing">
                                                    <AlternatingRowStyle BackColor="#F0F0F0" />
                                                    <RowStyle Font-Bold="True" Font-Names="Calibri" Font-Size="10pt" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" SortExpression="NO.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Receipe Code" SortExpression="RECIPE_CODE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lReceipeCode" ItemStyle-ForeColor="#ff0000" runat="server" Text='<%# Eval("RECIPE_CODE") %>' />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddReceipeCode" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqReceipeCode" runat="server"
                                                                    ControlToValidate="txtAddReceipeCode" ErrorMessage="*"
                                                                    ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Operation Receipe" SortExpression="OPERATION_RECIPE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lOperationReceipe" runat="server" CssClass="form-control border border-primary" Text='<%# Eval("OPERATION_RECIPE") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtOperationReceipe" runat="server" CssClass="form-control border border-primary" Text='<%#Eval("OPERATION_RECIPE") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtAddOperationReceipe" runat="server" CssClass="form-control border border-primary"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqOperationReceipe" runat="server"
                                                                    ControlToValidate="txtAddOperationReceipe" ErrorMessage="*"
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
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-3 mb-3">
                                        <label class="control-label" style="color: #555555">* Receipe Code</label>
                                        <asp:TextBox ID="txtReceipeCode" runat="server" CssClass="form-control border border-primary" required=''></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvReceipeCode" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtReceipeCode" />


                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-floating mb-3">
                                            <label class="control-label" style="color: #555555">* Operation Receipe</label>
                                            <asp:TextBox ID="txtOperationReceipe" CssClass="form-control border border-primary" runat="server" required=''></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvOperationReceipe" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtOperationReceipe" />
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
