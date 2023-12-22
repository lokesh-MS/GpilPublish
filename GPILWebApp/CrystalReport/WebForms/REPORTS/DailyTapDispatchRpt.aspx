<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="DailyTapDispatchRpt.aspx.cs" Inherits="GPILWebApp.CrystalReport.WebForms.REPORTS.DailyTapDispatchRpt" %>
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
                    <asp:TextBox ID="txt_From_Date" runat="server" class="form-control" TextMode="Date">
                    </asp:TextBox>
                </div>
            </div>
        </div>
        <div class="col-sm-4">
           <div class="col-sm-4">
            <div class="form-sm-3-md-4">
                <label class="control-label">Crop Year</label>
                <div>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCrop">
                        <asp:ListItem Value="0">Select To Crop</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
       
    </div>
    
        
       
        <div class="col-sm-4">
            <div class="form-sm-3-md-4">
                <label class="control-label">Variety</label>
                <div>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlVariety">
                        <asp:ListItem Value="0">Select To Variety</asp:ListItem>
                    </asp:DropDownList>
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
                <asp:Button ID="btnview" runat="server" CssClass="btn btn-sm btn-success" Text="View" OnClick="btnview_Click" />
                <asp:Button ID="btnclose" runat="server" CssClass="btn btn-sm btn-success" Text="Clear" OnClick="btnclose_Click" />
            </div>
        </div>
        <div class="col-sm-3">
            <div class="form-control-sm">
            </div>
        </div>
    </div>

    <hr />



    <div class="col-sm-12" style="width: 100%">
         <asp:GridView ID="GridViewSamp" runat="server"  
            AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="Solid" 
            BorderWidth="1px" Font-Names="Verdana" 
            ShowFooter="True" 
            Width="50%" ForeColor="Black" 
                   onpageindexchanging="GridViewSamp_PageIndexChanging">
            <AlternatingRowStyle BackColor="#FFD4BA" />
            <FooterStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
            <PagerStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
            <HeaderStyle BackColor="#FF9E66" BorderColor="#CCCCCC" BorderStyle="Solid" 
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
                     <HeaderStyle Width="15%" />
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
                     <HeaderStyle Width="10%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Value">
                <ItemTemplate>
                <asp:Label ID="lblValue" runat="server" Text='<%#Eval("VALUE") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="15%" />
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView>
               <asp:Button ID="btnExportToExcel0" Text="Export to Excel" 
             runat="server" 
                Width="110px" Height="25px" onclick="btnExportToExcel_Click"/>
               <br />
               <br />
               <asp:GridView ID="GridView1" runat="server"  
            AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="Solid" 
            BorderWidth="1px" Font-Names="Verdana" 
            ShowFooter="True" 
            Width="30%" ForeColor="Black" 
                   onpageindexchanging="GridView1_PageIndexChanging">
            <AlternatingRowStyle BackColor="#FFD4BA" />
            <FooterStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
            <PagerStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
            <HeaderStyle BackColor="#FF9E66" BorderColor="#CCCCCC" BorderStyle="Solid" 
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
        </asp:GridView>

         <asp:Button ID="btnExportToExcel1" Text="Export to Excel" 
             runat="server" 
                Width="110px" Height="25px" onclick="btnExportToExcel1_Click" />



        <asp:Label ID="lblSummary"  ForeColor="Red" BackColor="Yellow" Font-Size=Large 
        Font-Bold="True" runat="server"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Label ID="lblSummary1"  ForeColor="Red" BackColor="Yellow" Font-Size=Large 
        Font-Bold="True" runat="server"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Label ID="lblSummary2"  ForeColor="Red" BackColor="Yellow" Font-Size=Large 
        Font-Bold="True" runat="server"></asp:Label>



    </div>
</asp:Content>
