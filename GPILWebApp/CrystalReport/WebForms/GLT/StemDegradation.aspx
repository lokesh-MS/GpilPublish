<%--<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="StemDegradation.aspx.cs" Inherits="GPILWebApp.CrystalReport.WebForms.GLT.StemDegradation" %>--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StemDegradation.aspx.cs" Inherits="GPILWebApp.CrystalReport.WebForms.GLT.StemDegradation" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        .style1
     {
         height: 25px;
     }
     
     
        </style>
   <script language="javascript" type="text/javascript" >  
       function isNumberkey(evt) {
           var charCode = (evt.which) ? evt.which : event.keyCode
           if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
               return false;
           return true;
       }
       function integersOnly(obj) {
           obj.value = obj.value.replace(/[^0-9-.]/g, '');
       }
   </script>
</head>
<body >

    <%--<script type="text/javascript" language="javascript" src="~/js/validations.js" ></script>--%>
<div class="header">
    <h2 style="text-align: center; font-weight: bold; font-family: 'Times New Roman'; color: #438EB9">STEM DEGRADATION</h2>
</div>
 <div id="Div1"  style="background-color:White; width:100%; height:100%;" align="center" >
    <center>
    </center>
</div>

  <div id="Div4"  style="background-color:White; width:100%; height:100%;" align="center" >
  <center>
     
    <center>

            <form id="form1" runat="server">
                
                  <asp:RadioButton ID="RadioButton2" runat="server" groupname="Group1" ForeColor="Black" 
      Text="Enter Manually"
      AutoPostBack="true" CausesValidation="True" 
      oncheckedchanged="RadioButton2_CheckedChanged"  />
                  <asp:RadioButton ID="RadioButton1" runat="server" groupname="Group1" ForeColor="Black" 
      Text="Import From Excel" 
      AutoPostBack="true" CausesValidation="True" 
      oncheckedchanged="RadioButton1_CheckedChanged"  style="margin-top:10px"/>

      
 
 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
 


<%--          <asp:Button ID="Button2" runat="server" onclick="Button1_Click" Text="Complete" 
      Width="135px" BackColor="#87B87F" Height="30px"  ForeColor="White"/>--%>
        <div>
            <%--<asp:FileUpload ID="FileUpload2" runat="server" />--%>
           <%-- <asp:Button ID="Button3" runat="server" Text="Import" OnClick="btnImport_Click" />--%>
            <asp:Label ID="Label4" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            <%--<asp:GridView ID="GridViewSample" runat="server" AutoGenerateColumns="true" Visible="false"></asp:GridView>--%>
        </div>
                <%-- table code start--%>

                      <asp:Label ID="Label12" runat="server" ForeColor="Black" 
                           Text="Attach Excel File"></asp:Label>
                       &nbsp;&nbsp;&nbsp;&nbsp;
                       <asp:FileUpload ID="FileUpload01" runat="server" style="border:1px solid gray"/>
                       &nbsp;&nbsp;&nbsp;&nbsp;
                 <div style="margin-top:7px"></div>
                       <asp:Button ID="btnImport" runat="server" Height="30px" Text="Import" 
                           onclick="btnImport_Click" Width="10%" BackColor="#87B87F" ForeColor="White"/>

   
     <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Completed" 
         Width="135px" BackColor="#87B87F" Height="30px"  ForeColor="White"/>

                <%-- table1 code end --%>
                <%-- tabel2 code s--%>
                
                <%-- tablle2 code end --%>
                <%-- grideView code Start #FF9E66--%>
                <div style="margin-top:10px">
                                                    <asp:GridView ID="GridViewSample" runat="server" AutoGenerateColumns="False" 
                        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
                     Font-Names="Verdana" Width="98%" ForeColor="Black"  Height="15px">
                     <AlternatingRowStyle BackColor="#FFD4BA" />
                      <HeaderStyle BackColor="#307ECC" BorderColor="#CCCCCC" BorderStyle="Solid" 
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
                        <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Date") %>'></asp:Label>
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
           <asp:TemplateField  HeaderStyle-Width="3%" HeaderText=">3/32">
                    <ItemTemplate>
                        <asp:Label ID="lblG3_32" runat="server" Text='<%#Eval("G3_32") %>'></asp:Label>
                    </ItemTemplate>

<HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
               
                <asp:TemplateField  HeaderStyle-Width="3%" HeaderText=">3/32(%)">
                <ItemTemplate>
                        <asp:Label ID="lblG3_32Percent" runat="server" Text='<%#Eval("G3_32Percent") %>'></asp:Label>
                    </ItemTemplate>

<HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField  HeaderStyle-Width="3%" HeaderText="<3/32">
                <ItemTemplate>
                        <asp:Label ID="lblL3_32" runat="server" Text='<%#Eval("L3_32") %>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
           
                
                <asp:TemplateField  HeaderStyle-Width="3%" HeaderText="<3/32(%)">
                    <ItemTemplate>
                        <asp:Label ID="lblL3_32Percent" runat="server" Text='<%#Eval("L3_32Percent") %>'></asp:Label>
                    </ItemTemplate>

<HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderStyle-Width="3%" HeaderText="Sand & Dust">
                    <ItemTemplate>
                        <asp:Label ID="lblSandnDust" runat="server" Text='<%#Eval("SandnDust") %>'></asp:Label>
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
           
                
                <asp:TemplateField  HeaderStyle-Width="3%" HeaderText="No of <1/2 Stem Pieces">
                    <ItemTemplate>
                        <asp:Label ID="lblNoofL1_2StemPieces" runat="server" Text='<%#Eval("NoofL1_2StemPieces") %>'></asp:Label>
                    </ItemTemplate>

<HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderStyle-Width="3%" HeaderText="<1/2 Stem Pieces(%)">
                    <ItemTemplate>
                        <asp:Label ID="lblL1_2StemPiecesPercent" runat="server" Text='<%#Eval("L1_2StemPiecesPercent") %>'></asp:Label>
                    </ItemTemplate>

<HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
               
                <asp:TemplateField  HeaderStyle-Width="3%" HeaderText="No of >1/2<4 Stem Pieces ">
                <ItemTemplate>
                        <asp:Label ID="lblNoofG1_2L4StemPieces" runat="server" Text='<%#Eval("NoofG1_2L4StemPieces") %>'></asp:Label>
                    </ItemTemplate>

<HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField  HeaderStyle-Width="3%" HeaderText=">1/2 to < 4 Stem Pieces (%)">
                <ItemTemplate>
                        <asp:Label ID="lblG1_2to4StemPiecesPercent" runat="server" Text='<%#Eval("G1_2to4StemPiecesPercent") %>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
                    <asp:TemplateField  HeaderStyle-Width="3%" HeaderText="No of >4 Stem Pieces ">
                <ItemTemplate>
                        <asp:Label ID="lblNoofG4StemPieces" runat="server" Text='<%#Eval("NoofG4StemPieces") %>'></asp:Label>
                    </ItemTemplate>

<HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField  HeaderStyle-Width="3%" HeaderText=">4StemPieces">
                <ItemTemplate>
                        <asp:Label ID="lblG4StemPiecesPercent" runat="server" Text='<%#Eval("G4StemPiecesPercent") %>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
                
                
                
                
                <asp:TemplateField  HeaderStyle-Width="3%" HeaderText="SUM">
                <ItemTemplate>
                        <asp:Label ID="lblSum" runat="server" Text='<%#Eval("SUM") %>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle Width="3%"></HeaderStyle>
                </asp:TemplateField>
                            </Columns>
                 </asp:GridView>
                </div>
                               

                <%-- GrideView End --%>



                <%-- table code ddcrop --%>
                     <table style=" width:100%; height: 93px;" 
          align="center" class="col1">
        <tr>
            <tr>
       <td colspan="12">&nbsp;</td>
   </tr>
            <td align="right" >
                <asp:Label ID="lblDate0" runat = "server" Text="Crop"  ForeColor="Black"></asp:Label>
                <asp:Label ID="Label29" runat="server" ForeColor="Red" text="*" 
                    Visible="False" style="margin-right: 10px"></asp:Label>
            </td>
            <td align="left" >
                <asp:DropDownList ID="ddCrop" runat="server" Width="151px">
                </asp:DropDownList>
            </td>
            <td align="right" >
                <asp:Label ID="lblOver13" runat = "server" 
                     Text="Date"  ForeColor="Black" style="text-align: left"></asp:Label> 
                <asp:Label ID="Label41" runat="server" ForeColor="Red" text="*" 
                    Visible="False" style="margin-right: 10px"></asp:Label>
            </td>
            <td align="left" >
                    <asp:TextBox ID="txtDate" Width="152px" runat="server" 
                        CausesValidation="true" Height="21px" TextMode="Date"/>
                    </td>
      
            <td align="right" >
                <asp:Label ID="lblDate" runat = "server" Text="Grade"  ForeColor="Black"></asp:Label>
                <asp:Label ID="Label30" runat="server" ForeColor="Red" text="*" 
                    Visible="False" style="margin-right: 10px"></asp:Label>
                &nbsp;</td>
            <td align="left"  >
               
                   
                <asp:DropDownList ID="ddGrade" runat="server" Width="151px" >
                </asp:DropDownList>
               
                   
            </td>
              </tr>
         <tr>
       <td colspan="12">&nbsp;</td>
   </tr>

  <tr>

            <td align="right" >
                <asp:Label ID="lblOver12" runat = "server" 
                     Text="Time"  ForeColor="Black" style="text-align: left"></asp:Label> 
                <asp:Label ID="Label42" runat="server" ForeColor="Red" text="*" 
                    Visible="False" style="margin-right: 10px"></asp:Label>
            </td>
            <td align="left" >
                <asp:TextBox ID="txtTime" runat="server" Height="21px" 
                    Width="153px" ></asp:TextBox></td>
     
            <td align="right" >
                <asp:Label ID="Label1" runat = "server" Text="Variety"  ForeColor="Black"></asp:Label>
                <asp:Label ID="Label31" runat="server" ForeColor="Red" text="*" 
                    Visible="False" style="margin-right: 10px"></asp:Label>
            </td>
            <td align="left"  >
               
                   
                <asp:DropDownList ID="ddVariety" runat="server" Width="151px">
                </asp:DropDownList>
               
                   
            </td>
            <td align="right" >
                <asp:Label ID="Label2" runat = "server" 
                     Text="CaseNo"  ForeColor="Black" style="text-align: left"></asp:Label> 
                <asp:Label ID="Label43" runat="server" ForeColor="Red" text="*" 
                    Visible="False" style="margin-right: 10px"></asp:Label>
                   </td>
            <td align="left" >
                <asp:TextBox ID="txtCaseNo" runat="server" Height="21px" 
                    Width="153px" ontextchanged="txtEndRunNo_TextChanged" 
                    Onkeypress="return isNumberkey(event)" ></asp:TextBox></td>
         </tr>
   
         <tr>
       <td colspan="12">&nbsp;</td>
   </tr>

          <tr>

       
            <td align="right" >
                <asp:Label ID="Label3" runat = "server" Text="Lamina Grade"  ForeColor="Black"></asp:Label>
                <asp:Label ID="Label32" runat="server" ForeColor="Red" text="*" 
                    Visible="False" style="margin-right: 10px"></asp:Label>
            </td>
            <td align="left"  >
               
                   
                <asp:DropDownList ID="ddLamiaGrade" runat="server" Width="151px">
                </asp:DropDownList>
               
                   
            </td>
            <td align="right" >
                <asp:Label ID="Label16" runat = "server" 
                     Text="Total Length"  ForeColor="Black" style="text-align: left"></asp:Label> 
                <asp:Label ID="Label44" runat="server" ForeColor="Red" text="*" 
                    Visible="False" style="margin-right: 10px"></asp:Label>
                 </td>
            <td align="left" >
                <asp:TextBox ID="txtTotalLength" runat="server" Height="21px" 
                    Width="153px" ontextchanged="txtEndRunNo_TextChanged" 
                    Onkeypress="return isNumberkey(event)" AutoPostBack="true" ></asp:TextBox></td>
     
            <td align="right" class="style1" >
                <asp:Label ID="Label5" runat = "server" Text="No Of Stem Pieces"  ForeColor="Black"></asp:Label>
                <asp:Label ID="Label33" runat="server" ForeColor="Red" text="*" 
                    Visible="False" style="margin-right: 10px"></asp:Label>
            </td>
            <td align="left" class="style1"  >
               
                   
                <asp:TextBox ID="txtStemPieces" runat="server" Height="21px" 
                    Width="153px" Onkeypress="return isNumberkey(event)" 
                    ontextchanged="txtStemPieces_TextChanged" AutoPostBack="true" ></asp:TextBox>
               
                   
            </td>
               </tr>  
         <tr>
       <td colspan="12">&nbsp;</td>
   </tr>

      <tr>
            <td align="right" class="style1" >
                <asp:Label ID="Label6" runat = "server" 
                     Text="Avg Stem Length"  ForeColor="Black" style="text-align: left; margin-right: 10px" ></asp:Label> </td>
            <td align="left" class="style1" >
                <asp:TextBox ID="txtAvgStemLength" runat="server" Height="21px" 
                    Width="153px" ontextchanged="txtEndRunNo_TextChanged"  ReadOnly="true" Onkeypress="return isNumberkey(event)" AutoPostBack="true" style="margin-right: 10px"></asp:TextBox></td>
      
            <td align="right" >
                <asp:Label ID="Label8" runat = "server" 
                     Text="> 3/32"  ForeColor="Black" style="text-align: left"></asp:Label> 
                <asp:Label ID="Label34" runat="server" ForeColor="Red" text="*" 
                    Visible="False" style="margin-right: 10px"></asp:Label>
            </td>
            <td align="left"  >
               
                   
                <asp:TextBox ID="txt3_32" runat="server" Height="21px" 
                    Width="153px" Onkeypress="return isNumberkey(event)"  ></asp:TextBox>
               
                   
            </td>
            <td align="right" >
                <asp:Label ID="Label9" runat = "server" Text=">3/32(%) "  ForeColor="Black" style="margin-right: 10px"></asp:Label>
                      </td>
            <td align="left" >
               
                   
                <asp:TextBox ID="txt3_32Percent" runat="server" Height="21px" 
                    Width="153px" Onkeypress="return isNumberkey(event)" 
                    ontextchanged="txt3_32Percent_TextChanged" ></asp:TextBox>
               
                   
                      </td>


        </tr>   
         <tr>
       <td colspan="12">&nbsp;</td>
   </tr>

        
                  <tr>
            <td align="right" >
                <asp:Label ID="Label17" runat = "server" Text="<3_32"  ForeColor="Black"></asp:Label>
                <asp:Label ID="Label35" runat="server" ForeColor="Red" text="*" 
                    Visible="False" style="margin-right: 10px"></asp:Label>
            </td>
            <td align="left"  >
               
                   
                <asp:TextBox ID="txtL3_32" runat="server" Height="21px" 
                    Width="153px" Onkeypress="return isNumberkey(event)"  ></asp:TextBox>
            
                   
            </td>
            <td align="right" >
                <asp:Label ID="Label10" runat = "server" 
                     Text="&lt; 3/32(%)"  ForeColor="Black"  style="text-align: left; margin-right: 10px"></asp:Label> 
                      </td>
            <td align="left" >
               
                   
                <asp:TextBox ID="txtL3_32Percent" runat="server" Height="21px" 
                    Width="153px" ontextchanged="txtmpoisture_TextChanged" Onkeypress="return isNumberkey(event)"  ></asp:TextBox>
               
                   
                      </td>
      
            <td align="right" >
                <asp:Label ID="Label18" runat = "server" Text="Sand and Dust"  ForeColor="Black"></asp:Label>
                <asp:Label ID="Label36" runat="server" ForeColor="Red" text="*" 
                    Visible="False" style="margin-right: 10px"></asp:Label>
            </td>
            <td align="left"  >
               
                   
                <asp:TextBox ID="txtSandnDust" runat="server" Height="21px" 
                    Width="153px" Onkeypress="return isNumberkey(event)" 
                    ontextchanged="txtSandnDust_TextChanged" AutoPostBack="true"  ></asp:TextBox>
               
                   
            </td>
                        </tr> 
         <tr>
       <td colspan="12">&nbsp;</td>
   </tr>

      
            <tr>

            <td align="right" >
                <asp:Label ID="Label19" runat = "server" Text="Sand and Dust (%)"  ForeColor="Black" style="margin-right: 10px"></asp:Label>
                      </td>
            <td align="left" >
               
                   
                <asp:TextBox ID="txtSandnDustPercent" runat="server" Height="21px" 
                    Width="153px" Onkeypress="return isNumberkey(event)"  ></asp:TextBox>
               
                   
                      </td>
    
            <td align="right" >
                <asp:Label ID="Label25" runat = "server" Text="No of <1/2 Stem Pieces"  ForeColor="Black"></asp:Label>
                <asp:Label ID="Label37" runat="server" ForeColor="Red" text="*" 
                    Visible="False" style="margin-right: 10px"></asp:Label>
            </td>
            <td align="left"  >
               
                   
                <asp:TextBox ID="txtNoofL1_2StemPieces" runat="server" Height="21px" 
                    Width="153px" Onkeypress="return isNumberkey(event)"  ></asp:TextBox>
               
                   
            </td>
            <td align="right" >
                <asp:Label ID="Label7" runat = "server" Text="Stem Weight"  ForeColor="Black" style="margin-right: 10px"></asp:Label>
                      </td>
            <td align="left" >
               
                   
                <asp:TextBox ID="txtStemWeight" runat="server" Height="21px" 
                    Width="153px" ontextchanged="txtStemWeight_TextChanged" Onkeypress="return isNumberkey(event)"  ></asp:TextBox>
               
                   
                      </td>
        </tr>   
         <tr>
       <td colspan="12">&nbsp;</td>
   </tr>

        
                  <tr>
            <td align="right" >
                <asp:Label ID="Label24" runat = "server" Text="No of  > 4 Stem Pieces"  ForeColor="Black"></asp:Label>
                <asp:Label ID="Label38" runat="server" ForeColor="Red" text="*" 
                    Visible="False" style="margin-right: 10px"></asp:Label>
            </td>
            <td align="left"  >
               
                   
                <asp:TextBox ID="txtNoofG4StemPieces" runat="server" Height="21px" 
                    Width="153px" Onkeypress="return isNumberkey(event)" 
                    ontextchanged="txtNoofG4StemPieces_TextChanged" AutoPostBack="true"  ></asp:TextBox>
               
                   
            </td>
            <td align="right" >
                <asp:Label ID="Label26" runat = "server" Text="< 1/2 Stem Pieces (%)"  ForeColor="Black" style="margin-right: 10px"></asp:Label>
                      </td>
            <td align="left" >
               
                   
                <asp:TextBox ID="txtL1_2StemPiecesPercent" runat="server" Height="21px" 
                    Width="153px" Onkeypress="return isNumberkey(event)"  ></asp:TextBox>
               
                   
                      </td>
     
            <td align="right" >
                <asp:Label ID="Label23" runat = "server" Text="No Of > 1/2 To < 4 Stem Pieces"  ForeColor="Black"></asp:Label>
                <asp:Label ID="Label39" runat="server" ForeColor="Red" text="*" 
                    Visible="False" style="margin-right: 10px"></asp:Label>
            </td>
            <td align="left"  >
               
                   
                <asp:TextBox ID="txtNoofG1_2toL4StemPieces" runat="server" Height="21px" 
                    Width="153px" Onkeypress="return isNumberkey(event)"  ></asp:TextBox>
               
                   
            </td>
                          </tr>   
         <tr>
       <td colspan="12">&nbsp;</td>
   </tr>

    
              <tr>

            <td align="right" >
                <asp:Label ID="Label27" runat = "server" Text="No Of > 1/2 To < 4 Stem Pieces(%)"  ForeColor="Black" style="margin-right: 10px"></asp:Label>
                      </td>
            <td align="left" >
               
                   
                <asp:TextBox ID="txtNoofG1_2toL4StemPiecesPercent" runat="server" Height="21px" 
                    Width="153px" Onkeypress="return isNumberkey(event)"  ></asp:TextBox>
               
                   
                      </td>
     
            <td align="right" >
                <asp:Label ID="Label21" runat = "server" Text="No of Total Stem Pieces"  ForeColor="Black"></asp:Label>
                <asp:Label ID="Label40" runat="server" ForeColor="Red" text="*" 
                    Visible="False" style="margin-right: 10px"></asp:Label>
            </td>
            <td align="left"  >
               
                   
                <asp:TextBox ID="txtNoofTotalStemPieces" runat="server" Height="21px" 
                    Width="153px" Onkeypress="return isNumberkey(event)"  ></asp:TextBox>
               
                   
            </td>
            <td align="right" >
                <asp:Label ID="Label28" runat = "server" Text="No of  > 4 Stem Pieces (%) "  ForeColor="Black" style="margin-right: 10px"></asp:Label>
                      </td>
            <td align="left" >
               
                   
                <asp:TextBox ID="txtNoofG4StemPiecesPercent" runat="server" Height="21px" 
                    Width="153px" Onkeypress="return isNumberkey(event)"  ></asp:TextBox>
               
                   
                      </td>
        </tr>   

         <tr>
       <td colspan="12">&nbsp;</td>
   </tr>

        
                
        
                  <tr>
            <td align="right" >
                &nbsp;</td>
            <td align="left"  >
               
                   
                &nbsp;</td>
            <td align="right" >
                &nbsp;</td>
            <td align="left" >
               
                   
                <asp:Label ID="Label45" runat="server" Text="Label"></asp:Label>
               
                   
                      </td>
        </tr>   
        
                
        
          </table> 

                <%-- ddcrop --%>

                <%-- btn save --%>

                 <table align="center" style="margin-top:10px">
 <tr >
 <td>
 <asp:Button ID="btnSave" text="Save" runat="Server" Width="93px" 
         onclick="btnSave_Click" BackColor="#449d44" ForeColor="White"/>  
 <asp:Button ID="btnClear" text="Clear" runat="Server" Width="93px" 
         onclick="btnClear_Click" BackColor="#D15B47" ForeColor="White"/> 
 <asp:Button ID="btnClose" text="Close" runat="Server" Width="93px" 
         onclick="btnClose_Click" BackColor="#438EB9" ForeColor="White"/> 
 
 
 
 </td>
 </tr></table>
                <%--  --%>
                </form>



    </center>



         
     
 </center>
 </div>
 <div>
<asp:Button ID="btnDownload" runat="server" Text="Download" onclick="btnDownload_Click" 
        Visible="False" />
     &nbsp;<asp:Label ID="lblMessage" ForeColor="Red" BackColor="Yellow" Font-Size=Large Font-Bold="true" runat="server" Text=""></asp:Label></div>


    
</body>
</html>