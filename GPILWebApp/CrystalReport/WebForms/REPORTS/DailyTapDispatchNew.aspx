<%@ page title="" language="C#" masterpagefile="~/Master.Master" autoeventwireup="true" codebehind="DailyTapDispatchNew.aspx.cs" inherits="GPILWebApp.CrystalReport.WebForms.REPORTS.DailyTapDispatchNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">

        <h2 style="text-align: center; color: #438EB9">DISPATCH REPORT</h2>

    </div>


    <div class="row">
        <div class="col-sm-4">
            <div class="form-sm-3-md-4">
                <label class="control-label">From Date</label>
                <div class="form-control-sm">
                    <asp:textbox id="txt_From_Date" runat="server" class="form-control" textmode="Date">
                    </asp:textbox>
                </div>
            </div>
        </div>
        <div class="col-sm-4">
            <%--<div class="col-sm-4">--%>
                <div class="form-sm-3-md-4">
                    <label class="control-label">Crop Year</label>
                    <div>
                        <asp:dropdownlist runat="server" cssclass="form-control" id="ddlCrop">
                        <asp:ListItem Value="0">Select To Crop</asp:ListItem>
                    </asp:dropdownlist>
                    </div>
                </div>
            <%--</div>--%>

        </div>



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
        <label></label>
    </div>

    <div class="row">
        <div class="col-sm-3">
            <div>
                <label></label>
            </div>
            <div class="form-control-sm">
                <asp:button id="btnview" runat="server" cssclass="btn btn-sm btn-success" text="View" onclick="btnview_Click" />
                <asp:button id="btnclose" runat="server" cssclass="btn btn-sm btn-danger" text="Clear" onclick="btnclose_Click" />
            </div>
        </div>
        <div class="col-sm-3">
            <div class="form-control-sm">
            </div>
        </div>
    </div>

    <hr />



    <div class="col-sm-12" style="width: 100%">
        <asp:gridview id="GridViewSamp" runat="server"
            autogeneratecolumns="False" bordercolor="#CCCCCC" borderstyle="Solid"
            borderwidth="1px" font-names="Verdana"
            showfooter="True"
            width="100%" forecolor="Black"
            onpageindexchanging="GridViewSamp_PageIndexChanging">
            <AlternatingRowStyle BackColor="#FFD4BA" />
            <FooterStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
            <PagerStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
            <HeaderStyle BackColor="#438EB9" BorderColor="#CCCCCC" BorderStyle="Solid" 
                BorderWidth="1px" Font-Size="13px" Height="30px" />
            <RowStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
                Font-Size="13px" Height="20px" ForeColor="Black" />
            <Columns>
                              
                                
                <asp:TemplateField HeaderText="S.No." >
                   <ItemTemplate>
                        <asp:Label ID="lblSNo" runat="server" Text='<%#Eval("SNO") %>'></asp:Label>
                    </ItemTemplate>
                     <HeaderStyle Width="5%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Shipment No." >
                   <ItemTemplate>
                        <asp:Label ID="lblShipmentNo" runat="server" Text='<%#Eval("SHIPMENT_NO") %>'></asp:Label>
                    </ItemTemplate>
                     <HeaderStyle Width="25%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TAP Org." >
                   <ItemTemplate>
                        <asp:Label ID="lblSenderOrg" runat="server" Text='<%#Eval("SENDER_ORGN_CODE") %>'></asp:Label>
                    </ItemTemplate>
                     <HeaderStyle Width="10%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sender No." >
                   <ItemTemplate>
                        <asp:Label ID="lblSenderNo" runat="server" Text='<%#Eval("SENDER_NO") %>'></asp:Label>
                    </ItemTemplate>
                     <HeaderStyle Width="8%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Bales" >
                   <ItemTemplate>
                        <asp:Label ID="lblBales" runat="server" Text='<%#Eval("BALES") %>'></asp:Label>
                    </ItemTemplate>
                     <HeaderStyle Width="7%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity" >
                   <ItemTemplate>
                        <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("QUANTITY") %>'></asp:Label>
                    </ItemTemplate>
                     <HeaderStyle Width="10%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Receiver Org." >
                   <ItemTemplate>
                        <asp:Label ID="lblReceiverOrg" runat="server" Text='<%#Eval("RECEIVER_ORGN_CODE") %>'></asp:Label>
                    </ItemTemplate>
                     <HeaderStyle Width="10%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Lorry No." >
                   <ItemTemplate>
                        <asp:Label ID="lblLorryNo" runat="server" Text='<%#Eval("SENDER_TRUCK_NO") %>'></asp:Label>
                    </ItemTemplate>
                     <HeaderStyle Width="15%" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Ave. Price" >
                   <ItemTemplate>
                        <asp:Label ID="lblAvePrice" runat="server" Text='<%#Eval("AVE_PRICE") %>'></asp:Label>
                    </ItemTemplate>
                     <HeaderStyle Width="15%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Value">
                <ItemTemplate>
                <asp:Label ID="lblValue" runat="server" Text='<%#Eval("VALUE") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="15%" />
                </asp:TemplateField>
                
            </Columns>
        </asp:gridview>
        <center>

        
        <asp:button id="btnExportToExcel0" text="Export to Excel" CssClass="btn btn-sm btn-success"
            runat="server"
            width="110px" height="40px" onclick="btnExportToExcel_Click" />

            </center>
        <br />
        <br />
        <asp:gridview id="GridView1" runat="server"
            autogeneratecolumns="False" bordercolor="#CCCCCC" borderstyle="Solid"
            borderwidth="1px" font-names="Verdana"
            showfooter="True"
            width="100%" forecolor="Black"
            onpageindexchanging="GridView1_PageIndexChanging">
            <AlternatingRowStyle BackColor="#FFD4BA" />
            <FooterStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
            <PagerStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
            <HeaderStyle BackColor="#438EB9" BorderColor="#CCCCCC" BorderStyle="Solid" 
                BorderWidth="1px" Font-Size="13px" Height="30px" />
            <RowStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
                Font-Size="13px" Height="20px" ForeColor="Black" />
            <Columns>
                              
                                
                <asp:TemplateField HeaderText="S.No." >
                   <ItemTemplate>
                        <asp:Label ID="lblSNo1" runat="server" Text='<%#Eval("SNO") %>'></asp:Label>
                    </ItemTemplate>
                     <HeaderStyle Width="10%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Grade" >
                   <ItemTemplate>
                        <asp:Label ID="lblGrade" runat="server" Text='<%#Eval("GRADE") %>'></asp:Label>
                    </ItemTemplate>
                     <HeaderStyle Width="35%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Bales" >
                   <ItemTemplate>
                        <asp:Label ID="lblBales1" runat="server" Text='<%#Eval("BALES") %>'></asp:Label>
                    </ItemTemplate>
                     <HeaderStyle Width="15%" />
                </asp:TemplateField>               
                <asp:TemplateField HeaderText="Quantity" >
                   <ItemTemplate>
                        <asp:Label ID="lblQuantity1" runat="server" Text='<%#Eval("QUANTITY") %>'></asp:Label>
                    </ItemTemplate>
                     <HeaderStyle Width="25%" />
                </asp:TemplateField>
               
                  <asp:TemplateField HeaderText="Ave. Price" >
                   <ItemTemplate>
                        <asp:Label ID="lblAvePrice1" runat="server" Text='<%#Eval("AVE_PRICE") %>'></asp:Label>
                    </ItemTemplate>
                     <HeaderStyle Width="15%" />
                </asp:TemplateField>
                
            </Columns>
        </asp:gridview>
        <center>
        <asp:button id="btnExportToExcel1" text="Export to Excel" CssClass="btn btn-sm btn-success"
            runat="server"
            width="110px" height="40px"  onclick="btnExportToExcel1_Click" />

            </center>
        <div style="margin-top:20px">
            <center>
                            <asp:label id="lblSummary" forecolor="Red" backcolor="Yellow" font-size="Large"
    font-bold="True" runat="server"></asp:label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:label id="lblSummary1" forecolor="Red" backcolor="Yellow" font-size="Large"
    font-bold="True" runat="server"></asp:label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:label id="lblSummary2" forecolor="Red" backcolor="Yellow" font-size="Large"
    font-bold="True" runat="server"></asp:label>
            </center>
            

        </div>
       


    </div>

</asp:Content>
