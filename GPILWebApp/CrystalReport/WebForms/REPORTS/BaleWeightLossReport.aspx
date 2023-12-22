<%@ page title="" language="C#" masterpagefile="~/Master.Master" autoeventwireup="true" codebehind="BaleWeightLossReport.aspx.cs" inherits="GPILWebApp.CrystalReport.WebForms.REPORTS.BaleWeightLossReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="header">
        <h2 style="text-align: center; font-weight: bold; font-family: 'Times New Roman'; color: #438EB9">Bale WiseWeight Loss Report</h2>
        <asp:Label ID="lblMessage"  ForeColor="Red" BackColor="Yellow" Font-Size=Large Font-Bold="true" runat="server" Text=""></asp:Label>

    </div>


    <div class="row">


        <div class="col-sm-4">
            <div class="form-sm-3-md-4">
                <label class="control-label">Rec. Org. Code</label>
                <div>
                    <asp:dropdownlist runat="server" cssclass="form-control" id="ddlOrgCode">
                        <asp:ListItem Value="0">Select Org. Code</asp:ListItem>
                    </asp:dropdownlist>
                </div>
            </div>
        </div>
            <div class="col-sm-4">
        <div class="form-sm-3-md-4">
            <label class="control-label">From Date</label>
            <div class="form-control-sm">
               <%-- <asp:textbox id="txtFromDate" runat="server" class="form-control">                            
                </asp:textbox>--%>
                 <asp:textbox id="txtFromDate" runat="server" class="form-control" textmode="Date">
                </asp:textbox>
            </div>
        </div>
    </div>
    <div class="col-sm-4">
        <div class="form-sm-3-md-4">
            <label class="control-label">To Date</label>
            <div class="form-control-sm">
                <%--<asp:textbox id="txtToDate" runat="server" class="form-control">                            
                </asp:textbox>--%>
                 <asp:textbox id="txtToDate" runat="server" class="form-control" textmode="Date">
                  </asp:textbox>
            </div>
        </div>
    </div>
    </div>
<%--    <div class="row">
    
    </div>--%>

    <div class="row">
        <div class="col-sm-3"></div>
        <div class="col-sm-3">
            <div class="form-sm-3-md-4">
                <label class="control-label">.</label>
                <div class="form-control-sm">
                    <asp:radiobutton id="rdo20" runat="server" groupname="WeighmentType"
                        text="20% Weighment" forecolor="Black" autopostback="false"
                        checked="True" />
                </div>
            </div>
        </div>
        <div class="col-sm-3">
            <div class="form-sm-3-md-4">
                <label class="control-label">.</label>
                <div class="form-control-sm">
                    <asp:radiobutton id="rdo100" runat="server" groupname="WeighmentType"
                        text="100% Weighment" forecolor="Black" autopostback="false" />

                </div>
            </div>
        </div>
        <div class="col-sm-3"></div>
    </div>

    <label></label>
    <div class="col-sm-3">
        <div class="form-sm-3-md-4">
            <label></label>
            <div class="form-control-sm">

                <asp:button id="btnView" runat="server" cssclass="btn btn-sm btn-success" text="View" onclick="btnview_Click" />
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
                        <HeaderStyle BackColor="#71b7e0" BorderColor="#CCCCCC" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="13px" Height="30px" />
                        <RowStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="13px" Height="20px" ForeColor="Black" />
                        <Columns>                                          
                            <asp:TemplateField HeaderText="S.No." >
                               <ItemTemplate>
                                    <asp:Label ID="lblAtt1" runat="server" Text='<%#Eval("SNO") %>'></asp:Label>
                                </ItemTemplate>
                                 <HeaderStyle Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sender Code" >
                               <ItemTemplate>
                                    <asp:Label ID="lblAtt2" runat="server" Text='<%#Eval("SENDER_ORGN_CODE") %>'></asp:Label>
                                </ItemTemplate>
                                 <HeaderStyle Width="15%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Shipment Number" >
                               <ItemTemplate>
                                    <asp:Label ID="lblAtt3" runat="server" Text='<%#Eval("SHIPMENT_NO") %>'></asp:Label>
                                </ItemTemplate>
                                 <HeaderStyle Width="15%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Received No. of Bales" >
                               <ItemTemplate>
                                    <asp:Label ID="lblAtt4" runat="server" Text='<%#Eval("BALES") %>'></asp:Label>
                                </ItemTemplate>
                                 <HeaderStyle Width="8%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Marked Weight" >
                               <ItemTemplate>
                                    <asp:Label ID="lblAtt5" runat="server" Text='<%#Eval("MARKED_WT") %>'></asp:Label>
                                </ItemTemplate>
                                 <HeaderStyle Width="7%" />
                            </asp:TemplateField>
                            
                             <asp:TemplateField HeaderText="Received Weight" >
                               <ItemTemplate>
                                    <asp:Label ID="lblAtt6" runat="server" Text='<%#Eval("RECEIPT_WT") %>'></asp:Label>
                                </ItemTemplate>
                                 <HeaderStyle Width="7%" />
                            </asp:TemplateField>
                            
                            
                            <asp:TemplateField HeaderText="No. of Bales" >
                               <ItemTemplate>
                                    <asp:Label ID="lblAtt7" runat="server" Text='<%#Eval("W_BALES") %>'></asp:Label>
                                </ItemTemplate>
                                 <HeaderStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Marked Weight." >
                               <ItemTemplate>
                                    <asp:Label ID="lblAtt8" runat="server" Text='<%#Eval("W_DISPATCHED_WT") %>'></asp:Label>
                                </ItemTemplate>
                                 <HeaderStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Assc. Weight" >
                               <ItemTemplate>
                                    <asp:Label ID="lblAtt9" runat="server" Text='<%#Eval("W_RECEIPT_WT") %>'></asp:Label>
                                </ItemTemplate>
                                 <HeaderStyle Width="15%" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Difference (Kg)" >
                               <ItemTemplate>
                                    <asp:Label ID="lblAtt10" runat="server" Text='<%#Eval("DIFF_WT") %>'></asp:Label>
                                </ItemTemplate>
                                 <HeaderStyle Width="12%" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="(%)" >
                               <ItemTemplate>
                                    <asp:Label ID="lblAtt11" runat="server" Text='<%#Eval("PERC") %>'></asp:Label>
                                </ItemTemplate>
                                 <HeaderStyle Width="8%" />
                            </asp:TemplateField>
                            
                             <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkView" OnClick="GridViewSamp_SelectedIndexChanged">View</asp:LinkButton>
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                         
                                         
                        </Columns>
                    </asp:gridview>
    </div>
    <div class="row">
        <div class="col-sm-4"></div>
          <div class="col-sm-4">
      <div class="form-sm-3-md-4">
          <label></label>
          <div class="form-control-sm">
              <center>
                                  <asp:button id="btnExportToExcel0" text="Export to Excel" runat="server"
width="110px" height="25px" onclick="btnExportToExcel_Click" />
            
              </center>
               
              
          </div>
      </div>
  </div>
 <div class="col-sm-4"></div>
    </div>
  


    <div class="col-sm-12" style="width: 100%">
        <asp:gridview id="GridView1" runat="server"
            autogeneratecolumns="False" bordercolor="#CCCCCC" borderstyle="Solid"
            borderwidth="1px" font-names="Verdana"
            showfooter="True"
            width="100%" forecolor="Black"
            onpageindexchanging="GridView1_PageIndexChanging">
                        <AlternatingRowStyle BackColor="#FFD4BA" />
                        <FooterStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                        <PagerStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                        <HeaderStyle BackColor="#71b7e0" BorderColor="#CCCCCC" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Size="13px" Height="30px" />
                        <RowStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Size="13px" Height="20px" ForeColor="Black" />
                        <Columns>
                              
                                
                        <asp:TemplateField HeaderText="S.No." >
                           <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("SNO") %>'></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sender Number" >
                           <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("SENDER_ORGN_CODE") %>'></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle Width="15%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shipment No" >
                           <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%#Eval("SHIPMENT_NO") %>'></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle Width="25%" />
                        </asp:TemplateField>               
                        <asp:TemplateField HeaderText="Bale Number" >
                           <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%#Eval("GPIL_BALE_NUMBER") %>'></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle Width="25%" />
                        </asp:TemplateField>
                       
                          <asp:TemplateField HeaderText="Marked Weight" >
                           <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%#Eval("MARKED_WT") %>'></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle Width="15%" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Received Weight" >
                           <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%#Eval("RECEIPT_WEIGHT") %>'></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle Width="25%" />
                        </asp:TemplateField>
                       
                          <asp:TemplateField HeaderText="Weight Loss" >
                           <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%#Eval("DIFF_WT") %>'></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle Width="15%" />
                        </asp:TemplateField>
                        
                        
                          <asp:TemplateField HeaderText="(%)" >
                           <ItemTemplate>
                                <asp:Label ID="Label8" runat="server" Text='<%#Eval("PERC") %>'></asp:Label>
                            </ItemTemplate>
                             <HeaderStyle Width="15%" />
                        </asp:TemplateField>
                        
                    </Columns>
                    </asp:gridview>
    </div>

        <div class="row">
        <div class="col-sm-4"></div>
          <div class="col-sm-4">
       <div class="form-sm-3-md-4">
            <label></label>
            <div class="form-control-sm">
                <center>
                    <asp:button id="btnExportToExcel1" text="Export to Excel" runat="server"
    width="110px" height="25px" onclick="btnExportToExcel1_Click" />
                </center>
                
            </div>
        </div>
  </div>
 <div class="col-sm-4"></div>
    </div>

    <div class="col-sm-3">
      <%--  <div class="form-sm-3-md-4">
            <label></label>
            <div class="form-control-sm">
                <center>
                    <asp:button id="btnExportToExcel1" text="Export to Excel" runat="server"
    width="110px" height="25px" onclick="btnExportToExcel1_Click" />
                </center>
                
            </div>
        </div>--%>
    </div>

     <div class="row">


        <div class="col-sm-3">
            <div class="form-sm-3-md-4">
                <label class="control-label">.</label>
                <div>
                     <asp:Label ID="lblSummary"  ForeColor="Red" BackColor="Yellow" Font-Size=Large 
                        Font-Bold="True" runat="server"></asp:Label>
                </div>
            </div>
        </div>
          <div class="col-sm-3">
            <div class="form-sm-3-md-4">
                <label class="control-label">.</label>
                <div>
                    <asp:Label ID="lblSummary1"  ForeColor="Red" BackColor="Yellow" Font-Size=Large 
                        Font-Bold="True" runat="server"></asp:Label>
                </div>
            </div>
        </div>
          <div class="col-sm-3">
            <div class="form-sm-3-md-4">
                <label class="control-label">.</label>
                <div>
                    <asp:Label ID="lblSummary2"  ForeColor="Red" BackColor="Yellow" Font-Size=Large 
                        Font-Bold="True" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
