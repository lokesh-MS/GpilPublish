<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="FarmerPurchaseLoanLoader.aspx.cs" Inherits="GPILWebApp.FarmerPurchaseLoanLoader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content" style="background-color: white">
        <div class="row mb-0">
            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label">Attach File </label>
                    <div>
                        <%--  <asp:dropdownlist runat="server" cssclass="form-control" id="ddlOrgnCode" autopostback="True" onselectedindexchanged="ddlOrgnCode_SelectedIndexChanged"></asp:dropdownlist>--%>
                        <asp:FileUpload ID="fileuploaditem" runat="server" />
                    </div>
                </div>
            </div>

            <div class="col-sm-3">
                <div class="form-sm-3">
                    <label class="control-label"></label>
                    <div class="form-control-sm">
                        <asp:Button ID="btnImport" runat="server" Text="Import" OnClick="btnImport_Click" />
                    </div>
                </div>
            </div>

            <h4 class="header green">TAP Purchase Details</h4>

            <div class="col-sm-12" style="width: 100%">
                <asp:GridView ID="GridViewSample" runat="server"
                    AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="Solid"
                    BorderWidth="1px" Font-Names="Verdana"
                    OnPageIndexChanging="GridViewSample_PageIndexChanging"
                    OnRowDeleting="GridViewSample_RowDeleting"
                    OnRowCancelingEdit="GridViewSample_RowCancelingEdit"
                    OnRowEditing="GridViewSample_RowEditing" OnRowUpdating="GridViewSample_RowUpdating" ShowFooter="True"
                    Width="65%" ForeColor="Black" OnRowDataBound="GridViewSample_RowDataBound">
                    <AlternatingRowStyle BackColor="#FFD4BA" />
                    <FooterStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                    <PagerStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                    <HeaderStyle BackColor="#FF9E66" BorderColor="#CCCCCC" BorderStyle="Solid"
                        BorderWidth="1px" Font-Size="15px" Height="30px" />
                    <RowStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                        Font-Size="13px" Height="20px" />
                    <Columns>
                        <asp:TemplateField HeaderStyle-Width="10%" HeaderText="CROP">
                            <ItemTemplate>
                                <asp:Label ID="lblCrop" runat="server" Text='<%#Eval("CROP") %>'></asp:Label>
                            </ItemTemplate>
                            <%-- <FooterTemplate>
                        <asp:TextBox ID="txtBuyerGradeGroup" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqbaleno" runat="server" 
                            ControlToValidate="txtBuyerGradeGroup" ErrorMessage="*" 
                            ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                    </FooterTemplate>--%>
                   
                        </asp:TemplateField>

                        <asp:TemplateField HeaderStyle-Width="10%" HeaderText="VARIETY">
                            <ItemTemplate>
                                <asp:Label ID="lblVariety" runat="server" Text='<%#Eval("VARIETY") %>'></asp:Label>
                            </ItemTemplate>
                            <%-- <FooterTemplate>
                        <asp:TextBox ID="txtBuyerGradeGroup" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqbaleno" runat="server" 
                            ControlToValidate="txtBuyerGradeGroup" ErrorMessage="*" 
                            ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                    </FooterTemplate>--%>
                    
                        </asp:TemplateField>

                        <asp:TemplateField HeaderStyle-Width="30%" HeaderText="FARMER CODE">
                            <ItemTemplate>
                                <asp:Label ID="lblFarmerCode" runat="server" Text='<%#Eval("FARMER_CODE") %>'></asp:Label>
                            </ItemTemplate>
                            <%-- <FooterTemplate>
                        <asp:TextBox ID="txtBuyerGradeGroup" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqbaleno" runat="server" 
                            ControlToValidate="txtBuyerGradeGroup" ErrorMessage="*" 
                            ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                    </FooterTemplate>--%>
                    
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="30%" HeaderText="OFFERED LOAN AMOUNT">
                            <ItemTemplate>
                                <asp:Label ID="lblLoanAmount" runat="server" Text='<%#Eval("LOAN_AMOUNT") %>'></asp:Label>
                            </ItemTemplate>

                            <%--
                    <EditItemTemplate>
                        <asp:TextBox ID="txtClassifierGradeGroup" runat="server" Text='<%#Eval("TB_LOT_NO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtClassifierGradeGroup" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqtblot" runat="server" 
                            ControlToValidate="txtClassifierGradeGroup" ErrorMessage="*" 
                            ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                    </FooterTemplate>--%>
                            <HeaderStyle Width="20%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="15%" HeaderText="Insert Sts">
                            <ItemTemplate>
                                <asp:Label ID="lblupdatests" runat="server" Text='<%#Eval("INS_STS") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtupdatests" runat="server"
                                    Text='<%#Eval("INS_STS") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <%-- <FooterTemplate>
                        <asp:TextBox ID="txtAddupdatests" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqtupdatests" runat="server" 
                            ControlToValidate="txtAddupdatests" ErrorMessage="*" 
                            ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                    </FooterTemplate>--%>
                            <HeaderStyle Width="15%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="15%" HeaderText="Edit/Delete">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEdit0" runat="server" CommandName="Edit" Text="Edit" />
                                <span onclick="return confirm('Are you sure want to delete?')">
                                    <asp:LinkButton ID="btnDelete0" runat="server" CommandName="Delete"
                                        Text="Delete" />
                                </span>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="btnUpdate0" runat="server" CommandName="Update"
                                    Text="Update" />
                                <asp:LinkButton ID="btnCancel0" runat="server" CommandName="Cancel"
                                    Text="Cancel" />
                            </EditItemTemplate>
                            <%--<FooterTemplate>
                        <asp:Button ID="btnInsertRecord0" runat="server" CommandName="Insert" 
                            Text="Add" ValidationGroup="ValgrpCust" />
                    </FooterTemplate>--%>
                            <HeaderStyle Width="15%" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div class="col-sm-12" style="width: 100%">
                <asp:Label ID="Label1" ForeColor="Blue" BackColor="Yellow" Font-Bold="true" runat="server" Text="Total Records - " Visible="false"></asp:Label>
                <asp:Label ID="lblgridcount" ForeColor="Blue" BackColor="Yellow" Font-Bold="true" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblMessage" ForeColor="Red" BackColor="Yellow" Font-Size="Large" Font-Bold="true" runat="server" Text=""></asp:Label>
            </div>

            <div class="col-sm-12" style="width: 100%">
                <asp:Button ID="btndwnerr" runat="server" Text="Download" OnClick="btndwnerr_Click"
                    Visible="False" />
            </div>

        </div>
</asp:Content>
