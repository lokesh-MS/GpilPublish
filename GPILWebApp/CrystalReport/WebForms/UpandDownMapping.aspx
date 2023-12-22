<%@ page title="" language="C#" masterpagefile="~/Master.Master" autoeventwireup="true" codebehind="UpandDownMapping.aspx.cs" inherits="GPILWebApp.CrystalReport.WebForms.REPORTS.UpandDownMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="header">
        <h2 style="text-align: center; font-weight: bold; font-family: 'Times New Roman'; color: #438EB9">Up and down mapping </h2>
    </div>
    <div class="row">
        <div class="col-sm-4">
            <div class="form-sm-3-md-4">
                <label class="control-label">Crop Year</label>
                <div>
                    <asp:dropdownlist runat="server" cssclass="form-control" id="ddlCrop">
                        <asp:ListItem Value="0">Select To Crop</asp:ListItem>
                    </asp:dropdownlist>
                </div>
            </div>
        </div>

        <label></label>
        <div class="col-sm-4">
            <div class="form-sm-3-md-4">
                <label class="control-label">Variety</label>
                <div>
                    <asp:dropdownlist runat="server" cssclass="form-control" id="ddlVariety">
                        <asp:ListItem Value="0">Select To Variety</asp:ListItem>
                    </asp:dropdownlist>
                </div>
            </div>
        </div>
    </div>


    <div class="row">
        <div class="col-sm-3">
            <div>
                <label></label>
            </div>
            <div class="form-control-sm">
                <asp:button id="btnview" runat="server" cssclass="btn btn-sm btn-success" text="View" onclick="btnView_Click" />
            </div>
        </div>

        <div class="col-sm-3">

            <div class="form-control-sm">
            </div>
        </div>

    </div>




    <div class="col-sm-12 mb-0" style="width: 100%">
        <asp:gridview id="GridViewSamp" runat="server"
            autogeneratecolumns="False" bordercolor="#CCCCCC" borderstyle="Solid"
            borderwidth="1px" font-names="Verdana"
            onpageindexchanging="GridViewSamp_PageIndexChanging"
            onrowcancelingedit="GridViewSamp_RowCancelingEdit"
            onrowediting="GridViewSamp_RowEditing" showfooter="True"
            width="100%" forecolor="Black" onrowdatabound="GridViewSamp_RowDataBound">
            <AlternatingRowStyle BackColor="#FFD4BA" />
            <FooterStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
            <PagerStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
            <HeaderStyle BackColor="#438EB9" BorderColor="#CCCCCC" BorderStyle="Solid" 
                BorderWidth="1px" Font-Size="15px" Height="30px" />
            <RowStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
                Font-Size="13px" Height="20px" />
            <Columns>
             <asp:TemplateField HeaderStyle-Width="10%" HeaderText="CROP" >
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
              
             <asp:TemplateField HeaderStyle-Width="10%" HeaderText="VARIETY" >
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
                
             <asp:TemplateField HeaderStyle-Width="30%" HeaderText="BUYER GRADE GROUP" >
                   <ItemTemplate>
                        <asp:Label ID="lblBuyerGradeGroup" runat="server" Text='<%#Eval("BUYER_GRADE_GRP") %>'></asp:Label>
                    </ItemTemplate>
                     <%-- <FooterTemplate>
                        <asp:TextBox ID="txtBuyerGradeGroup" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqbaleno" runat="server" 
                            ControlToValidate="txtBuyerGradeGroup" ErrorMessage="*" 
                            ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                    </FooterTemplate>--%>
                    
                </asp:TemplateField>
              
             <asp:TemplateField HeaderStyle-Width="30%" HeaderText="CLASSIFIER GRADE GROUP">
                  <ItemTemplate>
                        <asp:Label ID="lblClassifierGradeGroup" runat="server" Text='<%#Eval("CLASSIFIER_GRADE_GRP") %>'></asp:Label>
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
                  
                </asp:TemplateField>
                
             <asp:TemplateField HeaderStyle-Width="20%" HeaderText="PAIR TYPE">
                    <ItemTemplate>
                     <asp:DropDownList ID="ddlPairType" runat="server" Width='100px' ></asp:DropDownList>
                        <%--<asp:Label ID="lblrejtype" runat="server" Text='<%# Eval("PAIR_TYPE") %>'></asp:Label>--%>
                    </ItemTemplate>
                    <%--<EditItemTemplate>
                        <asp:Label ID="lbltemprejtype" runat="server" Text='<%# Eval("PAIR_TYPE") %>' 
                            Visible="false" />
                        <asp:DropDownList ID="ddlPairType" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>--%>
                    <%--<FooterTemplate>
                        <asp:TextBox ID="txtAddAddrejtype" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqtrejtype" runat="server" 
                            ControlToValidate="txtAddAddrejtype" ErrorMessage="*" 
                            ValidationGroup="ValgrpCust"></asp:RequiredFieldValidator>
                    </FooterTemplate>--%>
                   <%-- <HeaderStyle Width="30%" />--%>
                </asp:TemplateField>
                              
                                
               <%-- <asp:TemplateField HeaderStyle-Width="15%" HeaderText="Edit/Delete">
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
                </asp:TemplateField>--%>
            </Columns>
        </asp:gridview>
    </div>



    <div class="row">
        <div class="col-sm-3">
            <div>
                <label></label>
            </div>
            <div class="form-control-sm">
                <asp:label id="lblRecordCount" forecolor="Blue" backcolor="White" font-size="Small" font-bold="true" runat="server" text=""></asp:label>
            </div>
        </div>

        <div class="col-sm-3">

            <div class="form-control-sm">
                <asp:button runat="server" id="btnAddToMaster"
                    postbackurl="~/Form_BuyerVsClassifierGradeMaster.aspx"
                    text="Add to Master" width="109px" onclick="btnAddToMaster_Click" />
                <asp:button runat="server" id="btnExportToExcel" text="Export To Excel" width="110px"
                    onclick="btnExportToExcel_Click" />
                <asp:label id="lblMessage" forecolor="Red" backcolor="Yellow" font-size="Large" font-bold="true" runat="server" text=""></asp:label>
            </div>
        </div>

    </div>






</asp:Content>
