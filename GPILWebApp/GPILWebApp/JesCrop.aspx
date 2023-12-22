<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="JesCrop.aspx.cs" Inherits="GPILWebApp.JesCrop" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-header">
        <h1>Crop Master</h1>
    </div>



    <div class="container-fluid">
        <div class="row">
            <div class="col-12">
                <div class="card">
                    <div class="card-body">


                        <%--<div class="row">
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
                        </div>--%>


                        <%--<div class="row">
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
                        </div>--%>



                        <%--<div class="row" id="idUpload" runat="server" visible="false">
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
                        </div>--%>

                        <asp:UpdatePanel ID="upMain" runat="server">
                            <ContentTemplate>

                                <div class="row" id="divGrid" runat="server">
                                    <div class="widget-body">
                                        <h3 class="card-title">Details</h3>
                                        <div class="widget-main">
                                            <div class="col-md-12 mb-12">
                                                <asp:GridView ID="gvjes" runat="server" BackColor="White" BorderColor="#CCCCCC" AutoGenerateColumns="false" ShowFooter="true" DataKeyNames="SNO"
                                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" OnPageIndexChanging="gvjes_PageIndexChanging" OnRowCommand="gvjes_RowCommand">


                                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                    <RowStyle ForeColor="#000066" />
                                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                    <SortedDescendingHeaderStyle BackColor="#00547E" />


                                                    <Columns>

                                                        <asp:TemplateField HeaderText="CROP">
                                                            <ItemTemplate>
                                                                <asp:Label Text='<%# Eval("CROP") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtCrop" Text='<%# Eval("CROP") %>' runat="server"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtCropFooter" runat="server"></asp:TextBox>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="CROP YEAR">
                                                            <ItemTemplate>
                                                                <asp:Label Text='<%# Eval("CROP_YEAR") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtCropYear" Text='<%# Eval("CROP_YEAR") %>' runat="server"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtCropYearFooter" runat="server"></asp:TextBox>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="SHORT CROP YEAR">
                                                            <ItemTemplate>
                                                                <asp:Label Text='<%# Eval("ATTRIBUTE1") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtShortCropYear" Text='<%# Eval("ATTRIBUTE1") %>' runat="server"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtShortCropYearFooter" runat="server"></asp:TextBox>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ImageUrl="~/Images/Edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px"/>
                                                                <asp:ImageButton ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px"/>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:ImageButton ImageUrl="~/Images/save.jfif" runat="server" CommandName="Save" ToolTip="Save" Width="20px" Height="20px"/>
                                                                <asp:ImageButton ImageUrl="~/Images/Cross.jfif" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px"/>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:ImageButton ImageUrl="~/Images/Add.png" runat="server" CommandName="AddNEW" ToolTip="Add New" Width="20px" Height="20px"/>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>

                                                <asp:Label ID="lblSuccessMsg" runat="server" ForeColor="Green"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>




                        <%--<div class="row" runat="server" id="divImportSave" visible="false">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <asp:Button ID="btnImportSave" runat="server" Text="Save" class="btn btn-primary rounded-pill px-4" ValidationGroup="id1" OnClick="btnImportSave_Click" />
                                </div>
                            </div>
                        </div>--%>


                        <%--<div class="row" runat="server" id="idManual" visible="false">
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div class="col-md-3 mb-3">
                                        <label class="control-label" style="color: #555555">* Crop</label>
                                        <asp:TextBox ID="txtCrop" runat="server" CssClass="form-control border border-primary" MaxLength="2" onkeypress="return isNumber(event)" required=''></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCode" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtCrop" />


                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-floating mb-3">
                                            <label class="control-label" style="color: #555555">* Crop Year</label>
                                            <asp:TextBox ID="txtCropYear" CssClass="form-control border border-primary" runat="server" MaxLength="4" onkeypress="return onlyAlphaNumeric(event,this)" required=''></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCropYear" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtCropYear" />
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-floating mb-3">
                                            <label class="control-label" style="color: #555555">* Short Crop Year</label>
                                            <asp:TextBox ID="txtShortCropYear" CssClass="form-control border border-primary" runat="server" MaxLength="4" onkeypress="return onlyAlphaNumeric(event,this)" required=''></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ForeColor="Red" ValidationGroup="id1" ControlToValidate="txtShortCropYear" />
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>--%>


                        <%--<div class="row" runat="server" id="idManualButton" visible="false">
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
                        </div>--%>

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
