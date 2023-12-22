<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="FactoryDispatch.aspx.cs" Inherits="GPILWebApp.FactoryDispatch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">

        function PrintGridData() {

            var prtGrid = document.getElementById('<%=GridView1.ClientID %>');

            prtGrid.border = 0;

            var prtwin = window.open('', 'PrintGridViewData', 'left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1');

            prtwin.document.write(prtGrid.outerHTML);

            prtwin.document.close();

            prtwin.focus();

            prtwin.print();

            prtwin.close();

        }

    </script>
     <div class="page-header">

     <h2 style="text-align: center; color: #438EB9">FACTORY DISPATCH COMPLETE</h2>

 </div>
     <div class="row">
         <div class="col-sm-4">
    <div class="form-sm-3-md-4">
        <label class="control-label">LR Date</label>
        <div class="form-control-sm">
            <asp:textbox id="txt_Report_Date" runat="server" class="form-control" textmode="Date">
            </asp:textbox>
        </div>
    </div>
</div>
          <div class="col-sm-4">
    
         <div class="form-sm-3-md-4">
             <label class="control-label">Receiver Location Code</label>
             <div>
                 <asp:dropdownlist runat="server" cssclass="form-control" id="ddlLocationCode">
                 <asp:ListItem Value="0">Select</asp:ListItem>
             </asp:dropdownlist>
             </div>
         </div>
     

 </div>


                  <div class="col-sm-4">
   
        <div class="form-sm-3-md-4">
            <label class="control-label">Transporter Code</label>
            <div>
                <asp:dropdownlist runat="server" cssclass="form-control" id="ddlTransporterCode" AutoPostBack="true" OnSelectedIndexChanged="ddlTransporterCode_SelectedIndexChanged">
                <asp:ListItem Value="0">Select</asp:ListItem>
            </asp:dropdownlist>
            </div>
        </div>
    

</div>
                           <div class="col-sm-4">
   
        <div class="form-sm-3-md-4">
            <label class="control-label">Truck Number</label>
            <div>
                <asp:dropdownlist runat="server" cssclass="form-control" id="ddlTruckNumber">
                <asp:ListItem Value="0">Select</asp:ListItem>
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
         <asp:button id="btnViewNew" runat="server" cssclass="btn btn-sm btn-success" text="View" onclick="btnViewNew_Click" />
         <asp:button id="btnClose" runat="server" cssclass="btn btn-sm btn-success" text="Clear" onclick="btnClose_Click" />
         <asp:button id="btnFumigate" runat="server" cssclass="btn btn-sm btn-success" text="Complete" onclick="btnFumigate_Click" />


     </div>
 </div>
         <div class="col-sm-3">
     <div class="form-control-sm">
     </div>
 </div>
        </div>
     <hr />
    <div class="col-sm-12" style="width: 100%" >

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            BorderColor="#CCCCCC" BorderStyle="Solid"
            


             DataKeyNames="SHIPMENT_NO" OnPageIndexChanging="GridView1_PageIndexChanging" 
               ForeColor="Black" CellPadding="0" width="100%"
             onselectedindexchanged="GridView1_SelectedIndexChanged"  >
                        <FooterStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
<PagerStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
<HeaderStyle BackColor="#307ECC" BorderColor="#CCCCCC" BorderStyle="Solid"
    BorderWidth="1px" Font-Size="15px" Height="30px" />
<RowStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
    Font-Size="13px" Height="20px" />
                <Columns>
         
            <asp:TemplateField Visible="false">
                 <ItemTemplate>
                       <asp:CheckBox ID="chkDispatch" Visible="false" Enabled="false" Checked="true" runat ="server"/>
                 </ItemTemplate>
                 <HeaderTemplate>
                        <asp:Label ID="lblSelectAll" Width="60px"  runat ="server" Text ="Select All"></asp:Label>
                        <asp:CheckBox ID="ChkHeaderDispatch" runat ="server" Visible="false" Enabled="false" Checked="true" />
                  </HeaderTemplate>
                        
                 <ItemStyle HorizontalAlign="Center" />
                 <HeaderStyle HorizontalAlign="Center" />
                        
                </asp:TemplateField>
        <asp:BoundField DataField="SHIPMENT_NO" HeaderText="Shipment Number" >
            <ItemStyle HorizontalAlign="Center" />
            <HeaderStyle HorizontalAlign="Center" />
        </asp:BoundField>
        
         <asp:BoundField DataField="SENDER_ORGN_CODE" HeaderText="Sender Location" >
            <ItemStyle HorizontalAlign="Center" />
            <HeaderStyle HorizontalAlign="Center" />
        </asp:BoundField>
        
        <asp:BoundField DataField="SENDER_TRUCK_NO" HeaderText="Sender Truck" >
            <ItemStyle HorizontalAlign="Center" />
            <HeaderStyle HorizontalAlign="Center" />
        </asp:BoundField>
        
        <asp:BoundField DataField="SENDER_DATE" HeaderText="Sender Date" >
        <ItemStyle HorizontalAlign="Center" />
        <HeaderStyle HorizontalAlign="Center" />
        </asp:BoundField>
        
           
        <asp:BoundField DataField="RECEIVED_DATE" HeaderText="Received Date" Visible="false" >
        <ItemStyle HorizontalAlign="Center" />
        <HeaderStyle HorizontalAlign="Center" />
        </asp:BoundField>
        
        <asp:BoundField DataField="CASES" HeaderText="No's Cases" >
        <ItemStyle HorizontalAlign="Center" />
        <HeaderStyle HorizontalAlign="Center" />
        </asp:BoundField>
        
        <asp:BoundField DataField="QUANTITY" HeaderText="Quantity (Kgs)" >
        <ItemStyle HorizontalAlign="Center" />
        <HeaderStyle HorizontalAlign="Center" />
        </asp:BoundField>
        
        </Columns>
            </asp:GridView>
        </div>
    <div id="Div3"  style="background-color:White; width:100%; height:100%;  overflow: auto" align="left" >
  <asp:Label ID="lblMessage" ForeColor="Red" BackColor="Yellow" Font-Bold="true" runat="server" Text=""></asp:Label>
<br />
        </div>

   
</asp:Content>