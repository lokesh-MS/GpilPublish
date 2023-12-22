<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ChemistryReport.aspx.cs" Inherits="GPILWebApp.ChemistryReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">




    <div class="header">
        <h2 style="text-align: center; font-weight: bold; font-family: 'Times New Roman'; color: #438EB9">Chemistry Report</h2>
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
            <div class="form-sm-3-md-4">
                <label class="control-label">To Date</label>
                <div class="form-control-sm">
                    <asp:TextBox ID="txt_To_Date" runat="server" class="form-control" TextMode="Date">                            
                    </asp:TextBox>
                </div>
            </div>
        </div>

        <div class="col-sm-4">
            <div class="form-sm-3-md-4">
                <label class="control-label">Crop Year</label>
                <div>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCrop" AutoPostBack="true">
                        <asp:ListItem Value="0">Select To Crop</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>

    </div>


    <div class="row">
        <div class="col-sm-4">
            <div class="form-sm-3-md-4">
                <label class="control-label">Grade</label>
                <div>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlGrade" AutoPostBack="true">
                        <asp:ListItem Value="0">Select To Batch Number</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="col-sm-4">
            <div class="form-sm-3-md-4">
                <label class="control-label">Variety</label>
                <div>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlVariety" AutoPostBack="true">
                        <asp:ListItem Value="0">Select To Variety</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>

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




    <div class="col-sm-12 mb-0" style="width: 100%">
        <table>
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" HasCrystalLogo="False" />
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            </CR:CrystalReportSource>






            <tr>
                <td align="center">
                    <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" style="margin-left: 40px">
                    <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                    <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                    <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                    <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                    <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                    <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
                    <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
                    <asp:TextBox ID="TextBox14" runat="server">0</asp:TextBox>
                    <asp:TextBox ID="TextBox15" runat="server">0</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:TextBox ID="TextBox16" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" class="style1">
                    <asp:TextBox ID="TextBox17" runat="server" Style="margin-bottom: 2px">0</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:TextBox ID="TextBox18" runat="server">0</asp:TextBox>
                </td>
            </tr>



            <asp:GridView ID="GridViewSample" runat="server"
                AutoGenerateColumns="False" BorderColor="#CCCCCC" BorderStyle="Solid"
                BorderWidth="1px" Font-Names="Verdana"
                Width="75%" ForeColor="Black">
                <AlternatingRowStyle BackColor="#FFD4BA" />
                <FooterStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                <PagerStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                <HeaderStyle BackColor="#FF9E66" BorderColor="#CCCCCC" BorderStyle="Solid"
                    BorderWidth="1px" Font-Size="15px" Height="30px" />
                <RowStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                    Font-Size="13px" Height="20px" />
                <Columns>

                    <asp:TemplateField HeaderStyle-Width="25%" HeaderText="GRADE">
                        <ItemTemplate>
                            <asp:Label ID="lblCrop" runat="server" Text='<%#Eval("Grade") %>'></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle Width="25%"></HeaderStyle>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="25%" HeaderText="LOTS PACKED">
                        <ItemTemplate>
                            <asp:Label ID="lblpacked" runat="server" Text='<%#Eval("NoOfPacked") %>'></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle Width="25%"></HeaderStyle>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="25%" HeaderText="SPECIFICATION RANGE">
                        <ItemTemplate>
                            <asp:Label ID="lblspecrance" runat="server" Text='<%#Eval("SpecificationRange") %>'></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle Width="25%"></HeaderStyle>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="25%" HeaderText="CONTROL RANGE">
                        <ItemTemplate>
                            <asp:Label ID="lblontrolrange" runat="server" Text='<%#Eval("ControlRange") %>'></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle Width="25%"></HeaderStyle>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="25%" HeaderText="No OF LOTS WITHIN SPEC LIMIT">
                        <ItemTemplate>
                            <asp:Label ID="lblnospeclimit" runat="server" Text='<%#Eval("WithinSpecLimit") %>'></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle Width="25%"></HeaderStyle>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="25%" HeaderText="WITHIN SPEC LIMIT PERCENTAGE">
                        <ItemTemplate>
                            <asp:Label ID="lblwithinspecper" runat="server" Text='<%#Eval("WithinSpecPer") %>'></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle Width="25%"></HeaderStyle>

                    </asp:TemplateField>


                    <asp:TemplateField HeaderStyle-Width="25%" HeaderText="NO OF LOTS WITHIN CONTROL LIMIT">
                        <ItemTemplate>
                            <asp:Label ID="lblnocontrollimit" runat="server" Text='<%#Eval("WithinControlLimit") %>'></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle Width="25%"></HeaderStyle>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="25%" HeaderText="WITHIN CONTROL LIMIT PERCENTAGE">
                        <ItemTemplate>
                            <asp:Label ID="lblwithincontrolPer" runat="server" Text='<%#Eval("WithinControlPer") %>'></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle Width="25%"></HeaderStyle>

                    </asp:TemplateField>



                    <asp:TemplateField HeaderStyle-Width="25%" HeaderText="NO OF LOTS BELOW CONTROL LIMIT">
                        <ItemTemplate>
                            <asp:Label ID="lblbelowcount" runat="server" Text='<%#Eval("BelowControlLimit") %>'></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle Width="25%"></HeaderStyle>

                    </asp:TemplateField>





                    <asp:TemplateField HeaderStyle-Width="25%" HeaderText="BELOW CONTROL LIMIT PERCENTAGE">
                        <ItemTemplate>
                            <asp:Label ID="lblbelowper" runat="server" Text='<%#Eval("BelowControlPer") %>'></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle Width="25%"></HeaderStyle>

                    </asp:TemplateField>



                    <asp:TemplateField HeaderStyle-Width="25%" HeaderText="NO OF LOTS ABOVE CONTROL LIMIT">
                        <ItemTemplate>
                            <asp:Label ID="lblabovelimit" runat="server" Text='<%#Eval("AboveControlLimit") %>'></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle Width="25%"></HeaderStyle>

                    </asp:TemplateField>


                    <asp:TemplateField HeaderStyle-Width="25%" HeaderText="ABOVE CONTROL LIMIT PERCENTAGE">
                        <ItemTemplate>
                            <asp:Label ID="lblabovelimitper" runat="server" Text='<%#Eval("AboveControlPer") %>'></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle Width="25%"></HeaderStyle>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="25%" HeaderText="ALKALOID PERCENTAGE">
                        <ItemTemplate>
                            <asp:Label ID="lblalkaper" runat="server" Text='<%#Eval("AlkaloidPer") %>'></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle Width="25%"></HeaderStyle>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="25%" HeaderText="TRS PERCENTAGE">
                        <ItemTemplate>
                            <asp:Label ID="lbltrsper" runat="server" Text='<%#Eval("TRSPer") %>'></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle Width="25%"></HeaderStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-Width="25%" HeaderText="CL PERCENTAGE">
                        <ItemTemplate>
                            <asp:Label ID="lblclper" runat="server" Text='<%#Eval("CLPer") %>'></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle Width="25%"></HeaderStyle>


                    </asp:TemplateField>
                </Columns>
            </asp:GridView>


        </table>
    </div>










</asp:Content>
