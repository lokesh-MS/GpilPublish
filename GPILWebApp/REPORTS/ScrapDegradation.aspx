<%--<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ScrapDegradation.aspx.cs" Inherits="GPILWebApp.CrystalReport.WebForms.REPORTS.ScrapDegradation" %>--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScrapDegradation.aspx.cs" Inherits="GPILWebApp.CrystalReport.WebForms.REPORTS.ScrapDegradation" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        /*Calendar Control CSS*/
        .cal_Theme1 .ajax__calendar_container {
            background-color: #DEF1F4;
            border: solid 1px #77D5F7;
        }

        .cal_Theme1 .ajax__calendar_header {
            background-color: #ffffff;
            margin-bottom: 4px;
        }

        .cal_Theme1 .ajax__calendar_title,
        .cal_Theme1 .ajax__calendar_next,
        .cal_Theme1 .ajax__calendar_prev {
            color: #004080;
            padding-top: 3px;
        }

        .cal_Theme1 .ajax__calendar_body {
            background-color: #ffffff;
            border: solid 1px #77D5F7;
        }

        .cal_Theme1 .ajax__calendar_dayname {
            text-align: center;
            font-weight: bold;
            margin-bottom: 4px;
            margin-top: 2px;
            color: #004080;
        }

        .cal_Theme1 .ajax__calendar_day {
            color: #004080;
            text-align: center;
        }

        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_day,
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_month,
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_year,
        .cal_Theme1 .ajax__calendar_active {
            color: #004080;
            font-weight: bold;
            background-color: #DEF1F4;
        }


        .cal_Theme1 .ajax__calendar_today {
            font-weight: bold;
        }


        .cal_Theme1 .ajax__calendar_other,
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_today,
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_title {
            color: #bbbbbb;
        }
    </style>
</head>
<body >
    <center>
        <h3 style="color: White; height: 20%;" id=" ">SCRAP DEGRADATION </h3>
    </center>
    <div id="Div1" style="background-color: White; width: 100%; height: 100%;" align="center">
        <center>
            <table style="width: 100%;" align="center" class="col1">
                <tr>
                    <td align="center" colspan="4">&nbsp;</td>
                </tr>
            </table>
        </center>
    </div>
    <div id="Div2" style="background-color: White; width: 100%; height: 100%;" align="center">

        <center>
                 <form id="form1" runat="server">
                            <table style="width: 100%;" align="center" class="col1">
           <tr>
               <td align="center">

                   <asp:RadioButton ID="rbManual" runat="server" GroupName="Group1" ForeColor="Black"
                       Text="Enter Manually"
                       AutoPostBack="true" CausesValidation="True"
                       OnCheckedChanged="rbManual_CheckedChanged" />

                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                 
                

                   <asp:RadioButton ID="rbImport" runat="server" GroupName="Group1" ForeColor="Black"
                       Text="Import From Excel"
                       AutoPostBack="true" CausesValidation="True"
                       OnCheckedChanged="rbImport_CheckedChanged" />

               </td>



           </tr>

           <tr>
               <td align="center">

                   <asp:Label ID="Label12" runat="server" ForeColor="Black"
                       Text="Attach Excel File"></asp:Label>

                   <asp:FileUpload ID="FileUpload01" runat="server" Style="border: 1px solid gray" />

                   <asp:Button ID="btnImport" runat="server" Height="29px" Width="7%" Style="margin-top: 10px; margin-bottom: 5px" Text="Import"
                       OnClick="btnImport_Click" ForeColor="White" BackColor="#87B87F" />



               </td>

           </tr>
       </table>


                     <%-- second table btn  --%>

                        <table align="center">
       <tr>
           <td>
               <asp:Button ID="btnSave" Text="Save" runat="Server" Width="93px"
                   OnClick="btnSave_Click" BackColor="#398439" ForeColor="White" />
               &nbsp;
     
    

               <asp:Button ID="btnsaveG" runat="server" ForeColor="White" Text="Complete" Width="106px"
                   OnClick="btnsaveG_Click1" BackColor="#398439" />

               <asp:Button ID="btnClear" Text="Clear" runat="Server" Width="93px"
                   OnClick="btnClear_Click" BackColor="#D15B47" ForeColor="White" />
               &nbsp;

               <asp:Button ID="btnClose" Text="Close" runat="Server" Width="93px"
                   OnClick="btnClose_Click" BackColor="#6FB3E0" ForeColor="White" />



           </td>
       </tr>
   </table>
                     <%-- btn end --%>

                     <%-- third table ddcrop --%>
                       <table style="width: 100%;" align="center" class="col1">
      <tr>
          <td colspan="12">&nbsp;</td>
      </tr>

      <tr>
          <td align="right">
              <asp:Label ID="lblDate0" runat="server" Text="Crop" ForeColor="Black"></asp:Label>
              <asp:Label ID="Label19" runat="server" ForeColor="Red" Text="*"
                  Visible="False" Style="margin-right: 10px"></asp:Label>
          </td>
          <td align="left">
              <asp:DropDownList ID="ddCrop" runat="server" Width="151px">
              </asp:DropDownList>
          </td>
          <td align="right">
              <asp:Label ID="lblOver13" runat="server"
                  Text="Date" ForeColor="Black" Style="text-align: left"></asp:Label>
              <asp:Label ID="Label29" runat="server" ForeColor="Red" Text="*"
                  Visible="False" Style="margin-right: 10px"></asp:Label>
          </td>
          <td align="left">
              <asp:TextBox ID="txtEntryDate" Width="152px" runat="server"
                  CausesValidation="true" Height="21px" TextMode="Date" />

          </td>

          <td align="right">
              <asp:Label ID="lblDate" runat="server" Text="Variety" ForeColor="Black"></asp:Label>
              <asp:Label ID="Label20" runat="server" ForeColor="Red" Text="*"
                  Visible="False" Style="margin-right: 10px"></asp:Label>
          </td>
          <td align="left">


              <asp:DropDownList ID="ddVariety" runat="server" Width="151px">
              </asp:DropDownList>


          </td>
          <td align="right">
              <asp:Label ID="lblOver12" runat="server"
                  Text="Run No" ForeColor="Black" Style="text-align: left"></asp:Label>
              <asp:Label ID="Label30" runat="server" ForeColor="Red" Text="*"
                  Visible="False" Style="margin-right: 10px"></asp:Label>
          </td>
          <td align="left">
              <asp:TextBox ID="txtRUnNo" runat="server" Height="21px"
                  Width="153px" Onkeypress="return isNumberkey(event)"></asp:TextBox></td>
      </tr>

      <tr>
          <td colspan="12">&nbsp;</td>
      </tr>

      <tr>
          <td align="right">
              <asp:Label ID="Label5" runat="server" Text="Scrap Grade" ForeColor="Black"></asp:Label>
              <asp:Label ID="Label21" runat="server" ForeColor="Red" Text="*"
                  Visible="False" Style="margin-right: 10px"></asp:Label>
          </td>
          <td align="left">


              <asp:DropDownList ID="ddScrapGrade" runat="server" Height="24px"
                  Width="153px" OnTextChanged="txtScrapGrade_TextChanged">
              </asp:DropDownList>


          </td>
          <td align="right">
              <asp:Label ID="Label2" runat="server"
                  Text="Sample Time" ForeColor="Black" Style="text-align: left"></asp:Label>
              <asp:Label ID="Label31" runat="server" ForeColor="Red" Text="*"
                  Visible="False" Style="margin-right: 10px"></asp:Label>
          </td>
          <td align="left">
              <asp:TextBox ID="txtTime" runat="server" Height="21px"
                  Width="153px"></asp:TextBox></td>

          <td align="right">
              <asp:Label ID="Label1" runat="server" Text="Production From" ForeColor="Black"></asp:Label>
              <asp:Label ID="Label22" runat="server" ForeColor="Red" Text="*"
                  Visible="False" Style="margin-right: 10px"></asp:Label>
          </td>
          <td align="left">


              <asp:DropDownList ID="ddProductionFrom" runat="server" Height="24px"
                  Width="153px">
              </asp:DropDownList>


          </td>
          <td align="right">
              <asp:Label ID="Label6" runat="server"
                  Text="Run Case No" ForeColor="Black" Style="text-align: left"></asp:Label>
              <asp:Label ID="Label32" runat="server" ForeColor="Red" Text="*"
                  Visible="False" Style="margin-right: 10px"></asp:Label>
          </td>
          <td align="left">
              <asp:TextBox ID="txtRunCaseNo" runat="server" Height="21px"
                  Width="153px" Onkeypress="return isNumberkey(event)"></asp:TextBox></td>
      </tr>

      <tr>
          <td colspan="12">&nbsp;</td>
      </tr>

      <tr>
          <td align="right">
              <asp:Label ID="Label7" runat="server" Text="Held Over 5 # Grams" ForeColor="Black"></asp:Label>
              <asp:Label ID="Label23" runat="server" ForeColor="Red" Text="*"
                  Visible="False" Style="margin-right: 10px"></asp:Label>
          </td>
          <td align="left">


              <asp:TextBox ID="txtHeadOver5" runat="server" Height="21px"
                  Width="153px" Onkeypress="return isNumberkey(event)"></asp:TextBox>


          </td>
          <td align="right">
              <asp:Label ID="Label8" runat="server"
                  Text="Held Over 10 #  Grams" ForeColor="Black" Style="text-align: left"></asp:Label>
              <asp:Label ID="Label33" runat="server" ForeColor="Red" Text="*"
                  Visible="False" Style="margin-right: 10px"></asp:Label>
          </td>
          <td align="left">
              <asp:TextBox ID="txtHeadOver10" runat="server" Height="21px"
                  Width="153px" Onkeypress="return isNumberkey(event)"></asp:TextBox></td>

          <td align="right">
              <asp:Label ID="Label9" runat="server" Text="Held Over 20 #  Grams" ForeColor="Black"></asp:Label>
              <asp:Label ID="Label24" runat="server" ForeColor="Red" Text="*"
                  Visible="False" Style="margin-right: 10px"></asp:Label>
          </td>
          <td align="left">


              <asp:TextBox ID="txtHeadOver20" runat="server" Height="21px"
                  Width="153px" Onkeypress="return isNumberkey(event)"></asp:TextBox>


          </td>
          <td align="right">
              <asp:Label ID="Label14" runat="server"
                  Text="Total Sample Weight" ForeColor="Black" Style="text-align: left"></asp:Label>
              <asp:Label ID="Label28" runat="server" ForeColor="Red" Text="*"
                  Visible="False" Style="margin-right: 10px"></asp:Label>
          </td>
          <td align="left">
              <asp:TextBox ID="txtSampleWeight" runat="server" Height="21px"
                  Width="153px" Onkeypress="return isNumberkey(event)"></asp:TextBox>
          </td>
      </tr>

      <tr>
          <td colspan="12">&nbsp;</td>
      </tr>
      <tr>
          <td align="right">&nbsp;
         
              <asp:Label ID="Label13" runat="server" Text="Pan Grams" ForeColor="Black"></asp:Label>
              <asp:Label ID="Label25" runat="server" ForeColor="Red" Text="*"
                  Visible="False" Style="margin-right: 10px"></asp:Label>
          </td>
          <td align="left">


              <asp:TextBox ID="txtPanGrams" runat="server" Height="21px"
                  Width="153px" Onkeypress="return isNumberkey(event)"
                  OnTextChanged="txtPanGrams_TextChanged" AutoPostBack="true"></asp:TextBox>
          </td>
          <td align="right">
              <asp:Label ID="Label4" runat="server"
                  Text="Fibre  Grams" ForeColor="Black" Style="text-align: left"></asp:Label>
              <asp:Label ID="Label34" runat="server" ForeColor="Red" Text="*"
                  Visible="False" Style="margin-right: 10px"></asp:Label>
          </td>
          <td align="left">
              <asp:TextBox ID="txtFiber" runat="server" Height="21px"
                  Width="153px" Onkeypress="return isNumberkey(event)"
                  OnTextChanged="txtFiber_TextChanged" AutoPostBack="true"></asp:TextBox></td>

          <td align="right">
              <asp:Label ID="nic" runat="server" Text="Held Over 5 #  %" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
          </td>
          <td align="left">


              <asp:TextBox ID="txtHeldOver5_2" runat="server" Height="21px"
                  Width="153px" ReadOnly="True"></asp:TextBox>
          </td>
          <td align="right">
              <asp:Label ID="Label16" runat="server"
                  Text="Held Over 10 # %" ForeColor="Black" Style="text-align: left; margin-right: 10px"></asp:Label>
          </td>
          <td align="left">
              <asp:TextBox ID="txtHeldOver10_2" runat="server" Height="21px"
                  Width="153px" ReadOnly="True"></asp:TextBox></td>

      </tr>

      <tr>
          <td colspan="12">&nbsp;</td>
      </tr>

      <tr>
          <td align="right">
              <asp:Label ID="Label17" runat="server" Text="Held Over 20 # %" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
          </td>
          <td align="left">
              <asp:TextBox ID="txtheldOver20_2" runat="server" Height="21px"
                  Width="153px" ReadOnly="True"></asp:TextBox>
          </td>
          <td align="right">
              <asp:Label ID="Label18" runat="server"
                  Text="Pan %" ForeColor="Black" Style="text-align: left; margin-right: 10px"></asp:Label>
          </td>
          <td align="left">
              <asp:TextBox ID="txtPan_2" runat="server" Height="21px"
                  Width="153px" ReadOnly="True"></asp:TextBox></td>

          <td align="right">
              <asp:Label ID="Label3" runat="server" Text="Sample weight" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
              &nbsp;</td>
          <td align="left">
              <asp:TextBox ID="txtTotalSampleWeight" runat="server" Height="21px"
                  Width="153px" ReadOnly="True" Onkeypress="return isNumberkey(event)"></asp:TextBox>
          </td>
          <td align="right">
              <asp:Label ID="Label11" runat="server" Text="Fibre %" ForeColor="Black" Style="margin-right: 10px"></asp:Label>
          </td>
          <td align="left">
              <asp:TextBox ID="txtFiber_2" runat="server" Height="21px"
                  Width="153px" ReadOnly="True"></asp:TextBox>
          </td>
      </tr>

      <tr>
          <td colspan="12">&nbsp;</td>
      </tr>

      <tr>
          <td align="right">&nbsp;
      </td>
          <td align="left">
              <asp:Label ID="Label35" runat="server" Text="Label"></asp:Label>
          </td>
      </tr>
  </table>
                     <%-- ddcrop end --%>

                     <%-- gridView start --%>

                        <table>
       <tr>
           <td>

               <asp:GridView ID="GridViewSample" runat="server" AutoGenerateColumns="False"
                   BorderColor="#CCCCCC" BorderStyle="Solid"
                   BorderWidth="1px" Font-Names="Verdana" Width="98%" ForeColor="Black" Height="15px">
                   <AlternatingRowStyle BackColor="#FFD4BA" />
                   <HeaderStyle BackColor="#FF9E66" BorderColor="#CCCCCC" BorderStyle="Solid"
                       BorderWidth="1px" Font-Size="15px" Height="15px" />
                   <RowStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                       Font-Size="13px" Height="20px" />
                   <Columns>
                       <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Crop">
                           <ItemTemplate>
                               <asp:Label ID="lblCrop" runat="server" Text='<%#Eval("CROP") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="3%"></HeaderStyle>
                       </asp:TemplateField>

                       <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Variety">
                           <ItemTemplate>
                               <asp:Label ID="lblVariety" runat="server" Text='<%#Eval("Variety") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="3%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Scrap Grade">
                           <ItemTemplate>
                               <asp:Label ID="lblGrade" runat="server" Text='<%#Eval("Grade") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="3%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Run No">
                           <ItemTemplate>
                               <asp:Label ID="lblRunNo" runat="server" Text='<%#Eval("RunNo") %>'></asp:Label>
                           </ItemTemplate>
                           <HeaderStyle Width="3%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Sample Date">
                           <ItemTemplate>
                               <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Date") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="3%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Sample Time">
                           <ItemTemplate>
                               <asp:Label ID="lblTime" runat="server" Text='<%#Eval("Time") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="3%"></HeaderStyle>
                       </asp:TemplateField>

                       <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Production From">
                           <ItemTemplate>
                               <asp:Label ID="lblProduction" runat="server" Text='<%#Eval("Production") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="3%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Run Case No">
                           <ItemTemplate>
                               <asp:Label ID="lblCaseNo" runat="server" Text='<%#Eval("CaseNo") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="3%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Held Over #5 Grams">
                           <ItemTemplate>
                               <asp:Label ID="lblHeldOver5" runat="server" Text='<%#Eval("HeldOver5") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="3%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Held Over #10 Grams">
                           <ItemTemplate>
                               <asp:Label ID="lblHeldOver10" runat="server" Text='<%#Eval("HeldOver10") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="3%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Held Over #20 Grams">
                           <ItemTemplate>
                               <asp:Label ID="lblHeldOver20" runat="server" Text='<%#Eval("HeldOver20") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="3%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Total Sample Weight">
                           <ItemTemplate>
                               <asp:Label ID="lblTotalSampleWeight" runat="server" Text='<%#Eval("TotalSampleWeight") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="3%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Pan Grams">
                           <ItemTemplate>
                               <asp:Label ID="lblPanGrams" runat="server" Text='<%#Eval("PanGrams") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="3%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Fiber Grams">
                           <ItemTemplate>
                               <asp:Label ID="lblFiberGrams" runat="server" Text='<%#Eval("FiberGrams") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="3%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Held Over #5 Grams %">
                           <ItemTemplate>
                               <asp:Label ID="lblHeldOver5Percent" runat="server" Text='<%#Eval("HeldOver5Percent") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="3%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Held Over #10 Grams %">
                           <ItemTemplate>
                               <asp:Label ID="lblHeldOver10Percent" runat="server" Text='<%#Eval("HeldOver10Percent") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="3%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Held Over #20 Grams %">
                           <ItemTemplate>
                               <asp:Label ID="lblHeldOver20Percent" runat="server" Text='<%#Eval("HeldOver20Percent") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="3%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Pan %">
                           <ItemTemplate>
                               <asp:Label ID="lblPanPercent" runat="server" Text='<%#Eval("PanPercent") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="3%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Fiber %">
                           <ItemTemplate>
                               <asp:Label ID="lblFiberPercent" runat="server" Text='<%#Eval("FiberPercent") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="3%"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Sample Weight">
                           <ItemTemplate>
                               <asp:Label ID="lblSampleWeight" runat="server" Text='<%#Eval("SampleWeight") %>'></asp:Label>
                           </ItemTemplate>

                           <HeaderStyle Width="3%"></HeaderStyle>
                       </asp:TemplateField>
                   </Columns>
               </asp:GridView>

           </td>
       </tr>
   </table>
                     <%-- grideView End --%>
                     </form>
     

        </center>
        <hr>
        <%--            <table style="border: Solid 1px #CCC; width: 100%;" align="center" class="col1">

                 </table>--%>
      
    </div>
    <div id="Div4" style="background-color: White; width: 100%; height: 100%;" align="center">
        <center>
         
            <table>
                <tr>
                    <td>&nbsp;</td>
                </tr>
            </table>
         

        </center>
        <asp:Button ID="btnDownload" runat="server" Text="Download" OnClick="btnDownload_Click"
            Visible="False" />
        &nbsp;<asp:Label ID="lblMessage" ForeColor="Red" BackColor="Yellow" Font-Size="Large" Font-Bold="true" runat="server" Text=""></asp:Label>
    </div>
    
</body>
</html>
