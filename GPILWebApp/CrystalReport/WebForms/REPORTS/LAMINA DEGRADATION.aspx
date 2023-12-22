<%--<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="LAMINA DEGRADATION.aspx.cs" Inherits="GPILWebApp.CrystalReport.WebForms.REPORTS.LAMINA_DEGRADATION" %>--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LAMINA DEGRADATION.aspx.cs" Inherits="GPILWebApp.CrystalReport.WebForms.REPORTS.LAMINA_DEGRADATION" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%--     <style type="text/css">
        .style1
        {
            width: 374px;
        }
        .style2
        {
            width: 182px;
        }
        .style3
        {
            width: 800px;
        }
        /*Calendar Control CSS*/
        .cal_Theme1 .ajax__calendar_container
        {
            background-color: #DEF1F4;
            color:Black;
            border: solid 1px #77D5F7;
        }
        .cal_Theme1 .ajax__calendar_header
        {
            background-color: #ffffff;
            color:Black;
            margin-bottom: 4px;
        }
        .cal_Theme1 .ajax__calendar_title, .cal_Theme1 .ajax__calendar_next, .cal_Theme1 .ajax__calendar_prev
        {
            color:Black;
            padding-top: 3px;
        }
        .cal_Theme1 .ajax__calendar_body
        {
            background-color: #ffffff;
            color:Black;
            border: solid 1px #77D5F7;
        }
        .cal_Theme1 .ajax__calendar_dayname
        {
            text-align: center;
            font-weight: bold;
            margin-bottom: 4px;
            margin-top: 2px;
            color: Black;
        }
        
       .cal_Theme1 .ajax__calendar_day
        {
            color: Black;
            text-align: center;
        }
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_day, .cal_Theme1 .ajax__calendar_hover .ajax__calendar_month, .cal_Theme1 .ajax__calendar_hover .ajax__calendar_year, .cal_Theme1 .ajax__calendar_active
        {
            color: Black;
            font-weight: bold;
            background-color: #DEF1F4;
        }
        .cal_Theme1 .ajax__calendar_today
        {
            color:Black;
            font-weight: bold;
        }
        .cal_Theme1 .ajax__calendar_other, .cal_Theme1 .ajax__calendar_hover .ajax__calendar_today, .cal_Theme1 .ajax__calendar_hover .ajax__calendar_title
        {
            color:Black;
        }
         
        .style4
        {
            width: 374px;
            height: 25px;
        }
        .style5
        {
            width: 182px;
            height: 25px;
        }
        .style6
        {
            height: 25px;
        }
         
        .style7
        {
            width: 800px;
            height: 24px;
        }
   
}
    </style>--%>


    <script language="javascript" type="text/javascript">
        function onlyNumeric(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if ((charCode >= 48 && charCode <= 57))
                    return true;
                else
                    return false;
            }
            catch (err) {
                alert(err.Description);
            }
        }

        function onlyAlphaNumeric(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if ((charCode >= 48 && charCode <= 57) || (charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123))
                    return true;
                else
                    return false;
            }
            catch (err) {
                alert(err.Description);
            }
        }
    </script>
    <%--<script type="text/javascript" language="javascript" src="~/js/validations.js" ></script>--%>


    <style>
        .button-hover-effect {
            background-color: Orange; /* Change the background color on hover */
            color: White; /* Change the text color on hover */
    </style>

    
</head>
<body>

        <center>
        <h3 style="color: cornflowerblue; height: 13px; width: 861px;" id=" ">LAMINA DEGRADATION 
    RESULT</h3>
    </center>
     <form id="form1" runat="server">
         <%-- table1 radio btn start --%>
            <table style="width: 100%;" align="center" class="col1">
       <tr>
           <td align="center" colspan="4" class="style7">

               <asp:RadioButton ID="RadioButton2" runat="server" GroupName="Group1" ForeColor="Black"
                   Text="Enter Manually" OnCheckedChanged="RadioButton1_CheckedChanged"
                   AutoPostBack="true" CausesValidation="True" />

               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
             
             <asp:RadioButton ID="RadioButton1" runat="server" GroupName="Group1" ForeColor="Black"
                 Text="Import From Excel" OnCheckedChanged="RadioButton1_CheckedChanged"
                 AutoPostBack="true" CausesValidation="True" />

           </td>
       </tr>
   </table>
         

                 <center>
              <div>
      <asp:Label ID="Label1" runat="server" ForeColor="Black" Text="Attach Excel File"></asp:Label>

      <asp:FileUpload ID="FileUpload01" runat="server" Style="border: 1px solid gray" />

  </div>

        </center>

             <center>
             <div style="margin-top: 10px; margin-bottom: 10px">
    <asp:Button ID="btnImport" runat="server" Height="25px" Text="Import"
        OnClick="Button2_Click" BackColor="#87B87F" ForeColor="white" />


    <asp:Button ID="btnSaveGrid" runat="server" OnClick="btnSaveGrid_Click"
        Text="Complete" BackColor="#438EB9" ForeColor="white" />
</div>
    </center>
         <%-- radio btn end --%>

         <%-- Gride View start --%>
            <table style="border: Solid 1px #CCC; width: 100%;" align="center" class="col1">
   
       <tr>
           <td>
               <asp:GridView ID="GridViewSample" runat="server"
                   AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="Solid"
                   BorderWidth="1px" Font-Names="Verdana"
                   Width="70%" ForeColor="Black">
                   <AlternatingRowStyle BackColor="#FFD4BA" />
                   <FooterStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                   <PagerStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                   <HeaderStyle BackColor="#FF9E66" BorderColor="#CCCCCC" BorderStyle="Solid"
                       BorderWidth="1px" Font-Size="15px" Height="30px" />
                   <RowStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                       Font-Size="13px" Height="20px" />
                   <Columns>




                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Crop">
                           <ItemTemplate>
                               <asp:Label ID="lblCrop" runat="server" Text='<%#Eval("Crop") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="25%"></HeaderStyle>

                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Grade">
                           <ItemTemplate>
                               <asp:Label ID="lblGrade" runat="server" Text='<%#Eval("Grade") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="25%"></HeaderStyle>

                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Vartiety">
                           <ItemTemplate>
                               <asp:Label ID="lblVariety" runat="server" Text='<%#Eval("Variety") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="25%"></HeaderStyle>

                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Grade Date">
                           <ItemTemplate>
                               <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Date") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="25%"></HeaderStyle>

                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Sample Time">
                           <ItemTemplate>
                               <asp:Label ID="lblTime" runat="server" Text='<%#Eval("Time") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="25%"></HeaderStyle>

                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Run No">
                           <ItemTemplate>
                               <asp:Label ID="lblRunNo" runat="server" Text='<%#Eval("RunNo") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="25%"></HeaderStyle>

                       </asp:TemplateField>


                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Case No">
                           <ItemTemplate>
                               <asp:Label ID="lblCaseNo" runat="server" Text='<%#Eval("CaseNo") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="25%"></HeaderStyle>

                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Sample Weight">
                           <ItemTemplate>
                               <asp:Label ID="lblSampleWeight" runat="server" Text='<%#Eval("Sampleweight") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="25%"></HeaderStyle>

                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Over 1/1">
                           <ItemTemplate>
                               <asp:Label ID="lblOver11" runat="server" Text='<%#Eval("Over11") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="25%"></HeaderStyle>

                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Over 1">
                           <ItemTemplate>
                               <asp:Label ID="lblOver1" runat="server" Text='<%#Eval("Over1") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="25%"></HeaderStyle>

                       </asp:TemplateField>

                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Over 1/2 1/2">
                           <ItemTemplate>
                               <asp:Label ID="lblOver1212" runat="server" Text='<%#Eval("Over1212") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="25%"></HeaderStyle>

                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Over 1/2 ">
                           <ItemTemplate>
                               <asp:Label ID="lblOver12" runat="server" Text='<%#Eval("Over12") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="25%"></HeaderStyle>

                       </asp:TemplateField>


                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="TOver 1/2 1/2">
                           <ItemTemplate>
                               <asp:Label ID="lblTOver1212" runat="server" Text='<%#Eval("TOver1212") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="25%"></HeaderStyle>

                       </asp:TemplateField>

                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="TOver 1/2">
                           <ItemTemplate>
                               <asp:Label ID="lblTOver12" runat="server" Text='<%#Eval("TOver12") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="25%"></HeaderStyle>

                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Over 1/4">
                           <ItemTemplate>
                               <asp:Label ID="lblOver14" runat="server" Text='<%#Eval("Over14") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="25%"></HeaderStyle>

                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Over 1/4 (%)">
                           <ItemTemplate>
                               <asp:Label ID="lblOVer14Second" runat="server" Text='<%#Eval("Over14Second") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="25%"></HeaderStyle>

                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="TOver 1/4">
                           <ItemTemplate>
                               <asp:Label ID="lblTOver14" runat="server" Text='<%#Eval("TOver14") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="25%"></HeaderStyle>

                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Over 1/8">
                           <ItemTemplate>
                               <asp:Label ID="lblOver18" runat="server" Text='<%#Eval("Over18") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="25%"></HeaderStyle>

                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Over 1/8 (%)">
                           <ItemTemplate>
                               <asp:Label ID="lblOver18Second" runat="server" Text='<%#Eval("Over18Second") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="25%"></HeaderStyle>

                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Over Pan">
                           <ItemTemplate>
                               <asp:Label ID="lblOverPan" runat="server" Text='<%#Eval("OverPAN") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="25%"></HeaderStyle>

                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Percent Over Pan">
                           <ItemTemplate>
                               <asp:Label ID="lblPercentOverPan" runat="server" Text='<%#Eval("PercentOverPan") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="25%"></HeaderStyle>

                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Over 18 (%)">
                           <ItemTemplate>
                               <asp:Label ID="lblOver18Percent" runat="server" Text='<%#Eval("Over18Percent") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="25%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="First Pass">
                           <ItemTemplate>
                               <asp:Label ID="lblOverFirstPass" runat="server" Text='<%#Eval("FirstPass") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="25%"></HeaderStyle>


                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Percentage First Pass">
                           <ItemTemplate>
                               <asp:Label ID="lblPercentFirstPass" runat="server" Text='<%#Eval("PercentFirstPass") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="25%"></HeaderStyle>


                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Second Pass">
                           <ItemTemplate>
                               <asp:Label ID="lblSecondPass" runat="server" Text='<%#Eval("SecondPass") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="25%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Percentage Second Pass">
                           <ItemTemplate>
                               <asp:Label ID="lblPercentSecondPass" runat="server" Text='<%#Eval("PercentSecondPass") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="25%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Obj 3/32">
                           <ItemTemplate>
                               <asp:Label ID="lblObj3_32" runat="server" Text='<%#Eval("Obj3_32") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="25%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Obj 3/32 (%)">
                           <ItemTemplate>
                               <asp:Label ID="lblObj3_32Second" runat="server" Text='<%#Eval("Obj3_32Second") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="25%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Slot #07">
                           <ItemTemplate>
                               <asp:Label ID="lblSlot07" runat="server" Text='<%#Eval("Slot07") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="25%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Slot #07 (%)">
                           <ItemTemplate>
                               <asp:Label ID="lblSlot07Second" runat="server" Text='<%#Eval("Slot07Second") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="25%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Slot #12">
                           <ItemTemplate>
                               <asp:Label ID="lblSlot12" runat="server" Text='<%#Eval("Slot12") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="25%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Slot #12 (%)">
                           <ItemTemplate>
                               <asp:Label ID="lblslot12Second" runat="server" Text='<%#Eval("Slot12Second") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="25%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Mesh #12">
                           <ItemTemplate>
                               <asp:Label ID="lblMesh12" runat="server" Text='<%#Eval("Mesh12") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="25%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Mesh #12 (%)">
                           <ItemTemplate>
                               <asp:Label ID="lblMesh12Second" runat="server" Text='<%#Eval("Mesh12Second") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="25%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Fiber Hist">
                           <ItemTemplate>
                               <asp:Label ID="lblFiberHist" runat="server" Text='<%#Eval("FiberHist") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="25%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Fiber Hist (%)">
                           <ItemTemplate>
                               <asp:Label ID="lblFiberHistSecond" runat="server" Text='<%#Eval("FiberHistSecond") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="25%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Tsef Hist">
                           <ItemTemplate>
                               <asp:Label ID="lblTsef" runat="server" Text='<%#Eval("Tsef") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="25%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Tsef Hist (%)">
                           <ItemTemplate>
                               <asp:Label ID="lblTsefHistSecond" runat="server" Text='<%#Eval("TsefHistSecond") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="25%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="New FIber">
                           <ItemTemplate>
                               <asp:Label ID="lblNewFiber" runat="server" Text='<%#Eval("NewFiber") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="25%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="New Fiber (%)">
                           <ItemTemplate>
                               <asp:Label ID="lblNewFiberSecond" runat="server" Text='<%#Eval("NewFiberSecond") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="25%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="TsefNew">
                           <ItemTemplate>
                               <asp:Label ID="lblTsefNew" runat="server" Text='<%#Eval("TsefNew") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="25%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="New">
                           <ItemTemplate>
                               <asp:Label ID="lblNew" runat="server" Text='<%#Eval("New") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="25%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Total Stem In Tips">
                           <ItemTemplate>
                               <asp:Label ID="lblTotalSteminTips" runat="server" Text='<%#Eval("TotalStemintips") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="25%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="LC">
                           <ItemTemplate>
                               <asp:Label ID="lblLC" runat="server" Text='<%#Eval("LC") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="25%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Stem">
                           <ItemTemplate>
                               <asp:Label ID="lblStem" runat="server" Text='<%#Eval("Stem") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="25%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Percentage Obj Stem">
                           <ItemTemplate>
                               <asp:Label ID="lblPercentageObjStem" runat="server" Text='<%#Eval("PercentageobjStem") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="25%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Percentage Stem Tips">
                           <ItemTemplate>
                               <asp:Label ID="lblPercentStemInTips" runat="server" Text='<%#Eval("PercentStemTips") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="25%"></HeaderStyle>
                       </asp:TemplateField>

                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="System Flag Analysis">
                           <ItemTemplate>
                               <asp:Label ID="lblSystemFlag" runat="server" Text='<%#Eval("SystemFlag") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="25%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Packed Density VDR">
                           <ItemTemplate>
                               <asp:Label ID="lblPackedDensity" runat="server" Text='<%#Eval("PackedDensity") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="25%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Remarks">
                           <ItemTemplate>
                               <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="25%"></HeaderStyle>
                       </asp:TemplateField>



                       <asp:TemplateField HeaderStyle-Width="25%" HeaderText="Sum">
                           <ItemTemplate>
                               <asp:Label ID="lblSum" runat="server" Text='<%#Eval("Sum") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="25%"></HeaderStyle>
                       </asp:TemplateField>
                   </Columns>
               </asp:GridView>
           </td>
       </tr>
   </table>
         <%-- Gride View End --%>

         <%-- File Upload Start --%>

   
     <div colspan="12" class="style3">

    



     </div>

         <%-- file upload End --%>

         <%-- ddCrop start --%>
            <div id="Div4" style="background-color: White; width: 103%; height: 100%;"
       align="center">
       <center>
           <table style="width: 100%;" align="center" class="col1">
               <tr>
                   <td colspan="12">&nbsp;</td>
               </tr>
               <tr>
                   <td align="right" class="style1">
                       <asp:Label ID="Label2" runat="server" Text="Crop" ForeColor="Black"></asp:Label>
                       <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>

                   </td>
                   <td align="left" class="style2">


                       <asp:DropDownList ID="ddCrop" runat="server" Width="151px">
                       </asp:DropDownList>


                   </td>
                   <td align="right">
                       <asp:Label ID="Label4" runat="server"
                           Text="Variety " ForeColor="Black"></asp:Label>
                       <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left">


                       <asp:DropDownList ID="ddVariety" runat="server" Width="151px"
                           AutoPostBack="False">
                       </asp:DropDownList>


                   </td>

                           <td align="right">
                       <asp:Label ID="lblOver1" runat="server"
                           Text="Over 1' " ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left">
                       <asp:TextBox ID="txtOver1" runat="server" Height="21px"
                           Width="153px" ReadOnly="True" Style="margin-right: 10px"></asp:TextBox></td>
                         <td align="right">
                       <asp:Label ID="lblOver12" runat="server"
                           Text="Over1/2'" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left">
                       <asp:TextBox ID="txtOver12" runat="server" Height="21px"
                           Width="153px" ReadOnly="True" Style="margin-right: 10px"></asp:TextBox></td>


                   
           
               </tr>

               <%-- 1 --%>

               <tr>
                   <td colspan="12">&nbsp;</td>
               </tr>
               <tr>

                   <td align="right" class="style1">
                       <asp:Label ID="lblGrade" runat="server" Text="Grade" ForeColor="Black"></asp:Label>
                       <asp:Label ID="lblSpan1" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left" class="style2">


                       <asp:DropDownList ID="ddGrade" runat="server" Width="151px">
                       </asp:DropDownList>


                   </td>
                   <td align="right" class="style1">
                       <asp:Label ID="lblDate" runat="server" Text="Date" ForeColor="Black"></asp:Label>
                       <asp:Label ID="lblSpan2" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left" class="style2">
                       <asp:TextBox ID="txtDate" runat="server" Height="21px" TextMode="Date" Width="152px" CausesValidation="true"></asp:TextBox>


                   </td>


                   <td align="right">
                       <asp:Label ID="lbltover12122" runat="server"
                           Text="TOver 1/2' 1/2'" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left">
                       <asp:TextBox ID="txtTOver1212" runat="server" Height="21px"
                           Width="153px" ReadOnly="True"></asp:TextBox></td>


                   <td align="right">
                       <asp:Label ID="lblTover122" runat="server" Text="TOver 1/2'"
                           ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left">
                       <asp:TextBox ID="txtTOver12" runat="server" Height="21px"
                           Width="153px" ReadOnly="True"></asp:TextBox></td>

               </tr>

               <%-- 2 --%>

               <tr>
                   <td colspan="12">&nbsp;</td>
               </tr>

               <tr>

                   <td align="right" class="style1">
                       <asp:Label ID="lblTime" runat="server" Text="Time" ForeColor="Black"></asp:Label>
                       <asp:Label ID="lblSpan3" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left" class="style2">
                       <asp:TextBox ID="txtTime" runat="server" Height="21px"
                           Width="155px"></asp:TextBox>
                   </td>
                   <td align="right" class="style1">
                       <asp:Label ID="lblRunNo" Text="Run No"
                           runat="server" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                       <asp:Label ID="lblSpan4" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left" class="style2">
                       <asp:TextBox ID="txtRunNo" runat="server" Height="21px"
                           Width="153px"></asp:TextBox></td>



                   <td align="right">
                       <asp:Label ID="lbl142" runat="server" Text="Over 1/4'"
                           ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left">
                       <asp:TextBox ID="txtOver14Second" runat="server" Height="21px"
                           Width="153px" ReadOnly="True"></asp:TextBox></td>
                   <td align="right">
                       <asp:Label ID="lbltOver14" runat="server" Text="TOver 1/4'"
                           ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left">
                       <asp:TextBox ID="txtTOver14" runat="server" Height="21px"
                           Width="153px" ReadOnly="True"></asp:TextBox></td>
               </tr>

               <%-- 3 --%>
               <tr>
                   <td colspan="12">&nbsp;</td>
               </tr>
               <tr>

                   <td align="right" class="style1">
                       <asp:Label ID="lblCaseNo" runat="server"
                           Text="Case No" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                       <asp:Label ID="lblSpan5" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left" class="style2">
                       <asp:TextBox ID="txtCaseNo" runat="server" Height="21px"
                           Width="153px"></asp:TextBox></td>
                   <td align="right" class="style1">
                       <asp:Label ID="lblOver11" runat="server"
                           Text="Over 1'1'" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                       <asp:Label ID="lblSpan6" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left" class="style2">
                       <asp:TextBox ID="txtOver11" runat="server" Height="21px"
                           Width="153px"></asp:TextBox></td>




                   <td align="right">
                       <asp:Label ID="lblOver182" runat="server" Text="Over 1/8'"
                           ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left">
                       <asp:TextBox ID="txtOver182" runat="server" Height="21px"
                           Width="153px" ReadOnly="True"></asp:TextBox></td>
                   <td align="right">
                       <asp:Label ID="lblPercentPan" runat="server"
                           Text="Percent On Pan" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left">
                       <asp:TextBox ID="txtPercenOnPan" runat="server" Height="21px"
                           Width="153px" MaxLength="11" ReadOnly="True"></asp:TextBox></td>
               </tr>


               <%-- 4 --%>
               <tr>
                   <td colspan="12">&nbsp;</td>
               </tr>

               <tr>

                   <td align="right" class="style1">
                       <asp:Label ID="lblOver1212" runat="server"
                           Text="Over 1/2'1/2'" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                       <asp:Label ID="lblSpan7" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left" class="style2">
                       <asp:TextBox ID="txtOver1212" runat="server" Height="21px"
                           Width="153px"></asp:TextBox></td>
                   <td align="right" class="style1">
                       <asp:Label ID="lblOver14" runat="server"
                           Text="Over 1/4'" ForeColor="Black"></asp:Label>
                       <asp:Label ID="lblSpan9" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left" class="style2">
                       <asp:TextBox ID="txtOver14" runat="server" Height="21px"
                           Width="153px" MaxLength="6"></asp:TextBox></td>





                   <td align="right" class="style6">
                       <asp:Label ID="lblPercent1pass" Text="% First Pass"
                           runat="server" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left" class="style6">
                       <asp:TextBox ID="txtPercentFirstPass" runat="server" Height="21px"
                           Width="153px" ReadOnly="True"></asp:TextBox></td>
                   <td align="right">
                       <asp:Label ID="lbl2Pass" runat="server"
                           Text="2nd Pass" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left">
                       <asp:TextBox ID="txtSecondPass" runat="server" Height="21px"
                           Width="153px" MaxLength="50" ReadOnly="True"></asp:TextBox></td>
               </tr>

               <%-- 5 --%>

               <tr>
                   <td colspan="12">&nbsp;</td>
               </tr>
               <tr>
                   <td align="right" class="style4">
                       <asp:Label ID="lblOver18" runat="server"
                           Text="Over 1/8'" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                       <asp:Label ID="lblSpan10" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left" class="style5">
                       <asp:TextBox ID="txtOver18" runat="server" Height="21px"
                           Width="153px" MaxLength="10"></asp:TextBox></td>

                   <td align="right" class="style1">
                       <asp:Label ID="lblOverpan" runat="server" Text="Over Pan"
                           ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                       <asp:Label ID="lblSpan11" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left" class="style2">
                       <asp:TextBox ID="txtOverPan" runat="server" Height="21px"
                           Width="153px" MaxLength="25" CausesValidation="True"
                           OnTextChanged="txtOverPan_TextChanged" AutoPostBack="true"></asp:TextBox></td>



                   <td align="right">
                       <asp:Label ID="lblpercent2pass" runat="server"
                           Text="% 2nd Pass" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left">
                       <asp:TextBox ID="txtpercentSecondPass" runat="server" Height="21px"
                           Width="153px" MaxLength="11" ReadOnly="True"></asp:TextBox></td>
                   <td align="right">
                       <asp:Label ID="lblobj3322" runat="server"
                           Text="Obj 3/32" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left">
                       <asp:TextBox ID="txtObj3_32Second" runat="server" Height="21px"
                           Width="153px" MaxLength="50" ReadOnly="True"></asp:TextBox>
                   </td>
               </tr>

               <%-- 6 --%>

               <tr>
                   <td colspan="12">&nbsp;</td>
               </tr>
               <tr>
                   <td align="right" class="style1">
                       <asp:Label ID="lblFirstPass" runat="server" Text="First Pass"
                           ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                       <asp:Label ID="lblSpan12" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left" class="style2">
                       <asp:TextBox ID="txtFirstPass" runat="server"
                           AutoPostBack="True" Height="21px" Width="153px" OnTextChanged="txtFirstPass_TextChanged"></asp:TextBox></td>

                   <td align="right" class="style1">
                       <asp:Label ID="lblSampleweight" runat="server"
                           Text="Sample Weight" ForeColor="Black"></asp:Label>
                       <asp:Label ID="lblSpan13" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left" class="style2">
                       <asp:TextBox ID="txtSampleWeight" runat="server" Height="21px"
                           Width="153px" MaxLength="50" ReadOnly="True"></asp:TextBox>
                   </td>



                   <td align="right">
                       <asp:Label ID="lblslot72" runat="server"
                           Text="Slot # 07" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left">
                       <asp:TextBox ID="txtSlot07Second" runat="server"
                           AutoPostBack="True" Height="21px" Width="153px" ReadOnly="True"></asp:TextBox></td>
                   <td align="right">
                       <asp:Label ID="lblslot122" runat="server"
                           Text="Slot # 12" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left">
                       <asp:TextBox ID="txtSlot12Second" runat="server"
                           AutoPostBack="True" Height="21px" Width="153px" ReadOnly="True"></asp:TextBox></td>
               </tr>

               <%-- 7 --%>


               <tr>
                   <td colspan="12">&nbsp;</td>
               </tr>

               <tr>
                   <td align="right" class="style1">
                       <asp:Label ID="lblOver18p" Text="Over1/8 + P"
                           runat="server" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                       <asp:Label ID="lblSpan14" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left" class="style2">
                       <asp:TextBox ID="txtOver18P" runat="server" Height="21px"
                           Width="153px" MaxLength="50" ReadOnly="True"></asp:TextBox>
                   </td>

                   <td align="right" class="style1">
                       <asp:Label ID="lblObj332" runat="server" Text="Obj 3/32"
                           ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                       <asp:Label ID="lblSpan15" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left" class="style2">
                       <asp:TextBox ID="txtObj3_32" runat="server" Height="21px"
                           Width="153px" MaxLength="6"></asp:TextBox>
                   </td>


                   <td align="right">
                       <asp:Label ID="lblmesh122" runat="server"
                           Text="Mesh # 12" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left">
                       <asp:TextBox ID="txtMesh12Second" runat="server"
                           AutoPostBack="True" Height="21px" Width="153px" ReadOnly="True"></asp:TextBox>
                   </td>
                   <td align="right">
                       <asp:Label ID="lblFiberhist2" runat="server"
                           Text="Fiber Hist" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left">
                       <asp:TextBox ID="txtFiberHistSecond" runat="server" Height="21px"
                           Width="153px" ReadOnly="True"></asp:TextBox></td>
               </tr>

               <%-- 8 --%>

               <tr>
                   <td colspan="12">&nbsp;</td>
               </tr>
               <tr>

                   <td align="right" class="style1">
                       <asp:Label ID="lblslot7" runat="server" Text="Slot # 07"
                           ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                       <asp:Label ID="lblSpan18" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left" class="style2">
                       <asp:TextBox ID="txtSloth07" runat="server"
                           AutoPostBack="True" Height="21px" Width="153px"></asp:TextBox></td>

                   <td align="right" class="style1">
                       <asp:Label ID="lblslot12" Text="Slot # 12" runat="server" ForeColor="Black"></asp:Label>
                       <asp:Label ID="lblSpan16" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left" class="style2">
                       <asp:TextBox ID="txtSloth12" runat="server" Height="21px"
                           Width="153px"></asp:TextBox></td>



                   <td align="right">
                       <asp:Label ID="lbltsefhisy2" runat="server"
                           Text="TSEF Hist" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left">
                       <asp:TextBox ID="txtTsefHist" runat="server" Height="21px"
                           Width="153px" ReadOnly="True"></asp:TextBox></td>
                   <td align="right">
                       <asp:Label ID="lblTsefhist" runat="server"
                           Text="TSEF Hist" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left">
                       <asp:TextBox ID="txtTsefHistSecond" runat="server" Height="21px"
                           Width="153px" ReadOnly="True"></asp:TextBox></td>
               </tr>

               <%-- 9 --%>

               <tr>
                   <td colspan="12">&nbsp;</td>
               </tr>
               <tr>
                   <td align="right" class="style1">
                       <asp:Label ID="lblMesh12" runat="server"
                           Text="Mesh # 12" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                       <asp:Label ID="lblSpan17" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left" class="style2">
                       <asp:TextBox ID="txtMesh12" runat="server" Height="21px"
                           Width="153px"></asp:TextBox></td>
                   <td align="right" class="style1">
                       <asp:Label ID="lblFiberHist" runat="server"
                           Text="Fiber Hist" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                       <asp:Label ID="lblSpan20" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left" class="style2">
                       <asp:TextBox ID="txtFiberHist" runat="server" Height="21px"
                           Width="153px" OnTextChanged="txtFiberHist_TextChanged" AutoPostBack="true"></asp:TextBox></td>


                   <td align="right">
                       <asp:Label ID="lblnewfiber2" runat="server"
                           Text="New Fiber" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left">
                       <asp:TextBox ID="txtNewFiberSecond" runat="server"
                           AutoPostBack="True" Height="21px" Width="153px" ReadOnly="True"></asp:TextBox></td>
                   <td align="right">
                       <asp:Label ID="lblNew" runat="server"
                           Text="New" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left">
                       <asp:TextBox ID="txtNew" runat="server"
                           AutoPostBack="True" Height="21px" Width="153px" ReadOnly="True"></asp:TextBox></td>
               </tr>

               <%-- 10 --%>

               <tr>
                   <td colspan="12">&nbsp;</td>
               </tr>
               <tr>

                   <td align="right" class="style1">
                       <asp:Label ID="lblNewFiber" runat="server" Text="New Fiber"
                           ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                       <asp:Label ID="lblSpan19" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left" class="style2">
                       <asp:TextBox ID="txtNewFiber" runat="server"
                           AutoPostBack="True" Height="21px" Width="153px"></asp:TextBox></td>
                   <td align="right" class="style1">
                       <asp:Label ID="lblTSEFNew" runat="server" Text="TSEF New"
                           ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                       <asp:Label ID="lblSpan21" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left" class="style2">
                       <asp:TextBox ID="txtTsefNew" runat="server"
                           AutoPostBack="True" Height="21px" Width="153px" ReadOnly="True"></asp:TextBox>
                   </td>


                   <td align="right" class="style1">
                       <asp:Label ID="lblTotalstem" runat="server" Text="% Total Stem in Tips"
                           ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                       <asp:Label ID="lblSpan22" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left" class="style2">
                       <asp:TextBox ID="txtPercentTotalStem" runat="server"
                           Height="21px" Width="153px"></asp:TextBox></td>
                   <td align="right">
                       <asp:Label ID="lblLC" runat="server"
                           Text="LC" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                       <asp:Label ID="lblSpan27" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left">
                       <asp:TextBox ID="txtLC" runat="server"
                           Height="21px" Width="153px"></asp:TextBox>
                   </td>
               </tr>

               <%-- 10 --%>

               <tr>
                   <td colspan="12">&nbsp;</td>
               </tr>
               <tr>


                   <td align="right" class="style4">
                       <asp:Label ID="lblStem" Text="Stem" runat="server" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                       <asp:Label ID="lblSpan23" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left" class="style5">
                       <asp:TextBox ID="txtStem" runat="server" Height="21px"
                           Width="153px"></asp:TextBox></td>
                   <td align="right" class="style6">
                       <asp:Label ID="lblObjStem" runat="server"
                           Text="% OBJ Stem in Tips" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                       <asp:Label ID="lblSpan28" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left" class="style6">
                       <asp:TextBox ID="txtPercentObjStemTips" runat="server" Height="21px"
                           Width="153px"></asp:TextBox></td>
                   <td align="right" class="style1">
                       <asp:Label ID="lblFlagAnalysis" runat="server"
                           Text="System flag Analysis" ForeColor="Black"></asp:Label>
                       <asp:Label ID="lblSpan24" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left" class="style2">
                       <asp:TextBox ID="txtSystemFlag" runat="server" Height="21px"
                           Width="153px"></asp:TextBox></td>
                   <td align="right">
                       <asp:Label ID="lblPackedDensity" runat="server"
                           Text="Packed Density DVR" ForeColor="Black"></asp:Label>
                       <asp:Label ID="lblSpan29" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left">
                       <asp:TextBox ID="txtPackeddensityDVR" runat="server" Height="21px"
                           Width="153px"></asp:TextBox></td>

               </tr>

               <%-- 11 --%>

               <tr>
                   <td colspan="12">&nbsp;</td>
               </tr>
               <tr>


                   <td align="right" class="style1">
                       <asp:Label ID="lblpercentstemtips" runat="server"
                           Text="% Stem Tips" ForeColor="Black"></asp:Label>
                       <asp:Label ID="lblSpan25" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left" class="style2">
                       <asp:TextBox ID="txtPercentStemTips" runat="server" Height="21px"
                           Width="153px"></asp:TextBox></td>


                   <td align="right" class="style1">
                       <asp:Label ID="lblRemarks" runat="server"
                           Text="Remarks" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
                       <asp:Label ID="lblSpan26" runat="server" ForeColor="Red" Text="*"
                           Visible="False" Style="margin-right: 10px"></asp:Label>
                   </td>
                   <td align="left">
                       <asp:TextBox ID="txtRemarks" runat="server" Height="21px"
                           Width="153px"></asp:TextBox></td>
                   <%-- <td colspan="4">&nbsp;</td>--%>
               </tr>
              
               <%-- 12 --%>

               <tr>
                   <td colspan="12">&nbsp;</td>
               </tr>
               <tr>
                   <td colspan="12" align="center">

                       <asp:Button ID="btnSave" runat="server"
                           Text="Save" Font-Names="Verdana" Width="70px" Height="25px"
                           BackColor="green" ForeColor="White" OnClick="btnSave_Click" CssClass="button-hover-effect" OnMouseOver="this.className='button-hover-effect'" />
                       &nbsp;&nbsp;
                   <asp:Button ID="btnClear" runat="server"
                       Text="Clear" Font-Names="Verdana" Width="70px" Height="25px"
                       BackColor="red" ForeColor="White" OnClick="btnClear_Click" />&nbsp;&nbsp;
                   <asp:Button ID="btnBack" runat="server"
                       Text="Back" Font-Names="Verdana" Width="70px" Height="25px"
                       OnClick="btnBack_Click" ForeColor="White" BackColor="#438EB9" />&nbsp;
               
                   </td>
               </tr>
               <tr>
                   <td colspan="12">&nbsp;</td>
               </tr>
           </table>
       </center>
   </div>
         <%-- ddCrop End --%>
         </form>

    <div id="Div1" style="background-color: White; width: 103%; height: 100%;"
        align="center">
        <center>
         
           
         
            <table align="center">
            </table>
        </center>
    </div>
    <div id="Div2" style="background-color: White; width: 1%; height: 100%;"
        align="center">
    </div>
    <div id="Div3"
        style="background-color: White; width: 103%; height: 4px; overflow: auto"
        align="center">
       <%-- <center>
            <table style="border: Solid 3px #e4e4e4; width: 100%;" align="center" class="col1">
            </table>
        </center>--%>
    </div>

 
    <div>
        &nbsp;<asp:Label ID="lblMessage" ForeColor="Red" BackColor="Yellow" Font-Size="Large" Font-Bold="true" runat="server" Text=""></asp:Label>
    </div>


    </body>
</html>
