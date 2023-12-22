<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Form_StemReport_Report.aspx.cs" Inherits="Form_StemReport_Report" Title="Stem Reports" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxcontrol" %>


<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
/*Calendar Control CSS*/
.cal_Theme1 .ajax__calendar_container   {
background-color: #DEF1F4;
border:solid 1px #77D5F7;
}


.cal_Theme1 .ajax__calendar_header  {
background-color: #ffffff;
margin-bottom: 4px;
}


.cal_Theme1 .ajax__calendar_title,
.cal_Theme1 .ajax__calendar_next,
.cal_Theme1 .ajax__calendar_prev    {
color: #004080;
padding-top: 3px;
}


.cal_Theme1 .ajax__calendar_body    {
background-color: #ffffff;
border: solid 1px #77D5F7;
}


.cal_Theme1 .ajax__calendar_dayname {
text-align:center;
font-weight:bold;
margin-bottom: 4px;
margin-top: 2px;
color: #004080;
}


.cal_Theme1 .ajax__calendar_day {
color: #004080;
text-align:center;
}


.cal_Theme1 .ajax__calendar_hover .ajax__calendar_day,
.cal_Theme1 .ajax__calendar_hover .ajax__calendar_month,
.cal_Theme1 .ajax__calendar_hover .ajax__calendar_year,
.cal_Theme1 .ajax__calendar_active  {
color: #004080;
font-weight: bold;
background-color: #DEF1F4;
}


.cal_Theme1 .ajax__calendar_today   {
font-weight:bold;
}


.cal_Theme1 .ajax__calendar_other,
.cal_Theme1 .ajax__calendar_hover .ajax__calendar_today,
.cal_Theme1 .ajax__calendar_hover .ajax__calendar_title {
color: #bbbbbb;
}
     
     
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <%--<script type="text/javascript" language="javascript" src="~/js/validations.js" ></script>--%>
<center><h3 style="color:White; height:20%;" id=" ">STEM DEGRADATION REPORT <ajaxcontrol:toolkitscriptmanager ID="AjaxScriptManager" runat="server"/>

             </h3></center>
 <div id="Div1"  style="background-color:White; width:100%; height:100%;" align="center" >
    <center>
    <table style="border:Solid 3px #e4e4e4; width:100%;" align="center" class="col1">
     <tr>
             <td align="right">
                <asp:Label ID="lblOver13" runat = "server" 
                     Text="From Date"  ForeColor="Black" style="text-align: left"></asp:Label> 
             </td>
             <td align="left">
                    <asp:TextBox ID="txt_Report_Date" Width="152px" runat="server" 
                        CausesValidation="true" Height="21px" 
                        />
                    <AjaxControl:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_Report_Date"
                        Format="dd-MM-yyyy" CssClass=" cal_Theme1" />
                    </td>
             <td align="right">
                <asp:Label ID="lblOver14" runat = "server" 
                     Text="To Date"  ForeColor="Black" style="text-align: left"></asp:Label> 
             </td>
             <td align="left">
                    <asp:TextBox ID="txt_Report_Date0" Width="152px" runat="server" 
                        CausesValidation="true" Height="21px"/>
                    <AjaxControl:CalendarExtender ID="txt_Report_Date0_CalendarExtender" 
                     runat="server" TargetControlID="txt_Report_Date0"
                        Format="dd-MM-yyyy" CssClass=" cal_Theme1" />
                    </td>
             </tr>
             </table>
     <table style="border:Solid 3px #e4e4e4; width:100%;" align="center" class="col1">
     <tr>
             <td align="right">
                <asp:Label ID="lblDate0" runat = "server" Text="Crop"  ForeColor="Black"></asp:Label>
                     </td>
                     <td align="left">
                <asp:DropDownList ID="ddCrop" runat="server"  Width="151px">
                </asp:DropDownList>
                     </td>
                     <td align="right">
                <asp:Label ID="lblDate" runat = "server" Text="Scrap Grade"  ForeColor="Black"></asp:Label>
                     </td>
                     <td align="left">
               
                   
                <asp:DropDownList ID="ddGrade" runat="server" Width="151px" >
                </asp:DropDownList>
               
                   
                     </td >
                     <td align="right">
                <asp:Label ID="Label1" runat = "server" Text="Variety"  ForeColor="Black"></asp:Label>
                     </td >
                     <td align="left">
               
                   
                <asp:DropDownList ID="ddVariety" runat="server" Width="151px">
                </asp:DropDownList>
               
                   
             </td>
             
                     <td align="right">
                <asp:Label ID="Label2" runat = "server" Text="lamia Grade"  ForeColor="Black"></asp:Label>
                     </td >
                     <td align="left">
               
                   
                <asp:DropDownList ID="ddlamiaGrade" runat="server" Width="151px">
                </asp:DropDownList>
               
                   
             </td>
              </tr>
              
               </table> 
    </center>
</div>
  <div id="Div2"  style="background-color:White; width:100%; height:100%;" align="center" >
  <center>
     <table style="border:Solid 3px #e4e4e4; width:100%;" align="center" class="col1">        
       <tr>
                <td colspan="4" align="center">
                   
                    &nbsp;</td>
            </tr>
             </table> 
    </center>
</div>    
   <div id="Div3"  
        style="background-color:White; width:154%; height:100%; overflow: auto" 
        align="center" >
  <center>
     <table style="border:Solid 3px #e4e4e4; width:100%;" align="center" class="col1">
       <tr>
             <td align="left">
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="85px" 
                     onclick="btnSubmit_Click" />
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <asp:Button ID="btnclose" Text="Close" runat="server" Width="79px" 
             onclick="btnclose_Click"/>
             </td>
                </tr>
       <tr>
             <td align="center">
        
                <asp:GridView ID="GridViewSample" runat="server" AutoGenerateColumns="False" 
                        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
                     Font-Names="Verdana" Width="98%" ForeColor="Black"  Height="15px">
                     <AlternatingRowStyle BackColor="#FFD4BA" />
                      <HeaderStyle BackColor="#FF9E66" BorderColor="#CCCCCC" BorderStyle="Solid" 
                BorderWidth="1px" Font-Size="15px" Height="15px" />
            <RowStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
                Font-Size="13px" Height="20px" />
                       <Columns>
                                <asp:TemplateField  HeaderStyle-Width="3%" HeaderText="Crop">
                    <ItemTemplate>
                        <asp:Label ID="lblCrop" runat="server" Text='<%#Eval("Crop") %>'></asp:Label>
                    </ItemTemplate>

<HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
               
                <asp:TemplateField  HeaderStyle-Width="3%" HeaderText="Variety">
                <ItemTemplate>
                        <asp:Label ID="lblVariety" runat="server" Text='<%#Eval("Variety") %>'></asp:Label>
                    </ItemTemplate>

<HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField  HeaderStyle-Width="3%" HeaderText="Lamia Grade">
                <ItemTemplate>
                        <asp:Label ID="lblLamiaGrade" runat="server" Text='<%#Eval("LamiaGrade") %>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
           
                
                <asp:TemplateField  HeaderStyle-Width="3%" HeaderText="Grade">
                    <ItemTemplate>
                        <asp:Label ID="lblGrade" runat="server" Text='<%#Eval("Grade") %>'></asp:Label>
                    </ItemTemplate>

<HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderStyle-Width="3%" HeaderText="Date">
                    <ItemTemplate>
                        <asp:Label ID="lblDate1" runat="server" Text='<%#Eval("Date") %>'></asp:Label>
                    </ItemTemplate>

<HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
               
                <asp:TemplateField  HeaderStyle-Width="3%" HeaderText="Case No">
                <ItemTemplate>
                        <asp:Label ID="lblCaseNo" runat="server" Text='<%#Eval("CaseNo") %>'></asp:Label>
                    </ItemTemplate>

<HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField  HeaderStyle-Width="3%" HeaderText="Time">
                <ItemTemplate>
                        <asp:Label ID="lblTimeStem" runat="server" Text='<%#Eval("TimeStem") %>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
           
                
                <asp:TemplateField  HeaderStyle-Width="3%" HeaderText="Total Length">
                    <ItemTemplate>
                        <asp:Label ID="lblTotalLength" runat="server" Text='<%#Eval("TotalLength") %>'></asp:Label>
                    </ItemTemplate>

<HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderStyle-Width="3%" HeaderText="No of Stem Pieces">
                    <ItemTemplate>
                        <asp:Label ID="lblNoofStemPieces" runat="server" Text='<%#Eval("NoofStemPieces") %>'></asp:Label>
                    </ItemTemplate>

<HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
               
                <asp:TemplateField  HeaderStyle-Width="3%" HeaderText="Avg Stem Length ">
                <ItemTemplate>
                        <asp:Label ID="lblAvgStemLength" runat="server" Text='<%#Eval("AvgStemLength") %>'></asp:Label>
                    </ItemTemplate>

<HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField  HeaderStyle-Width="3%" HeaderText="Stem Weight">
                <ItemTemplate>
                        <asp:Label ID="lblStemWeight" runat="server" Text='<%#Eval("StemWeight") %>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
           
               
                <asp:TemplateField  HeaderStyle-Width="3%" HeaderText=">3/32(%)">
                <ItemTemplate>
                        <asp:Label ID="lblG3_32Percent" runat="server" Text='<%#Eval("G3_32Percent") %>'></asp:Label>
                    </ItemTemplate>

<HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
               
           
                
                <asp:TemplateField  HeaderStyle-Width="3%" HeaderText="Lesser than 3/32(%)">
                    <ItemTemplate>
                        <asp:Label ID="lblL3_32Percent" runat="server" Text='<%#Eval("L3_32Percent") %>'></asp:Label>
                    </ItemTemplate>

<HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
                 
               
                <asp:TemplateField  HeaderStyle-Width="3%" HeaderText="Sand & Dust (%)">
                <ItemTemplate>
                        <asp:Label ID="lblSandnDustPercent" runat="server" Text='<%#Eval("SandnDustPercent") %>'></asp:Label>
                    </ItemTemplate>

<HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField  HeaderStyle-Width="3%" HeaderText="No of Total Stem Pieces">
                <ItemTemplate>
                        <asp:Label ID="lblNoofTotalStemPieces" runat="server" Text='<%#Eval("NoofTotalStemPieces") %>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
           
                
               
                 <asp:TemplateField  HeaderStyle-Width="3%" HeaderText="Lesser than 1/2 Stem Pieces(%)">
                    <ItemTemplate>
                        <asp:Label ID="lblL1_2StemPiecesPercent" runat="server" Text='<%#Eval("L1_2StemPiecesPercent") %>'></asp:Label>
                    </ItemTemplate>

<HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
               
                
                <asp:TemplateField  HeaderStyle-Width="3%" HeaderText=">1/2 to < 4 Stem Pieces (%)">
                <ItemTemplate>
                        <asp:Label ID="lblG1_2to4StemPiecesPercent" runat="server" Text='<%#Eval("G1_2to4StemPiecesPercent") %>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
                   
                <asp:TemplateField  HeaderStyle-Width="3%" HeaderText=">4 Stem Pieces">
                <ItemTemplate>
                        <asp:Label ID="lblG4StemPiecesPercent" runat="server" Text='<%#Eval("G4StemPiecesPercent") %>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
                            </Columns>
                 </asp:GridView>

             </td>
                </tr>
       <tr>
             <td align="center">
                 &nbsp;</td>
                </tr>
          </table> 
    </center>
</div>   

  <div id="Div4"  style="background-color:White; width:100%; height:100%;" align="center" >
  <center>
     
      <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
          Text="Export to EXCEL" />
     
 </center>
 </div>
 <div>
<asp:Button ID="btnDownload" runat="server" Text="Download" onclick="btnDownload_Click" 
        Visible="False" />
     &nbsp;<asp:Label ID="lblMessage" ForeColor="Red" BackColor="Yellow" Font-Size=Large Font-Bold="true" runat="server" Text=""></asp:Label></div>
</asp:Content>





