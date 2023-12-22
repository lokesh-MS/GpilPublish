<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="FarmerBankDetailsLoader.aspx.cs" Inherits="GPILWebApp.FarmerBankDetailsLoader" %>

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
                        <asp:Button ID="btnImport" runat="server" Text="Import"
                            OnClick="btnImport_Click" />
                    </div>
                </div>
            </div>
        </div>


        <h4 class="header green">TAP Purchase Details</h4>

        <div class="col-sm-12" style="width: 100%">
            <asp:GridView ID="GridViewSample" runat="server"
                AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="Solid"
                BorderWidth="1px" Font-Names="Verdana"
                OnPageIndexChanging="GridViewSample_PageIndexChanging"
                OnRowCancelingEdit="GridViewSample_RowCancelingEdit"
                OnRowCommand="GridViewSample_RowCommand"
                OnRowDeleting="GridViewSample_RowDeleting"
                OnRowEditing="GridViewSample_RowEditing"
                OnRowUpdating="GridViewSample_RowUpdating" PageSize="10" ShowFooter="true"
                Width="90%" ForeColor="Black" OnRowDataBound="GridViewSample_RowDataBound">
                <AlternatingRowStyle BackColor="#FFD4BA" />
                <FooterStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                <PagerStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                <HeaderStyle BackColor="#FF9E66" BorderColor="#CCCCCC" BorderStyle="Solid"
                    BorderWidth="1px" Font-Size="15px" Height="30px" />
                <RowStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                    Font-Size="13px" Height="20px" />
                <Columns>
                    <asp:TemplateField HeaderText="GPIL BALE NUMBER">
                        <ItemTemplate>
                            <asp:Label ID="lblbaleno" runat="server" Text='<%#Eval("GPIL_BALE_NUMBER") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddbaleno" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqbaleno" runat="server"
                                ControlToValidate="txtAddbaleno" ErrorMessage="*"
                                ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="15%" HeaderText="TB LOT NO">
                        <ItemTemplate>
                            <asp:Label ID="lbltblot" runat="server" Text='<%#Eval("TB_LOT_NO") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txttblot" runat="server" Text='<%#Eval("TB_LOT_NO") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddtblot" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqtblot" runat="server"
                                ControlToValidate="txtAddtblot" ErrorMessage="*"
                                ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                        </FooterTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="15%" HeaderText="TBGR NO">
                        <ItemTemplate>
                            <asp:Label ID="lbltbgrno" runat="server" Text='<%#Eval("TBGR_NO") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txttbgrno" runat="server"
                                Text='<%#Eval("TBGR_NO") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddtbgrno" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqtbgrno" runat="server"
                                ControlToValidate="txtAddtbgrno" ErrorMessage="*"
                                ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                        </FooterTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="15%" HeaderText="TB Grade">
                        <ItemTemplate>
                            <asp:Label ID="lbltbgrade" runat="server" Text='<%#Eval("TB_GRADE") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txttbgrade" runat="server"
                                Text='<%#Eval("TB_GRADE") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddtbgrade" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqtbgrade" runat="server"
                                ControlToValidate="txtAddtbgrade" ErrorMessage="*"
                                ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                        </FooterTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="15%" HeaderText="Net Wt">
                        <ItemTemplate>
                            <asp:Label ID="lblnetwt" runat="server" Text='<%#Eval("NET_WT") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtnetwt" runat="server"
                                Text='<%#Eval("NET_WT") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddnetwt" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqnetwt" runat="server"
                                ControlToValidate="txtAddnetwt" ErrorMessage="*"
                                ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                        </FooterTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="15%" HeaderText="Rate">
                        <ItemTemplate>
                            <asp:Label ID="lblrate" runat="server" Text='<%#Eval("RATE") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtrate" runat="server"
                                Text='<%#Eval("RATE") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddrate" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqrate" runat="server"
                                ControlToValidate="txtAddrate" ErrorMessage="*"
                                ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                        </FooterTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="15%" HeaderText="Buyer Grade">
                        <ItemTemplate>
                            <asp:Label ID="lblbuyergrade" runat="server" Text='<%#Eval("BUYER_GRADE") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtbuyergrade" runat="server"
                                Text='<%#Eval("BUYER_GRADE") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddbuyergrade" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqtbuyergrade" runat="server"
                                ControlToValidate="txtAddbuyergrade" ErrorMessage="*"
                                ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                        </FooterTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-Width="15%" HeaderText="Rejection Type">
                        <ItemTemplate>
                            <asp:Label ID="lblrejtype" runat="server" Text='<%# Eval("REJE_TYPE") %>' />

                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lbltemprejtype" runat="server" Text='<%# Eval("REJE_TYPE") %>' Visible="false" />
                            <asp:DropDownList ID="ddlrejtype" runat="server"></asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddAddrejtype" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqtrejtype" runat="server"
                                ControlToValidate="txtAddAddrejtype" ErrorMessage="*"
                                ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                        </FooterTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-Width="15%" HeaderText="Patta Charge">
                        <ItemTemplate>
                            <asp:Label ID="lblpattacharge" runat="server" Text='<%#Eval("PATTA_CHARGE") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtpattacharge" runat="server"
                                Text='<%#Eval("PATTA_CHARGE") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddpattacharge" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqtpattacharge" runat="server"
                                ControlToValidate="txtAddpattacharge" ErrorMessage="*"
                                ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                        </FooterTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-Width="15%" HeaderText="Orginization Code">
                        <ItemTemplate>
                            <asp:Label ID="lblorgcode" runat="server" Text='<%#Eval("ORGN_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtorgcode" runat="server"
                                Text='<%#Eval("ORGN_CODE") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddorgcode" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqtorgcode" runat="server"
                                ControlToValidate="txtAddorgcode" ErrorMessage="*"
                                ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                        </FooterTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-Width="15%" HeaderText="Buyer Code">
                        <ItemTemplate>
                            <asp:Label ID="lblbuyercode" runat="server" Text='<%#Eval("BUYER_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtbuyercode" runat="server"
                                Text='<%#Eval("BUYER_CODE") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddbuyercode" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqtbuyercode" runat="server"
                                ControlToValidate="txtAddbuyercode" ErrorMessage="*"
                                ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                        </FooterTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-Width="15%" HeaderText="Purchase Doc No">
                        <ItemTemplate>
                            <asp:Label ID="lblpurchdocno" runat="server" Text='<%#Eval("PURCH_DOC_NO") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtpurchdocno" runat="server"
                                Text='<%#Eval("PURCH_DOC_NO") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddpurchdocno" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqtpurchdocno" runat="server"
                                ControlToValidate="txtAddpurchdocno" ErrorMessage="*"
                                ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                        </FooterTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-Width="15%" HeaderText="Purchase Date">
                        <ItemTemplate>
                            <asp:Label ID="lblpurchdate" runat="server" Text='<%#Eval("PURCHASE_DATE") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtpurchdate" runat="server"
                                Text='<%#Eval("PURCHASE_DATE") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddpurchdate" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqtpurchdate" runat="server"
                                ControlToValidate="txtAddpurchdate" ErrorMessage="*"
                                ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                        </FooterTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-Width="15%" HeaderText="Crop">
                        <ItemTemplate>
                            <asp:Label ID="lblCrop" runat="server" Text='<%#Eval("CROP") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCrop" runat="server"
                                Text='<%#Eval("CROP") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddCrop" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqtCrop" runat="server"
                                ControlToValidate="txtAddCrop" ErrorMessage="*"
                                ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                        </FooterTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-Width="15%" HeaderText="Variety">
                        <ItemTemplate>
                            <asp:Label ID="lblvariety" runat="server" Text='<%#Eval("VARIETY") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtvariety" runat="server"
                                Text='<%#Eval("VARIETY") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddvariety" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqtvariety" runat="server"
                                ControlToValidate="txtAddvariety" ErrorMessage="*"
                                ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                        </FooterTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-Width="15%" HeaderText="Insert Sts">
                        <ItemTemplate>
                            <asp:Label ID="lblupdatests" runat="server" Text='<%#Eval("INS_STS") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtupdatests" runat="server"
                                Text='<%#Eval("INS_STS") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddupdatests" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqtupdatests" runat="server"
                                ControlToValidate="txtAddupdatests" ErrorMessage="*"
                                ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                        </FooterTemplate>
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
                        <FooterTemplate>
                            <asp:Button ID="btnInsertRecord0" runat="server" CommandName="Insert"
                                Text="Add" ValidationGroup="ValgrpCust" />
                        </FooterTemplate>
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
