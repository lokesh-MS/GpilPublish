<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="GLTLaminaMoisture.aspx.cs" Inherits="GPILWebApp.GLTLaminaMoisture" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="page-header">
        <h1>Lamina Moisture
								
        </h1>
    </div>


    <div class="row">
        <div class="col-sm-4">
            <div class="form-sm-3">
                <asp:Label ID="lblDate2" runat="server" Text="Crop" ForeColor="Black"></asp:Label>
                <div>
                    <asp:DropDownList ID="ddCrop" runat="server" Width="300px">
                    </asp:DropDownList>
                </div>
            </div>
        </div>



        <div class="col-sm-4">
            <div class="form-sm-3">
                <asp:Label ID="lblDate1" runat="server" Text="Grade" ForeColor="Black"></asp:Label>
                <div>
                    <asp:DropDownList ID="ddGrade" runat="server" Width="300px">
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <div class="col-sm-4">
            <div class="form-sm-3">
                <asp:Label ID="lblDate0" runat="server" Text="Type" ForeColor="Black"></asp:Label>
                <div>
                    <asp:DropDownList ID="ddType" runat="server" Width="300px">
                    </asp:DropDownList>
                </div>
            </div>
        </div>

    </div>

    <div class="row">
        <div class="col-sm-4">
            <div class="form-sm-3">
                <label></label>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-sm-4">
            <div class="form-sm-3">
                <asp:Label ID="lblOver12" runat="server" Text="Date" ForeColor="Black" Style="text-align: left"></asp:Label>
                <div>
                    <asp:TextBox ID="txtDate" runat="server"
                        Width="300px"></asp:TextBox>
                    <%--<ajaxcontrol:calendarextender id="CalendarExtender1" runat="server" targetcontrolid="txtDate"
                        format="yyyy-MM-dd" cssclass=" cal_Theme1" />--%>
                </div>
            </div>
        </div>



        <div class="col-sm-4">
            <div class="form-sm-3">
                <asp:Label ID="Label8" runat = "server" Text="Sample Time "  ForeColor="Black" style="text-align: left"></asp:Label>
                <div>
                    <asp:TextBox ID="txtSampleTIme" runat="server" Width="300px" ontextchanged="txtSampleTIme_TextChanged" AutoPostBack="true"></asp:TextBox>
                </div>
            </div>
        </div>

        <%--<div class="col-sm-4">
            <div class="form-sm-3">
                <asp:Label ID="lblDate" runat = "server" Text="Grade Code"  ForeColor="Black" 
                    Visible="False"></asp:Label>
                <div>
                   <asp:TextBox ID="txtGrade" runat="server" 
                    Width="300px" Visible="False" >NULL</asp:TextBox>
                </div>
            </div>
        </div>--%>
        <div class="col-sm-4">
            <div class="form-sm-3">
                <asp:Label ID="Label1" runat = "server" 
                     Text="Run No "  ForeColor="Black" style="text-align: left"></asp:Label>
                <div>
                    <asp:TextBox ID="txtRunno" runat="server" Width="300px" ></asp:TextBox>
                </div>
            </div>
        </div>

    </div>

    <div class="row">
        <div class="col-sm-4">
            <div class="form-sm-3">
                <label></label>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-sm-4">
            <div class="form-sm-3">
                <asp:Label ID="Label10" runat = "server" 
                     Text="Run Case No "  ForeColor="Black" style="text-align: left"></asp:Label>
                <div>
                    <asp:TextBox ID="txtRunCaseno" runat="server" Width="300px" ></asp:TextBox>
                </div>
            </div>
        </div>



        <div class="col-sm-4">
            <div class="form-sm-3">
                <asp:Label ID="Label3" runat = "server" 
                     Text="Time In "  ForeColor="Black" style="text-align: left"></asp:Label>
                <div>
                    <asp:TextBox ID="txttimeIN" runat="server" Width="300px" ontextchanged="txttimeIN_TextChanged" AutoPostBack="true" ></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="col-sm-4">
            <div class="form-sm-3">
                <asp:Label ID="Label4" runat = "server" 
                     Text="Time Out"  ForeColor="Black" style="text-align: left"></asp:Label>
                <div>
                    <asp:TextBox ID="txtTimeout" runat="server" Width="300px" ></asp:TextBox>
                </div>
            </div>
        </div>

    </div>

     <div class="row">
        <div class="col-sm-4">
            <div class="form-sm-3">
                <label></label>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-sm-4">
            <div class="form-sm-3">
                <asp:Label ID="Label2" runat = "server" 
                     Text="Results "  ForeColor="Black" style="text-align: left"></asp:Label>
                <div>
                    <asp:TextBox ID="txtResults" runat="server" Width="300px" ontextchanged="txtResults_TextChanged" ></asp:TextBox>
                </div>
            </div>
        </div>



        

        <div class="col-sm-4">
            <div class="form-sm-3">
                <asp:Label ID="Label7" runat = "server" 
                     Text="Griding Start Time "  ForeColor="Black" style="text-align: left"></asp:Label>
                <div>
                   <asp:TextBox ID="txtGrindingStart" runat="server" Width="300px" ></asp:TextBox>
                </div>
            </div>
        </div>

        <%--<div class="col-sm-4">
            <div class="form-sm-3">
                <asp:Label ID="Label6" runat = "server" 
                     Text="After CF"  ForeColor="Black" style="text-align: left" 
                    Visible="False"></asp:Label>
                <div>
                    <asp:TextBox ID="txtCF" runat="server" Height="21px" 
                    Width="153px" Visible="False" ></asp:TextBox>
                </div>
            </div>
        </div>--%>

        <div class="col-sm-4">
            <div class="form-sm-3">
                <asp:Label ID="Label6" runat = "server" 
                     Text="Packed Temp °C"  ForeColor="Black" style="text-align: left"></asp:Label>
                <div>
                    <asp:TextBox ID="txtPackedTemp" runat="server" Width="300px" ></asp:TextBox>
                </div>
            </div>
        </div>

    </div>

     <div class="row">
        <div class="col-sm-4">
            <div class="form-sm-3">
                <label></label>
            </div>
        </div>
    </div>

    <div class="row">
        



        <div class="col-sm-4">
            <div class="form-sm-3">
                <asp:Label ID="Label9" runat = "server" 
                     Text="Griding Stop Time "  ForeColor="Black" style="text-align: left"></asp:Label>
                <div>
                    <asp:TextBox ID="txtGrindingStop" runat="server" Width="300px" ></asp:TextBox>
                </div>
            </div>
        </div>



        <%--<div class="col-sm-4">
            <div class="form-sm-3">
                <asp:Label ID="Label6" runat = "server" 
                     Text="After CF"  ForeColor="Black" style="text-align: left" 
                    Visible="False"></asp:Label>
                <div>
                    <asp:TextBox ID="txtCF" runat="server" Height="21px" 
                    Width="153px" Visible="False" ></asp:TextBox>
                </div>
            </div>
        </div>--%>

         <div class="col-sm-1">
            <div class="form-sm-3">
                <label></label>
                <asp:Button ID="btn_Save" 
                  Font-Bold="true" runat="server" Text="Save" Width="87px" 
                  onclick="btn_Save_Click" />
            </div>
        </div>
          <div class="col-sm-1">
            <div class="form-sm-3">
                <label></label>
                <asp:Button ID="btn_Complete" 
                  Font-Bold="true" runat="server" Text="Complete" Width="87px" 
                  onclick="btn_Complete_Click" />
            </div>
        </div>
         <div class="col-sm-1">
            <div class="form-sm-3">
                <label></label>
                <asp:Button ID="btn_Clear" Font-Bold="true" runat="server" Text="Clear" 
                  Width="83px" onclick="btn_Clear_Click" />
            </div>
        </div>


    </div>



     <div class="row">
       <asp:Label ID="lblMessage" ForeColor="Red" BackColor="Yellow" Font-Size=Large Font-Bold="true" runat="server" Text=""></asp:Label>
    </div>



</asp:Content>
