<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Permission.aspx.cs" Inherits="GPILWebApp.CrystalReport.WebForms.Permission" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Permission.aspx.cs" EnableEventValidation="false" Inherits="GPILWebApp.CrystalReport.WebForms.Permission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript">
        function CheckAll(oCheckbox) {
            var GridView2 = document.getElementById("<%=dgReports.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[3].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
    </script>
    <style>
tr:nth-child(even) {
  background-color: #D6EEEE;
}
</style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <%--<table id="tblUser" runat="server"  visible="true" align="center" border="0" cellpadding="3" cellspacing="1" 
    bgcolor="#cccccc" width="600px" style="font-family: Verdana; font-size: small; height:300px "  >--%>
    <div style="background-color:white">

            <table id="tblUser" runat="server" visible="true" style="margin-top:30px ;width:60%; height:600px;" align="center"  class="col1">
    <tr><td>
    <div style="overflow:scroll; height:600px;">
    <table id="tblUser1" runat="server" >
        <tr>
            <td colspan="3" style="color:Black; font-family:Verdana; font-weight:bold; font-size:medium; " align="center">
                USER ACCESS PERMISSION
            </td>
        </tr>            
        <tr>
            <td align="left" colspan="2" >
                <asp:Label ID="lblTittle" runat="server" Text="User Details" ForeColor="Black" Font-Names="Verdana" Height="22px"></asp:Label>
            </td>
        </tr>       
        <tr>
            <td style="width:150px; color:Black; font-family:Verdana;" align="left" >Employee ID :
                <%--<asp:Label ID="lblStar" runat="server" CssClass="" Text="*"></asp:Label> --%>
            </td>
            <td align="left" >
                <table>
                  <tr>
                    <td align="left" style="vertical-align:middle; color:Black; font-family:Verdana; height:20px; "  >
                        <asp:TextBox ID="txtEmployeeID" runat="server" class="text_box" MaxLength="10" Font-Names="Verdana" Width="200px" Height="22px"
                            ReadOnly="false"></asp:TextBox>
                    </td>
                    <td align="right" style="width:35px; color:Black; font-family:Verdana;" >
                        <asp:ImageButton ID="btnRGPSearch" runat="server" 
                            ImageUrl="~/Images/search.jpg" onclick="btnRGPSearch_Click" Width="28px" /></td>
                  </tr>
                </table>
            </td>
            <td rowspan="11" align="left" valign="top" style="width:80px">       
            </td>    
        </tr>
        <tr>
            <td style="width:200px; color:Black; font-family:Verdana;" align="left" >Employee Name :</td>
            <td align="left" style="color:Black; font-family:Verdana;" > 
                <asp:TextBox ID="txtEmployeeName" runat="server" class="text_box" Width="200px" Height="22px"
                    MaxLength="30" AutoCompleteType="Disabled" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:200px; color:Black; font-family:Verdana;" align="left"  >Department :</td> 
            <td align="left" style="color:Black; font-family:Verdana;">  
               <%-- <asp:TextBox ID="txt123PasswordEntry" runat="server" class="text_box" 
                    MaxLength="10" autocomplete="off"></asp:TextBox>--%>
                <asp:TextBox ID="txtDepartment" runat="server"  class="text_box" Width="200px" Height="22px"
                   AutoCompleteType="Disabled" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
 <%--   <tr>
        <td style="width:150px" align="left"  class="col1">Security Question :</td> 
        <td bgcolor="#FFFFFF" align="left" class="col1"> 
            <asp:DropDownList ID="cmbSecurityQuestion" runat="server" Height="22px" 
                class="text_box" >
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="width:150px" align="left"  class="col1">Security Answer :</td> 
            <td bgcolor="#FFFFFF" align="left" class="col1" >  
            <asp:TextBox ID="txtSecurityAnswer" runat="server" class="text_box"  MaxLength="20"></asp:TextBox>
        </td>
        
    </tr>--%>
        <tr>
            <td style="width:200px; color:Black; font-family:Verdana;" align="left" >Designation :</td> 
            <td align="left" style="color:Black; font-family:Verdana;"> 
               <%-- <asp:DropDownList ID="cmbUserRights" runat="server" Height="22px" class="text_box">
                </asp:DropDownList>--%>
                <asp:TextBox ID="txtDesignation" runat="server" class="text_box" MaxLength="50" Width="200px" Height="22px"
                    ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
    <tr>
    <td colspan="2" style="height:auto;">
        <%--<div id="dvPerm" runat="server" style="overflow:scroll; " visible="false">--%>
        <asp:GridView ID="dgReports" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="Project" BackColor="#438EB9" BorderColor="#DEBA84" 
            BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" >
            <RowStyle BackColor="#cfe2f3" ForeColor="#000000" />
            <Columns>
                    <asp:BoundField DataField="Project" HeaderText="Project" ItemStyle-Width="150px"  
                        ControlStyle-BorderStyle="Solid" ControlStyle-BorderWidth="2px" 
                        ControlStyle-BorderColor="BurlyWood" >
                        
                        <ControlStyle BorderColor="BurlyWood" BorderWidth="2px" BorderStyle="Solid"></ControlStyle>
                        <ItemStyle Width="150px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Module" HeaderText="Module" ItemStyle-Width="150px" >
                        <ItemStyle Width="150px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Form" HeaderText="Form" ItemStyle-Width="150px" >            
                        <ItemStyle Width="150px"></ItemStyle>
                    </asp:BoundField>
                
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <input id="Checkbox2" type="checkbox" onclick="CheckAll(this)" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox  ID="chkSelect" runat="server" Checked='<%# Eval("Permission") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

            </Columns>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#438EB9"  Font-Bold="True" ForeColor="White" />
        </asp:GridView>
        <%--</div>--%>
         
    </td>
    
    </tr>
        <tr>
            <td></td>
        </tr>
    
     <tr> 
        <td style="width:200px; color:Black; font-family:Verdana;" align="left" >Actions : </td>      
       <td style="height:25px; padding-top:10px; padding-bottom:2px">       
      <asp:ImageButton ID="btnSave" runat="server" 
             ImageUrl="~/images/save.gif" onclick="btnSave_Click" />
         <asp:ImageButton ID="btnClear" runat="server" OnClick="btnClear_Click"
             ImageUrl="~/images/Clear.GIF" />
         <asp:ImageButton ID="btnBack" runat="server" OnClick="btnBack_Click"
             ImageUrl="~/images/Back.gif" />      
      <%-- <asp:Button ID="btnSave" runat="server" CssClass="buttonclass" OnClick="btnSave_Click"
                                                                Text="Save" 
                Width="67px"  /> &nbsp;&nbsp;
        <asp:Button ID="btnClear" runat="server" 
                 CssClass="buttonclass" OnClick="btnClear_Click"
                                                                Text="Clear" Height="22px" 
                  Width="67px" /> &nbsp;&nbsp;
        <asp:Button ID="btnBack" runat="server" 
                 CssClass="buttonclass" OnClick="btnBack_Click"
                                                                Text="Back" Height="22px" 
                  Width="67px" />        --%>  
                          
      </td>
         
    </tr>
    
    <tr>  
    
    <td style="width:200px; color:Black; font-family:Verdana;" align="left" >Status : </td>   
        <td align="left" style="color:Red; font-family:Verdana;" >
            <asp:Label ID="lblMessage" runat="server" Text="" Height="23px" Width="100%" ></asp:Label>
        </td>  
    
    </tr>
    </table>
    </div>
    </td></tr>
    </table>

    </div>

    <%--<table id="tblSearch" runat="server" cellpadding="3" visible="false" cellspacing="1" bgcolor="#cccccc" width="908px" 
        style="font-family: Verdana; font-size: small; height:100px " >--%>

    <div style="background-color:white">
          <table id="tblSearch" runat="server" style="width:70%; text-align:center;" class="col1" align="center" visible="false">
      <tr>
          <td colspan="4" align="center" style="color:Black; font-weight:bold; font-family:Verdana; padding-bottom:20px;">
              Employee Master Search     </td>
      </tr>
      <tr> 
          <td style="width:100px; color:Black; font-family:Verdana;" align="left"  >Employee Code :</td>                
          <td style="width:100px; color:Black; font-family:Verdana;" align="left" > Employee Name :</td>                 
          <td style="width:100px; color:Black; font-family:Verdana;" align="left" >Phone :</td>                 
          <td align="center">
          </td> 
      </tr>
      <tr>
          <td bgcolor="#FFFFFF" align="left" style="color:Black; font-family:Verdana;"> 
              <asp:TextBox ID="txtEmployeeCodeSearch" runat="server" class="text_box" MaxLength="10"></asp:TextBox>
          </td>
          <td bgcolor="#FFFFFF" align="left" style="color:Black; font-family:Verdana;"> 
              <asp:TextBox ID="txtEmployeeNameSearch" runat="server"  class="text_box" MaxLength="20"></asp:TextBox>
          </td>
          <td bgcolor="#FFFFFF" align="left" style="color:Black; font-family:Verdana;">
              <asp:TextBox ID="txtPhoneSearch" runat="server"  class="text_box" MaxLength="20"></asp:TextBox>
          </td>
          <td align="center" > 
              <asp:ImageButton ID="btnFetch" runat="server" OnClick="btnFetch_Click" ImageUrl="~/images/View.gif" />
          </td>
      </tr>
      <tr>
          <td colspan="4" bgcolor="ffffff" style="height:400px" align="center" valign="top">
              <asp:GridView ID="dgEmployeeSearch" runat="server" AllowPaging="True" 
                  AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                  Font-Names="Palatino Linotype" Font-Size="Medium" Height="100%" 
                  Style="text-align: left" Width="100%" 
                  onrowediting="dgEmployeeSearch_RowEditing" PageSize="8" 
                  onpageindexchanging="dgEmployeeSearch_PageIndexChanging" ForeColor="#333333" GridLines="None">
              <PagerSettings FirstPageText="&lt;" LastPageText="&gt;" NextPageText="&gt;&gt;" 
                  PreviousPageText="&lt;&lt;" />
              <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Size="Smaller" 
                       Font-Bold="True" />
              <Columns>
                  <asp:TemplateField HeaderText="Employee Code" Visible="true">
                      <ItemTemplate>
                          <asp:Label ID="lblEmployeeCode" runat="server" Text='<%# Bind("USER_ID") %>'></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle HorizontalAlign="Left" Font-Names="Verdana" Font-Size="Small" />
                      <ItemStyle Font-Names="Verdana" Font-Size="Small" BorderStyle="Solid"  BorderColor="#CCCCCC"  BorderWidth="1px" />
                  </asp:TemplateField>  
                                                                                   
                  <asp:TemplateField HeaderText="Employee Name" Visible="true">
                      <ItemTemplate>
                          <asp:Label ID="lblEmployeeName" runat="server" Text='<%# Bind("USER_NAME") %>'></asp:Label>
                      </ItemTemplate>
                      <HeaderStyle HorizontalAlign="Left" Font-Names="Verdana" Font-Size="Small" />
                      <ItemStyle Font-Names="Verdana" Font-Size="Small" BorderStyle="Solid"  BorderColor="#CCCCCC" BorderWidth="1px" />
                  </asp:TemplateField> 
                          
                  <asp:TemplateField HeaderText="Phone No." Visible="true">
                               <ItemTemplate>
                                  <asp:Label ID="lblPhone" runat="server" Text='<%# Bind("MOBILE_NO") %>'></asp:Label>
                              </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Left" Font-Names="Verdana" Font-Size="Small" />
                             <ItemStyle Font-Names="Verdana" Font-Size="Small" BorderStyle="Solid"  BorderColor="#CCCCCC" BorderWidth="1px" />
                          </asp:TemplateField>  
                          
                           <asp:TemplateField HeaderText="Designation" Visible="true" >
                               <ItemTemplate>
                                  <asp:Label ID="lblDesign" runat="server" Text='<%# Bind("DESIGNATION") %>'></asp:Label>
                              </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Left" Font-Names="Verdana" Font-Size="Small" />
                             <ItemStyle Font-Names="Verdana" Font-Size="Small" BorderStyle="Solid"  BorderColor="#CCCCCC" BorderWidth="1px" />
                          </asp:TemplateField>  
                           <asp:TemplateField HeaderText="Department" Visible="true">
                               <ItemTemplate>
                                  <asp:Label ID="lblDepartment" runat="server" Text='<%# Bind("DEPARTMENT") %>'></asp:Label>
                              </ItemTemplate>
                             <HeaderStyle HorizontalAlign="Left" Font-Names="Verdana" Font-Size="Small" />
                             <ItemStyle Font-Names="Verdana" Font-Size="Small" BorderStyle="Solid"  BorderColor="#CCCCCC" BorderWidth="1px" />
                          </asp:TemplateField>  
                          
                           <asp:TemplateField HeaderText="Select" Visible="true">
                              <ItemTemplate>
                                  <asp:Button ID="BtnEdit" runat="server" CausesValidation="false" 
                                      CommandName="Edit" CommandArgument="<%# Container.DataItemIndex %>" class="VgridButton" Height="20pt" Text="Select" />
                              </ItemTemplate>
                              <HeaderStyle HorizontalAlign="center" Font-Names="Verdana" Font-Size="Small" />
                              <ItemStyle Font-Names="Verdana" Font-Size="Small" BorderStyle="Solid"  
                              BorderColor="#CCCCCC" BorderWidth="1px" Width="10px"/>
                          </asp:TemplateField>
                          
                  </Columns>
                  <RowStyle Width="26pt" BackColor="#EFF3FB" />
                  <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                  <PagerStyle BackColor="#0E9E0E" ForeColor="White" HorizontalAlign="Center" />
                  <HeaderStyle BackColor="#0E9E0E" Font-Bold="True" ForeColor="White" Font-Size="9pt" 
                                  Height="25px" />
                   <EditRowStyle BackColor="#2461BF" />
                  <alternatingRowStyle ForeColor="#004040" />
              </asp:GridView>
        </td>
        </tr>
 </table>

    </div>
  
  
</asp:Content>